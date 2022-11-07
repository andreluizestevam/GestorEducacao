using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace C2BR.GestorEducacao.WebService.Models
{
    public class HistoricoNaoAtendimento
    {
        public DateTime dt_R { get; set; }
        public string dt
        {
            get
            {
                return this.dt_R.ToString("dd/MM/yy");
            }
        }

        public string diaSemana
        {
            get
            {
                return this.dt_R.ToString("dddd", new CultureInfo("pt-BR"));
            }
        }
        public string nomePaciente { get; set; }
        public string hr { get; set; }
        public string deAcao { get; set; }
    }
}