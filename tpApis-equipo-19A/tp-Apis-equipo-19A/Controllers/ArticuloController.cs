using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dominio;
using Microsoft.Ajax.Utilities;
using Negocio;

namespace tp_Apis_equipo_19A.Controllers
{
    public class ArticuloController : ApiController
    {
        // GET: api/Articulo
        public IEnumerable<Articulo> Get()
        {
            articuloNegocio negocio = new articuloNegocio();

            return negocio.listar();

        }

        // GET: api/Articulo/5
        public Articulo Get(int id)
        {
            articuloNegocio negocio = new articuloNegocio();
            List <Articulo> lista = negocio.listar();

            return lista.Find(x=> x.IdArticulo == id);
        }

        // POST: api/Articulo
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Articulo/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Articulo/5
        public void Delete(int id)
        {
        }
    }
}
