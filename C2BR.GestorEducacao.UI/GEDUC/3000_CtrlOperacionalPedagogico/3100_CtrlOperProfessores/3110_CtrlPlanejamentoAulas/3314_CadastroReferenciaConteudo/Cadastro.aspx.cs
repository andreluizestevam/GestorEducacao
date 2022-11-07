//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: TABELAS DE USO GERAL DA SOLUÇÃO GE
// OBJETIVO: BAIRRO
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using Resources;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3100_CtrlOperProfessores.F3110_CtrlPlanejamentoAulas.F3314_CadastroReferenciaConteudo
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
            if (IsPostBack)
             return;

            CarregaModalidades();
            CarregaSerieCurso();
            CarregaMaterias();
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { 
           CarregaFormulario(); 
        }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {           
            TB310_REFER_CONTEUDO tb310 = RetornaEntidade();

            tb310.CO_TIPO_REFER_CONTE = ddlTipoConte.SelectedValue;
            tb310.ID_MATERIA = int.Parse(ddlDisciplina.SelectedValue);
            tb310.CO_MODU_CUR = ddlModalidade.SelectedValue != "" ? (int?)int.Parse(ddlModalidade.SelectedValue) : null;
            tb310.CO_CUR = ddlSerieCurso.SelectedValue != "" ? (int?)int.Parse(ddlSerieCurso.SelectedValue) : null;
            tb310.NO_TITUL_REFER_CONTE = txtTitulConte.Text;
            tb310.DE_REFER_CONTE = txtDescConte.Text;
            
            //André
            tb310.BNCC = ddlBNCC.SelectedValue;
            tb310.COD_BNCC = tbcodigobncc.Text;
            tb310.PRT_BNCC = tbpraticabncc.Text;
            tb310.OBJ_BNCC = tbobjetobcnn.Text;

            tb310.CO_NIVEL_APREN = ddlNivelConte.SelectedValue;
            tb310.DE_LINK_EXTER = txtLinkExterConte.Text != "" ? txtLinkExterConte.Text : null;
            tb310.CO_STATUS = ddlStatus.SelectedValue;

            if (FileUploadControl.HasFile)
            {
                byte[] bytes;
                bytes = FileUploadControl.FileBytes;
                FileUploadControl.PostedFile.InputStream.Read(bytes, 0, bytes.Length);
                tb310.ANEXO = bytes;
                tb310.NO_ANEXO = FileUploadControl.FileName;
            }

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
            {
                tb310.DT_CADAS_REFER_CONTE = DateTime.Now;
                tb310.TB03_COLABOR = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL);
                tb310.CO_IP_CADAS = LoginAuxili.IP_USU;
                tb310.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);
            }

            CurrentPadraoCadastros.CurrentEntity = tb310;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TB310_REFER_CONTEUDO tb310 = RetornaEntidade();

            if (tb310 != null)
            {
                ddlTipoConte.SelectedValue = tb310.CO_TIPO_REFER_CONTE;

                CarregaModalidades();
                ddlModalidade.SelectedValue = tb310.CO_MODU_CUR != null ? tb310.CO_MODU_CUR.ToString() : "";
                CarregaSerieCurso();
                ddlSerieCurso.SelectedValue = tb310.CO_CUR != null ? tb310.CO_CUR.ToString() : "";
                CarregaMaterias();
                ddlDisciplina.SelectedValue = tb310.ID_MATERIA.ToString();
                txtTitulConte.Text = tb310.NO_TITUL_REFER_CONTE;
                txtDescConte.Text = tb310.DE_REFER_CONTE;
                ddlNivelConte.SelectedValue = tb310.CO_NIVEL_APREN;
                txtLinkExterConte.Text = tb310.DE_LINK_EXTER != null ? tb310.DE_LINK_EXTER : "";
                ddlStatus.SelectedValue = tb310.CO_STATUS;

                //André
                ddlBNCC.SelectedValue = tb310.BNCC;
                tbcodigobncc.Text = tb310.COD_BNCC;
                tbpraticabncc.Text = tb310.PRT_BNCC;
                tbobjetobcnn.Text = tb310.OBJ_BNCC;
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB310_REFER_CONTEUDO</returns>
        private TB310_REFER_CONTEUDO RetornaEntidade()
        {
            TB310_REFER_CONTEUDO tb310 = TB310_REFER_CONTEUDO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb310 == null) ? new TB310_REFER_CONTEUDO() : tb310;
        }

        #endregion       

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            if (LoginAuxili.FLA_PROFESSOR != "S")
            {
                ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where(m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

                ddlModalidade.DataTextField = "DE_MODU_CUR";
                ddlModalidade.DataValueField = "CO_MODU_CUR";
                ddlModalidade.DataBind();

                ddlModalidade.Items.Insert(0, new ListItem("Selecione", ""));
            }
            else
            {
                int ano = DateTime.Now.Year;
                AuxiliCarregamentos.carregaModalidadesProfeResp(ddlModalidade, LoginAuxili.CO_COL, ano, false);
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;

            //if (modalidade != 0)
            //{
                if (LoginAuxili.FLA_PROFESSOR != "S")
                {
                    ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                                where tb01.CO_MODU_CUR == modalidade
                                                select new { tb01.CO_CUR, tb01.NO_CUR }).Distinct().OrderBy(c => c.NO_CUR);

                    ddlSerieCurso.DataTextField = "NO_CUR";
                    ddlSerieCurso.DataValueField = "CO_CUR";
                    ddlSerieCurso.DataBind();
                    ddlSerieCurso.Items.Insert(0, new ListItem("Selecione", ""));
                }
                else
                {
                    int ano = DateTime.Now.Year;
                    AuxiliCarregamentos.carregaSeriesCursosProfeResp(ddlSerieCurso, modalidade,LoginAuxili.CO_COL, ano, false);
                }
            //}            
        }

        /// <summary>
        /// Método que carrega o dropdown de Disciplinas
        /// </summary>
        private void CarregaMaterias()
        {
            if (LoginAuxili.FLA_PROFESSOR != "S")
            {
                ddlDisciplina.DataSource = (from tb107 in TB107_CADMATERIAS.RetornaTodosRegistros()
                                            where tb107.CO_EMP == LoginAuxili.CO_EMP
                                            select new { tb107.ID_MATERIA, tb107.NO_MATERIA }).OrderBy(m => m.NO_MATERIA);

                ddlDisciplina.DataTextField = "NO_MATERIA";
                ddlDisciplina.DataValueField = "ID_MATERIA";
                ddlDisciplina.DataBind();
                ddlDisciplina.Items.Insert(0, new ListItem("Selecione", ""));
            }
            else
            {
                int ano = DateTime.Now.Year;
                int modalidade = (ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0);
                int serie = (ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0);
                AuxiliCarregamentos.CarregaMateriasProfeRespon(ddlDisciplina, LoginAuxili.CO_COL, modalidade, serie, ano, false);
            }
        }
        #endregion

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaMaterias();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaMaterias();
        }

        protected void ddlBNCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBNCC.SelectedValue == "Sim")
            {
                tbcodigobncc.Enabled = true;
                tbpraticabncc.Enabled = true;
                tbobjetobcnn.Enabled = true;
            }
            else
            {
                tbcodigobncc.Text = "";
                tbpraticabncc.Text = "";
                tbobjetobcnn.Text = "";
                tbcodigobncc.Enabled = false;
                tbpraticabncc.Enabled = false;
                tbobjetobcnn.Enabled = false;
            }
        }
    }
}