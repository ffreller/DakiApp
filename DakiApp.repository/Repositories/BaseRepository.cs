using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DakiApp.domain.Contracts;
using DakiApp.repository.Context;

namespace DakiApp.repository.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly DakiAppContext _dbContext;
        public BaseRepository(DakiAppContext context)
        {
            _dbContext = context;
        }

        public int Atualizar(T dados)
        {
            try
            {
                _dbContext.Set<T>().Update(dados);
                return _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public T BuscarPorId(int id, string[] includes = null)
        {
            try
            {
                var query = _dbContext.Set<T>().AsQueryable();
                if(includes != null)
                {
                    foreach(var include in includes)
                    {
                        query = query.Include(include);
                    }
                }
                var ChavePrimaria = _dbContext.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties[0];

                return query.FirstOrDefault(e => EF.Property<int>(e, ChavePrimaria.Name) == id);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }

        public int Deletar(T dados)
        {
            try
            {
                _dbContext.Set<T>().Remove(dados);
                return _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int Inserir(T dados)
        {
            try
            {
                _dbContext.Set<T>().Add(dados);
                return _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<T> Listar(string[] includes = null)
        {
            try
            {
                var query = _dbContext.Set<T>().AsQueryable();
                if(includes == null) return query.ToList();

                foreach(var item in includes)
                {
                    query = query.Include(item);
                }

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}