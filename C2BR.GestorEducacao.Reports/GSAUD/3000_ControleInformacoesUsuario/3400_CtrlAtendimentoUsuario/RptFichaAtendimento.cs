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
using System.Text;

namespace C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3400_CtrlAtendimentoUsuario
{
    public partial class RptFichaAtendimento : C2BR.GestorEducacao.Reports.RptRetrato
    {
        public RptFichaAtendimento()
        {
            InitializeComponent();
        }

        #region Init Report

        public int InitReport(string parametros,
                        string infos,
                        int coEmp,
                        int IdAtendimentoMedido
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

                #endregion

                // Instancia o contexto
                var ctx = GestorEntities.CurrentContext;
                #region Query

                var res = (from tbs219 in TBS219_ATEND_MEDIC.RetornaTodosRegistros()
                           //Coleta o Colaborador que fez o atendimento
                           join tb03MA in TB03_COLABOR.RetornaTodosRegistros() on tbs219.CO_COL equals tb03MA.CO_COL
                           join tb63MA in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tb03MA.CO_ESPEC equals tb63MA.CO_ESPECIALIDADE into espColMA
                           from lespColMA in espColMA.DefaultIfEmpty()
                           join tb14MA in TB14_DEPTO.RetornaTodosRegistros() on tb03MA.CO_DEPTO equals tb14MA.CO_DEPTO into depColMA
                           from lDptoColMA in depColMA.DefaultIfEmpty()
                           //Coleta unidade do atendimento
                           join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs219.CO_EMP equals tb25.CO_EMP
                           //Coleta paciente do atendimento
                           join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs219.CO_ALU equals tb07.CO_ALU
                           //Coleta encaminhamento do atendimento caso haja
                           join tbs195 in TBS195_ENCAM_MEDIC.RetornaTodosRegistros() on tbs219.ID_ENCAM_MEDIC equals tbs195.ID_ENCAM_MEDIC into lenc
                           from lencaminhamento in lenc.DefaultIfEmpty()
                           //Coleta Unidade do Encaminhamento caso haja
                           //join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on lencaminhamento.CO_EMP_ENCAM_MEDIC equals tb25.CO_EMP into leempEncam
                           //from lempEncaminhamento in leempEncam.DefaultIfEmpty()
                           //Coleta bairro do paciente caso haja
                           join tb905 in TB905_BAIRRO.RetornaTodosRegistros() on tb07.TB905_BAIRRO.CO_BAIRRO equals tb905.CO_BAIRRO into l1
                           from lbar in l1.DefaultIfEmpty()
                           //Coleta cidade do paciente caso haja
                           join tb904 in TB904_CIDADE.RetornaTodosRegistros() on lbar.CO_CIDADE equals tb904.CO_CIDADE into l2
                           from lcid in l2.DefaultIfEmpty()
                           //Coleta unidade de referência do paciente caso haja
                           //join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb07.CO_EMP_ORIGEM equals tb25.CO_EMP into lea
                           //from lempOrgPac in lea.DefaultIfEmpty()
                           //Coleta Acolhimento caso haja
                           join tbs194 in TBS194_PRE_ATEND.RetornaTodosRegistros() on tbs219.TBS194_PRE_ATEND.ID_PRE_ATEND equals tbs194.ID_PRE_ATEND into PreAtemd
                           from lacolhi in PreAtemd.DefaultIfEmpty()
                           //Coleta Colaborador responsável pelo acolhimento caso haja
                           join tb03 in TB03_COLABOR.RetornaTodosRegistros() on lacolhi.CO_COL_FUNC equals tb03.CO_COL into col
                           from colabPrAtend in col.DefaultIfEmpty()
                           //Coleta exames requisitados caso haja
                           join tb218 in TBS218_EXAME_MEDICO.RetornaTodosRegistros() on tbs219.ID_ATEND_MEDIC equals tb218.ID_ATEND_MEDIC into ExameMedico
                           from coExameMedico in ExameMedico.DefaultIfEmpty()
                           //Coleta Serviços Ambulatoriais requisitados caso haja
                           join tbs332 in TBS332_ATEND_SERV_AMBUL.RetornaTodosRegistros() on tbs219.ID_ATEND_MEDIC equals tbs332.ID_ATEND_MEDIC into AtendServicoAmbu
                           from coAtendServicoAmbu in AtendServicoAmbu.DefaultIfEmpty()
                           //Coleta receitas médicas caso haja
                           join tbs332 in TBS330_RECEI_ATEND_MEDIC.RetornaTodosRegistros() on tbs219.ID_ATEND_MEDIC equals tbs332.ID_ATEND_MEDIC into Recei
                           from coRecei in Recei.DefaultIfEmpty()
                           //Coleta CID 1 caso haja
                           join tb1171 in TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaTodosRegistros() on tbs219.IDE_CID equals tb1171.IDE_CID into cid1
                           from coCid1 in cid1.DefaultIfEmpty()
                           //Coleta CID 2 caso haja
                           join tb1172 in TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaTodosRegistros() on tbs219.TB117_CODIGO_INTERNACIONAL_DOENCA.IDE_CID equals tb1172.IDE_CID into cid2
                           from coCid2 in cid2.DefaultIfEmpty()
                           //Coleta CID 3 caso haja
                           join tb1173 in TB117_CODIGO_INTERNACIONAL_DOENCA.RetornaTodosRegistros() on tbs219.TB117_CODIGO_INTERNACIONAL_DOENCA1.IDE_CID equals tb1173.IDE_CID into cid3
                           from coCid3 in cid3.DefaultIfEmpty()
                           where tbs219.ID_ATEND_MEDIC == IdAtendimentoMedido                    
                           select new
                           {
                               //Topo 
                               NumeDirec = lencaminhamento.CO_ENCAM_MEDIC,
                               UnidDirec = "",//lempEncaminhamento.NO_FANTAS_EMP,
                               DataDirec = lencaminhamento.DT_ENCAM_MEDIC,

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
                               empOrigemPaci = "",//(lempOrgPac != null ? lempOrgPac.NO_FANTAS_EMP : " - "),
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
                               MatColabCad = colabPrAtend.CO_MAT_COL,
                               noColabCad = colabPrAtend.NO_COL,
                               nuPreAtend = lacolhi.CO_PRE_ATEND,
                               dtPreAtend = lacolhi.DT_PRE_ATEND,
                               hrPreAtend = lacolhi.DT_PRE_ATEND,
                               noClassRisco = lacolhi.CO_TIPO_RISCO,
                               //DADOS DE LEITURA E INFORMAÇÃO BÁSICA
                               Altura = lacolhi.NU_ALTU,
                               Peso = lacolhi.NU_PESO,
                               NuPressao = lacolhi.NU_PRES_ARTE,
                               HoraPressao = lacolhi.HR_PRES_ARTE,
                               temperatura = lacolhi.NU_TEMP,
                               HoraTemperatura = lacolhi.HR_TEMP,
                               TaxaGlicemica = lacolhi.NU_GLICE,
                               HoraTaxaGlicemica = lacolhi.HR_GLICE,
                               TeveEnjoo = lacolhi.FL_SINTO_ENJOO,
                               Vomitos = lacolhi.FL_SINTO_VOMIT,
                               TeveDores = lacolhi.FL_SINTO_DORES,
                               DataTeveDores = lacolhi.DT_DOR,
                               HoraTeveDores = lacolhi.HR_DOR,
                               //DADOS DE REGISTRO DE RISCO
                               Diabetes = lacolhi.FL_DIABE,
                               DeDiabetes = lacolhi.DE_DIABE,
                               flHipertensao = lacolhi.FL_HIPER_TENSO,
                               Hipertensao = lacolhi.DE_HIPER_TENSO,
                               Fumante = lacolhi.FL_FUMAN,
                               TempoFumante = lacolhi.NU_TEMPO_FUMAN,
                               UsuarioDeAlcool = lacolhi.FL_ALCOO,
                               TempoUsuarioDeAlcool = lacolhi.NU_TEMPO_ALCOO,
                               Cirurgia = lacolhi.FL_CIRUR,
                               DeCirurgia = lacolhi.DE_CIRUR,
                               MarcaPasso = lacolhi.FL_MARCA_PASSO,
                               DMarcaPasso = lacolhi.DE_MARCA_PASSO,
                               Valvula = lacolhi.FL_VALVU,
                               deValvula = lacolhi.DE_VALVU,
                               Alergia = lacolhi.FL_ALERG,
                               DeAlergia = lacolhi.DE_ALERG,
                               //MEDICAÇÃO DE USO CONTÍNUO
                               deMedicUsoC = lacolhi.DE_MEDIC_USO_CONTI,
                               deMedic = lacolhi.DE_MEDIC,
                               //DESCRIÇÃO DOS SINTOMAS INFORMADOS PELO PACIENTE E/OU RESPONSÁVEL
                               DeSintomas = lacolhi.DE_SINTO,

                               //INFORMAÇÕES DO ATENDIMENTO MÉDICO
                               coAtendMedic = tbs219.CO_ATEND_MEDIC,
                               dtAtendMedic = tbs219.DT_ATEND_MEDIC,
                               noProfi = tb03MA.NO_COL,
                               entidProfiCol = tb03MA.CO_SIGLA_ENTID_PROFI,
                               ufEntidProfi = tb03MA.CO_UF_ENTID_PROFI,
                               nuEntidProfi = tb03MA.NU_ENTID_PROFI,
                               NomeEntiPro = lacolhi.FL_ALERG,

                               EspecProfiSaude = lespColMA.NO_ESPECIALIDADE,
                               deLocAtenPro = lDptoColMA.NO_DEPTO,

                               //ANAMNESE
                               DesAnamPaci = tbs219.DE_ANAMN,
                               //DIAGNÓSTICO
                               desDiagPaci = tbs219.DE_DIAGN,
                               // CID - CÓDIGO INTERNACIONAL DE DOENÇA ATRIBUÍDO AO PACIENTE
                               coCID1Paci = coCid1.CO_CID,
                               coCID2Paci = coCid2.CO_CID,
                               coCID3Paci = coCid3.CO_CID,
                               deCID1Paci = coCid1.NO_CID,
                               deCID2Paci = coCid2.NO_CID,
                               deCID3Paci = coCid3.NO_CID,

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
                               Fl_Hidelogo = tb009.FL_HIDELOGO,
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
                        st.Value = st.Value.Replace("[deAlergia]", (!string.IsNullOrEmpty(dados.DeAlergia) ? dados.DeAlergia : " - "));
                        ////MEDICAÇÃO DE USO CONTÍNUO
                        st.Value = st.Value.Replace("[deMedicUsoC]", (!string.IsNullOrEmpty(dados.deMedicUsoC) ? dados.deMedicUsoC : " - "));
                        st.Value = st.Value.Replace("[deMedic]", (!string.IsNullOrEmpty(dados.deMedic) ? dados.deMedic : " - "));
                        //DESCRIÇÃO DOS SINTOMAS INFORMADOS PELO PACIENTE E/OU RESPONSÁVEL
                        st.Value = st.Value.Replace("[deSintomas]", (!string.IsNullOrEmpty(dados.DeSintomas) ? dados.DeSintomas : " - "));

                        ////INFORMAÇÕES DO ATENDIMENTO MÉDICO
                        st.Value = st.Value.Replace("[coAtendMedic]", Funcoes.TrataCodigoRegistroSaude(dados.coAtendMedic));
                        st.Value = st.Value.Replace("[dtAtendMedic]", dados.dtAtendMedic.ToString("dd/MM/yyyy"));
                        st.Value = st.Value.Replace("[hrIniMedic]", dados.dtAtendMedic.ToString("HH:mm"));
                        st.Value = st.Value.Replace("[hrFinMedic]", dados.dtAtendMedic.ToString("HH:mm"));
                        st.Value = st.Value.Replace("[noProfi]", dados.noProfi);
                        st.Value = st.Value.Replace("[deEntiPro]", (!string.IsNullOrEmpty(dados.entidProfiCol) ? dados.entidProfiCol + " " + dados.nuEntidProfi + " - " + dados.ufEntidProfi : ""));
                        st.Value = st.Value.Replace("[EspecProfiSaude]", (!string.IsNullOrEmpty(dados.EspecProfiSaude) ? dados.EspecProfiSaude : " - "));
                        st.Value = st.Value.Replace("[deLocAtenPro]", (!string.IsNullOrEmpty(dados.deLocAtenPro) ? dados.deLocAtenPro : " - "));
                        ////ANAMNESE
                        st.Value = st.Value.Replace("[desAnamPaci]", (!string.IsNullOrEmpty(dados.DesAnamPaci) ? dados.DesAnamPaci : " - "));
                        ////DIAGNÓSTICO
                        st.Value = st.Value.Replace("[desDiagPaci]", (!string.IsNullOrEmpty(dados.desDiagPaci) ? dados.desDiagPaci : " - "));

                        // CID - CÓDIGO INTERNACIONAL DE DOENÇA ATRIBUÍDO AO PACIENTE
                        st.Value = st.Value.Replace("[coCID1Paci]", dados.coCID1Paci);
                        st.Value = st.Value.Replace("[coCID2Paci]", dados.coCID2Paci);
                        st.Value = st.Value.Replace("[coCID3Paci]", dados.coCID3Paci);
                        st.Value = st.Value.Replace("[deCID1Paci]", dados.deCID1Paci);
                        st.Value = st.Value.Replace("[deCID2Paci]", dados.deCID2Paci);
                        st.Value = st.Value.Replace("[deCID3Paci]", dados.deCID3Paci);

                        ////ENCAMINHAMENTOS
                        st.Value = st.Value.Replace("[deEncamPaci]", "-");

                        ////PRESCRIÇÃO DE MEDICAMENTOS
                        #region Concatena infos Prescrições
                        StringBuilder sbrm = new StringBuilder();
                        string medicamentos = "";
                        int auxResRec = 0;

                        var resRec = TBS330_RECEI_ATEND_MEDIC.RetornaPeloIDAtendimento(IdAtendimentoMedido);
                        foreach (var rr in resRec)
                        {
                            auxResRec++;
                            string Nome = TB90_PRODUTO.RetornaTodosRegistros().Where(w=>w.CO_PROD == rr.CO_MEDIC).FirstOrDefault().NO_PROD;
                            string uso = (rr.QT_USO == 0 ? "USO CONTÍNUO" : "USO POR " + rr.QT_USO + " DIAS.");

                            //Concatena informações
                            sbrm.Append(string.Format("{3}( {0} - Princípio Ativo: {1} - Uso: {2} )",Nome, rr.DE_PRINC_ATIVO, uso, (auxResRec > 1 ? " ### " : "")));
                        }
                        medicamentos = sbrm.ToString();

                        #endregion
                        st.Value = st.Value.Replace("[deMedicPaci]", medicamentos);

                        ////PRESCRIÇÃO DE EXAMES
                        #region Concatena infos Exames
                        StringBuilder sbex = new StringBuilder();
                        string Exames = "";
                        int auxResExa = 0;

                        var resExa = TBS218_EXAME_MEDICO.RetornaPeloIDAtendimento(IdAtendimentoMedido);
                        foreach (var rr in resExa)
                        {
                            auxResExa++;
                            rr.TBS356_PROC_MEDIC_PROCEReference.Load();
                            rr.TB14_DEPTOReference.Load();

                            //Concatena informações
                            sbex.Append(string.Format("{2}( {0} - Local: {1} )", rr.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI, rr.TB14_DEPTO.NO_DEPTO, (auxResExa > 1 ? " ### " : "")));
                        }
                        Exames = sbex.ToString();

                        #endregion
                        st.Value = st.Value.Replace("[deExamePaci]", Exames);
                        
                        ////SERVIÇOS AMBULATORIAIS
                        #region Concatena infos Serviços Ambulatoriais
                        StringBuilder sbsa = new StringBuilder();
                        string ServAmbu = "";
                        int auxResServ = 0;

                        var resServ = TBS332_ATEND_SERV_AMBUL.RetornaPeloIDAtendimento(IdAtendimentoMedido);
                        foreach (var rr in resServ)
                        {
                            auxResServ++;
                            rr.TBS356_PROC_MEDIC_PROCEReference.Load();

                            //Concatena informações
                            sbsa.Append(string.Format("{2}( {0} - Tipo: {1} - Aplicação: {3} )", rr.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI, GetTpServicoAmbu(rr.TP_SERVI), (auxResServ > 1 ? " ### " : ""), GetTpAplicacao(rr.TP_APLIC)));
                        }
                        ServAmbu = sbsa.ToString();

                        #endregion
                        st.Value = st.Value.Replace("[deSerAmbPaci]", ServAmbu);
                        
                        ////OBSERVAÇÃO
                        st.Value = st.Value.Replace("[obsAtendPaci]", "-");
                        
                        ////ANOTAÇÃO
                        st.Value = st.Value.Replace("[EmiRecePaci]", (auxResRec > 0 ? "Sim" : "Não"));
                        st.Value = st.Value.Replace("[AteDispPaci]", (TBS333_ATEST_MEDIC_PACIE.RetornaTodosRegistros().Where(w=>w.ID_ATEND_MEDIC == IdAtendimentoMedido).Any() ? "Sim" : "Não"));
                        st.Value = st.Value.Replace("[ResMediPaci]", (TB092_RESER_MEDIC.RetornaTodosRegistros().Where(w=>w.TBS219_ATEND_MEDIC.ID_ATEND_MEDIC == IdAtendimentoMedido).Any() ? "Sim" : "Não"));

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
                                    richPagina.Rtf = st.Value;
                                    richPagina.Visible = true;
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
        /// Retorna o tipo de aplicação
        /// </summary>
        private string GetTpAplicacao(string TP_APLIC)
        {
            switch(TP_APLIC)
            {
                case "N":
                    return "Nenhum";
                case "O":
                    return "Via Oral";
                case "I":
                    return "Via Intravenosa";
                default:
                    return " - ";
            }
        }

        /// <summary>
        /// Retorna o tipo do serviço ambulatorial
        /// </summary>
        /// <param name="TP_SERV"></param>
        /// <returns></returns>
        private string GetTpServicoAmbu(string TP_SERV)
        {
            switch(TP_SERV)
            {
                case "M":
                    return "Medicação";
                case "A":
                    return "Acompanhamento";
                case "C":
                    return "Curativo";
                case "O":
                    return "Outras";
                default:
                    return " - ";
            }
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
            public string Fl_Hidelogo { get; set; }

        }

        private DevExpress.XtraReports.UI.XRRichText richPagina;
        private DevExpress.XtraReports.UI.XRRichText richPagina3;
        private DevExpress.XtraReports.UI.XRRichText richPagina1;
    }








}
