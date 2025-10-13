using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ArticuloNegocio
    {

        public List<Articulo> Lectura()
        {
            List<Articulo> Lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, STRING_AGG(I.ImagenUrl, '|') AS Imagenes FROM ARTICULOS A LEFT JOIN IMAGENES I ON A.Id = I.IdArticulo GROUP BY A.Id, A.Codigo, A.Nombre, A.Descripcion;");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.IdArticulo = datos.Lector["Id"] != DBNull.Value
                                    ? Convert.ToInt32(datos.Lector["Id"])
                                     : 0;
                    aux.Codigo = datos.Lector["Codigo"] != DBNull.Value
                                    ? datos.Lector["Codigo"].ToString()
                                        : ""; ;
                    aux.Nombre = datos.Lector["Nombre"] != DBNull.Value
                                     ? datos.Lector["Nombre"].ToString()
                                        : "";
                    aux.Descripcion = datos.Lector["Descripcion"] != DBNull.Value
                                        ? datos.Lector["Descripcion"].ToString()
                                        : "";

                    string urls = datos.Lector["Imagenes"] as string;
                    if (!string.IsNullOrEmpty(urls))
                        aux.Imagenes = urls.Split('|').ToList();

                    Lista.Add(aux);


                }
                return Lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }

    }
}
