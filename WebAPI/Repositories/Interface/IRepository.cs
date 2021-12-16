using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImportApi.Repositories.Interface
{
    public interface IRepository<T> where T : class
    {
        Task<int> Adicionar(T entity);
        Task<int> Alterar(T entity);
        Task<bool> Excluir(T entity);
    }
}
