using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Home
{
    public class EntidadTipoCambio
    {
        public void ReplaceNull()
        {
            tipoCambioID = tipoCambioID ?? 0;
        }
        public int? tipoCambioID { get; set; }
        public decimal tipoCambio { get; set; }
    }
}
