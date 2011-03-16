namespace SharpArch.Features.NHibernate.FluentNHibernate
{
    using System;

    using global::FluentNHibernate.Automapping;

    [CLSCompliant(false)]
    public interface IAutoPersistenceModelGenerator
    {
        AutoPersistenceModel Generate();
    }
}