﻿using AccesoDatos.Home.Interface;
using Configuracion.Implementacion;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using Entidades.Home;

namespace AccesoDatos.Home.Implementacion
{
    public class HomeDatos : IHomeDatos
    {
        private readonly string context;
        public HomeDatos(IConfiguration _configuration)
        {
            context = new ConfiguracionData(_configuration).GetConnectionString("ConexionBdComercio");
        }
        public HomeDatos(string _DbConexion)
        {
            context = _DbConexion;
        }
        public bool ListarTipoCambio(out DataTable objDtt)
        {
            SqlConnection objCnx = null;
            SqlDataReader objDtr = null;
            var bRsl = false;
            try
            {
                objCnx = new SqlConnection(this.context);
                using (var objCmd = new SqlCommand("[dbo].[sp_listar_TC_actual]", objCnx))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCnx.Open();
                    objDtr = objCmd.ExecuteReader();
                    var _objDtt = new DataTable();
                    _objDtt.Load(objDtr);
                    objDtt = _objDtt;
                    bRsl = true;
                }
            }
            catch (System.Exception ex)
            {
                throw; //new System.Exception(ex.Message);
            }
            finally
            {
                try
                {
                    if (objDtr != null && !objDtr.IsClosed) objDtr.Close();
                    if (objCnx != null && objCnx.State == ConnectionState.Open)
                    {
                        objCnx.Close();
                    }
                }
                catch (System.Exception)
                {

                }
            }

            return bRsl;
        }
        public bool GuardarTipoCambio(EntidadTipoCambio objTipoCambio, out string mensaje)
        {
            objTipoCambio.ReplaceNull();
            SqlConnection objCnx = null;
            var bRsl = false;
            mensaje = "";
            try
            {
                objCnx = new SqlConnection(this.context);
                using (var objCmd = new SqlCommand("[dbo].[sp_ingreso_TC]", objCnx))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter Pnombres = new SqlParameter("@PTipoCambio", SqlDbType.Decimal);
                    Pnombres.Value = objTipoCambio.tipoCambio;
                    objCmd.Parameters.Add(Pnombres);

                    objCnx.Open();
                    var dtr = objCmd.ExecuteReader();
                    if (!dtr.HasRows)
                    {
                        mensaje = "";
                        return bRsl;
                    }
                    while (dtr.Read())
                    {
                        bRsl = true;
                        mensaje = dtr[0].ToString();
                    }

                }
            }
            catch (System.Exception ex)
            {
                throw; //new System.Exception(ex.Message);
            }
            finally
            {
                try
                {
                    if (objCnx != null && objCnx.State == ConnectionState.Open)
                    {
                        objCnx.Close();
                    }
                }
                catch (System.Exception)
                {

                }
            }

            return bRsl;
        }
        public bool DashboardPagos(out DataSet _objDts)
        {
            SqlConnection objCnx = null;
            SqlDataReader objDtr = null;
            var bRsl = false;
            try
            {
                objCnx = new SqlConnection(this.context);
                using (var objCmd = new SqlCommand("[dbo].[sp_pagos_x_dia_dashboard]", objCnx))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCnx.Open();
                    var objDta = new SqlDataAdapter();
                    objDta.SelectCommand = objCmd;
                    var objDts = new DataSet();
                    objDta.Fill(objDts);
                    _objDts = objDts;
                    bRsl = true;
                }
            }
            catch (System.Exception ex)
            {
                throw; //new System.Exception(ex.Message);
            }
            finally
            {
                try
                {
                    if (objDtr != null && !objDtr.IsClosed) objDtr.Close();
                    if (objCnx != null && objCnx.State == ConnectionState.Open)
                    {
                        objCnx.Close();
                    }
                }
                catch (System.Exception)
                {

                }
            }
            return bRsl;
        }
        public bool DashboardIndicadores(out DataSet _objDts)
        {
            SqlConnection objCnx = null;
            SqlDataReader objDtr = null;
            var bRsl = false;
            try
            {
                objCnx = new SqlConnection(this.context);
                using (var objCmd = new SqlCommand("[dbo].[sp_indicadores_x_dia_dashboard]", objCnx))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCnx.Open();
                    var objDta = new SqlDataAdapter();
                    objDta.SelectCommand = objCmd;
                    var objDts = new DataSet();
                    objDta.Fill(objDts);
                    _objDts = objDts;
                    bRsl = true;
                }
            }
            catch (System.Exception ex)
            {
                throw; //new System.Exception(ex.Message);
            }
            finally
            {
                try
                {
                    if (objDtr != null && !objDtr.IsClosed) objDtr.Close();
                    if (objCnx != null && objCnx.State == ConnectionState.Open)
                    {
                        objCnx.Close();
                    }
                }
                catch (System.Exception)
                {

                }
            }
            return bRsl;
        }
        public bool DashboardNotificaciones(out DataSet _objDts)
        {
            SqlConnection objCnx = null;
            SqlDataReader objDtr = null;
            var bRsl = false;
            try
            {
                objCnx = new SqlConnection(this.context);
                using (var objCmd = new SqlCommand("[dbo].[sp_notificaciones_dashboard]", objCnx))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCnx.Open();
                    var objDta = new SqlDataAdapter();
                    objDta.SelectCommand = objCmd;
                    var objDts = new DataSet();
                    objDta.Fill(objDts);
                    _objDts = objDts;
                    bRsl = true;
                }
            }
            catch (System.Exception ex)
            {
                throw; //new System.Exception(ex.Message);
            }
            finally
            {
                try
                {
                    if (objDtr != null && !objDtr.IsClosed) objDtr.Close();
                    if (objCnx != null && objCnx.State == ConnectionState.Open)
                    {
                        objCnx.Close();
                    }
                }
                catch (System.Exception)
                {

                }
            }
            return bRsl;
        }
    }
}
