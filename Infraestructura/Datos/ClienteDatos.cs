using Infraestructura.Conexiones;
using Infraestructura.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Datos
{
    public class ClienteDatos
    {
        private ConexionDB conexion;

        public ClienteDatos(string cadenaConexion)
        {
            conexion = new ConexionDB(cadenaConexion);
        }

        public void insertarCliente(ClienteModel cliente)
        {
            var conn = conexion.GetConexion();
            var comando = new Npgsql.NpgsqlCommand("INSERT INTO cliente(idcliente, fechaIngreso, calificacion, estado)" +
                                                   "VALUES(@idcliente, @fechaIngreso, @calificacion, @estado)", conn);
            comando.Parameters.AddWithValue("idcliente", cliente.idPersona.idPersona);
            comando.Parameters.AddWithValue("fechaIngreso", cliente.fechaIngreso);
            comando.Parameters.AddWithValue("calificacion", cliente.calificacion);
            comando.Parameters.AddWithValue("estado", "estado");
            comando.ExecuteNonQuery();
        }

        public void modificarCliente(ClienteModel cliente)
        {
            var conn = conexion.GetConexion();
            var comando = new Npgsql.NpgsqlCommand($"UPDATE cliente SET idcliente = {cliente.idPersona.idPersona}, " +
                                                   $"calificacion = '{cliente.calificacion}', estado = '{cliente.estado}'" +
                                                   $" WHERE idcliente = '{cliente.idCliente}'", conn);

            comando.ExecuteNonQuery();
        }

        public void eliminarCliente(int id)
        {
            var conn = conexion.GetConexion();
            var comando = new Npgsql.NpgsqlCommand($"DELETE FROM cliente" +
                                                $" WHERE id_cliente = '{id}'", conn);
            comando.ExecuteNonQuery();
        }
        public ClienteModel obtenerClientePorId(int id)
        {
            var conn = conexion.GetConexion();
            var comando = new Npgsql.NpgsqlCommand($"Select ciudad.*, ciudad.*, persona.* from cliente cliente " +
                                                   $" inner join persona persona on persona.idcliente = cliente.idcliente" +
                                                   $" inner join ciudad ciudad on ciudad.id_ciudad = persona.id_ciudad" +
                                                   $" where cliente.id_cliente = '{id}'", conn);
            using var reader = comando.ExecuteReader();
            if (reader.Read())
            {
                return new ClienteModel
                {
                    idCliente = reader.GetInt32("id_ciudad"),
                    fechaIngreso = reader.GetDateTime("fechaIngreso"),
                    calificacion = reader.GetString("calificacion"),
                    estado = reader.GetString("estado"),
                    idPersona = new PersonaModel
                    {
                        idPersona = reader.GetInt32("idcliente"),
                        nombre = reader.GetString("nombre"),
                        apellido = reader.GetString("apellido"),
                        tipoDocumento = reader.GetString("tipo_doc"),
                        nroDocumento = reader.GetString("nro_doc"),
                        direccion = reader.GetString("direccion"),
                        celular = reader.GetString("celular"),
                        email = reader.GetString("email"),
                        estado = reader.GetString("estado"),
                        ciudad = new CiudadModel
                        {
                            idCiudad = reader.GetInt32("id_ciudad"),
                            ciudad = reader.GetString("ciudad"),
                            departamento = reader.GetString("depto"),
                            postalCode = reader.GetInt32("cp")
                        }
                    }
                };
            }
            return null;
        }
    }
}
