using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFSCar.BD.Repository
{
    public partial class dbUFSCarContext : DbContext
    {
        public dbUFSCarContext(DbConnection dbConnection, DbCompiledModel compileModel) : base(dbConnection, compileModel, true)
        { 
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.AutoDetectChangesEnabled = false;

            Database.SetInitializer<dbUFSCarContext>(null);
        }

    }
}
