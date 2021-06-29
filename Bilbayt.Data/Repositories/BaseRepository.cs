using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bilbayt.Domain.Interfaces;
using Bilbayt.Data.Interfaces;
using Bilbayt.Data.Context;
using MongoDB.Driver;

namespace Bilbayt.Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : IModel
    {
        private readonly IMongoCollection<TEntity> _collection;

        public BaseRepository(IDataContextSetting dataContextSetting)
        {
            var client = new MongoClient(dataContextSetting.ConnectionString);
            var database = client.GetDatabase(dataContextSetting.DatabaseName);

            _collection = database.GetCollection<TEntity>(dataContextSetting.CollectionName);
        }
        public async Task<TEntity> Create(TEntity entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity; 
        }

        public async Task Delete(string Id)
        {
            await _collection.DeleteOneAsync(x => x.Id == Id);
        }

        public async Task<IEnumerable<TEntity>> Get()
        {
            return (await _collection.FindAsync<TEntity>(x => true)).ToList();
        }

        public async Task<TEntity> Get(string Id)
        {
            return (await _collection.FindAsync<TEntity>(x => x.Id == Id)).FirstOrDefault();
        }

        public async Task Update(string Id, TEntity entity)
        {
            await _collection.ReplaceOneAsync(x => x.Id == Id, entity);
        }
    }
}
