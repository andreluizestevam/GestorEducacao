using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C2BR.GestorEducacao.WebService.Models.ModuloAgenda
{
    public class HistorAtendimentoMes : QuantitativosAtendimento
    {
        public string NO_APE_ALU { get; set; }
        public string pacienteSexo { get; set; }
        public DateTime? pacienteDataN { get; set; }
        public string idadeSaude
        {
            get
            {
                if (this.pacienteDataN.HasValue)
                {
                    try
                    {
                        int anos = DateTime.Now.Year - this.pacienteDataN.Value.Year;

                        //Descobre a idade
                        if (DateTime.Now.Month < this.pacienteDataN.Value.Month || (DateTime.Now.Month == this.pacienteDataN.Value.Month && DateTime.Now.Day < this.pacienteDataN.Value.Day))
                            anos--;

                        int idade = anos;

                        DateTime dtatu = DateTime.Now;
                        //Soma a idade à data de nascimento
                        int anoAux = this.pacienteDataN.Value.AddYears(anos).Year;

                        //Descobre a data do último aniversário
                        DateTime ultimoDtNiver = new DateTime(anoAux, this.pacienteDataN.Value.Month, this.pacienteDataN.Value.Day);
                        TimeSpan ts = dtatu.Subtract(ultimoDtNiver);

                        //Calcula meses desde o último aniversário
                        double qtMeses = (ts.Days / 30);

                        //Calcula dias desde o último dia de aniversário
                        int ano = (DateTime.Now.Month == 1 ? DateTime.Now.Year - 1 : DateTime.Now.Year); //Calcula para que, caso seja janeiro, seja contabilizado sobre o ano anterior, pois senão o calculo ficaria errado
                        DateTime ultimoMesDiaNiver = new DateTime(ano, DateTime.Now.AddMonths(-1).Month, this.pacienteDataN.Value.Day);
                        TimeSpan tsDas = dtatu.Subtract(ultimoMesDiaNiver);
                        double qtDias = tsDas.Days;

                        return anos + "a" + qtMeses + "m ";
                    }
                    catch (Exception e)
                    {
                        return " - ";
                    }
                }
                else
                {
                    return " - ";
                }
            }
        }
    }
}