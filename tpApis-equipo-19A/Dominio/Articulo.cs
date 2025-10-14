using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dominio
{
    public class Articulo
    {
        public int IdArticulo { get; set; }

        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public Marca marca { get; set; }
        public Categoria Categoria { get; set; }
        public Imagenes Imagenes { get; set; } = new Imagenes();
        public decimal Precio { get; set; }





    }
}
