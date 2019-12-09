package com.example.pruebas;



import android.app.Activity;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.telephony.TelephonyManager;

import org.json.JSONObject;





public class Globals extends Activity{

	//static String webService = "/EntregasWS/Service1.asmx?wsdl";
	//static String webService = "/WebServiceGoMobile/Service1.asmx?wsdl"; // solutia
	static String webService = "/Service1.asmx?wsdl"; // xero Juan
	//static String webService = "/ws/Service1.asmx?wsdl"; // local lap
	static String NAMESPACE = "http://tempuri.org/";

	
	static JSONObject jsonObject = new JSONObject();
	static Context ctx;


	
	
	public static String getImei(Context c){
		TelephonyManager telephonyManager = (TelephonyManager)c.getSystemService(Context.TELEPHONY_SERVICE);
	    String id = telephonyManager.getDeviceId().toString();
		return id;
	}

	
	
	
	// establece parametros de conexion
	public static String getUrlWebService(Context c){
		return "http://" + getConexion(c) + Globals.webService; 
	}
	// establece parametros de conexion
	private static String getConexion(Context c){
		SQLiteDatabase myDB = null;
		String TableName = "Parametros";
		myDB = c.openOrCreateDatabase("DBLogs", Context.MODE_PRIVATE, null);
		String cadena ="";
		Cursor cur = myDB.rawQuery("SELECT * FROM " + TableName , null);
		int Column1 = cur.getColumnIndex("IP");
		int Column2 = cur.getColumnIndex("Puerto");
		cur.moveToFirst();
		if (cur != null) {
		    do {
		    	String ip = cur.getString(Column1);
		    	String puerto = cur.getString(Column2);
		    	cadena = ip + ":" + puerto + "";
		    }while(cur.moveToNext());
		}
		return cadena;
	}
	
	
	
	
	public static String getMsj(Context c, String string) {
		
		final String[][] msjApp =
	    	{
	    		{"El numero de chofer no existe",c.getString(R.string.no_chofer_inexistente)},
	    		{"Error en el WS al loguearse",c.getString(R.string.error_ws_loguearse)},
	    		{"El IMEI no correponde a ese numero de chofer",c.getString(R.string.error_IMEI_incorrecto)},
	    		{"Error al Actualizar Ventas de Intelisis",c.getString(R.string.error_actualizando_cliente)},
	    		{"Error al llamar al SP",c.getString(R.string.error_llamando_sp)},
	    		{"Error",c.getString(R.string.error)},
	    		{"Error de conexion",c.getString(R.string.error_conexion)},
	    		{"Error de conexion Eventos",c.getString(R.string.error_conexion_eventos)},
	    		{"Guardado en App",c.getString(R.string.error_enviado_cache)},
				{"El PIN introducido no es valido","c.getString(R.string.error_PIN_invalido)"},
				{"Error Repuperar Folios","Error al Repuperar los Folios"},
	    		{"",""}
	    		
	    	};
		
		
		
		
		String[][] msjSP =
	    	{
		    	{"OK",c.getString(R.string.error_sp_ok)},
		    	{"DUPLICADO",c.getString(R.string.error_sp_duplicado)},
		    	{"no iniciado",c.getString(R.string.error_sp_noiniciado)},
		    	{"ya iniciado",c.getString(R.string.error_sp_yainiciado)},
		    	{"pausado",c.getString(R.string.error_sp_pausado)},
		    	{"ya arribado",c.getString(R.string.error_sp_arribado)},
		    	{"ya entregado",c.getString(R.string.error_sp_entregado)},
		    	{"ya excepciones",c.getString(R.string.error_sp_excepciones)},
		    	{"Guardado en App",""},
		    	{"no arribado",c.getString(R.string.error_sp_no_arribado)},
		    	{"",""}
	    	};
				
		
		
		
		String[] msjOrig = null; 
		msjOrig = string.split("-");
		String resultado="Mensaje desconocido";
		if (msjOrig.length==1){
			//for  (String[] item : msjApp) {
			for  (String[] item : msjApp) {
			    if (msjOrig[0].equals(item[0])){
	    			resultado= item[1];
	    			break;
	    		}
	    	}
		}else{
	    	for  (String[] item : msjSP) {
	    		if (msjOrig[2].contains(item[0])){
	    			resultado= item[1];
	    			break;
	    		}
	    	}		
		}
    	return resultado;
	}



	
	//crea tabla Parametros Para IP y puerto de comunicasion de nuestra aplicasion
	public static boolean setParametros(Context c){
		SQLiteDatabase myDB= null; 
		try{
			String TableName = "Parametros";
			myDB = c.openOrCreateDatabase("DBLogs", MODE_PRIVATE, null);
			
			
			//myDB.execSQL("DROP TABLE IF EXISTS " + TableName);
			
			
			myDB.execSQL("CREATE TABLE IF NOT EXISTS " + TableName + " (IP varchar(20), Puerto Integer);");
			String select = "SELECT * FROM " + TableName + "";
			Cursor cur = myDB.rawQuery(select, null);
			String dirIP="\""+"172.16.1.32"+"\"";
			if(cur.getCount()==0){
				String insert= "INSERT INTO "+ TableName+ " VALUES ("+dirIP+",80);";
				myDB.execSQL(insert);
			}
		}catch(Exception e){
		}
		myDB.close();
		return true;
	}
	
	
	
	
	
	// creacion y carga de tablas Folios y FoliosPendientes
	public static boolean setFolios(Context c){
		SQLiteDatabase myDB= null; 
		myDB = c.openOrCreateDatabase("DBLogs", MODE_PRIVATE, null);
		
		/*Tabla de Folios*/
		String TableName = "Folios";
		myDB.execSQL("DROP TABLE IF EXISTS "+TableName);
		myDB.execSQL("CREATE TABLE IF NOT EXISTS "
		+ TableName
		+ " (IDLog Integer PRIMARY KEY, IDChofer varchar(32), IDRemision varchar(32),"
		+ " Incidencia varchar(32), Clave varchar(32), Date TIMESTAMP DEFAULT (datetime('now','localtime')),"
		+ " Enviado varchar(1) not null  );");

		myDB.close();
		
		return true;
	}
	
	
	// creacion y carga de tablas Folios y FoliosPendientes
	public static boolean setFoliosPendientes(Context c){
		SQLiteDatabase myDB= null; 
		myDB = c.openOrCreateDatabase("DBLogs", MODE_PRIVATE, null);

		/*Tabla de FoliosPendientes*/
		String TableName2 = "FoliosPendientes";
		//myDB.execSQL("DROP TABLE IF EXISTS "+TableName2);
		myDB.execSQL("CREATE TABLE IF NOT EXISTS "
		+ TableName2
		+ " (IDLog Integer PRIMARY KEY, folio varchar(32), idEvento varchar(32),"
		+ " idExcepcion varchar(32), fecha varchar(32),"
		+ " hora varchar(32), latitud varchar(32), longitud varchar(32));");
		myDB.close();
		
		return true;
	}
	
	
	
	
	
	// obtiene pendiente si hay 
	protected static boolean hayPendientes(Context c){
		SQLiteDatabase myDB= null;
		myDB = c.openOrCreateDatabase("DBLogs", MODE_PRIVATE, null);
		String select = "SELECT * FROM FoliosPendientes";
		Cursor cur = myDB.rawQuery(select, null);
		if (cur.getCount() > 0){
			return true;
		} else {
		  	return false;
		}
	}
	
	

	
	
	
	@SuppressWarnings("rawtypes")
	static Object resizeArray (Object oldArray, int newSize) {
		   int oldSize = java.lang.reflect.Array.getLength(oldArray);
		   Class elementType = oldArray.getClass().getComponentType();
		   Object newArray = java.lang.reflect.Array.newInstance(
		         elementType, newSize);
		   int preserveLength = Math.min(oldSize, newSize);
		   if (preserveLength > 0)
		      System.arraycopy(oldArray, 0, newArray, 0, preserveLength);
		   return newArray; }
}
