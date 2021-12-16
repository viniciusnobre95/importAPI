using ImportApi.Models;
using ImportApi.Repositories.Interface;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImportApi.Repositories.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Adicionar(T entity)
        {
            try
            {
                _context.Add(entity);
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    throw new Exception(ex.InnerException.Message);
                else
                    throw new Exception(ex.Message);
            }

        }

        public async Task<int> Alterar(T entity)
        {
            try
            {
                _context.Update(entity);
                return await _context.SaveChangesAsync();
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    throw new Exception(ex.InnerException.Message);
                else
                    throw new Exception(ex.Message);
            }
        }
        public async Task<bool> Excluir(T entity)
        {
            try
            {
                _context.Remove(entity);
                return (await _context.SaveChangesAsync()) != 0;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    throw new Exception(ex.InnerException.Message);
                else
                    throw new Exception(ex.Message);
            }
        }
    }
}
