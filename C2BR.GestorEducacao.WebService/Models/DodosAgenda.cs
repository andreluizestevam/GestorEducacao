using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.WebService.Models
{
    public class DodosAgenda
    {

        public string CoClassProf { get; set; }
        public int coCol { get; set; }
        public string Profissional { get; set; }
        public int IdAgenda { get; set; }
        public string AgendaHora { get; set; }
        public string Paciente { get; set; }
        public string Operadora { get; set; }
        public string Situacao { get; set; }
        public string agendaConfirm { get; set; }
        public string agendaEncamin { get; set; }
        public string faltaJustif { get; set; }
        public string Unidade { get; set; }
        public string UnidadeNome { get; set; }
        public string UnidBairro { get; set; }
        private string UnidTelefone_;
        public string UnidTelefone
        {
            get
            {
                return AuxiliFormatoExibicao.PreparaTelefone(UnidTelefone_);
            }
            set
            {
                UnidTelefone_ = value;
            }
        }
        public string Situacao_Valid
        {
            get
            {
                //retorna "-" se estiver em aberto e sem paciente associado à agenda
                if (this.Paciente != "*** LIVRE")
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
                else
                    return " - ";
            }
        }
        public string Classificacao
        {
            get
            {
                return AuxiliGeral.GetNomeClassificacaoFuncional(CoClassProf, true);
            }
        }
        public int? Especialidade_ { get; set; }
        public string Especialidade
        {
            get
            {
                if (!Especialidade_.HasValue)
                    return "-";

                var res = TB63_ESPECIALIDADE.RetornaTodosRegistros().Where(e => e.CO_ESPECIALIDADE == this.Especialidade_).FirstOrDefault();
                
                return res != null ? res.NO_ESPECIALIDADE : "-";
            }
        }
        public DateTime Data { get; set; }
        public TimeSpan hora
        {
            get
            {
                return TimeSpan.Parse((AgendaHora));
            }
        }
        public string Data_V
        {
            get
            { return this.Data.ToString("dd/MM/yy"); }
        }
        public string diaSemana
        {
            get
            {
                return this.Data.ToString("dddd", new CultureInfo("pt-BR"));
            }
        }
        public string Agendas
        {
            get
            {
                return (string.Format("{0} - {1} - {2}", this.Data.ToString("dd/MM/yyyy"),this.hora , this.diaSemana));
            }
        }
        public string hrPrim
        {
            get
            {
                return this.AgendaHora;
            }
        }
        public string hrUltim
        {
            get
            {
                var res = TBS174_AGEND_HORAR.RetornaTodosRegistros().Where(a =>
                            a.CO_COL == this.coCol
                            && a.DT_AGEND_HORAR == this.Data
                            && (a.CO_ALU == null || a.CO_SITUA_AGEND_HORAR == "C"))
                            .OrderByDescending(a => a.HR_AGEND_HORAR).FirstOrDefault();

                return res != null ? res.HR_AGEND_HORAR : "-";
            }
        }
    }
}