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
// 15/04/2013| André Nobre Vinagre        | Criada a funcionalidade
//           |                            | 
// ----------+----------------------------+-------------------------------------
// 24/04/2013| Victor Martins Machado     | Foi incluída a regra da inserção das médias,
//           |                            | do portal de relacionamento para o histórico
//           |                            | do aluno no Gestor.
// ----------+----------------------------+-------------------------------------
// 24/04/2013| Victor Martins Machado     | Foi alterado o tipo de atividade das notas
//           |                            | para pegar da tabela TB273.
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
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0500_SuporteOperacionalGE.F0509_AtualNotasAlunoPortal
{
    public partial class AtualNotasAlunoPortal : System.Web.UI.Page
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

        private string RetornaTipoAtividade(string siglaTipoAtiv)
        {
            string descTipoAtiv = "";

            if (siglaTipoAtiv == "ANO")
            {
                descTipoAtiv = "Aula Normal";
            }
            else if (siglaTipoAtiv == "AEX")
            {
                descTipoAtiv = "Aula Extra";
            }
            else if (siglaTipoAtiv == "ARE")
            {
                descTipoAtiv = "Aula Reforço";
            }
            else if (siglaTipoAtiv == "ARC")
            {
                descTipoAtiv = "Aula de Recuperação";
            }
            else if (siglaTipoAtiv == "TES")
            {
                descTipoAtiv = "Teste";
            }
            else if (siglaTipoAtiv == "PRO")
            {
                descTipoAtiv = "Prova";
            }
            else if (siglaTipoAtiv == "TRA")
            {
                descTipoAtiv = "Trabalho";
            }
            else if (siglaTipoAtiv == "AGR")
            {
                descTipoAtiv = "Atividade em Grupo";
            }
            else if (siglaTipoAtiv == "ATE")
            {
                descTipoAtiv = "Atividade Externa";
            }
            else if (siglaTipoAtiv == "ATI")
            {
                descTipoAtiv = "Atividade Interna";
            }
            else if (siglaTipoAtiv == "OUT")
            {
                descTipoAtiv = "Outros";
            }

            return descTipoAtiv;
        }

        #endregion


        protected void lnkAtualBP_Click(object sender, EventArgs e)
        {
            HabilitaCampos(false);
            #region Parametros iniciais
            var portal = new BasePortal();
            var ctx = GestorEntities.CurrentContext;
            DateTime dataPadrao = new DateTime(2010, 01, 01, 1, 1, 1);
            DateTime dataPadraoAtual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 1, 1, 1);
            string cnpjInst = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO).ORG_NUMERO_CNPJ.ToString("00000000000000");
            #endregion

            var GW552 = (from lanc in portal.GW552_LANCA_NOTA.AsQueryable()
                             where lanc.FL_BAIXA_NOTA == "N"
                             && lanc.ID_INSTIT == cnpjInst
                             && lanc.FL_TIPO_NOTA == "N"
                             select lanc);
            if (GW552 != null && GW552.Count() > 0)
            {
                #region Notas e Médias do Aluno

                foreach (GW552_LANCA_NOTA linha in GW552)
                {
                    int coEmp = linha.ID_INFOR_UNID;
                    int coModuCur = linha.ID_MODAL_ENSINO;
                    int coCur = linha.ID_SERIE_ENSINO;
                    int coTur = linha.ID_TURMA_ENSINO;
                    int idMateria = TB02_MATERIA.RetornaPelaChavePrimaria(coEmp, coModuCur, linha.ID_GRADE_MATER, coCur).ID_MATERIA;
                    int idGradeMater = linha.ID_GRADE_MATER;
                    int coAlu = linha.ID_INFOR_ALUNO;
                    DateTime dataLanca = linha.DT_LANCA_NOTA;
                    int anoRefer = dataLanca.Year;
                    string dcmNota = (linha.VL_NOTA != "" ? linha.VL_NOTA.Replace(".", ",") : "");
                    string bimestre = (linha.CO_REFER_NOTA == "B1" ? "1" : (linha.CO_REFER_NOTA == "B2" ? "2" : (linha.CO_REFER_NOTA == "B3" ? "3" : "4")));
                    string strTipoAtivi = linha.ID_TIPO_ATIV.ToString();
                    string flLancaNota = linha.FL_LANCA_MEDIA;
                    int qtdFalta = linha.QT_FALTA_MEDIA ?? 0;
                    int tipoAtiv = int.Parse(strTipoAtivi);

                    if (flLancaNota != "S")
                    {
                        #region Nota
                        TB49_NOTA_ATIV_ALUNO tb49 = (from iTb49 in TB49_NOTA_ATIV_ALUNO.RetornaTodosRegistros()
                                                     where iTb49.TB01_CURSO.CO_CUR == coCur && iTb49.TB07_ALUNO.CO_ALU == coAlu
                                                    && iTb49.TB107_CADMATERIAS.ID_MATERIA == idMateria && iTb49.TB06_TURMAS.CO_TUR == coTur && iTb49.CO_ANO == anoRefer
                                                    && (iTb49.DT_NOTA_ATIV.Year == dataLanca.Year && iTb49.DT_NOTA_ATIV.Month == dataLanca.Month && iTb49.DT_NOTA_ATIV.Day == dataLanca.Day)
                                                    && iTb49.CO_TIPO_ATIV == strTipoAtivi && iTb49.CO_BIMESTRE == bimestre
                                                     select iTb49).FirstOrDefault();

                        if (tb49 == null)
                        {
                            tb49 = new TB49_NOTA_ATIV_ALUNO();

                            tb49.TB01_CURSO = TB01_CURSO.RetornaPelaChavePrimaria(coEmp, coModuCur, coCur);
                            tb49.TB07_ALUNO = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                            tb49.CO_ANO = anoRefer;
                            tb49.TB06_TURMAS = TB06_TURMAS.RetornaPeloCodigo(coTur);
                            tb49.TB107_CADMATERIAS = TB107_CADMATERIAS.RetornaPelaChavePrimaria(coEmp, idMateria);
                            tb49.DT_NOTA_ATIV = dataLanca;
                        }

                        TB273_TIPO_ATIVIDADE tb273 = TB273_TIPO_ATIVIDADE.RetornaTodosRegistros().Where(r => r.ID_TIPO_ATIV == tipoAtiv).FirstOrDefault();
                        tb49.TB273_TIPO_ATIVIDADE = tb273;

                        tb49.CO_BIMESTRE = bimestre;
                        tb49.CO_SEMESTRE = bimestre == "1" || bimestre == "2" ? "1" : "2";
                        tb49.CO_TIPO_ATIV = strTipoAtivi;
                        tb49.NO_NOTA_ATIV = RetornaTipoAtividade(strTipoAtivi);
                        if (dcmNota.Trim() != "")
                        {
                            tb49.VL_NOTA = decimal.Parse(dcmNota);
                        }
                        tb49.FL_NOTA_ATIV = "S";
                        tb49.FL_JUSTI_NOTA_ATIV = "N";
                        tb49.CO_STATUS = "A";
                        var vTb119 = (from tb119 in TB119_ATIV_PROF_TURMA.RetornaTodosRegistros()
                                      where tb119.DT_ATIV_REAL.Year == tb49.DT_NOTA_ATIV.Year && tb119.DT_ATIV_REAL.Month == tb49.DT_NOTA_ATIV.Month &&
                                      tb119.DT_ATIV_REAL.Day == tb49.DT_NOTA_ATIV.Day && tb119.CO_EMP == coEmp
                                      && tb119.CO_MODU_CUR == coModuCur && tb119.CO_CUR == coCur && tb119.CO_TUR == coTur
                                      select new { tb119.CO_ATIV_PROF_TUR }).FirstOrDefault();

                        tb49.CO_ATIV_PROF_TUR = vTb119 != null ? vTb119.CO_ATIV_PROF_TUR : 0;
                        tb49.FL_LANCA_NOTA = "S";
                        tb49.TB03_COLABOR = TB03_COLABOR.RetornaPeloCoCol(linha.ID_COLAB_LANCA_NOTA);
                        tb49.DT_NOTA_ATIV_CAD = linha.DT_NOTA_CADAS;

                        TB49_NOTA_ATIV_ALUNO.SaveOrUpdate(tb49, true);

                        linha.ID_COLAB_BAIXA_NOTA = LoginAuxili.CO_COL;
                        linha.NR_IP_COLAB_BAIXA_NOTA = LoginAuxili.IP_USU;
                        linha.FL_BAIXA_NOTA = "S";
                        linha.DT_BAIXA_NOTA = dataPadraoAtual;
                        #endregion
                    }
                    else
                    {
                        #region Média
                        string ano = anoRefer.ToString();
                        TB079_HIST_ALUNO tb079 = (from iTb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                                                  where iTb079.CO_EMP == coEmp && iTb079.CO_ALU == coAlu
                                                  && iTb079.CO_MODU_CUR == coModuCur && iTb079.CO_CUR == coCur
                                                  && iTb079.CO_ANO_REF == ano && iTb079.CO_MAT == idGradeMater
                                                  select iTb079).FirstOrDefault();

                        if (tb079 != null)
                        {
                            var notas = (from lanc in portal.GW552_LANCA_NOTA.AsQueryable()
                                         where lanc.ID_INFOR_ALUNO == coAlu
                                         && lanc.ID_GRADE_MATER == idGradeMater
                                         && lanc.FL_LANCA_MEDIA == "S"
                                         join inf in portal.GW151_DADOS_MATRIC.AsQueryable() on lanc.ID_INFOR_ALUNO equals inf.ID_INFOR_ALUNO
                                             where inf.ID_INFOR_UNID == coEmp
                                             && inf.ID_MODAL_ENSINO == coModuCur
                                             && inf.ID_SERIE_ENSINO == coCur
                                             && inf.ID_TURMA_ENSINO == coTur
                                             select lanc);

                            string v1 = "";
                            string v2 = "";
                            string v3 = "";
                            string v4 = "";

                            if (notas != null && notas.Count() > 0)
                            {
                                // Grava as médias dos bimestres no histórico do aluno
                                var notaB1 = notas.Where(f => f.CO_REFER_NOTA == "B1").FirstOrDefault();
                                if (notaB1 != null && notaB1.VL_NOTA != "")
                                {
                                    v1 = notaB1.VL_NOTA.Trim() != "" ? notaB1.VL_NOTA.Replace(".", ",") : "";
                                    tb079.VL_NOTA_BIM1 = v1 != "" ? decimal.Parse(v1) : (decimal?)null;
                                }
                                var notaB2 = notas.Where(f => f.CO_REFER_NOTA == "B2").FirstOrDefault();
                                if (notaB2 != null && notaB2.VL_NOTA != "")
                                {
                                    v2 = notaB2.VL_NOTA.Trim() != "" ? notaB2.VL_NOTA.Replace(".", ",") : "";
                                    tb079.VL_NOTA_BIM2 = v2 != "" ? decimal.Parse(v2) : (decimal?)null;
                                }
                                var notaB3 = notas.Where(f => f.CO_REFER_NOTA == "B3").FirstOrDefault();
                                if (notaB3 != null && notaB3.VL_NOTA != "")
                                {
                                    v3 = notaB3.VL_NOTA.Trim() != "" ? notaB3.VL_NOTA.Replace(".", ",") : "";
                                    tb079.VL_NOTA_BIM3 = v3 != "" ? decimal.Parse(v3) : (decimal?)null;
                                }
                                var notaB4 = notas.Where(f => f.CO_REFER_NOTA == "B4").FirstOrDefault();
                                if (notaB4 != null && notaB4.VL_NOTA != "")
                                {
                                    v4 = notaB4.VL_NOTA.Trim() != "" ? notaB4.VL_NOTA.Replace(".", ",") : "";
                                    tb079.VL_NOTA_BIM4 = v4 != "" ? decimal.Parse(v4) : (decimal?)null;
                                }

                                // Grava as faltas dos bimestres no histórico do aluno
                                if (notaB1 != null && notaB1.QT_FALTA_MEDIA != null)
                                    tb079.QT_FALTA_BIM1 = notaB1.QT_FALTA_MEDIA;

                                if (notaB2 != null && notaB2.QT_FALTA_MEDIA != null)
                                    tb079.QT_FALTA_BIM2 = notaB2.QT_FALTA_MEDIA;

                                if (notaB3 != null && notaB3.QT_FALTA_MEDIA != null)
                                    tb079.QT_FALTA_BIM3 = notaB3.QT_FALTA_MEDIA;

                                if (notaB4 != null && notaB4.QT_FALTA_MEDIA != null)
                                    tb079.QT_FALTA_BIM4 = notaB4.QT_FALTA_MEDIA;
                            }

                            TB079_HIST_ALUNO.SaveOrUpdate(tb079, true);

                            linha.ID_COLAB_BAIXA_NOTA = LoginAuxili.CO_COL;
                            linha.NR_IP_COLAB_BAIXA_NOTA = LoginAuxili.IP_USU;
                            linha.FL_BAIXA_NOTA = "S";
                            linha.DT_BAIXA_NOTA = dataPadraoAtual;

                            foreach (var linha2 in notas)
                            {
                                linha2.ID_COLAB_BAIXA_NOTA = LoginAuxili.CO_COL;
                                linha2.NR_IP_COLAB_BAIXA_NOTA = LoginAuxili.IP_USU;
                                linha2.FL_BAIXA_NOTA = "S";
                                linha2.DT_BAIXA_NOTA = dataPadraoAtual;
                            }
                        }
                        else
                        {
                            // Não existe histórico para o aluno
                            // Esta regra ainda deve ser definida com o Cordova
                        }

                        #endregion
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
                    AuxiliPagina.RedirecionaParaPaginaSucesso("Base de dados atualizada com sucesso. Tabela modificada: TB49_NOTA_ATIV_ALUNO", Request.Url.AbsoluteUri);
                }
                else
                    AuxiliPagina.RedirecionaParaPaginaErro("Nenhum dado foi atualizado. Pois não existem registros para baixa na tabela de notas do aluno no portal.", Request.Url.AbsoluteUri);
            }
            else
                AuxiliPagina.RedirecionaParaPaginaSucesso("Sem novos registros para atualização.", Request.Url.AbsoluteUri);
            
        }                        
    }
}
