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
    [RoutePrefix("exemplo")]
    public class ExemploController : ApiController
    {

        [HttpGet]
        [Route("exemplificar")]
        public HttpResponseMessage Exemplificar()
        {
            try
            {
                List<RegistroGraficoUm> lst = ExemploBL.New.Exemplificar();
                
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
