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
    public class ArquivoRepository : Repository<TblArquivo>, IArquivoRepository
    {
        private readonly AppDbContext _context;

        public ArquivoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public bool ExisteRegistro(int ArquivoId)
        {
            try
            {
                return (_context.TblArquivos.Where(x => x.ArquivoId == ArquivoId).ToListAsync().Result.Count) > 0;
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

        public async Task<TblArquivo> BuscarRegistro(int ArquivoId)
        {
            try
            {
                return await _context.TblArquivos.Where(x => x.ArquivoId == ArquivoId).FirstOrDefaultAsync();
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

        public async Task<List<TblArquivo>> Listar()
        {
            try
            {
                return await _context.TblArquivos.OrderBy(o => o.ArquivoId).ToListAsync();
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
