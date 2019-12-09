using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using System.Collections;
//xusing Project_RICSAWS;

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
                //SqlConnection conn = new SqlConnection("Data Source=192.168.21.223;Initial Catalog=NapresaPar;Persist Security Info=True;User ID=sa;Password=Adminpwd20");
                SqlConnection conn = new SqlConnection("Data Source=192.168.0.229;Initial Catalog=napresaPar;Persist Security Info=True;User ID=sa;Password=NapresaPwd20; timeout=10");
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
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return result;

        }

        [WebMethod]
        public string logearse(string numChofer, string imei)
        {

            string[] prueba = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");
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
                    int cosa = 1;
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

            string select = "select EmbarqueMov.MovId, Venta.ID, Embarque.Agente, Embarque.Vehiculo from EmbarqueMov inner join EmbarqueD on EmbarqueMov.ID = EmbarqueD.EmbarqueMov inner join Embarque on EmbarqueMov.AsignadoID = Embarque.ID inner join Venta on EmbarqueMov.MovID = Venta.MovID where Embarque.Estatus = 'PENDIENTE' and Embarque.Agente='" + numChofer + "' and Venta.EmbarqueEstado != 'Entregado'";
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
                String pin = "";
                for (int i = 0; i < contador; i++)
                {
                    pin = reader.GetValue(1) + "";
                    folio[i] = reader.GetString(0);
                    agente[i] = reader.GetString(2);
                    transporte[i] = reader.GetString(3);
                    //lista.Add(new cFolio(reader.GetString(0), pin, reader.GetString(2), reader.GetString(3)));

                }
                listaMovil.Add(new cFolio(reader.GetString(0), pin));
            }

            conn.Close();
            string[] prueba2 = getParametros("Solutia");
            SqlConnection conn2 = new SqlConnection("Data Source=" + prueba2[1] + "; Initial Catalog=" + prueba2[4] + "; Persist Security Info=True; User ID=" + prueba2[2] + "; Password=" + prueba2[3] + "");
            conn2.Open();
            for (int i = 0; i < folio.Length; i++)
            {
                string select2 = "Select count(*) as total from Entregas where numEntrega = '" + folio[i] + "'";
                SqlCommand cmd3 = new SqlCommand(select2, conn2);
                SqlDataReader reader2 = cmd3.ExecuteReader();
                //int result = int.Parse(reader2.GetString(0));
                int result = 0;
                while (reader2.Read())
                {
                    String pin2 = reader2.GetValue(0) + "";
                    if (pin2.Equals("0"))
                    {
                    }
                    else
                    {
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
            string[] res = new string[5];
            consolaAdmin.Open();
            SqlCommand cmd = new SqlCommand("select idUsuario, nombre, aPaterno, idCentro,usuario from Usuarios where usuario = '" + user + "' and password ='" + password + "'", consolaAdmin);
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
                    res[4] = dr["usuario"].ToString();
                }
                else
                {
                    res[0] = res[1] = res[2] = res[3] = res[4] = "0";
                }
                dr.Close();
                consolaAdmin.Close();
            }
            catch (Exception ex)
            {
                string error = ex.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
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
                string error = ex.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
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
                string error = ex.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return ds;
        }

        [WebMethod]
        public cExcepcion[] getExcepciones()
        {
            List<cExcepcion> lista = new List<cExcepcion>();
            try{
            string[] prueba = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");
            conn.Open();
            string select = "Select * from catExcepcion";
            SqlCommand cmd = new SqlCommand(select, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new cExcepcion(reader.GetString(0), reader.GetString(1)));
            }
            conn.Close();
            }catch(Exception e){
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return null;
            }
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
                Console.WriteLine(ex.Message);
            }
            conn.Close();
            return result;
        }

        [WebMethod]
        public String evento(String folio, int idEvento, int idExcepcion, String fecha, String hora, int idEmbarque, String folioEmbarque,string user)
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
                //cmd.Parameters.Add("@p_Error", SqlDbType.Int);
                //cmd.Parameters["@p_Error"].Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@p_Mensaje", SqlDbType.NVarChar, 255);
                cmd.Parameters["@p_Mensaje"].Direction = ParameterDirection.Output;
                conn.Open();

                cmd.ExecuteNonQuery();

                //int p_error = int.Parse(cmd.Parameters["@p_Error"].Value.ToString());
                string p_mensaje = (string)cmd.Parameters["@p_Mensaje"].Value.ToString();

                cmd.Dispose();
                conn.Close();

                if (p_mensaje.Contains("Exito"))
                {
                    result = "El folio: " + folio + " cambio de estado con exito: " + p_mensaje;
                }
                else
                {
                    result = "El evento enviado para el folio: " + folio + " presento la siguiente inconsistencia: " + p_mensaje;
                }

                if (idEvento == 4 || idEvento == 5)
                {
                    string[] prueba2 = getParametros("Intelisis");
                    SqlConnection conn2 = new SqlConnection("Data Source=" + prueba2[1] + "; Initial Catalog=" + prueba2[4] + "; Persist Security Info=True; User ID=" + prueba2[2] + "; Password=" + prueba2[3] + "");
                    conn2.Open();
                    try
                    {
                        //String update = "Update Venta set EmbarqueEstado ='Entregado' where MovID ='" + folio + "' ";
                        //string update = "exec spAfectar 'EMB', 36, 'AFECTAR', 'Todo', NULL, 'UA-00039'";
                        string update = "Update EmbarqueD set Estado = 'Entregado' where ID =" + idEmbarque;
                        SqlCommand cmdUpdate = new SqlCommand(update, conn2);
                        string afectar = "exec spAfectar 'EMB'," + idEmbarque + ",'AFECTAR','Todo',NULL,'"+user+"'";
                        SqlCommand cmd2 = new SqlCommand(afectar, conn2);
                        cmdUpdate.ExecuteNonQuery();
                        cmd2.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                    }
                    conn2.Close();
                    string[] prueba3 = getParametros("Solutia");
                    SqlConnection conn3 = new SqlConnection("Data Source=" + prueba3[1] + "; Initial Catalog=" + prueba3[4] + "; Persist Security Info=True; User ID=" + prueba3[2] + "; Password=" + prueba3[3] + "");
                    conn3.Open();
                    try
                    {
                        string update = "UPDATE Entregas set fechaEntrega = '" + fecha + "' where numEntrega='" + folio + "'";
                        SqlCommand cmd3 = new SqlCommand(update, conn3);
                        cmd3.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                    }
                    conn3.Close();
                }

            }
            catch (Exception ex)
            {
                string error = ex.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
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
        public int inserta(string consulta, string conexion)
        {
            int inserted = 0;
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
                string error = ex.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return -1;
            }
            return inserted;
        }

        [WebMethod]
        public int getIdInv(string clave, string descripcion, string IdUsuario)
        {
            int insertados = 0, idInv = 0;
            try
            {
                insertados = inserta("Insert INTO InventarioCongelado VALUES('" + clave + "','" + descripcion + "',getdate(),0," + IdUsuario + ")", "ConsolaAdmin");
                if (insertados > 0)
                {
                    idInv = getInt("SELECT max(IDInv) from InventarioCongelado", "ConsolaAdmin");
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return -1;
            };
            return idInv;
        }

        [WebMethod]
        public int congelaInv(String IDInv, String consulta, String IdUsuario)
        {
            int res = 0;
            try
            {
                string total =
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
                string error = ex.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
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

        [WebMethod]
        public string[] getDetalleOrden(string folio, string articulo)
        {
            string[] parametros = getParametros("Intelisis");
            string[] orden = new string[7];
            try{
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            conn.Open();

            string select = "select MovID,ArtDescripcion,Cantidad,Id,Estatus from ProdPendienteD where MovID = '" + folio + "' and Articulo = '" + articulo+ "'";

            SqlCommand cmd = new SqlCommand(select, conn);

            SqlDataReader reader = cmd.ExecuteReader();

            string[] result = new string[7];
            while (reader.Read())
            {
                result[0] = reader.GetInt32(0).ToString();
                result[1] = reader.GetString(1);
                result[2] = reader.GetString(2);
                result[3] = reader.GetString(3);
                result[4] = reader.GetInt32(4).ToString();
                result[5] = reader.GetString(5);
                result[6] = reader.GetInt32(6).ToString();
            }

            for (int i = 0; i < result.Length; i++)
            {
                orden[i] = result[i];
            }
            }catch(Exception e){
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return null;
            }
            return orden;
        }

        [WebMethod]
        public string[] getConteoProduccion(string folio, string articulo)
        {
            string[] parametros = getParametros("Intelisis");
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            conn.Open();

            string select = "select ArtDescripcion,Cantidad from ProdPendienteD where MovID = '" + folio + "' and Articulo ='" + articulo + "'";

            SqlCommand cmd = new SqlCommand(select, conn);

            SqlDataReader reader = cmd.ExecuteReader();

            string[] result = new string[2];
            while (reader.Read())
            {
                result[0] = reader.GetString(0);
                result[1] = reader.GetDouble(1).ToString();
            }

            string[] orden = new string[2];
            for (int i = 0; i < result.Length; i++)
            {
                orden[i] = result[i];
            }
            return orden;
        }
        
        [WebMethod]
        public DataSet getOrdenesProduccion()
        {
            string[] parametros = getParametros("Intelisis");
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            conn.Open();

            string select = "select * from ProdPendienteD where Estatus != 'TERMINADO'";

            SqlDataAdapter da = new SqlDataAdapter(select, conn);

            da.Fill(ds);
            conn.Close();
            return ds;
        }

        [WebMethod]
        public void confirmarProduccion(string folio, string producto, string cantidad)
        {
            string[] parametros = getParametros("Solutia");
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            conn.Open();

            string update = "UPDATE OrdenesProduccion Set Producto='" + producto + "' Cantidad =" + cantidad + " where FolioOP='" + folio + "";
            SqlCommand cmd = new SqlCommand(update, conn);
            cmd.ExecuteNonQuery();

            conn.Close();

        }

        [WebMethod]
        public string[] getProducto(int folio)
        {
            //string[] parametros = getParametros("WebserviceS");
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            conn.Open();

            //string select = "SELECT descripcion "
            //string select = "SELECT Descripcion1 FROM Art where Descripcion1 is not null";
            //SqlCommand cmd = new SqlCommand(select, conn);

            //SqlDataReader reader = cmd.ExecuteReader();

            String[] result = { };
            //int i = 0;
            //while (reader.Read())
            //{
            //    result[i] = reader.GetString(0);
            //    i++;
            //-

            return result;
        }

        [WebMethod]
        public string Pedido_Stock(String producto)
        {
            string resultado = "";

            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            conn.Open();

            string select = "Select * from Productos where Descripcion = '" + producto + "'";
            SqlCommand cmd = new SqlCommand(select, conn);

            SqlDataReader reader = cmd.ExecuteReader();

            string[] result = new string[5];
            while (reader.Read())
            {
                result[0] = reader.GetInt32(0).ToString();
                result[1] = reader.GetString(1);
                result[2] = reader.GetString(2);
                result[3] = reader.GetInt32(3).ToString();
                result[4] = reader.GetInt32(4).ToString();
            }
            resultado = result[4];

            return resultado;
        }

        [WebMethod]
        public DataSet getOrdenesCompletadas()
        {
            DataSet ds = new DataSet();
            try
            {
                string[] parametros = getParametros("Solutia");
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
                conn.Open();

                string select = "SELECT Id,OrdenProduccion,Pedido,Cliente,Descripcion,Tipo,Medida,Color,Codigo,Cantidad,Estatus,Renglon,Asignado,prodEstibado,prodAsignado FROM catProd WHERE Status = 'LIBERADO'";

                SqlDataAdapter da = new SqlDataAdapter(select, conn);

                da.Fill(ds);
                conn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return ds;
        }

        [WebMethod]
        public string[] getLIBERADO(string folio)
        {
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            conn.Open();
            string select = "SELECT cp.Pedido, cp.OrdenProduccion, cp.Cliente, cp.Tarima, cp.Descripcion, cp.Tipo, cp.Medida, cp.Color,  drp.c, cp.Codigo, cp.Cantidad FROM catProd cp inner join DetRProd drp on drp.OrdenProduccion = cp.OrdenProduccion and drp.CodigoProducto = cp.Codigo where cp.OrdenProduccion '= "+folio+"'";
            SqlCommand cmd = new SqlCommand(select, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            string[] result = new string[11];
            while (reader.Read())
            {
                result[0] = reader.GetInt32(0).ToString();
                result[1] = reader.GetString(1);
                result[2] = reader.GetString(2);
                result[3] = reader.GetInt32(3).ToString();
                result[4] = reader.GetString(4);
                result[5] = reader.GetString(5);
                result[6] = reader.GetString(6);
                result[7] = reader.GetString(7);
                result[8] = reader.GetInt32(8).ToString();
                result[9] = reader.GetString(9);
                result[10] = reader.GetInt32(10).ToString();
            }
            return result;
        }

        [WebMethod]
        public string Libera(string folio, string renglon)
        {
            string result = "";
            string[] parametros2 = getParametros("Solutia");
            try
            {
                SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + ";MultipleActiveResultSets=True");
                conn2.Open();
                string update = "update catProd set Estatus = 'LIBERADO' where OrdenProduccion='" + folio + "' and Renglon=" + renglon + "";
                SqlCommand cmd = new SqlCommand(update, conn2);
                cmd.ExecuteNonQuery();
                result = "Cambio Exitoso";
                conn2.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return result;
        }
        
        [WebMethod]
        public string avanzarEstado(string folio, string estado, string id, string renglon, int cantidad, string user)
        {
            string result ="";
            string[] parametros = getParametros("Solutia");
            string[] parametros2 = getParametros("intelisis");
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
                SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + ";MultipleActiveResultSets=True");
                conn.Open();
                conn2.Open();
                string select = "SELECT Codigo from CatProd where OrdenProduccion ='" + folio + "' and renglon = " + renglon + "";
                SqlCommand cmdSelect = new SqlCommand(select, conn);
                SqlDataReader reader = cmdSelect.ExecuteReader();
                string codigo = "";
                if (reader.Read())
                {
                    codigo = reader.GetString(0);
                }
                else
                {
                    return result = "X";
                }
                int i = 0;
                int qtyAsignado = 0;
                int max = maxRenglonSub(id, codigo);
                string selectAsignado = "SELECT prodAsignado from CatProd where OrdenProduccion ='" + folio + "' and renglon = " + renglon + "";
                SqlCommand cmdSelectAsignado = new SqlCommand(selectAsignado, conn);
                SqlDataReader readerAsignado = cmdSelectAsignado.ExecuteReader();
                if (readerAsignado.Read())
                {
                    qtyAsignado = int.Parse(readerAsignado.GetValue(0).ToString());
                }
                else
                {
                    return result = "X";
                }
                int qtyNvo = qtyAsignado - cantidad;
                int qtyCur = currentValue(folio, estado, id, renglon);
                qtyCur = qtyCur + cantidad;
                do
                {
                    Decimal total = getTotalCantPend(id, renglon, i, codigo);
                    if (total > 0 && cantidad > 0)
                    {
                        if (cantidad >= total)
                        {
                            string centroDestino = "SELECT Centro,CentroDestino from ProdD where id = " + id + " and Renglon = " + renglon + " and RenglonSub = " + i;
                            SqlCommand cmdDestino = new SqlCommand(centroDestino, conn2);
                            SqlDataReader readerDestino = cmdDestino.ExecuteReader();
                            string[] destino = new string[2];
                            if (readerDestino.Read())
                            {
                                destino[0] = readerDestino.GetValue(0).ToString();
                                destino[1] = readerDestino.GetValue(1).ToString();
                            }
                            else
                            {
                                i++;
                            } if (!destino[0].Contains("CURAD") && destino[1].Contains("CURAD"))
                            {
                                string updateProd = "update ProdD set CantidadA = " + total + " where id = " + id + " and Renglon = " + renglon + " and RenglonSub = " + i;
                                SqlCommand cmdUpdateProd = new SqlCommand(updateProd, conn2);
                                cmdUpdateProd.ExecuteNonQuery();
                                string spUpdate = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Seleccion', 'Avance', '" + user + "', @Estacion=99";
                                SqlCommand cmd2 = new SqlCommand(spUpdate, conn2);
                                cmd2.ExecuteNonQuery();
                                string spFinal = "EXEC spAfectar 'PROD', " + getIdProd() + ", 'AFECTAR', 'Todo', NULL, '" + user + "', @Estacion=99";
                                SqlCommand cmd3 = new SqlCommand(spFinal, conn2);
                                cmd3.ExecuteNonQuery();
                                string updateParcialidad = "UPDATE catProd set prodAsignado = " + qtyNvo + ", Estatus = 'CURADO', prodCurado = " + qtyCur + " where OrdenProduccion ='" + folio + "' and renglon = " + renglon + "";
                                SqlCommand cmdParcialidad = new SqlCommand(updateParcialidad, conn);
                                cmdParcialidad.ExecuteNonQuery();
                                result = "Cambio Exitoso";
                                break;
                            }
                            else if (!destino[0].Contains("CURAD") && !destino[1].Contains("CURAD"))
                            {
                                string updateParcialidad = "UPDATE catProd set prodAsignado = " + qtyNvo + ", Estatus = 'CURADO', prodCurado = " + qtyCur + " where OrdenProduccion ='" + folio + "' and renglon = " + renglon + "";
                                SqlCommand cmdParcialidad = new SqlCommand(updateParcialidad, conn);
                                cmdParcialidad.ExecuteNonQuery();
                                result = "Cambio Exitoso";
                                break;
                            }
                            else
                            {
                                i++;
                            }
                        }
                        else if (total > cantidad)
                        {
                            string centroDestino = "SELECT Centro,CentroDestino from ProdD where id = " + id + " and Renglon = " + renglon + " and RenglonSub = " + i;
                            SqlCommand cmdDestino = new SqlCommand(centroDestino, conn2);
                            SqlDataReader readerDestino = cmdDestino.ExecuteReader();
                            string[] destino = new string[2];
                            if (readerDestino.Read())
                            {
                                destino[0] = readerDestino.GetValue(0).ToString();
                                destino[1] = readerDestino.GetValue(1).ToString();
                            }
                            else
                            {
                                i++;
                            } if (!destino[0].Contains("CURAD") && destino[1].Contains("CURAD"))
                            {
                                string updateProd = "update ProdD set CantidadA = " + cantidad + " where id = " + id + " and Renglon = " + renglon + " and RenglonSub = " + i;
                                SqlCommand cmdUpdateProd = new SqlCommand(updateProd, conn2);
                                cmdUpdateProd.ExecuteNonQuery();
                                string spUpdate = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Seleccion', 'Avance', '" + user + "', @Estacion=99";
                                SqlCommand cmd2 = new SqlCommand(spUpdate, conn2);
                                cmd2.ExecuteNonQuery();
                                string spFinal = "EXEC spAfectar 'PROD', " + getIdProd() + ", 'AFECTAR', 'Todo', NULL, '" + user + "', @Estacion=99";
                                SqlCommand cmd3 = new SqlCommand(spFinal, conn2);
                                cmd3.ExecuteNonQuery();
                                string updateParcialidad = "UPDATE catProd set prodAsignado = " + qtyNvo + ", Estatus = 'CURADO', prodCurado = " + qtyCur + " where OrdenProduccion ='" + folio + "' and renglon = " + renglon + "";
                                SqlCommand cmdParcialidad = new SqlCommand(updateParcialidad, conn);
                                cmdParcialidad.ExecuteNonQuery();
                                result = "Cambio Exitoso";
                                break;
                            }
                            else if (!destino[0].Contains("CURAD") && !destino[1].Contains("CURAD"))
                            {
                                string updateParcialidad = "UPDATE catProd set prodAsignado = " + qtyNvo + ", Estatus = 'CURADO', prodCurado = " + qtyCur + " where OrdenProduccion ='" + folio + "' and renglon = " + renglon + "";
                                SqlCommand cmdParcialidad = new SqlCommand(updateParcialidad, conn);
                                cmdParcialidad.ExecuteNonQuery();
                                result = "Cambio Exitoso";
                                break;
                            }
                            else
                            {
                                i++;
                            }
                        }
                    }
                    else
                    {
                        i++;
                    }
                } while (i <= max);
                //string updateSolutia = "update catProd set Estatus = 'PENDIENTE', Asignado = 0 where OrdenProduccion='" + folio + "' and Renglon=" + renglon + "";
                //SqlCommand cmdSolutia = new SqlCommand(updateSolutia, conn);
                //cmdSolutia.ExecuteNonQuery();
                conn.Close();
                conn2.Close();
            }
            catch (Exception e)
            {
                result = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return result;
        }

        [WebMethod]
        public string avanzarEstadoParcial(string folio, string estado, string id, string renglon, int cantidad,string user)
            {
            string result = "";
            string[] parametros = getParametros("Solutia");
            string[] parametros2 = getParametros("intelisis");
            int oldQty = 0;
            try
            {
                
                DataSet ds = new DataSet();
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
                SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + ";MultipleActiveResultSets=True");
                conn.Open();
                conn2.Open();
                string select = "SELECT prodAsignado,Codigo from CatProd where OrdenProduccion ='" + folio + "' and renglon = " + renglon + "";
                SqlCommand cmdSelect = new SqlCommand(select, conn);
                SqlDataReader reader = cmdSelect.ExecuteReader();
                string codigo = "";
                if (reader.Read())
                {
                    oldQty = int.Parse(reader.GetInt32(0).ToString());
                    codigo = reader.GetString(1);
                }
                else
                {
                    return result = "X";
                }
                
                if (estado == "PENDIENTE" || estado == "PRODUCCION")
                {
                    int max = maxRenglonSub(id, codigo);
                    string[] centro = CentroTrabajo(max.ToString(),codigo);
                    if (centro[0].Contains("CURAD"))
                    {
                        
                        int qtyCur = currentValue(folio, estado, id, renglon);
                        qtyCur = qtyCur + cantidad;
                        int updateQty = nvaCantidad(folio, id, renglon);
                        updateQty = updateQty - cantidad;
                        string update = "update ProdD set CantidadA = " + cantidad + " where id=" + id + " and Renglon=" + renglon + " and RenglonSub = " + max;
                        string spUpdate = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Seleccion', 'Avance', '"+user+"', @Estacion=99";
                        SqlCommand cmd = new SqlCommand(update, conn2);
                        SqlCommand cmd2 = new SqlCommand(spUpdate, conn2);
                        cmd.ExecuteNonQuery();
                        cmd2.ExecuteNonQuery();
                        string spFinal = "EXEC spAfectar 'PROD'," + getIdProd() + ", 'AFECTAR', 'Todo', 'Avance', '"+user+"', @Estacion=99";
                        SqlCommand cmd3 = new SqlCommand(spFinal, conn2);
                        cmd3.ExecuteNonQuery();
                        Decimal nvoQty = oldQty - cantidad;
                        string updateParcialidad = "UPDATE catProd set prodAsignado = " + nvoQty + ", Estatus = 'CURADO', prodCurado = " + qtyCur + " where OrdenProduccion ='" + folio + "' and renglon = " + renglon + "";
                        SqlCommand cmdParcialidad = new SqlCommand(updateParcialidad, conn);
                        cmdParcialidad.ExecuteNonQuery();
                        result = "Cambio Exitoso";
                    }
                    else
                    {
                        max = max - 1;
                        int qtyCur = currentValue(folio, estado, id, renglon);
                        qtyCur = qtyCur + cantidad;
                        string update = "update ProdD set CantidadA = " + cantidad + " where id=" + id + " and Renglon=" + renglon + " and RenglonSub = " + maxRenglonSub(id, codigo);
                        string spUpdate = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Seleccion', 'Avance', '"+user+"', @Estacion=99";
                        SqlCommand cmd = new SqlCommand(update, conn2);
                        SqlCommand cmd2 = new SqlCommand(spUpdate, conn2);
                        cmd.ExecuteNonQuery();
                        cmd2.ExecuteNonQuery();
                        string spFinal = "EXEC spAfectar 'PROD'," + getIdProd() + ", 'AFECTAR', 'Todo', 'Avance', '"+user+"', @Estacion=99";
                        SqlCommand cmd3 = new SqlCommand(spFinal, conn2);
                        cmd3.ExecuteNonQuery();
                        Decimal nvoQty = oldQty - cantidad;
                        string updateParcialidad = "UPDATE catProd set prodAsignado = " + nvoQty + ", Estatus = 'CURADO', prodCurado = " + qtyCur + " where OrdenProduccion ='" + folio + "' and renglon = " + renglon + "";
                        SqlCommand cmdParcialidad = new SqlCommand(updateParcialidad, conn);
                        cmdParcialidad.ExecuteNonQuery();
                        result = "Cambio Exitoso";
                    }
                }
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return result;
        }

        [WebMethod]
        public string EntradaProduccion(string folio, string estado, string id, string renglon,string user)
        {
            string result = "";
            string[] parametros = getParametros("Solutia");
            string[] parametros2 = getParametros("intelisis");
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
                SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + ";MultipleActiveResultSets=True");
                conn.Open();
                conn2.Open();
                string spUpdate = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Pendiente', 'Entrada Produccion', '"+user+"', @Estacion=99";
                //SqlCommand cmd = new SqlCommand(spUpdate, conn);
                SqlCommand cmd2 = new SqlCommand(spUpdate, conn2);
                //cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                string spFinal = "EXEC spAfectar 'PROD'," + getIdProd() + ", 'AFECTAR', 'Todo', Null, '"+user+"', @Estacion=99";
                SqlCommand cmd3 = new SqlCommand(spFinal, conn2);
                cmd3.ExecuteNonQuery();
                string updateSolutia = "update catProd set Estatus = 'CONCLUIDO' where OrdenProduccion='" + folio + "' and Renglon=" + renglon + "";
                SqlCommand cmdSolutia = new SqlCommand(updateSolutia, conn);
                cmdSolutia.ExecuteNonQuery();
                result = "Cambio Exitoso";
                conn.Close();
                conn2.Close();
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            return result;
        }

        [WebMethod]
        public string EntradaProduccionParcial(string folio, string estado, string id, string renglon,Decimal cantidad, string user)
        {
            string result = "";
            string[] parametros = getParametros("Solutia");
            string[] parametros2 = getParametros("intelisis");
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
                SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + ";MultipleActiveResultSets=True");
                conn.Open();
                conn2.Open();
                //string updateProd = "update ProdD set CantidadA = " + cantidad + " where id=" + id + " and Renglon=" + renglon + "";
                //SqlCommand cmdUpdateProd = new SqlCommand(updateProd,conn2);
                //cmdUpdateProd.ExecuteNonQuery();
                string select = "SELECT Codigo from CatProd where OrdenProduccion ='" + folio + "' and renglon = " + renglon + "";
                SqlCommand cmdSelect = new SqlCommand(select, conn);
                SqlDataReader reader = cmdSelect.ExecuteReader();
                string codigo = "";
                if (reader.Read())
                {
                    codigo = reader.GetString(0);
                }
                else
                {
                    return result = "X";
                }
                int i = 0;
                int max = maxRenglonSub(id, codigo) + 1;
                do
                {
                    Decimal total = getTotalCantPend(id, renglon, i, codigo);
                    if (total > 0 && cantidad > 0)
                    {
                        if (cantidad >= total)
                        {
                            string centroDestino = "select Centro,CentroDestino from ProdD where id=" + id + " and Renglon=" + renglon + " and RenglonSub = " + i;
                            SqlCommand cmdDestino = new SqlCommand(centroDestino, conn2);
                            SqlDataReader readerDestino = cmdDestino.ExecuteReader();
                            string destino = "", cDestino = "";
                            if (readerDestino.Read())
                            {
                                destino = readerDestino.GetValue(0).ToString();
                                cDestino = readerDestino.GetValue(1).ToString();
                            }
                            else
                            {
                                i++;
                            }

                            if (destino.Contains("CURAD"))
                            {
                                string updateProd = "update ProdD set CantidadA = " + cantidad + " where id=" + id + " and Renglon=" + renglon + " and RenglonSub = " + i;
                                SqlCommand cmdUpdateProd = new SqlCommand(updateProd, conn2);
                                cmdUpdateProd.ExecuteNonQuery();
                                string spUpdate = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Seleccion', 'Entrada Produccion', '" + user + "', @Estacion=99";
                                //SqlCommanxd cmd = new SqlCommand(spUpdate, conn);
                                SqlCommand cmd2 = new SqlCommand(spUpdate, conn2);
                                //cmd.ExecuteNonQuery();
                                cmd2.ExecuteNonQuery();
                               string spFinal = "EXEC spAfectar 'PROD'," + getIdProd() + ", 'AFECTAR', 'Todo', Null, '" + user + "', @Estacion=99";
                                SqlCommand cmd3 = new SqlCommand(spFinal, conn2);
                                cmd3.ExecuteNonQuery();
                                i++;
                                cantidad = cantidad - total;
                            }
                            else if (!destino.Contains("CURAD") && !cDestino.Contains("CURAD"))
                            {
                                string updateProd = "update ProdD set CantidadA = " + cantidad + " where id=" + id + " and Renglon=" + renglon + " and RenglonSub = " + i;
                                SqlCommand cmdUpdateProd = new SqlCommand(updateProd, conn2);
                                cmdUpdateProd.ExecuteNonQuery();
                                string spUpdate = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Seleccion', 'Entrada Produccion', '" + user + "', @Estacion=99";
                                //SqlCommand cmd = new SqlCommand(spUpdate, conn);
                                SqlCommand cmd2 = new SqlCommand(spUpdate, conn2);
                                //cmd.ExecuteNonQuery();
                                cmd2.ExecuteNonQuery();
                                string spFinal = "EXEC spAfectar 'PROD'," + getIdProd() + ", 'AFECTAR', 'Todo', Null, '" + user + "', @Estacion=99";
                                SqlCommand cmd3 = new SqlCommand(spFinal, conn2);
                                cmd3.ExecuteNonQuery();
                                i++;
                            }
                            else
                            {
                                i++;
                            }
                        }
                        else if (total > cantidad)
                        {
                            string centroDestino = "select Centro,CentroDestino from ProdD where id=" + id + " and Renglon=" + renglon + " and RenglonSub = " + i;
                            SqlCommand cmdDestino = new SqlCommand(centroDestino, conn2);
                            SqlDataReader readerDestino = cmdDestino.ExecuteReader();
                            string destino = "",cDestino = "";
                            if (readerDestino.Read())
                            {
                                destino = readerDestino.GetValue(0).ToString();
                                cDestino = readerDestino.GetValue(1).ToString();
                            }
                            else
                            {
                                i++;
                            }
                            if (destino.Contains("CURAD"))
                            {
                                string updateProd = "update ProdD set CantidadA = " + cantidad + " where id=" + id + " and Renglon=" + renglon + " and RenglonSub = " + i;
                                SqlCommand cmdUpdateProd = new SqlCommand(updateProd, conn2);
                                cmdUpdateProd.ExecuteNonQuery();
                                string spUpdate = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Seleccion', 'Entrada Produccion', '"+user+"', @Estacion=99";
                                //SqlCommand cmd = new SqlCommand(spUpdate, conn);
                                SqlCommand cmd2 = new SqlCommand(spUpdate, conn2);
                                //cmd.ExecuteNonQuery();
                                cmd2.ExecuteNonQuery();
                                string spFinal = "EXEC spAfectar 'PROD'," + getIdProd() + ", 'AFECTAR', 'Todo', Null, '"+user+"', @Estacion=99";
                                SqlCommand cmd3 = new SqlCommand(spFinal, conn2);
                                cmd3.ExecuteNonQuery();
                                i = maxRenglonSub(id, codigo) + 1;
                            }
                            else if (!destino.Contains("CURAD") && !cDestino.Contains("CURAD"))
                            {
                                string updateProd = "update ProdD set CantidadA = " + cantidad + " where id=" + id + " and Renglon=" + renglon + " and RenglonSub = " + i;
                                SqlCommand cmdUpdateProd = new SqlCommand(updateProd, conn2);
                                cmdUpdateProd.ExecuteNonQuery();
                                string spUpdate = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Seleccion', 'Entrada Produccion', '" + user + "', @Estacion=99";
                                //SqlCommand cmd = new SqlCommand(spUpdate, conn);
                                SqlCommand cmd2 = new SqlCommand(spUpdate, conn2);
                                //cmd.ExecuteNonQuery();
                                cmd2.ExecuteNonQuery();
                                string spFinal = "EXEC spAfectar 'PROD'," + getIdProd() + ", 'AFECTAR', 'Todo', Null, '" + user + "', @Estacion=99";
                                SqlCommand cmd3 = new SqlCommand(spFinal, conn2);
                                cmd3.ExecuteNonQuery();
                                i = maxRenglonSub(id, codigo) + 1;
                            }
                            else
                            {
                                i++;
                            }
                        }
                    }
                    else
                    {
                        i++;
                    }
                } while (i <= max);
                int[] curado = CurrentCurado(codigo,folio);
                if (curado[0] == 0)
                {
                    string updateSolutia = "update catProd set Estatus = 'PENDIENTE', Asignado = 0 where OrdenProduccion='" + folio + "' and Renglon=" + renglon + "";
                    SqlCommand cmdSolutia = new SqlCommand(updateSolutia, conn);
                    cmdSolutia.ExecuteNonQuery();
                    result = "Cambio Exitoso";
                    conn.Close();
                    conn2.Close();
                }
                else
                {
                    string updateSolutia = "update catProd set Estatus = 'PENDIENTE' where OrdenProduccion='" + folio + "' and Renglon=" + renglon + "";
                    SqlCommand cmdSolutia = new SqlCommand(updateSolutia, conn);
                    cmdSolutia.ExecuteNonQuery();
                    result = "Cambio Exitoso";
                    conn.Close();
                    conn2.Close();
                }
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return result;
        }
        
        [WebMethod]
        public DataSet getRacks(string OP, string codigo, string renglon)
        {
            DataSet ds = new DataSet();
            try
            {
                string[] parametros = getParametros("Solutia");
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
                conn.Open();
                string select = "Select dp.EPC, rp.Modelo, dp.Numero, dp.OrdenProduccion, dp.CodigoProducto  from DetRProd dp inner join RProduccion rp on rp.IdRack = dp.IdRProd where OrdenProduccion = '" + OP + "' and CodigoProducto = '" + codigo + "' and Renglon = " + renglon + " and Estado = 1 and Verificado = 0";

                SqlDataAdapter da = new SqlDataAdapter(select, conn);

                da.Fill(ds);
                conn.Close();
            }
            catch (SqlException e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return ds;
        }

        [WebMethod]
        public DataSet getRacksLlenado(string OP)
        {
            DataSet ds = new DataSet();
            try
            {
                string[] parametros = getParametros("Solutia");
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
                conn.Open();
                string select = "Select dp.EPC,dp.Numero,rp.Modelo as Modelo,dp.OrdenProduccion,dp.CantidadEstimada as [C.Estimada], dp.CantidadReal as [C.Real] from DetRProd dp inner join RProduccion rp on rp.IdRack = dp.IdRProd where OrdenProduccion = '" + OP + "' and Estado = 1 and Verificado = 1 and Contado = 0";
                SqlDataAdapter da = new SqlDataAdapter(select, conn);

                da.Fill(ds);
                conn.Close();
            }
            catch (SqlException e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return ds;
        }

        [WebMethod]
        public List<cRack> getPersonas(string OP)
        {
            List<cRack> racks = new List<cRack>();
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            conn.Open();
            string select = "Select dp.EPC,dp.Numero,rp.Modelo as Modelo,dp.OrdenProduccion,dp.CantidadEstimada as [C.Estimada], dp.CantidadReal as [C.Real] from DetRProd dp inner join RProduccion rp on rp.IdRack = dp.IdRProd where OrdenProduccion = '" + OP + "' and Estado = 1 and Verificado = 1 and Contado = 0";
            SqlCommand cmd = new SqlCommand(select, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            SqlDataReader ded = cmd.ExecuteReader();

            while (ded.Read())
            {
                cRack cr = new cRack();
                cr.EPC = ded.GetString(0);
                cr.numero = ded.GetInt32(1);
                cr.modelo = ded.GetString(2);
                cr.ordenProduccion = ded.GetString(3);
                cr.cantidadEstimada = ded.GetInt32(4) ;
                cr.cantidadReal = ded.GetInt32(5);
                racks.Add(cr);
            }
            return racks;
        }

        [WebMethod]
        public int sizeList(string OP)
        {
            List<cRack> racks = new List<cRack>();
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            conn.Open();
            string select = "Select dp.EPC,dp.Numero,rp.Modelo as Modelo,dp.OrdenProduccion,dp.CantidadEstimada as [C.Estimada], dp.CantidadReal as [C.Real] from DetRProd dp inner join RProduccion rp on rp.IdRack = dp.IdRProd where OrdenProduccion = '" + OP + "' and Estado = 1 and Verificado = 1 and Contado = 0";
            SqlCommand cmd = new SqlCommand(select, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            SqlDataReader ded = cmd.ExecuteReader();

            while (ded.Read())
            {
                cRack cr = new cRack();
                cr.EPC = ded.GetString(0);
                cr.numero = ded.GetInt32(1);
                cr.modelo = ded.GetString(2);
                cr.ordenProduccion = ded.GetString(3);
                cr.cantidadEstimada = ded.GetInt32(4);
                cr.cantidadReal = ded.GetInt32(5);
                racks.Add(cr);
            }
            
            int size = 0;
            size = racks.Count();
            return size;
        }

        [WebMethod]
        public DataSet getRemisionUsuario(string folio)
        {
            string[] parametros = getParametros("Intelisis");
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            conn.Open();

            string select = "select v.MovId as [Folio Remision], v.FechaEmision as [Fecha de Emision], v.OrigenId as [Folio Pedido], vd.Articulo, a.Descripcion1 as [Descripcion], " +
                "vd.Cantidad, c.Nombre , v.Id, p.MovID as [Orden Produccion] " +
                "from Venta v  " +
                "inner join VentaD vd on vd.Id = v.Id  " +
                "inner join Prod p on p.Referencia = v.OrigenId  " +
                "inner join Cte c on c.Cliente = v.Cliente  " +
                "inner join Art A on a.Articulo = vd.Articulo " +
                "inner join Embarque e on e.Referencia = v.MovID " +
                //"inner join Embarque e on e.OrigenId = v.MovID " +
                "where v.Mov='ARM-BUSTAMANTE' " +
                "and v.MovID = '" + folio + "' ";

            SqlDataAdapter da = new SqlDataAdapter(select, conn);

            da.Fill(ds);
            conn.Close();
            return ds;
        }

        /*[WebMethod]
        public DataSet getRemisionSistema(string folio)
        {
            string[] parametros = getParametros("Intelisis");
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            conn.Open();

            string select = "select v.MovId as [Folio Remision], v.FechaEmision as [Fecha de Emision], v.OrigenId as [Folio Pedido], vd.Articulo, a.Descripcion1 as [Descripcion] ,vd.Cantidad, c.Nombre ,v.Estatus as [Estatus Venta],v.Id,p.MovID as [Orden Produccion], p.Estatus as [Estatus Produccion] " +
                " from Venta v" +
                " inner join VentaD vd on vd.Id = v.Id" +
                " inner join Prod p on p.Referencia = v.OrigenId" +
                " inner join Cte c on c.Cliente = v.Cliente" +
                " inner join Art A on a.Articulo = vd.Articulo" +
                " where v.Mov='ARM-BUSTAMANTE'" +
                " and v.MovID = '" + folio + "'";

            SqlDataAdapter da = new SqlDataAdapter(select, conn);

            da.Fill(ds);
            conn.Close();
            return ds;
        }
        */

        [WebMethod]
        public DataSet getProdD(string folio)
        {
            string[] parametros = getParametros("Intelisis");
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            conn.Open();

            string select = "select * from ProdPendienteD where MovID='" + folio + "'";

            SqlDataAdapter da = new SqlDataAdapter(select, conn);

            da.Fill(ds);
            conn.Close();
            return ds;
        }

        [WebMethod]
        public String verificaRacks(String EPC)
        {
            string result = "null";
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                string update = "update DetRProd set Verificado = 1 where EPC ='" + EPC + "'";
                conn.Open();

                SqlCommand cmd = new SqlCommand(update, conn);
                cmd.ExecuteNonQuery();

                conn.Close();
                result = "Rack Verificado";
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            return result;
        }

        [WebMethod]
        public int asignadaOP(string OP)
        {
            int result = 0;
            int count = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                conn.Open();
                string existe = "select count(id) from catProd where OrdenProduccion = '" + OP + "' and Asignado = 1";
                SqlCommand cmd = new SqlCommand(existe, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    count = int.Parse(reader.GetValue(0).ToString());
                }
                if (count != 0)
                {
                }
                else
                {
                    result = 1;
                }
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                result = 1;
            }
            return result;
        }

        [WebMethod]
        public int verificaOP(string OP)
        {
            int result = 0;
            int count = 0;
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                conn.Open();
                string existe = "Select * from Prod Where MovId like '" + OP + "'";
                SqlCommand cmdexiste = new SqlCommand(existe, conn);
                SqlDataReader readerExiste = cmdexiste.ExecuteReader();
                if (readerExiste.Read())
                {
                    string select = "Select Id,Mov,MovID,Estatus From Inv Where  id In (Select Did From MovFlujo Where Omodulo = 'PROD' And OMovID = '" + OP + "'  And Dmov = 'Orden Transferencia') and Estatus != 'CONCLUIDO'";
                    SqlCommand cmd = new SqlCommand(select, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        count = count + 1;
                    }
                    result = count;
                }
                else
                {
                    result = 404;
                }
                //verifica si hay ordenes de transferencia pendientes
                
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }

            return result;
            }
        
        [WebMethod]
        public DataSet getIncompletosTransfer(string OP)
        {
            string[] parametros = getParametros("Intelisis");
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            conn.Open();

            string select = "Select Id,Mov,MovID,Estatus From Inv Where  id In (Select Did From MovFlujo Where Omodulo = 'PROD' And OMovID = '" + OP + "'  And Dmov = 'Orden Transferencia') and Estatus != 'CONCLUIDO'";

            SqlDataAdapter da = new SqlDataAdapter(select, conn);

            da.Fill(ds);
            conn.Close();
            return ds;
        }

        [WebMethod]
        public int liberarRacks(string epc)
        {
            int res = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                conn.Open();
                string update = "update DetRProd set OrdenProduccion = NULL, Estado = 0, Verificado = 0 where EPC = '" + epc + "'";
                SqlCommand cmd = new SqlCommand(update, conn);
                cmd.ExecuteNonQuery();
                res = 1;
            }
            catch (Exception e)
            {
                string rror = e.Message;
                res = 0;
            }
            return res;
        }
        
        [WebMethod]
        public Decimal cantidad(int tipoRack,string codigo)
        {
            Decimal cantidad = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            //cantidad por ventana
            try
            {
                
                conn.Open();
                string select = "Select PxT from catArt where Clave = '" + codigo + "'";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    cantidad = Decimal.Parse(reader.GetString(0));
                }
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            if (cantidad == 0)
            {
                return cantidad;
            }
            else
            {
                //luego la cantidad por rack
                switch (tipoRack)
                {
                    case 1:
                        cantidad = cantidad * 14; break;
                    case 2:
                        cantidad = cantidad * 18; break;
                    case 3:
                        cantidad = cantidad * 21; break;
                    case 4:
                        cantidad = cantidad * 18; break;
                    default: break;
                }
                return cantidad;
            }
        }

        public Decimal getCantidad(int tipoRack, string codigo)
        {
            Decimal cantidad = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            //cantidad por ventana
            try
            {

                conn.Open();
                string select = "Select PxT from catArt where Clave = '" + codigo + "'";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    cantidad = Decimal.Parse(reader.GetString(0));
                }
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            if (cantidad == 0)
            {
                return cantidad;
            }
            else
            {
                //luego la cantidad por rack
                switch (tipoRack)
                {
                    case 1:
                        cantidad = cantidad * 14; break;
                    case 2:
                        cantidad = cantidad * 18; break;
                    case 3:
                        cantidad = cantidad * 21; break;
                    case 4:
                        cantidad = cantidad * 18; break;
                    default: break;
                }
                return cantidad;
            }
        }

        [WebMethod]
        public Decimal checkCantidad(int tipoRack, string codigo, Decimal actual)
        {
            Decimal diferencia = 0;
            Decimal cantidad = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            //cantidad por ventana
            try
            {

                conn.Open();
                string select = "Select PxT from catArt where Clave = '" + codigo + "'";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    cantidad = int.Parse(reader.GetString(0));
                }
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }


            if (actual > cantidad)
            {
                diferencia = -2;
                return diferencia;
            }
            else
            {
                //luego la cantidad por rack
                switch (tipoRack)
                {
                    case 1:
                        cantidad = cantidad * 14;
                        if (actual > cantidad)
                        {
                            diferencia = -1; break;
                        }
                        else
                        {
                            diferencia = cantidad - actual;
                        }
                        break;
                    case 2:
                        cantidad = cantidad * 18;
                        if (actual > cantidad)
                        {
                            diferencia = -1; break;
                        }
                        else
                        {
                            diferencia = cantidad - actual;
                        }
                        break;
                    case 3:
                        cantidad = cantidad * 21;
                        if (actual > cantidad)
                        {
                            diferencia = -1; break;
                        }
                        else
                            diferencia = cantidad - actual;
                        break;
                    case 4:
                        cantidad = cantidad * 18;
                        if (actual > cantidad)
                        {
                            diferencia = -1; break;
                        }
                        else
                            diferencia = cantidad - actual;
                        break;
                }
            }
            return diferencia;
        }

        [WebMethod]
        public Decimal calculaRacks(string codigo,int tipoRack, Decimal pedido)
        {
            Decimal cantidad = 0;
            Decimal res =0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            //primero se obtiene la cantidad de producto por ventana
            try
            {
                conn.Open();
                string select = "Select PxT from catArt where Clave = '" + codigo + "'";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    cantidad = Decimal.Parse(reader.GetString(0));
                }
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            if (cantidad == 0)
            {
                return res;
            }
            else
            {
                //luego la cantidad por rack
                switch (tipoRack)
                {
                    case 1:
                        cantidad = cantidad * 14; break;
                    case 2:
                        cantidad = cantidad * 18; break;
                    case 3:
                        cantidad = cantidad * 21; break;
                    case 4:
                        cantidad = cantidad * 18; break;
                    default: break;
                }
                //luego cuantos racks se van a requerir de ese tipo
                cantidad = pedido / cantidad;
                cantidad = Decimal.Ceiling(cantidad);
                res = Convert.ToInt32(cantidad);
            }
            return res;
        }

        //[WebMethod]
        //public DataSet fillProd()
        //{
        //    string[] parametros = getParametros("Intelisis");
        //    string[] parametros2 = getParametros("Solutia");
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
        //        SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + ";MultipleActiveResultSets=True");
        //        conn.Open();
        //        conn2.Open();
        //        string select = "select pd.Id, pd.MovId,c.Nombre,pd.ArtDescripcion, a.Familia, pd.Unidad, a.Fabricante, pd.Articulo, pd.Cantidad, pd.Estatus, pd.Renglon, p.Referencia as Pedido from ProdPendienteD pd inner join Art a on a.Articulo = pd.Articulo inner join Prod p on p.MovId = pd.MovId inner join Venta v on v.Movid = pd.Referencia inner join Cte c on c.Cliente = v.Cliente order by pd.Id desc";
        //        string selectSolutia = "select * from catProd";
        //        SqlCommand cmdIntelisis = new SqlCommand(select, conn);
        //        SqlCommand cmdSolutia = new SqlCommand(selectSolutia, conn2);
        //        SqlDataReader reader = cmdIntelisis.ExecuteReader();
        //        SqlDataReader readerSolutia = cmdSolutia.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            if (readerSolutia.Read())
        //            {
        //                if (reader.GetInt32(0).ToString() != readerSolutia.GetInt32(0).ToString() && reader.GetString(1) != readerSolutia.GetString(1)
        //                    && reader.GetString(2) != readerSolutia.GetString(2) && reader.GetString(3) != readerSolutia.GetString(3) &&
        //                    reader.GetString(4) != readerSolutia.GetString(4) && reader.GetString(5) != readerSolutia.GetString(5) &&
        //                    reader.GetString(6) != readerSolutia.GetString(6) && reader.GetString(7) != readerSolutia.GetString(7) &&
        //                    reader.GetDouble(8).ToString() != readerSolutia.GetDouble(8).ToString()
        //                    && reader.GetDouble(10).ToString() != readerSolutia.GetDouble(10).ToString() && reader.GetValue(11).ToString() != readerSolutia.GetValue(11).ToString())
        //                {
        //                    string id = reader.GetInt32(0).ToString();//id
        //                    string OrdenProduccion = reader.GetString(1);//movId
        //                    string Cliente = reader.GetString(2);//cliente
        //                    string Descripcion = reader.GetString(3);//descripcion
        //                    string Tipo = reader.GetString(4);//tipo
        //                    string Medida = reader.GetString(5);//medida
        //                    string Color = reader.GetString(6);//color
        //                    string Codigo = reader.GetString(7);//codigo
        //                    string Cantidad = reader.GetDouble(8).ToString();//cantidad
        //                    if (Cantidad.Contains(','))
        //                    {
        //                        string Estatus = reader.GetString(9);//Estatus
        //                        string Renglon = reader.GetDouble(10).ToString();//renglon
        //                        string Referencia = reader.GetValue(11).ToString();//pedido
        //                        Cantidad = numDecimal(Cantidad);
        //                        string insert = "insert into catProd values (" + id + ",'" + OrdenProduccion + "','" + Cliente + "','" + Descripcion + "','" + Tipo
        //                            + "','" + Medida + "','" + Color + "','" + Codigo + "'," + Cantidad + ",'" + Estatus + "'," + Renglon + ",'" + Referencia + "',0,0," + Cantidad + ")";
        //                        SqlCommand cmd2 = new SqlCommand(insert, conn2);
        //                        cmd2.ExecuteNonQuery();
        //                    }
        //                    else
        //                    {
        //                        string Estatus = reader.GetString(9);//Estatus
        //                        string Renglon = reader.GetDouble(10).ToString();//renglon
        //                        string Referencia = reader.GetValue(11).ToString();//pedido
        //                        string insert = "insert into catProd values (" + id + ",'" + OrdenProduccion + "','" + Cliente + "','" + Descripcion + "','" + Tipo
        //                            + "','" + Medida + "','" + Color + "','" + Codigo + "','" + Cantidad + "','" + Estatus + "'," + Renglon + ",'" + Referencia + "',0,0," + Cantidad + ")";
        //                        SqlCommand cmd2 = new SqlCommand(insert, conn2);
        //                        cmd2.ExecuteNonQuery();
        //                    }
        //                }
        //                else
        //                {
        //                }
        //            }
        //            else
        //            {
        //                string id = reader.GetInt32(0).ToString();//id
        //                string OrdenProduccion = reader.GetString(1);//movId
        //                string Cliente = reader.GetString(2);//cliente
        //                string Descripcion = reader.GetString(3);//descripcion
        //                string Tipo = reader.GetString(4);//tipo
        //                string Medida = reader.GetString(5);//medida
        //                string Color = reader.GetString(6);//color
        //                string Codigo = reader.GetString(7);//codigo
        //                string Cantidad = reader.GetDouble(8).ToString();//cantidad
        //                if (Cantidad.Contains(','))
        //                {
        //                    string Estatus = reader.GetString(9);//Estatus
        //                    string Renglon = reader.GetDouble(10).ToString();//renglon
        //                    string Referencia = reader.GetValue(11).ToString();//pedido
        //                    Cantidad = numDecimal(Cantidad);
        //                    string insert = "insert into catProd values (" + id + ",'" + OrdenProduccion + "','" + Cliente + "','" + Descripcion + "','" + Tipo
        //                        + "','" + Medida + "','" + Color + "','" + Codigo + "'," + Cantidad + ",'" + Estatus + "'," + Renglon + ",'" + Referencia + "',0,0," + Cantidad + ")";
        //                    SqlCommand cmd2 = new SqlCommand(insert, conn2);
        //                    cmd2.ExecuteNonQuery();
        //                }
        //                else
        //                {
        //                    string Estatus = reader.GetString(9);//Estatus
        //                    string Renglon = reader.GetDouble(10).ToString();//renglon
        //                    string Referencia = reader.GetValue(11).ToString();//pedido
        //                    string insert = "insert into catProd values (" + id + ",'" + OrdenProduccion + "','" + Cliente + "','" + Descripcion + "','" + Tipo
        //                        + "','" + Medida + "','" + Color + "','" + Codigo + "','" + Cantidad + "','" + Estatus + "'," + Renglon + ",'" + Referencia + "',0,0," + Cantidad + ")";
        //                    SqlCommand cmd2 = new SqlCommand(insert, conn2);
        //                    cmd2.ExecuteNonQuery();
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
        //    }
        //    return ds;

        //}

        /*
        [WebMethod]
        public int fillProd()
        {
            int res = 0;
            ArrayList nuevos = new ArrayList();
            string id, OrdenProduccion, Cliente, Descripcion, Tipo, Medida, Color, Codigo, Cantidad, Estatus, Renglon, Referencia;
            DataSet ds = new DataSet();
            try
            {
                string select = "select distinct pd.Id, pd.MovId,c.Nombre, pd.ArtDescripcion, a.Familia, pd.Unidad, a.Fabricante, pd.Articulo, pd.Cantidad, pd.Estatus, pd.Renglon, p.Referencia as Pedido from ProdPendienteD pd inner join Art a on a.Articulo = pd.Articulo inner join Prod p on p.MovId = pd.MovId inner join ProdD pdd on pdd.Id = p.ID inner join Venta v on v.Movid = pdd.DestinoID inner join Cte c on c.Cliente = v.Cliente order by pd.Id desc";
                DataSet dsInt = getDatasetConexion(select, "Intelisis");
                int countRows = getInt("select count(*) from CatProd", "Solutia");
                DataTable dtInt = dsInt.Tables[0];
                DataRow drInt = null;
                ArrayList agregados = new ArrayList();
                if (dtInt.Rows.Count > 0) //Si contiene registros Inte 
                {
                    for (int x = 0; x < dtInt.Rows.Count; x++)
                    {
                        drInt = dtInt.Rows[x];

                        if (countRows > 0) //Si contiene registros Sol
                        {
                            string cosa1 = " [" + drInt[0] + "] [" + drInt[1] + "] [" + drInt[2] + "] [" + drInt[3] + "] [" + drInt[4] + "] [" + drInt[5] + "] [" + drInt[6] + "] [" + drInt[7].ToString().TrimEnd() + "] [" + drInt[8] + "] [" + drInt[9] + "] [" + drInt[10] + "] [" + drInt[11] + "]";
                            string consulta = "select count(*) from CatProd where Id = " + drInt[0] + " and OrdenProduccion = '" + drInt[1] + "' and Cliente = '" + drInt[2] + "' and Descripcion = '" + drInt[3] + "' and Tipo = '" + drInt[4] + "' and Medida = '" + drInt[5] + "' and Color = '" + drInt[6] + "' and Codigo = '" + drInt[7].ToString().TrimEnd() + "' and Cantidad = " + drInt[8] + " and Estatus = '" + drInt[9] + "' and Renglon = " + drInt[10] + " and Pedido = '" + drInt[11] + "'";
                            int count = getInt(consulta, "Solutia");
                            if (count == 0)
                            {
                                agregados.Add(x);
                            }
                        }
                        else
                        {
                            id = drInt[0].ToString();               //id
                            OrdenProduccion = drInt[1].ToString();  //movId
                            Cliente = drInt[2].ToString();          //cliente
                            Descripcion = drInt[3].ToString();      //descripcion
                            Tipo = drInt[4].ToString();             //tipo
                            Medida = drInt[5].ToString();           //medida
                            Color = drInt[6].ToString();            //color
                            Codigo = drInt[7].ToString().TrimEnd();           //codigo
                            Cantidad = drInt[8].ToString();         //cantidad
                            if (Cantidad.Contains(','))
                            {
                                Estatus = drInt[9].ToString();      //Estatus
                                Renglon = drInt[10].ToString();     //renglon
                                Referencia = drInt[11].ToString();  //pedido
                                Cantidad = numDecimal(Cantidad);
                                string insert = "insert into catProd values (" + id + ",'" + OrdenProduccion + "','" + Cliente + "','" + Descripcion + "','" + Tipo
                                           + "','" + Medida + "','" + Color + "','" + Codigo + "','" + Cantidad + "','" + Estatus + "'," + Renglon + ",'" + Referencia + "',0," + Cantidad + ",0,0,0,0,0,0)";
                                int inserted = inserta(insert, "Solutia");
                                if (inserted == 1)
                                    res++;
                            }
                            else
                            {
                                Estatus = drInt[9].ToString();      //Estatus
                                Renglon = drInt[10].ToString();     //renglon
                                Referencia = drInt[11].ToString();  //pedido
                                string insert = "insert into catProd values (" + id + ",'" + OrdenProduccion + "','" + Cliente + "','" + Descripcion + "','" + Tipo
                                            + "','" + Medida + "','" + Color + "','" + Codigo + "','" + Cantidad + "','" + Estatus + "'," + Renglon + ",'" + Referencia + "',0," + Cantidad + ",0,0,0,0,0,0)";
                                int inserted = inserta(insert, "Solutia");
                                if (inserted == 1)
                                    res++;
                            }
                        }
                    }
                    //
                    if (agregados.Count > 0)
                    {
                        bool agregado = false;
                        for (int w = 0; w < agregados.Count; w++)
                        {
                            int index = Int32.Parse(agregados[w].ToString());
                            drInt = dtInt.Rows[index];
                            id = drInt[0].ToString();               //id
                            OrdenProduccion = drInt[1].ToString();  //movId
                            Cliente = drInt[2].ToString();          //cliente
                            Descripcion = drInt[3].ToString();      //descripcion
                            Tipo = drInt[4].ToString();             //tipo
                            Medida = drInt[5].ToString();           //medida
                            Color = drInt[6].ToString();            //color
                            Codigo = drInt[7].ToString().TrimEnd(); //codigo
                            Cantidad = drInt[8].ToString();         //cantidad
                            if (Cantidad.Contains(','))
                            {
                                Estatus = drInt[9].ToString();      //Estatus
                                Renglon = drInt[10].ToString();     //renglon
                                Referencia = drInt[11].ToString();  //pedido
                                Cantidad = numDecimal(Cantidad);
                                string insert = "insert into catProd values (" + id + ",'" + OrdenProduccion + "','" + Cliente + "','" + Descripcion + "','" + Tipo
                                   + "','" + Medida + "','" + Color + "','" + Codigo + "','" + Cantidad + "','" + Estatus + "'," + Renglon + ",'" + Referencia + "',0," + Cantidad + ",0,0,0,0,0,0)";
                                int inserted = inserta(insert, "Solutia");
                                if (inserted == 1)
                                {
                                    res++;
                                    agregado = true;
                                }
                            }
                            else
                            {
                                Estatus = drInt[9].ToString();      //Estatus
                                Renglon = drInt[10].ToString();     //renglon
                                Referencia = drInt[11].ToString();  //pedido
                                string insert = "insert into catProd values (" + id + ",'" + OrdenProduccion + "','" + Cliente + "','" + Descripcion + "','" + Tipo
                                    + "','" + Medida + "','" + Color + "','" + Codigo + "','" + Cantidad + "','" + Estatus + "'," + Renglon + ",'" + Referencia + "',0," + Cantidad + ",0,0,0,0,0,0)";
                                int inserted = inserta(insert, "Solutia");
                                if (inserted == 1)
                                {
                                    res++;
                                    agregado = true;
                                }
                            }
                        }

                    }
                    //
                }
            }
            catch (Exception e)
            {
                string error = e.Message;
            }
            return res;
        }
        */

        [WebMethod]
        public int fillProd()
        {
            int res = 0;
            string[] parametros2 = getParametros("Solutia");
            SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + ";MultipleActiveResultSets=True");
            try
            {
                conn2.Open();
                string instruction = "EXEC sp_FillProd";
                SqlCommand cmdSolutia = new SqlCommand(instruction, conn2);
                res = cmdSolutia.ExecuteNonQuery();
                conn2.Close();
                return res;
            }catch (Exception e)
            {
                return res = -1;
            }
        }

        [WebMethod]
        public int getTipoRackOP(string EPC)
        {
            int tr = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                string select = "select Top(1)IdRProd from DetRProd where OrdenProduccion ='" + EPC + "'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    tr = reader.GetInt32(0);
                }
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return tr;
        }



        public string numDecimal(string coma)
        {
            string numDec = "0";
            string[] firstStep = coma.Split(',');
            string lastStep = firstStep[0] + "." + firstStep[1];
            numDec = lastStep;
            return numDec;
        }

        [WebMethod]
        public int PiezasVentana(string epc)
        {
            try
            {
                string[] parametros = getParametros("Solutia");
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
                conn.Open();
                string getCodigo = "select CodigoProducto from DetRProd where EPC = '" + epc + "'";
                SqlCommand cmdCodigo = new SqlCommand(getCodigo, conn);
                SqlDataReader readCodigo = cmdCodigo.ExecuteReader();
                string codigo = "";
                if (readCodigo.Read())
                {
                    codigo = readCodigo.GetValue(0).ToString();
                    string getPxT = "select PxT from catArt where Clave = '" + codigo + "'";
                    SqlCommand cmdPxT = new SqlCommand(getPxT, conn);
                    SqlDataReader readPxT = cmdPxT.ExecuteReader();
                    int PxT = 0;
                    if (readPxT.Read())
                    {
                        PxT = int.Parse(readPxT.GetValue(0).ToString());
                        return PxT;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        [WebMethod]
        public DataSet showProd()
        {
            DataSet ds = new DataSet();
            try
            {
                string[] parametros = getParametros("Solutia");
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
                string select = "select Id,OrdenProduccion,Pedido,Cliente,Descripcion,Tipo,Medida,Color,Codigo,Cantidad,Estatus,Renglon,Asignado,prodEstibado,prodAsignado,prodCurado,prodLiberado,mermas from catProd where Estatus != 'CONCLUIDO' and Asignado = 1";
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(select, conn);
                da.Fill(ds);

                conn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return ds;
        }

        [WebMethod]
        public string xmlShowProd()
        {
            string xml = "";
            try
            {
                string[] parametros = getParametros("Solutia");
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
                string select = "select Id,OrdenProduccion,Pedido,Cliente,Descripcion,Tipo,Medida,Color,Codigo,Cantidad,Estatus,Renglon,Asignado,prodEstibado,prodAsignado,prodCurado,prodLiberado,mermas from catProd where Estatus != 'CONCLUIDO' and Asignado = 1 for XML PATH('Registro'), ROOT('Produccion')";
                conn.Open();
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    xml = reader.GetValue(0).ToString();
                }
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return null;
            }
            return xml;
        }

        [WebMethod]
        public DataSet hiddenProd()
        {
            DataSet ds = new DataSet();
            try
            {
                string[] parametros = getParametros("Solutia");
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
                string select = "select OrdenProduccion,Pedido,Cliente,Descripcion,Tipo,Medida,Color,Codigo,Cantidad,Estatus,prodEstibado,prodAsignado,prodCurado,prodLiberado,mermas from catProd where Estatus != 'CONCLUIDO' and Asignado = 1";
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(select, conn);
                da.Fill(ds);
                conn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return ds;
        }

        [WebMethod]
        public int checkComplete(string folio)
        {
            int res = 0;
            string status="";
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                string select = "Select Estatus from ProdPendienteD where MovId ='" + folio + "'";
                SqlCommand cmd = new SqlCommand(select, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    status = reader.GetString(1);
                }
                else
                {
                    res = 1;
                    return res;
                }
                conn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                res = 1;
            }
            if (status != "CONCLUIDO")
            {
                res = 0;
            }
            else
            {
                res = 1;
            }
            return res;
        }

        [WebMethod]
        public int checkTransfer(string folio)
        {
            int res = 0;
            int cantidad =0;
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                string select = "Select count(Id) From Inv Where  id In (Select Did From MovFlujo Where Omodulo = 'PROD' And OMovID = '"+folio+"'  And Dmov = 'Orden Transferencia') and Estatus != 'CONCLUIDO'";
                SqlCommand cmd = new SqlCommand(select, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    cantidad = reader.GetInt32(0);
                }
                else
                {
                    res = 1;
                    return res;
                }
                conn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            if (cantidad == 0)
            {
                res = 0;
            }
            else
            {
                res = 1;
            }
            return res;
        }

        [WebMethod]
        public int checkRacks(string folio)
        {
            int res = 0;
            int cantidadAsignados = 0;
            int cantidadLeidos = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            SqlConnection conn2 = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                string select = "Select count(IdDRProd) from DetRProd where OrdenProduccion = '"+folio+"'";
                string select2 = "Select count(IdDRProd) from DetRProd where Verificado = 1 and OrdenProduccion='"+folio+"'";
                conn.Open();
                conn2.Open();
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlCommand cmd2 = new SqlCommand(select2, conn2);

                SqlDataReader reader = cmd.ExecuteReader();
                SqlDataReader reader2 = cmd2.ExecuteReader();
                
                if (reader.Read())
                {
                    cantidadAsignados = reader.GetInt32(0);
                }
                if (reader2.Read())
                {
                    cantidadLeidos = reader2.GetInt32(0);
                }
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            if (cantidadAsignados == cantidadLeidos)
            {
                res = 0;
            }
            else
            {
                res = 1;
            }
            return res;
        }

        /*[WebMethod]
        public string verificaEscuadra(string EPC, string folio,string codigo, Decimal cantidadEstiba)
        {
            string result = "null";
            Decimal CXT = cantidadPorTarima(codigo);
            Decimal CP = cantidadProd(folio,codigo);
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                if (cantidadEstiba != 0 && cantidadEstiba <= CXT)
                {
                    string update = "update DetEscuadras set Asignado = 1, OrdenProduccion = '" + folio + "', CodigoProducto = '" + codigo + "', Piezas = " + cantidadEstiba + " where EPC ='" + EPC + "' and Asignado != 1";
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(update, conn);
                    cmd.ExecuteNonQuery();
                    if (CP > cantidadEstiba)
                    {
                        Decimal estiba = cantidadEstibado(folio,codigo) + cantidadEstiba;
                        if (estiba < CP)
                        {
                            Decimal restante = CP - estiba;
                            string updateCatProd = "update catProd set prodEstibado = " + estiba + ", prodRestante = " + restante + " where OrdenProduccion = '" + folio + "' and Codigo = '" + codigo + "'";
                            SqlCommand cmdUCP = new SqlCommand(updateCatProd, conn);
                            cmdUCP.ExecuteNonQuery();
                            result = "Escuadra Asignada";
                        }
                        else
                        {
                            return result = "Se ha hecho produccion de más";
                        }
                    }
                    else
                    {
                        return result = "Se ha hecho produccion de más o se ha tratado de Estibar más de lo permitido";
                    }
                    conn.Close();
                    
                }
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            return result;
        }*/

        
        [WebMethod]
        public string verificarEscuadra(string EPC, string folio, string codigo, int cantidadEstiba)
        {
            string result = "null";
            string renglon = "";
            int CXT = cantidadPorTarima(codigo);
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                conn.Open();
                string selectRenglon = "select renglon from catprod where OrdenProduccion = '" + folio + "' and Codigo = '" + codigo + "'";
                SqlCommand cmdRenglon = new SqlCommand(selectRenglon,conn);
                SqlDataReader reader = cmdRenglon.ExecuteReader();
                if (reader.Read())
                {
                    renglon = reader.GetValue(0).ToString();
                }
                else
                {
                    return null;
                }
                if (cantidadEstiba > 0 && cantidadEstiba <= maximoPorTarima(codigo))
                {
                    int liberado = currentValue(folio, "CURADO", "wea", renglon);
                    int nvoLiberado = liberado - cantidadEstiba;
                    int concluido = currentValue(folio, "LIBERADO", "wea", renglon);
                    int nvoConcluido = concluido + cantidadEstiba;
                    string update = "update catProd set prodLiberado = " + nvoLiberado + ", prodConcluido = " + nvoConcluido + " where OrdenProduccion = '" + folio + "' and Codigo = '" + codigo + "'";
                    SqlCommand cmd = new SqlCommand(update, conn);
                    cmd.ExecuteNonQuery();
                    string escuadra = "update DetEscuadras set OrdenProduccion = '" + folio + "', CodigoProducto = '" + codigo + "', Piezas = " + cantidadEstiba + ", Asignado = 1 where EPC = '" + EPC + "'";
                    SqlCommand cmdE = new SqlCommand(escuadra, conn);
                    cmdE.ExecuteNonQuery();

                    result = "Escuadra Asignada";
                }
                else
                {
                    result = "Se ha tratado de Estibar en Tarima más cantidad de la permitida";
                }
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            return result;
            }
        public int cantidadPorTarima(string codigo)
        {
            int cantidad = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                string selectCXT = "select PZxT from catArt where Clave ='" + codigo + "'";
                conn.Open();
                SqlCommand cmdCXT = new SqlCommand(selectCXT, conn);
                SqlDataReader readerCXT = cmdCXT.ExecuteReader();
                if (readerCXT.Read())
                {
                    cantidad = int.Parse(readerCXT.GetString(0));
                }
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return cantidad;
        }

        [WebMethod]
        public int cantidadPorTarimaW(string codigo)
        {
            int cantidad = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                string selectCXT = "select PZxT from catArt where Clave ='" + codigo + "'";
                conn.Open();
                SqlCommand cmdCXT = new SqlCommand(selectCXT, conn);
                SqlDataReader readerCXT = cmdCXT.ExecuteReader();
                if (readerCXT.Read())
                {
                    cantidad = int.Parse(readerCXT.GetString(0));
                }
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return cantidad;
        }

        public int cantidadProd(string op,string codigo)
        {
            int cantidad = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                string selectQty = "select prodAsignado from catProd where Codigo ='" + codigo + "' and OrdenProduccion ='"+op+"'";
                conn.Open();
                SqlCommand cmdQty = new SqlCommand(selectQty, conn);
                SqlDataReader readerQty = cmdQty.ExecuteReader();
                if (readerQty.Read())
                {
                    cantidad = readerQty.GetInt32(0);
                    
                }
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return cantidad;
        }

        public Decimal cantidadEstibado(string op,string codigo)
        {
            Decimal cantidad = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                string selectQty = "select prodEstibado from catProd where Codigo ='" + codigo + "' and OrdenProduccion ='"+op+"'";
                conn.Open();
                SqlCommand cmdQty = new SqlCommand(selectQty, conn);
                SqlDataReader readerQty = cmdQty.ExecuteReader();
                if (readerQty.Read())
                {
                    cantidad = readerQty.GetInt32(0);
                }
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return cantidad;
        }

        [WebMethod]
        public DataSet getEscuadra()
        {
            DataSet ds = new DataSet();
            try
            {
                string[] parametros = getParametros("Solutia");
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
                conn.Open();
                string select = "Select * from DetEscuadras where Asignado = 0";

                SqlDataAdapter da = new SqlDataAdapter(select, conn);

                da.Fill(ds);
                conn.Close();
            }
            catch (SqlException e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return ds;
        }

        [WebMethod]
        public DataSet getEscuadraPicking(string op, string codigo)
        {
            DataSet ds = new DataSet();
            try
            {
                string[] parametros = getParametros("Solutia");
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
                conn.Open();
                string select = "Select * from DetEscuadras where Asignado = 1 and Ubicada = 1 and OrdenProduccion = '"+op+"' and CodigoProducto = '"+codigo+"' and Picked = 0";

                SqlDataAdapter da = new SqlDataAdapter(select, conn);

                da.Fill(ds);
                conn.Close();
            }
            catch (SqlException e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return ds;
        }

        [WebMethod]
        public int pickEscuadra(string epc)
        {
            int result = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                string update = "update DetEscuadras set Picked = 1 where EPC ='" + epc + "'";
                conn.Open();

                SqlCommand cmd = new SqlCommand(update, conn);
                cmd.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                result = 1;
            }
            return result;
        }

        public int getIdProd()
        {
            int Id = 0;
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                string select = "select Top(1) id from Prod order by id desc";
                conn.Open();
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Id = reader.GetInt32(0);
                }
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return Id;
        }

        public int getIdInv()
        {
            int Id = 0;
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                string select = "select Top(1) id from Inv order by id desc";
                conn.Open();
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Id = reader.GetInt32(0);
                }
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return Id;
        }
        
        [WebMethod]
        public int getTipoRack(string EPC)
        {
            int tr=0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                string select = "select IdRProd from DetRProd where EPC ='" + EPC + "'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    tr = reader.GetInt32(0);
                }
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return tr;
        }

        public string[] EstimadoRack(string epc, string codigo){
string[] estimado = new string[2];
string[] parametros2 = getParametros("Solutia");
SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + ";MultipleActiveResultSets=True");
try
{
    conn2.Open();
    string selectEstimado = "Select CantidadEstimada, OrdenProduccion from DetRProd where EPC ='" + epc + "' and CodigoProducto = '" + codigo + "'";
    SqlCommand cmdEstimado = new SqlCommand(selectEstimado, conn2);
    SqlDataReader reader = cmdEstimado.ExecuteReader();
    if (reader.Read())
    {
        estimado[0] = reader.GetInt32(0).ToString();
        estimado[1] = reader.GetString(1);
    }
    else
    {
        return estimado = null;
    }
}
catch (Exception e)
{
    string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
    return estimado = null;
}
			return estimado;
}

        public int[] CurrentCurado(string codigo, string op)
        {
            int[] estimado = new int[3];
            string[] parametros2 = getParametros("Solutia");
            SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + ";MultipleActiveResultSets=True");
            try
            {
                conn2.Open();
                string selectQty = "Select prodCurado,Renglon from CatProd where codigo ='" + codigo + "' and OrdenProduccion ='" + op + "'";
                SqlCommand cmdQty = new SqlCommand(selectQty, conn2);
                SqlDataReader readerQty = cmdQty.ExecuteReader();
                if (readerQty.Read())
                {
                    //estimaQty 
                    estimado[0] = readerQty.GetInt32(0);
                    //renglon 
                    estimado[1] = readerQty.GetInt32(1);
                    //renglonId 
                    estimado[2] = estimado[1] / 2048;
                }
                else
                {
                    return estimado = null;
                }
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return estimado;


        }

        public int CurrentLiberado(string codigo, string op)
        {
            int estimado = 0;
            string[] parametros2 = getParametros("Solutia");
            SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + ";MultipleActiveResultSets=True");
            try
            {
                conn2.Open();
                string LibQty = "Select prodLiberado from CatProd where codigo ='" + codigo + "' and OrdenProduccion ='" + op + "'";
                SqlCommand cmdLib = new SqlCommand(LibQty, conn2);
                SqlDataReader readerLib = cmdLib.ExecuteReader();
                if (readerLib.Read())
                {
                    estimado = int.Parse(readerLib.GetValue(0).ToString());
                }
                else
                {
                    return estimado = -1;
                }
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return estimado - 1;
            }
            return estimado;

        }

        public int CurrentMerma(string codigo, string op)
        {
            int estimado = 0;
            string[] parametros2 = getParametros("Solutia");
            SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + ";MultipleActiveResultSets=True");
            try
            {
                conn2.Open();
                string LibQty = "Select mermas from CatProd where codigo ='" + codigo + "' and OrdenProduccion ='" + op + "'";
                SqlCommand cmdLib = new SqlCommand(LibQty, conn2);
                SqlDataReader readerLib = cmdLib.ExecuteReader();
                if (readerLib.Read())
                {
                    estimado = int.Parse(readerLib.GetValue(0).ToString());
                }
                else
                {
                    return estimado = -1;
                }
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return estimado - 1;
            }
            return estimado;


        }

        [WebMethod]
        public int getHuecos(int rackHuecos, int id, string epc, int tr, string codigo,string user,string sucursal)
        {
            int res = 0;

            string[] parametros = getParametros("Intelisis");
            string[] parametros2 = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + ";MultipleActiveResultSets=True");
            try
            {
                conn2.Open();
                string[] estimadoR = EstimadoRack(epc,codigo);
                int estimado = int.Parse(estimadoR[0]);
                string op = estimadoR[1];
                if (rackHuecos >= 0 && rackHuecos <= estimado)
                {
                    int real = estimado - rackHuecos;
                    conn2.Close();
                    conn2.Open();
                    int[] currentCurado = CurrentCurado(codigo, op);
                    int estimaQty = currentCurado[0];
                    int renglon = currentCurado[1];
                    int renglonId = currentCurado[2];
                    int qtyLib = CurrentLiberado(codigo, op);
                    int realQty = estimaQty;
                    int mermaActual = CurrentMerma(codigo, op);
                    conn2.Close();
                    //actualiza la tabla ProdD
                    conn2.Open();
                    int conteo = countRenglones(id.ToString(), codigo);
                    int contador = 0;
                    while (contador < conteo )
                    {
                        string[] centro = CentroTrabajo(id.ToString(), codigo);
                        if (centro[2] != null && centro[0].Contains("CURAD"))
                        {
                            string updateDetRProd = "UPDATE DetRProd set CantidadReal = " + real + " where EPC ='" + epc + "'";
                            SqlCommand cmdDetRProd = new SqlCommand(updateDetRProd, conn2);
                            cmdDetRProd.ExecuteNonQuery();
                            conn2.Close();
                            conn2.Open();
                            qtyLib = qtyLib + real;
                            estimaQty = estimaQty - estimado;
                            mermaActual = mermaActual + rackHuecos;
                            string updateCatProd = "UPDATE CatProd set prodCurado = " + estimaQty + ", prodLiberado = " + qtyLib + ", mermas = " + mermaActual + " where Codigo ='" + codigo + "' and OrdenProduccion ='" + op + "'";
                            SqlCommand cmdCatProd = new SqlCommand(updateCatProd, conn2);
                            cmdCatProd.ExecuteNonQuery();
                            conn2.Close();
                            //CantidadA
                            string updateCantidadA = "UPDATE ProdD set CantidadA=" + rackHuecos + " WHERE id=" + id + " and renglonSub = " + centro[1];
                            conn.Open();
                            SqlCommand cmdCantidadA = new SqlCommand(updateCantidadA, conn);
                            cmdCantidadA.ExecuteNonQuery();
                            //genera la entrada de produccion
                            string generarMerma = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Seleccion', 'Entrada Produccion', '"+user+"', @Estacion=99";
                            SqlCommand cmdGenerarMerma = new SqlCommand(generarMerma, conn);
                            cmdGenerarMerma.ExecuteNonQuery();
                            //afecta la entrada
                            string afectaMerma = "EXEC spAfectar 'PROD', " + getIdProd() + ", 'AFECTAR', 'Todo', 'Entrada Produccion', '"+user+"', @Estacion=99";
                            SqlCommand cmdAfectaMerma = new SqlCommand(afectaMerma, conn);
                            cmdAfectaMerma.ExecuteNonQuery();
                            conn.Close();
                            //por ultimo inserta la merma produccion
                            string movId = setMovID("Merma Produccion",sucursal);
                            string insertInv = "Insert into INV (Empresa,Mov,MovID,FechaEmision,UltimoCambio,UEN,Moneda,TipoCambio,Usuario,Referencia,Observaciones,Estatus,Directo,Almacen,AlmacenTransito,OrigenTipo,Origen,OrigenID,Sucursal,SucursalOrigen,SucursalDestino)" +
        "values					('GNAP','Merma Produccion','" + movId + "',GETDATE(),GETDATE(),2,'Pesos',1,'"+user+"',(select referencia from prod where id=" + id + "),(select observaciones from prod where id=" + id + "),'PENDIENTE',1,'APT-BUS','(TRANSITO)','PROD','Entrada Produccion','" + op + "',"+sucursal+","+sucursal+","+sucursal+")";
                            conn.Open();
                            SqlCommand cmdInv = new SqlCommand(insertInv, conn);
                            cmdInv.ExecuteNonQuery();
                            int idInv = getIdInv();
                            string insertInvD = "Insert into INVD (ID,Renglon,RenglonSub,RenglonId,Renglontipo,Cantidad,Almacen,Codigo,Articulo,Unidad,Factor,Producto,Tipo,Sucursal)" +
                                "values (" + getIdInv() + "," + renglon + ",0," + renglonId + ",'N'," + rackHuecos + ",'APT-BUS',null,'" + codigo + "',(select unidad from Art where Articulo='" + codigo + "'),(select factor from Art where Articulo='" + codigo + "'),'" + codigo + "','Salida',"+sucursal+")";
                            SqlCommand cmdInvD = new SqlCommand(insertInvD, conn);
                            cmdInvD.ExecuteNonQuery();
                            string spAfectarMP = "EXEC spAfectar 'INV', " + idInv + ",'Afectar','Todo','Merma Produccion','"+user+"',@Estacion = 99";
                            SqlCommand cmdAfectarMP = new SqlCommand(spAfectarMP, conn);
                            cmdAfectarMP.ExecuteNonQuery();
                            string act = "Update Inv set Estatus = 'CONCLUIDO' where id = " + idInv + "";
                            SqlCommand cmdAct = new SqlCommand(act, conn);
                            cmdAct.ExecuteNonQuery();
                            string insertMovFlujo = "Insert into movFlujo values ("+sucursal+",'GNAP','PROD'," + id + ",'Entrada Produccion','" + op + "','INV'," + idInv + ",'Merma Produccion','" + movId + "',0)";
                            SqlCommand cmdMovFlujo = new SqlCommand(insertMovFlujo, conn);
                            cmdMovFlujo.ExecuteNonQuery();
                            conn.Close();
                            contador = conteo;
                        }
                        else
                        {
                            contador++;
                        }
                    }
                    
                }
                else if (rackHuecos > estimado)
                {
                    res = 2;
                }
                else if (rackHuecos == 0)
                {
                    string updateDetRProd = "UPDATE DetRProd set CantidadReal = " + estimado + " where EPC ='" + epc + "'";
                    SqlCommand cmdDetRProd = new SqlCommand(updateDetRProd, conn2);
                    cmdDetRProd.ExecuteNonQuery();
                    res = 3;
                }
                else
                {
                }
            }
            catch (Exception e)
            {
                res = 1;
                string x = e.Message;
            }
            return res;
        }

        [WebMethod]
        public int contarHuecos(int huecos, int id, string epc, int tr, string codigo, string user, string sucursal)
        {
            int res = 0;
            string[] parametros = getParametros("Intelisis");
            string[] parametros2 = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + ";MultipleActiveResultSets=True");
            try
            {
                conn.Open();
                conn2.Open();
                //cuanta cantidad se estimo para este rack?
                string selectEstimado = "Select CantidadEstimada, OrdenProduccion from DetRProd where EPC ='" + epc + "' and CodigoProducto = '" + codigo + "'";
                SqlCommand cmdEstimado = new SqlCommand(selectEstimado, conn2);
                SqlDataReader readerEstimado = cmdEstimado.ExecuteReader();
                int cantidadEstimada = 0;
                string ordenProduccion = "";
                if (readerEstimado.Read())
                {
                    cantidadEstimada = readerEstimado.GetInt32(0);
                    ordenProduccion = readerEstimado.GetString(1);
                }
                else
                {
                    return res = 1;
                }

                if (huecos > 0 && huecos < cantidadEstimada)
                {
                    int cantidadReal = cantidadEstimada - huecos;
                    //datos de la produccion, cantidad en curado, renglon y renglonId
                    string selectCurado = "Select prodCurado,Renglon from CatProd where codigo ='" + codigo + "' and OrdenProduccion ='" + ordenProduccion + "'";
                    SqlCommand cmdCurado = new SqlCommand(selectCurado, conn2);
                    SqlDataReader readerCurado = cmdCurado.ExecuteReader();
                    int cantidadCurado = 0, renglon, renglonId;
                    if (readerCurado.Read())
                    {
                        //estimaQty 
                        cantidadCurado = readerCurado.GetInt32(0);
                        //renglon 
                        renglon = readerCurado.GetInt32(1);
                        //renglonId 
                        renglonId = renglon / 2048;
                    }
                    else
                    {
                        return res = 1;
                    }
                    //ahora vamos por la cantidad liberada hasta el momento
                    string selectLiberado = "Select prodLiberado from CatProd where codigo ='" + codigo + "' and OrdenProduccion ='" + ordenProduccion + "'";
                    SqlCommand cmdLiberado = new SqlCommand(selectLiberado, conn2);
                    SqlDataReader readerLiberado = cmdLiberado.ExecuteReader();
                    int cantidadLiberado = 0;
                    if (readerLiberado.Read())
                    {
                        cantidadLiberado = readerLiberado.GetInt32(0);
                    }
                    else
                    {
                        return res = 1;
                    }
                    //por ultimo, lo que se ha mermado hasta ahorita
                    string selectMermas = "Select mermas from CatProd where codigo ='" + codigo + "' and OrdenProduccion ='" + ordenProduccion + "'";
                    SqlCommand cmdMermas = new SqlCommand(selectMermas, conn2);
                    SqlDataReader readerMermas = cmdMermas.ExecuteReader();
                    int cantidadMerma = 0;
                    if (readerMermas.Read())
                    {
                        cantidadMerma = readerMermas.GetInt32(0);
                    }
                    else
                    {
                        return res = -1;
                    }
                    //conteo de renglones
                    string selectRenglonSub = "select count(RenglonSub) from ProdD where Articulo ='" + codigo + "' and id = " + id + "";
                    SqlCommand cmdRenglonSub = new SqlCommand(selectRenglonSub, conn);
                    SqlDataReader readerRenglonSub = cmdRenglonSub.ExecuteReader();
                    int conteoRenglones = 0, contador = 0;
                    if (readerRenglonSub.Read())
                    {
                        conteoRenglones = readerRenglonSub.GetInt32(0);
                    }
                    else
                    {
                        return res = 1;
                    }
                    while (contador < conteoRenglones)
                    {
                        int nuevoCurado = cantidadCurado - cantidadEstimada, nuevoLiberado = cantidadLiberado + cantidadReal, nuevoMermas = cantidadMerma + huecos;
                        string selectCentroDestino = "SELECT Centro,CentroDestino from ProdD where id = " + id + " and Renglon = " + renglon + " and RenglonSub = " + contador;
                        SqlCommand cmdDestino = new SqlCommand(selectCentroDestino, conn);
                        SqlDataReader readerDestino = cmdDestino.ExecuteReader();
                        string centro = "", centroDestino = "";
                        if (readerDestino.Read())
                        {
                            centro = readerDestino.GetValue(0).ToString();
                            centroDestino = readerDestino.GetValue(1).ToString();
                            //ahora si viene lo chingon, que budha te ampare por que ahora vamos a ver si es directo o no directo.
                            if (!centro.Contains("CURAD") && centroDestino.Contains("CURAD"))//esta es la primera, si el centro no esta en curado, y el destino si tiene curado entonces pelas gallo papá
                            {
                                return res = 1;
                            }
                            else if (!centro.Contains("CURAD") && !centroDestino.Contains("CURAD"))//segunda, si el centro es un patio de produccion y el destino no es un cuarto de curado
                            {
                                //primero dime cuanto quedo en los racks
                                string updateDetRprod = "UPDATE DetRPRod set CantidadReal = " + cantidadReal + " where EPC = '" + epc + "'";
                                SqlCommand cmdDetRProd = new SqlCommand(updateDetRprod, conn2);
                                cmdDetRProd.ExecuteNonQuery();
                                //luego actualizar CatProd
                                string updateCatProd = "UPDATE catProd set prodCurado = " + nuevoCurado + ", prodLiberado = " + nuevoLiberado + ", mermas = " + nuevoMermas + " where Codigo = '" + codigo + "' and OrdenProduccion = '" + ordenProduccion + "'";
                                SqlCommand cmdCatProd = new SqlCommand(updateCatProd, conn2);
                                cmdCatProd.ExecuteNonQuery();
                                //ahora sigue que actualicemos ProdD
                                string updateProdD = "UPDATE ProdD set CantidadA = " + huecos + " where id = " + id + "and RenglonSub = " + contador + "";
                                SqlCommand cmdProdD = new SqlCommand(updateProdD, conn);
                                cmdProdD.ExecuteNonQuery();
                                //Generamos la entrada de produccion
                                string generarMerma = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Seleccion', 'Entrada Produccion', '" + user + "', @Estacion = 99";
                                SqlCommand cmdGenerarMerma = new SqlCommand(generarMerma, conn);
                                cmdGenerarMerma.ExecuteNonQuery();
                                //afectamos la entrada de produccion generada
                                string afectarMerma = "EXEC spAfectar 'PROD', " + getIdProd() + ", 'AFECTAR', 'Todo', 'Entrada Produccion', '" + user + "', @Estacion = 99";
                                SqlCommand cmdAfectarMerma = new SqlCommand(afectarMerma, conn);
                                cmdAfectarMerma.ExecuteNonQuery();
                                //por ultimo se insertan las mermas en Inv e InvD
                                string movId = setMovID("Merma Produccion", sucursal);
                                string insertInv = "Insert into INV (Empresa,Mov,MovID,FechaEmision,UltimoCambio,UEN,Moneda,TipoCambio,Usuario,Referencia,Observaciones,Estatus,Directo,Almacen,AlmacenTransito,OrigenTipo,Origen,OrigenID,Sucursal,SucursalOrigen,SucursalDestino)" + "values ('GNAP','Merma Produccion','" + movId + "',GETDATE(),GETDATE(),2,'Pesos',1,'" + user + "',(select referencia from prod where id=" + id + "),(select observaciones from prod where id=" + id + "),'PENDIENTE',1,'APT-BUS','(TRANSITO)','PROD','Entrada Produccion','" + ordenProduccion + "'," + sucursal + "," + sucursal + "," + sucursal + ")";
                                SqlCommand cmdInv = new SqlCommand(insertInv, conn);
                                cmdInv.ExecuteNonQuery();
                                int idInv = getIdInv();
                                string insertInvD = "Insert into INVD (ID,Renglon,RenglonSub,RenglonId,Renglontipo,Cantidad,Almacen,Codigo,Articulo,Unidad,Factor,Producto,Tipo,Sucursal)" + "values (" + getIdInv() + "," + renglon + ",0," + renglonId + ",'N'," + huecos + ",'APT-BUS',null,'" + codigo + "',(select unidad from Art where Articulo='" + codigo + "'),(select factor from Art where Articulo='" + codigo + "'),'" + codigo + "','Salida'," + sucursal + ")";
                                SqlCommand cmdInvD = new SqlCommand(insertInvD, conn);
                                cmdInvD.ExecuteNonQuery();
                                string spAfectarMP = "EXEC spAfectar 'INV', " + idInv +
                                ",'Afectar','Todo','Merma Produccion','" + user + "',@Estacion = 99";
                                SqlCommand cmdAfectarMP = new SqlCommand(spAfectarMP, conn);
                                cmdAfectarMP.ExecuteNonQuery();
                                string act = "Update Inv set Estatus = 'CONCLUIDO' where id = " + idInv + "";
                                SqlCommand cmdAct = new SqlCommand(act, conn);
                                cmdAct.ExecuteNonQuery();
                                string insertMovFlujo = "Insert into movFlujo values (" + sucursal + ",'GNAP','PROD'," + id + ",'Entrada Produccion','" + ordenProduccion + "','INV'," + idInv + ",'Merma Produccion','" + movId + "',0)";
                                SqlCommand cmdMovFlujo = new SqlCommand(insertMovFlujo, conn);
                                cmdMovFlujo.ExecuteNonQuery();
                                contador = conteoRenglones;
                                return res;
                            }
                            else if (centro.Contains("CURAD") && !centroDestino.Contains("CURADO")) //son los productos que ya se avanzaron
                            {
                                //primero dime cuanto quedo en los racks
                                string updateDetRprod = "UPDATE DetRPRod set CantidadReal = " + cantidadReal + " where EPC = '" + epc + "'";
                                SqlCommand cmdDetRProd = new SqlCommand(updateDetRprod, conn2);
                                cmdDetRProd.ExecuteNonQuery();
                                //luego actualizar CatProd
                                string updateCatProd = "UPDATE catProd set prodCurado = " + nuevoCurado + ", prodLiberado = " + nuevoLiberado + ", mermas = " + nuevoMermas + " where Codigo = '" + codigo + "' and OrdenProduccion = '" + ordenProduccion + "'";
                                SqlCommand cmdCatProd = new SqlCommand(updateCatProd, conn2);
                                cmdCatProd.ExecuteNonQuery();
                                //ahora sigue que actualicemos ProdD
                                string updateProdD = "UPDATE ProdD set CantidadA = " + huecos + " where id = " + id + "and RenglonSub = " + contador + "";
                                SqlCommand cmdProdD = new SqlCommand(updateProdD, conn);
                                cmdProdD.ExecuteNonQuery();
                                //Generamos la entrada de produccion
                                string generarMerma = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Seleccion', 'Entrada Produccion', '" + user + "', @Estacion = 99";
                                SqlCommand cmdGenerarMerma = new SqlCommand(generarMerma, conn);
                                cmdGenerarMerma.ExecuteNonQuery();
                                //afectamos la entrada de produccion generada
                                string afectarMerma = "EXEC spAfectar 'PROD', " + getIdProd() + ", 'AFECTAR', 'Todo', 'Entrada Produccion', '" + user + "', @Estacion = 99";
                                SqlCommand cmdAfectarMerma = new SqlCommand(afectarMerma, conn);
                                cmdAfectarMerma.ExecuteNonQuery();
                                //por ultimo se insertan las mermas en Inv e InvD
                                string movId = setMovID("Merma Produccion", sucursal);
                                string insertInv = "Insert into INV (Empresa,Mov,MovID,FechaEmision,UltimoCambio,UEN,Moneda,TipoCambio,Usuario,Referencia,Observaciones,Estatus,Directo,Almacen,AlmacenTransito,OrigenTipo,Origen,OrigenID,Sucursal,SucursalOrigen,SucursalDestino)" + "values ('GNAP','Merma Produccion','" + movId + "',GETDATE(),GETDATE(),2,'Pesos',1,'" + user + "',(select referencia from prod where id=" + id + "),(select observaciones from prod where id=" + id + "),'PENDIENTE',1,'APT-BUS','(TRANSITO)','PROD','Entrada Produccion','" + ordenProduccion + "'," + sucursal + "," + sucursal + "," + sucursal + ")";
                                SqlCommand cmdInv = new SqlCommand(insertInv, conn);
                                cmdInv.ExecuteNonQuery();
                                int idInv = getIdInv();
                                string insertInvD = "Insert into INVD (ID,Renglon,RenglonSub,RenglonId,Renglontipo,Cantidad,Almacen,Codigo,Articulo,Unidad,Factor,Producto,Tipo,Sucursal)" + "values (" + getIdInv() + "," + renglon + ",0," + renglonId + ",'N'," + huecos + ",'APT-BUS',null,'" + codigo + "',(select unidad from Art where Articulo='" + codigo + "'),(select factor from Art where Articulo='" + codigo + "'),'" + codigo + "','Salida'," + sucursal + ")";
                                SqlCommand cmdInvD = new SqlCommand(insertInvD, conn);
                                cmdInvD.ExecuteNonQuery();
                                string spAfectarMP = "EXEC spAfectar 'INV', " + idInv +
                                ",'Afectar','Todo','Merma Produccion','" + user + "',@Estacion = 99";
                                SqlCommand cmdAfectarMP = new SqlCommand(spAfectarMP, conn);
                                cmdAfectarMP.ExecuteNonQuery();
                                string act = "Update Inv set Estatus = 'CONCLUIDO' where id = " + idInv + "";
                                SqlCommand cmdAct = new SqlCommand(act, conn);
                                cmdAct.ExecuteNonQuery();
                                string insertMovFlujo = "Insert into movFlujo values (" + sucursal + ",'GNAP','PROD'," + id + ",'Entrada Produccion','" + ordenProduccion + "','INV'," + idInv + ",'Merma Produccion','" + movId + "',0)";
                                SqlCommand cmdMovFlujo = new SqlCommand(insertMovFlujo, conn);
                                cmdMovFlujo.ExecuteNonQuery();
                                contador = conteoRenglones;
                                return res;
                            }
                        }
                        else
                        {
                            contador++;
                        }
                    }
                }
                else if (huecos > cantidadEstimada)
                {
                    return res = 2;
                }
                else if (huecos == 0)
                {
                    string updateDetRProd = "UPDATE DetRProd set CantidadReal = " + cantidadEstimada + " where EPC ='" + epc + "'";
                    SqlCommand cmdDetRProd = new SqlCommand(updateDetRProd, conn2);
                    cmdDetRProd.ExecuteNonQuery();
                    return res = 3;
                }
                else
                {
                    return res = 1;
                }
            }
            catch (Exception e)
            {
                string error = e.Message;
                return res = 1;
            }
            return res;
        }

        [WebMethod]
        public int MermaAlmacen(int id, int original, int nvoQty,string op, string code, int renglon,string epc,string user,string sucursal)
        {
            int res = 0;
            int renglonId = renglon / 2048;
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            string[] parametros2 = getParametros("Solutia");
            SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + ";MultipleActiveResultSets=True");
            try
            {
                conn.Open();
                conn2.Open();
                string movId = setMovID("Merma Almacen", sucursal);
                string insertInv = "Insert into INV (Empresa,Mov,MovID,FechaEmision,UltimoCambio,UEN,Moneda,TipoCambio,Usuario,Referencia,Observaciones,Estatus,Directo,Almacen,AlmacenTransito,OrigenTipo,Origen,OrigenID,Sucursal,SucursalOrigen,SucursalDestino)" +
        "values					('GNAP','Merma Almacen','" + movId + "',GETDATE(),GETDATE(),2,'Pesos',1,'"+user+"',(select referencia from prod where id=" + id + "),(select observaciones from prod where id=" + id + "),'PENDIENTE',1,'APT-BUS','(TRANSITO)','PROD','Entrada Produccion','" + op + "',"+sucursal+","+sucursal+","+sucursal+")";                
                SqlCommand cmdInv = new SqlCommand(insertInv, conn);
                cmdInv.ExecuteNonQuery();
                int idInv = getIdInv();
                string insertInvD = "Insert into INVD (ID,Renglon,RenglonSub,RenglonId,Renglontipo,Cantidad,CantidadPendiente,Almacen,Codigo,Articulo,Unidad,Factor,Producto,Tipo,Sucursal)" +
                            "values (" + idInv + "," + renglon + ",0," + renglonId + ",'N',"+ nvoQty+"," + nvoQty + ",'APT-BUS',null,'" + code + "',(select unidad from Art where Articulo='" + code + "'),(select factor from Art where Articulo='" + code + "'),'" + code + "','Salida',"+sucursal+")";
                SqlCommand cmdInvD = new SqlCommand(insertInvD, conn);
                cmdInvD.ExecuteNonQuery();
                string spAfectarMP = "EXEC spAfectar 'INV', " + idInv + ",'Afectar','Todo','Merma Almacen','" + user + "',@Estacion = 99";
                SqlCommand cmdAfectarMP = new SqlCommand(spAfectarMP, conn);
                cmdAfectarMP.ExecuteNonQuery();
                string act = "Update Inv set Estatus = 'CONCLUIDO' where id = " + idInv + "";
                SqlCommand cmdAct = new SqlCommand(act, conn);
                cmdAct.ExecuteNonQuery();
                string insertMovFlujo = "Insert into movFlujo values ("+sucursal+",'GNAP','PROD'," + id + ",'Entrada Produccion','" + op + "','INV'," + idInv + ",'Merma Almacen','" + movId + "',0)";
                SqlCommand cmdMovFlujo = new SqlCommand(insertMovFlujo, conn);
                cmdMovFlujo.ExecuteNonQuery();
                conn.Close();
                string updateCantidad = "Update DetEscuadras set Piezas = " + original + " where EPC = '" + epc + "'";
                SqlCommand cmdCantidad = new SqlCommand(updateCantidad, conn2);
                cmdCantidad.ExecuteNonQuery();
                conn2.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                res = 1;
            }
            return res;
        }

        [WebMethod]
        public int MermaEmbarques(int id, int original, int nvoQty, string op, string code, int renglon, string epc, string user, string sucursal)
        {
            int res = 0;
            int renglonId = renglon / 2048;
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            string[] parametros2 = getParametros("Solutia");
            SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + ";MultipleActiveResultSets=True");
            try
            {
                conn.Open();
                conn2.Open();
                string movId = setMovID("Merma Embarques", sucursal);
                string insertInv = "Insert into INV (Empresa,Mov,MovID,FechaEmision,UltimoCambio,UEN,Moneda,TipoCambio,Usuario,Referencia,Observaciones,Estatus,Directo,Almacen,AlmacenTransito,OrigenTipo,Origen,OrigenID,Sucursal,SucursalOrigen,SucursalDestino)" +
        "values					('GNAP','Merma Embarques','" + movId + "',GETDATE(),GETDATE(),2,'Pesos',1,'" + user + "',(select referencia from prod where id=" + id + "),(select observaciones from prod where id=" + id + "),'PENDIENTE',1,'APT-BUS','(TRANSITO)','PROD','Entrada Produccion','" + op + "'," + sucursal + "," + sucursal + "," + sucursal + ")";
                SqlCommand cmdInv = new SqlCommand(insertInv, conn);
                cmdInv.ExecuteNonQuery();
                int idInv = getIdInv();
                string insertInvD = "Insert into INVD (ID,Renglon,RenglonSub,RenglonId,Renglontipo,Cantidad,CantidadPendiente,Almacen,Codigo,Articulo,Unidad,Factor,Producto,Tipo,Sucursal)" +
                            "values (" + idInv + "," + renglon + ",0," + renglonId + ",'N'," + nvoQty + "," + nvoQty + ",'APT-BUS',null,'" + code + "',(select unidad from Art where Articulo='" + code + "'),(select factor from Art where Articulo='" + code + "'),'" + code + "','Salida'," + sucursal + ")";
                SqlCommand cmdInvD = new SqlCommand(insertInvD, conn);
                cmdInvD.ExecuteNonQuery();
                string spAfectarMP = "EXEC spAfectar 'INV', " + idInv + ",'Afectar','Todo','Merma Embarques','" + user + "',@Estacion = 99";
                SqlCommand cmdAfectarMP = new SqlCommand(spAfectarMP, conn);
                cmdAfectarMP.ExecuteNonQuery();
                string act = "Update Inv set Estatus = 'CONCLUIDO' where id = " + idInv + "";
                SqlCommand cmdAct = new SqlCommand(act, conn);
                cmdAct.ExecuteNonQuery();
                string insertMovFlujo = "Insert into movFlujo values (" + sucursal + ",'GNAP','PROD'," + id + ",'Entrada Produccion','" + op + "','INV'," + idInv + ",'Merma Embarques','" + movId + "',0)";
                SqlCommand cmdMovFlujo = new SqlCommand(insertMovFlujo, conn);
                cmdMovFlujo.ExecuteNonQuery();
                conn.Close();
                string updateCantidad = "Update DetEscuadras set Piezas = " + original + " where EPC = '" + epc + "'";
                SqlCommand cmdCantidad = new SqlCommand(updateCantidad, conn2);
                cmdCantidad.ExecuteNonQuery();
                conn2.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                res = 1;
            }
            return res;
        }

        [WebMethod]
        public int MermaRecepcionEmbarques(int id,int nvoQty,string op, string code,int renglon,string user,string sucursal)
        {
            int res = 0;
            int renglonId = renglon / 2048;
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            string[] parametros2 = getParametros("Solutia");
            SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + ";MultipleActiveResultSets=True");
            try
            {
                if (id == 0)
                {
                    conn2.Open();
                    string movId = setMovID("Merma Almacen", sucursal);
                    string insertInv = "Insert into INV (Empresa,Mov,MovID,FechaEmision,UltimoCambio,UEN,Moneda,TipoCambio,Usuario,Referencia,Observaciones,Estatus,Directo,Almacen,AlmacenTransito,OrigenTipo,Origen,OrigenID,Sucursal,SucursalOrigen,SucursalDestino)" +
        "values					('GNAP','Merma Produccion','" + movId + "',GETDATE(),GETDATE(),2,'Pesos',1,'"+user+"',(select referencia from prod where id=" + id + "),(select observaciones from prod where id=" + id + "),'PENDIENTE',1,'APT-BUS','(TRANSITO)','PROD','Entrada Produccion','" + op + "',"+sucursal+","+sucursal+","+sucursal+")";
                    conn.Open();
                    SqlCommand cmdInv = new SqlCommand(insertInv, conn);
                    cmdInv.ExecuteNonQuery();
                    int idInv = getIdInv();
                    string insertInvD = "Insert into INVD (ID,Renglon,RenglonSub,RenglonId,Renglontipo,Cantidad,Almacen,Codigo,Articulo,Unidad,Factor,Producto,Tipo,Sucursal)" +
                                "values (" + idInv + "," + renglon + ",0," + renglonId + ",'N'," + nvoQty + ",'APP-BUS',null,'" + code + "',(select unidad from Art where Articulo='" + code + "'),(select factor from Art where Articulo='" + code + "'),'" + code + "','Salida',"+sucursal+")";
                    SqlCommand cmdInvD = new SqlCommand(insertInvD, conn);
                    cmdInvD.ExecuteNonQuery();
                    string act = "Update Inv set Estatus = 'CONCLUIDO' where id = " + idInv + "";
                    SqlCommand cmdAct = new SqlCommand(act, conn);
                    cmdAct.ExecuteNonQuery();
                    string insertMovFlujo = "Insert into movFlujo values ("+sucursal+",'GNAP','PROD'," + id + ",'Entrada Produccion','" + op + "','INV'," + idInv + ",'Merma Almacen','" + movId + "',0)";
                    SqlCommand cmdMovFlujo = new SqlCommand(insertMovFlujo, conn);
                    cmdMovFlujo.ExecuteNonQuery();
                    conn.Close();
                    string updateCantidad = "Update detEscuadras set Piezas = " + nvoQty + " where OrdenProduccion='" + op + "' and CodigoProducto ='" + code + "'";
                    SqlCommand cmdCantidad = new SqlCommand(updateCantidad, conn2);
                    cmdCantidad.ExecuteNonQuery();
                    conn2.Close();

                }
                else
                {
                    conn2.Open();
                    string movId = setMovID("Merma Almacen", sucursal);
                    string insertInv = "Insert into INV (Empresa,Mov,MovID,FechaEmision,UltimoCambio,UEN,Moneda,TipoCambio,Usuario,Referencia,Observaciones,Estatus,Directo,Almacen,AlmacenTransito,OrigenTipo,Origen,OrigenID,Sucursal,SucursalOrigen,SucursalDestino)" +
                        "values					('GNAP','Merma Produccion','" + movId + "',GETDATE(),GETDATE(),2,'Pesos',1,'"+user+"',(select referencia from prod where id=" + id + "),(select observaciones from prod where id=" + id + "),'PENDIENTE',1,'APT-BUS','(TRANSITO)','PROD','Entrada Produccion','" + op + "',"+sucursal+","+sucursal+","+sucursal+")";
                    conn.Open();
                    SqlCommand cmdInv = new SqlCommand(insertInv, conn);
                    cmdInv.ExecuteNonQuery();
                    int idInv = getIdInv();
                    string insertInvD = "Insert into INVD (ID,Renglon,RenglonSub,RenglonId,Renglontipo,Cantidad,Almacen,Codigo,Articulo,Unidad,Factor,Producto,Tipo,Sucursal)" +
                                "values (" + idInv + "," + renglon + ",0," + renglonId + ",'N'," + nvoQty + ",'APP-BUS',null,'" + code + "',(select unidad from Art where Articulo='" + code + "'),(select factor from Art where Articulo='" + code + "'),'" + code + "','Salida',"+sucursal+")";
                    SqlCommand cmdInvD = new SqlCommand(insertInvD, conn);
                    cmdInvD.ExecuteNonQuery();
                    string act = "Update Inv set Estatus = 'CONCLUIDO' where id = " + idInv + "";
                    SqlCommand cmdAct = new SqlCommand(act, conn);
                    cmdAct.ExecuteNonQuery();
                    string insertMovFlujo = "Insert into movFlujo values ("+sucursal+",'GNAP','PROD'," + id + ",'Entrada Produccion','" + op + "','INV'," + idInv + ",'Merma Almacen','" + movId + "',0)";
                    SqlCommand cmdMovFlujo = new SqlCommand(insertMovFlujo, conn);
                    cmdMovFlujo.ExecuteNonQuery();
                    conn.Close();
                    string updateCantidad = "Update detEscuadras set Piezas = " + nvoQty + " where OrdenProduccion='" + op + "' and CodigoProducto ='" + code + "'";
                    SqlCommand cmdCantidad = new SqlCommand(updateCantidad, conn2);
                    cmdCantidad.ExecuteNonQuery();
                    conn2.Close();
                }
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                res = 1;
            }
            return res;
        }

        [WebMethod]
        public int MermaEstiba(int rackHuecos, int id, string codigo, string op, int renglon, string user, string sucursal)
        {
            int res = 0;
            string[] parametros = getParametros("Intelisis");
            string[] parametros2 = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + ";MultipleActiveResultSets=True");
            try
            {
                conn.Open();
                conn2.Open();
                //cuanta cantidad se estimo para este rack?
                string selectEstimado = "Select PZxT from CatArt where Clave ='" + codigo + "'";
                SqlCommand cmdEstimado = new SqlCommand(selectEstimado, conn2);
                SqlDataReader readerEstimado = cmdEstimado.ExecuteReader();
                int cantidadEstimada = 0;
                
                if (readerEstimado.Read())
                {
                    cantidadEstimada = int.Parse(readerEstimado.GetValue(0).ToString());
                }
                else
                {
                    return res = 1;
                }

                if (rackHuecos > 0 && rackHuecos < cantidadEstimada)
                {
                    int cantidadReal = cantidadEstimada - rackHuecos;
                    //datos de la produccion, cantidad en curado, renglon y renglonId
                    string selectCurado = "Select prodCurado from CatProd where codigo ='" + codigo + "' and OrdenProduccion ='" + op + "'";
                    SqlCommand cmdCurado = new SqlCommand(selectCurado, conn2);
                    SqlDataReader readerCurado = cmdCurado.ExecuteReader();
                    int cantidadCurado = 0, renglonId;
                    if (readerCurado.Read())
                    {
                        //estimaQty 
                        cantidadCurado = readerCurado.GetInt32(0);
                        //renglonId 
                        renglonId = renglon / 2048;
                    }
                    else
                    {
                        return res = 1;
                    }
                    //ahora vamos por la cantidad liberada hasta el momento
                    string selectLiberado = "Select prodLiberado from CatProd where codigo ='" + codigo + "' and OrdenProduccion ='" + op + "'";
                    SqlCommand cmdLiberado = new SqlCommand(selectLiberado, conn2);
                    SqlDataReader readerLiberado = cmdLiberado.ExecuteReader();
                    int cantidadLiberado = 0;
                    if (readerLiberado.Read())
                    {
                        cantidadLiberado = readerLiberado.GetInt32(0);
                    }
                    else
                    {
                        return res = 1;
                    }
                    //por ultimo, lo que se ha mermado hasta ahorita
                    string selectMermas = "Select mermas from CatProd where codigo ='" + codigo + "' and OrdenProduccion ='" + op + "'";
                    SqlCommand cmdMermas = new SqlCommand(selectMermas, conn2);
                    SqlDataReader readerMermas = cmdMermas.ExecuteReader();
                    int cantidadMerma = 0;
                    if (readerMermas.Read())
                    {
                        cantidadMerma = readerMermas.GetInt32(0);
                    }
                    else
                    {
                        return res = -1;
                    }
                    //conteo de renglones
                    string selectRenglonSub = "select count(RenglonSub) from ProdD where Articulo ='" + codigo + "' and id = " + id + "";
                    SqlCommand cmdRenglonSub = new SqlCommand(selectRenglonSub, conn);
                    SqlDataReader readerRenglonSub = cmdRenglonSub.ExecuteReader();
                    int conteoRenglones = 0, contador = 0;
                    if (readerRenglonSub.Read())
                    {
                        conteoRenglones = readerRenglonSub.GetInt32(0);
                    }
                    else
                    {
                        return res = 1;
                    }
                    while (contador < conteoRenglones)
                    {
                        int nuevoLiberado = cantidadLiberado - rackHuecos, nuevoMermas = cantidadMerma + rackHuecos;
                        string selectCentroDestino = "SELECT Centro,CentroDestino from ProdD where id = " + id + " and Renglon = " + renglon + " and RenglonSub = " + contador;
                        SqlCommand cmdDestino = new SqlCommand(selectCentroDestino, conn);
                        SqlDataReader readerDestino = cmdDestino.ExecuteReader();
                        string centro = "", centroDestino = "";
                        if (readerDestino.Read())
                        {
                            centro = readerDestino.GetValue(0).ToString();
                            centroDestino = readerDestino.GetValue(1).ToString();
                            //ahora si viene lo chingon, que budha te ampare por que ahora vamos a ver si es directo o no directo.
                            if (!centro.Contains("CURAD") && centroDestino.Contains("CURAD"))//esta es la primera, si el centro no esta en curado, y el destino si tiene curado entonces pelas gallo papá
                            {
                                return res = 1;
                            }
                            else if (!centro.Contains("CURAD") && !centroDestino.Contains("CURAD"))//segunda, si el centro es un patio de produccion y el destino no es un cuarto de curado
                            {
                                //primero dime cuanto quedo en los racks
                                //luego actualizar CatProd
                                string updateCatProd = "UPDATE catProd set prodLiberado = " + nuevoLiberado + ", mermas = " + nuevoMermas + " where Codigo = '" + codigo + "' and OrdenProduccion = '" + op + "'";
                                SqlCommand cmdCatProd = new SqlCommand(updateCatProd, conn2);
                                cmdCatProd.ExecuteNonQuery();
                                //ahora sigue que actualicemos ProdD
                                string updateProdD = "UPDATE ProdD set CantidadA = " + rackHuecos + " where id = " + id + "and RenglonSub = " + contador + "";
                                SqlCommand cmdProdD = new SqlCommand(updateProdD, conn);
                                cmdProdD.ExecuteNonQuery();
                                //Generamos la entrada de produccion
                                string generarMerma = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Seleccion', 'Entrada Produccion', '" + user + "', @Estacion = 99";
                                SqlCommand cmdGenerarMerma = new SqlCommand(generarMerma, conn);
                                cmdGenerarMerma.ExecuteNonQuery();
                                //afectamos la entrada de produccion generada
                                string afectarMerma = "EXEC spAfectar 'PROD', " + getIdProd() + ", 'AFECTAR', 'Todo', 'Entrada Produccion', '" + user + "', @Estacion = 99";
                                SqlCommand cmdAfectarMerma = new SqlCommand(afectarMerma, conn);
                                cmdAfectarMerma.ExecuteNonQuery();
                                //por ultimo se insertan las mermas en Inv e InvD
                                string movId = setMovID("Merma Estiba", sucursal);
                                string insertInv = "Insert into INV (Empresa,Mov,MovID,FechaEmision,UltimoCambio,UEN,Moneda,TipoCambio,Usuario,Referencia,Observaciones,Estatus,Directo,Almacen,AlmacenTransito,OrigenTipo,Origen,OrigenID,Sucursal,SucursalOrigen,SucursalDestino)" + "values ('GNAP','Merma Estiba','" + movId + "',GETDATE(),GETDATE(),2,'Pesos',1,'" + user + "',(select referencia from prod where id=" + id + "),(select observaciones from prod where id=" + id + "),'PENDIENTE',1,'APT-BUS','(TRANSITO)','PROD','Entrada Produccion','" + op + "'," + sucursal + "," + sucursal + "," + sucursal + ")";
                                SqlCommand cmdInv = new SqlCommand(insertInv, conn);
                                cmdInv.ExecuteNonQuery();
                                int idInv = getIdInv();
                                string insertInvD = "Insert into INVD (ID,Renglon,RenglonSub,RenglonId,Renglontipo,Cantidad,Almacen,Codigo,Articulo,Unidad,Factor,Producto,Tipo,Sucursal)" + "values (" + getIdInv() + "," + renglon + ",0," + renglonId + ",'N'," + rackHuecos + ",'APT-BUS',null,'" + codigo + "',(select unidad from Art where Articulo='" + codigo + "'),(select factor from Art where Articulo='" + codigo + "'),'" + codigo + "','Salida'," + sucursal + ")";
                                SqlCommand cmdInvD = new SqlCommand(insertInvD, conn);
                                cmdInvD.ExecuteNonQuery();
                                string spAfectarMP = "EXEC spAfectar 'INV', " + idInv +
                                ",'Afectar','Todo','Merma Estiba','" + user + "',@Estacion = 99";
                                SqlCommand cmdAfectarMP = new SqlCommand(spAfectarMP, conn);
                                cmdAfectarMP.ExecuteNonQuery();
                                string act = "Update Inv set Estatus = 'CONCLUIDO' where id = " + idInv + "";
                                SqlCommand cmdAct = new SqlCommand(act, conn);
                                cmdAct.ExecuteNonQuery();
                                string insertMovFlujo = "Insert into movFlujo values (" + sucursal + ",'GNAP','PROD'," + id + ",'Entrada Produccion','" + op + "','INV'," + idInv + ",'Merma Estiba','" + movId + "',0)";
                                SqlCommand cmdMovFlujo = new SqlCommand(insertMovFlujo, conn);
                                cmdMovFlujo.ExecuteNonQuery();
                                contador = conteoRenglones;
                                return res;
                            }
                            else if (centro.Contains("CURAD") && !centroDestino.Contains("CURADO")) //son los productos que ya se avanzaron
                            {
                                //primero dime cuanto quedo en los racks
                                //luego actualizar CatProd
                                string updateCatProd = "UPDATE catProd set prodLiberado = " + nuevoLiberado + ", mermas = " + nuevoMermas + " where Codigo = '" + codigo + "' and OrdenProduccion = '" + op + "'";
                                SqlCommand cmdCatProd = new SqlCommand(updateCatProd, conn2);
                                cmdCatProd.ExecuteNonQuery();
                                //ahora sigue que actualicemos ProdD
                                string updateProdD = "UPDATE ProdD set CantidadA = " + rackHuecos + " where id = " + id + "and RenglonSub = " + contador + "";
                                SqlCommand cmdProdD = new SqlCommand(updateProdD, conn);
                                cmdProdD.ExecuteNonQuery();
                                //Generamos la entrada de produccion
                                string generarMerma = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Seleccion', 'Entrada Produccion', '" + user + "', @Estacion = 99";
                                SqlCommand cmdGenerarMerma = new SqlCommand(generarMerma, conn);
                                cmdGenerarMerma.ExecuteNonQuery();
                                //afectamos la entrada de produccion generada
                                string afectarMerma = "EXEC spAfectar 'PROD', " + getIdProd() + ", 'AFECTAR', 'Todo', 'Entrada Produccion', '" + user + "', @Estacion = 99";
                                SqlCommand cmdAfectarMerma = new SqlCommand(afectarMerma, conn);
                                cmdAfectarMerma.ExecuteNonQuery();
                                //por ultimo se insertan las mermas en Inv e InvD
                                string movId = setMovID("Merma Estiba", sucursal);
                                string insertInv = "Insert into INV (Empresa,Mov,MovID,FechaEmision,UltimoCambio,UEN,Moneda,TipoCambio,Usuario,Referencia,Observaciones,Estatus,Directo,Almacen,AlmacenTransito,OrigenTipo,Origen,OrigenID,Sucursal,SucursalOrigen,SucursalDestino)" + "values ('GNAP','Merma Estiba','" + movId + "',GETDATE(),GETDATE(),2,'Pesos',1,'" + user + "',(select referencia from prod where id=" + id + "),(select observaciones from prod where id=" + id + "),'PENDIENTE',1,'APT-BUS','(TRANSITO)','PROD','Entrada Produccion','" + op + "'," + sucursal + "," + sucursal + "," + sucursal + ")";
                                SqlCommand cmdInv = new SqlCommand(insertInv, conn);
                                cmdInv.ExecuteNonQuery();
                                int idInv = getIdInv();
                                string insertInvD = "Insert into INVD (ID,Renglon,RenglonSub,RenglonId,Renglontipo,Cantidad,Almacen,Codigo,Articulo,Unidad,Factor,Producto,Tipo,Sucursal)" + "values (" + getIdInv() + "," + renglon + ",0," + renglonId + ",'N'," + rackHuecos + ",'APT-BUS',null,'" + codigo + "',(select unidad from Art where Articulo='" + codigo + "'),(select factor from Art where Articulo='" + codigo + "'),'" + codigo + "','Salida'," + sucursal + ")";
                                SqlCommand cmdInvD = new SqlCommand(insertInvD, conn);
                                cmdInvD.ExecuteNonQuery();
                                string spAfectarMP = "EXEC spAfectar 'INV', " + idInv +
                                ",'Afectar','Todo','Merma Estiba','" + user + "',@Estacion = 99";
                                SqlCommand cmdAfectarMP = new SqlCommand(spAfectarMP, conn);
                                cmdAfectarMP.ExecuteNonQuery();
                                string act = "Update Inv set Estatus = 'CONCLUIDO' where id = " + idInv + "";
                                SqlCommand cmdAct = new SqlCommand(act, conn);
                                cmdAct.ExecuteNonQuery();
                                string insertMovFlujo = "Insert into movFlujo values (" + sucursal + ",'GNAP','PROD'," + id + ",'Entrada Produccion','" + op + "','INV'," + idInv + ",'Merma Estiba','" + movId + "',0)";
                                SqlCommand cmdMovFlujo = new SqlCommand(insertMovFlujo, conn);
                                cmdMovFlujo.ExecuteNonQuery();
                                contador = conteoRenglones;
                                return res;
                            }
                        }
                        else
                        {
                            contador++;
                        }
                    }
                }
                else if (rackHuecos > cantidadEstimada)
                {
                    return res = 2;
                }
                else if (rackHuecos == 0)
                {
                    return res = 3;
                }
                else
                {
                    return res = 1;
                }
            }
            catch (Exception e)
            {
                string error = e.Message;
                return res = 1;
            }
            return res;

        }

        [WebMethod]
        public int mermaRecepcion(int rackHuecos, int id, string codigo, string op, int renglon, string user, string sucursal)
        {
            int res = 0;
            string[] parametros = getParametros("Intelisis");
            string[] parametros2 = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + ";MultipleActiveResultSets=True");
            try
            {
                conn.Open();
                conn2.Open();
                //cuanta cantidad se estimo para este rack?
                string selectEstimado = "Select PZxT from CatArt where Clave ='" + codigo + "'";
                SqlCommand cmdEstimado = new SqlCommand(selectEstimado, conn2);
                SqlDataReader readerEstimado = cmdEstimado.ExecuteReader();
                int cantidadEstimada = 0;

                if (readerEstimado.Read())
                {
                    cantidadEstimada = int.Parse(readerEstimado.GetValue(0).ToString());
                }
                else
                {
                    return res = 1;
                }

                if (rackHuecos > 0 && rackHuecos < cantidadEstimada)
                {
                    int cantidadReal = cantidadEstimada - rackHuecos;
                    //datos de la produccion, cantidad en curado, renglon y renglonId
                    string selectCurado = "Select prodCurado from CatProd where codigo ='" + codigo + "' and OrdenProduccion ='" + op + "'";
                    SqlCommand cmdCurado = new SqlCommand(selectCurado, conn2);
                    SqlDataReader readerCurado = cmdCurado.ExecuteReader();
                    int cantidadCurado = 0, renglonId;
                    if (readerCurado.Read())
                    {
                        //estimaQty 
                        cantidadCurado = readerCurado.GetInt32(0);
                        //renglonId 
                        renglonId = renglon / 2048;
                    }
                    else
                    {
                        return res = 1;
                    }
                    //ahora vamos por la cantidad liberada hasta el momento
                    string selectLiberado = "Select prodLiberado from CatProd where codigo ='" + codigo + "' and OrdenProduccion ='" + op + "'";
                    SqlCommand cmdLiberado = new SqlCommand(selectLiberado, conn2);
                    SqlDataReader readerLiberado = cmdLiberado.ExecuteReader();
                    int cantidadLiberado = 0;
                    if (readerLiberado.Read())
                    {
                        cantidadLiberado = readerLiberado.GetInt32(0);
                    }
                    else
                    {
                        return res = 1;
                    }
                    //por ultimo, lo que se ha mermado hasta ahorita
                    string selectMermas = "Select mermas from CatProd where codigo ='" + codigo + "' and OrdenProduccion ='" + op + "'";
                    SqlCommand cmdMermas = new SqlCommand(selectMermas, conn2);
                    SqlDataReader readerMermas = cmdMermas.ExecuteReader();
                    int cantidadMerma = 0;
                    if (readerMermas.Read())
                    {
                        cantidadMerma = readerMermas.GetInt32(0);
                    }
                    else
                    {
                        return res = -1;
                    }
                    //conteo de renglones
                    string selectRenglonSub = "select count(RenglonSub) from ProdD where Articulo ='" + codigo + "' and id = " + id + "";
                    SqlCommand cmdRenglonSub = new SqlCommand(selectRenglonSub, conn);
                    SqlDataReader readerRenglonSub = cmdRenglonSub.ExecuteReader();
                    int conteoRenglones = 0, contador = 0;
                    if (readerRenglonSub.Read())
                    {
                        conteoRenglones = readerRenglonSub.GetInt32(0);
                    }
                    else
                    {
                        return res = 1;
                    }
                    while (contador < conteoRenglones)
                    {
                        int nuevoLiberado = cantidadLiberado - rackHuecos, nuevoMermas = cantidadMerma + rackHuecos;
                        string selectCentroDestino = "SELECT Centro,CentroDestino from ProdD where id = " + id + " and Renglon = " + renglon + " and RenglonSub = " + contador;
                        SqlCommand cmdDestino = new SqlCommand(selectCentroDestino, conn);
                        SqlDataReader readerDestino = cmdDestino.ExecuteReader();
                        string centro = "", centroDestino = "";
                        if (readerDestino.Read())
                        {
                            centro = readerDestino.GetValue(0).ToString();
                            centroDestino = readerDestino.GetValue(1).ToString();
                            //ahora si viene lo chingon, que budha te ampare por que ahora vamos a ver si es directo o no directo.
                            if (!centro.Contains("CURAD") && centroDestino.Contains("CURAD"))//esta es la primera, si el centro no esta en curado, y el destino si tiene curado entonces pelas gallo papá
                            {
                                return res = 1;
                            }
                            else if (!centro.Contains("CURAD") && !centroDestino.Contains("CURAD"))//segunda, si el centro es un patio de produccion y el destino no es un cuarto de curado
                            {
                                //primero dime cuanto quedo en los racks
                                //luego actualizar CatProd
                                string updateCatProd = "UPDATE catProd set prodLiberado = " + nuevoLiberado + ", mermas = " + nuevoMermas + " where Codigo = '" + codigo + "' and OrdenProduccion = '" + op + "'";
                                SqlCommand cmdCatProd = new SqlCommand(updateCatProd, conn2);
                                cmdCatProd.ExecuteNonQuery();
                                //ahora sigue que actualicemos ProdD
                                string updateProdD = "UPDATE ProdD set CantidadA = " + rackHuecos + " where id = " + id + "and RenglonSub = " + contador + "";
                                SqlCommand cmdProdD = new SqlCommand(updateProdD, conn);
                                cmdProdD.ExecuteNonQuery();
                                //Generamos la entrada de produccion
                                string generarMerma = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Seleccion', 'Entrada Produccion', '" + user + "', @Estacion = 99";
                                SqlCommand cmdGenerarMerma = new SqlCommand(generarMerma, conn);
                                cmdGenerarMerma.ExecuteNonQuery();
                                //afectamos la entrada de produccion generada
                                string afectarMerma = "EXEC spAfectar 'PROD', " + getIdProd() + ", 'AFECTAR', 'Todo', 'Entrada Produccion', '" + user + "', @Estacion = 99";
                                SqlCommand cmdAfectarMerma = new SqlCommand(afectarMerma, conn);
                                cmdAfectarMerma.ExecuteNonQuery();
                                //por ultimo se insertan las mermas en Inv e InvD
                                string movId = setMovID("Merma Estiba", sucursal);
                                string insertInv = "Insert into INV (Empresa,Mov,MovID,FechaEmision,UltimoCambio,UEN,Moneda,TipoCambio,Usuario,Referencia,Observaciones,Estatus,Directo,Almacen,AlmacenTransito,OrigenTipo,Origen,OrigenID,Sucursal,SucursalOrigen,SucursalDestino)" + "values ('GNAP','Merma Estiba','" + movId + "',GETDATE(),GETDATE(),2,'Pesos',1,'" + user + "',(select referencia from prod where id=" + id + "),(select observaciones from prod where id=" + id + "),'PENDIENTE',1,'APT-BUS','(TRANSITO)','PROD','Entrada Produccion','" + op + "'," + sucursal + "," + sucursal + "," + sucursal + ")";
                                SqlCommand cmdInv = new SqlCommand(insertInv, conn);
                                cmdInv.ExecuteNonQuery();
                                int idInv = getIdInv();
                                string insertInvD = "Insert into INVD (ID,Renglon,RenglonSub,RenglonId,Renglontipo,Cantidad,Almacen,Codigo,Articulo,Unidad,Factor,Producto,Tipo,Sucursal)" + "values (" + getIdInv() + "," + renglon + ",0," + renglonId + ",'N'," + rackHuecos + ",'APT-BUS',null,'" + codigo + "',(select unidad from Art where Articulo='" + codigo + "'),(select factor from Art where Articulo='" + codigo + "'),'" + codigo + "','Salida'," + sucursal + ")";
                                SqlCommand cmdInvD = new SqlCommand(insertInvD, conn);
                                cmdInvD.ExecuteNonQuery();
                                string spAfectarMP = "EXEC spAfectar 'INV', " + idInv +
                                ",'Afectar','Todo','Merma Estiba','" + user + "',@Estacion = 99";
                                SqlCommand cmdAfectarMP = new SqlCommand(spAfectarMP, conn);
                                cmdAfectarMP.ExecuteNonQuery();
                                string act = "Update Inv set Estatus = 'CONCLUIDO' where id = " + idInv + "";
                                SqlCommand cmdAct = new SqlCommand(act, conn);
                                cmdAct.ExecuteNonQuery();
                                string insertMovFlujo = "Insert into movFlujo values (" + sucursal + ",'GNAP','PROD'," + id + ",'Entrada Produccion','" + op + "','INV'," + idInv + ",'Merma Estiba','" + movId + "',0)";
                                SqlCommand cmdMovFlujo = new SqlCommand(insertMovFlujo, conn);
                                cmdMovFlujo.ExecuteNonQuery();
                                contador = conteoRenglones;
                                return res;
                            }
                            else if (centro.Contains("CURAD") && !centroDestino.Contains("CURADO")) //son los productos que ya se avanzaron
                            {
                                //primero dime cuanto quedo en los racks
                                //luego actualizar CatProd
                                string updateCatProd = "UPDATE catProd set prodLiberado = " + nuevoLiberado + ", mermas = " + nuevoMermas + " where Codigo = '" + codigo + "' and OrdenProduccion = '" + op + "'";
                                SqlCommand cmdCatProd = new SqlCommand(updateCatProd, conn2);
                                cmdCatProd.ExecuteNonQuery();
                                //ahora sigue que actualicemos ProdD
                                string updateProdD = "UPDATE ProdD set CantidadA = " + rackHuecos + " where id = " + id + "and RenglonSub = " + contador + "";
                                SqlCommand cmdProdD = new SqlCommand(updateProdD, conn);
                                cmdProdD.ExecuteNonQuery();
                                //Generamos la entrada de produccion
                                string generarMerma = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Seleccion', 'Entrada Produccion', '" + user + "', @Estacion = 99";
                                SqlCommand cmdGenerarMerma = new SqlCommand(generarMerma, conn);
                                cmdGenerarMerma.ExecuteNonQuery();
                                //afectamos la entrada de produccion generada
                                string afectarMerma = "EXEC spAfectar 'PROD', " + getIdProd() + ", 'AFECTAR', 'Todo', 'Entrada Produccion', '" + user + "', @Estacion = 99";
                                SqlCommand cmdAfectarMerma = new SqlCommand(afectarMerma, conn);
                                cmdAfectarMerma.ExecuteNonQuery();
                                //por ultimo se insertan las mermas en Inv e InvD
                                string movId = setMovID("Merma Estiba", sucursal);
                                string insertInv = "Insert into INV (Empresa,Mov,MovID,FechaEmision,UltimoCambio,UEN,Moneda,TipoCambio,Usuario,Referencia,Observaciones,Estatus,Directo,Almacen,AlmacenTransito,OrigenTipo,Origen,OrigenID,Sucursal,SucursalOrigen,SucursalDestino)" + "values ('GNAP','Merma Estiba','" + movId + "',GETDATE(),GETDATE(),2,'Pesos',1,'" + user + "',(select referencia from prod where id=" + id + "),(select observaciones from prod where id=" + id + "),'PENDIENTE',1,'APT-BUS','(TRANSITO)','PROD','Entrada Produccion','" + op + "'," + sucursal + "," + sucursal + "," + sucursal + ")";
                                SqlCommand cmdInv = new SqlCommand(insertInv, conn);
                                cmdInv.ExecuteNonQuery();
                                int idInv = getIdInv();
                                string insertInvD = "Insert into INVD (ID,Renglon,RenglonSub,RenglonId,Renglontipo,Cantidad,Almacen,Codigo,Articulo,Unidad,Factor,Producto,Tipo,Sucursal)" + "values (" + getIdInv() + "," + renglon + ",0," + renglonId + ",'N'," + rackHuecos + ",'APT-BUS',null,'" + codigo + "',(select unidad from Art where Articulo='" + codigo + "'),(select factor from Art where Articulo='" + codigo + "'),'" + codigo + "','Salida'," + sucursal + ")";
                                SqlCommand cmdInvD = new SqlCommand(insertInvD, conn);
                                cmdInvD.ExecuteNonQuery();
                                string spAfectarMP = "EXEC spAfectar 'INV', " + idInv +
                                ",'Afectar','Todo','Merma Estiba','" + user + "',@Estacion = 99";
                                SqlCommand cmdAfectarMP = new SqlCommand(spAfectarMP, conn);
                                cmdAfectarMP.ExecuteNonQuery();
                                string act = "Update Inv set Estatus = 'CONCLUIDO' where id = " + idInv + "";
                                SqlCommand cmdAct = new SqlCommand(act, conn);
                                cmdAct.ExecuteNonQuery();
                                string insertMovFlujo = "Insert into movFlujo values (" + sucursal + ",'GNAP','PROD'," + id + ",'Entrada Produccion','" + op + "','INV'," + idInv + ",'Merma Estiba','" + movId + "',0)";
                                SqlCommand cmdMovFlujo = new SqlCommand(insertMovFlujo, conn);
                                cmdMovFlujo.ExecuteNonQuery();
                                contador = conteoRenglones;
                                return res;
                            }
                        }
                        else
                        {
                            contador++;
                        }
                    }
                }
                else if (rackHuecos > cantidadEstimada)
                {
                    return res = 2;
                }
                else if (rackHuecos == 0)
                {
                    return res = 3;
                }
                else
                {
                    return res = 1;
                }
            }
            catch (Exception e)
            {
                string error = e.Message;
                return res = 1;
            }
            return res;
        }

        [WebMethod]
        public DataSet showProdComp()
        {
            DataSet ds = new DataSet();
            try
            {
                string[] parametros = getParametros("Solutia");
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
                conn.Open();
                string select = "Select IdEscuadra as [Escuadra #], EPC as EPC, OrdenProduccion as [Orden de Produccion], CodigoProducto as Codigo, Piezas from DetEscuadras where Asignado = 1 and Ubicada=0";
                SqlDataAdapter da = new SqlDataAdapter(select, conn);
                da.Fill(ds);
                conn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return ds;
        }

        [WebMethod]
        public DataSet dsMermaAlmacen()
        {
            DataSet ds = new DataSet();
            try
            {
                string[] parametros = getParametros("Solutia");
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
                conn.Open();
                string select = "Select IdEscuadra as [Escuadra #], EPC as EPC, OrdenProduccion as [Orden de Produccion], CodigoProducto as Codigo, Piezas from DetEscuadras where Asignado = 1 and Ubicada=1";
                SqlDataAdapter da = new SqlDataAdapter(select, conn);
                da.Fill(ds);
                conn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return ds;
        }

        [WebMethod]
        public string[] getDataEscuadra(string epc)
        {
            string[] data = new string[6];
            try
            {
                string[] parametros = getParametros("Solutia");
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
                conn.Open();
                string select = "Select de.IdEscuadra as [Escuadra #], de.EPC as EPC, de.OrdenProduccion as [Orden de Produccion], de.CodigoProducto as Codigo, ca.[Descripción], de.Piezas from DetEscuadras de inner join CatArt ca on ca.Clave = de.CodigoProducto where de.Asignado = 1 and de.Ubicada=1 and de.EPC = '" + epc + "'";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    for (int x = 0; x < 6; x++)
                    {
                        data[x] = reader.GetValue(x).ToString();
                    }
                }
                else
                {
                    return data = null;
                }
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return data = null;
            }
            return data;
        }

        [WebMethod]
        public int getProdId(string op, string codigo)
        {
            int renglon = 0;
            string[] prueba = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");
            try
            {
                conn.Open();
                string select = "Select Id from catProd where OrdenProduccion ='" + op + "' and Codigo = '" + codigo + "'";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    renglon = int.Parse(reader.GetValue(0).ToString());
                }
                else
                {
                    renglon = -1;
                }
                conn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                renglon = -1;
            }
            return renglon;
        }

        [WebMethod]
        public int setContado(string epc)
        {
            string[] parametros = getParametros("Solutia");
            int res = 0;
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
                conn.Open();
                string update = "UPDATE DetRProd set Contado = 1 where EPC = '" + epc + "'";
                SqlCommand cmd = new SqlCommand(update, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                res = 1;
            }
            return res;
        }

        [WebMethod]
        public Boolean validaLPT(string epc, string op)
        {
            string[] parametros = getParametros("Solutia");
            Boolean result = false;
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                conn.Open();
                string select = "select * from v_movs where EPC = '" + epc + "' and OrdenProduccion = '" + op + "' and Concepto = 'LBT'";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                }
                else
                {
                    result = true;
                }
                conn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                result = true;
            }
            return result;
        }

        [WebMethod]
        public DataSet getEscuadrasAsignadas(string op,string codigo)
        {
            DataSet ds = new DataSet();
            try
            {
                string[] parametros = getParametros("Solutia");
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
                conn.Open();
                string select = "Select * from DetEscuadras where Asignado=1 and Ubicada=0 and OrdenProduccion='" + op + "' and CodigoProducto='" + codigo + "'";
                SqlDataAdapter da = new SqlDataAdapter(select, conn);
                da.Fill(ds);
                conn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return ds;
        }

        [WebMethod]
        public DataSet getPickedEscuadras(string op, string codigo)
        {
            DataSet ds = new DataSet();
            try
            {
                string[] parametros = getParametros("Solutia");
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
                conn.Open();
                string select = "Select * from DetEscuadras where Asignado=1 and Ubicada=1 and OrdenProduccion='" + op + "' and CodigoProducto='" + codigo + "' and picked = 1 and Embarcado=0";
                SqlDataAdapter da = new SqlDataAdapter(select, conn);
                da.Fill(ds);
                conn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return ds;
        }

        [WebMethod]
        public int embarcaEscuadra(string EPC)
        {
            int res = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                string update = "update DetEscuadras set Embarcado= 1 where EPC ='" + EPC + "'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(update, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                res = 1;
            }
            return res;
        }

        [WebMethod]
        public DataSet getEmbarcadas(string op, string codigo)
        {
            DataSet ds = new DataSet();
            try
            {
                string[] parametros = getParametros("Solutia");
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
                conn.Open();
                string select = "Select * from DetEscuadras where Asignado=1 and Ubicada=1 and OrdenProduccion='" + op + "' and CodigoProducto='" + codigo + "' and picked = 1 and Embarcado = 1";
                SqlDataAdapter da = new SqlDataAdapter(select, conn);
                da.Fill(ds);
                conn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return ds;
        }

        [WebMethod]
        public int clearEscuadra(string EPC)
        {
            int res = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                string update = "update DetEscuadras SET OrdenProduccion = NULL, Asignado = 0, Ubicada = 0, CodigoProducto = NULL, Picked = 0, Embarcado = 0, Piezas = 0 where EPC = '" + EPC + "'";
                //string delete = "delete from Etiquetas_Impresas where EPC = '" + EPC + "'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(update, conn);
                //SqlCommand cmd2 = new SqlCommand(delete, conn);
                cmd.ExecuteNonQuery();
                //cmd2.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                res = 1;
            }
            return res;
        }

        [WebMethod]
        public int clearRack(string OP, string renglon)
        {
            int res = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                string update = "update DetRProd SET OrdenProduccion = NULL, Estado = 0, Verificado = 0, CantidadEstimada = 0, CantidadReal = 0, CodigoProducto = NULL, Contado = 0, Renglon = NULL where OrdenProduccion = '" + OP + "' and Contado = 1 and Renglon = "+renglon+"";
                //string delete = "delete from Etiquetas_Impresas where EPC = '" + EPC + "'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(update, conn);
                //SqlCommand cmd2 = new SqlCommand(delete, conn);
                cmd.ExecuteNonQuery();
                //cmd2.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                res = 1;
            }
            return res;
        }

        [WebMethod]
        public int insertMermas(string cveArt, string descArt,string op, string tarima, string epcEscuadra, string tipoMerma, string responsable, int cantidad)
        {
            int result = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                conn.Open();
                string insert = "insert into Mermas (cveArticulo, descArticulo, ordenProduccion, tarima, epcEscuadra, tipoMerma,	responsable, cantMerma) values ('" + cveArt + "','" + descArt + "','" + op + "'," + tarima + ",'" + epcEscuadra + "','" + tipoMerma + "','" + responsable + "'," + cantidad + ")";
                SqlCommand cmd = new SqlCommand(insert, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                result = 1;
            }
            return result;
        }

        [WebMethod]
        public Boolean mermaAprobada(string epc, string op)
        {
            Boolean resultado = false;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                conn.Open();
                string select = "select * from Mermas where epcEscuadra ='" + epc + "' and ordenProduccion ='" + op + "' and aprobado = 1";
                SqlCommand cmd = new SqlCommand(select,conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                }
                else
                {
                    resultado = true;
                }
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                resultado = true;
            }
            return resultado;
        }

        [WebMethod]
        public int insertMovinEntrada(int idConcepto, string op, int partida, int tarima, string epc, string cveArticulo, int cantidad, string cveAlmacen)
        {
            int result = 0;
            int folio = getNextFolio(idConcepto);
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                conn.Open();
                string insertMovin = "Insert into movin (folio,idconcepto,ordenproduccion) values (" + folio + "," + idConcepto + ",'" + op + "')";
                string insertDemovin = "insert into demovin (idMovin,partida,tarima,epc,cveArticulo,cantidad,cveAlmacen) values(" + getLastMovin() + "," + partida + "," + tarima + ",'" + epc + "','" + cveArticulo + "'," + cantidad + ",'" + cveAlmacen + "')";
                SqlCommand cmdMovin = new SqlCommand(insertMovin, conn);
                cmdMovin.ExecuteNonQuery();
                SqlCommand cmdDemovin = new SqlCommand(insertDemovin, conn);
                cmdDemovin.ExecuteNonQuery();
                //--validar si el producto existe.
                string selectExistencia = "Select * from Existencias where cveArticulo='" + cveArticulo + "' and cveAlmacen ='" + cveAlmacen + "'";
                SqlCommand cmdExistencia = new SqlCommand(selectExistencia, conn);
                SqlDataReader reader = cmdExistencia.ExecuteReader();
                if (reader.Read())//si el lector regresa algo, actualiza existencias
                {
                    //primero obtengamos la cantidad actual del producto
                    string getCantidad = "Select existencia from Existencias where cveArticulo='" + cveArticulo + "' and cveAlmacen ='" + cveAlmacen + "'";
                    SqlCommand cmdCantidad = new SqlCommand(getCantidad, conn);
                    if (reader.Read())
                    {
                        int actual = reader.GetInt32(0);
                        //ahora obtenemos el nuevo valor de existencia ya con la entrada de producto
                        int final = actual + cantidad;
                        //ahora actualizamos la tabla;
                        string updateExistencias = "update Existencias set existencia =" + final + " where cveArticulo = '" + cveArticulo + "' and cveAlmacen = '" + cveAlmacen + "'";
                        SqlCommand cmdUExistencias = new SqlCommand(updateExistencias, conn);
                        cmdUExistencias.ExecuteNonQuery();
                    }
                    else
                    {
                    }
                }
                else//inserta los nuevos valores
                {
                    string insertExistencias = "insert into Existencias values('" + cveArticulo + "','" + cveAlmacen + "'," + cantidad + ")";
                    SqlCommand cmdIExistencias = new SqlCommand(insertExistencias, conn);
                    cmdIExistencias.ExecuteNonQuery();
                }
                
                string insertFolios = "insert into Folios values (" + idConcepto + "," + folio + ")";
                SqlCommand cmdFolios = new SqlCommand(insertFolios, conn);
                cmdFolios.ExecuteNonQuery();
                
                //actualizar el conteo de folios
                int nvoFolio = getFolioConcepto(idConcepto);
                nvoFolio = nvoFolio + 1;
                string updateFolios = "update catConcepto set folio = " + nvoFolio + " where id = " + idConcepto + "";
                SqlCommand cmdUF = new SqlCommand(updateFolios,conn);
                cmdUF.ExecuteNonQuery();
            conn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                result = 1;
            }
            return result;
        }

        [WebMethod]
        public int insertMovinSalida(int idConcepto, string op, int partida, int tarima, string epc, string cveArticulo, int cantidad, string cveAlmacen)
        {
            int result = 0;
            int folio = getNextFolio(idConcepto);
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                conn.Open();
                string insertMovin = "Insert into movin (folio,idconcepto,ordenproduccion) values (" + folio + "," + idConcepto + ",'" + op + "'";
                string insertDemovin = "insert into demovin values(" + getLastMovin() + "," + partida + "," + tarima + ",'" + epc + "','" + cveArticulo + "'," + cantidad + ")";
                SqlCommand cmdMovin = new SqlCommand(insertMovin, conn);
                cmdMovin.ExecuteNonQuery();
                SqlCommand cmdDemovin = new SqlCommand(insertDemovin, conn);
                cmdDemovin.ExecuteNonQuery();
                //--validar si el producto existe.
                string selectExistencia = "Select * from Existencias where cveArticulo='" + cveArticulo + " and cveAlmacen ='" + cveAlmacen + "'";
                SqlCommand cmdExistencia = new SqlCommand(selectExistencia, conn);
                SqlDataReader reader = cmdExistencia.ExecuteReader();
                if (reader.Read())//si el lector regresa algo, actualiza existencias
                {
                    //primero obtengamos la cantidad actual del producto
                    string getCantidad = "Select existencia from Existencias where cveArticulo='" + cveArticulo + "' and cveAlmacen ='" + cveAlmacen + "'";
                    SqlCommand cmdCantidad = new SqlCommand(getCantidad, conn);
                    if (reader.Read())
                    {
                        int actual = reader.GetInt32(0);
                        //ahora obtenemos el nuevo valor de existencia ya con la entrada de producto
                        int final = actual - cantidad;
                        //ahora actualizamos la tabla;
                        string updateExistencias = "update Existencias set existencia =" + final + " where cveArticulo = '" + cveArticulo + "' and cveAlmacen = '" + cveAlmacen + "'";
                        SqlCommand cmdUExistencias = new SqlCommand(updateExistencias, conn);
                        cmdUExistencias.ExecuteNonQuery();
                    }
                    else
                    {
                        int actual = 0;
                        //ahora obtenemos el nuevo valor de existencia ya con la entrada de producto
                        int final = actual - cantidad;
                        //ahora actualizamos la tabla;
                        string insertExistencias = "insert into Existencias values('" + cveArticulo + "','" + cveAlmacen + "'," + cantidad + ")";
                        SqlCommand cmdIExistencias = new SqlCommand(insertExistencias, conn);
                        cmdIExistencias.ExecuteNonQuery();
                    }
                }
                else//inserta los nuevos valores
                {
                    int actual = 0;
                    //ahora obtenemos el nuevo valor de existencia ya con la entrada de producto
                    int final = actual - cantidad;
                    //ahora actualizamos la tabla;
                    string insertExistencias = "insert into Existencias values('" + cveArticulo + "','" + cveAlmacen + "'," + cantidad + ")";
                    SqlCommand cmdIExistencias = new SqlCommand(insertExistencias, conn);
                    cmdIExistencias.ExecuteNonQuery();
                }

                string insertFolios = "insert into Folios values (" + idConcepto + "," + folio + ")";
                SqlCommand cmdFolios = new SqlCommand(insertFolios, conn);
                cmdFolios.ExecuteNonQuery();
                //actualizar el conteo de folios
                int nvoFolio = getFolioConcepto(idConcepto);
                nvoFolio = nvoFolio + 1;
                string updateFolios = "update catConcepto set folio = " + nvoFolio + " where id = " + idConcepto + "";
                SqlCommand cmdUF = new SqlCommand(updateFolios,conn);
                cmdUF.ExecuteNonQuery();
            conn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                result = 1;
            }
            return result;
        }
        
        [WebMethod]
        public int insertMovinMerma(int idConcepto, string op, int partida, int tarima, string epc, string cveArticulo, int cantidad, string cveAlmacen)
        {
            int result = 0;
            int folio = getNextFolio(idConcepto);
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                conn.Open();
                string insertMovin = "Insert into movin (folio,idconcepto,ordenproduccion) values (" + folio + "," + idConcepto + ",'" + op + "'";
                string insertDemovin = "insert into demovin values(" + getLastMovin() + "," + partida + "," + tarima + ",'" + epc + "','" + cveArticulo + "'," + cantidad + ")";
                string insertExistencias = "insert into Existencias values('" + cveArticulo + "','" + cveAlmacen + "'," + cantidad + ")";
                string insertFolios = "insert into Folios values ("+idConcepto+","+folio+")";
                SqlCommand cmdMovin = new SqlCommand(insertMovin,conn);
                SqlCommand cmdDemovin = new SqlCommand(insertDemovin,conn);
                SqlCommand cmdExistencias = new SqlCommand(insertExistencias,conn);
                SqlCommand cmdFolios = new SqlCommand(insertFolios,conn);
                cmdMovin.ExecuteNonQuery();
                cmdDemovin.ExecuteNonQuery();
                cmdExistencias.ExecuteNonQuery();
                cmdFolios.ExecuteNonQuery();
                //actualizar el conteo de folios
                int nvoFolio = getFolioConcepto(idConcepto);
                nvoFolio = nvoFolio + 1;
                string updateFolios = "update catConcepto set folio = " + nvoFolio + " where id = " + idConcepto + "";
                SqlCommand cmdUF = new SqlCommand(updateFolios,conn);
                cmdUF.ExecuteNonQuery();
            conn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                result = 1;
            }
            return result;
        }

        public int getNextFolio(int idConcepto)
        {
            int res = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                conn.Open();
                string getCantidad = "Select MAX(folio) from folios where idConcepto=" + idConcepto + "";
                SqlCommand cmdCantidad = new SqlCommand(getCantidad, conn);
                SqlDataReader reader = cmdCantidad.ExecuteReader();
                if (reader.Read())
                {
                    if (reader.IsDBNull(0))
                    {
                        res = 1;
                    }
                    else
                    {
                        res = reader.GetInt32(0);
                        res = res + 1;
                    }
                }
                else
                {
                    res = 1;
                }
                conn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return res;
            }
            return res;
        }

        public int getLastMovin()
        {
            int res = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                conn.Open();
                string getCantidad = "Select MAX(id) from movin";
                SqlCommand cmdCantidad = new SqlCommand(getCantidad, conn);
                SqlDataReader reader = cmdCantidad.ExecuteReader();
                if (reader.Read())
                {
                    if (reader.IsDBNull(0))
                    {
                        res = 1;
                        ;
                    }
                    else
                    {
                        res = reader.GetInt32(0);
                    }
                }
                else
                {
                    res = 1;
                }
                conn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return res;
            }
            return res;
        }

        public string setMovID(string mov,string sucursal)
        {
            string movId = "";
            int nextConsec = 0;
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                conn.Open();
                string getConsec = "select Consecutivo from InvC where Mov LIKE '%" + mov + "%' and Sucursal = "+sucursal+"";
                SqlCommand cmdConsec = new SqlCommand(getConsec, conn);
                SqlDataReader reader = cmdConsec.ExecuteReader();
                if (reader.Read())
                {
                     nextConsec= reader.GetInt32(0);
                }
                else
                {
                    nextConsec = 0;
                }
                nextConsec = nextConsec + 1;
                movId = "AB" + nextConsec;
                string updateConsec = "update InvC set Consecutivo = "+nextConsec+" where Mov like '%" +mov+"%'";
                SqlCommand cmdUC = new SqlCommand(updateConsec, conn);
                cmdUC.ExecuteNonQuery();
                conn.Close();
            }
            catch(Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return null;
            }
            return movId;
        }

        [WebMethod]
        public string[] getDetalleAlmacen(string op, string renglon)
        {
            string[] info = new string[12];
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                conn.Open();
                string select = "select Id,OrdenProduccion,Pedido,Cliente,Descripcion,Tipo,Medida,Color,Codigo,Cantidad,Estatus,Renglon,Asignado,prodEstibado,prodAsignado from catprod where OrdenProduccion = '" + op + "' and Codigo = " + renglon;
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    info[0] = reader.GetInt32(0).ToString();//id
                    info[1] = reader.GetString(1);//movId
                    info[2] = reader.GetValue(2).ToString();//pedido 
                    info[3] = reader.GetString(3);//cliente
                    info[4] = reader.GetString(4);//descripcion
                    info[5] = reader.GetString(5);//tipo
                    info[6] = reader.GetString(6);//medida
                    info[7] = reader.GetString(7);//color
                    info[8] = reader.GetString(8);//codigo
                    info[9] = reader.GetDouble(9).ToString();//cantidad
                    info[10] = reader.GetString(10);//Estatus
                    info[11] = reader.GetInt32(1).ToString();//renglon 
                    
                }
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return info;
        }

        [WebMethod]
        public DataSet getAsignadas(string op)
        {
            DataSet ds = new DataSet();
            try
            {
                string[] parametros = getParametros("Solutia");
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
                conn.Open();
                string select = "Select Id,OrdenProduccion,Pedido,Cliente,Descripcion,Tipo,Medida,Color,Codigo,Cantidad,Estatus,Renglon,Asignado,prodEstibado,prodAsignado from catProd where OrdenProduccion='" + op + "' and Asignado = 1";
                SqlDataAdapter da = new SqlDataAdapter(select, conn);
                da.Fill(ds);
                conn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return ds;
        }

        [WebMethod]
        public int getBautizoCompleto(string op, string codigo, string renglon)
        {
            int res = 0;
            int alta = 0;
            int asignados = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                conn.Open();
                string selectOP = "select * from detRProd where OrdenProduccion = '" + op + "' and CodigoProducto ='" + codigo + "' and Renglon=" + renglon + "";
                SqlCommand cmdSOP = new SqlCommand(selectOP, conn);
                SqlDataReader readerSOP = cmdSOP.ExecuteReader();

                if (readerSOP.Read())
                {
                    //primero seleccionamos CUANTAS estan dadas de alta
                    string selectAlta = "select count(*) from detRProd where OrdenProduccion = '" + op + "' and CodigoProducto ='" + codigo + "' and Renglon=" + renglon + "";
                    SqlCommand cmdAlta = new SqlCommand(selectAlta, conn);
                    SqlDataReader readerAlta = cmdAlta.ExecuteReader();
                    while (readerAlta.Read())
                    {
                        alta = readerAlta.GetInt32(0);
                    }
                    //luego cuantos racks tiene verificados esa orden
                    string selectAsignados = "select count(*) from detRProd where OrdenProduccion = '" + op + "' and CodigoProducto ='" + codigo + "' and Renglon=" + renglon + " and Verificado = 1";
                    SqlCommand cmdAsignados = new SqlCommand(selectAsignados, conn);
                    SqlDataReader readerAsignados = cmdAsignados.ExecuteReader();
                    while (readerAsignados.Read())
                    {
                        asignados = readerAsignados.GetInt32(0);
                    }
                    if (alta > asignados)
                    {
                        res = 2;
                    }
                    else
                    {
                        if (asignados == alta)
                        {
                        }
                        else
                        {
                            res = 1;
                        }
                    }
                }
                else
                {
                    res = 404;
                }
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                res = 1;
            }
            return res;
        }

        [WebMethod]
        public int ubicaEscuadra(string epcEscuadra, string codigo, string cantidad, string epcBandera, string codigoBandera, int rack)
        {
            int res = 0;
            try
            {
                string[] parametros = getParametros("Solutia");
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
                string[] prueba = getParametros("ConsolaAdmin");
                SqlConnection connCA = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + ";MultipleActiveResultSets=True");
                conn.Open();
                connCA.Open();
                string insertPos = "Insert into posiciones values (" + getNextId() + ",1,"+getIdAlmacen(rack)+",1,1,.000,1,1,2,'" + epcEscuadra + "','" + codigo + "'," + cantidad + ",'" + epcBandera + "','" + codigoBandera + "')";
                string insertPosD = "Insert into DetallePos values ('" + codigo + "', '" + epcEscuadra + "', '" + codigoBandera + "', '" + epcBandera + "')";
                string updateEsc = "Update DetEscuadras set Ubicada = 1 where EPC ='" + epcEscuadra + "' and CodigoProducto = '" + codigo + "'";
                SqlCommand cmdPos = new SqlCommand(insertPos, connCA);
                SqlCommand cmdPosD = new SqlCommand(insertPosD,connCA);
                SqlCommand cmdEsc = new SqlCommand(updateEsc, conn);
                cmdPos.ExecuteNonQuery();
                cmdPosD.ExecuteNonQuery();
                cmdEsc.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                res = 1;
            }
            return res;
        }

        [WebMethod]
        public int CxEsc(string epc, string op, string codigo)
        {
            int qty=0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                conn.Open();
                string select = "select Piezas from DetEscuadras where EPC = '" + epc + "' and OrdenProduccion = '" + op + "' and CodigoProducto = '" + codigo + "'";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    qty = reader.GetInt32(0);
                }
                else
                {
                    qty = -1;
                }

            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                qty = -1;
            }
            return qty;
        }

        public int getNextId(){
            int id = 0;
            string[] prueba = getParametros("ConsolaAdmin");
            SqlConnection connCA = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + ";MultipleActiveResultSets=True");
            try
            {
                connCA.Open();
                string select = "select IDPosicion from posiciones order by IDPosicion desc";
                SqlCommand cmd = new SqlCommand(select, connCA);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    id = reader.GetInt32(0);
                    id = id + 1;
                }
                else
                {
                }

            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return id;
        }
    
        [WebMethod]
        public DataSet getZonaEPC(string op)
        {
            DataSet ds = new DataSet();
            try
            {
                string[] parametros = getParametros("Solutia");
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
                conn.Open();
                string select = "Select EPC from detBanderas where IdBandera='" + op + "'";
                SqlDataAdapter da = new SqlDataAdapter(select, conn);
                da.Fill(ds);
                conn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return ds;
        }

        [WebMethod]
        public string[] getZonaRack(string idZona, string idRack)
        {
            string[] ZonaRack = new string[2];
            try
            {
                string[] parametros = getParametros("ConsolaAdmin");
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
                conn.Open();
                string selectZona = "Select ClaveZona from Zonas where idZona=" + idZona + "";
                string selectRack = "Select Clave from racks where IDRack=" + idRack + "";
                SqlCommand cmdZona = new SqlCommand(selectZona, conn);
                SqlCommand cmdRack = new SqlCommand(selectRack, conn);
                SqlDataReader readZona = cmdZona.ExecuteReader();
                SqlDataReader readRack = cmdRack.ExecuteReader();
                if (readZona.Read())
                {
                    ZonaRack[0] = readZona.GetString(0).ToString();
                    if (readRack.Read())
                    {
                        ZonaRack[1] = readRack.GetString(0).ToString();
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return null;
            }
            return ZonaRack;
        }

        [WebMethod]
        public string[] getDetalleEscuadra(string epc)
        {
            string[] parametros = getParametros("Solutia");
            string[] res = new string[4];
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                conn.Open();
                string select = "SELECT de.OrdenProduccion, cp.Pedido, ca.Descripción, de.Piezas,de.CodigoProducto" +
                    " FROM DetEscuadras de" +
                    " inner join CatProd cp on cp.OrdenProduccion = de.OrdenProduccion" +
                    " inner join catArt ca on ca.Clave = de.CodigoProducto" +
                    " where de.EPC='" + epc + "'";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    res[0] = reader.GetString(0);
                    res[1] = reader.GetString(1);
                    res[2] = reader.GetString(2);
                    res[3] = reader.GetInt32(3).ToString();
                }
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return res;
        }

        [WebMethod]
        public int getTotalCant(string op, string renglon)
        {
            int total = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                conn.Open();
                string select = "select Cantidad from catprod where OrdenProduccion = '" + op + "' and renglon = " + renglon + "";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    total = int.Parse(reader.GetDouble(0).ToString());
                }
                else
                {
                    total = -1;
                }
                conn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                total = -1;
            }
            return total;
        }

        [WebMethod]
        public int getEstibaActual(string op, string renglon)
        {
            int total = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                conn.Open();
                string select = "select prodEstibado from catprod where OrdenProduccion = '" + op + "' and renglon = " + renglon + "";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    total = reader.GetInt32(0);
                }
                else
                {
                    total = -1;
                }
                conn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                total = -1;
            }
            return total;
        }

        [WebMethod]
        public Decimal maximoPorTarima(string codigo)
        {
            Decimal cantidad = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                Decimal porcentaje = .10m;
                Decimal extra;
                string selectCXT = "select PZxT from catArt where Clave ='" + codigo + "'";
                conn.Open();
                SqlCommand cmdCXT = new SqlCommand(selectCXT, conn);
                SqlDataReader readerCXT = cmdCXT.ExecuteReader();
                if (readerCXT.Read())
                {
                    cantidad = int.Parse(readerCXT.GetString(0));
                    extra = cantidad * porcentaje;
                    extra = Decimal.Ceiling(extra);
                    cantidad = cantidad + extra;
                }
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return cantidad;
        }

        public int maxRenglonSub(string id, string codigo)
        {
            int cantidad = 0;
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                string selectRenglonSub = "select MAX(RenglonSub) from ProdD where Articulo ='" + codigo + "' and ID = " + id + "";
                conn.Open();
                SqlCommand cmdRenglonSub = new SqlCommand(selectRenglonSub, conn);
                SqlDataReader readerRenglonSub = cmdRenglonSub.ExecuteReader();
                if (readerRenglonSub.Read())
                {
                    cantidad = readerRenglonSub.GetInt32(0);
                }
                conn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return cantidad;
        }

        public int getTotalCantPend(string id, string renglon, int renglonSub, string codigo)
        {
            int total = 0;
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                conn.Open();
                string select = "select CantidadPendiente from ProdD where ID = '" + id + "' and renglon = " + renglon + " and renglonSub = " + renglonSub + " and Articulo = '" + codigo + "'";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string isNull = reader.GetDouble(0) + "";
                    total = int.Parse(reader.GetDouble(0).ToString());
                }
                else
                {
                    total = 0;
                }
                conn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                total = -1;
            }
            return total;
        }

        public string[] CentroTrabajo(string id, string codigo)
            {
            string[] centro = new string[3];
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                string selectRenglonSub = "select Centro,RenglonSub,CantidadPendiente from ProdD where Articulo ='" + codigo + "' and id = " + id + "";
                conn.Open();
                SqlCommand cmdRenglonSub = new SqlCommand(selectRenglonSub, conn);
                SqlDataReader readerRenglonSub = cmdRenglonSub.ExecuteReader();
                if (readerRenglonSub.Read())
                {
                    centro[0] = readerRenglonSub.GetString(0);
                    centro[1] = readerRenglonSub.GetValue(1).ToString();
                    centro[2] = readerRenglonSub.GetValue(2).ToString();
                }
                conn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return centro;
        }

        public int countRenglones(string id, string codigo)
        {
            int conteo = 0;
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                string selectRenglonSub = "select count(RenglonSub) from ProdD where Articulo ='" + codigo + "' and id = " + id + "";
                conn.Open();
                SqlCommand cmdRenglonSub = new SqlCommand(selectRenglonSub, conn);
                SqlDataReader readerRenglonSub = cmdRenglonSub.ExecuteReader();
                if (readerRenglonSub.Read())
                {
                    conteo = readerRenglonSub.GetInt32(0);
                }
                conn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return conteo;
        }
        
        /*
                [WebMethod(Description = "Trazabilidad: Agregar datos a MovimientosInventario, detallesMovimiento y actualizar catConceptos")]
                public DbQueryResultWS InsertaMovInventario(string claveConcepto, int idUsuario, MovimientoInventario[] arregloAux, DetalleMovimiento[] arreglodetallesMov)
                {
                    //
                    //MovimientoInventario MovInveAux = new MovimientoInventario();
                    //MovimientoInventario[] arregloAux = new MovimientoInventario[1];
                    //arregloAux[0] = MovInveAux;
                    //DetalleMovimiento detalleMov = new DetalleMovimiento();
                    //detalleMov.cantidad = 1;
                    //detalleMov.EPCArt = "";
                    //detalleMov.EPCPos = "";
                    //DetalleMovimiento[] arreglodetallesMov = new DetalleMovimiento[1];
                    //arreglodetallesMov[0] = detalleMov;

                    string ConsultaConcepto = "select * from catConceptos c where c.clave='" + claveConcepto + "'";
                    return insertTransaction(ConsultaConcepto, idUsuario, arregloAux, arreglodetallesMov);
                }

                public DbQueryResultWS insertTransaction(string consultaConcepto, int idusuario, MovimientoInventario[] arregloAux, DetalleMovimiento[] arreglodetallesMov)
                {
                    SqlConnection consolaAdmin = new SqlConnection("Data Source=172.16.1.31;Initial Catalog=RicsaMike;Persist Security Info=True;User ID=sa;Password=Adminpwd20");
                    consolaAdmin.Open();
                    DbQueryResultWS messag = new DbQueryResultWS();
                    SqlTransaction tran = consolaAdmin.BeginTransaction(IsolationLevel.Serializable);
                    SqlCommand reg = consolaAdmin.CreateCommand();
                    reg.Transaction = tran;
                    bool exito = false;
                    SqlDataReader rdr = null;
                    SqlDataReader rdr2 = null;
                    try
                    {
                        int idConcepto = 0;
                        string tipoMov = "";
                        int folio = 0;
                        reg.CommandText = consultaConcepto;
                        rdr = reg.ExecuteReader();
                        if (rdr.Read())
                        {
                            folio = rdr.GetInt32(4);
                            idConcepto = rdr.GetInt32(0);
                            tipoMov = rdr.GetValue(3).ToString();
                            rdr.Close();
                        }
                        if (idConcepto > 0 && folio > 0 && tipoMov.Length > 0)
                        {
                            reg.CommandText = "insert into movimientosInventario(idconcepto,fecha,idusuario,aux1,aux2,aux3,aux4,aux5,tipomovimiento,folio)" +
                            " values(" + idConcepto + ",getdate()," + idusuario + ",'" + arregloAux[0].Aux1 + "','" + arregloAux[0].Aux2 + "'" +
                            " ,'" + arregloAux[0].Aux3 + "','" + arregloAux[0].Aux4 + "','" + arregloAux[0].Aux5 + "','" + tipoMov + "'," + folio + ")";
                            exito = reg.ExecuteNonQuery() >= 1;
                            if (exito)
                            {

                                reg.CommandText = "SELECT idMovimientosInventario AS LastID FROM movimientosInventario WHERE idMovimientosInventario = @@Identity";

                                int idMovInv = 0;

                                rdr2 = reg.ExecuteReader();
                                if (rdr2.Read())
                                {
                                    idMovInv = rdr2.GetInt32(0);
                                    rdr2.Close();
                                }
                                if (idMovInv > 0)
                                {
                                    int partidas = 0;
                                    for (int dm = 0; dm < arreglodetallesMov.Length; dm++)
                                    {
                                        partidas++;
                                        reg.CommandText = "insert into detallesmovimiento(idmovimientoinventario,cantidad,epcart,epcpos,idart,costopromedio,ultimocosto,idalmacen,partida,importe)" +
                                        " values(" + idMovInv + "," + arreglodetallesMov[dm].cantidad + ",'" + arreglodetallesMov[dm].EPCArt + "','" + arreglodetallesMov[dm].EPCPos + "',nullif(" + arreglodetallesMov[dm].idArt + ",0)" +
                                        " ," + arreglodetallesMov[dm].CostoPromedio + "," + arreglodetallesMov[dm].UltimoCosto + ", nullif(" + arreglodetallesMov[dm].idAlmacen + ",0)," + partidas + "," + arreglodetallesMov[dm].Importe + ")";
                                        exito = reg.ExecuteNonQuery() >= 1;
                                    }
                                    folio++;
                                    reg.CommandText = "update catConceptos set folio=" + (folio) + " where idconcepto='" + idConcepto + "'";
                                    exito = reg.ExecuteNonQuery() > 0 && exito;
                                    if (exito)
                                    {
                                        tran.Commit();
                                        messag.Success = exito;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        messag.ErrorMessage = ex.Message;
                        if (rdr != null)
                        {
                            rdr.Close();
                        }
                        if (rdr2 != null)
                        {
                            rdr2.Close();
                        }
                        tran.Rollback();
                    }
                    consolaAdmin.Close();
                    return messag;
                }
                */
        
        [WebMethod(Description = "obtiene la posicion del articulo")]
        public WebServiceResult getPosicionArticulo(string EPCArticulo)
        {
            string[] parametros = getParametros("Solutia");
            SqlConnection consolaAdmin = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            string result = "";
            bool error = false;
            string errorMessage = "";
            string consulta = "SELECT PS.EPCPos FROM posiciones PS "
                + "FULL JOIN detallePos DP ON DP.EPCPos = PS.EPCPos "
                + "WHERE (PS.EPCArt = '" + EPCArticulo + "' OR DP.EPCArt = '" + EPCArticulo + "')";
            consolaAdmin.Open();
            try
            {
                String sql = consulta;
                SqlCommand comm = new SqlCommand(sql, consolaAdmin);
                SqlDataReader rdr;
                rdr = comm.ExecuteReader();
                if (rdr.Read())
                {
                    result = rdr[0].ToString();
                }
                consolaAdmin.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                consolaAdmin.Close();
                error = true;
                errorMessage = ex.Message;
            }

            WebServiceResult queryResult = new WebServiceResult();

            if (!error)
            {
                if (result != null && result.Length > 0)
                {
                    queryResult.id = 1;
                }
                else
                {
                    queryResult.id = -1;
                }
                queryResult.result = result;
                queryResult.Success = true;
            }
            else
            {
                queryResult.ErrorMessage = errorMessage;
            }


            return queryResult;
        }

        [WebMethod(Description = "valida si una ubicacion esta libre para ser usada")]
        public WebServiceResult posicionDisponible(string EPCPosicion)
        {
            string[] parametros = getParametros("Solutia");
            SqlConnection consolaAdmin = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            int result = -1;
            bool error = false;
            string errorMessage = "";
            string consulta = "DECLARE @v_tipo varchar(2)"
                              + " DECLARE @epc_articulo varchar(30)"
                              + " SELECT @v_tipo = TP.ClaveTP,@epc_articulo = PS.EPCArt  FROM posiciones PS"
                              + " INNER JOIN TipoPosiciones TP ON TP.IDTipoPos = PS.IDTipoPos"
                              + " WHERE PS.EPCPos = '" + EPCPosicion + "' AND TP.ClaveTP IN ('E','A')"
                              + " IF (@v_tipo = 'E' AND @epc_articulo = '')"
                              + " SELECT 1"
                              + " ELSE IF @v_tipo = 'A'"
                              + " SELECT 1"
                              + " ELSE"
                              + " SELECT -1";

            consolaAdmin.Open();
            try
            {
                String sql = consulta;
                SqlCommand comm = new SqlCommand(sql, consolaAdmin);
                SqlDataReader rdr;
                rdr = comm.ExecuteReader();
                if (rdr.Read())
                {
                    result = (int)rdr[0];
                }
                consolaAdmin.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                consolaAdmin.Close();
                error = true;
                errorMessage = ex.Message;
            }

            WebServiceResult queryResult = new WebServiceResult();

            if (!error)
            {
                queryResult.id = result;
                queryResult.Success = true;
            }
            else
            {
                queryResult.ErrorMessage = errorMessage;
            }


            return queryResult;
        }

        [WebMethod(Description = "Recibe una cadena de insert o update para ejecutarse")]
        public WebServiceResult asignarUbicacionArticulo(string EPCArticulo, string EPCPosicion)
        {
            string[] parametros = getParametros("Solutia");
            SqlConnection consolaAdmin = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            int inserted = 0;
            bool error = false;
            consolaAdmin.Open();
            string consulta = "DECLARE @v_tipo varchar(2)"
                            + " SELECT @v_tipo = TP.ClaveTP  FROM posiciones PS"
                            + " INNER JOIN TipoPosiciones TP ON TP.IDTipoPos = PS.IDTipoPos"
                            + " WHERE PS.EPCPos = '" + EPCPosicion + "' AND TP.ClaveTP IN ('E','A')"
                            + " IF @v_tipo = 'E'"
                            + " UPDATE posiciones set EPCArt = '" + EPCArticulo + "' where EPCPos = '" + EPCPosicion + "'"
                            + " ELSE IF @v_tipo = 'A'"
                            + " INSERT INTO detallePos VALUES ('" + EPCArticulo + "', '" + EPCArticulo + "', 1, '" + EPCPosicion + "', '" + EPCPosicion + "')";


            SqlCommand cmd = new SqlCommand(consulta, consolaAdmin);
            WebServiceResult webServiceResult = new WebServiceResult();
            try
            {
                inserted = cmd.ExecuteNonQuery();
                consolaAdmin.Close();
            }
            catch (Exception ex)
            {
                consolaAdmin.Close();
                webServiceResult.ErrorMessage = ex.Message;
                error = true;
            }

            if (!error)
            {
                webServiceResult.Success = true;
                webServiceResult.id = inserted;
            }
            else
            {
                webServiceResult.Success = false;
            }
            return webServiceResult;
        }

        [WebMethod(Description = "elimina un articulo de su posicion")]
        public WebServiceResult eliminarUbicacionArticulo(string EPCArticulo, string EPCPosicion)
        {
            string[] parametros = getParametros("Solutia");
            SqlConnection consolaAdmin = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            int deleted = 0;
            bool error = false;
            consolaAdmin.Open();
            string consulta = "DECLARE @v_tipo varchar(2)"
                            + " SELECT @v_tipo = TP.ClaveTP  FROM posiciones PS"
                            + " INNER JOIN TipoPosiciones TP ON TP.IDTipoPos = PS.IDTipoPos"
                            + " WHERE PS.EPCPos = '" + EPCPosicion + "' AND TP.ClaveTP IN ('E','A')"
                            + " IF @v_tipo = 'E'"
                            + " UPDATE posiciones set EPCArt = '' where EPCPos = '" + EPCPosicion + "'"
                            + " ELSE IF @v_tipo = 'A'"
                            + " DELETE FROM detallePos WHERE EPCArt='" + EPCArticulo + "' AND EPCPos='" + EPCPosicion + "'";


            SqlCommand cmd = new SqlCommand(consulta, consolaAdmin);
            WebServiceResult webServiceResult = new WebServiceResult();
            try
            {
                deleted = cmd.ExecuteNonQuery();
                consolaAdmin.Close();
            }
            catch (Exception ex)
            {
                consolaAdmin.Close();
                webServiceResult.ErrorMessage = ex.Message;
                error = true;
            }

            if (!error)
            {
                webServiceResult.Success = true;
                webServiceResult.id = deleted;
            }
            else
            {
                webServiceResult.Success = false;
            }
            return webServiceResult;
        }

        [WebMethod]
        public DataSet getMovOrFol(string command)
        {
            string[] prueba = getParametros("Solutia");
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
                string error = ex.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return ds;
        } 

        [WebMethod]
        public int getFolioConcepto(int concepto)
        {
            int folio = 0;
            string[] prueba = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");
            try
            {
                conn.Open();
                string getFolio = "select folio from catConceptos where id = " + concepto + "";
                SqlCommand cmdFolio = new SqlCommand(getFolio, conn);
                SqlDataReader readerFolio = cmdFolio.ExecuteReader();
                if (readerFolio.Read())
                {
                    folio = readerFolio.GetInt32(0);
                }
                else
                {
                    folio = -1;
                }
                conn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                folio = -1;
            }
            return folio;
        }

        [WebMethod]
        public int getCantidadEscuadra(string epc)
        {
            int cant = 0;
            string[] prueba = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");
            try
            {
                conn.Open();
                string getFolio = "select Piezas from detEscuadras where EPC = " + epc + "";
                SqlCommand cmdFolio = new SqlCommand(getFolio, conn);
                SqlDataReader readerFolio = cmdFolio.ExecuteReader();
                if (readerFolio.Read())
                {
                    cant = readerFolio.GetInt32(0);
                }
                else
                {
                    cant = -1;
                }
                conn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                cant = -1;
            }
            return cant;
        }

        [WebMethod]
        public int currentValue(string folio, string estado, string id, string renglon)
        {
            int current = 0;
            string[] prueba = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");
            try
            {
                conn.Open();
                if (estado == "INICIAL")
                {
                    string select = "Select prodAsignado from catProd where OrdenProduccion ='" + folio + "' and renglon = " + renglon + "";
                    SqlCommand cmd = new SqlCommand(select, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        current = reader.GetInt32(0);
                    }
                    else
                    {
                        current = -1;
                    }
                }
                if (estado == "PRODUCCION" || estado == "PENDIENTE")
                {
                    string select = "Select prodCurado from catProd where OrdenProduccion ='" + folio + "' and renglon = " + renglon + "";
                    SqlCommand cmd = new SqlCommand(select, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        current = reader.GetInt32(0);
                    }
                    else
                    {
                        current = -1;
                    }
                }
                else if (estado == "CURADO")
                {
                    string select = "Select prodLiberado from catProd where OrdenProduccion ='" + folio + "' and renglon = " + renglon + "";
                    SqlCommand cmd = new SqlCommand(select, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        current = reader.GetInt32(0);
                    }
                    else
                    {
                        current = -1;
                    }
                }
                else if (estado == "LIBERADO")
                {
                    string select = "Select prodConcluido from catProd where OrdenProduccion ='" + folio + "' and renglon = " + renglon + "";
                    SqlCommand cmd = new SqlCommand(select, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        current = reader.GetInt32(0);
                    }
                    else
                    {
                        current = -1;
                    }
                }
                conn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                current = -1;
            }
            return current;
        }

        [WebMethod]
        public int checkAsignados(string folio)
        {
            int count = 0;
            int res = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                string select = "Select count(IdDRProd) from DetRProd where OrdenProduccion = '" + folio + "'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                
                if (reader.Read())
                {
                    count = reader.GetInt32(0);
                }
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }

            if (count > 0)
                res = 0;
            else
                res = 1;
            return res;
        }

        [WebMethod]
        public string[] detalleEscuadra(string EPC)
        {
            string[] detalle = new string[5];
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                conn.Open();
                string select = "select cp.OrdenProduccion, cp.Pedido, cp.Descripcion, de.Piezas, cp.Codigo from DetEscuadras de inner join CatProd cp on cp.OrdenProduccion = de.Ordenproduccion where de.EPC = '" + EPC + "'";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    detalle[0] = reader.GetString(0);
                    detalle[1] = reader.GetString(1);
                    detalle[2] = reader.GetString(2);
                    detalle[3] = reader.GetInt32(3).ToString();
                    detalle[4] = reader.GetString(4);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return null;
            }
            return detalle;
        }

        [WebMethod]
        public string[] detalleProd(string op, string codigo)
        {
            string[] res = new string[17];
            try
            {
                string[] parametros = getParametros("Solutia");
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
                string select = "select Id,OrdenProduccion,Pedido,Cliente,Descripcion,Tipo,Medida,Color,Codigo,Cantidad,Estatus,Renglon,Asignado,prodEstibado,prodAsignado,prodCurado,prodLiberado from catProd where ordenProduccion = '" + op + "' and codigo = '" + codigo + "'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    for (int i = 0; i < res.Length; i++)
                    {
                        res[i] = reader.GetValue(i).ToString();
                    }
                }
                else
                {
                    return null;
                }

                conn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return null;
            }
            return res;
        }

        public string[] ordenRemision(string remision)
        {
            string[] remi = new string[2];
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                conn.Open();
                string select = "select MovId,Referencia from Prod where Mov = 'Orden Produccion' and Referencia = (Select OrigenID from Venta where MovId = '"+remision+"') ";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    remi[0] = reader.GetString(0);
                    remi[1] = reader.GetString(1);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return null;
            }
            return remi;
        }

        [WebMethod]
        public DataSet pickingEscuadra(string op,int valor)
        {
            string[] remision = ordenRemision(op);
            DataSet ds = new DataSet();
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                conn.Open();
                if (valor == 1)
                {
                    string select = "select distinct de.epc, de.OrdenProduccion, cp.Pedido, de.CodigoProducto, cp.Descripcion, de.Piezas, cp.Cliente from DetEscuadras de inner join catProd cp on cp.Codigo = de.CodigoProducto where de.OrdenProduccion = '" + remision[0] + "' and cp.Pedido = '"+remision[1]+"' and de.Asignado = 1 and de.Ubicada=1 and de.Picked = 0";
                    SqlCommand cmd = new SqlCommand(select, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
                else if (valor == 2)
                {
                    string select = "select distinct de.epc, de.OrdenProduccion, cp.Pedido, de.CodigoProducto, cp.Descripcion, de.Piezas, cp.Cliente from DetEscuadras de inner join catProd cp on cp.Codigo = de.CodigoProducto where de.OrdenProduccion = '" + remision[0] + "' and cp.Pedido = '" + remision[1] + "' and de.Asignado = 1 and de.Ubicada=1 and de.Picked = 1 and de.Embarcado = 0";
                    SqlCommand cmd = new SqlCommand(select, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
                else if (valor == 3)
                {
                    string select = "select distinct de.epc, de.OrdenProduccion, cp.Pedido, de.CodigoProducto, cp.Descripcion, de.Piezas, cp.Cliente from DetEscuadras de inner join catProd cp on cp.Codigo = de.CodigoProducto where de.OrdenProduccion = '" + remision[0] + "' and cp.Pedido = '" + remision[1] + "' and de.Asignado = 1 and de.Ubicada=1 and de.Picked = 1 and de.Embarcado = 1";
                    SqlCommand cmd = new SqlCommand(select, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return null;
            }
            return ds;
        }

        [WebMethod]
        public string[] arrayEPC()
        {
            string[] res;
            int size = 0;
            try
            {
                string[] parametros = getParametros("Solutia");
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
                string count = "select count(epc) from detEscuadras";
                string select = "select epc from detEscuadras";
                conn.Open();
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlCommand cmdCount = new SqlCommand(count, conn);
                SqlDataReader readerCount = cmdCount.ExecuteReader();
                SqlDataReader reader = cmd.ExecuteReader();
                if (readerCount.Read())
                    size = readerCount.GetInt32(0);
                else
                    return null;
                res = new string[size];
                int i = 0;
                while (reader.Read())
                {
                    res[i] = reader.GetValue(0).ToString();
                    i++;
                }
                conn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return null;
            }
            return res;
        }

        [WebMethod]
        public int remisionEmbarque(string remision)
        {
            int res = 0;
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                conn.Open();
                string select = "SELECT * FROM Embarque where Referencia = '"+remision+"'";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                }
                else
                {
                    res = 1;
                }
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                res = 1;
            }
            return res;
        }

        [WebMethod]
        public int calculaTarima(string codigo, int liberado)
        {
            Decimal cantidad = 0;
            int res = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            //primero se obtiene la cantidad de producto por ventana
            try
            {
                conn.Open();
                string select = "Select PZxT from catArt where Clave = '" + codigo + "'";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    cantidad = Decimal.Parse(reader.GetString(0));
                }
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            if (cantidad == 0)
            {
                return res;
            }
            else
            {
                cantidad = liberado / cantidad;
                cantidad = Decimal.Ceiling(cantidad);
                res = Convert.ToInt32(cantidad);
            }
            return res;
        }

        [WebMethod]
        public int TarimasAsignadas(string op)
        {
            int res = -1;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            //primero se obtiene la cantidad de producto por ventana
            try
            {
                conn.Open();
                string select = "Select count(EPC) from detEscuadras where OrdenProduccion = '" + op + "' and Asignado = 1";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    res = reader.GetInt32(0);
                }
                else
                {
                    return res;
                }

                if (res == 0)
                    res = res + 1;
                else
                {
                }
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return res = -1;
            }
            return res;
        }

        [WebMethod]
        public int TarimasImpresas(string op)
        {
            int res = -1;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            //primero se obtiene la cantidad de producto por ventana
            try
            {
                conn.Open();
                string select = "Select count(EPC) from detEscuadras where OrdenProduccion = '" + op + "' and Asignado = 1 and Ubicada = 1";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    res = reader.GetInt32(0);
                }
                else
                {
                    return res;
                }
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return res = -1;
            }
            return res;
        }

        public int nvaCantidad(string op, string id, string renglon)
        {
            int current = 0;
            string[] prueba = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");
            try
            {
                conn.Open();
                string select = "Select Cantidad from catProd where OrdenProduccion ='" + op + "' and Id = " + id + " and Renglon = " + renglon + "";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    current = int.Parse(reader.GetValue(0).ToString());
                }
                else
                {
                    current = -1;
                }
                conn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                current = -1;
            }
            return current;
        }

        [WebMethod]
        public int completaTransfer(string id,string user)
        {
            int res = 0;
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                conn.Open();
                string spAfectar = "exec spAfectar 'INV', "+id+", 'GENERAR', 'Todo', 'Transferencia', '"+user+"'";
                SqlCommand cmd = new SqlCommand(spAfectar, conn);
                cmd.ExecuteNonQuery();
                string Transferencia = "exec spAfectar 'INV', " + getIdInv() + ", 'AFECTAR', 'Todo', '"+user+"'";
                SqlCommand cmdTrans = new SqlCommand(Transferencia, conn);
                cmdTrans.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return res = 1;
            }
            return res;
        }

        [WebMethod]
        public int insertEtiquetas(string Pedido, string OrdenProduccion, string Cliente, string N_Tarima, string Producto, int Cantidad, int CantidadTarima, string Medida, string Codigo, string Color, string Tipo, string EPC)
        {
            int res = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            try
            {
                conn.Open();
                string insert = "insert into Etiquetas_Impresas values('" + Pedido + "','" + OrdenProduccion + "','" + Cliente + "','" + N_Tarima + "','" + Producto + "'," + Cantidad + "," + CantidadTarima + ",'" + Medida + "','" + Codigo + "','" + Color + "','" + Tipo + "','" + EPC + "')";
                SqlCommand cmd = new SqlCommand(insert, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                res = 1;
            }
            return res;
        }

        [WebMethod]
        public DataSet getEtiquetasOP(string op)
        {
            DataSet ds = new DataSet();
            try
            {
                string[] parametros = getParametros("Solutia");
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
                conn.Open();
                string select = "Select * from Etiquetas_Impresas where OrdenProduccion='" + op + "'";
                SqlDataAdapter da = new SqlDataAdapter(select, conn);
                da.Fill(ds);
                conn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return ds;
        }

        [WebMethod]
        public DataSet getEtiquetasCB(string op)
        {
            DataSet ds = new DataSet();
            try
            {
                string[] parametros = getParametros("Solutia");
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
                conn.Open();
                string select = "Select * from Etiquetas_Impresas where EPC='" + op + "'";
                SqlDataAdapter da = new SqlDataAdapter(select, conn);
                da.Fill(ds);
                conn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return ds;
        }

        [WebMethod]
        public string[] getArrayEtiquetasCB(string cb,string remision)
        {
            string[] or = ordenRemision(remision);
            string[] data = new string[12];
            try
            {
                string[] parametros = getParametros("Solutia");
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
                conn.Open();
                string select = "Select * from Etiquetas_Impresas where EPC='" + cb + "' and OrdenProduccion = '" + or[0] + "' and Pedido = '" + or[1] + "'";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int sizeReader = reader.FieldCount;
                    for (int x = 0; x < sizeReader; x++)
                    {
                        data[x] = reader.GetValue(x).ToString();
                    }
                }
                conn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                data = null;
            }
            return data;
        }

        [WebMethod]
        public string[] getArrayEtiquetasCodB(string cb)
        {
            string[] data = new string[12];
            try
            {
                string[] parametros = getParametros("Solutia");
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
                conn.Open();
                string select = "Select * from Etiquetas_Impresas where EPC='" + cb + "' order by Pedido desc";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int sizeReader = reader.FieldCount;
                    for (int x = 0; x < sizeReader; x++)
                    {
                        data[x] = reader.GetValue(x).ToString();
                    }
                }
                conn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                data = null;
            }
            return data;
        }
        
        [WebMethod]
        public int getRenglon(string op, string codigo)
        {
            int renglon = 0;
            string[] prueba = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");
            try
            {
                conn.Open();
                string select = "Select Renglon from catProd where OrdenProduccion ='" + op + "' and Codigo = '" + codigo + "'";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    renglon = int.Parse(reader.GetValue(0).ToString());
                }
                else
                {
                    renglon = -1;
                }
                conn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                renglon = -1;
            }
            return renglon;
        }

        [WebMethod]
        public DataSet hiddenEtiCB()
        {
            DataSet ds = new DataSet();
            try
            {
                string[] parametros = getParametros("Solutia");
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
                conn.Open();
                string select = "Select EPC from Etiquetas_Impresas ";
                SqlDataAdapter da = new SqlDataAdapter(select, conn);
                da.Fill(ds);
                conn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return ds;
        }

        [WebMethod]
        public int etiquetaReimpresa(int cantidad, string epc)
        {
            int res = 0;
            string[] parametros = getParametros("Solutia");
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
                conn.Open();

                string update = "UPDATE Etiquetas_Impresas Set CantidadTarima=" + cantidad + " where EPC = '" + epc + "";
                SqlCommand cmd = new SqlCommand(update, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                res = 1;
            }
            return res;            
        }

        public int getIdAlmacen(int Rack)
        {
            int res = 0;
            string[] parametros = getParametros("ConsolaAdmin");
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
                conn.Open(); 
                string select = "select v.IDVentana from ventanas v INNER JOIN niveles n on n.IDNivel = v.IDNivel inner join racks r on r.IDRack = n.IDRack where r.IDRack = " + Rack + "";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    res = reader.GetInt32(0);
                }
                conn.Close();
                return res;
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return res = 0;
            }
        }

        [WebMethod]
        public int racksAsignados(string codigo)
        {
            try
            {
                string[] parametros = getParametros("Solutia");
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
                conn.Open();
                string getPxT = "select PxT from catArt where Clave = '" + codigo + "'";
                SqlCommand cmdPxT = new SqlCommand(getPxT, conn);
                SqlDataReader readPxT = cmdPxT.ExecuteReader();
                int PxT = 0;
                if (readPxT.Read())
                {
                    PxT = int.Parse(readPxT.GetValue(0).ToString());
                    return PxT;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        [WebMethod]
        public int tarimasAsignadas(string codigo)
        {
            try
            {
                string[] parametros = getParametros("Solutia");
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
                conn.Open();
                string getPxT = "select PzxT from catArt where Clave = '" + codigo + "'";
                SqlCommand cmdPxT = new SqlCommand(getPxT, conn);
                SqlDataReader readPxT = cmdPxT.ExecuteReader();
                int PxT = 0;
                if (readPxT.Read())
                {
                    PxT = int.Parse(readPxT.GetValue(0).ToString());
                    return PxT;
                }
                else
                {
                    return -1;
                }
            }
            catch (FormatException e)
            {
                return -2;
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        [WebMethod]
        public int noHuecos(int huecos, int actual, int id, string op, string codigo, string user, string sucursal)
        {
            int res = 0;
            string[] parametros = getParametros("Intelisis");
            string[] parametros2 = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + ";MultipleActiveResultSets=True");
            SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + ";MultipleActiveResultSets=True");
            try
            {
                conn.Open();
                conn2.Open();

                if (huecos > 0 && huecos < actual)
                {
                    int diferencia = actual - huecos;
                    string selectRenglones = "Select Renglon from CatProd where codigo = '" + codigo + "' and OrdenProduccion = '" + op + "'";
                    SqlCommand cmdRenglones = new SqlCommand(selectRenglones, conn2);
                    SqlDataReader readerRenglones = cmdRenglones.ExecuteReader();
                    int renglon, renglonId;
                    if (readerRenglones.Read())
                    {
                        renglon = readerRenglones.GetInt32(0);
                        renglonId = renglon / 2048;
                    }
                    else
                    {
                        return res = 1;
                    }
                    string selectLiberado = "Select prodLiberado from CatProd where codigo ='" + codigo + "' and OrdenProduccion ='" + op + "'";
                    SqlCommand cmdLiberado = new SqlCommand(selectLiberado, conn2);
                    SqlDataReader readerLiberado = cmdLiberado.ExecuteReader();
                    int cantidadLiberado = 0;
                    if (readerLiberado.Read())
                    {
                        cantidadLiberado = readerLiberado.GetInt32(0);
                    }
                    else
                    {
                        return res = 1;
                    }
                    //por ultimo, lo que se ha mermado hasta ahorita
                    string selectMermas = "Select mermas from CatProd where codigo ='" + codigo + "' and OrdenProduccion ='" + op + "'";
                    SqlCommand cmdMermas = new SqlCommand(selectMermas, conn2);
                    SqlDataReader readerMermas = cmdMermas.ExecuteReader();
                    int cantidadMerma = 0;
                    if (readerMermas.Read())
                    {
                        cantidadMerma = readerMermas.GetInt32(0);
                    }
                    else
                    {
                        return res = -1;
                    }
                    //conteo de renglones
                    string selectRenglonSub = "select count(RenglonSub) from ProdD where Articulo ='" + codigo + "' and id = " + id + "";
                    SqlCommand cmdRenglonSub = new SqlCommand(selectRenglonSub, conn);
                    SqlDataReader readerRenglonSub = cmdRenglonSub.ExecuteReader();
                    int conteoRenglones = 0, contador = 0;
                    if (readerRenglonSub.Read())
                    {
                        conteoRenglones = readerRenglonSub.GetInt32(0);
                    }
                    else
                    {
                        return res = 1;
                    }
                    while (contador < conteoRenglones)
                    {
                        int nuevoCurado = 0, nuevoLiberado = cantidadLiberado + diferencia, nuevoMermas = cantidadMerma + huecos;
                        string selectCentroDestino = "SELECT Centro,CentroDestino from ProdD where id = " + id + " and Renglon = " + renglon + " and RenglonSub = " + contador;
                        SqlCommand cmdDestino = new SqlCommand(selectCentroDestino, conn);
                        SqlDataReader readerDestino = cmdDestino.ExecuteReader();
                        string centro = "", centroDestino = "";
                        if (readerDestino.Read())
                        {
                            centro = readerDestino.GetValue(0).ToString();
                            centroDestino = readerDestino.GetValue(1).ToString();
                            //ahora si viene lo chingon, que budha te ampare por que ahora vamos a ver si es directo o no directo.
                            if (!centro.Contains("CURAD") && centroDestino.Contains("CURAD"))//esta es la primera, si el centro no esta en curado, y el destino si tiene curado entonces pelas gallo papá
                            {
                                return res = 1;
                            }
                            else if (!centro.Contains("CURAD") && !centroDestino.Contains("CURAD"))//segunda, si el centro es un patio de produccion y el destino no es un cuarto de curado
                            {
                                //luego actualizar CatProd
                                string updateCatProd = "UPDATE catProd set prodCurado = " + nuevoCurado + ", prodLiberado = " + nuevoLiberado + ", mermas = " + nuevoMermas + " where Codigo = '" + codigo + "' and OrdenProduccion = '" + op + "'";
                                SqlCommand cmdCatProd = new SqlCommand(updateCatProd, conn2);
                                cmdCatProd.ExecuteNonQuery();
                                //ahora sigue que actualicemos ProdD
                                string updateProdD = "UPDATE ProdD set CantidadA = " + huecos + " where id = " + id + "and RenglonSub = " + contador + "";
                                SqlCommand cmdProdD = new SqlCommand(updateProdD, conn);
                                cmdProdD.ExecuteNonQuery();
                                //Generamos la entrada de produccion
                                string generarMerma = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Seleccion', 'Entrada Produccion', '" + user + "', @Estacion = 99";
                                SqlCommand cmdGenerarMerma = new SqlCommand(generarMerma, conn);
                                cmdGenerarMerma.ExecuteNonQuery();
                                //afectamos la entrada de produccion generada
                                string afectarMerma = "EXEC spAfectar 'PROD', " + getIdProd() + ", 'AFECTAR', 'Todo', 'Entrada Produccion', '" + user + "', @Estacion = 99";
                                SqlCommand cmdAfectarMerma = new SqlCommand(afectarMerma, conn);
                                cmdAfectarMerma.ExecuteNonQuery();
                                //por ultimo se insertan las mermas en Inv e InvD
                                string movId = setMovID("Merma Produccion", sucursal);
                                string insertInv = "Insert into INV (Empresa,Mov,MovID,FechaEmision,UltimoCambio,UEN,Moneda,TipoCambio,Usuario,Referencia,Observaciones,Estatus,Directo,Almacen,AlmacenTransito,OrigenTipo,Origen,OrigenID,Sucursal,SucursalOrigen,SucursalDestino)" + "values ('GNAP','Merma Produccion','" + movId + "',GETDATE(),GETDATE(),2,'Pesos',1,'" + user + "',(select referencia from prod where id=" + id + "),(select observaciones from prod where id=" + id + "),'PENDIENTE',1,'APT-BUS','(TRANSITO)','PROD','Entrada Produccion','" + op + "'," + sucursal + "," + sucursal + "," + sucursal + ")";
                                SqlCommand cmdInv = new SqlCommand(insertInv, conn);
                                cmdInv.ExecuteNonQuery();
                                int idInv = getIdInv();
                                string insertInvD = "Insert into INVD (ID,Renglon,RenglonSub,RenglonId,Renglontipo,Cantidad,Almacen,Codigo,Articulo,Unidad,Factor,Producto,Tipo,Sucursal)" + "values (" + getIdInv() + "," + renglon + ",0," + renglonId + ",'N'," + huecos + ",'APT-BUS',null,'" + codigo + "',(select unidad from Art where Articulo='" + codigo + "'),(select factor from Art where Articulo='" + codigo + "'),'" + codigo + "','Salida'," + sucursal + ")";
                                SqlCommand cmdInvD = new SqlCommand(insertInvD, conn);
                                cmdInvD.ExecuteNonQuery();
                                string spAfectarMP = "EXEC spAfectar 'INV', " + idInv +
                                ",'Afectar','Todo','Merma Produccion','" + user + "',@Estacion = 99";
                                SqlCommand cmdAfectarMP = new SqlCommand(spAfectarMP, conn);
                                cmdAfectarMP.ExecuteNonQuery();
                                string act = "Update Inv set Estatus = 'CONCLUIDO' where id = " + idInv + "";
                                SqlCommand cmdAct = new SqlCommand(act, conn);
                                cmdAct.ExecuteNonQuery();
                                string insertMovFlujo = "Insert into movFlujo values (" + sucursal + ",'GNAP','PROD'," + id + ",'Entrada Produccion','" + op + "','INV'," + idInv + ",'Merma Produccion','" + movId + "',0)";
                                SqlCommand cmdMovFlujo = new SqlCommand(insertMovFlujo, conn);
                                cmdMovFlujo.ExecuteNonQuery();
                                contador = conteoRenglones;
                                return res;
                            }
                            else if (centro.Contains("CURAD") && !centroDestino.Contains("CURADO")) //son los productos que ya se avanzaron
                            {
                                //luego actualizar CatProd
                                string updateCatProd = "UPDATE catProd set prodCurado = " + nuevoCurado + ", prodLiberado = " + nuevoLiberado + ", mermas = " + nuevoMermas + " where Codigo = '" + codigo + "' and OrdenProduccion = '" + op + "'";
                                SqlCommand cmdCatProd = new SqlCommand(updateCatProd, conn2);
                                cmdCatProd.ExecuteNonQuery();
                                //ahora sigue que actualicemos ProdD
                                string updateProdD = "UPDATE ProdD set CantidadA = " + huecos + " where id = " + id + "and RenglonSub = " + contador + "";
                                SqlCommand cmdProdD = new SqlCommand(updateProdD, conn);
                                cmdProdD.ExecuteNonQuery();
                                //Generamos la entrada de produccion
                                string generarMerma = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Seleccion', 'Entrada Produccion', '" + user + "', @Estacion = 99";
                                SqlCommand cmdGenerarMerma = new SqlCommand(generarMerma, conn);
                                cmdGenerarMerma.ExecuteNonQuery();
                                //afectamos la entrada de produccion generada
                                string afectarMerma = "EXEC spAfectar 'PROD', " + getIdProd() + ", 'AFECTAR', 'Todo', 'Entrada Produccion', '" + user + "', @Estacion = 99";
                                SqlCommand cmdAfectarMerma = new SqlCommand(afectarMerma, conn);
                                cmdAfectarMerma.ExecuteNonQuery();
                                //por ultimo se insertan las mermas en Inv e InvD
                                string movId = setMovID("Merma Produccion", sucursal);
                                string insertInv = "Insert into INV (Empresa,Mov,MovID,FechaEmision,UltimoCambio,UEN,Moneda,TipoCambio,Usuario,Referencia,Observaciones,Estatus,Directo,Almacen,AlmacenTransito,OrigenTipo,Origen,OrigenID,Sucursal,SucursalOrigen,SucursalDestino)" + "values ('GNAP','Merma Produccion','" + movId + "',GETDATE(),GETDATE(),2,'Pesos',1,'" + user + "',(select referencia from prod where id=" + id + "),(select observaciones from prod where id=" + id + "),'PENDIENTE',1,'APT-BUS','(TRANSITO)','PROD','Entrada Produccion','" + op + "'," + sucursal + "," + sucursal + "," + sucursal + ")";
                                SqlCommand cmdInv = new SqlCommand(insertInv, conn);
                                cmdInv.ExecuteNonQuery();
                                int idInv = getIdInv();
                                string insertInvD = "Insert into INVD (ID,Renglon,RenglonSub,RenglonId,Renglontipo,Cantidad,Almacen,Codigo,Articulo,Unidad,Factor,Producto,Tipo,Sucursal)" + "values (" + getIdInv() + "," + renglon + ",0," + renglonId + ",'N'," + huecos + ",'APT-BUS',null,'" + codigo + "',(select unidad from Art where Articulo='" + codigo + "'),(select factor from Art where Articulo='" + codigo + "'),'" + codigo + "','Salida'," + sucursal + ")";
                                SqlCommand cmdInvD = new SqlCommand(insertInvD, conn);
                                cmdInvD.ExecuteNonQuery();
                                string spAfectarMP = "EXEC spAfectar 'INV', " + idInv +
                                ",'Afectar','Todo','Merma Produccion','" + user + "',@Estacion = 99";
                                SqlCommand cmdAfectarMP = new SqlCommand(spAfectarMP, conn);
                                cmdAfectarMP.ExecuteNonQuery();
                                string act = "Update Inv set Estatus = 'CONCLUIDO' where id = " + idInv + "";
                                SqlCommand cmdAct = new SqlCommand(act, conn);
                                cmdAct.ExecuteNonQuery();
                                string insertMovFlujo = "Insert into movFlujo values (" + sucursal + ",'GNAP','PROD'," + id + ",'Entrada Produccion','" + op + "','INV'," + idInv + ",'Merma Produccion','" + movId + "',0)";
                                SqlCommand cmdMovFlujo = new SqlCommand(insertMovFlujo, conn);
                                cmdMovFlujo.ExecuteNonQuery();
                                contador = conteoRenglones;
                                return res;
                            }
                        }
                        else
                        {
                            contador++;
                        }
                    }
                }
                else if (huecos > actual)
                {
                    return res = 2;
                }
                else if (huecos == 0)
                {
                    return res = 0;
                }
                else
                {
                    return res = 1;
                }
            }
            catch (Exception e)
            {
                return res = 1;
            }
            return res;
        }
    }
}
    