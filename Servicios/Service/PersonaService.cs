using System;
using Infraestructura.Datos;
using Infraestructura.Modelos;

namespace Servicios.Service
{
    public class PersonaService
    {
        private PersonaDatos personaDatos;

        public PersonaService(string cadenaConexion)
        {
            personaDatos = new PersonaDatos(cadenaConexion);
        }

        public void insertarPersona(PersonaModel persona)
        {
            validarDatos(persona);
            personaDatos.insertarPersona(persona);
        }

        public void modificarPersona(PersonaModel persona)
        {
            validarDatos(persona);
            personaDatos.modificarPersona(persona);
        }

        public void eliminarPersona(string cedula)
        {
            personaDatos.eliminarPersona(cedula);
        }
        public PersonaModel obtenerPersonaPorCedula(string cedula)
        {
            return personaDatos.obtenerPersonaPorcedula(cedula);
        }

        public IEnumerable<PersonaModel> obtenerPersonas()
        {
            return personaDatos.obtenerPersonas();
        }

        private void validarDatos(PersonaModel persona)
        {
            if (persona.nombre.Trim().Length < 2)
            {
                throw new Exception("El campo nombre no puede ser nulo");
            }
            if (persona.apellido.Trim().Length < 2)
            {
                throw new Exception("El campo apellido no puede ser nulo");
            }
            if (persona.nro_documento.Trim().Length < 2)
            {
                throw new Exception("El campo nro_documento no puede ser nulo");
            }
            if (persona.direccion.Trim().Length < 2)
            {
                throw new Exception("El campo direccion no puede ser nulo");
            }
            if (persona.email.Trim().Length < 2)
            {
                throw new Exception("El campo email no puede ser nulo");
            }
            if (persona.celular.Trim().Length < 2)
            {
                throw new Exception("El campo celular no puede ser nulo");
            }
            if (persona.estado.Trim().Length < 1)
            {
                throw new Exception("El campo estado no puede ser nulo");
            }
            if (persona.estado.Trim().Length < 0)
            {
                throw new Exception("El campo estado no puede ser negativo");
            }
            if (persona.ciudad.id_ciudad < 1)
            {
                throw new Exception("El campo id_ciudad no puede ser nulo");
            }
        }
    }

