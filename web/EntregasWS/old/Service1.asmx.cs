using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;

namespace WebService1
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
  
    public class Service1 : System.Web.Services.WebService
    { 

        public string[] getParametros(string Descripcion)
        {         
            string[] result = new string[5];
            try
            {
                //SqlConnection conn = new SqlConnection("Data Source=172.16.1.31;Initial Catalog=NapresaPar;Persist Security Info=True;User ID=sa;Password=Adminpwd20");
                SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=napresaPar;Persist Security Info=True;User ID=sa;Password=NapresaPwd20");                
                conn.Open();
                string select = "Select * From Parametros where Descripcion='" + Descripcion + "'";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    result[0] = reader.GetString(0);
                    result[1] = reader.GetString(1);
                    result[2] = reader.GetString(2);
                    result[3] = reader.GetString(3);
                    result[4] = reader.GetString(4);                    
                }
                else
                {
                }
                conn.Close();    
            }
            catch (Exception e)
            {
            }
            return result;
            
        }

        [WebMethod]
        public string logearse(string numChofer, string imei)
        {

            string[] prueba = getParametros("Solutia");
                SqlConnection conn = new SqlConnection("Data Source="+prueba[1]+"; Initial Catalog="+prueba[4]+"; Persist Security Info=True; User ID="+prueba[2]+"; Password="+prueba[3]+"");
                String result = "1";
                conn.Open();
                string select = "SELECT * FROM catChoferTelefono WHERE numChofer = '" + numChofer + "';";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if (reader["numMIDI"].ToString().Equals(imei))
                    {
                        conn.Close();
                        int cosa= 1;
                        result = cosa.ToString();
                        return result;
                    }
                    else
                    {
                        conn.Close();
                        result = "El IMEI no correponde a ese numero de chofer";
                        return result;
                    }
                }
                else
                {
                    conn.Close();
                    result = "El numero de chofer no existe";
                    return result;
                }           
        }
        
        [WebMethod]
        public cFolio[] getFolios(string numChofer)
        {
            string[] prueba = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");
            conn.Open();

            string select = "select EmbarqueMov.MovId, Venta.ID, Embarque.Agente, Embarque.Vehiculo from EmbarqueMov inner join EmbarqueD on EmbarqueMov.ID = EmbarqueD.EmbarqueMov inner join Embarque on EmbarqueMov.AsignadoID = Embarque.ID inner join Venta on EmbarqueMov.MovID = Venta.MovID where Embarque.Estatus = 'PENDIENTE' and Embarque.Agente='" + numChofer + "'";
            SqlCommand cmd = new SqlCommand(select, conn);

            SqlDataReader reader = cmd.ExecuteReader();

            //List<cFolio> lista = new List<cFolio>();
            List<cFolio> listaMovil = new List<cFolio>();
            int contador = longitud(numChofer);
            String[] folio = new String[contador];
            String[] agente = new String[contador];
            String[] transporte = new String[contador];
            while (reader.Read())
            {
                for (int i = 0; i < contador; i++)
                {
                    String pin = reader.GetValue(1) + "";
                    folio[i] = reader.GetString(0);
                    agente[i] = reader.GetString(2);
                    transporte[i] = reader.GetString(3);
                    //lista.Add(new cFolio(reader.GetString(0), pin,reader.GetString(2),reader.GetString(3)));
                    listaMovil.Add(new cFolio(reader.GetString(0), pin));
                }
            }
           
           conn.Close();
           string[] prueba2 = getParametros("Solutia");
           SqlConnection conn2 = new SqlConnection("Data Source=" + prueba2[1] + "; Initial Catalog=" + prueba2[4] + "; Persist Security Info=True; User ID=" + prueba2[2] + "; Password=" + prueba2[3] + "");
           conn2.Open();
           for(int i = 0; i<folio.Length;i++){
               string select2 = "Select count(*) as total from Entregas where numEntrega = '" + folio[i] + "'";
               SqlCommand cmd3 = new SqlCommand(select2, conn2);
               SqlDataReader reader2 = cmd3.ExecuteReader();
               //int result = int.Parse(reader2.GetString(0));
               int result=0;
               while (reader2.Read())
               {
                   String pin2 = reader2.GetValue(0) + "";
                   if(pin2.Equals("0")){
                   }else{
                       result = 1;
                   }
               }
               conn2.Close();
               conn2.Open();
               if (result == 0)
               {
                   string insert = "Insert into Entregas values ('" + folio[i] + "','" + agente[i] + "','"
                   + transporte[i] + "','');";
                   SqlCommand cmd2 = new SqlCommand(insert, conn2);
                   cmd2.ExecuteNonQuery();
               }
           }
           conn2.Close();
           
           return listaMovil.ToArray();
                
        }
           
           
        
        [WebMethod]
        public string[] ingresoUsuario(string user, string password)
        {           
            string[] prueba = getParametros("ConsolaAdmin");                    
            SqlConnection consolaAdmin = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");
            //SqlConnection consolaAdmin = new SqlConnection("Data Source=172.16.1.31;Initial Catalog=napresa;Persist Security Info=True;User ID=sa;Password=Adminpwd20");
            string[] res = new string[4];
            consolaAdmin.Open();
            SqlCommand cmd = new SqlCommand("select idUsuario, nombre, aPaterno, idCentro from Usuarios where usuario = '" + user + "' and password ='" + password + "'", consolaAdmin);
            SqlDataReader dr;
            try
            {               
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    res[0] = dr["idUsuario"].ToString();
                    res[1] = dr["nombre"].ToString();
                    res[2] = dr["aPaterno"].ToString();
                    res[3] = dr["idCentro"].ToString();
                }
                else 
                {
                    res[0] = res[1] = res[2] = res[3] = "0";
                }
                dr.Close();
                consolaAdmin.Close();
            }
            catch (Exception ex)
            {               
                return res;
            }
            return res;
        }

        [WebMethod]
        public DataSet getDataset(string command)
        {
            string[] prueba = getParametros("ConsolaAdmin");
            SqlConnection conn = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");               
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand(command, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
            };
            return ds;
        }

        [WebMethod]
        public DataSet getDatasetConexion(string command, string descrip)
        {
            string[] prueba = getParametros(descrip);
            SqlConnection conn = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand(command, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
            };
            return ds;
        }

        //[WebMethod]
        //public cFolio[] getInfo(string Folio)
        //{
        //    string[] prueba = getParametros("Solutia");
        //    SqlConnection conn = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");
        //    conn.Open();
        //    string select = "Select e.numEntrega, e.numTransporte, c.Nombre from Entregas e inner join catChoferTelefono c on e.numChofer= c.numChofer where e.numEntrega= '" + Folio + "'";
        //    SqlCommand cmd = new SqlCommand(select, conn);
        //    SqlDataReader reader = cmd.ExecuteReader();
        //    List<cFolio> lista = new List<cFolio>();
        //    while (reader.Read())
        //    {
        //        lista.Add(new cFolio(reader.GetString(0), reader.GetString(1)));
        //    }
        //    conn.Close();
        //    return lista.ToArray();
        //}

        [WebMethod]
        public cExcepcion[] getExcepciones()
        {
            string[] prueba = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");
            conn.Open();
            string select = "Select * from catExcepcion";
            SqlCommand cmd = new SqlCommand(select, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            List<cExcepcion> lista = new List<cExcepcion>();
            while (reader.Read())
            {
                lista.Add(new cExcepcion(reader.GetString(0), reader.GetString(1)));
            }
            conn.Close();

            return lista.ToArray();
        }

        [WebMethod]
        public string getIdChofer(String nombre)
        {
            string[] prueba = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");
            String result = "";

            try
            {
                //Conn = new SQLServerConnection("host=nc-star;port=1433;User ID=test01;Password=test01; Database Name=Test");                        
                conn.Open();
                Console.WriteLine("Conexion Exitosa");
                String sql = "SELECT numChofer FROM catChoferTelefono WHERE Nombre= '" + nombre + "';";
                SqlCommand comm = new SqlCommand(sql, conn);
                SqlDataReader rdr;

                rdr = comm.ExecuteReader();

                if (rdr.Read())
                {
                    result = rdr.GetString(0);
                }
                else
                {
                    result = "";
                }
            }
            catch (Exception ex)
            {
                // Connection failed
                Console.WriteLine(ex.Message);
            }
            return result;
            conn.Close();
        }

        [WebMethod]
        public String evento(String folio, int idEvento, int idExcepcion, String fecha, String hora)
        {
            String result = "";
            try
            {
                string[] prueba = getParametros("Solutia");
                SqlConnection conn = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");
                SqlCommand cmd = new SqlCommand("spInsertaMovimientos", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@p_NumEntrega", SqlDbType.Char, 10);
                cmd.Parameters["@p_NumEntrega"].Value = folio;
                cmd.Parameters.Add("@p_IdEvento", SqlDbType.Char, 1);
                cmd.Parameters["@p_IdEvento"].Value = idEvento.ToString();
                cmd.Parameters.Add("@p_IdExcepcion", SqlDbType.Char, 4);
                cmd.Parameters["@p_IdExcepcion"].Value = idExcepcion.ToString();
                cmd.Parameters.Add("@p_FechaRegistro", SqlDbType.Char, 8);
                cmd.Parameters["@p_FechaRegistro"].Value = fecha;
                cmd.Parameters.Add("@p_HoraRegistro", SqlDbType.Char, 6);
                cmd.Parameters["@p_HoraRegistro"].Value = hora;
                cmd.Parameters.Add("@p_Error", SqlDbType.Int);
                cmd.Parameters["@p_Error"].Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@p_Mensaje", SqlDbType.NVarChar, 255);
                cmd.Parameters["@p_Mensaje"].Direction = ParameterDirection.Output;
                conn.Open();

                cmd.ExecuteNonQuery();

                //int p_error = int.Parse(cmd.Parameters["@p_Error"].Value.ToString());
                string p_mensaje = (string)cmd.Parameters["@p_Mensaje"].Value.ToString();

                cmd.Dispose();
                conn.Close();

                result = "El folio: " + folio + " cambio de estado con exito: " + p_mensaje;

                if (idEvento == 4 || idEvento == 5)
                {
                    string[] prueba2 = getParametros("Intelisis");
                        SqlConnection conn2 = new SqlConnection("Data Source=" + prueba2[1] + "; Initial Catalog=" + prueba2[4] + "; Persist Security Info=True; User ID=" + prueba2[2] + "; Password=" + prueba2[3] + "");
                        conn2.Open();
                    try
                    {
                        String update = "Update Venta set EmbarqueEstado ='Entregado' where MovID ='" + folio + "' ";
                        SqlCommand cmd2 = new SqlCommand(update, conn2);
                        cmd2.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                    }
                    conn2.Close();
                }
                
            }catch(Exception ex)
            {
            
            };
            return result;
        }

        [WebMethod]
        public int getInt(string consulta, string conexion)
        {
            string[] prueba = getParametros(conexion);
            SqlConnection conn = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");
            int result = 0;
            conn.Open();      
            try
            {                                                   
                String sql = consulta;
                SqlCommand comm = new SqlCommand(sql, conn);
                SqlDataReader rdr;
                rdr = comm.ExecuteReader();
                if (rdr.Read())
                {
                    result = rdr.GetInt32(0);
                }
                else
                {
                    result = -1;
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                conn.Close();
            }
            return result;       
        }

        [WebMethod]
        public int inserta(string consulta,string conexion)
        {            
            int inserted =0;
            string[] prueba = getParametros(conexion);
            SqlConnection consolaAdmin = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");            
            consolaAdmin.Open();
            SqlCommand cmd = new SqlCommand(consulta, consolaAdmin);
            try
            {
                inserted = cmd.ExecuteNonQuery();
                consolaAdmin.Close();
            }
            catch (Exception ex)
            {
                return -1;
            }
            return inserted;
        }

        [WebMethod]
        public int getIdInv(string clave, string descripcion, string IdUsuario)
        {
            int insertados=0, idInv=0; 
            try
            {
                insertados = inserta("Insert INTO InventarioCongelado VALUES('" + clave + "','" + descripcion + "',getdate(),0,"+IdUsuario+")", "ConsolaAdmin");
                if (insertados > 0) 
                {
                    idInv = getInt("SELECT max(IDInv) from InventarioCongelado", "ConsolaAdmin");                               
                }
            }catch(Exception ex)
            {
                return -1;
            };
            return idInv;
        }

        [WebMethod]
        public int congelaInv(String IDInv, String consulta, String IdUsuario)
        {
            int res=0;           
            try
            {
                 string total= 
                " SET NOCOUNT ON; " +
                " DECLARE @IDPosicion int, @IDArticulo varchar(20), @EPCArt varchar(28), @Cant int; " +
                " DECLARE csr_CongelaAlm CURSOR FOR " + consulta +
                " OPEN csr_CongelaAlm; " +
                " FETCH NEXT FROM csr_CongelaAlm " +
                " INTO @IDPosicion, @IDArticulo, @EPCArt, @Cant; " +
                " WHILE @@fetch_status = 0 " +
                    " BEGIN " +
                        " IF @IDArticulo IS NOT NULL  " +
                        " BEGIN " +
                            " INSERT INTO detalleInvCong VALUES(" + IDInv + ",@IDArticulo,@EPCArt,'',@IDPosicion,0,0,@Cant,0," + IdUsuario + ")" +
                        " END " +
                        " FETCH NEXT FROM csr_CongelaAlm INTO @IDPosicion, @IDArticulo, @EPCArt, @Cant" +
                    " END" +
                " CLOSE csr_CongelaAlm; " +
                " DEALLOCATE csr_CongelaAlm; ";
               res = inserta(total, "ConsolaAdmin");
              // res = res*(-1);
            }
            catch (Exception ex) 
            {
                return -1;
            };
            return res;
        }

        public int longitud(string numChofer)
        {
            int total = 0;
            string[] prueba2 = getParametros("Intelisis");
            SqlConnection conn2 = new SqlConnection("Data Source=" + prueba2[1] + "; Initial Catalog=" + prueba2[4] + "; Persist Security Info=True; User ID=" + prueba2[2] + "; Password=" + prueba2[3] + "");
            conn2.Open();
            string select = "select count(all EmbarqueMov.MovId) from EmbarqueMov inner join EmbarqueD on EmbarqueMov.ID = EmbarqueD.EmbarqueMov inner join Embarque on EmbarqueMov.AsignadoID = Embarque.ID inner join Venta on EmbarqueMov.MovID = Venta.MovID where Embarque.Estatus = 'PENDIENTE' and Embarque.Agente='" + numChofer + "';";
            SqlCommand cmd = new SqlCommand(select, conn2);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                total = reader.GetInt32(0);
            }
            return total;
        }

    }
}
