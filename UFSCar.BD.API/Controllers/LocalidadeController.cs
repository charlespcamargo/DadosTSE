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
    [RoutePrefix("localidade")]
    public class LocalidadeController : ApiController
    {

        [HttpGet]
        [Route("AutoCompleteEstado/{regiao}")]
        public HttpResponseMessage AutoCompleteEstado(string regiao)
        {
            List<Estado> lst = null;

            string termo = this.Request.GetQueryNameValuePairs().Where(c => c.Key == "id").FirstOrDefault().Value.ToUpper();
            decimal codigo = 0;
            if (decimal.TryParse(termo, out codigo))
                codigo = decimal.Parse(termo);

            var predicate = UtilEntity.True<Estado>();
            if (codigo == 0)
                predicate = predicate.And(p => p.Nome.Contains(termo));
            if (codigo > 0)
                predicate = predicate.And(p => p.ID == codigo);

            if (!string.IsNullOrEmpty(regiao))
                predicate = predicate.And(p => p.Regiao.Equals(regiao));

            try
            {
                using (UnitOfWork UoW = new UnitOfWork())
                {
                    lst = UoW.EstadoRepository.Listar(predicate, p => p.OrderBy(n => n.Nome));
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

        [HttpGet]
        [Route("AutoCompleteMunicipio/{estado}")]
        public HttpResponseMessage AutoCompleteMunicipio(int? estadoID)
        {
            List<Cidade> lst = null;

            string termo = this.Request.GetQueryNameValuePairs().Where(c => c.Key == "id").FirstOrDefault().Value.ToUpper();
            decimal codigo = 0;
            if (decimal.TryParse(termo, out codigo))
                codigo = decimal.Parse(termo);

            var predicate = UtilEntity.True<Cidade>();
            if (codigo == 0)
                predicate = predicate.And(p => p.Nome.Contains(termo));
            if (codigo > 0)
                predicate = predicate.And(p => p.ID == codigo);

            if (estadoID.HasValue && estadoID.Value > 0)
                predicate = predicate.And(p => p.EstadoID == estadoID);

            try
            {
                using (UnitOfWork UoW = new UnitOfWork())
                {
                    lst = UoW.CidadeRepository.Listar(predicate, o => o.OrderBy(by=> by.Nome));
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
