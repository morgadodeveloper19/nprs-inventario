package com.example.pruebas;


import android.app.Activity;
import android.content.Intent;
import android.content.pm.ActivityInfo;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.KeyEvent;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.Toast;

import org.ksoap2.SoapEnvelope;
import org.ksoap2.serialization.SoapObject;
import org.ksoap2.serialization.SoapPrimitive;
import org.ksoap2.serialization.SoapSerializationEnvelope;
import org.ksoap2.transport.HttpTransportSE;

import java.util.ArrayList;

public class Folios extends Activity {

	private ListView lstFolios;
	public static String folio;
	public static String idChofer;
	private String TAG ="";
	private int estadoFolio;
	
	
	
	final String METHOD_NAME = "getEstado";
	final String SOAP_ACTION = "http://tempuri.org/getEstado";
	String res = "";
	
	
	// al crear la clase ????
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_folios);
		setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_PORTRAIT);
		lstFolios= (ListView)findViewById(R.id.lstFolios);
		Intent i = getIntent();
		idChofer= i.getStringExtra("idChofer");
        fillFolios();
		
		lstFolios.setOnItemClickListener(new OnItemClickListener() {
        	public void onItemClick(AdapterView<?> arg0, View v, int position, long ident){
        		folio = (String) lstFolios.getItemAtPosition(position);
    			runOnUiThread(new Runnable() {
    			    public void run() {
    			        AsyncCall ac = new AsyncCall();
    					ac.execute();
    			    }
    			});
        	}
		});
	}
	
	
	//agrega los folios a el layout
	protected void fillFolios(){
		try{
			SQLiteDatabase myDB= null;
			myDB = this.openOrCreateDatabase("DBLogs", MODE_PRIVATE, null);
			//Cursor c = myDB.rawQuery("SELECT * FROM Folios WHERE IDChofer = '" + idChofer + "' AND (Enviado = 0 OR Enviado = 1)", null);
			Cursor c = myDB.rawQuery("SELECT * FROM Folios WHERE IDChofer = '" + idChofer + "' AND (Enviado = 0)", null);
			int nColumn = c.getColumnIndex("IDRemision");
			c.moveToFirst();
			if (c.getCount() > 0) {
				ArrayList<String> sFolios = new ArrayList<String>();
				do {
					sFolios.add(c.getString(nColumn));
				}while(c.moveToNext());
				ArrayAdapter<String> adaptador = new ArrayAdapter<String>(Folios.this,android.R.layout.simple_list_item_1, sFolios);
				lstFolios.setAdapter(adaptador);
			}
			myDB.close();
			Log.i(TAG, "onPostExecute");
		}catch(Exception ex){
            Toast.makeText(getApplicationContext(), Globals.getMsj(getApplicationContext(), "Error Repuperar Folios"), Toast.LENGTH_LONG).show();
			Log.i("onPostExecute",ex.toString());
		}
	}
	
	// al seleccionar un elemento
	@Override
	public boolean onKeyDown(int keyCode, KeyEvent event)
	{
	    if ((keyCode == KeyEvent.KEYCODE_BACK))
	    {
	    	finish();
	    }
	    return super.onKeyDown(keyCode, event);
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
	

	// Recupera el estado para poner en este folio
	private class AsyncCall extends AsyncTask<Void, Void, Void>{
		@Override
		protected Void doInBackground(Void... params) {
			try{
				SoapObject request = new SoapObject(Globals.NAMESPACE, METHOD_NAME);
				request.addProperty("folio",folio);
				SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(SoapEnvelope.VER11);
				envelope.dotNet = true;
				envelope.setOutputSoapObject(request);
				HttpTransportSE transporte = new HttpTransportSE(Globals.getUrlWebService(getApplicationContext()),1000);
				transporte.call(SOAP_ACTION, envelope);
				SoapPrimitive resultado_xml =(SoapPrimitive) envelope.getResponse();
				estadoFolio = Integer.parseInt(resultado_xml.toString());
			}catch (Exception e) {
				Log.e("Error : " , "Error on soapPrimitiveData() " + e.getMessage());
				e.printStackTrace();
			}
			return null;
		}
		
		@Override
	    protected void onPostExecute(Void result) {	    	
    		Intent i = new Intent(getApplicationContext(),Eventos.class);
			i.putExtra("folio", folio);
			i.putExtra("idChofer", idChofer);
			i.putExtra("estado",estadoFolio);
			startActivity(i);
			finish();				
		}
		
        @Override
        protected void onPreExecute() {

        }
		
        @Override
        protected void onProgressUpdate(Void... values) {
        }
	}
}
