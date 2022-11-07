using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C2BR.GestorEducacao.WebService.Models.ModuloFinanceiro
{
    public class ResumoFinanceiro
    {
        public DateTime data { get; set; }
        public string data_V
        {
            get
            {
                return this.data.ToString("dd/MM/yy");
            }
        }

        public int qtPart { get; set; }
        public string vlPart { get; set; }

        public int qtPlano { get; set; }
        public string vlPlano { get; set; }

        public int qtDia
        {
            get
            {
                return this.qtPart + this.qtPlano;
            }
        }
        public string vlDia
        {
            get
            {
                return ((!string.IsNullOrEmpty(this.vlPart) ? decimal.Parse(this.vlPart) : 0)
                    + (!string.IsNullOrEmpty(this.vlPlano) ? decimal.Parse(this.vlPlano) : 0)).ToString("N2");
            }
        }
    }
}