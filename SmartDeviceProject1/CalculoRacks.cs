
using System;
using System.Collections.Generic;
using System.Text;
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
    class CalculoRacks//JLMQ EN ESTA CLASE ESTA LA LOGICA DE NEGOCIO DE LA ASIGNACION MANUAL DE RACKS PARA UNA ORDEN DE PRODUCCION
    {
        cMetodos cm = new cMetodos();

        public Decimal calculaRacks(string codigo, int tipoRack, Decimal pedido)//PASARLE EL CODIGO, TIPO DE RACK Y LA CANTIDAD
        {
            Decimal cantidad = 0;
            Decimal res = 0;
            cMetodos cm = new cMetodos();

            string[] parametros = cm.getParametros("Solutia");

            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            //primero se obtiene la cantidad de producto por ventana
            try
            {
                conn.Open();
                string select = "Select PxT from catArt where Clave = '" + codigo + "'";//FT estaba PxT
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    cantidad = Decimal.Parse(reader.GetString(0));
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                conn.Close();
                string error = e.Message; //using (StreamWriter writer = new StreamWriter("C:\\Users\\Desarrollo1\\Desktop\\debug.txt", true)){writer.WriteLine("Error: "+ error+"| Hora: "+DateTime.Now+"");}           
            }
            if (cantidad == 0)
            {
                return res;
            }
            else
            {
                /* Se busca numero de tabla por rack*/
                conn.Open();
                String sSqlStr = "";
                int nTablas = 0;
                sSqlStr = "Select RutaNivel from RProduccion where idRack = " + tipoRack;
                // SE busca el numero por tabla de producción segun el tipo de maquina-Rack (Basser1,2,3 o Compactas)
                SqlCommand cmd = new SqlCommand(sSqlStr, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    nTablas = Convert.ToInt16(reader.GetString(0));
                    conn.Close();
                }
                // Se calcula: 
                // Cantidad = pedido / (Piezas por tabla X nTablas) 
                //

                cantidad = pedido / (nTablas * cantidad);
                //cantidad = Decimal.Ceiling(cantidad);//redondear

                res = Convert.ToInt32(cantidad);
            }
            return res;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Funcion: getOpInfo
        //Proposito: muestra la informacion de una orden de produccion para el calculor de racks
        public DataTable getOpInfo(string op)
        {
            DataTable res = new DataTable();
            string[] parametros = cm.getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                {
                    string select = "SELECT 	ifc.OP,	ifc.CantAsignada As Piezas,	ifc.Codigo,	ifc.lote, 	ifc.renglon, 	ifc.calculoRacks AS Calculo, ifc.racksAsignados As Asignados, ifc.tipoRack, ifc.cantidadParcialidad, cp.id_parcialidad AS ID  FROM 	infoCalculoRacks ifc 	INNER JOIN catProd cp ON ifc.OP = cp.OrdenProduccion AND ifc.cantidadParcialidad = cp.prodAsignado WHERE 	cp.Asignado = 1 AND cp.Estatus = 'PENDIENTE' AND ifc.OP = '" + op + "'";
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

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Funcion: AsignaBautizaRack
        //Proposito: SELECCIONA Y BAUTIZA UN RACK
        public string AsignaBautizaRack(string op, decimal pzas, string codprod, int renglon, string epc, Decimal totalParcialidad)
        {
            string result = null;
            string query = "";
            int filasAfectadas = 0;
            string[] parametros = cm.getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();

                query = "UPDATE DetRProd SET OrdenProduccion = '" + op + "', Estado = 1, Verificado = 1, CantidadEstimada = " + pzas + " , CodigoProducto = '" + codprod + "', Renglon = " + renglon + ", cantidadParcialidad = " + totalParcialidad + " WHERE EPC = '" + epc + "' ";
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


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool RealUpdate(string op,decimal cantidadEstimada, int tipoR, string codigo, int renglon, string epc, Decimal totalPzas, string lote,Decimal cantidadParcialidad, string newId)
        {
            bool result = false;
            Decimal estimada;
            estimada = cantidadEstimada;
            Decimal total;
            total = totalPzas;

            //string update = query;
            string[] parametros = cm.getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            //checarRacks(); //JLMQ PONER LA FUNCION SOLO SI ES NECESARIO CHECKAR DE NUEVO EL RACK
            try
            {
                if (estimada <= total)
                {
                    conn.Open();
                    string update2 = "UPDATE DetRProd set OrdenProduccion = '" + op + "', Estado = 1, Verificado = 1 , CantidadEstimada =" + estimada + ", CodigoProducto = '" + codigo + "', Renglon = " + renglon + ", cantidadParcialidad = " + cantidadParcialidad + ", newId_Parcialidad = '" + newId + "'  where EPC ='" + epc + "' "; //VERIFICAR SI ASI FUNCIONA CORRECTAMENTE JLMQ 10OCT2018----AND IdRProd = " + tipoR + " AND Estado = 0 AND Verificado = 0";
                    SqlCommand cmd2 = new SqlCommand(update2, conn);
                    cmd2.ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    Decimal pzasTotal;
                    pzasTotal = (total - estimada);
                    string totalreal = "UPDATE infoCalculoRacks SET CantAsignada = " + pzasTotal + " WHERE OP = '" + op + "' AND CODIGO = '" + codigo + "' AND lote = '" + lote + "' AND renglon = " + renglon + " AND CantAsignada = " + totalPzas + " ";
                    SqlCommand cmd3 = new SqlCommand(totalreal, conn);
                    cmd3.ExecuteNonQuery();
                    conn.Close();
                    //total = total - estimada;//CREAR FUNCION QUE ACTUALICE ESTO
                    result = true;
                 }
                else if (total < estimada)
                {
                    conn.Open();
                    string update2 = "UPDATE DetRProd set OrdenProduccion = '" + op + "', Estado = 1, Verificado = 1, CantidadEstimada =" + estimada + ", CodigoProducto = '" + codigo + "', Renglon = " + renglon + ", cantidadParcialidad = " + cantidadParcialidad + ", newId_Parcialidad = '" + newId + "' where EPC ='" + epc + "' AND IdRProd = " + tipoR + " AND Estado = 0 AND Verificado = 0";
                    SqlCommand cmd2 = new SqlCommand(update2, conn);
                    cmd2.ExecuteNonQuery();
                    conn.Close();
                    total = 0;
                    result = true;
                }
            }
            catch (Exception e)
            {
                conn.Close();
                result = false;
            }
            return result;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Decimal cantidad(int tipoRack, string codigo)
        {
            Decimal cantidad = 0;
            string[] parametros = cm.getParametros("Solutia");
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
                //AIR éste cálculo es el que da un valor incorrecto - revisar
                //luego la cantidad por rack
                switch (tipoRack)
                {
                    case 1:
                        cantidad = cantidad * 28; break; //Besser1
                    case 2:
                        cantidad = cantidad * 36; break; //Compactas
                    case 3:
                        cantidad = cantidad * 28; break; //Besser2
                    case 4:
                        cantidad = cantidad * 42; break; //Besser3
                    default: break;
                }
                return cantidad;
            }
        }



        //FUNCNION: RacksAsignados
        //Verifica si ya hay Racks Asignados para la OP seleccionada
        public int RacksAsignados(string op, Decimal pedido, string codigo, int renglon, string lote)
        {
            string[] parametros = cm.getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                using (conn)
                {
                    string select = "SELECT racksAsignados FROM infoCalculoRacks WHERE OP = '" + op + "' AND Codigo = '" + codigo + "' AND CantAsignada = " + pedido + "  AND lote = '" + lote + "' AND renglon = " + renglon + " ";
                    SqlCommand command = new SqlCommand(select, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                        return int.Parse(reader.GetValue(0).ToString());
                        //return reader.GetValue(0).ToString();
                    else
                        return -1;
                }
                conn.Close();
            }
            catch (Exception exp)
            {
                conn.Close();
                return -1;
            }
        }

        //FUNCNION: cantidadReal
        //obtiene la cantidad real de la parcialidad seleccionada
        public int cantidadReal(string op, int racks, string codigo, int renglon, string lote)
        {
            //VERIFICAR SI SE PUEDE PASAR EL NEWID JLMQ
            string[] parametros = cm.getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                using (conn)
                {
                    string select = "SELECT CantAsignada FROM infoCalculoRacks WHERE OP = '" + op + "' AND Codigo = '" + codigo + "' AND calculoRacks = " + racks + "  AND lote = '" + lote + "' AND renglon = " + renglon + " ";
                    SqlCommand command = new SqlCommand(select, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                        return int.Parse(reader.GetValue(0).ToString());
                    //return reader.GetValue(0).ToString();
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


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Funcion: contRAcksAsign
        //Proposito: actualiza de 1 en 1 cada que se asigna un racks
        public string contRAcksAsign(string op, decimal pzas, string codprod, int renglon, int numRacks, string lote)
        {
            string result = null;
            string query = "";
            int filasAfectadas = 0;
            string[] parametros = cm.getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();

                query = "UPDATE infoCalculoRacks SET racksAsignados = " + numRacks + " WHERE OP = '" + op + "' AND Codigo = '" + codprod + "' AND CantAsignada = " + pzas + " AND lote = '" + lote + "'  AND renglon = " + renglon + "";
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


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Funcion: racksCompletados
        //Proposito: actualiza el estatus a 1 indicando con esto que la parcialidad tiene todos los racks calculados asignados
        public string racksCompletados(string op, decimal pzas, string codprod, int renglon , string lote)
        {
            string result = null;
            string query = "";
            int filasAfectadas = 0;
            string[] parametros = cm.getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();

                query = "UPDATE infoCalculoRacks SET racksCompleto = 1 WHERE OP = '" + op + "' AND Codigo = '" + codprod + "' AND CantAsignada = " + pzas + " AND lote = '" + lote + "'  AND renglon = " + renglon + "";
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


        //Funcion: validaEPC
        //Proposito: valida si el EPC recien leido esta disponible y que sea del tipo de rack que se eligio
        public bool validaEPC(string epc, int id)
        {
            string[] parametros = cm.getParametros("Solutia");
            SqlConnection conn = new SqlConnection("Data Source=" + parametros[1] + "; Initial Catalog=" + parametros[4] + "; Persist Security Info=True; User ID=" + parametros[2] + "; Password=" + parametros[3] + "");
            try
            {
                conn.Open();
                using (conn)
                {
                    string select = "SELECT * FROM DetRProd WHERE IdRProd = " + id + " AND EPC = '" + epc + "' AND Estado = 0 AND Verificado = 0";
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

    }
}
