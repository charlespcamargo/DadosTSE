using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFSCar.BD.Commom.Util.Tools
{
    public class ValidadorErros
    {
        public List<ModeloValidacao> ListaErros { get; set; }

        public bool ErroSomenteSistema { get; set; }

        public StringBuilder Mensagem { get; set; }

        public void VerificarErros()
        {
            var itemAgrupado = ListaErros.GroupBy(x => x.TipoValidacao);
            Mensagem = new StringBuilder();
            foreach (var item in itemAgrupado)
            {
                item.ToList().ForEach(x =>
                {
                    Mensagem.Append("\t");
                    Mensagem.Append("Coluna - ");
                    Mensagem.Append(x.Atributo);
                    Mensagem.Append(": ");

                    var tipo = item.ToList().Select(y => y.TipoValidacao).FirstOrDefault();
                    switch (tipo)
                    {
                        case EnumTipoValidacao.ConversaoDecimal:
                            Mensagem.Append("Erro na conversão para valor numérico ");
                            break;
                        case EnumTipoValidacao.ConversaoData:
                            Mensagem.Append("Erro na conversão de Data.");
                            break;
                        case EnumTipoValidacao.TamanhoCampo:
                            Mensagem.Append("O tamanho da informação é maior que o esperado.");
                            break;
                        case EnumTipoValidacao.ConsultaCodigoRegistro:
                            Mensagem.Append("Código não encontrado.");
                            break;
                        case EnumTipoValidacao.ConversaoBooleano:
                            Mensagem.Append("Erro ao tentar verificar o valor. Informe (SIM/NÃO) no registro.");
                            break;
                    }
                    if (!string.IsNullOrEmpty(x.MensagemCustom))
                    {
                        Mensagem.Append(x.MensagemCustom);
                    }
                    Mensagem.Append(System.Environment.NewLine);
                });
            }

            ErroSomenteSistema = (ListaErros.Count > 0 && !ListaErros.Any(x => x.TipoErro == EnumTipoErro.Planilha));
        }
    }

    public class ModeloValidacao
    {
        public string Atributo { get; set; }
        public EnumTipoValidacao TipoValidacao { get; set; }
        public string MensagemCustom { get; set; }
        public EnumTipoErro TipoErro { get; set; }
    }

    public enum EnumTipoValidacao
    {
        ConversaoDecimal = 0,
        ConversaoData = 1,
        TamanhoCampo = 2,
        ConversaoBooleano = 3,
        ConsultaCodigoRegistro = 4
    }

    public enum EnumTipoErro
    {
        Sistema = 0,
        Planilha = 1
    }
}
