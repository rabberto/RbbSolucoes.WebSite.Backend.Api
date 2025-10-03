using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using RbbSolucoes.Website.Backend.Api.Domain.Entities.Shared;
using RbbSolucoes.Website.Backend.Api.Domain.Interface.Repositories;
using RbbSolucoes.Website.Backend.Api.Infrastructure.Data;

namespace RbbSolucoes.Website.Backend.Api.Infrastructure.Data.Repositories.Shared
{
    public abstract class BaseRepository<T>(MongoDbContext dbContext) : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly MongoDbContext _dbContext = dbContext;
        protected abstract IMongoCollection<T> Collection { get; }

        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            return await Collection.Find(e => e.Id == id).FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Collection.Find(_ => true).ToListAsync();
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            await Collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<T?> UpdateAsync(T entity)
        {
            await Collection.ReplaceOneAsync(e => e.Id == entity.Id, entity);

            return await GetByIdAsync(entity.Id);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await Collection.DeleteOneAsync(e => e.Id == id);

            return result.DeletedCount > 0;
        }
    }
}