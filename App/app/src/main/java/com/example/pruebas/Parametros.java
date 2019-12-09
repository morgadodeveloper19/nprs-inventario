package com.example.pruebas;

import android.os.Bundle;
import android.app.Activity;
import android.content.Intent;
import android.content.pm.ActivityInfo;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

public class Parametros extends Activity {

	public EditText txtIP;
	public EditText txtPuerto;
	public Button btnGuardar;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(com.example.pruebas.R.layout.activity_config);
		setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_PORTRAIT);
		txtIP = (EditText)findViewById(com.example.pruebas.R.id.txtIP);
		txtPuerto = (EditText)findViewById(com.example.pruebas.R.id.txtPuerto);
		btnGuardar = (Button)findViewById(com.example.pruebas.R.id.btnGuardar);
		getParametros();

		btnGuardar.setOnClickListener( new OnClickListener() {
			@Override
			public void onClick(View v) {
				String protocolo = "\""+txtIP.getText().toString()+"\"";
				String puerto = txtPuerto.getText().toString();
				if(setParametros(protocolo, puerto)){
					Intent i = new Intent(getApplicationContext(), Principal.class).addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
					startActivity(i);
					finish();
				}else{
					Toast.makeText(getApplicationContext(), getString(R.string.error_parametros), Toast.LENGTH_SHORT).show();
				}
			}
		});
	}
	
	public void getParametros(){
		SQLiteDatabase myDB= null;
		  String TableName = "Parametros";
		  myDB = this.openOrCreateDatabase("DBLogs", MODE_PRIVATE, null);
		   Cursor c = myDB.rawQuery("SELECT * FROM " + TableName , null);
		   int Column1 = c.getColumnIndex("IP");
		   int Column2 = c.getColumnIndex("Puerto");
		   // Check if our result was valid.
		   c.moveToFirst();
		   if (c != null) {
		    // Loop through all Results
		    do {
		     String Name = c.getString(Column1);
		     String Age = c.getString(Column2);
		     txtIP.setText(Name);
		     txtPuerto.setText(Age);
		     }while(c.moveToNext());
		   }
	}
	
	public boolean setParametros(String IP, String Puerto){
		boolean result = false;
		SQLiteDatabase myDB= null;
		String TableName = "Parametros";
		try{
			myDB = this.openOrCreateDatabase("DBLogs", MODE_PRIVATE, null);
			myDB.execSQL("UPDATE "
			     + TableName
			     + " SET IP = " + IP + ", Puerto = " + Puerto + ";");
		result = true;
		}catch(Exception e){
		}
		return result;
	}

	// agrega menu
	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		getMenuInflater().inflate(com.example.pruebas.R.menu.menu_configuracion, menu);
		return true;
	}
	
	//agrega eventos al menu
	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		switch (item.getItemId()) {
	        case R.id.action_cancel:
				Intent i = new Intent(getApplicationContext(), Principal.class).addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
				startActivity(i);
				finish();
	            return true;
	        case R.id.action_exit:
	        	finish();
	        	return true;
	        default:
	            return super.onOptionsItemSelected(item);
		}
	}

}
