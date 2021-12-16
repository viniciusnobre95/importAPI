using apivistoria21.Repositories.Interface;
using ImportApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImportApi.Repositories.Interface
{
    public interface IRegistroRepository : IBase<TblRegistro>
    {
        bool ExisteRegistro(int ArquivoId, string Tconst);

        Task<TblRegistro> BuscarRegistro(int ArquivoId, string Tconst);
        Task<List<TblRegistro>> Listar( int ArquivoId);
    }
}
