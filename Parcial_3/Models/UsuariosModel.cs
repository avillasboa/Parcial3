﻿namespace Optativo_Api.Models;

public class UsuariosModels
{
    public int id_usuarios { get; set; }
    public string nombre_usuario { get; set; }
    public string contrasena { get; set; }
    public string nivel { get; set; }
    public string estado { get; set; }
    public int id_persona { get; set; }
}