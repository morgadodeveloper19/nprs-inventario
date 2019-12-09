namespace WebService1
{
    public class cExcepcion
    {
        public string id { get; set; }
        public string excepcion { get; set; }

        public cExcepcion()
        {
            this.id="";
            this.excepcion = "";
        }

        public cExcepcion(string id, string excepcion)
        {
            this.id = id;
            this.excepcion = excepcion;
        }
    }
}