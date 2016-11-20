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


        private BaseRepository<ImportacaoLegenda> importacaoLegendaRepository;
        public BaseRepository<ImportacaoLegenda> ImportacaoLegendaRepository
        {
            get
            {
                if (this.importacaoLegendaRepository == null)
                {
                    this.importacaoLegendaRepository = new BaseRepository<ImportacaoLegenda>(context);
                }
                return importacaoLegendaRepository;
            }
        }


        private BaseRepository<ImportacaoVaga> importacaoVagaRepository;
        public BaseRepository<ImportacaoVaga> ImportacaoVagaRepository
        {
            get
            {
                if (this.importacaoVagaRepository == null)
                {
                    this.importacaoVagaRepository = new BaseRepository<ImportacaoVaga>(context);
                }
                return importacaoVagaRepository;
            }
        }



        private BaseRepository<Cargo> _CargoRepository;
        public BaseRepository<Cargo> CargoRepository
        {
            get
            {
                if (this._CargoRepository == null)
                {
                    this._CargoRepository = new BaseRepository<Cargo>(context);
                }
                return _CargoRepository;
            }
        }

        private BaseRepository<Eleicao> _EleicaoRepository;
        public BaseRepository<Eleicao> EleicaoRepository
        {
            get
            {
                if (this._EleicaoRepository == null)
                {
                    this._EleicaoRepository = new BaseRepository<Eleicao>(context);
                }
                return _EleicaoRepository;
            }
        }

        private BaseRepository<EleicaoCargo> _EleicaoCargoRepository;
        public BaseRepository<EleicaoCargo> EleicaoCargoRepository
        {
            get
            {
                if (this._EleicaoCargoRepository == null)
                {
                    this._EleicaoCargoRepository = new BaseRepository<EleicaoCargo>(context);
                }
                return _EleicaoCargoRepository;
            }
        }

        private BaseRepository<Cidade> _CidadeRepository;
        public BaseRepository<Cidade> CidadeRepository
        {
            get
            {
                if (this._CidadeRepository == null)
                {
                    this._CidadeRepository = new BaseRepository<Cidade>(context);
                }
                return _CidadeRepository;
            }
        }

        private BaseRepository<Pais> _PaisRepository;
        public BaseRepository<Pais> PaisRepository
        {
            get
            {
                if (this._PaisRepository == null)
                {
                    this._PaisRepository = new BaseRepository<Pais>(context);
                }
                return _PaisRepository;
            }
        }

        private BaseRepository<Estado> _EstadoRepository;
        public BaseRepository<Estado> EstadoRepository
        {
            get
            {
                if (this._EstadoRepository == null)
                {
                    this._EstadoRepository = new BaseRepository<Estado>(context);
                }
                return _EstadoRepository;
            }
        }


        private BaseRepository<Ocupacao> _OcupacaoRepository;
        public BaseRepository<Ocupacao> OcupacaoRepository
        {
            get
            {
                if (this._OcupacaoRepository == null)
                {
                    this._OcupacaoRepository = new BaseRepository<Ocupacao>(context);
                }
                return _OcupacaoRepository;
            }
        }

    }
}
