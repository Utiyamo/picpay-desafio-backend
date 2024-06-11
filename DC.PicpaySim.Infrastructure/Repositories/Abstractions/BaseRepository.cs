using DC.PicpaySim.Infrastructure.ORM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DC.PicpaySim.Infrastructure.Repositories.Abstractions
{
    public abstract class BaseRepository<T, TKey> where T : class
    {
        protected readonly DatabaseContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public async Task Commit()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Rollback()
        {
            try
            {
                foreach (var entry in _dbContext.ChangeTracker.Entries())
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;

                        case EntityState.Modified:
                            entry.State = EntityState.Unchanged;
                            break;

                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public async Task<T> Create(T entity)
        {
            try
            {
                _dbSet.Add(entity);

                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(TKey id)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity == null)
                    throw new Exception($"Entity with ID {id} not found.");

                _dbSet.Remove(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<T>> FindAll()
        {
            try
            {
                return await _dbSet.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> expression)
        {
            try
            {
                IQueryable<T> queryable = _dbSet.AsNoTracking();

                if (expression != null)
                    queryable = queryable.Where(expression);

                return await queryable.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> FindById(TKey id)
        {
            try
            {
                return await _dbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> Update(T entity)
        {
            try
            {
                _dbSet.Update(entity);

                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
