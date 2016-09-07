using UFSCar.BD.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFSCar.BD.Repository
{
    public partial class UnitOfWork : UnitOfWorkBase
    { 

        private BaseRepository<ImportacaoTipoArquivo> importacaoTipoArquivoRepository;
        public BaseRepository<ImportacaoTipoArquivo> ImportacaoTipoArquivoRepository
        {
            get
            {
                if (this.importacaoTipoArquivoRepository == null)
                {
                    this.importacaoTipoArquivoRepository = new BaseRepository<ImportacaoTipoArquivo>(context);
                }
                return importacaoTipoArquivoRepository;
            }
        }


        private BaseRepository<ImportacaoArquivo> importacaoArquivoRepository;
        public BaseRepository<ImportacaoArquivo> ImportacaoArquivoRepository
        {
            get
            {
                if (this.importacaoArquivoRepository == null)
                {
                    this.importacaoArquivoRepository = new BaseRepository<ImportacaoArquivo>(context);
                }
                return importacaoArquivoRepository;
            }
        }


        private BaseRepository<ImportacaoCandidato> importacaoCandidatoRepository;
        public BaseRepository<ImportacaoCandidato> ImportacaoCandidatoRepository
        {
            get
            {
                if (this.importacaoCandidatoRepository == null)
                {
                    this.importacaoCandidatoRepository = new BaseRepository<ImportacaoCandidato>(context);
                }
                return importacaoCandidatoRepository;
            }
        }

        private BaseRepository<ImportacaoBensCandidato> importacaoBensCandidatoRepository;
        public BaseRepository<ImportacaoBensCandidato> ImportacaoBensCandidatoRepository
        {
            get
            {
                if (this.importacaoBensCandidatoRepository == null)
                {
                    this.importacaoBensCandidatoRepository = new BaseRepository<ImportacaoBensCandidato>(context);
                }
                return importacaoBensCandidatoRepository;
            }
        }
         
    }
}
