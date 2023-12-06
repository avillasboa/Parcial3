using System;
namespace Infraestructura.Modelos
{
    public class PersonaModel
    {
        public object id_persona;
        internal CiudadModel ciudad;

        public int idPersona { get; set; }
        public CiudadModel ciudadModel { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string tipoDocumento { get; set; }
        public string nroDocumento { get; set; }
        public string direccion { get; set; }
        public string celular { get; set; }
        public string email { get; set; }
        public string estado { get; set; }
        public string nro_documento { get; set; }
    }
}

