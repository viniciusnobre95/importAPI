using ImportApi.Models;
using apivistoria21.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImportApi.Repositories.Interface
{
    public interface IArquivoRepository : IBase<TblArquivo>
    {
        bool ExisteRegistro(int ArquivoId);

        Task<TblArquivo> BuscarRegistro(int ArquivoId);
        Task<List<TblArquivo>> Listar();
    }
}
