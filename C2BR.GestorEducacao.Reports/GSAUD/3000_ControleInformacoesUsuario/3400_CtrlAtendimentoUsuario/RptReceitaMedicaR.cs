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

namespace C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3400_CtrlAtendimentoUsuario
{
    public partial class RptReceitaMedicaR : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptReceitaMedicaR()
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

                var res = (from tbs219 in TBS219_ATEND_MEDIC.RetornaTodosRegistros()
                           join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs219.CO_ALU equals tb07.CO_ALU
                           join tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros() on tbs219.TB108_RESPONSAVEL.CO_RESP equals tb108.CO_RESP into lre
                           from lresp in lre.DefaultIfEmpty()
                           join tb905 in TB905_BAIRRO.RetornaTodosRegistros() on tb07.TB905_BAIRRO.CO_BAIRRO equals tb905.CO_BAIRRO into l1
                           from ls in l1.DefaultIfEmpty()
                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs219.CO_COL equals tb03.CO_COL
                           where tbs219.ID_ATEND_MEDIC == ID_AGEND_HORAR
                           select new
                           {
                               //Informações do Paciente
                               nomePac = tb07.NO_ALU,
                               co_sexo = tb07.CO_SEXO_ALU,
                               dt_nascimento = tb07.DT_NASC_ALU,
                               NirePaci = tb07.NU_NIRE,
                               nuNisPac = tb07.NU_NIS,
                               Bairro = ls.NO_BAIRRO ?? "",
                               Cidade = ls.TB904_CIDADE.NO_CIDADE ?? "",
                               Uf = tb07.CO_ESTA_ALU,
                               rgPacien = tb07.CO_RG_ALU,
                               orgRgPaci = tb07.CO_ORG_RG_ALU,
                               ufRgPacien = tb07.CO_ESTA_RG_ALU,
                               noGrauPar = lresp.DE_GRAU_PAREN,

                               //Informações do Médico
                               NO_COL_RECEB = tb03.NO_COL,
                               sexoMedico = tb03.CO_SEXO_COL,
                               SIGLA_ENT = tb03.CO_SIGLA_ENTID_PROFI,
                               NUMER_ENT = tb03.NU_ENTID_PROFI,
                               UF_ENT = tb03.CO_UF_ENTID_PROFI,
                               DT_ENT = tb03.DT_EMISS_ENTID_PROFI,

                               //Informações do Responsável
                               cpfResp = lresp.NU_CPF_RESP,
                               noResp = lresp.NO_RESP,
                               nuNisResp = lresp.NU_NIS_RESP,

                               //Informações gerais
                               coAtendMedic = tbs219.CO_ATEND_MEDIC,
                           }).ToList();

                #region dell
                var dados = res.FirstOrDefault();

                if (dados == null)
                    return -1;

                #endregion
                #endregion

                var lst = (from tb009 in TB009_RTF_DOCTOS.RetornaTodosRegistros()
                           join tb010 in TB010_RTF_ARQUIVO.RetornaTodosRegistros() on tb009.ID_DOCUM equals tb010.TB009_RTF_DOCTOS.ID_DOCUM
                           //from tb010 in tb009.TB010_RTF_ARQUIVO.DefaultIfEmpty()
                           where tb009.CO_SIGLA_DOCUM == "DCTRC"
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
                        st.Value = st.Value.Replace("[nisPaci]", dados.nuNisPac.ToString().PadLeft(7, '0'));
                        st.Value = st.Value.Replace("[nirePaci]", dados.NirePaci.ToString().PadLeft(7, '0'));
                        st.Value = st.Value.Replace("[sexoPaci]", (dados.co_sexo == "M" ? "Mas" : "Fem"));
                        st.Value = st.Value.Replace("[dtNascPaci]", (dados.dt_nascimento.HasValue ? dados.dt_nascimento.Value.ToString("dd/MM/yyyy") : " - "));
                        st.Value = st.Value.Replace("[nuCpfPaci]", Funcoes.Format(dados.nomePac, TipoFormat.CPF));
                        st.Value = st.Value.Replace("[nuRgPaci]", dados.rgPacien);
                        st.Value = st.Value.Replace("[nuRgPaciComp]", (!string.IsNullOrEmpty(dados.rgPacien) ? dados.rgPacien + " - " + dados.orgRgPaci + "/" + dados.ufRgPacien : " - "));
                        st.Value = st.Value.Replace("[endPacieComp]", (!string.IsNullOrEmpty(dados.Bairro) ? dados.Bairro + ", " + dados.Cidade + " - " + dados.Uf : " - "));

                        //Dados do Responsável
                        st.Value = st.Value.Replace("[nuCpfResp]", Funcoes.Format(dados.cpfResp, TipoFormat.CPF));
                        st.Value = st.Value.Replace("[noResp]", dados.noResp);
                        st.Value = st.Value.Replace("[nuNisResp]", dados.nuNisResp.ToString());
                        st.Value = st.Value.Replace("[noGrauPar]", CarregaGrauParentesco(dados.noGrauPar));

                        //Dados do Médico
                        st.Value = st.Value.Replace("[deEntiPro]", (!string.IsNullOrEmpty(dados.SIGLA_ENT) ? dados.SIGLA_ENT + " " + dados.NUMER_ENT + " - " + dados.UF_ENT : ""));
                        st.Value = st.Value.Replace("[noProfi]", (dados.sexoMedico == "M" ? "Dr. " : "Dra. ") + dados.NO_COL_RECEB);

                        //Dados Gerais
                        st.Value = st.Value.Replace("[coAtendMedic]", dados.coAtendMedic.Insert(2, ".").Insert(6, ".").Insert(9, "."));

                        var resRecei = (from tbs330 in TBS330_RECEI_ATEND_MEDIC.RetornaTodosRegistros()
                                   join tb90 in TB90_PRODUTO.RetornaTodosRegistros() on tbs330.CO_MEDIC equals tb90.CO_PROD
                                   where tbs330.ID_ATEND_MEDIC == ID_AGEND_HORAR
                                   select new 
                                   {
                                       nomeMedicamento = tb90.NO_PROD,
                                       ModoUso = tbs330.QT_USO,
                                       Prescricao = tbs330.DE_PRESC,
                                       PrinciAtivo = tbs330.DE_PRINC_ATIVO,
                                   }).ToList();

                        int auxRecei = 0;

                        //Loop que preenche as informações dos medicamentos
                        foreach (var li in resRecei)
                        {
                            auxRecei++;
                            string auxRe = auxRecei.ToString().PadLeft(2, '0');
                            st.Value = st.Value.Replace("[ContItensRece" + auxRe + "]", auxRe);
                            st.Value = st.Value.Replace("[noProdItem" + auxRe + "]", li.nomeMedicamento);
                            st.Value = st.Value.Replace("[modoUsoItem" + auxRe + "]", (li.ModoUso == 0 ? "USO CONTÍNUO" : "USO POR " + li.ModoUso + " DIAS."));
                            st.Value = st.Value.Replace("[dePrincAtivo" + auxRe + "]", li.PrinciAtivo);
                            st.Value = st.Value.Replace("[dePrescItem" + auxRe + "]", li.Prescricao);
                        }
                        
                        //Trata as Tags de itens do receituário
                        while (auxRecei <= 3)
                        {
                            auxRecei++;
                            string auxRe = auxRecei.ToString().PadLeft(2, '0');
                            st.Value = st.Value.Replace("[MatrCCTituX]".Replace("X", auxRe), "---");
                            st.Value = st.Value.Replace("[MatrCCBandX]".Replace("X", auxRe), "---");
                            st.Value = st.Value.Replace("[MatrCCNumeX]".Replace("X", auxRe), "---");
                            st.Value = st.Value.Replace("[MatCDVX]".Replace("X", auxRe), "---"); ;
                            st.Value = st.Value.Replace("[MtCCDX]".Replace("X", auxRe), "---");
                        }

                        //Concatena dia, mes e ano
                        //Trata a label de informações do dia
                        CultureInfo culture = new CultureInfo("pt-BR");
                        DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                        int dia = DateTime.Now.Day;
                        int ano = DateTime.Now.Year;
                        string mes = culture.TextInfo.ToTitleCase(dtfi.GetMonthName(DateTime.Now.Month));
                        string diasemana = culture.TextInfo.ToTitleCase(dtfi.GetDayName(DateTime.Now.DayOfWeek));
                        string data = diasemana + ", " + dia + " de " + mes + " de " + ano;
                        st.Value = st.Value.Replace("[deDiaAtual]", data);

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