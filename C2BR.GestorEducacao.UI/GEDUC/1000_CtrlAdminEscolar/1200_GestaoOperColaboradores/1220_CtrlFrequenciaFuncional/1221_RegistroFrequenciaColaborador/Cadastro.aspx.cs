//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: GESTÃO OPERACIONAL DE FREQÜÊNCIA 
// OBJETIVO: REGISTRO DA FREQÜÊNCIA/PONTO DO COLABORADOR
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library;
using System.Data.Objects;
using C2BR.GestorEducacao.UI.App_Masters;

namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1220_CtrlFrequenciaFuncional.F1221_RegistroFrequenciaColaborador
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Variaveis

        int vCO_SEQ_FREQ = 1;
        string vTP_FREQ = "E";

        #endregion

        #region Eventos

        protected void Page_Init(object sender, EventArgs e)
        {
            base.OnPreInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtData.Text = DateTime.Now.ToString("dd/MM/yyyy");
                BuscaDadosRef();

                var resultado = from tb198 in TB198_USR_UNID_FREQ.RetornaTodosRegistros()
                                where tb198.CO_COL == LoginAuxili.CO_COL && tb198.CO_EMP == LoginAuxili.CO_UNID_FUNC && tb198.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                select tb198;
                
//------------> Verifica se colaborador tem a unidade vinculada como unidade de frequencia
                if (resultado.Count() == 0)
                    AuxiliPagina.RedirecionaParaNenhumaPagina("Colaborador não tem permissão para registro de frequência.", C2BR.GestorEducacao.UI.RedirecionaMensagem.TipoMessagemRedirecionamento.Error);                                    

                var colabor = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_UNID_FUNC, LoginAuxili.CO_COL);

                txtColaborador.Text = colabor.NO_COL;
                txtMatricula.Text = int.Parse(colabor.CO_MAT_COL.Replace(".", "").Replace("-", "")).ToString("00000000").Insert(7, "-").Insert(4, ".").Insert(1, ".");
                txtFuncao.Text = TB15_FUNCAO.RetornaPelaChavePrimaria(colabor.CO_FUN).NO_FUN;
                txtCategoriaFuncional.Text = colabor.FLA_PROFESSOR == "S" ? "Professor" : "Funcionário";
                txtUnidadeFrequencia.Text = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP).NO_FANTAS_EMP;
                txtHora.Text = DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00");
                txtTipo.Text = vTP_FREQ == "E" ? "Entrada" : "Saída";

                SetGridViewColumns();
                PreencheHistoricoFreq();
            }
        }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        protected void btnContrFrequ_Click(object sender, EventArgs e)
        {
            bool flaMultiFreq = false;

            var ocoFalta = (from iTb199 in TB199_FREQ_FUNC.RetornaTodosRegistros()
                            where iTb199.DT_FREQ.Year == DateTime.Now.Year && iTb199.FLA_PRESENCA == "N"
                            && iTb199.DT_FREQ.Month == DateTime.Now.Month && iTb199.DT_FREQ.Day == DateTime.Now.Day
                            && iTb199.TB03_COLABOR.CO_COL == LoginAuxili.CO_COL
                            select iTb199).FirstOrDefault();

            if (ocoFalta != null)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Colaborador já apresenta falta para a data informada.");
                return;
            }


            TB199_FREQ_FUNC tb199 = new TB199_FREQ_FUNC();

            BuscaDadosRef();

            if (!ValidaRegrasFrequencia())
            {
                AuxiliPagina.EnvioMensagemErro(this, "Horário não permitido");
                return;
            }

            DateTime dtFreq = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy"));
            int hora = int.Parse(DateTime.Now.ToString("HH:mm").Replace(":", ""));

//--------> Faz a verificação para saber se existe
            var ocorrFreqFunc = (from iTb199 in TB199_FREQ_FUNC.RetornaTodosRegistros()
                                 where iTb199.TB03_COLABOR.CO_COL == LoginAuxili.CO_COL && iTb199.DT_FREQ == dtFreq && iTb199.CO_EMP_ATIV == LoginAuxili.CO_EMP
                                 && iTb199.HR_FREQ == hora
                                 select iTb199).OrderBy(f => f.CO_SEQ_FREQ).ToList();

            if (ocorrFreqFunc.Count > 0)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Já existe ocorrência de frequência para a data e hora infomada.");
                return;
            }

//--------> Faz a verificação para saber se ponto é multifrequência de acordo com o "Colaborador"
            var vTb03 = (from iTb03 in TB03_COLABOR.RetornaTodosRegistros()
                        where iTb03.CO_COL == LoginAuxili.CO_COL
                        select new { iTb03.FL_MULTI_FREQU }).FirstOrDefault();

            if (vTb03 != null)
            {
                if (vTb03.FL_MULTI_FREQU != null)
                {
                    if (vTb03.FL_MULTI_FREQU == "S")
                        flaMultiFreq = true;
                }
                else
                {
//----------------> Faz a verificação para saber se ponto é multifrequência de acordo com a "Unidade"
                    var tb83 = (from iTb83 in TB83_PARAMETRO.RetornaTodosRegistros()
                                where iTb83.CO_EMP == LoginAuxili.CO_UNID_FUNC
                                select new { iTb83.FL_MULTI_FREQU }).FirstOrDefault();

                    if (tb83 != null)
                    {
                        if (tb83.FL_MULTI_FREQU != null)
                        {
                            if (tb83.FL_MULTI_FREQU == "S")
                                flaMultiFreq = true;
                        }
                        else
                        {
//------------------------> Faz a verificação para saber se ponto é multifrequência de acordo com a "Instituição"
                            var tb149 = (from iTb149 in TB149_PARAM_INSTI.RetornaTodosRegistros()
                                         where iTb149.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                         select new { iTb149.FL_MULTI_FREQU }).FirstOrDefault();

                            if (tb149 != null)
                            {
                                if (tb149.FL_MULTI_FREQU != null)
                                {
                                    if (tb149.FL_MULTI_FREQU == "S")
                                        flaMultiFreq = true;
                                }
                            }
                        }
                    }
                }
            }

            if (!flaMultiFreq)
            {
//------------> Faz a verificação para a quantidade de ocorrências de frequência para o dia informado
                var countFreqFunc = (from iTb199 in TB199_FREQ_FUNC.RetornaTodosRegistros()
                                     where iTb199.TB03_COLABOR.CO_COL == LoginAuxili.CO_COL && iTb199.DT_FREQ == dtFreq && iTb199.CO_EMP_ATIV == LoginAuxili.CO_EMP
                                     select iTb199).OrderBy(f => f.CO_SEQ_FREQ).ToList();

                if (countFreqFunc.Count >= 2)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Limite do ponto de frequência diário atingido.");
                    return;
                }
            }
            
            tb199.CO_EMP = LoginAuxili.CO_UNID_FUNC;
            tb199.CO_COL = LoginAuxili.CO_COL;
            tb199.DT_FREQ = dtFreq;
            tb199.HR_FREQ = hora;
            tb199.CO_SEQ_FREQ = vCO_SEQ_FREQ;
            tb199.CO_EMP_ATIV = LoginAuxili.CO_EMP;
            tb199.TP_FREQ = vTP_FREQ;
            var tb03 = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_UNID_FUNC, LoginAuxili.CO_COL);
            tb199.TB03_COLABOR = tb03;
            tb199.TB03_COLABOR1 = tb03;
            tb03.TB25_EMPRESA1Reference.Load();
            tb199.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(tb03.TB25_EMPRESA1.CO_EMP);
            tb199.FLA_PRESENCA = "S";
            tb199.DT_CADASTRO = DateTime.Now;
            tb199.STATUS = "A";

            CurrentPadraoCadastros.CurrentEntity = null;

            if (tb199 != null)
            {
                string strMensagem = vTP_FREQ == "E" ? "Entrada" : "Saída";
                strMensagem += " registrada com sucesso!";

                if (tb199.EntityState == System.Data.EntityState.Added)
                    if (GestorEntities.SaveOrUpdate(tb199) > 0)
                        AuxiliPagina.RedirecionaParaPaginaMensagem(strMensagem, this.AppRelativeVirtualPath + "&moduloNome=" + Request.QueryString["moduloNome"].ToString() + "&", C2BR.GestorEducacao.UI.RedirecionaMensagem.TipoMessagemRedirecionamento.Sucess);
            }
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Seta as colunas da grid de Histórico de Frequência
        /// </summary>
        void SetGridViewColumns()
        {
            grdHistoricoFrequencia.DataKeyNames = new string[] { "CO_EMP", "CO_COL", "DT_FREQ", "HR_FREQ", "CO_SEQ_FREQ" };

            grdHistoricoFrequencia.Columns.Add(new BoundField
            {
                DataField = "DT_FREQ",
                HeaderText = "Data",
                DataFormatString = "{0:dd/MM/yyyy}"
            });

            grdHistoricoFrequencia.Columns.Add(new BoundField
            {
                DataField = "HR_FREQ",
                HeaderText = "Hora",
                DataFormatString = "{0:00:00}"
            });

            grdHistoricoFrequencia.Columns.Add(new BoundField
            {
                DataField = "TP_FREQ",
                HeaderText = "Tipo"
            });

            grdHistoricoFrequencia.Columns.Add(new BoundField
            {
                DataField = "",
                HeaderText = "Observações"
            });
        }

        /// <summary>
        /// Preenche a grid de histórico de frequência
        /// </summary>
        void PreencheHistoricoFreq()
        {
            DateTime dataIni = DateTime.Parse(txtData.Text);
            DateTime dataFim = DateTime.Parse(txtData.Text);

            var resultado = from tb199 in TB199_FREQ_FUNC.RetornaTodosRegistros()
                            join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tb199.CO_COL equals tb03.CO_COL
                            join unidadeativ in TB25_EMPRESA.RetornaTodosRegistros() on tb199.CO_EMP_ATIV equals unidadeativ.CO_EMP
                            where tb199.TB03_COLABOR.CO_COL == LoginAuxili.CO_COL && (tb199.DT_FREQ >= dataIni && tb199.DT_FREQ <= dataFim)
                            && tb199.FLA_PRESENCA == "S"
                            select new
                            {
                                tb199.CO_EMP, tb199.CO_COL, tb03.NO_COL, tb03.CO_MAT_COL, tb199.DT_FREQ,
                                tb199.HR_FREQ, tb199.CO_SEQ_FREQ, tb199.CO_EMP_ATIV, unidadeativ.sigla,
                                TP_FREQ = tb199.TP_FREQ == "E" ? "Entrada" : ( tb199.TP_FREQ == "S" ? "Saída" : ( tb199.TP_FREQ == "F" ? "Falta" : ""))
                            };

            grdHistoricoFrequencia.DataSource = (resultado.Count() > 0) ? resultado : null;
            grdHistoricoFrequencia.DataBind();
        }

        /// <summary>
        /// Seta os valores vCO_SEQ_FREQ (CO_SEQ_FREQ + 1) e vTP_FREQ ("E"ntrada ou "S"aída)
        /// </summary>
        private void BuscaDadosRef()
        {
            DateTime dt_freq = DateTime.Parse(txtData.Text);

            var resultado = (from tb199 in TB199_FREQ_FUNC.RetornaTodosRegistros()
                             where tb199.TB03_COLABOR.CO_COL == LoginAuxili.CO_COL && tb199.DT_FREQ == dt_freq && tb199.CO_EMP_ATIV == LoginAuxili.CO_EMP
                             select tb199).OrderBy( f => f.CO_SEQ_FREQ ).ToList();

            if (resultado.Count > 0)
            {
                vCO_SEQ_FREQ = resultado.Last().CO_SEQ_FREQ + 1;
                vTP_FREQ = resultado.Last().TP_FREQ == "E" ? "S" : "E";
            }
        }        

        /// <summary>
        /// Faz a verificação para saber se a frequência está sendo registrada em um horário válido
        /// </summary>
        /// <returns>True ou false</returns>
        private bool ValidaRegrasFrequencia()
        {
            var tb198 = (from lTb198 in TB198_USR_UNID_FREQ.RetornaTodosRegistros()
                         where lTb198.CO_COL == LoginAuxili.CO_COL && lTb198.CO_EMP == LoginAuxili.CO_UNID_FUNC && lTb198.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                         select lTb198).First();
            
//--------> Faz a análise para saber se o tipo de Ponto de Frequência do Funcionário é Padrão
            if (tb198.TP_PONTO_FREQ == "P")
            {
                var tb149 = (from lTb149 in TB149_PARAM_INSTI.RetornaTodosRegistros()
                             where lTb149.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                             select lTb149).First();

                var tb03 = (from lTb03 in TB03_COLABOR.RetornaTodosRegistros()
                             where lTb03.CO_COL == LoginAuxili.CO_COL
                             select lTb03).First();

                var tb300 = (from lTb300 in TB300_QUADRO_HORAR_FUNCI.RetornaTodosRegistros()
                             where lTb300.ID_QUADRO_HORAR_FUNCI == tb03.ID_TIPO_PONTO
                             select lTb300).First();

//------------> Faz a verificação para saber se as regras de frequência são pela Instituição
                if (tb149.FLA_CTRL_FREQ == "I")
                {                    
//----------------> Faz a verificação para saber se o registro de Ponto está válido para manhã
                    if (tb149.TP_HORA_FUNC.Contains("M") &&
                       int.Parse(txtHora.Text.Replace(":", "")) >= int.Parse(tb300.HR_LIMIT_ENTRA) &&
                       int.Parse(txtHora.Text.Replace(":", "")) <= int.Parse(tb300.HR_LIMIT_SAIDA_EXTRA))
                        return true;

//----------------> Faz a verificação para saber se o registro de Ponto está válido para tarde
                    if (tb149.TP_HORA_FUNC.Contains("T") &&
                       int.Parse(txtHora.Text.Replace(":", "")) >= int.Parse(tb300.HR_LIMIT_ENTRA) &&
                       int.Parse(txtHora.Text.Replace(":", "")) <= int.Parse(tb300.HR_LIMIT_SAIDA_EXTRA))
                        return true;

//----------------> Faz a verificação para saber se o registro de Ponto está válido para noite
                    if (tb149.TP_HORA_FUNC.Contains("N") &&
                       int.Parse(txtHora.Text.Replace(":", "")) >= int.Parse(tb300.HR_LIMIT_ENTRA) &&
                       int.Parse(txtHora.Text.Replace(":", "")) <= int.Parse(tb300.HR_LIMIT_SAIDA_EXTRA))
                        return true;

                    return false;
                }
                else
                {
                    var tb25 = (from lTb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                where lTb25.CO_EMP == LoginAuxili.CO_EMP
                                select lTb25).First();

//----------------> Faz a verificação para saber se o registro de Ponto está válido para manhã
                    if (tb25.TP_HORA_FUNC.Contains("M") &&
                       int.Parse(txtHora.Text.Replace(":", "")) >= int.Parse(tb300.HR_LIMIT_ENTRA) &&
                       int.Parse(txtHora.Text.Replace(":", "")) <= int.Parse(tb300.HR_LIMIT_SAIDA_EXTRA))
                        return true;

//----------------> Faz a verificação para saber se o registro de Ponto está válido para tarde
                    if (tb25.TP_HORA_FUNC.Contains("T") &&
                       int.Parse(txtHora.Text.Replace(":", "")) >= int.Parse(tb300.HR_LIMIT_ENTRA) &&
                       int.Parse(txtHora.Text.Replace(":", "")) <= int.Parse(tb300.HR_LIMIT_SAIDA_EXTRA))
                        return true;

//----------------> Faz a verificação para saber se o registro de Ponto está válido para noite
                    if (tb25.TP_HORA_FUNC.Contains("N") &&
                       int.Parse(txtHora.Text.Replace(":", "")) >= int.Parse(tb300.HR_LIMIT_ENTRA) &&
                       int.Parse(txtHora.Text.Replace(":", "")) <= int.Parse(tb300.HR_LIMIT_SAIDA_EXTRA))
                        return true;

                    return false;
                }
            }

            return true;
        }
        #endregion        
    }
}