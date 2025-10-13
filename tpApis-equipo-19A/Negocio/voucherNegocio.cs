using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class voucherNegocio
    {
        public List<Voucher> Lectura()
        {
            List<Voucher> Lista = new List<Voucher>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT CodigoVoucher, IdCliente, FechaCanje, IdArticulo from Vouchers;");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Voucher aux = new Voucher();
                    aux.CodVaucher = datos.Lector["CodigoVoucher"] != DBNull.Value
                                     ? datos.Lector["CodigoVoucher"].ToString()
                                     : "";
                    aux.IdCliente = datos.Lector["IdCliente"] != DBNull.Value
                                    ? Convert.ToInt32(datos.Lector["IdCliente"])
                                    : 0;
                    aux.IdArticulo = datos.Lector["IdArticulo"] != DBNull.Value
                                     ? Convert.ToInt32(datos.Lector["IdArticulo"])
                                     : 0;
                    aux.FechaCanje = datos.Lector["FechaCanje"] != DBNull.Value
                ? Convert.ToDateTime(datos.Lector["FechaCanje"])
                : (DateTime?)null;

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


        public void MarcarCanjeo(string Codigovoucher, int idCliente, int idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("UPDATE Vouchers SET FechaCanje = @FechaCanje, IdCliente = @IdCliente, IdArticulo = @IdArticulo WHERE CodigoVoucher = @CodigoVoucher");
                datos.SetearParametro("@FechaCanje", DateTime.Now);
                datos.SetearParametro("@IdCliente", idCliente);
                datos.SetearParametro("@IdArticulo", idArticulo);
                datos.SetearParametro("@CodigoVoucher", Codigovoucher);

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

    }

}
