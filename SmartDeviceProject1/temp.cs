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

namespace SmartDeviceProject1
{
    class temp
    {
        public string[] getParametros(string Descripcion)
        {
            string[] result = new string[5];
            try
            {
                //SqlConnection conn = new SqlConnection("Data Source=172.16.1.31;Initial Catalog=NapresaPar;Persist Security Info=True;User ID=sa;Password=Adminpwd20");
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

        public int CxEsc(string epc, string op, string codigo)
        {
            int qty = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
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

        public string EntradaProduccionParcial(string folio, string estado, string id, string renglon, Decimal cantidad, string user, string codigo)
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
                int i = 0;
                int max = maxRenglonSub(id, codigo) + 1;
                do
                {
                    Decimal total = getTotalCantPend(id, renglon, i, codigo);
                    if (total > 0 && cantidad > 0)
                    {
                        if (cantidad >= total)
                        {
                            conn2.Open();
                            string[] centroDestino = getCentroData(id, renglon, i);;
                            if (centroDestino[0].Contains("CURAD"))
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
                            else if (!centroDestino[0].Contains("CURAD") && !centroDestino[1].Contains("CURAD"))
                            {
                                string updateProd = "update ProdD set CantidadA = " + cantidad + " where id=" + id + " and Renglon=" + renglon + " and RenglonSub = " + i;
                                Ejecuta(updateProd, conn2);
                                string spUpdate = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Seleccion', 'Entrada Produccion', '" + user + "', @Estacion=99";
                                Ejecuta(spUpdate, conn2);
                                string spFinal = "EXEC spAfectar 'PROD'," + getIdProd() + ", 'AFECTAR', 'Todo', Null, '" + user + "', @Estacion=99";
                                Ejecuta(spFinal, conn2);
                                i++;
                            }
                            else
                            {
                                i++;
                            }
                        }
                        else if (total > cantidad)
                        {
                            conn2.Open();
                            string[] centroDestino = getCentroData(id, renglon, i);
                            if (centroDestino[0].Contains("CURAD"))
                            {
                                string updateProd = "update ProdD set CantidadA = " + cantidad + " where id=" + id + " and Renglon=" + renglon + " and RenglonSub = " + i;
                                Ejecuta(updateProd, conn2);
                                string spUpdate = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Seleccion', 'Entrada Produccion', '" + user + "', @Estacion=99";
                                Ejecuta(spUpdate, conn2);
                                string spFinal = "EXEC spAfectar 'PROD'," + getIdProd() + ", 'AFECTAR', 'Todo', Null, '" + user + "', @Estacion=99";
                                Ejecuta(spFinal, conn2);
                                max = max + 1;
                                i = max;
                            }
                            else if (!centroDestino[0].Contains("CURAD") && !centroDestino[1].Contains("CURAD"))
                            {
                                string updateProd = "update ProdD set CantidadA = " + cantidad + " where id=" + id + " and Renglon=" + renglon + " and RenglonSub = " + i;
                                Ejecuta(updateProd, conn2);
                                string spUpdate = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Seleccion', 'Entrada Produccion', '" + user + "', @Estacion=99";
                                Ejecuta(spUpdate, conn2);
                                string spFinal = "EXEC spAfectar 'PROD'," + getIdProd() + ", 'AFECTAR', 'Todo', Null, '" + user + "', @Estacion=99";
                                Ejecuta(spFinal, conn2);
                                max = max + 1;
                                i = max;
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
                int[] curado = CurrentCurado(codigo, folio);
                if (curado[0] == 0)
                {
                    string updateSolutia = "update catProd set Estatus = 'PENDIENTE', Asignado = 0 where OrdenProduccion='" + folio + "' and Renglon=" + renglon + "";
                    Ejecuta(updateSolutia, conn);
                    result = "Cambio Exitoso";
                }
                else
                {
                    string updateSolutia = "update catProd set Estatus = 'PENDIENTE' where OrdenProduccion='" + folio + "' and Renglon=" + renglon + "";
                    Ejecuta(updateSolutia, conn);
                    result = "Cambio Exitoso";
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
                string insertInvD = "Insert into INVD (ID,Renglon,RenglonSub,RenglonId,Renglontipo,Cantidad,CantidadPendiente,Almacen,Codigo,Articulo,Unidad,Factor,Producto,Tipo,Sucursal)" +
                            "values (" + idInv + "," + renglon + ",0," + 1 + ",'N'," + nvoQty + "," + nvoQty + ",'APT-BUS',null,'" + code + "',(select unidad from Art where Articulo='" + code + "'),(select factor from Art where Articulo='" + code + "'),'" + code + "','Salida'," + sucursal + ")";
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
                string updateCantidad = "Update DetEscuadras set Piezas = " + original + " where EPC = '" + epc + "'";
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

        public int MermaEmbarques(int id, int original, int nvoQty, string op, string code, int renglon, string epc, string user, string sucursal)
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
                string movId = setMovID("Merma Embarques", sucursal);
                string insertInv = "Insert into INV (Empresa,Mov,MovID,FechaEmision,UltimoCambio,UEN,Moneda,TipoCambio,Usuario,Referencia,Observaciones,Estatus,Directo,Almacen,AlmacenTransito,OrigenTipo,Origen,OrigenID,Sucursal,SucursalOrigen,SucursalDestino)" +
        "values					('GNAP','Merma Embarques','" + movId + "',GETDATE(),GETDATE(),2,'Pesos',1,'" + user + "',(select referencia from prod where id=" + id + "),(select observaciones from prod where id=" + id + "),'PENDIENTE',1,'APT-BUS','(TRANSITO)','PROD','Entrada Produccion','" + op + "'," + sucursal + "," + sucursal + "," + sucursal + ")";
                SqlCommand cmdInv = new SqlCommand(insertInv, conn);
                cmdInv.ExecuteNonQuery();
                conn.Close();
                conn.Open();
                int idInv = getIdInv();
                string insertInvD = "Insert into INVD (ID,Renglon,RenglonSub,RenglonId,Renglontipo,Cantidad,CantidadPendiente,Almacen,Codigo,Articulo,Unidad,Factor,Producto,Tipo,Sucursal)" +
                            "values (" + idInv + "," + renglon + ",0," + 1 + ",'N'," + nvoQty + "," + nvoQty + ",'APT-BUS',null,'" + code + "',(select unidad from Art where Articulo='" + code + "'),(select factor from Art where Articulo='" + code + "'),'" + code + "','Salida'," + sucursal + ")";
                SqlCommand cmdInvD = new SqlCommand(insertInvD, conn);
                cmdInvD.ExecuteNonQuery();
                conn.Close();
                conn.Open();
                string spAfectarMP = "EXEC spAfectar 'INV', " + idInv + ",'Afectar','Todo','Merma Embarques','" + user + "',@Estacion = 99";
                SqlCommand cmdAfectarMP = new SqlCommand(spAfectarMP, conn);
                cmdAfectarMP.ExecuteNonQuery();
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
                string updateCantidad = "Update DetEscuadras set Piezas = " + original + " where EPC = '" + epc + "'";
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
                        renglonId = renglon / 2048;
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
                                string insertInvD = "Insert into INVD (ID,Renglon,RenglonSub,RenglonId,Renglontipo,Cantidad,Almacen,Codigo,Articulo,CantidadPendiente,Unidad,Factor,Producto,Tipo,Sucursal)" + "values (" + getIdInv() + "," + renglon + ",0," + renglonId + ",'N'," + rackHuecos + ",'APT-BUS',null,'" + codigo + "'," + rackHuecos + ",(select unidad from Art where Articulo='" + codigo + "'),(select factor from Art where Articulo='" + codigo + "'),'" + codigo + "','Salida'," + sucursal + ")";
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
                                string insertInvD = "Insert into INVD (ID,Renglon,RenglonSub,RenglonId,Renglontipo,Cantidad,Almacen,Codigo,Articulo,Unidad,Factor,Producto,Tipo,Sucursal)" + "values (" + getIdInv() + "," + renglon + ",0," + renglonId + ",'N'," + rackHuecos + ",'APT-BUS',null,'" + codigo + "',(select unidad from Art where Articulo='" + codigo + "'),(select factor from Art where Articulo='" + codigo + "'),'" + codigo + "','Salida'," + sucursal + ")";
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
                    conn.Close();
                }
                else
                {
                    conn.Close();
                    res = res++;
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

        public int asignadaOP(string OP)
        {
            int result = 0;
            int count = 0;
            string[] parametros = getParametros("Solutia");
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

        public bool Ejecuta(string Cadena, SqlConnection con)
        {
            try
            {
                con.Open();
                SqlCommand comand = new SqlCommand(Cadena, con);
                comand.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                //JLMQ using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\mike.txt", true)) { writer.WriteLine("Error: " + ex.Message + "\n" + Cadena + "| Hora: " + DateTime.Now + ""); }
                return false;
            }
            return true;

        }

        public string avanzarEstado(string folio, string estado, string id, string renglon, int cantidad, string user)
        {
            string result = "Cambio Exitoso";
            string[] parametros = getParametros("Solutia");
            string[] parametros2 = getParametros("intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + "");

            try
            {
                conn.Open();
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
                reader.Close();
                conn.Close();
                conn.Open();
                int i = 0;
                int qtyAsignado = 0;
                int max = maxRenglonSub(id, codigo);
                string selectAsignado = "SELECT prodCurado from CatProd where OrdenProduccion ='" + folio + "' and renglon = " + renglon + "";
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
                int qtyCur = currentValue(folio, estado, id, renglon);
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
                                string updateParcialidad = "UPDATE catProd set prodCurado = " + qtyNvo + ", Estatus = 'LIBERADO', prodAsignado = " + qtyCur + " where OrdenProduccion ='" + folio + "' and renglon = " + renglon + "";
                                Ejecuta(updateParcialidad, conn);
                                result = "Cambio Exitoso";
                                break;
                            }
                            else if (!destino[0].Contains("CURAD") && !destino[1].Contains("CURAD"))
                            {
                                string updateParcialidad = "UPDATE catProd set prodCurado = " + qtyNvo + ", Estatus = 'LIBERADO', prodAsignado = " + qtyCur + " where OrdenProduccion ='" + folio + "' and renglon = " + renglon + "";
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
                                string updateParcialidad = "UPDATE catProd set prodCurado = " + qtyNvo + ", Estatus = 'LIBERADO', prodAsignado = " + qtyCur + " where OrdenProduccion ='" + folio + "' and renglon = " + renglon + "";
                                Ejecuta(updateParcialidad, conn);
                                result = "Cambio Exitoso";
                                break;
                            }
                            else if (!destino[0].Contains("CURAD") && !destino[1].Contains("CURAD"))
                            {
                                string updateParcialidad = "UPDATE catProd set prodCurado = " + qtyNvo + ", Estatus = 'LIBERADO', prodAsignado = " + qtyCur + " where OrdenProduccion ='" + folio + "' and renglon = " + renglon + "";
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
                        i++;
                    }
                } while (i <= max);
                //string updateSolutia = "update catProd set Estatus = 'PENDIENTE', Asignado = 0 where OrdenProduccion='" + folio + "' and Renglon=" + renglon + "";
                //SqlCommand cmdSolutia = new SqlCommand(updateSolutia, conn);
                //cmdSolutia.ExecuteNonQuery();
                //conn.Close();
                //conn2.Close();
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
                    res = Convert.ToInt32(Tarimas);
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
                            cantidad = cantidad * 14; break;
                        case 2:
                            cantidad = cantidad * 18; break;
                        case 3:
                            cantidad = cantidad * 21; break;
                        case 4:
                            cantidad = cantidad * 18; break;
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

        public int[] checkAsignados(string folio)
        {
            int[] count = new int[2];
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                string select = "Select count(IdDRProd) from DetRProd where OrdenProduccion = '" + folio + "'";
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

        public int checkRacks(string folio)
        {
            int res = 0;
            int cantidadAsignados = 0;
            int cantidadLeidos = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            SqlConnection conn2 = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                string select = "Select count(IdDRProd) from DetRProd where OrdenProduccion = '" + folio + "'";
                string select2 = "Select count(IdDRProd) from DetRProd where Verificado = 1 and OrdenProduccion='" + folio + "'";
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

        public int clearEscuadra(string EPC)
        {
            int res = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                string update = "update DetEscuadras SET OrdenProduccion = NULL, Asignado = 0, Ubicada = 0, CodigoProducto = NULL, Picked = 0, Embarcado = 0, Piezas = 0 where EPC = '" + EPC + "'";
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

        public int clearRack(string OP, string renglon)
        {
            int res = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                string update = "update DetRProd SET OrdenProduccion = NULL, Estado = 0, Verificado = 0, CantidadEstimada = 0, CantidadReal = 0, CodigoProducto = NULL, ContadoProd = 0, ContadoCurado = 0, Renglon = NULL where OrdenProduccion = '" + OP + "' and ContadoProd = 1 and ContadoCurado = 1 and Renglon = " + renglon + "";
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

        /*
        public int contarHuecos(int huecos, int id, string epc, int tr, string codigo, string user, string sucursal)
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
                string selectEstimado = "Select CantidadReal, OrdenProduccion from DetRProd where EPC ='" + epc + "' and CodigoProducto = '" + codigo + "'";
                SqlCommand cmdEstimado = new SqlCommand(selectEstimado, conn2);
                SqlDataReader readerEstimado = cmdEstimado.ExecuteReader();
                int cantidadEstimada = 0;
                int antesImpuestos = 0;
                string ordenProduccion = "";
                if (readerEstimado.Read())
                {
                    cantidadEstimada = readerEstimado.GetInt32(0);
                    antesImpuestos = readerEstimado.GetInt32(0) + huecos;
                    ordenProduccion = readerEstimado.GetString(1);
                }
                else
                {
                    return res = 1;
                }
                conn2.Close();
                conn2.Open();
                if (huecos > 0 && huecos < antesImpuestos)
                {
                    int cantidadReal = cantidadEstimada;
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
                    conn2.Close();
                    conn2.Open();
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
                    conn2.Close();
                    conn2.Open();
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
                        int nuevoCurado = cantidadCurado - cantidadEstimada, nuevoLiberado = cantidadLiberado + cantidadReal, nuevoMermas = cantidadMerma + huecos;
                        string selectCentroDestino = "SELECT Centro,CentroDestino from ProdD where id = " + id + " and Renglon = " + renglon + " and RenglonSub = " + contador + " and Articulo = '" + codigo + "'";
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
                                //string updateDetRprod = "UPDATE DetRPRod set CantidadReal = " + cantidadReal + " where EPC = '" + epc + "'";
                                //SqlCommand cmdDetRProd = new SqlCommand(updateDetRprod, conn2);
                                //cmdDetRProd.ExecuteNonQuery();
                                //conn2.Close();
                                //conn2.Open();
                                //luego actualizar CatProd
                                string updateCatProd = "UPDATE catProd set prodCurado = " + nuevoCurado + ", prodLiberado = " + nuevoLiberado + ", mermas = " + nuevoMermas + " where Codigo = '" + codigo + "' and OrdenProduccion = '" + ordenProduccion + "'";
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
                                //por ultimo se insertan las mermas en Inv e InvD
                                string movId = setMovID("Merma Produccion", sucursal);
                                string insertInv = "Insert into INV (Empresa,Mov,MovID,FechaEmision,UltimoCambio,UEN,Moneda,TipoCambio,Usuario,Referencia,Observaciones,Estatus,Directo,Almacen,AlmacenTransito,OrigenTipo,Origen,OrigenID,Sucursal,SucursalOrigen,SucursalDestino)" + "values ('GNAP','Merma Produccion','" + movId + "',GETDATE(),GETDATE(),2,'Pesos',1,'" + user + "',(select referencia from prod where id=" + id + "),(select observaciones from prod where id=" + id + "),'PENDIENTE',1,'APT-BUS','(TRANSITO)','PROD','Entrada Produccion','" + ordenProduccion + "'," + sucursal + "," + sucursal + "," + sucursal + ")";
                                Ejecuta(insertInv, conn);
                                int idInv = getIdInv();
                                string insertInvD = "Insert into INVD (ID,Renglon,RenglonSub,RenglonId,Renglontipo,Cantidad,Almacen,Codigo,Articulo,Unidad,Factor,Producto,Tipo,Sucursal)" + "values (" + getIdInv() + "," + renglon + ",0," + renglonId + ",'N'," + huecos + ",'APT-BUS',null,'" + codigo + "',(select unidad from Art where Articulo='" + codigo + "'),(select factor from Art where Articulo='" + codigo + "'),'" + codigo + "','Salida'," + sucursal + ")";
                                Ejecuta(insertInvD, conn);
                                string spAfectarMP = "EXEC spAfectar 'INV', " + idInv + ",'Afectar','Todo','Merma Produccion','" + user + "',@Estacion = 99";
                                Ejecuta(spAfectarMP, conn);
                                string act = "Update Inv set Estatus = 'CONCLUIDO' where id = " + idInv + "";
                                Ejecuta(act, conn);
                                string insertMovFlujo = "Insert into movFlujo values (" + sucursal + ",'GNAP','PROD'," + id + ",'Entrada Produccion','" + ordenProduccion + "','INV'," + idInv + ",'Merma Produccion','" + movId + "',0)";
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
                                string updateCatProd = "UPDATE catProd set prodCurado = " + nuevoCurado + ", prodLiberado = " + nuevoLiberado + ", mermas = " + nuevoMermas + " where Codigo = '" + codigo + "' and OrdenProduccion = '" + ordenProduccion + "'";
                                Ejecuta(updateCatProd, conn2);
                                //ahora sigue que actualicemos ProdD
                                string updateProdD = "UPDATE ProdD set CantidadA = " + huecos + " where id = " + id + " and RenglonSub = " + contador + "";
                                Ejecuta(updateProdD, conn);
                                //Generamos la entrada de produccion
                                string generarMerma = "EXEC spAfectar 'PROD', " + id + ", 'GENERAR', 'Seleccion', 'Entrada Produccion', '" + user + "', @Estacion = 99";
                                Ejecuta(generarMerma, conn);
                                //afectamos la entrada de produccion generada
                                string afectarMerma = "EXEC spAfectar 'PROD', " + getIdProd() + ", 'AFECTAR', 'Todo', 'Entrada Produccion', '" + user + "', @Estacion = 99";
                                Ejecuta(afectarMerma, conn);
                                //por ultimo se insertan las mermas en Inv e InvD
                                string movId = setMovID("Merma Produccion", sucursal);
                                string insertInv = "Insert into INV (Empresa,Mov,MovID,FechaEmision,UltimoCambio,UEN,Moneda,TipoCambio,Usuario,Referencia,Observaciones,Estatus,Directo,Almacen,AlmacenTransito,OrigenTipo,Origen,OrigenID,Sucursal,SucursalOrigen,SucursalDestino)" + "values ('GNAP','Merma Produccion','" + movId + "',GETDATE(),GETDATE(),2,'Pesos',1,'" + user + "',(select referencia from prod where id=" + id + "),(select observaciones from prod where id=" + id + "),'PENDIENTE',1,'APT-BUS','(TRANSITO)','PROD','Entrada Produccion','" + ordenProduccion + "'," + sucursal + "," + sucursal + "," + sucursal + ")";
                                Ejecuta(insertInv, conn);
                                int idInv = getIdInv();
                                string insertInvD = "Insert into INVD (ID,Renglon,RenglonSub,RenglonId,Renglontipo,Cantidad,Almacen,Codigo,Articulo,Unidad,Factor,Producto,Tipo,Sucursal)" + "values (" + getIdInv() + "," + renglon + ",0," + renglonId + ",'N'," + huecos + ",'APT-BUS',null,'" + codigo + "',(select unidad from Art where Articulo='" + codigo + "'),(select factor from Art where Articulo='" + codigo + "'),'" + codigo + "','Salida'," + sucursal + ")";
                                Ejecuta(insertInvD, conn);
                                string spAfectarMP = "EXEC spAfectar 'INV', " + idInv + ",'Afectar','Todo','Merma Produccion','" + user + "',@Estacion = 99";
                                Ejecuta(spAfectarMP, conn);
                                string act = "Update Inv set Estatus = 'CONCLUIDO' where id = " + idInv + "";
                                Ejecuta(act, conn);
                                string insertMovFlujo = "Insert into movFlujo values (" + sucursal + ",'GNAP','PROD'," + id + ",'Entrada Produccion','" + ordenProduccion + "','INV'," + idInv + ",'Merma Produccion','" + movId + "',0)";
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
                else if (huecos > cantidadEstimada)
                {
                    return res = 2;
                }
                else if (huecos == 0)
                {
                    //conn2.Close();
                    //conn2.Open();
                    //string updateDetRProd = "UPDATE DetRProd set CantidadReal = " + cantidadEstimada + " where EPC ='" + epc + "'";
                    //SqlCommand cmdDetRProd = new SqlCommand(updateDetRProd, conn2);
                    //cmdDetRProd.ExecuteNonQuery();
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
        */

        /*
        public int contarHuecos(int huecos, int id, string epc, int tr, string codigo, string user, string sucursal)
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
                string selectEstimado = "Select CantidadReal, OrdenProduccion from DetRProd where EPC ='" + epc + "' and CodigoProducto = '" + codigo + "'";
                SqlCommand cmdEstimado = new SqlCommand(selectEstimado, conn2);
                SqlDataReader readerEstimado = cmdEstimado.ExecuteReader();
                int cantidadEstimada = 0;
                int antesImpuestos = 0;
                string ordenProduccion = "";
                if (readerEstimado.Read())
                {
                    cantidadEstimada = readerEstimado.GetInt32(0);
                    antesImpuestos = readerEstimado.GetInt32(0) + huecos;
                    ordenProduccion = readerEstimado.GetString(1);
                }
                else
                {
                    return res = 1;
                }
                conn2.Close();
                conn2.Open();
                if (huecos > 0 && huecos < antesImpuestos)
                {
                    int cantidadReal = cantidadEstimada;
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
                        //renglonId = renglon / 2048;
                    }
                    else
                    {
                        return res = 1;
                    }
                    conn2.Close();
                    conn2.Open();
                    //ahora vamos por la cantidad liberada hasta el momento
                    string selectLiberado = "Select prodLiberado, prodRestante, prodAsignado from CatProd where codigo ='" + codigo + "' and OrdenProduccion ='" + ordenProduccion + "'";
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
                        int nuevoCurado = cantidadCurado - antesImpuestos, nuevoLiberado = cantidadLiberado + cantidadReal, nuevoMermas = cantidadMerma + huecos, nuevoRestante = cantidadRestante - cantidadLiberado;
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
                                //string updateDetRprod = "UPDATE DetRPRod set CantidadReal = " + cantidadReal + " where EPC = '" + epc + "'";
                                //SqlCommand cmdDetRProd = new SqlCommand(updateDetRprod, conn2);
                                //cmdDetRProd.ExecuteNonQuery();
                                //conn2.Close();
                                //conn2.Open();
                                //luego actualizar CatProd
                                string updateCatProd = "UPDATE catProd set prodCurado = " + nuevoCurado + ", prodLiberado = " + nuevoLiberado + ", mermas = " + nuevoMermas + ", prodRestante = " + cantidadRestante +" where Codigo = '" + codigo + "' and OrdenProduccion = '" + ordenProduccion + "'";
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
                                string insertInv = "Insert into INV (Empresa,Mov,MovID,FechaEmision,UltimoCambio,UEN,Moneda,TipoCambio,Usuario,Referencia,Observaciones,Estatus,Directo,Almacen,AlmacenTransito,OrigenTipo,Origen,OrigenID,Sucursal,SucursalOrigen,SucursalDestino)" + "values ('GNAP','Merma Produccion','" + movId + "',GETDATE(),GETDATE(),2,'Pesos',1,'" + user + "',(select referencia from prod where id=" + id + "),(select observaciones from prod where id=" + id + "),'PENDIENTE',1,'APT-BUS','(TRANSITO)','PROD','Entrada Produccion','" + ordenProduccion + "'," + sucursal + "," + sucursal + "," + sucursal + ")";
                                Ejecuta(insertInv, conn);
                                int idInv = getIdInv();
                                string insertInvD = "Insert into INVD (ID,Renglon,RenglonSub,RenglonId,Renglontipo,Cantidad,Almacen,Codigo,Articulo,CantidadPendiente,Unidad,Factor,Producto,Tipo,Sucursal)" + "values (" + getIdInv() + "," + renglon + ",0," + rID + ",'N'," + huecos + ",'APT-BUS',null,'" + codigo + "',"+huecos+",(select unidad from Art where Articulo='" + codigo + "'),(select factor from Art where Articulo='" + codigo + "'),'" + codigo + "','Salida'," + sucursal + ")";
                                Ejecuta(insertInvD, conn);
                                string spAfectarMP = "EXEC spAfectar 'INV', " + idInv + ",'Afectar','Todo','Merma Produccion','" + user + "',@Estacion = 99";
                                Ejecuta(spAfectarMP, conn);
                                string act = "Update Inv set Estatus = 'CONCLUIDO' where id = " + idInv + "";
                                Ejecuta(act, conn);
                                string insertMovFlujo = "Insert into movFlujo values (" + sucursal + ",'GNAP','PROD'," + id + ",'Entrada Produccion','" + ordenProduccion + "','INV'," + idInv + ",'Merma Produccion','" + movId + "',0)";
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
                                string updateCatProd = "UPDATE catProd set prodCurado = " + nuevoCurado + ", prodLiberado = " + nuevoLiberado + ", mermas = " + nuevoMermas + ", prodRestante = " + cantidadRestante + " where Codigo = '" + codigo + "' and OrdenProduccion = '" + ordenProduccion + "'";
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
                                string insertInv = "Insert into INV (Empresa,Mov,MovID,FechaEmision,UltimoCambio,UEN,Moneda,TipoCambio,Usuario,Referencia,Observaciones,Estatus,Directo,Almacen,AlmacenTransito,OrigenTipo,Origen,OrigenID,Sucursal,SucursalOrigen,SucursalDestino)" + "values ('GNAP','Merma Produccion','" + movId + "',GETDATE(),GETDATE(),2,'Pesos',1,'" + user + "',(select referencia from prod where id=" + id + "),(select observaciones from prod where id=" + id + "),'PENDIENTE',1,'APT-BUS','(TRANSITO)','PROD','Entrada Produccion','" + ordenProduccion + "'," + sucursal + "," + sucursal + "," + sucursal + ")";
                                Ejecuta(insertInv, conn);
                                int idInv = getIdInv();
                                string insertInvD = "Insert into INVD (ID,Renglon,RenglonSub,RenglonId,Renglontipo,Cantidad,Almacen,Codigo,Articulo,CantidadPendiente,Unidad,Factor,Producto,Tipo,Sucursal)" + "values (" + getIdInv() + "," + renglon + ",0," + rID + ",'N'," + huecos + ",'APT-BUS',null,'" + codigo + "'," + huecos + ",(select unidad from Art where Articulo='" + codigo + "'),(select factor from Art where Articulo='" + codigo + "'),'" + codigo + "','Salida'," + sucursal + ")";
                                Ejecuta(insertInvD, conn);
                                string spAfectarMP = "EXEC spAfectar 'INV', " + idInv + ",'Afectar','Todo','Merma Produccion','" + user + "',@Estacion = 99";
                                Ejecuta(spAfectarMP, conn);
                                string act = "Update Inv set Estatus = 'CONCLUIDO' where id = " + idInv + "";
                                Ejecuta(act, conn);
                                string insertMovFlujo = "Insert into movFlujo values (" + sucursal + ",'GNAP','PROD'," + id + ",'Entrada Produccion','" + ordenProduccion + "','INV'," + idInv + ",'Merma Produccion','" + movId + "',0)";
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
                else if (huecos > cantidadEstimada)
                {
                    return res = 2;
                }
                else if (huecos == 0)
                {

                    //conn2.Close();
                    //conn2.Open();
                    //string updateDetRProd = "UPDATE DetRProd set CantidadReal = " + cantidadEstimada + " where EPC ='" + epc + "'";
                    //SqlCommand cmdDetRProd = new SqlCommand(updateDetRProd, conn2);
                    //cmdDetRProd.ExecuteNonQuery();
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
        */

        public int contarHuecos(int huecos, int id, string epc, int tr, string codigo, string user, string sucursal)
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
                string selectEstimado = "Select CantidadReal, OrdenProduccion from DetRProd where EPC ='" + epc + "' and CodigoProducto = '" + codigo + "'";
                SqlCommand cmdEstimado = new SqlCommand(selectEstimado, conn2);
                SqlDataReader readerEstimado = cmdEstimado.ExecuteReader();
                int cantidadEstimada = 0;
                int antesImpuestos = 0;
                string ordenProduccion = "";
                if (readerEstimado.Read())
                {
                    cantidadEstimada = readerEstimado.GetInt32(0);
                    antesImpuestos = readerEstimado.GetInt32(0) + huecos;
                    ordenProduccion = readerEstimado.GetString(1);
                }
                else
                {
                    return res = 1;
                }
                conn2.Close();
                conn2.Open();
                if (huecos > 0 && huecos < antesImpuestos)
                {
                    int cantidadReal = cantidadEstimada;
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
                        //renglonId = renglon / 2048;
                    }
                    else
                    {
                        return res = 1;
                    }
                    conn2.Close();
                    conn2.Open();
                    //ahora vamos por la cantidad liberada hasta el momento
                    string selectLiberado = "Select prodLiberado, prodRestante, prodAsignado from CatProd where codigo ='" + codigo + "' and OrdenProduccion ='" + ordenProduccion + "'";
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
                        int nuevoCurado = cantidadCurado - antesImpuestos, nuevoLiberado = cantidadLiberado + cantidadReal, nuevoMermas = cantidadMerma + huecos, nuevoRestante = cantidadRestante - cantidadLiberado;
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
                                //string updateDetRprod = "UPDATE DetRPRod set CantidadReal = " + cantidadReal + " where EPC = '" + epc + "'";
                                //SqlCommand cmdDetRProd = new SqlCommand(updateDetRprod, conn2);
                                //cmdDetRProd.ExecuteNonQuery();
                                //conn2.Close();
                                //conn2.Open();
                                //luego actualizar CatProd
                                string updateCatProd = "UPDATE catProd set prodCurado = " + nuevoCurado + ", prodLiberado = " + nuevoLiberado + ", mermas = " + nuevoMermas + ", prodRestante = " + cantidadRestante + " where Codigo = '" + codigo + "' and OrdenProduccion = '" + ordenProduccion + "'";
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
                                string insertInv = "Insert into INV (Empresa,Mov,MovID,FechaEmision,UltimoCambio,UEN,Moneda,TipoCambio,Usuario,Referencia,Observaciones,Estatus,Directo,Almacen,AlmacenTransito,OrigenTipo,Origen,OrigenID,Sucursal,SucursalOrigen,SucursalDestino)" + "values ('GNAP','Merma Produccion','" + movId + "',GETDATE(),GETDATE(),2,'Pesos',1,'" + user + "',(select referencia from prod where id=" + id + "),(select observaciones from prod where id=" + id + "),'PENDIENTE',1,'APT-BUS','(TRANSITO)','PROD','Entrada Produccion','" + ordenProduccion + "'," + sucursal + "," + sucursal + "," + sucursal + ")";
                                Ejecuta(insertInv, conn);
                                int idInv = getIdInv();
                                string insertInvD = "Insert into INVD (ID,Renglon,RenglonSub,RenglonId,Renglontipo,Cantidad,Almacen,Codigo,Articulo,CantidadPendiente,Unidad,Factor,Producto,Tipo,Sucursal)" + "values (" + getIdInv() + "," + renglon + ",0," + rID + ",'N'," + huecos + ",'APT-BUS',null,'" + codigo + "'," + huecos + ",(select unidad from Art where Articulo='" + codigo + "'),(select factor from Art where Articulo='" + codigo + "'),'" + codigo + "','Salida'," + sucursal + ")";
                                Ejecuta(insertInvD, conn);
                                string spAfectarMP = "EXEC spAfectar 'INV', " + idInv + ",'Afectar','Todo','Merma Produccion','" + user + "',@Estacion = 99";
                                Ejecuta(spAfectarMP, conn);
                                string act = "Update Inv set Estatus = 'CONCLUIDO' where id = " + idInv + "";
                                Ejecuta(act, conn);
                                string insertMovFlujo = "Insert into movFlujo values (" + sucursal + ",'GNAP','PROD'," + id + ",'Entrada Produccion','" + ordenProduccion + "','INV'," + idInv + ",'Merma Produccion','" + movId + "',0)";
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
                                string updateCatProd = "UPDATE catProd set prodCurado = " + nuevoCurado + ", prodLiberado = " + nuevoLiberado + ", mermas = " + nuevoMermas + ", prodRestante = " + cantidadRestante + " where Codigo = '" + codigo + "' and OrdenProduccion = '" + ordenProduccion + "'";
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
                                string insertInv = "Insert into INV (Empresa,Mov,MovID,FechaEmision,UltimoCambio,UEN,Moneda,TipoCambio,Usuario,Referencia,Observaciones,Estatus,Directo,Almacen,AlmacenTransito,OrigenTipo,Origen,OrigenID,Sucursal,SucursalOrigen,SucursalDestino)" + "values ('GNAP','Merma Produccion','" + movId + "',GETDATE(),GETDATE(),2,'Pesos',1,'" + user + "',(select referencia from prod where id=" + id + "),(select observaciones from prod where id=" + id + "),'PENDIENTE',1,'APT-BUS','(TRANSITO)','PROD','Entrada Produccion','" + ordenProduccion + "'," + sucursal + "," + sucursal + "," + sucursal + ")";
                                Ejecuta(insertInv, conn);
                                int idInv = getIdInv();
                                string insertInvD = "Insert into INVD (ID,Renglon,RenglonSub,RenglonId,Renglontipo,Cantidad,Almacen,Codigo,Articulo,CantidadPendiente,Unidad,Factor,Producto,Tipo,Sucursal)" + "values (" + getIdInv() + "," + renglon + ",0," + rID + ",'N'," + huecos + ",'APT-BUS',null,'" + codigo + "'," + huecos + ",(select unidad from Art where Articulo='" + codigo + "'),(select factor from Art where Articulo='" + codigo + "'),'" + codigo + "','Salida'," + sucursal + ")";
                                Ejecuta(insertInvD, conn);
                                string spAfectarMP = "EXEC spAfectar 'INV', " + idInv + ",'Afectar','Todo','Merma Produccion','" + user + "',@Estacion = 99";
                                Ejecuta(spAfectarMP, conn);
                                string act = "Update Inv set Estatus = 'CONCLUIDO' where id = " + idInv + "";
                                Ejecuta(act, conn);
                                string insertMovFlujo = "Insert into movFlujo values (" + sucursal + ",'GNAP','PROD'," + id + ",'Entrada Produccion','" + ordenProduccion + "','INV'," + idInv + ",'Merma Produccion','" + movId + "',0)";
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
                else if (huecos > cantidadEstimada)
                {
                    return res = 2;
                }
                else if (huecos == 0)
                {
                    int cantidadReal = cantidadEstimada;
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
                        //renglonId = renglon / 2048;
                    }
                    else
                    {
                        return res = 1;
                    }
                    conn2.Close();
                    conn2.Open();
                    //ahora vamos por la cantidad liberada hasta el momento
                    string selectLiberado = "Select prodLiberado, prodRestante, prodAsignado from CatProd where codigo ='" + codigo + "' and OrdenProduccion ='" + ordenProduccion + "'";
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
                    int nuevoCurado = cantidadCurado - cantidadEstimada, nuevoLiberado = cantidadLiberado + cantidadEstimada, nuevoRestante = cantidadRestante - cantidadEstimada;
                    string updateCatProd = "UPDATE catProd set prodCurado = " + nuevoCurado + ", prodLiberado = " + nuevoLiberado + ", prodRestante = " + nuevoRestante + " where Codigo = '" + codigo + "' and OrdenProduccion = '" + ordenProduccion + "'";
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
                else if (estado == "CURADO")
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
                else if (estado == "LIBERADO")
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
                else if (estado == "CONCLUIDO")
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
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                current = -1;
            }
            return current;
        }

        public string[] detalleEscuadra(string EPC)
        {
            string[] detalle = new string[5];
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                //string select = "select cp.OrdenProduccion, cp.Pedido, cp.Descripcion, de.Piezas, cp.Codigo from DetEscuadras de inner join CatProd cp on cp.OrdenProduccion = de.Ordenproduccion where de.EPC = '" + EPC + "' and de.CodigoProducto = '" + cp + "'";
                string select = "select cp.OrdenProduccion, cp.Pedido, cp.Descripcion, de.Piezas, cp.Codigo from DetEscuadras de inner join CatProd cp on cp.OrdenProduccion = de.Ordenproduccion where cp.Codigo = (SELECT CodigoProducto from DetEscuadras where EPC = '" + EPC + "')";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    detalle[0] = reader.GetString(0);
                    detalle[1] = reader.GetValue(1).ToString();
                    detalle[2] = reader.GetString(2);
                    detalle[3] = reader.GetInt32(3).ToString();
                    detalle[4] = reader.GetString(4);
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

        public string[] detalleProd(string op, string codigo)
        {
            string[] res = new string[17];
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
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
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return null;
            }
            return res;
        }

        public int embarcaEscuadra(string EPC)
        {
            int res = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                string update = "update DetEscuadras set Embarcado= 1 where EPC ='" + EPC + "'";
                conn.Open();
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

        public string[] getArrayEtiquetasCB(string cb, string remision)
        {
            string[] or = ordenRemision(remision);
            string[] data = new string[12];
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

        //public DataSet getAsignadas(string op)
        //{
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        string[] parametros = getParametros("Solutia");
        //        SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
        //        conn.Open();
        //        string select = "Select Id,OrdenProduccion,Pedido,Cliente,Descripcion,Tipo,Medida,Color,Codigo,Cantidad,Estatus,Renglon,Asignado,prodEstibado,prodAsignado from catProd where OrdenProduccion='" + op + "' and Asignado = 1";
        //        SqlDataAdapter da = new SqlDataAdapter(select, conn);
        //        da.Fill(ds);
        //        conn.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
        //    }
        //    return ds;
        //}

        public DataTable getAsignadasWDR(string op)
        {
            string select = "Select Id,OrdenProduccion,Pedido,Cliente,Descripcion,Tipo,Medida,Color,Codigo,Cantidad,Estatus,Renglon,Asignado,prodEstibado,prodAsignado from catProd where OrdenProduccion='" + op + "' and Asignado = 1";
            return getDatasetConexionWDR(select, "Solutia");
        }

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

        //public DataSet getDataset(string command)
        //{
        //    string[] prueba = getParametros("ConsolaAdmin");
        //    SqlConnection conn = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand(command, conn);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        da.Fill(ds);
        //    }
        //    catch (Exception ex)
        //    {
        //        string error = ex.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
        //    };
        //    return ds;
        //}

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

        //public DataSet getDatasetConexion(string command, string descrip)
        //{
        //    string[] prueba = getParametros(descrip);
        //    SqlConnection conn = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand(command, conn);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        da.Fill(ds);
        //    }
        //    catch (Exception ex)
        //    {
        //        string error = ex.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
        //    }
        //    return ds;
        //}

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
                conn.Close();
                string error = e.Message;
                //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return res;
        }

        public int getEstibaActual(string op, string renglon)
        {
            int total = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
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
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                total = -1;
            }
            return total;
        }

        //public DataSet getIncompletosTransfer(string OP)
        //{
        //    string[] parametros = getParametros("Intelisis");
        //    DataSet ds = new DataSet();
        //    SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
        //    conn.Open();

        //    string select = "Select Id,Mov,MovID,Estatus From Inv Where  id In (Select Did From MovFlujo Where Omodulo = 'PROD' And OMovID = '" + OP + "'  And Dmov = 'Orden Transferencia') and Estatus != 'CONCLUIDO'";

        //    SqlDataAdapter da = new SqlDataAdapter(select, conn);

        //    da.Fill(ds);
        //    conn.Close();
        //    return ds;
        //}

        public DataTable getIncompletosTransferWDR(string OP)
        {
            string select = "Select Id,Mov,MovID,Estatus From Inv Where  id In (Select Did From MovFlujo Where Omodulo = 'PROD' And OMovID = '" + OP + "'  And Dmov = 'Orden Transferencia') and Estatus != 'CONCLUIDO'";
            return getDatasetConexionWDR(select, "Intelisis");
        }

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

        //public List<cRack> getPersonas(string OP)
        //{
        //    List<cRack> racks = new List<cRack>();
        //    string[] parametros = getParametros("Solutia");
        //    SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
        //    conn.Open();
        //    string select = "Select dp.EPC,dp.Numero,rp.Modelo as Modelo,dp.OrdenProduccion,dp.CantidadEstimada as [C.Estimada], dp.CantidadReal as [C.Real] from DetRProd dp inner join RProduccion rp on rp.IdRack = dp.IdRProd where OrdenProduccion = '" + OP + "' and Estado = 1 and Verificado = 1 and Contado = 0";
        //    SqlCommand cmd = new SqlCommand(select, conn);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    SqlDataReader ded = cmd.ExecuteReader();

        //    while (ded.Read())
        //    {
        //        cRack cr = new cRack();
        //        cr.EPC = ded.GetString(0);
        //        cr.numero = ded.GetInt32(1);
        //        cr.modelo = ded.GetString(2);
        //        cr.ordenProduccion = ded.GetString(3);
        //        cr.cantidadEstimada = ded.GetInt32(4);
        //        cr.cantidadReal = ded.GetInt32(5);
        //        racks.Add(cr);
        //    }
        //    conn.Close();
        //    return racks;
        //}

        //public DataSet getProdD(string folio)
        //{
        //    string[] parametros = getParametros("Intelisis");
        //    DataSet ds = new DataSet();
        //    SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
        //    conn.Open();

        //    string select = "select * from ProdPendienteD where MovID='" + folio + "'";

        //    SqlDataAdapter da = new SqlDataAdapter(select, conn);

        //    da.Fill(ds);
        //    conn.Close();
        //    return ds;
        //}

        public DataTable getPersonas(string op)
        {
            string select = "Select dp.EPC,dp.Numero,rp.Modelo as Modelo,dp.OrdenProduccion,dp.CantidadEstimada as [C.Estimada], dp.CantidadReal as [C.Real] from DetRProd dp inner join RProduccion rp on rp.IdRack = dp.IdRProd where OrdenProduccion = '" + op + "' and Estado = 1 and Verificado = 1 and ContadoProd = 0";
            return getDatasetConexionWDR(select, "Solutia");
        }

        public DataTable getCuradoRacks(string op)
        {
            string select = "Select dp.EPC,dp.Numero,rp.Modelo as Modelo,dp.OrdenProduccion,dp.CantidadEstimada as [C.Estimada], dp.CantidadReal as [C.Real] from DetRProd dp inner join RProduccion rp on rp.IdRack = dp.IdRProd where OrdenProduccion = '" + op + "' and Estado = 1 and Verificado = 1 and ContadoProd = 1 and ContadoCurado = 0";
            return getDatasetConexionWDR(select, "Solutia");
        }

        public DataTable getProdDWDR(string folio)
        {
            string select = "select * from ProdPendienteD where MovID='" + folio + "'";
            return getDatasetConexionWDR(select, "Intelisis");
        }

        //public DataSet getRacks(string OP, string codigo, string renglon)
        //{
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        string[] parametros = getParametros("Solutia");
        //        SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
        //        conn.Open();
        //        string select = "Select dp.EPC, rp.Modelo, dp.Numero, dp.OrdenProduccion, dp.CodigoProducto  from DetRProd dp inner join RProduccion rp on rp.IdRack = dp.IdRProd where OrdenProduccion = '" + OP + "' and CodigoProducto = '" + codigo + "' and Renglon = " + renglon + " and Estado = 1 and Verificado = 0";

        //        SqlDataAdapter da = new SqlDataAdapter(select, conn);

        //        da.Fill(ds);
        //        conn.Close();
        //    }
        //    catch (SqlException e)
        //    {
        //        string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
        //    }
        //    return ds;
        //}

        public DataTable getRacksWDR(string OP, string codigo, string renglon)
        {
            string select = "Select dp.EPC, rp.Modelo, dp.Numero, dp.OrdenProduccion, dp.CodigoProducto  from DetRProd dp inner join RProduccion rp on rp.IdRack = dp.IdRProd where OrdenProduccion = '" + OP + "' and CodigoProducto = '" + codigo + "' and Renglon = " + renglon + " and Estado = 1 and Verificado = 0";
            return getDatasetConexionWDR(select, "Solutia");
        }

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
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                renglon = -1;
            }
            return renglon;
        }

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

        public int getTotalCant(string op, string renglon)
        {
            int total = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
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
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                total = -1;
            }
            return total;
        }

        public string[] getZonaRack(string idZona, string idRack)
        {
            string[] ZonaRack = new string[2];
            string[] parametros = getParametros("ConsolaAdmin");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            conn.Open();

            try
            {
                string selectZona = "Select ClaveZona from Zonas where idZona=" + idZona + "";
                string selectRack = "Select Clave from racks where IDRack=" + idRack + "";
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

        //public DataSet getZonaEPC(string op)
        //{
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        string[] parametros = getParametros("Solutia");
        //        SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
        //        conn.Open();
        //        string select = "Select EPC from detBanderas where IdBandera='" + op + "'";
        //        SqlDataAdapter da = new SqlDataAdapter(select, conn);
        //        da.Fill(ds);
        //        conn.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
        //    }
        //    return ds;
        //}

        public DataTable getZonaEPCWDR(string op)
        {
            string select = "Select EPC from detBanderas where IdBandera='" + op + "'";
            return getDatasetConexionWDR(select, "Solutia");
        }

        //public DataSet hiddenProd()
        //{
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        string[] parametros = getParametros("Solutia");
        //        SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
        //        string select = "select OrdenProduccion,Pedido,Cliente,Descripcion,Tipo,Medida,Color,Codigo,Cantidad,Estatus,prodEstibado,prodAsignado,prodCurado,prodLiberado,mermas from catProd where Estatus != 'CONCLUIDO' and Asignado = 1";
        //        conn.Open();
        //        SqlDataAdapter da = new SqlDataAdapter(select, conn);
        //        da.Fill(ds);
        //        conn.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
        //    }
        //    return ds;
        //}

        public DataTable hiddenProdWDR()
        {
            string select = "select OrdenProduccion,Pedido,Cliente,Descripcion,Tipo,Medida,Color,Codigo,Cantidad,Estatus,prodEstibado,prodAsignado,prodCurado,prodLiberado,mermas from catProd where Estatus != 'CONCLUIDO' and Asignado = 1";
            return getDatasetConexionWDR(select, "Solutia");
        }

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
            return res;
        }

        public int insertEtiquetas(string Pedido, string OrdenProduccion, string Cliente, string N_Tarima, string Producto, int Cantidad, int CantidadTarima, string Medida, string Codigo, string Color, string Tipo, string EPC)
        {
            int res = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
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
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                res = 1;
            }
            return res;
        }

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

        public int mermaRecepcion(int rackHuecos, int id, string codigo, string op, int renglon, string user, string sucursal)
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
                                string insertInvD = "Insert into INVD (ID,Renglon,RenglonSub,RenglonId,Renglontipo,Cantidad,Almacen,Codigo,Articulo,CantidadPendiente, Unidad,Factor,Producto,Tipo,Sucursal)" + "values (" + getIdInv() + "," + renglon + ",0," + rID + ",'N'," + rackHuecos + ",'APT-BUS',null,'" + codigo + "'," + rackHuecos + ",(select unidad from Art where Articulo='" + codigo + "'),(select factor from Art where Articulo='" + codigo + "'),'" + codigo + "','Salida'," + sucursal + ")";
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
                                Ejecuta(afectarMerma, conn);//por ultimo se insertan las mermas en Inv e InvD
                                string movId = setMovID("Merma Estiba", sucursal);
                                string insertInv = "Insert into INV (Empresa,Mov,MovID,FechaEmision,UltimoCambio,UEN,Moneda,TipoCambio,Usuario,Referencia,Observaciones,Estatus,Directo,Almacen,AlmacenTransito,OrigenTipo,Origen,OrigenID,Sucursal,SucursalOrigen,SucursalDestino)" + "values ('GNAP','Merma Estiba','" + movId + "',GETDATE(),GETDATE(),2,'Pesos',1,'" + user + "',(select referencia from prod where id=" + id + "),(select observaciones from prod where id=" + id + "),'PENDIENTE',1,'APT-BUS','(TRANSITO)','PROD','Entrada Produccion','" + op + "'," + sucursal + "," + sucursal + "," + sucursal + ")";
                                Ejecuta(insertInv, conn);
                                int idInv = getIdInv();
                                int rID = getRenglonId(renglon, contador);
                                string insertInvD = "Insert into INVD (ID,Renglon,RenglonSub,RenglonId,Renglontipo,Cantidad,Almacen,Codigo,Articulo,CantidadPendiente,Unidad,Factor,Producto,Tipo,Sucursal)" + "values (" + getIdInv() + "," + renglon + ",0," + rID + ",'N'," + rackHuecos + ",'APT-BUS',null,'" + codigo + "'," + rackHuecos + ",(select unidad from Art where Articulo='" + codigo + "'),(select factor from Art where Articulo='" + codigo + "'),'" + codigo + "','Salida'," + sucursal + ")";
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

        public int noHuecos(int huecos, int actual, int id, string op, string codigo, string user, string sucursal)
        {
            int res = 0;
            string[] parametros = getParametros("Intelisis");
            string[] parametros2 = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + "");
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
                    readerRenglones.Close();
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
                            readerDestino.Close();
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
                conn.Close();
                conn2.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                conn2.Close();
                return res = 1;
            }
            return res;
        }

        public int pickEscuadra(string epc)
        {
            int result = 0;
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
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
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                result = 1;
            }
            return result;
        }

        //public DataSet pickingEscuadra(string op, int valor)
        //{
        //    string[] remision = ordenRemision(op);
        //    DataSet ds = new DataSet();
        //    string[] parametros = getParametros("Solutia");
        //    SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
        //    try
        //    {
        //        conn.Open();
        //        if (valor == 1)
        //        {
        //            string select = "select distinct de.epc, de.OrdenProduccion, cp.Pedido, de.CodigoProducto, cp.Descripcion, de.Piezas, cp.Cliente from DetEscuadras de inner join catProd cp on cp.Codigo = de.CodigoProducto where de.OrdenProduccion = '" + remision[0] + "' and cp.Pedido = '" + remision[1] + "' and de.Asignado = 1 and de.Ubicada=1 and de.Picked = 0";
        //            SqlCommand cmd = new SqlCommand(select, conn);
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            da.Fill(ds);
        //        }
        //        else if (valor == 2)
        //        {
        //            string select = "select distinct de.epc, de.OrdenProduccion, cp.Pedido, de.CodigoProducto, cp.Descripcion, de.Piezas, cp.Cliente from DetEscuadras de inner join catProd cp on cp.Codigo = de.CodigoProducto where de.OrdenProduccion = '" + remision[0] + "' and cp.Pedido = '" + remision[1] + "' and de.Asignado = 1 and de.Ubicada=1 and de.Picked = 1 and de.Embarcado = 0";
        //            SqlCommand cmd = new SqlCommand(select, conn);
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            da.Fill(ds);
        //        }
        //        else if (valor == 3)
        //        {
        //            string select = "select distinct de.epc, de.OrdenProduccion, cp.Pedido, de.CodigoProducto, cp.Descripcion, de.Piezas, cp.Cliente from DetEscuadras de inner join catProd cp on cp.Codigo = de.CodigoProducto where de.OrdenProduccion = '" + remision[0] + "' and cp.Pedido = '" + remision[1] + "' and de.Asignado = 1 and de.Ubicada=1 and de.Picked = 1 and de.Embarcado = 1";
        //            SqlCommand cmd = new SqlCommand(select, conn);
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            da.Fill(ds);
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
        //        return null;
        //    }
        //    return ds;
        //}

        public DataTable pickingEscuadraWDR(string op, int valor)
        {
            string[] remision = ordenRemision(op);
            DataTable ds = new DataTable();

            try
            {
                if (valor == 1)
                {
                    string select = "select distinct de.epc, de.OrdenProduccion, cp.Pedido, de.CodigoProducto, cp.Descripcion, de.Piezas, cp.Cliente from DetEscuadras de inner join catProd cp on cp.Codigo = de.CodigoProducto where de.OrdenProduccion = '" + remision[0] + "' and cp.Pedido = '" + remision[1] + "' and de.Asignado = 1 and de.Ubicada=1 and de.Picked = 0";
                    ds = getDatasetConexionWDR(select, "Solutia");
                }
                else if (valor == 2)
                {
                    string select = "select distinct de.epc, de.OrdenProduccion, cp.Pedido, de.CodigoProducto, cp.Descripcion, de.Piezas, cp.Cliente from DetEscuadras de inner join catProd cp on cp.Codigo = de.CodigoProducto where de.OrdenProduccion = '" + remision[0] + "' and cp.Pedido = '" + remision[1] + "' and de.Asignado = 1 and de.Ubicada=1 and de.Picked = 1 and de.Embarcado = 0";
                    ds = getDatasetConexionWDR(select, "Solutia");
                }
                else if (valor == 3)
                {
                    string select = "select distinct de.epc, de.OrdenProduccion, cp.Pedido, de.CodigoProducto, cp.Descripcion, de.Piezas, cp.Cliente from DetEscuadras de inner join catProd cp on cp.Codigo = de.CodigoProducto where de.OrdenProduccion = '" + remision[0] + "' and cp.Pedido = '" + remision[1] + "' and de.Asignado = 1 and de.Ubicada=1 and de.Picked = 1 and de.Embarcado = 1";
                    ds = getDatasetConexionWDR(select, "Solutia");
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
                    return -1;
                }

            }
            catch (Exception e)
            {
                conn.Close();
                return -1;
            }
        }

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

        //public DataSet showProd()
        //{
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        string[] parametros = getParametros("Solutia");
        //        SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
        //        string select = "select Id,OrdenProduccion,Pedido,Cliente,Descripcion,Tipo,Medida,Color,Codigo,Cantidad,Estatus,Renglon,Asignado,prodEstibado,prodAsignado,prodCurado,prodLiberado,mermas from catProd where Estatus != 'CONCLUIDO' and Asignado = 1";
        //        conn.Open();
        //        SqlDataAdapter da = new SqlDataAdapter(select, conn);
        //        da.Fill(ds);

        //        conn.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
        //    }
        //    return ds;
        //}

        public DataTable showProdWDR()
        {
            string select = "select Id,OrdenProduccion,Pedido,Cliente,Descripcion,Tipo,Medida,Color,Codigo,Cantidad,Estatus,Renglon,Asignado,prodEstibado,prodAsignado,prodCurado,prodLiberado,mermas from catProd where Estatus != 'CONCLUIDO' and Asignado = 1";
            return getDatasetConexionWDR(select, "Solutia");
        }

        //public DataSet showProdComp()
        //{
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        string[] parametros = getParametros("Solutia");
        //        SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
        //        conn.Open();
        //        string select = "Select IdEscuadra as [Escuadra #], EPC as EPC, OrdenProduccion as [Orden de Produccion], CodigoProducto as Codigo, Piezas from DetEscuadras where Asignado = 1 and Ubicada=0";
        //        SqlDataAdapter da = new SqlDataAdapter(select, conn);
        //        da.Fill(ds);
        //        conn.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
        //    }
        //    return ds;
        //}

        public DataTable showProdCompWDR()
        {
            string select = "Select IdEscuadra as [Escuadra #], EPC as EPC, OrdenProduccion as [Orden de Produccion], CodigoProducto as Codigo, Piezas from DetEscuadras where Asignado = 1 and Ubicada=0";
            return getDatasetConexionWDR(select, "Solutia");
        }

        public int sizeList(string OP, int evnt)
        {
            List<cRack> racks = new List<cRack>();
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            conn.Open();
            string select = "";
            if (evnt == 0)
                select = "Select dp.EPC,dp.Numero,rp.Modelo as Modelo,dp.OrdenProduccion,dp.CantidadEstimada as [C.Estimada], dp.CantidadReal as [C.Real] from DetRProd dp inner join RProduccion rp on rp.IdRack = dp.IdRProd where OrdenProduccion = '" + OP + "' and Estado = 1 and Verificado = 1 and ContadoProd = 0";
            if (evnt == 1)
                select = "Select dp.EPC,dp.Numero,rp.Modelo as Modelo,dp.OrdenProduccion,dp.CantidadEstimada as [C.Estimada], dp.CantidadReal as [C.Real] from DetRProd dp inner join RProduccion rp on rp.IdRack = dp.IdRProd where OrdenProduccion = '" + OP + "' and Estado = 1 and Verificado = 1 and ContadoProd = 1 and ContadoCurado = 0";
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

        public int ubicaEscuadra(string epcEscuadra, string codigo, string cantidad, string epcBandera, string codigoBandera, int rack)
        {
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            string[] prueba = getParametros("ConsolaAdmin");
            SqlConnection connCA = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");

            int res = 0;
            try
            {
                conn.Open();
                connCA.Open();
                string insertPos = "Insert into posiciones values (" + getNextId() + ",1," + getIdAlmacen(rack) + ",1,1,.000,1,1,2,'" + epcEscuadra + "','" + codigo + "'," + cantidad + ",'" + epcBandera + "','" + codigoBandera + "')";
                string insertPosD = "Insert into DetallePos values ('" + codigo + "', '" + epcEscuadra + "', '" + codigoBandera + "', '" + epcBandera + "')";
                string updateEsc = "Update DetEscuadras set Ubicada = 1 where EPC ='" + epcEscuadra + "' and CodigoProducto = '" + codigo + "'";
                SqlCommand cmdPos = new SqlCommand(insertPos, connCA);
                SqlCommand cmdPosD = new SqlCommand(insertPosD, connCA);
                SqlCommand cmdEsc = new SqlCommand(updateEsc, conn);
                cmdPos.ExecuteNonQuery();
                cmdPosD.ExecuteNonQuery();
                cmdEsc.ExecuteNonQuery();
                conn.Close();
                connCA.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                connCA.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                res = 1;
            }
            return res;
        }

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

        public string Libera(string folio, string renglon)
        {
            string[] parametros2 = getParametros("Solutia");
            SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + "");
            conn2.Open();
            string result = "";

            try
            {
                string update = "update catProd set Estatus = 'LIBERADO' where OrdenProduccion='" + folio + "' and Renglon=" + renglon + "";
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

        public int embarqueRestante(string remision, string epc)
        {
            int res = 0;
            try
            {
                string[] escuadraD = getEscuadraD(epc);
                int remisionado = getCantidadRemision(remision, escuadraD[1]);
                res = remisionado - int.Parse(escuadraD[2]);
                return res;
            }
            catch (Exception e)
            {
                return res;
            }
        }

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

        public string nuevoHuecos(int estimado, int huecos, string[] rack, string epc)
        {
            string res = "";
            string[] parametros = getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            int[] curado = CurrentCurado(rack[4], rack[2]);
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

        public int getCantidadRemision(string Remision, string codigo)
        {
            int id = 0;
            Decimal factor = 0, cantidad = 0;
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                string selectIdRemision = "select Id from Venta where MovId = '" + Remision + "'";
                conn.Open();
                SqlCommand cmdIdRemision = new SqlCommand(selectIdRemision, conn);
                SqlDataReader readerIdRemision = cmdIdRemision.ExecuteReader();
                if (readerIdRemision.Read())
                {
                    id = readerIdRemision.GetInt32(0);
                }
                else
                {
                    conn.Close();
                    return id = -1;
                }
                conn.Close();
                conn.Open();
                string selectCantidadRemision = "Select Cantidad,Factor from VentaD where ID = " + id + " and Articulo = '" + codigo + "'";
                SqlCommand cmdCantidadRemision = new SqlCommand(selectCantidadRemision, conn);
                SqlDataReader readerCantidadRemision = cmdCantidadRemision.ExecuteReader();
                if (readerCantidadRemision.Read())
                {
                    id = readerCantidadRemision.GetInt32(0);
                    conn.Close();
                    return id = -1;
                }
                else
                {
                    conn.Close();
                    return id = -1;
                }
            }
            catch (Exception e)
            {
                conn.Close();
                return id = -1;
            }
        }

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

        public int[] CurrentCurado(string codigo, string op)
        {
            int[] estimado = new int[3];
            string[] parametros2 = getParametros("Solutia");
            SqlConnection conn2 = new SqlConnection("Data Source=" + parametros2[1] + "; Initial Catalog=" + parametros2[4] + "; Persist Security Info=True; User ID=" + parametros2[2] + "; Password=" + parametros2[3] + "");
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

        public int maxRenglonSub(string id, string codigo)
        {
            int cantidad = 0;
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
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
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return cantidad;
        }

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

        public int getNextId()
        {
            int id = 0;
            string[] prueba = getParametros("ConsolaAdmin");
            SqlConnection connCA = new SqlConnection("Data Source=" + prueba[1] + "; Initial Catalog=" + prueba[4] + "; Persist Security Info=True; User ID=" + prueba[2] + "; Password=" + prueba[3] + "");
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
                connCA.Close();
            }
            catch (Exception e)
            {
                connCA.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return id;
        }

        public int getIdAlmacen(int Rack)
        {
            int res = 0;
            string[] parametros = getParametros("ConsolaAdmin");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            conn.Open();
            try
            {
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
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                return res = 0;
            }
        }

        public string numDecimal(string coma)
        {
            string numDec = "0";
            string[] firstStep = coma.Split(',');
            string lastStep = firstStep[0] + "." + firstStep[1];
            numDec = lastStep;
            return numDec;
        }

        public string[] ordenRemision(string remision)
        {
            string[] remi = new string[2];
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                string select = "select MovId,Referencia from Prod where Mov = 'Orden Produccion' and Referencia = (Select OrigenID from Venta where MovId = '" + remision + "') ";
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

        //public DataSet getEscuadra()
        //{
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        string[] parametros = getParametros("Solutia");
        //        SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
        //        conn.Open();
        //        string select = "Select * from DetEscuadras where Asignado = 0";

        //        SqlDataAdapter da = new SqlDataAdapter(select, conn);

        //        da.Fill(ds);
        //        conn.Close();
        //    }
        //    catch (SqlException e)
        //    {
        //        string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
        //    }
        //    return ds;
        //}

        public DataTable getEscuadraWDR()
        {
            string select = "Select * from DetEscuadras where Asignado = 0";
            return getDatasetConexionWDR(select, "Solutia");
        }

        public string verificarEscuadra(string EPC, string folio, string codigo, int cantidadEstiba)
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
                string selectRenglon = "select cantidad, renglon from catprod where OrdenProduccion = '" + folio + "' and Codigo = '" + codigo + "'";
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
                if (cantidadEstiba > 0 && cantidadEstiba <= maximoPorTarima(codigo))
                {
                    int liberado = currentValue(folio, "LIBERADO", "wea", renglon);
                    int nvoLiberado = liberado - cantidadEstiba;
                    int concluido = currentValue(folio, "CONCLUIDO", "wea", renglon);
                    int nvoConcluido = concluido + cantidadEstiba;
                    int restante = total - cantidadEstiba;
                    string update = "update catProd set prodLiberado = " + nvoLiberado + ", prodConcluido = " + nvoConcluido + ", prodRestante = " + restante + " where OrdenProduccion = '" + folio + "' and Codigo = '" + codigo + "'";
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(update, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    string escuadra = "update DetEscuadras set OrdenProduccion = '" + folio + "', CodigoProducto = '" + codigo + "', Piezas = " + cantidadEstiba + ", Asignado = 1 where EPC = '" + EPC + "'";
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
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
                renglon = -1;
            }
            return renglon;
        }

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

        public string[] getCentroData(string id, string renglon, int renglonSub)
        {
            string[] cd = new string[2];
            string[] parametros = getParametros("Intelisis");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                string select = "select Centro,CentroDestino from ProdD where id=" + id + " and Renglon=" + renglon + " and RenglonSub = " + renglonSub;
                conn.Open();
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    cd[0] = reader.GetValue(0).ToString();
                    cd[1] = reader.GetValue(1).ToString();
                }
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}
            }
            return cd;
        }
    }
}
