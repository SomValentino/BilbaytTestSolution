using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bilbayt.Domain.Interfaces;
using Bilbayt.Data.Interfaces;
using Bilbayt.Data.Context;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Bilbayt.Data.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : IModel
    {
        private readonly IMongoCollection<TEntity> _collection;

        public  BaseRepository(IDataContextSetting dataContextSetting, string collectionName)
        {
            var client = new MongoClient(dataContextSetting.ConnectionString);
            var database = client.GetDatabase(dataContextSetting.DatabaseName);

            _collection = database.GetCollection<TEntity>(collectionName);
        }
        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity; 
        }

        public async Task DeleteAsync(string Id)
        {
            await _collection.DeleteOneAsync(x => x.Id == Id);
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return (await _collection.FindAsync<TEntity>(predicate)).ToList();

        }

        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            return (await _collection.FindAsync<TEntity>(x => true)).ToList();
        }

        public async Task<TEntity> GetAsync(string Id)
        {
            return (await _collection.FindAsync<TEntity>(x => x.Id == Id)).FirstOrDefault();
        }

        public async Task UpdateAsync(string Id, TEntity entity)
        {
            await _collection.ReplaceOneAsync(x => x.Id == Id, entity);
        }
    }
}
