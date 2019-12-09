package com.example.pruebas;


import org.ksoap2.SoapEnvelope;
import org.ksoap2.serialization.SoapObject;
import org.ksoap2.serialization.SoapSerializationEnvelope;
import org.ksoap2.transport.HttpTransportSE;

import android.os.AsyncTask;
import android.os.Bundle;
import android.app.Activity;
import android.content.Intent;
import android.database.sqlite.SQLiteDatabase;
import android.util.Log;
import android.widget.Toast;

public class CargaFolios extends Activity {
	final String METHOD_NAME = "getFolios";
	final String SOAP_ACTION = Globals.NAMESPACE + METHOD_NAME + "";
	boolean flag = false, actualizar;
	public static String idChofer;
	private String TAG ="";
	
	
	
	// carga de la clase
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_carga_folios);
		
		
		

		

		
		
		runOnUiThread(new Runnable() {
			@Override
			public void run() {
				AsyncCall ac = new AsyncCall();
				ac.execute();
			}
		});
		
		
	}

	
	// inserta cada folio en bd del dispositivo
	protected void insertFolios(String remision, String clave){
		SQLiteDatabase myDB = null;
		myDB = this.openOrCreateDatabase("DBLogs", MODE_PRIVATE, null);
		String TableName = "Folios";
		myDB.execSQL("INSERT INTO "
		+ TableName
		+ " (IDChofer, IDRemision, Incidencia,Clave, Enviado)"
		+ " VALUES ('" + idChofer + "', '"+remision+"',0,'"+clave+"',0);");
		myDB.close();
	}
	
	// obtiene los folios del usuario logueado en el webservice y los pasa a el dispositivo
	public boolean getFolios(String id){
		boolean resultado = false;
		try
		{
			SoapObject request = new SoapObject(Globals.NAMESPACE, METHOD_NAME);
			request.addProperty("numChofer", id);
			SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(SoapEnvelope.VER11);
			envelope.dotNet = true;
			envelope.setOutputSoapObject(request);
			HttpTransportSE transporte = new HttpTransportSE(Globals.getUrlWebService(getApplicationContext()),1000);

			transporte.call(SOAP_ACTION, envelope);


			SoapObject resSoap =(SoapObject)envelope.getResponse();


            Globals.setFolios(getApplicationContext());
            if (resSoap == null) return false;


            cFolios[] listaFolios = new cFolios[resSoap.getPropertyCount()];
			for (int i = 0; i < listaFolios.length; i++){
				SoapObject ic = (SoapObject)resSoap.getProperty(i);
				cFolios fol = new cFolios();
				fol.folio = ic.getProperty(0).toString();
				fol.nip = ic.getProperty(1).toString();
				listaFolios[i]=fol;	
			}

			for (int i = 0; i < listaFolios.length; i++){
				SoapObject ic = (SoapObject)resSoap.getProperty(i);
				cFolios fol = new cFolios();
				fol.folio = ic.getProperty(0).toString();
				fol.nip = ic.getProperty(1).toString();	
				insertFolios(fol.folio, fol.nip);
			}

			resultado=true;
		}catch (Exception e){

        	
			resultado=false;
			Toast.makeText(getApplicationContext(), Globals.getMsj(getApplicationContext(),"Error de conexion"), Toast.LENGTH_LONG).show();
		}		

		return resultado;
	}
	
	

	
	
	
	private class AsyncCall extends AsyncTask<Void,Void,Void>{
		
		// obten folios asincrono
        @Override
        protected Void doInBackground(Void... params) {
        	try{
        		Log.i(TAG, "doInBackground");
        		getFolios(idChofer);
        	}catch(Exception e){
        		e.printStackTrace();
        	}
        	return null;
        }

        // abre Catalogo despues de cargar folios
        @Override
        protected void onPostExecute(Void result) {
            Log.i(TAG, "onPostExecute");
            
            Intent i = new Intent(getApplicationContext(),Folios.class).addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
            i.putExtra("idChofer", idChofer);
			startActivity(i);
        	finish();
        	/* */
        }

        // carga de parametros del layout anterior
        @Override
        protected void onPreExecute() {	
            Log.i(TAG, "onPreExecute");
            Intent i = getIntent();
            idChofer = i.getStringExtra("idChofer");
            
            
            
            
    		// encvia pendiente si hay
            
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

  
        }

        
        @Override
        protected void onProgressUpdate(Void... values) {
            Log.i(TAG, "onProgressUpdate");
        }		
	}
	
	


}
