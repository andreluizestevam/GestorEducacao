//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: ************************
// SUBMÓDULO: *********************
// OBJETIVO: **********************
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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3600_CtrlInformacoesAlunos.F3610_CtrlInformacoesCadastraisAlunos.F3615_ExclusaoAlunoSemMovtoDados
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
                upImagem.ImagemLargura = 66;
                CarregaUfs(ddlUf);
                CarregaUfs(ddlUfNacionalidade);
                CarregaUfs(ddlUfRg);
                CarregaUfs(ddlUfTitulo);
                CarregaInstEspecializadas();
                CarregaProgramasSociais();
                CarregaBolsas();

                if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao))
                {
                    txtDataCadastro.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    ddlUf.SelectedValue = LoginAuxili.CO_UF_INSTITUICAO;
                    ddlUfNacionalidade.SelectedValue = LoginAuxili.CO_UF_INSTITUICAO;
                    ddlUfRg.SelectedValue = LoginAuxili.CO_UF_INSTITUICAO;
                    ddlUfTitulo.SelectedValue = LoginAuxili.CO_UF_INSTITUICAO;
                    CarregaCidades();
                    ddlCidade.SelectedValue = LoginAuxili.CO_CIDADE_INSTITUICAO.ToString();
                    CarregaBairros();
                }
            }
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            TB07_ALUNO tb07 = RetornaEntidade();

            int ocoMatricula = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                where tb08.CO_ALU == tb07.CO_ALU
                                select tb08).Count();

            tb07.TB125_DADOSESCOLAR_ALUReference.Load();

//--------> Faz a verificação para saber se existe matrícula ou dados escolares associados ao aluno
            if (ocoMatricula > 0 || tb07.TB125_DADOSESCOLAR_ALU != null)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Aluno não pode ser excluído");
                return;
            }

            CurrentPadraoCadastros.CurrentEntity = tb07;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        void CarregaFormulario()
        {
            TB07_ALUNO tb07 = RetornaEntidade();

            if (tb07 != null)
            {
                CarregaDadosMatricula();

                if (tb07.TB108_RESPONSAVEL != null)
                    txtResponsavel.Text = TB108_RESPONSAVEL.RetornaPelaChavePrimaria((int)tb07.TB108_RESPONSAVEL.CO_RESP).NO_RESP;

                string grauParent = "";

                if (ParentescoResponsavel.PM.ToString() == tb07.CO_GRAU_PAREN_RESP)
                    grauParent = "Pai/Mãe";
                else if (ParentescoResponsavel.TI.ToString() == tb07.CO_GRAU_PAREN_RESP)
                    grauParent = "Tio(a)";
                else if (ParentescoResponsavel.AV.ToString() == tb07.CO_GRAU_PAREN_RESP)
                    grauParent = "Avô/Avó";
                else if (ParentescoResponsavel.PR.ToString() == tb07.CO_GRAU_PAREN_RESP)
                    grauParent = "Primo(a)";
                else if (ParentescoResponsavel.CN.ToString() == tb07.CO_GRAU_PAREN_RESP)
                    grauParent = "Cunhado(a)";
                else if (ParentescoResponsavel.TU.ToString() == tb07.CO_GRAU_PAREN_RESP)
                    grauParent = "Tutor(a)";
                else if (ParentescoResponsavel.IR.ToString() == tb07.CO_GRAU_PAREN_RESP)
                    grauParent = "Irmão(ã)";
                else if (ParentescoResponsavel.OU.ToString() == tb07.CO_GRAU_PAREN_RESP)
                    grauParent = "Outros";

                txtGrauParentesco.Text = grauParent;

                if (tb07.Image != null)
                    upImagem.CarregaImagem(tb07.Image.ImageId);
                else
                    upImagem.CarregaImagem(0);

                txtCartaoSaude.Text = tb07.NU_CARTAO_SAUDE_ALU == null ? "" : tb07.NU_CARTAO_SAUDE_ALU.ToString();
                txtNacionalidade.Text = tb07.DE_NACI_ALU;
                txtCartorio.Text = tb07.DE_CERT_CARTORIO;
                txtCep.Text = tb07.CO_CEP_ALU;
                txtComplemento.Text = tb07.DE_COMP_ALU;
                txtCpf.Text = tb07.NU_CPF_ALU;
                txtDataCadastro.Text = tb07.DT_CADA_ALU != null ? tb07.DT_CADA_ALU.Value.ToString("dd/MM/yyyy") : "";
                txtDataEmissaoRg.Text = tb07.DT_EMIS_RG_ALU != null ? tb07.DT_EMIS_RG_ALU.Value.ToString("dd/MM/yyyy") : "";
                txtDataNascimento.Text = tb07.DT_NASC_ALU != null ? tb07.DT_NASC_ALU.Value.ToString("dd/MM/yyyy") : "";
                txtDeficiencia.Text = tb07.DES_DEF;
                txtDesconto.Text = tb07.NU_PEC_DESBOL != null ? tb07.NU_PEC_DESBOL.ToString() : "";
                txtEmail.Text = tb07.NO_WEB_ALU;
                txtFolha.Text = tb07.NU_CERT_FOLHA;
                txtLivro.Text = tb07.DE_CERT_LIVRO;
                txtLogradouro.Text = tb07.DE_ENDE_ALU;
                txtNaturalidade.Text = tb07.DE_NATU_ALU;
                txtNis.Text = tb07.NU_NIS != null ? tb07.NU_NIS.ToString() : "";
                txtNire.Text = tb07.NU_NIRE.ToString();
                txtNome.Text = tb07.NO_ALU;
                txtNomeMae.Text = tb07.NO_MAE_ALU;
                txtNomePai.Text = tb07.NO_PAI_ALU;
                txtNumero.Text = tb07.NU_ENDE_ALU != null ? tb07.NU_ENDE_ALU.ToString() : "";
                txtNumeroCertidao.Text = tb07.NU_CERT;
                txtNumeroTitulo.Text = tb07.NU_TIT_ELE;
                txtObservacoes.Text = tb07.DES_OBSERVACAO;
                txtOrgaoEmissor.Text = tb07.CO_ORG_RG_ALU;
                txtPeriodoDe.Text = tb07.DT_VENC_BOLSA != null ? tb07.DT_VENC_BOLSA.Value.ToString("dd/MM/yyyy") : "";
                txtPeriodoAte.Text = tb07.DT_VENC_BOLSAF != null ? tb07.DT_VENC_BOLSAF.Value.ToString("dd/MM/yyyy") : "";
                txtRg.Text = tb07.CO_RG_ALU;
                txtSecao.Text = tb07.NU_SEC_ELE;
                txtTelCelular.Text = tb07.NU_TELE_CELU_ALU;
                txtTelResidencial.Text = tb07.NU_TELE_RESI_ALU;
                txtZona.Text = tb07.NU_ZONA_ELE;
                ddlUf.SelectedValue = tb07.CO_ESTA_ALU;
                CarregaCidades();
                ddlCidade.SelectedValue = tb07.TB905_BAIRRO != null ? tb07.TB905_BAIRRO.CO_CIDADE.ToString() : "";
                CarregaBairros();
                ddlBairro.SelectedValue = tb07.TB905_BAIRRO != null ? tb07.TB905_BAIRRO.CO_BAIRRO.ToString() : "";
                ddlBolsa.SelectedValue = tb07.TB148_TIPO_BOLSA != null ? tb07.TB148_TIPO_BOLSA.CO_TIPO_BOLSA.ToString() : "";
                ddlDeficiencia.SelectedValue = tb07.TP_DEF;
                ddlEstadoCivil.SelectedValue = tb07.CO_ESTADO_CIVIL;
                ddlEtnia.SelectedValue = tb07.TP_RACA;
                ddlNacionalidade.SelectedValue = tb07.CO_NACI_ALU;
                ddlPasseEscolar.SelectedValue = tb07.FLA_PASSE_ESCOLA != null ? tb07.FLA_PASSE_ESCOLA.ToString() : "";
                ddlTransporteEscolar.SelectedValue = tb07.FLA_TRANSP_ESCOLAR != null ? tb07.FLA_TRANSP_ESCOLAR.ToString() : "";
                ddlRendaFamiliar.SelectedValue = tb07.RENDA_FAMILIAR;
                ddlSexo.SelectedValue = tb07.CO_SEXO_ALU;
                ddlSituacao.SelectedValue = tb07.CO_SITU_ALU;
                ddlTipoCertidao.SelectedValue = tb07.TP_CERTIDAO;
                ddlUfNacionalidade.SelectedValue = tb07.CO_UF_NATU_ALU;
                ddlUfRg.SelectedValue = tb07.CO_ESTA_RG_ALU;
                ddlUfTitulo.SelectedValue = tb07.CO_UF_TIT_ELE;
                rblBolsista.SelectedValue = tb07.FLA_BOLSISTA;
                HabilitaCamposBolsa();

                foreach (var tb164 in tb07.TB164_INST_ESP)
                    cblInstEspecializada.Items.FindByValue(tb164.CO_INST_ESP.ToString()).Selected = true;

                foreach (var tb136 in tb07.TB136_ALU_PROG_SOCIAIS)
                    cblProgramasSociais.Items.FindByValue(tb136.CO_IDENT_PROGR_SOCIA.ToString()).Selected = true;
            }            
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB07_ALUNO</returns>
        private TB07_ALUNO RetornaEntidade()
        {
            TB07_ALUNO tb07 = TB07_ALUNO.RetornaPeloCoAlu(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
            return (tb07 == null) ? new TB07_ALUNO() : tb07;
        }

        /// <summary>
        /// Método que carrega o dropdown de UFs
        /// </summary>
        /// <param name="ddl">DropDown de UF</param>
        private void CarregaUfs(DropDownList ddl)
        {
            ddl.DataSource = TB74_UF.RetornaTodosRegistros();

            ddl.DataTextField = "CODUF";
            ddl.DataValueField = "CODUF";
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Cidades
        /// </summary>
        private void CarregaCidades()
        {
            ddlCidade.DataSource = TB904_CIDADE.RetornaPeloUF(ddlUf.SelectedValue);

            ddlCidade.DataTextField = "NO_CIDADE";
            ddlCidade.DataValueField = "CO_CIDADE";
            ddlCidade.DataBind();

            ddlCidade.Enabled = ddlCidade.Items.Count > 0;
            ddlCidade.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Bairros
        /// </summary>
        private void CarregaBairros()
        {
            int coCidade = ddlCidade.SelectedValue != "" ? int.Parse(ddlCidade.SelectedValue) : 0;

            if (coCidade == 0)
            {
                ddlBairro.Enabled = false;
                ddlBairro.Items.Clear();
                return;
            }

            ddlBairro.DataSource = TB905_BAIRRO.RetornaPelaCidade(coCidade);

            ddlBairro.DataTextField = "NO_BAIRRO";
            ddlBairro.DataValueField = "CO_BAIRRO";
            ddlBairro.DataBind();

            ddlBairro.Enabled = ddlBairro.Items.Count > 0;
            ddlBairro.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o CheckBoxList de Instituições Especializadas
        /// </summary>
        private void CarregaInstEspecializadas()
        {
            cblInstEspecializada.DataSource = TB164_INST_ESP.RetornaTodosRegistros();

            cblInstEspecializada.DataTextField = "NO_INST_ESP";
            cblInstEspecializada.DataValueField = "CO_INST_ESP";
            cblInstEspecializada.DataBind();
        }

        /// <summary>
        /// Método que carrega o CheckBoxList de Programas Sociais
        /// </summary>
        private void CarregaProgramasSociais()
        {
            cblProgramasSociais.DataSource = TB135_PROG_SOCIAIS.RetornaPelaInstituicao(LoginAuxili.ORG_CODIGO_ORGAO);

            cblProgramasSociais.DataTextField = "NO_PROGR_SOCIA";
            cblProgramasSociais.DataValueField = "CO_IDENT_PROGR_SOCIA";
            cblProgramasSociais.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Bolsas
        /// </summary>
        private void CarregaBolsas()
        {
            ddlBolsa.DataSource = TB148_TIPO_BOLSA.RetornaTodosRegistros();

            ddlBolsa.DataTextField = "DE_TIPO_BOLSA";
            ddlBolsa.DataValueField = "CO_TIPO_BOLSA";
            ddlBolsa.DataBind();

            ddlBolsa.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que habilita/desabilita campos de bolsa
        /// </summary>
        private void HabilitaCamposBolsa()
        {
            txtDesconto.Enabled = ddlBolsa.Enabled = txtPeriodoDe.Enabled = txtPeriodoAte.Enabled = rblBolsista.SelectedValue == "S";
        }

        /// <summary>
        /// Método que carrega informações da matrícula do Aluno
        /// </summary>
        private void CarregaDadosMatricula()
        {
            int coAlu = 0;

            if (QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id) == 0)
                return;
            else
                coAlu = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id);

            var tb08 = (from lTb08 in TB08_MATRCUR.RetornaTodosRegistros()
                        where lTb08.CO_ALU == coAlu && lTb08.CO_SIT_MAT == "A"
                        join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on lTb08.CO_EMP equals tb25.CO_EMP
                        join tb01 in TB01_CURSO.RetornaTodosRegistros() on lTb08.CO_CUR equals tb01.CO_CUR
                        join tb06 in TB06_TURMAS.RetornaTodosRegistros() on lTb08.CO_TUR equals tb06.CO_TUR
                        select new
                        {
                            lTb08.CO_ALU_CAD, lTb08.TB44_MODULO.DE_MODU_CUR, tb25.sigla, tb01.CO_SIGL_CUR, tb06.TB129_CADTURMAS.CO_SIGLA_TURMA
                        }).FirstOrDefault();

            if (tb08 != null)
            {
                txtMatricula.Text = tb08.CO_ALU_CAD;
                txtModalidade.Text = tb08.DE_MODU_CUR;
                txtSerie.Text = tb08.CO_SIGL_CUR;
                txtUnidade.Text = tb08.sigla;
                txtTurma.Text = tb08.CO_SIGLA_TURMA;
            }
        }
        #endregion
    }
}