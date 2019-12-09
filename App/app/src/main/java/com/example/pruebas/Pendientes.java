package com.example.pruebas;

import org.ksoap2.SoapEnvelope;
import org.ksoap2.serialization.SoapObject;
import org.ksoap2.serialization.SoapPrimitive;
import org.ksoap2.serialization.SoapSerializationEnvelope;
import org.ksoap2.transport.HttpTransportSE;

import android.os.AsyncTask;
import android.os.Bundle;
import android.app.Activity;
import android.content.pm.ActivityInfo;
import android.view.Menu;

public class Pendientes extends Activity {
	final String NAMESPACE = "http://tempuri.org/";
	final String METHOD_NAME = "logearse";
	final String SOAP_ACTION = "http://tempuri.org/logearse";
	
	String mensajeError;
	boolean accesoAutorizado;
	String idChofer;
	String imei;
	String respuesta;
	
	
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_pendientes);
		setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_PORTRAIT);


		AsyncCall ac = new AsyncCall();
		ac.execute();
		
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.menu_secundario, menu);
		return true;
	}
	
	
	
	// procedimentos de peticion asincrona de datos
	private class AsyncCall extends AsyncTask<Void, Void, Void>{
		@Override
		protected Void doInBackground(Void... params) {
			
			
			final String urlWebService="http://172.16.1.32/WebServiceGoManager/Service1.asmx"; 
			try{
				SoapObject request = new SoapObject(NAMESPACE, METHOD_NAME);
				request.addProperty("numChofer","AGAD-001");
				request.addProperty("imei","352279050860836");
				
				SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(SoapEnvelope.VER11);
				envelope.dotNet = true;

				envelope.setOutputSoapObject(request);
				HttpTransportSE transporte = new HttpTransportSE(urlWebService,2000);
				transporte.call(SOAP_ACTION, envelope);
				
				SoapPrimitive resultado_xml =(SoapPrimitive) envelope.getResponse();
				respuesta = resultado_xml.toString();
				if(respuesta.equals("1"));
				}catch (Exception e) {
					respuesta = "Error de conexion";
				}
			
			
			return null;
		}
    }
}
