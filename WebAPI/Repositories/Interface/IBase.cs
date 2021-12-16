using ImportApi.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apivistoria21.Repositories.Interface
{
    public interface IBase<T> : IRepository<T> where T : class
    {
    }
}
