using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Services;

namespace Data
{
    public class WriteOnlyRepository<T> : IWriteOnlyRepository where T : class, IEntity
    {
        private readonly ProCulturaContext context;
        private IDbSet<T> DbSet;
        public WriteOnlyRepository()
        {
            context = new ProCulturaContext();
        }

        public void Archive<T>(long id) where T : IEntity
        {
            var entity = DbSet.FirstOrDefault(x => x.Id == id);
            if (entity == null)
                return;
            entity.Archive();
        }

        public T Update<T>(T item) where T : IEntity
        {
            throw new NotImplementedException();
        }

        public T Create<T>(T item) where T : IEntity
        {
            throw new NotImplementedException();
        }

        public void ArchiveAll<T>(IEnumerable<T> list) where T : class, IEntity
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> CreateAll<T>(IEnumerable<T> list) where T : IEntity
        {
            throw new NotImplementedException();
        }
    }
}
