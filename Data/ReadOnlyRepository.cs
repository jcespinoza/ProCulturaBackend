using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Services;

namespace Data
{
    public class ReadOnlyRepository : IReadOnlyRepository
    {
        public T First<T>(Expression<Func<T, bool>> query) where T : class, IEntity
        {
            throw new NotImplementedException();
        }

        public T FirstOrDefault<T>(Expression<Func<T, bool>> query) where T : class, IEntity
        {
            throw new NotImplementedException();
        }

        public T GetById<T>(long id) where T : IEntity
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll<T>() where T : IEntity
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Query<T>(Expression<Func<T, bool>> expression) where T : IEntity
        {
            throw new NotImplementedException();
        }
    }
}
