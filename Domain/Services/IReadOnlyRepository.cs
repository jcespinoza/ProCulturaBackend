using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Domain.Entities;

namespace Domain.Services
{
    public interface IReadOnlyRepository
    {
        T First<T>(Expression<Func<T, bool>> query) where T : class, IEntity;
        T FirstOrDefault<T>(Expression<Func<T, bool>> query) where T : class, IEntity;
        T GetById<T>(long id) where T : IEntity;
        IEnumerable<T> GetAll<T>() where T : IEntity;
        IEnumerable<T> Query<T>(Expression<Func<T, bool>> expression) where T : IEntity;
    }
}