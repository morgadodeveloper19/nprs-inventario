package com.example.pruebas;

import android.os.Bundle;
import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.telephony.TelephonyManager;
import android.view.KeyEvent;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.TextView;

public class Select extends Activity {

    String c1;
    String c2;
    String c3;
    String c4;
    String c5;
    String c6;
    String c7;
    
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_select);
		
		SQLiteDatabase myDB = null;
		myDB = this.openOrCreateDatabase("DBLogs", MODE_PRIVATE, null);
		String data="";	


		//Cursor c = myDB.rawQuery("SELECT * FROM Folios WHERE IDChofer = 'AGAD-001' AND Enviado = 2", null);
		Cursor c = myDB.rawQuery("SELECT * FROM Folios" , null);
		//Cursor c = myDB.rawQuery("SELECT * FROM Folios WHERE Clave = ''" , null); 
		// Check if our result was valid.
		c.moveToFirst();
		if (c != null) {
			// Loop through all Results
		    do {
			     c1 = c.getString(0);	// IDLog
			     c2 = c.getString(1);	// nip
			     c3 = c.getString(2);	// IDRemision
			     c4 = c.getString(3);	// Incidencia
			     c5 = c.getString(4);	// Clave
			     c6 = c.getString(5);	// Date
			     c7 = c.getString(6);	// Enviado
			     data = data 
			     +c1+"/"
			     +c2+"/"
			     +c3+"/"
			     +c4+"/"
			     +c5+"/"
			     +c6+"/"
			     +c7+"/"+"\n";
		    }while(c.moveToNext());
	   }
	   TextView tv = new TextView(this);
	   tv.setText(data);
	   setContentView(tv);
	}
	
	public void selectDB(){
		  
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.menu_secundario, menu);
	   return true;
	}

	
	public boolean onKeyDown(int keyCode, KeyEvent event)
	{
	    if ((keyCode == KeyEvent.KEYCODE_BACK))
	    {
	    	TelephonyManager telephonyManager = (TelephonyManager)getSystemService(Context.TELEPHONY_SERVICE);
	        String id = telephonyManager.getDeviceId().toString();
    		Intent i = new Intent(getApplicationContext(),Folios.class).addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
			i.putExtra("keyId", c2);
			i.putExtra("keyImei", id);
			startActivity(i);
			finish();
	    }
	    return super.onKeyDown(keyCode, event);
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
				i2.putExtra("idChofer", "AGAD-001");
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
		
}
