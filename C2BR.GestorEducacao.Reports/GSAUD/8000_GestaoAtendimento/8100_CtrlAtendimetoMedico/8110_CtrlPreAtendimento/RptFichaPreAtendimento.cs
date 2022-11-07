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

namespace C2BR.GestorEducacao.Reports.GSAUD._8000_GestaoAtendimento._8100_CtrlAtendimetoMedico._8110_CtrlPreAtendimento
{
    public partial class RptFichaPreAtendimento : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptFichaPreAtendimento()
        {
            InitializeComponent();
        }
        public int InitReport(
                              string infos,
                              int codEmp,
                              int ID_PRE_ATEND
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

                var res = from tbs194 in TBS194_PRE_ATEND.RetornaTodosRegistros()
                          join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs194.CO_COL_FUNC equals tb03.CO_COL
                          join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs194.CO_ALU equals tb07.CO_ALU into l1
                          from lpac in l1.DefaultIfEmpty()
                          join tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros() on tbs194.CO_RESP equals tb108.CO_RESP into l2
                          from lres in l2.DefaultIfEmpty()
                          join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tbs194.CO_ESPEC equals tb63.CO_ESPECIALIDADE
                          join tbs337 in TBS337_TIPO_DORES.RetornaTodosRegistros() on tbs194.TBS337_TIPO_DORES.ID_TIPO_DORES equals tbs337.ID_TIPO_DORES into ld1
                          from ldor1 in ld1.DefaultIfEmpty()
                          join tbs337 in TBS337_TIPO_DORES.RetornaTodosRegistros() on tbs194.TBS337_TIPO_DORES.ID_TIPO_DORES equals tbs337.ID_TIPO_DORES into ld2
                          from ldor2 in ld2.DefaultIfEmpty()
                          join tbs337 in TBS337_TIPO_DORES.RetornaTodosRegistros() on tbs194.TBS337_TIPO_DORES.ID_TIPO_DORES equals tbs337.ID_TIPO_DORES into ld3
                          from ldor3 in ld3.DefaultIfEmpty()
                          join tbs337 in TBS337_TIPO_DORES.RetornaTodosRegistros() on tbs194.TBS337_TIPO_DORES.ID_TIPO_DORES equals tbs337.ID_TIPO_DORES into ld4
                          from ldor4 in ld4.DefaultIfEmpty()
                          where tbs194.ID_PRE_ATEND == ID_PRE_ATEND
                          select new
                          {
                              //Dados do Paciente
                              nomePac = tbs194.NO_USU,
                              nuNisPac = tbs194.NU_NIS ?? lpac.NU_NIS, 
                              nuNire = lpac.NU_NIRE,
                              co_sexo = tbs194.CO_SEXO_USU,
                              dt_nascimento = tbs194.DT_NASC_USU,
                              rgPacien = lpac.CO_RG_ALU,
                              cpfPacie = tbs194.NU_CPF_USU ?? lpac.NU_CPF_ALU,
                              coGrauParen = tbs194.CO_GRAU_PAREN,

                              //Dados do Responsável
                              noResp = lres.NO_RESP,
                              rgResp = lres.CO_RG_RESP,
                              cpfResp = lres.NU_CPF_RESP,
                              nuNisResp = lres.NU_NIS_RESP,

                              //Dados do pré-atendimento
                              protocoloPreAtend = tbs194.CO_PRE_ATEND,
                              dataPreAtend = tbs194.DT_PRE_ATEND,

                              //Leitura Pré-Atendimento
                              altura = tbs194.NU_ALTU,
                              peso = tbs194.NU_PESO,
                              pressao = tbs194.NU_PRES_ARTE,
                              hrPressao = tbs194.HR_PRES_ARTE,
                              temper = tbs194.NU_TEMP,
                              hrTemper = tbs194.HR_TEMP,
                              Glicem = tbs194.NU_GLICE,
                              hrGlicem = tbs194.HR_GLICE,

                              //Registro de Risco Pré-Atendimento
                              flDiabetes = tbs194.FL_DIABE,
                              deDiabetes = tbs194.DE_DIABE,
                              flHiperten = tbs194.FL_HIPER_TENSO,
                              deHiperten = tbs194.DE_HIPER_TENSO,
                              flFumante = tbs194.FL_FUMAN,
                              tpFumante = tbs194.NU_TEMPO_FUMAN,
                              flAlcool = tbs194.FL_ALCOO,
                              tpAlcool = tbs194.NU_TEMPO_ALCOO,
                              flCirurgia = tbs194.FL_CIRUR,
                              deCirurgia = tbs194.DE_CIRUR,
                              flAlergia = tbs194.FL_ALERG,
                              deAlergia = tbs194.DE_ALERG,
                              flMarcaPa = tbs194.FL_MARCA_PASSO,
                              deMarcaPa = tbs194.DE_MARCA_PASSO,
                              flValvula = tbs194.FL_VALVU,
                              deValvula = tbs194.DE_VALVU,
                              flEnjoos = tbs194.FL_SINTO_ENJOO,
                              flVomitos = tbs194.FL_SINTO_VOMIT,
                              flFebre = tbs194.FL_SINTO_FEBRE,

                              //Dores Pré-Atendimento
                              flDores = tbs194.FL_SINTO_DORES,
                              dtDores = tbs194.DT_DOR,
                              deDores1 = (ldor1 != null ? ldor1.NM_TIPO_DORES : " - "),
                              deDores2 = (ldor2 != null ? ldor2.NM_TIPO_DORES : " - "),
                              deDores3 = (ldor3 != null ? ldor3.NM_TIPO_DORES : " - "),
                              deDores4 = (ldor4 != null ? ldor4.NM_TIPO_DORES : " - "),

                              //Medicação e Sintomas Pré-Atendimento
                              deMedicUsoC = tbs194.DE_MEDIC_USO_CONTI,
                              deMedic = tbs194.DE_MEDIC,
                              deSintomas = tbs194.DE_SINTO,

                              //Dados Resultado
                              coClassRisco = tbs194.CO_TIPO_RISCO,
                              especialidade = tb63.NO_ESPECIALIDADE,
                              deObservacao = tbs194.DE_OBSER,

                              //Dados do Médico
                              NO_COL_RECEB = tb03.NO_COL,
                              CO_MATRIC_COL = tb03.CO_MAT_COL,
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
                           where tb009.CO_SIGLA_DOCUM == "DCFPA"
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
                        st.Value = st.Value.Replace("[nirePaci]", dados.nomePac);
                        st.Value = st.Value.Replace("[sexoPaci]", (dados.co_sexo == "M" ? "Mas" : "Fem"));
                        st.Value = st.Value.Replace("[dtNascPaci]", dados.dt_nascimento.ToString("dd/MM/yyyy"));
                        st.Value = st.Value.Replace("[nuRgPaci]", dados.rgPacien);
                        st.Value = st.Value.Replace("[nuCpfPaci]", Funcoes.Format(dados.cpfPacie , TipoFormat.CPF));

                        //Dados do Responsável
                        st.Value = st.Value.Replace("[nuCpfResp]", Funcoes.Format(dados.cpfResp, TipoFormat.CPF));
                        st.Value = st.Value.Replace("[noResp]", dados.noResp);
                        st.Value = st.Value.Replace("[nuNisResp]", dados.nuNisResp.ToString());
                        st.Value = st.Value.Replace("[noGrauPar]", CarregaGrauParentesco(dados.coGrauParen));

                        //Dados do Pré-Atendimento
                        st.Value = st.Value.Replace("[nuPreAtend]", (!string.IsNullOrEmpty(dados.protocoloPreAtend) ? dados.protocoloPreAtend.Insert(2, ".").Insert(6, ".").Insert(9, ".") : " - "));
                        st.Value = st.Value.Replace("[dtPreAtend]", dados.dataPreAtend.ToString("dd/MM/yyyy"));
                        st.Value = st.Value.Replace("[hrPreAtend]", dados.dataPreAtend.ToString("HH:mm"));

                        //Dados da Leitura
                        st.Value = st.Value.Replace("[nuAltura]", (dados.altura.HasValue ? dados.altura.ToString() : " - "));
                        st.Value = st.Value.Replace("[nuPeso]", (dados.peso.HasValue ? dados.peso.ToString() : " - "));
                        st.Value = st.Value.Replace("[nuPressao]", (!string.IsNullOrEmpty(dados.pressao) ? dados.pressao : " - "));
                        st.Value = st.Value.Replace("[hrPressao]", dados.hrPressao);
                        st.Value = st.Value.Replace("[temper]", (dados.temper.HasValue ? dados.temper.ToString() : " - "));
                        st.Value = st.Value.Replace("[hrTemper]", dados.hrTemper);
                        st.Value = st.Value.Replace("[Glicem]", (dados.Glicem.HasValue ? dados.Glicem.ToString() : " - "));
                        st.Value = st.Value.Replace("[hrGlicem]", dados.hrGlicem);

                        //Dados do Registro de Risco
                         st.Value = st.Value.Replace("[flDiabetes]", CarregaBooleano(dados.flDiabetes));
                        st.Value = st.Value.Replace("[deDiabetes]", dados.deDiabetes);
                        st.Value = st.Value.Replace("[flHiperten]", CarregaBooleano(dados.flHiperten));
                        st.Value = st.Value.Replace("[deHiperten]", dados.deHiperten);
                        st.Value = st.Value.Replace("[flFumante]", CarregaBooleano(dados.flFumante));
                        st.Value = st.Value.Replace("[tpFumante]", dados.tpFumante.ToString());
                        st.Value = st.Value.Replace("[flAlcool]", CarregaBooleano(dados.flAlcool));
                        st.Value = st.Value.Replace("[tpAlcool]", dados.tpAlcool.ToString());
                        st.Value = st.Value.Replace("[flCirurgia]", CarregaBooleano(dados.flCirurgia));
                        st.Value = st.Value.Replace("[deCirurgia]", dados.deCirurgia);
                        st.Value = st.Value.Replace("[flAlergia]", CarregaBooleano(dados.flAlergia));
                        st.Value = st.Value.Replace("[deAlergia]", dados.deAlergia);
                        st.Value = st.Value.Replace("[flMarcaPa]", CarregaBooleano(dados.flMarcaPa));
                        st.Value = st.Value.Replace("[deMarcaPa]", dados.deMarcaPa);
                        st.Value = st.Value.Replace("[flValvula]", CarregaBooleano(dados.flValvula));
                        st.Value = st.Value.Replace("[deValvula]", dados.deValvula);
                        st.Value = st.Value.Replace("[flEnjoos]", CarregaBooleano(dados.flEnjoos));
                        st.Value = st.Value.Replace("[flVomitos]", CarregaBooleano(dados.flVomitos));
                        st.Value = st.Value.Replace("[flFebre]", CarregaBooleano(dados.flFebre));


                        //Dores Pré-Atendimento
                        st.Value = st.Value.Replace("[dtDores]", (dados.dtDores.HasValue ? dados.dtDores.Value.ToString("dd/MM/yyyy") : " - "));
                        st.Value = st.Value.Replace("[hrDores]", (dados.dtDores.HasValue ? dados.dtDores.Value.ToString("HH:mm") : " - "));
                        st.Value = st.Value.Replace("[flDores]", CarregaBooleano(dados.flDores));
                        st.Value = st.Value.Replace("[deDores1]", dados.deDores1);
                        st.Value = st.Value.Replace("[deDores2]", dados.deDores2);
                        st.Value = st.Value.Replace("[deDores3]", dados.deDores3);
                        st.Value = st.Value.Replace("[deDores4]", dados.deDores4);

                        //Medicação e Sintomas Pré-Atendimento
                        st.Value = st.Value.Replace("[deMedicUsoC]", dados.deMedicUsoC);
                        st.Value = st.Value.Replace("[deMedic]", dados.deMedic);
                        st.Value = st.Value.Replace("[deSintomas]", dados.deSintomas);


                        //Dados Resultado
                        st.Value = st.Value.Replace("[noClassRisco]", RetornaNomeClassificacaoRisco(dados.coClassRisco));
                        st.Value = st.Value.Replace("[noEspec]", dados.especialidade);
                        st.Value = st.Value.Replace("[deObservacao]", dados.deObservacao);

                        //Dados do Médico
                        st.Value = st.Value.Replace("[noColabCad]", dados.NO_COL_RECEB);
                        st.Value = st.Value.Replace("[MatColabCad]", dados.CO_MATRIC_COL);

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

        public static string RetornaNomeClassificacaoRisco(int CO_TIPO_RISCO)
        {
            string s = "";
            switch (CO_TIPO_RISCO)
            {
                case 1:
                    s = "EMERGÊNCIA";
                    break;

                case 2:
                    s = "MUITO URGENTE";
                    break;

                case 3:
                    s = "URGENTE";
                    break;

                case 4:
                    s = "POUCO URGENTE";
                    break;

                case 5:
                    s = "NÃO URGENTE";
                    break;

                default:
                    s = " - ";
                    break;
            }
            return s;
        }


        /// <summary>
        /// Retorna o nome booleano correspondente 
        /// </summary>
        private string CarregaBooleano(string FL)
        {
            string s = "";
            switch (FL)
            {
                case "S":
                    s = "Sim";
                    break;
                case "N":
                    s = "Não";
                    break;
                case "A":
                    s = "As Vezes";
                    break;
            }
            return s;
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