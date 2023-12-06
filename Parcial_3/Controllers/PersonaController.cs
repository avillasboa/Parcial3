﻿using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Infraestructura.Modelos;
using Microsoft.AspNetCore.Authorization;
using Infraestructura.Datos;
using Servicios.Service;



namespace Parcial_II_Optativo_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private const string connectionString = "Host=localhost;User Id=postgres;Password=1234;Database=Parcial_1;";
        private PersonaService PersonaService;

        public PersonaController()
        {
            PersonaService = new PersonaService(connectionString);
        }

        //Mostra todas las porsona
        //
        [HttpGet("obtenerTodasLasPersona")]
        public IActionResult obtenerTodasLasPersona()
        {
            return Ok(PersonaService.btenerTodasLasPersona());
            
        }
        //


        //Mostra porsona por documento
        //
        [HttpGet("obtenerPersonaPorId{id}")]
        public IActionResult obtenerPersonaPorId(int id)
        {
            return Ok(personaServicio.obtenerPersonaPorId(id));
        }
        //


        //Insertar persona - Basico
        //
        [HttpPost("RegistrarPersona")]
        public IActionResult RegistrarPersona([FromBody] Models.PersonaModels modelo)
        {
            personaServicio.RegistrarPersona(
                new Infraestructura.Modelos.PersonaModel
                {
                    nombre = modelo.nombre,
                    apellido = modelo.apellido,
                    tipoDocumento = modelo.tipoDocumento,
                    direccion = modelo.direccion,
                    email = modelo.email,
                    celular = modelo.celular,
                    estado = modelo.estado,
                    ciudad = new Infraestructura.Modelos.CiudadModel
                    {
                        id_ciudad = modelo.id_ciudad
                    }
                });
            return Ok("Los datos de persona fueron insertados correctamente");
        }
        //

        //Modificar PERSONA
        //
        [HttpPut("modificarPersonaPorId")]
        public IActionResult modificarPersonaPorId([FromBody] Infraestructura.Modelos.PersonaModel modelo)
        {
            try
            {
                personaServicio.modificarPersonaPorId(modelo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
            return Ok("se actualizo con exito");
        }
        //


        //Eliminar Persona
        //
        [HttpDelete("EliminarPersonaPorId{id}")]
        public IActionResult EliminarPersonaPorId(int id)
        {
            return Ok(personaServicio.EliminarPersonaPorId(id));
        }
        //
    }