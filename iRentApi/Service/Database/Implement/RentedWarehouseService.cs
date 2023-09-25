using AutoMapper;
using Data.Context;
using Humanizer;
using iRentApi.DTO;
using iRentApi.DTO.Contract;
using iRentApi.Helpers;
using iRentApi.Model.Entity;
using iRentApi.Model.Service.Crud;
using iRentApi.Service.Database.Contract;
using iRentApi.Service.Database.Exception;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace iRentApi.Service.Database.Implement
{
    public class RentedWarehouseService : IRentedWarehouseService
    {
        public RentedWarehouseService(IRentContext context, IMapper mapper, IOptions<AppSettings> appSettings) : base(context, mapper, appSettings)
        {
        }

        public override EntityEntry<RentedWarehouseInfo> Insert(IInsertDTO<RentedWarehouseInfo> insert)
        {
            var entityEntry = base.Insert(insert);
            entityEntry.Reference(e => e.Warehouse).Load();
            return entityEntry;
        }

        public override Task<bool> CheckWarehouseRented(long warehouseId)
        {
            DateTime now = DateTime.Now;
            return Context.RentedWarehouseInfos.Where(rw => warehouseId == rw.WarehouseId && rw.EndDate.CompareTo(now) >= 0).AnyAsync();
        }

        public override Task<List<RentedWarehouseDTO>> GetRenterRentedWarehouseList(long userId, GetStaticRequest? options = null)
        {
            return SelectAll<RentedWarehouseDTO>(options, rentedWarehouseInfo => rentedWarehouseInfo.RenterId == userId);
        }

        public override async Task<List<RentedWarehouseDTO>> GetOwnerRentedWarehouseList(long userId, GetStaticRequest? options = null)
        {
            return await SelectAll<RentedWarehouseDTO>(
                options,
                rwi => 
                    rwi.Warehouse.UserId == userId 
                    && RentedWarehouseUtility.notRentingStatuses.Contains(rwi.Status)
            );
            //var select = await Context.RentedWarehouseInfos
            //    .Where(rwi => rwi.Warehouse.UserId == userId)
            //    .Where(rwi => rentingStatuses.Contains(rwi.Status))
            //    .ToListAsync();
            //return Mapper.Map<List<RentedWarehouseDTO>>(select);
        }

        public override async Task Confirm(long rentedWarehouseId)
        {
            var rentedWarehouse = await Context.RentedWarehouseInfos.FindAsync(rentedWarehouseId) ?? throw new EntityNotFoundException();
            if (rentedWarehouse.Status == RentedWarehouseStatus.Waiting)
            {
                rentedWarehouse.Status = RentedWarehouseStatus.Confirmed;
                Context.SaveChanges();
            }
            else throw new InvalidOperationException("Invalid confirm action");
        }

        public override async Task RequestCancel(long rentedWarehouseId)
        {
            var rentedWarehouse = await Context.RentedWarehouseInfos.FindAsync(rentedWarehouseId) ?? throw new EntityNotFoundException();
            if (rentedWarehouse.Status == RentedWarehouseStatus.Waiting)
            {
                rentedWarehouse.Status = RentedWarehouseStatus.Canceling;
                Context.SaveChanges();
            }
            else
                throw new InvalidOperationException("Invalid cancel request action");
        }

        public override async Task ConfirmCancel(long rentedWarehouseId)
        {
            var rentedWarehouse = await Context.RentedWarehouseInfos.FindAsync(rentedWarehouseId) ?? throw new EntityNotFoundException();
            if (rentedWarehouse.Status == RentedWarehouseStatus.Canceling)
            {
                rentedWarehouse.Status = RentedWarehouseStatus.Canceled;
                Context.SaveChanges();
            }
            else
                throw new InvalidOperationException("Invalid cancel confirm action");
        }

        public override async Task ResolveAllStatus()
        {
            var now = DateTime.Now.AtMidnight();

            var expiredWarehouse = Context.RentedWarehouseInfos
                .Where(rwi => rwi.Status == RentedWarehouseStatus.Waiting && rwi.StartDate.CompareTo(now) <= 0).ToList();
            expiredWarehouse.ForEach(ew => ew.Status = RentedWarehouseStatus.Expired);

            var rentingWarehouse = Context.RentedWarehouseInfos
                .Where(rwi => rwi.Status == RentedWarehouseStatus.Confirmed && rwi.StartDate.CompareTo(now) <= 0).ToList();
            rentingWarehouse.ForEach(ew => ew.Status = RentedWarehouseStatus.Renting);

            Context.SaveChanges();
        }

        public override VerifyContractResult VerifyContract(string hash, string key)
        {
            var result = new VerifyContractResult();
            var rentedWarehouseInfo = Context.RentedWarehouseInfos.Include(rwi => rwi.Warehouse).SingleOrDefault(rwi => rwi.Hash == hash);
            if(rentedWarehouseInfo == null)
            {
                return result;
            } else
            {
                result.RentedWarehouseInfo = rentedWarehouseInfo;
                var dataToHash = $"{rentedWarehouseInfo.RenterId}.{rentedWarehouseInfo.WarehouseId}.{rentedWarehouseInfo.RentedDate.ToString("dd-MM-yyyy")}.{key}";
                using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(AppSettings.ContractSecret)))
                {
                    byte[] dataBytes = Encoding.UTF8.GetBytes(dataToHash);
                    byte[] hmacBytes = hmac.ComputeHash(dataBytes);

                    string fullHashHex = BitConverter.ToString(hmacBytes).Replace("-", string.Empty);

                    string truncatedHash = fullHashHex.Substring(0, 16);

                    // Truncate the received hash to the same length as the original
                    string receivedTruncatedHash = hash.Substring(0, 16); // Adjust the length as needed

                    // Compare the truncated received hash with the truncated calculated hash
                    if (receivedTruncatedHash.Equals(truncatedHash, StringComparison.OrdinalIgnoreCase))
                    {
                        result.IsValid = true;
                    }
                    else
                    {
                        result.IsValid = false;
                    }
                }
                return result;
            }
        }
    }
}
