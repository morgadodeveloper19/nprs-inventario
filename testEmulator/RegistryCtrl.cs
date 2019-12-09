using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegEditLinq
{
    public class RegistryCtrl
    {
        
        private static RegistryKey rootKey;

        public static string Launch98
        {                
            get {
                rootKey = Registry.LocalMachine.CreateSubKey(@"init");
                return rootKey.GetValue("Launch98", "").ToString();            
            }
        }
        public static string Name
        {                
            get {
                rootKey = Registry.LocalMachine.CreateSubKey(@"System\StorageManager\Profiles\VCEFSD");
                return rootKey.GetValue("Name", "").ToString();            
            }
        }
        public static string Display
        {                
            get {
                rootKey = Registry.LocalMachine.CreateSubKey(@"System\GDI\Drivers");
                return rootKey.GetValue("Display", "").ToString();            
            }
        }
        public static string DisplayDll
        {                
            get {
                rootKey = Registry.LocalMachine.CreateSubKey(@"Drivers\Display\S3C2410\CONFIG");
                return rootKey.GetValue("DisplayDll", "").ToString();            
            }
        }





    }
}
