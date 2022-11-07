//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: MATRÍCULA NOVA PARA ALUNO
// OBJETIVO: REGISTRO DE ATIVIDADE EXTRA-CLASSE
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2107_MatriculaAluno.RegistroAtividadeExtraClasse
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //--------> Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
            CurrentPadraoCadastros.OnCarregaFormulario += new PadraoCadastros.OnCarregaFormularioHandler(CurrentPadraoCadastros_OnCarregaFormulario);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            /* if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao) ||
                 QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoExclusao))
                 AuxiliPagina.RedirecionaParaPaginaMensagem("Favor, selecione um aluno.", Request.Url.AbsolutePath.ToLower().Replace("cadastro", "busca") + "&moduloNome=" + Request.QueryString["moduloNome"], C2BR.GestorEducacao.UI.RedirecionaMensagem.TipoMessagemRedirecionamento.Error);
             */
            if (!IsPostBack)
            {
                CarregaAlunos();
                CarregaAtiviExtras();
                CarregaGridAtivExtra(int.Parse(Request.QueryString["coAlu"] != null ? Request.QueryString["coAlu"] : "0"), LoginAuxili.CO_EMP);
                carregaDados();
            };

            // txtFuncionario.Text = LoginAuxili.NOME_USU_LOGADO;
            //txtDataC.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { }//CarregaFormulario(); }


        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int coAlu = Convert.ToInt32(ddlAluno.SelectedValue);

            TB106_ATIVEXTRA_ALUNO tb106;

            //--------> Varre toda a grid de Atividades Extras
            foreach (GridViewRow linha in grdAtividade.Rows)
            {
                HiddenField hdCoTpAtv = ((HiddenField)linha.Cells[2].FindControl("hdCoAtiv"));
                HiddenField hdVlrAtiv = ((HiddenField)linha.Cells[2].FindControl("hdVlrAtiv"));

                //HiddenField hdCoTpAtv = ((HiddenField)linha.Cells[2].FindControl("hdCO_ATIV_EXTRA"));
                //HiddenField hdVlrAtiv = ((HiddenField)linha.Cells[2].FindControl("hdCO_INSC_ATIV"));

                int coAtivExtra = Convert.ToInt32(hdCoTpAtv.Value);

                if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                {
                    tb106 = (from lTb106 in TB106_ATIVEXTRA_ALUNO.RetornaTodosRegistros()
                             where lTb106.CO_EMP == LoginAuxili.CO_EMP && lTb106.CO_ALU == coAlu && lTb106.CO_ATIV_EXTRA == coAtivExtra
                             select lTb106).FirstOrDefault();

                    if (tb106 == null)
                    {
                        var tb07 = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                        tb106 = new TB106_ATIVEXTRA_ALUNO();

                        tb106.CO_ALU = coAlu;
                        tb106.CO_ATIV_EXTRA = coAtivExtra;
                        tb106.CO_EMP = LoginAuxili.CO_EMP;
                        tb106.TB07_ALUNO = tb07;
                        tb106.TB105_ATIVIDADES_EXTRAS = TB105_ATIVIDADES_EXTRAS.RetornaPelaChavePrimaria(coAtivExtra);
                        tb106.DT_CAD_ATIV = DateTime.Now;

                        if (hdVlrAtiv != null)
                        {
                            tb106.VL_ATIV_EXTRA = Convert.ToDecimal(hdVlrAtiv.Value);
                        }

                        tb07.TB25_EMPRESA1Reference.Load();
                        tb106.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(tb07.TB25_EMPRESA1.CO_EMP);

                        TB106_ATIVEXTRA_ALUNO.SaveOrUpdate(tb106, false);
                    }
                }
                else
                {
                    tb106 = (from lTb106 in TB106_ATIVEXTRA_ALUNO.RetornaTodosRegistros()
                             where lTb106.CO_EMP == LoginAuxili.CO_EMP && lTb106.CO_ALU == coAlu && lTb106.CO_ATIV_EXTRA == coAtivExtra
                             select lTb106).FirstOrDefault();

                    if (tb106 != null)
                        TB106_ATIVEXTRA_ALUNO.Delete(tb106, false);
                }
            }

            GestorEntities.CurrentContext.SaveChanges();

            AuxiliPagina.RedirecionaParaPaginaSucesso("Registro Efetuado com Sucesso", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        /*private void CarregaFormulario()
        {
            string anoMatricula = QueryStringAuxili.RetornaQueryStringPelaChave(QueryStrings.Ano);
            int coAlu = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoAlu);

            var alunoSelecionado = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                    join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb08.CO_CUR equals tb01.CO_CUR
                                    join tb06 in TB06_TURMAS.RetornaTodosRegistros() on tb08.CO_TUR equals tb06.CO_TUR
                                    join tb44 in TB44_MODULO.RetornaTodosRegistros() on tb01.CO_MODU_CUR equals tb44.CO_MODU_CUR
                                    where tb08.CO_ANO_MES_MAT == anoMatricula && tb08.CO_ALU == coAlu && tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                    select new
                                    {
                                        tb08.CO_CUR, tb08.CO_TUR, tb08.CO_ALU, tb08.TB07_ALUNO.NO_ALU, tb01.NO_CUR,
                                        tb06.TB129_CADTURMAS.NO_TURMA, tb44.CO_MODU_CUR, tb44.DE_MODU_CUR
                                    }).FirstOrDefault();

            if (alunoSelecionado != null)
            {
                txtAluno.Text = alunoSelecionado.NO_ALU;
                txtAno.Text = anoMatricula.ToString();
                txtModalidade.Text = alunoSelecionado.DE_MODU_CUR;
                txtSérie.Text = alunoSelecionado.NO_CUR;
                txtTurma.Text = alunoSelecionado.NO_TURMA;
                hdAluno.Value = alunoSelecionado.CO_ALU.ToString();
                hdCodMod.Value = alunoSelecionado.CO_MODU_CUR.ToString();
                hdSerie.Value = alunoSelecionado.CO_CUR.ToString();
                hdTurma.Value = alunoSelecionado.CO_TUR.ToString();

                CarregaGridDocumentos();
                CarregaValor();
            }
        }*/

        /// <summary>
        /// Método que carrega a grid de Atividades Extras
        /// </summary>
        private void CarregaAtiviExtras()
        {
            ddlAtivExtra.DataSource = TB105_ATIVIDADES_EXTRAS.RetornaTodosRegistros();
            ddlAtivExtra.DataBind();
            ddlAtivExtra.DataValueField = "CO_ATIV_EXTRA";
            ddlAtivExtra.DataTextField = "DES_ATIV_EXTRA";
            ddlAtivExtra.DataBind();

            ddlAtivExtra.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que atualiza o valor total no campo txtValor
        /// </summary>
        /*private void CarregaValor()
        {
            decimal decimalValorTotal = 0;

//--------> Varre toda a grid de Atividade
            foreach (GridViewRow linha in grdAtividade.Rows)
            {
                HiddenField hdVlrAtiv = ((HiddenField)linha.Cells[2].FindControl("hdVlrAtiv"));

                if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                    decimalValorTotal += Convert.ToDecimal(hdVlrAtiv.Value);
            }

            txtValor.Text = decimalValorTotal.ToString();
        }*/
        #endregion



        protected void ckSelect_CheckedChanged(object sender, EventArgs e)
        {
            //CarregaValor();            
        }
        protected void ddlAluno_SelectedIndexChanged(object sender, EventArgs e)
        {
            int coAlu = int.Parse(ddlAluno.SelectedValue);
            var lst = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb08.CO_ALU equals tb07.CO_ALU
                       join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb08.CO_CUR equals tb01.CO_CUR
                       join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on tb08.CO_TUR equals tb129.CO_TUR
                       where tb07.CO_ALU == coAlu
                       select new { tb07.CO_ALU, tb07.NO_ALU, tb08.TB44_MODULO.DE_MODU_CUR, tb01.NO_CUR, tb129.CO_SIGLA_TURMA, tb08.CO_ANO_MES_MAT }).First();
            txtModalidade.Text = lst.DE_MODU_CUR;
            txtSérie.Text = lst.NO_CUR;
            txtTurma.Text = lst.CO_SIGLA_TURMA;
            txtAno.Text = lst.CO_ANO_MES_MAT;

        }

        private void carregaDados()
        {
            int coAlu = 0;
            if (ddlAluno.SelectedValue != "")
                coAlu = int.Parse(ddlAluno.SelectedValue);
            var lst = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb08.CO_ALU equals tb07.CO_ALU
                       join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb08.CO_CUR equals tb01.CO_CUR
                       join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on tb08.CO_TUR equals tb129.CO_TUR
                       where tb07.CO_ALU == coAlu
                       select new { tb07.CO_ALU, tb07.NO_ALU, tb08.TB44_MODULO.DE_MODU_CUR, tb01.NO_CUR, tb129.CO_SIGLA_TURMA, tb08.CO_ANO_MES_MAT }).First();
            txtModalidade.Text = lst.DE_MODU_CUR;
            txtSérie.Text = lst.NO_CUR;
            txtTurma.Text = lst.CO_SIGLA_TURMA;
            txtAno.Text = lst.CO_ANO_MES_MAT;
        }

        private void CarregaAlunos()
        {
            int coAlu = 0;
            if (Request.QueryString["coAlu"] != null && Request.QueryString["coAlu"] != "")
                coAlu = int.Parse(Request.QueryString["coAlu"]);
            ddlAluno.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                   where tb08.CO_SIT_MAT.ToUpper() == "A" && (coAlu == 0 ? 0 == 0 : tb08.CO_ALU == coAlu)
                                   select new { tb08.CO_ALU, tb08.TB07_ALUNO.NO_ALU, }).OrderBy(a => a.NO_ALU);

            ddlAluno.DataValueField = "CO_ALU";
            ddlAluno.DataTextField = "NO_ALU";
            ddlAluno.DataBind();
            ddlAluno.Enabled = !(Request.QueryString["op"] != null && Request.QueryString["op"] == "edit");
        }
        protected void ddlAtivExtra_SelectedIndexChanged(object sender, EventArgs e)
        {
            int coAtvExt = 0;

            int.TryParse(ddlAtivExtra.SelectedValue, out coAtvExt);

            if (coAtvExt == 0)
            {
                return;
            }

            var lst = (from tb105 in TB105_ATIVIDADES_EXTRAS.RetornaTodosRegistros()
                       where tb105.CO_ATIV_EXTRA == coAtvExt
                       select new { tb105.CO_ATIV_EXTRA, tb105.DES_ATIV_EXTRA, tb105.VL_ATIV_EXTRA, tb105.SIGLA_ATIV_EXTRA }).First();
            txtSiglaAEA.Text = lst.SIGLA_ATIV_EXTRA;
            txtValorAEA.Text = lst.VL_ATIV_EXTRA.ToString(); ;
        }
        protected void lnkIncAtiExt_Click(object sender, EventArgs e)
        {
            //ControlaTabs("AEA");
            //ControlaChecks(chkRegAtiExt);

            //int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP;
            int coAlu = int.Parse(ddlAluno.SelectedValue);
            Decimal decimalRetorno;
            DateTime dataRetorno;

            var tb07 = TB07_ALUNO.RetornaPeloCoAlu(coAlu);

            TB106_ATIVEXTRA_ALUNO tb106 = new TB106_ATIVEXTRA_ALUNO();
            tb106.TB07_ALUNO = tb07;
            tb106.TB105_ATIVIDADES_EXTRAS = TB105_ATIVIDADES_EXTRAS.RetornaPelaChavePrimaria(int.Parse(ddlAtivExtra.SelectedValue));
            tb106.VL_ATIV_EXTRA = Decimal.TryParse(this.txtValorAEA.Text, out decimalRetorno) ? (Decimal?)decimalRetorno : null;
            tb106.DT_CAD_ATIV = DateTime.Now;
            tb106.DT_INI_ATIV = DateTime.TryParse(this.txtDtIniAEA.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
            tb106.DT_VENC_ATIV = DateTime.TryParse(this.txtDtFimAEA.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
            tb07.TB25_EMPRESA1Reference.Load();
            tb106.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(tb07.TB25_EMPRESA1.CO_EMP);

            TB106_ATIVEXTRA_ALUNO.SaveOrUpdate(tb106, true);

            ddlAtivExtra.SelectedIndex = 0;
            txtValorAEA.Text = txtDtIniAEA.Text = txtDtFimAEA.Text = "";

            CarregaGridAtivExtra(coAlu, tb07.TB25_EMPRESA1.CO_EMP);
        }
        private void CarregaGridAtivExtra(int coAlu, int coEmp)
        {
            grdAtividade.DataSource = (from tb106 in TB106_ATIVEXTRA_ALUNO.RetornaTodosRegistros()
                                       where tb106.CO_ALU == coAlu && tb106.CO_EMP == coEmp
                                       select new
                                       {
                                           tb106.TB105_ATIVIDADES_EXTRAS.DES_ATIV_EXTRA,
                                           tb106.TB105_ATIVIDADES_EXTRAS.SIGLA_ATIV_EXTRA,
                                           tb106.VL_ATIV_EXTRA,
                                           tb106.DT_VENC_ATIV,
                                           tb106.DT_INI_ATIV,
                                           tb106.CO_ALU,
                                           tb106.CO_EMP,
                                           tb106.CO_ATIV_EXTRA,
                                           tb106.CO_INSC_ATIV
                                       });
            grdAtividade.DataBind();
        }
        protected void lnkAtiExtAlu_Click(object sender, EventArgs e)
        {
            //chkRegAtiExt.Enabled = false;
            //lblSucRegAtiExt.Visible = true;

            AuxiliPagina.EnvioMensagemSucesso(this, "Atividade(s) Extra(s) atualizada(s) com sucesso.");

            //chkMatEsc.Checked = true;
            //ControlaTabs("UMA");
        }
        /*protected void ControlaTabs(string tab)
        {
            tabAluno.Style.Add("display", "none");
            tabCuiEspAdd.Style.Add("display", "none");
            tabEndAdd.Style.Add("display", "none");
            tabTelAdd.Style.Add("display", "none");
            tabResp.Style.Add("display", "none");
            tabResAliAdd.Style.Add("display", "none");
            tabUniMat.Style.Add("display", "none");
            tabDocumentos.Style.Add("display", "none");
            tabAtiExtAlu.Style.Add("display", "none");
            tabMenEsc.Style.Add("display", "none");

            if (tab == "CEA")
                tabCuiEspAdd.Style.Add("display", "block");
            else if (tab == "ALU")
                tabAluno.Style.Add("display", "block");
            else if (tab == "ENA")
                tabEndAdd.Style.Add("display", "block");
            else if (tab == "TEA")
                tabTelAdd.Style.Add("display", "block");
            else if (tab == "RES")
                tabResp.Style.Add("display", "block");
            else if (tab == "RAD")
                tabResAliAdd.Style.Add("display", "block");
            else if (tab == "UMA")
                tabUniMat.Style.Add("display", "block");
            else if (tab == "DOC")
                tabDocumentos.Style.Add("display", "block");
            else if (tab == "AEA")
                tabAtiExtAlu.Style.Add("display", "block");
            else if (tab == "MEN")
                tabMenEsc.Style.Add("display", "block");
        }*/
        protected void lnkExcAtiExt_Click(object sender, EventArgs e)
        {
            /* ControlaTabs("AEA");
             ControlaChecks(chkRegAtiExt);
             */
            //--------> Percorre todas as linhas da grid de Atividades
            foreach (GridViewRow linha in grdAtividade.Rows)
            {
                if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                {
                    HiddenField hdCO_ATIV_EXTRA = ((HiddenField)linha.Cells[0].FindControl("hdCO_ATIV_EXTRA"));
                    HiddenField hdCO_INSC_ATIV = ((HiddenField)linha.Cells[0].FindControl("hdCO_INSC_ATIV"));

                    int idAtiExt = Convert.ToInt32(hdCO_ATIV_EXTRA.Value);
                    int idCO_INSC_ATIV = Convert.ToInt32(hdCO_INSC_ATIV.Value);

                    //int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP;
                    int coAlu = int.Parse(ddlAluno.SelectedValue);

                    TB106_ATIVEXTRA_ALUNO atiExt = TB106_ATIVEXTRA_ALUNO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, coAlu, idAtiExt, idCO_INSC_ATIV);

                    TB106_ATIVEXTRA_ALUNO.Delete(atiExt, true);

                    CarregaGridAtivExtra(coAlu, LoginAuxili.CO_EMP);
                }
            }
        }

        /*protected void ckSelect_CheckedChanged(object sender, EventArgs e)
        {
            bool chkAtivExtra = false;

            foreach (GridViewRow linha in grdAtividade.Rows)
            {
                if (grdAtividade.Rows.Count > 0)
                {
                    if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                    {
                        if (linha.Cells[1].Text == hdfCoSeqSelec.Value)
                        {
                            ((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked = false;
                            hdfCoSeqSelec.Value = "";
                        }
                    }
                }
            }
        
            // Varre cada linha do grid
            foreach (GridViewRow linha in grdContratos.Rows)
            {
                if (grdContratos.Rows.Count > 0)
                {
                    if (((CheckBox)linha.Cells[0].FindControl("ckSelect")).Checked)
                    {
                        chkContrato = true;
                        hdfCoSeqSelec.Value = linha.Cells[1].Text;

                        MontaContratoSelecionado(int.Parse(linha.Cells[1].Text));
                    }
                }
            }

            if (!chkContrato)
                LimpaCampos();
        }*/
    }
}
