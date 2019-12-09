package com.example.pruebas;

import android.app.Activity;
import android.content.Intent;
import android.content.pm.ActivityInfo;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.location.Criteria;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.RadioGroup.OnCheckedChangeListener;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import org.ksoap2.SoapEnvelope;
import org.ksoap2.serialization.SoapObject;
import org.ksoap2.serialization.SoapPrimitive;
import org.ksoap2.serialization.SoapSerializationEnvelope;
import org.ksoap2.transport.HttpTransportSE;

import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Locale;

public class Eventos extends Activity implements LocationListener{
	public RadioButton r1,r2,r3,r4,r5;
	public Button btnSend;
	public TextView tv1;
	public Spinner spinner, spinner1;
	public EditText txtPIN,Edit1;
	public ImageView logPlay, logPause, logHome, logOk, logAlert;
	public ImageButton btnBack;
	
	public static final String KEY_ROWID="idExcepcion";
	public static final String KEY_TITLE="Descripcion";
	public int evento, Expected=0, estadoFolio, excepcion=0;
	public static String idChofer, folio;
    String idX; 
	public String respuesta;
	private String TAG ="";
	boolean flagEntregado, flagPinValido=false, flagHayPIN=false, pendientesEnvio=false, errorConexion;
	
	SimpleDateFormat sdfH = new SimpleDateFormat("yyyyMMdd",Locale.getDefault());
	String fecha = sdfH.format(Calendar.getInstance().getTime());
	SimpleDateFormat sdfD = new SimpleDateFormat("HHmmss", Locale.getDefault());
	String hora = sdfD.format(Calendar.getInstance().getTime());
	
	
	
	final String METHOD_NAME = "setEvento";
	final String SOAP_ACTION = Globals.NAMESPACE + METHOD_NAME + "";

	
	// GPS
	LocationManager locationManager;
	private String provider;
	
	//al crear el formulario
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_eventos);
		setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_PORTRAIT);
		
		r1 = (RadioButton)findViewById(R.id.radio0);
	    r2 = (RadioButton)findViewById(R.id.radio1);
	    r3 = (RadioButton)findViewById(R.id.radio2);
	    r4 = (RadioButton)findViewById(R.id.radio3);
	    r5 = (RadioButton)findViewById(R.id.radio4);
	    spinner = (Spinner)findViewById(R.id.spinner1);
	    tv1 = (TextView)findViewById(R.id.textView1);
	    btnSend = (Button)findViewById(R.id.button1);
	    txtPIN = (EditText)findViewById(R.id.txtPIN);
	    logPlay = (ImageView)findViewById(R.id.imagePlay);
	    logPause = (ImageView)findViewById(R.id.imagePause);
	    logHome = (ImageView)findViewById(R.id.imageHome);
	    logOk = (ImageView)findViewById(R.id.imageOk);
	    logAlert = (ImageView)findViewById(R.id.imageAlert);
	    btnBack = (ImageButton)findViewById(R.id.imageButtonBack);
	    	    
	    
	    Intent i = getIntent();
    	idChofer= i.getStringExtra("idChofer");
    	folio=i.getStringExtra("folio");
    	estadoFolio=i.getIntExtra("estado",0);
    	tv1.setText("Folio: "+folio);
		
    	
    	// GPS
    	
    	locationManager = (LocationManager) getSystemService(LOCATION_SERVICE);
    	Criteria criteria = new Criteria();
    	provider = locationManager.getBestProvider(criteria, false);

    	
    	
    	
    	
    	
		if(estadoFolio==0){
			runOnUiThread(new Runnable() {
			    public void run() {

					// Guarda en parametros para hilo
					String[] params={
							txtPIN.getText().toString(),
							String.valueOf(r1.isChecked()),
							String.valueOf(r2.isChecked()),
							String.valueOf(r3.isChecked()),
							String.valueOf(r4.isChecked()),
							String.valueOf(r5.isChecked()),
							spinner.getSelectedItem().toString()
					};


					AsyncCall ac = new AsyncCall();
					ac.execute(params);
			    }
			});
		}		
		
		seleccionaEstado(estadoFolio);
		
		
		RadioGroup radioGroup = (RadioGroup) findViewById(R.id.radioGroup1);        
	    radioGroup.setOnCheckedChangeListener(new OnCheckedChangeListener() 
	    {
	        public void onCheckedChanged(RadioGroup group, int checkedId) {
	    		if(r5.isChecked()){
	    			spinner.setVisibility(View.VISIBLE);
	    		}else{
	    			spinner.setVisibility(View.INVISIBLE);
	    		}
	    		if(r4.isChecked() || r5.isChecked()){
	    			txtPIN.setVisibility(View.VISIBLE);
    			}else{
    				txtPIN.setVisibility(View.INVISIBLE);
    				txtPIN.setText("");
				}
	        }
	    });
		    
    	btnBack.setOnClickListener(new OnClickListener() {
    		@Override
    		public void onClick(View v) {
				Intent i = new Intent(getApplicationContext(), Folios.class).addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
				i.putExtra("idChofer", idChofer);
				startActivity(i);
				finish();
    		}
    	});
    	btnSend.setOnClickListener(new OnClickListener() {
    		@Override
    		public void onClick(View v) {
    			btnSend.setEnabled(false);
    			Toast.makeText(getApplicationContext(), getString(R.string.enviando_eventos) , Toast.LENGTH_LONG).show();
    			runOnUiThread(new Runnable() {
    			    public void run() {
						String[] params={
								txtPIN.getText().toString(),
								String.valueOf(r1.isChecked()),
								String.valueOf(r2.isChecked()),
								String.valueOf(r3.isChecked()),
								String.valueOf(r4.isChecked()),
								String.valueOf(r5.isChecked()),
								spinner.getSelectedItem().toString()
						};


						AsyncCall ac = new AsyncCall();
						ac.execute(params);
					}
    			});
			}
		});
	}
	
	
	
	// selecciond el estado del folio actual
	private void seleccionaEstado(int estado){

		logPlay.setImageResource(R.drawable.playgray);
		logPause.setImageResource(R.drawable.pausegray);				
		logHome.setImageResource(R.drawable.handshakegray);
		logOk.setImageResource(R.drawable.okgray);
		logAlert.setImageResource(R.drawable.alertgray);

		switch (estado){
			//case 0: r2.setChecked(true); break;
			case 1:
				r2.setChecked(true);
				break;
			case 2:
				r2.setChecked(true);
				logPlay.setImageResource(R.drawable.play);
				break;
			case 3:
				r1.setChecked(true);
				logPause.setImageResource(R.drawable.pause);				
				break;
			case 4:
				r3.setChecked(true);
				logHome.setImageResource(R.drawable.handshake);
				break;
			case 5:
				r4.setChecked(true);
				logOk.setImageResource(R.drawable.ok);
				break;
			case 6:
				r5.setChecked(true);
				logAlert.setImageResource(R.drawable.alert);
				break;
		}
	}
	 
	
	// inserta el evento mediante el webservice
	public boolean insertaEvento(String idFolio,int evento,int excepcion,String fecha, String hora){	
		Boolean estado = false;
		errorConexion = false;
		try
		{	
        	Location location = locationManager.getLastKnownLocation(provider);
        	SoapObject request = new SoapObject(Globals.NAMESPACE, METHOD_NAME);
			SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(SoapEnvelope.VER11);
			envelope.dotNet = true;
			request.addProperty("folio",folio);
			request.addProperty("idEvento",evento);
			request.addProperty("idExcepcion",excepcion);
			request.addProperty("fecha",fecha);
			request.addProperty("hora",hora);
			if (location==null){
				request.addProperty("latitud","0.0");
				request.addProperty("longitud","0.0");
			}else{
				request.addProperty("latitud", String.valueOf(location.getLatitude()));
				request.addProperty("longitud", String.valueOf(location.getLongitude()));
			}
			request.addProperty("omiteReglas","false");
			
			envelope.setOutputSoapObject(request);
			HttpTransportSE transporte = new HttpTransportSE(Globals.getUrlWebService(getApplicationContext()));
			transporte.call(SOAP_ACTION, envelope);
			SoapPrimitive resultado_xml = (SoapPrimitive) envelope.getResponse();
			respuesta = resultado_xml.toString();
			if(respuesta.contains("OK"))
				estado = true;
		}catch (Exception e){
			errorConexion = true;
			  respuesta = "Error de conexion Eventos";
			  //Toast.makeText(getApplicationContext(), Globals.getMsj(getApplicationContext(),"Error de conexion Eventos"), Toast.LENGTH_LONG).show();
		}
		return estado;
	}
	
	
	// verifica que el PIN corresponda a  el Folio
	public boolean validarPin(String PIN){
		boolean result = false;
		try{
			String TableName = "Folios";
			SQLiteDatabase myDB= null;  
			myDB = this.openOrCreateDatabase("DBLogs", MODE_PRIVATE, null);
			String select = "SELECT * FROM " + TableName +" WHERE Clave = '" + PIN + "' AND IDRemision ='" + folio + "'";
			Cursor c = myDB.rawQuery(select, null);
			c.moveToFirst();
			if(c.getCount()>0)
				return true;
			
			
			
			myDB.close();
		}catch (Exception e) {
		}
		return result;
	}


	// establece enviado en la base local
	protected void updateFolios(String folio){
		try{
			String TableName = "Folios";
			SQLiteDatabase myDB= null;   
			myDB = this.openOrCreateDatabase("DBLogs", MODE_PRIVATE, null);
			String update="Update "+TableName+" SET Enviado = '1' WHERE IDRemision = '" + folio + "'";
			myDB.execSQL(update);
			myDB.close();
		}catch(Exception e){
		}
	}

	// establece pendiente para enviar en base local
	protected void updateFoliosNE(String folio){
		//agrega registros a tabla temporal
		try{			
			Location location = locationManager.getLastKnownLocation(provider);
        	String latitud, longitud;
			if (location==null){
				latitud = "0.0";
				longitud = "0.0";
			}else{
				latitud =  String.valueOf(location.getLatitude());
				longitud = String.valueOf(location.getLongitude());
			}
			
			SQLiteDatabase myDB = null;
			myDB = this.openOrCreateDatabase("DBLogs", MODE_PRIVATE, null);
			String TableName3 = "FoliosPendientes";
			myDB.execSQL("INSERT INTO "
			+ TableName3
			+ " (folio, idEvento, idExcepcion, fecha, hora, latitud, longitud)"
			+ " VALUES ('" + folio + "', '" + evento + "', '" + excepcion + "', '" + fecha + "', '" + hora + "', '" + latitud + "', '" +longitud + "');");
			myDB.close();			

			pendientesEnvio = true;
			respuesta = "Guardado en App";

		}catch(Exception e){	
		}
		
		
		//elimina registro de tabla de folios
		try{
			SQLiteDatabase myDB= null;   
			myDB = this.openOrCreateDatabase("DBLogs", MODE_PRIVATE, null);
			//String update="Update Folios SET Enviado = '2' WHERE IDRemision = '"+folio+"'";
			myDB.delete("Folios", "IDRemision = '"+folio+"'", null);
			myDB.close();
		}catch(Exception e){
			
		}
		
		
		
		
		
		
		
		
	}

	// agrega menu
	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		getMenuInflater().inflate(R.menu.menu_secundario, menu);
		return true;
	}
	
	//agrega eventos al menu
	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		switch (item.getItemId()) {
	        case R.id.action_logout:
				Intent i = new Intent(getApplicationContext(), Principal.class).addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
				startActivity(i);
				finish();
	            return true;
	        case R.id.action_reload:
				Intent i2 = new Intent(getApplicationContext(), CargaFolios.class).addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
				i2.putExtra("idChofer", idChofer);
				startActivity(i2);
				finish();
	            return true;
	        case R.id.action_exit:
	        	android.os.Process.killProcess(android.os.Process.myPid());
	        	return true;
	        default:
	            return super.onOptionsItemSelected(item);
		}
	}
	
	
	
	
	
	/********************************************************************************************
	 *							Actividad de aplicasion para gps
	 ********************************************************************************************/
	

	  /* Request updates at startup */
	  @Override
	  protected void onResume() {
	    super.onResume();
	    locationManager.requestLocationUpdates(provider, 400, 1, this);
	  }

	  /* Remove the locationlistener updates when Activity is paused */
	  @Override
	  protected void onPause() {
	    super.onPause();
	    locationManager.removeUpdates(this);
	  }

	  @Override
	  public void onLocationChanged(Location location) {
	  }

	  @Override
	  public void onStatusChanged(String provider, int status, Bundle extras) {
	    // TODO Auto-generated method stub
	  }

	  @Override
	  public void onProviderEnabled(String provider) {
	  }

	  @Override
	  public void onProviderDisabled(String provider) {
	    Toast.makeText(this, "Disabled provider " + provider,
	        Toast.LENGTH_SHORT).show();
	  }
	
	
	
	
	  
		/********************************************************************************************
		 *							llamada asyncrona 
		 ********************************************************************************************/
	
	
	
	// escoge que evento insertar segun el checkbox
	private class AsyncCall extends AsyncTask<String, Void, Void>{
		@Override
		protected Void doInBackground(String... params) {
			try{
				String PIN = params[0];
				if(estadoFolio==0){
					flagEntregado = insertaEvento(folio,1,0,fecha,hora);
					estadoFolio=1;
				}else if(Boolean.valueOf(params[1])){
					flagEntregado = insertaEvento(folio,3,0,fecha,hora);
				}else if(Boolean.valueOf(params[2])){
					flagEntregado = insertaEvento(folio,2,0,fecha,hora);
				}else if(Boolean.valueOf(params[3])){
					flagEntregado = insertaEvento(folio,4,0,fecha,hora);
				}else if(Boolean.valueOf(params[4])){
					flagHayPIN = PIN.length() > 0;
					if (flagHayPIN == true){
						flagPinValido = validarPin(PIN);
						if(flagPinValido){
							flagEntregado=insertaEvento(folio,5,0,fecha,hora);
							evento=5;

							if(flagEntregado){
								updateFolios(folio);
							}else{
								if (errorConexion)
									updateFoliosNE(folio);
							}
						}else{respuesta=null;}////// error pin no
					}else{respuesta=null;}////// error pin no
				}else if(Boolean.valueOf(params[5])){
					String text = params[6];
					flagHayPIN = PIN.length() > 0;
					if(flagHayPIN){
						flagPinValido = validarPin(PIN);
						if(flagPinValido){
							if(text.equals("No hay quien reciba")){
								flagEntregado = insertaEvento(folio,6,0,fecha,hora);
								excepcion=0;
							}else if(text.equalsIgnoreCase("Llegada fuera de horario")){
								flagEntregado = insertaEvento(folio,6,1,fecha,hora);
								excepcion=1;
							}else if(text.equalsIgnoreCase("Entrega duplicada")){
								flagEntregado = insertaEvento(folio,6,2,fecha,hora);
								excepcion=2;
							}else if(text.equalsIgnoreCase("No hay acceso")){
								flagEntregado = insertaEvento(folio,6,3,fecha,hora);
								excepcion=3;
							}else if(text.equalsIgnoreCase("Error en el pedido")){
								flagEntregado = insertaEvento(folio,6,4,fecha,hora);
								excepcion=4;
							}else if(text.equalsIgnoreCase("Faltante o sobrante")){
								flagEntregado = insertaEvento(folio,6,5,fecha,hora);
								excepcion=5;
							}else if(text.equalsIgnoreCase("Mercancia dañada")){
								flagEntregado = insertaEvento(folio,6,6,fecha,hora);
								excepcion=6;								
							}else if(text.equalsIgnoreCase("Caducidad incorrecta")){
								flagEntregado = insertaEvento(folio,6,7,fecha,hora);
								excepcion=7;
							}else if(text.equalsIgnoreCase("No se cargo")){
								flagEntregado = insertaEvento(folio,6,8,fecha,hora);
								excepcion=8;
							}else if(text.equalsIgnoreCase("Documentos")){
								flagEntregado = insertaEvento(folio,6,9,fecha,hora);
								excepcion=9;
							}
							evento=6;
							
							if(flagEntregado){
								updateFolios(folio);
							}else{
								if (errorConexion)
									updateFoliosNE(folio);
							}
						}else{respuesta=null;}////// error pin no
					}else{respuesta=null;}////// error pin no
				}
			}catch(Exception e){
				Log.i(e.getMessage(),"ERROR");
			}
			return null;
		}
		
		
		
		
		// mensajes de  error y alerta y llamada a actualizacion de folios
		@Override
	    protected void onPostExecute(Void result) {
	        Log.i(TAG, "onPostExecute");
	    	btnSend.setEnabled(true);
	    	if (respuesta != null){
	    		if (respuesta.contains("-")){
	    			String[] str = respuesta.split("-");
	    			if(!respuesta.contains("ERROR-"))
	    				seleccionaEstado(Integer.parseInt(str[1]));
	    		}
	    		Toast.makeText(getApplicationContext(),  Globals.getMsj(getApplicationContext(),respuesta) , Toast.LENGTH_LONG).show();
	    	}
	    	

	    	if (r4.isChecked() || r5.isChecked()){
	    		
	    		if (!flagHayPIN){
	       			Toast.makeText(getApplicationContext(), getString(R.string.pin_vacio) , Toast.LENGTH_LONG).show();
	    		}

	    		if (flagHayPIN && !flagPinValido){
	    			Toast.makeText(getApplicationContext(), getString(R.string.pin_erroneo), Toast.LENGTH_LONG).show();
	    		}
	    		
	    		if(flagHayPIN && flagPinValido){
	    			if(flagEntregado || pendientesEnvio){
    					Intent i = new Intent(getApplicationContext(),Folios.class).addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
        				i.putExtra("idChofer", idChofer);
        				startActivity(i);
        				finish();
    				}
		    	}
	    	}
		}
		
        @Override
        protected void onPreExecute() {
        }
		
        @Override
        protected void onProgressUpdate(Void... values) {
            Log.i(TAG, "onProgressUpdate");
        }
	}
	
}
