using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TeduCore.Infrastructure.Enums;
using TeduCore.Infrastructure.Interfaces;
using TeduCore.Infrastructure.SharedKernel;

namespace TeduCore.Data.EF
{
    /// <summary>
    /// /// <summary>
    /// Serves as a generic base class for concrete repositories to support basic CRUD oprations on data in the system.
    /// </summary>
    /// </summary>
    /// <typeparam name="T">The type of entity this repository works with. Must be a class inheriting DomainEntity</typeparam>
    /// <typeparam name="K">The type of primary key</typeparam>
    public class EFRepository<T, K> : IRepository<T, K>, IDisposable where T : DomainEntity<K>
    {
        protected readonly AppDbContext DbContext;

        public EFRepository(AppDbContext dbContext)
        {
            DbContext = dbContext;
        }

        /// <summary>
        /// Finds an item by its unique ID.
        /// </summary>
        /// <param name="id">The unique ID of the item in the database.</param>
        /// <param name="includeProperties">An expression of additional properties to eager load. For example: x => x.SomeCollection, x => x.SomeOtherCollection.</param>
        /// <returns>The requested item when found, or null otherwise.</returns>
        public virtual T FindById(K id, params Expression<Func<T, object>>[] includeProperties)
        {
            return FindAll(includeProperties).SingleOrDefault(x => x.Id.Equals(id));
        }

        /// <summary>
        /// Returns an IQueryable of all items of type T.
        /// </summary>
        /// <param name="includeProperties">An expression of additional properties to eager load. For example: x => x.SomeCollection, x => x.SomeOtherCollection.</param>
        /// <returns>An IQueryable of the requested type T.</returns>
        public virtual IQueryable<T> FindAll(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> items = DbContext.Set<T>();

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    items = items.Include(includeProperty);
                }
            }
            return items;
        }

        /// <summary>
        /// Returns an IQueryable of items of type T.
        /// </summary>
        /// <param name="predicate">A predicate to limit the items being returned.</param>
        /// <param name="includeProperties">An expression of additional properties to eager load. For example: x => x.SomeCollection, x => x.SomeOtherCollection.</param>
        /// <returns>An IEnumerable of the requested type T.</returns>
        public IQueryable<T> FindAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> items = DbContext.Set<T>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    items = items.Include(includeProperty);
                }
            }
            return items.Where(predicate);
        }

        /// <summary>
        /// Adds an entity to the underlying DbContext.
        /// </summary>
        /// <param name="entity">The entity that should be added.</param>
        public virtual void Add(T entity)
        {
            DbContext.Set<T>().Add(entity);
        }

        public virtual void Update(T entity, params Expression<Func<T, object>>[] updatedProperties)
        {
            var dbEntityEntry = DbContext.Entry(entity);
            if (updatedProperties.Any())
            {
                //update explicitly mentioned properties
                foreach (var property in updatedProperties)
                {
                    dbEntityEntry.Property(property).IsModified = true;
                }
            }
            else
            {
                var databaseEntity = DbContext.Set<T>().AsNoTracking().Single(p => p.Id.Equals(entity.Id));
                var databaseEntry = DbContext.Entry(databaseEntity);
                //no items mentioned, so find out the updated entries
                foreach (var property in dbEntityEntry.Metadata.GetProperties())
                {
                    var proposedValue = dbEntityEntry.Property(property.Name).CurrentValue;
                    var originalValue = databaseEntry.Property(property.Name).OriginalValue;

                    if (proposedValue !=null  && !proposedValue.Equals(originalValue))
                        dbEntityEntry.Property(property.Name).IsModified = true;

                    if (originalValue != null && !originalValue.Equals(proposedValue))
                        dbEntityEntry.Property(property.Name).IsModified = true;
                }
            }

            //DbContext.Set<T>().Attach(entity);
            //DbContext.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Removes an entity from the underlying DbContext.
        /// </summary>
        /// <param name="entity">The entity that should be removed.</param>
        public virtual void Remove(T entity)
        {
            DbContext.Set<T>().Remove(entity);
        }

        /// <summary>
        /// Removes an entity from the underlying DbContext. Calls <see cref="FindById" /> to resolve the item.
        /// </summary>
        /// <param name="id">The ID of the entity that should be removed.</param>
        public virtual void Remove(K id)
        {
            Remove(FindById(id));
        }

        /// <summary>
        /// Disposes the underlying data context.
        /// </summary>
        public void Dispose()
        {
            if (DbContext != null)
            {
                DbContext.Dispose();
            }
        }

        public T FindSingle(Expression<Func<T, bool>> predicate)
        {
            return FindAll(predicate).SingleOrDefault();
        }

        public IQueryable<T> FindAll(Expression<Func<T, bool>> predicate, Func<T, bool> orderBy,
            SortDirection sortDirection, int pageIndex, int pageSize, out int total,
            params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> items = DbContext.Set<T>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    items = items.Include(includeProperty);
                }
            }
            var query = items.Where(predicate);
            total = query.Count();

            if (sortDirection == SortDirection.Ascending)
            {
                return query.OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsQueryable();
            }
            else
            {
                return query.OrderByDescending(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsQueryable();
            }
        }

        public T FindSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            return FindAll(predicate, includeProperties).SingleOrDefault();
        }

        public void RemoveMultiple(List<T> entities)
        {
            DbContext.Set<T>().RemoveRange(entities);
        }
    }
}