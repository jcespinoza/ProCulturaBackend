using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Services
{
    public interface IWriteOnlyRepository
    {
        void Archive<T>(long id) where T : IEntity;
        T Update<T>(T item) where T : IEntity;
        T Create<T>(T item) where T : IEntity;
        void ArchiveAll<T>(IEnumerable<T> list) where T : class, IEntity;
        IEnumerable<T> CreateAll<T>(IEnumerable<T> list) where T : IEntity;
    }
}