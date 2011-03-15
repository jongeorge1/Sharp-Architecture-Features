namespace SharpArch.Features.RavenDb
{
    using Raven.Client;

    using SharpArch.Core.DomainModel;
    using SharpArch.Core.PersistenceSupport.RavenDb;

    public class RavenDbRepository<T> : RavenDbRepositoryWithTypeId<T, int>, IRavenDbRepository<T> where T : Entity
    {
        public RavenDbRepository(IDocumentSession context) : base(context)
        {
        }
    }
}