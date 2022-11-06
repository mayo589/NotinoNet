using System.Linq.Expressions;

namespace NotinoDotNetHW.Repositories
{
    /// <summary>
    /// Interface for main repository class used in this application
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll();

        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);

        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
