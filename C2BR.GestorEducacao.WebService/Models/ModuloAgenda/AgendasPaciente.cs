using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.WebService.Models.ModuloAgenda
{
    public class AgendasPaciente
    {
        public int Numero { get; set; }
        public string Item { get; set; }
        public int idAgenda { get; set; }
        public DateTime dt { get; set; }
        public string hr { get; set; }
        public string plano { get; set; }
        public TimeSpan hora
        {
            get
            {
                if (hr == null)
                {
                    return TimeSpan.Parse((DateTime.Now.Hour.ToString()));
                }
                else
                {
                    return TimeSpan.Parse((hr));
                } 
            }
        }
        public string CoClassProf { get; set; }
        public string Profissional { get; set; }
        public string Unidade { get; set; }
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
        public string Agendas
        {
            get
            {
                if (this.Numero == 1)
                //if (string.IsNullOrEmpty(this.Item))
                {
                    return (string.Format("{0} - {1} - {2}", this.dt.ToString("dd/MM/yyyy"), this.hr, this.plano));
                }
                else
                {
                    return (Item);
                }
            }
        }
        public string AgendaDetalhada
        {
            get
            {
                if (this.Numero == 1)
                {
                    return (string.Format("{0} - {1} - {2} - {3}", this.dt.ToString("dd/MM/yy"), this.hr, this.Profissional, this.Especialidade));
                }
                else
                {
                    return (Item);
                }
            }
        }
    }
}