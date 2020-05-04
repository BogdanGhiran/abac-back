using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.GenericRepository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
    Task<List<TEntity>> GetAll();

    IQueryable<TEntity> GetAllQueryable();

    Task<TEntity> GetById(int id);

    Task Create(TEntity entity);

    Task Update(int id, TEntity entity);

    Task Delete(int id);
    }
}
