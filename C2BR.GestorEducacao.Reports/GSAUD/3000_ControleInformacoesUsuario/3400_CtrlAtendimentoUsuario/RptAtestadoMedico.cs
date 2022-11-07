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
using System.Globalization;

namespace C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3400_CtrlAtendimentoUsuario
{
    public partial class RptAtestadoMedico : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptAtestadoMedico()
        {
            InitializeComponent();
        }
        
        public int InitReport(
                                string infos,
                                int codEmp, 
                                int idAtendMedic,
                                int id_atest_medic 
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
                this.bsHeader.Clear();
                this.bsHeader.Add(header);

                // Setar o header do relatorio
                this.BaseInit(header);

                #endregion
                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;

                #region Query Atestado Médico

                var res = from tbs219 in TBS219_ATEND_MEDIC.RetornaTodosRegistros()
                          join tbs333 in TBS333_ATEST_MEDIC_PACIE.RetornaTodosRegistros() on tbs219.ID_ATEND_MEDIC equals tbs333.ID_ATEND_MEDIC
                          join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs333.CO_COL_MEDIC equals tb03.CO_COL
                          join tb117 in TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaTodosRegistros() on tbs333.IDE_CID equals tb117.IDE_CID
                          join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs333.CO_EMP equals tb25.CO_EMP
                          join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs219.CO_ALU equals tb07.CO_ALU
                          join tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros() on tb07.TB108_RESPONSAVEL.CO_RESP equals tb108.CO_RESP into l1
                          from ls in l1.DefaultIfEmpty()
                          where tbs219.ID_ATEND_MEDIC == idAtendMedic
                          &&    tbs333.ID_ATEST_MEDIC_PACIE == id_atest_medic
                          select new
                          {
                              nomePac = tb07.NO_ALU,
                              codCid = tb117.CO_CID,
                              nomCid = tb117.NO_CID,
                              qtDias = tbs333.QT_DIAS,
                              dataAtest = tbs333.DT_ATEST_MEDIC,
                              UnidAtend = tb25.NO_FANTAS_EMP,
                              responsavel = ls.NO_RESP,
                              horaAtend = tbs333.NU_HORAS,
                              rgPacien = tb07.CO_RG_ALU,
                              cpfPacie = tb07.NU_CPF_ALU,
                              rgResp = ls.CO_RG_RESP,
                              cpfResp = ls.NU_CPF_RESP,
                              noColabo = tb03.NO_COL,
                              idDocum = tbs333.ID_DOCUM,
                              observacao = tbs333.DE_OBSER,
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
                           where tb009.ID_DOCUM == dados.idDocum
                           select new AtestadoMedico
                           {
                               Pagina = tb010.NU_PAGINA,
                               Titulo = tb009.NM_TITUL_DOCUM,
                               Texto = tb010.AR_DADOS
                           }).OrderBy(x => x.Pagina);


                if (lst != null && lst.Where(x => x.Pagina == 1).Any())
                {
                    foreach (var Doc in lst)
                    {
                        //lblTitulo.Text = Doc.Titulo.Replace("[AnoLetivo]", dados.AnoLetivo);
                         //string st.Value = Doc.Texto;
                        //lbltitulo.Text = Doc.Titulo;
                        SerializableString st = new SerializableString(Doc.Texto);
                        
                        //Informações gerais do Atestado
                        st.Value = st.Value.Replace("[codCid]", dados.codCid);
                        st.Value = st.Value.Replace("[nomCid]", dados.nomCid);
                        st.Value = st.Value.Replace("[qtDias]", (dados.qtDias.HasValue ? (dados.qtDias.Value == 1 ? dados.qtDias.Value.ToString() + " dia" : dados.qtDias.Value.ToString() + " dias") : "--"));
                        st.Value = st.Value.Replace("[nomCid]", dados.nomCid);
                        st.Value = st.Value.Replace("[dataIniAtest]", (dados.dataAtest.HasValue ? dados.dataAtest.Value.ToString("dd/MM/yyyy") : "--"));
                        st.Value = st.Value.Replace("[obs]", dados.observacao);
                        
                        //Verifica se existe data de início e quantidade de dias no registro em questão, caso exista, calcula a data de término do afastamento
                        st.Value = st.Value.Replace("[dataFimAtest]", (dados.qtDias.HasValue && dados.dataAtest.HasValue ? dados.dataAtest.Value.AddDays(dados.qtDias.Value).ToString("dd/MM/yyyy") : "" ));
                        st.Value = st.Value.Replace("[UnidAtend]", dados.UnidAtend);
                        st.Value = st.Value.Replace("[horaAtend]", dados.horaAtend);

                        //Informações do paciente e acompanhante
                        st.Value = st.Value.Replace("[nomePaciente]", dados.nomePac);
                        st.Value = st.Value.Replace("[noRespons]", dados.responsavel);
                        st.Value = st.Value.Replace("[rgPacien]", dados.rgPacien);
                        st.Value = st.Value.Replace("[cpfPacie]", (dados.cpfPacie != null ? dados.cpfPacie.Insert(3, ".").Insert(7, ".").Insert(11, "-") : "---"));
                        st.Value = st.Value.Replace("[rgResp]", dados.rgResp);
                        st.Value = st.Value.Replace("[cpfResp]", (dados.cpfResp != null ? dados.cpfResp.Insert(3, ".").Insert(7, ".").Insert(11, "-") : "---"));

                        //Informações do médico
                        st.Value = st.Value.Replace("[noColab]", dados.noColabo);

                        //Trata a label de informações do dia
                        CultureInfo culture = new CultureInfo("pt-BR");
                        DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                        int dia = DateTime.Now.Day;
                        int ano = DateTime.Now.Year;
                        string mes = culture.TextInfo.ToTitleCase(dtfi.GetMonthName(DateTime.Now.Month));
                        string diasemana = culture.TextInfo.ToTitleCase(dtfi.GetDayName(DateTime.Now.DayOfWeek));
                        string data = diasemana + ", " + dia + " de " + mes + " de " + ano;
                        st.Value = st.Value.Replace("[dtAtualPorExtenso]", data);

                        #region Mês Atual
                        switch (DateTime.Now.Month.ToString())
                        {
                            case "1":
                                st.Value = st.Value.Replace("[MesAtual]", "Janeiro");
                                break;
                            case "2":
                                st.Value = st.Value.Replace("[MesAtual]", "Fevereiro");
                                break;
                            case "3":
                                st.Value = st.Value.Replace("[MesAtual]", "Março");
                                break;
                            case "4":
                                st.Value = st.Value.Replace("[MesAtual]", "Abril");
                                break;
                            case "5":
                                st.Value = st.Value.Replace("[MesAtual]", "Maio");
                                break;
                            case "6":
                                st.Value = st.Value.Replace("[MesAtual]", "Junho");
                                break;
                            case "7":
                                st.Value = st.Value.Replace("[MesAtual]", "Julho");
                                break;
                            case "8":
                                st.Value = st.Value.Replace("[MesAtual]", "Agosto");
                                break;
                            case "9":
                                st.Value = st.Value.Replace("[MesAtual]", "Setembro");
                                break;
                            case "10":
                                st.Value = st.Value.Replace("[MesAtual]", "Outubro");
                                break;
                            case "11":
                                st.Value = st.Value.Replace("[MesAtual]", "Novembro");
                                break;
                            case "12":
                                st.Value = st.Value.Replace("[MesAtual]", "Desembro");
                                break;
                        }
                        #endregion

                        switch (Doc.Pagina)
                        {
                            case 1:
                                {
                                    richPagina1.Rtf = st.Value;
                                    richPagina1.Visible = true;
                                    break;
                                }
                        }
                    }
                }

                return 1;
            }
            catch { return 0; }
        }
        public class AtestadoMedico
        {
            public bool HideLogo { get; set; }
            public string Titulo { get; set; }
            public string SubTitulo { get; set; }
            public int Pagina { get; set; }
            public string Texto { get; set; }
        }
    }
}