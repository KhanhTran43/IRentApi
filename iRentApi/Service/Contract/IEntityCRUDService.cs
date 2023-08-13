﻿using Data.Context;
using Domain.Model.Entity;
using iRentApi.DTO.Contract;
using iRentApi.Model.Entity.Contract;
using iRentApi.Service.ServiceException;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Reflection;

namespace iRentApi.Service.Contract
{
    public class SelectOptions
    {
        public IEnumerable<string>? Includes { get; set; }
    }

    public interface IEntityCRUDService<TEntity> : IService 
        where TEntity : EntityBase
    {
        public async Task<List<TEntity>> SelectAll(SelectOptions? options = null)
        {
            if (Context.Set<TEntity>() == null)
            {
                throw new EntitySetEmptyException();
            }

            var query = Context.Set<TEntity>().AsNoTracking();

            if (options?.Includes != null)
            {
                foreach (var navigationPath in options.Includes)
                {
                    if (typeof(TEntity).GetProperty(navigationPath) != null)
                    {
                        query = query.Include(navigationPath);
                    }
                }
            }

            return await query.ToListAsync();
        }

        public async Task<List<TSelect>> SelectAll<TSelect>(SelectOptions? options = null)
        {
            return Mapper.Map<List<TSelect>>(await SelectAll(options));
        }

        public async Task<TEntity> SelectByID(long key, SelectOptions? options = null)
        {
            if (Context.Set<TEntity>() == null)
            {
                throw new EntitySetEmptyException();
            }

            var query = Context.Set<TEntity>().AsQueryable();

            if (options != null)
            {
                foreach (var navigationPath in options.Includes)
                {
                    query = query.Include(navigationPath);
                }
            }

            return await query.SingleAsync(e => e.Id == key);
        }

        public async Task<TSelect> SelectByID<TSelect>(long key, SelectOptions? options = null)
        {
            return Mapper.Map<TSelect>(await SelectByID(key, options));
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

        public async Task<TEntity> Insert(IInsertDTO<TEntity> insert)
        {
            if (Context.Set<TEntity>() == null)
            {
                throw new EntitySetEmptyException();
            }

            var entity = Mapper.Map<TEntity>(insert);
            var entityEntry = Context.Set<TEntity>().Add(entity);
            await Context.SaveChangesAsync();

            return entityEntry.Entity;
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
    }
}