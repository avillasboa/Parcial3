using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Modelos
{
    public class CuentaModel
    {
        public int idCuenta { get; set; }
        public string nroCuenta { get; set; }
        public DateTime fechaAlta { get; set; }
        public string tipoCuenta { get; set; }
        public string estado { get; set; }
        public ClienteModel clmodel { get; set; }
        public string nroContrato { get; set; }
        public string promedioAcreditacion { get; set; }
        public string moneda { get; set; }
        public double saldo { get; set; }
        public double costoMantenimiento { get; set; }
    }
}
