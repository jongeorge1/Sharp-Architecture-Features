namespace SharpArch.Features.RavenDb
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Raven.Client;

    using SharpArch.Core.DomainModel;
    using SharpArch.Core.PersistenceSupport.RavenDb;

    public class RavenDbRepositoryWithTypeId<T, TIdT> : IRavenDbRepositoryWithTypeId<T, TIdT> where T : Entity
    {
        private readonly IDocumentSession context;

        public RavenDbRepositoryWithTypeId(IDocumentSession context)
        {
            this.context = context;
        }

        public IEnumerable<T> FindAll(Func<T, bool> where)
        {
            return this.context.Query<T>().Where<T>(where);
        }

        public T FindOne(Func<T, bool> where)
        {
            var foundList = this.context.Query<T>().Where<T>(where);

            if (foundList.Count() > 1)
            {
                throw new InvalidOperationException("The query returned more than one result. Please refine your query.");
            }

            if (foundList.Count() == 1)
            {
                return foundList.First();
            }

            return default(T);
        }

        public T First(Func<T, bool> where)
        {
            return this.context.Query<T>().First<T>(where);
        }

        public T Get(TIdT id)
        {
            return this.FindOne(w => Equals(w.Id, id));
        }

        public IList<T> GetAll()
        {
            return this.context.Query<T>().ToList();
        }

        public T SaveOrUpdate(T entity)
        {
            this.context.Store(entity);
            this.context.SaveChanges();

            return entity;
        }

        public void Delete(T entity)
        {
            this.context.Delete(entity);
            this.context.SaveChanges();
        }
    }
}