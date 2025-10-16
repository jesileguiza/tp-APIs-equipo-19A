using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dominio;
using Microsoft.Ajax.Utilities;
using Negocio;
using tp_Apis_equipo_19A.Dto_s;

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
        public HttpResponseMessage Post([FromBody]ArticuloDto articulo)
        {
            articuloNegocio negocio = new articuloNegocio();
            Articulo nuevo = new Articulo();

            nuevo.Codigo = articulo.Codigo;
            nuevo.Nombre = articulo.Nombre;
            nuevo.Descripcion = articulo.Descripcion;
            nuevo.marca = new Marca { IdMarca = articulo.IdMarca };
            nuevo.Categoria = new Categoria { IdCategoria = articulo.IdCategoria };
            nuevo.Imagenes = new Imagenes { ImagenUrl = articulo.UrlImagen };
            nuevo.Precio = articulo.Precio;

            negocio.AgregarArticulo(nuevo);

            return Request.CreateResponse(HttpStatusCode.OK, "Código: 200 OK");

        }



        // POST: api/Articulo
        [HttpPost]
        [Route("Api/Articulo/AgregarImagenes")]
        public IHttpActionResult AgregarImagenes ([FromBody] ImagenesDto Dto)
        {
            if (Dto == null || Dto.Urls == null || !Dto.Urls.Any()) return BadRequest("Debe proporcionar una lista de imagenes");

            try
            {
                articuloNegocio negocio= new articuloNegocio(); 
                negocio.AgregarImagenes(Dto.Urls, Dto.Id);
                return Ok("Codigo: 200 OK");

            }
            catch (Exception ex) {   
                return InternalServerError(ex);
            }



        }



        // PUT: api/Articulo/5
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] ArticuloDto articulo)
        {
            try
            {

                articuloNegocio negocio = new articuloNegocio();
                Articulo nuevo = new Articulo();

                nuevo.Codigo = articulo.Codigo;
                nuevo.Nombre = articulo.Nombre;
                nuevo.Descripcion = articulo.Descripcion;
                nuevo.marca = new Marca { IdMarca = articulo.IdMarca };
                nuevo.Categoria = new Categoria { IdCategoria = articulo.IdCategoria };
                nuevo.Imagenes = new Imagenes { ImagenUrl = articulo.UrlImagen };
                nuevo.Precio = articulo.Precio;
                nuevo.IdArticulo = id;

                negocio.modificarArticulo(nuevo);
                return Ok("Codigo: 200 OK");

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        // DELETE: api/Articulo/5
        public HttpResponseMessage Delete(int id)
        {
            articuloNegocio negocio = new articuloNegocio();
            negocio.eliminar(id);
            return Request.CreateResponse(HttpStatusCode.OK, "Código: 200 OK");
        }
    }
}
