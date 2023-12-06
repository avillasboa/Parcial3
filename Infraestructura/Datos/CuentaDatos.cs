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
    public class CuentaDatos
    {
        private ConexionDB conexion;

        public CuentaDatos(string cadenaConexion)
        {
            conexion = new ConexionDB(cadenaConexion);
        }

        public void insertarCuenta(CuentaModel Cuenta)
        {
            var conn = conexion.GetConexion();
            var comando = new Npgsql.NpgsqlCommand("INSERT INTO cuentas(idCliente, NroCuenta, FechaAlta, TipoCuenta, Estadocuenta, Saldo, nroContrato, " +
                                                   "nroContrato, CostoMantenimiento, Promedioacreditacion, moneda,)" +
                                                   "VALUES(@idCliente, @NroCuenta, @FechaAlta, @TipoCuenta, @EstadoCuenta, @saldo, " +
                                                   "@nroContrato, @CostoMantenimiento, @PromedioAcreditacion, @moneda)", conn);
            comando.Parameters.AddWithValue("idCliente", Cuenta.clmodel.idCliente);
            comando.Parameters.AddWithValue("NroCuenta", Cuenta.nroCuenta);
            comando.Parameters.AddWithValue("FechaAlta", Cuenta.fechaAlta);
            comando.Parameters.AddWithValue("TipoCuenta", Cuenta.tipoCuenta);
            comando.Parameters.AddWithValue("Estadocuenta", "Activo");
            comando.Parameters.AddWithValue("Saldo", Cuenta.saldo);
            comando.Parameters.AddWithValue("nroContrato", Cuenta.nroContrato);
            comando.Parameters.AddWithValue("CostoMantenimiento", Cuenta.costoMantenimiento);
            comando.Parameters.AddWithValue("PromedioAcreditacion", Cuenta.promedioAcreditacion);
            comando.Parameters.AddWithValue("moneda", Cuenta.moneda);
            comando.ExecuteNonQuery();
        }

        public void modificarCuenta(CuentaModel Cuenta)
        {
            var conn = conexion.GetConexion();
            var comando = new Npgsql.NpgsqlCommand($"UPDATE cuentas SET estado_cta = '{Cuenta.estado}', " +
                                                   $"saldo = {Cuenta.saldo}, " +
                                                   $"CostoMantenimiento = {Cuenta.costoMantenimiento}, " +
                                                   $"PromedioAcreditacion = '{Cuenta.promedioAcreditacion}'" +
                                                   $" WHERE nroContrato = '{Cuenta.nroContrato}'", conn);

            comando.ExecuteNonQuery();
        }

        public void eliminarCuenta(string nroContrato)
        {
            var conn = conexion.GetConexion();
            var comando = new Npgsql.NpgsqlCommand($"DELETE FROM cuentas" +
                                                $" WHERE nro_contrato = '{nroContrato}'", conn);
            comando.ExecuteNonQuery();
        }
        public CuentaModel obtenerCuentaPorContrato(string nroContrato)
        {
            var conn = conexion.GetConexion();
            var comando = new Npgsql.NpgsqlCommand($"Select cuentas.*, cliente.*, ciudad.*, persona.* from cuentas cuentas" +
                                                   $" inner join cliente cliente on cliente.id_cliente = cuentas.id_cliente" +
                                                   $" inner join persona persona on persona.id_per = cliente.id_per" +
                                                   $" inner join ciudad ciudad on ciudad.id_ciudad = persona.id_ciudad" +
                                                   $" where cuentas.nro_contrato = '{nroContrato}'", conn);
            using var reader = comando.ExecuteReader();
            if (reader.Read())
            {
                return new CuentaModel
                {
                    idCuenta = reader.GetInt32("idCuenta"),
                    nroCuenta = reader.GetString("nroCuenta"),
                    fechaAlta = reader.GetDateTime("FechaAlta"),
                    tipoCuenta = reader.GetString("TipoCuenta"),
                    estado = reader.GetString("Estado"),
                    saldo = reader.GetDouble("Saldo"),
                    nroContrato = reader.GetString("nroContrato"),
                    costoMantenimiento = reader.GetDouble("CostoMantenimiento"),
                    promedioAcreditacion = reader.GetString("PromedioAcreditacion"),
                    moneda = reader.GetString("moneda"),
                    clmodel = new ClienteModel
                    {
                        idCliente = reader.GetInt32("idCiudad"),
                        fechaIngreso = reader.GetDateTime("fechaIngreso"),
                        calificacion = reader.GetString("calificacion"),
                        estado = reader.GetString("estado"),
                        pModel = new PersonaModel
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
                        }
                    }
                };
            }
            return null;
        }

    }
}
