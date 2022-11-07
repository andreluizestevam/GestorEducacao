using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using C2BR.GestorEducacao.UI.Library.Auxiliares;

namespace C2BR.GestorEducacao.WebService.Models.ModuloPaciente
{
    public class HistoricoAtendimRealizadosPaci
    {
        public DateTime DT {get;set;}
        public string DT_V
        {
            get
            {
                return this.DT.ToString("dd/MM/yy");
            }
        }
        public string HR { get; set; }
        public TimeSpan hora
        {
            get
            {
                return TimeSpan.Parse((HR));
            }
        }
        public string nome { get; set; }
        public string coClass { get; set; }
        public string noClass
        {
            get
            {
                return AuxiliGeral.GetNomeClassificacaoFuncional(this.coClass);
            }
        }
        public string deAcao { get; set; }
        public string Colaborador { get; set; }
        public string Situacao { get; set; }
        public string agendaConfirm { get; set; }
        public string agendaEncamin { get; set; }
        public string faltaJustif { get; set; }
        public string Situacao_Valid
        {
            get
            {
                //Trata as situações possíveis
                if (this.Situacao == "A") //Se for agendado, pode estar confirmado, presente, ou encaminhado
                {
                    if (this.agendaEncamin == "S")
                        return "Encaminhado(a)";
                    else if (this.agendaConfirm == "S")
                        return "Presente";
                    else
                        return "Agendado";
                }
                else if (this.Situacao == "C") //Se for falta, pode ter sido justificada ou não
                {
                    if (this.faltaJustif == "S")
                        return "Falta Justificada";
                    else
                        return "Falta não Justificada";
                }
                else if (this.Situacao == "R")
                    return "Atendido";
                else
                    return " - ";
            }
        }
    }
}