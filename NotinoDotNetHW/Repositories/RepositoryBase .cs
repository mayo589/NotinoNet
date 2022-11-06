using Microsoft.EntityFrameworkCore;
using NotinoDotNetHW.Data;
using System.Linq.Expressions;

namespace NotinoDotNetHW.Repositories
{
    /// <summary>
    /// Main repository class used in this application
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected DocumentsDb RepositoryDbContext { get; set; }

        public RepositoryBase(DocumentsDb repositoryContext)
        {
            RepositoryDbContext = repositoryContext;
        }

        public IQueryable<T> FindAll() => RepositoryDbContext.Set<T>().AsNoTracking();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            RepositoryDbContext.Set<T>().Where(expression).AsNoTracking();

        public T FindById(Guid id) =>
            RepositoryDbContext.Set<T>().Find(id);

        public void Create(T entity) => RepositoryDbContext.Set<T>().Add(entity);

        public void Update(T entity) => RepositoryDbContext.Set<T>().Update(entity);

        public void Delete(T entity) => RepositoryDbContext.Set<T>().Remove(entity);
        
        public async Task SaveChangesAsync() => await RepositoryDbContext.SaveChangesAsync();
    }
}
