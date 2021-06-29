using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bilbayt.Domain.Interfaces;

namespace Bilbayt.Data.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : IModel
    {
        Task<IEnumerable<TEntity>> Get();
        Task<TEntity> Get(string Id);
        Task<TEntity> Create(TEntity entity);
        Task Update(string Id, TEntity entity);
        Task Delete(string Id);

    }
}
