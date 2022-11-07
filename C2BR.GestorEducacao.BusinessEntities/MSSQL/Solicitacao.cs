using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public class Solicitacao
    {
        public int Codigo { get; set; }
        public bool Seleciona { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int Qtd { get; set; }
        public decimal Total { get { return Valor * Qtd; } }
    }
}
