﻿using Infraestructura.Modelos;
using Infraestructura.Conexiones;
using System.Data;

namespace Infraestructura.Datos;

public class UsuariosDatos
{
    private ConexionDB conexion;

    public UsuariosDatos(string cadenaConexion)
    {
        conexion = new ConexionDB(cadenaConexion);
    }

    public List<UsuariosModel> ObtenerTodosLosUsuarios()
    {
        var conn = conexion.GetConexion();
        var comando = new Npgsql.NpgsqlCommand($"SELECT persona.*, usuario.* " +
                                               $"FROM persona persona " +
                                               $"INNER JOIN usuarios u ON persona.id_persona = usuario.id_persona ", conn);
        List<UsuariosModel> usuarios = new List<UsuariosModel>();

        using var reader = comando.ExecuteReader();
        while (reader.Read())
        {
            usuarios.Add(new UsuariosModel()
            {
                id_usuarios = reader.GetInt32("id_usuarios"),
                nombre_usuario = reader.GetString("nombre_usuario"),
                contrasena = reader.GetString("contrasena"),
                nivel = reader.GetString("nivel"),
                estado = reader.GetString("estado"),
                persona = new PersonaModel()
                {
                    id_persona = reader.GetInt32("id_persona"),
                    nombre = reader.GetString("nombre"),
                    apellido = reader.GetString("apellido"),
                    nro_documento = reader.GetString("nro_documento"),
                    direccion = reader.GetString("direccion"),
                    email = reader.GetString("email"),
                    celular = reader.GetString("celular"),
                    estado = reader.GetString("estado"),
                }
            });
        }

        return usuarios;
    }


    public UsuariosModel ObtenerUsuariosPorId(int id)
    {
        var conn = conexion.GetConexion();
        var comando = new Npgsql.NpgsqlCommand($"SELECT persona.*, usuario.* " +
                                               $"FROM persona persona " +
                                               $"INNER JOIN usuarios u ON persona.id_persona = usuario.id_persona " +
                                               $"WHERE usuario.id_usuarios = {id}", conn);
        using var reader = comando.ExecuteReader();
        if (reader.Read())
        {
            return new UsuariosModel()
            {
                id_usuarios = reader.GetInt32("id_usuarios"),
                nombre_usuario = reader.GetString("nombre_usuario"),
                contrasena = reader.GetString("contrasena"),
                nivel = reader.GetString("nivel"),
                estado = reader.GetString("estado"),
                persona = new PersonaModel()
                {
                    id_persona = reader.GetInt32("id_persona"),
                    nombre = reader.GetString("nombre"),
                    apellido = reader.GetString("apellido"),
                    nro_documento = reader.GetString("nro_documento"),
                    direccion = reader.GetString("direccion"),
                }
            };
        }
        return null;
    }

    public void RegistrarUsuarios(UsuariosModel usuarios)
    {
        var conn = conexion.GetConexion();
        var comando = new Npgsql.NpgsqlCommand("INSERT INTO public.usuarios (nombre_usuario, contrasena, nivel, estado, id_persona) " +
                                               "VALUES (@nombre_usuario, @contrasena, @nivel, @estado, @id_persona)", conn);
        comando.Parameters.AddWithValue("nombre_usuario", usuarios.nombre_usuario);
        comando.Parameters.AddWithValue("contrasena", usuarios.contrasena);
        comando.Parameters.AddWithValue("nivel", usuarios.nivel);
        comando.Parameters.AddWithValue("estado", usuarios.estado);
        comando.Parameters.AddWithValue("id_persona", usuarios.persona.id_persona);

        comando.ExecuteNonQuery();
    }



    public void modificarUsuariosPorId(UsuariosModel usuarios)
    {
        var conn = conexion.GetConexion();
        var comando = new Npgsql.NpgsqlCommand($"UPDATE persona " +
                                               $"SET nombre_usuario = '{usuarios.nombre_usuario}', " +
                                               $"contrasena = '{usuarios.contrasena}', " +
                                               $"nivel = '{usuarios.nivel}', " +
                                               $"estado = '{usuarios.estado}', " +
                                               $"id_persona = {usuarios.persona.id_persona} " +
                                               $"WHERE id_usuarios = {usuarios.id_usuarios}", conn);

        comando.ExecuteNonQuery();
    }

    public UsuariosModel EliminarUsuariosPorId(int id)
    {
        var conn = conexion.GetConexion();
        var comando = new Npgsql.NpgsqlCommand($"DELETE FROM usuarios WHERE id_usuarios = {id}", conn);
        using var reader = comando.ExecuteReader();
        return null;
    }

}