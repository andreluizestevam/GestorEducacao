//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: MATRÍCULA NOVA PARA ALUNO
// OBJETIVO: MATRÍCULAS DE ALUNOS SIMPLIFICADA
// DATA DE CRIAÇÃO: 04/07/2013
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+----------------------------------------
// 04/07/2013| André Nobre Vinagre        | Criada a funcionalidade
//           |                            | 
// ----------+----------------------------+----------------------------------------
// 08/07/2013| André Nobre Vinagre        | Colocado o tratamento quando a não ocorrência de CPF e NIRE, 
//           |                            | corrigida a questão do carregamento da lista de alunos e
//           |                            | adicionada a verificação da ocorrência de matricula na mesma modalidade
//           |                            | regular e escolinha
//           |                            | 
// ----------+----------------------------+----------------------------------------
// 15/07/2013| André Nobre Vinagre        | Criado o botão de novo registro
//           |                            | 
// ----------+----------------------------+----------------------------------------
// 19/12/2016| Bruno Vieira Landim        | Alterado formato de geração de boleto
// ----------+----------------------------+----------------------------------------
// 20/12/2016| Alex Ribeiro da Silva      | Corrigido o processo de cadastro da funcionalidade
//           |                            |
// ----------+----------------------------+----------------------------------------
// 21/12/2016| Alex Ribeiro da Silva      | Alterado campo de turno
//           |                            |
//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Data;
using System.ServiceModel;
using System.IO;
using System.Reflection;
using Resources;
using WRAuxiliares = C2BR.GestorEducacao.DLLRelatorioWeb.Auxiliares;
using C2BR.GestorEducacao.DLLRelatorioWeb.Interfaces;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3010_CtrlPedagogicoSeries;
using C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2107_MatriculaAluno;
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3600_CtrlInformacoesAlunos;
using C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2111_PreMatriculaAluno;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2107_MatriculaAluno.MatriculaSimplificada
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }


        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                txtAno.Text = DateTime.Now.Year.ToString();
                CarregaModalidades();
                CarregaSerieCurso();
                CarregaTurma();

                //Desabilita campos do Financeiro
                ddlTipoContrato.Enabled =
                        chkIntegMensaSerie.Enabled =
                        chkValorContratoCalc.Enabled =
                        chkAlterValorContr.Enabled =
                        txtValorContratoCalc.Enabled =
                        ddlValorContratoCalc.Enabled =
                        ddlTipoContrato.Enabled =
                        chkTipoContrato.Enabled =
                        chkGeraTotalParce.Enabled =
                        txtQtdeParcelas.Enabled =
                        RequiredFieldValidator6.Enabled =
                        chkDataPrimeiraParcela.Enabled =
                        txtDtPrimeiraParcela.Enabled =
                        txtValorPrimParce.Enabled =
                        chkManterDesconto.Enabled =
                        ddlTpBolsaAlt.Enabled =
                        ddlBolsaAlunoAlt.Enabled =
                        txtValorDescto.Enabled =
                        chkManterDescontoPerc.Enabled =
                        txtPeriodoIniDesconto.Enabled =
                        txtPeriodoFimDesconto.Enabled =
                        ddlTipoDesctoMensa.Enabled =
                        txtQtdeMesesDesctoMensa.Enabled =
                        txtDesctoMensa.Enabled =
                        txtMesIniDesconto.Enabled =
                        ddlBoleto.Enabled =
                        txtValorContratoCalc.Enabled =
                        ddlDiaVecto.Enabled =
                        chkTipoContrato.Enabled = false;

                if (LoginAuxili.ORG_CODIGO_ORGAO == 0 || LoginAuxili.ORG_CODIGO_ORGAO == null)
                {
                    string script = "window.parent.location = '{0}';";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", string.Format(script, "/logout.aspx"), true);

                    return;
                }

                if (txtNoInfAluno.Text != "")
                {
                    //--------> Valida se o evento é de exibição do relatório gerado.
                    if (Session["ApresentaRelatorio"] != null)
                        if (int.Parse(Session["ApresentaRelatorio"].ToString()) == 1)
                        {
                            AuxiliPagina.AbreNovaJanela(this, Session["URLRelatorio"].ToString());
                            //----------------> Limpa a var de sessão com o url do relatório.
                            Session.Remove("URLRelatorio");
                            Session.Remove("ApresentaRelatorio");
                            //----------------> Limpa a ref da url utilizada para carregar o relatório.
                            PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                            isreadonly.SetValue(this.Request.QueryString, false, null);
                            isreadonly.SetValue(this.Request.QueryString, true, null);
                        }
                }
                else
                {
                    Session.Remove("URLRelatorio");
                    Session.Remove("ApresentaRelatorio");
                }

                DateTime dataIniMatric;
                DateTime dataFimMatric;

                TB149_PARAM_INSTI tb149 = TB149_PARAM_INSTI.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);

                //------------> Faz a verificação para saber se o controle de datas é por Instituição ou Unidade, depois carrega informações do mesmo
                if (tb149.FLA_CTRL_DATA == TipoControle.I.ToString())
                {
                    dataIniMatric = Convert.ToDateTime(tb149.DT_INI_MAT);
                    dataFimMatric = Convert.ToDateTime(tb149.DT_FIM_MAT);
                }
                else if (tb149.FLA_CTRL_DATA == TipoControle.U.ToString())
                {
                    TB25_EMPRESA tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

                    tb25.TB82_DTCT_EMPReference.Load();
                    dataIniMatric = Convert.ToDateTime(tb25.TB82_DTCT_EMP.DT_INI_MAT);
                    dataFimMatric = Convert.ToDateTime(tb25.TB82_DTCT_EMP.DT_FIM_MAT);
                }
                else
                    dataIniMatric = dataFimMatric = DateTime.Now;

                //------------> Faz a verificação para saber se Data de Matrícula está no período correto
                DateTime dataHoje = DateTime.Now;
                if ((dataIniMatric.Date > dataHoje.Date) || (dataFimMatric.Date < dataHoje.Date))
                    AuxiliPagina.RedirecionaParaNenhumaPagina("Não é possível efetuar Matrícula. Data de Matrícula está fora do Período determinado.", C2BR.GestorEducacao.UI.RedirecionaMensagem.TipoMessagemRedirecionamento.Error);
                else
                {
                    if (txtNoInfAluno.Text != "")
                    {
                        //--------> Valida se o evento é de exibição do relatório gerado.
                        if (Session["ApresentaRelatorio"] != null)
                            if (int.Parse(Session["ApresentaRelatorio"].ToString()) == 1)
                            {
                                AuxiliPagina.AbreNovaJanela(this, Session["URLRelatorio"].ToString());
                                //----------------> Limpa a var de sessão com o url do relatório.
                                Session.Remove("URLRelatorio");
                                Session.Remove("ApresentaRelatorio");
                                //----------------> Limpa a ref da url utilizada para carregar o relatório.
                                PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                                isreadonly.SetValue(this.Request.QueryString, false, null);
                                isreadonly.SetValue(this.Request.QueryString, true, null);
                            }
                    }
                    else
                    {
                        Session.Remove("URLRelatorio");
                        Session.Remove("ApresentaRelatorio");
                    }

                    this.CarregaBolsasAlt();
                }

                //-----------> Valida se o CheckBox Atualizar Financeira está checado, se for o caso, o formulário estará disponível
                if (!chkAtualiFinan.Checked)
                {
                    ddlTipoContrato.Enabled =
                        chkIntegMensaSerie.Enabled =
                        chkValorContratoCalc.Enabled =
                        chkAlterValorContr.Enabled =
                        txtValorContratoCalc.Enabled =
                        ddlValorContratoCalc.Enabled =
                        ddlTipoContrato.Enabled =
                        chkTipoContrato.Enabled =
                        chkGeraTotalParce.Enabled =
                        txtQtdeParcelas.Enabled =
                        RequiredFieldValidator6.Enabled =
                        chkDataPrimeiraParcela.Enabled =
                        txtDtPrimeiraParcela.Enabled =
                        txtValorPrimParce.Enabled =
                        chkManterDesconto.Enabled =
                        ddlTpBolsaAlt.Enabled =
                        ddlBolsaAlunoAlt.Enabled =
                        txtValorDescto.Enabled =
                        chkManterDescontoPerc.Enabled =
                        txtPeriodoIniDesconto.Enabled =
                        txtPeriodoFimDesconto.Enabled =
                        ddlTipoDesctoMensa.Enabled =
                        txtQtdeMesesDesctoMensa.Enabled =
                        txtDesctoMensa.Enabled =
                        txtMesIniDesconto.Enabled =
                        ddlBoleto.Enabled =
                        txtValorContratoCalc.Enabled =
                        ddlDiaVecto.Enabled =
                        chkTipoContrato.Enabled = false;
                }
                else
                {
                    var admUsu = (from adUs in ADMUSUARIO.RetornaTodosRegistros()
                                  where adUs.CodUsuario == LoginAuxili.CO_COL
                                  select new { adUs.FLA_ALT_BOL_ALU, adUs.FLA_ALT_BOL_ESPE_ALU, adUs.FLA_ALT_PARAM_MAT }).FirstOrDefault();

                    if (admUsu != null)
                    {
                        //-----------> Valida se o usuário possui permissão para alterar o desconto dado ao aluno.
                        if (admUsu.FLA_ALT_BOL_ALU == "S")
                        {
                            chkManterDesconto.Enabled = true;
                        }
                        else
                        {
                            chkManterDesconto.Enabled =
                            txtValorDescto.Enabled =
                            chkManterDescontoPerc.Enabled =
                            txtPeriodoIniDesconto.Enabled =
                            txtPeriodoFimDesconto.Enabled = false;
                        }

                        //-----------> Valida se o usuário possui permissão para alterar o desconto especial dado ao aluno.
                        if (admUsu.FLA_ALT_BOL_ESPE_ALU == "S")
                        {
                            ddlTipoDesctoMensa.Enabled =
                            txtDesctoMensa.Enabled = true;
                        }
                        else
                        {
                            ddlTipoDesctoMensa.Enabled =
                            txtDesctoMensa.Enabled =
                            txtMesIniDesconto.Enabled = false;
                        }

                        //-----------> Valida se o usuário possui permissão para alterar parâmetros da matrícula
                        if (admUsu.FLA_ALT_PARAM_MAT == "S")
                        {
                            chkTipoContrato.Enabled =
                            chkIntegMensaSerie.Enabled =
                            chkGeraTotalParce.Enabled =
                            chkDataPrimeiraParcela.Enabled = true;
                        }
                        else
                        {
                            chkTipoContrato.Enabled =
                            chkIntegMensaSerie.Enabled =
                            chkGeraTotalParce.Enabled =
                            txtValorContratoCalc.Enabled =
                            chkDataPrimeiraParcela.Enabled = false;
                        }
                    }
                    else
                    {
                        chkManterDesconto.Enabled =
                            ddlTpBolsaAlt.Enabled =
                            ddlBolsaAlunoAlt.Enabled =
                            txtValorDescto.Enabled =
                            chkManterDescontoPerc.Enabled =
                            txtPeriodoIniDesconto.Enabled =
                            txtPeriodoFimDesconto.Enabled = false;
                        ddlTipoDesctoMensa.Enabled =
                            txtQtdeMesesDesctoMensa.Enabled =
                            txtDesctoMensa.Enabled =
                            txtMesIniDesconto.Enabled =
                            chkTipoContrato.Enabled =
                            chkIntegMensaSerie.Enabled =
                            chkGeraTotalParce.Enabled =
                            txtValorContratoCalc.Enabled =
                            chkDataPrimeiraParcela.Enabled = false;
                    }
                }
            }
            else
            {
                if (txtNoInfAluno.Text != "")
                {
                    //--------> Valida se o evento é de exibição do relatório gerado.
                    if (Session["ApresentaRelatorio"] != null)
                        if (int.Parse(Session["ApresentaRelatorio"].ToString()) == 1)
                        {
                            AuxiliPagina.AbreNovaJanela(this, Session["URLRelatorio"].ToString());
                            //----------------> Limpa a var de sessão com o url do relatório.
                            Session.Remove("URLRelatorio");
                            Session.Remove("ApresentaRelatorio");
                            //----------------> Limpa a ref da url utilizada para carregar o relatório.
                            PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                            isreadonly.SetValue(this.Request.QueryString, false, null);
                            isreadonly.SetValue(this.Request.QueryString, true, null);
                        }
                }
                else
                {
                    Session.Remove("URLRelatorio");
                    Session.Remove("ApresentaRelatorio");
                }

            }

            if (!String.IsNullOrEmpty(ddlTurma.SelectedValue) && !chkAlterValorContr.Checked)
            {
                //-----------> Pega o Curso
                int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
                int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
                var varSer = TB01_CURSO.RetornaPelaChavePrimaria(ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP, modalidade, serie);
                string turnoTurma = TB06_TURMAS.RetornaPeloCodigo(int.Parse(ddlTurma.SelectedValue)).CO_PERI_TUR;

                if (varSer != null)
                {
                    //-------> Verifica se é para utilizar o valor integral
                    if (chkIntegMensaSerie.Checked)
                    {
                        #region Valor integral
                        switch (ddlTipoContrato.SelectedValue)
                        {
                            case "P":
                                if (String.IsNullOrEmpty(varSer.VL_CONTINT_APRAZ.ToString()))
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "A Modalidade de Ensino \"" + varSer.NO_CUR + "\" não possui valor integral a prazo.");
                                    return;
                                }
                                txtValorContratoCalc.Text = varSer.VL_CONTINT_APRAZ.ToString();
                                break;
                            case "V":
                                if (String.IsNullOrEmpty(varSer.VL_CONTINT_AVIST.ToString()))
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "A Modalidade de Ensino \"" + varSer.NO_CUR + "\" não possui valor integral a vista.");
                                    return;
                                }
                                txtValorContratoCalc.Text = varSer.VL_CONTINT_AVIST.ToString();
                                break;
                        }
                        #endregion
                    }
                    else
                    {
                        //-----------> Valida o turno do curso e coloca o valor do contrato no campo de valor do contrato de acordo com o turno da turma.
                        switch (turnoTurma)
                        {
                            #region Turno Matutino
                            case "M":
                                switch (ddlTipoContrato.SelectedValue)
                                {
                                    case "P":
                                        if (varSer.VL_CONTMAN_APRAZ == null)
                                        {
                                            AuxiliPagina.EnvioMensagemErro(this.Page, "A Modalidade de Ensino \"" + varSer.NO_CUR + "\" não possui valor a prazo para o turno Matutino.");
                                            return;
                                        }
                                        txtValorContratoCalc.Text = varSer.VL_CONTMAN_APRAZ.ToString();
                                        break;
                                    case "V":
                                        if (varSer.VL_CONTMAN_AVIST == null)
                                        {
                                            AuxiliPagina.EnvioMensagemErro(this.Page, "A Modalidade de Ensino \"" + varSer.NO_CUR + "\" não possui valor a vista para o turno Matutino.");
                                            return;
                                        }
                                        txtValorContratoCalc.Text = varSer.VL_CONTMAN_AVIST.ToString();
                                        break;
                                }
                                break;
                            #endregion

                            #region Turno Vespertino
                            case "V":
                                switch (ddlTipoContrato.SelectedValue)
                                {
                                    case "P":
                                        if (varSer.VL_CONTTAR_APRAZ == null)
                                        {
                                            AuxiliPagina.EnvioMensagemErro(this.Page, "A Modalidade de Ensino \"" + varSer.NO_CUR + "\" não possui valor a prazo para o turno Vespertino.");
                                            return;
                                        }

                                        txtValorContratoCalc.Text = varSer.VL_CONTTAR_APRAZ.ToString();
                                        break;
                                    case "V":
                                        if (varSer.VL_CONTTAR_AVIST == null)
                                        {
                                            AuxiliPagina.EnvioMensagemErro(this.Page, "A Modalidade de Ensino \"" + varSer.NO_CUR + "\" não possui valor a vista para o turno Vespertino.");
                                            return;
                                        }
                                        txtValorContratoCalc.Text = varSer.VL_CONTTAR_AVIST.ToString();
                                        break;
                                }
                                break;
                            #endregion

                            #region Turno Noturno
                            case "N":
                                switch (ddlTipoContrato.SelectedValue)
                                {
                                    case "P":
                                        if (varSer.VL_CONTNOI_APRAZ == null)
                                        {
                                            AuxiliPagina.EnvioMensagemErro(this.Page, "A Modalidade de Ensino \"" + varSer.NO_CUR + "\" não possui valor a prazo para o turno Vespertino.");
                                            return;
                                        }
                                        txtValorContratoCalc.Text = varSer.VL_CONTNOI_APRAZ.ToString();
                                        break;
                                    case "V":
                                        if (varSer.VL_CONTNOI_AVIST == null)
                                        {
                                            AuxiliPagina.EnvioMensagemErro(this.Page, "A Modalidade de Ensino \"" + varSer.NO_CUR + "\" não possui valor a vista para o turno Vespertino.");
                                            return;
                                        }
                                        txtValorContratoCalc.Text = varSer.VL_CONTNOI_AVIST.ToString();
                                        break;
                                }
                                break;
                            #endregion
                        }
                    }

                    atualizaParcela();
                }
            }

            if (txtDtPrimeiraParcela.Text == "")
            {
                txtDtPrimeiraParcela.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }
        #endregion

        #region Inclusões

        /// <summary>
        /// Faz a Confirmação da modalidade, série e turma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkConfirModSerTur_Click(object sender, EventArgs e)
        {

            int serieCu = int.Parse(ddlSerieCurso.SelectedValue);

            var res = (from tb01s in TB01_CURSO.RetornaTodosRegistros()
                       where tb01s.CO_CUR == serieCu
                       select new
                       {
                           tb01s.NU_MDV
                       }).FirstOrDefault();

            ddlDiaVecto.SelectedValue = res.NU_MDV.ToString();

            TB01_CURSO tb01 = TB01_CURSO.RetornaPeloCoCur(int.Parse(ddlSerieCurso.SelectedValue));

            if (hdCoRes.Value == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Responsável deve ser informado.");
                return;
            }

            if (hdCoAlu.Value == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Aluno deve ser informado.");
                return;
            }

            if (tb01 != null)
            {
                int coAlu = Convert.ToInt32(this.hdCoAlu.Value);
                if (!(tb01.CO_NIVEL_CUR == "I" || tb01.CO_NIVEL_CUR == "F"
                                || tb01.CO_NIVEL_CUR == "M"))
                {
                    var ocoMat = (from lTb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                  where lTb08.TB07_ALUNO.CO_ALU == coAlu && lTb08.CO_CUR == tb01.CO_CUR && lTb08.CO_SIT_MAT == "A"
                                  && lTb08.TB44_MODULO.CO_MODU_CUR == tb01.CO_MODU_CUR && lTb08.CO_ANO_MES_MAT.Contains(txtAno.Text)
                                  select new { lTb08.CO_SIT_MAT, lTb08.CO_ANO_MES_MAT, lTb08.TB44_MODULO.DE_MODU_CUR });

                    if (ocoMat.Count() > 0)
                    {
                        if (ocoMat.First().DE_MODU_CUR.ToLower().Contains("escolinh"))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Aluno já possui matrícula para ano, modalidade e escolinha informados.");
                        }
                        else
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Aluno já possui matrícula para ano, modalidade e série/curso informados.");
                        return;
                    }
                }
            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Série informada não existe.");
                return;
            }

            txtQtdeParcelas.Text = tb01.NU_QUANT_MESES.ToString();
            ddlModalidade.Enabled = ddlSerieCurso.Enabled = ddlTurma.Enabled = txtAno.Enabled = chkIntegMensaSerie.Enabled = false;
            lblSucDadosMatr.Visible = true;
            lnkConfirModSerTur.Enabled = false;

            int coEmpUnidCont = TB129_CADTURMAS.RetornaPelaChavePrimaria(int.Parse(ddlTurma.SelectedValue)).CO_EMP_UNID_CONT;
            if (TB83_PARAMETRO.RetornaPelaChavePrimaria(coEmpUnidCont).FL_INTEG_FINAN == "S")
            {
                CarregaBoletos();
                chkMenEscAlu.Checked = chkAtualiFinan.Checked = lnkMontaGridMensa.Enabled = chkAtualiFinan.Enabled = true;
            }
            else
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Série informada não existe.");
                chkMenEscAlu.Enabled = chkAtualiFinan.Checked = lnkMontaGridMensa.Enabled = chkAtualiFinan.Enabled = false;
                return;
            }

            chkAtualiFinan_CheckedChanged(null, null);
        }

        /// <summary>
        /// Faz a Efetivação da Matrícula do Aluno
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkEfetMatric_Click(object sender, EventArgs e)
        {
            int coAlu;
            //string tipoMatricula = ddlSituMatAluno.SelectedValue != null ? ddlSituMatAluno.SelectedValue == "X" ? "R" : "A"; 

            if (txtAno.Text == "" || ddlModalidade.SelectedValue == "" || ddlSerieCurso.SelectedValue == "" || ddlTurma.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this, "Ano, Modalidade, Série e Turma devem ser informados.");
                return;
            }

            if (this.hdCoAlu.Value == "")
            {
                AuxiliPagina.EnvioMensagemErro(this, "Primeiro cadastre o Aluno.");
                return;
            }
            else
            {
                int nire = int.Parse(this.hdCoAlu.Value);
                coAlu = TB07_ALUNO.RetornaTodosRegistros().Where(x => x.NU_NIRE == nire).FirstOrDefault().CO_ALU;
                int proxTurma = Convert.ToInt32(this.ddlTurma.SelectedValue);
                int coEmp = TB129_CADTURMAS.RetornaPelaChavePrimaria(proxTurma).CO_EMP_UNID_CONT;

                if (chkAtualiFinan.Checked)
                {
                    if (grdNegociacao.Rows.Count == 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "É necessário gerar a grid de mensalidades.");
                        return;
                    }

                    TB44_MODULO tb44 = TB44_MODULO.RetornaPelaChavePrimaria(Convert.ToInt32(this.ddlModalidade.SelectedValue));

                    if (tb44.CO_SEQU_PC == null || tb44.CO_SEQU_PC_BANCO == null || tb44.CO_SEQU_PC_CAIXA == null)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Modalidade selecionada para cadastro no financeiro não possui código de conta contábil ativa, de caixa e de banco associados.");
                        return;
                    }
                }

                int modalidade = Convert.ToInt32(this.ddlModalidade.SelectedValue);
                int proxSerie = Convert.ToInt32(this.ddlSerieCurso.SelectedValue);
                int qtdZero = 6 - coAlu.ToString().Length;
                string strAluno = "";

                for (int i = 0; i < qtdZero; i++)
                    strAluno = strAluno + "0";

                strAluno = strAluno + coAlu.ToString();
                ///string strProxMatricula = DateTime.Now.Year.ToString().Substring(2, 2) + coEmp.ToString() + strAluno;
                string strProxMatricula = txtAno.Text.ToString().Substring(2, 2) + proxSerie.ToString().PadLeft(3, '0') + strAluno;
                ///string proxAno = DateTime.Now.Year.ToString();
                string proxAno = txtAno.Text;
                string turno = TB06_TURMAS.RetornaPeloCodigo(proxTurma).CO_PERI_TUR;

                TB01_CURSO hTb01 = TB01_CURSO.RetornaPeloCoCur(proxSerie);

                TB08_MATRCUR tb08 = TB08_MATRCUR.RetornaPelaChavePrimaria(coAlu, proxSerie, proxAno, "1");

                if (tb08 != null)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Não é possível efetuar Matrícula. Aluno já matriculado.");
                    return;
                }

                ///Monta o texto do desconto, apresentado no relatório
                string noDesconto = "";
                noDesconto = ddlTpBolsaAlt.SelectedValue == "N" ? "XXX " : chkManterDesconto.Checked == false ? "XXX " : ddlTpBolsaAlt.SelectedValue == "C" ? "CON " : "BOL ";
                int bolsa = ddlBolsaAlunoAlt.SelectedValue != "" ? int.Parse(ddlBolsaAlunoAlt.SelectedValue) : 0;
                string bolsaPerc = txtValorDescto.Text;
                bolsaPerc += chkManterDescontoPerc.Checked ? "%" : "";
                noDesconto += chkManterDesconto.Checked ? (bolsa != 0 ? TB148_TIPO_BOLSA.RetornaTodosRegistros().Where(r => r.CO_TIPO_BOLSA == bolsa).FirstOrDefault().NO_TIPO_BOLSA : "*****") : "ESPECIAL " + bolsaPerc;

                tb08 = new TB08_MATRCUR();
                var refAluno = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                tb08.TB07_ALUNO = refAluno;
                refAluno.TB25_EMPRESA1Reference.Load();
                tb08.TB25_EMPRESA = refAluno.TB25_EMPRESA1;
                tb08.CO_EMP_UNID_CONT = coEmp;
                tb08.CO_ALU_CAD = strProxMatricula;
                tb08.CO_ANO_MES_MAT = proxAno;
                tb08.CO_COL = new int?(LoginAuxili.CO_COL);
                tb08.CO_CUR = proxSerie;
                tb08.CO_SIT_MAT = "A";
                tb08.CO_TUR = new int?(proxTurma);
                tb08.DT_CAD_MAT = DateTime.Now;
                tb08.DT_CADASTRO = new DateTime?(DateTime.Now);
                tb08.DT_EFE_MAT = DateTime.Now;
                tb08.DT_SIT_MAT = DateTime.Now;

                ///Pega o tipo de bolsa selecionado pelo usuário e grava na tabela TB08_MATRCUR que é a tabela de matrícula
                if (ddlBolsaAlunoAlt.SelectedValue != "")
                {
                    TB148_TIPO_BOLSA tb148M = TB148_TIPO_BOLSA.RetornaPelaChavePrimaria(int.Parse(ddlBolsaAlunoAlt.SelectedValue));
                    tb08.TB148_TIPO_BOLSA = tb148M;
                }

                ///Pega o responsável selecionado pelo usuário e gravana tablea de matrícula, TB08_MATRCUR
                int coResp = this.hdCoRes.Value != "" ? Convert.ToInt32(this.hdCoRes.Value) : 0;
                TB108_RESPONSAVEL tb108M = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(coResp);
                if (tb108M != null)
                {
                    tb08.TB108_RESPONSAVEL = tb108M;
                }

                tb08.CO_TURN_MAT = turno;
                tb08.FLA_REMATRICULADO = "N";
                tb08.FLA_ESCOLA_NOVATO = true;
                tb08.FLA_ESCOLA_ANO_NOVATO = true;
                tb08.NU_SEM_LET = "1";
                tb08.TB44_MODULO = TB44_MODULO.RetornaPelaChavePrimaria(modalidade);
                tb08.NO_DESCONTO = noDesconto;
                if (chkAtualiFinan.Checked)
                {
                    tb08.VL_TOT_MODU_MAT = decimal.Parse(txtTotalMensa.Text);
                    tb08.VL_DES_MOD_MAT = decimal.Parse(txtTotalDesctoEspec.Text);
                    tb08.VL_DES_BOL_MOD_MAT = decimal.Parse(txtTotalDesctoBolsa.Text);
                    tb08.VL_ENT_MOD_MAT = decimal.Parse(grdNegociacao.Rows[0].Cells[3].Text);
                    tb08.VL_PAR_MOD_MAT = decimal.Parse(grdNegociacao.Rows[0].Cells[3].Text);
                    int qtPar = 0;
                    foreach (GridViewRow l in grdNegociacao.Rows)
                    {
                        if (int.Parse(l.Cells[1].Text) != 0)
                        {
                            qtPar++;
                        }
                        else
                        {
                            tb08.VL_TAXA_MATRIC = decimal.Parse(l.Cells[3].Text);
                        }
                    }

                    //tb08.QT_PAR_MOD_MAT = grdNegociacao.Rows.Count;
                    tb08.QT_PAR_MOD_MAT = qtPar;

                    ///Este if verifica se existe mais de um registro na grid de neciação
                    if (grdNegociacao.Rows.Count > 1)
                    {
                        ///Caso tenha mais de 1 registro, ele pega o valor do próximo registro
                        tb08.NU_DIA_VEN_MOD_MAT = DateTime.Parse(grdNegociacao.Rows[2].Cells[2].Text).Day;
                    }
                    else
                    {
                        ///Caso tenha somente 1 registro, ele pega o valor deste único registro
                        tb08.NU_DIA_VEN_MOD_MAT = DateTime.Parse(grdNegociacao.Rows[0].Cells[2].Text).Day;
                    }
                    tb08.DT_PRI_PAR_MOD_MAT = DateTime.Parse(grdNegociacao.Rows[0].Cells[2].Text);
                    tb08.QT_PAR_DES_MAT = txtQtdeMesesDesctoMensa.Text != "" ? (int?)int.Parse(txtQtdeMesesDesctoMensa.Text) : null;
                }
                tb08.FL_INCLU_MAT = true;
                tb08.FL_ALTER_MAT = false;
                TB08_MATRCUR.SaveOrUpdate(tb08);
                this.Session[SessoesHttp.CodigoMatriculaAluno] = tb08.CO_ALU_CAD;

                #region Atualiza o histórico do aluno

                List<TB43_GRD_CURSO> tb43 = this.GradeSerie(proxSerie);
                foreach (TB43_GRD_CURSO lstTb43 in tb43)
                {
                    var ocoTb079 = (from iTb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                                    where iTb079.CO_EMP == refAluno.TB25_EMPRESA1.CO_EMP && iTb079.CO_ALU == coAlu
                                    && iTb079.CO_MODU_CUR == modalidade && iTb079.CO_CUR == proxSerie && iTb079.CO_ANO_REF == proxAno
                                    && iTb079.CO_MAT == lstTb43.CO_MAT
                                    select iTb079).FirstOrDefault();

                    if (ocoTb079 != null)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Não é possível efetuar Matrícula. Histórico de Aluno já cadastrado.");
                        return;
                    }

                    TB079_HIST_ALUNO tb79 = new TB079_HIST_ALUNO();
                    tb79.CO_ALU = coAlu;
                    tb79.CO_ANO_REF = proxAno;
                    tb79.CO_CUR = proxSerie;
                    tb79.CO_EMP = refAluno.TB25_EMPRESA1.CO_EMP;
                    tb79.TB25_EMPRESA = refAluno.TB25_EMPRESA1;
                    tb79.CO_MAT = lstTb43.CO_MAT;
                    tb79.CO_MODU_CUR = modalidade;
                    tb79.CO_TUR = proxTurma;
                    tb79.CO_USUARIO = new int?(LoginAuxili.CO_COL);
                    //tb79.DT_LANC = DateTime.Now;
                    tb79.DT_LANC = DateTime.Parse("05/01/" + proxAno);
                    tb79.TB44_MODULO = TB44_MODULO.RetornaPelaChavePrimaria(modalidade);
                    tb79.FL_TIPO_LANC_MEDIA = "N";
                    tb79.CO_FLAG_STATUS = "A";
                    TB079_HIST_ALUNO.SaveOrUpdate(tb79, false);
                    TB48_GRADE_ALUNO tb48 = new TB48_GRADE_ALUNO();
                    tb48.TB07_ALUNO = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                    tb48.TB25_EMPRESA = refAluno.TB25_EMPRESA1;
                    tb48.CO_ANO_MES_MAT = proxAno;
                    tb48.CO_CUR = proxSerie;
                    tb48.CO_MAT = lstTb43.CO_MAT;
                    tb48.CO_MODU_CUR = modalidade;
                    tb48.CO_STAT_MATE = "E";
                    tb48.CO_TUR = proxTurma;
                    tb48.NU_SEM_LET = "1";
                    tb48.CO_FLAG_STATUS = "A";
                    tb48.TB07_ALUNO = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                    tb48.TB44_MODULO = TB44_MODULO.RetornaPelaChavePrimaria(modalidade);
                    TB48_GRADE_ALUNO.SaveOrUpdate(tb48, false);
                }

                #endregion

                #region Atualiza a master matrícula do aluno

                TB80_MASTERMATR tb80 = TB80_MASTERMATR.RetornaPelaChavePrimaria(modalidade, coAlu, proxAno, null);
                if (tb80 == null)
                {
                    tb80 = new TB80_MASTERMATR();
                    tb80.TB25_EMPRESA = refAluno.TB25_EMPRESA1;
                    tb80.CO_ALU_CAD = strProxMatricula;
                    tb80.CO_ANO_MES_MAT = proxAno;
                    tb80.CO_COL = new int?(LoginAuxili.CO_COL);
                    tb80.CO_CUR = proxSerie;
                    tb80.CO_SITU_MTR = "A";
                    //tb80.DT_CADA_MTR = DateTime.Now;
                    //tb80.DT_SITU_MTR = DateTime.Now;
                    tb80.DT_CADA_MTR = DateTime.Parse("05/01/" + proxAno);
                    tb80.DT_SITU_MTR = DateTime.Parse("05/01/" + proxAno);
                    //tb80.DT_ALT_REGISTRO = new DateTime?(DateTime.Now);
                    tb80.DT_ALT_REGISTRO = new DateTime?(DateTime.Parse("05/01/" + proxAno));
                    tb80.TB07_ALUNO = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
                    tb80.TB44_MODULO = TB44_MODULO.RetornaPelaChavePrimaria(modalidade);
                }
                else
                {
                    tb80.CO_SITU_MTR = "A";
                    //tb80.DT_ALT_REGISTRO = new DateTime?(DateTime.Now);
                    tb80.DT_ALT_REGISTRO = new DateTime?(DateTime.Parse("05/01/" + proxAno));
                }
                TB80_MASTERMATR.SaveOrUpdate(tb80, false);

                #endregion

                #region Atualiza a quantidade de vagas

                var qtdDispVaga = (from tb289 in TB289_DISP_VAGA_TURMA.RetornaTodosRegistros()
                                   where tb289.TB25_EMPRESA.CO_EMP == refAluno.TB25_EMPRESA1.CO_EMP && tb289.TB44_MODULO.CO_MODU_CUR == modalidade
                                   && tb289.CO_CUR == proxSerie && tb289.CO_ANO == proxAno && tb289.CO_TUR == proxTurma && tb289.CO_PERI_TUR == turno
                                   select tb289).FirstOrDefault();

                if (qtdDispVaga != null)
                {
                    if (!qtdDispVaga.QTDE_VAG_MAT.HasValue)
                        qtdDispVaga.QTDE_VAG_MAT = 1;
                    else
                        qtdDispVaga.QTDE_VAG_MAT += 1;

                    TB289_DISP_VAGA_TURMA.SaveOrUpdate(qtdDispVaga, false);
                }
                if (this.txtNumReserva.Text != string.Empty)
                {
                    TB052_RESERV_MATRI tb052 = TB052_RESERV_MATRI.RetornaPelaChavePrimaria(this.txtNumReserva.Text, LoginAuxili.CO_EMP);
                    tb052.CO_STATUS = "E";
                    TB052_RESERV_MATRI.SaveOrUpdate(tb052, false);
                }

                #endregion

                #region Atualiza o financeiro

                if (chkAtualiFinan.Checked)
                {
                    TB25_EMPRESA tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);

                    TB44_MODULO tb44 = TB44_MODULO.RetornaPelaChavePrimaria(modalidade);

                    if (tb44.CO_SEQU_PC != null && tb44.CO_SEQU_PC_BANCO != null && tb44.CO_SEQU_PC_CAIXA != null)
                    {
                        //----------------> Cria uma lista da TB47_CTA_RECEB
                        List<TB47_CTA_RECEB> lstTb47 = new List<TB47_CTA_RECEB>();

                        TB47_CTA_RECEB tb47;

                        //----------------> Lança a(s) parcela(s) no contas a receber
                        for (int i = 0; i < grdNegociacao.Rows.Count; i++)
                        {
                            tb47 = new TB47_CTA_RECEB();
                            tb47.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(refAluno.TB25_EMPRESA1.CO_EMP);
                            tb47.CO_EMP_UNID_CONT = coEmp;
                            tb47.NU_DOC = grdNegociacao.Rows[i].Cells[0].Text;
                            tb47.NU_PAR = int.Parse(grdNegociacao.Rows[i].Cells[1].Text);
                            tb47.QT_PAR = grdNegociacao.Rows.Count;
                            tb47.DT_CAD_DOC = DateTime.Now;
                            //tb47.DT_CAD_DOC = DateTime.Parse("05/01/" + proxAno);
                            tb47.DE_COM_HIST = "VALOR MENSALIDADE ESCOLAR.";
                            tb47.VR_TOT_DOC = decimal.Parse(txtTotalMensa.Text);
                            tb47.VR_PAR_DOC = decimal.Parse(grdNegociacao.Rows[i].Cells[3].Text);
                            tb47.DT_VEN_DOC = DateTime.Parse(grdNegociacao.Rows[i].Cells[2].Text);
                            tb47.VL_DES_BOLSA_ALUNO = decimal.Parse(grdNegociacao.Rows[i].Cells[4].Text);
                            tb47.VR_DES_DOC = decimal.Parse(grdNegociacao.Rows[i].Cells[5].Text);
                            tb47.VR_MUL_DOC = decimal.Parse(grdNegociacao.Rows[i].Cells[7].Text);
                            tb47.VR_JUR_DOC = decimal.Parse(string.Format("{0:0.0000}", decimal.Parse(grdNegociacao.Rows[i].Cells[8].Text)));
                            tb47.DT_EMISS_DOCTO = DateTime.Now;


                            // Alterar para o campo do CO_AGRUP_REC
                            tb47.CO_AGRUP_RECDESP = TB83_PARAMETRO.RetornaPelaChavePrimaria(refAluno.TB25_EMPRESA1.CO_EMP).CO_AGRUP_REC;
                            //tb47.DT_EMISS_DOCTO = DateTime.Parse("05/01/" + proxAno);

                            // Flag emissão boleto "S"im ou "N"ão
                            if (ddlBoleto.SelectedValue != "")
                            {
                                tb47.FL_EMITE_BOLETO = "S";
                                tb47.FL_TIPO_PREV_RECEB = "B";
                                //------------------------> Salvando o tipo de documento "Boleto Bancário"
                                tb47.TB086_TIPO_DOC = TB086_TIPO_DOC.RetornaPeloCoTipoDoc(3);

                                //------------------------> Dados do boleto bancário
                                tb47.TB227_DADOS_BOLETO_BANCARIO = TB227_DADOS_BOLETO_BANCARIO.RetornaPelaChavePrimaria(int.Parse(ddlBoleto.SelectedValue));
                            }
                            else
                            {
                                tb47.FL_EMITE_BOLETO = "N";
                                //------------------------> Salvando o tipo de documento "Recibo"
                                tb47.TB086_TIPO_DOC = TB086_TIPO_DOC.RetornaPeloCoTipoDoc(1);
                            }

                            tb47.TB39_HISTORICO = hTb01.ID_HISTO_MENSA == null ? (from tb39 in TB39_HISTORICO.RetornaTodosRegistros()
                                                                                  where tb39.DE_HISTORICO.Contains("Mensalidade")
                                                                                  select tb39).FirstOrDefault() : TB39_HISTORICO.RetornaPelaChavePrimaria((int)hTb01.ID_HISTO_MENSA);

                            if (tb44.CO_CENT_CUSTO != null)
                                tb47.TB099_CENTRO_CUSTO = TB099_CENTRO_CUSTO.RetornaPelaChavePrimaria(tb44.CO_CENT_CUSTO.Value);

                            tb47.TB56_PLANOCTA = TB56_PLANOCTA.RetornaPelaChavePrimaria(tb44.CO_SEQU_PC.Value);
                            tb47.CO_SEQU_PC_BANCO = tb44.CO_SEQU_PC_BANCO.Value;
                            tb47.CO_SEQU_PC_CAIXA = tb44.CO_SEQU_PC_CAIXA.Value;

                            tb47.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);

                            tb47.CO_FLAG_TP_VALOR_MUL = "P";
                            tb47.CO_FLAG_TP_VALOR_JUR = tb25.CO_FLAG_TP_VALOR_JUROS != null ? tb25.CO_FLAG_TP_VALOR_JUROS : "P";
                            tb47.CO_FLAG_TP_VALOR_DES = "V";
                            tb47.CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO = "V";
                            tb47.CO_FLAG_TP_VALOR_OUT = "V";

                            tb47.IC_SIT_DOC = "A";
                            tb47.TP_CLIENTE_DOC = "A";

                            ///Formato =>Ano: XXXX - Série: XXXXX - Turma: XXXXX - Turno: XXXXX
                            tb47.DE_OBS_BOL_MAT = "Ano: " + DateTime.Parse(grdNegociacao.Rows[i].Cells[2].Text).ToString("yyyy") + " - Série/Curso: " + ddlSerieCurso.SelectedItem + " - Turma: " + ddlTurma.SelectedItem + " - Turno: " + txtTurno.Text;

                            tb47.DE_OBS = "MENSALIDADE ESCOLAR";

                            tb47.TP_CLIENTE_DOC = "A";

                            tb47.CO_ALU = coAlu;
                            //tb47.CO_ANO_MES_MAT = DateTime.Now.Year.ToString();
                            tb47.CO_ANO_MES_MAT = txtAno.Text;
                            tb47.NU_SEM_LET = "1";
                            tb47.CO_CUR = proxSerie;
                            tb47.CO_TUR = proxTurma;
                            tb47.CO_MODU_CUR = modalidade;
                            tb47.TB108_RESPONSAVEL = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(int.Parse(hdCoRes.Value));

                            tb47.DT_SITU_DOC = DateTime.Now;
                            tb47.DT_ALT_REGISTRO = DateTime.Now;
                            //tb47.DT_ALT_REGISTRO = DateTime.Parse("05/01/" + proxAno);

                            ///Atualiza o código da bolsa
                            refAluno.TB148_TIPO_BOLSAReference.Load();
                            if (refAluno.TB148_TIPO_BOLSA != null)
                                tb47.TB148_TIPO_BOLSA = TB148_TIPO_BOLSA.RetornaPelaChavePrimaria(refAluno.TB148_TIPO_BOLSA.CO_TIPO_BOLSA);

                            lstTb47.Add(tb47);
                        }
                    }
                }

                #endregion

                #region Atualiza o dados da série no aluno

                TB01_CURSO tb01 = TB01_CURSO.RetornaPeloCoCur(proxSerie);

                if (tb01 != null)
                {
                    if (tb01.CO_NIVEL_CUR == "F" || tb01.CO_NIVEL_CUR == "M" || tb01.CO_NIVEL_CUR == "I")
                    {
                        var varTb07 = TB07_ALUNO.RetornaPeloCoAlu(coAlu);

                        varTb07.CO_MODU_CUR = modalidade;
                        varTb07.CO_CUR = proxSerie;
                        varTb07.CO_TUR = proxTurma;

                        TB07_ALUNO.SaveOrUpdate(varTb07, true);
                    }
                }

                #endregion

                #region Finalização

                GestorEntities.CurrentContext.SaveChanges();

                if (chkAtualiFinan.Checked)
                {
                    if (ddlBoleto.SelectedValue != "")
                    {
                        lnkBolCarne.Enabled = true;
                    }
                    lnkRecMatric.Enabled = lnkFichaMatric.Enabled = lnkCarteira.Enabled = true;

                    var admUsu = (from adUs in ADMUSUARIO.RetornaTodosRegistros()
                                  where adUs.CodUsuario == LoginAuxili.CO_COL
                                  select new { adUs.FLA_ALT_REG_PAG_MAT }).FirstOrDefault();

                    if (admUsu != null)
                    {
                        if (admUsu.FLA_ALT_REG_PAG_MAT == "S")
                        {
                            ADMMODULO admModulo = (from admMod in ADMMODULO.RetornaTodosRegistros()
                                                   where admMod.nomURLModulo.Contains("5103_RecebPagamCompromisso")
                                                   select admMod).FirstOrDefault();

                            if (admModulo != null)
                            {
                                lnkRealiPagto.HRef = "/" + String.Format("{0}?moduloNome=+Registro+de+Recebimento+ou+Pagamento+de+Compromissos+Financeiros.&+NU_NIRE=" + ddlNire.SelectedValue, admModulo.nomURLModulo);
                            }
                        }
                    }

                    DesabilitaCamposMatricula();
                    AuxiliPagina.EnvioMensagemSucesso(this, "Matrícula realizada e atualização de dados e mensalidades efetuadas com sucesso");
                }
                else
                {
                    lnkRecMatric.Enabled = lnkFichaMatric.Enabled = lnkCarteira.Enabled = true;
                    DesabilitaCamposMatricula();
                    AuxiliPagina.EnvioMensagemSucesso(this, "Matrícula realizada e atualização de dados efetuadas com sucesso");
                }

                #endregion

            }
        }

        #endregion

        #region Carregamento

        /// <summary>
        /// Método que monta grid de negociação
        /// </summary>
        protected void MontaGridNegociacao()
        {
            int coEmp = TB129_CADTURMAS.RetornaPelaChavePrimaria(int.Parse(ddlTurma.SelectedValue)).CO_EMP_UNID_CONT;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            //--------> Retorna o turno da turma selecionada (V - Vespertino, M - Matutino, N - Noturno)
            string turnoTurma = TB06_TURMAS.RetornaPeloCodigo(int.Parse(ddlTurma.SelectedValue)).CO_PERI_TUR;

            var varSer = TB01_CURSO.RetornaPelaChavePrimaria(ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP, modalidade, serie);
            var tb83 = TB83_PARAMETRO.RetornaPelaChavePrimaria(coEmp);

            //--------> Mensalidade do Curso
            decimal menCur = 0;

            //--------> Verifica se o usuário selecionou um tipo de contrato
            if (ddlTipoContrato.SelectedValue == "")
            {
                ddlTipoContrato.SelectedValue = "P";
                ddlTipoContrato.DataBind();
            }

            //--------> Carrega o valor total do curso
            //--------> Valida se o sistema deve utilizar o valor integral
            if (!chkIntegMensaSerie.Checked)
            {
                //--------> O valor utilizado não é o integral
                //--------> Valida o turno da turma para determinar o valor
                switch (turnoTurma)
                {
                    //--------> Matutino
                    #region Turno Matutino
                    case "M":
                        //--------> Valida o tipo do contrato (P - A Prazo, V - A Vista)
                        switch (ddlTipoContrato.SelectedValue)
                        {
                            //--------> A Prazo
                            #region Valor A Prazo
                            case "P":
                                //--------> Valida se o curso possui valor a prazo para o turno matutino
                                if (varSer.VL_CONTMAN_APRAZ == null)
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "A Modalidade de Ensino \"" + varSer.NO_CUR + "\" não possui valor a prazo para o turno Matutino.");
                                    return;
                                }
                                //--------> Valida se o usuário escolheu alterar o cálculo do valor do contrato
                                if (chkAlterValorContr.Checked)
                                {
                                    //--------> Valida o tipo de calculo do valor (P - Proporcional a quantidade de meses, T - Total de meses do curso)
                                    switch (ddlValorContratoCalc.SelectedValue)
                                    {
                                        //--------> Proporcional a quantidade de meses
                                        case "P":
                                            if (txtValorContratoCalc.Text != "")
                                            {
                                                //menCur = (Decimal.Parse(txtValorContratoCalc.Text)/varSer.NU_QUANT_MESES) * Decimal.Parse(txtQtdeParcelas.Text);
                                                menCur = Decimal.Parse(txtValorContratoCalc.Text);
                                            }
                                            else
                                                menCur = (Decimal.Parse(varSer.VL_CONTMAN_APRAZ.ToString()) / varSer.NU_QUANT_MESES) * Decimal.Parse(txtQtdeParcelas.Text);

                                            break;

                                        //--------> Total de meses do curso
                                        case "T":
                                            if (txtValorContratoCalc.Text != "")
                                            {
                                                menCur = Decimal.Parse(txtValorContratoCalc.Text);
                                            }
                                            else
                                                menCur = Decimal.Parse(varSer.VL_CONTMAN_APRAZ.ToString());
                                            break;
                                    }
                                }
                                else
                                {
                                    menCur = Decimal.Parse(varSer.VL_CONTMAN_APRAZ.ToString());

                                }
                                break;
                            #endregion

                            //--------> A Vista
                            #region Valor A Vista
                            case "V":
                                //--------> Valida se o curso possui valor a vista para o turno matutino
                                if (varSer.VL_CONTMAN_APRAZ == null)
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "A Modalidade de Ensino \"" + varSer.NO_CUR + "\" não possui valor a vista para o turno Matutino.");
                                    return;
                                }
                                //--------> Valida se o usuário escolheu alterar o cálculo do valor do contrato
                                if (chkAlterValorContr.Checked)
                                {
                                    //--------> Valida o tipo de calculo do valor (P - Proporcional a quantidade de meses, T - Total de meses do curso)
                                    switch (ddlValorContratoCalc.SelectedValue)
                                    {
                                        //--------> Proporcional a quantidade de meses
                                        case "P":
                                            if (txtValorContratoCalc.Text != "")
                                            {
                                                //menCur = (Decimal.Parse(txtValorContratoCalc.Text)/varSer.NU_QUANT_MESES) * Decimal.Parse(txtQtdeParcelas.Text);
                                                menCur = Decimal.Parse(txtValorContratoCalc.Text);
                                            }
                                            else
                                                menCur = (Decimal.Parse(varSer.VL_CONTMAN_AVIST.ToString()) / varSer.NU_QUANT_MESES) * Decimal.Parse(txtQtdeParcelas.Text);

                                            break;

                                        //--------> Total de meses do curso
                                        case "T":
                                            if (txtValorContratoCalc.Text != "")
                                            {
                                                menCur = Decimal.Parse(txtValorContratoCalc.Text);
                                            }
                                            else
                                                menCur = Decimal.Parse(varSer.VL_CONTMAN_AVIST.ToString());

                                            break;
                                    }
                                }
                                else
                                {
                                    menCur = Decimal.Parse(varSer.VL_CONTMAN_AVIST.ToString());

                                }
                                break;
                            #endregion
                        }
                        break;
                    #endregion

                    //--------> Vespertino
                    #region Turno Vespertino
                    case "V":
                        //--------> Valida o tipo do contrato (P - A Prazo, V - A Vista)
                        switch (ddlTipoContrato.SelectedValue)
                        {
                            //--------> A Prazo
                            #region A Prazo
                            case "P":
                                //--------> Valida se o curso possui valor a prazo para o turno vespertino
                                if (varSer.VL_CONTTAR_APRAZ == null)
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "A Modalidade de Ensino \"" + varSer.NO_CUR + "\" não possui valor a prazo para o turno Vespertino.");
                                    return;
                                }
                                //--------> Valida se o usuário escolheu alterar o cálculo do valor do contrato
                                if (chkAlterValorContr.Checked)
                                {
                                    //--------> Valida o tipo de calculo do valor (P - Proporcional a quantidade de meses, T - Total de meses do curso)
                                    switch (ddlValorContratoCalc.SelectedValue)
                                    {
                                        //--------> Proporcional a quantidade de meses
                                        case "P":
                                            if (txtValorContratoCalc.Text != "")
                                            {
                                                //menCur = (Decimal.Parse(txtValorContratoCalc.Text)/varSer.NU_QUANT_MESES) * Decimal.Parse(txtQtdeParcelas.Text);
                                                menCur = Decimal.Parse(txtValorContratoCalc.Text);
                                            }
                                            else
                                                menCur = (Decimal.Parse(varSer.VL_CONTTAR_APRAZ.ToString()) / varSer.NU_QUANT_MESES) * Decimal.Parse(txtQtdeParcelas.Text);

                                            break;

                                        //--------> Total de meses do curso
                                        case "T":
                                            if (txtValorContratoCalc.Text != "")
                                            {
                                                menCur = Decimal.Parse(txtValorContratoCalc.Text);
                                            }
                                            else
                                                menCur = Decimal.Parse(varSer.VL_CONTTAR_APRAZ.ToString());
                                            break;
                                    }
                                }
                                else
                                {
                                    menCur = Decimal.Parse(varSer.VL_CONTTAR_APRAZ.ToString());

                                }
                                break;
                            #endregion

                            //--------> A Vista
                            #region A Vista
                            case "V":
                                //--------> Valida se o curso possui valor a vista para o turno vespertino
                                if (varSer.VL_CONTTAR_AVIST == null)
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "A Modalidade de Ensino \"" + varSer.NO_CUR + "\" não possui valor a vista para o turno Vespertino.");
                                    return;
                                }
                                //--------> Valida se o usuário escolheu alterar o cálculo do valor do contrato
                                if (chkAlterValorContr.Checked)
                                {
                                    //--------> Valida o tipo de calculo do valor (P - Proporcional a quantidade de meses, T - Total de meses do curso)
                                    switch (ddlValorContratoCalc.SelectedValue)
                                    {
                                        //--------> Proporcional a quantidade de meses
                                        case "P":
                                            if (txtValorContratoCalc.Text != "")
                                            {
                                                //menCur = (Decimal.Parse(txtValorContratoCalc.Text)/varSer.NU_QUANT_MESES) * Decimal.Parse(txtQtdeParcelas.Text);
                                                menCur = Decimal.Parse(txtValorContratoCalc.Text);
                                            }
                                            else
                                                menCur = (Decimal.Parse(varSer.VL_CONTTAR_AVIST.ToString()) / varSer.NU_QUANT_MESES) * Decimal.Parse(txtQtdeParcelas.Text);

                                            break;

                                        //--------> Total de meses do curso
                                        case "T":
                                            if (txtValorContratoCalc.Text != "")
                                            {
                                                menCur = Decimal.Parse(txtValorContratoCalc.Text);
                                            }
                                            else
                                                menCur = Decimal.Parse(varSer.VL_CONTTAR_AVIST.ToString());

                                            break;
                                    }
                                }
                                else
                                {
                                    menCur = Decimal.Parse(varSer.VL_CONTTAR_AVIST.ToString());

                                }
                                break;
                            #endregion
                        }
                        break;
                    #endregion

                    //--------> Noturno
                    #region Turno Noturno
                    case "N":
                        //--------> Valida o tipo do contrato (P - A Prazo, V - A Vista)
                        switch (ddlTipoContrato.SelectedValue)
                        {
                            //--------> A Prazo
                            #region A Prazo
                            case "P":
                                //--------> Valida se o curso possui valor a prazo para o turno noturno
                                if (varSer.VL_CONTNOI_APRAZ == null)
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "A Modalidade de Ensino \"" + varSer.NO_CUR + "\" não possui valor a prazo para o turno Noturno.");
                                    return;
                                }
                                //--------> Valida se o usuário escolheu alterar o cálculo do valor do contrato
                                if (chkAlterValorContr.Checked)
                                {
                                    //--------> Valida o tipo de calculo do valor (P - Proporcional a quantidade de meses, T - Total de meses do curso)
                                    switch (ddlValorContratoCalc.SelectedValue)
                                    {
                                        //--------> Proporcional a quantidade de meses
                                        case "P":
                                            if (txtValorContratoCalc.Text != "")
                                            {
                                                //menCur = (Decimal.Parse(txtValorContratoCalc.Text)/varSer.NU_QUANT_MESES) * Decimal.Parse(txtQtdeParcelas.Text);
                                                menCur = Decimal.Parse(txtValorContratoCalc.Text);
                                            }
                                            else
                                                menCur = (Decimal.Parse(varSer.VL_CONTNOI_APRAZ.ToString()) / varSer.NU_QUANT_MESES) * Decimal.Parse(txtQtdeParcelas.Text);
                                            break;

                                        //--------> Total de meses do curso
                                        case "T":
                                            if (txtValorContratoCalc.Text != "")
                                            {
                                                menCur = Decimal.Parse(txtValorContratoCalc.Text);
                                            }
                                            else
                                                menCur = Decimal.Parse(varSer.VL_CONTNOI_APRAZ.ToString());

                                            break;
                                    }
                                }
                                else
                                {
                                    menCur = Decimal.Parse(varSer.VL_CONTNOI_APRAZ.ToString());

                                }
                                break;
                            #endregion

                            //--------> A Vista
                            #region A Vista
                            case "V":
                                //--------> Valida se o curso possui valor a vista para o turno noturno
                                if (varSer.VL_CONTNOI_AVIST == null)
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "A Modalidade de Ensino \"" + varSer.NO_CUR + "\" não possui valor a vista para o turno Noturno.");
                                    return;
                                }
                                //--------> Valida se o usuário escolheu alterar o cálculo do valor do contrato
                                if (chkAlterValorContr.Checked)
                                {
                                    //--------> Valida o tipo de calculo do valor (P - Proporcional a quantidade de meses, T - Total de meses do curso)
                                    switch (ddlValorContratoCalc.SelectedValue)
                                    {
                                        //--------> Proporcional a quantidade de meses
                                        case "P":
                                            if (txtValorContratoCalc.Text != "")
                                            {
                                                //menCur = (Decimal.Parse(txtValorContratoCalc.Text)/varSer.NU_QUANT_MESES) * Decimal.Parse(txtQtdeParcelas.Text);
                                                menCur = Decimal.Parse(txtValorContratoCalc.Text);
                                            }
                                            else
                                                menCur = (Decimal.Parse(varSer.VL_CONTNOI_AVIST.ToString()) / varSer.NU_QUANT_MESES) * Decimal.Parse(txtQtdeParcelas.Text);

                                            break;

                                        //--------> Total de meses do curso
                                        case "T":
                                            if (txtValorContratoCalc.Text != "")
                                            {
                                                menCur = Decimal.Parse(txtValorContratoCalc.Text);
                                            }
                                            else
                                                menCur = Decimal.Parse(varSer.VL_CONTNOI_AVIST.ToString());
                                            break;
                                    }
                                }
                                else
                                {
                                    menCur = Decimal.Parse(varSer.VL_CONTNOI_AVIST.ToString());

                                }
                                break;
                            #endregion
                        }
                        break;
                    #endregion
                }
            }
            else
            {
                //--------> O valor utilizado é o integral
                //--------> Valida se o curso possui valor integral
                if (varSer.VL_CONTINT_APRAZ == null || varSer.VL_CONTINT_AVIST == null)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "A Modalidade de Ensino \"" + varSer.NO_CUR + "\" não possui valor integral.");
                    return;
                }
                //--------> Valida o tipo do contrato (P - A Prazo, V - A Vista)
                switch (ddlTipoContrato.SelectedValue)
                {
                    //--------> A Prazo
                    #region A Prazo
                    case "P":
                        //--------> Valida se o curso possui valor integral a prazo
                        if (String.IsNullOrEmpty(varSer.VL_CONTINT_APRAZ.ToString()))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "A Modalidade de Ensino \"" + varSer.NO_CUR + "\" não possui valor integral a prazo.");
                            return;
                        }
                        if (chkAlterValorContr.Checked)
                        {
                            //--------> Valida o tipo de calculo do valor (P - Proporcional a quantidade de meses, T - Total de meses do curso)
                            switch (ddlValorContratoCalc.SelectedValue)
                            {
                                //--------> Proporcional a quantidade de meses
                                case "P":
                                    if (txtValorContratoCalc.Text != "")
                                    {
                                        //menCur = (Decimal.Parse(txtValorContratoCalc.Text)/varSer.NU_QUANT_MESES) * Decimal.Parse(txtQtdeParcelas.Text);
                                        menCur = Decimal.Parse(txtValorContratoCalc.Text);
                                    }
                                    else
                                        menCur = (Decimal.Parse(varSer.VL_CONTINT_APRAZ.ToString()) / varSer.NU_QUANT_MESES) * Decimal.Parse(txtQtdeParcelas.Text);

                                    break;

                                //--------> Total de meses do curso
                                case "T":
                                    if (txtValorContratoCalc.Text != "")
                                    {
                                        menCur = Decimal.Parse(txtValorContratoCalc.Text);
                                    }
                                    else
                                        menCur = Decimal.Parse(varSer.VL_CONTINT_APRAZ.ToString());

                                    break;
                            }
                        }
                        else
                        {
                            menCur = Decimal.Parse(varSer.VL_CONTINT_APRAZ.ToString());
                        }
                        break;
                    #endregion

                    //--------> A Vista
                    #region A Vista
                    case "V":
                        //--------> Valida se o curso possui valor integral a vista
                        if (varSer.VL_CONTINT_AVIST != null)
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "A Modalidade de Ensino \"" + varSer.NO_CUR + "\" não possui valor integral a vista.");
                            return;
                        }
                        if (chkAlterValorContr.Checked)
                        {
                            //--------> Valida o tipo de calculo do valor (P - Proporcional a quantidade de meses, T - Total de meses do curso)
                            switch (ddlValorContratoCalc.SelectedValue)
                            {
                                //--------> Proporcional a quantidade de meses
                                case "P":
                                    if (txtValorContratoCalc.Text != "")
                                    {
                                        //menCur = (Decimal.Parse(txtValorContratoCalc.Text)/varSer.NU_QUANT_MESES) * Decimal.Parse(txtQtdeParcelas.Text);
                                        menCur = Decimal.Parse(txtValorContratoCalc.Text);
                                    }
                                    else
                                        menCur = (Decimal.Parse(varSer.VL_CONTINT_AVIST.ToString()) / varSer.NU_QUANT_MESES) * Decimal.Parse(txtQtdeParcelas.Text);
                                    break;

                                //--------> Total de meses do curso
                                case "T":
                                    if (txtValorContratoCalc.Text != "")
                                    {
                                        menCur = Decimal.Parse(txtValorContratoCalc.Text);
                                    }
                                    else
                                        menCur = Decimal.Parse(varSer.VL_CONTINT_AVIST.ToString());
                                    break;
                            }
                        }
                        else
                        {
                            menCur = Decimal.Parse(varSer.VL_CONTINT_AVIST.ToString());
                        }
                        break;
                    #endregion
                }
            }

            if (menCur == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Série/Curso informado não apresenta mensalidade.");
                return;
            }
            else
            {
                if (chkDataPrimeiraParcela.Checked)
                {
                    if (Decimal.Parse(txtValorPrimParce.Text) > menCur)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Valor da 1ª Parcela esta inconsistente.");
                        return;
                    }
                }

                if (txtDesctoMensa.Text != "")
                {
                    if (Decimal.Parse(txtDesctoMensa.Text) > menCur)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Valor de Desconto Especial inconsistente.");
                        return;
                    }
                }
            }
            int qtdParcCur = int.Parse(txtQtdeParcelas.Text);
            if (qtdParcCur == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Quantidade de parcelas informada para Série/Curso deve ser maior que zero.");
                return;
            }
            int qtdDiasInterMeses = varSer.QT_INTERV_DIAS != null ? varSer.QT_INTERV_DIAS.Value : 0;
            decimal desCur = 0;
            decimal totalMensa = 0;
            decimal totalDescto = 0;
            decimal totalDesctoEspec = 0;
            decimal totalValorLiqui = 0;
            decimal multaUnid = tb83.VL_PERCE_MULTA != null ? (decimal)tb83.VL_PERCE_MULTA : 0;
            decimal jurosUnid = tb83.VL_PERCE_JUROS != null ? (decimal)tb83.VL_PERCE_JUROS : 0;
            int diaVenctoUnid = int.Parse(ddlDiaVecto.SelectedValue);
            int qtdeMesesDescto = txtQtdeMesesDesctoMensa.Text != "" ? int.Parse(txtQtdeMesesDesctoMensa.Text) : 0;
            int mesDescto = txtMesIniDesconto.Text != "" ? int.Parse(txtMesIniDesconto.Text) : 0;
            int numNire = txtNumNIRE.Text.Replace(".", "").Replace("-", "") != "" ? int.Parse(txtNumNIRE.Text.Replace(".", "").Replace("-", "")) : 0;

            if ((menCur > 0) && (numNire > 0))
            {
                DateTime dataSelec;
                if (chkDataPrimeiraParcela.Checked && txtDtPrimeiraParcela.Text != "")
                {
                    dataSelec = DateTime.Parse(txtDtPrimeiraParcela.Text);
                }
                else
                {
                    dataSelec = DateTime.Now.Year == int.Parse(txtAno.Text) ? (int.Parse(ddlDiaVecto.SelectedValue) > 27 && DateTime.Now.Month == 2 ? DateTime.Parse("27/" + DateTime.Now.Month.ToString("D2") + '/' + DateTime.Now.Year.ToString()) : DateTime.Parse(ddlDiaVecto.SelectedValue + '/' + DateTime.Now.Month.ToString("D2") + '/' + DateTime.Now.Year.ToString())) : DateTime.Parse(ddlDiaVecto.SelectedValue + "/01/" + txtAno.Text);
                    if (DateTime.Now.Year == int.Parse(txtAno.Text) && dataSelec < DateTime.Now)
                    {
                        if (varSer.QT_DIAS_PARC1 != null)
                        {
                            if (varSer.QT_DIAS_PARC1 > 0)
                            {
                                dataSelec = DateTime.Now.AddDays((double)varSer.QT_DIAS_PARC1);
                            }
                        }
                        else
                            dataSelec = DateTime.Now;
                    }
                    else
                    {
                        if (varSer.QT_DIAS_PARC1 != null)
                        {
                            if (varSer.QT_DIAS_PARC1 > 0)
                            {
                                dataSelec = dataSelec.AddDays((double)varSer.QT_DIAS_PARC1);
                            }
                        }
                    }
                }

                if (int.Parse(txtQtdeParcelas.Text) > varSer.NU_QUANT_MESES)
                {
                    return;
                }

                int parcelas = 0;
                if (ddlTipoContrato.SelectedValue == "P")
                {
                    parcelas = qtdParcCur > 0 && qtdParcCur == 12 && (!chkGeraTotalParce.Checked) ? qtdParcCur - dataSelec.Month + 1 : qtdParcCur;
                }

                int anoDesctoIni = txtPeriodoIniDesconto.Text != "" ? DateTime.Parse(txtPeriodoIniDesconto.Text).Year : dataSelec.Year;
                int anoDesctoFim = txtPeriodoFimDesconto.Text != "" ? DateTime.Parse(txtPeriodoFimDesconto.Text).Year : 0;
                int mesIniDescto = txtPeriodoIniDesconto.Text != "" ? DateTime.Parse(txtPeriodoIniDesconto.Text).Month : 0;
                int mesFimDescto = txtPeriodoFimDesconto.Text != "" ? DateTime.Parse(txtPeriodoFimDesconto.Text).Month : 0;
                int mesSelecionado = dataSelec.Month;
                decimal desEspec = txtDesctoMensa.Text != "" ? (decimal)decimal.Parse(txtDesctoMensa.Text) : 0;
                decimal valorLiqui = 0;
                decimal valorPrimParce = txtValorPrimParce.Text != "" ? Decimal.Parse(txtValorPrimParce.Text) : 0;
                int countDesconto = 1; // Contador para o desconto especial                

                if (ddlTipoContrato.SelectedValue == "V")
                {
                    parcelas = 1;
                }

                //Verifica se a quantidade de parcelas do desconto especial é maior que a qtde de parcelas geradas
                if (txtDesctoMensa.Text != "")
                {
                    DateTime dataTeste = dataSelec;
                    int contParcTeste = 1;
                    int serieCu = int.Parse(ddlSerieCurso.SelectedValue); // ;)

                    var res = (from tb01 in TB01_CURSO.RetornaTodosRegistros()
                               where tb01.CO_CUR == serieCu
                               select new
                               {
                                   tb01.FL_VENCI_FIXO,
                                   tb01.QT_INTERV_DIAS,
                                   tb01.NU_MDV
                               }).FirstOrDefault();

                    string fixo = res.FL_VENCI_FIXO;
                    int? dataVariavel = res.QT_INTERV_DIAS;
                    for (int i = 2; i <= parcelas; i++)
                    {
                        contParcTeste = i;
                        if (i == 2)
                        {
                            if (fixo != "S")
                                dataTeste = dataVariavel > 0 ? dataTeste.AddDays((double)(dataVariavel + 1)) : dataTeste.AddMonths(i - 1);
                            else
                                dataTeste = dataVariavel > 0 ? dataTeste.AddMonths(i - 1) : dataTeste.AddDays((double)(dataVariavel + 1));
                        }
                        else
                        {
                            if (fixo != "S")
                                dataTeste = dataVariavel > 0 ? dataTeste.AddDays((double)(dataVariavel)) : dataTeste.AddMonths(1);
                            else
                                dataTeste = dataVariavel > 0 ? dataTeste.AddMonths(1) : dataTeste.AddDays((double)(dataVariavel));

                        }

                        if (qtdDiasInterMeses == 0)
                        {
                            dataTeste = DateTime.Parse(((diaVenctoUnid != 0) ? (diaVenctoUnid > 27 && dataTeste.Month == 2 ? "27" : diaVenctoUnid.ToString("00")) : "05") + "/" + dataTeste.Month.ToString("00") + "/" + dataTeste.Year.ToString());
                        }

                        //-----> Caso o usuário não tenha escolhido a quantidade de parcelas e o ano da parcela for maior que o 
                        if ((!chkGeraTotalParce.Checked) && (dataTeste.Year > dataSelec.Year) && (ddlValorContratoCalc.SelectedValue != "T"))
                        {
                            i = parcelas;
                        }
                    }

                    if (ddlTipoDesctoMensa.SelectedValue == "M")
                    {
                        if (int.Parse(txtQtdeMesesDesctoMensa.Text) > (parcelas == contParcTeste ? parcelas : (contParcTeste - 1)))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Qtde meses de desconto especial inconsistente.");
                            return;
                        }

                        if (mesDescto > (parcelas == contParcTeste ? parcelas : (contParcTeste - 1)))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Mês de início de desconto (MID) inválido.");
                            return;
                        }

                        if ((mesDescto + int.Parse(txtQtdeMesesDesctoMensa.Text) - 1) > (parcelas == contParcTeste ? parcelas : (contParcTeste - 1)))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Qtde de mes de desconto especial combinado ao mês de início de desconto estão inconsistentes.");
                            return;
                        }
                    }
                    else
                    {
                        qtdeMesesDescto = (parcelas == contParcTeste ? parcelas : (contParcTeste - 1));
                        desEspec = desEspec / (parcelas == contParcTeste ? parcelas : (contParcTeste - 1));
                    }
                }

                decimal valorDemaisParc = 0;
                if (valorPrimParce != 0 && chkDataPrimeiraParcela.Checked && parcelas > 1)
                {
                    valorDemaisParc = Decimal.Parse(((menCur - valorPrimParce) / (parcelas - 1)).ToString("N2"));
                    menCur = valorPrimParce;
                }
                else
                {
                    //------------> Divide o valor total do curso pela quantidade de parcela do curso, encontrando o valor mensal
                    menCur = Decimal.Parse((menCur / parcelas).ToString("N2"));
                }

                //------------> Verifica se o valor do desconto especial é maior que o valor da parcela, se for o caso, o sistema apresenta uma mensagem de erro.
                if (desEspec > menCur)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de desconto especial inconsistente.");
                    return;
                }

                if (txtValorDescto.Text != "")
                {
                    desCur = chkManterDescontoPerc.Checked ? ((menCur * decimal.Parse(txtValorDescto.Text)) / 100) : decimal.Parse(txtValorDescto.Text);
                }
                else
                    desCur = 0;

                if (chkDataPrimeiraParcela.Checked == false)
                {
                    txtValorPrimParce.Text = menCur.ToString("N2");
                }

                grdNegociacao.DataKeyNames = new string[] { "CO_EMP", "NU_DOC", "NU_PAR", "DT_CAD_DOC" };

                DataTable Dt = new DataTable();

                Dt.Columns.Add("CO_EMP");

                Dt.Columns.Add("NU_DOC");

                Dt.Columns.Add("NU_PAR");

                Dt.Columns.Add("DT_CAD_DOC");

                Dt.Columns.Add("dtVencimento");

                Dt.Columns.Add("valorParcela");

                Dt.Columns.Add("valorBolsa");

                Dt.Columns.Add("valorDescto");

                Dt.Columns.Add("valorLiquido");

                Dt.Columns.Add("valorMulta");

                Dt.Columns.Add("valorJuros");

                if ((anoDesctoFim != 0) && (anoDesctoFim >= dataSelec.Year))
                {
                    if ((anoDesctoIni < dataSelec.Year) && (anoDesctoFim > dataSelec.Year))
                    {
                        valorLiqui = menCur - desCur;
                        if (valorLiqui < 0)
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Convênio/Bolsa (R$ BOLSA) inválido.");
                            grdNegociacao.DataSource = null;
                            grdNegociacao.DataBind();
                            return;
                        }
                        totalMensa = totalMensa + menCur;
                        totalDescto = totalDescto + desCur;
                        totalDesctoEspec = totalDesctoEspec + (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0);
                        if (valorLiqui < (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Desconto (R$ DESCTO) inválido.");
                            grdNegociacao.DataSource = null;
                            grdNegociacao.DataBind();
                            return;
                        }
                        valorLiqui = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? valorLiqui - desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? valorLiqui - desEspec : valorLiqui);
                        totalValorLiqui = totalValorLiqui + valorLiqui;
                        Dt.Rows.Add(coEmp, "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + "01", "01", dataSelec.ToString("dd/MM/yyyy"),
                            dataSelec.ToString("dd/MM/yyyy"), menCur.ToString("N2"), desCur.ToString("N2"),
                            ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec.ToString("N2") : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec.ToString("N2") : "0,00",
                            valorLiqui.ToString("N2"),
                            multaUnid.ToString("N2"), jurosUnid.ToString("N3"));
                    }
                    else if ((anoDesctoIni < dataSelec.Year) && (anoDesctoFim == dataSelec.Year))
                    {
                        if (dataSelec.Month <= mesFimDescto)
                        {
                            valorLiqui = menCur - desCur;
                            if (valorLiqui < 0)
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Convênio/Bolsa (R$ BOLSA) inválido.");
                                grdNegociacao.DataSource = null;
                                grdNegociacao.DataBind();
                                return;
                            }
                            totalMensa = totalMensa + menCur;
                            totalDescto = totalDescto + desCur;
                            totalDesctoEspec = totalDesctoEspec + (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0);
                            if (valorLiqui < (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0))
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Desconto (R$ DESCTO) inválido.");
                                grdNegociacao.DataSource = null;
                                grdNegociacao.DataBind();
                                return;
                            }
                            valorLiqui = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? valorLiqui - desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? valorLiqui - desEspec : valorLiqui);
                            totalValorLiqui = totalValorLiqui + valorLiqui;
                            Dt.Rows.Add(coEmp, "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + "01", "01", dataSelec.ToString("dd/MM/yyyy"),
                                dataSelec.ToString("dd/MM/yyyy"), menCur.ToString("N2"), desCur.ToString("N2"),
                                ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec.ToString("N2") : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec.ToString("N2") : "0,00",
                                valorLiqui.ToString("N2"),
                                multaUnid.ToString("N2"), jurosUnid.ToString("N3"));
                        }
                        else
                        {
                            desCur = 0;
                            valorLiqui = menCur - desCur;
                            if (valorLiqui < 0)
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Convênio/Bolsa (R$ BOLSA) inválido.");
                                grdNegociacao.DataSource = null;
                                grdNegociacao.DataBind();
                                return;
                            }
                            totalMensa = totalMensa + menCur;
                            totalDescto = totalDescto + desCur;
                            totalDesctoEspec = totalDesctoEspec + (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0);
                            if (valorLiqui < (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0))
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Desconto (R$ DESCTO) inválido.");
                                grdNegociacao.DataSource = null;
                                grdNegociacao.DataBind();
                                return;
                            }
                            valorLiqui = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? valorLiqui - desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? valorLiqui - desEspec : valorLiqui);
                            totalValorLiqui = totalValorLiqui + valorLiqui;
                            Dt.Rows.Add(coEmp, "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + "01", "01", dataSelec.ToString("dd/MM/yyyy"),
                                dataSelec.ToString("dd/MM/yyyy"), menCur.ToString("N2"), desCur.ToString("N2"),
                                ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec.ToString("N2") : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec.ToString("N2") : "0,00",
                                valorLiqui.ToString("N2"),
                                multaUnid.ToString("N2"), jurosUnid.ToString("N3"));
                        }
                    }
                    else if ((anoDesctoIni == dataSelec.Year) && (anoDesctoFim > dataSelec.Year))
                    {
                        if (dataSelec.Month >= mesIniDescto)
                        {
                            valorLiqui = menCur - desCur;
                            if (valorLiqui < 0)
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Convênio/Bolsa (R$ BOLSA) inválido.");
                                grdNegociacao.DataSource = null;
                                grdNegociacao.DataBind();
                                return;
                            }
                            totalMensa = totalMensa + menCur;
                            totalDescto = totalDescto + desCur;
                            totalDesctoEspec = totalDesctoEspec + (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0);
                            if (valorLiqui < (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0))
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Desconto (R$ DESCTO) inválido.");
                                grdNegociacao.DataSource = null;
                                grdNegociacao.DataBind();
                                return;
                            }
                            valorLiqui = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? valorLiqui - desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? valorLiqui - desEspec : valorLiqui);
                            totalValorLiqui = totalValorLiqui + valorLiqui;
                            Dt.Rows.Add(coEmp, "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + "01", "01", dataSelec.ToString("dd/MM/yyyy"),
                                dataSelec.ToString("dd/MM/yyyy"), menCur.ToString("N2"), desCur.ToString("N2"),
                                ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec.ToString("N2") : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec.ToString("N2") : "0,00",
                                valorLiqui.ToString("N2"),
                                multaUnid.ToString("N2"), jurosUnid.ToString("N3"));
                        }
                        else
                        {
                            desCur = 0;
                            valorLiqui = menCur - desCur;
                            if (valorLiqui < 0)
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Convênio/Bolsa (R$ BOLSA) inválido.");
                                grdNegociacao.DataSource = null;
                                grdNegociacao.DataBind();
                                return;
                            }
                            totalMensa = totalMensa + menCur;
                            totalDescto = totalDescto + desCur;
                            totalDesctoEspec = totalDesctoEspec + (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0);
                            if (valorLiqui < (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0))
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Desconto (R$ DESCTO) inválido.");
                                grdNegociacao.DataSource = null;
                                grdNegociacao.DataBind();
                                return;
                            }
                            valorLiqui = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? valorLiqui - desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? valorLiqui - desEspec : valorLiqui);
                            totalValorLiqui = totalValorLiqui + valorLiqui;
                            Dt.Rows.Add(coEmp, "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + "01", "01", dataSelec.ToString("dd/MM/yyyy"),
                                dataSelec.ToString("dd/MM/yyyy"), menCur.ToString("N2"), desCur.ToString("N2"),
                                ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec.ToString("N2") : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec.ToString("N2") : "0,00",
                                valorLiqui.ToString("N2"),
                                multaUnid.ToString("N2"), jurosUnid.ToString("N3"));
                        }
                    }
                    else if ((anoDesctoIni == dataSelec.Year) && (anoDesctoFim == dataSelec.Year))
                    {
                        if ((dataSelec.Month >= mesIniDescto) && (dataSelec.Month <= mesFimDescto))
                        {
                            valorLiqui = menCur - desCur;
                            if (valorLiqui < 0)
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Convênio/Bolsa (R$ BOLSA) inválido.");
                                grdNegociacao.DataSource = null;
                                grdNegociacao.DataBind();
                                return;
                            }
                            totalMensa = totalMensa + menCur;
                            totalDescto = totalDescto + desCur;
                            totalDesctoEspec = totalDesctoEspec + (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0);
                            if (valorLiqui < (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0))
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Desconto (R$ DESCTO) inválido.");
                                grdNegociacao.DataSource = null;
                                grdNegociacao.DataBind();
                                return;
                            }
                            valorLiqui = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? valorLiqui - desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? valorLiqui - desEspec : valorLiqui);
                            totalValorLiqui = totalValorLiqui + valorLiqui;
                            Dt.Rows.Add(coEmp, "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + "01", "01", dataSelec.ToString("dd/MM/yyyy"),
                                dataSelec.ToString("dd/MM/yyyy"), menCur.ToString("N2"), desCur.ToString("N2"),
                                ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec.ToString("N2") : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec.ToString("N2") : "0,00",
                                valorLiqui.ToString("N2"),
                                multaUnid.ToString("N2"), jurosUnid.ToString("N3"));
                        }
                        else
                        {
                            desCur = 0;
                            valorLiqui = menCur - desCur;
                            if (valorLiqui < 0)
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Convênio/Bolsa (R$ BOLSA) inválido.");
                                grdNegociacao.DataSource = null;
                                grdNegociacao.DataBind();
                                return;
                            }
                            totalMensa = totalMensa + menCur;
                            totalDescto = totalDescto + desCur;
                            totalDesctoEspec = totalDesctoEspec + (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0);
                            if (valorLiqui < (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0))
                            {
                                AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Desconto (R$ DESCTO) inválido.");
                                grdNegociacao.DataSource = null;
                                grdNegociacao.DataBind();
                                return;
                            }
                            valorLiqui = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? valorLiqui - desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? valorLiqui - desEspec : valorLiqui);
                            totalValorLiqui = totalValorLiqui + valorLiqui;
                            Dt.Rows.Add(coEmp, "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + "01", "01", dataSelec.ToString("dd/MM/yyyy"),
                                dataSelec.ToString("dd/MM/yyyy"), menCur.ToString("N2"), desCur.ToString("N2"),
                                ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec.ToString("N2") : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec.ToString("N2") : "0,00",
                                valorLiqui.ToString("N2"),
                                multaUnid.ToString("N2"), jurosUnid.ToString("N3"));
                        }
                    }
                }
                else
                {
                    desCur = 0;
                    valorLiqui = menCur - desCur;
                    if (valorLiqui < 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Convênio/Bolsa (R$ BOLSA) inválido.");
                        grdNegociacao.DataSource = null;
                        grdNegociacao.DataBind();
                        return;
                    }
                    totalMensa = totalMensa + menCur;
                    totalDescto = totalDescto + desCur;
                    totalDesctoEspec = totalDesctoEspec + (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0);
                    if (valorLiqui < (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0))
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Desconto (R$ DESCTO) inválido.");
                        grdNegociacao.DataSource = null;
                        grdNegociacao.DataBind();
                        return;
                    }
                    valorLiqui = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? valorLiqui - desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? valorLiqui - desEspec : valorLiqui);
                    totalValorLiqui = totalValorLiqui + valorLiqui;
                    Dt.Rows.Add(coEmp, "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + "01", "01", dataSelec.ToString("dd/MM/yyyy"),
                        dataSelec.ToString("dd/MM/yyyy"), menCur.ToString("N2"), desCur.ToString("N2"),
                        ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec.ToString("N2") : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec.ToString("N2") : "0,00",
                        valorLiqui.ToString("N2"),
                        multaUnid.ToString("N2"), jurosUnid.ToString("N3"));
                }

                if (valorPrimParce != 0 && chkDataPrimeiraParcela.Checked)
                {
                    menCur = valorDemaisParc;
                }
                DateTime dataVectoMensa = dataSelec;
                DateTime dtInicDescto, dtFimDescto = DateTime.Now;
                int mesDescontoFim = 0;
                qtdeMesesDescto = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && 1 >= mesDescto ? desEspec : 0) > 0 ? qtdeMesesDescto - 1 : qtdeMesesDescto;
                if (parcelas > 1)
                {
                    decimal t = 0;
                    if (valorPrimParce != 0 && chkDataPrimeiraParcela.Checked)
                    {
                        t = valorPrimParce;
                    }
                    else
                    {
                        t = 0;
                    }

                    for (int i = 2; i <= parcelas; i++)
                    {
                        t = t + menCur;

                        if (i == parcelas)
                        {
                            decimal tt = 0;
                            if (valorPrimParce == 0 || !chkDataPrimeiraParcela.Checked)
                            {
                                tt = menCur * parcelas;
                            }
                            else
                            {
                                tt = (menCur * (parcelas - 1)) + valorPrimParce;
                            }

                            if (tt > decimal.Parse(txtValorContratoCalc.Text))
                            {
                                decimal d = tt - decimal.Parse(txtValorContratoCalc.Text);
                                menCur = menCur - d;
                            }
                            else
                            {
                                if (tt < decimal.Parse(txtValorContratoCalc.Text))
                                {
                                    decimal d = decimal.Parse(txtValorContratoCalc.Text) - tt;
                                    menCur = menCur + d;
                                }
                            }
                        }

                        mesSelecionado++;
                        // Determina o Mês fim para o desconto
                        mesDescontoFim = mesDescto + (qtdeMesesDescto - 1);
                        if (mesDescontoFim > 12)
                        {
                            mesDescontoFim = mesDescontoFim - 12;
                        }

                        if (dataVectoMensa.Month >= mesDescto && dataVectoMensa.Year == DateTime.Now.Year)
                        {
                            countDesconto++;
                        }
                        else
                        {
                            if (dataVectoMensa.Year > DateTime.Now.Year && dataVectoMensa.Month >= mesDescontoFim)
                            {
                                countDesconto++;
                            }
                        }

                        int serieCu = int.Parse(ddlSerieCurso.SelectedValue);

                        var resu = (from tb01 in TB01_CURSO.RetornaTodosRegistros()
                                    where tb01.CO_CUR == serieCu
                                    select new
                                    {
                                        tb01.FL_VENCI_FIXO,
                                        tb01.QT_INTERV_DIAS,
                                        tb01.NU_MDV
                                    }).FirstOrDefault();

                        string fixo = resu.FL_VENCI_FIXO;
                        int? dataVariavel = resu.QT_INTERV_DIAS;

                        if (i == 2)
                        {
                            if (fixo != "S")
                                dataVectoMensa = dataVariavel > 0 ? dataVectoMensa.AddDays((double)(dataVariavel + 1)) : dataVectoMensa.AddMonths(i - 1);
                            else
                                dataVectoMensa = dataVariavel > 0 ? dataVectoMensa.AddMonths(i - 1) : dataVectoMensa.AddDays((double)(dataVariavel + 1));
                        }
                        else
                        {
                            if (fixo != "S")
                                dataVectoMensa = dataVariavel > 0 ? dataVectoMensa.AddDays((double)(dataVariavel)) : dataVectoMensa.AddMonths(1);
                            else
                                dataVectoMensa = dataVariavel > 0 ? dataVectoMensa.AddMonths(1) : dataVectoMensa.AddDays((double)(dataVariavel));

                        }

                        //if (dataVariavel == 0)
                        //{
                        //    dataVectoMensa = DateTime.Parse(((diaVenctoUnid != 0) ? (diaVenctoUnid > 27 && dataVectoMensa.Month == 2 ? "27" : diaVenctoUnid.ToString("00")) : "05") + "/" + dataVectoMensa.Month.ToString("00") + "/" + dataVectoMensa.Year.ToString());
                        //}

                        //-----> Caso o usuário não tenha escolhido a quantidade de parcelas e o ano da parcela for maior que o 
                        if ((!chkGeraTotalParce.Checked) && (dataVectoMensa.Year > dataSelec.Year) && (ddlValorContratoCalc.SelectedValue != "T"))
                        {
                            i = parcelas;
                        }
                        else
                        {
                            if (i == 2)
                            {
                                desCur = 0;
                                if (DateTime.TryParse(txtPeriodoIniDesconto.Text, out dtInicDescto) && DateTime.TryParse(txtPeriodoFimDesconto.Text, out dtFimDescto))
                                {
                                    if ((dtInicDescto <= dataVectoMensa) && (dtFimDescto >= dataVectoMensa))
                                    {
                                        if (txtValorDescto.Text != "")
                                        {
                                            desCur = chkManterDescontoPerc.Checked ? ((menCur * decimal.Parse(txtValorDescto.Text)) / 100) : decimal.Parse(txtValorDescto.Text);
                                        }
                                        else
                                            desCur = 0;
                                        valorLiqui = menCur - desCur;
                                        if (valorLiqui < 0)
                                        {
                                            AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Convênio/Bolsa (R$ BOLSA) inválido.");
                                            grdNegociacao.DataSource = null;
                                            grdNegociacao.DataBind();
                                            return;
                                        }
                                        totalMensa = totalMensa + menCur;
                                        totalDescto = totalDescto + desCur;
                                        totalDesctoEspec = totalDesctoEspec + (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec : 0);
                                        if (valorLiqui < (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec : 0))
                                        {
                                            AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Desconto (R$ DESCTO) inválido.");
                                            grdNegociacao.DataSource = null;
                                            grdNegociacao.DataBind();
                                            return;
                                        }
                                        valorLiqui = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? valorLiqui - desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? valorLiqui - desEspec : valorLiqui);
                                        totalValorLiqui = totalValorLiqui + valorLiqui;
                                        Dt.Rows.Add(coEmp, "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + i.ToString("D2"), i.ToString("D2"), dataSelec.ToShortDateString(),
                                            dataVectoMensa.ToShortDateString(), menCur.ToString("N2"), desCur.ToString("N2"),
                                            ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec.ToString("N2") : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec.ToString("N2") : "0,00",
                                            valorLiqui.ToString("N2"),
                                            multaUnid.ToString("N2"), jurosUnid.ToString("N3"));
                                    }
                                    else
                                    {
                                        valorLiqui = menCur - desCur;
                                        if (valorLiqui < 0)
                                        {
                                            AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Convênio/Bolsa (R$ BOLSA) inválido.");
                                            grdNegociacao.DataSource = null;
                                            grdNegociacao.DataBind();
                                            return;
                                        }
                                        totalMensa = totalMensa + menCur;
                                        totalDescto = totalDescto + desCur;
                                        totalDesctoEspec = totalDesctoEspec + (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec : 0);
                                        if (valorLiqui < (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec : 0))
                                        {
                                            AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Desconto (R$ DESCTO) inválido.");
                                            grdNegociacao.DataSource = null;
                                            grdNegociacao.DataBind();
                                            return;
                                        }
                                        valorLiqui = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? valorLiqui - desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? valorLiqui - desEspec : valorLiqui);
                                        totalValorLiqui = totalValorLiqui + valorLiqui;
                                        Dt.Rows.Add(coEmp, "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + i.ToString("D2"), i.ToString("D2"), dataSelec.ToShortDateString(),
                                            dataVectoMensa.ToShortDateString(), menCur.ToString("N2"), desCur.ToString("N2"),
                                            ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec.ToString("N2") : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec.ToString("N2") : "0,00",
                                            valorLiqui.ToString("N2"),
                                            multaUnid.ToString("N2"), jurosUnid.ToString("N3"));
                                    }
                                }
                                else
                                {
                                    valorLiqui = menCur - desCur;
                                    if (valorLiqui < 0)
                                    {
                                        AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Convênio/Bolsa (R$ BOLSA) inválido.");
                                        grdNegociacao.DataSource = null;
                                        grdNegociacao.DataBind();
                                        return;
                                    }
                                    totalMensa = totalMensa + menCur;
                                    totalDescto = totalDescto + desCur;
                                    totalDesctoEspec = totalDesctoEspec + (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec : 0);
                                    if (valorLiqui < (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec : 0))
                                    {
                                        AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Desconto (R$ DESCTO) inválido.");
                                        grdNegociacao.DataSource = null;
                                        grdNegociacao.DataBind();
                                        return;
                                    }
                                    valorLiqui = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? valorLiqui - desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? valorLiqui - desEspec : valorLiqui);
                                    totalValorLiqui = totalValorLiqui + valorLiqui;
                                    Dt.Rows.Add(coEmp, "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + i.ToString("D2"), i.ToString("D2"), dataSelec.ToShortDateString(),
                                        dataVectoMensa.ToShortDateString(), menCur.ToString("N2"), desCur.ToString("N2"),
                                        ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec.ToString("N2") : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec.ToString("N2") : "0,00",
                                        valorLiqui.ToString("N2"),
                                        multaUnid.ToString("N2"), jurosUnid.ToString("N3"));
                                }
                            }
                            else
                            {
                                desCur = 0;
                                if (DateTime.TryParse(txtPeriodoIniDesconto.Text, out dtInicDescto) && DateTime.TryParse(txtPeriodoFimDesconto.Text, out dtFimDescto))
                                {
                                    if ((dtInicDescto <= dataVectoMensa) && (dtFimDescto >= dataVectoMensa))
                                    {
                                        if (txtValorDescto.Text != "")
                                        {
                                            desCur = chkManterDescontoPerc.Checked ? ((menCur * decimal.Parse(txtValorDescto.Text)) / 100) : decimal.Parse(txtValorDescto.Text);
                                        }
                                        else
                                            desCur = 0;
                                        valorLiqui = menCur - desCur;
                                        if (valorLiqui < 0)
                                        {
                                            AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Convênio/Bolsa (R$ BOLSA) inválido.");
                                            grdNegociacao.DataSource = null;
                                            grdNegociacao.DataBind();
                                            return;
                                        }
                                        totalMensa = totalMensa + menCur;
                                        totalDescto = totalDescto + desCur;
                                        totalDesctoEspec = totalDesctoEspec + (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec : 0);
                                        if (valorLiqui < (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec : 0))
                                        {
                                            AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Desconto (R$ DESCTO) inválido.");
                                            grdNegociacao.DataSource = null;
                                            grdNegociacao.DataBind();
                                            return;
                                        }
                                        valorLiqui = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? valorLiqui - desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? valorLiqui - desEspec : valorLiqui);
                                        totalValorLiqui = totalValorLiqui + valorLiqui;
                                        Dt.Rows.Add(coEmp, "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + i.ToString("D2"), i.ToString("D2"), dataSelec.ToShortDateString(),
                                            dataVectoMensa.ToShortDateString(), menCur.ToString("N2"), desCur.ToString("N2"),
                                            ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec.ToString("N2") : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec.ToString("N2") : "0,00",
                                            valorLiqui.ToString("N2"),
                                            multaUnid.ToString("N2"), jurosUnid.ToString("N3"));
                                    }
                                    else
                                    {
                                        valorLiqui = menCur - desCur;
                                        if (valorLiqui < 0)
                                        {
                                            AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Convênio/Bolsa (R$ BOLSA) inválido.");
                                            grdNegociacao.DataSource = null;
                                            grdNegociacao.DataBind();
                                            return;
                                        }
                                        totalMensa = totalMensa + menCur;
                                        totalDescto = totalDescto + desCur;
                                        totalDesctoEspec = totalDesctoEspec + (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec : 0);
                                        if (valorLiqui < (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec : 0))
                                        {
                                            AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Desconto (R$ DESCTO) inválido.");
                                            grdNegociacao.DataSource = null;
                                            grdNegociacao.DataBind();
                                            return;
                                        }
                                        valorLiqui = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? valorLiqui - desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? valorLiqui - desEspec : valorLiqui);
                                        totalValorLiqui = totalValorLiqui + valorLiqui;
                                        Dt.Rows.Add(coEmp, "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + i.ToString("D2"), i.ToString("D2"), dataSelec.ToShortDateString(),
                                            dataVectoMensa.ToShortDateString(), menCur.ToString("N2"), desCur.ToString("N2"),
                                            ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec.ToString("N2") : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec.ToString("N2") : "0,00",
                                            valorLiqui.ToString("N2"),
                                            multaUnid.ToString("N2"), jurosUnid.ToString("N3"));
                                    }
                                }
                                else
                                {
                                    valorLiqui = menCur - desCur;
                                    if (valorLiqui < 0)
                                    {
                                        AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Convênio/Bolsa (R$ BOLSA) inválido.");
                                        grdNegociacao.DataSource = null;
                                        grdNegociacao.DataBind();
                                        return;
                                    }
                                    totalMensa = totalMensa + menCur;
                                    totalDescto = totalDescto + desCur;
                                    totalDesctoEspec = totalDesctoEspec + (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec : 0);
                                    if (valorLiqui < (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec : 0))
                                    {
                                        AuxiliPagina.EnvioMensagemErro(this.Page, "Valor de Desconto (R$ DESCTO) inválido.");
                                        grdNegociacao.DataSource = null;
                                        grdNegociacao.DataBind();
                                        return;
                                    }
                                    valorLiqui = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? valorLiqui - desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? valorLiqui - desEspec : valorLiqui);
                                    totalValorLiqui = totalValorLiqui + valorLiqui;
                                    Dt.Rows.Add(coEmp, "MN" + dataSelec.ToString("yy") + varSer.CO_CUR.ToString().PadLeft(3, '0') + '.' + numNire.ToString().PadLeft(7, '0') + '.' + i.ToString("D2"), i.ToString("D2"), dataSelec.ToShortDateString(),
                                        dataVectoMensa.ToShortDateString(), menCur.ToString("N2"), desCur.ToString("N2"),
                                        ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec.ToString("N2") : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec.ToString("N2") : "0,00",
                                        valorLiqui.ToString("N2"),
                                        multaUnid.ToString("N2"), jurosUnid.ToString("N3"));
                                }
                            }
                        }
                        qtdeMesesDescto = (ddlTipoDesctoMensa.SelectedValue == "T" && desEspec > 0 ? desEspec : qtdeMesesDescto > 0 && i >= mesDescto ? desEspec : 0) > 0 ? qtdeMesesDescto - 1 : qtdeMesesDescto;
                    }
                }

                grdNegociacao.DataSource = Dt;
                grdNegociacao.DataBind();

                txtTotalMensa.Text = totalMensa.ToString("N2");
                txtTotalDesctoBolsa.Text = totalDescto.ToString("N2");
                txtTotalDesctoEspec.Text = totalDesctoEspec.ToString("N2");
                txtTotalLiquiContra.Text = totalValorLiqui.ToString("N2");
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Tipos de Bolsa
        /// </summary>
        private void CarregaBolsasAlt()
        {
            ddlBolsaAlunoAlt.DataSource = TB148_TIPO_BOLSA.RetornaTodosRegistros().Where(c => c.TP_GRUPO_BOLSA == ddlTpBolsaAlt.SelectedValue && c.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                && c.CO_SITUA_TIPO_BOLSA == "A");

            ddlBolsaAlunoAlt.DataTextField = "NO_TIPO_BOLSA";
            ddlBolsaAlunoAlt.DataValueField = "CO_TIPO_BOLSA";
            ddlBolsaAlunoAlt.DataBind();

            ddlBolsaAlunoAlt.Items.Insert(0, new ListItem("Nenhuma", ""));
            ddlBolsaAlunoAlt.Items.Insert(1, new ListItem("Livre", "0"));

            txtValorDescto.Text = txtPeriodoIniDesconto.Text = txtPeriodoFimDesconto.Text = "";
            chkManterDescontoPerc.Checked = chkManterDescontoPerc.Enabled =
            txtValorDescto.Enabled = txtPeriodoIniDesconto.Enabled = txtPeriodoFimDesconto.Enabled = false;
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where(m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

            ddlModalidade.DataTextField = "DE_MODU_CUR";
            ddlModalidade.DataValueField = "CO_MODU_CUR";
            ddlModalidade.DataBind();

            ddlModalidade.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            string anoGrade = DateTime.Now.Year.ToString();

            if (modalidade != 0)
            {
                ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                            where tb01.CO_MODU_CUR == modalidade && tb01.CO_SITU == "A"
                                            join tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on tb01.CO_CUR equals tb43.CO_CUR
                                            where tb43.CO_ANO_GRADE == anoGrade && tb43.TB44_MODULO.CO_MODU_CUR == modalidade
                                            select new { tb01.CO_CUR, tb01.NO_CUR }).Distinct().OrderBy(c => c.NO_CUR);

                ddlSerieCurso.DataTextField = "NO_CUR";
                ddlSerieCurso.DataValueField = "CO_CUR";
                ddlSerieCurso.DataBind();
            }
            else
                ddlSerieCurso.Items.Clear();

            ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;

            if ((modalidade != 0) && (serie != 0))
            {
                ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                       where tb06.CO_MODU_CUR == modalidade && tb06.CO_CUR == serie
                                       select new
                                       {
                                           /*CO_SIGLA_TURMA = tb06.TB129_CADTURMAS.CO_SIGLA_TURMA + " (" +
                                           (tb06.TB129_CADTURMAS.CO_TUR_REFER_ANTER != null ?
                                           (tb06.TB129_CADTURMAS.CO_TUR_REFER_ANTER == 1 ? "0T202" :
                                            tb06.TB129_CADTURMAS.CO_TUR_REFER_ANTER == 2 ? "0T302" :
                                            tb06.TB129_CADTURMAS.CO_TUR_REFER_ANTER == 3 ? "0T402" :
                                            tb06.TB129_CADTURMAS.CO_TUR_REFER_ANTER == 4 ? "0T403" :
                                            tb06.TB129_CADTURMAS.CO_TUR_REFER_ANTER == 5 ? "0M503" :
                                            tb06.TB129_CADTURMAS.CO_TUR_REFER_ANTER == 6 ? "0T502" :
                                            tb06.TB129_CADTURMAS.CO_TUR_REFER_ANTER == 7 ? "0T503" :
                                            tb06.TB129_CADTURMAS.CO_TUR_REFER_ANTER == 8 ? "1M204" :
                                            tb06.TB129_CADTURMAS.CO_TUR_REFER_ANTER == 9 ? "1T202" :
                                            tb06.TB129_CADTURMAS.CO_TUR_REFER_ANTER == 10 ? "1T203" :
                                            tb06.TB129_CADTURMAS.CO_TUR_REFER_ANTER == 11 ? "1T902" :
                                            tb06.TB129_CADTURMAS.CO_TUR_REFER_ANTER == 12 ? "2M101" :
                                            tb06.TB129_CADTURMAS.CO_TUR_REFER_ANTER == 13 ? "2M103" :
                                            tb06.TB129_CADTURMAS.CO_TUR_REFER_ANTER == 14 ? "2M201" :
                                            tb06.TB129_CADTURMAS.CO_TUR_REFER_ANTER == 15 ? "2M203" :
                                            "2M301") : tb06.TB129_CADTURMAS.CO_SIGLA_TURMA) + ")",*/
                                           tb06.TB129_CADTURMAS.CO_SIGLA_TURMA,
                                           tb06.CO_TUR
                                       }).OrderBy(t => t.CO_SIGLA_TURMA);

                ddlTurma.DataTextField = "CO_SIGLA_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataBind();
            }
            else
                ddlTurma.Items.Clear();

            ddlTurma.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Boletos
        /// </summary>
        private void CarregaBoletos()
        {
            int coEmp = TB129_CADTURMAS.RetornaPelaChavePrimaria(int.Parse(ddlTurma.SelectedValue)).CO_EMP_UNID_CONT;
            int coMod = (!string.IsNullOrEmpty(ddlModalidade.SelectedValue) ? int.Parse(ddlModalidade.SelectedValue) : 0);
            int coCur = (!string.IsNullOrEmpty(ddlSerieCurso.SelectedValue) ? int.Parse(ddlSerieCurso.SelectedValue) : 0);

            AuxiliCarregamentos.CarregaBoletos(ddlBoleto, coEmp, "E", coMod, coCur, false, false);

            ddlBoleto.Items.Insert(0, new ListItem("Nenhum", ""));

            var tb83 = (from iTb83 in TB83_PARAMETRO.RetornaTodosRegistros()
                        where iTb83.CO_EMP == coEmp
                        select new { iTb83.ID_BOLETO_MATRIC, iTb83.ID_BOLETO_MENSA }).FirstOrDefault();

            if (tb83 != null)
            {
                if (tb83.ID_BOLETO_MENSA != null)
                {
                    ddlBoleto.SelectedValue = tb83.ID_BOLETO_MENSA.ToString();
                }
            }
        }

        private void CarregaNire(int responsavel)
        {
            ddlNire.DataSource = TB07_ALUNO.RetornaTodosRegistros().Where(x => x.TB108_RESPONSAVEL.CO_RESP == responsavel);

            ddlNire.DataTextField = "NO_ALU";
            ddlNire.DataValueField = "NU_NIRE";
            ddlNire.DataBind();

            ddlNire.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion

        #region Controles

        /// <summary>
        /// Método para geração do boleto
        /// </summary>
        protected void GeraBoleto()
        {
            //--------> Instancia um novo conjunto de dados de boleto na sessão
            Session[SessoesHttp.BoletoBancarioCorrente] = new List<InformacoesBoletoBancario>();

            //--------> Dados do Aluno e Unidade
            int coAlu = int.Parse(hdCoAlu.Value);
            int coemp = TB129_CADTURMAS.RetornaPelaChavePrimaria(int.Parse(ddlTurma.SelectedValue)).CO_EMP_UNID_CONT;

            //--------> Recupera dados do Responsável do Aluno
            var s = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                     join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb108.CO_RESP equals tb07.TB108_RESPONSAVEL.CO_RESP
                     where tb07.CO_ALU == coAlu
                     join tb905 in TB905_BAIRRO.RetornaTodosRegistros() on tb108.CO_BAIRRO equals tb905.CO_BAIRRO
                     join tb904 in TB904_CIDADE.RetornaTodosRegistros() on tb108.CO_CIDADE equals tb904.CO_CIDADE
                     select new
                     {
                         NOME = tb108.NO_RESP,
                         BAIRRO = tb905.NO_BAIRRO,
                         CEP = tb108.CO_CEP_RESP,
                         CIDADE = tb904.NO_CIDADE,
                         ENDERECO = tb108.DE_ENDE_RESP,
                         NUMERO = tb108.NU_ENDE_RESP,
                         COMPL = tb108.DE_COMP_RESP,
                         UF = tb904.CO_UF,
                         CPFCNPJ = tb108.NU_CPF_RESP.Length >= 11 ? tb108.NU_CPF_RESP.Insert(9, "-").Insert(6, ".").Insert(3, ".") : tb108.NU_CPF_RESP
                     }).FirstOrDefault();

            int iGrdNeg = 1;
            //--------> Varre os títulos da grid
            foreach (GridViewRow row in grdNegociacao.Rows)
            {
                int coEmp = Convert.ToInt32(grdNegociacao.DataKeys[row.RowIndex].Values[0]);
                string nuDoc = grdNegociacao.DataKeys[row.RowIndex].Values[1].ToString();
                int nuPar = Convert.ToInt32(grdNegociacao.DataKeys[row.RowIndex].Values[2]);
                string strInstruBoleto = "";

                TB47_CTA_RECEB tb47 = TB47_CTA_RECEB.RetornaPrimeiroRegistroPeloCoEmpNuDocNuPar(ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP, nuDoc, nuPar);

                tb47.TB227_DADOS_BOLETO_BANCARIOReference.Load();
                if (tb47.TB227_DADOS_BOLETO_BANCARIO == null)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Não é possivel gerar o Boleto, Título sem Boleto Associado!");
                    return;
                }
                tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTEReference.Load();
                tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIAReference.Load();
                tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCOReference.Load();
                tb47.TB108_RESPONSAVELReference.Load();

                //------------> Se o título for gerado para um aluno:
                if (tb47.TB108_RESPONSAVEL == null)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Não é possivel gerar o Boleto, Título sem Responsável!");
                    return;
                }

                if (tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCO.CO_PROX_NOS_NUM == null)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Não é possivel gerar o Boleto. Nosso número não cadastrado para banco informado.");
                    return;
                }

                //------------> Obtém a unidade
                TB25_EMPRESA unidade = TB25_EMPRESA.RetornaPelaChavePrimaria(coemp);

                InformacoesBoletoBancario boleto = new InformacoesBoletoBancario();

                //------------> Informações do Boleto
                boleto.Carteira = tb47.TB227_DADOS_BOLETO_BANCARIO.CO_CARTEIRA.Trim();
                boleto.CodigoBanco = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.IDEBANCO;

                /*
                 * Esta parte do código valida se o título já possui um nosso número, se já tiver, ele usa o NossoNúmero do título, registrado na tabela TB47, caso contrário,
                 * ele pega o próximo NossoNúmero registrado no banco, tabela TB29.
                 * */
                if (tb47.CO_NOS_NUM != null)
                {
                    boleto.NossoNumero = tb47.CO_NOS_NUM.Trim();
                }
                else
                {
                    boleto.NossoNumero = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.TB29_BANCO.CO_PROX_NOS_NUM.Trim();
                }
                boleto.NumeroDocumento = tb47.NU_DOC + "-" + tb47.NU_PAR;
                boleto.Valor = tb47.VR_PAR_DOC; //valor da parcela do documento
                boleto.Vencimento = tb47.DT_VEN_DOC;

                //------------> Informações do Cedente
                boleto.NumeroConvenio = tb47.TB227_DADOS_BOLETO_BANCARIO.NU_CONVENIO;
                boleto.Agencia = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.CO_AGENCIA + "-" +
                                    tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.DI_AGENCIA; //titulo AGENCIA E DIGITO

                boleto.CodigoCedente = tb47.TB227_DADOS_BOLETO_BANCARIO.CO_CEDENTE.Trim();
                boleto.Conta = tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.CO_CONTA.Trim();
                boleto.CpfCnpjCedente = unidade.CO_CPFCGC_EMP;
                boleto.NomeCedente = unidade.NO_RAZSOC_EMP;

                boleto.Desconto =
                        ((!tb47.VR_DES_DOC.HasValue ? 0
                            : (tb47.CO_FLAG_TP_VALOR_DES == "P"
                                ? (boleto.Valor * tb47.VR_DES_DOC.Value / 100)
                                : tb47.VR_DES_DOC.Value))
                        + (!tb47.VL_DES_BOLSA_ALUNO.HasValue ? 0
                            : (tb47.CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO == "P"
                                ? (boleto.Valor * tb47.VL_DES_BOLSA_ALUNO.Value / 100)
                                : tb47.VL_DES_BOLSA_ALUNO.Value)));

                /**
                 * Esta validação verifica o tipo de boleto para incluir o valor de desconto nas intruções se o tipo for "M" - Modelo 4.
                 * */
                #region Valida layout do boleto gerado
                TB25_EMPRESA tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

                tb25.TB000_INSTITUICAOReference.Load();
                tb25.TB000_INSTITUICAO.TB149_PARAM_INSTIReference.Load();
                //TB000_INSTITUICAO tb000 = TB000_INSTITUICAO.RetornaPelaChavePrimaria(tb25.TB000_INSTITUICAO.ORG_CODIGO_ORGAO);

                if (tb25.TB000_INSTITUICAO.TB149_PARAM_INSTI.TP_CTRLE_SECRE_ESCOL == "I")
                {
                    switch (tb25.TB000_INSTITUICAO.TB149_PARAM_INSTI.TP_BOLETO_BANC)
                    {
                        case "M":
                            strInstruBoleto = "<b>Desconto total: </b>" + string.Format("{0:C}", boleto.Desconto) + "<br>";
                            break;
                    }
                }
                else
                {
                    switch (tb25.TP_BOLETO_BANC)
                    {
                        case "M":
                            strInstruBoleto = "<b>Desconto total: </b>" + string.Format("{0:C}", boleto.Desconto) + "<br>";
                            break;
                    }
                }
                #endregion

                if (tb47.TB227_DADOS_BOLETO_BANCARIO.FL_DESC_DIA_UTIL == "S" && tb47.TB227_DADOS_BOLETO_BANCARIO.VL_DESC_DIA_UTIL.HasValue)
                {
                    var desc = boleto.Valor - tb47.TB227_DADOS_BOLETO_BANCARIO.VL_DESC_DIA_UTIL.Value;
                    strInstruBoleto = "Receber até o 5º dia útil (" + AuxiliCalculos.RetornarDiaUtilMes(boleto.Vencimento.Year, boleto.Vencimento.Month, 5).ToShortDateString() + ") o valor: " + string.Format("{0:C}", desc) + "<br>";
                }

                //------------> Prenche as instruções do boleto de acordo com o cadastro do boleto informado
                if (tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR1_BOLETO_BANCO != null)
                    strInstruBoleto += tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR1_BOLETO_BANCO + "</br>";

                if (tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR2_BOLETO_BANCO != null)
                    strInstruBoleto += tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR2_BOLETO_BANCO + "</br>";

                if (tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR3_BOLETO_BANCO != null)
                    strInstruBoleto += tb47.TB227_DADOS_BOLETO_BANCARIO.DE_INSTR3_BOLETO_BANCO + "</br>";

                boleto.Instrucoes = strInstruBoleto;

                //------------> Chave do Título do Contas a Receber
                boleto.CO_EMP = tb47.CO_EMP;
                boleto.NU_DOC = tb47.NU_DOC;
                boleto.NU_PAR = tb47.NU_PAR;
                boleto.DT_CAD_DOC = tb47.DT_CAD_DOC;

                //                string multaMoraDesc = "";

                ////------------> Informações da Multa
                //                multaMoraDesc += tb47.VR_MUL_DOC != null && tb47.VR_MUL_DOC.Value != 0 ?
                //                    (tb47.CO_FLAG_TP_VALOR_MUL == "P" ? "Multa: " + tb47.VR_MUL_DOC.Value.ToString("0.00") + "% (R$ " +
                //                    (boleto.Valor * (decimal)tb47.VR_MUL_DOC.Value / 100).ToString("0.00") + ")" : "Multa: R$ " + tb47.VR_MUL_DOC.Value.ToString("0.00")) : "Multa: XX";

                ////------------> Informações da Mora
                //                multaMoraDesc += tb47.VR_JUR_DOC != null && tb47.VR_JUR_DOC.Value != 0 ?
                //                     (tb47.CO_FLAG_TP_VALOR_JUR == "P" ? " - Juros Diário: " + tb47.VR_JUR_DOC.Value.ToString() + "% (R$ " +
                //                     (boleto.Valor * (decimal)tb47.VR_JUR_DOC.Value / 100).ToString("0.00") + ")" : " - Juros Diário: R$ " +
                //                        tb47.VR_JUR_DOC.Value.ToString("0.00")) : " - Juros Diário: XX";

                ////------------> Informações do desconto
                //                multaMoraDesc += tb47.VR_DES_DOC != null && tb47.VR_DES_DOC.Value != 0 ?
                //                     (tb47.CO_FLAG_TP_VALOR_DES == "P" ? " - Descto: " + tb47.VR_DES_DOC.Value.ToString("0.00") + "% (R$ " +
                //                     (boleto.Valor * (decimal)tb47.VR_DES_DOC.Value / 100).ToString("0.00") + ")" : " - Descto: R$ " +
                //                        tb47.VR_DES_DOC.Value.ToString("0.00")) : " - Descto: XX";

                //                multaMoraDesc += tb47.VL_DES_BOLSA_ALUNO != null && tb47.VL_DES_BOLSA_ALUNO.Value != 0 ?
                //                         (tb47.CO_FLAG_TP_VALOR_DES_BOLSA_ALUNO == "P" ? " - Descto Bolsa: " + tb47.VL_DES_BOLSA_ALUNO.Value.ToString("0.00") + "% (R$ " +
                //                         (boleto.Valor * (decimal)tb47.VL_DES_BOLSA_ALUNO.Value / 100).ToString("0.00") + ")" : " - Descto Bolsa: R$ " +
                //                            tb47.VL_DES_BOLSA_ALUNO.Value.ToString("0.00")) : "";


                //------------> Faz a adição de instruções ao Boleto
                boleto.Instrucoes += "<br>";
                //boleto.Instrucoes += "(*) " + multaMoraDesc + "<br>";


                //------------> Coloca na Instrução as Informações do Responsável do Aluno ou Informações do Cliente
                string CnpjCPF = "";

                //------------> Ano Refer: - Matrícula: - Nº NIRE:
                //------------> Modalidade: - Série: - Turma: - Turno:
                var inforAluno = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                  join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb08.CO_CUR equals tb01.CO_CUR
                                  join tb129 in TB129_CADTURMAS.RetornaTodosRegistros() on tb08.CO_TUR equals tb129.CO_TUR
                                  where tb08.CO_EMP == tb47.CO_EMP && tb08.CO_CUR == tb47.CO_CUR && tb08.CO_ANO_MES_MAT == tb47.CO_ANO_MES_MAT
                                  && tb08.CO_ALU == tb47.CO_ALU
                                  select new
                                  {
                                      tb08.TB44_MODULO.DE_MODU_CUR,
                                      tb01.NO_CUR,
                                      tb129.CO_SIGLA_TURMA,
                                      tb08.CO_ANO_MES_MAT,
                                      tb08.CO_ALU_CAD,
                                      tb08.TB07_ALUNO.NU_NIRE,
                                      tb08.TB07_ALUNO.NO_ALU,
                                      TURNO = tb08.CO_TURN_MAT == "M" ? "Matutino" : tb08.CO_TURN_MAT == "N" ? "Noturno" : "Vespertino"
                                  }).FirstOrDefault();

                if (inforAluno != null)
                {
                    CnpjCPF = "Aluno(a): " + inforAluno.NO_ALU + "<br>Nº NIRE: " + inforAluno.NU_NIRE.ToString() +
                                 " - Matrícula: " + inforAluno.CO_ALU_CAD.Insert(2, ".").Insert(6, ".") +
                                 " - Ano/Mês Refer: " + tb47.NU_PAR.ToString().PadLeft(2, '0') + "/" + inforAluno.CO_ANO_MES_MAT.Trim() +
                                 "<br>" + inforAluno.DE_MODU_CUR + " - Série: " + inforAluno.NO_CUR +
                                 " - Turma: " + inforAluno.CO_SIGLA_TURMA + " - Turno: " + inforAluno.TURNO;
                    //CnpjCPF = "Ano Refer: " + inforAluno.CO_ANO_MES_MAT.Trim() + " - Matrícula: " + inforAluno.CO_ALU_CAD.Insert(2, ".").Insert(6, ".") + " - Nº NIRE: " +
                    //    inforAluno.NU_NIRE.ToString() + "<br> Modalidade: " + inforAluno.DE_MODU_CUR + " - Série: " + inforAluno.NO_CUR +
                    //    " - Turma: " + inforAluno.CO_SIGLA_TURMA + " - Turno: " + inforAluno.TURNO + " <br> Aluno(a): " + inforAluno.NO_ALU;

                    boleto.ObsCanhoto = string.Format("Aluno: {0}", inforAluno.NO_ALU);
                }

                boleto.Instrucoes += CnpjCPF + "<br>*** Referente: " + tb47.DE_COM_HIST + " ***";

                //------------> Informações do Sacado
                boleto.BairroSacado = s.BAIRRO;
                boleto.CepSacado = s.CEP;
                boleto.CidadeSacado = s.CIDADE;
                boleto.CpfCnpjSacado = s.CPFCNPJ;
                boleto.EnderecoSacado = s.ENDERECO + " " + s.NUMERO + " " + s.COMPL;
                boleto.NomeSacado = s.NOME;
                boleto.UfSacado = s.UF;

                //------------> Adiciona o título atual na Sessão
                ((List<InformacoesBoletoBancario>)Session[SessoesHttp.BoletoBancarioCorrente]).Add(boleto);

                /*
                 * Esta validação verifica se o título já possui NossoNúmaro, se não for o caso, ele atualiza o título incluíndo um novo NossoNúmero, e atualiza a tabela
                 * TB29 para incrementar o próximo NossoNúmero do banco.
                 * */
                if (tb47.CO_NOS_NUM == null)
                {
                    if ((iGrdNeg <= grdNegociacao.Rows.Count) && (grdNegociacao.Rows.Count > 1))
                    {
                        TB29_BANCO u = TB29_BANCO.RetornaPelaChavePrimaria(tb47.TB227_DADOS_BOLETO_BANCARIO.TB224_CONTA_CORRENTE.TB30_AGENCIA.IDEBANCO);

                        /*
                         * Esta parte do código atualiza o NossoNúmero do título (TB47).
                         * Esta linha foi incluída para resolver o problema de boletos diferentes sendo gerados para um mesmo
                         * título
                         * */
                        tb47.CO_NOS_NUM = u.CO_PROX_NOS_NUM;
                        GestorEntities.SaveOrUpdate(tb47, true);

                        //===> Incluí o nosso número na tabela de nossos números por título
                        TB045_NOS_NUM tb045 = new TB045_NOS_NUM();
                        tb045.NU_DOC = tb47.NU_DOC;
                        tb045.NU_PAR = tb47.NU_PAR;
                        tb045.DT_CAD_DOC = tb47.DT_CAD_DOC;
                        tb045.DT_NOS_NUM = DateTime.Now;
                        tb045.IP_NOS_NUM = LoginAuxili.IP_USU;
                        //===> Pega as informações da empresa/unidade
                        TB25_EMPRESA emp = TB25_EMPRESA.RetornaPelaChavePrimaria(tb47.CO_EMP);
                        tb045.TB25_EMPRESA = emp;
                        //===> Pega as informações do colaborador
                        TB03_COLABOR tb03 = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, LoginAuxili.CO_COL);
                        tb045.TB03_COLABOR = tb03;
                        tb045.CO_NOS_NUM = tb47.CO_NOS_NUM;
                        tb045.CO_BARRA_DOC = tb47.CO_BARRA_DOC;
                        GestorEntities.SaveOrUpdate(tb045, true);

                        long nossoNumero = long.Parse(u.CO_PROX_NOS_NUM) + 1;
                        int casas = u.CO_PROX_NOS_NUM.Length;
                        string mask = string.Empty;
                        foreach (char ch in u.CO_PROX_NOS_NUM) mask += "0";
                        u.CO_PROX_NOS_NUM = nossoNumero.ToString(mask);
                        GestorEntities.SaveOrUpdate(u, true);
                    }
                }

                iGrdNeg++;
            }

            //--------> Gera e exibe os boletos
            BoletoBancarioHelpers.GeraBoletos(this);
        }

        /// <summary>
        /// Método para desabilitar campos após a matrícula        
        /// </summary>
        private void DesabilitaCamposMatricula()
        {
            btnPesqReserva.Enabled = ddlSituMatAluno.Enabled = ddlModalidade.Enabled = ddlSerieCurso.Enabled = ddlTurma.Enabled = lnkEfetMatric.Enabled = chkIntegMensaSerie.Enabled =
            chkMenEscAlu.Enabled = txtNumReserva.Enabled = ddlUnidade.Enabled = false;
            chkMenEscAlu.Checked = true;
            chkAtualiFinan.Enabled = ddlBoleto.Enabled = ddlTipoDesctoMensa.Enabled = txtQtdeMesesDesctoMensa.Enabled = txtDesctoMensa.Enabled =
            lnkMontaGridMensa.Enabled = lnkMenAlu.Enabled = false;
        }
        #endregion

        protected void atualizaParcela()
        {
            if (!String.IsNullOrEmpty(ddlTurma.SelectedValue) && String.IsNullOrEmpty(txtValorPrimParce.Text))
            {
                //-------> Valor calculada a partir do turno da turma
                //-----------> Pega o Curso
                int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
                int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
                var varSer = TB01_CURSO.RetornaPelaChavePrimaria(ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP, modalidade, serie);
                string turnoTurma = TB06_TURMAS.RetornaPeloCodigo(int.Parse(ddlTurma.SelectedValue)).CO_PERI_TUR;

                if (varSer != null)
                {
                    //-----> Determina o número de parcelas utilizado no cálculo das parcelas
                    int qtdParcelas = 0;
                    if (chkGeraTotalParce.Checked)
                    {
                        qtdParcelas = int.Parse(txtQtdeParcelas.Text);
                    }

                    DateTime dataSelec;
                    if (chkDataPrimeiraParcela.Checked && txtDtPrimeiraParcela.Text != "")
                    {
                        dataSelec = DateTime.Parse(txtDtPrimeiraParcela.Text);
                    }
                    else
                    {
                        dataSelec = DateTime.Now.Year == int.Parse(txtAno.Text) ? (int.Parse(ddlDiaVecto.SelectedValue) > 27 && DateTime.Now.Month == 2 ? DateTime.Parse("27/" + DateTime.Now.Month.ToString("D2") + '/' + DateTime.Now.Year.ToString()) : DateTime.Parse(ddlDiaVecto.SelectedValue + '/' + DateTime.Now.Month.ToString("D2") + '/' + DateTime.Now.Year.ToString())) : DateTime.Parse(ddlDiaVecto.SelectedValue + "/01/" + txtAno.Text);
                        if (DateTime.Now.Year == int.Parse(txtAno.Text) && dataSelec < DateTime.Now)
                        {
                            if (varSer.QT_DIAS_PARC1 != null)
                            {
                                if (varSer.QT_DIAS_PARC1 > 0)
                                {
                                    dataSelec = DateTime.Now.AddDays((double)varSer.QT_DIAS_PARC1);
                                }
                            }
                            else
                                dataSelec = DateTime.Now;
                        }
                        else
                        {
                            if (varSer.QT_DIAS_PARC1 != null)
                            {
                                if (varSer.QT_DIAS_PARC1 > 0)
                                {
                                    dataSelec = dataSelec.AddDays((double)varSer.QT_DIAS_PARC1);
                                }
                            }
                        }
                    }

                    switch (ddlValorContratoCalc.SelectedValue)
                    {
                        case "P":
                            qtdParcelas = varSer.NU_QUANT_MESES > 0 && varSer.NU_QUANT_MESES == 12 && (!chkGeraTotalParce.Checked) ? varSer.NU_QUANT_MESES - dataSelec.Month + 1 : varSer.NU_QUANT_MESES;
                            break;
                        case "T":
                            qtdParcelas = varSer.NU_QUANT_MESES;
                            break;
                    }

                    decimal x = 0;
                    if (!chkIntegMensaSerie.Checked)
                    {
                        switch (turnoTurma)
                        {
                            case "M":
                                //------> Parcela calculada com o valor referente ao turno Matutino
                                switch (ddlTipoContrato.SelectedValue)
                                {
                                    case "P":
                                        //-------> Parcela calculada com o valor referente ao turno Matutino a prazo
                                        x = decimal.Parse(varSer.VL_CONTMAN_APRAZ.ToString()) / qtdParcelas;
                                        txtValorPrimParce.Text = Math.Round(x, 2).ToString();
                                        break;
                                    case "V":
                                        //-------> Parcela calculada com o valor referente ao turno Matutino a vista
                                        x = decimal.Parse(varSer.VL_CONTMAN_AVIST.ToString());
                                        txtValorPrimParce.Text = Math.Round(x, 2).ToString();
                                        break;
                                }
                                break;
                            case "V":
                                //------> Parcela calculada com o valor referente ao turno Vespertino
                                switch (ddlTipoContrato.SelectedValue)
                                {
                                    case "P":
                                        //-------> Parcela calculada com o valor referente ao turno Vespertino a prazo
                                        x = decimal.Parse(varSer.VL_CONTTAR_APRAZ.ToString()) / qtdParcelas;
                                        txtValorPrimParce.Text = Math.Round(x, 2).ToString();
                                        break;
                                    case "V":
                                        //-------> Parcela calculada com o valor referente ao turno Vespertino a vista
                                        x = decimal.Parse(varSer.VL_CONTTAR_AVIST.ToString());
                                        txtValorPrimParce.Text = Math.Round(x, 2).ToString();
                                        break;
                                }
                                break;
                            case "N":
                                //------> Parcela calculada com o valor referente ao turno Noturno
                                switch (ddlTipoContrato.SelectedValue)
                                {
                                    case "P":
                                        //-------> Parcela calculada com o valor referente ao turno Noturno a prazo
                                        x = decimal.Parse(varSer.VL_CONTNOI_APRAZ.ToString()) / qtdParcelas;
                                        txtValorPrimParce.Text = Math.Round(x, 2).ToString();
                                        break;
                                    case "V":
                                        //-------> Parcela calculada com o valor referente ao turno Noturno a vista
                                        x = decimal.Parse(varSer.VL_CONTNOI_AVIST.ToString());
                                        txtValorPrimParce.Text = Math.Round(x, 2).ToString();
                                        break;
                                }
                                break;
                        }
                    }
                    else
                    {
                        //------> Parcela calculada a apartir do valor integral
                        switch (ddlTipoContrato.SelectedValue)
                        {
                            case "P":
                                //-------> Parcela calculada com o valor integral a prazo
                                x = decimal.Parse(varSer.VL_CONTINT_APRAZ.ToString()) / qtdParcelas;
                                txtValorPrimParce.Text = Math.Round(x, 2).ToString();
                                break;
                            case "V":
                                //-------> Parcela calculada com o valor integral a vista
                                x = decimal.Parse(varSer.VL_CONTINT_AVIST.ToString());
                                txtValorPrimParce.Text = Math.Round(x, 2).ToString();
                                break;
                        }
                    }
                }
            }
        }

        #region Eventos componentes do sistema

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
        }

        /// <summary>
        /// Método que trata o clique do botão de CPF do responsável
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCPFResp_Click(object sender, ImageClickEventArgs e)
        {
            string strCPFResp = this.txtCPFResp.Text.Replace(".", "").Replace("-", "").Trim();
            hdCoRes.Value = "";
            txtNoRespCPF.Text = "";

            if (strCPFResp != "")
            {
                var tb108 = (from iTb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                             where iTb108.NU_CPF_RESP == strCPFResp
                             select new { iTb108.CO_RESP, iTb108.NO_RESP, iTb108.CO_UF_TIT_ELE_RESP }).FirstOrDefault();

                if (tb108 != null)
                {
                    if (tb108.CO_UF_TIT_ELE_RESP == null || tb108.CO_UF_TIT_ELE_RESP.Trim() == "" || tb108.CO_UF_TIT_ELE_RESP.Trim().Length != 2)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "UF do título de eleitor do responsável inválida.");
                        return;
                    }
                    hdCoRes.Value = tb108.CO_RESP.ToString();
                    txtNoRespCPF.Text = tb108.NO_RESP;
                    CarregaNire(tb108.CO_RESP);
                }
                else
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Não existe responsável cadastrado com o CPF informado.");
                    return;
                }
            }
        }

        /// <summary>
        /// Método que trata o clique do botão de NIRE do Aluno
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        protected void ddlNoRespCPF_OnSelectedIndexChanged(object sender, EventArgs e) { }

        protected void btnPesqNIRE_Click(object sender, ImageClickEventArgs e)
        {
            string tipoMatricula = ddlSituMatAluno.SelectedValue != null && ddlSituMatAluno.SelectedValue == "X" ? "R" : "A";
            int numNIRE = this.txtNumNIRE.Text.Replace(".", "").Replace("-", "").Trim() != "" ? int.Parse(this.txtNumNIRE.Text.Replace(".", "").Replace("-", "").Trim()) : 0;
            hdCoAlu.Value = "";
            txtNoInfAluno.Text = "";
            txtNIREAluME.Text = "";
            txtNomeAluME.Text = "";
            txtNISAluME.Text = "";

            if (numNIRE != 0)
            {
                var tb07 = (from iTb07 in TB07_ALUNO.RetornaTodosRegistros()
                            join itb107 in TB108_RESPONSAVEL.RetornaTodosRegistros() on iTb07.TB108_RESPONSAVEL.CO_RESP equals itb107.CO_RESP
                            where iTb07.NU_NIRE == numNIRE
                            select new { iTb07.CO_ALU, iTb07.NO_ALU, iTb07.NU_NIRE, iTb07.NU_NIS, itb107.NO_RESP, itb107.NU_CPF_RESP, itb107.NU_NIS_RESP }).FirstOrDefault();

                if (tb07 != null)
                {
                    var ocoMat = (from lTb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                  join iTb01 in TB01_CURSO.RetornaTodosRegistros() on lTb08.CO_CUR equals iTb01.CO_CUR
                                  where lTb08.TB07_ALUNO.CO_ALU == tb07.CO_ALU && lTb08.CO_SIT_MAT == "A"
                                  && (iTb01.CO_NIVEL_CUR == "I" || iTb01.CO_NIVEL_CUR == "F"
                                  || iTb01.CO_NIVEL_CUR == "M")
                                  select new { lTb08.CO_SIT_MAT, lTb08.CO_ANO_MES_MAT });

                    if (ocoMat.Count() > 0 && tipoMatricula.Equals("A"))
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Aluno do Ensino Regular possui matrícula em aberto.");
                        hdCoRes.Value = "";
                        txtNoRespCPF.Text = "";
                        hdCoAlu.Value = "";
                        txtNumNIRE.Text = "";
                        txtNoInfAluno.Text = "";
                        ddlNire.Items.Clear();
                        txtAno.Text = DateTime.Now.Year.ToString();
                        return;
                    }
                    else if (ocoMat.Count() == 0 && tipoMatricula.Equals("A"))
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Por favor, escolha o tipo de matrícula como RENOVAÇÃO.");
                        hdCoRes.Value = "";
                        txtNoRespCPF.Text = "";
                        hdCoAlu.Value = "";
                        txtNumNIRE.Text = "";
                        txtNoInfAluno.Text = "";
                        ddlNire.Items.Clear();
                        txtAno.Text = DateTime.Now.Year.ToString();
                        return;
                    }
                    else
                    {
                        int proxAno = int.Parse(DateTime.Now.Year.ToString()) + 1;
                        hdCoAlu.Value = tb07.CO_ALU.ToString();
                        txtNoInfAluno.Visible = true;
                        ddlNire.Visible = false;
                        txtNoInfAluno.Text = tb07.NO_ALU;
                        txtNIREAluME.Text = tb07.NU_NIRE.ToString();
                        txtNomeAluME.Text = tb07.NO_ALU.ToUpper();
                        txtNISAluME.Text = tb07.NU_NIS != null ? tb07.NU_NIS.ToString() : "";
                        txtCPFResp.Text = tb07.NU_CPF_RESP;
                        txtNoRespCPF.Text = tb07.NO_RESP;
                        txtAno.Text = proxAno.ToString();
                    }
                }
                else
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Não existe aluno cadastrado com o NIRE informado.");
                    return;
                }
            }
        }

        protected void ddlNire_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tipoMatricula = ddlSituMatAluno.SelectedValue != null && ddlSituMatAluno.SelectedValue == "X" ? "R" : "A";
            int numNIRE = ddlNire.SelectedValue != null || ddlNire.SelectedValue != "" ? int.Parse(ddlNire.SelectedValue) : 0;

            if (numNIRE != 0)
            {
                var tb07 = (from iTb07 in TB07_ALUNO.RetornaTodosRegistros()
                            where iTb07.NU_NIRE == numNIRE
                            select new { iTb07.CO_ALU, iTb07.NO_ALU, iTb07.NU_NIRE, iTb07.NU_NIS }).FirstOrDefault();

                if (tb07 != null)
                {
                    var ocoMat = (from lTb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                  join iTb01 in TB01_CURSO.RetornaTodosRegistros() on lTb08.CO_CUR equals iTb01.CO_CUR
                                  where lTb08.TB07_ALUNO.CO_ALU == tb07.CO_ALU && lTb08.CO_SIT_MAT == "A"
                                  && (iTb01.CO_NIVEL_CUR == "I" || iTb01.CO_NIVEL_CUR == "F"
                                  || iTb01.CO_NIVEL_CUR == "M")
                                  select new { lTb08.CO_SIT_MAT, lTb08.CO_ANO_MES_MAT });

                    if (ocoMat.Count() > 0 && tipoMatricula.Equals("A"))
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Aluno do Ensino Regular possui matrícula em aberto.");
                        hdCoRes.Value = "";
                        txtNoRespCPF.Text = "";
                        hdCoAlu.Value = "";
                        txtNumNIRE.Text = "";
                        txtNoInfAluno.Text = "";
                        ddlNire.Items.Clear();
                        txtAno.Text = DateTime.Now.Year.ToString();
                        return;
                    }
                    else if (ocoMat.Count() == 0 && tipoMatricula.Equals("A"))
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Por favor, escolha o tipo de matrícula como RENOVAÇÃO.");
                        hdCoRes.Value = "";
                        txtNoRespCPF.Text = "";
                        hdCoAlu.Value = "";
                        txtNumNIRE.Text = "";
                        txtNoInfAluno.Text = "";
                        ddlNire.Items.Clear();
                        txtAno.Text = DateTime.Now.Year.ToString();
                        return;
                    }
                    else
                    {
                        int proxAno = int.Parse(DateTime.Now.Year.ToString()) + 1;
                        hdCoAlu.Value = ddlNire.SelectedValue.ToString();
                        txtNumNIRE.Text = (ddlNire.SelectedValue).ToString();
                        txtNoInfAluno.Text = ddlNire.SelectedItem.Text;
                        txtNIREAluME.Text = tb07.NU_NIRE.ToString();
                        txtNomeAluME.Text = tb07.NO_ALU.ToUpper();
                        txtNISAluME.Text = tb07.NU_NIS != null ? tb07.NU_NIS.ToString() : "";
                        txtAno.Text = DateTime.Now.Year.ToString();
                    }
                }
                else
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Não existe aluno cadastrado com o NIRE informado.");
                    return;
                }
            }
        }

        /// <summary>
        /// Método que trata o clique do botão de Reserva da Matrícula
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPesqReserva_Click(object sender, ImageClickEventArgs e)
        {
            if (this.txtNumReserva.Text.Trim() != "")
            {
                var reser = (from lTb052 in TB052_RESERV_MATRI.RetornaTodosRegistros()
                             join tb01 in TB01_CURSO.RetornaTodosRegistros() on lTb052.TB01_CURSO.CO_CUR equals tb01.CO_CUR
                             where tb01.CO_EMP == lTb052.TB01_CURSO.CO_EMP && tb01.TB44_MODULO.CO_MODU_CUR == lTb052.TB01_CURSO.CO_MODU_CUR
                             && lTb052.NU_RESERVA == txtNumReserva.Text.Trim() && lTb052.CO_STATUS == "A"
                             select new { tb01.NO_CUR, tb01.CO_NIVEL_CUR, tb01.TB44_MODULO.DE_MODU_CUR, lTb052 }).FirstOrDefault();

                if (reser != null)
                {
                    //btnPesqNIRE.Enabled = false;
                    txtNumNIRE.Enabled = false;
                    this.ddlSituMatAluno.SelectedValue = "MR";
                    reser.lTb052.TB01_CURSOReference.Load();
                    reser.lTb052.TB07_ALUNOReference.Load();
                    reser.lTb052.TB25_EMPRESAReference.Load();
                    reser.lTb052.TB25_EMPRESA1Reference.Load();

                    if (reser.CO_NIVEL_CUR == "I")
                        this.txtDadosReserva.Text = "Ensino Infantil";
                    else if (reser.CO_NIVEL_CUR == "F")
                        this.txtDadosReserva.Text = "Ensino Fundamental";
                    else if (reser.CO_NIVEL_CUR == "M")
                        this.txtDadosReserva.Text = "Ensino Médio";
                    else if (reser.CO_NIVEL_CUR == "G")
                        this.txtDadosReserva.Text = "Graduação";
                    else if (reser.CO_NIVEL_CUR == "C")
                        this.txtDadosReserva.Text = "Pós-Graduação";
                    else if (reser.CO_NIVEL_CUR == "R")
                        this.txtDadosReserva.Text = "Mestrado";
                    else if (reser.CO_NIVEL_CUR == "D")
                        this.txtDadosReserva.Text = "Doutorado";
                    else if (reser.CO_NIVEL_CUR == "S")
                        this.txtDadosReserva.Text = "Pós-Doutorado";
                    else if (reser.CO_NIVEL_CUR == "E")
                        this.txtDadosReserva.Text = "Especialização";
                    else if (reser.CO_NIVEL_CUR == "P")
                        this.txtDadosReserva.Text = "Preparatório";
                    else
                        this.txtDadosReserva.Text = "Outros";

                    this.txtDadosReserva.Text = this.txtDadosReserva.Text + " - " + reser.NO_CUR;

                    if (reser.lTb052.CO_PERI_TUR == "M")
                        this.txtDadosReserva.Text = this.txtDadosReserva.Text + " - Manhã";
                    else if (reser.lTb052.CO_PERI_TUR == "N")
                        this.txtDadosReserva.Text = this.txtDadosReserva.Text + " - Noturno";
                    else
                        this.txtDadosReserva.Text = this.txtDadosReserva.Text + " - Vespertino";

                    int unid2 = (reser.lTb052.TB25_EMPRESA != null) ? reser.lTb052.TB25_EMPRESA.CO_EMP : 0;
                    int unid3 = (reser.lTb052.TB25_EMPRESA1 != null) ? reser.lTb052.TB25_EMPRESA1.CO_EMP : 0;

                    var unidEns = from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                  where tb25.CO_EMP.Equals(reser.lTb052.TB01_CURSO.CO_EMP) &&
                                  (unid2 != 0 ? tb25.CO_EMP.Equals(unid2) : unid2 == 0) &&
                                  (unid3 != 0 ? tb25.CO_EMP.Equals(unid3) : unid3 == 0)
                                  select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP };

                    this.ddlUnidade.DataSource = unidEns;
                    this.ddlUnidade.DataTextField = "NO_FANTAS_EMP";
                    this.ddlUnidade.DataValueField = "CO_EMP";
                    this.ddlUnidade.DataBind();
                    this.ddlUnidade.SelectedValue = reser.lTb052.TB01_CURSO.CO_EMP.ToString();
                    this.ddlModalidade.Items.Clear();
                    this.ddlModalidade.Items.Insert(0, new ListItem(reser.DE_MODU_CUR, reser.lTb052.TB01_CURSO.CO_MODU_CUR.ToString()));
                    this.ddlModalidade.Enabled = false;
                    this.ddlSerieCurso.Items.Clear();
                    this.ddlSerieCurso.Items.Insert(0, new ListItem(reser.NO_CUR, reser.lTb052.TB01_CURSO.CO_CUR.ToString()));
                    this.ddlSerieCurso.Enabled = false;

                    this.ddlTurma.Items.Clear();
                    this.ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                                where tb06.CO_CUR == reser.lTb052.TB01_CURSO.CO_CUR && tb06.CO_MODU_CUR == reser.lTb052.TB01_CURSO.CO_MODU_CUR
                                                && tb06.CO_EMP == reser.lTb052.TB01_CURSO.CO_EMP
                                                select new { tb06.CO_TUR, tb06.TB129_CADTURMAS.CO_SIGLA_TURMA }).OrderBy(t => t.CO_SIGLA_TURMA);

                    this.ddlTurma.DataTextField = "CO_SIGLA_TURMA";
                    this.ddlTurma.DataValueField = "CO_TUR";
                    this.ddlTurma.DataBind();

                    reser.lTb052.TB108_RESPONSAVELReference.Load();

                    //----------------> Verifica se na tabela de reserva responsável já é cadastrado.
                    if (reser.lTb052.TB108_RESPONSAVEL != null)
                    {
                        this.chkRecResResp.Checked = this.chkRecResResp.Enabled = true;
                        this.hdfCPFRespRes.Value = reser.lTb052.TB108_RESPONSAVEL.NU_CPF_RESP;
                        this.txtCPFResp.Text = reser.lTb052.TB108_RESPONSAVEL.NU_CPF_RESP;
                        this.txtNoRespCPF.Text = reser.lTb052.TB108_RESPONSAVEL.NO_RESP;
                        this.hdCoRes.Value = reser.lTb052.TB108_RESPONSAVEL.CO_RESP.ToString();
                    }

                    reser.lTb052.TB07_ALUNOReference.Load();
                    if (reser.lTb052.TB07_ALUNO != null)
                    {
                        this.txtNumNIRE.Text = reser.lTb052.TB07_ALUNO.NU_NIRE.ToString();
                        this.txtNoInfAluno.Text = reser.lTb052.TB07_ALUNO.NO_ALU;
                        this.hdCoAlu.Value = reser.lTb052.TB07_ALUNO.CO_ALU.ToString();

                    }
                }
                else
                {
                    this.txtDadosReserva.Text = "";
                    this.ddlUnidade.Items.Clear();
                    this.CarregaModalidades();
                    this.CarregaSerieCurso();
                    this.CarregaTurma();
                    ddlSituMatAluno.SelectedValue = "MS";
                }
            }
        }

        protected void chkRecResResp_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkRecResResp.Checked && (this.txtCPFResp.Text.Replace(".", "").Replace("-", "").Trim() != this.hdfCPFRespRes.Value))
            {
                if (this.hdCoRes.Value != "")
                {
                    //this.txtCPFResp.Text = this.txtCPFRespDados.Text;
                }
                else
                {
                    TB052_RESERV_MATRI tb052 = TB052_RESERV_MATRI.RetornaPelaChavePrimaria(this.txtNumReserva.Text, LoginAuxili.CO_EMP);

                    if (tb052 != null)
                    {
                    }
                }
            }
        }

        protected void chkGeraTotalParce_CheckedChanged(object sender, EventArgs e)
        {
            TB01_CURSO serie = TB01_CURSO.RetornaPeloCoCur(int.Parse(this.ddlSerieCurso.SelectedValue));

            txtQtdeParcelas.Text = serie.NU_QUANT_MESES.ToString();
            txtQtdeParcelas.Enabled = chkGeraTotalParce.Checked;
        }

        protected void chkManterDesconto_CheckedChanged(object sender, EventArgs e)
        {
            if (chkManterDesconto.Checked)
            {
                // Habilita os campos de alteração do desconto do aluno
                ddlTpBolsaAlt.Enabled =
                ddlBolsaAlunoAlt.Enabled =
                txtPeriodoIniDesconto.Enabled =
                txtPeriodoFimDesconto.Enabled = true;

                CarregaBolsasAlt();

                #region Carrega a bolsa cadastrada para o aluno
                //int coBolsa = int.Parse(ddlBolsaAluno.SelectedValue);
                //var tb148 = (from iTb148 in TB148_TIPO_BOLSA.RetornaTodosRegistros()
                //             where iTb148.CO_TIPO_BOLSA == coBolsa
                //             select new { iTb148.VL_TIPO_BOLSA, iTb148.FL_TIPO_VALOR_BOLSA, iTb148.DT_INICI_TIPO_BOLSA, iTb148.DT_FIM_TIPO_BOLSA }).FirstOrDefault();

                //if (tb148 != null)
                //{
                //    txtValorDescto.Text = tb148.VL_TIPO_BOLSA != null ? String.Format("{0:N}", tb148.VL_TIPO_BOLSA) : "";
                //    chkManterDescontoPerc.Checked = tb148.FL_TIPO_VALOR_BOLSA == "P";

                //    if (tb148.DT_INICI_TIPO_BOLSA != null)
                //    {
                //        txtPeriodoIniDesconto.Text = tb148.DT_INICI_TIPO_BOLSA.Value.ToString("dd/MM/yyyy");
                //        txtPeriodoFimDesconto.Text = tb148.DT_FIM_TIPO_BOLSA.Value.ToString("dd/MM/yyyy");
                //    }
                //    else
                //    {
                //        txtPeriodoIniDesconto.Text = "";
                //        txtPeriodoFimDesconto.Text = "";
                //    }
                //}
                #endregion

            }
            else
            {
                // Habilita os campos de alteração do desconto do aluno
                ddlTpBolsaAlt.Enabled =
                ddlBolsaAlunoAlt.Enabled =
                txtPeriodoIniDesconto.Enabled =
                txtValorDescto.Enabled =
                chkManterDescontoPerc.Enabled =
                txtPeriodoFimDesconto.Enabled = false;

                CarregaBolsasAlt();

                #region Carrega a bolsa cadastrada para o aluno
                //int coBolsa = int.Parse(ddlBolsaAluno.SelectedValue);
                //var tb148 = (from iTb148 in TB148_TIPO_BOLSA.RetornaTodosRegistros()
                //             where iTb148.CO_TIPO_BOLSA == coBolsa
                //             select new { iTb148.VL_TIPO_BOLSA, iTb148.FL_TIPO_VALOR_BOLSA, iTb148.DT_INICI_TIPO_BOLSA, iTb148.DT_FIM_TIPO_BOLSA }).FirstOrDefault();

                //if (tb148 != null)
                //{
                //    txtValorDescto.Text = tb148.VL_TIPO_BOLSA != null ? String.Format("{0:N}", tb148.VL_TIPO_BOLSA) : "";
                //    chkManterDescontoPerc.Checked = tb148.FL_TIPO_VALOR_BOLSA == "P";

                //    if (tb148.DT_INICI_TIPO_BOLSA != null)
                //    {
                //        txtPeriodoIniDesconto.Text = tb148.DT_INICI_TIPO_BOLSA.Value.ToString("dd/MM/yyyy");
                //        txtPeriodoFimDesconto.Text = tb148.DT_FIM_TIPO_BOLSA.Value.ToString("dd/MM/yyyy");
                //    }
                //    else
                //    {
                //        txtPeriodoIniDesconto.Text = "";
                //        txtPeriodoFimDesconto.Text = "";
                //    }
                //}
                #endregion
            }

        }

        protected void chkAtualiFinan_CheckedChanged(object sender, EventArgs e)
        {
            grdNegociacao.DataSource = null;
            grdNegociacao.DataBind();

            if (chkAtualiFinan.Checked)
            {
                ddlBoleto.Enabled = ddlTipoDesctoMensa.Enabled = txtDesctoMensa.Enabled = true;
                ddlTipoDesctoMensa.SelectedValue = "T";
                #region Habilita os campos
                chkIntegMensaSerie.Enabled =
                chkTipoContrato.Enabled =
                chkValorContratoCalc.Enabled =
                chkAlterValorContr.Enabled =
                chkGeraTotalParce.Enabled =
                chkDataPrimeiraParcela.Enabled =
                chkManterDesconto.Enabled =
                ddlTipoDesctoMensa.Enabled =
                txtDesctoMensa.Enabled =
                ddlBoleto.Enabled =
                ddlDiaVecto.Enabled =
                chkTipoContrato.Enabled = true;
                txtMesIniDesconto.Enabled = false;
                #endregion

                var admUsu = (from adUs in ADMUSUARIO.RetornaTodosRegistros()
                              where adUs.CodUsuario == LoginAuxili.CO_COL
                              select new { adUs.FLA_ALT_BOL_ALU, adUs.FLA_ALT_BOL_ESPE_ALU, adUs.FLA_ALT_PARAM_MAT }).FirstOrDefault();

                if (admUsu != null)
                {
                    //-----------> Valida se o usuário possui permissão para alterar o desconto dado ao aluno.
                    if (admUsu.FLA_ALT_BOL_ALU == "S")
                    {
                        chkManterDesconto.Enabled = true;
                    }
                    else
                    {
                        chkManterDesconto.Enabled =
                        txtValorDescto.Enabled =
                        chkManterDescontoPerc.Enabled =
                        txtPeriodoIniDesconto.Enabled =
                        txtPeriodoFimDesconto.Enabled = false;
                    }

                    //-----------> Valida se o usuário possui permissão para alterar o desconto especial dado ao aluno.
                    if (admUsu.FLA_ALT_BOL_ESPE_ALU == "S")
                    {
                        ddlTipoDesctoMensa.Enabled =
                        txtDesctoMensa.Enabled = true;
                    }
                    else
                    {
                        ddlTipoDesctoMensa.Enabled =
                        txtDesctoMensa.Enabled =
                        txtMesIniDesconto.Enabled = false;
                    }

                    //-----------> Valida se o usuário possui permissão para alterar parâmetros da matrícula
                    if (admUsu.FLA_ALT_PARAM_MAT == "S")
                    {
                        chkTipoContrato.Enabled =
                        chkIntegMensaSerie.Enabled =
                        chkGeraTotalParce.Enabled =
                        chkValorContratoCalc.Enabled =
                        chkAlterValorContr.Enabled =
                        chkDataPrimeiraParcela.Enabled = true;
                    }
                    else
                    {
                        chkTipoContrato.Enabled =
                        chkIntegMensaSerie.Enabled =
                        chkGeraTotalParce.Enabled =
                        chkValorContratoCalc.Enabled =
                        txtValorContratoCalc.Enabled =
                        chkAlterValorContr.Enabled =
                        chkDataPrimeiraParcela.Enabled = false;
                    }
                }
                else
                {
                    chkManterDesconto.Enabled =
                        ddlTpBolsaAlt.Enabled =
                        ddlBolsaAlunoAlt.Enabled =
                        txtValorDescto.Enabled =
                        chkManterDescontoPerc.Enabled =
                        txtPeriodoIniDesconto.Enabled =
                        txtPeriodoFimDesconto.Enabled = false;
                    ddlTipoDesctoMensa.Enabled =
                        txtQtdeMesesDesctoMensa.Enabled =
                        txtDesctoMensa.Enabled =
                        txtMesIniDesconto.Enabled =
                        chkTipoContrato.Enabled =
                        chkIntegMensaSerie.Enabled =
                        chkGeraTotalParce.Enabled =
                        chkValorContratoCalc.Enabled =
                        chkAlterValorContr.Enabled =
                        txtValorContratoCalc.Enabled =
                        chkDataPrimeiraParcela.Enabled = false;
                }
            }
            else
            {
                ddlBoleto.Enabled = ddlTipoDesctoMensa.Enabled = txtQtdeMesesDesctoMensa.Enabled = txtDesctoMensa.Enabled = false;
                txtQtdeMesesDesctoMensa.Text = txtDesctoMensa.Text = txtTotalMensa.Text =
                    txtTotalDesctoBolsa.Text = txtTotalDesctoEspec.Text = txtTotalLiquiContra.Text = "";
                ddlTipoDesctoMensa.SelectedValue = "T";
                #region Desabilita os campos
                ddlTipoContrato.Enabled =
                    chkIntegMensaSerie.Enabled =
                    chkTipoContrato.Enabled =
                    ddlTipoContrato.Enabled =
                    chkIntegMensaSerie.Enabled =
                    chkValorContratoCalc.Enabled =
                    txtValorContratoCalc.Enabled =
                    chkAlterValorContr.Enabled =
                    ddlValorContratoCalc.Enabled =
                    chkGeraTotalParce.Enabled =
                    txtQtdeParcelas.Enabled =
                    RequiredFieldValidator6.Enabled =
                    chkDataPrimeiraParcela.Enabled =
                    txtDtPrimeiraParcela.Enabled =
                    txtValorPrimParce.Enabled =
                    chkManterDesconto.Enabled =
                    ddlTpBolsaAlt.Enabled =
                    ddlBolsaAlunoAlt.Enabled =
                    txtValorDescto.Enabled =
                    chkManterDescontoPerc.Enabled =
                    txtPeriodoIniDesconto.Enabled =
                    txtPeriodoFimDesconto.Enabled =
                    ddlTipoDesctoMensa.Enabled =
                    txtQtdeMesesDesctoMensa.Enabled =
                    txtDesctoMensa.Enabled =
                    txtMesIniDesconto.Enabled =
                    ddlBoleto.Enabled =
                    ddlDiaVecto.Enabled =
                    chkTipoContrato.Enabled = false;
                #endregion
            }
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(this.ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(this.ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(this.ddlTurma.SelectedValue) : 0;

            if (turma != 0)
            {
                string strTurno = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                   where tb06.CO_CUR == serie && tb06.CO_MODU_CUR == modalidade && tb06.CO_TUR == turma
                                   select new { tb06.CO_PERI_TUR }).FirstOrDefault().CO_PERI_TUR;

                if (strTurno == "M")
                    this.txtTurno.Text = "MATUTINO";
                else if (strTurno == "N")
                    this.txtTurno.Text = "NOTURNO";
                else
                    this.txtTurno.Text = "VESPERTINO";
            }
        }

        protected void ddlUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(this.ddlUnidade.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(this.ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(this.ddlSerieCurso.SelectedValue) : 0;
            string strSerieRef = TB01_CURSO.RetornaTodosRegistros().Where(p => p.CO_CUR.Equals(serie)).FirstOrDefault().CO_SERIE_REFER;

            if (strSerieRef != "")
            {
                var tb01 = TB01_CURSO.RetornaTodosRegistros().Where(s => s.CO_MODU_CUR == modalidade && s.CO_EMP == coEmp).FirstOrDefault();

                if (tb01 != null)
                {
                    this.ddlSerieCurso.Items.Clear();
                    this.ddlSerieCurso.Items.Insert(0, new ListItem(tb01.NO_CUR, tb01.CO_CUR.ToString()));

                    this.ddlTurma.Items.Clear();
                    this.ddlTurma.DataSource = (from tb06 in TB06_TURMAS.RetornaTodosRegistros()
                                                where tb06.CO_MODU_CUR == modalidade && tb06.CO_EMP == coEmp && tb06.CO_CUR == tb01.CO_CUR
                                                select new { tb06.TB129_CADTURMAS.CO_SIGLA_TURMA, tb06.CO_TUR }).OrderBy(t => t.CO_SIGLA_TURMA);

                    this.ddlTurma.DataTextField = "CO_SIGLA_TURMA";
                    this.ddlTurma.DataValueField = "CO_TUR";
                    this.ddlTurma.DataBind();

                    if (this.ddlTurma.SelectedValue != "")
                    {
                        int turma = int.Parse(this.ddlTurma.SelectedValue);

                        string strTurno = TB06_TURMAS.RetornaTodosRegistros().Where(t => t.CO_CUR == tb01.CO_CUR && t.CO_MODU_CUR == modalidade && t.CO_TUR == turma).FirstOrDefault().CO_PERI_TUR;

                        if (strTurno == "M")
                            this.txtTurno.Text = "MANHÃ";
                        else if (strTurno == "N")
                            this.txtTurno.Text = "NOITE";
                        else
                            this.txtTurno.Text = "TARDE";
                    }
                }
                else
                {
                    this.ddlUnidade.SelectedValue = TB01_CURSO.RetornaTodosRegistros().Where(c => c.CO_CUR == serie).FirstOrDefault().CO_EMP.ToString();
                    AuxiliPagina.EnvioMensagemErro(this, "Unidade escolhida não possui série cadastrada.");
                }
            }
        }

        private List<TB43_GRD_CURSO> GradeSerie(int serie)
        {
            string coAnoGrade = DateTime.Now.Year.ToString();
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP;

            return TB43_GRD_CURSO.RetornaTodosRegistros().Where(g => g.CO_CUR == serie && g.CO_ANO_GRADE == coAnoGrade && g.CO_EMP == coEmp).ToList<TB43_GRD_CURSO>();
        }

        protected void lnkMenAlu_Click(object sender, EventArgs e)
        {
            int serieCu = int.Parse(ddlSerieCurso.SelectedValue);

            var res = (from tb01 in TB01_CURSO.RetornaTodosRegistros()
                       where tb01.CO_CUR == serieCu
                       select new
                       {
                           tb01.NU_MDV
                       }).FirstOrDefault();

            ddlDiaVecto.SelectedValue = res.NU_MDV.ToString();

            if (chkAtualiFinan.Checked)
            {
                if (grdNegociacao.Rows.Count == 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "É necessário criar a grid de mensalidades.");
                    return;
                }
            }

            lnkSucMenEscAlu.Visible = true;
            chkMenEscAlu.Enabled = false;

            chkMenEscAlu.Enabled = chkAtualiFinan.Enabled = ddlBoleto.Enabled = ddlTipoDesctoMensa.Enabled = lnkMenAlu.Enabled =
                txtQtdeMesesDesctoMensa.Enabled = txtDesctoMensa.Enabled = lnkMontaGridMensa.Enabled = lnkMenAlu.Enabled = false;

            #region Altera o desconto do aluno
            decimal decimalRetorno;
            DateTime dataRetorno;

            int coAlu = this.hdCoAlu.Value != "" ? Convert.ToInt32(this.hdCoAlu.Value) : 0;
            TB07_ALUNO tb07 = TB07_ALUNO.RetornaPeloCoAlu(coAlu);
            if (tb07 != null)
            {
                tb07.TB148_TIPO_BOLSA = this.ddlBolsaAlunoAlt.SelectedValue != "" && this.ddlBolsaAlunoAlt.SelectedValue != "0" ? TB148_TIPO_BOLSA.RetornaPelaChavePrimaria(int.Parse(this.ddlBolsaAlunoAlt.SelectedValue)) : null;
                if (chkManterDescontoPerc.Checked)
                {
                    tb07.NU_PEC_DESBOL = decimal.TryParse(this.txtValorDescto.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
                    tb07.NU_VAL_DESBOL = null;
                }
                else
                {
                    tb07.NU_PEC_DESBOL = null;
                    tb07.NU_VAL_DESBOL = decimal.TryParse(this.txtValorDescto.Text, out decimalRetorno) ? (decimal?)decimalRetorno : null;
                }

                tb07.DT_VENC_BOLSA = DateTime.TryParse(this.txtPeriodoIniDesconto.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
                tb07.DT_VENC_BOLSAF = DateTime.TryParse(this.txtPeriodoFimDesconto.Text, out dataRetorno) ? (DateTime?)dataRetorno : null;
            }
            TB07_ALUNO.SaveOrUpdate(tb07);
            #endregion



        }

        protected void ddlTipoDesctoMensa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoDesctoMensa.SelectedValue == "M")
            {
                txtQtdeMesesDesctoMensa.Enabled = true;
                txtMesIniDesconto.Enabled = true;
            }
            else
            {
                txtQtdeMesesDesctoMensa.Enabled = false;
                txtQtdeMesesDesctoMensa.Text = "";
                txtMesIniDesconto.Enabled = false;
                txtMesIniDesconto.Text = "";
            }
        }

        protected void lnkBolCarne_Click(object sender, EventArgs e)
        {
            if (ddlBoleto.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this, "Não é possível gerar boleto, pois não existe boleto selecionado.");
                return;
            }
            else
            {
                GeraBoleto();
                imgRecMatric.Src = "/Library/IMG/Gestor_IcoImpres.ico";
                lblRecibo.Text = "CONTRATO";
                imgBolCarne.Src = "/Library/IMG/Gestor_IcoImpres.ico";
                lblBoleto.Text = "BOLETO";
                imgEfetiMatric.Src = "/Library/IMG/Gestor_CheckSucess.png";
                lblEfetiMatric.Text = "EFETIVAR";
            }
        }

        protected void lnkRecMatric_Click(object sender, EventArgs e)
        {
            if (Session[SessoesHttp.CodigoMatriculaAluno] == null)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Não é possível imprimir Recibo/Protocolo de Matrícula. Aluno deve ser matriculado.");
                return;
            }

            //--------> Variáveis obrigatórias para gerar o Relatório
            string codAlunoCad;
            int lRetorno;
            string anoRef = txtAno.Text != "" ? DateTime.Now.Year.ToString() : txtAno.Text;
            //--------> Variáveis de parâmetro do Relatório
            int codEmp;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            codEmp = LoginAuxili.CO_EMP;
            codAlunoCad = Session[SessoesHttp.CodigoMatriculaAluno].ToString();

            RptContrato rpt = new RptContrato();
            lRetorno = rpt.InitReport(codEmp, codAlunoCad, "M", anoRef);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";
            HttpContext.Current.Session["ApresentaRelatorio"] = 1;

            //AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
            /*
            if (Session[SessoesHttp.CodigoMatriculaAluno] == null)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Não é possível imprimir Recibo/Protocolo de Matrícula. Aluno deve ser matriculado.");
                return;
            }
//--------> GERAÇÃO DO RELATÓRIO ###
//--------> Variáveis obrigatórias para gerar o Relatório
            string strIDSessao, strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio;
            int lRetorno;

//--------> Variáveis de parâmetro do Relatório
            string strP_CO_EMP, strP_CO_ALU_CAD;

            var varRelatorioWeb = new ChannelFactory<IRelatorioWeb>(new NetTcpBinding(), WRAuxiliares.URLRelatorioWeb);
            IRelatorioWeb lIRelatorioWeb;

            strIDSessao = Session.SessionID.ToString();
            strIdentFunc = WRAuxiliares.IdentFunc;
            strCaminhoRelatorioGerado = HttpRuntime.AppDomainAppPath + "TMP_Relatorios\\" + strIDSessao + "\\";
            strNomeRelatorio = WRAuxiliares.GeraNomeRelatorio("RelComprReserMatric");

//--------> Criação da Pasta
            if (!Directory.Exists(@strCaminhoRelatorioGerado))
                Directory.CreateDirectory(@strCaminhoRelatorioGerado);

            strP_CO_EMP = LoginAuxili.CO_EMP.ToString();
            strP_CO_ALU_CAD = Session[SessoesHttp.CodigoMatriculaAluno].ToString();

            lIRelatorioWeb = varRelatorioWeb.CreateChannel();

            lRetorno = lIRelatorioWeb.RelComprMatric(strIdentFunc, strCaminhoRelatorioGerado, strNomeRelatorio, strP_CO_EMP, strP_CO_ALU_CAD);

            string strURL = WRAuxiliares.RetornaCaminhoRelatorioPDF("/TMP_Relatorios/" + strIDSessao + "/") + strNomeRelatorio;
            Session["URLRelatorio"] = strURL;
            Session["ApresentaRelatorio"] = "1";

            AuxiliPagina.EnvioMensagemSucesso(this.Page, "Comprovante de matrícula gerado com sucesso.");

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);

            varRelatorioWeb.Close();*/
        }

        protected void lnkFichaMatric_Click(object sender, EventArgs e)
        {
            if (Session[SessoesHttp.CodigoMatriculaAluno] == null || Session[SessoesHttp.CodigoMatriculaAluno] == "")
            {
                AuxiliPagina.EnvioMensagemErro(this, "Não é possível imprimir a ficha de Matrícula. Aluno deve ser matriculado.");
                return;
            }

            //--------> Variáveis obrigatórias para gerar o Relatório
            string codAlunoCad;
            int lRetorno;

            //--------> Variáveis de parâmetro do Relatório
            string codEmp;

            //--------> Lança nas variáveis dos parâmetros os valores escolhidos no formulário
            codEmp = LoginAuxili.CO_EMP.ToString();
            codAlunoCad = Session[Resources.SessoesHttp.CodigoMatriculaAluno].ToString();
            if (codAlunoCad != "")
            {
                string infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
                string parametros = "";
                //C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2111_PreMatriculaAluno.RptFichaMatricAluno rpt = new C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2111_PreMatriculaAluno.RptFichaMatricAluno();
                RptFichaMatricAluno rpt = new RptFichaMatricAluno();
                lRetorno = rpt.InitReport(parametros, LoginAuxili.CO_EMP, infos, codEmp, codAlunoCad, txtAno.Text, ddlSituMatAluno.SelectedValue == "X" ? "R" : "A");
                Session["Report"] = rpt;
                Session["URLRelatorio"] = "/GeducReportViewer.aspx";
                HttpContext.Current.Session["ApresentaRelatorio"] = 1;
            }
        }

        protected void ddlBolsaAlunoAlt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBolsaAlunoAlt.SelectedValue == "")
            {
                txtValorDescto.Text = txtPeriodoIniDesconto.Text = txtPeriodoFimDesconto.Text = "";
                txtValorDescto.Enabled = chkManterDescontoPerc.Checked = chkManterDescontoPerc.Enabled = txtPeriodoIniDesconto.Enabled = txtPeriodoFimDesconto.Enabled = false;
            }
            else
            {
                if (ddlBolsaAlunoAlt.SelectedValue == "0")
                {
                    txtValorDescto.Enabled =
                    chkManterDescontoPerc.Enabled =
                    txtPeriodoIniDesconto.Enabled =
                    txtPeriodoFimDesconto.Enabled = true;

                    txtValorDescto.Text = "";
                    txtPeriodoIniDesconto.Text = "";
                    txtPeriodoFimDesconto.Text = "";
                    chkManterDescontoPerc.Checked = true;
                }
                else
                {
                    txtPeriodoIniDesconto.Enabled = txtPeriodoFimDesconto.Enabled = true;
                    txtValorDescto.Enabled = chkManterDescontoPerc.Enabled = false;
                    int coBolsa = int.Parse(ddlBolsaAlunoAlt.SelectedValue);

                    var tb148 = (from iTb148 in TB148_TIPO_BOLSA.RetornaTodosRegistros()
                                 where iTb148.CO_TIPO_BOLSA == coBolsa
                                 select new { iTb148.VL_TIPO_BOLSA, iTb148.FL_TIPO_VALOR_BOLSA, iTb148.DT_INICI_TIPO_BOLSA, iTb148.DT_FIM_TIPO_BOLSA }).FirstOrDefault();

                    if (tb148 != null)
                    {
                        txtValorDescto.Text = tb148.VL_TIPO_BOLSA != null ? String.Format("{0:N}", tb148.VL_TIPO_BOLSA) : "";
                        chkManterDescontoPerc.Checked = tb148.FL_TIPO_VALOR_BOLSA == "P";

                        if (tb148.DT_INICI_TIPO_BOLSA != null)
                        {
                            txtPeriodoIniDesconto.Text = tb148.DT_INICI_TIPO_BOLSA.Value.ToString("dd/MM/yyyy");
                            txtPeriodoFimDesconto.Text = tb148.DT_FIM_TIPO_BOLSA.Value.ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            txtPeriodoIniDesconto.Text = "";
                            txtPeriodoFimDesconto.Text = "";
                        }
                    }
                }
            }
        }

        protected void ddlTpBolsaAlt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTpBolsaAlt.SelectedValue == "")
            {
                txtValorDescto.Text = "";
                txtPeriodoFimDesconto.Text = "";
                txtPeriodoFimDesconto.Text = "";

                txtValorDescto.Enabled =
                txtPeriodoFimDesconto.Enabled =
                ddlBolsaAlunoAlt.Enabled =
                txtPeriodoFimDesconto.Enabled = false;

                CarregaBolsasAlt();
            }
            else
            {
                CarregaBolsasAlt();
            }
        }

        /// <summary>
        /// Método que carrega a grid de mensalidades
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkMontaGridMensa_Click(object sender, EventArgs e)
        {
            int parcelas = 12;

            if (ddlSerieCurso.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this, "Não é possível gerar grid de mensalidade porque série não foi selecionada.");
                return;
            }

            if (!chkAtualiFinan.Checked)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Não é possível gerar grid de mensalidade porque não será atualizado no financeiro.");
                return;
            }

            if (chkDataPrimeiraParcela.Checked)
            {
                if (txtValorPrimParce.Text == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Valor da primeira parcela deve ser informado.");
                    return;
                }
                else
                {
                    if (Decimal.Parse(txtValorPrimParce.Text) == 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Valor 1ª Parcela não pode ser zero ou negativo.");
                        return;
                    }
                }
            }

            if (chkAlterValorContr.Checked)
            {
                if (txtValorContratoCalc.Text == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Valor de Contrato deve ser informado.");
                    return;
                }
                else
                {
                    if (Decimal.Parse(txtValorContratoCalc.Text) == 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Valor Editado de Contrato não pode ser zero ou negativo.");
                        return;
                    }
                }
            }

            if (chkManterDesconto.Checked)
            {
                if (txtPeriodoIniDesconto.Text != "" && txtPeriodoFimDesconto.Text != "")
                {
                    DateTime validaData;
                    if (DateTime.TryParse(txtPeriodoIniDesconto.Text, out validaData) && DateTime.TryParse(txtPeriodoFimDesconto.Text, out validaData))
                    {
                        if (DateTime.Parse(txtPeriodoIniDesconto.Text) > DateTime.Parse(txtPeriodoFimDesconto.Text))
                        {
                            AuxiliPagina.EnvioMensagemErro(this, "Data de fim de período inválida");
                            return;
                        }
                    }
                }
            }

            if (txtDesctoMensa.Text != "")
            {
                if (Decimal.Parse(txtDesctoMensa.Text) == 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Valor de Desconto Especial não pode ser zero ou negativo.");
                    return;
                }
            }

            if ((ddlTipoDesctoMensa.SelectedValue == "M") && (txtDesctoMensa.Text != ""))
            {
                if (txtQtdeMesesDesctoMensa.Text == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Quantidade de meses de desconto especial deve ser informada.");
                    return;
                }

                if (txtMesIniDesconto.Text == "")
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Número da Parcela de início de desconto deve ser informada.");
                    return;
                }

                if (int.Parse(txtQtdeMesesDesctoMensa.Text) == 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Não é possível gerar grid de mensalidade porque mês informado igual a zero.");
                    return;
                }

                if (int.Parse(txtMesIniDesconto.Text) == 0)
                {
                    AuxiliPagina.EnvioMensagemErro(this, "Não é possível gerar grid de mensalidade porque número da parcela de início informado igual a zero.");
                    return;
                }
            }

            MontaGridNegociacao();
        }

        protected void lnkCarteira_Click(object sender, EventArgs e)
        {
            int lRetorno, strSerie;

            int coEmp, coModu, coCur, coTur, coAlu = 0;
            string infos, parametros, coAno, deModu, noEmp, noCur, noTur, noAlu, codAlu, dtValidade = "";

            coAno = DateTime.Now.Year.ToString(); //ddlAno.SelectedValue;
            coEmp = LoginAuxili.CO_EMP;
            coModu = int.Parse(ddlModalidade.SelectedValue);
            coCur = int.Parse(ddlSerieCurso.SelectedValue);
            coTur = int.Parse(ddlTurma.SelectedValue);
            codAlu = Session[Resources.SessoesHttp.CodigoMatriculaAluno].ToString();
            dtValidade = "30/03/" + (DateTime.Now.Year + 1);

            coAlu = TB08_MATRCUR.RetornaTodosRegistros().Where(w => w.CO_ALU_CAD == codAlu).FirstOrDefault().CO_ALU;

            noEmp = (coEmp != 0 ? TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp).NO_FANTAS_EMP : "TODOS");
            deModu = (coModu != 0 ? TB44_MODULO.RetornaPelaChavePrimaria(coModu).CO_SIGLA_MODU_CUR : "TODOS");
            noCur = (coCur != 0 && coModu != 0 && coEmp != 0 ? TB01_CURSO.RetornaPelaChavePrimaria(coEmp, coModu, coCur).CO_SIGL_CUR : "TODOS");
            noTur = (coTur != 0 ? TB129_CADTURMAS.RetornaPelaChavePrimaria(coTur).CO_SIGLA_TURMA : "TODOS");
            noAlu = (coAlu != 0 && coEmp != 0 ? TB07_ALUNO.RetornaPelaChavePrimaria(coAlu, coEmp).NO_ALU : "TODOS");

            int strNIRE;
            strSerie = int.Parse(ddlSerieCurso.SelectedValue);
            //strNIRE = txtNireAluno.Text != "" ? int.Parse(txtNireAluno.Text.Replace(".", "").Replace("-", "")) : 0;

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            parametros = "(Ano: " + coAno + " - Unidade: " + noEmp + " - Modalidade: " + deModu + " - Série/Curso: " + noCur + " - Turma: " + noTur + " - Aluno: " + noAlu + ")";

            //RptAlunoCarteira fpcb = new RptAlunoCarteira();
            RptCarteiraEstudantil fpcb = new RptCarteiraEstudantil();
            //lRetorno = fpcb.InitReport(strSerie, strNIRE);
            lRetorno = fpcb.InitReport(infos, parametros, coAno, coEmp, coEmp, coModu, coCur, coTur, coAlu, dtValidade);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";
            HttpContext.Current.Session["ApresentaRelatorio"] = 1;
            //AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        protected void chkValorContratoCalc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkValorContratoCalc.Checked)
            {
                ddlValorContratoCalc.Enabled = true;
            }
            else
            {
                ddlValorContratoCalc.Enabled = false;
            }
        }

        protected void chkAlterValorContr_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAlterValorContr.Checked)
            {
                txtValorContratoCalc.Enabled = true;
            }
            else
            {
                txtValorContratoCalc.Enabled = false;
                txtValorContratoCalc.Text = "";

                int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
                int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
                var varSer = TB01_CURSO.RetornaPelaChavePrimaria(ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP, modalidade, serie);
                string turnoTurma = TB06_TURMAS.RetornaPeloCodigo(int.Parse(ddlTurma.SelectedValue)).CO_PERI_TUR;

                //-----------> Valida o turno do curso e coloca o valor do contrato no campo de valor do contrato de acordo com o turno da turma.
                switch (turnoTurma)
                {
                    case "M":
                        //--------> Turma Matutina
                        #region Turno Matutino
                        switch (ddlTipoContrato.SelectedValue)
                        {
                            case "P":
                                if (varSer.VL_CONTMAN_APRAZ == null)
                                {
                                    txtValorContratoCalc.Text = "";
                                    return;
                                }
                                //---------> A Prazo
                                txtValorContratoCalc.Text = varSer.VL_CONTMAN_APRAZ.ToString();
                                break;
                            case "V":
                                if (varSer.VL_CONTMAN_AVIST == null)
                                {
                                    txtValorContratoCalc.Text = "";
                                    return;
                                }
                                //---------> A Vista
                                txtValorContratoCalc.Text = varSer.VL_CONTMAN_AVIST.ToString();
                                break;
                        }
                        break;
                        #endregion
                    case "V":
                        //--------> Turma Vespertina
                        #region Turno Vespertino
                        switch (ddlTipoContrato.SelectedValue)
                        {
                            case "P":
                                if (varSer.VL_CONTTAR_APRAZ == null)
                                {
                                    txtValorContratoCalc.Text = "";
                                    return;
                                }
                                //---------> A Prazo
                                txtValorContratoCalc.Text = varSer.VL_CONTTAR_APRAZ.ToString();
                                break;
                            case "V":
                                if (varSer.VL_CONTTAR_AVIST == null)
                                {
                                    txtValorContratoCalc.Text = "";
                                    return;
                                }
                                //---------> A Vista
                                txtValorContratoCalc.Text = varSer.VL_CONTTAR_AVIST.ToString();
                                break;
                        }
                        break;
                        #endregion
                    case "N":
                        //--------> Turma Noturna
                        #region Turno Noturno
                        switch (ddlTipoContrato.SelectedValue)
                        {
                            case "P":
                                if (varSer.VL_CONTNOI_APRAZ == null)
                                {
                                    txtValorContratoCalc.Text = "";
                                    return;
                                }
                                //---------> A Prazo
                                txtValorContratoCalc.Text = varSer.VL_CONTNOI_APRAZ.ToString();
                                break;
                            case "V":
                                if (varSer.VL_CONTNOI_AVIST == null)
                                {
                                    txtValorContratoCalc.Text = "";
                                    return;
                                }
                                //---------> A Vista
                                txtValorContratoCalc.Text = varSer.VL_CONTNOI_AVIST.ToString();
                                break;
                        }
                        break;
                        #endregion
                }
            }
        }

        protected void chkDataPrimeiraParcela_CheckedChange(object sender, EventArgs e)
        {
            if (chkDataPrimeiraParcela.Checked)
            {
                txtDtPrimeiraParcela.Enabled = txtValorPrimParce.Enabled = true;
            }
            else
            {
                txtDtPrimeiraParcela.Enabled = txtValorPrimParce.Enabled = false;
            }
        }

        protected void chkTipoContrato_CheckedChange(object sender, EventArgs e)
        {
            if (chkTipoContrato.Checked)
            {
                ddlTipoContrato.Enabled =
                    chkIntegMensaSerie.Enabled = true;
            }
            else
            {
                ddlTipoContrato.SelectedValue = "P";
                chkIntegMensaSerie.Checked = false;

                int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
                int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
                var varSer = TB01_CURSO.RetornaPelaChavePrimaria(ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP, modalidade, serie);
                string turnoTurma = TB06_TURMAS.RetornaPeloCodigo(int.Parse(ddlTurma.SelectedValue)).CO_PERI_TUR;

                txtQtdeParcelas.Text = varSer.NU_QUANT_MESES.ToString();

                //-----------> Valida o turno do curso e coloca o valor do contrato no campo de valor do contrato de acordo com o turno da turma.
                switch (turnoTurma)
                {
                    case "M":
                        //--------> Turma Matutina
                        #region Turno Matutino
                        switch (ddlTipoContrato.SelectedValue)
                        {
                            case "P":
                                if (varSer.VL_CONTMAN_APRAZ == null)
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "A Modalidade de Ensino \"" + varSer.NO_CUR + "\" não possui valor a prazo para o turno Matutino.");
                                    chkIntegMensaSerie.Checked = false;
                                    return;
                                }
                                //---------> A Prazo
                                txtValorContratoCalc.Text = varSer.VL_CONTMAN_APRAZ.ToString();
                                break;
                            case "V":
                                if (varSer.VL_CONTMAN_AVIST == null)
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "A Modalidade de Ensino \"" + varSer.NO_CUR + "\" não possui valor a vista para o turno Matutino.");
                                    chkIntegMensaSerie.Checked = false;
                                    return;
                                }
                                //---------> A Vista
                                txtValorContratoCalc.Text = varSer.VL_CONTMAN_AVIST.ToString();
                                break;
                        }
                        break;
                        #endregion
                    case "V":
                        //--------> Turma Vespertina
                        #region Turno Vespertino
                        switch (ddlTipoContrato.SelectedValue)
                        {
                            case "P":
                                if (varSer.VL_CONTTAR_APRAZ == null)
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "A Modalidade de Ensino \"" + varSer.NO_CUR + "\" não possui valor a prazo para o turno Vespertino.");
                                    chkIntegMensaSerie.Checked = false;
                                    return;
                                }
                                //---------> A Prazo
                                txtValorContratoCalc.Text = varSer.VL_CONTTAR_APRAZ.ToString();
                                break;
                            case "V":
                                if (varSer.VL_CONTTAR_AVIST == null)
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "A Modalidade de Ensino \"" + varSer.NO_CUR + "\" não possui valor a vista para o turno Vespertino.");
                                    chkIntegMensaSerie.Checked = false;
                                    return;
                                }
                                //---------> A Vista
                                txtValorContratoCalc.Text = varSer.VL_CONTTAR_AVIST.ToString();
                                break;
                        }
                        break;
                        #endregion
                    case "N":
                        //--------> Turma Noturna
                        #region Turno Noturno
                        switch (ddlTipoContrato.SelectedValue)
                        {
                            case "P":
                                if (varSer.VL_CONTNOI_APRAZ == null)
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "A Modalidade de Ensino \"" + varSer.NO_CUR + "\" não possui valor a prazo para o turno Noturno.");
                                    chkIntegMensaSerie.Checked = false;
                                    return;
                                }
                                //---------> A Prazo
                                txtValorContratoCalc.Text = varSer.VL_CONTNOI_APRAZ.ToString();
                                break;
                            case "V":
                                if (varSer.VL_CONTNOI_AVIST == null)
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "A Modalidade de Ensino \"" + varSer.NO_CUR + "\" não possui valor a vista para o turno Noturno.");
                                    chkIntegMensaSerie.Checked = false;
                                    return;
                                }
                                //---------> A Vista
                                txtValorContratoCalc.Text = varSer.VL_CONTNOI_AVIST.ToString();
                                break;
                        }
                        break;
                        #endregion
                }

                ddlTipoContrato.Enabled =
                    chkIntegMensaSerie.Enabled = false;
            }
        }

        protected void ddlTipoContrato_SelectedIndexChanged(object sender, EventArgs e)
        {
            //-----------> Desabilita a funcionalidade de primeir parcela para contrato A Vista
            if (ddlTipoContrato.SelectedValue == "V")
            {
                chkDataPrimeiraParcela.Enabled = false;
                txtValorPrimParce.Text = "";
                txtQtdeParcelas.Text = "1";
                chkGeraTotalParce.Enabled = false;
            }

            //-----------> Pega o Curso
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            var varSer = TB01_CURSO.RetornaPelaChavePrimaria(ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP, modalidade, serie);
            string turnoTurma = TB06_TURMAS.RetornaPeloCodigo(int.Parse(ddlTurma.SelectedValue)).CO_PERI_TUR;

            if (!chkIntegMensaSerie.Checked)
            {
                //-----------> Valida o turno do curso e coloca o valor do contrato no campo de valor do contrato de acordo com o turno da turma.
                switch (turnoTurma)
                {
                    case "M":
                        //--------> Turma Matutina
                        #region Turno Matutino
                        switch (ddlTipoContrato.SelectedValue)
                        {
                            case "P":
                                if (varSer.VL_CONTMAN_APRAZ == null)
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "A Modalidade de Ensino \"" + varSer.NO_CUR + "\" não possui valor a prazo para o turno Matutino.");
                                    chkIntegMensaSerie.Checked = false;
                                    return;
                                }
                                //---------> A Prazo
                                txtValorContratoCalc.Text = varSer.VL_CONTMAN_APRAZ.ToString();
                                break;
                            case "V":
                                if (varSer.VL_CONTMAN_AVIST == null)
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "A Modalidade de Ensino \"" + varSer.NO_CUR + "\" não possui valor a vista para o turno Matutino.");
                                    chkIntegMensaSerie.Checked = false;
                                    return;
                                }
                                //---------> A Vista
                                txtValorContratoCalc.Text = varSer.VL_CONTMAN_AVIST.ToString();
                                txtValorPrimParce.Text = varSer.VL_CONTMAN_AVIST.ToString();
                                break;
                        }
                        break;
                        #endregion
                    case "V":
                        //--------> Turma Vespertina
                        #region Turno Vespertino
                        switch (ddlTipoContrato.SelectedValue)
                        {
                            case "P":
                                if (varSer.VL_CONTTAR_APRAZ == null)
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "A Modalidade de Ensino \"" + varSer.NO_CUR + "\" não possui valor a prazo para o turno Vespertino.");
                                    chkIntegMensaSerie.Checked = false;
                                    return;
                                }
                                //---------> A Prazo
                                txtValorContratoCalc.Text = varSer.VL_CONTTAR_APRAZ.ToString();
                                txtValorPrimParce.Text = varSer.VL_CONTTAR_APRAZ.ToString();
                                break;
                            case "V":
                                if (varSer.VL_CONTTAR_AVIST == null)
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "A Modalidade de Ensino \"" + varSer.NO_CUR + "\" não possui valor a vista para o turno Vespertino.");
                                    chkIntegMensaSerie.Checked = false;
                                    return;
                                }
                                //---------> A Vista
                                txtValorContratoCalc.Text = varSer.VL_CONTTAR_AVIST.ToString();
                                break;
                        }
                        break;
                        #endregion
                    case "N":
                        //--------> Turma Noturna
                        #region Turno Noturno
                        switch (ddlTipoContrato.SelectedValue)
                        {
                            case "P":
                                if (varSer.VL_CONTNOI_APRAZ == null)
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "A Modalidade de Ensino \"" + varSer.NO_CUR + "\" não possui valor a prazo para o turno Noturno.");
                                    chkIntegMensaSerie.Checked = false;
                                    return;
                                }
                                //---------> A Prazo
                                txtValorContratoCalc.Text = varSer.VL_CONTNOI_APRAZ.ToString();
                                break;
                            case "V":
                                if (varSer.VL_CONTNOI_AVIST == null)
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "A Modalidade de Ensino \"" + varSer.NO_CUR + "\" não possui valor a vista para o turno Noturno.");
                                    chkIntegMensaSerie.Checked = false;
                                    return;
                                }
                                //---------> A Vista
                                txtValorContratoCalc.Text = varSer.VL_CONTNOI_AVIST.ToString();
                                txtValorPrimParce.Text = varSer.VL_CONTNOI_AVIST.ToString();
                                break;
                        }
                        break;
                        #endregion
                }
            }
            else
            {
                //-----------> Retorna o valor integral do curso
                switch (ddlTipoContrato.SelectedValue)
                {
                    case "P":
                        if (String.IsNullOrEmpty(varSer.VL_CONTINT_APRAZ.ToString()))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "A Modalidade de Ensino \"" + varSer.NO_CUR + "\" não possui valor integral a prazo.");
                            chkIntegMensaSerie.Checked = false;
                            return;
                        }
                        //---------> A Prazo
                        txtValorContratoCalc.Text = varSer.VL_CONTINT_APRAZ.ToString();
                        break;
                    case "V":
                        if (String.IsNullOrEmpty(varSer.VL_CONTINT_AVIST.ToString()))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "A Modalidade de Ensino \"" + varSer.NO_CUR + "\" não possui valor integral a vista.");
                            chkIntegMensaSerie.Checked = false;
                            return;
                        }
                        //---------> A Vista
                        txtValorContratoCalc.Text = varSer.VL_CONTINT_AVIST.ToString();
                        txtValorPrimParce.Text = varSer.VL_CONTINT_AVIST.ToString();
                        break;
                }
            }
        }

        protected void chkIntegMensaSerie_CheckedChange(object sender, EventArgs e)
        {
            //-----------> Pega o Curso
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            var varSer = TB01_CURSO.RetornaPelaChavePrimaria(ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP, modalidade, serie);
            string turnoTurma = TB06_TURMAS.RetornaPeloCodigo(int.Parse(ddlTurma.SelectedValue)).CO_PERI_TUR;

            if (chkIntegMensaSerie.Checked)
            {
                //-----------> Retorna o valor integral do curso
                switch (ddlTipoContrato.SelectedValue)
                {
                    case "P":
                        if (String.IsNullOrEmpty(varSer.VL_CONTINT_APRAZ.ToString()))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "A Modalidade de Ensino \"" + varSer.NO_CUR + "\" não possui valor integral a prazo.");
                            chkIntegMensaSerie.Checked = false;
                            return;
                        }
                        //---------> A Prazo
                        txtValorContratoCalc.Text = varSer.VL_CONTINT_APRAZ.ToString();
                        break;
                    case "V":
                        if (String.IsNullOrEmpty(varSer.VL_CONTINT_AVIST.ToString()))
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "A Modalidade de Ensino \"" + varSer.NO_CUR + "\" não possui valor integral a vista.");
                            chkIntegMensaSerie.Checked = false;
                            return;
                        }
                        //---------> A Vista
                        txtValorContratoCalc.Text = varSer.VL_CONTINT_AVIST.ToString();
                        break;
                }
            }
            else
            {
                //-----------> Valida o turno do curso e coloca o valor do contrato no campo de valor do contrato de acordo com o turno da turma.
                switch (turnoTurma)
                {
                    case "M":
                        //--------> Turma Matutina
                        #region Turno Matutino
                        switch (ddlTipoContrato.SelectedValue)
                        {
                            case "P":
                                if (varSer.VL_CONTMAN_APRAZ == null)
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "A Modalidade de Ensino \"" + varSer.NO_CUR + "\" não possui valor a prazo para o turno Matutino.");
                                    chkIntegMensaSerie.Checked = false;
                                    return;
                                }
                                //---------> A Prazo
                                txtValorContratoCalc.Text = varSer.VL_CONTMAN_APRAZ.ToString();
                                break;
                            case "V":
                                if (varSer.VL_CONTMAN_AVIST == null)
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "A Modalidade de Ensino \"" + varSer.NO_CUR + "\" não possui valor a vista para o turno Matutino.");
                                    chkIntegMensaSerie.Checked = false;
                                    return;
                                }
                                //---------> A Vista
                                txtValorContratoCalc.Text = varSer.VL_CONTMAN_AVIST.ToString();
                                break;
                        }
                        break;
                        #endregion
                    case "V":
                        //--------> Turma Vespertina
                        #region Turno Vespertino
                        switch (ddlTipoContrato.SelectedValue)
                        {
                            case "P":
                                if (varSer.VL_CONTTAR_APRAZ == null)
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "A Modalidade de Ensino \"" + varSer.NO_CUR + "\" não possui valor a prazo para o turno Vespertino.");
                                    chkIntegMensaSerie.Checked = false;
                                    return;
                                }
                                //---------> A Prazo
                                txtValorContratoCalc.Text = varSer.VL_CONTTAR_APRAZ.ToString();
                                break;
                            case "V":
                                if (varSer.VL_CONTTAR_AVIST == null)
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "A Modalidade de Ensino \"" + varSer.NO_CUR + "\" não possui valor a vista para o turno Vespertino.");
                                    chkIntegMensaSerie.Checked = false;
                                    return;
                                }
                                //---------> A Vista
                                txtValorContratoCalc.Text = varSer.VL_CONTTAR_AVIST.ToString();
                                break;
                        }
                        break;
                        #endregion
                    case "N":
                        //--------> Turma Noturna
                        #region Turno Noturno
                        switch (ddlTipoContrato.SelectedValue)
                        {
                            case "P":
                                if (varSer.VL_CONTNOI_APRAZ == null)
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "A Modalidade de Ensino \"" + varSer.NO_CUR + "\" não possui valor a prazo para o turno Noturno.");
                                    chkIntegMensaSerie.Checked = false;
                                    return;
                                }
                                //---------> A Prazo
                                txtValorContratoCalc.Text = varSer.VL_CONTNOI_APRAZ.ToString();
                                break;
                            case "V":
                                if (varSer.VL_CONTNOI_AVIST == null)
                                {
                                    AuxiliPagina.EnvioMensagemErro(this.Page, "A Modalidade de Ensino \"" + varSer.NO_CUR + "\" não possui valor a vista para o turno Noturno.");
                                    chkIntegMensaSerie.Checked = false;
                                    return;
                                }
                                //---------> A Vista
                                txtValorContratoCalc.Text = varSer.VL_CONTNOI_AVIST.ToString();
                                break;
                        }
                        break;
                        #endregion
                }
            }
        }

        protected void txtQtdeParcelas_TextChanged(object sender, EventArgs e)
        {
            //-----------> Pega o Curso
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            var varSer = TB01_CURSO.RetornaPelaChavePrimaria(ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : LoginAuxili.CO_EMP, modalidade, serie);

            if (ddlTipoContrato.SelectedValue == "P")
            {
                if (txtQtdeParcelas.Text != "")
                {
                    if (int.Parse(txtQtdeParcelas.Text) <= 1)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "A quantidade de parcelas a prazo deve ser maior que 1.");
                        return;
                    }

                    if (int.Parse(txtQtdeParcelas.Text) > varSer.NU_QUANT_MESES)
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "A quantidade de parcelas a prazo não pode ser maior que quantidade de parcelas do contrato.");
                        return;
                    }
                }
            }
        }

        protected void lnkNovo_Click(object sender, EventArgs e)
        {
            hdCoAlu.Value = "";
            hdCoRes.Value = "";
            hdfCPFRespRes.Value = "";
            Session.Remove("URLRelatorio");
            Session.Remove("ApresentaRelatorio");
            Session.Remove(SessoesHttp.CodigoMatriculaAluno);
            Session.Remove(SessoesHttp.BoletoBancarioCorrente);
            Session.Remove("Report");
            AuxiliPagina.RedirecionaParaPaginaCadastro();
        }

        #endregion
    }
}
