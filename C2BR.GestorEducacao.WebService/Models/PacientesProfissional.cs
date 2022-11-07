using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;

namespace C2BR.GestorEducacao.WebService.Models
{
    public class PacientesProfissional
    {
        public int coAlu { get; set; }
        public string pacienteNome { get; set; }
        public string pacienteSexo { get; set; }
        public DateTime? pacienteDataN { get; set; }
        public string pacienteIdade
        {
            get
            {
                string idade = " - ";

                //Calcula a idade do Paciente de acordo com a data de nascimento do mesmo.
                if (this.pacienteDataN.HasValue)
                {
                    int anos = DateTime.Now.Year - this.pacienteDataN.Value.Year;

                    if (DateTime.Now.Month < this.pacienteDataN.Value.Month || (DateTime.Now.Month == this.pacienteDataN.Value.Month && DateTime.Now.Day < this.pacienteDataN.Value.Day))
                        anos--;

                    idade = anos.ToString("00");
                }
                return idade;
            }
        }
        public string pacienteDataN_V
        {
            get
            {
                return (this.pacienteDataN.HasValue ? this.pacienteDataN.Value.ToString("MM/yyyy") : " - ");
            }
        }
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

                        return anos + "a " + qtMeses + "m ";
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

        //Insumo para tratar o nome do responsável dinamicamente
        public string NO_RESP { get; set; }
        public string FL_PAI_RESP { get; set; }
        public string FL_MAE_RESP { get; set; }
        public string NO_PAI { get; set; }
        public string NO_MAE { get; set; }
        public string NO_RESP_DINAMICO
        {
            get
            {
                string NomePaiConcat = "";
                #region Nome do Pai

                if (!string.IsNullOrEmpty(NO_PAI))
                {
                    var nomePai = NO_PAI.Split(' ');
                    string nomePai1 = nomePai[0];
                    NomePaiConcat = nomePai1;
                }

                #endregion

                string NomeMaeConcat = "";
                #region Nome da Mãe

                if (!string.IsNullOrEmpty(NO_MAE))
                {
                    var nomeMae = NO_MAE.Split(' ');
                    string nomeMae1 = nomeMae[0];
                    NomeMaeConcat = nomeMae1;
                }

                #endregion

                string noresp = "";
                #region Nome do Responsável

                if (!string.IsNullOrEmpty(NO_RESP))
                {
                    var nore = NO_RESP.Split(' ');
                    string nore1 = nore[0];
                    noresp = nore1;
                }

                #endregion

                if (FL_MAE_RESP == "S") //Se for só mãe
                    return NomeMaeConcat;
                else if (FL_PAI_RESP == "S") //Se for só pai
                    return NomePaiConcat;
                else
                    return (!string.IsNullOrEmpty(noresp) ? noresp : " - "); //Se não for nenhum dos dois, retorna o nome do responsável associado
            }
        }
        public string NO_RESP_DINAMICO_V
        {
            get
            {
                if (!string.IsNullOrEmpty(this.NO_RESP_DINAMICO))
                    return (this.NO_RESP_DINAMICO.Length > 40 ? this.NO_RESP_DINAMICO.Substring(0, 40) + "..." : this.NO_RESP_DINAMICO);
                else
                    return " - ";
            }
        }
        public string TELEFONE_MAE { get; set; }
        public string TELEFONE_PAI { get; set; }
        public string TELEFONE_RESP_DINAMICO
        {
            get
            {
                if (this.FL_MAE_RESP == "S") //Se só a mãe for a responsável
                    return AuxiliFormatoExibicao.PreparaTelefone(this.TELEFONE_MAE);
                else if (this.FL_PAI_RESP == "S") //Se só o pai for o responsável
                    return AuxiliFormatoExibicao.PreparaTelefone(this.TELEFONE_PAI);
                else //Se nenhum dos dois forem responsáveis, retorna o telefone do responsável associado
                    return AuxiliFormatoExibicao.PreparaTelefone(this.TELEFONE_CEL);
            }
        }
        public string TELEFONE_FIX { get; set; }
        public string TELEFONE_CEL { get; set; }

        public int CO_COL { get; set; }
        public string dt_ultimo_atend
        {
            get
            {
                var res = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                           where tbs174.CO_COL == this.CO_COL && tbs174.CO_ALU == this.coAlu
                           && tbs174.FL_CONF_AGEND == "S"
                           select new { tbs174.DT_AGEND_HORAR }).OrderByDescending(w => w.DT_AGEND_HORAR).FirstOrDefault();

                if (res != null)
                    return res.DT_AGEND_HORAR.ToString("dd/MM/yy");
                else
                    return " - ";
            }
        }
        public string diaAniversario
        {
            get
            {
                return (this.pacienteDataN.HasValue ? this.pacienteDataN.Value.Day.ToString("00") : " - ");
            }
        }
        public int diaAniversarioINT
        {
            get
            {
                if (this.diaAniversario != " - ")
                    return int.Parse(this.diaAniversario);
                else
                    return 0;
            }
        }
    }
}