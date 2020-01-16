using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using System.Collections;
using System.Data.SqlClient;
using SmartDeviceProject1.servicenapresa;

namespace SmartDeviceProject1
{
    class cMetodos
    {
		public const string PROD_TERMINADO = "prodAsignado";
		public const string TESTING = "ConsolaAdmin";
		public const string CONEXION_SOLUTIA = "Solutia";
		public const string CONEXION_NAPRESA = CONEXION_SOLUTIA;
		
		// Testing Solutia - Local
        public const string IP_FROM_DB_PARAMS_SOLUTIA = "192.168.0.229";
        //public const string IP_FROM_DB_PARAMS_SOLUTIA = "172.16.1.33";
        public const string CATALOGO_PARAMS_SOLUTIA = "napresaPar";
		public const string TABLA_CATALOGO_SOLUTIA = "Conexiones";
		public const string SA_SOLUTIA = "sa";
        public const string PASSWORD_DB_SOLUTIA = "NapresaPwd20";
        //public const string PASSWORD_DB_SOLUTIA = "Solutia";
        /**/
		
        
		// Napresa 
        public const string IP_FROM_DB_PARAMS_NAPRESA = "192.168.0.229";
        //public const string IP_FROM_DB_PARAMS_NAPRESA = "172.16.1.33";
        //public const string CATALOGO_PARAMS_NAPRESA = "PNAPRESAPAR";//PRUEBAS
        public const string CATALOGO_PARAMS_NAPRESA = "NapresaPar";//PRODUCCION
        public const string TABLA_CATALOGO_NAPRESA = "Parametros";
        public const string SA_NAPRESA = "sa";
        public const string PASSWORD_DB_NAPRESA = "NapresaPwd20";
       
		
        /*
		// Datos para trabajar con la DB y WS
        public const string IP_FROM_DB_PARAMS = IP_FROM_DB_PARAMS_SOLUTIA;
        public const string CATALOGO_PARAMS = CATALOGO_PARAMS_SOLUTIA;
        public const string TABLA_CATALOGO = TABLA_CATALOGO_SOLUTIA;
        public const string CONEXION = CONEXION_SOLUTIA;
        public const string SA = SA_SOLUTIA;
        public const string PASSWORD_DB = PASSWORD_DB_SOLUTIA;
        /* */
        
        public const string IP_FROM_DB_PARAMS = IP_FROM_DB_PARAMS_NAPRESA;
		public const string CATALOGO_PARAMS = CATALOGO_PARAMS_NAPRESA;
		public const string TABLA_CATALOGO = TABLA_CATALOGO_NAPRESA;
		public const string CONEXION = CONEXION_NAPRESA;
        public const string CONEXION_INTELISIS = "Intelisis";
		public const string SA = SA_NAPRESA;
		public const string PASSWORD_DB = PASSWORD_DB_NAPRESA;
        /* */
		// Variables de parámetros de envío de correo
		public static string EMAIL_FROM = "Operador";
		public static string EMAIL_TO_DEFAULT = "desasigna.bustamante@napresa.com.mx";

        //INICIA: Funciones

        //Funcion: getParametros
        //Proposito: busca en una base de datos los parametros de conexion de SQL según la descripcion seleccionada
        public string[] getParametros(string Descripcion)
        {
            string[] result = new string[5];
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=" + IP_FROM_DB_PARAMS + ";Initial Catalog=" + CATALOGO_PARAMS + ";Persist Security Info=true;User ID=" + SA + ";Password=" + PASSWORD_DB + "");
                conn.Open();
                string select = "Select * From " + TABLA_CATALOGO + " where Descripcion='" + Descripcion + "'";

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
            catch (SqlException excep)
            {
                string errorSql;
                errorSql = excep.Message;
            }
            catch (Exception e)
            {

                string error = e.InnerException.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }

            return result;
        }

        //Funcion: fillArray
        //Proposito: recibe una consulta de multiples parametros para luego regresarlos en un array de tipo string
        public string[] fillArray(string consulta, SqlConnection conex)
        {
            try
            {
                conex.Open();
                SqlCommand comando = new SqlCommand(consulta, conex);
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    int arraySize = reader.FieldCount;
                    string[] array = new string[arraySize];
                    for (int position = 0; position < arraySize; position++)
                    {
                        array[position] = reader.GetValue(position).ToString().Trim();
                    }
                    reader.Close();
                    conex.Close();
                    return array;
                }
                else
                {
                    reader.Close();
                    conex.Close();
                    return null;
                }
            }
            catch (SqlException e)
            {
                conex.Close();
                return null;
            }
            catch (Exception e)
            {
                string error = e.Message;
                conex.Close();
                return null;
            }
        }

        //Funcion: CxEsc
        //Proposito: obtiene la cantidad actual de producto en una escuadra, ubicandola por Orden de producción y codigo de producto.
        public int CxEsc(string epc, string op, string codigo)
        {
            int qty = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                string select = "select Piezas from DetEscuadras where EPC = '" + epc + "'"; //and OrdenProduccion = '" + op + "' and CodigoProducto = '" + codigo + "'";
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
                reader.Close();
                conn.Close();

            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                qty = -1;
            }
            return qty;
        }

        //Funcion: liberaGranel
        //Proposito: esta funcion sirve para afectar el inventario de PT de Napresa en caso del producto que se trabaja a granel
        public bool liberaGranel(string epc, string ordenProd, string codigoArt, int cantidadProd)
        {
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                int actual = cantidadEscuadra(epc);
                string updateEscuadra = "UPDATE DetEscuadras SET OrdenProduccion = '" + ordenProd + "', Asignado = 1, CodigoProducto = '" + codigoArt + "', Piezas = '" + cantidadProd + "', Pendiente = " + actual + " WHERE EPC = '" + codigoArt + "'";
                if(Ejecuta(updateEscuadra,conn))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //actualiza los conteos
        public bool updateConteos(int idConteo, string EPC, int numConteo, int conteo)
        {
            bool result = true;
            string[] parametros2 = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + "");

            try
            {


                numConteo = numConteo + 1;

                string update = "";
                switch (numConteo)
                {
                   
                    case 1://CONTEO 1
                        update = "UPDATE DetalleCongelado SET conteo1 = " + conteo + ", Estatus = 1 WHERE EPC = '" + EPC +"' AND idInvCong = "+idConteo+" ";
                        Ejecuta(update,conn);
                        result = true;
                        break;
                    case 2://CONTEO 2
                        update = "UPDATE DetalleCongelado SET conteo2 = " + conteo + ", Estatus = 2 WHERE EPC = '" + EPC + "' AND idInvCong = " + idConteo + " ";
                        Ejecuta(update,conn);
                        result = true;
                        break;
                    case 3://CONTEO 3
                        update = "UPDATE DetalleCongelado SET conteo3 = " + conteo + ", Estatus = 3 WHERE EPC = '" + EPC + "' AND idInvCong = " + idConteo + " ";
                        Ejecuta(update,conn);
                        result = true;
                        break;
                    case 4: //CONTEO 4
                        update = "UPDATE DetalleCongelado SET conteo4 = " + conteo + ", Estatus = 4 WHERE EPC = '" + EPC + "' AND idInvCong = " + idConteo + " ";
                        Ejecuta(update, conn);
                        result = true;
                        break; 
                }

            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message;
                return result = false;
            }
            return result;
    
        }


        //EP: Entrada de Produccion 
        public int execEP(string [] detalle, string user)
        {
            int result = 0;
            int cantidad = Convert.ToInt32(detalle [2]);
            int id = Convert.ToInt32(detalle[5]);
            int renglon = Convert.ToInt32(detalle[9]);
            string codigo = detalle[1];
            
            string op = detalle[0];


            string[] parametros2 = getParametros("intelisis");
            SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + "");
            try
            {
                //fechaEmision = fechaEmisionEP(op);

                int renglonSub = 0;
                int max = maxRenglonSub3(id, codigo, renglon);
                renglonSub = max;

                string updateProd = "update ProdD set CantidadA = " + cantidad + " where id=" + id + " and Renglon=" + renglon + " and RenglonSub = " + renglonSub;
                Ejecuta(updateProd, conn2);

                string spUpdate = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Seleccion', 'Entrada Produccion', '" + user + "', @Estacion=99";
                Ejecuta(spUpdate, conn2);

                //AFECTAR LAS FECHAS EN Mov, Prod HE Inv
                string updateMov = "UPDATE Mov SET FechaEmision = (SELECT FechaEmision FROM Prod WHERE MovID = '" + op + "') WHERE Modulo = 'PROD' AND ID = " + getIdProd() + " ";//mover esta linea y la de abajo despues de ejecutar por primera vez el sp_Afectar 
                Ejecuta(updateMov, conn2);

                string updateFecha = "UPDATE Prod SET FechaConclusion = (SELECT FechaEmision FROM Prod WHERE MovID = '" + op + "'), UltimoCambio = (SELECT FechaEmision FROM Prod WHERE MovID = '" + op + "'), FechaEmision = (SELECT FechaEmision FROM Prod WHERE MovID = '" + op + "')  WHERE ID = " + getIdProd() + "";
                Ejecuta(updateFecha, conn2);

                string updateInv = "UPDATE Inv SET FechaEmision = (SELECT FechaEmision FROM Prod WHERE MovID = '" + op + "') WHERE ID = (SELECT TOP(1) ID FROM INV WHERE Usuario = '" + user + "' ORDER BY UltimoCambio DESC ) AND Usuario = '" + user + "'";
                Ejecuta(updateInv, conn2);
                //HASTA AQUI SE TERMINAN LAS AFECTACIONES A FECHA 
                
                string spFinal = "EXEC spAfectar 'PROD'," + getIdProd() + ", 'AFECTAR', 'Todo', Null, '" + user + "', @Estacion=99";
                Ejecuta(spFinal, conn2);

                
               
                result = 1;
            }
            catch (InvalidCastException ice)
            {
                string error2 = ice.Message;
                conn2.Close();                
                result = 0;
            }
            catch (Exception e)
            {
                conn2.Close();
                string error = e.Message;
                result = 0;
            }
            return result;
        }

       

        //JLMQ 15 NOV 2018 AGREGAR NEWID
        //Funcion: EntradaProduccionParcial
        //Proposito: afecta el inventario de PT de Napresa en caso de que la cantidad sea MENOR al total de producto avanzado
        public string EntradaProduccionParcial(string folio, string estado, string id, string renglon, Decimal cantidad, string user, string newId)
        {
            string result = "";
            string[] parametros = getParametros("Solutia");
            string[] parametros2 = getParametros("intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + "");
            try
            {
                conn.Open();
                //string updateProd = "update ProdD set CantidadA = " + cantidad + " where id=" + id + " and Renglon=" + renglon + "";
                //SqlCommand cmdUpdateProd = new SqlCommand(updateProd,conn2);
                //cmdUpdateProd.ExecuteNonQuery();
                string select = "SELECT Codigo from CatProd where id_parcialidad = '" + newId + "'";
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
                reader.Close();
                conn.Close();
                int i = 0;
                //int max = maxRenglonSub3(id, codigo); //DESCOMENTAR SI ES NECESARIO
                //i = max;
                
                Decimal total = getTotalCantPend(id, renglon, i, codigo);//POSIBLE ERROR VERIFICAR JLMQ
                if (total > 0 && cantidad > 0)
                {
                    if (cantidad >= total)
                    {
                        string centroDestino = "select Centro,CentroDestino from ProdD where id=" + id + " and Renglon=" + renglon + " and RenglonSub = " + i;
                        conn2.Open();
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
                        readerDestino.Close();
                        conn2.Close();
                        if (destino.Contains("CURAD"))//UBICACION PARA EL NUEVO FORMULARIO
                        {
                            string updateProd = "update ProdD set CantidadA = " + cantidad + " where id=" + id + " and Renglon=" + renglon + " and RenglonSub = " + i;
                            Ejecuta(updateProd, conn2);
                            string spUpdate = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Seleccion', 'Entrada Produccion', '" + user + "', @Estacion=99";
                            Ejecuta(spUpdate, conn2);
                            string spFinal = "EXEC spAfectar 'PROD'," + getIdProd() + ", 'AFECTAR', 'Todo', Null, '" + user + "', @Estacion=99";
                            Ejecuta(spFinal, conn2);
                            i++;
                            cantidad = cantidad - total;
                        }
                        else
                        {
                            string updateProd = "update ProdD set CantidadA = " + cantidad + " where id=" + id + " and Renglon=" + renglon + " and RenglonSub = " + i;
                            Ejecuta(updateProd, conn2);
                            string spUpdate = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Seleccion', 'Entrada Produccion', '" + user + "', @Estacion=99";
                            Ejecuta(spUpdate, conn2);
                            string spFinal = "EXEC spAfectar 'PROD'," + getIdProd() + ", 'AFECTAR', 'Todo', Null, '" + user + "', @Estacion=99";
                            Ejecuta(spFinal, conn2);
                            i++;
                        }                            
                    }
                    else if (total > cantidad)
                    {
                        conn2.Open();
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
                        readerDestino.Close();
                        conn2.Close();
                        if (destino.Contains("CURAD"))
                        {
                            string updateProd = "update ProdD set CantidadA = " + cantidad + " where id=" + id + " and Renglon=" + renglon + " and RenglonSub = " + i;
                            Ejecuta(updateProd, conn2);
                            string spUpdate = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Seleccion', 'Entrada Produccion', '" + user + "', @Estacion=99";
                            Ejecuta(spUpdate, conn2);
                            string spFinal = "EXEC spAfectar 'PROD'," + getIdProd() + ", 'AFECTAR', 'Todo', Null, '" + user + "', @Estacion=99";
                            Ejecuta(spFinal, conn2);
                            i = maxRenglonSub(id, codigo) + 1;
                        }
                        else if (!destino.Contains("CURAD") && !cDestino.Contains("CURAD"))
                        {
                            string updateProd = "update ProdD set CantidadA = " + cantidad + " where id=" + id + " and Renglon=" + renglon + " and RenglonSub = " + i;
                            Ejecuta(updateProd, conn2);
                            string spUpdate = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Seleccion', 'Entrada Produccion', '" + user + "', @Estacion=99";
                            Ejecuta(spUpdate, conn2);
                            string spFinal = "EXEC spAfectar 'PROD'," + getIdProd() + ", 'AFECTAR', 'Todo', Null, '" + user + "', @Estacion=99";
                            Ejecuta(spFinal, conn2);
                            i = maxRenglonSub(id, codigo) + 1;
                        }
                        
                    }
                }
                    
                
                
                
            }
            catch (Exception e)
            {
                conn.Close();
                conn2.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return result;
        }

        //Funcion: MermaAlmacen
        //Proposito: reporta las mermas en el inventario ocurridas durante el almacenamiento del producto en los patios.
        public int MermaAlmacen(int id, int original, int nvoQty, string op, string code, int renglon, string epc, string user, string sucursal)
        {
            int res = 0;
            //int renglonId = renglon / 2048;
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            string[] parametros2 = getParametros("Solutia");
            SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + "");
            try
            {
                conn.Open();
                conn2.Open();
                string movId = setMovID("Merma Almacen", sucursal);
                string insertInv = "Insert into INV (Empresa,Mov,MovID,FechaEmision,UltimoCambio,UEN,Moneda,TipoCambio,Usuario,Referencia,Observaciones,Estatus,Directo,Almacen,AlmacenTransito,OrigenTipo,Origen,OrigenID,Sucursal,SucursalOrigen,SucursalDestino)" +
        "values					('GNAP','Merma Almacen','" + movId + "',GETDATE(),GETDATE(),2,'Pesos',1,'" + user + "',(select referencia from prod where id=" + id + "),(select observaciones from prod where id=" + id + "),'PENDIENTE',1,'APT-BUS','(TRANSITO)','PROD','Entrada Produccion','" + op + "'," + sucursal + "," + sucursal + "," + sucursal + ")";
                SqlCommand cmdInv = new SqlCommand(insertInv, conn);
                cmdInv.ExecuteNonQuery();
                conn.Close();
                conn.Open();
                int idInv = getIdInv();
                string convertido = convertUnidad2Metro(code, nvoQty);
                string insertInvD = "Insert into INVD (ID,Renglon,RenglonSub,RenglonId,Renglontipo,Cantidad,CantidadPendiente,Almacen,Codigo,Articulo,Unidad,Factor,Producto,Tipo,Sucursal)" +
                            "values (" + idInv + "," + renglon + ",0," + 1 + ",'N'," + convertido + "," + convertido + ",'APT-BUS',null,'" + code + "',(select unidad from Art where Articulo='" + code + "'),(select factor from Art where Articulo='" + code + "' and Unidad = 'M2'),'" + code + "','Salida'," + sucursal + ")";
                SqlCommand cmdInvD = new SqlCommand(insertInvD, conn);
                cmdInvD.ExecuteNonQuery();
                conn.Close();
                conn.Open();
                string spAfectarMP = "EXEC spAfectar 'INV', " + idInv + ",'Afectar','Todo','Merma Almacen','" + user + "',@Estacion = 99";
                SqlCommand cmdAfectarMP = new SqlCommand(spAfectarMP, conn);
                cmdAfectarMP.ExecuteNonQuery();
                conn.Close();
                conn.Open();
                string act = "Update Inv set Estatus = 'CONCLUIDO' where id = " + idInv + "";
                SqlCommand cmdAct = new SqlCommand(act, conn);
                cmdAct.ExecuteNonQuery();
                conn.Close();
                conn.Open();
                string insertMovFlujo = "Insert into movFlujo values (" + sucursal + ",'GNAP','PROD'," + id + ",'Entrada Produccion','" + op + "','INV'," + idInv + ",'Merma Almacen','" + movId + "',0)";
                SqlCommand cmdMovFlujo = new SqlCommand(insertMovFlujo, conn);
                cmdMovFlujo.ExecuteNonQuery();
                conn.Close();
                string updateCantidad = "Update DetEscuadras set Piezas = " + nvoQty + " where EPC = '" + epc + "'";//sustituir original por nvoQty
                SqlCommand cmdCantidad = new SqlCommand(updateCantidad, conn2);
                cmdCantidad.ExecuteNonQuery();
                conn2.Close();
                conn2.Open();
                string updateEtiqueta = "Update Etiquetas_Impresas set CantidadTarima = " + original + " where EPC = '" + epc + "' and OrdenProduccion = '" + op + "' and Codigo = '" + code + "'";
                SqlCommand cmdEtiqueta = new SqlCommand(updateEtiqueta, conn2);
                cmdEtiqueta.ExecuteNonQuery();
                conn2.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                conn2.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                res = 1;
            }
            return res;
        }

        //Funcion: MermaEmbarques
        //Proposito: reporta las mermas en el inventario ocurridas durante el proceso de embarque del producto para entrega.
        public int MermaEmbarques(int id, int original, int nvoQty, string op, string code, int renglon, string epc, string user, string sucursal, string codigo)
        {
            int res = 0;
            string centrocosto = "";
            //int renglonId = renglon / 2048;
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            string[] parametros2 = getParametros("Solutia");
            SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + "");
            try
            {
                conn.Open();
                conn2.Open();
                string movId = setMovID("Merma Embarques", sucursal);
                string insertInv = "Insert into INV (Empresa,Mov,MovID,FechaEmision,UltimoCambio,UEN,Moneda,TipoCambio,Usuario,Referencia,Observaciones,Estatus,Directo,Almacen,AlmacenTransito,OrigenTipo,Origen,OrigenID,Sucursal,SucursalOrigen,SucursalDestino)values ('GNAP','Merma Embarques','" + movId + "',GETDATE(),GETDATE(),2,'Pesos',1,'" + user + "',(select referencia from prod where id=" + id + "),(select observaciones from prod where id=" + id + "),'SINAFECTAR',1,'APT-BUS','(TRANSITO)','PROD','Entrada Produccion','" + op + "'," + sucursal + "," + sucursal + "," + sucursal + ")";
                SqlCommand cmdInv = new SqlCommand(insertInv, conn);
                cmdInv.ExecuteNonQuery();
                conn.Close();
                conn.Open();
                int idInv = getIdInv();
                string convertido = convertUnidad2Metro(code, original);//era nvoQty
                string unidadProd = getUnidad(codigo);
                centrocosto = getcentrocosto(codigo);
                string insertInvD = "Insert into INVD (ID,Renglon,RenglonSub,RenglonId,Renglontipo,Cantidad,CantidadPendiente,Almacen,Codigo,Articulo,ContUso,Unidad,Factor,Producto,Tipo,Sucursal)" +
                            "values (" + idInv + "," + renglon + ",0," + 1 + ",'N'," + convertido + "," + convertido + ",'APT-BUS',null,'" + code + "','" + centrocosto + "','" + unidadProd + "',(select factor from ArtUnidad where Articulo='" + code + "' and Unidad = '" + unidadProd + "'),'" + code + "','Salida'," + sucursal + ")";
                SqlCommand cmdInvD = new SqlCommand(insertInvD, conn);
                cmdInvD.ExecuteNonQuery();
                conn.Close();
                conn.Open();
                string spAfectarMP = "EXEC spAfectar 'INV', " + idInv + ",'Afectar','Todo','Merma Embarques','" + user + "',@Estacion = 99";
                SqlCommand cmdAfectarMP = new SqlCommand(spAfectarMP, conn);
                cmdAfectarMP.ExecuteNonQuery();
                conn.Close();
                conn.Open();
                string estatusUsu = "UPDATE MovEstatusLog SET Estatus = 'CONCLUIDO' WHERE ModuloID = " + idInv + " AND mODULO = 'INV ' AND Sucursal = " + sucursal + " AND Usuario = '" + user + "'";
                SqlCommand cmdEstatusUsu = new SqlCommand(estatusUsu, conn);
                cmdEstatusUsu.ExecuteNonQuery();
                conn.Close();
                conn.Open();
                string act = "Update Inv set Estatus = 'CONCLUIDO' where id = " + idInv + "";
                SqlCommand cmdAct = new SqlCommand(act, conn);
                cmdAct.ExecuteNonQuery();
                conn.Close();
                conn.Open();
                string insertMovFlujo = "Insert into movFlujo values (" + sucursal + ",'GNAP','PROD'," + id + ",'Entrada Produccion','" + op + "','INV'," + idInv + ",'Merma Embarques','" + movId + "',0)";
                SqlCommand cmdMovFlujo = new SqlCommand(insertMovFlujo, conn);
                cmdMovFlujo.ExecuteNonQuery();
                conn.Close();
                string updateCantidad = "Update DetEscuadras set Piezas = " + nvoQty + " where EPC = '" + epc + "'";
                SqlCommand cmdCantidad = new SqlCommand(updateCantidad, conn2);
                cmdCantidad.ExecuteNonQuery();
                conn2.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                conn2.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                res = 1;
            }
            return res;
        }

        //Funcion: MermaEstiba
        //Proposito: reporta las mermas en el inventario ocurridas durante el proceso de entarimado del producto antes de que ingrese a PT
        public int MermaEstiba(int rackHuecos, int id, string codigo, string op, int renglon, string user, string sucursal)
        {
            int res = 0;
            string[] parametros = getParametros("Intelisis");
            string[] parametros2 = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + "");
            try
            {
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
                readerEstimado.Close();
                conn2.Close();
                conn2.Open();
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
                        //renglonId = renglon / 2048;
                    }
                    else
                    {
                        return res = 1;
                    }
                    readerCurado.Close();
                    conn2.Close();
                    conn2.Open();
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
                    readerLiberado.Close();
                    conn2.Close();
                    conn2.Open();
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
                    readerMermas.Close();
                    conn2.Close();
                    conn.Open();
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
                    readerRenglonSub.Close();
                    conn.Close();
                    conn.Open();
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
                            readerDestino.Close();
                            conn.Close();
                            if (!centro.Contains("CURAD") && centroDestino.Contains("CURAD"))//esta es la primera, si el centro no esta en curado, y el destino si tiene curado entonces pelas gallo papá
                            {
                                return res = 1;
                            }
                            else if (!centro.Contains("CURAD") && !centroDestino.Contains("CURAD"))//segunda, si el centro es un patio de produccion y el destino no es un cuarto de curado
                            {
                                //primero dime cuanto quedo en los racks
                                //luego actualizar CatProd
                                string updateCatProd = "UPDATE catProd set prodLiberado = " + nuevoLiberado + ", mermas = " + nuevoMermas + " where Codigo = '" + codigo + "' and OrdenProduccion = '" + op + "'";
                                Ejecuta(updateCatProd, conn2);
                                //ahora sigue que actualicemos ProdD
                                string updateProdD = "UPDATE ProdD set CantidadA = " + rackHuecos + " where id = " + id + "and RenglonSub = " + contador + "";
                                Ejecuta(updateProdD, conn);
                                //Generamos la entrada de produccion
                                string generarMerma = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Seleccion', 'Entrada Produccion', '" + user + "', @Estacion = 99";
                                Ejecuta(generarMerma, conn);
                                //afectamos la entrada de produccion generada
                                string afectarMerma = "EXEC spAfectar 'PROD', " + getIdProd() + ", 'AFECTAR', 'Todo', 'Entrada Produccion', '" + user + "', @Estacion = 99";
                                Ejecuta(afectarMerma, conn);
                                //por ultimo se insertan las mermas en Inv e InvD
                                string movId = setMovID("Merma Estiba", sucursal);
                                string insertInv = "Insert into INV (Empresa,Mov,MovID,FechaEmision,UltimoCambio,UEN,Moneda,TipoCambio,Usuario,Referencia,Observaciones,Estatus,Directo,Almacen,AlmacenTransito,OrigenTipo,Origen,OrigenID,Sucursal,SucursalOrigen,SucursalDestino)" + "values ('GNAP','Merma Estiba','" + movId + "',GETDATE(),GETDATE(),2,'Pesos',1,'" + user + "',(select referencia from prod where id=" + id + "),(select observaciones from prod where id=" + id + "),'PENDIENTE',1,'APT-BUS','(TRANSITO)','PROD','Entrada Produccion','" + op + "'," + sucursal + "," + sucursal + "," + sucursal + ")";
                                Ejecuta(insertInv, conn);
                                int idInv = getIdInv();
                                int rID = getRenglonId(renglon, contador);
                                string convertido = convertUnidad2Metro(codigo, rackHuecos);
                                string insertInvD = "Insert into INVD (ID,Renglon,RenglonSub,RenglonId,Renglontipo,Cantidad,Almacen,Codigo,Articulo,CantidadPendiente,Unidad,Factor,Producto,Tipo,Sucursal)" + "values (" + getIdInv() + "," + renglon + ",0," + rID + ",'N'," + convertido + ",'APT-BUS',null,'" + codigo + "'," + convertido + ",(select unidad from Art where Articulo='" + codigo + "'),(select factor from ArtUnidad where Articulo='" + codigo + "' and Unidad = 'M2'),'" + codigo + "','Salida'," + sucursal + ")";
                                Ejecuta(insertInvD, conn);
                                string spAfectarMP = "EXEC spAfectar 'INV', " + idInv + ",'Afectar','Todo','Merma Estiba','" + user + "',@Estacion = 99";
                                Ejecuta(spAfectarMP, conn);
                                string act = "Update Inv set Estatus = 'CONCLUIDO' where id = " + idInv + "";
                                Ejecuta(act, conn);
                                string insertMovFlujo = "Insert into movFlujo values (" + sucursal + ",'GNAP','PROD'," + id + ",'Entrada Produccion','" + op + "','INV'," + idInv + ",'Merma Estiba','" + movId + "',0)";
                                Ejecuta(insertMovFlujo, conn);
                                contador = conteoRenglones;
                                return res;
                            }
                            else if (centro.Contains("CURAD") && !centroDestino.Contains("CURADO")) //son los productos que ya se avanzaron
                            {
                                //primero dime cuanto quedo en los racks
                                //luego actualizar CatProd
                                string updateCatProd = "UPDATE catProd set prodLiberado = " + nuevoLiberado + ", mermas = " + nuevoMermas + " where Codigo = '" + codigo + "' and OrdenProduccion = '" + op + "'";
                                Ejecuta(updateCatProd, conn2);
                                //ahora sigue que actualicemos ProdD
                                string updateProdD = "UPDATE ProdD set CantidadA = " + rackHuecos + " where id = " + id + "and RenglonSub = " + contador + "";
                                Ejecuta(updateProdD, conn);
                                //Generamos la entrada de produccion
                                string generarMerma = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Seleccion', 'Entrada Produccion', '" + user + "', @Estacion = 99";
                                Ejecuta(generarMerma, conn);
                                //afectamos la entrada de produccion generada
                                string afectarMerma = "EXEC spAfectar 'PROD', " + getIdProd() + ", 'AFECTAR', 'Todo', 'Entrada Produccion', '" + user + "', @Estacion = 99";
                                Ejecuta(updateProdD, conn);
                                //por ultimo se insertan las mermas en Inv e InvD
                                string movId = setMovID("Merma Estiba", sucursal);
                                string insertInv = "Insert into INV (Empresa,Mov,MovID,FechaEmision,UltimoCambio,UEN,Moneda,TipoCambio,Usuario,Referencia,Observaciones,Estatus,Directo,Almacen,AlmacenTransito,OrigenTipo,Origen,OrigenID,Sucursal,SucursalOrigen,SucursalDestino)" + "values ('GNAP','Merma Estiba','" + movId + "',GETDATE(),GETDATE(),2,'Pesos',1,'" + user + "',(select referencia from prod where id=" + id + "),(select observaciones from prod where id=" + id + "),'PENDIENTE',1,'APT-BUS','(TRANSITO)','PROD','Entrada Produccion','" + op + "'," + sucursal + "," + sucursal + "," + sucursal + ")";
                                Ejecuta(insertInv, conn);
                                int idInv = getIdInv();
                                int rID = getRenglonId(renglon, contador);
                                string convertido = convertUnidad2Metro(codigo, rackHuecos);
                                string insertInvD = "Insert into INVD (ID,Renglon,RenglonSub,RenglonId,Renglontipo,Cantidad,Almacen,Codigo,Articulo,CantidadPendiente,Unidad,Factor,Producto,Tipo,Sucursal)" + "values (" + getIdInv() + "," + renglon + ",0," + rID + ",'N'," + convertido + ",'APT-BUS',null,'" + codigo + "'," + convertido + ",(select unidad from Art where Articulo='" + codigo + "'),(select factor from ArtUnidad where Articulo='" + codigo + "' and Unidad = 'M2'),'" + codigo + "','Salida'," + sucursal + ")";
                                Ejecuta(insertInvD, conn);
                                string spAfectarMP = "EXEC spAfectar 'INV', " + idInv + ",'Afectar','Todo','Merma Estiba','" + user + "',@Estacion = 99";
                                Ejecuta(spAfectarMP, conn);
                                string act = "Update Inv set Estatus = 'CONCLUIDO' where id = " + idInv + "";
                                Ejecuta(act, conn);
                                string insertMovFlujo = "Insert into movFlujo values (" + sucursal + ",'GNAP','PROD'," + id + ",'Entrada Produccion','" + op + "','INV'," + idInv + ",'Merma Estiba','" + movId + "',0)";
                                Ejecuta(insertMovFlujo, conn);
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
                conn.Close();
                conn2.Close();
                string error = e.Message;
                return res = 1;
            }
            return res;

        }

        //Funcion: MermaEstibaTarima
        //Proposito: reporta las mermas en el inventario ocurridas durante el proceso de embarque del producto para entrega.
        public int MermaEstibaTarima(int rackHuecos, int id, string codigo, string op, int renglon, string user, string sucursal, int actual)
        {
            int res = 0;
            string[] parametros = getParametros("Intelisis");
            string[] parametros2 = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + "");
            try
            {
                conn2.Open();
                if (rackHuecos > 0 && rackHuecos < actual)
                {
                    int cantidadReal = actual - rackHuecos;
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
                        //renglonId = renglon / 2048;
                    }
                    else
                    {
                        return res = 1;
                    }
                    readerCurado.Close();
                    conn2.Close();
                    conn2.Open();
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
                    readerLiberado.Close();
                    conn2.Close();
                    conn2.Open();
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
                    readerMermas.Close();
                    conn2.Close();
                    conn.Open();
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
                    readerRenglonSub.Close();
                    conn.Close();
                    conn.Open();
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
                            readerDestino.Close();
                            conn.Close();
                            if (!centro.Contains("CURAD") && centroDestino.Contains("CURAD"))//esta es la primera, si el centro no esta en curado, y el destino si tiene curado entonces pelas gallo papá
                            {
                                return res = 1;
                            }
                            else if (!centro.Contains("CURAD") && !centroDestino.Contains("CURAD"))//segunda, si el centro es un patio de produccion y el destino no es un cuarto de curado
                            {
                                //primero dime cuanto quedo en los racks
                                //luego actualizar CatProd
                                string updateCatProd = "UPDATE catProd set prodLiberado = " + nuevoLiberado + ", mermas = " + nuevoMermas + " where Codigo = '" + codigo + "' and OrdenProduccion = '" + op + "'";
                                Ejecuta(updateCatProd, conn2);
                                //ahora sigue que actualicemos ProdD
                                string updateProdD = "UPDATE ProdD set CantidadA = " + rackHuecos + " where id = " + id + "and RenglonSub = " + contador + "";
                                Ejecuta(updateProdD, conn);
                                //Generamos la entrada de produccion
                                string generarMerma = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Seleccion', 'Entrada Produccion', '" + user + "', @Estacion = 99";
                                Ejecuta(generarMerma, conn);
                                //afectamos la entrada de produccion generada
                                string afectarMerma = "EXEC spAfectar 'PROD', " + getIdProd() + ", 'AFECTAR', 'Todo', 'Entrada Produccion', '" + user + "', @Estacion = 99";
                                Ejecuta(afectarMerma, conn);
                                //por ultimo se insertan las mermas en Inv e InvD
                                string movId = setMovID("Merma Estiba", sucursal);
                                string insertInv = "Insert into INV (Empresa,Mov,MovID,FechaEmision,UltimoCambio,UEN,Moneda,TipoCambio,Usuario,Referencia,Observaciones,Estatus,Directo,Almacen,AlmacenTransito,OrigenTipo,Origen,OrigenID,Sucursal,SucursalOrigen,SucursalDestino)" + "values ('GNAP','Merma Estiba','" + movId + "',GETDATE(),GETDATE(),2,'Pesos',1,'" + user + "',(select referencia from prod where id=" + id + "),(select observaciones from prod where id=" + id + "),'PENDIENTE',1,'APT-BUS','(TRANSITO)','PROD','Entrada Produccion','" + op + "'," + sucursal + "," + sucursal + "," + sucursal + ")";
                                Ejecuta(insertInv, conn);
                                int idInv = getIdInv();
                                int rID = getRenglonId(renglon, contador);
                                string convertido = convertUnidad2Metro(codigo, rackHuecos);
                                string insertInvD = "Insert into INVD (ID,Renglon,RenglonSub,RenglonId,Renglontipo,Cantidad,Almacen,Codigo,Articulo,CantidadPendiente,Unidad,Factor,Producto,Tipo,Sucursal)" + "values (" + getIdInv() + "," + renglon + ",0," + rID + ",'N'," + convertido + ",'APT-BUS',null,'" + codigo + "'," + convertido + ",(select unidad from Art where Articulo='" + codigo + "'),(select factor from ArtUnidad where Articulo='" + codigo + "' and Unidad = 'M2'),'" + codigo + "','Salida'," + sucursal + ")";
                                Ejecuta(insertInvD, conn);
                                string spAfectarMP = "EXEC spAfectar 'INV', " + idInv + ",'Afectar','Todo','Merma Estiba','" + user + "',@Estacion = 99";
                                Ejecuta(spAfectarMP, conn);
                                string act = "Update Inv set Estatus = 'CONCLUIDO' where id = " + idInv + "";
                                Ejecuta(act, conn);
                                string insertMovFlujo = "Insert into movFlujo values (" + sucursal + ",'GNAP','PROD'," + id + ",'Entrada Produccion','" + op + "','INV'," + idInv + ",'Merma Estiba','" + movId + "',0)";
                                Ejecuta(insertMovFlujo, conn);
                                contador = conteoRenglones;
                                return res;
                            }
                            else if (centro.Contains("CURAD") && !centroDestino.Contains("CURADO")) //son los productos que ya se avanzaron
                            {
                                //primero dime cuanto quedo en los racks
                                //luego actualizar CatProd
                                string updateCatProd = "UPDATE catProd set prodLiberado = " + nuevoLiberado + ", mermas = " + nuevoMermas + " where Codigo = '" + codigo + "' and OrdenProduccion = '" + op + "'";
                                Ejecuta(updateCatProd, conn2);
                                //ahora sigue que actualicemos ProdD
                                string updateProdD = "UPDATE ProdD set CantidadA = " + rackHuecos + " where id = " + id + "and RenglonSub = " + contador + "";
                                Ejecuta(updateProdD, conn);
                                //Generamos la entrada de produccion
                                string generarMerma = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Seleccion', 'Entrada Produccion', '" + user + "', @Estacion = 99";
                                Ejecuta(generarMerma, conn);
                                //afectamos la entrada de produccion generada
                                string afectarMerma = "EXEC spAfectar 'PROD', " + getIdProd() + ", 'AFECTAR', 'Todo', 'Entrada Produccion', '" + user + "', @Estacion = 99";
                                Ejecuta(updateProdD, conn);
                                //por ultimo se insertan las mermas en Inv e InvD
                                string movId = setMovID("Merma Estiba", sucursal);
                                string insertInv = "Insert into INV (Empresa,Mov,MovID,FechaEmision,UltimoCambio,UEN,Moneda,TipoCambio,Usuario,Referencia,Observaciones,Estatus,Directo,Almacen,AlmacenTransito,OrigenTipo,Origen,OrigenID,Sucursal,SucursalOrigen,SucursalDestino)" + "values ('GNAP','Merma Estiba','" + movId + "',GETDATE(),GETDATE(),2,'Pesos',1,'" + user + "',(select referencia from prod where id=" + id + "),(select observaciones from prod where id=" + id + "),'PENDIENTE',1,'APT-BUS','(TRANSITO)','PROD','Entrada Produccion','" + op + "'," + sucursal + "," + sucursal + "," + sucursal + ")";
                                Ejecuta(insertInv, conn);
                                int idInv = getIdInv();
                                int rID = getRenglonId(renglon, contador);
                                string convertido = convertUnidad2Metro(codigo, rackHuecos);
                                string insertInvD = "Insert into INVD (ID,Renglon,RenglonSub,RenglonId,Renglontipo,Cantidad,Almacen,Codigo,Articulo,CantidadPendiente,Unidad,Factor,Producto,Tipo,Sucursal)" + "values (" + getIdInv() + "," + renglon + ",0," + rID + ",'N'," + convertido + ",'APT-BUS',null,'" + codigo + "'," + convertido + ",(select unidad from Art where Articulo='" + codigo + "'),(select factor from ArtUnidad where Articulo='" + codigo + "' and Unidad = 'M2'),'" + codigo + "','Salida'," + sucursal + ")";
                                Ejecuta(insertInvD, conn);
                                string spAfectarMP = "EXEC spAfectar 'INV', " + idInv + ",'Afectar','Todo','Merma Estiba','" + user + "',@Estacion = 99";
                                Ejecuta(spAfectarMP, conn);
                                string act = "Update Inv set Estatus = 'CONCLUIDO' where id = " + idInv + "";
                                Ejecuta(act, conn);
                                string insertMovFlujo = "Insert into movFlujo values (" + sucursal + ",'GNAP','PROD'," + id + ",'Entrada Produccion','" + op + "','INV'," + idInv + ",'Merma Estiba','" + movId + "',0)";
                                Ejecuta(insertMovFlujo, conn);
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
                else if (rackHuecos > actual)
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
                conn.Close();
                conn2.Close();
                string error = e.Message;
                return res = 1;
            }
            return res;

        }

        //Funcion: PiezasVentana
        //Proposito: toma de base de datos las piezas por VENTANA que puede haber en un rack.
        public int PiezasVentana(string epc)
        {
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                string getCodigo = "select CodigoProducto from DetRProd where EPC = '" + epc + "'";
                SqlCommand cmdCodigo = new SqlCommand(getCodigo, conn);
                SqlDataReader readCodigo = cmdCodigo.ExecuteReader();
                string codigo = "";
                if (readCodigo.Read())
                {
                    codigo = readCodigo.GetValue(0).ToString();
                    readCodigo.Close();
                    conn.Close();
                    conn.Open();
                    string getPxT = "select PxT from catArt where Clave = '" + codigo + "'";
                    SqlCommand cmdPxT = new SqlCommand(getPxT, conn);
                    SqlDataReader readPxT = cmdPxT.ExecuteReader();
                    int PxT = 0;
                    if (readPxT.Read())
                    {
                        PxT = int.Parse(readPxT.GetValue(0).ToString());
                        conn.Close();
                        return PxT;
                    }
                    else
                    {
                        conn.Close();
                        return -1;
                    }
                }
                else
                {
                    conn.Close();
                    return -1;
                }

            }
            catch (Exception e)
            {
                conn.Close();
                return -1;
            }
        }

        //Funcion: TarimasEmpresa
        //Proposito: regresa la cantidad de etiquetas que se han impreso para un determinado producto de una determinada orden de produccion
        public int TarimasImpresas(string op, string cp)
        {
            int res = -1;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            //primero se obtiene la cantidad de producto por ventana
            try
            {
                conn.Open();
                string select = "Select count(*) from Etiquetas_Impresas where OrdenProduccion = '" + op + "' and Codigo = '" + cp + "'";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    res = reader.GetInt32(0);
                    res = res + 1;
                    conn.Close();
                }
                else
                {
                    conn.Close();
                    return res;
                }

            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return res = -1;
            }
            return res;
        }

        //Funcion: asignadaOP
        //Proposito: regresa la cantidad de racks que han sido asignados a una orden de produccion especifica.
        public int asignadaOP(string OP)
        {
            int result = 0;
            int count = 0;
            string[] parametros	= getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
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
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                result = 1;
            }
            return result;
        }

        //Funcion: Ejecuta
        //Proposito: ejecuta un query de SQL en la base de datos que se le envie con el parametro conn
        public bool Ejecuta(string Cadena, SqlConnection con)
        {
            string error;
            try
            {
                con.Open();
                SqlCommand comand = new SqlCommand(Cadena, con);
                comand.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception excepcion)
            {
                error = excepcion.Message;
                return false;
            }
            return true;

        }

        //Funcion: avanzarEstado
        //Proposito: afecta # cantidad de producto con un movimiento que en intelisis se conoce como AVANCE
        public string avanzarEstado(string folio, string estado, string id, string renglon, int cantidad, string user, string newId)
        {
            string result = "Cambio Exitoso";
            string[] parametros = getParametros("Solutia");
            string[] parametros2 = getParametros("intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + "");

            try
            {
                conn.Open();
                string select = "SELECT Codigo from CatProd where id_parcialidad = '" + newId + "'";
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
                reader.Close();
                conn.Close();
                conn.Open();
                int i = 0;
                int qtyAsignado = 0;
                int max = maxRenglonSub2(id, codigo,cantidad);
                i = max;
                string selectAsignado = "SELECT prodCurado from CatProd where id_parcialidad = '" + newId + "'";
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
                readerAsignado.Close();
                conn.Close();
                int qtyNvo = qtyAsignado + cantidad;
                int qtyCur = currentValue(folio, estado, id, renglon, newId);
                qtyCur = qtyCur - cantidad;
                do
                {
                    Decimal total = getTotalCantPend(id, renglon, i, codigo);
                    if (total > 0 && cantidad > 0)
                    {
                        if (cantidad >= total)
                        {
                            //string centroDestino = "SELECT Centro,CentroDestino from ProdD where id = " + id + " and Renglon = " + renglon + " and RenglonSub = " + i;
                            //conn2.Open();
                            //SqlCommand cmdDestino = new SqlCommand(centroDestino, conn2);
                            //SqlDataReader readerDestino = cmdDestino.ExecuteReader();
                            //string[] destino = new string[2];
                            //if (readerDestino.Read())
                            //{
                            //    destino[0] = readerDestino.GetValue(0).ToString();
                            //    destino[1] = readerDestino.GetValue(1).ToString();
                            //}
                            //else
                            //{
                            //    i++;
                            //}
                            //readerDestino.Close();
                            //conn2.Close();
                            //if (!destino[0].Contains("CURAD") && destino[1].Contains("CURAD"))
                            //{
                                string updateProd = "update ProdD set CantidadA = " + total + " where id = " + id + " and Renglon = " + renglon + " and RenglonSub = " + i;
                                Ejecuta(updateProd, conn2);
                                string spUpdate = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Seleccion', 'Avance', '" + user + "', @Estacion=99";
                                Ejecuta(spUpdate, conn2);
                                string spFinal = "EXEC spAfectar 'PROD', " + getIdProd() + ", 'AFECTAR', 'Todo', NULL, '" + user + "', @Estacion=99";
                                Ejecuta(spFinal, conn2);
                                string updateParcialidad = "UPDATE catProd set prodCurado = " + qtyNvo + ", Estatus = 'CURADO', prodAsignado = " + qtyCur + " where OrdenProduccion ='" + folio + "' and renglon = " + renglon + " and id_parcialidad = '" + newId + "'";
                                Ejecuta(updateParcialidad, conn);
                                result = "Cambio Exitoso";
                                break;
                            //}
                            //else if (!destino[0].Contains("CURAD") && !destino[1].Contains("CURAD"))
                            //{
                                string updateParcialidad2 = "UPDATE catProd set prodCurado = " + qtyNvo + ", Estatus = 'CURADO', prodAsignado = " + qtyCur + " where OrdenProduccion ='" + folio + "' and renglon = " + renglon + " and id_parcialidad = '" + newId + "'";
                                Ejecuta(updateParcialidad2, conn);
                                result = "Cambio Exitoso";
                                break;
                            //}
                            //else
                            //{
                            //    i++;
                            //}
                        }
                        else if (total > cantidad)
                        {
                            //conn2.Open();
                            //string centroDestino = "SELECT Centro,CentroDestino from ProdD where id = " + id + " and Renglon = " + renglon + " and RenglonSub = " + i;
                            //SqlCommand cmdDestino = new SqlCommand(centroDestino, conn2);
                            //SqlDataReader readerDestino = cmdDestino.ExecuteReader();
                            //string[] destino = new string[2];
                            //if (readerDestino.Read())
                            //{
                            //    destino[0] = readerDestino.GetValue(0).ToString();
                            //    destino[1] = readerDestino.GetValue(1).ToString();
                            //}
                            //else
                            //{
                            //    i++;
                            //}
                            //readerDestino.Close();
                            //conn2.Close();
                            //if (!destino[0].Contains("CURAD") && destino[1].Contains("CURAD"))
                            //{

                                string updateProd = "update ProdD set CantidadA = " + cantidad + " where id = " + id + " and Renglon = " + renglon + " and RenglonSub = " + i;
                                Ejecuta(updateProd, conn2);
                                string spUpdate = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Seleccion', 'Avance', '" + user + "', @Estacion=99";
                                Ejecuta(spUpdate, conn2);
                                string spFinal = "EXEC spAfectar 'PROD', " + getIdProd() + ", 'AFECTAR', 'Todo', NULL, '" + user + "', @Estacion=99";
                                Ejecuta(spFinal, conn2);
                                string updateParcialidad = "UPDATE catProd set prodCurado = " + qtyNvo + ", Estatus = 'CURADO', prodAsignado = " + qtyCur + " where OrdenProduccion ='" + folio + "' and renglon = " + renglon + " and id_parcialidad = '" + newId + "'";
                                Ejecuta(updateParcialidad, conn);
                                result = "Cambio Exitoso";
                                break;
                            //}
                            //else if (!destino[0].Contains("CURAD") && !destino[1].Contains("CURAD"))
                            //{
                            //    string updateParcialidad = "UPDATE catProd set prodCurado = " + qtyNvo + ", Estatus = 'CURADO', prodAsignado = " + qtyCur + " where OrdenProduccion ='" + folio + "' and renglon = " + renglon + " and id_parcialidad = '" + newId + "'";
                            //    Ejecuta(updateParcialidad, conn);
                            //    result = "Cambio Exitoso";
                            //    break;
                            //}
                            //else
                            //{
                            //    i++;
                            //}
                        }
                    }
                    else
                    {
                        string updateParcialidad = "UPDATE catProd set prodCurado = " + qtyNvo + ", Estatus = 'CURADO', prodAsignado = " + qtyCur + " where OrdenProduccion ='" + folio + "' and renglon = " + renglon + " and id_parcialidad = '" + newId + "'";
                        Ejecuta(updateParcialidad, conn);
                        result = "Cambio Exitoso";
                        break;
                    }
                } while (i <= max);
            }
            catch (SqlException se)
            {
                SqlError err = se.Errors[0];
                conn.Close();
                conn2.Close();
                result = se.Errors[0].Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            catch (Exception e)
            {
                conn.Close();
                conn2.Close();
                result = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return result;
        }


        //Funcion: avanzarEstadoHuecos
        //Proposito: afecta # cantidad de producto con un movimiento que en intelisis se conoce como AVANCE
        public string avanzarEstadoHuecos(string folio, string estado, string id, string renglon, int cantidad, string[] user, int pzasTotalParcialidad, string newId)
        {
            string result = "Cambio Exitoso";
            string[] parametros = getParametros("Solutia");
            string[] parametros2 = getParametros("intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + "");

            try
            {
                conn.Open();
                string select = "SELECT Codigo from CatProd where id_parcialidad = '" + newId + "' ";
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
                reader.Close();
                conn.Close();
                conn.Open();
                int i = 0;
                int qtyAsignado = 0;
                int max = maxRenglonSub(id, codigo);
                i = max;
                string selectAsignado = "SELECT prodCurado from CatProd where id_parcialidad = '" + newId + "'";//SE OMITE ESTO  and prodAsignado = " + pzasTotalParcialidad + " JLMQ 18SEP2018
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
                readerAsignado.Close();
                conn.Close();
                int qtyNvo = qtyAsignado + cantidad;
                int qtyCur = currentValueHuecos(estado, newId);//VERIFICAR SI ESTA MMADA SIRVE
                qtyCur = qtyCur - cantidad;
                do
                {
                    Decimal total = getTotalCantPend(id, renglon, i, codigo);
                    if (total > 0 && cantidad > 0)
                    {
                        if (cantidad >= total)
                        {
                            string centroDestino = "SELECT Centro,CentroDestino from ProdD where id = " + id + " and Renglon = " + renglon + " and RenglonSub = " + i;
                            conn2.Open();
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
                            }
                            readerDestino.Close();
                            conn2.Close();
                            if (!destino[0].Contains("CURAD") && destino[1].Contains("CURAD"))
                            {
                                string updateProd = "update ProdD set CantidadA = " + total + " where id = " + id + " and Renglon = " + renglon + " and RenglonSub = " + i;
                                Ejecuta(updateProd, conn2);
                                string spUpdate = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Seleccion', 'Avance', '" + user + "', @Estacion=99";
                                Ejecuta(spUpdate, conn2);
                                string spFinal = "EXEC spAfectar 'PROD', " + getIdProd() + ", 'AFECTAR', 'Todo', NULL, '" + user + "', @Estacion=99";
                                Ejecuta(spFinal, conn2);
                                string updateParcialidad = "UPDATE catProd set prodCurado = " + qtyNvo + ", Estatus = 'CURADO', prodAsignado = " + qtyCur + " where id_parcialidad = '" + newId + "'";
                                Ejecuta(updateParcialidad, conn);
                                result = "Cambio Exitoso";
                                break;
                            }
                            else if (!destino[0].Contains("CURAD") && !destino[1].Contains("CURAD"))
                            {
                                string updateParcialidad = "UPDATE catProd set prodCurado = " + qtyNvo + ", Estatus = 'CURADO', prodAsignado = " + qtyCur + " where id_parcialidad = '" + newId + "'";
                                Ejecuta(updateParcialidad, conn);
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
                            conn2.Open();
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
                            }
                            readerDestino.Close();
                            conn2.Close();
                            if (!destino[0].Contains("CURAD") && destino[1].Contains("CURAD"))
                            {

                                string updateProd = "update ProdD set CantidadA = " + cantidad + " where id = " + id + " and Renglon = " + renglon + " and RenglonSub = " + i;
                                Ejecuta(updateProd, conn2);
                                string spUpdate = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Seleccion', 'Avance', '" + user + "', @Estacion=99";
                                Ejecuta(spUpdate, conn2);
                                string spFinal = "EXEC spAfectar 'PROD', " + getIdProd() + ", 'AFECTAR', 'Todo', NULL, '" + user + "', @Estacion=99";
                                Ejecuta(spFinal, conn2);
                                string updateParcialidad = "UPDATE catProd set prodCurado = " + qtyNvo + ", Estatus = 'CURADO', prodAsignado = " + qtyCur + " where id_parcialidad = '" + newId + "'";
                                Ejecuta(updateParcialidad, conn);
                                result = "Cambio Exitoso";
                                break;
                            }
                            else if (!destino[0].Contains("CURAD") && !destino[1].Contains("CURAD"))
                            {
                                string updateParcialidad = "UPDATE catProd set prodCurado = " + qtyNvo + ", Estatus = 'CURADO', prodAsignado = " + qtyCur + " where OrdenProduccion ='" + folio + "' and renglon = " + renglon + " and id_parcialidad = '" + newId + "'";
                                Ejecuta(updateParcialidad, conn);
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
                        string updateParcialidad = "UPDATE catProd set prodCurado = " + qtyNvo + ", Estatus = 'CURADO', prodAsignado = " + qtyCur + " where OrdenProduccion ='" + folio + "' and renglon = " + renglon + " and id_parcialidad = '" + newId + "'";//SE OMITE  and prodAsignado = " + pzasTotalParcialidad + " JLMQ SEPT 2018
                        Ejecuta(updateParcialidad, conn);
                        result = "Cambio Exitoso";
                        break;
                    }
                } while (i <= max);
            }
            catch (SqlException se)
            {
                SqlError err = se.Errors[0];
                conn.Close();
                conn2.Close();
                result = se.Errors[0].Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            catch (Exception e)
            {
                conn.Close();
                conn2.Close();
                result = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return result;
        }


        //Funcion: avanzarEstadoCurado
        //Proposito: afecta # cantidad de producto con un movimiento que en intelisis se conoce como AVANCE
        public string avanzarEstadoCurado(string folio, string estado, string id, string renglon, int cantidad, string user)
        {
            string result = "";
            string[] parametros = getParametros("Solutia");
            string[] parametros2 = getParametros("intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + "");
            try
            {
                conn.Open();
                string select = "SELECT Codigo from CatProd where OrdenProduccion ='" + folio + "' and renglon = '" + renglon + "'";
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
                conn.Close();
                int i = 0;
                int qtyAsignado = 0;
                int max = maxRenglonSub(id, codigo);
                string selectAsignado = "SELECT prodAsignado from CatProd where OrdenProduccion ='" + folio + "' and renglon = " + renglon + "";
                conn.Open();
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
                conn.Close();
                int qtyNvo = qtyAsignado - cantidad;
                int qtyCur = 2;//currentValue(folio, estado, id, renglon);//JLMQ 22NOV2018 ESTE METODO NO LO LLAMA NADIE, REVISAR
                qtyCur = qtyCur + cantidad;
                do
                {
                    Decimal total = getTotalCantPend(id, renglon, i, codigo);
                    if (total > 0 && cantidad > 0)
                    {
                        if (cantidad >= total)
                        {
                            string centroDestino = "SELECT Centro,CentroDestino from ProdD where id = " + id + " and Renglon = " + renglon + " and RenglonSub = " + i;
                            conn2.Open();
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
                            }
                            conn2.Close();
                            if (!destino[0].Contains("CURAD") && destino[1].Contains("CURAD"))
                            {
                                string updateProd = "update ProdD set CantidadA = " + total + " where id = " + id + " and Renglon = " + renglon + " and RenglonSub = " + i;
                                conn2.Open();
                                SqlCommand cmdUpdateProd = new SqlCommand(updateProd, conn2);
                                cmdUpdateProd.ExecuteNonQuery();
                                conn.Close();
                                string spUpdate = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Seleccion', 'Avance', '" + user + "', @Estacion=99";
                                conn2.Open();
                                SqlCommand cmd2 = new SqlCommand(spUpdate, conn2);
                                cmd2.ExecuteNonQuery();
                                conn2.Close();
                                string spFinal = "EXEC spAfectar 'PROD', " + getIdProd() + ", 'AFECTAR', 'Todo', NULL, '" + user + "', @Estacion=99";
                                conn2.Open();
                                SqlCommand cmd3 = new SqlCommand(spFinal, conn2);
                                cmd3.ExecuteNonQuery();
                                conn2.Close();
                                string updateParcialidad = "UPDATE catProd set prodAsignado = " + qtyNvo + ", Estatus = 'CURADO', prodCurado = " + qtyCur + " where OrdenProduccion ='" + folio + "' and renglon = " + renglon + "";
                                conn.Open();
                                SqlCommand cmdParcialidad = new SqlCommand(updateParcialidad, conn);
                                cmdParcialidad.ExecuteNonQuery();
                                conn.Close();
                                result = "Cambio Exitoso";
                                break;
                            }
                            else if (!destino[0].Contains("CURAD") && !destino[1].Contains("CURAD"))
                            {
                                string updateParcialidad = "UPDATE catProd set prodAsignado = " + qtyNvo + ", Estatus = 'CURADO', prodCurado = " + qtyCur + " where OrdenProduccion ='" + folio + "' and renglon = " + renglon + "";
                                conn.Open();
                                SqlCommand cmdParcialidad = new SqlCommand(updateParcialidad, conn);
                                cmdParcialidad.ExecuteNonQuery();
                                conn.Close();
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
                            conn2.Open();
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
                            }
                            conn2.Close();
                            if (!destino[0].Contains("CURAD") && destino[1].Contains("CURAD"))
                            {
                                string updateProd = "update ProdD set CantidadA = " + cantidad + " where id = " + id + " and Renglon = " + renglon + " and RenglonSub = " + i;
                                conn2.Open();
                                SqlCommand cmdUpdateProd = new SqlCommand(updateProd, conn2);
                                cmdUpdateProd.ExecuteNonQuery();
                                conn2.Close();
                                string spUpdate = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Seleccion', 'Avance', '" + user + "', @Estacion=99";
                                conn2.Open();
                                SqlCommand cmd2 = new SqlCommand(spUpdate, conn2);
                                cmd2.ExecuteNonQuery();
                                conn2.Close();
                                string spFinal = "EXEC spAfectar 'PROD', " + getIdProd() + ", 'AFECTAR', 'Todo', NULL, '" + user + "', @Estacion=99";
                                conn2.Open();
                                SqlCommand cmd3 = new SqlCommand(spFinal, conn2);
                                cmd3.ExecuteNonQuery();
                                string updateParcialidad = "UPDATE catProd set prodAsignado = " + qtyNvo + ", Estatus = 'CURADO', prodCurado = " + qtyCur + " where OrdenProduccion ='" + folio + "' and renglon = " + renglon + "";
                                conn.Open();
                                SqlCommand cmdParcialidad = new SqlCommand(updateParcialidad, conn);
                                cmdParcialidad.ExecuteNonQuery();
                                conn.Close();
                                result = "Cambio Exitoso";
                                break;
                            }
                            else if (!destino[0].Contains("CURAD") && !destino[1].Contains("CURAD"))
                            {
                                string updateParcialidad = "UPDATE catProd set prodAsignado = " + qtyNvo + ", Estatus = 'CURADO', prodCurado = " + qtyCur + " where OrdenProduccion ='" + folio + "' and renglon = " + renglon + "";
                                conn.Open();
                                SqlCommand cmdParcialidad = new SqlCommand(updateParcialidad, conn);
                                cmdParcialidad.ExecuteNonQuery();
                                conn.Close();
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
            }
            catch (Exception e)
            {
                conn2.Close();
                conn.Close();
                result = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return result;
        }

        //Funcion: calculaRacks
        //Proposito: calcula cuantos racks se van a necesitar para cubrir cierto pedido de cierto producto
        public Decimal calculaRacks(string codigo, int tipoRack, Decimal pedido)
        {
            Decimal cantidad = 0;
            Decimal res = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
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
                cantidad = Decimal.Round(cantidad, 0);
                res = Convert.ToInt32(cantidad);
            }
            conn.Close();
            return res;
        }

        //Funcion: calculaTarima
        //Proposito: calcula cuantas tarimas se van a necesitar para cubrir cierto pedido de cierto producto
        public int calculaTarima(string codigo, int pedido)
        {
            int cantidad = 0;
            Decimal Tarimas = 0;
            int res = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            //primero se obtiene la cantidad de producto por ventana
            try
            {
                conn.Open();
                string select = "Select PZxT from catArt where Clave = '" + codigo + "'";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    cantidad = int.Parse(reader.GetValue(0).ToString());
                }
                if (cantidad == 0)
                {
                    return res;
                }
                else
                {
                    cantidad = pedido / cantidad;
                    Tarimas = Decimal.Round(cantidad, 0);
                    res = Convert.ToInt32(Tarimas + 1);
                }
                conn.Close();

            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return res;
        }

        //Funcion: cantidad
        //Proposito: regresa la cantidad final de piezas que debe tener un rack segun el producto que se vaya a producir.
        public Decimal cantidad(int tipoRack, string codigo)
        {
            Decimal cantidad = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
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
                            cantidad = cantidad * 28; break;
                        case 2:
                            cantidad = cantidad * 36; break;
                        case 3:
                            cantidad = cantidad * 28; break;
                        case 4:
                            cantidad = cantidad * 42; break;
                        default: break;
                    }
                    conn.Close();

                }
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return cantidad;
        }

        //Funcion: checkAsignados
        //Proposito: cuenta cuantos racks fueron asignados a una orden de produccion
        public int[] checkAsignados(string folio, string codigo, int renglon, string newId)
        {
            int[] count = new int[2];
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                string select = "Select count(IdDRProd) from DetRProd where OrdenProduccion = '" + folio + "' and CodigoProducto= '" + codigo + "' and Renglon = " + renglon + " and newId_Parcialidad = '"+ newId +"'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    count[1] = reader.GetInt32(0);
                }
                if (count[1] > 0)
                    count[0] = 0;
                else
                    count[0] = 1;
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return count;
        }

        //Funcion: checkCantidad
        //Proposito: regresa cuantas piezas tiene como maximo un rack segun el producto.
        public Decimal checkCantidad(int tipoRack, string codigo, Decimal actual)
        {
            Decimal diferencia = 0;
            Decimal cantidad = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
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
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return diferencia;
        }

        //Funcion: checkComplete
        //Proposito: revisa que la orden de produccion no este como CONCLUIDO en Intelisis
        public int checkComplete(string folio)
        {
            int res = 0;
            string status = "";
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                string select = "Select Estatus from ProdPendienteD where MovId ='" + folio + "'";
                SqlCommand cmd = new SqlCommand(select, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    status = reader.GetString(0);
                }
                else
                {
                    res = 1;
                    return res;
                }
                conn.Close();
                if (status != "CONCLUIDO")
                {
                    res = 0;
                }
                else
                {
                    res = 1;
                }
            }

            catch (Exception e)
            {
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                res = 1;
            }

            return res;
        }

        //Funcion: checkRacks
        //Proposito: valida que todos los racks asignados a la orden de produccion ya hayan sido bautizados.
        public int checkRacks(string folio, string codprod, int renglon, string newId)
        {
            int res = 0;
            int cantidadAsignados = 0;
            int cantidadLeidos = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            SqlConnection conn2 = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                string select = "Select count(IdDRProd) from DetRProd where OrdenProduccion = '" + folio + "' and CodigoProducto= '" + codprod + "' and Renglon = " + renglon + " and newId_Parcialidad = '"+ newId +"' ";
                string select2 = "Select count(IdDRProd) from DetRProd where ContadoProd = 0 and ContadoCurado = 0 and OrdenProduccion='" + folio + "'  and CodigoProducto= '" + codprod + "' and Renglon = " + renglon + " and newId_Parcialidad = '" + newId + "' ";
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
                if (cantidadAsignados == cantidadLeidos)
                {
                    res = 0;
                }
                else
                {
                    res = 1;
                }
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }

            return res;
        }

        //Funcion: checkTransfer
        //Proposito: revisa que todas las ordenes de transferencia de una orden de produccion esten concluidas
        public int checkTransfer(string folio)
        {
            int res = 0;
            int cantidad = 0;
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                string select = "Select count(Id) From Inv Where  id In (Select Did From MovFlujo Where Omodulo = 'PROD' And OMovID = '" + folio + "'  And Dmov = 'Orden Transferencia') and Estatus != 'CONCLUIDO'";
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
                if (cantidad == 0)
                {
                    res = 0;
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
            }

            return res;
        }

        //Funcion: clearRack
        //Proposito: libera los racks correspondientes a cierta partida (renglon) de una orden de produccion
        public int clearRack(string OP, string renglon, string newId)//JLMQ 21NOV2018 AQUI AGREGAR NEWID AL QUERY
        {
            int res = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                string update = "update DetRProd SET OrdenProduccion = NULL, Estado = 0, Verificado = 0, CantidadEstimada = 0, CantidadReal = 0, CodigoProducto = NULL, ContadoProd = 0, ContadoCurado = 0, Renglon = NULL, cantidadParcialidad = 0, newId_Parcialidad = NULL where OrdenProduccion = '" + OP + "' and ContadoProd = 1 and ContadoCurado = 1 and Renglon = " + renglon + "";
                //string delete = "delete from Etiquetas_Impresas where EPC = '" + EPC + "'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(update, conn);
                //SqlCommand cmd2 = new SqlCommand(delete, conn);
                cmd.ExecuteNonQuery();
                //cmd2.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                res = 1;
            }
            return res;
        }

        //Funcion: completaTransfer
        //Proposito: concluye una orden de transferencia pendiente en INTELISIS
        public int completaTransfer(string id, string user)
        {
            int res = 0;
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                string spAfectar = "exec spAfectar 'INV', " + id + ", 'GENERAR', 'Todo', 'Transferencia', '" + user + "'";
                SqlCommand cmd = new SqlCommand(spAfectar, conn);
                cmd.ExecuteNonQuery();
                string Transferencia = "exec spAfectar 'INV', " + getIdInv() + ", 'AFECTAR', 'Todo', '" + user + "'";
                SqlCommand cmdTrans = new SqlCommand(Transferencia, conn);
                cmdTrans.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return res = 1;
            }
            return res;
        }

        //Funcion: congelaInv
        //Proposito: congela los productos en una ubicacion especifica para poder iniciar el inventario de dicha ubicación
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

        //Funcion: contarHuecos
        //Proposito: reporta los huecos (merma produccion) que existen en un rack
        public int contarHuecos(string id, string ordenProduccion, string codigo, string user, string sucursal, string reng, string newId)
        {
            int huecos = 0;
            int res = 0;
            int nuevoLiberado = 0;
            string centrocosto = "";
            string[] parametros = getParametros("Intelisis");
            string[] parametros2 = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + "");
            try
            {

                conn2.Open();


                //JLMQ datos de la produccion, cantidad en curado, renglon y renglonId
                string selectCurado = "Select prodCurado,Renglon from CatProd where id_parcialidad = '" + newId + "' ";
                SqlCommand cmdCurado = new SqlCommand(selectCurado, conn2);
                SqlDataReader readerCurado = cmdCurado.ExecuteReader();
                int cantidadCurado = 0, renglon, renglonId;
                if (readerCurado.Read())
                {
                    //estimaQty 
                    cantidadCurado = readerCurado.GetInt32(0);
                    //renglon 
                    renglon = readerCurado.GetInt32(1);
                }
                else
                {
                    return res = 1;
                }
                readerCurado.Close();
                conn2.Close();
                conn2.Open();
                //ahora vamos por la cantidad liberada hasta el momento
                string selectLiberado = "Select prodLiberado, prodRestante, prodAsignado from CatProd where id_parcialidad = '" + newId + "'";
                SqlCommand cmdLiberado = new SqlCommand(selectLiberado, conn2);
                SqlDataReader readerLiberado = cmdLiberado.ExecuteReader();
                int cantidadLiberado = 0, cantidadRestante = 0;
                if (readerLiberado.Read())
                {
                    cantidadLiberado = readerLiberado.GetInt32(0);
                    cantidadRestante = readerLiberado.GetInt32(1);
                }
                else
                {
                    return res = 1;
                }
                readerLiberado.Close();
                conn2.Close();
                conn2.Open();
                //por ultimo, lo que se ha mermado hasta ahorita
                string selectMermas = "Select mermas from CatProd where id_parcialidad = '" + newId + "'";
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
                readerMermas.Close();
                conn2.Close();
                conn.Open();
                //conteo de renglones
                //string selectRenglonSub = "select count(renglonsub) from ProdD where Renglon ='" + renglon + "'";
                string selectRenglonSub = "select RenglonSub from ProdD where Articulo ='" + codigo + "' and ID = " + id + " and CantidadPendiente is not NULL and CentroDestino IS NULL and Centro = 'BUSP1CURAD' and Cantidad = " + cantidadCurado + " ";//JLMQ3ENE19
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
                readerRenglonSub.Close();
                conn.Close();
                conn.Open();

                contador = conteoRenglones;
                huecos = cantidadMerma;
                nuevoLiberado = cantidadCurado - huecos;


                string selectCentroDestino = "SELECT Centro,CentroDestino from ProdD where id = " + id + " and Renglon = " + renglon + " and RenglonSub = " + conteoRenglones;
                SqlCommand cmdDestino = new SqlCommand(selectCentroDestino, conn);
                SqlDataReader readerDestino = cmdDestino.ExecuteReader();
                string centro = "", centroDestino = "";
                if (readerDestino.Read())
                {
                    centro = readerDestino.GetValue(0).ToString();
                    centroDestino = readerDestino.GetValue(1).ToString();
                    conn.Close();

                    string updateCatProd = "UPDATE catProd set prodCurado = 0, prodLiberado = " + nuevoLiberado + ", mermas = " + huecos + ", prodRestante = " + cantidadRestante + " where id_parcialidad = '" + newId + "'";
                    Ejecuta(updateCatProd, conn2);
                    //ahora sigue que actualicemos ProdD
                    string updateProdD = "UPDATE ProdD set CantidadA = " + huecos + " where id = " + id + " and RenglonSub = " + conteoRenglones + " and Articulo = '" + codigo + "'";
                    Ejecuta(updateProdD, conn);
                    //Generamos la entrada de produccion
                    string generarMerma = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Seleccion', 'Entrada Produccion', '" + user + "', @Estacion = 99";
                    Ejecuta(generarMerma, conn);
                    //afectamos la entrada de produccion generada
                    string afectarMerma = "EXEC spAfectar 'PROD', " + getIdProd() + ", 'AFECTAR', 'Todo', 'Entrada Produccion', '" + user + "', @Estacion = 99";
                    Ejecuta(afectarMerma, conn);
                    int rID = getRenglonId(renglon, contador);
                    //por ultimo se insertan las mermas en Inv e InvD
                    string movId = setMovID("Merma Produccion", sucursal);
                    string insertInv = "Insert into INV (Empresa,Mov,MovID,FechaEmision,UltimoCambio,UEN,Moneda,TipoCambio,Usuario,Referencia,Observaciones,Estatus,Directo,Almacen,AlmacenTransito,OrigenTipo,Origen,OrigenID,Sucursal,SucursalOrigen,SucursalDestino)" + "values ('GNAP','Merma Produccion','" + movId + "',GETDATE(),GETDATE(),2,'Pesos',1,'" + user + "',(select referencia from prod where id=" + id + "),(select observaciones from prod where id=" + id + "),'SINAFECTAR',1,'APT-BUS','(TRANSITO)','PROD','Entrada Produccion','" + ordenProduccion + "'," + sucursal + "," + sucursal + "," + sucursal + ")";
                    Ejecuta(insertInv, conn);
                    int idInv = getIdInv();
                    string convertido = convertUnidad2Metro(codigo, huecos).ToString();
                    string uniProd = getUnidad(codigo);
                    centrocosto = getcentrocosto(codigo);
                    string insertInvD = "Insert into INVD (ID,Renglon,RenglonSub,RenglonId,Renglontipo,Cantidad,Almacen,Codigo,Articulo,ContUso,CantidadPendiente,Unidad,Factor,Producto,Tipo,Sucursal)" + "values (" + getIdInv() + "," + renglon + ",0," + rID + ",'N'," + convertido + ",'APT-BUS',null,'" + codigo + "','" + centrocosto + "'," + convertido + ",(select UnidadCompra from Art where Articulo='" + codigo + "'),(select factor from ArtUnidad where Articulo='" + codigo + "' and Unidad = '" + uniProd + "'),'" + codigo + "','Salida'," + sucursal + ")";
                    Ejecuta(insertInvD, conn);
                    string spAfectarMP = "EXEC spAfectar 'INV', " + idInv + ",'AFECTAR','Todo','Merma Produccion','" + user + "',@Estacion = 99";//aqui pone costo
                    Ejecuta(spAfectarMP, conn);
                    string estatusUsu = "UPDATE MovEstatusLog SET Estatus = 'CONCLUIDO' WHERE ModuloID = " + idInv + " AND mODULO = 'INV ' AND Sucursal = " + sucursal + " AND Usuario = '" + user + "'";
                    Ejecuta(estatusUsu, conn);
                    string act = "Update Inv set Estatus = 'CONCLUIDO' where id = " + idInv + "";
                    Ejecuta(act, conn);
                    string insertMovFlujo = "Insert into movFlujo values (" + sucursal + ",'GNAP','PROD'," + id + ",'Entrada Produccion','" + ordenProduccion + "','INV'," + idInv + ",'Merma Produccion','" + movId + "',0)";
                    Ejecuta(insertMovFlujo, conn);
                    contador = conteoRenglones;
                    return res;

                }
                else
                {
                    readerDestino.Close();//JLMQ 18SEP2018
                    contador++;
                }
            }
            catch (Exception e)
            {
                conn.Close();
                conn2.Close();
                string error = e.Message;
                return res = 1;
            }

        
        
            return res;
        }


        //Funcion: currentValueMermas
        //Proposito: regresa el valor especifico de piezas que la orden tiene en ese estado (PENDIENTE,CURADO,LIBERADO,MERMA)
        public int currentValueMermas(string folio, string estado, string id, string renglon, string newId)
        {
            int current = 0;
            string[] prueba = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");
            try
            {
                conn.Open();
                if (estado == "INICIAL")
                {
                    string select = "Select prodAsignado from catProd where OrdenProduccion ='" + folio + "' and renglon = " + renglon + " and id_parcialidad = '" + newId + "'";
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
                    string select = "Select prodAsignado from catProd where OrdenProduccion ='" + folio + "' and renglon = " + renglon + " and id_parcialidad = '" + newId + "'";
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
                    string select = "Select prodCurado from catProd where OrdenProduccion ='" + folio + "' and renglon = " + renglon + " AND Asignado = 1 and id_parcialidad = '" + newId + "'";
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
                    string select = "Select prodLiberado from catProd where OrdenProduccion ='" + folio + "' and renglon = " + renglon + " and id_parcialidad = '" + newId + "'";
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
                else if (estado == "CONCLUIDO")
                {
                    string select = "Select prodConcluido from catProd where OrdenProduccion ='" + folio + "' and renglon = " + renglon + " and id_parcialidad = '" + newId + "'";
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
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                current = -1;
            }
            return current;
        }


        //Funcion: currentValueLiberar
        //Proposito: regresa el valor especifico de piezas que la orden tiene en ese estado (PENDIENTE,CURADO,LIBERADO,MERMA)
        public int currentValueLiberar(string folio, string estado, string id, string renglon, string newId)
        {
            int current = 0;
            string[] prueba = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");
            try
            {
                conn.Open();
                if (estado == "INICIAL")
                {
                    string select = "Select prodAsignado from catProd where OrdenProduccion ='" + folio + "' and renglon = " + renglon + " and id_parcialidad = '" + newId + "'";
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
                    string select = "Select prodAsignado from catProd where OrdenProduccion ='" + folio + "' and renglon = " + renglon + " and id_parcialidad = '" + newId + "'";
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
                    string select = "Select prodCurado from catProd where OrdenProduccion ='" + folio + "' and renglon = " + renglon + " AND Asignado = 1 and id_parcialidad = '" + newId + "'";
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
                    string select = "Select prodLiberado from catProd where OrdenProduccion ='" + folio + "' and renglon = " + renglon + " and id_parcialidad = '" + newId + "'";
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
                else if (estado == "CONCLUIDO")
                {
                    string select = "Select prodConcluido from catProd where OrdenProduccion ='" + folio + "' and renglon = " + renglon + " and id_parcialidad = '" + newId + "'";
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
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                current = -1;
            }
            return current;
        }


        //Funcion: currentValue
        //Proposito: regresa el valor especifico de piezas que la orden tiene en ese estado (PENDIENTE,CURADO,LIBERADO,MERMA)
        public int currentValue(string folio, string estado, string id, string renglon, string newId)
        {
            int current = 0;
            string[] prueba = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");
            try
            {
                conn.Open();
                if (estado == "INICIAL")
                {
                    string select = "Select prodAsignado from catProd where OrdenProduccion ='" + folio + "' and renglon = " + renglon + " and id_parcialidad = '" + newId + "'";
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
                    string select = "Select prodAsignado from catProd where OrdenProduccion ='" + folio + "' and renglon = " + renglon + " and id_parcialidad = '" + newId + "'";
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
                    string select = "Select prodCurado from catProd where OrdenProduccion ='" + folio + "' and renglon = " + renglon + " AND Asignado = 1 and id_parcialidad = '" + newId + "'";
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
                    string select = "Select prodLiberado from catProd where OrdenProduccion ='" + folio + "' and renglon = " + renglon + " and id_parcialidad = '" + newId + "'";
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
                else if (estado == "CONCLUIDO")
                {
                    string select = "Select prodConcluido from catProd where OrdenProduccion ='" + folio + "' and renglon = " + renglon + " and id_parcialidad = '" + newId + "'";
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
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                current = -1;
            }
            return current;
        }


        //Funcion: currentValueHuecos
        //Proposito: regresa el valor especifico de piezas que la orden tiene en ese estado (PENDIENTE,CURADO,LIBERADO,MERMA)
        public int currentValueHuecos(string estado, string newId)
        {
            int current = 0;
            string[] prueba = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");
            try
            {
                conn.Open();
                if (estado == "INICIAL")
                {
                    string select = "Select prodAsignado from catProd where id_parcialidad = '" + newId + "'";
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
                    string select = "Select prodAsignado from catProd where id_parcialidad = '" + newId + "'";//and prodAsignado = " + pzasParcialidad + "  SE OMITE JLMQ 18SEPT2018
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
                    string select = "Select prodCurado from catProd where id_parcialidad = '" + newId + "'";
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
                    string select = "Select prodLiberado from catProd where id_parcialidad = '" + newId + "'";
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
                else if (estado == "CONCLUIDO")
                {
                    string select = "Select prodConcluido from catProd where id_parcialidad = '" + newId + "'";
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
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                current = -1;
            }
            return current;
        }



        //Funcion: detalleEscuadra
        //Proposito: regresa un array con toda la informacion necesaria de una escuadra segun el EPC que se haya leido
        public string[] detalleEscuadra(string EPC)
        {
            string[] detalle = new string[6];
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                //string select = "select cp.OrdenProduccion, cp.Pedido, cp.Descripcion, de.Piezas, cp.Codigo from DetEscuadras de inner join CatProd cp on cp.OrdenProduccion = de.Ordenproduccion where de.EPC = '" + EPC + "' and de.CodigoProducto = '" + cp + "'";
                //string select = "select de.OrdenProduccion, de.Pedido, cp.Descripcion, de.Piezas, de.CodigoProducto from DetEscuadras de inner join CatProd cp on cp.OrdenProduccion = de.Ordenproduccion where cp.Codigo = (SELECT TOP(1) CodigoProducto from DetEscuadras where EPC = '" + EPC + "')";//JLMQ Se agrega TOP(1) 14 junio 2016
                //string select = "select de.OrdenProduccion, de.Pedido, cp.Descripcion, de.Piezas, de.CodigoProducto from DetEscuadras de inner join CatProd cp on cp.Codigo = de.CodigoProducto where cp.Codigo = (SELECT CodigoProducto from DetEscuadras where EPC = '" + EPC + "')";
                string select = "SELECT de.OrdenProduccion, de.Pedido, cp.Descripcion, de.Piezas, de.CodigoProducto, de.newIdEscuadra FROM DetEscuadras AS de INNER JOIN CatProd AS cp ON cp.Codigo = de.CodigoProducto AND cp.OrdenProduccion = de.OrdenProduccion  where EPC = '" + EPC + "'";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    detalle[0] = reader.GetString(0);
                    detalle[1] = reader.GetValue(1).ToString();
                    detalle[2] = reader.GetString(2);
                    detalle[3] = reader.GetInt32(3).ToString();
                    detalle[4] = reader.GetString(4);
                    detalle[5] = reader.GetString(5);
                }
                else
                {
                    return null;
                }
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return null;
            }
            return detalle;
        }

        //Funcion: cantidadEscuadra
        //Proposito: regresa la cantidad de piezas que estan asignadas a una escuadra (tarima)
        public int cantidadEscuadra(string EPC)
        {
            int current = 0;
            string[] prueba = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");
            try
            {
                conn.Open();
                string select = "Select Piezas from detEscuadras where EPC ='" + EPC + "' and CodigoProducto = '" + EPC + "'";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return current = reader.GetInt32(0);
                }
                else
                {
                    return current = -1;
                }
            }
            catch (Exception)
            {
                conn.Close();
                return -1;
            }
        }

        //Funcion: detalleProd
        //Proposito: regresa un array con toda la informacion necesaria de una orden de produccion especifica.
        public string[] detalleProd(string op, string codigo, string newId)
        {
            string[] res = new string[18];
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                string select = "select Id,OrdenProduccion,Pedido,Cliente,Descripcion,Tipo,Medida,Color,Codigo,Cantidad,Estatus,Renglon,Asignado,prodEstibado,prodAsignado,prodCurado,prodLiberado, id_parcialidad from catProd where ordenProduccion = '" + op + "' and codigo = '" + codigo + "' and  id_parcialidad = '" + newId + "'";
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
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return null;
            }
            return res;
        }

        //Funcion: fillProd
        //Proposito: llena la base de datos de TAGO con las ordenes de produccion pendientes en INTELISIS
        public int fillProd()
        {
            int res = 0;
            ArrayList nuevos = new ArrayList();
            string id, OrdenProduccion, Cliente, Descripcion, Tipo, Medida, Color, Codigo, Cantidad, Estatus, Renglon, Referencia;
            //DataSet ds = new DataSet();
            try
            {
                string select = "select distinct pd.Id, pd.MovId,c.Nombre, pd.ArtDescripcion, a.Familia, pd.Unidad, a.Fabricante, pd.Articulo, pd.Cantidad, pd.Estatus, pd.Renglon, p.Referencia as Pedido from ProdPendienteD pd inner join Art a on a.Articulo = pd.Articulo inner join Prod p on p.MovId = pd.MovId inner join ProdD pdd on pdd.Id = p.ID inner join Venta v on v.Movid = pdd.DestinoID inner join Cte c on c.Cliente = v.Cliente order by pd.Id desc";
                DataTable dtInt = getDatasetConexionWDR(select, "Intelisis");
                int countRows = getInt("select count(*) from CatProd", "Solutia");
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

        //Funcion: getArrayEtiquetasCB
        //Proposito: regresa un array con toda la informacion de una etiqueta previamente impresa.
        public string[] getArrayEtiquetasCB(string cb, string remision)
        {
            string[] or = ordenRemision(remision);
            string[] data = new string[16];
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
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
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                data = null;
            }
            return data;
        }

        //Funcion: getArrayEtiquetasCB
        //Proposito: regresa un array con toda la informacion de una etiqueta previamente impresa (Codigo de Barras).
        public string[] getArrayEtiquetasCodB(string cb)
        {
            string[] data = new string[12];
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
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
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                data = null;
            }
            return data;
        }

        //Funcion: getArrayData
        //Proposito: regresa un array con los datos de una escuadra con el EPC leido por el IP30
        public string[] getArrayData(string epc, string codigo)
        {
            
            string[] data = new string[6];
            string[] data2 = new string[1];
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                //obtener el resultado de la consulta
                conn.Open();
                string select2 = "SELECT OrdenProduccion from DetEscuadras WHERE EPC = '" + epc + "'"; 
                SqlCommand cmd2 = new SqlCommand(select2, conn);
                SqlDataReader reader2 = cmd2.ExecuteReader();
                if (reader2.Read())
                {
                    int sizeReader = reader2.FieldCount;
                    for (int x = 0; x < sizeReader; x++)
                    {
                        data2[x] = reader2.GetValue(x).ToString();
                    }
                }
                conn.Close();
                if (data2[0].Equals("SIN OP"))
                {
                    conn.Open();
                    //string select = "SELECT de.OrdenProduccion, dr.Pedido, dr.CodigoProducto AS Cliente, ca.Descripción as Descripcion , dr.PzaRemision, de.CodigoProducto FROM DetEscuadras de inner join detRemision dr on de.Pedido=dr.pedido inner join  catArt ca ON ca.clave=de.CodigoProducto WHERE de.EPC = '" + epc + "'";
                    string select = "SELECT de.OrdenProduccion, dr.Pedido, dr.CodigoProducto AS Cliente, ca.Descripción as Descripcion , dr.PzaRemision, de.CodigoProducto FROM DetEscuadras de inner join detRemision dr on de.Pedido=dr.pedido AND de.CodigoProducto = dr.CodigoProducto inner join  catArt ca ON ca.clave=de.CodigoProducto WHERE de.EPC = '" + epc + "' AND de.CodigoProducto = '" + codigo + "'";
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
                else
                {
                    conn.Open();
                    //string select = "SELECT de.OrdenProduccion,cp.Pedido,cp.Cliente,cp.Descripcion, dr.CantidadPza, de.CodigoProducto FROM DetEscuadras de INNER JOIN catProd cp on cp.OrdenProduccion = de.OrdenProduccion WHERE de.EPC = '" + epc + "'";//cambiar de.piezas por dr.CantidadPza
                    string select = "SELECT de.OrdenProduccion,cp.Pedido,cp.Cliente,cp.Descripcion, dr.PzaRemision, de.CodigoProducto FROM DetEscuadras de INNER JOIN catProd cp on cp.OrdenProduccion = de.OrdenProduccion INNER JOIN detRemision dr ON de.Pedido=dr.Pedido WHERE de.EPC = '" + epc + "' AND dr.CodigoProducto = '" + codigo + "' and cp.Codigo = '" + codigo + "'";//jlmq 18 may 2016 para pruebas
                    //string select = "SELECT de.OrdenProduccion,cp.Pedido,cp.Cliente,cp.Descripcion, v.Paquetes, de.CodigoProducto FROM napresaws.dbo.DetEscuadras de INNER JOIN napresaws.dbo.catProd cp on cp.OrdenProduccion = de.OrdenProduccion INNER JOIN PRUEBASPILLO.dbo.Venta v ON cp.Pedido=v.OrigenID  WHERE de.EPC = '" + epc + "'";//JLMQ prueba en xero 20 may 2016 se comento el de arriba

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
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                data = null;
            }
            return data;
        }

        //Funcion: getAsignadasWDR
        //Proposito: traes de BD todas las ordenes de produccion asignadas para revisar el seguimiento de las mismas.
        public DataTable getAsignadasWDR(string op)
        {
            string select = "Select Id,OrdenProduccion,Pedido,Cliente,Descripcion,Tipo,Medida,Color,Codigo,Cantidad,Estatus,Renglon,Asignado,prodEstibado,prodAsignado from catProd where OrdenProduccion='" + op + "' and Asignado = 1";
            return getDatasetConexionWDR(select, "Solutia");
        }

        //Funcion: getBautizoCompleto
        //Proposito: valida que los racks asignados hayan sido bautizados
        public int getBautizoCompleto(string op, string codigo, string renglon)
        {
            int res = 0;
            int alta = 0;
            int asignados = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                string selectOP = "select * from detRProd where OrdenProduccion = '" + op + "' and CodigoProducto ='" + codigo + "' and Renglon=" + renglon + "";
                SqlCommand cmdSOP = new SqlCommand(selectOP, conn);
                SqlDataReader readerSOP = cmdSOP.ExecuteReader();

                if (readerSOP.Read())
                {
                    readerSOP.Close();
                    //primero seleccionamos CUANTAS estan dadas de alta
                    string selectAlta = "select count(*) from detRProd where OrdenProduccion = '" + op + "' and CodigoProducto ='" + codigo + "' and Renglon=" + renglon + "";
                    SqlCommand cmdAlta = new SqlCommand(selectAlta, conn);
                    SqlDataReader readerAlta = cmdAlta.ExecuteReader();
                    while (readerAlta.Read())
                    {
                        alta = readerAlta.GetInt32(0);
                    }
                    readerAlta.Close();
                    //luego cuantos racks tiene verificados esa orden
                    string selectAsignados = "select count(*) from detRProd where OrdenProduccion = '" + op + "' and CodigoProducto ='" + codigo + "' and Renglon=" + renglon + " and Verificado = 1";
                    SqlCommand cmdAsignados = new SqlCommand(selectAsignados, conn);
                    SqlDataReader readerAsignados = cmdAsignados.ExecuteReader();
                    while (readerAsignados.Read())
                    {
                        asignados = readerAsignados.GetInt32(0);
                    }
                    readerAsignados.Close();
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
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                res = 1;
            }
            return res;
        }

        //Funcion: getDatasetWDR
        //Proposito: trae cualquier select a la aplicacion para que sea mostrado en pantalla.
        public DataTable getDatasetWDR(string comando)
        {
            DataTable res = new DataTable();
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                using (conn)
                {
                    string select = comando;
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
                //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return res;
        }

		public class Producto {
			public int cantidad { get; set; }
			public int cantidadTotal { get; set; }
			public string epc { get; set; }
			public Boolean tarimaParcial { get; set; }

			public Producto() {
				cantidad = 0;
				cantidadTotal = 0;
				tarimaParcial = false;
				epc = "";
			}

			public Producto(int cantidad, int cantidadTotal, string epc, bool tarimaParcial) {
				this.cantidad = cantidad;
				this.cantidadTotal = cantidadTotal;
				this.epc = epc;
				this.tarimaParcial = tarimaParcial;
			}
		}

		/// <summary>MandarEmail
		///  Proposito:Mandar el objeto sendEmail_model que es un correo
		/// </summary>
		/// <param name="vFrom">Usuario o nombre de la persona que realiza la acción.</param>
		/// <param name="vTo">Correo del encargado d eproducción</param>
		/// <param name="vOrdenProd">Orden de producción de la cual se le está quitando el producto</param>
		/// <param name="vProducto">Nombre o descripción del producto</param>
		/// <param name="vCant">Cantidad que se le está quitando a la orden</param>
		/// <returns>Un string para validar el envío del correo</returns>
		public static string SendEmail(string vFrom, string vTo, string vOrdenProd, string vProducto, string vCant) {
			string retString = "";
			using (GoMobileWS ws = new GoMobileWS()) {
				DateTime dtHoy = System.DateTime.Now;
				string fecha = dtHoy.Day + "/" + dtHoy.Month + "/" + dtHoy.Year;
				string hora = dtHoy.TimeOfDay.Hours + ":" + dtHoy.TimeOfDay.Minutes + ":" + dtHoy.TimeOfDay.Seconds;
				sendEmail_model mail = new sendEmail_model();   //Objeto en el cual se arma el Correo.
				clsError result = new clsError();               //Objeto que regresa el método de envío de correo.
				mail.subject = "Orden de producción " + vOrdenProd + " desacompletada";
				mail.to = vTo;
				mail.from = vFrom;
				
				mail.body = "Se te Informa que se desasoció el producto terminado (" + vProducto + ") "
					+ "de la orden de producción (" + vOrdenProd + "), "
					+ "si es el caso favor de generar una nueva Orden de Producción para completar el Pedido."
					+ "</br></br> Día: [ " + fecha + " ]"
					+ "</br> Hora: [ " + hora + " ]"
					+ "</br> Usuario [ " + vFrom.ToUpper() + " ]"
					+ "</br> No. de piezas: [" + vCant + "]";
				result = null;

				try {
					result = ws.sendEmail(mail);
					if (result.successful) {
						retString = "Correo Enviado " + fecha + " " + hora;
					} else {
						retString = result.details.ToString();
					}
				} catch (Exception e) {
					return null;
					//throw;
				}
			}
			return retString;
		}

		// Objetivo: Modificar la BD 
		public Boolean sendToStock(string codigo, string ordenProduccion, int cantidad, int cantidadTotal) {
			string[] parametros = getParametros(CONEXION_SOLUTIA);
			try {
				SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; "
					+ "Initial Catalog=" + parametros[4] + "; "
					+ "Persist Security Info=True; "
					+ "User ID=" + parametros[2] + "; "
					+ "Password=" + parametros[3] + "");
				 
				using (conn) {
					string query = "UPDATE catProd SET " + PROD_TERMINADO + " = (" + PROD_TERMINADO + " - " + cantidad + ")"
						+ " WHERE (Codigo = '" + codigo + "' AND "
						+ "OrdenProduccion = '" + ordenProduccion + "' AND "
						+ PROD_TERMINADO + " = " + cantidadTotal + ");";
					return Ejecuta(query, conn);
				}
			} catch (Exception e) {
				//throw;
			}

			return false;
		}

		public Boolean escuadrasToStock(string epc, string nuevaUbicacion, bool esParcial, int cantidad, string nuevoEPC) {
			string[] parametros = getParametros(CONEXION_SOLUTIA);
			try {
				SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; "
					+ "Initial Catalog=" + parametros[4] + "; "
					+ "Persist Security Info=True; "
					+ "User ID=" + parametros[2] + "; "
					+ "Password=" + parametros[3] + "");
				using (conn) {
					Boolean existeEnRegistro = existeEscuadraVirtual(nuevoEPC, conn);
					String query = (existeEnRegistro)
								? "UPDATE DetEscuadras SET Piezas = (Piezas + " + cantidad + ") "
									+ "WHERE (EPC = '" + nuevoEPC + "');"
							: (esParcial)
								? "INSERT INTO DetEscuadras (idEscuadra, EPC, OrdenProduccion, Asignado, Ubicada, CodigoProducto, "
									+ "Picked, Embarcado, Piezas, Pedido, Posicion, Pendiente) "
									+ "VALUES((SELECT MAX(idEscuadra) FROM DetEscuadras) + 1, '" + nuevoEPC + "', "
									+ "'GRANEL', 1, 1, '" + nuevoEPC + "', 0, 0, " + cantidad + ", null, 'A01', 0);"
								: "UPDATE DetEscuadras SET OrdenProduccion = 'GRANEL', "
									+ "EPC = '" + nuevoEPC + "', "
									+ "Posicion = '" + nuevaUbicacion + "' "
									+ "WHERE (EPC = '" + epc + "');";

					Ejecuta(query, conn);
					
					query = (esParcial) ? query = "UPDATE DetEscuadras SET Piezas = (Piezas - " + cantidad + ") WHERE (EPC = '" + epc + "');"
						: "UPDATE DetEscuadras SET OrdenProduccion = NULL, Asignado = 0, Ubicada = 0, CodigoProducto = NULL, Picked = 0, Embarcado = 0, Piezas = 0 WHERE (EPC = '" + epc + "');";
					Ejecuta(query, conn);
				}
			} catch (Exception e) {
				//throw;
			}

			return false;
		}

		public Boolean existeEscuadraVirtual(String epc, SqlConnection conn) {
			try {
				string query = "SELECT 1 AS [ExisteRegistro] FROM DetEscuadras WHERE (EPC = '" + epc + "');";
				SqlCommand cmd = new SqlCommand(query, conn);
				conn.Open();
				string existe = "";
				using (SqlDataReader reader = cmd.ExecuteReader()) {
					while (reader.Read()) {
						existe = reader["ExisteRegistro"].ToString();
					}
				}
				conn.Close();
				return existe != null && existe.Equals("1");
			} catch (Exception) {
				//
			}

			return false;
		}

		//
		public string[] emailsProcesoStock(string planta) {
			string[] parametros = getParametros(CONEXION_SOLUTIA);
			try {
				SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; "
				+ "Initial Catalog=" + parametros[4] + "; "
				+ "Persist Security Info=True; "
				+ "User ID=" + parametros[2] + "; "
				+ "Password=" + parametros[3] + "");
				using (conn) {
					string query = "SELECT email FROM Emails_Stock WHERE (planta = '" + planta + "');";
					SqlCommand cmd = new SqlCommand(query, conn);
					conn.Open();
					using (SqlDataReader reader = cmd.ExecuteReader()) {
						List<string> listaEmails = new List<string>();
						while (reader.Read()) {
							listaEmails.Add(reader["email"].ToString());
						}
						conn.Close();
						return listaEmails.ToArray();
					}
				}
			} catch (Exception e) {
				return null;
				//throw;
			}
		}
        
		public int getRacksPendientes(string ordenProduccion, string codprod, string renglon, string newId) {
			int racksPendientes = 0;
			string[] parametros = getParametros(CONEXION_SOLUTIA);
			try {
				SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; "
				+ "Initial Catalog=" + parametros[4] + "; "
				+ "Persist Security Info=True; "
				+ "User ID=" + parametros[2] + "; "
				+ "Password=" + parametros[3] + "");
				using (conn) {
                    string query = "SELECT COUNT(*) AS [RacksPendientes] FROM DetRProd WHERE (OrdenProduccion = '" + ordenProduccion + "' AND ContadoProd = 0 AND CodigoProducto = '" + codprod + "' AND Renglon = " + renglon + " AND newId_Parcialidad = '"+ newId +"' );";
					SqlCommand cmd = new SqlCommand(query, conn);
					conn.Open();
					using (SqlDataReader reader = cmd.ExecuteReader()) {
						while (reader.Read()) {
							racksPendientes = reader.GetInt32(0);
						}
					}
				}
			} catch (Exception e) {
				racksPendientes = -1;
			}

			return racksPendientes;
		}

        //Funcion: getDatasetConexionWDR
        //Proposito: trae cualquier select a la aplicacion para que sea mostrado en pantalla.
        public DataTable getDatasetConexionWDR(string comando, string descrip)
        {
            DataTable res = new DataTable();
            string[] parametros = getParametros(descrip);
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                using (conn)
                {
                    string select = comando;
                    SqlCommand command = new SqlCommand(select, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    res.Load(reader);
                    reader.Close();
                }
                conn.Close();
            }
            catch (Exception e)
            {
                res = null;
                conn.Close();
                string error = e.Message;
                //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return res;
        }
        //JLMQ 15NOV2018 AGREGAR NEWID
        //Funcion: getEstibaActual 
        //Proposito: regresa la cantidad de piezas que una orden de produccion tiene estibadas actualmente.
        public int getEstibaActual(string op, string renglon, string newId)
        {
            int total = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                string select = "select prodEstibado from catprod where OrdenProduccion = '" + op + "' and renglon = " + renglon + " and id_parcialidad = '"+ newId +"'";
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
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                total = -1;
            }
            return total;
        }

        //Funcion: getIncompletosTransferWDR
        //Proposito: regresa un dataset con todas las ordenes de transferencia que hay pendientes en una orden de produccion
        public DataTable getIncompletosTransferWDR(string OP)
        {
            string select = "Select Id,Mov,MovID,Estatus From Inv Where  id In (Select Did From MovFlujo Where Omodulo = 'PROD' And OMovID = '" + OP + "'  And Dmov = 'Orden Transferencia') and Estatus != 'CONCLUIDO'";
            return getDatasetConexionWDR(select, "Intelisis");
        }

        //Funcion: getInt
        //Proposito: regresa un int
        public int getInt(string consulta, string conexion)
        {
            string[] prueba = getParametros(conexion);
            int result = 0;
            SqlConnection conn = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + ""); ;
            try
            {
                conn.Open();
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

        //Funcion: getPersonas
        //Proposito: no tiene nada que ver con personas, en su lugar trae un dataset de los racks que aun no han sido contados pór primera vez
        public DataTable getPersonas(string op, string codprod, string renglon, int pzas, string newId)
        {
            string select = "Select dp.EPC,dp.Numero,rp.Modelo as Modelo,dp.OrdenProduccion,dp.CantidadEstimada as [C.Estimada], dp.CantidadReal as [C.Real] from DetRProd dp inner join RProduccion rp on rp.IdRack = dp.IdRProd where OrdenProduccion = '" + op + "' and Estado = 1 and Verificado = 1 and ContadoProd = 0 and CodigoProducto = '" + codprod + "' and Renglon = " + renglon + " and newId_Parcialidad = '"+ newId +"' ";
            return getDatasetConexionWDR(select, "Solutia");
        }

        //Funcion: getCuradoRacks
        //Proposito: regresa un dataset para mostrar todas los racks que estan en cuarto de curado
        public DataTable getCuradoRacks(string op, string codprod, string renglon, int pzas, string newId)//JLMQ AGREGAR QUE FILTRE POR LA CANTIDAD DE LA PARCIALIDAD QUE SE ASIGNO DESDE UN PRINCICPIO
        {
            string select = "Select dp.EPC,dp.Numero,rp.Modelo as Modelo,dp.OrdenProduccion,dp.CantidadEstimada as [C.Estimada], dp.CantidadReal as [C.Real] from DetRProd dp inner join RProduccion rp on rp.IdRack = dp.IdRProd where OrdenProduccion = '" + op + "' and Estado = 1 and Verificado = 1 and ContadoProd = 1 and ContadoCurado = 0 and CodigoProducto = '" + codprod + "' and Renglon = " + renglon + " and newId_Parcialidad = '" + newId + "' ";
            return getDatasetConexionWDR(select, "Solutia");
        }

        //Funcion: getProdDWDR
        //Proposito: regresa un dataset con todas las ordenes de produccion pendientes en Intelisis
        public DataTable getProdDWDR(string folio)
        {
            string select = "select * from ProdPendienteD where MovID='" + folio + "'";
            return getDatasetConexionWDR(select, "Intelisis");
        }

        //Funcion: getRacksWDR
        //Proposito: regresa un dataset con todos los racks disponibles en la planta
        public DataTable getRacksWDR(string OP, string codigo, string renglon)
        {
            string select = "Select dp.EPC, rp.Modelo, dp.Numero, dp.OrdenProduccion, dp.CodigoProducto  from DetRProd dp inner join RProduccion rp on rp.IdRack = dp.IdRProd where OrdenProduccion = '" + OP + "' and CodigoProducto = '" + codigo + "' and Renglon = " + renglon + " and Estado = 1 and Verificado = 0";
            return getDatasetConexionWDR(select, "Solutia");
        }

        //Funcion: getRenglon
        //Proposito: regresa la partida(renglon) especifica de un producto en una orden de produccion especifica.
        public int getRenglon(string op, string codigo, string newId)
        {
            int renglon = 0;
            string[] prueba = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");
            try
            {
                conn.Open();
                string select = "Select Renglon from catProdD where idNew = '" + newId + "'";
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
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                renglon = -1;
            }
            return renglon;
        }

        //Funcion: getTipoRack
        //Proposito: regresa el modelo del rack leido (1-4) (Besser,Col22,Compacta)
        public int getTipoRack(string EPC)
        {
            int tr = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
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
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return tr;
        }
        //JLMQ 15NOV2018 AGREGAR NEWID
        //Funcion: getTotalCant
        //Proposito: regresa la cantidad total de piezas de la orden de producción
        public int getTotalCant(string op, string renglon, string newId)
        {
            int total = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                string select = "select Cantidad from catprod where OrdenProduccion = '" + op + "' and renglon = " + renglon + " and id_parcialidad = '"+ newId +"'";
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
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                total = -1;
            }
            return total;
        }

        //Funcion: getZonaRack
        //Proposito: regresa un array con la clave de la zona y la clave del rack donde se ubicara la escuadra
        public string[] getZonaRack(string idZona, string idRack)
        {
            string[] ZonaRack = new string[2];
            string[] parametros = getParametros("ConsolaAdmin");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            conn.Open();

            try
            {
                string selectZona = "Select ClaveZona from Zonas where idZona='" + idZona + "'";
                string selectRack = "Select Clave from racks where IDRack='" + idRack + "'";
                SqlCommand cmdZona = new SqlCommand(selectZona, conn);
                SqlCommand cmdRack = new SqlCommand(selectRack, conn);
                SqlDataReader readZona = cmdZona.ExecuteReader();

                if (readZona.Read())
                {
                    ZonaRack[0] = readZona.GetString(0).ToString();
                    readZona.Close();
                    SqlDataReader readRack = cmdRack.ExecuteReader();
                    if (readRack.Read())
                    {
                        ZonaRack[1] = readRack.GetString(0).ToString();
                        readRack.Close();
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
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return null;
            }
            return ZonaRack;
        }

        //Funcion: getZonaEPCWDR
        //Proposito: regresa un dataset con los EPCs de todas las banderas que esten dadas de alta.
        public DataTable getZonaEPCWDR(string op)
        {
            string select = "Select EPC from DetEscuadras where Asignado = 0 and Ubicada = 0";
            return getDatasetConexionWDR(select, "Solutia");
        }

        //Funcion: hiddenProdWDR
        //Proposito: regresa un dataset "invisible" con toda la informacion de una orden de produccion
        public DataTable hiddenProdWDR(int filtro)
        {
            string select = "";
            switch (filtro)
            {
                default:
                    select = "select OrdenProduccion,Pedido,Cliente,Descripcion,Tipo,Medida,id_parcialidad,Codigo,Cantidad,Estatus,prodEstibado,prodAsignado,prodCurado,prodLiberado,mermas,lote,Color from catProd where Estatus != 'CONCLUIDO' and Asignado = 1";
                    return getDatasetConexionWDR(select, "Solutia");
                case 1://Solo Ordenes Pendientes
                    select = "select OrdenProduccion,Pedido,Cliente,Descripcion,Tipo,Medida,id_parcialidad,Codigo,Cantidad,Estatus,prodEstibado,prodAsignado,prodCurado,prodLiberado,mermas,lote, Color from catProd where Estatus = 'PENDIENTE' and Asignado = 1";
                    return getDatasetConexionWDR(select, "Solutia");
                case 2://Solo Ordenes En Curado
                    select = "select OrdenProduccion,Pedido,Cliente,Descripcion,Tipo,Medida,id_parcialidad,Codigo,Cantidad,Estatus,prodEstibado,prodAsignado,prodCurado,prodLiberado,mermas from,lote, Color catProd where Estatus = 'CURADO' and Asignado = 1";
                    return getDatasetConexionWDR(select, "Solutia");
                case 3://Solo Ordenes Liberado
                    select = "select OrdenProduccion,Pedido,Cliente,Descripcion,Tipo,Medida,id_parcialidad,Codigo,Cantidad,Estatus,prodEstibado,prodAsignado,prodCurado,prodLiberado,mermas,lote, Color from catProd where Estatus = 'LIBERADO' and Asignado = 1";
                    return getDatasetConexionWDR(select, "Solutia");
            }
        }

        //Funcion: ingresoUsuario
        //Proposito: definitivamente un Logout no hace o si?
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
                consolaAdmin.Close();
                string error = ex.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return res;
            }
			Program.usuario = res;
            return res;
        }

        //Funcion: insertEtiquetas
        //Proposito: inserta nuevas etiquetas una vez que estas se han impreso exitosamente
        public int insertEtiquetas(string Pedido, string OrdenProduccion, string Cliente, string N_Tarima, string Producto, int Cantidad, int CantidadTarima, string Medida, string Codigo, string Color, string Tipo, string EPC)
        {
            int res = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                string insert = "insert into Etiquetas_Impresas (Pedido,OrdenProduccion,Cliente,N_Tarima,Producto,Cantidad,CantidadTarima,Medida,Codigo,Color,Tipo,EPC) values('" + Pedido + "','" + OrdenProduccion + "','" + Cliente + "','" + N_Tarima + "','" + Producto + "'," + Cantidad + "," + CantidadTarima + ",'" + Medida + "','" + Codigo + "','" + Color + "','" + Tipo + "','" + EPC + "')";
                SqlCommand cmd = new SqlCommand(insert, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                res = 1;
            }
            return res;
        }

        //Funcion: inserta
        //Proposito: ¿en serio necesitas que te explique esto? 
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
                consolaAdmin.Close();
                string error = ex.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return -1;
            }
            return inserted;
        }

        //Funcion: mermaRecepcion
        //Proposito: afecta las base de datos de intelisis para registrar a nueva merma en inventario.(MERMA ESTIBA)
        public int mermaRecepcion(int rackHuecos, int id, string codigo, string op, int renglon, string user, string sucursal, int nuevo, string epc, int bandera, string newId)
        {
            int res = 0;
            string centrocosto = "";
            string[] parametros = getParametros("Intelisis");
            string[] parametros2 = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + "");
            try
            {
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
                readerEstimado.Close();
                conn2.Close();
                conn2.Open();
                //if (rackHuecos > 0 && rackHuecos < cantidadEstimada)//JLMQ debe ir la merma y no el restante de cantidad-merma
                //if (nuevo > 0 && nuevo < cantidadEstimada)
                if (nuevo > 0)
                {
                    int cantidadReal = rackHuecos;
                    //datos de la produccion, cantidad en curado, renglon y renglonId
                    string selectCurado = "Select prodCurado from CatProd where codigo ='" + codigo + "' and OrdenProduccion ='" + op + "' and id_parcialidad = '" + newId + "'";
                    SqlCommand cmdCurado = new SqlCommand(selectCurado, conn2);
                    SqlDataReader readerCurado = cmdCurado.ExecuteReader();
                    int cantidadCurado = 0, renglonId;
                    if (readerCurado.Read())
                    {
                        //estimaQty 
                        cantidadCurado = readerCurado.GetInt32(0);
                        //renglonId 
                        //renglonId = renglon / 2048;
                    }
                    else
                    {
                        return res = 1;
                    }
                    readerCurado.Close();
                    conn2.Close();
                    conn2.Open();
                    //ahora vamos por la cantidad liberada hasta el momento
                    string selectLiberado = "Select prodLiberado from CatProd where codigo ='" + codigo + "' and OrdenProduccion ='" + op + "' and id_parcialidad = '" + newId + "'";
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
                    conn2.Close();
                    conn2.Open();
                    //por ultimo, lo que se ha mermado hasta ahorita
                    string selectMermas = "Select mermas from CatProd where codigo ='" + codigo + "' and OrdenProduccion ='" + op + "' and id_parcialidad = '" + newId + "'";
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
                    conn2.Close();

                    conn2.Open();
                    int piezasEsc = 0;
                    string queryPiezas;
                    queryPiezas = "SELECT Piezas FROM DetEscuadras WHERE EPC = '"+ epc +"'";
                    SqlCommand queyPza = new SqlCommand(queryPiezas, conn2);
                    SqlDataReader readerQuery = queyPza.ExecuteReader();
                    if (readerQuery.Read())
                    {
                        piezasEsc = readerQuery.GetInt32(0);
                    }
                    else
                    {
                        return res = 1;
                    }
                    conn2.Close();

                    conn.Open();
                    //conteo de renglones
                    string selectRenglonSub = "select RenglonSub from ProdD where Articulo ='" + codigo + "' and id = " + id + " AND Cantidad = "+ piezasEsc +"";
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
                    conn.Close();
                    conn.Open();
                    contador = conteoRenglones;
                    
                    //int nuevoLiberado = cantidadLiberado - rackHuecos, nuevoMermas = cantidadMerma + rackHuecos;
                    int nuevoLiberado = rackHuecos;//CANTIDAD DE MERMA
                    int nuevoMermas;
                    if (bandera == 2)
                    {
                        nuevoMermas = cantidadMerma;
                    }
                    else
                        nuevoMermas = nuevo;// - cantidadMerma;
                    int mermaReal;
                    mermaReal = cantidadMerma + nuevoLiberado;
                    string selectCentroDestino = "SELECT Centro,CentroDestino from ProdD where id = " + id + " and Renglon = " + renglon + " and RenglonSub = " + contador;
                    SqlCommand cmdDestino = new SqlCommand(selectCentroDestino, conn);
                    SqlDataReader readerDestino = cmdDestino.ExecuteReader();
                    string centro = "", centroDestino = "";
                    if (readerDestino.Read())
                    {
                        centro = readerDestino.GetValue(0).ToString();
                        centroDestino = readerDestino.GetValue(1).ToString();
                        conn.Close();
                                                
                    
                        //primero dime cuanto quedo en los racks
                        //luego actualizar CatProd
                        //string updateCatProd = "UPDATE catProd set prodLiberado = " + nuevoLiberado + ", mermas = " + nuevoMermas + " where Codigo = '" + codigo + "' and OrdenProduccion = '" + op + "'";
                        string updateCatProd = "UPDATE catProd set prodConcluido = " + nuevoMermas + ", mermas = " + mermaReal + " where Codigo = '" + codigo + "' and OrdenProduccion = '" + op + "' and id_parcialidad = '" + newId + "'";
                        Ejecuta(updateCatProd, conn2);
                        //string updateDetEscuadra = "UPDATE detEscuadras set Piezas = " + nuevo + " WHERE EPC = '" + epc + "'";//JLMQ18 AQUI NO DEBE IR NUEVO DEBE IR rackHuecos
                        string updateDetEscuadra = "UPDATE detEscuadras set Piezas = " + nuevoMermas + " WHERE EPC = '" + epc + "'";
                        Ejecuta(updateDetEscuadra, conn2);
                        //ahora sigue que actualicemos ProdD
                        //string updateProdD = "UPDATE ProdD set CantidadA = " + rackHuecos + " where id = " + id + "and RenglonSub = " + contador + "";//JLM118 SE DEBE AGREGAR QUE FILTRE POR Codigo
                        string updateProdD = "UPDATE ProdD set CantidadA = " + rackHuecos + " where id = " + id + "and RenglonSub = " + contador + " and Articulo = '" + codigo + "'";
                        Ejecuta(updateProdD, conn);
                        //Generamos la entrada de produccion
                        string generarMerma = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Seleccion', 'Entrada Produccion', '" + user + "', @Estacion = 99";
                        Ejecuta(generarMerma, conn);
                        //afectamos la entrada de produccion generada
                        string afectarMerma = "EXEC spAfectar 'PROD', " + getIdProd() + ", 'AFECTAR', 'Todo', 'Entrada Produccion', '" + user + "', @Estacion = 99";
                        Ejecuta(afectarMerma, conn);
                        int rID = getRenglonId(renglon, contador);
                        //por ultimo se insertan las mermas en Inv e InvD
                        string movId = setMovID("Merma Estiba", sucursal);
                        centrocosto = getcentrocosto(codigo);
                        string insertInv = "Insert into INV (Empresa,Mov,MovID,FechaEmision,UltimoCambio,UEN,Moneda,TipoCambio,Usuario,Referencia,Observaciones,Estatus,Directo,Almacen,AlmacenTransito,OrigenTipo,Origen,OrigenID,Sucursal,SucursalOrigen,SucursalDestino)" + "values ('GNAP','Merma Estiba','" + movId + "',GETDATE(),GETDATE(),2,'Pesos',1,'" + user + "',(select referencia from prod where id=" + id + "),(select observaciones from prod where id=" + id + "),'SINAFECTAR',1,'APT-BUS','(TRANSITO)','PROD','Entrada Produccion','" + op + "'," + sucursal + "," + sucursal + "," + sucursal + ")";//JLMQ estatus estaba como PENDIENTE
                        Ejecuta(insertInv, conn);
                        int idInv = getIdInv();
                        string convertido = convertUnidad2Metro(codigo, rackHuecos);
                        string unidadProd = getUnidad(codigo);
                        centrocosto = getcentrocosto(codigo);
                        string insertInvD = "Insert into INVD (ID,Renglon,RenglonSub,RenglonId,Renglontipo,Cantidad,Almacen,Codigo,Articulo,ContUso,CantidadPendiente,Unidad,Factor,Producto,Tipo,Sucursal)" + "values (" + getIdInv() + "," + renglon + ",0," + rID + ",'N'," + convertido + ",'APT-BUS',null,'" + codigo + "','" + centrocosto + "'," + convertido + ",(select UnidadCompra from Art where Articulo='" + codigo + "'),(select factor from ArtUnidad where Articulo='" + codigo + "' and Unidad = '" + unidadProd + "'),'" + codigo + "','Salida'," + sucursal + ")";
                        Ejecuta(insertInvD, conn);
                        string spAfectarMP = "EXEC spAfectar 'INV', " + idInv + ",'AFECTAR','Todo','Merma Estiba','" + user + "',@Estacion = 99";
                        Ejecuta(spAfectarMP, conn);
                        string estatusUsu = "UPDATE MovEstatusLog SET Estatus = 'CONCLUIDO' WHERE ModuloID = " + idInv + " AND mODULO = 'INV ' AND Sucursal = " + sucursal + " AND Usuario = '" + user + "'";
                        Ejecuta(estatusUsu, conn);
                        string act = "Update Inv set Estatus = 'CONCLUIDO' where id = " + idInv + "";
                        Ejecuta(act, conn);
                        string insertMovFlujo = "Insert into movFlujo values (" + sucursal + ",'GNAP','PROD'," + id + ",'Entrada Produccion','" + op + "','INV'," + idInv + ",'Merma Estiba','" + movId + "',0)";
                        Ejecuta(insertMovFlujo, conn);
                        contador = conteoRenglones;
                        return res;
                    
                    }//if este se queda                       
                    
                }//if
                conn.Close();
                conn2.Close();
            }//try
            catch (Exception e)
            {
                conn.Close();
                conn2.Close();
                string error = e.Message;
                return res = 1;
            }
            return res;
        }

        //Funcion: mermaRecepcionTarima
        //Proposito: afecta las base de datos de intelisis para registrar a nueva merma en inventario.
        public int mermaRecepcionTarima(int rackHuecos, int id, string codigo, string op, int renglon, string user, string sucursal, int nuevo, string epc, string newId)
        {
            int res = 0;
            string[] parametros = getParametros("Intelisis");
            string[] parametros2 = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + "");
            try
            {
                conn2.Open();
                if (rackHuecos > 0 && rackHuecos < nuevo)
                {
                    int cantidadReal = nuevo - rackHuecos;
                    //datos de la produccion, cantidad en curado, renglon y renglonId
                    string selectCurado = "Select prodCurado from CatProd where codigo ='" + codigo + "' and OrdenProduccion ='" + op + "' and id_parcialidad = '" + newId + "'";
                    SqlCommand cmdCurado = new SqlCommand(selectCurado, conn2);
                    SqlDataReader readerCurado = cmdCurado.ExecuteReader();
                    int cantidadCurado = 0, renglonId;
                    if (readerCurado.Read())
                    {
                        //estimaQty 
                        cantidadCurado = readerCurado.GetInt32(0);
                        //renglonId 
                        //renglonId = renglon / 2048;
                    }
                    else
                    {
                        return res = 1;
                    }
                    readerCurado.Close();
                    conn2.Close();
                    conn2.Open();
                    //ahora vamos por la cantidad liberada hasta el momento
                    string selectLiberado = "Select prodLiberado from CatProd where codigo ='" + codigo + "' and OrdenProduccion ='" + op + "' and id_parcialidad = '" + newId + "'";
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
                    conn2.Close();
                    conn2.Open();
                    //por ultimo, lo que se ha mermado hasta ahorita
                    string selectMermas = "Select mermas from CatProd where codigo ='" + codigo + "' and OrdenProduccion ='" + op + "' and id_parcialidad = '" + newId + "'";
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
                    conn2.Close();
                    conn.Open();
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
                    conn.Close();
                    conn.Open();
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
                            conn.Close();
                            //ahora si viene lo chingon, que budha te ampare por que ahora vamos a ver si es directo o no directo.
                            if (!centro.Contains("CURAD") && centroDestino.Contains("CURAD"))//esta es la primera, si el centro no esta en curado, y el destino si tiene curado entonces pelas gallo papá
                            {
                                return res = 1;
                            }
                            else if (!centro.Contains("CURAD") && !centroDestino.Contains("CURAD"))//segunda, si el centro es un patio de produccion y el destino no es un cuarto de curado
                            {
                                //primero dime cuanto quedo en los racks
                                //luego actualizar CatProd
                                string updateCatProd = "UPDATE catProd set prodLiberado = " + nuevoLiberado + ", mermas = " + nuevoMermas + " where Codigo = '" + codigo + "' and OrdenProduccion = '" + op + "' and id_parcialidad = '" + newId + "'";
                                Ejecuta(updateCatProd, conn2);
                                string updateDetEscuadra = "UPDATE detEscuadras set Piezas = " + nuevo + " WHERE EPC = '" + epc + "'";
                                Ejecuta(updateDetEscuadra, conn2);
                                //ahora sigue que actualicemos ProdD
                                string updateProdD = "UPDATE ProdD set CantidadA = " + rackHuecos + " where id = " + id + "and RenglonSub = " + contador + "";
                                Ejecuta(updateProdD, conn);
                                //Generamos la entrada de produccion
                                string generarMerma = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Seleccion', 'Entrada Produccion', '" + user + "', @Estacion = 99";
                                Ejecuta(generarMerma, conn);
                                //afectamos la entrada de produccion generada
                                string afectarMerma = "EXEC spAfectar 'PROD', " + getIdProd() + ", 'AFECTAR', 'Todo', 'Entrada Produccion', '" + user + "', @Estacion = 99";
                                Ejecuta(afectarMerma, conn);
                                //por ultimo se insertan las mermas en Inv e InvD
                                string movId = setMovID("Merma Estiba", sucursal);
                                string insertInv = "Insert into INV (Empresa,Mov,MovID,FechaEmision,UltimoCambio,UEN,Moneda,TipoCambio,Usuario,Referencia,Observaciones,Estatus,Directo,Almacen,AlmacenTransito,OrigenTipo,Origen,OrigenID,Sucursal,SucursalOrigen,SucursalDestino)" + "values ('GNAP','Merma Estiba','" + movId + "',GETDATE(),GETDATE(),2,'Pesos',1,'" + user + "',(select referencia from prod where id=" + id + "),(select observaciones from prod where id=" + id + "),'PENDIENTE',1,'APT-BUS','(TRANSITO)','PROD','Entrada Produccion','" + op + "'," + sucursal + "," + sucursal + "," + sucursal + ")";
                                Ejecuta(insertInv, conn);
                                int idInv = getIdInv();
                                int rID = getRenglonId(renglon, contador);
                                string convertido = convertUnidad2Metro(codigo, rackHuecos);
                                string insertInvD = "Insert into INVD (ID,Renglon,RenglonSub,RenglonId,Renglontipo,Cantidad,Almacen,Codigo,Articulo,CantidadPendiente,Unidad,Factor,Producto,Tipo,Sucursal)" + "values (" + getIdInv() + "," + renglon + ",0," + rID + ",'N'," + convertido + ",'APT-BUS',null,'" + codigo + "'," + convertido + ",(select unidad from ArtUnidad where Articulo='" + codigo + "'),(select factor from Art where Articulo='" + codigo + "' and Unidad = 'M2'),'" + codigo + "','Salida'," + sucursal + ")";
                                Ejecuta(insertInvD, conn);
                                string spAfectarMP = "EXEC spAfectar 'INV', " + idInv + ",'Afectar','Todo','Merma Estiba','" + user + "',@Estacion = 99";
                                Ejecuta(spAfectarMP, conn);
                                string act = "Update Inv set Estatus = 'CONCLUIDO' where id = " + idInv + "";
                                Ejecuta(act, conn);
                                string insertMovFlujo = "Insert into movFlujo values (" + sucursal + ",'GNAP','PROD'," + id + ",'Entrada Produccion','" + op + "','INV'," + idInv + ",'Merma Estiba','" + movId + "',0)";
                                Ejecuta(insertMovFlujo, conn);
                                contador = conteoRenglones;
                                return res;
                            }
                            else if (centro.Contains("CURAD") && !centroDestino.Contains("CURADO")) //son los productos que ya se avanzaron
                            {
                                //primero dime cuanto quedo en los racks
                                //luego actualizar CatProd
                                string updateCatProd = "UPDATE catProd set prodLiberado = " + nuevoLiberado + ", mermas = " + nuevoMermas + " where Codigo = '" + codigo + "' and OrdenProduccion = '" + op + "' and id_parcialidad = '" + newId + "'";
                                Ejecuta(updateCatProd, conn2);
                                string updateDetEscuadra = "UPDATE detEscuadras set Piezas = " + nuevo + " WHERE EPC = '" + epc + "'";
                                Ejecuta(updateDetEscuadra, conn2);
                                //ahora sigue que actualicemos ProdD
                                string updateProdD = "UPDATE ProdD set CantidadA = " + rackHuecos + " where id = " + id + "and RenglonSub = " + contador + "";
                                Ejecuta(updateProdD, conn);
                                //Generamos la entrada de produccion
                                string generarMerma = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Seleccion', 'Entrada Produccion', '" + user + "', @Estacion = 99";
                                Ejecuta(generarMerma, conn);
                                //afectamos la entrada de produccion generada
                                string afectarMerma = "EXEC spAfectar 'PROD', " + getIdProd() + ", 'AFECTAR', 'Todo', 'Entrada Produccion', '" + user + "', @Estacion = 99";
                                Ejecuta(afectarMerma, conn);//por ultimo se insertan las mermas en Inv e InvD
                                string movId = setMovID("Merma Estiba", sucursal);
                                string insertInv = "Insert into INV (Empresa,Mov,MovID,FechaEmision,UltimoCambio,UEN,Moneda,TipoCambio,Usuario,Referencia,Observaciones,Estatus,Directo,Almacen,AlmacenTransito,OrigenTipo,Origen,OrigenID,Sucursal,SucursalOrigen,SucursalDestino)" + "values ('GNAP','Merma Estiba','" + movId + "',GETDATE(),GETDATE(),2,'Pesos',1,'" + user + "',(select referencia from prod where id=" + id + "),(select observaciones from prod where id=" + id + "),'PENDIENTE',1,'APT-BUS','(TRANSITO)','PROD','Entrada Produccion','" + op + "'," + sucursal + "," + sucursal + "," + sucursal + ")";
                                Ejecuta(insertInv, conn);
                                int idInv = getIdInv();
                                int rID = getRenglonId(renglon, contador);
                                string convertido = convertUnidad2Metro(codigo, rackHuecos);
                                string insertInvD = "Insert into INVD (ID,Renglon,RenglonSub,RenglonId,Renglontipo,Cantidad,Almacen,Codigo,Articulo,CantidadPendiente,Unidad,Factor,Producto,Tipo,Sucursal)" + "values (" + getIdInv() + "," + renglon + ",0," + rID + ",'N'," + convertido + ",'APT-BUS',null,'" + codigo + "'," + convertido + ",(select unidad from Art where Articulo='" + codigo + "'),(select factor from ArtUnidad where Articulo='" + codigo + "' and Unidad = 'M2'),'" + codigo + "','Salida'," + sucursal + ")";
                                Ejecuta(insertInvD, conn);
                                string spAfectarMP = "EXEC spAfectar 'INV', " + idInv + ",'Afectar','Todo','Merma Estiba','" + user + "',@Estacion = 99";
                                Ejecuta(spAfectarMP, conn);
                                string act = "Update Inv set Estatus = 'CONCLUIDO' where id = " + idInv + "";
                                Ejecuta(act, conn);
                                string insertMovFlujo = "Insert into movFlujo values (" + sucursal + ",'GNAP','PROD'," + id + ",'Entrada Produccion','" + op + "','INV'," + idInv + ",'Merma Estiba','" + movId + "',0)";
                                Ejecuta(insertMovFlujo, conn);
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
                else if (rackHuecos > nuevo)
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
                conn.Close();
                conn2.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                conn2.Close();
                string error = e.Message;
                return res = 1;
            }
            return res;
        }

        //Funcion: noHuecos
        //Proposito: marca los racks como contados en caso de que no haya huecos en dicho rack.
        public int noHuecos(int huecos, int actual, int id, string op, string codigo, string user, string sucursal)
        {
            int res = 0;
            string[] parametros = getParametros("Intelisis");
            string[] parametros2 = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + "");
            try
            {
                if (huecos > 0 && huecos < actual)
                {
                    int diferencia = actual - huecos;
                    conn2.Open();
                    string selectCurado = "Select prodCurado,Renglon from CatProd where codigo ='" + codigo + "' and OrdenProduccion ='" + op + "'";
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
                        //renglonId = renglon / 2048;
                    }
                    else
                    {
                        return res = 1;
                    }
                    readerCurado.Close();
                    conn2.Close();
                    conn2.Open();
                    //ahora vamos por la cantidad liberada hasta el momento
                    string selectLiberado = "Select prodLiberado, prodRestante, prodAsignado from CatProd where codigo ='" + codigo + "' and OrdenProduccion ='" + op + "'";
                    SqlCommand cmdLiberado = new SqlCommand(selectLiberado, conn2);
                    SqlDataReader readerLiberado = cmdLiberado.ExecuteReader();
                    int cantidadLiberado = 0, cantidadRestante = 0;
                    if (readerLiberado.Read())
                    {
                        cantidadLiberado = readerLiberado.GetInt32(0);
                        cantidadRestante = readerLiberado.GetInt32(1);
                    }
                    else
                    {
                        return res = 1;
                    }
                    conn2.Close();
                    conn2.Open();
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
                    conn2.Close();
                    conn.Open();
                    //conteo de renglones
                    string selectRenglonSub = "select count(renglonsub) from ProdD where Renglon ='" + renglon + "'";
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
                    readerRenglonSub.Close();
                    conn.Close();
                    conn.Open();

                    while (contador < conteoRenglones)
                    {
                        int nuevoCurado = cantidadCurado - actual, nuevoLiberado = cantidadLiberado + diferencia, nuevoMermas = cantidadMerma + huecos, nuevoRestante = cantidadRestante - cantidadLiberado;
                        string selectCentroDestino = "SELECT Centro,CentroDestino from ProdD where id = " + id + " and Renglon = " + renglon + " and RenglonSub = " + contador;
                        SqlCommand cmdDestino = new SqlCommand(selectCentroDestino, conn);
                        SqlDataReader readerDestino = cmdDestino.ExecuteReader();
                        string centro = "", centroDestino = "";
                        if (readerDestino.Read())
                        {
                            centro = readerDestino.GetValue(0).ToString();
                            centroDestino = readerDestino.GetValue(1).ToString();
                            conn.Close();
                            //ahora si viene lo chingon, que budha te ampare por que ahora vamos a ver si es directo o no directo.
                            if (!centro.Contains("CURAD") && centroDestino.Contains("CURAD"))//esta es la primera, si el centro no esta en curado, y el destino si tiene curado entonces pelas gallo papá
                            {
                                return res = 1;
                            }
                            else if (!centro.Contains("CURAD") && !centroDestino.Contains("CURAD"))//segunda, si el centro es un patio de produccion y el destino no es un cuarto de curado
                            {
                                //primero dime cuanto quedo en los racks
                                //luego actualizar CatProd
                                string updateCatProd = "UPDATE catProd set prodCurado = " + nuevoCurado + ", prodLiberado = " + nuevoLiberado + ", mermas = " + nuevoMermas + ", prodRestante = " + cantidadRestante + " where Codigo = '" + codigo + "' and OrdenProduccion = '" + op + "'";
                                Ejecuta(updateCatProd, conn2);
                                //ahora sigue que actualicemos ProdD
                                string updateProdD = "UPDATE ProdD set CantidadA = " + huecos + " where id = " + id + " and RenglonSub = " + contador + " and Articulo = '" + codigo + "'";
                                Ejecuta(updateProdD, conn);
                                //Generamos la entrada de produccion
                                string generarMerma = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Seleccion', 'Entrada Produccion', '" + user + "', @Estacion = 99";
                                Ejecuta(generarMerma, conn);
                                //afectamos la entrada de produccion generada
                                string afectarMerma = "EXEC spAfectar 'PROD', " + getIdProd() + ", 'AFECTAR', 'Todo', NULL, '" + user + "', @Estacion = 99";
                                Ejecuta(afectarMerma, conn);
                                int rID = getRenglonId(renglon, contador);
                                //por ultimo se insertan las mermas en Inv e InvD
                                string movId = setMovID("Merma Produccion", sucursal);
                                string insertInv = "Insert into INV (Empresa,Mov,MovID,FechaEmision,UltimoCambio,UEN,Moneda,TipoCambio,Usuario,Referencia,Observaciones,Estatus,Directo,Almacen,AlmacenTransito,OrigenTipo,Origen,OrigenID,Sucursal,SucursalOrigen,SucursalDestino)" + "values ('GNAP','Merma Produccion','" + movId + "',GETDATE(),GETDATE(),2,'Pesos',1,'" + user + "',(select referencia from prod where id=" + id + "),(select observaciones from prod where id=" + id + "),'PENDIENTE',1,'APT-BUS','(TRANSITO)','PROD','Entrada Produccion','" + op + "'," + sucursal + "," + sucursal + "," + sucursal + ")";
                                Ejecuta(insertInv, conn);
                                int idInv = getIdInv();
                                string convertido = convertUnidad2Metro(codigo, huecos);
                                string insertInvD = "Insert into INVD (ID,Renglon,RenglonSub,RenglonId,Renglontipo,Cantidad,Almacen,Codigo,Articulo,CantidadPendiente,Unidad,Factor,Producto,Tipo,Sucursal)" + "values (" + getIdInv() + "," + renglon + ",0," + rID + ",'N'," + convertido + ",'APT-BUS',null,'" + codigo + "'," + convertido + ",(select unidad from Art where Articulo='" + codigo + "'),(select factor from ArtUnidad where Articulo='" + codigo + "' and Unidad = 'M2'),'" + codigo + "','Salida'," + sucursal + ")";
                                Ejecuta(insertInvD, conn);
                                string spAfectarMP = "EXEC spAfectar 'INV', " + idInv + ",'Afectar','Todo','Merma Produccion','" + user + "',@Estacion = 99";
                                Ejecuta(spAfectarMP, conn);
                                string act = "Update Inv set Estatus = 'CONCLUIDO' where id = " + idInv + "";
                                Ejecuta(act, conn);
                                string insertMovFlujo = "Insert into movFlujo values (" + sucursal + ",'GNAP','PROD'," + id + ",'Entrada Produccion','" + op + "','INV'," + idInv + ",'Merma Produccion','" + movId + "',0)";
                                Ejecuta(insertMovFlujo, conn);
                                contador = conteoRenglones;
                                return res;
                            }
                            else if (centro.Contains("CURAD") && !centroDestino.Contains("CURADO")) //son los productos que ya se avanzaron
                            {
                                //primero dime cuanto quedo en los racks
                                //string updateDetRprod = "UPDATE DetRPRod set CantidadReal = " + cantidadReal + " where EPC = '" + epc + "'";
                                //SqlCommand cmdDetRProd = new SqlCommand(updateDetRprod, conn2);
                                //cmdDetRProd.ExecuteNonQuery();
                                //conn2.Close();
                                //conn2.Open();
                                ////luego actualizar CatProd
                                string updateCatProd = "UPDATE catProd set prodCurado = " + nuevoCurado + ", prodLiberado = " + nuevoLiberado + ", mermas = " + nuevoMermas + ", prodRestante = " + cantidadRestante + " where Codigo = '" + codigo + "' and OrdenProduccion = '" + op + "'";
                                Ejecuta(updateCatProd, conn2);
                                //ahora sigue que actualicemos ProdD
                                string updateProdD = "UPDATE ProdD set CantidadA = " + huecos + " where id = " + id + " and RenglonSub = " + contador + " and Articulo = '" + codigo + "'";
                                Ejecuta(updateProdD, conn);
                                //Generamos la entrada de produccion
                                string generarMerma = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Seleccion', 'Entrada Produccion', '" + user + "', @Estacion = 99";
                                Ejecuta(generarMerma, conn);
                                //afectamos la entrada de produccion generada
                                string afectarMerma = "EXEC spAfectar 'PROD', " + getIdProd() + ", 'AFECTAR', 'Todo', 'Entrada Produccion', '" + user + "', @Estacion = 99";
                                Ejecuta(afectarMerma, conn);
                                int rID = getRenglonId(renglon, contador);
                                //por ultimo se insertan las mermas en Inv e InvD
                                string movId = setMovID("Merma Produccion", sucursal);
                                string insertInv = "Insert into INV (Empresa,Mov,MovID,FechaEmision,UltimoCambio,UEN,Moneda,TipoCambio,Usuario,Referencia,Observaciones,Estatus,Directo,Almacen,AlmacenTransito,OrigenTipo,Origen,OrigenID,Sucursal,SucursalOrigen,SucursalDestino)" + "values ('GNAP','Merma Produccion','" + movId + "',GETDATE(),GETDATE(),2,'Pesos',1,'" + user + "',(select referencia from prod where id=" + id + "),(select observaciones from prod where id=" + id + "),'PENDIENTE',1,'APT-BUS','(TRANSITO)','PROD','Entrada Produccion','" + op + "'," + sucursal + "," + sucursal + "," + sucursal + ")";
                                Ejecuta(insertInv, conn);
                                int idInv = getIdInv();
                                string convertido = convertUnidad2Metro(codigo, huecos);
                                string insertInvD = "Insert into INVD (ID,Renglon,RenglonSub,RenglonId,Renglontipo,Cantidad,Almacen,Codigo,Articulo,CantidadPendiente,Unidad,Factor,Producto,Tipo,Sucursal)" + "values (" + getIdInv() + "," + renglon + ",0," + rID + ",'N'," + convertido + ",'APT-BUS',null,'" + codigo + "'," + convertido + ",(select unidad from Art where Articulo='" + codigo + "'),(select factor from ArtUnidad where Articulo='" + codigo + "' and Unidad = 'M2'),'" + codigo + "','Salida'," + sucursal + ")";
                                Ejecuta(insertInvD, conn);
                                string spAfectarMP = "EXEC spAfectar 'INV', " + idInv + ",'Afectar','Todo','Merma Produccion','" + user + "',@Estacion = 99";
                                Ejecuta(spAfectarMP, conn);
                                string act = "Update Inv set Estatus = 'CONCLUIDO' where id = " + idInv + "";
                                Ejecuta(act, conn);
                                string insertMovFlujo = "Insert into movFlujo values (" + sucursal + ",'GNAP','PROD'," + id + ",'Entrada Produccion','" + op + "','INV'," + idInv + ",'Merma Produccion','" + movId + "',0)";
                                Ejecuta(insertMovFlujo, conn);
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
                else if (huecos == 0)//JLMQ NAPRESA 15 AGO 2016 A PARTIR DE AQUI FALLA AL REPORTAR MERMAS
                {
                    int cantidadReal = actual;
                    //datos de la produccion, cantidad en curado, renglon y renglonId
                    string selectCurado = "Select prodCurado,Renglon from CatProd where codigo ='" + codigo + "' and OrdenProduccion ='" + op + "'";
                    SqlCommand cmdCurado = new SqlCommand(selectCurado, conn2);
                    conn2.Open();
                    SqlDataReader readerCurado = cmdCurado.ExecuteReader();
                    int cantidadCurado = 0, renglon, renglonId;
                    if (readerCurado.Read())
                    {
                        //estimaQty 
                        cantidadCurado = readerCurado.GetInt32(0);
                        //renglon 
                        renglon = readerCurado.GetInt32(1);
                        //renglonId 
                        //renglonId = renglon / 2048;
                    }
                    else
                    {
                        return res = 1;
                    }
                    conn2.Close();
                    conn2.Open();
                    //ahora vamos por la cantidad liberada hasta el momento
                    string selectLiberado = "Select prodLiberado, prodRestante, prodAsignado from CatProd where codigo ='" + codigo + "' and OrdenProduccion ='" + op + "'";
                    SqlCommand cmdLiberado = new SqlCommand(selectLiberado, conn2);
                    SqlDataReader readerLiberado = cmdLiberado.ExecuteReader();
                    int cantidadLiberado = 0, cantidadRestante = 0;
                    if (readerLiberado.Read())
                    {
                        cantidadLiberado = readerLiberado.GetInt32(0);
                        cantidadRestante = readerLiberado.GetInt32(1);
                    }
                    else
                    {
                        return res = 1;
                    }
                    conn2.Close();
                    int nuevoCurado = cantidadCurado - actual, nuevoLiberado = cantidadLiberado + actual, nuevoRestante = cantidadRestante - actual;
                    string updateCatProd = "UPDATE catProd set prodCurado = " + nuevoCurado + ", prodLiberado = " + nuevoLiberado + ", prodRestante = " + nuevoRestante + " where Codigo = '" + codigo + "' and OrdenProduccion = '" + op + "'";
                    Ejecuta(updateCatProd, conn2);
                    return res = 3;
                }
                else
                {
                    return res = 1;
                }
            }
            catch (Exception e)
            {
                conn.Close();
                conn2.Close();
                string error = e.Message;
                return res = 1;
            }
            return res;
        }

        //Funcion: pickingEscuadra
        //Proposito: muestra las escuadras segun sea el caso (1 = escuadras para picking, 2 = escuadras para embarque, 3 = escuadras embarcadas)
        public DataTable pickingEscuadraWDR(string op, int valor)
        {
           
            DataTable ds = new DataTable();
                    
          
                   
          
            try
            {
                if (valor == 1)//al parecer nunca se usa.
                {
                    string select = "SELECT DISTINCT de.EPC, de.OrdenProduccion, de.Pedido, de.CodigoProducto, de.Piezas, de.Posicion FROM detRemision dr INNER JOIN DetEscuadras de ON de.Pedido = dr.Pedido WHERE dr.Pedido = (SELECT Top(1) Pedido FROM detRemision where Remision = '" + op.Trim() + "') AND de.Asignado = 1 and de.Ubicada=1 and de.Picked = 0";
                    //string select = "select distinct de.epc, de.OrdenProduccion, cp.Pedido, de.CodigoProducto, cp.Descripcion, de.Piezas, cp.Cliente from DetEscuadras de inner join catProd cp on cp.Codigo = de.CodigoProducto where de.OrdenProduccion = '" + remision[0] + "' and cp.Pedido = '" + remision[1] + "' and de.Asignado = 1 and de.Ubicada=1 and de.Picked = 0";
                    //string select = "(select ei.epc, ei.OrdenProduccion, cp.Pedido, ei.Codigo, cp.Descripcion, ei.CantidadTarima as Piezas, cp.Cliente from Etiquetas_Impresas ei inner join catProd cp on cp.Codigo = ei.Codigo where ei.OrdenProduccion = '" + remision[0] + "' and ei.Pedido = '" + remision[1] + "' and ei.Asignado = 1 and ei.Ubicada=1 and ei.Picked = 0) union all (select de.epc, de.OrdenProduccion, cp.Pedido, de.CodigoProducto as Codigo, cp.Descripcion, de.Piezas, cp.Cliente from DetEscuadras de inner join catProd cp on cp.Codigo = de.CodigoProducto where de.OrdenProduccion = '" + remision[0] + "' and cp.Pedido = '" + remision[1] + "' and de.Asignado = 1 and de.Ubicada=1 and de.Picked = 0)";
                    //string select = "select ei.epc, ei.OrdenProduccion, cp.Pedido, ei.Codigo, cp.Descripcion, ei.CantidadTarima as Piezas, cp.Cliente from Etiquetas_Impresas ei inner join catProd cp on cp.Codigo = ei.Codigo where ei.OrdenProduccion = '" + remision[0] + "' and ei.Pedido = '" + remision[1] + "' and ei.Asignado = 1 and ei.Ubicada=1 and ei.Picked = 0";
                    ds = getDatasetConexionWDR(select, "Solutia");
                }
                else if (valor == 2)
                {
                    string codProd = "";
                    string[] parametros = getParametros("Solutia");
                    SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
                    string codigoProd = "select CodigoProducto from DetEscuadras where pedido = (select pedido from detRemision where Remision = '" + op + "')";
                    
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(codigoProd, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        codProd = reader.GetString(0);
                    }
                    conn.Close();
                    string select = "SELECT de.EPC, de.OrdenProduccion, de.Pedido, de.CodigoProducto, dr.PzaRemision, de.Posicion FROM detRemision dr INNER JOIN DetEscuadras de ON de.Pedido = dr.Pedido AND de.CodigoProducto = dr.CodigoProducto and de.Piezas=dr.PzaRemision WHERE dr.Remision = '" + op + "' AND de.Ubicada=1 and de.Embarcado = 0";
                        //string select = " EXEC piezas_remision  '" + op.Trim() + "'";
                    ds = getDatasetConexionWDR(select, "Solutia");
                }
                else if (valor == 3)
                {
                    string[] arreglo = op.Trim().Split(",".ToCharArray());//JLMQ
                    String cadenaIn = "";
                    //"A,B,C,D" -> 'A','B','C','D'
                    for (int i = 0; i < arreglo.Length; i++)
                    {
                        cadenaIn += "'" + arreglo[i] + "'";
                        if (arreglo.Length - 1 > i)
                        {
                            cadenaIn += ",";
                            cadenaIn = cadenaIn.Trim();
                        }
                    }
                    //string select = "SELECT DISTINCT de.EPC, de.OrdenProduccion, de.Pedido,dr.CantidadPza AS Piezas, de.CodigoProducto, de.Posicion FROM detRemision dr INNER JOIN DetEscuadras de ON de.Pedido = dr.Pedido WHERE dr.Pedido In (SELECT Pedido FROM detRemision where Remision in (" + cadenaIn + ")) AND de.Asignado = 1 and de.Ubicada=1 and de.Embarcado = 1 AND dr.CodigoProducto = de.CodigoProducto";//dr.PzaRemision se quito
                    string select = "SELECT DISTINCT "+
	                                    "de.EPC, de.OrdenProduccion, "+
	                                    "de.Pedido, "+
	                                    "dr.PzaRemision AS Piezas, "+
	                                    "de.CodigoProducto, "+
	                                    "de.Posicion "+
                                    "FROM detRemision dr "+
	                                    "INNER JOIN DetEscuadras de "+
	                                    "ON de.newIdEscuadra = dr.idRemi "+
                                    "WHERE "+
	                                    "de.Asignado = 1 "+
	                                    "AND de.OrdenProduccion = 'SIN OP' "+
	                                    "AND de.Ubicada=1 "+
	                                    "AND de.Embarcado = 1 "+
	                                    "AND dr.conEscuadra = 1 "+
	                                    "AND dr.PzasRemiCompletas = 1 "+
	                                    "AND dr.idRemi = de.newIdEscuadra "+
                                        "AND dr.Remision = '"+op+"'";
                    ds = getDatasetConexionWDR(select, "Solutia");
                }
                else
                {
                    return null;
                }
            }
            catch (SqlException e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return null;
            }
            return ds;
        }

        //Funcion: getProdGranel
        //Proposito: regresa las "escuadras" de los productos que exclusivamente se manejan a granel
        public DataTable getProdGranel()
        {
            DataTable ds = new DataTable();
            try
            {
                string select = "SELECT de.EPC, ca.[Descripción], de.Piezas FROM DetEscuadras de INNER JOIN catArt ca on ca.Clave = de.EPC";
                ds = getDatasetConexionWDR(select, "Solutia");
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return null;
            }
            return ds;
        }

        //Funcion: getStockRemision
        //Proposito: muestra las escuadras disponibles EN STOCK que pueden usarse para surtir un pedido
        public DataTable getStockRemision(string remision)
        {
            DataTable ds = new DataTable();
            try
            {
                string select = "SELECT DISTINCT de.EPC, de.OrdenProduccion, de.Pedido, de.CodigoProducto, de.Piezas, de.Posicion FROM detRemision dr INNER JOIN DetEscuadras de ON de.CodigoProducto = dr.CodigoProducto WHERE de.OrdenProduccion = 'STOCK' and de.CodigoProducto IN (SELECT CodigoProducto FROM detRemision where Remision = '" + remision + "')";
                ds = getDatasetConexionWDR(select, "Solutia");
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return null;
            }
            return ds;
        }

        //Funcion: rackAsignados
        //Proposito: regresa la cantidad de píezas por ventana de un rack segun el producto. si regresa 0 es porque se v a trabajar con un producto que se produce sin rack
        public int racksAsignados(string codigo)
        {
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");

            try
            {
                conn.Open();
                string getPxT = "select PxT from catArt where Clave = '" + codigo + "'";
                SqlCommand cmdPxT = new SqlCommand(getPxT, conn);
                SqlDataReader readPxT = cmdPxT.ExecuteReader();
                int PxT = 0;
                if (readPxT.Read())
                {
                    PxT = int.Parse(readPxT.GetValue(0).ToString());
                    conn.Close();
                    return PxT;
                }
                else
                {
                    conn.Close();
                    return -404;
                }

            }
            catch (Exception e)
            {
                conn.Close();
                return -1;
            }
        }

        //Funcion: remisionEmbarque
        //Proposito: 0 = la remision esta embarcada, 1 = la remision NO esta embarcada
        public int remisionEmbarque(string remision)
        {
            int res = 0;
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                string select = "SELECT * FROM Embarque where Referencia = '" + remision + "'";                
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                }
                else
                {
                    res = 1;
                }
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                res = 1;
            }
            return res;
        }

        //Funcion: setContado
        //Proposito: marca como contado el rack recien leido
        public int setContado(string epc, int real)
        {
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            int res = 0;
            try
            {
                conn.Open();
                string update = "UPDATE DetRProd set ContadoProd = 1, cantidadReal = " + real + " where EPC = '" + epc + "'";
                SqlCommand cmd = new SqlCommand(update, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                res = 1;
            }
            return res;
        }

        //Funcion: setContadoCurado
        //Proposito: marca como contado el rack recien leido. (conteo de huecos)
        public int setContadoCurado(string epc, int real)
        {
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            int res = 0;
            try
            {
                conn.Open();
                string update = "UPDATE DetRProd set ContadoCurado = 1, cantidadReal = " + real + " where EPC = '" + epc + "'";
                SqlCommand cmd = new SqlCommand(update, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                res = 1;
            }
            return res;
        }

        //Funcion: showProdWDR
        //Proposito: regresa todas las ordenes asignadas par amostrarlas en un gridview
        public DataTable showProdWDR(int filtro)
        {
            string select = "";
            switch (filtro)
            {
                default:
                    select = "select Id,OrdenProduccion,Pedido,Cliente,Descripcion,Tipo,Medida,id_parcialidad,Codigo,Cantidad,Estatus,Renglon,Asignado,prodEstibado,prodAsignado,prodCurado,prodLiberado,mermas,Color from catProd where Estatus != 'CONCLUIDO' and Asignado = 1";
                    return getDatasetConexionWDR(select, "Solutia");
                case 1://Solo Ordenes Pendientes
                    select = "select Id,OrdenProduccion,Pedido,Cliente,Descripcion,Tipo,Medida,id_parcialidad,Codigo,Cantidad,Estatus,Renglon,Asignado,prodEstibado,prodAsignado,prodCurado,prodLiberado,mermas,Color from catProd where Estatus = 'PENDIENTE' and Asignado = 1";
                    return getDatasetConexionWDR(select, "Solutia");
                case 2://Solo Ordenes En Curado
                    select = "select Id,OrdenProduccion,Pedido,Cliente,Descripcion,Tipo,Medida,id_parcialidad,Codigo,Cantidad,Estatus,Renglon,Asignado,prodEstibado,prodAsignado,prodCurado,prodLiberado,mermas,Color from catProd where Estatus = 'CURADO' and Asignado = 1";
                    return getDatasetConexionWDR(select, "Solutia");
                case 3://Solo Ordenes Liberado
                    select = "select Id,OrdenProduccion,Pedido,Cliente,Descripcion,Tipo,Medida,id_parcialidad,Codigo,Cantidad,Estatus,Renglon,Asignado,prodEstibado,prodAsignado,prodCurado,prodLiberado,mermas,Color from catProd where Estatus = 'LIBERADO' and Asignado = 1";
                    return getDatasetConexionWDR(select, "Solutia");
            }
        }

        //Funcion: showProdCompWDR
        //Proposito: muestra las escuadras que ya estan asignadas a producto terminado pero aun no han sido ubicadas en el patio.
        public DataTable showProdCompWDR()
        {
            string select = "Select IdEscuadra as [Escuadra #], EPC as EPC, OrdenProduccion as [Orden de Produccion], CodigoProducto as Codigo, Piezas, newIdEscuadra from DetEscuadras where Asignado = 1 and Ubicada=0";
            return getDatasetConexionWDR(select, "Solutia");
        }

        //Funcion: showProdGranelWDR
        //Proposito: muestras el producto a granel pendiente de ubicar
        public DataTable showProdGranelWDR()
        {
            string select = "Select IdEscuadra as [Escuadra #], EPC as EPC, OrdenProduccion as [Orden de Produccion], CodigoProducto as Codigo, Piezas, newIdEscuadra from DetEscuadras where Posicion like 'A%' and OrdenProduccion <> 'GRANEL'";
            return getDatasetConexionWDR(select, "Solutia");
        }

        //Funcion: showProdGeneralWDR
        //Proposito: muestra las tarimas pendientes de ubicar
        public DataTable showProdGeneralWDR()
        {
            string select = "Select IdEscuadra as [Escuadra #], EPC as EPC, OrdenProduccion as [Orden de Produccion], CodigoProducto as Codigo, Piezas from DetEscuadras where (Asignado = 1 and Ubicada=0) or (Posicion like 'A%' and OrdenProduccion <> 'GRANEL' AND Ubicada = 0)";
            return getDatasetConexionWDR(select, "Solutia");
        }

        //Funcion: showProdGeneralWDR
        //Proposito: muestra las tarimas pendientes de ubicar
        public DataTable Esc_Remi(string codigo)
        {
            string select = "Select IdEscuadra as [Escuadra #], EPC as EPC, OrdenProduccion as [Orden de Produccion], CodigoProducto as Codigo, Piezas from DetEscuadras where Asignado = 1 and Ubicada = 1 and CodigoProducto = '" + codigo + "'";
            return getDatasetConexionWDR(select, "Solutia");
        }

        //Funcion: sizeList
        //Proposito: regresa el tamaño de una lista
        public int sizeList(string OP, int evnt, string codprod, string renglon, string newId)
        {
            List<cRack> racks = new List<cRack>();
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            conn.Open();
            string select = "";
            if (evnt == 0)
                select = "Select dp.EPC,dp.Numero,rp.Modelo as Modelo,dp.OrdenProduccion,dp.CantidadEstimada as [C.Estimada], dp.CantidadReal as [C.Real] from DetRProd dp inner join RProduccion rp on rp.IdRack = dp.IdRProd where OrdenProduccion = '" + OP + "' and Estado = 1 and Verificado = 1 and ContadoProd = 0 and CodigoProducto = '" + codprod + "' and Renglon = " + renglon + " and newId_Parcialidad = '"+ newId +"' ";
            if (evnt == 1)
                select = "Select dp.EPC,dp.Numero,rp.Modelo as Modelo,dp.OrdenProduccion,dp.CantidadEstimada as [C.Estimada], dp.CantidadReal as [C.Real] from DetRProd dp inner join RProduccion rp on rp.IdRack = dp.IdRProd where OrdenProduccion = '" + OP + "' and Estado = 1 and Verificado = 1 and ContadoProd = 1 and ContadoCurado = 0 and CodigoProducto = '" + codprod + "' and Renglon = " + renglon + " and newId_Parcialidad = '" + newId + "'";
            SqlCommand cmd = new SqlCommand(select, conn);
            SqlDataReader ded = cmd.ExecuteReader();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            while (ded.Read())
            {
                cRack cr = new cRack();
                cr.EPC = ded.GetString(0);
                cr.numero = int.Parse(ded.GetValue(1).ToString());
                cr.modelo = ded.GetString(2);
                cr.ordenProduccion = ded.GetString(3);
                cr.cantidadEstimada = ded.GetInt32(4);
                cr.cantidadReal = ded.GetInt32(5);
                racks.Add(cr);
            }
            conn.Close();
            int size = 0;
            size = racks.Count();
            return size;
        }

        //Funcion: tarimasAsignadas
        //Proposito: regresa la cantidad maxima por tarima que puede haber de un producto
        public int tarimasAsignadas(string codigo)
        {
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                string getPxT = "select PzxT from catArt where Clave = '" + codigo + "'";
                SqlCommand cmdPxT = new SqlCommand(getPxT, conn);
                SqlDataReader readPxT = cmdPxT.ExecuteReader();
                int PxT = 0;
                if (readPxT.Read())
                {
                    PxT = int.Parse(readPxT.GetValue(0).ToString());
                    conn.Close();
                    return PxT;
                }
                else
                {
                    conn.Close();
                    return -1;
                }
            }
            catch (FormatException e)
            {
                conn.Close();
                return -2;
            }
            catch (Exception e)
            {
                conn.Close();
                return -1;
            }
        }

        //Funcion: ubicaEscuadra
        //Proposito: asigna la ubicacion en el patio a una escuadra
        //public int ubicaEscuadra(string epcEscuadra, string codigo, string cantidad, string epcBandera, string codigoBandera,
        //    int rack, int tarima)
        public bool ubicaEscuadra(int idTag, string [] infoEsc, string zonaAlm)
        {
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            
            bool res = false;
            string OP = infoEsc[0];
            string Codigo = infoEsc[1];
            int cantidad = Convert.ToInt32(infoEsc[2]);
            string ubicacion = zonaAlm;
            string lote = infoEsc[4];

            try
            {
                conn.Open();
                //string updateEsc = "UPDATE DetEscuadras "+ 
                //                    "SET "+
                //                        "OrdenProduccion = '"+OP+"',"+
                //                        "Asignado = 1,"+
                //                        "Ubicada = 1,"+
                //                        "CodigoProducto = '"+Codigo+"',"+
                //                        "Piezas = "+cantidad+","+
                //                        "Posicion = '"+ubicacion+"',"+
                //                        "newIdEscuadra = (SELECT idNew FROM catProdD WHERE Lote = '"+lote+"'),"+
                //                        "Lote = '"+lote+"'"+
                //                    "WHERE EPC = '"+epcEscuadra+"' ";
                string updateEsc = "UPDATE DetEscuadras " +
                                    "SET " +
                                        "OrdenProduccion = '" + OP + "'," +
                                        "Asignado = 1," +
                                        "Ubicada = 1," +
                                        "CodigoProducto = '" + Codigo + "'," +
                                        "Piezas = " + cantidad + "," +
                                        "Posicion = '" + ubicacion + "'," +
                                        "newIdEscuadra = (SELECT TOP(1) idNew FROM catProdD WHERE Lote = '" + lote + "')," +
                                        "Lote = '" + lote + "'" +
                                    " WHERE idEscuadra = " + idTag + " ";
                conn.Close();
                if(Ejecuta(updateEsc, conn))
                            res = true;
                else
                    res = false;
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

        //Funcion: verificaOP
        //Proposito: valida que la orden de produccion exista y tenga sus ordenes de transferencia completas
        public int verificaOP(string OP)
        {
            int result = 0;
            int count = 0;
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                string select = "Select Id,Mov,MovID,Estatus From Inv Where  id In (Select Did From MovFlujo Where Omodulo = 'PROD' And OMovID = '" + OP + "'  And Dmov = 'Orden Transferencia') and Estatus != 'CONCLUIDO'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    count = count + 1;
                }
                result = count;
                //verifica si hay ordenes de transferencia pendientes
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }

            return result;
        }

        //Funcion: verificaRacks
        //Proposito: Actualiza la base de datos para marcar un Rack como Bautizado.
        public String verificaRacks(String EPC)
        {
            string result = "null";
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                string update = "update DetRProd set Verificado = 1 where EPC ='" + EPC + "'";
                conn.Open();

                SqlCommand cmd = new SqlCommand(update, conn);
                cmd.ExecuteNonQuery();

                result = "Rack Verificado";
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                result = e.Message;
            }
            return result;
        }

        //Funcion: Libera
        //Proposito: actualiza una orden de produccion como LIBERADA indicando que ha finalizado el proceso actual.
        public string Libera(string Estatus, string renglon, string newId)
        {
            string[] parametros2 = getParametros("Solutia");//A ESTE METODO AGREGAR AND prodCurado = LA CANTIDAD QUE SE PUSO EN CURADO
            SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + "");
            conn2.Open();
            string result = "";

            try
            {
                string update = "update catProd set Estatus = '"+ Estatus +"' where id_parcialidad = '"  + newId + "'";
                SqlCommand cmd = new SqlCommand(update, conn2);
                cmd.ExecuteNonQuery();
                result = "Cambio Exitoso";
                conn2.Close();
            }
            catch (Exception e)
            {
                conn2.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return result;
        }

        //Funcion: TarimasAsignadas
        //Proposito: regresa la cantidad de tarimas asignadas a una orden de produccion.
        public int TarimasAsignadas(string op)
        {
            int res = -1;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
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
                    conn.Close();
                    return res;
                }

                if (res == 0)
                    res = res + 1;
                else
                {
                }
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return res = -1;
            }
            return res;
        }

        //Funcion: embarqueRestante
        //Proposito: regresa la cantidad que falta por cargarse en una remisión
        public int embarqueRestante(string remision, string epc)
        {
            int res = 0;
            try
            {
                string[] escuadraD = getEscuadraD(epc);
                int remisionado = int.Parse(getCantidadRemision(remision, escuadraD[1]));
                res = remisionado - int.Parse(escuadraD[2]);
                return res;
            }
            catch (Exception e)
            {
                return res;
            }
        }

        //Funcion: rackInfo
        //Proposito: regresa un array con el detalle de un rack en especifico.
        public string[] rackInfo(string epc)
        {
            string[] res = new string[9];
            string[] rack = new string[6];
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                string selectRack = "select Numero, IdRProd, OrdenProduccion, CantidadEstimada, CantidadReal, CodigoProducto from DetRProd where EPC = '" + epc + "'";
                conn.Open();
                SqlCommand cmdRack = new SqlCommand(selectRack, conn);
                SqlDataReader readerRack = cmdRack.ExecuteReader();
                if (readerRack.Read())
                {
                    rack[0] = readerRack.GetValue(0).ToString();
                    rack[1] = readerRack.GetValue(1).ToString();
                    rack[2] = readerRack.GetValue(2).ToString();
                    rack[3] = readerRack.GetValue(3).ToString();
                    rack[4] = readerRack.GetValue(4).ToString();
                    rack[5] = readerRack.GetValue(5).ToString();
                }
                else
                {
                    conn.Close();
                    return res = null;
                }
                conn.Close();
                string selectRProd = "select Modelo, Niveles, Ventanas from RProduccion where IdRack = '" + rack[1] + "'";
                conn.Open();
                SqlCommand cmdRProd = new SqlCommand(selectRProd, conn);
                SqlDataReader readerRProd = cmdRProd.ExecuteReader();
                string modelo = "";
                int niveles, ventanas;
                if (readerRProd.Read())
                {
                    modelo = readerRProd.GetValue(0).ToString();
                    niveles = readerRProd.GetInt32(1);
                    ventanas = readerRProd.GetInt32(2);
                }
                else
                {
                    conn.Close();
                    return res = null;
                }
                conn.Close();
                string selectArt = "Select Descripción from catArt where Clave = '" + rack[5] + "'";
                conn.Open();
                SqlCommand cmdArt = new SqlCommand(selectArt, conn);
                SqlDataReader readerArt = cmdArt.ExecuteReader();
                string item = "";
                if (readerArt.Read())
                {
                    item = readerArt.GetValue(0).ToString();
                }
                else
                {
                    conn.Close();
                    return res = null;
                }
                conn.Close();
                res[0] = rack[0];//numero de rack
                res[1] = modelo;//modelo de rack
                res[2] = rack[2];//orden de produccion
                res[3] = rack[3];//cantidad estimada
                res[4] = rack[4];//cantidad real
                res[5] = rack[5];//codigo del producto
                res[6] = niveles + ""; //cantidad de niveles
                res[7] = ventanas + "";//cantidad de ventanas
                res[8] = item;//nombre del producto
                return res;
            }
            catch (Exception e)
            {
                conn.Close();
                return res = null;
            }
        }

        //Funcion: nuevoHuecos
        //Proposito: actualiza la informacion despues de realizar un avance a una orden de produccion.
        public string nuevoHuecos(int estimado, int huecos, string[] rack, string epc)
        {
            string res = "";
            string newid = "d5509a97-b0fa-4406-b63e-c4cd0730df5c";//JLMQ 14MARZO2019
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            int[] curado = CurrentCurado(rack[4], rack[2], newid);
            int merma = CurrentMerma(rack[4], rack[2]), nvoMerma = merma + huecos;
            int diferencia = estimado - huecos, asignado = CurrentAsignado(rack[4], rack[2]), restante = asignado - estimado, nvoCurado = curado[0] + diferencia;
            try
            {
                string updateRack = "UPDATE DetRProd SET contadoProd = 1, cantidadReal = " + diferencia + " where EPC = '" + epc + "'";
                Ejecuta(updateRack, conn);
                string updateOrden = "UPDATE CatProd SET estatus = 'CURADO', prodCurado = " + nvoCurado + ", mermas = " + nvoMerma + ", prodAsignado = " + restante + " where OrdenProduccion = '" + rack[2] + "' and Codigo = '" + rack[4] + "'";
                Ejecuta(updateOrden, conn);
                return res = "Conteo Exitoso";
            }
            catch (Exception e)
            {
                conn.Close();
                return res = "No se pudo realizar el conteo. Intente después nuevamente";
            }

        }

        //funciones secundarias
        /* */

        //Funcion: getEscuadraD
        //Proposito: regresa un array con el detalle de una escuadra en especifico.
        public string[] getEscuadraD(string epc)
        {
            string[] escuadraD = new string[3];
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                string selectEscuadraD = "select OrdenProduccion,CodigoProducto, Piezas from DetEscuadras where EPC = '" + epc + "'";
                conn.Open();
                SqlCommand cmdEscuadraD = new SqlCommand(selectEscuadraD, conn);
                SqlDataReader readerEscuadraD = cmdEscuadraD.ExecuteReader();
                if (readerEscuadraD.Read())
                {
                    escuadraD[0] = readerEscuadraD.GetValue(0).ToString();
                    escuadraD[1] = readerEscuadraD.GetValue(1).ToString();
                    escuadraD[2] = readerEscuadraD.GetInt32(2).ToString();
                    conn.Close();
                    return escuadraD;
                }
                else
                {
                    conn.Close();
                    return null;
                }

            }
            catch (Exception e)
            {
                conn.Close();
                return null;
            }

        }

        //Funcion: getCantidadRemision
        //Proposito: selecciona cuanta cantidad se va a embarcar (remisionar)
        public string getCantidadRemision(string Remision, string codigo)
        {
            string id = "";
            string renglon = "";
            string id2 = "";//JLMQ NAPRESA 27 JUNIO 2016
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                string selectIdRemision = "select Id from Venta where MovId = '" + Remision + "'";//selecciona id de la remision
                conn.Open();
                SqlCommand cmdIdRemision = new SqlCommand(selectIdRemision, conn);
                SqlDataReader readerIdRemision = cmdIdRemision.ExecuteReader();
                if (readerIdRemision.Read())
                {
                    id = readerIdRemision.GetValue(0).ToString();
                }
                else
                {
                    conn.Close();
                    return id = "-1";
                }
                conn.Close();
                //SACAR RENGLON PARA EVITAR TRAER MAS DE UN PRODUCTO
                string selectRenglon = "select Renglon from VentaD where ID = '" + id + "' AND Articulo = '" + codigo + "'";
                conn.Open();
                SqlCommand cmdRenglon = new SqlCommand(selectRenglon, conn);
                SqlDataReader readerRenglon = cmdRenglon.ExecuteReader();
                if (readerRenglon.Read())
                {
                    renglon = readerRenglon.GetValue(0).ToString();
                }
                else
                {
                    conn.Close();
                    return id = "-1";
                }
                conn.Close();

                conn.Open();
                //string selectCantidadRemision = "Select Cantidad from VentaD where ID = " + id + " and Articulo = '" + codigo + "'"; //pruebas
                //string selectCantidadRemision = "Select Paquetes from Venta where ID = " + id + " ";
                string selectCantidadRemision = "SELECT ROUND(SUM(vd.cantidad * ArtUnidad.Factor),0) as Calculo FROM Venta v INNER JOIN (VentaD vd INNER JOIN Art on art.Articulo = vd.Articulo LEFT JOIN ArtUnidad on ArtUnidad.Articulo = vd.Articulo AND ArtUnidad.Unidad = art.Unidad) on vd.ID = v.ID  WHERE v.MovID = '" + Remision + "' AND vd.id = '" + id + "' AND vd.Renglon = '" + renglon + "'";                
                SqlCommand cmdCantidadRemision = new SqlCommand(selectCantidadRemision, conn);
                SqlDataReader readerCantidadRemision = cmdCantidadRemision.ExecuteReader();
                if (readerCantidadRemision.Read())
                {
                    id2 = readerCantidadRemision.GetValue(0).ToString();
                    conn.Close();
                    //id = convertMetro2Unidad(codigo, double.Parse(id2));//se cambia a id2 para pruebas 21/07/16 xero
                    return id2;
                }
                else
                {
                    conn.Close();
                    return id = "-1";
                }
            }
            catch (Exception e)
            {
                conn.Close();
                return id = "-1";
            }
        }

        //Funcion: getIdProd
        //Proposito: regresa el ultimo id existente en la tabla Prod de Intelisis
        public int getIdProd()
        {
            int Id = 0;
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
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
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return Id;
        }

        //Funcion: getIdInv
        //Proposito: regresa el ultimo id existente en la tabla Inv de Intelisis
        public int getIdInv()
        {
            int Id = 0;
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
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
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return Id;
        }

        //Funcion: EstimadoRack
        //Proposito: regresa en un array la cantidad estimada de producto para un rack, asi como la orden de produccion a la que esta asignado.
        public string[] EstimadoRack(string epc, string codigo)
        {
            string[] estimado = new string[2];
            string[] parametros2 = getParametros("Solutia");
            SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + "");
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
                    conn2.Close();
                }
                else
                {
                    conn2.Close();
                    return estimado = null;
                }
            }
            catch (Exception e)
            {
                conn2.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return estimado = null;
            }
            return estimado;
        }

        //Funcion: CurrentCurado
        //Proposito: regresa la cantidad de producto actual en CURADO
        public int[] CurrentCurado(string codigo, string op, string newId)
        {
            int[] estimado = new int[3];
            string[] parametros2 = getParametros("Solutia");
            SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + "");
            try
            {
                conn2.Open();
                string selectQty = "Select prodCurado,Renglon from CatProd where codigo ='" + codigo + "' and OrdenProduccion ='" + op + "' and id_parcialidad ='"+ newId +"'";
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
                    conn2.Close();
                    return estimado = null;
                }
                conn2.Close();
            }
            catch (Exception e)
            {
                conn2.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return estimado;


        }

        //Funcion: CurrentLiberado
        //Proposito: regresa la cantidad de producto actual en LIBERADO
        public int CurrentLiberado(string codigo, string op)
        {
            int estimado = 0;
            string[] parametros2 = getParametros("Solutia");
            SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + "");
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
                    conn2.Close();
                    return estimado = -1;
                }
                conn2.Close();
            }
            catch (Exception e)
            {
                conn2.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return estimado - 1;
            }
            return estimado;

        }

        //Funcion: CurrentMerma
        //Proposito: regresa la cantidad de producto actual en MERMA
        public int CurrentMerma(string codigo, string op)
        {
            int estimado = 0;
            string[] parametros2 = getParametros("Solutia");
            SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + "");
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
                    conn2.Close();
                    return estimado = -1;
                }
                conn2.Close();
            }
            catch (Exception e)
            {
                conn2.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return estimado - 1;
            }
            return estimado;


        }

        //Funcion: CurrentAsignado
        //Proposito: regresa la cantidad de producto actual en ASIGNADO 
        public int CurrentAsignado(string codigo, string op)
        {
            int estimado = 0;
            string[] parametros2 = getParametros("Solutia");
            SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + "");
            try
            {
                conn2.Open();
                string LibQty = "Select prodAsignado from CatProd where codigo ='" + codigo + "' and OrdenProduccion ='" + op + "'";
                SqlCommand cmdLib = new SqlCommand(LibQty, conn2);
                SqlDataReader readerLib = cmdLib.ExecuteReader();
                if (readerLib.Read())
                {
                    estimado = int.Parse(readerLib.GetValue(0).ToString());
                }
                else
                {
                    conn2.Close();
                    return estimado = -1;
                }
                conn2.Close();
            }
            catch (Exception e)
            {
                conn2.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return estimado - 1;
            }
            return estimado;


        }

        //Funcion: maxRenglonSub
        //Proposito: regresa el maximo valor de (partidas) renglones existentes en una orden de produccion POR PRODUCTO
        public int maxRenglonSub(string id, string codigo)
        {
            int cantidad = 0;
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                string selectRenglonSub = "select MAX(RenglonSub) from ProdD where Articulo ='" + codigo + "' and ID = " + id + " and Centro = 'ABP1BE1' and CentroDestino = 'BUSP1CURAD'";
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
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return cantidad;
        }


        //Funcion: maxRenglonSub
        //Proposito: regresa el maximo valor de (partidas) renglones existentes en una orden de produccion POR PRODUCTO
        public int maxRenglonSub2(string id, string codigo, int cantidad2)
        {
            int cantidad = 0;
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                string selectRenglonSub = "select MAX(RenglonSub) from ProdD where Articulo ='" + codigo + "' and ID = " + id + " and CantidadPendiente = "+cantidad2+"";
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
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return cantidad;
        }


        //Funcion: maxRenglonSub
        //Proposito: regresa el maximo valor de (partidas) renglones existentes en una orden de produccion POR PRODUCTO
        public int maxRenglonSub3(int id, string codigo, int renglon)
        {
            int cantidad = 0;
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                string selectRenglonSub = "SELECT RenglonSub "+
                                          "FROM ProdD "+
                                          "WHERE Articulo ='" + codigo + "' AND ID = " + id + " AND Renglon = "+renglon+" ";
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
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return cantidad;
        }


        //Funcion: getTotalCantPend
        //Proposito: regresa la cantidad pendiente por producirse de un producto en una Orden de Produccion
        public int getTotalCantPend(string id, string renglon, int renglonSub, string codigo)
        {
            int total = 0;
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
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
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                total = -1;
            }
            return total;
        }

        //Funcion: CentroTrabajo
        //Proposito: regresa los centros de trabajo asignados a una partida.
        public string[] CentroTrabajo(string id, string codigo)
        {
            string[] centro = new string[3];
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
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
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return centro;
        }

        //Funcion: countRenglones
        //Proposito: cuenta cuantos renglones existen dentro de una orden de produccion.
        public int countRenglones(string id, string codigo)
        {
            int conteo = 0;
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
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
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return conteo;
        }

        //Funcion: setMovID
        //Proposito: actualiza los consecutivos en la tabla InvC de Intelisis.
        public string setMovID(string mov, string sucursal)
        {
            string movId = "";
            int nextConsec = 0;
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                string getConsec = "select Consecutivo from InvC where Mov LIKE '%" + mov + "%' and Sucursal = " + sucursal + "";
                SqlCommand cmdConsec = new SqlCommand(getConsec, conn);
                SqlDataReader reader = cmdConsec.ExecuteReader();
                if (reader.Read())

                {
                    nextConsec = reader.GetInt32(0);
                    reader.Close();
                }
                else
                {
                    nextConsec = 0;
                }
                nextConsec = nextConsec + 1;
                movId = "AB" + nextConsec;
                string updateConsec = "update InvC set Consecutivo = " + nextConsec + " where Mov like '%" + mov + "%' and Sucursal = " + sucursal + "";
                SqlCommand cmdUC = new SqlCommand(updateConsec, conn);
                cmdUC.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return null;
            }
            return movId;
        }

        //Funcion: numDecimal
        //Proposito: remplaza la coma en los decimales por un punto
        public string numDecimal(string coma)
        {
            string numDec = "0";
            string[] firstStep = coma.Split(',');
            string lastStep = firstStep[0] + "." + firstStep[1];
            numDec = lastStep;
            return numDec;
        }

        //Funcion: ordenRemision
        //Proposito: valida que la remision exista
        public string[] ordenRemision(string remision)
        {
            string[] remi = new string[2];
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                //string select = "select TOP(1) MovId,Referencia from Prod where Mov = 'Orden Produccion' and Referencia = (Select TOP(1) OrigenID from Venta where MovId = '" + remision + "') order by MovId DESC";
                string select = "SELECT TOP(1) MovId, OrigenID from Venta where MovId = '" + remision + "'";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    remi[0] = reader.GetString(0);
                    remi[1] = reader.GetString(1);
                }
                else
                {
                    conn.Close();
                    return null;
                }
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return null;
            }
            return remi;
        }

        //Funcion: getEscuadraWDR
        //Proposito: trae un data set con todas las escuadras disponibles
        public DataTable getEscuadraWDR()
        {
            //string select = "Select * from DetEscuadras where Asignado = 0";JLMQ es el que estaba inicialmente
            string select = "Select * from DetEscuadras where Asignado = 0 and Ubicada = 0";//JLMQ  se agrego
            return getDatasetConexionWDR(select, "Solutia");
        }

        //Funcion: verificarEscuadra
        //Proposito: marca una escuadra como asignada
        public string verificarEscuadra(string EPC, string folio, string codigo, int cantidadEstiba, string pedido, string newId)
        {
            string result = "null";
            string renglon = "";
            int total = 0;
            int CXT = cantidadPorTarima(codigo);
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                string selectRenglon = "select cantidad, renglon from catprod where OrdenProduccion = '" + folio + "' and Codigo = '" + codigo + "' and id_parcialidad = '" + newId + "'";
                SqlCommand cmdRenglon = new SqlCommand(selectRenglon, conn);
                SqlDataReader reader = cmdRenglon.ExecuteReader();
                if (reader.Read())
                {
                    total = int.Parse(reader.GetValue(0).ToString());
                    renglon = reader.GetValue(1).ToString();
                }
                else
                {
                    return null;
                }
                conn.Close();
                if (cantidadEstiba > 0 || (cantidadEstiba <= maximoPorTarima(codigo) || maximoPorTarima(codigo) == 0))//VALIDA SI LO ESTIBADO ES > 0`Ò
                //if (cantidadEstiba > 0 && (cantidadEstiba <= maximoPorTarima(codigo) || maximoPorTarima(codigo) == 0))
                {
                    int liberado = currentValue(folio, "LIBERADO", "wea", renglon, newId);
                    int nvoLiberado = liberado - cantidadEstiba;
                    int concluido = currentValue(folio, "CONCLUIDO", "wea", renglon, newId);
                    int nvoConcluido = concluido + cantidadEstiba;
                    int restante = total - cantidadEstiba;
                    string update = "update catProd set prodLiberado = " + nvoLiberado + ", prodConcluido = " + nvoConcluido + ", prodRestante = " + restante + " where OrdenProduccion = '" + folio + "' and Codigo = '" + codigo + "' and id_parcialidad = '" + newId + "'";
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(update, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    string escuadra ="";
                    if(pedido != "")
                        escuadra = "update DetEscuadras set OrdenProduccion = '" + folio + "', CodigoProducto = '" + codigo + "', Piezas = " + cantidadEstiba + ", Asignado = 1, Ubicada = 0, Pedido = '" + pedido + "', newIdEscuadra = '"+ newId +"' where EPC = '" + EPC + "'"; //JLMQ se agrega Ubicada = 0 para que forces a que te muestre la mercancia en almacen sin importar que escuadra te halla asignado
                    else
                        escuadra = "update DetEscuadras set OrdenProduccion = '" + folio + "', CodigoProducto = '" + codigo + "', Piezas = " + cantidadEstiba + ", Asignado = 1, Ubicada = 0, Pedido = 'STOCK', newIdEscuadra = '" + newId + "' where EPC = '" + EPC + "'"; //JLMQ se agrega Ubicada = 0 para que forces a que te muestre la mercancia en almacen sin importar que escuadra te halla asignado
                    SqlCommand cmdE = new SqlCommand(escuadra, conn);
                    cmdE.ExecuteNonQuery();
                    result = "Escuadra Asignada";
                }
                else
                {
                    result = "Se ha tratado de Estibar en Tarima más cantidad de la permitida";
                }
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                result = e.Message;
            }
            return result;
        }

        //Funcion: asignaEscuadraFisica
        //Proposito: asigna una escuadra fisica para producto a granel
        public string asignaEscuadraFisica(string EPC, int piezasNvo, int piezasAct, string op, string posicion, string codigoProd,string pedido,string[] remision)
        {
            string result = null;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                int diferencia = piezasAct - piezasNvo;
                string updateDetEscuadras = "UPDATE DetEscuadras SET CodigoProducto = '" + codigoProd + "', Pedido = '" + remision[1] + "', OrdenProduccion = 'SIN OP', Posicion = '" + posicion + "', Piezas = " + piezasNvo + ", Asignado = 1, Ubicada = 1 WHERE EPC = '" + EPC + "'";
                if (Ejecuta(updateDetEscuadras, conn))
                {
                    string updateEscuadraGranel = "UPDATE DetEscuadras SET Piezas = " + diferencia + " WHERE EPC = '" + codigoProd + "'";
                    if(Ejecuta(updateEscuadraGranel, conn))
                        result = "Escuadra Asignada";
                    else
                        result = "Hubo un error al actualizar el inventario de escuadras. Intente nuevamente más tarde";
                }else
                    result = "Hubo un error al actualizar el inventario de escuadras. Intente nuevamente más tarde";
            }
            catch (Exception e)
            {
                conn.Close();
                result = e.Message;
            }
            return result;
        }

        //Funcion: asignaEscuadraVirtual
        //Proposito: da entrada a almacen PT a producto a granel
        public bool asignaEscuadraVirtual(string codigoEPC, int cantidadNva)
        {
            int cantidadAct = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                string selectCXT = "SELECT Pendiente from detEscuadras where EPC ='" + codigoEPC.Trim() + "'";
                conn.Open();
                SqlCommand cmdCXT = new SqlCommand(selectCXT, conn);
                SqlDataReader readerCXT = cmdCXT.ExecuteReader();
                if (readerCXT.Read())
                {
                    cantidadAct = int.Parse(readerCXT.GetValue(0).ToString());
                }
                conn.Close();
                int diferencia = cantidadAct + cantidadNva;
                string updateCantidades = "UPDATE detEscuadras SET OrdenProduccion = 'GRANEL',  Piezas = " + diferencia + ", Pendiente = 0 WHERE EPC = '" + codigoEPC + "'";
                if (Ejecuta(updateCantidades, conn))
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return false;
            }
            
        }

        //Funcion: cantidadPorTarimaW
        //Proposito: regresa la cantidad de piezas por tarima
        public int cantidadPorTarimaW(string codigo)
        {
            int cantidad = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
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
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return cantidad;
        }

        //Funcion: maximoPorTarima
        //Proposito: regresa la cantidad de piezas por tarima
        public Decimal maximoPorTarima(string codigo)
        {
            Decimal cantidad = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
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
                    extra = Decimal.Round(extra, 0);
                    cantidad = cantidad + extra;
                }
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return cantidad;
        }

        //Funcion: cantidadPorTarima
        //Proposito: regresa la cantidad de piezas por tarima
        public int cantidadPorTarima(string codigo)
        {
            int cantidad = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
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
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return cantidad;
        }

        //Funcion: getProdId
        //Proposito: regresa el ultimo id insertado en la tabla Prod de Intelisis
        public int getProdId(string op, string codigo, string newId)
        {
            int renglon = 0;
            string[] prueba = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");
            try
            {
                conn.Open();
                string select = "SELECT Id from catProdD WHERE  idNew = '" + newId + "'";
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
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                renglon = -1;
            }
            return renglon;
        }

        //Funcion: getRenglonId
        //Proposito: regresa el id de un subrenglon en especifico
        public int getRenglonId(int renglon, int renglonSub)
        {
            int rID = 0;
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                string select = "select RenglonId from ProdD where renglon = " + renglon + " and renglonSub = " + renglonSub;
                conn.Open();
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    rID = reader.GetInt32(0);
                }
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return rID;
        }

        //Funcion: convertUnidad2Metro
        //Proposito: convierte unidades (pza) en metros (m2)
        public string convertUnidad2Metro(string codigo, int cantidad)
        {
            double res = 0;
            string msj = "";
            string[] parametros = getParametros("Intelisis");
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
                string unidad = getUnidad(codigo);
                while (unidad != null)
                {
                    if (unidad.Equals("M2"))
                    {
                        double factor = getFactor(codigo);
                        if (factor < 0)
                        {
                            return msj = "-1";
                            break;
                        }
                        else
                        {
                            res = cantidad / factor;
                            break;
                        }
                    }
                    else
                    {
                        res = cantidad;
                        break;
                    }
                }
                msj = res.ToString().Replace(',', '.');
                return msj;
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return msj = "-1";
            }
        }

        //Funcion: convertMetro2Unidad
        //Proposito: convierte metros (m2) en piezas (pza)
        public string convertMetro2Unidad(string codigo, double cantidad)
        {
            double res = 0;
            string msj = "";
            string[] parametros = getParametros("Intelisis");
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
                string unidad = getUnidad(codigo);//OBTIENE SI ES M2
                while (unidad != null)
                {
                    if (unidad.Equals("M2"))
                    {
                        double factor = getFactor(codigo);
                        if (factor < 0)
                        {
                            return msj = null;
                            break;
                        }
                        else
                        {
                            res = cantidad * factor;
                            break;
                        }
                    }
                    else
                    {
                        res = cantidad;
                        break;
                    }
                }
                msj = res.ToString().Replace(',', '.');
                return msj;
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return msj = null;
            }
        }

        //Funcion: getUnidad
        //Proposito:regresa la unidad de venta de un producto (PZA o M2)
        public string getUnidad(string codigo)
        {
            string res = "";
            string[] parametros = getParametros("Intelisis");
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
                string selectUnidad = "SELECT UnidadCompra FROM Art where Articulo = '" + codigo + "'";
                conn.Open();
                SqlCommand cmdUnidad = new SqlCommand(selectUnidad, conn);
                SqlDataReader readUnidad = cmdUnidad.ExecuteReader();
                if (readUnidad.Read())
                {
                    res = readUnidad.GetValue(0).ToString();
                    return res;
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
        }

        //Funcion: getFactor
        //Proposito: regresa el factor de conversion de un producto (A pza = B m2
        public double getFactor(string codigo)
        {
            double res = 0;
            string[] parametros = getParametros("Intelisis");
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
                string selectFactor = "SELECT Factor FROM ArtUnidad where Articulo = '" + codigo + "' and Unidad = 'M2'";
                conn.Open();
                SqlCommand cmdFactor = new SqlCommand(selectFactor, conn);
                SqlDataReader readFactor = cmdFactor.ExecuteReader();
                if (readFactor.Read())
                {
                    string sinPunto = readFactor.GetValue(0).ToString();
                    sinPunto = sinPunto.Replace('.', ',');
                    res = double.Parse(sinPunto);
                    return res;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return -1;
            }
        }

        //Funcion: getDatasetInventarioWDR
        //Proposito: regresa el inventario congelado seleccionado
        public DataTable getDatasetInventarioWDR(string comando)
        {
            DataTable res = new DataTable();
            string[] parametros = getParametros("ConsolaAdmin");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                
                conn.Open();
                using (conn)
                {
                    string select = comando;
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
                //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return res;
        }


        public bool reportarMerma(int Id, int mermaR, int diferencia, int renglon, string epc, string user, string sucursal, string codigo, string op, int pzaEsc, string tipoMerma)
        {
            bool merma = false;
            string error = "";
            bool res = false;

            string[] parametros = getParametros("Intelisis");
            string[] parametros2 = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + "");
    

            try
            {
                int renglonSub = 0;
                renglon = 2048;
                int rID = 1;

                string movId = setMovID(tipoMerma, sucursal);//CONSECUTIVO
                string centrocosto = getcentrocosto(codigo);
                string insertInv = "INSERT INTO Inv ("+
                                    "Empresa,"+
                                    "Mov,"+
                                    "MovID,"+
                                    "FechaEmision,"+
                                    "UltimoCambio,"+
                                    "UEN,"+
                                    "Moneda,"+
                                    "TipoCambio,"+
                                    "Usuario,"+
                                    "Referencia,"+
                                    "Observaciones,"+
                                    "Estatus,"+
                                    "Directo,"+
                                    "Almacen,"+
                                    "AlmacenTransito,"+
                                    "OrigenTipo,"+
                                    "Origen,"+
                                    "OrigenID," +
                                    "Sucursal,"+
                                    "SucursalOrigen,"+
                                    "SucursalDestino)" + 
                                   "VALUES "+
                                    "('GNAP',"+
                                    "'"+tipoMerma+"',"+
                                    "'" + movId + "',"+
                                    "GETDATE(),"+
                                    "GETDATE(),"+
                                    "2,"+
                                    "'Pesos',"+
                                    "1,"+
                                    "'" + user + "',"+
                                    "NULL,"+
                                    "NULL,"+
                                    "'SINAFECTAR',"+
                                    "1,"+
                                    "'APT-BUS',"+
                                    "'(TRANSITO)',"+
                                    "NULL,"+
                                    "NULL,"+
                                    "NULL,"+
                                    "" + sucursal + ","+
                                    "" + sucursal + "," +
                                    "" + sucursal + ")";//JLMQ estatus estaba como PENDIENTE
                Ejecuta(insertInv, conn);
                int idInv = getIdInv();
                string convertido = convertUnidad2Metro(codigo, mermaR);//aqui si se pasa la merma
                string unidadProd = getUnidad(codigo);
                centrocosto = getcentrocosto(codigo);
                string insertInvD = "INSERT INTO InvD "+
                                        "(ID,"+
                                        "Renglon," +
                                        "RenglonSub,"+
                                        "RenglonId,"+
                                        "Renglontipo,"+
                                        "Cantidad,"+
                                        "Almacen,"+
                                        "Codigo,"+
                                        "Articulo,"+
                                        "ContUso,"+
                                        "CantidadPendiente,"+
                                        "Unidad,"+
                                        "Factor,"+
                                        "Producto,"+
                                        "Tipo,Sucursal)" + 
                                    "VALUES "+
                                        "(" + getIdInv() + ","+
                                        "" + renglon +","+
                                        "0,"+
                                        "" + rID + ","+
                                        "'N',"+
                                        "" + convertido + ","+
                                        "'APT-BUS',"+
                                        "NULL,"+
                                        "'" + codigo + "',"+
                                        "'" + centrocosto + "',"+
                                        "" + convertido + ","+
                                        "(select UnidadCompra from Art where Articulo='" + codigo + "'),"+
                                        "(select factor from ArtUnidad where Articulo='" + codigo + "' and Unidad = '" + unidadProd + "'),"+
                                        "'" + codigo + "',"+
                                        "'Salida',"+
                                        "" + sucursal + ")";
                Ejecuta(insertInvD, conn);
                string spAfectarMP = "EXEC spAfectar 'INV', " + idInv + ",'AFECTAR','Todo','Merma Estiba','" + user + "',@Estacion = 99";
                Ejecuta(spAfectarMP, conn);
                string estatusUsu = "UPDATE MovEstatusLog SET Estatus = 'CONCLUIDO' WHERE ModuloID = " + idInv + " AND mODULO = 'INV ' AND Sucursal = " + sucursal + " AND Usuario = '" + user + "'";
                Ejecuta(estatusUsu, conn);
                string act = "Update Inv set Estatus = 'CONCLUIDO' where id = " + idInv + "";
                Ejecuta(act, conn);
                string updateDetEscuadra = "UPDATE detEscuadras set Piezas = " + diferencia + " WHERE EPC = '" + epc + "'";
                Ejecuta(updateDetEscuadra, conn2);

                merma = true;
            }
            catch (Exception exc)
            {
                error = exc.Message;
                merma = false;
            }
            return merma;
        }

        //Funcion: pickEscuadra
        //Proposito: marca una escuadra como "lista para su embarque"
        public int pickEscuadra(string epc, string codigo, string op)
        {
            int result = 0;
            //string[] dfp = dataFromEpc(epc);
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {   //verificar si en los 2 updates se puede solo dejar Where epc= ''
                string update = "update DetEscuadras set Picked = 1 where EPC ='" + epc + "' and OrdenProduccion = '" + op + "' and CodigoProducto = '" + codigo + "'";
                if (Ejecuta(update, conn))
                {
                    string update2 = "update Etiquetas_Impresas set Picked = 1 where EPC ='" + epc + "' and OrdenProduccion = '" + op + "' and Codigo = '" + codigo + "'";
                    if (Ejecuta(update2, conn))
                            return 0;
                        else
                            return 1;
                }
                else
                    return 1;
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                result = 1;
            }
            return result;
        }

        //Funcion: pickEscuadra
        //Proposito: marca una escuadra como "lista para su embarque"
        public int pickEscuadra(string epc, string codigo, string op, int nuevo, string remision, int diferencia)
        {
            int result = 0;
            //string[] dfp = dataFromEpc(epc);
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                string update = "update DetEscuadras set Picked = 1 where EPC ='" + epc + "' and OrdenProduccion = '" + op + "' and CodigoProducto = '" + codigo + "'";
                if (Ejecuta(update, conn))
                {
                    string update2 = "update Etiquetas_Impresas set Picked = 1 where EPC ='" + epc + "' and OrdenProduccion = '" + op + "' and Codigo = '" + codigo + "'";
                    if (Ejecuta(update2, conn))
                    {
                        string update3 = "update detRemision set CtaPzaCargada = " + nuevo + ", CtaPzaFaltante = " +diferencia + " WHERE CodigoProducto = '" + codigo + "' and Remision = '" + remision + "'";
                        if (Ejecuta(update3, conn))
                            return 0;
                        else
                            return 1;
                    }
                    else
                        return 1;
                }
                else
                    return 1;
            }
            catch (Exception e)
            {
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                result = 1;
            }
            return result;
        }

        //Funcion: clearEscuadra
        //Proposito: deja en los datos de una escuadra para poder reutilizarla
        public int clearEscuadra(string EPC, string codigo, string op)
        {
            int res = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                //string update = "update DetEscuadras SET OrdenProduccion = NULL, Asignado = 0, Ubicada = 0, CodigoProducto = NULL, Picked = 0, Embarcado = 0, Piezas = 0 where EPC = '" + EPC + "'"; //JLMQ En la linea de abajo se agrega que tambien  ponga el pedido en NULL
                string update = "UPDATE DETESCUADRAS "+
                                    "SET "+
                                    "OrdenProduccion = NULL,"+
                                    "Asignado = 0,"+
                                    "Ubicada = 0,"+
                                    "CodigoProducto = NULL,"+
                                    "Picked = 0,"+
                                    "Embarcado = 0,"+
                                    "Piezas = 0,"+
                                    "Pedido = NULL,"+
                                    "Posicion = NULL,"+
                                    "Pendiente = 0,"+
                                    "pzaRemi = 0,"+
                                    "newIdEscuadra = NULL,"+
                                    "Lote = NULL "+
                                "WHERE EPC = '"+EPC+"'";
                //string delete = "delete from Etiquetas_Impresas where EPC = '" + EPC + "' and OrdenProduccion = '" + op + "' and Codigo = '" + codigo + "'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(update, conn);
                //SqlCommand cmd2 = new SqlCommand(delete, conn);
                cmd.ExecuteNonQuery();
                //cmd2.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                res = 1;
            }
            return res;
        }

        //Funcion: embarcaEscuadra
        //Proposito: marca una escuadra como "embarcada"
        public int embarcaEscuadra(string EPC, string codigo, string op, string remision, int ctaCarga)
        {
            int res = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                string update = "update DetEscuadras set Embarcado= 1 where EPC ='" + EPC + "' and OrdenProduccion = '" + op + "' and CodigoProducto = '" + codigo + "'";
                if (Ejecuta(update, conn))
                {
                    int qtyActual = currentEmbarcado(remision, codigo);
                    int qtyNuevo = qtyActual - ctaCarga;
                    string updateRemision = "UPDATE detRemision SET CtaPzaCargada = " + qtyActual + " WHERE Remision = '" + remision + "' AND CodigoProducto = '" + codigo + "'";
                    if (Ejecuta(updateRemision, conn))
                    {
                        string update2 = "update Etiquetas_Impresas set Embarcado = 1 where EPC ='" + EPC + "' and OrdenProduccion = '" + op + "' and Codigo = '" + codigo + "'";
                        if (Ejecuta(update2, conn))
                        {
                            res = 0;
                        }
                        else
                        {
                            res = 1;
                        }
                    }
                    else
                    {
                        res = 1;
                    }
                }
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                res = 1;
            }
            return res;
        }

        //Funcion: showPaquetesRemision
        //Proposito: muestra los paquetes relacionados a una remision
        public DataTable showPaquetesRemision(string remision, string sucursal)
        {
            DataTable res = new DataTable();
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                //using (conn)
                {
                    string select = "SELECT v.MovID as Remision,v.OrigenID as Pedido,vd.Articulo as CodigoArticulo,vd.Cantidad as CantidadPza,0 as CtaPzaCargada, vd.Cantidad as CtaPzaFaltante, ROUND(vd.cantidad * ArtUnidad.Factor, 0) AS PzaRemision, 0 AS conEscuadra, 0 AS PzasRemiCompletas  FROM Venta v INNER JOIN (VentaD vd INNER JOIN Art on art.Articulo = vd.Articulo LEFT JOIN ArtUnidad on ArtUnidad.Articulo = vd.Articulo AND ArtUnidad.Unidad = art.Unidad) on vd.ID = v.ID WHERE v.MovID = '" + remision + "' AND Art.Tipo = 'NORMAL'"; //AND vd.SucursalOrigen = " + sucursal + "";
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
                //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return res;
        }

        //Funcion: cantidadPendienteRemision
        //Proposito: regresa la cantidad que aun falta por surtirse de una remision
        public int cantidadPendienteRemision(string remision, string codProd)
        {
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                using (conn)
                {
                    string select = "SELECT CtaPzaFaltante from detRemision where Remision = '" + remision + "' AND CodigoProducto = '" + codProd + "'";
                    SqlCommand command = new SqlCommand(select, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                        return int.Parse(reader.GetValue(0).ToString());
                    else
                        return -1;
                }
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message;
                return -1;
                //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
        }

        //Funcion: currentEmbarcado
        //Proposito: regresa la cantidad actualmente embarcada
        public int currentEmbarcado(string remision, string codProd)
        {
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                using (conn)
                {
                    string select = "SELECT CtaPzaCargada from detRemision where Remision = '" + remision + "' AND CodigoProducto = '" + codProd + "'";
                    SqlCommand command = new SqlCommand(select, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                        return int.Parse(reader.GetValue(0).ToString());
                    else
                        return -1;
                }
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message;
                return -1;
                //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
        }

        //Funcion: validaRemisionExiste
        //Proposito: valida si una remision existe en la base de datos de TAGO
        public bool validaRemisionExiste(string remision)
        {
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                using (conn)
                {
                    string select = "SELECT * from detRemision where Remision = '" + remision + "'";
                    SqlCommand command = new SqlCommand(select, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                        return true;
                    else
                        return false;
                }
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message;
                return true;
                //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
        }

        //Funcion: insertaRemision
        //Proposito: inserta una remision en la base de datos de TAGO
        public bool insertaRemision(string remision)
        {
            
            
            string[] data2 = new string[1];
            string[] data = new string[1];
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            string[] parametros2 = getParametros("intelisis");//JLMQ SE agrego conn2
            SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + "");


            try
            {
                string select = "SELECT v.MovID as Remision,v.OrigenID as Pedido,vd.Articulo as CodigoArticulo,vd.Cantidad as CantidadPza,0 as CtaPzaCargada, vd.Cantidad as CtaPzaFaltante, (vd.cantidad * ArtUnidad.Factor) as PzaRemision FROM PRUEBASPILLO.dbo.Venta v INNER JOIN (PRUEBASPILLO.dbo.VentaD vd INNER JOIN Art on art.Articulo = vd.Articulo LEFT JOIN ArtUnidad on ArtUnidad.Articulo = vd.Articulo AND ArtUnidad.Unidad = art.Unidad) on vd.ID = v.ID WHERE v.MovID = '" + remision + "' ";
                string[] dataRemision = fillArray(select, conn2);
                if (dataRemision != null)
                {
                    string insert = "INSERT INTO detRemision VALUES ('" + dataRemision[0] + "','" + dataRemision[1] + "','" + dataRemision[2] + "'," + dataRemision[3] + "," + dataRemision[4] + "," + dataRemision[5] + "," + dataRemision[6] + ")";
                    if (Ejecuta(insert, conn))
                        return true;
                    else
                        return false;
                }
                else
                    return false;
             }

             catch (Exception e)
             {
                conn.Close();
                string error = e.Message;
                return true;
             }
            
        }

        //Funcion: getCantidadEscuadra
        //Proposito: regresa la cantidad actual de piezas en una tarima (escuadra)                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              
        public int getCantidadEscuadra(string tagEPC)
        {
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                using (conn)
                {
                    string select = "SELECT Piezas from detEscuadras where EPC = '" + tagEPC + "'";
                    SqlCommand command = new SqlCommand(select, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                        return int.Parse(reader.GetValue(0).ToString());
                    else
                        return -1;
                }
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message;
                return -1;
                //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
        }

        //Funcion: validaInventario
        //Proposito: valida que el inventario a generar tenga producto
        public bool validaInventario(string query)
        {
            string[] prueba = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");
            try
            {
                conn.Open();
                string select = query;
                SqlCommand cmdSelect = new SqlCommand(select, conn);
                SqlDataReader readerSelect = cmdSelect.ExecuteReader();
                if (readerSelect.Read())
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();	
                    return false;
                }
            }
            catch (SqlException e)
            {
                conn.Close();
                using (StreamWriter writer = File.AppendText("My Documents\\Error.txt"))
                {
                    writer.WriteLine("Funcion: validaInventario | Error: " + e.Message + "| Hora: " + DateTime.Now + "");
                }
                return false;
            }
            catch (Exception e)
            {
                conn.Close();
                using (StreamWriter writer = File.AppendText("My Documents\\Error.txt"))
                {
                    writer.WriteLine("Funcion: validaInventario | Error: " + e.Message + "| Hora: " + DateTime.Now + "");
                }
                return false;
            }
        }

        //Funcion: insertaInvCong
        //Proposito: inserta los datos general del nuevo inventario en la base de datos de TAGO
        public bool insertaInvCong(string almacen, string user, string clave, string ubicacion)
        {
            string[] prueba = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");
            try
            {
                conn.Open();
                string insert = "INSERT INTO invCongelado values (" + getNvoIdInv() + ",'" + almacen + "',GETDATE(),0,'" + user + "', '" + clave + "',0,' " + ubicacion + " ')";
                SqlCommand cmdInsert = new SqlCommand(insert, conn);
                int insertAfectados = cmdInsert.ExecuteNonQuery();
                if (insertAfectados > 0)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (SqlException e)
            {
                conn.Close();
                using (StreamWriter writer = File.AppendText("My Documents\\Error.txt"))
                {
                    writer.WriteLine("Funcion: insertaInvCong | Error: " + e.Message + "| Hora: " + DateTime.Now + "");
                }
                return false;
            }
            catch (Exception e)
            {
                conn.Close();
                using (StreamWriter writer = File.AppendText("My Documents\\Error.txt"))
                {
                    writer.WriteLine("Funcion: insertaInvCong | Error: " + e.Message + "| Hora: " + DateTime.Now + "");
                }
                return false;
            }
        }

        //Funcion: insertaDetalleCongelado
        //Proposito: inserta los productos relacionados al inventario congelado nuevo
        public bool insertaDetalleCongelado(string select)
        {
            string[] prueba = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");
            SqlConnection connMirror = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");
            try
            {
                conn.Open();
                SqlCommand cmdSelect = new SqlCommand(select, conn);
                SqlDataReader reader = cmdSelect.ExecuteReader();
                while (reader.Read())
                {//JLMQ EN la linea de abajo se sustituye EPC por ProdSKU
                    string insert = "INSERT INTO DetalleCongelado (idInvCong, EPC, ProdCB, Estatus, Cantidad, Codigo) VALUES (" + getIdInvSol() + ",'" + reader.GetValue(0).ToString() + "','" + reader.GetValue(3).ToString() + "',0,'" + reader.GetValue(2).ToString() + "','" + reader.GetValue(1).ToString() + "')";
                    connMirror.Open();
                    SqlCommand cmdInsert = new SqlCommand(insert, connMirror);
                    int insertAfectado = cmdInsert.ExecuteNonQuery();
                    if (insertAfectado > 0)
                    {
                        connMirror.Close();
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (SqlException e)
            {
                conn.Close();
                using (StreamWriter writer = File.AppendText("My Documents\\Error.txt"))
                {
                    writer.WriteLine("Funcion: insertaDetalleCongelado | Error: " + e.Message + "| Hora: " + DateTime.Now + "");
                }
                return false;
            }
            catch (Exception e)
            {
                conn.Close();
                using (StreamWriter writer = File.AppendText("My Documents\\Error.txt"))
                {
                    writer.WriteLine("Funcion: insertaDetalleCongelado | Error: " + e.Message + "| Hora: " + DateTime.Now + "");
                }
                return false;
            }
        }

        //Funcion: getIdInvSol
        //Proposito: obtiene el id el ultimo registro insertado en los inventarios de TAGO
        public int getIdInvSol()
        {
            string[] prueba = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");
            int res;
            try
            {
                conn.Open();
                string select = "SELECT Count(idInv) FROM InvCongelado";
                SqlCommand cmdSelect = new SqlCommand(select, conn);
                SqlDataReader readerSelect = cmdSelect.ExecuteReader();
                if (readerSelect.Read())
                {
                    res = int.Parse(readerSelect.GetValue(0).ToString());
                    conn.Close();
                    return res;
                }
                else
                {
                    conn.Close();
                    return 0;
                }
            }
            catch (SqlException e)
            {
                conn.Close();
                using (StreamWriter writer = File.AppendText("My Documents\\Error.txt"))
                {
                    writer.WriteLine("Funcion: login | Error: " + e.Message + "| Hora: " + DateTime.Now + "");
                }
                return 0;
            }
            catch (Exception e)
            {
                conn.Close();
                using (StreamWriter writer = File.AppendText("My Documents\\Error.txt"))
                {
                    writer.WriteLine("Funcion: login | Error: " + e.Message + "| Hora: " + DateTime.Now + "");
                }
                return 0;
            }
        }

        //Funcion: getIdInvC
        //Proposito: obtiene el id el ultimo registro insertado en los inventarios de TAGO
        public int getIdInvC()
        {
            string[] prueba = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");
            int res;
            try
            {
                conn.Open();
                string select = "SELECT Count(idDIC) FROM DetalleCongelado";
                SqlCommand cmdSelect = new SqlCommand(select, conn);
                SqlDataReader readerSelect = cmdSelect.ExecuteReader();
                if (readerSelect.Read())
                {
                    res = int.Parse(readerSelect.GetValue(0).ToString());
                    conn.Close();
                    return res;
                }
                else
                {
                    conn.Close();
                    return 0;
                }
            }
            catch (SqlException e)
            {
                conn.Close();
                using (StreamWriter writer = File.AppendText("My Documents\\Error.txt"))
                {
                    writer.WriteLine("Funcion: login | Error: " + e.Message + "| Hora: " + DateTime.Now + "");
                }
                return 0;
            }
            catch (Exception e)
            {
                conn.Close();
                using (StreamWriter writer = File.AppendText("My Documents\\Error.txt"))
                {
                    writer.WriteLine("Funcion: login | Error: " + e.Message + "| Hora: " + DateTime.Now + "");
                }
                return 0;
            }
        }

        //Funcion: getNvoIdInv
        //Proposito: obtiene el siguiente id en los inventarios  de TAGO
        public int getNvoIdInv()
        {
            string[] prueba = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");
            int res;
            try
            {
                conn.Open();
                string select = "SELECT Count(idInv) FROM InvCongelado";
                SqlCommand cmdSelect = new SqlCommand(select, conn);
                SqlDataReader readerSelect = cmdSelect.ExecuteReader();
                if (readerSelect.Read())
                {
                    res = int.Parse(readerSelect.GetValue(0).ToString());
                    res = res + 1;
                    conn.Close();
                    return res;
                }
                else
                {
                    conn.Close();
                    return 0;
                }
            }
            catch (SqlException e)
            {
                conn.Close();
                using (StreamWriter writer = File.AppendText("My Documents\\Error.txt"))
                {
                    writer.WriteLine("Funcion: login | Error: " + e.Message + "| Hora: " + DateTime.Now + "");
                }
                return 0;
            }
            catch (Exception e)
            {
                conn.Close();
                using (StreamWriter writer = File.AppendText("My Documents\\Error.txt"))
                {
                    writer.WriteLine("Funcion: login | Error: " + e.Message + "| Hora: " + DateTime.Now + "");
                }
                return 0;
            }
        }

        //Funcion: getNvoIdInvC
        //Proposito: obtiene el siguiente id en los inventarios congelados de TAGO
        public int getNvoIdInvC()
        {
            string[] prueba = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");
            int res;
            try
            {
                conn.Open();
                string select = "SELECT Count(idDIC) FROM DetalleCongelado";
                SqlCommand cmdSelect = new SqlCommand(select, conn);
                SqlDataReader readerSelect = cmdSelect.ExecuteReader();
                if (readerSelect.Read())
                {
                    res = int.Parse(readerSelect.GetValue(0).ToString());
                    res = res + 1;
                    conn.Close();
                    return res;
                }
                else
                {
                    conn.Close();
                    return 0;
                }
            }
            catch (SqlException e)
            {
                conn.Close();
                using (StreamWriter writer = File.AppendText("My Documents\\Error.txt"))
                {
                    writer.WriteLine("Funcion: login | Error: " + e.Message + "| Hora: " + DateTime.Now + "");
                }
                return 0;
            }
            catch (Exception e)
            {
                conn.Close();
                using (StreamWriter writer = File.AppendText("My Documents\\Error.txt"))
                {
                    writer.WriteLine("Funcion: login | Error: " + e.Message + "| Hora: " + DateTime.Now + "");
                }
                return 0;
            }
        }
		//GRANEL
        //Funcion: Escuadra2Stock
        //Proposito: desasigna una escuadra a un pedido para convertirla en STOCK
        public bool Escuadra2Stock(string EPC,int cantidad, int diferencia)
        {
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                string update = "UPDATE detEscuadras set OrdenProduccion = 'STOCK', Piezas = " + diferencia + " WHERE EPC = '" + EPC + "'";
                if (Ejecuta(update, conn)){
                    string updateRemision = "UPDATE detRemision SET CantidadPza = " + diferencia + ", CtaPzaCargada = " + cantidad + " WHERE EPC = '" + EPC + "'";
                    if (Ejecuta(updateRemision, conn))
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Funcion: actualizaescuadraPicking
        //Proposito: 
        public bool actualizaescuadraPicking(string EPC, int cantidad, int diferencia)
        {
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                string update = "UPDATE detEscuadras set Piezas = " + diferencia + " WHERE EPC = '" + EPC + "'";
                if (Ejecuta(update, conn))
                {
                    string updateRemision = "UPDATE detRemision SET CantidadPza = " + diferencia + ", CtaPzaCargada = " + cantidad + " WHERE EPC = '" + EPC + "'";
                    if (Ejecuta(updateRemision, conn))
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }


        //Funcion: deleteRemision
        public bool deleteRemision(string remision)
        {
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {        
                string updateRemision = "DELETE FROM detRemision WHERE Remision = '"+remision+"'";
                if (Ejecuta(updateRemision, conn))
                    return true;
                else
                    return false;        
            }
            catch (Exception)
            {
                return false;
            }
        }


        //Funcion: deleteOP
        public bool deleteOP(string op)
        {
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                string updateRemision = "DELETE FROM catProdD WHERE Lote = '" + op + "'";
                if (Ejecuta(updateRemision, conn))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Funcion: actualizaInvLeido
        //Proposito: actualiza la lista de productos leidos de un inventario.
        public bool actualizaInvLeido(string EPC)
        {
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                string update = "UPDATE DetalleCongelado set Estatus = 0 WHERE EPC = '" + EPC + "'";
                if (Ejecuta(update, conn))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Funcion: getUbicacion
        //Proposito: trae la ubicacion de un inventario
        public int getUbicacion(int idInv)
        {
            string ubi = "";
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                string select = "Select TOP(1) ProdCB FROM DetalleCongelado WHERE idInvCong = " + idInv + "";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader readerSelect = cmd.ExecuteReader();
                if (readerSelect.Read())
                    ubi = readerSelect.GetValue(0).ToString();
                else
                    return 0;
                if(ubi.Equals("A01"))
                    return 1;
                else
                
                    return 2;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        //Funcion: getPedidoRemision
        //Proposito: selecciona el pedido del que sale la remision.
        public string getPedidoRemision(string remision)
        {
            string pedido = "";
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                string select = "SELECT TOP(1) OrigenId FROM Venta WHERE MovId = '" + remision + "' AND Mov Like 'ARM-%'";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader readerSelect = cmd.ExecuteReader();
                if (readerSelect.Read())
                    return pedido = readerSelect.GetValue(0).ToString();
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //Funcion: finalizaInventario
        //Proposito: marca como finalizado un inventario
        public bool finalizaInventario(string idInv)
        {
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                string update = "UPDATE InvCongelado set status = 0 WHERE idInv = '" + idInv + "'";
                if (Ejecuta(update, conn))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Funcion: getcodiremi
        //Proposito: Mostrar el codigo de producto de una remision

        public string getcodiremi(string remi)
        {
            string codigoprod = "";
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");

            try
            {
                conn.Open();
                string select = "SELECT Articulo FROM VentaD WHERE id IN (SELECT id FROM Venta WHERE MovID = '" + remi + "')";//se cambia in por =
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader readerSelect = cmd.ExecuteReader();
                if (readerSelect.Read())
                    return codigoprod = readerSelect.GetValue(0).ToString();
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }

           


        }
        //funcion:getdisponible
        //proposito: VALIDA DISPONIBILIDAD EN INVENTARIO PARA UNA REMISION SIN OP
        public string getdisponible(string remi)
        {
            string disponible = "";
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");

            try
            {
                conn.Open();
                //string select = "SELECT Disponible FROM ArtDisponible where Articulo = (select TOP(1) Articulo from VentaD where id = (select TOP(1) id from venta where MovID = '" + remi + "')) and Almacen = 'ARM-BUS'";
                string select = "SELECT SUM(Disponible) FROM ArtDisponible where Articulo IN (select Articulo from VentaD where id = (select id from venta where MovID = '" + remi + "')) and Almacen = 'ARM-BUS'";//PRUEBAS 
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader readerSelect = cmd.ExecuteReader();
                if (readerSelect.Read())
                {
                    return disponible = readerSelect.GetValue(0).ToString();//aqui debe redondearse                                                          
                    
                   
                }



                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }




        }


        //Funcion: Pzaremimerma
        //Proposito: Actualizar en detRemision las piezas del embarque despues de reportar una merma en Detalle_Recepcion 
        public int Pzaremimerma(string remi, int diferencia)
        {
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            int res = 0;
            try
            {
                conn.Open();
                string update = "UPDATE detRemision set PzaRemision = '" + diferencia + "' WHERE Remision = '" + remi + "'";
                SqlCommand cmd = new SqlCommand(update, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                res = 1;
            }
            return res;
        }

        //Funcion: escuadrafalta
        //Proposito: 0 = todas las escuadras fueron leeidas, 1 = faltan escuadras por leer
        
        public int escuadrafalta(string remision)
        {
            int res = 0;
            int a;
            int b;
            string[] data = new string[1];
            string[] datos = new string[1];
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                //string[] arreglo = remision.Trim().Split(",".ToCharArray());//JLMQ
                //    String cadenaIn = "";
                //    for (int i = 0; i < arreglo.Length; i++)
                //    {
                //        cadenaIn += "'" + arreglo[i] + "'";
                //        if (arreglo.Length - 1 > i)
                //        {
                //            cadenaIn += ",";
                //            cadenaIn = cadenaIn.Trim();
                //        }
                //    }
                conn.Open();
                string select2 = "SELECT "+ 
                                    "SUM(PzasRemiCompletas) "+ 
                                 "FROM detRemision "+
                                 "WHERE "+ 
                                    "Remision = '"+remision+"' "+
                                    "AND PzasRemiCompletas = 1 "+
                                    "AND conEscuadra = 1";
                SqlCommand query = new SqlCommand(select2, conn);
                SqlDataReader read = query.ExecuteReader();
                if (read.Read())
                {
                    int size = read.FieldCount;
                    for (int x = 0; x < size; x++)
                    {
                        datos[x] = read.GetValue(x).ToString();
                    }
                    b = Convert.ToInt32(datos[0]);
                }
                else
                {
                    res = 2;
                }
                //conn.Close();

                //conn.Open();
                string select = "SELECT "+ 
	                                "SUM(Embarcado) "+ 
                                "FROM "+
	                                "DetEscuadras de "+
	                                "INNER JOIN detRemision dr ON de.newIdEscuadra = dr.idRemi "+
                                "WHERE "+
	                                "de.Asignado = 1 "+
	                                "AND de.Ubicada=1 "+
                                    "AND de.Embarcado = 1" +
	                                "AND dr.Remision = '"+remision+"'";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int sizeReader = reader.FieldCount;
                    for (int x = 0; x < sizeReader; x++)
                    {
                        data[x] = reader.GetValue(x).ToString();
                    }
                    a = Convert.ToInt32(data[0]);
                }
                else
                {
                    res = 2;
                }
                

                //if (a == b)
                //{
                //    res = 0;
                //}
                //else
                //{
                //    res = 1;
                //}
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                res = 2;
            }
            return res;
        }


        //Funcion: checkEscFalta
        //Proposito: VALIDAR SI HAY ESCUADRAS VIRTUALES POR SUBIR AL CAMION, NO DEBE SER MAS NI MENOS EXACTO.
        public int checkEscFalta(string remision)
        {
            int res = 0;
            int ProdTotalRemi = 0;
            int totalEmbarcado = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            SqlConnection conn2 = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                string select = "SELECT SUM(conEscuadra) " +
                                "FROM detRemision "+
                                "WHERE Remision = '"+remision+"'";

                string select2 = "SELECT SUM(Embarcado) "+
                                 "FROM DetEscuadras de INNER JOIN detRemision dr ON de.newIdEscuadra = dr.idRemi "+
                                 "WHERE de.Asignado = 1 AND de.Ubicada=1 AND de.Embarcado = 1 AND dr.Remision = '"+remision+"'";
                conn.Open();
                conn2.Open();
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlCommand cmd2 = new SqlCommand(select2, conn2);

                SqlDataReader reader = cmd.ExecuteReader();
                SqlDataReader reader2 = cmd2.ExecuteReader();

                if (reader.Read())
                {
                    ProdTotalRemi = reader.GetInt32(0);//detremision
                }
                if (reader2.Read())
                {
                    totalEmbarcado = reader2.GetInt32(0);//detesc inner detremision
                }
                if (ProdTotalRemi == totalEmbarcado)
                {
                    res = 0;
                }
                else
                {
                    res = 1;
                }
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message;
                res = 2;
            }

            return res;
        }

        public int conOsinRack(string OP)
        {
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");

            try
            {
                conn.Open();
                string getPxT = "SELECT PxT FROM catArt ca INNER JOIN catprod cp ON ca.Clave=cp.Codigo WHERE cp.OrdenProduccion = '" + OP + "'";//JLMQ XERO 12 ago 2016 pruebas
                SqlCommand cmdPxT = new SqlCommand(getPxT, conn);
                SqlDataReader readPxT = cmdPxT.ExecuteReader();
                int PxT = 0;
                if (readPxT.Read())
                {
                    PxT = int.Parse(readPxT.GetValue(0).ToString());
                    conn.Close();
                    return PxT;
                }
                else
                {
                    conn.Close();
                    return -404;
                }

            }
            catch (Exception e)
            {
                conn.Close();
                return -1;
            }
        }


        //funcion:verificaremi
        //proposito: Verifica que exista la remision 
        public string verificaremi(string remi)
        {
            string disponible = "";
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");

            try
            {
                conn.Open();
                //string select = "SELECT Disponible FROM ArtDisponible where Articulo = (select TOP(1) Articulo from VentaD where id = (select TOP(1) id from venta where MovID = '" + remi + "')) and Almacen = 'ARM-BUS'";
                string select = ("SELECT * FROM venta WHERE MovID =  '" + remi + "'");
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader readerSelect = cmd.ExecuteReader();
                if (readerSelect.Read())
                {
                    return disponible = readerSelect.GetValue(0).ToString();//aqui debe redondearse                                                          


                }



                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }




        }

        //funcion:getcentrocosto
        //proposito: Devuelve el centro de costo de un producto, basandose en el codigo del producto.
        public string getcentrocosto(string codigo)
        {
            string centro = "";
            if (codigo.Contains("BL"))
            {
                centro = "FB-01-01";
            }
            else
            {
                centro = "FB-02-01";
            }
            return centro;
        }

        //funcion: insertaDataTable
        //Proposito: Inserta una remision con N productos en detRemision
        public bool insertaDataTable(DataTable tabla, string nombreTabla)
        {


            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            //conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            int filasAfectadas = 0;

            string query = "";
            string newId = "NEWID()";
            string fecha = "GETDATE()";

            foreach (DataRow registro in tabla.Rows)
            {
                conn.Open();
                filasAfectadas = 0;
                query = "INSERT INTO " + nombreTabla + " VALUES(";

                query += "'" + registro["Remision"].ToString() + "', "
                       + "'" + registro["Pedido"].ToString() + "', "
                       + "'" + registro["CodigoArticulo"].ToString() + "', "
                       + registro["CantidadPza"].ToString()
                        + ", " + registro["CtaPzaCargada"].ToString()
                        + ", " + registro["CtaPzaFaltante"].ToString()
                        + ", " + registro["PzaRemision"].ToString()
                        + ", " + registro["conEscuadra"].ToString()
                        + ", " + registro["PzasRemiCompletas"].ToString()
                        + ", " + newId +""
                        + ", " + fecha + ")";

                cmd.CommandText = query;


                try
                {                    
                    filasAfectadas = cmd.ExecuteNonQuery();
                }
                catch (Exception error)
                {
                    string varerror = string.Empty;
                    varerror = error.Message;
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }

            return true;
        }


        //Funcion: remiforescvirtual
        //Proposito: envia los datos de una remision para relacionarlos a la tabla detescuadras y asignar N escuadras virtuales
        public DataTable remiforescvirtual(string remision, string articulo)
        {
            DataTable res = new DataTable();
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                using (conn)
                {
                    string select = "SELECT Pedido, CodigoProducto, PzaRemision FROM detRemision where Remision = '" + remision + "' AND CodigoProducto = '" + articulo + "' ";
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


        //Funcion: asignaEscuadraVirtual
        //Proposito: asigna una escuadra fisica para producto a granel
        public string remiEscuadraVirtual(string EPC, DataTable dtremi, int posicion, string idNew)
        {
            string result = null;
            int pzaRemi = 0;
            string query = "";
            int filasAfectadas = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();
                //foreach (DataRow producto in dtremi.Rows)
                //{
                    query = "UPDATE DetEscuadras SET ";

                    query += "OrdenProduccion = 'SIN OP', "//MODIFICAR PARA QUE PONGA OP O SIN OP SEGUN VENGA LA OP
                           + "Asignado = 1, "
                           + "Ubicada = 1, "
                           + "CodigoProducto = '" + dtremi.Rows[posicion]["CodigoProducto"].ToString() + "', "
                           + "Piezas = " + dtremi.Rows[posicion]["PzaRemision"].ToString() + ", "
                           + "Pedido = '" + dtremi.Rows[posicion]["Pedido"].ToString() + "', "
                           + "pzaRemi = " + pzaRemi + ", "
                           + "newIdEscuadra = '"+ idNew +"' "
                           + "WHERE EPC = '" + EPC + "'";

                        cmd.CommandText = query;
                        filasAfectadas = cmd.ExecuteNonQuery();
                //}
            }
            catch (Exception e)
            {
                conn.Close();
                result = e.Message;
            }
            conn.Close();
            return result;
        }


        //Funcion: getcodigoembarque
        //Proposito: Mostrar el codigo de una escuadra filtrado por el epc

        public string getcodigoembarque(string EPC)
        {
            string codigoprod = "";
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");

            try
            {
                conn.Open();
                string select = "SELECT CodigoProducto FROM DetEscuadras WHERE EPC = '" + EPC + "'";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader readerSelect = cmd.ExecuteReader();
                if (readerSelect.Read())
                    return codigoprod = readerSelect.GetValue(0).ToString();
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        //Funcion: guardarColaImpresion
        //Proposito: inserta nuevas etiquetas una vez que estas se han impreso exitosamente
        public string guardarColaImpresion(string Pedido, string OrdenProduccion, string Cliente, string Producto, int Cantidad, int CantidadTarima, string Medida, string Codigo, string Color, string Tipo, string EPC, int total)
        {
            string res = "";
            string N_Tarima = "";
            int filasAfectadas = 0;
            int cantidadXTarima = 0;
            List<int> pzasXtarima = new List<int>();
            int pzaXtarima = 0;
            int cantidadTotalTarimas = 0;
            int residuoTarima = 0;
            string insert = "";

            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                pzaXtarima = obtenerPzaXtarima(Codigo);
                cantidadTotalTarimas = Cantidad / pzaXtarima; //5
                residuoTarima = Cantidad % pzaXtarima; //5

                for (int i = 0; i < cantidadTotalTarimas; i++)
                    pzasXtarima.Add(pzaXtarima);

                if (residuoTarima > 0)
                    pzasXtarima.Add(residuoTarima);

                    for (int i = 1; i <= total; i++)
                    {
                        N_Tarima = i + " de " + total;
                        //Cantidad = obtenerPzaXtarima(Codigo);

                        conn.Open();
                        insert = "insert into Etiquetas_Impresas (Pedido,OrdenProduccion,Cliente,N_Tarima,Producto,Cantidad,CantidadTarima,Medida,Codigo,Color,Tipo,EPC) values('" + Pedido + "','" + OrdenProduccion + "','" + Cliente + "','" + N_Tarima + "','" + Producto + "'," + Cantidad + "," + pzasXtarima[i-1] + ",'" + Medida + "','" + Codigo + "','" + Color + "','" + Tipo + "','" + EPC + "')";
                        SqlCommand cmd = new SqlCommand(insert, conn);
                        filasAfectadas += cmd.ExecuteNonQuery();
                        conn.Close();

                    }

                if (filasAfectadas == total)
                {
                    return "Se agregaron todos los registros a la cola de impresión.";
                }
                else
                {
                    return "Solo se agregaron " + filasAfectadas + " de " + total + " registros a la cola de impresión, favor de reimprimir.";
                }
                    
            }
            catch (Exception e)
            {
                conn.Close();
                return e.Message;
            }
            return res;
        }

        public string newIdEscVirtual(string remi, string codigo, int piezas)
        {
            string newid = "";
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");

            try
            {
                conn.Open();
                string select = "SELECT idRemi FROM detRemision WHERE Remision = '"+remi+"' AND CodigoProducto = '"+codigo+"' AND PzaRemision = "+piezas+" ORDER BY fecha ASC";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader readerSelect = cmd.ExecuteReader();
                if (readerSelect.Read())
                    return newid = readerSelect.GetValue(0).ToString();
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        //Funcion: cargarColaImpresion
        //Proposito: envia los datos de una remision para relacionarlos a la tabla detescuadras y asignar N escuadras virtuales
        public DataTable cargarColaImpresion(string pedido, string OP, string EPC)
        {
            DataTable colaImpresion = new DataTable();
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                using (conn)
                {
                    string select = "SELECT * FROM Etiquetas_Impresas WHERE Pedido = '" + pedido + "' AND OrdenProduccion = '" + OP + "' AND EPC = '"+ EPC +"'";
                    SqlCommand command = new SqlCommand(select, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    colaImpresion.Load(reader);
                }
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message;
            }
            return colaImpresion;
        }



        //Funcion: obtenerPzaXtarima
        //Proposito: regresa la cantidad final de piezas que debe tener un rack segun el producto que se vaya a producir.
        public int obtenerPzaXtarima(string codigo)
        {
            int cantidad = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            //cantidad por ventana
            try
            {

                conn.Open();
                string select = "Select PzxT from catArt where Clave = '" + codigo + "'";
                SqlCommand cmd = new SqlCommand(select, conn);
                cantidad = Convert.ToInt32(cmd.ExecuteScalar());
                return cantidad;
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }

            conn.Close();
            return cantidad;
        }


        //Funcion: pedidoRemi
        //Proposito: Mostrar con que cantidad viene una remision

        public string pedidoRemi(string remision)
        {
            string pedidoremi = "";
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");

            try
            {
                conn.Open();
                string select = "SELECT v.OrigenID as Pedido FROM PRUEBASPILLO.dbo.Venta v INNER JOIN (PRUEBASPILLO.dbo.VentaD vd INNER JOIN Art on art.Articulo = vd.Articulo LEFT JOIN ArtUnidad on ArtUnidad.Articulo = vd.Articulo AND ArtUnidad.Unidad = art.Unidad) on vd.ID = v.ID WHERE v.MovID =  '" + remision + "'";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader readerSelect = cmd.ExecuteReader();
                if (readerSelect.Read())
                {
                    return pedidoremi = readerSelect.GetValue(0).ToString();
                    
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }



        //Funcion: pzasXescuadra
        //Proposito: Mostrar con que cantidad viene una remision

        public string pzasXescuadra(string pedido, string codigo)
        {
            string pzaXremi = "";
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");

            try
            {
                conn.Open();
                string select = "SELECT Piezas FROM DetEscuadras WHERE Pedido = '" + pedido + "' AND CodigoProducto = '" + codigo + "'";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader readerSelect = cmd.ExecuteReader();
                if (readerSelect.Read())
                    return pzaXremi = readerSelect.GetValue(0).ToString();
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        //Funcion: actualizarPzaEscuadra
        //Proposito: Actualiza en la escuadra la cantidad de piezas despues de embarcar una remision parcial
        public bool actualizarPzaEscuadra(string pedido, int diferencia)
        {
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            int res = 0;
            try
            {
                conn.Open();
                string update = "UPDATE DetEscuadras SET Piezas = " + diferencia + " WHERE Pedido = '" + pedido + "'";
                SqlCommand cmd = new SqlCommand(update, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                return false;
            }
            return true;
        }


        //Funcion: pzaXpedidoRemi
        //Proposito: Muestra las piezas que trae una remision

        public string pzaXpedidoRemi(string remision)
        {
            string pzaRemi = "";
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");

            try
            {
                conn.Open();
                string select = "SELECT (vd.cantidad * ArtUnidad.Factor) as PzaRemision FROM PRUEBASPILLO.dbo.Venta v INNER JOIN (PRUEBASPILLO.dbo.VentaD vd INNER JOIN Art on art.Articulo = vd.Articulo LEFT JOIN ArtUnidad on ArtUnidad.Articulo = vd.Articulo AND ArtUnidad.Unidad = art.Unidad) on vd.ID = v.ID WHERE v.MovID =  '" + remision + "'";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader readerSelect = cmd.ExecuteReader();
                if (readerSelect.Read())
                {
                    return pzaRemi = readerSelect.GetValue(0).ToString();

                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }

        }


        //Funcion: getcodigoremision
        //Proposito: Mostrar el codigo de una escuadra filtrado por la remision

        public string getcodigoremision(string remision)
        {
            string codigoprod = "";
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");

            try
            {
                conn.Open();
                string select = "SELECT vd.Articulo as CodigoArticulo FROM PRUEBASPILLO.dbo.Venta v INNER JOIN (PRUEBASPILLO.dbo.VentaD vd INNER JOIN Art on art.Articulo = vd.Articulo LEFT JOIN ArtUnidad on ArtUnidad.Articulo = vd.Articulo AND ArtUnidad.Unidad = art.Unidad) on vd.ID = v.ID WHERE v.MovID = '" + remision + "'";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader readerSelect = cmd.ExecuteReader();
                if (readerSelect.Read())
                    return codigoprod = readerSelect.GetValue(0).ToString();
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        //Funcion: EscForInvInicial
        //Proposito: regresa un DT con la informacion de inventario inicial
        public DataTable EscForInvInicial()
        {
            string select = "SELECT IdEscuadra as [# Escuadra], OrdenProduccion as [Orden de Produccion], CodigoProducto as Codigo, Piezas FROM DetEscuadras WHERE OrdenProduccion = 'SIN OP' AND Pedido = 'SIN PEDIDO' ";
            return getDatasetConexionWDR(select, "Solutia");
        }

        //Funcion: getEpcEsc
        //Proposito: Muestra un listado de escuadras de acuerdo al producto
        public string[] getEpcEsc(string remision, string codigo)
        {
            string[] data = new string[1];
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");

            try
            {
                string select = "SELECT EPC FROM DetEscuadras WHERE OrdenProduccion = 'SIN OP' AND PEDIDO = 'SIN PEDIDO' AND CodigoProducto = '" + codigo + "'";
                string[] dataRemision = guardaEnArray(select, conn);
                if (dataRemision != null && dataRemision.Count() > 0)
                {
                    return dataRemision;
                }
                else
                    return null;
            }

            catch (Exception e)
            {
                conn.Close();
                string error = e.Message;
                return null;
            }

        }


        //Funcion: guardaEnArray
        //Proposito: recibe una consulta de multiples parametros para luego regresarlos en un array de tipo string
        public string[] guardaEnArray(string consulta, SqlConnection conex)
        {
            try
            {
                conex.Open();

                //string consultaCount = "SELECT count(EPC) FROM DetEscuadras WHERE OrdenProduccion = 'SIN OP' AND PEDIDO = 'SIN PEDIDO'";
                //comando.CommandText = consulta;

                //SqlCommand comandoCount = new SqlCommand(consultaCount, conex);
                //int arraySize = Convert.ToInt32(comandoCount.ExecuteScalar());


                SqlCommand comando = new SqlCommand(consulta, conex);

                


                SqlDataReader reader = comando.ExecuteReader();
                
                    int position = 0;
                    List<string> lista = new List<string>();
                    while (reader.Read())
                    {
                        lista.Add(reader.GetValue(0).ToString().Trim());
                        position++;
                    }

                    string[] array = lista.ToArray();
                                    
                    reader.Close();
                    conex.Close();
                    return array;
                
            }
            catch (SqlException e)
            {
                conex.Close();
                return null;
            }
            catch (Exception e)
            {
                string error = e.Message;
                conex.Close();
                return null;
            }
        }


        //Funcion: validaEscDisp
        //Proposito: valida si una escuadra esta diponible para poder cargar inventario inicial
        public bool validaEscDisp(int idescuadra)
        {
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                using (conn)
                {
                    string select = "SELECT * FROM DetEscuadras WHERE OrdenProduccion IS NULL AND CodigoProducto IS NULL AND Piezas = 0 AND PEDIDO IS NULL AND Posicion IS NULL AND Asignado = 0 AND Ubicada = 0 AND idEscuadra = " + idescuadra + "";
                    SqlCommand command = new SqlCommand(select, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                        return true;
                    else
                        return false;
                }
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message;
                return false;
                
            }
        }



        //Funcion: cargaInvInicial
        //Proposito: Actualiza en la escuadra la informacion de inventario inicial
        public bool cargaInvInicial(string CodigoProducto , int Pzas, string posicion, int idEscuadra)
        {
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            int res = 0;
            try
            {
                conn.Open();
                string update = "UPDATE DetEscuadras SET OrdenProduccion = 'SIN OP', Asignado = 1, Ubicada = 1, CodigoProducto = '" + CodigoProducto + "', Piezas = " + Pzas + " ,Pedido = 'SIN PEDIDO', Posicion = '" + posicion + "' WHERE idEscuadra = " + idEscuadra + "";
                SqlCommand cmd = new SqlCommand(update, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                return false;
            }
            return true;
        }


        //Funcion: ActualizaEscInvInicial
        //Proposito: Actualiza en la escuadra de inventario inicial la informacion de una remision
        public bool ActualizaEscInvInicial(string codigo, string pzaRemi, string tag)
        {
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            int res = 0;
            try
            {
                conn.Open();
                //string update = "UPDATE DetEscuadras SET Piezas = " + pzaRemi + " WHERE EPC = '" + tag + "' AND CodigoProducto = '" + codigo + "'";
                string update = "UPDATE DetEscuadras SET Piezas = (select (piezas - " + pzaRemi + ") from DetEscuadras WHERE EPC = '" + tag + "' AND CodigoProducto = '" + codigo + "') WHERE EPC = '" + tag + "' AND CodigoProducto = '" + codigo + "'";
                SqlCommand cmd = new SqlCommand(update, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                return false;
            }
            return true;
        }



        //Funcion: ActNvaEsc
        //Proposito: Actualiza en la escuadra de inventario inicial que selecciona el usuario la informacion de una remision
        public bool ActNvaEsc(string piezas, string IdEsc)
        {
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            int res = 0;
            try
            {
                conn.Open();
                string update = "UPDATE DetEscuadras SET Piezas = '" + piezas + "', Posicion = 'A01' WHERE idEscuadra = '" + IdEsc + "'";
                SqlCommand cmd = new SqlCommand(update, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                return false;
            }
            return true;
        }


        //Funcion: validapedido
        //Proposito: valida si existe informacion de ese pedido en la tabla DetEscuadras
        public bool validapedido(string pedido)
        {
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                using (conn)
                {
                    string select = "SELECT PEDIDO FROM DetEscuadras WHERE Pedido = '" + pedido + "'";
                    SqlCommand command = new SqlCommand(select, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                        return true;
                    else
                        return false;
                }
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                return false;

            }
         }



            //Funcion: validacodprodInvIni
            //Proposito: valida si existe informacion de ese codigo en la tabla DetEscuadras (Inv inicial)
            public bool validacodprodInvIni(string codigo)
            {
                string[] parametros = getParametros("Solutia");
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
                try
                {
                    conn.Open();
                    using (conn)
                    {
                        string select = "SELECT * FROM DetEscuadras WHERE OrdenProduccion = 'SIN OP' AND Pedido = 'SIN PEDIDO' AND CodigoProducto = '" + codigo + "'";
                        SqlCommand command = new SqlCommand(select, conn);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                            return true;
                        else
                            return false;
                    }
                    conn.Close();
                }
                catch (Exception e)
                {
                    conn.Close();
                    return false;

                }
             }

            public int obtenerCantidadPiezasEscuadra(int idTag)
            {
                int cantPiezas = 0;
                string[] parametros = getParametros("Solutia");
                string sqlConexion = "Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "";



                using (SqlConnection conn = new SqlConnection(sqlConexion))
                {
                    try
                    {
                        conn.Open();

                        string select = "SELECT Piezas FROM DetEscuadras "
                                      + "WHERE idEscuadra = " + idTag;
                        SqlCommand command = new SqlCommand(select, conn);
                        SqlDataReader reader = command.ExecuteReader(); //creo que aquí es nonquery
                        if (reader.Read())
                        {
                            cantPiezas = Convert.ToInt32(reader["Piezas"]);
                            return cantPiezas;
                        }
                        else
                            return -1;


                    }
                    catch (Exception e)
                    {
                        conn.Close();
                        return -1;

                    }
                    finally
                    {
                        conn.Close();
                    }

                }

            }


            public string sumarPiezasTag(EscuadraVirtual nuevoTag, string cantidadAntTag)
            {
                string[] parametros = getParametros("Solutia");
                int cantidadNuevaTag = Convert.ToInt32(cantidadAntTag) - Convert.ToInt32(nuevoTag.cantidad);
                int cantPiezasTag = obtenerCantidadPiezasEscuadra(Convert.ToInt32(nuevoTag.idTag));

                string sqlConexion = "Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "";



                using (SqlConnection conn = new SqlConnection(sqlConexion))
                {
                    try
                    {
                        conn.Open();

                        string select = "UPDATE FROM DetEscuadras "
                                      + "SET Piezas = " + (cantPiezasTag + cantidadNuevaTag)
                                      + "WHERE idEscuadra = " + nuevoTag.idTag;
                        SqlCommand command = new SqlCommand(select, conn);
                        int resultado = command.ExecuteNonQuery();
                        if (resultado > 0)
                            return "Se actualizó la cantidad de la escuadra de forma satisfactoria";
                        else
                            return "No se pudo actualizar la cantidad de piezas, revise con el área de sistemas.";


                    }
                    catch (Exception e)
                    {
                        conn.Close();
                        return "Hubo un problema al actualizar la escuadra, favor de revisarlo con el área de sistemas.";

                    }
                    finally
                    {
                        conn.Close();
                    }

                }

            }

            //Resta piezas al tag de la remisión
            public string RestaPiezasTag(TagRemision nuevoTag, string cantidadAntTag)
            {
                string[] parametros = getParametros("Solutia");
                int cantidadRestanteTag = Convert.ToInt32(cantidadAntTag) - Convert.ToInt32(nuevoTag.cantidad);

                string sqlConexion = "Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "";



                using(SqlConnection conn = new SqlConnection(sqlConexion))
                {
                    try
                    {
                        conn.Open();

                        string select = "UPDATE FROM DetEscuadras "
                                      + "SET Piezas = " + cantidadRestanteTag 
                                      + "WHERE EPC = " + nuevoTag.idTag;
                        SqlCommand command = new SqlCommand(select, conn);
                        int resultado = command.ExecuteNonQuery();
                        if (resultado > 0)
                            return "Se actualizó la cantidad de la escuadra de forma satisfactoria";
                        else
                            return "No se pudo actualizar la cantidad de piezas en la escuadra, consulte con el área de sistemas.";


                    }
                    catch (Exception e)
                    {
                        conn.Close();
                        return "Hubo un problema al actualizar la escuadra, favor de revisarlo con el área de sistemas.";

                    }
                    finally
                    {
                        conn.Close();
                    }

                }

            }

            //Se asignará la primer escuadra virtual encontrada
            public EscuadraVirtual obtenerEscuadraVirtual()
            {
                EscuadraVirtual nuevaEscuadra = new EscuadraVirtual();

                string[] parametros = getParametros("Solutia");
                string sqlConexion = "Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "";



                using (SqlConnection conn = new SqlConnection(sqlConexion))
                {
                    try
                    {
                        conn.Open();

                        string select = "SELECT TOP 1 idEscuadra, EPC " 
                                     + "FROM DetEscuadras "
                                     + "WHERE OrdenProduccion is NULL AND Asignado = 0 Pedido is NULL";
                        SqlCommand command = new SqlCommand(select, conn);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            nuevaEscuadra.idTag = Convert.ToInt32(reader["idEscuadra"]);
                            nuevaEscuadra.EPC = reader["EPC"].ToString();
                            return nuevaEscuadra;
                        }
                        else
                        {
                            nuevaEscuadra.EPC = "No hay escuadras disponibles.";
                            return nuevaEscuadra;

                        }

                    }
                    catch (Exception e)
                    {
                        nuevaEscuadra.EPC = "Ocurrió un error al obtener la escuadra.";
                        conn.Close();
                        return nuevaEscuadra;

                    }
                    finally
                    {
                        conn.Close();
                    }

                }

            }

            //Asignará una escuadra virtual para remisiones
            public int asignaEscuadraVirtual(EscuadraVirtual nuevaEscuadra)
            {

                string[] parametros = getParametros("Solutia");
                string sqlConexion = "Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "";



                using (SqlConnection conn = new SqlConnection(sqlConexion))
                {
                    try
                    {
                        conn.Open();
                        //AGREGAR LA CANTIDAD DE PIEZAS DE ESA ESCUADRA
                        string select = "UPDATE FROM DetEscuadras "
                                      + "SET  Asignado = 1, OrdenProduccion = 'SIN OP', Pedido = 'SIN PEDIDO'" //cantidad igual á y listo
                                      + "WHERE idEscuadra = " + nuevaEscuadra.idTag + " AND EPC = '" + nuevaEscuadra.EPC + "' ";
                        SqlCommand command = new SqlCommand(select, conn);
                        int resultado = command.ExecuteNonQuery();


                        if (resultado == 1)
                            return nuevaEscuadra.idTag;
                        else
                            return -1;


                    }
                    catch (Exception e)
                    {
                        conn.Close();
                        return -1;

                    }
                    finally
                    {
                        conn.Close();
                    }

                }

            }

            public string insertaDetRemision(DetRemision nuevoDetRemision)
            {

                string[] parametros = getParametros("Solutia");
                string sqlConexion = "Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "";



                using (SqlConnection conn = new SqlConnection(sqlConexion))
                {
                    try
                    {
                        conn.Open();

                        string select = "INSERT INTO DetRemision "
                                      + "VALUES('" + nuevoDetRemision.Remision + "', '" + nuevoDetRemision.Pedido 
                                      + "', '" + nuevoDetRemision.CodigoProducto + "', " + nuevoDetRemision.CantPiezas 
                                      + ", " + nuevoDetRemision.CtaPzaCargada + ", " + nuevoDetRemision.CtaPzaFaltante 
                                      + ", " + nuevoDetRemision.PzaRemision + ")";
                        SqlCommand command = new SqlCommand(select, conn);
                        int resultado = command.ExecuteNonQuery();
                        if (resultado > 0) //corregir esta validación
                            return "";
                        else return "";

                    }
                    catch (Exception e)
                    {
                        conn.Close();
                        return "";

                    }
                    finally
                    {
                        conn.Close();
                    }

                }


            }

        //Funcion: PzaXCodigoRemi
        //Proposito: devuelve la cantidad de piezas de el articulo de una remision
        public int PzaXCodigoRemi(string remision, string codProd, string sucursal)
        {
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                using (conn)
                {
                    string select = "SELECT ROUND(vd.cantidad * ArtUnidad.Factor, 0) AS PzaRemision FROM Venta v INNER JOIN (VentaD vd INNER JOIN Art on art.Articulo = vd.Articulo LEFT JOIN ArtUnidad on ArtUnidad.Articulo = vd.Articulo AND ArtUnidad.Unidad = art.Unidad) on vd.ID = v.ID WHERE v.MovID = '" + remision + "' AND Art.Tipo = 'NORMAL' AND vd.Articulo = '" + codProd + "'";//AND vd.SucursalOrigen = " + sucursal + " SE QUITO ESTO
                    SqlCommand command = new SqlCommand(select, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                        return int.Parse(reader.GetValue(0).ToString());
                    else
                        return -1;
                }
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                return -1;
            }
        }


        //Funcion: updateDetremi
        //Proposito: Cambia estatus del campo conEscuadra ya que a ese articulo de la remision ya se le asigno escuadra virtual
        public string updateDetremi(string remi, string codProd)
        {
            string result = null;
            string query = "";
            int filasAfectadas = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();

                query = "UPDATE detRemision SET conEscuadra = 1 WHERE Remision = '" + remi + "' AND CodigoProducto = '" + codProd + "'";
                cmd.CommandText = query;
                filasAfectadas = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                conn.Close();
                result = e.Message;
            }
            conn.Close();
            return result;
        }


          //Funcion: InfoRemision
        //Proposito: envia los datos de una remision para obtener el numero de productos y crear ciclo
        public DataTable InfoRemision(string remision)
        {
            DataTable res = new DataTable();
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                using (conn)
                {
                    string select = "SELECT Pedido, CodigoProducto, PzaRemision FROM detRemision where Remision = '" + remision + "' AND conEscuadra = 0 ";
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


        //FUNCNION: infoEscuadra
        //devuelve la informacion de la escuadra para comparar con la que hay en remision en tago y poder completar el ciclo de remisiones
        public int infoEscuadra(string epc)
        {
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                using (conn)
                {
                    //string select = "SELECT Piezas FROM DetEscuadras WHERE EPC = '" + epc + "' AND ISNULL(OrdenProduccion, '') != 'SIN OP'";
                    string select = "SELECT Piezas FROM DetEscuadras WHERE EPC = '" + epc + "'";// AND ISNULL(OrdenProduccion, '') != 'SIN OP'";
                    SqlCommand command = new SqlCommand(select, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                        return int.Parse(reader.GetValue(0).ToString());
                    else
                        return -1;
                }
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                return -1;
            }
        }


        //FUNCNION: infoEscuadraArt
        //devuelve la informacion de la escuadra para comparar con la que hay en remision en tago y poder completar el ciclo de remisiones
        public string infoEscuadraArt(string epc)
        {
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                using (conn)
                {
                    string select = "SELECT CodigoProducto FROM DetEscuadras WHERE EPC = '" + epc + "'";
                    SqlCommand command = new SqlCommand(select, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                        //return int.Parse(reader.GetValue(0).ToString());
                        return reader.GetValue(0).ToString();
                    else
                        return "-1";
                }
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                return "-1";
            }
        }


        //Funcion: updapzaRemi
        //Proposito: ACTUALIZA EN LA ESCUADRA LEIDA LAS PIEZAS QUE SE LE DESCONTARON
        public string updapzaRemi(string epc, int diferencia)
        {
            string result = null;
            string query = "";
            int filasAfectadas = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();
                if (diferencia == 0)
                {
                    query = "UPDATE DETESCUADRAS " +
                                "SET " +
                                "OrdenProduccion = NULL," +
                                "Asignado = 0," +
                                "Ubicada = 0," +
                                "CodigoProducto = NULL," +
                                "Picked = 0," +
                                "Embarcado = 0," +
                                "Piezas = 0," +
                                "Pedido = NULL," +
                                "Posicion = NULL," +
                                "Pendiente = 0," +
                                "pzaRemi = 0," +
                                "newIdEscuadra = NULL," +
                                "Lote = NULL " +
                            "WHERE EPC = '" + epc + "'";
                }
                else
                {
                    query = "UPDATE DetEscuadras SET Piezas = " + diferencia + " WHERE EPC = '" + epc + "' ";
                }

                cmd.CommandText = query;
                filasAfectadas = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                conn.Close();
                result = e.Message;
            }
            conn.Close();
            return result;
        }

        
        public string EmbarcaEscVirt(string epc)
        {
            string result = null;
            string query = "";
            int filasAfectadas = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();

                query = "UPDATE DetEscuadras SET Embarcado = 1 WHERE EPC = '" + epc + "' ";
                cmd.CommandText = query;
                filasAfectadas = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                conn.Close();
                result = e.Message;
            }
            conn.Close();
            return result;
        }


        //FUNCNION: pzasCargadasEsc
        //devuelve las piezas que han sido cargadas en la escuadra virtual
        public string pzasCargadasEsc(string newid)
        {
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                using (conn)
                {
                    string select = "SELECT pzaRemi FROM DetEscuadras WHERE newIdEscuadra = '"+newid+"'";
                    SqlCommand command = new SqlCommand(select, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                        //return int.Parse(reader.GetValue(0).ToString());
                        return reader.GetValue(0).ToString();
                    else
                        return "-1";
                }
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                return "-1";
            }
        }

        //obtiene la fechaEmision de una Orden de Produccion
        public string getDateOP(string op)
        {
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                using (conn)
                {
                    string select = "SELECT CONVERT(varchar, FechaEmision, 0) FROM Prod WHERE MovID = '" + op + "'";
                    SqlCommand command = new SqlCommand(select, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                        return reader.GetValue(0).ToString();
                    else
                        return "-1";
                }
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                return "-1";
            }
        }


        //FUNCNION: pedidoRemi
        //obtiene el pedido de una remision
        public string pedidoRemi2(int pzaRemi, string remi, string codprod)
        {
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                using (conn)
                {
                    string select = "SELECT Pedido FROM detRemision WHERE PzaRemision = " + pzaRemi + " AND Remision = '" + remi + "' AND CodigoProducto = '" + codprod + "'";
                    SqlCommand command = new SqlCommand(select, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                        //return int.Parse(reader.GetValue(0).ToString());
                        return reader.GetValue(0).ToString();
                    else
                        return "-1";
                }
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                return "-1";
            }
        }


        //Funcion: actualizaPzaEsc
        //Proposito: ACTUALIZA EN LA ESCUADRA LEIDA LAS PIEZAS QUE SE LE DESCONTARON
        public string actualizaPzaEsc(string epc, int diferencia)
        {
            string result = null;
            string query = "";
            int filasAfectadas = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();

                query = "UPDATE DetEscuadras SET pzaRemi = " + diferencia + " WHERE EPC = '" + epc + "' ";
                cmd.CommandText = query;
                filasAfectadas = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                conn.Close();
                result = e.Message;
            }
            conn.Close();
            return result;
        }


        //FUNCNION: getEscVirtual
        //obtiene el pedido de una remision
        public string getEscVirtual(string newid)
        {
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                using (conn)
                {
                    string select = "SELECT EPC FROM DetEscuadras WHERE newIdEscuadra = '"+newid+"'";
                    SqlCommand command = new SqlCommand(select, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                        //return int.Parse(reader.GetValue(0).ToString());
                        return reader.GetValue(0).ToString();
                    else
                        return "-1";
                }
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                return "-1";
            }
        }


        //FUNCNION: getEscVirtual
        //obtiene el pedido de una remision
        public string escVirtualEPC(string pedido, string codigo, string remi)
        {
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                using (conn)
                {
                    string select = "SELECT idRemi FROM detRemision WHERE Remision = '"+remi+"' AND CodigoProducto = '"+codigo+"' AND Pedido = '"+pedido+"'";
                    SqlCommand command = new SqlCommand(select, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                        //return int.Parse(reader.GetValue(0).ToString());
                        return reader.GetValue(0).ToString();
                    else
                        return "-1";
                }
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                return "-1";
            }
        }



        //Funcion: pzaCargadaComplete
        //Proposito: ACTUALIZA DE 0 A 1 EL CAMPO PzasRemiCompletas (ESTO SIGNIFICA QUE SE CARGARON TODAS LAS PIEZAS EN LA ESC VIRTUAL)
        public string pzaCargadaComplete(string newid)
        {
            string result = null;
            string query = "";
            int filasAfectadas = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();

                query = "UPDATE detRemision SET PzasRemiCompletas = 1 WHERE  idRemi = '"+newid+"'";
                cmd.CommandText = query;
                filasAfectadas = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                conn.Close();
                result = e.Message;
            }
            conn.Close();
            return result;
        }


        //Funcion: getNewIdEsc
        //Proposito:Obtiene el newId de una escuadra.
        public string getNewIdEsc(string EPC)
        {
            string res = "";
            string[] parametros = getParametros("Solutia");
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
                string selectUnidad = "SELECT newIdEscuadra FROM DetEscuadras WHERE EPC = '" + EPC + "'";
                conn.Open();
                SqlCommand cmdUnidad = new SqlCommand(selectUnidad, conn);
                SqlDataReader readUnidad = cmdUnidad.ExecuteReader();
                if (readUnidad.Read())
                {
                    res = readUnidad.GetValue(0).ToString();
                    return res;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                string error = e.Message;
                return null;
            }
        }


        //Funcion: getInfoReportMerma
        //Proposito: regresa un array con los datos de una escuadra con el EPC leido por el IP30
        public string[] getInfoReportMerma(string epc, string articulo)
        {

            string[] data = new string[6];
            string[] data2 = new string[1];
            string codigo = "";
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                //obtener el resultado de la consulta
                conn.Open();
                string select2 = "SELECT CodigoProducto from DetEscuadras WHERE EPC = '" + epc + "'";
                SqlCommand cmd2 = new SqlCommand(select2, conn);
                SqlDataReader reader2 = cmd2.ExecuteReader();
                if (reader2.Read())
                {
                    int sizeReader = reader2.FieldCount;
                    for (int x = 0; x < sizeReader; x++)
                    {
                        data2[x] = reader2.GetValue(x).ToString();
                    }
                }
                codigo = data2[0];
                conn.Close();
                if (data2[0].Equals("NULL"))
                {
                    conn.Open();
                    string select = "SELECT OrdenProduccion," +
                                        "(SELECT Familia FROM [192.168.0.247].[PRUEBASNAPRESA].dbo.Art WHERE Articulo = '" + codigo + "')  AS TIPO," +
                                        "(SELECT Fabricante FROM [192.168.0.247].[PRUEBASNAPRESA].dbo.Art WHERE Articulo = '" + codigo + "') AS COLOR," +
                                        "(SELECT Descripcion1 FROM [192.168.0.247].[PRUEBASNAPRESA].dbo.Art WHERE Articulo = '" + codigo + "') AS DESCRIPCION," +
                                        "Piezas AS CANTIDAD," +
                                        "(SELECT Articulo FROM [192.168.0.247].[PRUEBASNAPRESA].dbo.Art WHERE Articulo = '" + codigo + "') AS ARTICULO " +
                                    "FROM DetEscuadras " +
                                    "WHERE EPC = '" + epc + "'";
                    
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
                else
                {
                    conn.Open();

                    //string select = "SELECT de.OrdenProduccion,cp.Pedido,cp.Cliente,cp.Descripcion, de.Piezas, de.CodigoProducto FROM DetEscuadras de INNER JOIN catProd cp on cp.id_parcialidad = de.newIdEscuadra WHERE de.EPC = '" + epc + "' AND de.OrdenProduccion = (SELECT OrdenProduccion FROM DetEscuadras WHERE EPC = '" + epc + "')";  //AND cp.Codigo = '" + codigo + "'"; //jlmq 13dic2018 pruebas
                    string select = "SELECT OrdenProduccion,"+
                                        "(SELECT Familia FROM [192.168.0.247].[PRUEBASNAPRESA].dbo.Art WHERE Articulo = '"+codigo+"')  AS TIPO," +
                                        "(SELECT Fabricante FROM [192.168.0.247].[PRUEBASNAPRESA].dbo.Art WHERE Articulo = '" + codigo + "') AS COLOR," +
                                        "(SELECT Descripcion1 FROM [192.168.0.247].[PRUEBASNAPRESA].dbo.Art WHERE Articulo = '" + codigo + "') AS DESCRIPCION," +
	                                    "Piezas AS CANTIDAD,"+
                                        "(SELECT Articulo FROM [192.168.0.247].[PRUEBASNAPRESA].dbo.Art WHERE Articulo = '" + codigo + "') AS ARTICULO " +
                                    "FROM DetEscuadras "+
                                    "WHERE EPC = '"+epc+"'";

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
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message;
                data = null;
            }
            return data;
        }


        //Funcion: ActualizaCantidadParcialidad
        //Proposito: Actualiza en Catprod el campo cantidad al de la parcialidad
        public string ActualizaCantidadParcialidad(string newId, int cantidad)
        {
            string result = null;
            string query = "";
            int filasAfectadas = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();

                query = "UPDATE catProd SET CantidadParcialidad = " + cantidad + " WHERE id_parcialidad = '" + newId + "'";
                cmd.CommandText = query;
                filasAfectadas = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                conn.Close();
                result = e.Message;
            }
            conn.Close();
            return result;
            

        }


        //Funcion: validaParciExist
        //Proposito: valida si una remision existe en la base de datos de TAGO
        public bool validaParciExist(string newId)
        {
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                using (conn)
                {
                    string select = "SELECT CantidadParcialidad FROM catProd WHERE id_parcialidad = '" + newId + "' AND CantidadParcialidad IS NOT NULL AND CantidadParcialidad > 0";
                    SqlCommand command = new SqlCommand(select, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                        return true;
                    else
                        return false;
                }
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message;
                return true;                
            }
        }


        //Funcion: CalculaPedidoParcialidadImprimir
        //Proposito: calcula cuantas etiquetas se van a imprimir.
        public int CalculaPedidoParcialidadImprimir(string newId)
        {
            int cantidad = 0;
            int res = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            //primero se obtiene la cantidad de producto por ventana
            try
            {
                conn.Open();
                string select = "SELECT CantidadParcialidad FROM catProd WHERE id_parcialidad = '"+ newId +"'";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    cantidad = int.Parse(reader.GetValue(0).ToString());
                    res = cantidad;
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


        //Funcion: reportaMermaProd
        //Proposito: Actualiza mermas en catprod para modulo produccion parcialidades.
        public bool reportaMermaProd(string newId, int mermas)
        {
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            int merma = 0;
            try
            {
                conn.Open();

                string select = "SELECT mermas FROM catProd WHERE id_parcialidad = '" + newId + "'";
                SqlCommand cmd1 = new SqlCommand(select, conn);
                SqlDataReader reader = cmd1.ExecuteReader();
                if (reader.Read())
                {
                    merma = int.Parse(reader.GetValue(0).ToString());                    
                }
                mermas = merma + mermas;
                conn.Close();

                conn.Open();
                string update = "UPDATE catProd SET mermas = "+ mermas +" WHERE id_parcialidad = '"+ newId +"' ";
                SqlCommand cmd = new SqlCommand(update, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                return false;
            }
            return true;
        }


        //Funcion: CalculaPedidoParcialidadImprimir
        //Proposito: calcula cuantas etiquetas se van a imprimir.
        public int getConteo(int idConteo)
        {
            int conteo = 0;
            int res = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            //primero se obtiene la cantidad de producto por ventana
            try
            {
                conn.Open();
                string select = "SELECT numConteo FROM InvCongelado WHERE idInv = " + idConteo + "";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    conteo = int.Parse(reader.GetValue(0).ToString());
                    res = conteo;
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

        //Funcion: CalculaPedidoParcialidadImprimir
        //Proposito: calcula cuantas etiquetas se van a imprimir.
        public int getStatusConteo(int idConteo)
        {
            int status = 0;
            int res = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            //primero se obtiene la cantidad de producto por ventana
            try
            {
                conn.Open();
                string select = "SELECT status FROM InvCongelado WHERE idInv = " + idConteo + "";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    status = int.Parse(reader.GetValue(0).ToString());
                    res = status;
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

        //obtiene la ubicacion de un conteo
        public string getUbiConteo(int idConteo)
        {
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                using (conn)
                {
                    string select = "SELECT ubicacionConteo FROM InvCongelado WHERE idInv = "+idConteo+"";
                    SqlCommand command = new SqlCommand(select, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                        //return int.Parse(reader.GetValue(0).ToString());
                        return reader.GetValue(0).ToString();
                    else
                        return "-1";
                }
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                return "-1";
            }
        }

        
        //Proposito: cantidad de tags por ubicacion seleccionada para conteos.
        public int getNumConteo(string ubicacion, int idInv)
        {
            int conteo = 0;
            int res = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            //primero se obtiene la cantidad de producto por ventana
            try
            {
                conn.Open();
                string select = "SELECT COUNT(*) FROM DetalleCongelado WHERE ProdCB = '"+ubicacion+"' AND idInvCong = "+idInv+" AND Estatus = 0";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    conteo = int.Parse(reader.GetValue(0).ToString());
                    res = conteo;
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

        //obtiene la descripcion de un codigo
        public string getDescripcionCodigo(string codigo)
        {
            string descripcion = "";
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                using (conn)
                {
                    string select = "SELECT Descripcion1 FROM Art WHERE Articulo = '"+codigo+"'";
                    SqlCommand command = new SqlCommand(select, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                        //return int.Parse(reader.GetValue(0).ToString());
                        descripcion = reader.GetValue(0).ToString();
                    else
                        descripcion = "SIN DESCRIPCION";
                }
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                descripcion = "SIN DESCRIPCION";
            }
            return descripcion;
        }

        //obtiene el codigo de un tag
        public string getCodigoEsc(int tag)
        {
            string codigo = "";
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                
                conn.Open();
                using (conn)
                {
                    string select = "SELECT UPPER(CodigoProducto) FROM DetEscuadras WHERE idEscuadra = "+tag+" AND Asignado = 1 AND Ubicada = 1";
                    SqlCommand command = new SqlCommand(select, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                        //return int.Parse(reader.GetValue(0).ToString());
                        codigo = reader.GetValue(0).ToString();
                    else
                        codigo = "SIN CODIGO";
                }
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                codigo = "SIN CODIGO";
            }
            return codigo;
        }

        //FUNCNION: pzaEscReclasificacion
        public int pzaEscReclasificacion(int tag)
        {
            int piezas = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                using (conn)
                {
                    
                    string select = "SELECT Piezas FROM DetEscuadras WHERE idEscuadra = " + tag + "";
                    SqlCommand command = new SqlCommand(select, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                        piezas = int.Parse(reader.GetValue(0).ToString());
                    else
                        piezas = -1;
                }
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                piezas = -1;
            }
            return piezas;
        }


        //actualiza status y numConteo de un Inventario
        public bool updateStatusInv(int idConteo, int numConteo)
        {
            bool result = true;
            string[] parametros2 = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + "");

            try
            {
                numConteo = numConteo + 1;

                string update = "";

                update = "UPDATE InvCongelado SET status = "+numConteo+" , numConteo = "+numConteo+" WHERE idInv = "+idConteo+"";
                Ejecuta(update, conn);
                result = true;
             
                

            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message;
                return result = false;
            }
            return result;

        }

        //ACTUALIZA ESCUADRAS EN EL PROCESO DE RECLASIFICACION
        public bool updateReclasificacion(int tagSalida, int tagIngreso, int difSalida, int difIngreso)
        {
            bool result = true;
            string updateSalida = "";
            string updateIngreso = "";
            string clean = "";

            string[] parametros2 = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + "");

            try
            {
                if (difSalida == 0)
                {
                    clean = "UPDATE DETESCUADRAS " +
                              "SET " +
                              "OrdenProduccion = NULL," +
                              "Asignado = 0," +
                              "Ubicada = 0," +
                              "CodigoProducto = NULL," +
                              "Picked = 0," +
                              "Embarcado = 0," +
                              "Piezas = 0," +
                              "Pedido = NULL," +
                              "Posicion = NULL," +
                              "Pendiente = 0," +
                              "pzaRemi = 0," +
                              "newIdEscuadra = NULL," +
                              "Lote = NULL " +
                            "WHERE idEscuadra = " + tagSalida + "";
                    Ejecuta(clean, conn);
                    result = true;

                    updateIngreso = "UPDATE DetEscuadras SET Piezas = " + difIngreso + " WHERE idEscuadra = " + tagIngreso + " ";
                    Ejecuta(updateIngreso, conn);
                    result = true;
                }
                else
                {
                    updateSalida = "UPDATE DetEscuadras SET Piezas = " + difSalida + " WHERE idEscuadra = " + tagSalida + "";
                    Ejecuta(updateSalida, conn);
                    result = true;

                    updateIngreso = "UPDATE DetEscuadras SET Piezas = " + difIngreso + " WHERE idEscuadra = " + tagIngreso + " ";
                    Ejecuta(updateIngreso, conn);
                    result = true;
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string error = exc.Message;
                return result = false;
            }
            return result;

        }

        //obtiene el codigo de un tag
        public string getUbicacionEsc(int tag)
        {
            string codigo = "";
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {

                conn.Open();
                using (conn)
                {
                    string select = "SELECT Posicion FROM DetEscuadras WHERE idEscuadra = " + tag + " AND Asignado = 1 AND Ubicada = 1";
                    SqlCommand command = new SqlCommand(select, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                        //return int.Parse(reader.GetValue(0).ToString());
                        codigo = reader.GetValue(0).ToString();
                    else
                        codigo = "SIN UBICACIÓN";
                }
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                codigo = "SIN UBICACIÓN";
            }
            return codigo;
        }

        
    
    }
}

