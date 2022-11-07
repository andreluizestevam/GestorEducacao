using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports.Helper;
using System.Data.Objects;

namespace C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3600_CtrlInformacoesAlunos._3640_CtrlOcorrenciasDisciplinares
{
    public partial class RptExtratoOcorrDisc : C2BR.GestorEducacao.Reports.RptPaisagem
    {
        public RptExtratoOcorrDisc()
        {
            InitializeComponent();
        }
        public int InitReport(
                    string parametros,
                    string infos,
                    int coEmp,
                    int codUnid,
                    string coCateg,
                    int idFlex,
                    DateTime dtIni,
                    DateTime dtFim,
                    string coClassOcorr,
                    int idTipoOcorr,
                    string NO_RELATORIO,
                    string DT_EMISS,
                    int CO_INST
            )
        {
            try
            {
                #region Inicializa o header/Labels

                // Seta as informaçoes do rodape do relatorio
                this.InfosRodape = infos;
                this.lblpar.Text = parametros;

                // Cria o header a partir do cod da instituicao
                var header = ReportHeader.GetHeaderFromEmpresa(coEmp);

                //Seta o título dinamicamente inserindo um * caso não exista título definido de forma que apenas emitindo o relatório é possível saber se é dinamico ou não
                this.lblTitulo.Text = (!string.IsNullOrEmpty(NO_RELATORIO) ? NO_RELATORIO : "EXTRATO DE OCORRÊNCIAS DISCIPLINARES*");

                if (header == null)
                    return -1;

                // Inicializa o header
                base.BaseInit(header);
                //this.celFreq.Text = string.Format(celFreq.Text, anoBase);

                #endregion

                // Instancia o contexto

                dtFim = DateTime.Parse(dtFim.ToString("dd/MM/yyyy") + " 23:59:59");
                var res = (from tb191 in TB191_OCORR_ALUNO.RetornaTodosRegistros()
                           join tbe196 in TBE196_OCORR_DISCI.RetornaTodosRegistros() on tb191.TBE196_OCORR_DISCI.ID_OCORR_DISCI equals tbe196.ID_OCORR_DISCI into l1
                           from ls in l1.DefaultIfEmpty()
                           where (codUnid != 0 ? tb191.TB25_EMPRESA.CO_EMP == codUnid : 0 == 0)
                           && (coCateg != "0" ? tb191.CO_CATEG == coCateg : 0 == 0)
                           && (idFlex != 0 ? tb191.ID_RECEB_OCORR == idFlex : 0 == 0)
                           && (coClassOcorr != "0" ? tb191.TB150_TIPO_OCORR.CO_SIGL_OCORR == coClassOcorr : 0 == 0)
                           && (idTipoOcorr != 0 ? tb191.TBE196_OCORR_DISCI.ID_OCORR_DISCI == idTipoOcorr : 0 == 0)
                           && ((tb191.DT_OCORR >= dtIni) && (tb191.DT_OCORR <= dtFim))
                           select new MoviFinanMatric
                           {
                               COD_OCORRENCIA = tb191.CO_REGIS_OCORR,
                               IDE_OCORR_ALUNO = tb191.IDE_OCORR_ALUNO,
                               //Dados Gerais da Ocorrência 
                               DT_OCORR = tb191.DT_OCORR,
                               COMPLEMENTO = tb191.DE_OCORR,
                               ACAO = tb191.DE_ACAO_TOMAD,
                               NO_RESP_OCORR = tb191.TB03_COLABOR.NO_COL,
                               MAT_RESP_OCORR = tb191.TB03_COLABOR.CO_MAT_COL,
                               NO_TIPO_CLASS = tb191.TB150_TIPO_OCORR.DE_TIPO_OCORR,
                               CO_EMP = tb191.TB25_EMPRESA.CO_EMP,

                               CO_CATEG = tb191.CO_CATEG,
                               ID_RECEB = tb191.ID_RECEB_OCORR,

                               DT_CADAS = tb191.DT_CADASTRO,
                               FL_AVISADO_SMS = tb191.FL_AVISO_SMS,
                               TP_OCORR_SGL = ls.DE_OCORR,
                           }).ToList();

                res = res.OrderBy(w => w.NOME).ThenByDescending(w => w.DT_OCORR).ToList();

                if (res.Count == 0)
                    return -1;

                //Coleta informações da instituição que serão apresentadas no relatório
                #region Dados da Instituição

                var dadosInst = (from tb000 in TB000_INSTITUICAO.RetornaTodosRegistros()
                                 where tb000.ORG_CODIGO_ORGAO == CO_INST
                                 select new
                                 {
                                     tb000.TB905_BAIRRO.TB904_CIDADE.CO_UF,
                                     tb000.TB905_BAIRRO.TB904_CIDADE.NO_CIDADE,
                                 }).FirstOrDefault();

                //Seta os labels na região das assinaturas
                lblAssin1.Text = dadosInst.NO_CIDADE + " - " + dadosInst.CO_UF + ", " + DT_EMISS;
                lblAssin2.Text = dadosInst.NO_CIDADE + " - " + dadosInst.CO_UF + ", " + "____/_________/201___";

                #endregion

                int auxCountGroup = 0;
                int auxCoutGeral = 0;
                int auxIdReceb = 0;
                string auxCateg = "";
                bsReport.Clear();
                foreach (MoviFinanMatric r in res)
                {
                    //Verificase o id do ator ou a categoria são diferentes das já salvas nas variáveis, caso sejam, salva os novos valores nas variáveis
                    if ((auxIdReceb != r.ID_RECEB.Value) || (auxCateg != r.CO_CATEG))
                    {
                        auxIdReceb = r.ID_RECEB.Value;
                        auxCountGroup = 0;
                        auxCateg = r.CO_CATEG;
                    }

                    auxCountGroup++;
                    auxCoutGeral++;

                    //Executa somente ao passar no último registro do ator em questão
                    //if(auxCountGroup == res.Where(w=>w.ID_RECEB == auxIdReceb && w.CO_CATEG == auxCateg).Count())


                    //Prepara e apresenta os parâmetros de acordo com a categoria do registro (Aluno, Responsável, Professor ou Funcionário)
                    #region Prepara Parâmetros

                    //Seta o cabeçalho do ator em questão apenas se estiver passando em sua primeira linha, economizando,
                    //assim, mais memória e agilizando todo o processo
                    if (auxCountGroup == 1)
                    {
                        switch (r.CO_CATEG)
                        {
                            case "A":
                                //Prepara a primeira linha com o tipo da categoria e o nire do aluno
                                r.PARAM_UM = " - ALUNO(A) " + TB07_ALUNO.RetornaTodosRegistros().Where(w => w.CO_ALU == r.ID_RECEB).FirstOrDefault().NU_NIRE.ToString().PadLeft(7, '0') + " -";
                                //Segunda linha mostrará o nome
                                r.PARAM_DOIS = r.NOME.ToUpper();
                                //Terceira linha mostrará os dados da matrícula
                                #region Dados Matrícula

                                var resmat = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                              join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb08.CO_CUR equals tb01.CO_CUR
                                              join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on tb08.CO_TUR equals tb129.CO_TUR
                                              where tb08.CO_ALU == r.ID_RECEB
                                                  //Se tiver sido escolhida unidade nos parâmetros, filtra a unidade da matrícula
                                              && (codUnid != 0 ? tb08.CO_EMP == codUnid : 0 == 0)
                                              select new
                                              {
                                                  tb08.CO_ANO_MES_MAT,
                                                  tb08.TB44_MODULO.DE_MODU_CUR,
                                                  tb01.NO_CUR,
                                                  tb129.NO_TURMA,
                                                  tb08.TB07_ALUNO.CO_SEXO_ALU,
                                                  tb08.TB07_ALUNO.DT_NASC_ALU,
                                              }).FirstOrDefault();

                                r.PARAM_TRES = "Ano Letivo " + resmat.CO_ANO_MES_MAT + " - " + resmat.DE_MODU_CUR
                                    + " - " + resmat.NO_CUR + " - " + resmat.NO_TURMA + " - Sexo: "
                                    + Funcoes.RetornaSexo(resmat.CO_SEXO_ALU, false)
                                    + " - Idade " + (resmat.DT_NASC_ALU.HasValue ? Funcoes.GetIdade(resmat.DT_NASC_ALU.Value).ToString()
                                    + " (" + resmat.DT_NASC_ALU.Value.ToString("dd/MM/yy") + ")" : " - ");


                                #endregion
                                //Quarta linha mostrará os dados do responsável pelo aluno
                                #region Recolhe dados do responsável

                                var resResp = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                                               where tb07.CO_ALU == r.ID_RECEB
                                               select new
                                               {
                                                   tb07.TB108_RESPONSAVEL.NO_RESP,
                                                   tb07.TB108_RESPONSAVEL.NU_TELE_CELU_RESP,
                                                   tb07.TB108_RESPONSAVEL.NU_CPF_RESP,
                                               }).FirstOrDefault();

                                r.PARAM_QUATRO = "Responsável: " + Funcoes.Format(resResp.NU_CPF_RESP, TipoFormat.CPF)
                                    + " - " + resResp.NO_RESP.ToUpper() + " - Contato: "
                                    + Funcoes.Format(resResp.NU_TELE_CELU_RESP, TipoFormat.Telefone);

                                #endregion
                                break;
                            case "R":

                                //Prepara a primeira linha com o tipo da categoria e o CPF do responsável
                                r.PARAM_UM = " - RESPONSÁVEL " + Funcoes.Format(TB108_RESPONSAVEL.RetornaTodosRegistros().Where(w => w.CO_RESP == r.ID_RECEB).FirstOrDefault().NU_CPF_RESP, TipoFormat.CPF) + " -";
                                //Segunda linha mostrará o nome
                                r.PARAM_DOIS = r.NOME.ToUpper();
                                //Terceira linha mostrará os dados de sexo, idade, e contato
                                #region Dados do Responsável

                                var resRespR = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                                                where tb108.CO_RESP == r.ID_RECEB
                                                select new
                                                {
                                                    tb108.NO_RESP,
                                                    tb108.NU_TELE_CELU_RESP,
                                                    tb108.CO_SEXO_RESP,
                                                    tb108.DT_NASC_RESP,
                                                }).FirstOrDefault();

                                r.PARAM_TRES = "Contato: " + Funcoes.Format(resRespR.NU_TELE_CELU_RESP, TipoFormat.Telefone)
                                    + " - Sexo: " + Funcoes.RetornaSexo(resRespR.CO_SEXO_RESP, false)
                                    + " - Idade " + (resRespR.DT_NASC_RESP.HasValue ? Funcoes.GetIdade(resRespR.DT_NASC_RESP.Value).ToString()
                                    + " (" + resRespR.DT_NASC_RESP.Value.ToString("dd/MM/yy") + ")" : " - ");

                                #endregion
                                break;
                            case "P":
                            case "F":
                                //Prepara a primeira linha com o tipo da categoria e o CPF do funcionário ou professor
                                r.PARAM_UM = (r.CO_CATEG == "F" ? " - FUNCIONÁRIO(A) " : " - PROFESSOR(A) ") + Funcoes.Format(TB03_COLABOR.RetornaTodosRegistros().Where(w => w.CO_COL == r.ID_RECEB).FirstOrDefault().CO_MAT_COL, TipoFormat.MatriculaColaborador) + " -";
                                //Segunda linha mostrará o nome
                                r.PARAM_DOIS = r.NOME.ToUpper();
                                //Terceira linha mostrará os dados de cpf, sexo, idade, e contato
                                #region Dados do Funcionário

                                var resDadosColab = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                                                     where tb03.CO_COL == r.ID_RECEB
                                                     select new
                                                     {
                                                         tb03.NO_COL,
                                                         tb03.NU_TELE_CELU_COL,
                                                         tb03.CO_SEXO_COL,
                                                         tb03.DT_NASC_COL,
                                                         tb03.NU_CPF_COL,
                                                         tb03.DE_FUNC_COL,
                                                     }).FirstOrDefault();

                                r.PARAM_TRES = "Função: " + resDadosColab.DE_FUNC_COL + " - CPF: " + Funcoes.Format(resDadosColab.NU_CPF_COL, TipoFormat.CPF) + " - Contato: " + Funcoes.Format(resDadosColab.NU_TELE_CELU_COL, TipoFormat.Telefone)
                                    + " - Sexo: " + Funcoes.RetornaSexo(resDadosColab.CO_SEXO_COL, false)
                                    + " - Idade " + Funcoes.GetIdade(resDadosColab.DT_NASC_COL).ToString()
                                    + " (" + resDadosColab.DT_NASC_COL.ToString("dd/MM/yy") + ")";

                                #endregion
                                break;

                        }
                    }

                    #endregion

                    //Executa apenas no final
                    if (auxCoutGeral == res.Count)
                    {
                        //Coleta matrícula e nome do coordenador e insere abaixo da linha da assinatura 
                        #region Dados do Coordenador

                        var dadCoord = (from tb83 in TB83_PARAMETRO.RetornaTodosRegistros()
                                        join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tb83.CO_COOR1 equals tb03.CO_COL
                                        where tb83.CO_EMP == r.CO_EMP
                                        select new
                                        {
                                            tb03.NO_COL,
                                            tb03.CO_MAT_COL,
                                        }).FirstOrDefault();

                        lblNomeCoord.Text = dadCoord.NO_COL.ToUpper();
                        lblAssinMatCoord.Text = "Matrícula " + Funcoes.Format(dadCoord.CO_MAT_COL, TipoFormat.MatriculaColaborador);

                        #endregion
                    }

                    bsReport.Add(r);
                }

                return 1;
            }
            catch { return 0; }
        }

        public class MoviFinanMatric
        {
            //Dados Gerais da Ocorrência
            public int IDE_OCORR_ALUNO { get; set; }
            public DateTime DT_OCORR { get; set; }
            public string DT_OCORR_V
            {
                get
                {
                    return (this.DT_OCORR.ToString("dd/MM/yy") + " " + this.DT_OCORR.ToString("HH:mm"));
                }
            }
            public string NO_TIPO_CLASS { get; set; }

            public string TP_OCORR_SGL { get; set; }
            public string TP_OCORR_SGL_V
            {
                get
                {
                    return (!string.IsNullOrEmpty(this.TP_OCORR_SGL) ? this.TP_OCORR_SGL : "***");
                }
            }

            public string COMPLEMENTO { get; set; }

            public string ACAO { get; set; }
            public string ACAO_V
            {
                get
                {
                    return (!string.IsNullOrEmpty(this.ACAO) ? this.ACAO : "***");
                }
            }
            public string NO_RESP_OCORR { get; set; }
            public string MAT_RESP_OCORR { get; set; }
            public string NO_RESP_OCORR_V
            {
                get
                { 
                    string no_col = (this.NO_RESP_OCORR.Length > 18 ? this.NO_RESP_OCORR.Substring(0, 18) + "..." : this.NO_RESP_OCORR);
                    return (Funcoes.Format(this.MAT_RESP_OCORR, TipoFormat.MatriculaColaborador) + " - " + no_col);
                }
            }
            public int CO_EMP { get; set; }

            public string NU_TEL { get; set; }
            public string NU_CEL { get; set; }
            public string NU_COM { get; set; }
            public string concat_numeros
            {
                get
                {
                    return "CEL: " + Funcoes.Format(this.NU_CEL, TipoFormat.Telefone) + " - COM: " + Funcoes.Format(this.NU_COM, TipoFormat.Telefone) + " - TEL: " + Funcoes.Format(this.NU_TEL, TipoFormat.Telefone);
                }
            }

            public string CO_CATEG { get; set; }
            public int? ID_RECEB { get; set; }
            public string NOME
            {
                get
                {
                    string s = "";

                    if (ID_RECEB.HasValue)
                    {
                        ///Coleta o nome de quem recebeu a ocorrência dinamicamente
                        switch (this.CO_CATEG)
                        {
                            case "A":
                                s = TB07_ALUNO.RetornaTodosRegistros().Where(w => w.CO_ALU == this.ID_RECEB).FirstOrDefault().NO_ALU;
                                break;

                            case "P":
                            case "F":
                                s = TB03_COLABOR.RetornaTodosRegistros().Where(wde => wde.CO_COL == this.ID_RECEB).FirstOrDefault().NO_COL;
                                break;

                            case "R":
                                s = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(this.ID_RECEB.Value).NO_RESP;
                                break;
                        }
                        return s;
                    }
                    else
                        return " - ";
                }
            }

            public string PARAM_UM { get; set; }
            public string PARAM_DOIS { get; set; }
            public string PARAM_TRES { get; set; }
            public string PARAM_QUATRO { get; set; }

            public string LINHA_UM_ASSIN
            {
                get
                {
                    string s = "";
                    switch (this.CO_CATEG)
                    {
                        case "A":
                            s = "(Responsável/Aluno)";
                            break;
                        case "R":
                            s = "(Responsável)";
                            break;
                        case "F":
                            s = "(Funcionário(a))";
                            break;
                        case "P":
                            s = "(Professor(a))";
                            break;
                    }
                    return s;
                }
            }
            public string LINHA_DOIS_ASSIN
            {
                get
                {
                    string s = "";
                    switch (this.CO_CATEG)
                    {
                        case "A":
                            var resResp = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                                           where tb07.CO_ALU == this.ID_RECEB
                                           select new
                                           {
                                               tb07.TB108_RESPONSAVEL.NO_RESP,
                                           }).FirstOrDefault();

                            s = (resResp != null ? !string.IsNullOrEmpty(resResp.NO_RESP) ? resResp.NO_RESP : "***" : "***");
                            break;
                        case "F":
                        case "P":
                        case "R":
                            s = this.NOME;
                            break;
                    }
                    return s;
                }
            }
            public string COD_OCORRENCIA { get; set; }
            public string COD_OCORRENCIA_V
            {
                get
                {
                    return (!string.IsNullOrEmpty(this.COD_OCORRENCIA) ? this.COD_OCORRENCIA : "***");
                }
            }

            public DateTime DT_CADAS { get; set; }
            public string FL_AVISADO_SMS { get; set; }
            public string AVISO_SMS
            {
                get
                {
                    return (this.FL_AVISADO_SMS == "S" ? "SIM " + DT_CADAS.ToString("dd/MM/yy") + " " + DT_CADAS.ToString("HH:mm") : "NÃO");
                }
            }
        }
    }
}
