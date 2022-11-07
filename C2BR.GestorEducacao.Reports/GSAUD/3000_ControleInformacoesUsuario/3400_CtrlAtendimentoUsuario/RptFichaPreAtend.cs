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
    public partial class RptFichaPreAtend : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptFichaPreAtend()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                        string infos,
                        int coEmp,
                       int id_pre_atend,
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
                {
                    lblTitulo.Text = "";
                }
                else
                {
                    lblTitulo.Text = NomeFuncionalidadeCadastrada;
                }
                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;
                #region Query

                var res = (from tbs194 in TBS194_PRE_ATEND.RetornaTodosRegistros()
                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs194.CO_COL_FUNC equals tb03.CO_COL
                           join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs194.CO_ALU equals tb07.CO_ALU
                           join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb07.CO_EMP_ORIGEM equals tb25.CO_EMP into lea
                           from lempOrgPac in lea.DefaultIfEmpty()
                           join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs194.CO_EMP equals tb25.CO_EMP
                           join tb905 in TB905_BAIRRO.RetornaTodosRegistros() on tb07.TB905_BAIRRO.CO_BAIRRO equals tb905.CO_BAIRRO into l1
                           from lbar in l1.DefaultIfEmpty()
                           join tb904 in TB904_CIDADE.RetornaTodosRegistros() on lbar.CO_CIDADE equals tb904.CO_CIDADE into l2
                           from lcid in l2.DefaultIfEmpty()

                           where (id_pre_atend != 0 ? tbs194.ID_PRE_ATEND == id_pre_atend : 0 == 0)
                           select new
                           {
                               //Topo 
                               NumeDirec = tbs194.CO_ENCAM_MEDIC,
                               UnidDirec = tb25.NO_FANTAS_EMP,
                               DataDirec = tbs194.DT_PRE_ATEND,
                               
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
                               MatColabCad = tb03.CO_MAT_COL,
                               noColabCad = tb03.NO_COL,
                               nuPreAtend = tbs194.CO_PRE_ATEND,
                               dtPreAtend = tbs194.DT_PRE_ATEND,
                               hrPreAtend = tbs194.DT_PRE_ATEND,
                               noClassRisco = tbs194.CO_TIPO_RISCO,
                               //DADOS DE LEITURA E INFORMAÇÃO BÁSICA
                               Altura = tbs194.NU_ALTU,
                               Peso = tbs194.NU_PESO,
                               NuPressao = tbs194.NU_PRES_ARTE,
                               HoraPressao = tbs194.HR_PRES_ARTE,
                               temperatura = tbs194.NU_TEMP,
                               HoraTemperatura = tbs194.HR_TEMP,
                               TaxaGlicemica = tbs194.NU_GLICE,
                               HoraTaxaGlicemica = tbs194.HR_GLICE,
                               TeveEnjoo = tbs194.FL_SINTO_ENJOO,
                               Vomitos = tbs194.FL_SINTO_VOMIT,
                               TeveDores = tbs194.FL_SINTO_DORES,
                               DataTeveDores = tbs194.DT_DOR,
                               HoraTeveDores = tbs194.HR_DOR,
                               //DADOS DE REGISTRO DE RISCO
                               Diabetes = tbs194.FL_DIABE,
                               DeDiabetes = tbs194.DE_DIABE,
                               flHipertensao = tbs194.FL_HIPER_TENSO,
                               Hipertensao = tbs194.DE_HIPER_TENSO,
                               Fumante = tbs194.FL_FUMAN,
                               TempoFumante = tbs194.NU_TEMPO_FUMAN,
                               UsuarioDeAlcool = tbs194.FL_ALCOO,
                               TempoUsuarioDeAlcool = tbs194.NU_TEMPO_ALCOO,
                               Cirurgia = tbs194.FL_CIRUR,
                               DeCirurgia = tbs194.DE_CIRUR,
                               MarcaPasso = tbs194.FL_MARCA_PASSO,
                               DMarcaPasso = tbs194.DE_MARCA_PASSO,
                               Valvula = tbs194.FL_VALVU,
                               deValvula = tbs194.DE_VALVU,
                               Alergia = tbs194.FL_ALERG,
                               DeAlergia = tbs194.DE_ALERG,
                               //MEDICAÇÃO DE USO CONTÍNUO
                               deMedicUsoC = tbs194.DE_MEDIC_USO_CONTI,
                               deMedic = tbs194.DE_MEDIC,
                               //DESCRIÇÃO DOS SINTOMAS INFORMADOS PELO PACIENTE E/OU RESPONSÁVEL
                               DeSintomas = tbs194.DE_SINTO,


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
                        st.Value = st.Value.Replace("[DataDirec]", dados.DataDirec.ToString("dd/MM/yyyy"));
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
                        st.Value = st.Value.Replace("[EndLogPaci]", (string.Format("{0}, {1}, {2}-{3}", dados.logradouroAlu, dados.bairroAlu, dados.cidadeAlu, dados.ufAlu)));
                        st.Value = st.Value.Replace("[nuTelFixResp]", dados.NFoneFixoRep != "" ? Funcoes.Format(dados.NFoneFixoRep, TipoFormat.Telefone) : "-");
                        st.Value = st.Value.Replace("[EmailResp]", dados.EmailResp != "" ? dados.EmailResp : "-");
                        st.Value = st.Value.Replace("[nuTelCelResp]", dados.NCelularResp != "" ? Funcoes.Format(dados.NCelularResp, TipoFormat.Telefone) : "-");
                        st.Value = st.Value.Replace("[nuWhatsResp]", dados.NWhatsAppResp != "" ? Funcoes.Format(dados.NWhatsAppResp, TipoFormat.Telefone) : "-");
                        st.Value = st.Value.Replace("[nuNisResp]", Convert.ToString(dados.NisResp != 0 ? dados.NisResp : 0));

                        //INFORMAÇÕES DO PRÉ-ATENDIMENTO
                        st.Value = st.Value.Replace("[MatColabCad]", Funcoes.Format(dados.MatColabCad, TipoFormat.MatriculaColaborador));
                        st.Value = st.Value.Replace("[noColabCad]", dados.noColabCad);
                        st.Value = st.Value.Replace("[nuPreAtend]", Funcoes.TrataCodigoRegistroSaude(dados.nuPreAtend));
                        st.Value = st.Value.Replace("[dtPreAtend]", dados.dtPreAtend.ToString("dd/MM/yyyy"));
                        st.Value = st.Value.Replace("[hrPreAtend]", dados.dtPreAtend.ToShortTimeString());
                        st.Value = st.Value.Replace("[noClassRisco]", Funcoes.GetNomeClassificacaoRisco(dados.noClassRisco));
                        //DADOS DE LEITURA E INFORMAÇÃO BÁSICA                     
                        st.Value = st.Value.Replace("[nuAltura]", (dados.Altura.HasValue ? dados.Altura.Value.ToString("N1") : " - "));
                        st.Value = st.Value.Replace("[nuPeso]", (dados.Peso.HasValue ? dados.Peso.Value.ToString("N1") : " - "));
                        st.Value = st.Value.Replace("[nuPressao]", (!string.IsNullOrEmpty(dados.NuPressao) ? dados.NuPressao : " - "));
                        st.Value = st.Value.Replace("[hrPressao]", (!string.IsNullOrEmpty(dados.HoraPressao) ? dados.HoraPressao : " - "));
                        st.Value = st.Value.Replace("[temper]", (dados.temperatura.HasValue ? dados.temperatura.Value.ToString("N2") : " - "));
                        st.Value = st.Value.Replace("[hrTemper]", (!string.IsNullOrEmpty(dados.HoraTemperatura) ? dados.HoraTemperatura : " - "));
                        st.Value = st.Value.Replace("[Glicem]", (dados.TaxaGlicemica.HasValue ? dados.TaxaGlicemica.Value.ToString() : " - "));
                        st.Value = st.Value.Replace("[hrGlicem]", (!string.IsNullOrEmpty(dados.HoraTaxaGlicemica) ? dados.HoraTaxaGlicemica : " - "));
                        st.Value = st.Value.Replace("[flEnjoos]", (dados.TeveEnjoo == "S" ? "Sim" : "Não"));
                        st.Value = st.Value.Replace("[flVomitos]", (dados.Vomitos == "S" ? "Sim" : "Não"));
                        st.Value = st.Value.Replace("[flDores]", (dados.TeveDores == "S" ? "Sim" : "Não"));
                        st.Value = st.Value.Replace("[dtDores]", (dados.DataTeveDores.HasValue ? dados.DataTeveDores.Value.ToShortDateString() : " - "));
                        st.Value = st.Value.Replace("[hrDores]", (!string.IsNullOrEmpty(dados.HoraTeveDores) ? dados.HoraTeveDores : " - "));
                        //DADOS DE REGISTRO DE RISCO
                        st.Value = st.Value.Replace("[flDiabetes]", (dados.Diabetes == "S" ? "Sim" : "Não"));
                        st.Value = st.Value.Replace("[deDiabetes]", (dados.DeDiabetes == "1" ? "Tipo 1" : dados.DeDiabetes == "2" ? "Tipo 2" : " - "));
                        st.Value = st.Value.Replace("[flHiperten]", (dados.flHipertensao == "S" ? "Sim" : "Não"));
                        st.Value = st.Value.Replace("[deHiperten]", (!string.IsNullOrEmpty(dados.Hipertensao) ? dados.Hipertensao : " - "));
                        st.Value = st.Value.Replace("[flFumante]", (dados.Fumante == "S" ? "Sim" : "Não"));
                        st.Value = st.Value.Replace("[tpFumante]", (dados.TempoFumante.HasValue ? dados.TempoFumante.Value.ToString() : " - "));
                        st.Value = st.Value.Replace("[flAlcool]", (dados.UsuarioDeAlcool == "S" ? "Sim" : "Não"));
                        st.Value = st.Value.Replace("[tpAlcool]", (dados.TempoUsuarioDeAlcool.HasValue ? dados.TempoUsuarioDeAlcool.ToString() : " - "));
                        st.Value = st.Value.Replace("[flCirurgia]", (dados.Cirurgia == "S" ? "Sim" : "Não"));
                        st.Value = st.Value.Replace("[deCirurgia]", (!string.IsNullOrEmpty(dados.DeCirurgia) ? dados.DeCirurgia : " - "));
                        st.Value = st.Value.Replace("[flMarcaPa]", (dados.MarcaPasso == "S" ? "Sim" : "Não"));
                        st.Value = st.Value.Replace("[deMarcaPa]", (!string.IsNullOrEmpty(dados.DMarcaPasso) ? dados.DMarcaPasso : " - "));
                        st.Value = st.Value.Replace("[flValvula]", (dados.Valvula == "S" ? "Sim" : "Não"));
                        st.Value = st.Value.Replace("[deValvula]", (!string.IsNullOrEmpty(dados.deValvula) ? dados.deValvula : " - "));
                        st.Value = st.Value.Replace("[flAlergia]", (dados.Alergia == "S" ? "Sim" : "Não"));
                        st.Value = st.Value.Replace("[deAlergia]", (!string.IsNullOrEmpty(dados.DeAlergia) ? dados.DeAlergia: " - "));
                        ////MEDICAÇÃO DE USO CONTÍNUO
                        st.Value = st.Value.Replace("[deMedicUsoC]", (!string.IsNullOrEmpty(dados.deMedicUsoC) ? dados.deMedicUsoC : " - "));
                        st.Value = st.Value.Replace("[deMedic]", (!string.IsNullOrEmpty(dados.deMedic) ? dados.deMedic : " - "));
                        //DESCRIÇÃO DOS SINTOMAS INFORMADOS PELO PACIENTE E/OU RESPONSÁVEL
                        st.Value = st.Value.Replace("[deSintomas]", (!string.IsNullOrEmpty(dados.DeSintomas) ? dados.DeSintomas : " - "));
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
    }
}
