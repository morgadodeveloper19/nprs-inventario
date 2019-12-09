using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService1
{
    public class cFolio
    {
        public string folio { get; set; }
        public string NIP { get; set; }
        public string chofer { get; set; }
        public string transporte { get; set; }


        public cFolio()
        {
            this.folio = "";
            this.NIP = "";
            this.chofer = "";
            this.transporte = "";
        }

        //public cFolio(string folio, string NIP, string chofer)
        //{
        //    this.folio = folio;
        //    this.NIP = NIP;
        //    this.chofer = chofer;
        //}

        public cFolio(string folio, string NIP)// string chofer, string transporte)
        {
            this.folio = folio;
            this.NIP = NIP;
            //this.chofer = chofer;
            //this.transporte = transporte;
        }

        public cFolio(string folio, string NIP, string chofer, string transporte)
        {
            this.folio = folio;
            this.NIP = NIP;
            this.chofer = chofer;
            this.transporte = transporte;
        }
    }
}