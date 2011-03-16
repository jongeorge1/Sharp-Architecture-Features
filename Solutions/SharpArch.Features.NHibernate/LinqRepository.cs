﻿namespace SharpArch.Features.NHibernate
{
    using System.Linq;

    using global::NHibernate.Linq;

    using SharpArch.Domain.PersistenceSupport;
    using SharpArch.Domain.Specifications;

    public class LinqRepository<T> : NHibernateRepository<T>, ILinqRepository<T>
    {
        public override void Delete(T target)
        {
            this.Session.Delete(target);
        }

        public IQueryable<T> FindAll()
        {
            return this.Session.Query<T>();
        }

        public IQueryable<T> FindAll(ILinqSpecification<T> specification)
        {
            return specification.SatisfyingElementsFrom(this.Session.Query<T>());
        }

        public T FindOne(int id)
        {
            return this.Session.Get<T>(id);
        }

        public T FindOne(ILinqSpecification<T> specification)
        {
            return specification.SatisfyingElementsFrom(this.Session.Query<T>()).SingleOrDefault();
        }

        public void Save(T entity)
        {
            try
            {
                this.Session.Save(entity);
            }
            catch
            {
                if (this.Session.IsOpen)
                {
                    this.Session.Close();
                }

                throw;
            }

            this.Session.Flush();
        }

        public void SaveAndEvict(T entity)
        {
            this.Save(entity);
            this.Session.Evict(entity);
        }
    }
}