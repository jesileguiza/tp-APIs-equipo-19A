using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class ClienteNegocio
    {

        public List<Cliente> Lectura()
        {

            List<Cliente> lista = new List<Cliente>();
            AccesoDatos data = new AccesoDatos();

            try
            {
                data.SetearConsulta("select Id, Documento, Nombre, Apellido, Email, Direccion, Ciudad, CP from Clientes;");
                data.EjecutarLectura();

                while (data.Lector.Read())
                {
                    Cliente aux = new Cliente();

                    aux.Documento = data.Lector["Documento"] != DBNull.Value
                        ? data.Lector["Documento"].ToString() : "";
                    aux.Nombre = data.Lector["Nombre"] != DBNull.Value
                        ? data.Lector["Nombre"].ToString() : "";
                    aux.IdCliente = data.Lector["Id"] != DBNull.Value
                        ? Convert.ToInt32(data.Lector["Id"]) : 0;
                    aux.Apellido = data.Lector["Apellido"] != DBNull.Value
                        ? data.Lector["Apellido"].ToString() : "";
                    aux.Email = data.Lector["Email"] != DBNull.Value
                        ? data.Lector["Email"].ToString() : "";
                    aux.Ciudad = data.Lector["Ciudad"] != DBNull.Value
                        ? data.Lector["Ciudad"].ToString() : "";
                    aux.CP = data.Lector["CP"] != DBNull.Value
                        ? Convert.ToInt32(data.Lector["CP"]) : 0;
                    aux.Direccion = data.Lector["Direccion"] != DBNull.Value
                        ? data.Lector["Direccion"].ToString() : "";

                    lista.Add(aux);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                data.cerrarConexion();
            }

            return lista;
        }






        public void AgregarCliente(Cliente nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("INSERT INTO Clientes (Documento, Nombre, Apellido, Email, Direccion, Ciudad, CP) " +
                                     "VALUES (@Documento, @Nombre, @Apellido, @Email, @Direccion, @Ciudad, @CP)");
                datos.SetearParametro("@Documento", nuevo.Documento);
                datos.SetearParametro("@Nombre", nuevo.Nombre);
                datos.SetearParametro("@Apellido", nuevo.Apellido);
                datos.SetearParametro("@Email", nuevo.Email);
                datos.SetearParametro("@Direccion", nuevo.Direccion);
                datos.SetearParametro("@Ciudad", nuevo.Ciudad);
                datos.SetearParametro("@CP", nuevo.CP);

                datos.EjecutarAccion();
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

        public void Agregar(Cliente nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("INSERT INTO Clientes (Documento, Nombre, Apellido, Email, Direccion, Ciudad, CP) " +
                                     "VALUES (@Documento, @Nombre, @Apellido, @Email, @Direccion, @Ciudad, @CP)");
                datos.SetearParametro("@Documento", nuevo.Documento);
                datos.SetearParametro("@Nombre", nuevo.Nombre);
                datos.SetearParametro("@Apellido", nuevo.Apellido);
                datos.SetearParametro("@Email", nuevo.Email);
                datos.SetearParametro("@Direccion", nuevo.Direccion);
                datos.SetearParametro("@Ciudad", nuevo.Ciudad);
                datos.SetearParametro("@CP", nuevo.CP);

                datos.EjecutarAccion();
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

        public bool ValidacionxDNI(string dni)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT COUNT(*) FROM Clientes WHERE Documento = @DNI");
                datos.SetearParametro("@DNI", dni);
                datos.EjecutarLectura();

                if (datos.Lector.Read())
                {
                    int cantidad = Convert.ToInt32(datos.Lector[0]);
                    return cantidad > 0;
                }

                return false;
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
