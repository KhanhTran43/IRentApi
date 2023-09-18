using AutoMapper;
using Data.Context;
using iRentApi.DTO.Contract;
using iRentApi.Helpers;
using iRentApi.Model.Entity.Contract;
using iRentApi.Model.Service.Crud;
using iRentApi.Service.Implement;
using iRentApi.Service.ServiceException;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;

namespace iRentApi.Service.Contract
{
    public class SelectOptions
    {
        public IEnumerable<string>? Includes { get; set; }
    }

    public abstract class IRentCRUDService<TEntity> : IRentService where TEntity : EntityBase
    {
        protected IRentCRUDService(IRentContext context, IMapper mapper, IOptions<AppSettings> appSettings) : base(context, mapper, appSettings)
        {
        }

        public async Task<List<TEntity>> SelectAll(GetStaticRequest? reqeust = null, Expression<Func<TEntity, bool>>? wherePredicate = null)
        {
            if (Context.Set<TEntity>() == null)
            {
                throw new EntitySetEmptyException();
            }

            var query = Context.Set<TEntity>().AsNoTracking();

            query = HandleIncludes(query, reqeust);

            if (wherePredicate != null) query = query.Where(wherePredicate);

            var result = await query.ToListAsync();

            return result;
        }

        public async Task<List<TSelect>> SelectAll<TSelect>(GetStaticRequest? request = null, Expression<Func<TEntity, bool>>? wherePredicate = null)
        {
            return Mapper.Map<List<TSelect>>(await SelectAll(request, wherePredicate));
        }

        public async Task<TEntity> SelectByID(long key, GetStaticRequest? request = null)
        {
            if (Context.Set<TEntity>() == null)
            {
                throw new EntitySetEmptyException();
            }

            var query = Context.Set<TEntity>().AsNoTracking();

            query = HandleIncludes(query, request);

            return await query.SingleAsync(e => e.Id == key);
        }

        public async Task<TSelect> SelectByID<TSelect>(long key, GetStaticRequest? request = null)
        {
            return Mapper.Map<TSelect>(await SelectByID(key, request));
        }

        public async Task<bool> ExistByID(long key)
        {
            if (Context.Set<TEntity>() == null)
            {
                throw new EntitySetEmptyException();
            }

            try
            {
                var entity = await Context.Set<TEntity>().SingleAsync(e => e.Id == key);
                return entity != null;
            }
            catch (Exception ex) when (ex is InvalidOperationException || ex is ArgumentNullException)
            {
                return false;
            }
        }

        public async Task<TResult> Insert<TResult>(IInsertDTO<TEntity> insert)
        {
            if (Context.Set<TEntity>() == null)
            {
                throw new EntitySetEmptyException();
            }

            var entity = Mapper.Map<TEntity>(insert);
            var entityEntry = Context.Set<TEntity>().Add(entity);
            await Context.SaveChangesAsync();

            return Mapper.Map<TResult>(entityEntry.Entity);
        }

        public async Task Update(IUpdateDTO<TEntity> update)
        {
            if (Context.Set<TEntity>() == null)
            {
                throw new EntityNotFoundException();
            }

            var entity = Mapper.Map<TEntity>(update);

            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public async Task Delete(long key)
        {
            if (Context.Set<TEntity>() == null)
            {
                throw new EntityNotFoundException();
            }

            var entity = await Context.Set<TEntity>().FindAsync(key) ?? throw new EntityNotFoundException();
            Context.Set<TEntity>().Remove(entity);
            await Context.SaveChangesAsync();
        }

        private IQueryable<TEntity> HandleIncludes(IQueryable<TEntity> query, GetStaticRequest? request)
        {
            var includes = request?.Includes;

            if(includes != null)
            {
                foreach (var navigationPath in includes)
                {
                    if (IsNavigationPathValid<TEntity>(navigationPath))
                    {
                        query = query.Include(navigationPath);
                    }
                    else
                    {
                        Console.WriteLine($"Invalid navigation path: {navigationPath}");
                    }
                }
            }

            return query;
        }

        private bool IsNavigationPathValid<TEntity>(string navigationPath)
        {
            var parts = navigationPath.Split('.');
            var type = typeof(TEntity);

            foreach (var part in parts)
            {
                var property = type.GetProperty(part);

                if (property == null)
                {
                    return false; // The part of the navigation path is not a valid property.
                }

                type = GetPropertyType(property);
            }

            return true; // All parts of the navigation path are valid properties.
        }

        private Type GetPropertyType(PropertyInfo property)
        {
            // Handle properties that are collections (e.g., List<T>, ICollection<T>)
            if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType) && property.PropertyType.IsGenericType)
            {
                return property.PropertyType.GetGenericArguments()[0]; // Get the generic type argument
            }

            return property.PropertyType;
        }

    }
}
