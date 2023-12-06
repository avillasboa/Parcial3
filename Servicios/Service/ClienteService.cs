using Infraestructura.Datos;
using Infraestructura.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Service
{
    public class ClienteService
    {
        private ClienteDatos clienteDatos;

        public ClienteService(string cadenaConexion)
        {
            clienteDatos = new ClienteDatos(cadenaConexion);
        }

        public void insertarCliente(ClienteModel cliente)
        {
            validarDatos(cliente);
            clienteDatos.insertarCliente(cliente);
        }

        public void modificarCliente(ClienteModel cliente)
        {
            validarDatos(cliente);
            clienteDatos.modificarCliente(cliente);
        }

        public void eliminarCliente(int id)
        {
            clienteDatos.eliminarCliente(id);
        }
        public ClienteModel obtenerClientePorId(int id)
        {
            return clienteDatos.obtenerClientePorId(id);
        }

        /*public IEnumerable<ClienteModel> obtenerClientes()
        {
            return clienteDatos.obtenerClientes();
        }*/

        private void validarDatos(ClienteModel cliente)
        {
            if (cliente.calificacion.Trim().Length < 1)
            {
                throw new Exception("Debe agregar una calificacion al cliente");
            }
            if (cliente.fechaIngreso == DateTime.MinValue)
            {
                throw new Exception("Debe indicar la fecha de ingreso");
            }
        }
    }
}
