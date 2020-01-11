using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Services;
using System.IO;
using System.Collections;
using System.Data.SqlClient;
using SmartDeviceProject1.servicenapresa;

namespace SmartDeviceProject1
{
    class ValidateOP
    {
        cMetodos cm = new cMetodos();


        //Valida si hay OP disponibles y las muestra
        public DataTable validaOrden(string op)
        {
            DataTable res = new DataTable();
            string[] parametros = cm.getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                {
                    string select = "SELECT DISTINCT "+
                                        "ppd.MovID AS Orden, " +
                                        "ppd.Articulo AS Codigo, " +
                                        "ppd.Cantidad AS Cantidad, " +
                                        "a.Fabricante AS Color, " +
                                        "pd.ProdSerieLote AS Lote, " +
                                        "ppd.ID AS ID, " +
                                        "ppd.ArtDescripcion AS Descripcion, " +
                                        "a.Familia AS Tipo, " +
                                        "ppd.Estatus AS Estatus, " +
                                        "ppd.Renglon AS Renglon  " +
                                    "FROM ProdPendienteD ppd "+
                                        "INNER JOIN Prod p on p.MovID = ppd.MovID "+
                                        "INNER JOIN ProdD pd on pd.ID = p.ID "+
                                        "INNER JOIN Art a on a.Articulo = ppd.Articulo "+
                                        "LEFT JOIN  Venta v on v.MovId = ppd.Referencia "+
                                        "WHERE pd.CantidadPendiente IS NOT NULL "+
                                        "AND v.OrigenTipo IS NULL "+
                                        "AND pd.ID = PPD.Id "+
                                        "AND ppd.renglon = pd.renglon "+
                                        "AND pd.ProdSerieLote COLLATE Modern_Spanish_CI_AS  NOT IN (SELECT Lote  FROM [192.168.0.229].[napresaws].dbo.catProdD)"+
                                        "AND ppd.MovID = '" + op + "'";
                    SqlCommand command = new SqlCommand(select, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    res.Load(reader);
                }
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message;
            }
            return res;
        }


        //Funcion: execQuery
        //Proposito: ejecuta un query de SQL en la base de datos que se le envie con el parametro conn
        public bool execQuery(string Cadena, SqlConnection conexion)
        {
            try
            {                
                conexion.Open();                
                SqlCommand comand = new SqlCommand(Cadena, conexion);
                comand.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception ex)
            {
                string error;
                error = ex.Message;
                return false;
            }
            return true;

        }

        //Inserta informacion de una OP de intelisis a ProdD
        public bool insertOP(string [] OP, string user)
        {
            bool res = false;
            string[] parametros = cm.getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                string insert = "INSERT INTO catProdD " +
                                "VALUES (" + OP[5] + ", '" + OP[0] + "', '" + OP[1] + "', " + OP[2] + ", '" + OP[6] + "', '" + OP[7] + "', '" + OP[3] + "', '" + OP[8] + "', " + OP[9] + ", '" + OP[4] + "', NEWID(),'"+user+"', GETDATE())";// aqui agregar nuevos campos
                    
                SqlCommand cmd = new SqlCommand(insert, conn);
                cmd.ExecuteNonQuery();
                res = true;
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message;
                res = false;
            }
            return res;
            

        }




    }

    
}
