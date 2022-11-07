using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace C2BR.GestorEducacao.WebService.Models.ModuloColaborador
{
    public class MinhasFaltasMes
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
        public bool justific { get; set; }
        public string deJustific
        {
            get
            {
                return (this.justific ? "Justificado" : "Não justificado");
            }
        }
        public bool comAtestado { get; set; }
        public string deAtestado
        {
            get
            {
                return (this.comAtestado ? "Com atestado" : "Sem atestado");
            }
        }
    }
}