using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bilbayt.Domain.Interfaces;

namespace Bilbayt.Data.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : IModel
    {
        Task<IEnumerable<TEntity>> GetAsync();
        Task<TEntity> GetAsync(string Id);
        Task<TEntity> CreateAsync(TEntity entity);
        Task UpdateAsync(string Id, TEntity entity);
        Task DeleteAsync(string Id);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

    }
}
