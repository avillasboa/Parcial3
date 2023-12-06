﻿using Infraestructura.Datos;
using Infraestructura.Modelos;

namespace Servicios.ContactosService;

public class UsuariosService
{
    UsuariosDatos usuariosDatos;

    public UsuariosService(string cadenaConexion)
    {
        usuariosDatos = new UsuariosDatos(cadenaConexion);
    }

    public List<UsuariosModel> obtenerTodasLosUsuarios()
    {
        return usuariosDatos.ObtenerTodosLosUsuarios();
    }

    public UsuariosModel obtenerUsuariosPorId(int id)
    {
        return usuariosDatos.ObtenerUsuariosPorId(id);
    }

    public void RegistrarUsuarios(UsuariosModel usuarios)
    {
        validarDatos(usuarios);
        usuariosDatos.RegistrarUsuarios(usuarios);
    }

    public void modificarUsuariosPorId(UsuariosModel usuarios)
    {
        validarDatos(usuarios);
        usuariosDatos.modificarUsuariosPorId(usuarios);
    }

    public UsuariosModel EliminarUsuariosPorId(int id)
    {
        return usuariosDatos.EliminarUsuariosPorId(id);
    }

    private void validarDatos(UsuariosModel usuarios)
    {
        if (usuarios.nombre_usuario.Trim().Length < 2)
        {
            throw new Exception("El campo nombre_usuario no puede ser nulo");
        }
        if (usuarios.contrasena.Trim().Length < 2)
        {
            throw new Exception("El campo contrasena no puede ser nulo");
        }
        if (usuarios.nivel.Trim().Length < 1)
        {
            throw new Exception("El campo nivel no puede ser nulo");
        }
        if (usuarios.estado.Trim().Length < 0)
        {
            throw new Exception("El campo estado no puede ser nulo");
        }

        if(usuarios.id_usuarios > 0)
        {
            throw new Exception("El campo id_persona no puede ser nulo");
        }
    }
}