﻿using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;

namespace StackOverflow.DAL.NHibernate
{
    public class SessionManagerFactory : ISessionManagerFactory
    {
        public ISessionFactory _sessionFactory;
      
        public SessionManagerFactory(string connectionString)
        {
            
            if (_sessionFactory == null)
            {
                _sessionFactory = Fluently
                                 .Configure()
                                 .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString))
                                 .Mappings(m => m.FluentMappings.AddFromAssemblyOf<SessionManagerFactory>())
                                 .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
                                 .BuildSessionFactory();
            }
        }

        public ISession OpenSession()
        {
            return _sessionFactory.OpenSession();
        }
    }
}