using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Negocio
{
    public class articuloNegocio
    {
        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.Precio, M.Id AS IdMarca, M.Descripcion AS Marca, C.Id AS IdCategoria, C.Descripcion AS Categoria, I.IdArticulo AS IdArticuloImagen, I.ImagenUrl FROM Articulos A LEFT JOIN Marcas M ON A.IdMarca = M.Id LEFT JOIN Categorias C ON A.IdCategoria = C.Id LEFT JOIN Imagenes I ON A.Id = I.IdArticulo;");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.IdArticulo = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Precio = datos.Lector.GetSqlMoney(datos.Lector.GetOrdinal("Precio")).ToDecimal();

                    aux.marca = new Marca();

                    if (!(datos.Lector["IdMarca"] is DBNull))
                        aux.marca.IdMarca = (int)datos.Lector["IdMarca"];

                    if (!(datos.Lector["Marca"] is DBNull))
                        aux.marca.Descripcion = (string)datos.Lector["Marca"];
                    else aux.marca.Descripcion = "Inexistente";

                    aux.Categoria = new Categoria();

                    if (!(datos.Lector["IdCategoria"] is DBNull))
                        aux.Categoria.IdCategoria = (int)datos.Lector["IdCategoria"];

                    if (!(datos.Lector["Categoria"] is DBNull))
                        aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    else aux.Categoria.Descripcion = "Inexistente";

                    aux.Imagenes = new Imagenes();
                    if (!(datos.Lector["IdArticuloImagen"] is DBNull))
                        aux.Imagenes.IdArticulo = (int)datos.Lector["IdArticuloImagen"];

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.Imagenes.ImagenUrl = (string)datos.Lector["ImagenUrl"];



                    lista.Add(aux);
                }

                return lista;

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

        public void AgregarArticulo(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.setearConsulta(@"INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio) VALUES (@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @Precio); SELECT CAST(SCOPE_IDENTITY() AS int);");

                datos.setearParametro("@Codigo", nuevo.Codigo);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
                datos.setearParametro("@IdMarca", nuevo.marca.IdMarca);
                datos.setearParametro("@IdCategoria", nuevo.Categoria.IdCategoria);
                datos.setearParametro("@Precio", nuevo.Precio);



                int idArticulo = (int)datos.ejecutarScalar();

                datos.setearConsulta(@"INSERT INTO IMAGENES (IdArticulo, ImagenUrl) VALUES (@IdArticulo, @ImagenUrl)");
                datos.setearParametro("@IdArticulo", idArticulo);
                datos.setearParametro("@ImagenUrl", nuevo.Imagenes.ImagenUrl);
                datos.ejecutarAccion();


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

        public void modificarArticulo(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update ARTICULOS set Codigo = @codigo, Nombre = @nombre, Descripcion = @descripcion, IdMarca = @idMarca, IdCategoria = @IdCategoria, Precio = @precio where Id = @id");
                datos.setearParametro("@codigo", articulo.Codigo);
                datos.setearParametro("@nombre", articulo.Nombre);
                datos.setearParametro("@descripcion", articulo.Descripcion);
                datos.setearParametro("@idMarca", articulo.marca.IdMarca);
                datos.setearParametro("@IdCategoria", articulo.Categoria.IdCategoria);
                datos.setearParametro("@precio", articulo.Precio);
                datos.setearParametro("@id", articulo.IdArticulo);

                datos.ejecutarAccion();

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

        public void eliminar(int id)
        {

            try
            {
                AccesoDatos datos = new AccesoDatos();

                datos.setearConsulta("DELETE from ARTICULOS WHERE Id = @Id");
                datos.setearParametro("@Id", id);

                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }




        public List<Articulo> filtrar(string campo, string criterio, string filtro)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.Precio, M.Id AS IdMarca, M.Descripcion AS Marca, C.Id AS IdCategoria, C.Descripcion AS Categoria, I.IdArticulo AS IdArticuloImagen, I.ImagenUrl FROM Articulos A LEFT JOIN Marcas M ON A.IdMarca = M.Id LEFT JOIN Categorias C ON A.IdCategoria = C.Id LEFT JOIN Imagenes I ON A.Id = I.IdArticulo where ";

                switch (campo)
                {
                    case "Codigo":
                        switch (criterio)
                        {

                            case "Comienza con":
                                consulta += "Codigo like '" + filtro + "%' ";
                                break;

                            case "Termina con":
                                consulta += "Codigo like '%" + filtro + "' ";
                                break;

                            case "Contiene":
                                consulta += "Codigo like '%" + filtro + "%' ";
                                break;


                            default:
                                break;
                        }

                        break;

                    case "Nombre":
                        switch (criterio)
                        {

                            case "Comienza con":
                                consulta += "Nombre like '" + filtro + "%' ";
                                break;

                            case "Termina con":
                                consulta += "Nombre like '%" + filtro + "' ";
                                break;

                            case "Contiene":
                                consulta += "Nombre like '%" + filtro + "%' ";
                                break;


                            default:
                                break;
                        }
                        break;

                    case "Descripcion":
                        switch (criterio)
                        {

                            case "Comienza con":
                                consulta += "A.Descripcion like '" + filtro + "%' ";
                                break;

                            case "Termina con":
                                consulta += "A.Descripcion like '%" + filtro + "' ";
                                break;

                            case "Contiene":
                                consulta += "A.Descripcion like '%" + filtro + "%' ";
                                break;


                            default:
                                break;
                        }
                        break;

                    case "Marca":
                        switch (criterio)
                        {

                            case "Comienza con":
                                consulta += "M.Descripcion like '" + filtro + "%' ";
                                break;

                            case "Termina con":
                                consulta += "M.Descripcion like '%" + filtro + "' ";
                                break;

                            case "Contiene":
                                consulta += "M.Descripcion like '%" + filtro + "%' ";
                                break;


                            default:
                                break;
                        }
                        break;

                    case "Categoria":
                        switch (criterio)
                        {

                            case "Comienza con":
                                consulta += "C.Descripcion like '" + filtro + "%' ";
                                break;

                            case "Termina con":
                                consulta += "C.Descripcion like '%" + filtro + "' ";
                                break;

                            case "Contiene":
                                consulta += "C.Descripcion like '%" + filtro + "%' ";
                                break;


                            default:
                                break;
                        }
                        break;

                    case "Precio":
                        switch (criterio)
                        {

                            case "Mayor a":
                                consulta += "Precio > " + filtro;
                                break;

                            case "Menor a":
                                consulta += "Precio < " + filtro;
                                break;

                            case "Igual a":
                                consulta += "Precio = " + filtro;
                                break;


                            default:
                                break;
                        }
                        break;



                    default:
                        break;
                }

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.IdArticulo = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Precio = datos.Lector.GetSqlMoney(datos.Lector.GetOrdinal("Precio")).ToDecimal();

                    aux.marca = new Marca();

                    if (!(datos.Lector["IdMarca"] is DBNull))
                        aux.marca.IdMarca = (int)datos.Lector["IdMarca"];

                    if (!(datos.Lector["Marca"] is DBNull))
                        aux.marca.Descripcion = (string)datos.Lector["Marca"];
                    else aux.marca.Descripcion = "Inexistente";

                    aux.Categoria = new Categoria();

                    if (!(datos.Lector["IdCategoria"] is DBNull))
                        aux.Categoria.IdCategoria = (int)datos.Lector["IdCategoria"];

                    if (!(datos.Lector["Categoria"] is DBNull))
                        aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    else aux.Categoria.Descripcion = "Inexistente";

                    aux.Imagenes = new Imagenes();
                    if (!(datos.Lector["IdArticuloImagen"] is DBNull))
                        aux.Imagenes.IdArticulo = (int)datos.Lector["IdArticuloImagen"];

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.Imagenes.ImagenUrl = (string)datos.Lector["ImagenUrl"];



                    lista.Add(aux);
                }





                return lista;




            }
            catch (Exception ex)
            {

                throw ex;

            }
        }

        public bool ExisteNombre(string nombre, string codigo, int idActual = 0)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM Articulos WHERE (Nombre = @nombre OR Codigo = @codigo) AND Id != @idActual");
                datos.setearParametro("@nombre", nombre);
                datos.setearParametro("@codigo", codigo);
                datos.setearParametro("@idActual", idActual);

                int cantidad = Convert.ToInt32(datos.ejecutarScalar());
                return cantidad > 0;
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

        public bool ExisteId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            {

                try
                {
                    datos.setearConsulta("SELECT COUNT(*) FROM Articulos WHERE Id = @id");
                    datos.setearParametro("@id", id);

                    int cantidad = Convert.ToInt32(datos.ejecutarScalar());
                    return cantidad > 0;
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
}
