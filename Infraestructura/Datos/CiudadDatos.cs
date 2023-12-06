using System;
using System.Data;
using Infraestructura.Conexiones;
using Infraestructura.Modelos;

namespace Infraestructura.Datos
{
    public class CiudadDatos
    {
        private ConexionDB conexion;

        public CiudadDatos(string cadenaConexion)
        {
            conexion = new ConexionDB(cadenaConexion);
        }

        public void insertarCiudad(CiudadModel ciudad)
        {
            var conn = conexion.GetConexion();
            var comando = new Npgsql.NpgsqlCommand("INSERT INTO ciudad(id, ciudad, departamento, postal_code)" +
                                                "VALUES(@id, @ciudad, @departamento, @postal_code)", conn);
            comando.Parameters.AddWithValue("id", ciudad.idCiudad);
            comando.Parameters.AddWithValue("ciudad", ciudad.ciudad);
            comando.Parameters.AddWithValue("departamento", ciudad.departamento);
            comando.Parameters.AddWithValue("postal_code", ciudad.postalCode);
            comando.ExecuteNonQuery();
        }

        public void modificarCiudad(CiudadModel ciudad)
        {
            var conn = conexion.GetConexion();
            var comando = new Npgsql.NpgsqlCommand($"UPDATE ciudad SET ciudad = '{ciudad.ciudad}', departamento = '{ciudad.departamento}', " +
                                                   $"postal_code = {ciudad.postalCode}" +
                                                   $" WHERE id = {ciudad.idCiudad}", conn);

            comando.ExecuteNonQuery();
        }

        public void eliminarCiudad(int id)
        {
            var conn = conexion.GetConexion();
            var comando = new Npgsql.NpgsqlCommand($"DELETE FROM ciudad" +
                                                $" WHERE id = {id}", conn);
            comando.ExecuteNonQuery();
        }
        public CiudadModel obtenerCiudadPorId(int id)
        {
            var conn = conexion.GetConexion();
            var comando = new Npgsql.NpgsqlCommand($"Select * from ciudad where id = {id}", conn);
            using var reader = comando.ExecuteReader();
            if (reader.Read())
            {
                return new CiudadModel
                {
                    idCiudad = reader.GetInt32("id"),
                    ciudad = reader.GetString("ciudad"),
                    departamento = reader.GetString("departamento"),
                    postalCode = reader.GetInt32("postal_code")
                };
            }
            return null;
        }

        public List<CiudadModel> obtenerCiudades()
        {
            var conn = conexion.GetConexion();
            var comando = new Npgsql.NpgsqlCommand($"Select * from ciudad", conn);
            using var reader = comando.ExecuteReader();
            List<CiudadModel> ciudades = new List<CiudadModel>();

            while (reader.Read())
            {
                var ciudad = new CiudadModel
                {
                    idCiudad = reader.GetInt32("id"),
                    ciudad = reader.GetString("ciudad"),
                    departamento = reader.GetString("departamento"),
                    postalCode = reader.GetInt32("postal_code")
                };
                ciudades.Add(ciudad);
            }
            return ciudades;
        }
    }
}

