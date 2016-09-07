using UFSCar.BD.Model;
using UFSCar.BD.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFSCar.BD.Repository
{
    public class UnitOfWorkBase : IDisposable
    {

        internal dbUFSCarContext context = new dbUFSCarContext(ConfigModel.Conexao, ConfigModel.CompileModel);

        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                StringBuilder sbMensagem = new StringBuilder();

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        sbMensagem.AppendFormat("{0} - Error: {1} <br />", validationError.PropertyName, validationError.ErrorMessage);
                        System.Diagnostics.Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                throw new Exception(sbMensagem.ToString());
            }
            catch (System.Data.EntityException eex)
            {
                throw eex;
            }
            catch (DbUpdateException dUe)
            {
                if (dUe.InnerException != null && dUe.InnerException.InnerException != null)
                {
                    string mensagem = dUe.InnerException.InnerException.Message;

                    if (mensagem.Contains("Dependent foreign key constraint violation in a referential integrity constraint."))
                        throw new Exception("Não foi possível excluir o registro, pois ele está sendo utilizado em outro lugar!");
                    else if (mensagem.Contains("Attempt to insert duplicate key"))
                        throw new Exception("Registro duplicado - " + mensagem);
                    else if (mensagem.Contains("String or binary data would be truncated"))
                        throw new Exception("Truncate - O valor informado tem um tamanho superior ao suportado pelo campo.");
                    else
                        throw new Exception(mensagem);
                }
                else
                {
                    throw dUe;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public dbUFSCarContext GetContext()
        {
            return context;
        }
    }
}
