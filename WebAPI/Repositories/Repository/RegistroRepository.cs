using ImportApi.Models;
using ImportApi.Repositories.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImportApi.Repositories.Repository
{
    public class RegistroRepository : Repository<TblRegistro>, IRegistroRepository
    {
        private readonly AppDbContext _context;

        public RegistroRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public bool ExisteRegistro(int ArquivoId, string Tconst)
        {
            try
            {
                return (_context.TblRegistros.Where(x => x.ArquivoId == ArquivoId && x.Tconst == Tconst).ToListAsync().Result.Count) > 0;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TblRegistro> BuscarRegistro(int ArquivoId, string Tconst)
        {
            try
            {
                return await _context.TblRegistros.Where(x => x.ArquivoId == ArquivoId && x.Tconst == Tconst).FirstOrDefaultAsync();
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<TblRegistro>> Listar(int ArquivoId)
        {
            try
            {
                return await _context.TblRegistros.Where(x => x.ArquivoId == ArquivoId).OrderBy(o => o.Tconst).ToListAsync();
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
