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
    [RoutePrefix("consultas")]
    public class ConsultasController : ApiController
    {

        [HttpPost]
        [Route("patrimonio")]
        public HttpResponseMessage Patrimonio([FromBody] AnaliseFiltro filtro)
        {
            List<ANALISE1_1> lst = null;

            try
            {
                lst = ConsultasBL.New.EvolucaoPatrimonial(filtro);

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
        [Route("patrimoniografico/{cpf}")]
        public HttpResponseMessage PatrimonioGrafico(string cpf)
        {
            try
            {
                //lst = ConsultasBL.New.EvolucaoPatrimonialGrafico(cpf);

                return Request.CreateResponse(HttpStatusCode.OK);
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

        [HttpPost]
        [Route("escolaridade")]
        public HttpResponseMessage Escolaridade([FromBody] AnaliseFiltro filtro)
        {
            List<dynamic> lst = null;

            try
            {
                //lst = ConsultasBL.New.EvolucaoPatrimonial(filtro);

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


        [HttpPost]
        [Route("porsexo")]
        public HttpResponseMessage PorSexo([FromBody] AnaliseFiltro filtro)
        {
            List<ANALISE3_2> lst = null;

            try
            {
                lst = ConsultasBL.New.PorSexo(filtro);

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
