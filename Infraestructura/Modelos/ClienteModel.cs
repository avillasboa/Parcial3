using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Modelos
{
    public class ClienteModel
    {
        internal object idPersona;

        public int idCliente { get; set; }
        public DateTime fechaIngreso { get; set; }
        public string calificacion { get; set; }
        public PersonaModel pModel { get; set; }
        public string estado { get; set; }
    }
}
