using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.IO;


namespace WebServiceGoMobile
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class Service1 : System.Web.Services.WebService
    {

        /*++++++++++++++++++++++++++++++++++++++++++++++++++++++
         *              CONFIGURACION OBLIGATORIA 
         *              
         *  Mover context de Web.config a tabla parametros
         * 
         * ++++++++++++++++++++++++++++++++++++++++++++++++++++++*/
        
        //String cadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings["ParametrosContext"].ConnectionString;
        String cadenaConexion = "Data Source=192.168.0.229;Initial Catalog=Parametros;Persist Security Info=True;User ID=sa;Password=NapresaPwd20; timeout=10";
        //indica el nombre de la Base Parametros para localizar base de cliente
        String connCliente = "Intelisis";

        /*++++++++++++++++++++++++++++++++++++++++++++++++++++++
        *               FIN DE CONFIGURACION OBLIGATORIA
        ++++++++++++++++++++++++++++++++++++++++++++++++++++++*/
                
        /*++++++++++++++++++++++++++++++++++++++++++++++++++++++
        *               METODOS EXCLUSIVOS PARA CADA CLIENTE
        ++++++++++++++++++++++++++++++++++++++++++++++++++++++*/
        
        private string entrega(string folio) {
            string[] prueba2 = getParametros(connCliente);
            SqlConnection conn2 = new SqlConnection("Data Source=" + prueba2[1] + "; Initial Catalog=" + prueba2[4] + "; Persist Security Info=True; User ID=" + prueba2[2] + "; Password=" + prueba2[3] + "");
            conn2.Open();
            int idEmbarque;
            try
            {
                string id = "SELECT id FROM embarque where Referencia LIKE '%" + folio + "%'";
                SqlCommand cmdId = new SqlCommand(id, conn2);
                SqlDataReader readId = cmdId.ExecuteReader();
                if (readId.Read())
                {
                    idEmbarque = int.Parse(readId.GetValue(0).ToString());
                }
                else
                {
                    return "Error al Actualizar Ventas de Intelisis";
                }
                conn2.Close();
                conn2.Open();
                //linea de modificasion anterior
                //zmb cambio 300714 String update = "Update Venta set EmbarqueEstado ='Entregado' where MovID ='" + folio + "' ";
                String update = "Update EmbarqueD set Estado = 'Entregado' where ID =" + idEmbarque + " and EmbarqueMov = (Select ID from EmbarqueMov where MovId = '" + folio + "')";
                //String update = "Update EmbarqueMov set Estado ='Entregado' where AsignadoID = (SELECT id FROM embarque where Referencia ='" + folio + "') and MovId = '" + folio + "'";
                SqlCommand cmd2 = new SqlCommand(update, conn2);
                int actualizado = cmd2.ExecuteNonQuery();
                conn2.Close();
                if (actualizado > 0)
                {
                    conn2.Open();
                    //string update = "Update EmbarqueD set Estado = 'Entregado' where ID =" + idEmbarque + " and EmbarqueMov = (Select ID from EmbarqueMov where MovId = '" + folio + "')";
                    //SqlCommand cmdUpdate = new SqlCommand(update, conn2);
                    string afectar = "exec spAfectar 'EMB'," + idEmbarque + ",'AFECTAR','Todo',NULL,''";
                    SqlCommand cmdAfectar = new SqlCommand(afectar, conn2);
                    cmdAfectar.ExecuteNonQuery();
                    //cmd2.ExecuteNonQuery();
                    return null;
                }
                else
                {
                    return "No hubo afectaciones a los embarques";
                }
            }
            catch
            {
                return "Error al Actualizar Ventas de Intelisis";
            }
            finally
            {
                conn2.Close();
            }
        } 
        
        /*++++++++++++++++++++++++++++++++++++++++++++++++++++++
        *               FIN METODOS EXCLUSIVOS PARA CADA CLIENTE
        ++++++++++++++++++++++++++++++++++++++++++++++++++++++*/

        /*
        [WebMethod]
        public string entrega(string folio)
        {
            string[] prueba2 = getParametros(connCliente);
            SqlConnection conn2 = new SqlConnection("Data Source=" + prueba2[1] + "; Initial Catalog=" + prueba2[4] + "; Persist Security Info=True; User ID=" + prueba2[2] + "; Password=" + prueba2[3] + "");
            conn2.Open();
            int idEmbarque;
            try
            {
                string id = "SELECT id FROM embarque where Referencia LIKE '%" + folio + "%'";
                SqlCommand cmdId = new SqlCommand(id, conn2);
                SqlDataReader readId = cmdId.ExecuteReader();
                if (readId.Read())
                {
                    idEmbarque = int.Parse(readId.GetValue(0).ToString());
                }
                else
                {
                    return "Error al Actualizar Ventas de Intelisis";
                }
                conn2.Close();
                conn2.Open();
                //linea de modificasion anterior
                //zmb cambio 300714 String update = "Update Venta set EmbarqueEstado ='Entregado' where MovID ='" + folio + "' ";
                String update = "Update EmbarqueD set Estado = 'Entregado' where ID =" + idEmbarque + " and EmbarqueMov = (Select ID from EmbarqueMov where MovId = '" + folio + "')";
                //String update = "Update EmbarqueMov set Estado ='Entregado' where AsignadoID = (SELECT id FROM embarque where Referencia ='" + folio + "') and MovId = '" + folio + "'";
                SqlCommand cmd2 = new SqlCommand(update, conn2);
                cmd2.ExecuteNonQuery();
                conn2.Close();
                conn2.Open();
                //string update = "Update EmbarqueD set Estado = 'Entregado' where ID =" + idEmbarque + " and EmbarqueMov = (Select ID from EmbarqueMov where MovId = '" + folio + "')";
                //SqlCommand cmdUpdate = new SqlCommand(update, conn2);
                string afectar = "exec spAfectar 'EMB'," + idEmbarque + ",'AFECTAR','Todo',NULL,''";
                SqlCommand cmdAfectar = new SqlCommand(afectar, conn2);
                cmdAfectar.ExecuteNonQuery();
                //cmd2.ExecuteNonQuery();
                return null;
            }
            catch
            {
                return "Error al Actualizar Ventas de Intelisis";
            }
            finally
            {
                conn2.Close();
            }
        } 
        */

        //crear variable para control general de conexiones
        public string[] getParametros(string Descripcion)
        {
            string[] result = new string[5];
            // zmb decidiendo eliminar linea
            // SqlConnection conn = new SqlConnection("Data Source=" + ipConfiguracion + ";Initial Catalog=" + Catalogo + ";Persist Security Info=True;User ID=sa;Password=Adminpwd20");
            SqlConnection conn = new SqlConnection(cadenaConexion);
            try
            {
                conn.Open();
                string select = "Select * From Conexiones where Descripcion='" + Descripcion + "'";
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
            }
            catch (Exception ex)
            {
                result[0] = "Error al recuperar los parametros!";
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        
        //zmb agregar doble try optimizar doble camino a el close
        // es necesario crear base general en servidor si ya se crea dentro de dispositivo movil?
        
        //pruevas
        [WebMethod]
        public cFolio[] getFolios(string numChofer)
        {
            //  Coneccion a Intelisis - napresa Real
            string[] parametros1 = getParametros(connCliente);      // nepresa Real
            SqlConnection conn1 = new SqlConnection("Data Source=" + parametros1[1] + "; Initial Catalog=" + parametros1[4] + "; Persist Security Info=True; User ID=" + parametros1[2] + "; Password=" + parametros1[3] + "");

            List<cFolio> listaMovil = new List<cFolio>();

            try
            {
                conn1.Open();
                
                // zmb cambio 300714 string query1 = "select EmbarqueMov.MovId, Venta.ID, Embarque.Agente, Embarque.Vehiculo from EmbarqueMov inner join EmbarqueD on EmbarqueMov.ID = EmbarqueD.EmbarqueMov inner join Embarque on EmbarqueMov.AsignadoID = Embarque.ID inner join Venta on EmbarqueMov.MovID = Venta.MovID where Embarque.Estatus = 'PENDIENTE' and Embarque.Agente='" + numChofer + "' and Venta.EmbarqueEstado != 'Entregado'";
                string query1 = "select EmbarqueMov.MovId, (select TOP 1 vt.Id as 'codigo de validacion' from venta as vt where vt.MovID = Venta.OrigenID), Embarque.Agente, Embarque.Vehiculo from EmbarqueMov inner join EmbarqueD on EmbarqueMov.ID = EmbarqueD.EmbarqueMov inner join Embarque on EmbarqueMov.AsignadoID = Embarque.ID inner join Venta on EmbarqueMov.MovID = Venta.MovID where Embarque.Estatus = 'PENDIENTE' and Embarque.Agente='"+ numChofer +"' and EmbarqueD.Estado != 'Entregado' and Venta.mov like 'arm%' AND Venta.Origen = 'Pedido'";
                SqlCommand cmd1 = new SqlCommand(query1, conn1);
                SqlDataReader reader1 = cmd1.ExecuteReader();

                //  Inicio de Ciclo de revision
                while (reader1.Read())
                {
                    String pin = reader1.GetValue(1) + "";
                    String folio = reader1.GetString(0);
                    String agente = reader1.GetString(2);
                    String transporte = reader1.GetString(3);
                    listaMovil.Add(new cFolio(folio, pin));

                    string[] parametros2 = getParametros("GoMobile");
                    SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + "");


                    //  insertar Entrega si no existe en GoMobile
                    try
                    {
                        conn2.Open();
                        string query2 = "Select count(*) as total from entregas where numEntrega = '" + folio + "'";
                        SqlCommand cmd2 = new SqlCommand(query2, conn2);

                        int existe = (int)cmd2.ExecuteScalar();
                        if (existe == 0)
                        {
                            string aaaa = String.Format("{0:yyyy}", DateTime.Now);
                            string mm = String.Format("{0:MM}", DateTime.Now);
                            string dd = String.Format("{0:dd}", DateTime.Now);

                            string query3 = "Insert into Entregas values ('" + folio + "','" + agente + "','"
                            + transporte + "','','1','" + aaaa + mm + dd + "');";
                            SqlCommand cmd3 = new SqlCommand(query3, conn2);
                            cmd3.ExecuteNonQuery();
                        }
                        conn2.Close();
                    }
                    catch
                    {
                        conn2.Close();
                        conn1.Close();
                        return null;
                    }
                }
                conn1.Close();
                if (listaMovil.Count > 0)
                {
                    return listaMovil.ToArray();
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                conn1.Close();
                return null;
            }
        }

        /*
        //Original
        [WebMethod]
        public cFolio[] getFolios(string numChofer)
        {
            //  Coneccion a Intelisis - napresa Real
            string[] parametros1 = getParametros(connCliente);      // nepresa Real
            SqlConnection conn1 = new SqlConnection("Data Source=" + parametros1[1] + "; Initial Catalog=" + parametros1[4] + "; Persist Security Info=True; User ID=" + parametros1[2] + "; Password=" + parametros1[3] + "");

            List<cFolio> listaMovil = new List<cFolio>();

            try
            {
                conn1.Open();
                string query1 = "select EmbarqueMov.MovId, Venta.ID, Embarque.Agente, Embarque.Vehiculo from EmbarqueMov inner join EmbarqueD on EmbarqueMov.ID = EmbarqueD.EmbarqueMov inner join Embarque on EmbarqueMov.AsignadoID = Embarque.ID inner join Venta on EmbarqueMov.MovID = Venta.MovID where Embarque.Estatus = 'PENDIENTE' and Embarque.Agente='" + numChofer + "' and Venta.EmbarqueEstado != 'Entregado'";
                SqlCommand cmd1 = new SqlCommand(query1, conn1);
                SqlDataReader reader1 = cmd1.ExecuteReader();

                //  Inicio de Ciclo de revision
                while (reader1.Read())
                {
                    String pin = reader1.GetValue(1) + "";
                    String folio = reader1.GetString(0);
                    String agente = reader1.GetString(2);
                    String transporte = reader1.GetString(3);
                    listaMovil.Add(new cFolio(folio, pin));

                    string[] parametros2 = getParametros("GoMobile");
                    SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + "");

                    try
                    {
                        conn2.Open();
                        string query2 = "Select count(*) as total from entregas where numEntrega = '" + folio + "'";
                        SqlCommand cmd2 = new SqlCommand(query2, conn2);

                        //  insertar Entrega si no existe en GoMobile
                        int existe = (int)cmd2.ExecuteScalar();
                        if (existe == 0)
                        {
                            //fecha de ingreso del servidor
                            string aaaa = String.Format("{0:yyyy}", DateTime.Now);
                            string mm = String.Format("{0:MM}", DateTime.Now);
                            string dd = String.Format("{0:dd}", DateTime.Now);

                            string query3 = "Insert into Entregas values ('" + folio + "','" + agente + "','"
                            + transporte + "','','1','" + aaaa + mm + dd + "');";
                            SqlCommand cmd3 = new SqlCommand(query3, conn2);
                            cmd3.ExecuteNonQuery();
                        }
                        conn2.Close();
                    }
                    catch
                    {
                        conn2.Close();
                        conn1.Close();
                        return null;
                    }
                }
                conn1.Close();
                if (listaMovil.Count > 0)
                {
                    return listaMovil.ToArray();
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                conn1.Close();
                return null;
            }
        }

        */
        
        [WebMethod]
        public String setEvento(String folio, int idEvento, int idExcepcion, String fecha, String hora, String latitud, String longitud, String omiteReglas)
        {
            String result = "";

            string[] prueba = getParametros("GoMobile");
            SqlConnection conn = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");
            SqlCommand cmd = new SqlCommand("spInsertaMovimientos", conn);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@p_NumEntrega", SqlDbType.Char, 10);
                cmd.Parameters["@p_NumEntrega"].Value = folio;
                cmd.Parameters.Add("@p_IdEventoProximo", SqlDbType.Char, 1);
                cmd.Parameters["@p_IdEventoProximo"].Value = idEvento.ToString();
                cmd.Parameters.Add("@p_IdExcepcion", SqlDbType.Char, 4);
                cmd.Parameters["@p_IdExcepcion"].Value = idExcepcion.ToString();
                cmd.Parameters.Add("@p_FechaRegistro", SqlDbType.Char, 8);
                cmd.Parameters["@p_FechaRegistro"].Value = fecha;
                cmd.Parameters.Add("@p_HoraRegistro", SqlDbType.Char, 6);
                cmd.Parameters["@p_HoraRegistro"].Value = hora;
                cmd.Parameters.Add("@p_Latitud", SqlDbType.Char, 11);
                cmd.Parameters["@p_Latitud"].Value = latitud;
                cmd.Parameters.Add("@p_Longitud", SqlDbType.Char, 11);
                cmd.Parameters["@p_Longitud"].Value = longitud;

                cmd.Parameters.Add("@p_omiteReglas", SqlDbType.Bit, 1);
                try
                {
                    cmd.Parameters["@p_omiteReglas"].Value = Convert.ToBoolean(omiteReglas);
                }
                catch
                {
                    cmd.Parameters["@p_omiteReglas"].Value = false;
                }

                conn.Open();
                
                // si es entregado hace los correspondientes cambios en base del cliente
                if (idEvento == 5 || idEvento == 6)
                {
                    String tmpResult = entrega(folio);
                    if (tmpResult == null)
                    {
                        result = "Entrega Exitosa";

                        cmd.Parameters.Add("@p_Mensaje", SqlDbType.VarChar, 22);
                        cmd.Parameters["@p_Mensaje"].Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        string p_mensaje = (string)cmd.Parameters["@p_Mensaje"].Value.ToString();
                        result = p_mensaje;
                    }
                    else
                    {
                        return tmpResult;
                    }
                }
                else
                {
                    cmd.Parameters.Add("@p_Mensaje", SqlDbType.VarChar, 22);
                    cmd.Parameters["@p_Mensaje"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    string p_mensaje = (string)cmd.Parameters["@p_Mensaje"].Value.ToString();
                    result = p_mensaje;
                }
            }
            catch(Exception e)
            {
                result = "Error al llamar al SP";
            }
            finally
            {

                conn.Close();
            }
            return result;
            // mover store procedure fecha y evento o estado en entregas
        }

        // ingreso de usuario para 
        [WebMethod]
        public string login(string numChofer, string imei)
        {
            string result = "";
            string[] prueba = getParametros("GoMobile");
            SqlConnection conn = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");
            try
            {
                conn.Open();
                string select = "SELECT * FROM telefonoRepartidor WHERE numChofer = '" + numChofer + "';";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if (reader["IMEI"].ToString().Equals(imei))
                    {
                        result = "1";
                    }
                    else
                    {
                        result = "El IMEI no correponde a ese numero de chofer";
                    }
                }
                else
                {
                    result = "El numero de chofer no existe";
                }
            }
            catch
            {
                result = "Error en el WS al loguearse";
            }
            finally
            {
                //cmd.Dispose(); // es necesario cerrar el comando si ya se cerro la coneccion?
                conn.Close();
            }
            return result;
        }
        
        // regresa el estado en el que se encuentra un folio
        [WebMethod]
        public String getEstado(string folio)
        {
            String result = "";
            string[] prueba = getParametros("GoMobile");
            SqlConnection conn = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");
            SqlCommand cmd = new SqlCommand("spConsultaEstado", conn);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@p_NumEntrega", SqlDbType.VarChar, 10);
                cmd.Parameters["@p_NumEntrega"].Value = folio;

                cmd.Parameters.Add("@p_Mensaje", SqlDbType.Int);
                cmd.Parameters["@p_Mensaje"].Direction = ParameterDirection.Output;
                conn.Open();

                cmd.ExecuteNonQuery();
                result = (string)cmd.Parameters["@p_Mensaje"].Value.ToString();
            }
            catch
            {
                result = "Error al llamar al SP";
            }
            finally
            {
                conn.Close();
            }
            return result;
            // mover store procedure fecha y evento o estado en entregas
        }
        
        // manejo offline de GoMobile pendiente de terminar
        [WebMethod]
        public bool[] getDatos(String jsonarray)
        {
            String result = "";
            string[] prueba = getParametros("GoMobile");
            SqlConnection conn = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");
            SqlCommand cmd = new SqlCommand("spConsultaEstado", conn);

            var project = JsonConvert.DeserializeObject<RootObject>(jsonarray);

            bool[] actualizados = new bool[project.Entrega.Count];
            int i = 0;
            foreach (var registro in project.Entrega)
            {
                result = setEvento(registro.folio, Convert.ToInt32(registro.idEvento), Convert.ToInt32(registro.idExcepcion), registro.fecha, registro.hora, registro.latitud, registro.longitud, "true");
                if (result != "UPDATE-OK")
                {
                    actualizados[i] = true;
                }
                else
                {
                    actualizados[i] = false;
                }
                i++;
            }
            return actualizados;
        }
        
        // regresa dataset para sitio web
        [WebMethod]
        public DataSet getMovOrFol(string command)
        {
            string[] prueba = getParametros("GoMobile");
            SqlConnection conn = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand(command, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            };
            return ds;
        }
        
        // manejo offline de GoMobile pendiente de terminar
        [WebMethod]
        public bool[] updatePendientes(String jsonarray)
        {
            String result = "";
            string[] prueba = getParametros("GoMobile");
            SqlConnection conn = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");
            SqlCommand cmd = new SqlCommand("spConsultaEstado", conn);

            var project = JsonConvert.DeserializeObject<RootObject>(jsonarray);

            bool[] actualizados = new bool[project.Entrega.Count];
            int i = 0;
            foreach (var registro in project.Entrega)
            {
                result = setEvento(registro.folio, Convert.ToInt32(registro.idEvento), Convert.ToInt32(registro.idExcepcion), registro.fecha, registro.hora, registro.latitud, registro.longitud, "true");
                if (result != "UPDATE-OK")
                {
                    actualizados[i] = true;
                }
                else
                {
                    actualizados[i] = false;
                }
                i++;
            }
            return actualizados;
        }

        public class Registro
        {
            public string latitud { get; set; }
            public string hora { get; set; }
            public string longitud { get; set; }
            public string idEvento { get; set; }
            public string fecha { get; set; }
            public string folio { get; set; }
            public string idExcepcion { get; set; }
        }

        public class RootObject
        {
            public List<Registro> Entrega { get; set; }
        }
    
    }
}

//{"Entrega":[{"latitud":"0.0","hora":"112130","longitud":"0.0","idEvento":"0","fecha":"20140516","folio":"AB100643","idExcepcion":"0"},{"latitud":"0.0","hora":"112444","longitud":"0.0","idEvento":"0","fecha":"20140516","folio":"AB100643","idExcepcion":"0"},{"latitud":"0.0","hora":"114629","longitud":"0.0","idEvento":"0","fecha":"20140516","folio":"AB100643","idExcepcion":"0"},{"latitud":"0.0","hora":"114859","longitud":"0.0","idEvento":"0","fecha":"20140516","folio":"AB100643","idExcepcion":"0"},{"latitud":"0.0","hora":"115006","longitud":"0.0","idEvento":"0","fecha":"20140516","folio":"AB100644","idExcepcion":"0"},{"latitud":"0.0","hora":"115038","longitud":"0.0","idEvento":"0","fecha":"20140516","folio":"AB100647","idExcepcion":"0"}]}