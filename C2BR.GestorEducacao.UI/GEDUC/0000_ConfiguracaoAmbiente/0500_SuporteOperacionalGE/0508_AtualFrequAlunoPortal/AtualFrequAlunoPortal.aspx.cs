//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: *****
// OBJETIVO: *****
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 05/04/2013| André Nobre Vinagre        | Criada a funcionalidade
//           |                            | 
//           |                            | 
// ----------+----------------------------+-------------------------------------
// 15/04/2013| André Nobre Vinagre        | Corrigida a lógica para update
//           |                            | 
//           |                            |

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using System.Data;
using System.IO;
using System.Text;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using Resources;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.Configuration;
using System.Collections.Generic;
using C2BR.GestorEducacao.BusinessEntities.MSSQLPORTAL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0500_SuporteOperacionalGE.F0508_AtualFrequAlunoPortal
{
    public partial class AtualFrequAlunoPortal : System.Web.UI.Page
    {
        public PadraoGenericas CurrentPadraoBuscas { get { return (App_Masters.PadraoGenericas)Master; } }

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            //CurrentPadraoBuscas.DefineMensagem("", "Selecione um ou mais itens no quadro abaixo, escolha uma das ações de execução <br /> (botões abaixo do quadro) e clique para executar.");
            CurrentPadraoBuscas.DefineMensagem("", "Clique no botão abaixo para importação dos dados <br /> de frequência do aluno oriundos do portal.");
        }

        #endregion

        #region Métodos

        private void HabilitaCampos(bool habilita)
        {
            lnkAtualBP.Enabled = habilita;

            if (habilita)
            {
                liLnkAtualBD.Style.Add("display", "block");
            }
        }

        #endregion


        protected void lnkAtualBP_Click(object sender, EventArgs e)
        {
            HabilitaCampos(false);
            var portal = new BasePortal();
            var ctx = GestorEntities.CurrentContext;
            DateTime dataPadrao = new DateTime(2010, 01, 01, 1, 1, 1);
            DateTime dataPadraoAtual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 1, 1, 1);
            string cnpjInst = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO).ORG_NUMERO_CNPJ.ToString("00000000000000");
            var GW551 = (from fre in portal.GW551_FREQU.AsQueryable()
                             where fre.FL_BAIXA_FREQU == "N"
                             && fre.ID_INSTIT == cnpjInst
                             select fre);
            if (GW551 != null && GW551.Count() > 0)
            {
                #region Frequência do Aluno

                foreach (var linha in GW551)
                {
                    int idFrequ = linha.ID_FREQU;
                    int coEmp = linha.ID_INFOR_UNID ?? 0;
                    int coModuCur = linha.ID_MODAL_ENSINO ?? 0;
                    int coCur = linha.ID_SERIE_ENSINO ?? 0;
                    int coTur = linha.ID_TURMA_ENSINO ?? 0;
                    int coMat = linha.ID_GRADE_MATER ?? 0;
                    int coAtiv = linha.CO_ATIV_PROF_TUR ?? 0;
                    int coCol = linha.ID_COLAB_LANCA_FREQU ?? 0;
                    DateTime dataFreq = linha.DT_FREQU ?? new DateTime();

                    #region Registro da atividade no Gestor
                    //====> Pega a atividade lançada pelo portal do professor
                    var GW554 = (from ativ in portal.GW554_ATIV_PROF_TURMA.AsQueryable()
                                     where ativ.CO_EMP == coEmp
                                        && ativ.CO_CUR == coCur
                                        && ativ.CO_TUR == coTur
                                        && ativ.CO_COL == coCol 
                                        && ativ.CO_MAT == coMat
                                        && ativ.CO_ATIV_PROF_TUR == coAtiv
                                     select ativ).FirstOrDefault();
                    

                    TB119_ATIV_PROF_TURMA a = new TB119_ATIV_PROF_TURMA();

                    if (GW554 != null)
                    {
                        int coAtivEmp = GW554.CO_EMP;
                        int coAtivCur = GW554.CO_CUR;
                        int coAtivTur = GW554.CO_TUR;
                        int coAtivCol = GW554.CO_COL;
                        int coAtivMat = GW554.CO_MAT;
                        int coAtivColA = GW554.CO_COL_ATIV;
                        int coTempo = linha.NR_TEMPO ?? 0;
                        DateTime dtAtivReal = GW554.DT_ATIV_REAL;
                        DateTime dtAtivRealTerm = GW554.DT_ATIV_REAL_TERM;
                        string coTpAtiv = GW554.CO_TIPO_ATIV;
                        string deTema = GW554.DE_TEMA_AULA;
                        string deFormaAval = GW554.DE_FORMA_AVALI_ATIV;
                        bool flaAula = GW554.FLA_AULA_PLAN;
                        string coAno = GW554.CO_ANO_MES_MAT;
                        int coModu = GW554.CO_MODU_CUR ?? 0;
                        string hrAtivIni = GW554.HR_INI_ATIV;
                        string hrAtivFim = GW554.HR_TER_ATIV;
                        string deResumo = GW554.DE_RES_ATIV;
                        string coIpCad = GW554.CO_IP_CADAST;
                        int idTipoAtiv = GW554.ID_TIPO_ATIV ?? 0;
                        string coRefer = GW554.CO_REFER_ATIV;
                        string flLancaNota = GW554.FL_LANCA_NOTA;
                        string flAval = GW554.FL_AVALIA_ATIV;

                        string dtARM = dtAtivRealTerm.Date.Month.ToString().Length == 1 ? "0" + dtAtivRealTerm.Date.Month.ToString() : dtAtivRealTerm.Date.Month.ToString();
                        string dtAtvR = dtAtivRealTerm.Date.Year + "-" + dtARM + "-" + dtAtivRealTerm.Date.Day;

                        string dtAM = dtAtivReal.Date.Month.ToString().Length == 1 ? "0" + dtAtivReal.Date.Month.ToString() : dtAtivReal.Date.Month.ToString();
                        string dtAtv = dtAtivReal.Date.Year + "-" + dtAM + "-" + dtAtivReal.Date.Day;

                        a.CO_EMP = coAtivEmp;
                        a.CO_CUR = coAtivCur;
                        a.CO_TUR = coAtivTur;
                        a.CO_COL = coAtivCol;
                        a.CO_MAT = coAtivMat;
                        a.CO_COL_ATIV = coAtivColA;
                        a.DT_ATIV_REAL = Convert.ToDateTime(dtAtv);
                        a.DT_ATIV_REAL_TERM = Convert.ToDateTime(dtAtvR);
                        a.CO_TIPO_ATIV = coTpAtiv;
                        a.DE_TEMA_AULA = deTema;
                        a.DE_FORMA_AVALI_ATIV = deFormaAval != "" ? deFormaAval : null;
                        a.FLA_AULA_PLAN = flaAula;
                        a.CO_ANO_MES_MAT = coAno;
                        a.CO_MODU_CUR = coModu;
                        a.HR_INI_ATIV = hrAtivIni;
                        a.HR_TER_ATIV = hrAtivFim;
                        a.DE_RES_ATIV = deResumo;
                        a.CO_IP_CADAST = coIpCad;
                        a.CO_REFER_ATIV = coRefer;
                        a.FL_LANCA_NOTA = flLancaNota;
                        a.FL_AVALIA_ATIV = flAval;
                        a.DT_REGISTRO_ATIV = DateTime.Now;
                        a.TB273_TIPO_ATIVIDADE = TB273_TIPO_ATIVIDADE.RetornaPelaChavePrimaria(idTipoAtiv);
                        a.CO_TEMPO_ATIV = coTempo;

                        TB119_ATIV_PROF_TURMA.SaveOrUpdate(a, true);
                        

                    }

                    linha.FL_BAIXA_FREQU = "S";
                    linha.ID_COLAB_BAIXA_FREQU = LoginAuxili.CO_COL;
                    linha.NR_IP_BAIXA_FREQU = LoginAuxili.IP_USU;
                    linha.DT_BAIXA_FREQU = dataPadraoAtual;

                    #endregion

                    var GW551_2 = (from lanc in portal.GW551_LANCA_FREQU.AsQueryable()
                                       where lanc.FL_BAIXA_FREQU == "N"
                                       && lanc.ID_FREQU == idFrequ
                                       && lanc.ID_INSTIT == cnpjInst
                                       select lanc);
                    if (GW551_2 != null && GW551_2.Count() > 0)
                    {
                        foreach (var linha2 in GW551_2)
                        {
                            int coAlu = linha2.ID_INFOR_ALUNO;
                            int nrTempo = linha2.NR_TEMPO ?? 0;
                            string coRefer = linha2.CO_REFER_FREQU;
                            int anoRefer = dataFreq.Year;

                            TB132_FREQ_ALU tb132 = (from iTb132 in TB132_FREQ_ALU.RetornaTodosRegistros()
                                                    where iTb132.TB01_CURSO.CO_CUR == coCur 
                                                    && iTb132.TB07_ALUNO.CO_ALU == coAlu
                                                    && iTb132.CO_MAT == coMat 
                                                    && iTb132.CO_TUR == coTur 
                                                    && iTb132.CO_ANO_REFER_FREQ_ALUNO == anoRefer
                                                    && (iTb132.DT_FRE.Year == dataFreq.Year 
                                                    && iTb132.DT_FRE.Month == dataFreq.Month 
                                                    && iTb132.DT_FRE.Day == dataFreq.Day)
                                                    && iTb132.NR_TEMPO == nrTempo
                                                    select iTb132).FirstOrDefault();

                            if (tb132 == null)
                            {
                                tb132 = new TB132_FREQ_ALU();

                                tb132.TB01_CURSO = TB01_CURSO.RetornaPelaChavePrimaria(coEmp, coModuCur, coCur);
                                tb132.TB07_ALUNO = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                                tb132.CO_ANO_REFER_FREQ_ALUNO = dataFreq.Year;
                                tb132.CO_TUR = coTur;
                                tb132.CO_MAT = coMat;
                                tb132.DT_FRE = dataFreq;
                                tb132.NR_TEMPO = nrTempo;
                                tb132.CO_BIMESTRE = coRefer;
                                tb132.CO_FLAG_FREQ_ALUNO = linha2.TP_FREQU;
                            }
                            else
                                tb132.FL_HOMOL_FREQU = "N";

                            tb132.CO_COL = linha2.ID_COLAB_LANCA_FREQU;
                            tb132.CO_FLAG_FREQ_ALUNO = linha2.TP_FREQU;//S ou N                

                            #region Atividade

                            if (a != null)
                            {
                                tb132.CO_ATIV_PROF_TUR = a.CO_ATIV_PROF_TUR;
                            }
                            else
                            {
                                var vTb119 = (from tb119 in TB119_ATIV_PROF_TURMA.RetornaTodosRegistros()
                                              where tb119.DT_ATIV_REAL.Year == tb132.DT_FRE.Year
                                              && tb119.DT_ATIV_REAL.Month == tb132.DT_FRE.Month
                                              && tb119.DT_ATIV_REAL.Day == tb132.DT_FRE.Day
                                              && tb119.CO_EMP == coEmp
                                              && tb119.CO_MODU_CUR == coModuCur
                                              && tb119.CO_CUR == coCur
                                              && tb119.CO_TUR == coTur
                                              && tb119.CO_TEMPO_ATIV == nrTempo
                                              select new { tb119.CO_ATIV_PROF_TUR }).FirstOrDefault();

                                tb132.CO_ATIV_PROF_TUR = vTb119 != null ? vTb119.CO_ATIV_PROF_TUR : 0;
                            }

                            tb132.DT_LANCTO_FREQ_ALUNO = linha2.DT_CADAS_FREQU;

                            #endregion

                            linha2.FL_BAIXA_FREQU = "S";
                            linha2.ID_COLAB_BAIXA_FREQU = LoginAuxili.CO_COL;
                            linha2.NR_IP_BAIXA_FREQU = LoginAuxili.IP_USU;
                            linha2.DT_BAIXA_FREQU = dataPadraoAtual;

                            TB132_FREQ_ALU.SaveOrUpdate(tb132, true);

                        }
                    }
                   
                }
                #endregion

                string Error = string.Empty;
                try
                {
                    portal.SaveChanges();
                }
                catch (Exception)
                {
                    Error = "Erro ao salvar";
                }


                int idModulo = 0;
                if (!int.TryParse(HttpContext.Current.Session[SessoesHttp.IdModuloCorrente].ToString(), out idModulo))
                {
                    HabilitaCampos(true);
                    divTelaExportacaoCarregando.Style.Add("display", "none");
                    AuxiliPagina.RedirecionaParaPaginaErro("Erro ao salvar na tabela de log de atividades.", Request.Url.AbsoluteUri);
                }

                TB236_LOG_ATIVIDADES tb236 = new TB236_LOG_ATIVIDADES();

                tb236.ORG_CODIGO_ORGAO = LoginAuxili.ORG_CODIGO_ORGAO;
                tb236.DT_ATIVI_LOG = DateTime.Now;
                tb236.CO_EMP_ATIVI_LOG = LoginAuxili.CO_EMP;
                tb236.IDEADMMODULO = idModulo;
                tb236.CO_ACAO_ATIVI_LOG = "A";
                tb236.CO_TABEL_ATIVI_LOG = "TB132_FREQ_ALU";
                tb236.NR_IP_ACESS_ATIVI_LOG = LoginAuxili.IP_USU;
                tb236.NR_ACESS_ATIVI_LOG = LoginAuxili.QTD_ACESSO_USU + 1;
                tb236.CO_EMP = LoginAuxili.CO_UNID_FUNC;
                tb236.CO_COL = LoginAuxili.CO_COL;

                TB236_LOG_ATIVIDADES.SaveOrUpdate(tb236);

                GestorEntities.CurrentContext.SaveChanges();

                GC.Collect();
                GC.WaitForPendingFinalizers();

                HabilitaCampos(true);
                divTelaExportacaoCarregando.Style.Add("display", "none");
                if (Error == string.Empty)
                {
                    AuxiliPagina.RedirecionaParaPaginaSucesso("Base de dados atualizada com sucesso. Tabela modificada: TB132_FREQ_ALU", Request.Url.AbsoluteUri);
                }
                else
                    AuxiliPagina.RedirecionaParaPaginaErro("Nenhum dado foi atualizado. Pois não existem registros para baixa na tabela de frequência do aluno no portal.", Request.Url.AbsoluteUri);
            }
            else
                AuxiliPagina.RedirecionaParaPaginaSucesso("Sem novos registros para atualização.", Request.Url.AbsoluteUri);
            
        }                        
    }
}
