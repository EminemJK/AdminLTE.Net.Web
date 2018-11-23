using System;
using System.Collections.Generic;
using System.Text;
using Banana.Uow;
using Banana.Uow.Models;

namespace AdminLTE.Domain.Service
{
    public class BaseService<T> : Repository<T> where T : class, IEntity
    {
        public BaseService()
        {
            Repository = new Repository<T>();
        }

        public Repository<T> Repository { get; private set; }
    }
}
