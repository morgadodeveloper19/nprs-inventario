using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;

namespace WebServiceGoMobile
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

        public cFolio(string folio, string NIP)
        {
            this.folio = folio;
            this.NIP = NIP;
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