using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Transactions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace UFSCar.BD.Repository
{
    public class BaseRepository<T> where T : class
    {
        internal DbContext context;
        internal DbSet<T> dbSet;


        public BaseRepository(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public virtual List<T> Listar(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                      string includeProperties = "", int pagina = -1, int qtdRegistros = -1)
        {
            if (pagina > -1 && qtdRegistros > -1)//redefine a página
            {
                pagina = (pagina - 1) * qtdRegistros;
            }

            if (context.Database.Connection.State != ConnectionState.Closed)
            {
                Thread.Sleep(2000);
                return Listar(filter, orderBy, includeProperties, pagina, qtdRegistros);
            }
            else
            {
                try
                {
                    List<T> retorno = null;
                    IQueryable<T> query = dbSet;
                    if (filter != null)
                    {
                        query = query.Where(filter);
                    }

                    foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProperty);
                    }

                    if (orderBy != null)
                    {
                        if (pagina > -1 && qtdRegistros > -1)
                            retorno = orderBy(query).Skip(pagina).Take(qtdRegistros).ToList();
                        else
                            retorno = orderBy(query).ToList();
                    }
                    else
                    {
                        if (pagina > -1 && qtdRegistros > -1)
                        {
                            if (orderBy == null)
                                throw new EntityException("Ao informar uma paginação a opção OrderBy é obrigatória");
                            else
                                retorno = query.Skip(pagina).Take(qtdRegistros).ToList();
                        }
                        else
                            retorno = query.ToList();
                    }

                    return retorno;
                }
                catch (Exception ex)
                {
                    if (context.Database.Connection.State != ConnectionState.Closed)
                    {
                        Thread.Sleep(5000);
                        return Listar(filter, orderBy, includeProperties, pagina, qtdRegistros);
                    }
                    else
                    {
                        if (context.Database.Connection.State == ConnectionState.Closed)//tenta mais uma vez
                        {
                            Console.Write("O");
                            Thread.Sleep(8000);
                            return Listar(filter, orderBy, includeProperties, pagina, qtdRegistros);
                        }
                        else
                            throw new ApplicationException("ERRO NA CONEXÃO - " + context.Database.Connection.State);
                    }
                }

            }
        }

        public virtual T Carregar(object id)
        {
            return dbSet.Find(id);
        }

        public virtual T Carregar(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            return this.Listar(filter, orderBy, includeProperties, 1, 1).FirstOrDefault();
        }

        public int Contar(Expression<Func<T, bool>> predicate)
        {
            try
            {
                if (context.Database.Connection.State != ConnectionState.Closed)
                {
                    Thread.Sleep(2000);
                    return Contar(predicate);
                }
                else
                    return dbSet.Count(predicate);
            }
            catch (Exception)
            {
                if (context.Database.Connection.State != ConnectionState.Closed)
                {
                    Thread.Sleep(5000);
                    return Contar(predicate);
                }
                else
                {
                    if (context.Database.Connection.State == ConnectionState.Closed)//tenta mais uma vez
                    {
                        Console.Write("O");
                        Thread.Sleep(8000);
                        return Contar(predicate);
                    }
                    else
                        throw new ApplicationException("ERRO NA CONEXÃO CONTAR - " + context.Database.Connection.State);
                }
            }

        }

        public virtual void Inserir(T entity)
        {
            if (entity != null)
                dbSet.Add(entity);
        }

        public virtual void Excluir(object id)
        {
            T entityToDelete = dbSet.Find(id);
            Excluir(entityToDelete);
        }

        public virtual void Excluir(T entityToDelete)
        {
            if (context.Entry(entityToDelete).State == System.Data.Entity.EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Alterar(T entityToUpdate)
        {
            if (entityToUpdate != null)
            {
                var entry = context.Entry<T>(entityToUpdate);
                if (entry.State == System.Data.Entity.EntityState.Detached)
                {
                    dbSet.Attach(entityToUpdate);
                    context.Entry(entityToUpdate).State = System.Data.Entity.EntityState.Modified;
                }
            }
        }

        public virtual void Alterar(T entity, string nomeChave)
        {
            if (entity != null)
            {
                var entry = context.Entry<T>(entity);
                if (entry != null)
                {
                    object pkId = dbSet.Create().GetType().GetProperty(nomeChave).GetValue(entity);
                    if (pkId != null)
                    {
                        if (entry.State == System.Data.Entity.EntityState.Detached)
                        {
                            var set = context.Set<T>();
                            T attachedEntity = set.Find(pkId);  // access the key
                            if (attachedEntity != null)
                            {
                                var attachedEntry = context.Entry(attachedEntity);
                                attachedEntry.CurrentValues.SetValues(entity);
                            }
                            else
                            {
                                entry.State = System.Data.Entity.EntityState.Modified; // attach the entity
                            }
                        }
                    }
                }
            }
        }


    }
}
