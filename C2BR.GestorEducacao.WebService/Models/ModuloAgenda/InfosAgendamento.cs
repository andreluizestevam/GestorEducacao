using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.WebService.Models.ModuloAgenda
{
    public class InfosAgendamento
    {
        public DateTime dt { get; set; }
        public string hr { get; set; }
        public string plano { get; set; }
        public string Agendas
        {
            get
            {
                return (string.Format("{0} - {1} - {2}", this.dt.ToString("dd/MM/yyyy"), this.hr, this.plano));
            }
        }

        public int idAgenda { get; set; }
        public int? idPlanej { get; set; }
        public string Queixa
        {
            get
            {
                //Se houver planejamento
                if (this.idPlanej.HasValue)
                {
                    //Se houver recepção para o planejamento
                    var Planejamento = TBS370_PLANE_AVALI.RetornaPelaChavePrimaria(this.idPlanej.Value);
                    Planejamento.TBS367_RECEP_SOLICReference.Load();
                    if (Planejamento.TBS367_RECEP_SOLIC != null)
                    {
                        Planejamento.TBS367_RECEP_SOLIC.TBS372_AGEND_AVALIReference.Load();
                        if (Planejamento.TBS367_RECEP_SOLIC.TBS372_AGEND_AVALI != null)
                        {
                             return Planejamento.TBS367_RECEP_SOLIC.TBS372_AGEND_AVALI.DE_OBSER_NECES;
                        }
                        else
                            return " - ";
                    }
                    else
                        return " - ";
                }
                else
                    return " - ";
            }
        }
        public string coTipoConsulta { get; set; }
        public string tipoConsulta
        {
            get
            {
                switch (this.coTipoConsulta)
                {
                    case "E":
                        return "Encaixe";
                    case "N":
                        return "Normal";
                    case "R":
                        return "Retorno";
                    default:
                        return " - ";
                }
            }
        }

        public string colabNome { get; set; }
        public string funcCol { get; set; }
        public string colabClassFunci
        {
            get
            {
                string nomeCol = "";

                //Tratamento para coletar os três primeiros nomes do colaborador
                #region Tratamento

                if (!string.IsNullOrEmpty(this.colabNome))
                {
                    var nome = colabNome.Split(' ');
                    string nome1 = nome[0];
                    string nome2 = "";
                    string nome3 = "";

                    //Segundo nome
                    try
                    {
                        nome2 = nome[1];
                    }
                    catch (Exception e)
                    {
                    }

                    //Terceiro nome
                    try
                    {
                        nome3 = nome[2];
                    }
                    catch (Exception e)
                    {

                    }

                    nomeCol = nome1 + " " + nome2 + " " + nome3;
                }
                #endregion

                if (!string.IsNullOrEmpty(this.funcCol))
                {
                    //return classFunc + " - " + nomeCol.ToUpper();
                    return (this.funcCol.Length > 14 ? this.funcCol.Substring(0, 14) : this.funcCol) + " - " + nomeCol.ToUpper();
                }
                else
                    return " - ";
            }
        }
    }
}