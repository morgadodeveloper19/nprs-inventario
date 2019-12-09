using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Intermec.DataCollection;
using Intermec.DataCollection.RFID;
using Intermec.Device.Audio;
using System.Threading;
//using test_emulator;


namespace SmartDeviceProject1.Inventario
{
	
    public partial class Producto_Stock : Form
    {
		// Reader vars
		BRIReader reader;
		Boolean b = false;
		PolyTone Ptone1 = new PolyTone();
		PolyTone Ptone2 = new PolyTone(300, 100, Tone.VOLUME.VERY_LOUD);
		Boolean RFID = false;
		String att1 = "100";
		String ant = "1";
		string tag = null;
		Thread hilo;
		//

		// Nombres constantes para llamar los campos del DataTable
		const string COL_EPC = "EPC";
		const string COL_CANTIDAD = "Cantidad";
		const string COL_UBICACION = "Ubicacion";

		// Toma el producto disponible para stock de la columna llamada "prodAsignado" de la tabla catProd
		// En caso que se requiera cambiar a prodConcluido bastaría con cambiar el valor de la siguiente 
		// variable en el archivo cMetodos para que los querys alimenten los DataTable.
		public const string PROD_TERMINADO = cMetodos.PROD_TERMINADO;

		// Some stuff
        cMetodos met = new cMetodos();
		DataTable tabla = null, dtProdTerminados = null, dtProdTerminadosAux = null;
		List<cMetodos.Producto> listaEPCS = null;
		string ordenProduccion = "", descripcionProd = "";
		string nombreCompleto = "Operario";
		int cantidadTotal = 0, indexDG = 0;
		Boolean todasParciales = true;
		
        public Producto_Stock(string nombre, string apellidos)
        {
            InitializeComponent();
            try
            {
                cbProdBusq.SelectedIndexChanged -= new EventHandler(cbProdBusq_SelectedIndexChanged);
                string query = "SELECT cp.Descripcion as Items, cp.Codigo as ID "
                    + "FROM catProd cp INNER JOIN DetEscuadras de "
                    + "ON (cp.OrdenProduccion = de.OrdenProduccion "
                    + "AND cp.Codigo = de.CodigoProducto "
                    + "AND cp." + PROD_TERMINADO + " = de.Piezas) "
                    + "WHERE cp.Estatus = 'TERMINADO' "
                    + "GROUP BY Descripcion, Codigo ORDER BY Descripcion;";
                llenaCB(cbProdBusq, "Items", "ID", query, cMetodos.CONEXION);
                cbOrdProdTerm.Enabled = false;
                cbProdBusq.SelectedIndexChanged += new EventHandler(cbProdBusq_SelectedIndexChanged);

                cbProdBusq.Enabled = true;
                dgTarimas.Enabled = false;
                btnLeer.Enabled = false;
                listaEPCS = new List<cMetodos.Producto>();
                nombreCompleto = nombre + " " + apellidos;
            }
            catch (Exception ps)
            {
                
                MessageBox.Show("Hubo un pequeño detalle de comunicacion. Por favor intentelo de nuevo más tarde", "Advertencia");
            }
        }

        public void llenaCB(ComboBox Objeto, string nomCve, string idCve, string consulta, string conex)
        {
            DataTable dt = met.getDatasetConexionWDR(consulta, conex);
            if (dt == null)
            {
                MessageBox.Show("Error en la BD, intentalo más tarde");
                this.Close();
                return;
            }

            Objeto.DataSource = null; 
            Objeto.DataSource = dt;
            Objeto.DisplayMember = nomCve;
            Objeto.ValueMember = idCve;
			dt.Columns[0].MaxLength = 255;
            DataRow dr = dt.NewRow();
			string opcSelec = "SELECCIONAR";
			dr[nomCve] = (dt.Rows.Count > 0) ? opcSelec : "SIN ELEMENTOS";
			dr[idCve] = 0;
			try {
				dt.Rows.InsertAt((dr), 0);
			} catch (Exception e) {
				dt.Columns[0].MaxLength = 255;
				dt.Rows.InsertAt((dr), 0);
				//throw;
			}
            Objeto.SelectedValue = 0;
        }

		private void cbProdBusq_SelectedIndexChanged(object sender, EventArgs e) {
			cbOrdProdTerm.Enabled = (cbProdBusq.SelectedValue.ToString() == "0") ? false : true;
			if (cbOrdProdTerm.Enabled) {
				cbOrdProdTerm.SelectedIndexChanged -= new EventHandler(cbOrdProdTerm_SelectedIndexChanged);
				descripcionProd = cbProdBusq.SelectedValue.ToString();
				string query = "SELECT cp.OrdenProduccion, cp.OrdenProduccion AS ID "
					+ "FROM catProd cp JOIN DetEscuadras de "
					+ "ON (cp.OrdenProduccion = de.OrdenProduccion AND cp.Codigo = '" + descripcionProd + "' AND cp." + PROD_TERMINADO + " = de.Piezas) "
					+ "WHERE (cp.Estatus = 'TERMINADO' AND cp." + PROD_TERMINADO + " > 0)"
					+ "GROUP BY cp.OrdenProduccion, cp.Codigo ORDER BY cp.OrdenProduccion;";
				llenaCB(cbOrdProdTerm, "OrdenProduccion", "ID", query, cMetodos.CONEXION);
				cbOrdProdTerm.SelectedIndexChanged += new EventHandler(cbOrdProdTerm_SelectedIndexChanged);
				clearStuff();
			}
		}

		private void cbOrdProdTerm_SelectedIndexChanged(object sender, EventArgs e) {
			string valor = cbOrdProdTerm.SelectedValue.ToString();
			if (!valor.Equals("0")) {
				ordenProduccion = cbOrdProdTerm.SelectedValue.ToString();
				string query = "SELECT cp.prodAsignado AS [" + COL_CANTIDAD + "], "
					+ "de.Posicion AS [" + COL_UBICACION + "], "
					+ "de.EPC AS [" + COL_EPC +"] "
					+ "FROM catProd cp JOIN DetEscuadras de "
					+ "ON (de.OrdenProduccion = '" + ordenProduccion + "' AND de.CodigoProducto = cp.Codigo AND cp." + PROD_TERMINADO + " = de.Piezas) "
					+ "WHERE (cp.Estatus = 'TERMINADO' AND cp." + PROD_TERMINADO + " > 0 AND de.CodigoProducto = '" + descripcionProd + "') ";
				
				clearStuff();	
				dtProdTerminados = met.getDatasetConexionWDR(query, cMetodos.CONEXION);
				dtProdTerminadosAux = dtProdTerminados.Copy();
				dgTarimas.DataSource = dtProdTerminados;
				setStyle(dgTarimas, dtProdTerminados);
				dgTarimas.Enabled = true;
			} else {
				clearStuff();
			}
		}

		private void dgTarimas_Click(object sender, EventArgs e) {
			int rowIndex = dgTarimas.CurrentRowIndex;
			//string producto = dgTarimas[rowIndex, 2].ToString();
			//MessageBox.Show(producto);
		}

		//Botón de conectar el reader con la Hand Held, debe de estar pareado con anterioridad.
		private void btnConectar_Click(object sender, EventArgs e) {
			Cursor.Current = Cursors.WaitCursor;
            try {
				reader = new BRIReader(this, null);
				if (reader.Attributes.SetTAGTYPE("EPCC1G2") == true) {
					if (reader.IsConnected == true) {
						lblStatus.Text = "READER CONECTADO";
						btnConectar.Enabled = false;
						btnLeer.Enabled = true;
						b = true;
						btnLeer.Focus();
					} else {
						MessageBox.Show("No se pudo establecer el protocolo Gen2. Intente conectarse de nuevo o reinicie el lector.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
						b = false;
					}
				} else {
					MessageBox.Show("No se pudo establecer el protocolo Gen2. Intente conectarse de nuevo o reinicie el lector.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
					b = false;
					reader.Dispose();
				}
			} catch (Exception ex) {
				MessageBox.Show(ex.Message);
				MessageBox.Show("No se pudo establecer el protocolo Gen2. Intente conectarse de nuevo o reinicie el lector.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
				b = false;
			}
			Cursor.Current = Cursors.Default;
		}

		//Comienza la lectura y activa la señal del reader con los atributos de lectura, te lleva a la función leer_tag.
		private void btnBuscar_Click(object sender, EventArgs e) {
			Cursor.Current = Cursors.WaitCursor;          
            
            try {
				if (b == true) {
					//lstEpc.Items.Clear();
					txtEPC.Text = "";
					tag = "";
					reader.Execute("Attrib ants=" + ant);
					reader.Execute("Attrib fieldstrength=" + att1);
					lblStatus.Text = "Lectura iniciada";
					hilo = new Thread(leer_tag);
					RFID = true;
					// whats this for?
					btnLeer.Visible = false;
					btnLeer.Enabled = false;
					btnLeer.Visible = true;
					btnLeer.Enabled = true;
					//
					hilo.Start();
				} else {
					lblStatus.Text = "Sin conexión del reader";
				}
			} catch (Exception ex) {
				string men = ex.Message;
				string algo = "";
			}
			Cursor.Current = Cursors.Default;
		}

		//Función para la lectura de 1 tag y posicionarlo en el txtTagEPC y la variable tag.
		public void leer_tag() {
            
            try {
				while (RFID == true) {
					reader.Read();
					if (reader.TagCount > 0) {
						int contador = 0;
						foreach (Tag eti in reader.Tags) {
							if (contador == 0) {
								//Ptone1.Play();
								if (txtEPC.InvokeRequired) {
									txtEPC.Invoke((Action)(() => txtEPC.Text = eti.ToString()));
									tag = eti.ToString();
								}
								//Ptone2.Play();                                                                                    
								RFID = false;
								if (btnLeer.InvokeRequired) {
									btnLeer.Invoke((Action)(() => btnLeer.Visible = true));
									btnLeer.Invoke((Action)(() => btnLeer.Enabled = true));
								}
								
								if (lblStatus.InvokeRequired) {
									lblStatus.Invoke((Action)(() => lblStatus.Text = "Tag leído."));
								}
								if (btnMover.InvokeRequired) {
									btnMover.Invoke((Action)(() => btnMover.Focus()));
								}
							}
							contador++;
							break;
						}
					}
				}
			} catch (ObjectDisposedException odex) {
				//MessageBox.Show(odex.Message, "Error de lectura.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
			} catch (ThreadAbortException ex) {
				MessageBox.Show(ex.Message, "Error de lectura.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
			} catch (Exception eex) {
				MessageBox.Show(eex.Message, "Error de lectura.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
			}
		}

		//Función que deberá de actualizar a la base de datos la información relacionada a las tarimas a ubicar.
		private void btnMandaStock_Click(object sender, EventArgs e) {
			
		}

		//Funicón para borrar el campo del EPC así como la variable tag.
		private void btnBorrar_Click(object sender, EventArgs e) {
			//txtTagEpc.Text = "";
			//tag = "";
		}

		//Función para detener la lectura, si es que fuera continua.
		private void btnDetener_Click_1(object sender, EventArgs e) {
			/* btnBuscar.Visible = true;
			btnBuscar.Enabled = true;
			btnDetener.Visible = false;
			btnDetener.Enabled = false;
			RFID = false; */
		}

		public void limpia() {
			if (hilo != null) {
				hilo.Abort();
			}
			if (reader != null) {
				reader.Dispose();
			}
			if (btnConectar.Enabled == false) {
				btnConectar.Enabled = true;
			}
			b = false;
			//this.Close();
		}

		private void btnMover_Click(object sender, EventArgs e) {
			string msg = null;
			if (tag != null && dtProdTerminados.Rows.Count > 0) {
				bool existeEnDG, estaEnListaStock = false;
				cMetodos.Producto producto = new cMetodos.Producto();
				// Validar si el EPC existe en el DG
				existeEnDG = (dtProdTerminados != null) ? buscarEnDG(ref producto) : false;
				// Mover a segundo DG (Stock)
				if (existeEnDG) {
					DataTable tabla = getTable();
					estaEnListaStock = existeEnListaStock(producto.epc);
					if (!estaEnListaStock) {
						listaEPCS.Add(producto);
						tabla.Rows.Add(producto.epc, producto.cantidad);
						eliminarDeListaTarimas(producto.epc);
					}
					setStyle(dgStock, tabla);
					dgStock.DataSource = tabla;
				}

				msg = (dtProdTerminados == null) ? "No se ha seleccionado ninguna Orden de Producción."
					: (existeEnDG) ?
						(!estaEnListaStock) ? "Encontró el EPC en el DT: " + tag
						: "Este tag ya ha sido leído."
					: "Este tag no corresponde a esta Orden de Producción.";
			} else {
				msg = (dtProdTerminados == null) ? "Seleccione primero una Orden de Producción."
					: (dtProdTerminados.Rows.Count == 0) ? "No hay más Ordenes de Producción disponibles."
					: "No se ha leído ningún tag!";
			}
			MessageBox.Show(msg);
			tag = null;
			txtEPC.Text = "";
		}

		private Boolean existeEnListaStock(String epc) {
			for (int i = 0; i < listaEPCS.Count; i++) {
				if (epc.Equals(listaEPCS[i].epc)) {
					return true;	
				}
			}

			return false;
		}
	
		private DataTable getTable() {
			if (tabla == null) {
				tabla = new DataTable();
				tabla.Columns.Add(COL_EPC, typeof(string));
				tabla.Columns.Add(COL_CANTIDAD, typeof(int));
			}
			
			return tabla;
		}

		private void clearStuff() {
			dgTarimas.DataSource = null;
			dgStock.DataSource = null;
			dtProdTerminados = null;
			listaEPCS.Clear();
			tag = null;
			txtEPC.Text = "";
			lblStatus.Text = (b) ? "READER CONECTADO" : "STATUS";
			DataTable tabla = getTable();
			tabla.Clear();
			todasParciales = true;
		}

		private String enviarMail(string emailTo, string cantidad) {
			return cMetodos.SendEmail(
					nombreCompleto,
					emailTo,
					ordenProduccion,
					descripcionProd,
					cantidad);
		}

		private void menuItem2_Click(object sender, EventArgs e) {
			string msj = "";
			/*foreach (cMetodos.Producto producto in listaEPCS) {
				if (!producto.tarimaParcial) {
					todasParciales = false;
					break;
				}
			}*/

			//if (txtUbicacion.Text.Length > 0 || todasParciales == true) {
				//string nuevaUbicacion = txtUbicacion.Text;
				if (listaEPCS.Count > 0) {
					Cursor.Current = Cursors.WaitCursor;
					int cantidad = 0;
					cMetodos ws = new cMetodos();
					foreach (cMetodos.Producto prod in listaEPCS) {
						cantidad += prod.cantidad;
					}

					// Enviar email a encargado de Producción en Napresa
					string planta = ordenProduccion.Substring(0, 2);
					cMetodos metodos = new cMetodos();
					string[] emails = metodos.emailsProcesoStock(planta);
					int cont = 0;
					if (emails != null) {
						for (int i = 0; i < emails.Length; i++) {
							if (enviarMail(emails[i], cantidad.ToString()) != null) 
								cont++;
						}

						if (cont == 0) {
							MessageBox.Show("Error en el WebService al enviar emails. Intentelo de nuevo más tarde.");
							Cursor.Current = Cursors.Default;
							this.Dispose();
							return;
						}
					} else {
						if (enviarMail(cMetodos.EMAIL_TO_DEFAULT, cantidad.ToString()) == null) {
							MessageBox.Show("Error en el WebService al enviar emails. Intentelo de nuevo más tarde.");
							Cursor.Current = Cursors.Default;
							this.Dispose();
							return;
						}
						cont++;
					}

					// Modificar datos en BD
					foreach (cMetodos.Producto prod in listaEPCS) {
						ws.sendToStock(descripcionProd, ordenProduccion, prod.cantidad, prod.cantidadTotal);
						ws.escuadrasToStock(prod.epc, "A01", prod.tarimaParcial, prod.cantidad, descripcionProd);
						/*if (prod.tarimaParcial) {
							ws.crearEscuadraVirtual(descripcionProd, prod.cantidad);
						}*/
					}
					ws = null;
					msj = "Se han enviado " + cont + " correos de notificación a los encargados correspondientes de la planta.";
					this.Dispose();
				} else {
					msj = "Debes hacer el proceso de selección de tarimas antes de poder reubicar.";
					cbProdBusq.Focus();
				}
			/*} else {
				msj = "Ingresa la nueva ubicación.";
				txtUbicacion.Focus();
			}*/
			Cursor.Current = Cursors.Default;
			MessageBox.Show(msj);
		}

		private void menuItem1_Click(object sender, EventArgs e) {
			this.Dispose();
		}

		private void setStyle(DataGrid dg, DataTable dt) {
			dg.TableStyles.Clear();
			DataGridTableStyle tableStyle = new DataGridTableStyle();
			tableStyle.MappingName = dt.TableName;
			foreach (DataColumn item in dt.Columns) {
				DataGridTextBoxColumn tbcName = new DataGridTextBoxColumn();
				tbcName.Width = 70;
				tbcName.MappingName = item.ColumnName;
				tbcName.HeaderText = item.ColumnName;
				tableStyle.GridColumnStyles.Add(tbcName);
			}
			dg.TableStyles.Add(tableStyle);
		}

		private void dgTarimas_DoubleClick(object sender, EventArgs e) {
			txtCantToStock.Text = "";
			indexDG = dgTarimas.CurrentRowIndex;
			cantidadTotal = Int32.Parse(dtProdTerminados.Rows[indexDG][COL_CANTIDAD].ToString());
			panelStock.Visible = true;
			panelStock.Enabled = true;
			lblCantidad.Text = "de " + cantidadTotal;
			dgTarimas.Enabled = false;
			panelStock.Focus();
		}

		private void eliminarDeListaTarimas(String epc) {
			DataRow dr;
			String epcDr = "";
			for (int index = 0; index < dtProdTerminados.Rows.Count; index++) {
				dr = dtProdTerminados.Rows[index];
				epcDr = dr[COL_EPC].ToString();
				if (epcDr.Equals(epc)) {
					dtProdTerminados.Rows.Remove(dr);
					break;
				}
			}
			dr = null;
		}

		private Boolean buscarEnDG(ref cMetodos.Producto producto) {
			for (int index = 0; index < dtProdTerminadosAux.Rows.Count; index++) {
				if (tag.Equals(dtProdTerminadosAux.Rows[index][COL_EPC].ToString())) {
					producto.cantidad = Int32.Parse(dtProdTerminadosAux.Rows[index][COL_CANTIDAD].ToString());
					producto.cantidadTotal = producto.cantidad;
					producto.tarimaParcial = false;
					producto.epc = dtProdTerminadosAux.Rows[index][COL_EPC].ToString();
					return true;
				}
			}

			return false;
		}

		private void btnCancelar_Click(object sender, EventArgs e) {
			panelStock.Visible = false;
			panelStock.Enabled = false;
			dgTarimas.Enabled = true;
			dgTarimas.Focus();
		}

		private void btnToStock_Click(object sender, EventArgs e) {
			String msg = "";
			bool procedeCantidad = true;
			int cantidadAStock = 0;

			try {
				if (txtCantToStock.Text.Length == 0) {
					msg = "No se aceptan valores vacíos.";
					procedeCantidad = false;
				}

				if (procedeCantidad) {
                    cantidadAStock = Int32.Parse(txtCantToStock.Text);//panelStock
					if (cantidadAStock == 0)
						msg = "La cantidad tiene que ser mayor a 0.";
					else if (cantidadAStock > cantidadTotal)
						msg = "La cantidad ingresada excede la cantidad total de la tarima.";

					procedeCantidad = (msg.Equals("")) ? true : false;
				}
			} catch (Exception) {
				msg = "Valor inválido para la cantidad.";
				procedeCantidad = false;
			}

			if (procedeCantidad) {
				cMetodos.Producto producto = new cMetodos.Producto();
				producto.epc = dtProdTerminados.Rows[indexDG][COL_EPC].ToString();
				producto.cantidad = cantidadAStock;
				producto.cantidadTotal = cantidadTotal;
				producto.tarimaParcial = true;
				listaEPCS.Add(producto);
				DataTable tabla = getTable();
				tabla.Rows.Add(producto.epc, producto.cantidad);
				eliminarDeListaTarimas(producto.epc);
				dgStock.DataSource = tabla;
				panelStock.Visible = false;
				panelStock.Enabled = false;
				dgTarimas.Enabled = true;
			}

			if (!procedeCantidad) {
				txtCantToStock.Focus();
				MessageBox.Show(msg);
			}
		}
	}
}
