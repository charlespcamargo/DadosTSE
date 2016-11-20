using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using UFSCar.BD.BackEnd.Business;
using UFSCar.BD.BackEnd.Model;
using UFSCar.BD.Model;
using UFSCar.BD.Repository;

namespace UFSCar.BD.API.Controllers
{
    [RoutePrefix("ocupacao")]
    public class OcupacaoController : ApiController
    {

        [HttpGet]
        [Route("AutoComplete")]
        public HttpResponseMessage AutoComplete()
        {
            List<Ocupacao> lst = null;

            string termo = this.Request.GetQueryNameValuePairs().Where(c => c.Key == "id").FirstOrDefault().Value.ToUpper();
            decimal codigo = 0;
            if (decimal.TryParse(termo, out codigo))
                codigo = decimal.Parse(termo);

            var predicate = UtilEntity.True<Ocupacao>();
            if (codigo == 0)
                predicate = predicate.And(p => p.Descricao.Contains(termo));
            if (codigo > 0)
                predicate = predicate.And(p => p.ID == codigo);
            
            try
            {
                using (UnitOfWork UoW = new UnitOfWork())
                {
                    lst = UoW.OcupacaoRepository.Listar(predicate, p => p.OrderBy(n => n.Descricao));
                }

                return Request.CreateResponse(HttpStatusCode.OK, lst);
            }
            catch (ArgumentException aex)
            {
                var errorResponse = Request.CreateErrorResponse(HttpStatusCode.BadRequest, new HttpError(aex.Message));
                throw new HttpResponseException(errorResponse);
            }
            catch (Exception ex)
            {
                var errorResponse = Request.CreateErrorResponse(HttpStatusCode.Conflict, new HttpError(ex.Message));
                throw new HttpResponseException(errorResponse);
            }
        }
         

    }
}
