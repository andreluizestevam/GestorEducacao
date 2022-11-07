using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.WebService.Models.Geral
{
    public class TblAluno
    {

        public string Nome { get; set; }
        public string Sexo { get; set; }
        public DateTime? DataNasc { get; set; }
        public string Data_V
        {
            get
            {
                if (DataNasc == null)
                {
                    return "00/00/0000";
                }
                else
                {
                    DateTime dt = Convert.ToDateTime(this.DataNasc);
                    return dt.ToString("dd/MM/yyyy"); 
                }
                
            }
        }
        public int Deficiencia { get; set; }
        public string TelefoneCelular { get; set; }
        public string FixoCelular { get; set; }
        public string Email { get; set; }
        public string NomeMae { get; set; }
        public string NomePai { get; set; }
        public string ResponsavelPai { get; set; }
        public string ResponsavelMae { get; set; }

    }
}