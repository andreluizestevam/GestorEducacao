using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Text.RegularExpressions;
using C2BR.GestorEducacao.Reports.Helper;
using System.Globalization;

namespace C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3400_CtrlAtendimentoUsuario
{
    public partial class RptFichaDirecionamento : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptFichaDirecionamento()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                        string infos,
                        int coEmp,
                       int IDEncamMedico,
                        string NomeFuncionalidadeCadastrada
                        )
        {
            try
            {
                #region Setar o Header e as Labels

                // Setar as labels de parametros e rodape
                this.InfosRodape = infos;
                this.Parametros = parametros;

                // Instancia o header do relatorio
                ReportHeader header = C2BR.GestorEducacao.Reports.ReportHeader.GetHeaderFromEmpresa(coEmp);
                if (header == null)
                    return 0;

                lblParametros.Text = parametros;
               
                // Setar o header do relatorio
                this.BaseInit(header);
                if (NomeFuncionalidadeCadastrada == "")
                    lblTitulo.Text = "";
                else
                     lblTitulo.Text = NomeFuncionalidadeCadastrada;

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;
                #region Query

                var res = (from tbs195 in TBS195_ENCAM_MEDIC.RetornaTodosRegistros()
                           join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs195.CO_ALU equals tb07.CO_ALU
                           join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb07.CO_EMP_ORIGEM equals tb25.CO_EMP into lea
                           from lempOrgPac in lea.DefaultIfEmpty()
                           join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs195.CO_EMP_ENCAM_MEDIC equals tb25.CO_EMP
                           join tb905 in TB905_BAIRRO.RetornaTodosRegistros() on tb07.TB905_BAIRRO.CO_BAIRRO equals tb905.CO_BAIRRO into l1
                           from lbar in l1.DefaultIfEmpty()
                           join tb904 in TB904_CIDADE.RetornaTodosRegistros() on lbar.CO_CIDADE equals tb904.CO_CIDADE into l2
                           from lcid in l2.DefaultIfEmpty()

                           where (IDEncamMedico != 0 ? tbs195.ID_ENCAM_MEDIC == IDEncamMedico : 0 == 0)
                           select new
                           {
                               //Topo 
                               NumeDirec = tbs195.CO_ENCAM_MEDIC,
                               UnidDirec = tb25.NO_FANTAS_EMP,
                               DataDirec = tbs195.DT_ENCAM_MEDIC,
                               UnRefPaci = tb07.NU_NIS,
                               //Informações do Paciente
                               NomeDoPaciente = tb07.NO_ALU,
                               Sexo = tb07.CO_SEXO_ALU,
                               NascimentoPa = tb07.DT_NASC_ALU,
                               NProntuario = tb07.NU_NIRE,
                               Etnia = tb07.TP_RACA,
                               Deficiencia = tb07.TP_DEF,
                               NomeDaMae = tb07.NO_MAE_ALU ?? " - ",
                               NFoneFixo = tb07.NU_TELE_RESI_ALU,
                               NCelular = tb07.NU_TELE_CELU_ALU,
                               Nis = tb07.NU_NIS,
                               cidadeAlu = lcid.NO_CIDADE,
                               bairroAlu = lbar.NO_BAIRRO,
                               ufAlu = tb07.CO_ESTA_ALU,
                               logradouroAlu = tb07.DE_ENDE_ALU,
                               cepAlu = tb07.CO_CEP_ALU,
                               EnderecoAlu = tb07.NO_ENDE_ELET_ALU,
                               grauParentesco = tb07.CO_GRAU_PAREN_RESP,
                               empOrigemPaci = (lempOrgPac != null ? lempOrgPac.NO_FANTAS_EMP : " - "),
                               // Informações do Responsável
                               NomeDoResponsavel = tb07.TB108_RESPONSAVEL.NO_RESP,
                               SexoRep = tb07.TB108_RESPONSAVEL.CO_SEXO_RESP,
                               Parentesco = tb07.TB108_RESPONSAVEL.DE_GRAU_PAREN,
                               NCPF = tb07.TB108_RESPONSAVEL.NU_CPF_RESP,
                               NFoneFixoRep = tb07.TB108_RESPONSAVEL.NU_TELE_RESI_RESP,
                               NCelularResp = tb07.TB108_RESPONSAVEL.NU_TELE_CELU_RESP,
                               NWhatsAppResp = tb07.TB108_RESPONSAVEL.NU_TELE_WHATS_RESP,
                               EmailResp = tb07.TB108_RESPONSAVEL.DES_EMAIL_RESP,
                               NisResp = tb07.TB108_RESPONSAVEL.NU_NIS_RESP,


                               ////INFORMAÇÕES DO PRÉ-ATENDIMENTO
                               //MatColabCad = colab.CO_MAT_COL,
                               //noColabCad = colab.NO_COL,
                               //nuPreAtend = lacolhi.NO_RESP,
                               //dtPreAtend = lacolhi.DT_PRE_ATEND,
                               //hrPreAtend = lacolhi.DT_PRE_ATEND,
                               //noClassRisco = lacolhi.CO_TIPO_RISCO,
                               ////DADOS DE LEITURA E INFORMAÇÃO BÁSICA
                               //Altura = lacolhi.NU_ALTU,
                               //Peso = lacolhi.NU_PESO,
                               //NuPressao = lacolhi.NU_PRES_ARTE,
                               //HoraPressao = lacolhi.HR_PRES_ARTE,
                               //temperatura = lacolhi.NU_TEMP,
                               //HoraTemperatura = lacolhi.HR_TEMP,
                               //TaxaGlicemica = lacolhi.NU_GLICE,
                               //HoraTaxaGlicemica = lacolhi.HR_GLICE,
                               //TeveEnjoo = lacolhi.FL_SINTO_ENJOO,
                               //Vomitos = lacolhi.FL_SINTO_VOMIT,
                               //TeveDores = lacolhi.FL_SINTO_DORES,
                               //DataTeveDores = lacolhi.DT_DOR,
                               //HoraTeveDores = lacolhi.HR_DOR,
                               ////DADOS DE REGISTRO DE RISCO
                               //Diabetes = lacolhi.FL_DIABE,
                               //DeDiabetes = lacolhi.DE_DIABE,
                               //flHipertensao = lacolhi.FL_HIPER_TENSO,
                               //Hipertensao = lacolhi.DE_HIPER_TENSO,
                               //Fumante = lacolhi.FL_FUMAN,
                               //TempoFumante = lacolhi.NU_TEMPO_FUMAN,
                               //UsuarioDeAlcool = lacolhi.FL_ALCOO,
                               //TempoUsuarioDeAlcool = lacolhi.NU_TEMPO_ALCOO,
                               //Cirurgia = lacolhi.FL_CIRUR,
                               //DeCirurgia = lacolhi.DE_CIRUR,
                               //MarcaPasso = lacolhi.FL_MARCA_PASSO,
                               //DMarcaPasso = lacolhi.DE_MARCA_PASSO,
                               //Valvula = lacolhi.FL_VALVU,
                               //Alergia = lacolhi.FL_ALERG,
                               //DeAlergia = lacolhi.DE_ALERG,



                               ////MEDICAÇÃO DE USO CONTÍNUO
                               //deMedicUsoC = lacolhi.FL_ALERG,
                               //deMedic = lacolhi.DE_ALERG,
                               ////DESCRIÇÃO DOS SINTOMAS INFORMADOS PELO PACIENTE E/OU RESPONSÁVEL
                               //DeSintomas = lacolhi.DE_ALERG,
                               ////INFORMAÇÕES DO ATENDIMENTO MÉDICO
                               //coAtendMedic = lencamin.CO_ATEND_MEDIC,
                               //dtAtendMedic = lencamin.DT_ATEND_MEDIC,
                               //HoraFinMedic = lencamin.DT_ATEND_MEDIC,
                               //noProfi = lencamin.CO_EMP,
                               //NomeEntiPro = lacolhi.FL_ALERG,
                               //EspecProfiSaude = lacolhi.FL_ALERG,
                               //deLocAtenPro = lacolhi.FL_ALERG,
                               ////ANAMNESE
                               //DesAnamPaci = lencamin.DE_DIAGN,
                               ////DIAGNÓSTICO
                               //Diagnostico = lencamin.DE_DIAGN,
                               //// CID - CÓDIGO INTERNACIONAL DE DOENÇA ATRIBUÍDO AO PACIENTE
                               //coCID1Paci = coCid.IDE_CID,
                               //coCID2Paci = coCid.CO_CID,
                               //coCID3Paci = coCid.CO_CID,
                               //deCID1Paci = coCid.NO_CID,
                               //deCID2Paci = coCid.NO_CID,
                               //deCID3Paci = coCid.NO_CID,

                               //// //ENCAMINHAMENTOS
                               //deEncamPaci = lencamin.CO_ENCAM_MEDIC,

                               //// //PRESCRIÇÃO DE MEDICAMENTOS
                               //deMedicPaci = coAtendServicoAmbu.CO_ALU,

                               //// //PRESCRIÇÃO DE EXAMES
                               //deExamePaci = lencamin.NU_REGIS_CONSUL,

                               //////SERVIÇOS AMBULATORIAIS
                               //deSerAmbPaci = lencamin.DE_DIAGN,
                               //// //OBSERVAÇÃO
                               //obsAtendPaci = lencamin.DE_DIAGN,
                               ////ANOTAÇÃO
                               //EmiRecePaci = lencamin.DE_DIAGN,
                               //AteDispPaci = lencamin.DE_DIAGN,
                               //ResMediPaci = lencamin.DE_DIAGN,
                           }).ToList();


                var dados = res.FirstOrDefault();

                if (dados == null)
                    return -1;



                #endregion

                var lst = (from tb009 in TB009_RTF_DOCTOS.RetornaTodosRegistros()
                           join tb010 in TB010_RTF_ARQUIVO.RetornaTodosRegistros() on tb009.ID_DOCUM equals tb010.TB009_RTF_DOCTOS.ID_DOCUM
                           //from tb010 in tb009.TB010_RTF_ARQUIVO.DefaultIfEmpty()
                           where tb009.CO_SIGLA_DOCUM == "DCTFA"
                           select new GuiaConsultas
                           {
                               Pagina = tb010.NU_PAGINA,
                               Titulo = tb009.NM_TITUL_DOCUM,
                               Texto = tb010.AR_DADOS,
                               Fl_Hidelogo = tb009.FL_HIDELOGO
                           }).OrderBy(x => x.Pagina);

                if (lst == null)
                    return -1;

                if (lst != null && lst.Where(x => x.Pagina == 1).Any())
                {
                    foreach (var Doc in lst)
                    {
                        if (lst.FirstOrDefault().Fl_Hidelogo == "N")
                        {
                            MostrarCabecalho(false);
                            GroupHeaderTitulo.Visible = false;
                        }
                        else
                        {
                            MostrarCabecalho(true);
                            GroupHeaderTitulo.Visible = true;
                        }

                        SerializableString st = new SerializableString(Doc.Texto);
                        //Topo
                        st.Value = st.Value.Replace("[NumeDirec]", dados.NumeDirec);
                        st.Value = st.Value.Replace("[UnidDirec]", dados.UnidDirec != "" ? dados.UnidDirec : "-");
                        st.Value = st.Value.Replace("[DataDirec]", (dados.DataDirec.HasValue ? dados.DataDirec.Value.ToString("dd/MM/yyyy") : " - "));
                        st.Value = st.Value.Replace("[UnRefPaci]", dados.empOrigemPaci);
                        //Dados do Paciente
                        st.Value = st.Value.Replace("[nomePaci]", dados.NomeDoPaciente != "" ? dados.NomeDoPaciente : "-");
                        st.Value = st.Value.Replace("[sexoPaci]", (dados.Sexo == "M" ? "Mas" : "Fem"));
                        st.Value = st.Value.Replace("[dtNascPaci]", (dados.NascimentoPa.HasValue ? dados.NascimentoPa.Value.ToString("dd/MM/yyyy") : " - "));
                        st.Value = st.Value.Replace("[idadePaci]", (dados.NascimentoPa.HasValue ? Funcoes.FormataDataNascimento(dados.NascimentoPa.Value) : "-"));
                        st.Value = st.Value.Replace("[nirePaci]", Convert.ToString(dados.NProntuario));
                        st.Value = st.Value.Replace("[noEtniaPaci]", Funcoes.RetornaEtnia(dados.Etnia));
                        st.Value = st.Value.Replace("[DeficiPaci]", Funcoes.RetornaDeficiencia(dados.Deficiencia));
                        st.Value = st.Value.Replace("[NmMaePaci]", dados.NomeDaMae != "" ? dados.NomeDaMae : "-");
                        st.Value = st.Value.Replace("[nuTelFixPaci]", dados.NFoneFixo != "" ? Funcoes.Format(dados.NFoneFixo, TipoFormat.Telefone) : "-");
                        st.Value = st.Value.Replace("[nuTelCelPaci]", dados.NCelular != "" ? Funcoes.Format(dados.NCelular, TipoFormat.Telefone) : "-");
                        st.Value = st.Value.Replace("[nisPaci]", Convert.ToString(dados.Nis != 0 ? dados.Nis : 0));
                        st.Value = st.Value.Replace("[noGrauPar]", Funcoes.RetornaGrauParentesco(dados.grauParentesco));

                        //Dados do Responsável
                        st.Value = st.Value.Replace("[noResp]", dados.NomeDoResponsavel != "" ? dados.NomeDoResponsavel : "-");
                        st.Value = st.Value.Replace("[sexoResp]", (dados.SexoRep != "M" ? "Mas" : "Fem"));
                        st.Value = st.Value.Replace("[noGrauPar]", CarregaGrauParentesco(dados.Parentesco != "" ? dados.Parentesco : "-"));
                        st.Value = st.Value.Replace("[nuCpfResp]", Funcoes.Format(dados.NCPF, TipoFormat.CPF));
                        st.Value = st.Value.Replace("[EndLogPaci]", (string.Format("{0}, {1}, {2}-{3}",dados.logradouroAlu, dados.bairroAlu, dados.cidadeAlu, dados.ufAlu)));
                        st.Value = st.Value.Replace("[nuTelFixResp]", dados.NFoneFixoRep != "" ? Funcoes.Format(dados.NFoneFixoRep, TipoFormat.Telefone) : "-");
                        st.Value = st.Value.Replace("[EmailResp]", dados.EmailResp != "" ? dados.EmailResp : "-");
                        st.Value = st.Value.Replace("[nuTelCelResp]", dados.NCelularResp != "" ? Funcoes.Format(dados.NCelularResp, TipoFormat.Telefone) : "-");
                        st.Value = st.Value.Replace("[nuWhatsResp]", dados.NWhatsAppResp != "" ? Funcoes.Format(dados.NWhatsAppResp, TipoFormat.Telefone) : "-");
                        st.Value = st.Value.Replace("[nuNisResp]", Convert.ToString(dados.NisResp != 0 ? dados.NisResp : 0));

                        //INFORMAÇÕES DO PRÉ-ATENDIMENTO
                        st.Value = st.Value.Replace("[MatColabCad]", "-");
                        st.Value = st.Value.Replace("[noColabCad]", "-");
                        st.Value = st.Value.Replace("[nuPreAtend]", "-");
                        st.Value = st.Value.Replace("[dtPreAtend]", "-");
                        st.Value = st.Value.Replace("[hrPreAtend]", "-");
                        st.Value = st.Value.Replace("[noClassRisco]", "-");
                        //DADOS DE LEITURA E INFORMAÇÃO BÁSICA                     
                        st.Value = st.Value.Replace("[nuAltura]", "-");
                        st.Value = st.Value.Replace("[nuPeso]", "-");
                        st.Value = st.Value.Replace("[nuPressao]", "-");
                        st.Value = st.Value.Replace("[hrPressao]", "-");
                        st.Value = st.Value.Replace("[temper]", "-");
                        st.Value = st.Value.Replace("[hrTemper]", "-");
                        st.Value = st.Value.Replace("[Glicem]", "-");
                        st.Value = st.Value.Replace("[hrGlicem]", "-");
                        st.Value = st.Value.Replace("[flEnjoos]", "-");
                        st.Value = st.Value.Replace("[flVomitos]", "-");
                        st.Value = st.Value.Replace("[flDores]", "-");
                        st.Value = st.Value.Replace("[dtDores]", "-");
                        st.Value = st.Value.Replace("[hrDores]", "-");
                        //DADOS DE REGISTRO DE RISCO
                        st.Value = st.Value.Replace("[flDiabetes]", "-");
                        st.Value = st.Value.Replace("[deDiabetes]", "-");
                        st.Value = st.Value.Replace("[flHiperten]", "-");
                        st.Value = st.Value.Replace("[deHiperten]", "-");
                        st.Value = st.Value.Replace("[flFumante]", "-");
                        st.Value = st.Value.Replace("[tpFumante]", "-");
                        st.Value = st.Value.Replace("[flAlcool]", "-");
                        st.Value = st.Value.Replace("[tpAlcool]", "-");
                        st.Value = st.Value.Replace("[flCirurgia]", "-");
                        st.Value = st.Value.Replace("[deCirurgia]", "-");
                        st.Value = st.Value.Replace("[flMarcaPa]", "-");
                        st.Value = st.Value.Replace("[deMarcaPa]", "-");
                        st.Value = st.Value.Replace("[flValvula]", "-");
                        st.Value = st.Value.Replace("[flAlergia]", "-");
                        st.Value = st.Value.Replace("[deAlergia]", "-");



                        ////MEDICAÇÃO DE USO CONTÍNUO
                        st.Value = st.Value.Replace("[deMedicUsoC]", "-");
                        st.Value = st.Value.Replace("[deMedic]", "-");
                        //DESCRIÇÃO DOS SINTOMAS INFORMADOS PELO PACIENTE E/OU RESPONSÁVEL
                        st.Value = st.Value.Replace("[deSintomas]", "-");
                        ////INFORMAÇÕES DO ATENDIMENTO MÉDICO
                        st.Value = st.Value.Replace("[coAtendMedic]", "-");
                        st.Value = st.Value.Replace("[dtAtendMedic]", "-");
                        st.Value = st.Value.Replace("[hrFinMedic]", "-");
                        st.Value = st.Value.Replace("[noProfi]", "-");
                        st.Value = st.Value.Replace("[deEntiPro]", "-");
                        st.Value = st.Value.Replace("[EspecProfiSaude]", "-");
                        st.Value = st.Value.Replace("[deLocAtenPro]", "-");
                        ////ANAMNESE
                        st.Value = st.Value.Replace("[desAnamPaci]", "-");
                        ////DIAGNÓSTICO
                        st.Value = st.Value.Replace("[desDiagPaci]", "-");
                        // CID - CÓDIGO INTERNACIONAL DE DOENÇA ATRIBUÍDO AO PACIENTE
                        st.Value = st.Value.Replace("[coCID1Paci]", "-");
                        st.Value = st.Value.Replace("[coCID2Paci]", "-");
                        st.Value = st.Value.Replace("[coCID3Paci]", "-");
                        st.Value = st.Value.Replace("[deCID1Paci]", "-");
                        st.Value = st.Value.Replace("[deCID2Paci]", "-");
                        st.Value = st.Value.Replace("[deCID3Paci]", "-");
                        ////ENCAMINHAMENTOS


                        st.Value = st.Value.Replace("[deEncamPaci]", "-");

                        ////PRESCRIÇÃO DE MEDICAMENTOS



                        st.Value = st.Value.Replace("[deMedicPaci]", "-");
                        ////PRESCRIÇÃO DE EXAMES


                        st.Value = st.Value.Replace("[deExamePaci]", "-");
                        ////SERVIÇOS AMBULATORIAIS
                        st.Value = st.Value.Replace("[deSerAmbPaci]", "-");
                        ////OBSERVAÇÃO
                        st.Value = st.Value.Replace("[obsAtendPaci]", "-");
                        ////ANOTAÇÃO
                        st.Value = st.Value.Replace("[EmiRecePaci]", "-");
                        st.Value = st.Value.Replace("[AteDispPaci]", "-");
                        st.Value = st.Value.Replace("[ResMediPaci]", "-");

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

        #endregion

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
            public string Fl_Hidelogo { get; set; }
            public bool HideLogo { get; set; }
            public string Titulo { get; set; }
            public string SubTitulo { get; set; }
            public int Pagina { get; set; }
            public string Texto { get; set; }

        }

        //private DevExpress.XtraReports.UI.XRRichText richPagina;
        //private DevExpress.XtraReports.UI.XRRichText richPagina3;
        //private DevExpress.XtraReports.UI.XRRichText richPagina1;
    }
}
