//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: CONTROLE DE SÉRIES OU CURSOS
// OBJETIVO: GERA GRADE ANUAL DE DISCIPLINA (MATÉRIA) DE SÉRIE/CURSO
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+------------------------------------
// 17/10/2014| Maxwell Almeida            |  Criação da funcionalidade para registrode Campanhas de Saúde
//           |                            | 
//           |                            |  

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9100_CtrlCampanhasSaude._9110_Registros._9111_CampanhaSaude
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
                //------------> Tamanho da imagem de Produto
                ///Define altura e largura da imagem do funcionário
                upImagemProdu.ImagemLargura = 190;
                upImagemProdu.ImagemAltura = 70;

                CarregaTipos();
                CarregaCompetencias(); 
                CarregaClassificacoes();
                CarregaSituacoes();
                CarregaFuncionarios();
                CarregaUnidades();
                CarregaUF();
                CarregaCidades();
                CarregaBairros();
            }
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int hrI = int.Parse(txtHrInicio.Text.Substring(0,2));
            int mnI = int.Parse(txtHrInicio.Text.Substring(3,2));
            int hrT = int.Parse(txtHrTermino.Text.Substring(0,2));
            int mnT = int.Parse(txtHrTermino.Text.Substring(3,2));

            //Valida as horas e minutos digitados
            if (hrI >= 60)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "A Hora de Início da Campanha de Saúde precisa ser uma hora válida");
                txtHrInicio.Focus();
                return;
            }
            if (mnI >= 60)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "O minuto de Início da Campanha de Saúde precisa ser um minuto válido");
                txtHrInicio.Focus();
                return;
            }
            if (hrT >= 60)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "A Hora de Término da Campanha de Saúde precisa ser uma hora válida");
                txtHrTermino.Focus();
                return;
            }
            if (mnT >= 60)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "O minuto de Término da Campanha de Saúde precisa ser um minuto válido");
                txtHrTermino.Focus();
                return;
            }

            //Faz as verificações de consistências de dados
            if(chkRespEhFunc.Checked)
            {
                if(string.IsNullOrEmpty(ddlRespCamp.SelectedValue))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor Selecionar o(a) Responsável pela Campanha de Saúde");
                    ddlRespCamp.Focus();
                    return;
                }
            }
            else
            {
                if(string.IsNullOrEmpty(txtNoResp.Text))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o nome do(a) Responsável pela Campanha de Saúde");
                    txtNoResp.Focus();
                    return;
                }
            }

            TBS339_CAMPSAUDE tbs339 = RetornaEntidade();

            tbs339.NM_CAMPAN = txtNomeCampanha.Text;
            tbs339.DE_CAMPAN = verificaStrings(txtDesc.Text);
            tbs339.CO_SIGLA_CAMPAN = txtSigla.Text;
            tbs339.CO_TIPO_CAMPAN = ddlTipoCamp.SelectedValue;
            tbs339.CO_COMPE_TIPO_CAMPAN = ddlCompetencia.SelectedValue;
            tbs339.DT_INICI_CAMPAN = DateTime.Parse(txtDataInicio.Text);
            tbs339.HR_INICI_CAMPAN = txtHrInicio.Text;
            tbs339.DT_TERMI_CAMPAN = DateTime.Parse(txtDataTermino.Text);
            tbs339.HR_TERMI_CAMPAN = txtHrTermino.Text;
            tbs339.NM_RESPO_CAMPAN = (chkRespEhFunc.Checked ? ddlRespCamp.SelectedItem.Text : txtNoResp.Text);
            tbs339.CO_CLASS_CAMPAN = ddlClassCamp.SelectedValue;
            tbs339.CO_IP_SITUA_TIPO_CAMPAN = Request.UserHostAddress;

            int codImagem = upImagemProdu.GravaImagem();
            tbs339.Image = C2BR.GestorEducacao.BusinessEntities.MSSQL.Image.RetornaPelaChavePrimaria(codImagem);
            
            //Dados do local
            tbs339.CO_EMP_LOCAL_CAMPAN = (!string.IsNullOrEmpty(ddlUnidCampa.SelectedValue) ? int.Parse(ddlUnidCampa.SelectedValue) : (int?)null);
            tbs339.NM_LOCAL_CAMPAN = (chkEhUnidadeCadastrada.Checked ? ddlUnidCampa.SelectedItem.Text : txtNomeLocal.Text);
            //tbs339.NR_CELUL_LOCAL_CAMPA = (!string.IsNullOrEmpty(txtCeluLocal.Text) ? PreparaTelefones(txtCeluLocal.Text) : null);
            tbs339.NR_TELEF_LOCAL_CAMPAN = (!string.IsNullOrEmpty(txtTeleLocal.Text) ? PreparaTelefones(txtTeleLocal.Text) : null);
            tbs339.NR_CELUL_LOCAL_CAMPA = (!string.IsNullOrEmpty(txtTeleLocal.Text) ? PreparaTelefones(txtTeleLocal.Text) : null);
            tbs339.NM_EMAIL_LOCAL_CAMPAN = verificaStrings(txtEmailLocal.Text);
            tbs339.CO_CEP_LOCAL_CAMPAN = verificaStrings(txtCeluLocal.Text);
            tbs339.CO_UF_LOCAL_CAMPAN = verificaStrings(ddlUFLocal.SelectedValue);
            tbs339.CO_CIDAD_LOCAL_CAMPAN = (!string.IsNullOrEmpty(ddlCidadeLocal.SelectedValue) ? int.Parse(ddlCidadeLocal.SelectedValue) : (int?)null);
            tbs339.NM_CIDAD_LOCAL_CAMPAN = (!string.IsNullOrEmpty(ddlCidadeLocal.SelectedValue) ? ddlCidadeLocal.SelectedItem.Text : null);
            tbs339.CO_BAIRRO_LOCAL_CAMPAN = (!string.IsNullOrEmpty(ddlBairroLocal.SelectedValue) ? int.Parse(ddlBairroLocal.SelectedValue) : (int?)null);
            tbs339.NM_BAIRRO_LOCAL_CAMPAN = (!string.IsNullOrEmpty(ddlBairroLocal.SelectedValue) ? ddlBairroLocal.SelectedItem.Text : null);
            tbs339.DE_ENDERE_LOCAL_CAMPAN = verificaStrings(txtEndeLocal.Text);

            tbs339.NR_TELEF_RESID_RESPO = (!string.IsNullOrEmpty(txtTelResResp.Text) ? PreparaTelefones(txtTelResResp.Text) : null);
            tbs339.NR_TELEF_CELUL_RESPO = (!string.IsNullOrEmpty(txtTelCelResp.Text) ? PreparaTelefones(txtTelCelResp.Text) : null);
            tbs339.NR_TELEF_WHATS_RESPO = (!string.IsNullOrEmpty(txtNuWhatsResp.Text) ? PreparaTelefones(txtNuWhatsResp.Text) : null);
            tbs339.NM_EMAIL_RESPO = verificaStrings(txtEmailResp.Text);
            tbs339.DE_LINK_EXTERNO_HOMEP = verificaStrings(txtLinkHomePage.Text);
            tbs339.DE_LINK_EXTERNO_MIDIA = verificaStrings(txtLinkMidia.Text);
            tbs339.DE_SLOGAN_CAMPA = verificaStrings(txtSlogan.Text);

            //Salva os dados do responsável 
            if(ddlRespCamp.SelectedValue != "")
            {
                int co = int.Parse(ddlRespCamp.SelectedValue);
                var col = (from t03 in TB03_COLABOR.RetornaTodosRegistros()
                           where t03.CO_COL == co
                           select new {t03.NO_COL, t03.CO_EMP, t03.CO_COL}).FirstOrDefault();

                tbs339.CO_EMP_RESPO_CAMPAN = col.CO_EMP;
                tbs339.CO_COL_RESPO_CAMPAN = col.CO_COL;
            }

            //Salva essas informações apenas quando a situação tiver sido alterada
            if (hidCoSitua.Value != ddlSituacao.SelectedValue)
            {
                tbs339.CO_SITUA_TIPO_CAMPAN = ddlSituacao.SelectedValue;
                tbs339.DT_SITUA_TIPO_CAMPAN = DateTime.Now;
                tbs339.CO_COL_SITUA = LoginAuxili.CO_COL;
            }
            
            //Salva essas informações apenas quando for cadastro novo
            switch (tbs339.EntityState)
            {
                case EntityState.Added:
                case EntityState.Detached:
                    tbs339.DT_CADAS = DateTime.Now;
                    tbs339.CO_COL_CADAS = LoginAuxili.CO_COL;
                    break;
            }

            CurrentPadraoCadastros.CurrentEntity = tbs339;
        }

        #endregion

        #region Carregamentos

        /// <summary>
        /// Carrega as informações
        /// </summary>
        private void CarregaFormulario()
        {
            TBS339_CAMPSAUDE tbs339 = RetornaEntidade();

            if (tbs339 != null)
            {
                tbs339.ImageReference.Load();

                if (tbs339.Image != null)
                    upImagemProdu.CarregaImagem(tbs339.Image.ImageId);
                else
                    upImagemProdu.CarregaImagem(0);

                txtNomeCampanha.Text = tbs339.NM_CAMPAN;
                txtSigla.Text = tbs339.CO_SIGLA_CAMPAN;
                txtDesc.Text = tbs339.DE_CAMPAN;
                ddlTipoCamp.SelectedValue = tbs339.CO_TIPO_CAMPAN;
                ddlCompetencia.SelectedValue = tbs339.CO_COMPE_TIPO_CAMPAN;
                ddlClassCamp.SelectedValue = tbs339.CO_CLASS_CAMPAN;
                ddlSituacao.SelectedValue = tbs339.CO_SITUA_TIPO_CAMPAN;
                txtDataInicio.Text = tbs339.DT_INICI_CAMPAN.ToString();
                txtHrInicio.Text = tbs339.HR_INICI_CAMPAN;
                txtDataTermino.Text = tbs339.DT_TERMI_CAMPAN.ToString();
                txtHrTermino.Text = tbs339.HR_TERMI_CAMPAN;
                txtNoResp.Text = tbs339.NM_RESPO_CAMPAN;

                ddlRespCamp.SelectedValue = tbs339.CO_COL_RESPO_CAMPAN.ToString();
                txtTelResResp.Text = tbs339.NR_TELEF_RESID_RESPO;
                txtTelCelResp.Text = tbs339.NR_TELEF_CELUL_RESPO;
                txtNuWhatsResp.Text = tbs339.NR_TELEF_WHATS_RESPO;
                txtEmailResp.Text = tbs339.NM_EMAIL_RESPO;

                txtLinkHomePage.Text = tbs339.DE_LINK_EXTERNO_HOMEP;
                txtLinkMidia.Text = tbs339.DE_LINK_EXTERNO_MIDIA;
                txtSlogan.Text = tbs339.DE_SLOGAN_CAMPA;

                ddlUnidCampa.SelectedValue = tbs339.CO_EMP_LOCAL_CAMPAN.HasValue ? tbs339.CO_EMP_LOCAL_CAMPAN.ToString() : "";
                txtNomeLocal.Text = tbs339.NM_LOCAL_CAMPAN;
                txtTeleLocal.Text = tbs339.NR_TELEF_LOCAL_CAMPAN;
                txtCeluLocal.Text = tbs339.NR_TELEF_LOCAL_CAMPAN;
                //txtCeluLocal.Text = tbs339.NR_CELUL_LOCAL_CAMPA;
                txtEmailLocal.Text = tbs339.NM_EMAIL_LOCAL_CAMPAN;
                txtCEPLocal.Text = tbs339.CO_CEP_LOCAL_CAMPAN;
                ddlUFLocal.SelectedValue = (!string.IsNullOrEmpty(tbs339.CO_UF_LOCAL_CAMPAN) ? tbs339.CO_UF_LOCAL_CAMPAN : "");
                CarregaCidades();
                ddlCidadeLocal.SelectedValue = tbs339.CO_CIDAD_LOCAL_CAMPAN.HasValue ? tbs339.CO_CIDAD_LOCAL_CAMPAN.ToString() : "";
                CarregaBairros();
                ddlBairroLocal.SelectedValue = tbs339.CO_BAIRRO_LOCAL_CAMPAN.HasValue ? tbs339.CO_BAIRRO_LOCAL_CAMPAN.ToString() : "";
                txtEndeLocal.Text = tbs339.DE_ENDERE_LOCAL_CAMPAN;

                if (tbs339.CO_COL_RESPO_CAMPAN != null)
                {
                    chkRespEhFunc.Checked =
                    ddlRespCamp.Visible = true;
                    txtNoResp.Visible = false;
                }
                else
                {
                    chkRespEhFunc.Checked = 
                    ddlRespCamp.Visible = false;
                    txtNoResp.Visible = true;
                }
            }
        }

        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TBS339_CAMPSAUDE</returns>
        private TBS339_CAMPSAUDE RetornaEntidade()
        {
            TBS339_CAMPSAUDE tbs339 = TBS339_CAMPSAUDE.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tbs339 == null) ? new TBS339_CAMPSAUDE() : tbs339;
        }

        /// <summary>
        /// Método responsável por preparar o número retirando os caracteres especiais
        /// </summary>
        /// <param name="tel"></param>
        /// <returns></returns>
        public string PreparaTelefones(string tel)
        {
            return tel.Replace("(", "").Replace(")", "").Replace("-", "").Trim();
        }

        /// <summary>
        /// Método responsável por verificar as strings
        /// </summary>
        /// <param name="stg"></param>
        /// <returns></returns>
        public string verificaStrings(string stg)
        {
            return (!string.IsNullOrEmpty(stg) ? stg : null);
        }

        /// <summary>
        /// Carrega os tipos de campanhas
        /// </summary>
        private void CarregaTipos()
        {
            AuxiliCarregamentos.CarregaTiposCampanhaSaude(ddlTipoCamp, false);
        }

        /// <summary>
        /// Carrega as competências
        /// </summary>
        private void CarregaCompetencias()
        {
            AuxiliCarregamentos.CarregaCompetenciasCampanhaSaude(ddlCompetencia, false);
        }

        /// <summary>
        /// Carrega as Classificações
        /// </summary>
        private void CarregaClassificacoes()
        {
            AuxiliCarregamentos.CarregaClassificacoesCampanhaSaude(ddlClassCamp, false);
        }

        /// <summary>
        /// Carrega as Situacoes
        /// </summary>
        private void CarregaSituacoes()
        {
            AuxiliCarregamentos.CarregaSituacaoCampanhaSaude(ddlSituacao, false);
            ddlSituacao.SelectedValue = "A";
        }

        /// <summary>
        /// Carrega os funcionários
        /// </summary>
        private void CarregaFuncionarios()
        {
            AuxiliCarregamentos.CarregaFuncionarios(ddlRespCamp, LoginAuxili.CO_EMP, false, false);
            ddlRespCamp.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Carrega as Unidades
        /// </summary>
        private void CarregaUnidades()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidCampa, LoginAuxili.ORG_CODIGO_ORGAO, false, false, false);
            ddlUnidCampa.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Carrega as UF's
        /// </summary>
        private void CarregaUF()
        {
            AuxiliCarregamentos.CarregaUFs(ddlUFLocal, false, LoginAuxili.CO_EMP);
            ddlUFLocal.Items.Insert(0, new ListItem("", ""));
            CarregaCidades();
        }

        /// <summary>
        /// Carrega as Cidades
        /// </summary>
        private void CarregaCidades()
        {
            AuxiliCarregamentos.CarregaCidades(ddlCidadeLocal, false, ddlUFLocal.SelectedValue, LoginAuxili.CO_EMP, false, false);
            ddlCidadeLocal.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Carrega os Bairros
        /// </summary>
        private void CarregaBairros()
        {
            int cida = (!string.IsNullOrEmpty(ddlCidadeLocal.SelectedValue) ? int.Parse(ddlCidadeLocal.SelectedValue) : 0);
            AuxiliCarregamentos.CarregaBairros(ddlBairroLocal, ddlUFLocal.SelectedValue, cida, false, false);
            ddlBairroLocal.Items.Insert(0, new ListItem("", ""));
        }

        #endregion

        #region Funções de Campo

        protected void chkRespEhFunc_OnCheckedChanged(object sender, EventArgs e)
        {
            //Muda o campo do nome do colaborador de acordo com o selecionado
            if(chkRespEhFunc.Checked)
            {
                ddlRespCamp.Visible = true;
                txtNoResp.Visible = false;
            }
            else
            {
                 ddlRespCamp.Visible = false;
                txtNoResp.Visible = true;
            }
        }

        protected void chkEhUnidadeCadastrada_OnCheckedChanged(object sender, EventArgs e)
        {
            //Muda o campo do nome do colaborador de acordo com o selecionado
            if (chkEhUnidadeCadastrada.Checked)
            {
                ddlUnidCampa.Visible = true;
                txtNomeLocal.Visible = false;
            }
            else
            {
                ddlUnidCampa.Visible = false;
                txtNomeLocal.Visible = true;
            }
        }

        protected void ddlUFLocal_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCidades();
        }

        protected void ddlCidadeLocal_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaBairros();
        }

        #endregion
    }
}