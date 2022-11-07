//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: DADOS OPERACIONAIS DE CONTRATO
// OBJETIVO: DEPARTAMENTO INSTITUCIONAL.
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//------------------------------------------------------------------------------
//  DATA      |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// -----------+----------------------------+-------------------------------------
// 09/12/2016 | Alex Ribeiro               | Foram adicionadas referências para tipo de departamento e classificação de risco

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
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
using C2BR.GestorEducacao.UI.App_Masters;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0100_DadosOperContrato.F0103_DepartamentoInstitucional
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
            if (!IsPostBack)
            {
                CarregaTipo();
                VerificaTipo();
                CarregaAcomodacao();
                CarregaCtrlInter();
                CarregaEsteriliza();
                CarregaHigiene();
                CarregaLavanderia();
                CarregaManutencao();
                CarregaSeguranca();
            }
            VerificaTipo();

        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TB14_DEPTO tb14 = RetornaEntidade();

            if (tb14 == null)
            {
                tb14 = new TB14_DEPTO();
                tb14.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
            }

            tb14.CO_SIGLA_DEPTO = txtSiglaDepto.Text.Trim().ToUpper();
            tb14.NO_DEPTO = txtNomeDepto.Text;
            tb14.DE_DEPTO = txtDescricao.Text;
            tb14.NU_TEL_DEPTO = txtTelefone.Text.Replace("(","").Replace(")","").Replace("-","").Replace(" ","");
            tb14.NU_RAMAL_DEPTO = txtRamal.Text;
            tb14.CO_SITUA_DEPTO = ddlSitua.SelectedValue;
            tb14.CO_TIPO_DEPTO = ddlTipo.SelectedValue != "" ? int.Parse(ddlTipo.SelectedValue) : (int?)null;
            tb14.CO_CLASS_RISCO = ddlRisco.SelectedValue != "" ? int.Parse(ddlRisco.SelectedValue) : (int?)null;
            tb14.DT_SITUA_DEPTO = DateTime.Now;
            if (LoginAuxili.CO_TIPO_UNID.Equals("PGS"))
            {
                tb14.FL_RECEP = ckbREC.Checked ? "S" : "N";
                tb14.FL_AVALI_RISCO = ckbAVR.Checked ? "S" : "N";
                tb14.FL_CAIXA_FINAN = ckbCXF.Checked ? "S" : "N";
                tb14.FL_LABOR_INTER = ckbLBI.Checked ? "S" : "N";
                tb14.FL_LABOR_EXTER = ckbLBE.Checked ? "S" : "N";
                tb14.FL_CONSU = ckbCON.Checked ? "S" : "N";
                tb14.FL_SALA_EXAME = ckbSEX.Checked ? "S" : "N";
                tb14.FL_UTI = ckbUTI.Checked ? "S" : "N";
                tb14.FL_INTER = chbACO.Checked ? "S" : "N";
                tb14.FL_FARMA = ckbFAM.Checked ? "S" : "N";
                tb14.FL_AMBUL = ckbAMB.Checked ? "S" : "N";
                tb14.FL_SALA_PROCE = ckbSLP.Checked ? "S" : "N";
                tb14.FL_SALA_ESPER = ckbSLE.Checked ? "S" : "N";
                tb14.FL_SALA_REPOU = ckbSRE.Checked ? "S" : "N";
                tb14.FL_ENFER = ckbENF.Checked ? "S" : "N";
                tb14.FL_MANUN = ckbMAN.Checked ? "S" : "N";
                tb14.FL_CENTR_CIRUR = ckbCCI.Checked ? "S" : "N";
            }

            tb14.CO_PROTO_ACOMO = ddlAcomoda.SelectedValue != "" ? int.Parse(ddlAcomoda.SelectedValue) : (int?)null;
            tb14.CO_PROTO_CONIN = ddlCtrlInter.SelectedValue != "" ? int.Parse(ddlCtrlInter.SelectedValue) : (int?)null;
            tb14.CO_PROTO_ESTER = ddlEsteriliza.SelectedValue != "" ? int.Parse(ddlEsteriliza.SelectedValue) : (int?)null;
            tb14.CO_PROTO_HIGIE = ddlHigiene.SelectedValue != "" ? int.Parse(ddlHigiene.SelectedValue) : (int?)null;
            tb14.CO_PROTO_LAVAN = ddlLavanderia.SelectedValue != "" ? int.Parse(ddlLavanderia.SelectedValue) : (int?)null;
            tb14.CO_PROTO_MANUT = ddlManutencao.SelectedValue != "" ? int.Parse(ddlManutencao.SelectedValue) : (int?)null;
            tb14.CO_PROTO_SEGUR = ddlSeguro.SelectedValue != "" ? int.Parse(ddlSeguro.SelectedValue) : (int?)null;

            tb14.FL_CTRL_DETET = ckbDTE.Checked ? "S" : "N";
            tb14.FL_CTRL_EQUIP = ckbEQP.Checked ? "S" : "N";
            tb14.FL_CTRL_ESTER = ckbEST.Checked ? "S" : "N";
            tb14.FL_CTRL_HIGIE = ckbHIG.Checked ? "S" : "N";
            tb14.FL_CTRL_MANUT = ckbMNT.Checked ? "S" : "N";
            tb14.FL_CTRL_MONIT = ckbMON.Checked ? "S" : "N";
            tb14.FL_CTRL_SEGUR = ckbSEG.Checked ? "S" : "N";
            tb14.FL_CTRL_TEMAM = ckbTMP.Checked ? "S" : "N";

            CurrentPadraoCadastros.CurrentEntity = tb14;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB14_DEPTO tb14 = RetornaEntidade();

            if (tb14 != null)
            {
                CarregaCentroCusto();
                
                txtNomeDepto.Text = tb14.NO_DEPTO;
                txtDescricao.Text = tb14.DE_DEPTO;
                txtTelefone.Text = tb14.NU_TEL_DEPTO;
                txtRamal.Text = tb14.NU_RAMAL_DEPTO;
                txtSiglaDepto.Text = tb14.CO_SIGLA_DEPTO;
                ddlRisco.SelectedValue = String.IsNullOrEmpty(tb14.CO_CLASS_RISCO.ToString()) ? "" : tb14.CO_CLASS_RISCO.ToString();
                ddlSitua.SelectedValue = String.IsNullOrEmpty(tb14.CO_SITUA_DEPTO) ? "" : tb14.CO_SITUA_DEPTO;
                ddlTipo.SelectedValue = String.IsNullOrEmpty(tb14.CO_TIPO_DEPTO.ToString()) ? "" : tb14.CO_TIPO_DEPTO.ToString();
                VerificaTipo();
                ckbREC.Checked = tb14.FL_RECEP == "S" ? true : false;
                ckbAVR.Checked = tb14.FL_AVALI_RISCO == "S" ? true : false;
                ckbCXF.Checked = tb14.FL_CAIXA_FINAN == "S" ? true : false;
                ckbLBI.Checked = tb14.FL_LABOR_INTER == "S" ? true : false;
                ckbLBE.Checked = tb14.FL_LABOR_EXTER == "S" ? true : false;
                ckbCON.Checked = tb14.FL_CONSU == "S" ? true : false;
                ckbSEX.Checked = tb14.FL_SALA_EXAME == "S" ? true : false;
                ckbUTI.Checked = tb14.FL_UTI == "S" ? true : false;
                chbACO.Checked = tb14.FL_INTER == "S" ? true : false;
                ckbFAM.Checked = tb14.FL_FARMA == "S" ? true : false;
                ckbAMB.Checked = tb14.FL_AMBUL == "S" ? true : false;
                ckbSLP.Checked = tb14.FL_SALA_PROCE == "S" ? true : false;
                ckbSLE.Checked = tb14.FL_SALA_ESPER == "S" ? true : false;
                ckbSRE.Checked = tb14.FL_SALA_REPOU == "S" ? true : false;
                ckbENF.Checked = tb14.FL_ENFER == "S" ? true : false;
                ckbMAN.Checked = tb14.FL_MANUN == "S" ? true : false;
                ckbCCI.Checked = tb14.FL_CENTR_CIRUR == "S" ? true : false;

                ddlAcomoda.SelectedValue = String.IsNullOrEmpty(tb14.CO_PROTO_ACOMO.ToString()) ? "" : tb14.CO_PROTO_ACOMO.ToString();
                ddlCtrlInter.SelectedValue = String.IsNullOrEmpty(tb14.CO_PROTO_CONIN.ToString()) ? "" : tb14.CO_PROTO_CONIN.ToString();
                ddlEsteriliza.SelectedValue = String.IsNullOrEmpty(tb14.CO_PROTO_ESTER.ToString()) ? "" : tb14.CO_PROTO_ESTER.ToString();
                ddlHigiene.SelectedValue = String.IsNullOrEmpty(tb14.CO_PROTO_HIGIE.ToString()) ? "" : tb14.CO_PROTO_HIGIE.ToString();
                ddlLavanderia.SelectedValue = String.IsNullOrEmpty(tb14.CO_PROTO_LAVAN.ToString()) ? "" : tb14.CO_PROTO_LAVAN.ToString();
                ddlManutencao.SelectedValue = String.IsNullOrEmpty(tb14.CO_PROTO_MANUT.ToString()) ? "" : tb14.CO_PROTO_MANUT.ToString();
                ddlSeguro.SelectedValue = String.IsNullOrEmpty(tb14.CO_PROTO_SEGUR.ToString()) ? "" : tb14.CO_PROTO_SEGUR.ToString();

                ckbMON.Checked = tb14.FL_CTRL_MONIT == "S" ? true : false;
                ckbSEG.Checked = tb14.FL_CTRL_SEGUR == "S" ? true : false;
                ckbHIG.Checked = tb14.FL_CTRL_HIGIE == "S" ? true : false;
                ckbEST.Checked = tb14.FL_CTRL_ESTER == "S" ? true : false;
                ckbMNT.Checked = tb14.FL_CTRL_MANUT == "S" ? true : false;
                ckbEQP.Checked = tb14.FL_CTRL_EQUIP == "S" ? true : false;
                ckbTMP.Checked = tb14.FL_CTRL_TEMAM == "S" ? true : false;
                ckbDTE.Checked = tb14.FL_CTRL_DETET == "S" ? true : false;
            }
        }

        private void CarregaCentroCusto()
        {
            TB14_DEPTO tb14 = RetornaEntidade();

            if (tb14 != null)
            {
                var tb099 = TB099_CENTRO_CUSTO.RetornaTodosRegistros().Where(x => x.TB14_DEPTO.CO_DEPTO == tb14.CO_DEPTO).FirstOrDefault();

                if (tb099 != null)
                    txtCentroCusto.Text = tb099.NU_CTA_CENT_CUSTO + " - " + tb099.DE_CENT_CUSTO;
            }
        }

        /// <summary>
        /// Método de carregamento dos tipos de departamento
        /// </summary>
        private void CarregaTipo()
        {
            ddlTipo.DataSource = (from tb174 in TB174_DEPTO_TIPO.RetornaTodosRegistros()
                                  where tb174.CO_SITU_TIPO.Equals("A")
                                  select new { tb174.ID_DEPTO_TIPO, tb174.NO_DEPTO_TIPO }).OrderBy(a => a.NO_DEPTO_TIPO);

            ddlTipo.DataTextField = "NO_DEPTO_TIPO";
            ddlTipo.DataValueField = "ID_DEPTO_TIPO";
            ddlTipo.DataBind();

            ddlTipo.Items.Insert(0, new ListItem("Selecione", ""));

            VerificaTipo();
        }

        private void VerificaTipo()
        {
            //bool tpTipo = ddlTipo.SelectedValue != "" ? TB174_DEPTO_TIPO.RetornaPelaChavePrimaria(int.Parse(ddlTipo.SelectedValue)).CO_CLASS_TIPO_LOCAL.Equals("TEC") : false;

            int depto = !string.IsNullOrEmpty(ddlTipo.SelectedValue) ? int.Parse(ddlTipo.SelectedValue) : 0;
            var tb174 = TB174_DEPTO_TIPO.RetornaPelaChavePrimaria(depto);
            bool tpTipo = tb174 != null ? tb174.CO_CLASS_TIPO_LOCAL.Equals("TEC") : false;

            if (tpTipo && LoginAuxili.CO_TIPO_UNID.Equals("PGS"))
            {
                panelUtilDepto.Visible = true;
            }
            else
            {
                panelUtilDepto.Visible = false;
            }
        }



        private void CarregaAcomodacao()
        {
            ddlAcomoda.DataSource = (from tb426 in TB426_PROTO_ACAO.RetornaTodosRegistros()
                                     where tb426.FL_SITUA.Equals("A")
                                     && tb426.TP_PROTO_ACAO.Equals("ACO")
                                     select new { tb426.ID_PROTO_ACAO, tb426.NO_PROTO_ACAO }).OrderBy(a => a.NO_PROTO_ACAO);

            ddlAcomoda.DataTextField = "NO_PROTO_ACAO";
            ddlAcomoda.DataValueField = "ID_PROTO_ACAO";
            ddlAcomoda.DataBind();

            ddlAcomoda.Items.Insert(0, new ListItem("Selecione", ""));
        }

        private void CarregaHigiene()
        {
            ddlHigiene.DataSource = (from tb426 in TB426_PROTO_ACAO.RetornaTodosRegistros()
                                     where tb426.FL_SITUA.Equals("A")
                                     && tb426.TP_PROTO_ACAO.Equals("HIG")
                                     select new { tb426.ID_PROTO_ACAO, tb426.NO_PROTO_ACAO }).OrderBy(a => a.NO_PROTO_ACAO);

            ddlHigiene.DataTextField = "NO_PROTO_ACAO";
            ddlHigiene.DataValueField = "ID_PROTO_ACAO";
            ddlHigiene.DataBind();

            ddlHigiene.Items.Insert(0, new ListItem("Selecione", ""));
        }

        private void CarregaLavanderia()
        {
            ddlLavanderia.DataSource = (from tb426 in TB426_PROTO_ACAO.RetornaTodosRegistros()
                                     where tb426.FL_SITUA.Equals("A")
                                     && tb426.TP_PROTO_ACAO.Equals("LAV")
                                     select new { tb426.ID_PROTO_ACAO, tb426.NO_PROTO_ACAO }).OrderBy(a => a.NO_PROTO_ACAO);

            ddlLavanderia.DataTextField = "NO_PROTO_ACAO";
            ddlLavanderia.DataValueField = "ID_PROTO_ACAO";
            ddlLavanderia.DataBind();

            ddlLavanderia.Items.Insert(0, new ListItem("Selecione", ""));
        }

        private void CarregaCtrlInter()
        {
            ddlCtrlInter.DataSource = (from tb426 in TB426_PROTO_ACAO.RetornaTodosRegistros()
                                        where tb426.FL_SITUA.Equals("A")
                                        && tb426.TP_PROTO_ACAO.Equals("CTI")
                                        select new { tb426.ID_PROTO_ACAO, tb426.NO_PROTO_ACAO }).OrderBy(a => a.NO_PROTO_ACAO);

            ddlCtrlInter.DataTextField = "NO_PROTO_ACAO";
            ddlCtrlInter.DataValueField = "ID_PROTO_ACAO";
            ddlCtrlInter.DataBind();

            ddlCtrlInter.Items.Insert(0, new ListItem("Selecione", ""));
        }

        private void CarregaManutencao()
        {
            ddlManutencao.DataSource = (from tb426 in TB426_PROTO_ACAO.RetornaTodosRegistros()
                                       where tb426.FL_SITUA.Equals("A")
                                       && tb426.TP_PROTO_ACAO.Equals("MAN")
                                       select new { tb426.ID_PROTO_ACAO, tb426.NO_PROTO_ACAO }).OrderBy(a => a.NO_PROTO_ACAO);

            ddlManutencao.DataTextField = "NO_PROTO_ACAO";
            ddlManutencao.DataValueField = "ID_PROTO_ACAO";
            ddlManutencao.DataBind();

            ddlManutencao.Items.Insert(0, new ListItem("Selecione", ""));
        }

        private void CarregaEsteriliza()
        {
            ddlEsteriliza.DataSource = (from tb426 in TB426_PROTO_ACAO.RetornaTodosRegistros()
                                        where tb426.FL_SITUA.Equals("A")
                                        && tb426.TP_PROTO_ACAO.Equals("EST")
                                        select new { tb426.ID_PROTO_ACAO, tb426.NO_PROTO_ACAO }).OrderBy(a => a.NO_PROTO_ACAO);

            ddlEsteriliza.DataTextField = "NO_PROTO_ACAO";
            ddlEsteriliza.DataValueField = "ID_PROTO_ACAO";
            ddlEsteriliza.DataBind();

            ddlEsteriliza.Items.Insert(0, new ListItem("Selecione", ""));
        }

        private void CarregaSeguranca()
        {
            ddlSeguro.DataSource = (from tb426 in TB426_PROTO_ACAO.RetornaTodosRegistros()
                                        where tb426.FL_SITUA.Equals("A")
                                        && tb426.TP_PROTO_ACAO.Equals("SEG")
                                        select new { tb426.ID_PROTO_ACAO, tb426.NO_PROTO_ACAO }).OrderBy(a => a.NO_PROTO_ACAO);

            ddlSeguro.DataTextField = "NO_PROTO_ACAO";
            ddlSeguro.DataValueField = "ID_PROTO_ACAO";
            ddlSeguro.DataBind();

            ddlSeguro.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB14_DEPTO</returns>
        private TB14_DEPTO RetornaEntidade()
        {
            return TB14_DEPTO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
        }
        #endregion
    }
}
