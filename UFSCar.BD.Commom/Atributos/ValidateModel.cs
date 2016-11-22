using UFSCar.BD.Commom.Erros;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;

namespace UFSCar.BD.Commom.Atributos
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ModelState.IsValid == false)
            {
                List<ErrorMessage> validacoes = new List<ErrorMessage>();
                var errorList = this.RetornaListaErros(actionContext.ModelState);

                if (errorList != null && errorList.Count() > 0)
                {
                    foreach (var error in errorList)
                    {
                        foreach (ModelError errorModel in error.Value.Errors)
                        {
                            var idControle = RecuperaIDControle(error.Key);
                            if (validacoes.Count(d => d.IdControle == idControle) == 0)
                            {
                                if (validacoes.Count(d => d.MensagemValidacao == errorModel.ErrorMessage) == 0)
                                {
                                    validacoes.Add(new ErrorMessage()
                                    {
                                        IdControle = idControle,
                                        MensagemValidacao = (string.IsNullOrEmpty(errorModel.ErrorMessage) ? "Valor incorreto para o campo!" : errorModel.ErrorMessage)
                                    });
                                }
                            }
                        }
                    }
                }

                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.MethodNotAllowed, validacoes);
            }

        }

        private static string RecuperaIDControle(string NomePropriedade)
        {
            string nome = NomePropriedade;
            string[] props = NomePropriedade.Split('.');
            if (props.Length > 1)
                nome = props[1];

            return nome;
        }

        private IEnumerable<KeyValuePair<string, ModelState>> RetornaListaErros(ModelStateDictionary estadoModel)
        {
            ///Para a operação de list e validando os campos com DataAnnotation de requerid devem ser ignorados
            var errorList = from item in estadoModel
                            where item.Value.Errors.Any()
                            select item;

            return errorList;
        }
    }
}

