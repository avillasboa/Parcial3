using System;
using System.Data;
using Infraestructura.Conexiones;
using Infraestructura.Modelos;

namespace Infraestructura.Datos
{
    public class PersonaDatos
    {
        private ConexionDB conexion;

        public PersonaDatos(string cadenaConexion)
        {
            conexion = new ConexionDB(cadenaConexion);
        }

        public void insertarPersona(PersonaModel persona)
        {
            var conn = conexion.GetConexion();
            var comando = new Npgsql.NpgsqlCommand("INSERT INTO persona(Id, idCiudad, nombre, apellido, tipoDocumento, nroDocumento, direccion" +
                                                   ", celular, email, estado)" +
                                                   "VALUES(@Id, @idCiudad, @nombre, @apellido, @tipoDocumento, @nroDocumento, " +
                                                   "@direccion, @celular, @email, @estado)", conn);
            comando.Parameters.AddWithValue("Id", persona.idPersona);
            comando.Parameters.AddWithValue("idCiudad", persona.ciudadModel.idCiudad);
            comando.Parameters.AddWithValue("nombre", persona.nombre);
            comando.Parameters.AddWithValue("apellido", persona.apellido);
            comando.Parameters.AddWithValue("tipoDocumento", persona.tipoDocumento);
            comando.Parameters.AddWithValue("nroDocumento", persona.nroDocumento);
            comando.Parameters.AddWithValue("direccion", persona.direccion);
            comando.Parameters.AddWithValue("celular", persona.celular);
            comando.Parameters.AddWithValue("email", persona.email);
            comando.Parameters.AddWithValue("estado", "estado");
            comando.ExecuteNonQuery();
        }

        public void modificarPersona(PersonaModel persona)
        {
            var conn = conexion.GetConexion();
            var comando = new Npgsql.NpgsqlCommand($"UPDATE persona SET id_ciudad = {persona.ciudadModel.idCiudad}, nombre = '{persona.nombre}', " +
                                                   $"apellido = '{persona.apellido}'" +
                                                   $", tipo_doc = '{persona.tipoDocumento}', " +
                                                   $"direccion = '{persona.direccion}'" +
                                                   $", celular = '{persona.celular}', email = '{persona.email}', estado = '{persona.estado}'" +
                                                   $" WHERE nro_doc = '{persona.nroDocumento}'", conn);

            comando.ExecuteNonQuery();
        }

        public void eliminarPersona(string cedula)
        {
            var conn = conexion.GetConexion();
            var comando = new Npgsql.NpgsqlCommand($"DELETE FROM persona" +
                                                $" WHERE nro_doc = '{cedula}'", conn);
            comando.ExecuteNonQuery();
        }
        public PersonaModel obtenerPersonaPorcedula(string cedula)
        {
            var conn = conexion.GetConexion();
            var comando = new Npgsql.NpgsqlCommand($"Select ciudad.*, persona.* from ciudad ciudad " +
                                                   $" inner join persona persona on persona.id_ciudad = ciudad.id_ciudad" +
                                                   $" where persona.nro_doc = '{cedula}'", conn);
            using var reader = comando.ExecuteReader();
            if (reader.Read())
            {
                return new PersonaModel
                {
                    idPersona = reader.GetInt32("idCiudad"),
                    nombre = reader.GetString("nombre"),
                    apellido = reader.GetString("apellido"),
                    tipoDocumento = reader.GetString("tipoDocumento"),
                    nroDocumento = reader.GetString("nroDocumento"),
                    direccion = reader.GetString("direccion"),
                    celular = reader.GetString("celular"),
                    email = reader.GetString("email"),
                    estado = reader.GetString("estado"),
                    ciudadModel = new CiudadModel
                    {
                        idCiudad = reader.GetInt32("id"),
                        ciudad = reader.GetString("ciudad"),
                        departamento = reader.GetString("departamento"),
                        postalCode = reader.GetInt32("postal_code")
                    }
                };
            }
            return null;
        }

        public List<PersonaModel> obtenerPersonas()
        {
            var conn = conexion.GetConexion();
            var comando = new Npgsql.NpgsqlCommand($"Select * from persona", conn);
            using var reader = comando.ExecuteReader();
            List<PersonaModel> personas = new List<PersonaModel>();

            while (reader.Read())
            {
                var persona = new PersonaModel
                {
                    idPersona = reader.GetInt32("idCiudad"),
                    nombre = reader.GetString("nombre"),
                    apellido = reader.GetString("apellido"),
                    tipoDocumento = reader.GetString("tipoDocumento"),
                    nroDocumento = reader.GetString("nroDocumento"),
                    direccion = reader.GetString("direccion"),
                    celular = reader.GetString("celular"),
                    email = reader.GetString("email"),
                    estado = reader.GetString("estado"),
                    ciudadModel = new CiudadModel
                    {
                        idCiudad = reader.GetInt32("id"),
                        ciudad = reader.GetString("ciudad"),
                        departamento = reader.GetString("departamento"),
                        postalCode = reader.GetInt32("postal_code")
                    }
                };
                personas.Add(persona);
            }
            return personas;
        }
    }
}

