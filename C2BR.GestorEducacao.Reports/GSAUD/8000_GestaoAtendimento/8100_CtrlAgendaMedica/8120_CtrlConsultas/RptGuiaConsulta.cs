using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.Reports.Helper;
using C2BR.GestorEducacao.Reports.Properties;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

namespace C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8120_CtrlConsultas
{
    public partial class RptGuiaConsulta : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptGuiaConsulta()
        {
            InitializeComponent();
        }
        public int InitReport(
                              string infos,
                              int codEmp,
                              int ID_AGEND_HORAR
          )
        {


            try
            {
                #region Setar o Header e as Labels

                // Instancia o header do relatorio
                ReportHeader header = ReportHeader.GetHeaderFromEmpresa(codEmp);
                if (header == null)
                    return 0;

                // Setar o header do relatorio
                //this.bsHeader.Clear();
                //this.bsHeader.Add(header);

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion

                #region Query Atestado Médico

                var res = from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                          join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs174.CO_COL equals tb03.CO_COL
                          join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs174.CO_ALU equals tb07.CO_ALU
                          join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tbs174.CO_ESPEC equals tb63.CO_ESPECIALIDADE
                          join tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros() on tb07.TB108_RESPONSAVEL.CO_RESP equals tb108.CO_RESP into l1
                          from ls in l1.DefaultIfEmpty()
                          where tbs174.ID_AGEND_HORAR == ID_AGEND_HORAR
                          select new
                          {
                              //Dados do Paciente
                              nomePac = tb07.NO_ALU,
                              nuNisPac = tb07.NU_NIS,
                              nuNire = tb07.NU_NIRE,
                              co_sexo = tb07.CO_SEXO_ALU,
                              dt_nascimento = tb07.DT_NASC_ALU,
                              rgPacien = tb07.CO_RG_ALU,
                              cpfPacie = tb07.NU_CPF_ALU,
                              coGrauParen = tb07.CO_GRAU_PAREN_RESP,

                              //Dados do Responsável
                              noResp = ls.NO_RESP,
                              rgResp = ls.CO_RG_RESP,
                              cpfResp = ls.NU_CPF_RESP,
                              nuNisResp = ls.NU_NIS_RESP,

                              //Dados da Consulta
                              protocoloConsulta = tbs174.NU_REGIS_CONSUL,
                              especialidade = tb63.NO_ESPECIALIDADE,
                              dataConsulta = tbs174.DT_AGEND_HORAR,
                              tipoConsulta = tbs174.TP_CONSU,

                              //Dados do Médico
                              NO_COL_RECEB = tb03.NO_COL,
                              CO_MATRIC_COL = tb03.CO_MAT_COL,
                              SIGLA_ENT = tb03.CO_SIGLA_ENTID_PROFI,
                              NUMER_ENT = tb03.NU_ENTID_PROFI,
                              UF_ENT = tb03.CO_UF_ENTID_PROFI,
                              DT_ENT = tb03.DT_EMISS_ENTID_PROFI,

                          };

                #region dell
                var dados = res.FirstOrDefault();

                if (dados == null)
                    return -1;

                #endregion
                #endregion

                var lst = (from tb009 in TB009_RTF_DOCTOS.RetornaTodosRegistros()
                           join tb010 in TB010_RTF_ARQUIVO.RetornaTodosRegistros() on tb009.ID_DOCUM equals tb010.TB009_RTF_DOCTOS.ID_DOCUM
                           //from tb010 in tb009.TB010_RTF_ARQUIVO.DefaultIfEmpty()
                           where tb009.CO_SIGLA_DOCUM == "DCTGC"
                           select new GuiaConsultas
                           {
                               Pagina = tb010.NU_PAGINA,
                               Titulo = tb009.NM_TITUL_DOCUM,
                               Texto = tb010.AR_DADOS
                           }).OrderBy(x => x.Pagina);

                if (lst == null)
                    return -1;

                if (lst != null && lst.Where(x => x.Pagina == 1).Any())
                {
                    foreach (var Doc in lst)
                    {
                        SerializableString st = new SerializableString(Doc.Texto);

                        //Dados do Paciente
                        st.Value = st.Value.Replace("[nomePaci]", dados.nomePac);
                        st.Value = st.Value.Replace("[nisPaci]", dados.nuNisPac.ToString());
                        st.Value = st.Value.Replace("[nirePaci]", dados.nuNire.ToString().PadLeft(7, '0'));
                        st.Value = st.Value.Replace("[sexoPaci]", (dados.co_sexo == "M" ? "Mas" : "Fem"));
                        st.Value = st.Value.Replace("[dtNascPaci]", (dados.dt_nascimento.HasValue ? dados.dt_nascimento.Value.ToString("dd/MM/yyyy") : " - "));
                        st.Value = st.Value.Replace("[nuRgPaci]", dados.rgPacien);
                        st.Value = st.Value.Replace("[nuCpfPaci]", Funcoes.Format(dados.nomePac, TipoFormat.CPF));

                        //Dados do Responsável
                        st.Value = st.Value.Replace("[nuCpfResp]", Funcoes.Format(dados.cpfResp, TipoFormat.CPF));
                        st.Value = st.Value.Replace("[noResp]", Funcoes.Format(dados.cpfResp, TipoFormat.CPF));
                        st.Value = st.Value.Replace("[nuNisResp]", dados.nuNisResp.ToString());
                        st.Value = st.Value.Replace("[noGrauPar]", CarregaGrauParentesco(dados.coGrauParen));

                        //Dados da Consulta
                        st.Value = st.Value.Replace("[nuConsulta]", (!string.IsNullOrEmpty(dados.protocoloConsulta) ? dados.protocoloConsulta.Insert(2, ".").Insert(6, ".").Insert(9, ".") : " - "));
                        st.Value = st.Value.Replace("[noEspecCon]", dados.especialidade);
                        st.Value = st.Value.Replace("[noTipoCon]", CarregaTipoConsulta(dados.tipoConsulta));

                        //Dados do Médico
                        st.Value = st.Value.Replace("[deEntiPro]", (!string.IsNullOrEmpty(dados.SIGLA_ENT) ? dados.SIGLA_ENT + " " + dados.NUMER_ENT + " - " + dados.UF_ENT : ""));
                        st.Value = st.Value.Replace("[noProfi]", dados.NO_COL_RECEB);

                        switch (Doc.Pagina)
                        {
                            case 1:
                                {
                                    richPagina1.Rtf = st.Value;
                                    richPagina1.Visible = true;
                                    break;
                                }
                            case 2:
                                {
                                    richPagina2.Rtf = st.Value;
                                    richPagina2.Visible = true;
                                    break;
                                }
                            case 3:
                                {
                                    richPagina3.Rtf = st.Value;
                                    richPagina3.Visible = true;
                                    break;
                                }
                        }
                    }
                }

                return 1;
            }
            catch { return 0; }
        }

        /// <summary>
        /// Verifica de acordo com o código recebido, qual o tipo da consulta
        /// </summary>
        /// <param name="CO_TIPO"></param>
        private string CarregaTipoConsulta(string CO_TIPO)
        {
            string s = "";
            switch (CO_TIPO)
            {
                case "N":
                    s = "Normal";
                    break;
                case "U":
                    s = "Urgente";
                    break;
                case "R":
                    s = "Retorno";
                    break;
                default:
                    s = " - ";
                    break;
            }
            return s;
        }

        /// <summary>
        /// Verifica de acordo com o código recebido, qual o grau de parentesco do Responsável
        /// </summary>
        /// <param name="CO_TIPO"></param>
        private string CarregaGrauParentesco(string CO_GRAU)
        {

            string s = "";
            switch (CO_GRAU)
            {
                case "PM":
                    s = "Pai/Mãe";
                    break;
                case "TI":
                    s = "Tio(a)";
                    break;
                case "AV":
                    s = "Avô/Avó";
                    break;
                case "PR":
                    s = "Primo(a)";
                    break;
                case "CN":
                    s = "Cunhado(a)";
                    break;
                case "TU":
                    s = "Tutor(a)";
                    break;
                case "IR":
                    s = "Irmão(ã)";
                    break;
                case "OU":
                    s = "Outros";
                    break;
                default:
                    s = " - ";
                    break;
            }
            return s;
        }

        public class GuiaConsultas
        {
            public bool HideLogo { get; set; }
            public string Titulo { get; set; }
            public string SubTitulo { get; set; }
            public int Pagina { get; set; }
            public string Texto { get; set; }
        }
    }
}