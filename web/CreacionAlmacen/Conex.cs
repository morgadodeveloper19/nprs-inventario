using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Web;
using System.Windows.Forms;

namespace CreacionAlamacen
{
    public class Conex
    {

        public string cadenaCon;
        SqlConnection conex = new SqlConnection();

        public Conex()
        {
            string path = Environment.GetEnvironmentVariable("CualliRF") != null ? Environment.GetEnvironmentVariable("CualliRF") : Environment.GetEnvironmentVariable("USERPROFILE");
            StreamReader objReader = new StreamReader(path + "/baseDatos.txt");
            FileInfo f = new FileInfo(path + "/baseDatos.txt");
            string servidor = "", user = "", psw = "", bd = "", sLine = "";
            sLine = objReader.ReadLine();
            while (sLine != null)
            {
                if (sLine.Contains("server="))
                    servidor = sLine.Replace("server=", "");
                else
                    if (sLine.Contains("usuario="))
                        user = sLine.Replace("usuario=", "");
                    else
                        if (sLine.Contains("password="))
                            psw = sLine.Replace("password=", "");
                        else
                            if (sLine.Contains("baseDatos="))
                                bd = sLine.Replace("baseDatos=", "");
                sLine = objReader.ReadLine();
            }
            cadenaCon = "Server=" + servidor + ";" + "uid=" + user + ";" + "password=" + psw + ";" + "database=" + bd;
            conex.ConnectionString = cadenaCon;         
            conex.Open();
            }

        public int getInt(string consulta)
        {
            int cosa = 0;
            SqlCommand cmd = new SqlCommand(consulta, conex);
            DataSet ds = new DataSet();
            SqlDataReader dr;
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                   cosa =  Convert.ToInt32( dr[0].ToString() );
                }
                dr.Close();
            }
            catch (Exception ex)
            {
            }
            return cosa;
        }

        public string[] getNivVent(string rack)
        {
            //Obtiene los nives y ventanas del Rack 'especificado'
            string[] cosa = new string[2];
            SqlCommand cmd = new SqlCommand("SELECT top(1) n.Clave as [Niveles], count(v.Clave) as [Ventanas] FROM posiciones p INNER JOIN ventanas v ON v.IDVentana = p.IDVentana INNER JOIN niveles n ON n.IDNivel = v.IDNivel INNER JOIN racks r ON r.IDRack = n.IDRack WHERE r.Clave = '"+rack+"' GROUP by n.Clave ORDER BY n.Clave desc ", conex);
            DataSet ds = new DataSet();
            SqlDataReader dr;
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    cosa[0] = dr[0].ToString();//Niveles
                    cosa[1] = dr[1].ToString();//Ventanas
                }
                dr.Close();
            }
            catch(SqlException ex)
            {
            
            };
            return cosa;
        }

        public string[] getUsuario(string usr, string pwd)
        {
            //Obtiene los nives y ventanas del Rack 'especificado'
            string[] cosa = new string[3];
            SqlCommand cmd = new SqlCommand("select nombre, aPaterno, aMaterno FROM Usuarios where usuario = '"+usr+"' and password = '"+pwd+"'", conex);
            DataSet ds = new DataSet();
            SqlDataReader dr;
            try
            {
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    cosa[0] = dr[0].ToString();//Nombre
                    cosa[1] = dr[1].ToString();//aPaterno
                    cosa[2] = dr[2].ToString();//aMaterno
                }
                dr.Close();
            }
            catch(SqlException ex)
            {
            
            };
            return cosa;
        }

        public DataSet getDataset(string consulta)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand( consulta, conex);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
            };
            return ds;
        }


        public SqlDataAdapter getDataAdapter(string consultas)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                SqlCommand cmd = new SqlCommand(consultas, conex);
                da = new SqlDataAdapter(cmd);
                //da.Fill(ds);
            }
            catch (Exception ex)
            {
            };
            return da;
        }

        public int insert(string consulta)
        {
            int hecho = 0;
            string sql = consulta;
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conex);
                hecho = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                return -1;
            }
            return hecho;
        }

        public int Update_Int(string consulta)
        {
            int hecho = 0;
            try
            {
                SqlCommand cmd = new SqlCommand(consulta, conex);
                hecho = cmd.ExecuteNonQuery();
                hecho = hecho * (-1);
            }
            catch (SqlException ex)
            {
                return -1;
            }
            return hecho;
        }

    }
}
