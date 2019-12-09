package com.example.pruebas;

import android.app.Activity;
import android.content.Intent;
import android.content.pm.ActivityInfo;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.Toast;

import org.ksoap2.SoapEnvelope;
import org.ksoap2.serialization.SoapObject;
import org.ksoap2.serialization.SoapPrimitive;
import org.ksoap2.serialization.SoapSerializationEnvelope;
import org.ksoap2.transport.HttpTransportSE;

public class Principal extends Activity {
	//variables del webservice
	private String TAG ="";
	private Button btnEnviar;
	private EditText txtId;
	private CheckBox cbActualizar;
	final String METHOD_NAME = "login";
	final String SOAP_ACTION = Globals.NAMESPACE + METHOD_NAME + "";
	String mensajeError;
	boolean accesoAutorizado;
	String idChofer;
	String imei;
	String respuesta;
	
	


	
	// adignacion inicial y click boton enviar al crear clase
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
					
			
		setContentView(R.layout.activity_principal);
		setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_PORTRAIT);
		cbActualizar = (CheckBox)findViewById(R.id.cbActualizar);
		txtId = (EditText)findViewById(R.id.txtId);
		btnEnviar = (Button)findViewById(R.id.btnEnviar);	
		
		
		Globals.setParametros(getApplicationContext());
		Globals.setFoliosPendientes(getApplicationContext());
		


		
		// encvia pendiente si hay
        /*
		if (Globals.hayPendientes(getApplicationContext())){
			Globals.ctx=getApplicationContext();
			runOnUiThread(new Runnable() {
				@Override
				public void run() {
					EnviaPendientes ac = new EnviaPendientes();
					ac.execute();
				}
			});
		}		
		/* */
		
		
		
		
				
		btnEnviar.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				btnEnviar.setEnabled(false);
				String id = txtId.getText().toString();
				if(id.length()==0){
					Toast.makeText(getApplicationContext(), getString(R.string.ingrese_nochofer), Toast.LENGTH_SHORT).show();
				}else{
					AsyncCall ac = new AsyncCall();
					ac.execute();
				}
			}
		});
	}
	  
	// agrega el menu
	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		getMenuInflater().inflate(R.menu.menu_principal, menu);
		return true;
	}

	//agrega eventos al menu
	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		switch (item.getItemId()) {
	        case R.id.action_settings:
				Intent i = new Intent(getApplicationContext(), Parametros.class).addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
				startActivity(i);	
				finish();
	            return true;
	        case R.id.action_exit:
	        	android.os.Process.killProcess(android.os.Process.myPid());
	        	return true;
	        default:
	            return super.onOptionsItemSelected(item);
		}
	}
	
	
	// revisa login en el WebService
	public boolean Login(String idChofer, String imei){

		boolean resultado=false; 
		try{
			SoapObject request = new SoapObject(Globals.NAMESPACE, METHOD_NAME);
			SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(SoapEnvelope.VER11);
			envelope.dotNet = true;
			request.addProperty("numChofer",idChofer);
			request.addProperty("imei",imei);
			envelope.setOutputSoapObject(request);
			HttpTransportSE transporte = new HttpTransportSE(Globals.getUrlWebService(getApplicationContext()),2000);
			transporte.call(SOAP_ACTION, envelope);
			SoapPrimitive resultado_xml =(SoapPrimitive) envelope.getResponse();
			respuesta = resultado_xml.toString();
			if(respuesta.equals("1"))
				resultado = true;
			}catch (Exception e) {
                Log.e("Error LogIn", e.toString());
				respuesta = "Error de conexion";
				resultado=false;
			}
			return resultado;
		}

	// procedimentos de peticion asincrona de datos
	private class AsyncCall extends AsyncTask<Void, Void, Void>{
		@Override
		protected Void doInBackground(Void... params) {
			try{
				Log.i(TAG,"doInBackground");
				if(Login(idChofer,imei)){
					accesoAutorizado=true;
				}else{
					accesoAutorizado=false;
				}
			}catch(Exception e){
				e.printStackTrace();
			}
			return null;
		}
		
		// Escoge que formulario cargar
		@Override
        protected void onPostExecute(Void result) {
            Log.i(TAG, "onPostExecute");
            if(accesoAutorizado){
            	Intent i;
            	if(cbActualizar.isChecked()){
            		i = new Intent(getApplicationContext(),CargaFolios.class).addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
            	}else{
            		i = new Intent(getApplicationContext(),Folios.class).addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
            	}
            	i.putExtra("idChofer", idChofer);
				startActivity(i);
            	finish();
            }else{
            	Toast.makeText(getApplicationContext(), Globals.getMsj(getApplicationContext(),respuesta), Toast.LENGTH_LONG).show();
            }
            btnEnviar.setEnabled(true);
        }

        @Override
        protected void onPreExecute() {
            Log.i(TAG, "onPreExecute");
            idChofer = txtId.getText().toString().trim();
            try{
                imei = Globals.getImei(getApplicationContext());
            }catch (Exception e){
                // ZMB prueba 280416
                imei="868585021905964";
                // ZMB prueba 280416
            }
        }
    }
}
