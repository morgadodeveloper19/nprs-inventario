package com.example.pruebas;

import java.util.Hashtable;

import org.ksoap2.serialization.KvmSerializable;
import org.ksoap2.serialization.PropertyInfo;

public class cFolios implements KvmSerializable {
	
	public String folio;
	public String nip;

	public cFolios(){
		folio="";
		nip="";
	}
	
	public cFolios(String folio, String nip){
		this.folio = folio;
		this.nip = nip;
	}
	
	@Override
	public Object getProperty(int arg0) {
		switch (arg0) {
		case 0:return folio;
		case 1:return nip;
		}
		return null;
	}
	
	@Override
	public int getPropertyCount() {
		return 2;
	}
	
	@Override
	public void getPropertyInfo(int ind, Hashtable ht, PropertyInfo info) {
		switch(ind){
		case 0:
			info.type = PropertyInfo.STRING_CLASS;
			info.name = "folio";
			break;
		case 1:
			info.type = PropertyInfo.STRING_CLASS;
			info.name = "nip";
			break;
		default:break;
		}
	}
    
	public void setProperty(int ind, Object val) {
		switch (ind) {
		case 0:folio = val.toString();break;
		case 1:nip = val.toString();break;
		default:break;
		}
	}
}
