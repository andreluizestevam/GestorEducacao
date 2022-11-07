//================o=============================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: GESTÃO OPERACIONAL DE RH
// OBJETIVO: REGISTRO DO PONTO DO COLABORADOR
// DATA DE CRIAÇÃO: 07/05/2014
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//------------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
//           |                            | 

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
using System.Net;

namespace C2BR.GestorEducacao.UI.GSAUD._7000_ControleOperRH._7100_CtrlPontoFuncional._7110_RegisPontoFuncional
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Variaveis

        int vCO_SEQ_FREQ = 1;
        string vTP_FREQ = "E";
        string ipM;

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
                var colabor = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_UNID_FUNC, LoginAuxili.CO_COL);
                if (Request.UserHostAddress != colabor.IP_REGIS_PONTO.Trim())
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Registro do ponto não e permitido para este local ");
                }

                txtData.Text = DateTime.Now.ToString("dd/MM/yyyy");
                BuscaDadosRef();
                verificaColabPlanto();
                var resultado = from tb198 in TB198_USR_UNID_FREQ.RetornaTodosRegistros()
                                where tb198.CO_COL == LoginAuxili.CO_COL && tb198.CO_EMP == LoginAuxili.CO_UNID_FUNC && tb198.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                select tb198;

                //------------> Verifica se colaborador tem a unidade vinculada como unidade de frequencia
                if (resultado.Count() == 0)
                    AuxiliPagina.RedirecionaParaNenhumaPagina("Colaborador não tem permissão para registro de frequência.", C2BR.GestorEducacao.UI.RedirecionaMensagem.TipoMessagemRedirecionamento.Error);



                //string func = (TB128_FUNCA_FUNCI.RetornaPelaChavePrimaria(colabor.TB128_FUNCA_FUNCI.ID_FUNCA_FUNCI).NO_FUNCA_FUNCI != null ? TB128_FUNCA_FUNCI.RetornaPelaChavePrimaria(colabor.TB128_FUNCA_FUNCI.ID_FUNCA_FUNCI).NO_FUNCA_FUNCI : "");

                txtColaborador.Text = colabor.NO_COL;
                txtMatricula.Text = int.Parse(colabor.CO_MAT_COL.Replace(".", "").Replace("-", "")).ToString("00000000").Insert(7, "-").Insert(4, ".").Insert(1, ".");
                txtFuncao.Text = TB15_FUNCAO.RetornaPelaChavePrimaria(colabor.CO_FUN).NO_FUN;
                txtCategoriaFuncional.Text = colabor.FLA_PROFESSOR == "S" ? "Professor" : "Funcionário";
                txtUnidadeFrequencia.Text = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP).NO_FANTAS_EMP;
                txtHora.Text = DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00");
                txtTipo.Text = vTP_FREQ == "E" ? "Entrada" : "Saída";

                verificaFreqPlantao();

                SetGridViewColumns();
                PreencheHistoricoFreq();

                //string nomeMa = Dns.GetHostName();
                //IPAddress[] ip = Dns.GetHostAddresses(nomeMa);

                //ipM = ip[1].ToString();
            }
        }

        protected void verificaFreqPlantao()
        {
            DateTime dtAtu = DateTime.Now;
            string dtt = dtAtu.ToString("yyyy/MM/dd");
            DateTime dttConv = DateTime.Parse(dtt);

            if (ddlTipoPonto.SelectedValue == "P")
            {
                var tb199ob = (from tb199i in TB199_FREQ_FUNC.RetornaTodosRegistros()
                               where
                               (EntityFunctions.TruncateTime(tb199i.DT_FREQ) == EntityFunctions.TruncateTime(dttConv))
                               && (tb199i.TP_PONTO == "P")
                               select tb199i).FirstOrDefault();

                if (tb199ob != null)
                {
                    if (tb199ob.TP_FREQ == "S")
                        txtTipo.Text = "Saída";
                }
                else
                    txtTipo.Text = "Entrada";

            }
            else
            {
                DateTime dt_freq = DateTime.Parse(txtData.Text);

                var resultado = (from tb199 in TB199_FREQ_FUNC.RetornaTodosRegistros()
                                 where tb199.TB03_COLABOR.CO_COL == LoginAuxili.CO_COL && tb199.DT_FREQ == dt_freq && tb199.CO_EMP_ATIV == LoginAuxili.CO_EMP
                                 && tb199.TP_PONTO == "N"
                                 select tb199).OrderBy(f => f.CO_SEQ_FREQ).ToList();

                //if (resultado.Count > 0)
                //{
                //    vCO_SEQ_FREQ = resultado.Last().CO_SEQ_FREQ + 1;
                //    vTP_FREQ = resultado.Last().TP_FREQ == "E" ? "S" : "E";
                //}
                //txtTipo.Text = vTP_FREQ == "E" ? "Entrada" : "Saída";

                if (resultado.Count() > 0)
                {
                    txtTipo.Text = "Saída";
                    vTP_FREQ = "S";
                }
                else
                {
                    txtTipo.Text = "Entrada";
                    vTP_FREQ = "E";
                }
            }
        }


        protected void btnContrFrequ_Click(object sender, EventArgs e)
        {
            string tipoP = ddlTipoPonto.SelectedValue;

            if (tipoP == "N")
            {
                SalvaFrequencia();
            }
            else
            {

                int cocol = LoginAuxili.CO_COL;
                DateTime dtAtu = DateTime.Now;
                DateTime.TryParse(txtData.Text, out dtAtu);

                //TB159_AGENDA_PLANT_COLABOR tb159 = TB159_AGENDA_PLANT_COLABOR.RetornaPeloCoColEDiaAtual(cocol, dtAtu);
                //verficaFunc v = new verficaFunc();

                string dtt = dtAtu.ToString("yyyy/MM/dd");
                DateTime dttConv = DateTime.Parse(dtt);

                var res = (from tb159 in TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros()
                           where (tb159.TB03_COLABOR.CO_COL == cocol)
                           && (EntityFunctions.TruncateTime(tb159.DT_INICIO_PREV) == EntityFunctions.TruncateTime(dttConv))
                           select tb159).FirstOrDefault();


                if (res != null)
                {
                    txtidagendapla.Text = res.CO_AGEND_PLANT_COLAB.ToString();
                    int COPLA = res.CO_AGEND_PLANT_COLAB;

                    if (res.DT_INICIO_REAL != null)
                    {
                        if ((res.DT_TERMIN_REAL != null) && (res.CO_SITUA_AGEND == "R"))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "O Plantão Agendado para o Funcionário Logado já foi Realizado e tem Registro de Entrada e Saída.");
                        }
                        else
                        {
                            DateTime dtFreq = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy"));
                            int hora = int.Parse(DateTime.Now.ToString("HH:mm").Replace(":", ""));

                            TB199_FREQ_FUNC tb199 = new TB199_FREQ_FUNC();
                            tb199.CO_EMP = LoginAuxili.CO_UNID_FUNC;
                            tb199.CO_COL = LoginAuxili.CO_COL;
                            tb199.DT_FREQ = dtFreq;
                            tb199.HR_FREQ = hora;
                            tb199.CO_SEQ_FREQ = vCO_SEQ_FREQ;
                            tb199.CO_EMP_ATIV = LoginAuxili.CO_EMP;
                            tb199.TP_FREQ = "S";
                            var tb03 = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_UNID_FUNC, LoginAuxili.CO_COL);
                            tb199.TB03_COLABOR = tb03;
                            tb199.TB03_COLABOR1 = tb03;
                            tb03.TB25_EMPRESA1Reference.Load();
                            tb199.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(tb03.TB25_EMPRESA1.CO_EMP);
                            tb199.FLA_PRESENCA = "S";
                            tb199.DT_CADASTRO = DateTime.Now;
                            tb199.STATUS = "A";
                            tb199.TP_PONTO = "P";
                            tb199.TB159_AGENDA_PLANT_COLABOR = TB159_AGENDA_PLANT_COLABOR.RetornaPelaChavePrimaria(COPLA);
                            tb199.IP_REGIS_PONTO = Request.UserHostAddress;

                            CurrentPadraoCadastros.CurrentEntity = null;


                            res.DT_TERMIN_REAL = DateTime.Now;
                            res.CO_SITUA_AGEND = "R";
                            GestorEntities.SaveOrUpdate(res);
                            CurrentPadraoCadastros.CurrentEntity = res;

                            AuxiliPagina.RedirecionaParaPaginaSucesso("Saída do Plantão registrada com sucesso.", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());

                            //AuxiliPagina.EnvioMensagemSucesso(this.Page, "Saída do Plantão registrada com Sucesso.");
                        }
                    }
                    else
                    {
                        DateTime dtFreq = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy"));
                        int hora = int.Parse(DateTime.Now.ToString("HH:mm").Replace(":", ""));

                        TB199_FREQ_FUNC tb199 = new TB199_FREQ_FUNC();
                        tb199.CO_EMP = LoginAuxili.CO_UNID_FUNC;
                        tb199.CO_COL = LoginAuxili.CO_COL;
                        tb199.DT_FREQ = dtFreq;
                        tb199.HR_FREQ = hora;
                        tb199.CO_SEQ_FREQ = vCO_SEQ_FREQ + 1;
                        tb199.CO_EMP_ATIV = LoginAuxili.CO_EMP;
                        tb199.TP_FREQ = "E";
                        var tb03 = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_UNID_FUNC, LoginAuxili.CO_COL);
                        tb199.TB03_COLABOR = tb03;
                        tb199.TB03_COLABOR1 = tb03;
                        tb03.TB25_EMPRESA1Reference.Load();
                        tb199.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(tb03.TB25_EMPRESA1.CO_EMP);
                        tb199.FLA_PRESENCA = "S";
                        tb199.DT_CADASTRO = DateTime.Now;
                        tb199.STATUS = "A";
                        tb199.TP_PONTO = "P";
                        tb199.TB159_AGENDA_PLANT_COLABOR = TB159_AGENDA_PLANT_COLABOR.RetornaPelaChavePrimaria(COPLA);
                        tb199.IP_REGIS_PONTO = Request.UserHostAddress;

                        CurrentPadraoCadastros.CurrentEntity = null;

                        res.DT_INICIO_REAL = DateTime.Now;
                        GestorEntities.SaveOrUpdate(res);
                        CurrentPadraoCadastros.CurrentEntity = res;

                        AuxiliPagina.RedirecionaParaPaginaSucesso("Entrada do Plantão registrada com sucesso.", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());

                        //AuxiliPagina.EnvioMensagemSucesso(this.Page, "Entrada do Plantão registrada com Sucesso.");
                    }
                }
                else
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Não Existe Plantão Agendado para o Funcionário logado na Data de Hoje.");
            }
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Salva a Frequência do ponto do Colaborador
        /// </summary>
        private void SalvaFrequencia()
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

            //--------------Faz a verificação para saber se existe
            var ocorrFreqFunc = (from iTb199 in TB199_FREQ_FUNC.RetornaTodosRegistros()
                                 where iTb199.TB03_COLABOR.CO_COL == LoginAuxili.CO_COL && iTb199.DT_FREQ == dtFreq && iTb199.CO_EMP_ATIV == LoginAuxili.CO_EMP
                                 && iTb199.HR_FREQ == hora
                                 && iTb199.TP_PONTO == "N"
                                 select iTb199).OrderBy(f => f.CO_SEQ_FREQ).ToList();

            if (ocorrFreqFunc.Count > 0)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Já existe ocorrência de frequência para a data e hora infomada.");
                return;
            }
            //Verifica o endereço de ip onde e permitido bater o ponto 
            var vTb = (from iTb03 in TB03_COLABOR.RetornaTodosRegistros()
                         where iTb03.CO_COL == LoginAuxili.CO_COL
                       select new { iTb03.IP_REGIS_PONTO }).FirstOrDefault();
            if (Request.UserHostAddress != vTb.IP_REGIS_PONTO.Trim())
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Registro do ponto não e permitido para este local ");
                return;
            }

            //--------------Faz a verificação para saber se ponto é multifrequência de acordo com o "Colaborador"
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
                    //------------------> Faz a verificação para saber se ponto é multifrequência de acordo com a "Unidade"
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
                            //----------------------------> Faz a verificação para saber se ponto é multifrequência de acordo com a "Instituição"
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
                //---------------> Faz a verificação para a quantidade de ocorrências de frequência para o dia informado
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
            tb199.TP_FREQ = (txtTipo.Text == "Saída" ? "E" : "S");  
            var tb03 = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_UNID_FUNC, LoginAuxili.CO_COL);
            tb199.TB03_COLABOR = tb03;
            tb199.TB03_COLABOR1 = tb03;
            tb03.TB25_EMPRESA1Reference.Load();
            tb199.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(tb03.TB25_EMPRESA1.CO_EMP);
            tb199.FLA_PRESENCA = "S";
            tb199.DT_CADASTRO = DateTime.Now;
            tb199.STATUS = "A";
            tb199.TP_PONTO = "N";
            tb199.IP_REGIS_PONTO = Request.UserHostAddress;

            CurrentPadraoCadastros.CurrentEntity = null;

            if (tb199 != null)
            {
                string strMensagem = txtTipo.Text;
                strMensagem += " registrada com sucesso!";

                if (tb199.EntityState == System.Data.EntityState.Added)
                    if (GestorEntities.SaveOrUpdate(tb199) > 0)
                        AuxiliPagina.RedirecionaParaPaginaMensagem(strMensagem, this.AppRelativeVirtualPath + "&moduloNome=" + Request.QueryString["moduloNome"].ToString() + "&", C2BR.GestorEducacao.UI.RedirecionaMensagem.TipoMessagemRedirecionamento.Sucess);

            }


        }

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

            grdHistoricoFrequencia.Columns.Add(new BoundField
            {
                DataField = "tp_ponto",
                HeaderText = "Tipo Ponto"
            });

            grdHistoricoFrequencia.Columns.Add(new BoundField
            {
                DataField = "FL_INCON_PONTO",
                HeaderText = "IRP"
            });

            grdHistoricoFrequencia.Columns.Add(new BoundField
            {
                DataField = "IP_REGIS_PONTO",
                HeaderText = "IP"
            });
        }



        /// <summary>
        /// Preenche a grid de histórico de frequência
        /// </summary>
        void PreencheHistoricoFreq()
        {
            DateTime dataIni = DateTime.Parse(txtData.Text);
            DateTime dataFim = DateTime.Parse(txtData.Text);

            int mes = dataIni.Month;

            var resultado = (from tb199 in TB199_FREQ_FUNC.RetornaTodosRegistros()
                             join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tb199.CO_COL equals tb03.CO_COL
                             join unidadeativ in TB25_EMPRESA.RetornaTodosRegistros() on tb199.CO_EMP_ATIV equals unidadeativ.CO_EMP
                             where tb199.TB03_COLABOR.CO_COL == LoginAuxili.CO_COL && (tb199.DT_FREQ.Month == mes)
                             && tb199.FLA_PRESENCA == "S"
                             select new
                             {
                                 tb199.CO_EMP,
                                 tb199.CO_COL,
                                 tb03.NO_COL,
                                 tb03.CO_MAT_COL,
                                 tb199.DT_FREQ,
                                 tb199.HR_FREQ,
                                 tb199.CO_SEQ_FREQ,
                                 tb199.CO_EMP_ATIV,
                                 unidadeativ.sigla,
                                 tb199.FL_INCON_PONTO,
                                 tb199.IP_REGIS_PONTO,
                                 TP_FREQ = tb199.TP_FREQ == "E" ? "Entrada" : (tb199.TP_FREQ == "S" ? "Saída" : (tb199.TP_FREQ == "F" ? "Falta" : "")),
                                 tp_ponto = (tb199.TP_PONTO == "N" ? "Normal" : tb199.TP_PONTO == "P" ? "Plantão" : "")
                             }).OrderByDescending(w => w.DT_FREQ).ThenByDescending(y => y.HR_FREQ);
           
            //txtTipo.Text = tipo;
            grdHistoricoFrequencia.DataSource = (resultado.Count() > 0) ? resultado : null;
            grdHistoricoFrequencia.DataBind();
        }

        /// <summary>
        /// Verifica se o funcionário logado é plantonista, para visualização de uma div diferenciada.
        /// </summary>
        private void verificaColabPlanto()
        {
            var co = (from tb03c in TB03_COLABOR.RetornaTodosRegistros()
                      where (tb03c.CO_COL == LoginAuxili.CO_COL)
                      && (tb03c.FL_PERM_PLANT == "S")
                      select new { tb03c });

            if (co.Count() > 0)
            {
                HidColabPlanto.Value = "S";
                ddlTipoPonto.Enabled = true;
            }
        }

        #region Classes de Saída

        public class verficaFunc
        {
            public int coPla { get; set; }
            public DateTime dtinireal { get; set; }
            public DateTime dtterreal { get; set; }
        }

        #endregion

        /// <summary>
        /// Seta os valores vCO_SEQ_FREQ (CO_SEQ_FREQ + 1) e vTP_FREQ ("E"ntrada ou "S"aída)
        /// </summary>
        private void BuscaDadosRef()
        {
            DateTime dt_freq = DateTime.Parse(txtData.Text);

            var resultado = (from tb199 in TB199_FREQ_FUNC.RetornaTodosRegistros()
                             where tb199.TB03_COLABOR.CO_COL == LoginAuxili.CO_COL && tb199.DT_FREQ == dt_freq && tb199.CO_EMP_ATIV == LoginAuxili.CO_EMP
                             select tb199).OrderBy(f => f.CO_SEQ_FREQ).ToList();

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
                             select lTb149).FirstOrDefault();

                var tb03 = (from lTb03 in TB03_COLABOR.RetornaTodosRegistros()
                            where lTb03.CO_COL == LoginAuxili.CO_COL
                            select lTb03).FirstOrDefault();

                var tb300 = (from lTb300 in TB300_QUADRO_HORAR_FUNCI.RetornaTodosRegistros()
                             where lTb300.ID_QUADRO_HORAR_FUNCI == tb03.ID_TIPO_PONTO
                             select lTb300).FirstOrDefault();

                //------------> Faz a verificação para saber se as regras de frequência são pela Instituição
                if (tb149.FLA_CTRL_FREQ == "I")
                {
                    //if (tb300 != null)
                    //{

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

        #region Funções de Campo

        protected void ddlTipoPonto_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            verificaFreqPlantao();
        }
        #endregion
        //public void OnClick_SalvaPontoPlantao(object sender, EventArgs e)
        //{
        //    if (HidColabPlanto.Value == "S")
        //    {
        //        ///Varre toda a gride de Solicitações e salva na tabela TB114_FARDMAT 
        //        foreach (GridViewRow linha in grdAgendaPlantoes.Rows)
        //        {
        //            CheckBox grAg = ((CheckBox)linha.Cells[0].FindControl("chkselect"));

        //            if (grAg.Checked)
        //            {
        //                HiddenField hdfPla = ((HiddenField)linha.Cells[0].FindControl("hidcoPla"));
        //                int cop = int.Parse(hdfPla.Value);

        //                TB159_AGENDA_PLANT_COLABOR tb159 = TB159_AGENDA_PLANT_COLABOR.RetornaPelaChave(cop);

        //                DropDownList ddlTp = (DropDownList)linha.Cells[3].FindControl("ddlTipoPontoPlantao");

        //                string tipoPontoPla = ddlTp.SelectedValue.ToString();

        //                if (tipoPontoPla == "E")
        //                {
        //                    if (tb159.DT_INICIO_REAL != null)
        //                        AuxiliPagina.EnvioMensagemErro(this.Page, "Já existe um Ponto de Entrada lançado para este Plantão.");

        //                    else
        //                    {
        //                        tb159.DT_INICIO_REAL = DateTime.Now;

        //                        GestorEntities.SaveOrUpdate(tb159);
        //                        CurrentPadraoCadastros.CurrentEntity = tb159;
        //                    }

        //                }
        //                else
        //                {
        //                    if (tb159.DT_TERMIN_REAL != null)
        //                        AuxiliPagina.EnvioMensagemErro(this.Page, "Já existe um Ponto de Saída lançado para este Plantão.");

        //                    else
        //                    {
        //                        if (tb159.DT_INICIO_REAL == null)
        //                        {
        //                            tb159.DT_INICIO_REAL = DateTime.Now;
        //                        }

        //                        tb159.DT_TERMIN_REAL = DateTime.Now;
        //                        tb159.CO_SITUA_AGEND = "R";
        //                        GestorEntities.SaveOrUpdate(tb159);
        //                        CurrentPadraoCadastros.CurrentEntity = tb159;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
    }
}