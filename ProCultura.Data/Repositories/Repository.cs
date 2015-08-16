namespace ProCultura.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using ProCultura.Domain.Entities;
    using ProCultura.Domain.Repositories;
    using ProCultura.Domain.UnitOfWork;

    /// <summary>
    /// Implements IRepository for Entity Framework.
    /// </summary>
    /// <typeparam name="TDbContext">DbContext which contains <see cref="TEntity"/>.</typeparam>
    /// <typeparam name="TEntity">Type of the Entity for this repository</typeparam>
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        public IUnitOfWork UnitOfWork { get; set; }

        /// <summary>
        /// Gets EF DbContext object.
        /// </summary>
        private readonly DbContext _context;

        /// <summary>
        /// Gets DbSet for given entity.
        /// </summary>
        protected virtual IDbSet<TEntity> EntitySet { get { return _context.Set<TEntity>(); } }

        public Repository(IUnitOfWork unitOfWork, DbContext dbContext)
        {
            UnitOfWork = unitOfWork;
            _context = dbContext;
        }

        /// <inheritdoc/>
        public IQueryable<TEntity> GetAll()
        {
            return EntitySet;
        }

        /// <inheritdoc/>
        public IEnumerable<TEntity> GetAllList()
        {
            return EntitySet.ToList();
        }

        /// <inheritdoc/>
        public Task<IEnumerable<TEntity>> GetAllListAsync()
        {
            return Task.FromResult(EntitySet.ToList().AsEnumerable());
        }

        /// <inheritdoc/>
        public IEnumerable<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }

        /// <inheritdoc/>
        public Task<IEnumerable<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(GetAllList(predicate));
        }

        /// <inheritdoc/>
        public T Query<T>(Func<IQueryable<TEntity>, T> queryMethod)
        {
            return queryMethod(GetAll());
        }

        /// <inheritdoc/>
        public TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        /// <inheritdoc/>
        public Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(Single(predicate));
        }

        /// <inheritdoc/>
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        /// <inheritdoc/>
        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(FirstOrDefault(predicate));
        }

        /// <inheritdoc/>
        public TEntity Insert(TEntity entity)
        {
            return EntitySet.Add(entity);
        }

        /// <inheritdoc/>
        public Task<TEntity> InsertAsync(TEntity entity)
        {
            return Task.FromResult(Insert(entity));
        }

        /// <inheritdoc/>
        public TEntity Update(TEntity entity)
        {
            AttachIfNotAttached(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        /// <inheritdoc/>
        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            return Task.FromResult(Update(entity));
        }

        /// <inheritdoc/>
        public void Delete(TEntity entity)
        {
            AttachIfNotAttached(entity);
            EntitySet.Remove(entity);
        }

        /// <inheritdoc/>
        public virtual async Task DeleteAsync(TEntity entity)
        {
            Delete(entity);
        }

        /// <inheritdoc/>
        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            foreach (var entity in GetAll().Where(predicate).ToList())
            {
                Delete(entity);
            }
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            Delete(predicate);
        }

        /// <inheritdoc/>
        public int Count()
        {
            return GetAll().Count();
        }

        /// <inheritdoc/>
        public Task<int> CountAsync()
        {
            return Task.FromResult(Count());
        }

        /// <inheritdoc/>
        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).Count();
        }

        /// <inheritdoc/>
        public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(Count(predicate));
        }

        /// <inheritdoc/>
        public long LongCount()
        {
            return GetAll().LongCount();
        }

        /// <inheritdoc/>
        public Task<long> LongCountAsync()
        {
            return Task.FromResult(this.LongCount());
        }

        /// <inheritdoc/>
        public long LongCount(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).LongCount();
        }

        /// <inheritdoc/>
        public Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(this.LongCount(predicate));
        }

        /// <inheritdoc/>
        protected virtual void AttachIfNotAttached(TEntity entity)
        {
            if (!EntitySet.Local.Contains(entity))
            {
                EntitySet.Attach(entity);
            }
        }

    }
}
