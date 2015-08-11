namespace ProCultura.Data.UnitOfWork
{
    using System.Data.Entity;

    using ProCultura.Domain.UnitOfWork;

    public class UnitOfWork : IUnitOfWork
    {
        protected DbContext Context { get; set; }

        public UnitOfWork(DbContext context)
        {
            Context = context;
        }
        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
