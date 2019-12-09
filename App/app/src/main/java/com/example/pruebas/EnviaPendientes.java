package com.example.pruebas;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;
import org.ksoap2.SoapEnvelope;
import org.ksoap2.serialization.SoapObject;
import org.ksoap2.serialization.SoapSerializationEnvelope;
import org.ksoap2.transport.HttpTransportSE;

import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.os.AsyncTask;

public class EnviaPendientes  extends AsyncTask<Void, Void, Void>{

	final String METHOD_NAME = "updatePendientes";
	final String SOAP_ACTION = Globals.NAMESPACE + METHOD_NAME + "";

	
	Boolean errorConexion;
	Boolean[] listaRespuestas;
	
	@Override
	protected Void doInBackground(Void... params) {
		
		try
		{
			SoapObject request = new SoapObject(Globals.NAMESPACE, METHOD_NAME);
			SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(SoapEnvelope.VER11);
			envelope.dotNet = true;
			request.addProperty("jsonarray", Globals.jsonObject.toString());
			envelope.setOutputSoapObject(request);
			HttpTransportSE transporte = new HttpTransportSE(Globals.getUrlWebService(Globals.ctx),1000);
			transporte.call(SOAP_ACTION, envelope);

			SoapObject resSoap =(SoapObject)envelope.getResponse();
			int cont = resSoap.getPropertyCount();
			listaRespuestas = new Boolean[cont];
			
	        for(int i=0;i<cont; ++i)
            {
	        	listaRespuestas[i] = Boolean.valueOf(resSoap.getProperty(i).toString());
            }
	        errorConexion=false;
		}catch (Exception e){
			errorConexion=true;
		}		

		return null;
	}
	
	@Override
    protected void onPostExecute(Void result) {	   	

		if(!errorConexion){
			
			//recupera respuesta de transacciones
			SQLiteDatabase myDB= null;
			myDB = Globals.ctx.openOrCreateDatabase("DBLogs", Context.MODE_PRIVATE, null);
			Cursor cur = myDB.rawQuery("SELECT * FROM FoliosPendientes", null);
			int Column1 = cur.getColumnIndex("folio");
			cur.moveToFirst();
			
			int nRes = 0;
			int nFol = 0;
			String[] listaFolios= new String[nFol];
			
			
			
	
			//crear arreglo de folios a borrar 
	        do {
	        	if(listaRespuestas[nRes++]){
	        		nFol ++ ;
	        		listaFolios = (String[])Globals.resizeArray(listaFolios, nFol);
	        		listaFolios[nFol-1] = cur.getString(Column1).toString();
	        	}
		    }while(cur.moveToNext());
	        
	        
	        
	        
	        // borrar folios encontrados
	        for (String folio : listaFolios){
				myDB.delete("FoliosPendientes", "folio = '"+folio+"'", null);
	        }
			myDB.close();
			
		}
	}
	
   @Override
   protected void onPreExecute() {
	   	
		SQLiteDatabase myDB= null;
		myDB = Globals.ctx.openOrCreateDatabase("DBLogs", Context.MODE_PRIVATE, null);
		Cursor cur = myDB.rawQuery("SELECT * FROM FoliosPendientes", null);
		int Column1 = cur.getColumnIndex("folio");
		int Column2 = cur.getColumnIndex("idEvento");
		int Column3 = cur.getColumnIndex("idExcepcion");
		int Column4 = cur.getColumnIndex("fecha");
		int Column5 = cur.getColumnIndex("hora");
		int Column6 = cur.getColumnIndex("latitud");
		int Column7 = cur.getColumnIndex("longitud");
		cur.moveToFirst();

		
		if (cur != null) {
	        JSONArray array = new JSONArray();
		    
	        do {
				JSONObject json = new JSONObject();
				try {
					json.put("folio", cur.getString(Column1));
					json.put("idEvento", cur.getString(Column2));
					json.put("idExcepcion", cur.getString(Column3));
					json.put("fecha", cur.getString(Column4));
					json.put("hora", cur.getString(Column5));
					json.put("latitud", cur.getString(Column6));
					json.put("longitud", cur.getString(Column7));
			        array.put(json);
				} catch (JSONException e) {
				}
		    }while(cur.moveToNext());
		    
	        try {
				Globals.jsonObject.put("Entrega",array);
			} catch (JSONException e) {
			}
	        
		}
		myDB.close();

   }
	
   @Override
   protected void onProgressUpdate(Void... values) {
   }

}
