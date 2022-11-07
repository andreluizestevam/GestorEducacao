using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C2BR.GestorEducacao.WebService.Models
{
    public class QuadroAgendaSemanal
    {
        public List<DiaDaSemana> Dia { get; set; }
        public string Hora { get; set; }
        public string Data { get; set; }
        public string NomePaciente { get; set; }
    }
    public class DiaDaSemana
    {
        public string DiaSemana { get; set; }
    }
}