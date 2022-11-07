using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Linq;

namespace C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2111_PreMatriculaAluno
{
    public partial class RptFichaMatricAluno : C2BR.GestorEducacao.Reports.RptRetrato
    {
        static GestorEntities ctx;
        public RptFichaMatricAluno()
        {
            ctx = GestorEntities.CurrentContext;
            InitializeComponent();
        }

        public int InitReport(string parametros, int codEmp, string infos, string strP_CO_EMP, string codAlu, String anoBase, string Situacao)
        {
            #region Inicializa o header/Labels

            // Seta as informaçoes do rodape do relatorio
            this.InfosRodape = infos;

            // Cria o header a partir do cod da instituicao
            var header = ReportHeader.GetHeaderFromEmpresa(codEmp);
            if (header == null)
                return -1;
            lblTitulo.Text = "FICHA DE MATRÍCULA INDIVIDUAL";
            // Inicializa o header
            base.BaseInit(header);
            //this.celFreq.Text = string.Format(celFreq.Text, anoBase);

            #endregion
            try
            {
                #region Comando query
                string anoAtual = DateTime.Now.Year.ToString();
                // todo: Realizar o tratamento de tipo e outros letras por texto
                var matriculas = (from tb08 in ctx.TB08_MATRCUR
                                  where tb08.CO_ALU_CAD == codAlu
                                  join tb07 in ctx.TB07_ALUNO on tb08.CO_ALU equals tb07.CO_ALU
                                  join tb904 in ctx.TB904_CIDADE on tb07.TB905_BAIRRO.CO_CIDADE equals tb904.CO_CIDADE
                                    into c1 from tb904 in c1.DefaultIfEmpty()
                                  join tb25 in ctx.TB25_EMPRESA on tb08.CO_EMP_UNID_CONT equals tb25.CO_EMP
                                    into c3 from tb25 in c3.DefaultIfEmpty()
                                  join tb01 in ctx.TB01_CURSO on tb08.CO_CUR equals tb01.CO_CUR
                                    into c4 from tb01 in c4.DefaultIfEmpty()
                                  join tb06 in ctx.TB06_TURMAS on tb08.CO_TUR equals tb06.CO_TUR
                                    into c5 from tb06 in c5.DefaultIfEmpty()
                                  join tb129 in ctx.TB129_CADTURMAS on tb06.CO_TUR equals tb129.CO_TUR
                                    into c6 from tb129 in c6.DefaultIfEmpty()
                                  where tb08.CO_ANO_MES_MAT == anoBase
                                && tb08.CO_SIT_MAT == "A"
                                  select new DadosFichaAluno
                                 {
                                    #region Dados do Aluno
                                     codigoAluno = tb07.CO_ALU
                                     , fotoAluno = (tb07.Image.ImageStream)
                                    , nireAluno = (tb07.NU_NIRE)
                                    , nomeAluno = (tb07.NO_ALU ?? " ")
                                    , sexoAluno = (tb07.CO_SEXO_ALU ?? " ")
                                    , naturalidadeAluno = (tb07.DE_NATU_ALU ?? " ")
                                    , naturalidadeUfAluno = (tb07.CO_UF_NATU_ALU ?? " ")
                                    , nascimentoAluno = (tb07.DT_NASC_ALU)
                                    , nacionalidadeAluno = (tb07.DE_NACI_ALU ?? " ")
                                    , enderecoAluno = (tb07.DE_ENDE_ALU ?? " ")
                                    , cepAluno = (tb07.CO_CEP_ALU ?? " ")
                                    , bairroAluno = (tb07.TB905_BAIRRO.NO_BAIRRO ?? " ")
                                    , cidadeAluno = (tb904.NO_CIDADE ?? " ")
                                    , ufAluno = (tb904.CO_UF ?? " ")
                                    , nuCertidaoAluno = (tb07.NU_CERT ?? " ")
                                    , livroCertidaoAluno = (tb07.DE_CERT_LIVRO ?? " ")
                                    , folhaCertidaoAluno = (tb07.NU_CERT_FOLHA ?? " ")
                                    , cartorioCertidaoAluno = (tb07.DE_CERT_CARTORIO ?? " ")
                                    , cidadeCartorioCertidaoAluno = (tb07.NO_CIDA_CARTORIO_ALU ?? " ")
                                    , ufCartorioCertidaoAluno = (tb07.CO_UF_CARTORIO ?? " ")
                                    , situacaoAcademicaAluno = (tb08.CO_SIT_MAT ?? " ")
                                    , emailAluno = (tb07.NO_WEB_ALU ?? " ")
                                    , pesoAluno = " "
                                    , alturaAluno = " " 
                                    , tipoSangueAluno = (tb07.CO_TIPO_SANGUE_ALU ?? " ")
                                    , aptoEFAluno = " "
                                    , statusSangueAluno = (tb07.CO_STATUS_SANGUE_ALU ?? " ")
                                    , racaAluno = (tb07.TP_RACA ?? " ")
                                    , paiAluno = (tb07.NO_PAI_ALU ?? " ")
                                    , maeAluno = (tb07.NO_MAE_ALU ?? " ")
                                    , telefoneAluno = (tb07.NU_TELE_RESI_ALU ?? " ")
                                    #endregion
                                    #region Dados do Responsável
                                    , nomeResponsavel = (tb07.TB108_RESPONSAVEL.NO_RESP ?? " ")
                                    , nascimentoResponsavel = (tb07.TB108_RESPONSAVEL.DT_NASC_RESP)
                                    , cpfResponsavel = (tb07.TB108_RESPONSAVEL.NU_CPF_RESP ?? " ")
                                    , rgResponsavel = (tb07.TB108_RESPONSAVEL.CO_RG_RESP ?? " ")
                                    , rgEmissaoResponsavel = (tb07.TB108_RESPONSAVEL.CO_ORG_RG_RESP ?? " ")
                                    , dataEmissaoResponsavel = (tb07.TB108_RESPONSAVEL.DT_EMIS_RG_RESP)
                                    , instrucaoResponsavel = (tb07.TB108_RESPONSAVEL.CO_STATUS_GRAU_INSTR_RESP ?? " ")
                                    , profissaoResponsavel = (tb07.TB108_RESPONSAVEL.NO_FUNCAO_RESP ?? " ")
                                    , empresaResponsavel = (tb07.TB108_RESPONSAVEL.NO_EMPR_RESP ?? " ")
                                    , telefoneEmpresaResponsavel = (tb07.TB108_RESPONSAVEL.NU_TELE_COME_RESP ?? " ")
                                    , ramaEmpresalResponsavel = (tb07.TB108_RESPONSAVEL.NU_RAMA_COME_RESP ?? " ")
                                    , celularResponsavel = (tb07.TB108_RESPONSAVEL.NU_TELE_CELU_RESP ?? " ")
                                    , emailResponsavel = (tb07.TB108_RESPONSAVEL.DES_EMAIL_RESP ?? " ")
                                    , enderecoResponsavel = (tb07.TB108_RESPONSAVEL.DE_ENDE_RESP ?? " ")
                                    , cepResponsavel = (tb07.TB108_RESPONSAVEL.CO_CEP_RESP ?? " ")
                                    , cidadeResponsavel = (tb07.TB108_RESPONSAVEL.CO_CIDADE ?? 0)
                                    , telefoneResidencialResponsavel = (tb07.TB108_RESPONSAVEL.NU_TELE_RESI_RESP ?? " ")
                                    #endregion
                                    #region Dados Curriculares
                                    , unidadeCurricular = (tb25.NO_FANTAS_EMP ?? " ")
                                    , cursoCurricular = (tb01.NO_CUR ?? " ")
                                    , turnoCurricular = (tb06.CO_PERI_TUR ?? " ")
                                    , grauCurricular = (tb01.CO_NIVEL_CUR ?? " ")
                                    , serieCurricular = (tb01.CO_SIGL_CUR ?? " ")
                                    , turmaCurricular = (tb129.CO_SIGLA_TURMA ?? " ")
                                    , blocoCurricular = (tb129.TB248_UNIDADE_SALAS_AULA.CO_TIPO_SALA_AULA ?? " ")
                                    , salaCurricular = (tb129.TB248_UNIDADE_SALAS_AULA.DE_SALA_AULA ?? " ")
                                    , dataMatriculaCurricular = (tb08.DT_CAD_MAT)
                                    , anoCurricular = (tb08.CO_ANO_MES_MAT ?? " ")
                                    , possuiDeficienciaCurricular = ((tb07.DES_DEF == null || tb07.DES_DEF == "") ? false : true)
                                    , deficienciaCurricular = (tb07.DES_DEF ?? " ")
                                    #endregion
                                 });
                #endregion

                var aluno = matriculas.FirstOrDefault();

                if (aluno == null)
                    return -1;

                if (aluno.codigoAluno != 0)
                {
                    #region Dados complementares Aluno
                    ///Dados sobre a situação acadêmica
                    switch (aluno.situacaoAcademicaAluno)
                    {
                        case "A":
                            aluno.situacaoAcademicaAluno = "Matrículado";
                            break;
                        case "C":
                            aluno.situacaoAcademicaAluno = "Cancelado";
                            break;
                        case "T":
                            aluno.situacaoAcademicaAluno = "Transferido";
                            break;
                        case "R":
                            aluno.situacaoAcademicaAluno = "Pré-Matrículado";
                            break;
                        default:
                            aluno.situacaoAcademicaAluno = "Outros";
                            break;
                    }
                    ///Dados sobre a raça
                    switch (aluno.racaAluno)
                    {
                        case "X":
                            aluno.racaAluno = "Não informada";
                            break;
                        case "I":
                            aluno.racaAluno = "Indígena";
                            break;
                        case "P":
                            aluno.racaAluno = "Parda";
                            break;
                        case "A":
                            aluno.racaAluno = "Amarela";
                            break;
                        case "B":
                            aluno.racaAluno = "Branca";
                            break;
                    }
                    ///Dados sobre deficiência
                    switch (aluno.deficienciaCurricular)
                    {
                        case "N":
                            aluno.deficienciaCurricular = "Nenhuma";
                            break;
                        case "A":
                            aluno.deficienciaCurricular = "Auditiva";
                            break;
                        case "V":
                            aluno.deficienciaCurricular = "Visual";
                            break;
                        case "F":
                            aluno.deficienciaCurricular = "Fisica";
                            break;
                        case "M":
                            aluno.deficienciaCurricular = "Mental";
                            break;
                        case "P":
                            aluno.deficienciaCurricular = "Multiplas";
                            break;
                        case "O":
                            aluno.deficienciaCurricular = "Outras";
                            break;
                    }
                    #endregion
                    #region Dados complementares Responsável
                    ///Dados da Cidade e UF
                    int codigoCidade = aluno.cidadeResponsavel ?? 0;
                    var cidade = (from tb904 in ctx.TB904_CIDADE
                                 where tb904.CO_CIDADE == codigoCidade
                                 select new { tb904.NO_CIDADE, tb904.CO_UF }).FirstOrDefault();
                    aluno.nomeCidadeResponsavel = cidade.NO_CIDADE ?? " ";
                    aluno.ufResponsavel = cidade.CO_UF ?? " ";

                    ///Dados do cpf
                    Int64 numeroCPFResp = 0;
                    if (aluno.cpfResponsavel.Trim() != "" && Int64.TryParse(aluno.cpfResponsavel, out numeroCPFResp))
                        aluno.cpfIntResponsavel = numeroCPFResp;
                    else
                        aluno.cpfIntResponsavel = numeroCPFResp;
                    #endregion
                    #region Query saúde
                    // todo: Realizar o tratamento de tipo e outros letras por texto
                    var saudes = (from tb293 in ctx.TB293_CUIDAD_SAUDE
                                  where tb293.TB07_ALUNO.CO_ALU == aluno.codigoAluno
                                  select new saudeAluno
                                  {
                                      tipoCuidadoSaude = tb293.TP_CUIDADO_SAUDE
                                      , tipoAplicacaoSaude = tb293.TP_APLICAC_CUIDADO
                                      , horarioSaude = tb293.HR_APLICAC_CUIDADO
                                      , descricaoSaude = tb293.NM_REMEDIO_CUIDADO
                                      , quantidadeSaude = tb293.DE_DOSE_REMEDIO_CUIDADO
                                      , unidadeMedidaSaude = tb293.TB89_UNIDADES.NO_UNID_ITEM
                                  }
                                      );
                    if (saudes != null && saudes.Count() > 0)
                        aluno.saudeCurricular = saudes.ToList();
                    #endregion
                    #region Dados complementares Saúde
                    if (saudes != null && saudes.Count() > 0)
                    {
                        foreach (var linha in aluno.saudeCurricular)
                        {
                            ///Dados do tipo de cuidado de saúde
                            switch (linha.tipoCuidadoSaude)
                            {
                                case "M":
                                    linha.tipoCuidadoSaude = "Medicação";
                                    break;
                                case "A":
                                    linha.tipoCuidadoSaude = "Acompanhamento";
                                    break;
                                case "C":
                                    linha.tipoCuidadoSaude = "Curativa";
                                    break;
                                case "O":
                                    linha.tipoCuidadoSaude = "Outras";
                                    break;
                            }
                            ///Dados do tipo de aplicação
                            switch (linha.tipoAplicacaoSaude)
                            {
                                case "O":
                                    linha.tipoAplicacaoSaude = "Via oral";
                                    break;
                            }
                        }
                    }
                    #endregion
                    #region Query cuidado
                    // todo: Realizar o tratamento de tipo e outros letras por texto
                    var alimentos = (from tb294 in ctx.TB294_RESTR_ALIMEN
                                     where tb294.TB07_ALUNO.CO_ALU == aluno.codigoAluno
                                     select new alimentoAluno
                                     {
                                         tipoAlimento = tb294.TP_RESTR_ALIMEN
                                         , descricaoAlimento = tb294.NM_RESTR_ALIMEN
                                         , acaoAlimento = tb294.DE_ACAO_RESTR_ALIMEN
                                         , grauAlimento = tb294.CO_GRAU_RESTR_ALIMEN
                                     }
                                         );
                    if (alimentos != null && alimentos.Count() > 0)
                        aluno.alimentoCurricular = alimentos.ToList();
                    #endregion
                    #region Dados complementares Cuidado
                    if (alimentos != null && alimentos.Count() > 0)
                    {
                        foreach (var linha in aluno.alimentoCurricular)
                        {
                            ///Dados do tipo de restrição
                            switch (linha.tipoAlimento)
                            {
                                case "A":
                                    linha.tipoAlimento = "Alimentar";
                                    break;
                                case "L":
                                    linha.tipoAlimento = "Alérgica";
                                    break;
                                case "M":
                                    linha.tipoAlimento = "Médica";
                                    break;
                                case "O":
                                    linha.tipoAlimento = "Outras";
                                    break;
                                case "R":
                                    linha.tipoAlimento = "Responsável";
                                    break;
                            }
                            ///Dados do grau de restrição
                            switch (linha.grauAlimento)
                            {
                                case "A":
                                    linha.grauAlimento = "Alto risco";
                                    break;
                                case "M":
                                    linha.grauAlimento = "Médio risco";
                                    break;
                                case "B":
                                    linha.grauAlimento = "Baixo risco";
                                    break;
                                case "N":
                                    linha.grauAlimento = "Nenhum";
                                    break;
                            }
                        }
                    }
                    #endregion
                }

                bsReport.Clear();
                bsReport.Add(aluno);

                return 1;
            }
            catch { return 0; }
        }

        #region Classe
        public class DadosFichaAluno
        {
            public int codigoAluno { get; set; }
            #region Dados do Aluno            
            public byte[] fotoAluno { get; set; }
            public Int32 nireAluno { get; set; }
            public string nomeAluno { get; set; }
            public string sexoAluno { get; set; }
            public string naturalidadeAluno { get; set; }
            public string naturalidadeUfAluno { get; set; }
            public DateTime? nascimentoAluno { get; set; }
            public string nacionalidadeAluno { get; set; }
            public string enderecoAluno { get; set; }
            public string cepAluno { get; set; }
            public string bairroAluno { get; set; }
            public string cidadeAluno { get; set; }
            public string ufAluno { get; set; }
            public string nuCertidaoAluno { get; set; }
            public string livroCertidaoAluno { get; set; }
            public string folhaCertidaoAluno { get; set; }
            public string cartorioCertidaoAluno { get; set; }
            public string cidadeCartorioCertidaoAluno { get; set; }
            public string ufCartorioCertidaoAluno { get; set; }
            public string situacaoAcademicaAluno { get; set; }
            public string emailAluno { get; set; }
            public string pesoAluno { get; set; }
            public string alturaAluno { get; set; }
            public string statusSangueAluno { get; set; }
            public string tipoSangueAluno
            {
                get;
                set;
            }
            public string aptoEFAluno { get; set; }
            public string racaAluno 
            { 
                get; 
                set; 
            }
            public string paiAluno { get; set; }
            public string maeAluno { get; set; }
            public string telefoneAluno { get; set; }
            #endregion
            #region Dados do Responsável
            public string nomeResponsavel { get; set; }
            public DateTime? nascimentoResponsavel 
            { 
                get; 
                set; 
            }
            public string cpfResponsavel { get; set; }
            public Int64 cpfIntResponsavel { get; set; }
            public string rgResponsavel { get; set; }
            public string rgEmissaoResponsavel { get; set; }
            public DateTime? dataEmissaoResponsavel 
            { 
                get; 
                set; 
            }
            public string instrucaoResponsavel 
            { 
                get; 
                set; 
            }
            public string profissaoResponsavel { get; set; }
            public string empresaResponsavel { get; set; }
            public string telefoneEmpresaResponsavel { get; set; }
            public string ramaEmpresalResponsavel { get; set; }
            public string celularResponsavel { get; set; }
            public string emailResponsavel { get; set; }
            public string enderecoResponsavel { get; set; }
            public string cepResponsavel { get; set; }
            public int? cidadeResponsavel { get; set; }
            public string nomeCidadeResponsavel { get; set; }
            public string ufResponsavel 
            { 
                get; 
                set; 
            }
            public string telefoneResidencialResponsavel { get; set; }
            #endregion
            #region Dados Curriculares
            public string unidadeCurricular { get; set; }
            public string cursoCurricular { get; set; }
            public string turnoCurricular 
            { 
                get; 
                set; 
            }
            public string grauCurricular { get; set; }
            public string serieCurricular { get; set; }
            public string turmaCurricular { get; set; }
            public string blocoCurricular 
            {
                get; 
                set;
            }
            public string salaCurricular { get; set; }
            public DateTime dataMatriculaCurricular 
            { 
                get;
                set;
            }
            public string anoCurricular { get; set; }
            public List<saudeAluno> saudeCurricular { get; set; }
            public List<alimentoAluno> alimentoCurricular { get; set; }
            public string deficienciaCurricular { get; set; }
            public Boolean possuiDeficienciaCurricular { get; set; }
            #endregion
        }
        #endregion

        #region Classe adicionais
        #region Cuidados de saúde
        public class saudeAluno
        {
            public string tipoCuidadoSaude 
            { 
                get; 
                set;
            }
            public string tipoAplicacaoSaude 
            {
                get;
                set;
            }
            public string horarioSaude { get; set; }
            public string descricaoSaude { get; set; }
            public string unidadeMedidaSaude { get; set; }
            public int? quantidadeSaude {
                get;
                set;
            }
        }
        #endregion
        #region Cuidados alimentares
        public class alimentoAluno
        {
            public string tipoAlimento 
            { 
                get; 
                set; 
            }
            public string descricaoAlimento { get; set; }
            public string acaoAlimento { get; set; }
            public string grauAlimento 
            { 
                get; 
                set; 
            }
        }
        #endregion
        #endregion


    }
}
