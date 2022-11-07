using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C2BR.GestorEducacao.WebService.Models
{
    public class HorariosLivresPartir
    {
        public string Data { get; set; }
        public List<Hora> Hora { get; set; }
       
    }
    public class Hora
    {
        public string HoraRecebe { get; set; }
    }

}