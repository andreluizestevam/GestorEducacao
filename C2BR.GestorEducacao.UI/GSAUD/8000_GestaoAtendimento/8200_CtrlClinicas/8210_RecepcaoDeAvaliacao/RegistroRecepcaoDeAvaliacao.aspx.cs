//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE DE PONTO DO COLABORADOR
// OBJETIVO: REGISTRO DE PLANTÃO
// DATA DE CRIAÇÃO: 20/05/2014
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//11/06/2014| MAXWELL ALMEIDA            | Criação da página para encaminhamento do Pré-Atendimento para o Atendimento propriamente dito.
//30/12/2014| MAXWELL ALMEIDA            | Inserção de regra para salvar o código do Pré-Atendimento no Direcionamento

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
using System.Data.Objects;
using C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3400_CtrlAtendimentoUsuario;

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8210_RecepcaoDeAvaliacao
{
    public partial class RegistroRecepcaoDeAvaliacao : System.Web.UI.Page
    {
        #region Váriaveis

        int qtdLinhasGrid = 0;

        #endregion

        #region Eventos

        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtCPFResp.Text = "00000000000";
                txtDtNascResp.Text = "01/01/1900";
                txtNuIDResp.Text = "000000";
                txtOrgEmiss.Text = "SSP";

                //------------> Tamanho da imagem de Aluno
                upImagemAluno.ImagemLargura = 70;
                upImagemAluno.ImagemAltura = 85;
                upImagemAluno.MostraProcurar = false;

                carregaGridMedicosPlantonistas();
                CarregaSubGrupos();

                AuxiliCarregamentos.CarregaUFs(ddlUF, false, LoginAuxili.CO_EMP);
                AuxiliCarregamentos.CarregaUFs(ddlUFOrgEmis, false, LoginAuxili.CO_EMP);
                AuxiliCarregamentos.CarregaGruposProcedimentosMedicos(ddlGrupoProc, true);
                CarregaSubGrupos();
                carregaCidade();
                carregaBairro();
                CarregaDadosUnidLogada();
                VerificarNireAutomatico();
                CarregarFuncoesSimp();

                carregaGridSolicitacoes();
            }
        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //    --------> criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
        }

        /// <summary>
        /// Salva as informações nas tabelas cabíveis, TB108, TB07 e TBS195
        /// </summary>
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            if (ValidaSolicitacoes())
            {
                if (SalvaEntidades())
                    AuxiliPagina.RedirecionaParaPaginaSucesso("Recepção realizada com Sucesso!", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
            }
        }

        #endregion

        #region Carregamentos

        /// <summary>
        /// Carrega os subgrupos de procedimentos
        /// </summary>
        private void CarregaSubGrupos()
        {
            AuxiliCarregamentos.CarregaSubGruposProcedimentosMedicos(ddlSubGrupo, ddlGrupoProc, true);
        }

        /// <summary>
        /// Método responsável por salvar as entidades.
        /// </summary>
        private bool SalvaEntidades()
        {
            if (string.IsNullOrEmpty(hidCoPac.Value))
                VerificarNireAutomatico();

            bool erros = false;

            //----------------------------------------------------- Valida os campos do Responsável -----------------------------------------------------
            if (txtNomeResp.Text == "")
            //{ AuxiliPagina.EnvioMensagemErro(this.Page, "O Nome do Responsável é Requerido"); erros = true; }
            { AbreMensagemInfos("O Nome do Responsável é Requerido"); return false; }

            //if (txtCPFResp.Text == "")
            ////{ AuxiliPagina.EnvioMensagemErro(this.Page, "O CPF do Responsável é Requerido"); erros = true; }
            //{ AbreMensagemInfos("O CPF do Responsável é Requerido"); return false; }

            if (ddlSexResp.SelectedValue == "")
            //{ AuxiliPagina.EnvioMensagemErro(this.Page, "O Sexo do Responsável é Requerido"); erros = true; }
            { AbreMensagemInfos("O Sexo do Responsável é Requerido"); return false; }

            if (txtDtNascResp.Text == "")
            //{ AuxiliPagina.EnvioMensagemErro(this.Page, "A Data de Nascimento do Responsável é Requerida"); erros = true; }
            { AbreMensagemInfos("A Data de Nascimento do Responsável é Requerida"); return false; }

            if (txtNuIDResp.Text == "")
            //{ AuxiliPagina.EnvioMensagemErro(this.Page, "O Número da Identidade do Responsável é Requerido"); erros = true; }
            { AbreMensagemInfos("O Número da Identidade do Responsável é Requerido"); return false; }

            if (txtCEP.Text == "")
            //{ AuxiliPagina.EnvioMensagemErro(this.Page, "O CEP do Endereço do Responsável é Requerido"); erros = true; }
            { AbreMensagemInfos("O CEP do Endereço do Responsável é Requerido"); return false; }

            if (ddlUF.Text == "")
            //{ AuxiliPagina.EnvioMensagemErro(this.Page, "O UF do Endereço do Responsável é Requerida"); erros = true; }
            { AbreMensagemInfos("O UF do Endereço do Responsável é Requerida"); return false; }

            if (ddlCidade.Text == "")
            //{ AuxiliPagina.EnvioMensagemErro(this.Page, "A Cidade do Responsável é Requerida"); erros = true; }
            { AbreMensagemInfos("A Cidade do Responsável é Requerida"); return false; }

            if (ddlBairro.Text == "")
            //{ AuxiliPagina.EnvioMensagemErro(this.Page, "O Bairro do Responsável é Requerido"); erros = true; }
            { AbreMensagemInfos("O Bairro do Responsável é Requerido"); return false; }

            if (txtLograEndResp.Text == "")
            //{ AuxiliPagina.EnvioMensagemErro(this.Page, "O Logradouro do Responsável é Requerido"); erros = true; }
            { AbreMensagemInfos("O Logradouro do Responsável é Requerido"); return false; }

            //----------------------------------------------------- Valida os campos do Aluno -----------------------------------------------------
            if (ddlSexoPaci.SelectedValue == "")
            //{ AuxiliPagina.EnvioMensagemErro(this.Page, "O Sexo do Paciente é Requerido"); erros = true; }
            { AbreMensagemInfos("O Sexo do Paciente é Requerido"); return false; }

            if (txtDtNascPaci.Text == "")
            //{ AuxiliPagina.EnvioMensagemErro(this.Page, "A Data de Nascimento do Paciente é Requerida"); erros = true; }
            { AbreMensagemInfos("A Data de Nascimento do Paciente é Requerida"); return false; }

            if (txtNuProntuario.Text == "")
            //{ AuxiliPagina.EnvioMensagemErro(this.Page, "O Número do PRONTUÁRIO do Paciente é Requerido"); erros = true; }
            { AbreMensagemInfos("O Número do PRONTUÁRIO do Paciente é Requerido"); return false; }
            
            var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

            if (!String.IsNullOrEmpty(txtCPFResp.Text))
            {
                if (!AuxiliValidacao.ValidaCpf(txtCPFResp.Text))
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "CPF do responsável invalido!");
                    txtCPFResp.Focus();
                    return false;
                }
            }
            else if (tb25.FL_CPF_RESP_OBRIGATORIO == "S")
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "O CPF do responsável é obrigatório");
                txtCPFResp.Focus();
                return false;
            }
            else if (tb25.FL_CPF_RESP_OBRIGATORIO == "N" && String.IsNullOrEmpty(txtCPFResp.Text))
            {
                var cpfGerado = AuxiliValidacao.GerarNovoCPF(false);

                //Enquanto existir, calcula um novo cpf
                while (TB108_RESPONSAVEL.RetornaTodosRegistros().Where(w => w.NU_CPF_RESP == cpfGerado).Any())
                    cpfGerado = AuxiliValidacao.GerarNovoCPF(false);

                txtCPFResp.Text = cpfGerado;
            }

            if (!String.IsNullOrEmpty(txtCpfPaci.Text))
            {
                if (!AuxiliValidacao.ValidaCpf(txtCpfPaci.Text))
                {
                    AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "CPF do paciente invalido!");
                    txtCpfPaci.Focus();
                    return false;
                }
            }
            else if (tb25.FL_CPF_PAC_OBRIGATORIO == "S")
            {
                AuxiliPagina.EnvioMensagemErroPopUp(this.Page, "O CPF do paciente é obrigatório");
                txtCpfPaci.Focus();
                return false;
            }

            if (erros != true)
            {
                //Salva os dados do Responsável na tabela 108
                #region Salva Responsável na tb108

                TB108_RESPONSAVEL tb108;
                //Verifica se já não existe um responsável cadastrdado com esse CPF, caso não exista ele cadastra um novo com os dados informados
                var cpfResp = txtCPFResp.Text.Replace("-", "").Replace(".", "");
                var resp = TB108_RESPONSAVEL.RetornaTodosRegistros().Where(r => r.NU_CPF_RESP == cpfResp).FirstOrDefault();

                if (resp != null)
                    tb108 = resp;
                else if (string.IsNullOrEmpty(hidCoResp.Value))
                {
                    tb108 = new TB108_RESPONSAVEL();

                    tb108.CO_CEP_RESP = txtCEP.Text;
                    tb108.CO_ESTA_RESP = ddlUF.SelectedValue;
                    tb108.CO_BAIRRO = int.Parse(ddlBairro.SelectedValue);
                    tb108.CO_CIDADE = int.Parse(ddlCidade.SelectedValue);
                    tb108.DE_ENDE_RESP = txtLograEndResp.Text;

                    tb108.TBS366_FUNCAO_SIMPL = (!string.IsNullOrEmpty(ddlFuncao.SelectedValue) ? TBS366_FUNCAO_SIMPL.RetornaPelaChavePrimaria(int.Parse(ddlFuncao.SelectedValue)) : null);
                    tb108.NO_RESP = txtNomeResp.Text.ToUpper();
                    tb108.NU_CPF_RESP = cpfResp;
                    tb108.CO_RG_RESP = txtNuIDResp.Text;
                    tb108.CO_ORG_RG_RESP = txtOrgEmiss.Text;
                    tb108.CO_ESTA_RG_RESP = ddlUFOrgEmis.SelectedValue;
                    tb108.DT_NASC_RESP = DateTime.Parse(txtDtNascResp.Text);
                    tb108.CO_SEXO_RESP = ddlSexResp.SelectedValue;
                    tb108.CO_CEP_RESP = txtCEP.Text;
                    tb108.CO_ESTA_RESP = ddlUF.SelectedValue;
                    tb108.CO_CIDADE = int.Parse(ddlCidade.SelectedValue);
                    tb108.CO_BAIRRO = int.Parse(ddlBairro.SelectedValue);
                    tb108.DE_ENDE_RESP = txtLograEndResp.Text;
                    tb108.DES_EMAIL_RESP = txtEmailResp.Text;
                    tb108.NU_TELE_CELU_RESP = txtTelCelResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                    tb108.NU_TELE_RESI_RESP = txtTelFixResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                    tb108.NU_TELE_WHATS_RESP = txtNuWhatsResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                    tb108.NU_TELE_COME_RESP = txtTelComResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                    tb108.CO_ORIGEM_RESP = "NN";
                    tb108.CO_SITU_RESP = "A";

                    //Atribui valores vazios para os campos not null da tabela de Responsável.
                    tb108.FL_NEGAT_CHEQUE = "V";
                    tb108.FL_NEGAT_SERASA = "V";
                    tb108.FL_NEGAT_SPC = "V";
                    tb108.CO_INST = 0;
                    tb108.TB000_INSTITUICAO = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO);

                    tb108 = TB108_RESPONSAVEL.SaveOrUpdate(tb108);
                }
                else
                    tb108 = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(int.Parse(hidCoResp.Value));

                #endregion

                //Salva os dados do Usuário em um registro na tb07
                #region Salva o Usuário na TB07

                ////Verifica antes se já existe o paciente algum paciente com o mesmo CPF e NIS informados nos campos, caso não exista, cria um novo
                //string cpfPac = txtCpfPaci.Text.Replace("-", "").Replace(".", "").Trim();
                //var realu = (from tb07li in TB07_ALUNO.RetornaTodosRegistros()
                //             where tb07li.NU_CPF_ALU == cpfPac
                //             select new { tb07li.CO_ALU }).FirstOrDefault();

                //int? paExis = (realu != null ? realu.CO_ALU : (int?)null);

                //Decimal nis = 0;
                //if (!string.IsNullOrEmpty(txtNuNisPaci.Text))
                //{
                //    nis = decimal.Parse(txtNuNisPaci.Text.Trim());
                //}

                //var realu2 = (from tb07ob in TB07_ALUNO.RetornaTodosRegistros()
                //              where tb07ob.NU_NIS == nis
                //              select new { tb07ob.CO_ALU }).FirstOrDefault();

                //int? paExisNis = (realu2 != null ? realu2.CO_ALU : (int?)null);

                TB07_ALUNO tb07;
                
                string cpfPac = txtCpfPaci.Text.Replace(".", "").Replace("-", "").Trim();
                var pac = TB07_ALUNO.RetornaTodosRegistros().Where(a => a.NU_CPF_ALU == cpfPac).FirstOrDefault();

                if (pac != null)
                    tb07 = pac;
                else if (string.IsNullOrEmpty(hidCoPac.Value))
                {
                    tb07 = new TB07_ALUNO();

                    #region Bloco foto
                    int codImagem = upImagemAluno.GravaImagem();
                    tb07.Image = C2BR.GestorEducacao.BusinessEntities.MSSQL.Image.RetornaPelaChavePrimaria(codImagem);
                    #endregion

                    tb07.CO_ORIGEM_ALU = ddlOrigemPaci.SelectedValue;
                    tb07.NO_ALU = txtnompac.Text.ToUpper();
                    tb07.NO_APE_ALU = tb07.NO_ALU.Split(' ')[0];
                    tb07.NU_NIRE = int.Parse(txtNuProntuario.Text);
                    tb07.NU_CPF_ALU = cpfPac;
                    tb07.NU_NIS = decimal.Parse(txtNuNisPaci.Text);
                    tb07.DT_NASC_ALU = DateTime.Parse(txtDtNascPaci.Text);
                    tb07.CO_SEXO_ALU = ddlSexoPaci.SelectedValue;
                    tb07.NU_TELE_CELU_ALU = txtTelCelPaci.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                    tb07.NU_TELE_RESI_ALU = txtTelResPaci.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                    tb07.NU_TELE_WHATS_ALU = txtNuWhatsResp.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                    tb07.CO_GRAU_PAREN_RESP = ddlGrParen.SelectedValue;
                    tb07.NO_EMAIL_PAI = txtEmailPaci.Text;
                    tb07.CO_EMP = LoginAuxili.CO_EMP;
                    tb07.TB25_EMPRESA1 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                    tb07.TB25_EMPRESA = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
                    tb07.TB108_RESPONSAVEL = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(tb108.CO_RESP);
                    tb07.TP_RACA = ddlEtniaAlu.SelectedValue != "" ? ddlEtniaAlu.SelectedValue : null;

                    //Endereço
                    tb07.CO_CEP_ALU = txtCEP.Text;
                    tb07.CO_ESTA_ALU = ddlUF.SelectedValue;
                    tb07.TB905_BAIRRO = TB905_BAIRRO.RetornaPelaChavePrimaria(int.Parse(ddlBairro.SelectedValue));
                    tb07.DE_ENDE_ALU = txtLograEndResp.Text;

                    //Salva os valores para os campos not null da tabela de Usuário
                    tb07.CO_SITU_ALU = "A";
                    tb07.TP_DEF = "N";
                    tb07.FL_LIST_ESP = "S";

                    #region trata para criação do nire

                    var res = (from tb07pesq in TB07_ALUNO.RetornaTodosRegistros()
                               select new { tb07pesq.NU_NIRE }).OrderByDescending(w => w.NU_NIRE).FirstOrDefault();

                    int nir = 0;
                    if (res == null)
                    {
                        nir = 1;
                    }
                    else
                    {
                        nir = res.NU_NIRE;
                    }

                    int nirTot = nir + 1;

                    #endregion
                    tb07.NU_NIRE = nirTot;

                    tb07 = TB07_ALUNO.SaveOrUpdate(tb07);
                }
                else
                    tb07 = TB07_ALUNO.RetornaPeloCoAlu(int.Parse(hidCoPac.Value));

                #endregion

                //Os dados da Recepção na tbs367
                #region Salva na tbs367

                #region Trata sequencial
                //Trata para gerar um Código do Encaminhamento
                var res2 = (from tbs367pesq in TBS367_RECEP_SOLIC.RetornaTodosRegistros()
                            select new { tbs367pesq.NU_REGIS_RECEP_SOLIC }).OrderByDescending(w => w.NU_REGIS_RECEP_SOLIC).FirstOrDefault();

                string seq;
                int seq2;
                int seqConcat;
                string seqcon;
                string ano = DateTime.Now.Year.ToString().Substring(2, 2);
                string mes = DateTime.Now.Month.ToString().PadLeft(2, '0');

                if (res2 == null)
                    seq2 = 1;
                else
                {
                    seq = res2.NU_REGIS_RECEP_SOLIC.Substring(6, 6);
                    seq2 = int.Parse(seq);
                }

                seqConcat = seq2 + 1;
                seqcon = seqConcat.ToString().PadLeft(6, '0');

                string CodigoRecepcao = string.Format("SP{0}{1}{2}", ano, mes, seqcon);
                #endregion

                #region Dados do Usuário Logado

                int coEmpColLogado = TB03_COLABOR.RetornaPeloCoCol(LoginAuxili.CO_COL).CO_EMP;

                #endregion

                #region Dados do Colaborador selecionado para análise

                int? coEmpColAnalise = (!string.IsNullOrEmpty(hidCoColEncAnalise.Value) ? TB03_COLABOR.RetornaPeloCoCol(int.Parse(hidCoColEncAnalise.Value)).CO_EMP : (int?)null);

                #endregion

                TBS367_RECEP_SOLIC tbs367 = new TBS367_RECEP_SOLIC();

                //======================> Dados Gerais
                tbs367.NU_REGIS_RECEP_SOLIC = CodigoRecepcao;
                tbs367.CO_EMP = LoginAuxili.CO_EMP;
                tbs367.CO_ALU = tb07.CO_ALU;
                tbs367.CO_RESP = tb108.CO_RESP;

                //======================> Dados do Cadastro
                tbs367.DT_CADAS = DateTime.Now;
                tbs367.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                tbs367.CO_COL_CADAS = LoginAuxili.CO_COL;
                tbs367.CO_EMP_COL_CADAS = coEmpColLogado;
                tbs367.IP_CADAS = Request.UserHostAddress;

                //======================> Dados do Colaborador que irá analisar previamente
                tbs367.CO_COL_ANALI = (!string.IsNullOrEmpty(hidCoColEncAnalise.Value) ? int.Parse(hidCoColEncAnalise.Value) : (int?)null);
                tbs367.CO_EMP_COL_ANALI = (!coEmpColAnalise.HasValue ? coEmpColAnalise.Value : (int?)null);

                //======================> Dados da Situação
                tbs367.CO_SITUA = "A";
                tbs367.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                tbs367.CO_COL_SITUA = LoginAuxili.CO_COL;
                tbs367.CO_EMP_COL_SITUA = coEmpColLogado;
                tbs367.IP_SITUA = Request.UserHostAddress;
                tbs367.DT_SITUA = DateTime.Now;

                TBS367_RECEP_SOLIC.SaveOrUpdate(tbs367);

                #endregion

                //Percorre a grid de solicitações e persiste as informações
                foreach (GridViewRow i in grdSolicitacoes.Rows)
                {
                    #region Coleta os Dados
                    string ddlOper, ddlPlan, ddlCateg, ddlProc, txtQtde, txtNumero, txtNuGuia,
                        txtNuAutor, txtVlTotal, txtVlDscto, vlUnit;

                    ddlOper = ((DropDownList)i.Cells[0].FindControl("ddlOperadora")).SelectedValue;
                    ddlPlan = ((DropDownList)i.Cells[1].FindControl("ddlPlanoSaude")).SelectedValue;
                    ddlCateg = ((DropDownList)i.Cells[2].FindControl("ddlCategoria")).SelectedValue;
                    txtNumero = ((TextBox)i.Cells[3].FindControl("txtNumeroPlano")).Text;
                    txtNuGuia = ((TextBox)i.Cells[4].FindControl("txtNuGuia")).Text;
                    txtNuAutor = ((TextBox)i.Cells[5].FindControl("txtNuAutor")).Text;
                    txtQtde = ((TextBox)i.Cells[6].FindControl("txtQtde")).Text;
                    ddlProc = ((DropDownList)i.Cells[7].FindControl("ddlProcedimento")).SelectedValue;
                    txtVlTotal = ((TextBox)i.Cells[8].FindControl("txtVlUnitario")).Text;
                    txtVlDscto = ((TextBox)i.Cells[9].FindControl("txtVlDesconto")).Text;
                    vlUnit = ((HiddenField)i.Cells[7].FindControl("hidValUnitProc")).Value;

                    #region Validações



                    #endregion

                    decimal ValorLiquido = 0;
                    //Calcula valor líquido multiplicando a quantidade de sessões pelo valor unitário e subtraindo o desconto
                    if ((!string.IsNullOrEmpty(txtQtde)) && (!string.IsNullOrEmpty(txtVlTotal)))
                        ValorLiquido = ((Convert.ToInt32(txtQtde) * Convert.ToDecimal(txtVlTotal)) - (!string.IsNullOrEmpty(txtVlDscto) ? Convert.ToDecimal(txtVlDscto) : 0));

                    #endregion

                    //Salva objeto da entidade tbs368 que armazena os itens solicitados em uma recepção
                    #region Salva entidade tbs368

                    TBS368_RECEP_SOLIC_ITENS tbs368 = new TBS368_RECEP_SOLIC_ITENS();

                    //======================> Dados Gerais
                    tbs368.TBS367_RECEP_SOLIC = tbs367;
                    tbs368.NU_REGIS_RECEP_SOLIC = tbs367.NU_REGIS_RECEP_SOLIC;

                    //======================> Dados do Plano de Saúde (Quando houver)
                    tbs368.TB250_OPERA = (!string.IsNullOrEmpty(ddlOper) ? TB250_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlOper)) : null);
                    tbs368.TB251_PLANO_OPERA = (!string.IsNullOrEmpty(ddlPlan) ? TB251_PLANO_OPERA.RetornaPelaChavePrimaria(int.Parse(ddlPlan)) : null);
                    tbs368.TB367_CATEG_PLANO_SAUDE = (!string.IsNullOrEmpty(ddlCateg) ? TB367_CATEG_PLANO_SAUDE.RetornaPelaChavePrimaria(int.Parse(ddlCateg)) : null);
                    tbs368.NU_PLAN_SAUDE = (!string.IsNullOrEmpty(txtNumero) ? int.Parse(txtNumero) : (int?)null);
                    tbs368.NU_GUIA = (!string.IsNullOrEmpty(txtNuGuia) ? txtNuGuia : null);
                    tbs368.NU_AUTOR = (!string.IsNullOrEmpty(txtNuAutor) ? txtNuAutor : null);
                    tbs368.NU_QTDE_SESSO = (!string.IsNullOrEmpty(txtQtde) ? int.Parse(txtQtde) : (int?)null);

                    //======================> Dados do procedimento e valores
                    tbs368.TBS356_PROC_MEDIC_PROCE = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlProc));
                    tbs368.VL_UNIT = (!string.IsNullOrEmpty(vlUnit) ? decimal.Parse(vlUnit) : (decimal?)null);
                    tbs368.VL_DSCTO = (!string.IsNullOrEmpty(txtVlDscto) ? decimal.Parse(txtVlDscto) : (decimal?)null);
                    tbs368.VL_LIQUI = ValorLiquido;

                    //Dados Gerais
                    //tbs368.DT_CADAS

                    //======================> Dados do Cadastro
                    tbs368.DT_CADAS = DateTime.Now;
                    tbs368.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                    tbs368.CO_COL_CADAS = LoginAuxili.CO_COL;
                    tbs368.CO_EMP_COL_CADAS = coEmpColLogado;
                    tbs368.IP_CADAS = Request.UserHostAddress;

                    //======================> Dados da Situação
                    tbs368.CO_SITUA = "A";
                    tbs368.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                    tbs368.CO_COL_SITUA = LoginAuxili.CO_COL;
                    tbs368.CO_EMP_COL_SITUA = coEmpColLogado;
                    tbs368.IP_SITUA = Request.UserHostAddress;
                    tbs368.DT_SITUA = DateTime.Now;

                    TBS368_RECEP_SOLIC_ITENS.SaveOrUpdate(tbs368);

                    #endregion

                    //Salva objeto da entidade tbs369 que armazena os itens solicitados em em regulação na recepção
                    #region Salva Entidade tbs369

                    TBS369_RECEP_REGUL_ITENS tbs369 = new TBS369_RECEP_REGUL_ITENS();

                    //======================> Dados Gerais
                    tbs369.TBS368_RECEP_SOLIC_ITENS = tbs368;
                    tbs369.TBS367_RECEP_SOLIC = tbs367;
                    tbs369.NU_REGIS_RECEP_SOLIC = tbs367.NU_REGIS_RECEP_SOLIC;

                    //======================> Dados do Procedimento e Valores
                    tbs369.TBS356_PROC_MEDIC_PROCE = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(int.Parse(ddlProc));
                    tbs369.VL_UNIT = (!string.IsNullOrEmpty(txtVlTotal) ? decimal.Parse(txtVlTotal) : (decimal?)null);
                    tbs369.VL_DSCTO = (!string.IsNullOrEmpty(txtVlDscto) ? decimal.Parse(txtVlDscto) : (decimal?)null);
                    tbs369.VL_LIQUI = ValorLiquido;

                    tbs369.NU_QTDE_SESSO_GUIA = (!string.IsNullOrEmpty(txtQtde) ? int.Parse(txtQtde) : (int?)null);

                    //======================> Dados do Cadastro
                    tbs369.DT_CADAS = DateTime.Now;
                    tbs369.CO_EMP_CADAS = LoginAuxili.CO_EMP;
                    tbs369.CO_COL_CADAS = LoginAuxili.CO_COL;
                    tbs369.CO_EMP_COL_CADAS = coEmpColLogado;
                    tbs369.IP_CADAS = Request.UserHostAddress;

                    //======================> Dados da Situação
                    tbs369.CO_SITUA = "A";
                    tbs369.CO_EMP_SITUA = LoginAuxili.CO_EMP;
                    tbs369.CO_COL_SITUA = LoginAuxili.CO_COL;
                    tbs369.CO_EMP_COL_SITUA = coEmpColLogado;
                    tbs369.IP_SITUA = Request.UserHostAddress;
                    tbs369.DT_SITUA = DateTime.Now;

                    TBS369_RECEP_REGUL_ITENS.SaveOrUpdate(tbs369, true);

                    #endregion
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Valida se todas as informações das solicitações foram preenchidas
        /// </summary>
        /// <returns>FALSE quando há críticas, TRUE quando está tudo preenchido</returns>
        private bool ValidaSolicitacoes()
        {
            int qt = 1;
            foreach (GridViewRow li in grdSolicitacoes.Rows)
            {
                DropDownList ddlProc;
                TextBox txtQtde;
                txtQtde = ((TextBox)li.Cells[6].FindControl("txtQtde"));
                ddlProc = ((DropDownList)li.Cells[7].FindControl("ddlProcedimento"));

                if (string.IsNullOrEmpty(txtQtde.Text))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar a quantidade de sessões no item nº " + qt);
                    txtQtde.Focus();
                    return false;
                }

                if (string.IsNullOrEmpty(ddlProc.SelectedValue))
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar o procedimento solicitado no item nº " + qt);
                    ddlProc.Focus();
                    return false;
                }

                qt++;
            }
            return true; //Se chegar até aqui, então não houve nenhumna crítica
        }

        /// <summary>
        /// Método responsável por carregar os médicos plantonistas na grid correspondente
        /// </summary>
        /// <param name="CO_ESPEC"></param>
        private void carregaGridMedicosPlantonistas(string CLASS_PROFI = "0", int CO_DEPTO = 0)
        {
            var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                       join tb14 in TB14_DEPTO.RetornaTodosRegistros() on tb03.CO_DEPTO equals tb14.CO_DEPTO into l1
                       from ls in l1.DefaultIfEmpty()
                       where tb03.FL_PROFI_AVALI == "S"
                       && tb03.CO_SITU_COL == "ATI"
                       select new MedicosPlantonistas
                       {
                           NO_COL_V = tb03.NO_COL,
                           co_col = tb03.CO_COL,
                           co_emp_col_pla = tb03.CO_EMP,
                           CO_CLASS_PROFI = tb03.CO_CLASS_PROFI,
                           LOCAL = ls.CO_SIGLA_DEPTO,
                           CO_ESPEC = tb03.CO_ESPEC,
                       }).OrderBy(w => w.NO_COL_V).ToList();

            if (res.Count > 0)
            {
                grdMedicosPlanto.DataSource = res;
                grdMedicosPlanto.DataBind();
            }
            else
            {
                grdMedicosPlanto.DataSource = null;
                grdMedicosPlanto.DataBind();
            }
        }

        /// <summary>
        /// Carrega as funções simplificadas
        /// </summary>
        private void CarregarFuncoesSimp()
        {
            AuxiliCarregamentos.CarregaFuncoesSimplificadas(ddlFuncao, false);
        }

        #region Classes de Saída

        /// <summary>
        /// Preenche a Grid de Consultas com os registros previamente cadastrados na funcionalidade e processo de registro de consulta.
        /// </summary>
        public class Consultas
        {
            public int ANTIGOS { get; set; }

            public string NO_COL { get; set; }
            public int CO_ALU { get; set; }
            public string NO_PAC
            {
                get
                {
                    return (this.NO_PAC_RECEB.Length > 25 ? this.NO_PAC_RECEB.Substring(0, 25) + "..." : this.NO_PAC_RECEB);
                }
            }
            public string NO_PAC_RECEB { get; set; }
            public DateTime? dt_nascimento { get; set; }
            public string NU_IDADE
            {
                get
                {
                    string idade = " - ";

                    //Calcula a idade do Paciente de acordo com a data de nascimento do mesmo.
                    if (this.dt_nascimento.HasValue)
                    {
                        int anos = DateTime.Now.Year - dt_nascimento.Value.Year;

                        if (DateTime.Now.Month < dt_nascimento.Value.Month || (DateTime.Now.Month == dt_nascimento.Value.Month && DateTime.Now.Day < dt_nascimento.Value.Day))
                            anos--;

                        idade = anos.ToString();
                    }
                    return idade;
                }
            }
            public string CO_SEXO { get; set; }

            public int? CO_COL { get; set; }
            public int CO_AGEND_MEDIC { get; set; }

            //Carrega informações gerais do agendamento
            public DateTime dt_Consul { get; set; }
            public string hr_Consul { get; set; }
            public string hora
            {
                get
                {
                    return this.dt_Consul.ToString("dd/MM/yy") + " - " + this.hr_Consul;
                }
            }
            public string CO_CLASS_PROFI { get; set; }
            public string NO_CLASS_PROFI
            {
                get
                {
                    return AuxiliGeral.GetNomeClassificacaoFuncional(CO_CLASS_PROFI, false);
                }
            }
            public int CO_ESPEC { get; set; }

            public string FL_CONF { get; set; }
            public bool FL_CONF_VALID
            {
                get
                {
                    return this.FL_CONF == "S" ? true : false;
                }
            }
            public string CO_SITU { get; set; }
            public string CO_SITU_VALID
            {
                get
                {
                    string situacao = "";
                    switch (this.CO_SITU)
                    {
                        case "A":
                            situacao = "Aberto";
                            break;
                        case "C":
                            situacao = "Cancelado";
                            break;
                        case "I":
                            situacao = "Inativo";
                            break;
                        case "S":
                            situacao = "Suspenso";
                            break;
                    }

                    return situacao;
                }
            }
            public string TP_CONSUL { get; set; }
            public string TP_CONSUL_VALID
            {
                get
                {
                    string tipo = "";
                    switch (this.TP_CONSUL)
                    {
                        case "N":
                            tipo = "Normal";
                            break;
                        case "R":
                            tipo = "Retorno";
                            break;
                        case "U":
                            tipo = "Urgência";
                            break;
                        default:
                            tipo = " - ";
                            break;
                    }
                    return tipo;
                }
            }
        }

        /// <summary>
        /// Classe de saída para a Grid de Médicos
        /// </summary>
        public class MedicosPlantonistas
        {
            public string NO_COL_V { get; set; }
            public string NO_COL
            {
                get
                {
                    return (this.NO_COL_V.Length > 21 ? this.NO_COL_V.Substring(0, 21) + "..." : this.NO_COL_V);
                }
            }
            public int co_col { get; set; }
            public int co_emp_col_pla { get; set; }
            public string CO_CLASS_PROFI { get; set; }
            public string NO_CLASS_PROFI
            {
                get
                {
                    return AuxiliGeral.GetNomeClassificacaoFuncional(this.CO_CLASS_PROFI);
                }
            }
            public int? CO_ESPEC { get; set; }
            public string CO_TIPO_RISCO { get; set; }
            public string LOCAL { get; set; }
        }

        #endregion

        /// <summary>
        /// Carrega as Cidades relacionadas ao Bairro selecionado anteriormente.
        /// </summary>
        private void carregaCidade()
        {
            string uf = ddlUF.SelectedValue;
            AuxiliCarregamentos.CarregaCidades(ddlCidade, false, uf, LoginAuxili.CO_EMP, true, true);
        }

        /// <summary>
        /// Carrega os Bairros ligados à UF e Cidade selecionados anteriormente.
        /// </summary>
        private void carregaBairro()
        {
            string uf = ddlUF.SelectedValue;
            int cid = ddlCidade.SelectedValue != "" ? int.Parse(ddlCidade.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaBairros(ddlBairro, uf, cid, false, true, false, LoginAuxili.CO_EMP, true);
        }

        /// <summary>
        /// Carrega as informações da unidade logada em campos definidos
        /// </summary>
        private void CarregaDadosUnidLogada()
        {
            var res = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);
            res.TB83_PARAMETROReference.Load();

            //Verifica se existe integração com o financeiro
            if (res.TB83_PARAMETRO != null)
                chkRespFinanc.Visible = res.TB83_PARAMETRO.FL_INTEG_FINAN == "S" ? true : false;

            txtCEP.Text = txtCEP_PADRAO.Text = res.CO_CEP_EMP;
            txtLograEndResp.Text = txtLograEndResp_PADRAO.Text = res.DE_END_EMP;
        }
        
        /// <summary>
        /// Método responsável por receber os valores por parâmetros e inserir nos campos correspondentes
        /// </summary>
        /// <param name="cpf"></param>
        /// <param name="Nome"></param>
        /// <param name="sexo"></param>
        /// <param name="dtNasc"></param>
        /// <param name="RG"></param>
        /// <param name="ORGrg"></param>
        /// <param name="UFrg"></param>
        /// <param name="TelFixo"></param>
        /// <param name="TelCelu"></param>
        /// <param name="TelCome"></param>
        /// <param name="Whats"></param>
        /// <param name="Face"></param>
        /// <param name="CEP"></param>
        /// <param name="UF"></param>
        /// <param name="Cidade"></param>
        /// <param name="Bairro"></param>
        /// <param name="Logradouro"></param>
        /// <param name="Email"></param>
        private void CarregarDadosResponsavel(string cpf, string Nome, string sexo, DateTime dtNasc, string RG,
            string ORGrg, string UFrg, string TelFixo, string TelCelu, string TelCome, string Whats, string Face,
            string CEP, string UF, int Cidade, int? Bairro, string Logradouro, string Email)
        {
            txtCPFResp.Text = cpf;
            txtNomeResp.Text = Nome;
            ddlSexResp.SelectedValue = sexo;
            txtDtNascResp.Text = dtNasc.ToString();
            txtNuIDResp.Text = RG;
            txtOrgEmiss.Text = ORGrg;
            ddlUFOrgEmis.SelectedValue = UFrg;
            txtTelFixResp.Text = TelFixo;
            txtTelCelResp.Text = TelCelu;
            txtTelComResp.Text = TelCome;
            txtNuWhatsResp.Text = Whats;
            txtCEP.Text = CEP;
            ddlUF.SelectedValue = UF;
            carregaCidade();
            ddlCidade.SelectedValue = (Cidade != 0 ? Cidade.ToString() : "");
            carregaBairro();
            ddlBairro.SelectedValue = (Bairro != 0 && Cidade != 0 ? Bairro.ToString() : "");
            txtLograEndResp.Text = Logradouro;
            txtEmailResp.Text = Email;
        }

        /// <summary>
        /// Carrega as Informações de Responsável e Paciente, de acordo com o registro que é clicado na Grid de Pré-Atendimentos.
        /// </summary>
        /// <param name="ID_PRE_ATEND"></param>
        private void CarregaCampos(int ID_AGENDA)
        {
            var tbs174 = TBS174_AGEND_HORAR.RetornaPelaChavePrimaria(ID_AGENDA);
            var tb07 = TB07_ALUNO.RetornaPeloCoAlu(tbs174.CO_ALU.Value);
            tb07.TB108_RESPONSAVELReference.Load();
            var tb108 = (tb07.TB108_RESPONSAVEL != null ? tb07.TB108_RESPONSAVEL : null);

            hidCoPac.Value = tbs174.CO_ALU.ToString();

            //Carrega essas informações apenas se o paciente da consulta tiver um responsável associado 
            if (tb108 != null)
            {
                tb108.TB904_CIDADEReference.Load();
                tb108.TB904_CIDADE1Reference.Load();

                txtCPFResp.Text = tb108.NU_CPF_RESP;
                txtNomeResp.Text = tb108.NO_RESP.ToUpper();
                txtDtNascResp.Text = tb108.DT_NASC_RESP.ToString();
                ddlSexResp.SelectedValue = tb108.CO_SEXO_RESP;
                txtTelFixResp.Text = tb108.NU_TELE_RESI_RESP;
                txtTelCelResp.Text = tb108.NU_TELE_CELU_RESP;

                CarregarDadosResponsavel(tb108.NU_CPF_RESP, tb108.NO_RESP, tb108.CO_SEXO_RESP, tb108.DT_NASC_RESP.Value, tb108.CO_RG_RESP
                           , tb108.CO_ORG_RG_RESP, tb108.CO_ESTA_RG_RESP, tb108.NU_TELE_RESI_RESP, tb108.NU_TELE_CELU_RESP,
                           tb108.NU_TELE_COME_RESP, tb108.NU_TELE_WHATS_RESP, tb108.NM_FACEBOOK_RESP, tb108.CO_CEP_RESP,
                           tb108.CO_ESTA_RESP, (tb108.CO_CIDADE.HasValue ? tb108.CO_CIDADE.Value : 0), tb108.CO_BAIRRO, tb108.DE_ENDE_RESP, "");
            }
            else
            {
            }

            //Carrega as informações do Paciente
            txtnompac.Text = tb07.NO_ALU.ToUpper();
            txtCpfPaci.Text = tb07.NU_CPF_ALU;
            txtNuNisPaci.Text = tb07.NU_NIS.ToString().PadLeft(7, '0');
            txtTelResPaci.Text = tb07.NU_TELE_RESI_ALU;
            txtTelCelPaci.Text = tb07.NU_TELE_CELU_ALU;
            //chkPesqCPFUsu.Checked = true;
            txtDtNascPaci.Text = tb07.DT_NASC_ALU.ToString();
            ddlSexoPaci.SelectedValue = tb07.CO_SEXO_ALU;
            ddlGrParen.SelectedValue = tb07.CO_GRAU_PAREN_RESP;

            upImagemAluno.ImagemLargura = 70;
            upImagemAluno.ImagemAltura = 85;
            upImagemAluno.MostraProcurar = false;

            tb07.TB250_OPERAReference.Load();
            //if (tb07.TB250_OPERA != null)
            //    ddlOperPlano.SelectedValue = tb07.TB250_OPERA.ID_OPER.ToString();
            //else
            //    ddlOperPlano.SelectedValue = "";

            #region Instancia objeto da entidade para mostrar a foto correspondente

            string cpfPac = txtCpfPaci.Text.Replace("-", "").Replace(".", "").Trim();
            var realu = (from tb07li in TB07_ALUNO.RetornaTodosRegistros()
                         where tb07li.NU_CPF_ALU == cpfPac
                         select new { tb07li.CO_ALU }).FirstOrDefault();

            int? paExis = (realu != null ? realu.CO_ALU : (int?)null);

            Decimal nis = 0;
            if (!string.IsNullOrEmpty(txtNuNisPaci.Text))
            {
                nis = decimal.Parse(txtNuNisPaci.Text.Trim());
            }

            var realu2 = (from tb07ob in TB07_ALUNO.RetornaTodosRegistros()
                          where tb07ob.NU_NIS == nis
                          select new { tb07ob.CO_ALU }).FirstOrDefault();

            int? paExisNis = (realu2 != null ? realu2.CO_ALU : (int?)null);

            if ((!paExis.HasValue) && (!paExisNis.HasValue))
                upImagemAluno.CarregaImagem(0);
            else
            {
                int coPac = (paExis.HasValue ? paExis.Value : paExisNis.Value);
                var resupac = TB07_ALUNO.RetornaTodosRegistros().Where(w => w.CO_ALU == coPac).FirstOrDefault();
            }

            #endregion
        }

        /// <summary>
        /// Método que duplica as informações do responsável nos campos do paciente, quando o usuário clica em Paciente é o Responsável.
        /// </summary>
        private void carregaPaciehoResponsavel()
        {
            if (chkPaciEhResp.Checked)
            {
                //Se tiver cpf no paciente, carrega no responsável, se não tiver, deixa os 000...
                txtCPFResp.Text = (!string.IsNullOrEmpty(txtCpfPaci.Text) ? txtCpfPaci.Text : txtCPFResp.Text);
                txtNomeResp.Text = txtnompac.Text;
                txtDtNascResp.Text = txtDtNascPaci.Text;
                ddlSexResp.SelectedValue = ddlSexoPaci.SelectedValue;
                txtTelCelResp.Text = txtTelCelPaci.Text;
                txtTelFixResp.Text = txtTelResPaci.Text;
                ddlGrParen.SelectedValue = "OU";
                txtEmailResp.Text = txtEmailPaci.Text;
                txtNuWhatsResp.Text = txtWhatsPaci.Text;

                PesquisaCarregaResp((int?)null, txtCPFResp.Text);

                //txtEmailPaci.Enabled = false;
                //txtCPFMOD.Enabled = false;
                //txtnompac.Enabled = false;
                //txtDtNascPaci.Enabled = false;
                //ddlSexoPaci.Enabled = false;
                //txtTelCelPaci.Enabled = false;
                //txtTelCelPaci.Enabled = false;
                //txtTelResPaci.Enabled = false;
                //ddlGrParen.Enabled = false;
                //txtWhatsPaci.Enabled = false;

                #region Verifica se já existe

                string cpf = txtCpfPaci.Text.Replace(".", "").Replace("-", "").Trim();

                var tb07 = TB07_ALUNO.RetornaTodosRegistros().Where(w => w.NU_CPF_ALU == cpf).FirstOrDefault();

                if (tb07 != null)
                    hidCoPac.Value = tb07.CO_ALU.ToString();

                #endregion

            }
            else
            {
                txtCPFResp.Text = "000.000.000-00";
                txtNomeResp.Text = "";
                txtDtNascResp.Text = "01/01/1900";
                ddlSexResp.SelectedValue = "";
                txtTelCelResp.Text = "";
                txtTelFixResp.Text = "";
                txtEmailResp.Text = "";
                txtNuWhatsResp.Text = "";

                //txtCPFMOD.Enabled = true;
                //txtnompac.Enabled = true;
                //txtDtNascPaci.Enabled = true;
                //ddlSexoPaci.Enabled = true;
                //txtTelCelPaci.Enabled = true;
                //txtTelCelPaci.Enabled = true;
                //txtTelResPaci.Enabled = true;
                //ddlGrParen.Enabled = true;
                //txtEmailPaci.Enabled = true;
                //txtWhatsPaci.Enabled = true;
                //hidCoPac.Value = "";
            }
            //updCadasUsuario.Update();
        }

        /// <summary>
        /// Pesquisa se já existe um responsável com o CPF informado na tabela de responsáveis, se já existe ele preenche as informações do responsável 
        /// </summary>
        private void PesquisaCarregaResp(int? co_resp, string cpfRespParam = null)
        {
            string cpfResp = (string.IsNullOrEmpty(cpfRespParam) ?
                txtCPFResp.Text.Replace(".", "").Replace("-", "") : cpfRespParam.Replace(".", "").Replace("-", ""));

            var res = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                       where (co_resp.HasValue ? tb108.CO_RESP == co_resp : tb108.NU_CPF_RESP == cpfResp)
                       select tb108).FirstOrDefault();

            if (res != null)
            {
                txtNomeResp.Text = res.NO_RESP.ToUpper();
                txtCPFResp.Text = res.NU_CPF_RESP;
                txtNuIDResp.Text = res.CO_RG_RESP;
                txtOrgEmiss.Text = res.CO_ORG_RG_RESP;
                ddlUFOrgEmis.SelectedValue = res.CO_ESTA_RG_RESP;
                txtDtNascResp.Text = res.DT_NASC_RESP.ToString();
                ddlSexResp.SelectedValue = res.CO_SEXO_RESP;
                //txtCEP.Text = res.CO_CEP_RESP;
                //ddlUF.SelectedValue = res.CO_ESTA_RESP;
                //carregaCidade();
                //ddlCidade.SelectedValue = res.CO_CIDADE != null ? res.CO_CIDADE.ToString() : "";
                //carregaBairro();
                //ddlBairro.SelectedValue = res.CO_BAIRRO != null ? res.CO_BAIRRO.ToString() : "";
                //txtLograEndResp.Text = res.DE_ENDE_RESP;
                txtEmailResp.Text = res.DES_EMAIL_RESP;
                txtTelCelResp.Text = res.NU_TELE_CELU_RESP;
                txtTelFixResp.Text = res.NU_TELE_RESI_RESP;
                txtNuWhatsResp.Text = res.NU_TELE_WHATS_RESP;
                txtTelComResp.Text = res.NU_TELE_COME_RESP;
                txtDeFaceResp.Text = res.NM_FACEBOOK_RESP;
                hidCoResp.Value = res.CO_RESP.ToString();
                this.lblComRestricao.Visible = (res.FL_RESTR_PLANO_SAUDE == "S" ? true : false);
                this.lblSemRestricao.Visible = (res.FL_RESTR_PLANO_SAUDE != "S" ? true : false);

                res.TBS366_FUNCAO_SIMPLReference.Load();
                if (res.TBS366_FUNCAO_SIMPL != null)
                    ddlFuncao.SelectedValue = res.TBS366_FUNCAO_SIMPL.ID_FUNCAO_SIMPL.ToString();

                CarregarResponsavelCmRestricao();
            }
            ExecutaJavaScript();
        }

        /// <summary>
        /// Pesquisa se já existe um Paciente com o CPF informado na tabela de Pacientes, se já existe ele preenche as informações do Paciente 
        /// </summary>
        private void PesquisaCarregaPaci()
        {
            string cpfPaci = (!string.IsNullOrEmpty(txtCpfPaci.Text) ? txtCpfPaci.Text.Replace(".", "").Replace("-", "") : string.Empty);
            decimal? nis = (!string.IsNullOrEmpty(txtNuNisPaci.Text) ? decimal.Parse(txtNuNisPaci.Text) : (decimal?)null);
            int? prontuario = (!string.IsNullOrEmpty(txtNuProntuario.Text) ? int.Parse(txtNuProntuario.Text) : (int?)null);

            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       where (rdbPesqCPF.Checked ? tb07.NU_CPF_ALU == cpfPaci : 0 == 0)
                       && (rdbPesqNIS.Checked && nis.HasValue ? tb07.NU_NIS == nis.Value : 0 == 0)
                       && (rdbPesqProntuario.Checked && prontuario.HasValue ? tb07.NU_NIRE == prontuario : 0 == 0)
                       select tb07).FirstOrDefault();

            if (res != null)
            {
                txtnompac.Text = res.NO_ALU.ToUpper();
                txtCpfPaci.Text = res.NU_CPF_ALU;
                txtNuNisPaci.Text = res.NU_NIS.ToString();
                txtDtNascPaci.Text = res.DT_NASC_ALU.ToString();
                ddlSexoPaci.SelectedValue = res.CO_SEXO_ALU;
                txtTelCelPaci.Text = res.NU_TELE_CELU_ALU;
                txtTelResPaci.Text = res.NU_TELE_RESI_ALU;
                ddlGrParen.SelectedValue = res.CO_GRAU_PAREN_RESP;
                txtEmailPaci.Text = res.NO_EMAIL_PAI;
                txtWhatsPaci.Text = res.NU_TELE_WHATS_ALU;
                hidCoPac.Value = res.CO_ALU.ToString();
                ddlEtniaAlu.SelectedValue = res.TP_RACA;

                txtLograEndResp.Text = txtLograEndResp_PADRAO.Text = res.DE_ENDE_ALU;
                ddlUF.SelectedValue = res.CO_ESTA_ALU;
                txtCEP.Text = txtCEP_PADRAO.Text = res.CO_CEP_ALU;
                res.TB905_BAIRROReference.Load();

                carregaCidade();
                ddlCidade.SelectedValue = res.TB905_BAIRRO != null ? res.TB905_BAIRRO.CO_CIDADE.ToString() : "";
                carregaBairro();
                ddlBairro.SelectedValue = res.TB905_BAIRRO != null ? res.TB905_BAIRRO.CO_BAIRRO.ToString() : "";

                //if (res.TB905_BAIRRO != null)
                //{
                //    carregaCidade();
                //    res.TB905_BAIRRO.TB904_CIDADEReference.Load();
                //    if (res.TB905_BAIRRO.TB904_CIDADE != null)
                //    {
                //        ListItem it1 = ddlCidade.Items.FindByValue(res.TB905_BAIRRO.TB904_CIDADE.CO_CIDADE.ToString());
                //        if (it1 != null)
                //            ddlCidade.SelectedValue = it1.Value;

                //        carregaBairro();
                //        ListItem it2 = ddlCidade.Items.FindByValue(res.TB905_BAIRRO.CO_BAIRRO.ToString());
                //        if (it2 != null)
                //            ddlCidade.SelectedValue = it2.Value;
                //    }
                //}

                res.ImageReference.Load();
                upImagemAluno.ImagemLargura = 70;
                upImagemAluno.ImagemAltura = 85;

                if (res.Image != null)
                    upImagemAluno.CarregaImagem(res.Image.ImageId);
                else
                    upImagemAluno.CarregaImagem(0);

                res.TB108_RESPONSAVELReference.Load();

                if (res.TB108_RESPONSAVEL != null)
                    PesquisaCarregaResp(res.TB108_RESPONSAVEL.CO_RESP);
            }
        }

        /// <summary>
        /// Devido ao método de reload na grid de Médicos Plantonistas, ela perde a seleção do checkbox, este método coleta 
        /// </summary>
        private void selecionaGridMedicPlan()
        {
            CheckBox chk;
            // Valida se a grid de atividades possui algum registro
            if (grdMedicosPlanto.Rows.Count != 0)
            {
                // Passa por todos os registros da grid de atividades
                foreach (GridViewRow linha in grdMedicosPlanto.Rows)
                {
                    int coColPlantonista = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidcoCol")).Value);
                    int coCol = (int)HttpContext.Current.Session["VL_MEDIC"];

                    if (coColPlantonista == coCol)
                    {
                        chk = (CheckBox)linha.Cells[0].FindControl("chkselect2");
                        chk.Checked = true;
                    }
                }
            }
        }

        /// <summary>
        /// Responsável por executar as funções padrões para quando um registro do Pré-Atendimento for desmarcado pelo usuário
        /// </summary>
        private void GridPreAtendimentoDesmarcada()
        {
            //Guarda a FLAG para saber se o Checkbox está sendo clicado para marcar ou desmarcar, neste caso, ele grava como desmarcada.
            HttpContext.Current.Session.Add("FL_Select_Grid", "N");
            HttpContext.Current.Session.Remove("VL_Agenda_DMB");
            LimpaCampos();
        }

        /// <summary>
        /// Responsável por executar as funções padrões para quando um registro do Pré-Atendimento for desmarcado pelo usuário
        /// </summary>
        private void GridMedicosPlantonistasDesmarcada()
        {
            HttpContext.Current.Session.Remove("CoCol");
            HttpContext.Current.Session.Remove("coEmp");

            //Guarda a FLAG para saber se o Checkbox está sendo clicado para marcar ou desmarcar, neste caso, ele grava como desmarcada.
            HttpContext.Current.Session.Add("FL_Select_Grid_MEDIC", "N");
            HttpContext.Current.Session.Remove("VL_MEDIC");
        }

        /// <summary>
        /// Métodos padrões à serem chamados quando uma linha da grid de pré-atendimento for selecionada
        /// </summary>
        private void GridMedicosPlantonistasSelecionada(string coEspec, int coColPlantonista, int coEmpColPlantonista)
        {
            //Guarda a FLAG para saber se o Checkbox está sendo clicado para marcar ou desmarcar, neste caso, ele grava como para marcar.
            HttpContext.Current.Session.Add("FL_Select_Grid_MEDIC", "S");

            //Guarda o Valor do Médico Plantonista, para fins de posteriormente comparar este valor 
            HttpContext.Current.Session.Add("VL_MEDIC", coColPlantonista);

            HttpContext.Current.Session.Add("CoCol", coColPlantonista);
            HttpContext.Current.Session.Add("coEmp", coEmpColPlantonista);
        }

        /// <summary>
        /// Limpa as informações de todos os campos
        /// </summary>
        private void LimpaCampos()
        {
            txtCPFResp.Text = txtNomeResp.Text = txtNuIDResp.Text = txtOrgEmiss.Text = ddlUFOrgEmis.SelectedValue =
                txtDtNascResp.Text = ddlSexResp.SelectedValue = txtCEP.Text = ddlCidade.SelectedValue
                = ddlBairro.SelectedValue = txtLograEndResp.Text = txtEmailResp.Text = txtTelCelResp.Text = txtTelFixResp.Text
                = txtNuNisPaci.Text = txtCpfPaci.Text = txtDtNascPaci.Text = ddlSexoPaci.SelectedValue
                = txtTelResPaci.Text = txtTelCelPaci.Text = ddlGrParen.SelectedValue = txtEmailPaci.Text
                = ddlUF.SelectedValue = txtnompac.Text = txtNuWhatsResp.Text = txtWhatsPaci.Text = txtDeFaceResp.Text = "";

            ExecutaJavaScript();
        }

        /// <summary>
        /// Executa método javascript que corrige algumas regras faltantes
        /// </summary>
        private void ExecutaJavaScript()
        {
            //ScriptManager.RegisterStartupScript(
            //    this.Page,
            //    this.GetType(),
            //    "Acao",
            //    "carregaPadroes();",
            //    true
            //);
        }

        /// <summary>
        /// Abre mensagem com informações
        /// </summary>
        private void AbreMensagemInfos(string texto)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + texto + "');", true);
            //ScriptManager.RegisterClientScriptBlock(UpdatePanel2, GetType(), "newmsgE", "AbreMensagem('" + texto + "\');", true);
        }

        /// <summary>
        /// Verifica o nire
        /// </summary>
        private void VerificarNireAutomatico()
        {
            string strTipoNireAuto = "";
            //----------------> Variável que guarda informações da unidade selecionada pelo usuário logado
            var tb25 = (from iTb25 in TB25_EMPRESA.RetornaTodosRegistros()
                        where iTb25.CO_EMP == LoginAuxili.CO_EMP
                        select new { iTb25.TB83_PARAMETRO.FLA_GERA_NIRE_AUTO }).First();

            strTipoNireAuto = tb25.FLA_GERA_NIRE_AUTO != null ? tb25.FLA_GERA_NIRE_AUTO : "";

            if (strTipoNireAuto != "")
            {
                //----------------> Faz a verificação para saber se o NIRE é automático ou não
                if (strTipoNireAuto == "N")
                {
                }
                else
                {
                    ///-------------------> Adiciona no campo NU_NIRE o número do último código do aluno + 1
                    int? lastCoAlu = (from lTb07 in TB07_ALUNO.RetornaTodosRegistros()
                                      select lTb07.NU_NIRE == null ? new Nullable<int>() : lTb07.NU_NIRE).Max();
                    if (lastCoAlu != null)
                    {
                        txtNuProntuario.Text = (lastCoAlu.Value + 1).ToString();
                    }
                    else
                        txtNuProntuario.Text = "1";
                }
            }
        }

        #region Carregamentos na Grid de Solicitações

        /// <summary>
        /// Calcula e seta o valor total de um determinado procedimento de acordo com o parâmetro
        /// </summary>
        private void CalculaValorTotalProcedimento(int qt, string vlProcUnit, TextBox txtValor)
        {
            //Identifica o resultado multiplicando as sessões pelo valor unitário
            decimal result = qt * (!string.IsNullOrEmpty(vlProcUnit) ? decimal.Parse(vlProcUnit) : 0);
            //Insere o valor calculado no campo de valor resultado
            txtValor.Text = result.ToString("N2");
        }

        /// <summary>
        /// Percorre a grid de solicitações e totaliza os valores referentes
        /// </summary>
        private void CarregarValoresTotaisFooter()
        {
            decimal VlTotal = 0;
            decimal VlDesconto = 0;
            foreach (GridViewRow li in grdSolicitacoes.Rows)
            {
                //Coleta os valores da linha
                string Valor, Desconto;
                Valor = ((TextBox)li.Cells[8].FindControl("txtVlUnitario")).Text;
                Desconto = ((TextBox)li.Cells[9].FindControl("txtVlDesconto")).Text;

                //Soma os valores com os valores das outras linhas da grid
                VlTotal += (!string.IsNullOrEmpty(Valor) ? decimal.Parse(Valor) : 0);
                VlDesconto += (!string.IsNullOrEmpty(Desconto) ? decimal.Parse(Desconto) : 0);
            }

            decimal vlLiquido = VlTotal - VlDesconto;

            //Seta os valores nos textboxes
            txtVlBaseTotal.Text = VlTotal.ToString("N2");
            txtVlDescontoTotal.Text = VlDesconto.ToString("N2");
            txtVlLiquidoTotal.Text = vlLiquido.ToString("N2");
        }

        /// <summary>
        /// Carrega as Operadoras de saúde
        /// </summary>
        private void CarregarOperadoras(DropDownList ddl)
        {
            AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddl, false, false, true, true);
        }

        /// <summary>
        /// Carrega os planos de saúde da operadora recebida como parâmetro
        /// </summary>
        /// <param name="ddlPlan"></param>
        /// <param name="ddlOper"></param>
        private void CarregarPlanosSaude(DropDownList ddlPlan, DropDownList ddlOper)
        {
            AuxiliCarregamentos.CarregaPlanosSaude(ddlPlan, ddlOper, false, false, true, true);
        }

        /// <summary>
        /// Carrega as categorias dos planos de saúde
        /// </summary>
        /// <param name="ddlCateg"></param>
        /// <param name="ddlPlan"></param>
        private void CarregarCategoriasPlano(DropDownList ddlCateg, DropDownList ddlPlan)
        {
            AuxiliCarregamentos.CarregaCategoriaPlanoSaude(ddlCateg, ddlPlan, false, false, true, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ddl"></param>
        private void CarregarProcedimentosMedicos(DropDownList ddl, DropDownList ddlOper, DropDownList ddlGrupo, DropDownList ddlSubGrupo)
        {
            int idOper = (!string.IsNullOrEmpty(ddlOper.SelectedValue) ? int.Parse(ddlOper.SelectedValue) : 0);
            AuxiliCarregamentos.CarregaProcedimentosMedicos(ddl, ddlGrupo, ddlSubGrupo, idOper, false);
        }

        /// <summary>
        /// Calcula os valores de tabela e calculado de acordo com desconto concedido à determinada operadora e plano de saúde informados no cadastro na página
        /// </summary>
        private void CalcularPreencherValoresTabelaECalculado(DropDownList ddlProcConsul, DropDownList ddlOperPlano, DropDownList ddlPlano, HiddenField hidValorUnitario)
        {
            int idProc = (!string.IsNullOrEmpty(ddlProcConsul.SelectedValue) ? int.Parse(ddlProcConsul.SelectedValue) : 0);
            int idOper = (!string.IsNullOrEmpty(ddlOperPlano.SelectedValue) ? int.Parse(ddlOperPlano.SelectedValue) : 0);
            int idPlan = (!string.IsNullOrEmpty(ddlPlano.SelectedValue) ? int.Parse(ddlPlano.SelectedValue) : 0);

            //Apenas se tiver sido escolhido algum procedimento
            if (idProc != 0)
            {
                int? procAgrupador = (int?)null; // Se for procedimento de alguma operadora, verifica qual o id do procedimento agrupador do mesmo
                if (!string.IsNullOrEmpty(ddlOperPlano.SelectedValue))
                    procAgrupador = TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(idProc).ID_AGRUP_PROC_MEDI_PROCE;

                AuxiliCalculos.ValoresProcedimentosMedicos ob = AuxiliCalculos.RetornaValoresProcedimentosMedicos((procAgrupador.HasValue ? procAgrupador.Value : idProc), idOper, idPlan);
                hidValorUnitario.Value = ob.VL_CALCULADO.ToString("N2"); // Insere o valor Calculado
            }
        }

        /// <summary>
        /// Exclui item da grid recebendo como parâmetro o index correspondente
        /// </summary>
        protected void ExcluiItemGrid(int Index)
        {
            DataTable dtV = new DataTable();
            DataColumn dcATM;

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "OPERADORA";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "PLANO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "CATEGORIA";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "NUMERO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "NUGUIA";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "NUAUTOR";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "QTDE";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "PROCEDIMENTO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "VLUNITARIO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "VLDESCONTO";
            dtV.Columns.Add(dcATM);

            DataRow linha;
            foreach (GridViewRow li in grdSolicitacoes.Rows)
            {
                linha = dtV.NewRow();
                linha["OPERADORA"] = ((DropDownList)li.Cells[0].FindControl("ddlOperadora")).SelectedValue;
                linha["PLANO"] = ((DropDownList)li.Cells[1].FindControl("ddlPlanoSaude")).SelectedValue;
                linha["CATEGORIA"] = ((DropDownList)li.Cells[2].FindControl("ddlCategoria")).SelectedValue;
                linha["NUMERO"] = ((TextBox)li.Cells[3].FindControl("txtNumeroPlano")).Text;
                linha["NUGUIA"] = ((TextBox)li.Cells[4].FindControl("txtNuGuia")).Text;
                linha["NUAUTOR"] = ((TextBox)li.Cells[5].FindControl("txtNuAutor")).Text;
                linha["QTDE"] = ((TextBox)li.Cells[6].FindControl("txtQtde")).Text;
                linha["PROCEDIMENTO"] = ((DropDownList)li.Cells[7].FindControl("ddlProcedimento")).SelectedValue;
                linha["VLUNITARIO"] = ((TextBox)li.Cells[8].FindControl("txtVlUnitario")).Text;
                linha["VLDESCONTO"] = ((TextBox)li.Cells[9].FindControl("txtVlDesconto")).Text;
                dtV.Rows.Add(linha);
            }

            dtV.Rows.RemoveAt(Index); // Exclui o item de index correspondente
            Session["GridSolic"] = dtV;

            carregaGridNovaComContexto();
            //grdChequesPgto.DataSource = dt;
            //grdChequesPgto.DataBind();
        }

        /// <summary>
        /// Cria uma nova linha na forma de pagamento cheque
        /// </summary>
        protected void CriaNovaLinhaGridSolicitacoes()
        {
            DataTable dtV = new DataTable();
            DataColumn dcATM;

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "OPERADORA";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "PLANO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "CATEGORIA";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "NUMERO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "NUGUIA";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "NUAUTOR";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "QTDE";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "PROCEDIMENTO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "VLUNITARIO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "VLDESCONTO";
            dtV.Columns.Add(dcATM);

            DataRow linha;
            foreach (GridViewRow li in grdSolicitacoes.Rows)
            {
                linha = dtV.NewRow();
                linha["OPERADORA"] = ((DropDownList)li.Cells[0].FindControl("ddlOperadora")).SelectedValue;
                linha["PLANO"] = ((DropDownList)li.Cells[1].FindControl("ddlPlanoSaude")).SelectedValue;
                linha["CATEGORIA"] = ((DropDownList)li.Cells[2].FindControl("ddlCategoria")).SelectedValue;
                linha["NUMERO"] = ((TextBox)li.Cells[3].FindControl("txtNumeroPlano")).Text;
                linha["NUGUIA"] = ((TextBox)li.Cells[4].FindControl("txtNuGuia")).Text;
                linha["NUAUTOR"] = ((TextBox)li.Cells[5].FindControl("txtNuAutor")).Text;
                linha["QTDE"] = ((TextBox)li.Cells[6].FindControl("txtQtde")).Text;
                linha["PROCEDIMENTO"] = ((DropDownList)li.Cells[7].FindControl("ddlProcedimento")).SelectedValue;
                linha["VLUNITARIO"] = ((TextBox)li.Cells[8].FindControl("txtVlUnitario")).Text;
                linha["VLDESCONTO"] = ((TextBox)li.Cells[9].FindControl("txtVlDesconto")).Text;
                dtV.Rows.Add(linha);
            }

            linha = dtV.NewRow();
            linha["OPERADORA"] = "";
            linha["PLANO"] = "";
            linha["CATEGORIA"] = "";
            linha["NUMERO"] = "";
            linha["NUGUIA"] = "";
            linha["NUAUTOR"] = "";
            linha["QTDE"] = "";
            linha["PROCEDIMENTO"] = "";
            linha["VLUNITARIO"] = "";
            linha["VLDESCONTO"] = "";
            dtV.Rows.Add(linha);

            Session["GridSolic"] = dtV;

            carregaGridNovaComContexto();
            //grdChequesPgto.DataSource = dt;
            //grdChequesPgto.DataBind();
        }

        /// <summary>
        /// Carrega o a DataTable em Session com as informações anteriores e a nova linha.
        /// </summary>
        protected void carregaGridNovaComContexto()
        {
            DataTable dtV = new DataTable();

            dtV = (DataTable)Session["GridSolic"];

            grdSolicitacoes.DataSource = dtV;
            grdSolicitacoes.DataBind();

            int aux = 0;
            foreach (GridViewRow li in grdSolicitacoes.Rows)
            {
                DropDownList ddlOper, ddlPlan, ddlCateg, ddlProc;
                TextBox txtNumeroPlano, txtNuGuia, txtNuAutor, txtQtde, txtVlUnitario, txtVlDesconto;
                ddlOper = (DropDownList)li.Cells[0].FindControl("ddlOperadora");
                ddlPlan = (DropDownList)li.Cells[1].FindControl("ddlPlanoSaude");
                ddlCateg = (DropDownList)li.Cells[2].FindControl("ddlCategoria");
                txtNumeroPlano = (TextBox)li.Cells[3].FindControl("txtNumeroPlano");
                txtNuGuia = (TextBox)li.Cells[4].FindControl("txtNuGuia");
                txtNuAutor = (TextBox)li.Cells[5].FindControl("txtNuAutor");
                txtQtde = (TextBox)li.Cells[6].FindControl("txtQtde");
                ddlProc = (DropDownList)li.Cells[7].FindControl("ddlProcedimento");
                txtVlUnitario = (TextBox)li.Cells[8].FindControl("txtVlUnitario");
                txtVlDesconto = (TextBox)li.Cells[9].FindControl("txtVlDesconto");

                string Operadora, Plano, Categoria, nuPlano, nuGuia, nuAutor, nuQtde, Procedimento, vlUnit, vlDscto;

                //Coleta os valores do dtv
                Operadora = dtV.Rows[aux]["OPERADORA"].ToString();
                Plano = dtV.Rows[aux]["PLANO"].ToString();
                Categoria = dtV.Rows[aux]["CATEGORIA"].ToString();
                nuPlano = dtV.Rows[aux]["NUMERO"].ToString();
                nuGuia = dtV.Rows[aux]["NUGUIA"].ToString();
                nuAutor = dtV.Rows[aux]["NUAUTOR"].ToString();
                nuQtde = dtV.Rows[aux]["QTDE"].ToString();
                Procedimento = dtV.Rows[aux]["PROCEDIMENTO"].ToString();
                vlUnit = dtV.Rows[aux]["VLUNITARIO"].ToString();
                vlDscto = dtV.Rows[aux]["VLDESCONTO"].ToString();

                CarregarOperadoras(ddlOper);
                //Seta os valores nos controles
                ddlOper.SelectedValue = Operadora;
                CarregarPlanosSaude(ddlPlan, ddlOper); // Carrega os planos de acordo com a operadora
                ddlPlan.SelectedValue = Plano;
                CarregarCategoriasPlano(ddlCateg, ddlPlan); // Carrega as categorias de plano de saúde de acordo com o plano
                ddlCateg.SelectedValue = Categoria;
                txtNumeroPlano.Text = nuPlano;
                txtNuGuia.Text = nuGuia;
                txtNuAutor.Text = nuAutor;
                txtQtde.Text = nuQtde;
                txtVlUnitario.Text = vlUnit;
                txtVlDesconto.Text = vlDscto;
                CarregarProcedimentosMedicos(ddlProc, ddlOper, ddlGrupoProc, ddlSubGrupo);
                ddlProc.SelectedValue = Procedimento;

                aux++;
            }
        }

        /// <summary>
        /// Carrega a Grid de Cheques da aba de Formas de Pagamento
        /// </summary>
        protected void carregaGridSolicitacoes()
        {
            DataTable dtV = new DataTable();
            DataColumn dcATM;

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "OPERADORA";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "PLANO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "CATEGORIA";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "NUMERO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "NUGUIA";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "NUAUTOR";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "QTDE";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "PROCEDIMENTO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "VLUNITARIO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "VLDESCONTO";
            dtV.Columns.Add(dcATM);

            int i = 1;
            DataRow linha;
            while (i <= 1)
            {
                linha = dtV.NewRow();
                linha["OPERADORA"] = "";
                linha["PLANO"] = "";
                linha["CATEGORIA"] = "";
                linha["NUMERO"] = "";
                linha["NUGUIA"] = "";
                linha["NUAUTOR"] = "";
                linha["QTDE"] = "";
                linha["PROCEDIMENTO"] = "";
                linha["VLUNITARIO"] = "";
                linha["VLDESCONTO"] = "";
                dtV.Rows.Add(linha);
                i++;
            }

            HttpContext.Current.Session.Add("GridSolic", dtV);

            grdSolicitacoes.DataSource = dtV;
            grdSolicitacoes.DataBind();

            foreach (GridViewRow li in grdSolicitacoes.Rows)
            {
                DropDownList ddlOper, ddlPlan, ddlCateg, ddlProc;
                ddlOper = (DropDownList)li.Cells[0].FindControl("ddlOperadora");
                ddlPlan = (DropDownList)li.Cells[1].FindControl("ddlPlanoSaude");
                ddlCateg = (DropDownList)li.Cells[2].FindControl("ddlCategoria");
                ddlProc = (DropDownList)li.Cells[7].FindControl("ddlProcedimento");
                CarregarOperadoras(ddlOper);
                CarregarPlanosSaude(ddlPlan, ddlOper);
                CarregarCategoriasPlano(ddlCateg, ddlPlan);
                CarregarProcedimentosMedicos(ddlProc, ddlOper, ddlGrupoProc, ddlSubGrupo);
            }
        }

        /// <summary>
        /// Trata a grid de solicitações para que, ao selecionar o responsável, seja verificado se possui restrição ou não, e caso possua, bloqueia as opções de plano de saúde
        /// </summary>
        protected void CarregarResponsavelCmRestricao()
        {
            foreach (GridViewRow li in grdSolicitacoes.Rows)
            {
                DropDownList ddlOper, ddlPlan, ddlCateg;
                ddlOper = (DropDownList)li.Cells[0].FindControl("ddlOperadora");
                ddlPlan = (DropDownList)li.Cells[1].FindControl("ddlPlanoSaude");
                ddlCateg = (DropDownList)li.Cells[2].FindControl("ddlCategoria");

                ddlOper.Enabled = ddlPlan.Enabled = ddlCateg.Enabled = !lblComRestricao.Visible;
                ddlOper.SelectedValue = ddlPlan.SelectedValue = ddlCateg.SelectedValue = "";
            }
        }

        #endregion

        #endregion

        #region Funções de Campo

        protected void ddlOperadora_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList atual = (DropDownList)sender;
            DropDownList ddl;

            if (grdSolicitacoes.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdSolicitacoes.Rows)
                {
                    ddl = (DropDownList)linha.Cells[0].FindControl("ddlOperadora");
                    DropDownList ddlPlan = (DropDownList)linha.Cells[1].FindControl("ddlPlanoSaude");
                    DropDownList ddlProc = (DropDownList)linha.Cells[7].FindControl("ddlProcedimento");

                    //Carrega os planos de saúde da operadora quando encontra o objeto que invocou o postback
                    if (ddl.ClientID == atual.ClientID)
                    {
                        CarregarPlanosSaude(ddlPlan, ddl);
                        CarregarProcedimentosMedicos(ddlProc, ddl, ddlGrupoProc, ddlSubGrupo);
                    }
                }
            }
        }

        protected void ddlPlanoSaude_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList atual = (DropDownList)sender;
            DropDownList ddlPlan, ddlCateg;

            if (grdSolicitacoes.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdSolicitacoes.Rows)
                {
                    ddlCateg = (DropDownList)linha.Cells[2].FindControl("ddlCategoria");
                    ddlPlan = (DropDownList)linha.Cells[1].FindControl("ddlPlanoSaude");

                    //Carrega as categorias do plano quando encontra o objeto que invocou o postback
                    if (ddlPlan.ClientID == atual.ClientID)
                        CarregarCategoriasPlano(ddlCateg, ddlPlan);
                }
            }
        }

        protected void ddlProcedimento_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList atual = (DropDownList)sender;
            DropDownList ddlOper, ddlPlan, ddlProc;

            if (grdSolicitacoes.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdSolicitacoes.Rows)
                {
                    ddlOper = (DropDownList)linha.Cells[0].FindControl("ddlOperadora");
                    ddlPlan = (DropDownList)linha.Cells[1].FindControl("ddlPlanoSaude");
                    ddlProc = (DropDownList)linha.Cells[7].FindControl("ddlProcedimento");
                    HiddenField hidValorUnitario = (HiddenField)linha.Cells[7].FindControl("hidValUnitProc");
                    //textbox que vai receber valor calculado
                    TextBox txtValor = (TextBox)linha.Cells[8].FindControl("txtVlUnitario");

                    //Carrega os planos de saúde da operadora quando encontra o objeto que invocou o postback
                    if (ddlProc.ClientID == atual.ClientID)
                        CalcularPreencherValoresTabelaECalculado(ddlProc, ddlOper, ddlPlan, hidValorUnitario);

                    TextBox txtqtde = (TextBox)linha.Cells[6].FindControl("txtQtde");
                    //Quantidade de sessões
                    int qt = (!string.IsNullOrEmpty(txtqtde.Text) ? int.Parse(txtqtde.Text) : 0);
                    CalculaValorTotalProcedimento(qt, hidValorUnitario.Value, txtValor);
                    CarregarValoresTotaisFooter();
                }
            }
        }

        protected void grdMedicosPlanto_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //--------> Criação do estilo e links das linhas da GRID
            if (e.Row.DataItem != null)
            {
                e.Row.Attributes.Add("onClick", "javascript:__doPostBack('" + grdMedicosPlanto.UniqueID + "','Select$" + qtdLinhasGrid + "')");
                qtdLinhasGrid = qtdLinhasGrid + 1;
            }
        }

        protected void grdMedicosPlanto_SelectedIndexChanged(object sender, EventArgs e)
        {
            //--------> Define o código da Unidade Educacional Selecionada como a Unidade em uso
            if (grdMedicosPlanto.DataKeys[grdMedicosPlanto.SelectedIndex].Value != null)
            {
                // Passa por todos os registros da grid de Pré-Atendimentos
                foreach (GridViewRow linha in grdMedicosPlanto.Rows)
                {
                    CheckBox chk = (CheckBox)linha.Cells[0].FindControl("chkselect2");
                    int coCol = int.Parse((((HiddenField)linha.Cells[0].FindControl("hidcoCol")).Value));

                    //Verifica se foi clicada uma linha diferente da selecionada, caso tenha sido, desmarca a linha
                    if (coCol != Convert.ToInt32(grdMedicosPlanto.DataKeys[grdMedicosPlanto.SelectedIndex].Value))
                        chk.Checked = false;
                    //Se for a mesma linha então seleciona o checkbox correspondente
                    else
                    {
                        //Verifico se já está selecionado, caso já esteja eu recorro aos padrões de limpeza de dados
                        if (chk.Checked)
                        {
                            chk.Checked = false;
                            GridMedicosPlantonistasDesmarcada();
                        }
                        //Caso não esteja selecionado ainda, chamo os métodos responsáveis pelos carregamentos
                        else
                        {
                            chk.Checked = true;
                            int idAtend = Convert.ToInt32(grdMedicosPlanto.DataKeys[grdMedicosPlanto.SelectedIndex].Value);

                            int coColPlantonista = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidcoCol")).Value);
                            int coEmpColPlantonista = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidcoEmpColPla")).Value);
                            string coEspec = ((HiddenField)linha.Cells[0].FindControl("hidCoEspec")).Value;

                            GridMedicosPlantonistasSelecionada(coEspec, coColPlantonista, coEmpColPlantonista);
                        }
                    }
                }
            }
        }

        protected void ddlUF_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaCidade();
            ExecutaJavaScript();
            ddlCidade.Focus();
        }

        protected void ddlCidade_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            carregaBairro();
            ExecutaJavaScript();

            ddlBairro.Focus();
        }

        protected void chkPaciEhResp_OnCheckedChanged(object sender, EventArgs e)
        {
            carregaPaciehoResponsavel();

            if (chkPaciEhResp.Checked)
                chkPaciMoraCoResp.Checked = true;
        }

        protected void imgCpfResp_OnClick(object sender, EventArgs e)
        {
            PesquisaCarregaResp(null);
            //ExecutaJavaScript();
        }

        protected void imbPesqPaci_OnClick(object sender, EventArgs e)
        {
            #region Validações

            //Se não houver nenhum tipo de pesquisa marcado
            if ((!rdbPesqCPF.Checked) && (!rdbPesqNIS.Checked) && (!rdbPesqProntuario.Checked))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "É preciso selecionar ao menos uma das opções de pesquisa!");
                return;
            }

            //Apresenta erro, caso esteja selecionada busca por CPF mas nenhum tenha sido informado
            if ((rdbPesqCPF.Checked) && (string.IsNullOrEmpty(txtCpfPaci.Text)))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Você selecionou pesquisa por CPF mas não informou o número pelo qual pesquisar. Favor informar!");
                return;
            }

            //Apresenta erro, caso esteja selecionada busca por NIS mas nenhum tenha sido informado
            if ((rdbPesqNIS.Checked) && (string.IsNullOrEmpty(txtNuNisPaci.Text)))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Você selecionou pesquisa por NIS mas não informou o número pelo qual pesquisar. Favor informar!");
                return;
            }

            //Apresenta erro, caso esteja selecionada busca por PRONTUÁRIO mas nenhum tenha sido informado
            if ((rdbPesqProntuario.Checked) && (string.IsNullOrEmpty(txtNuProntuario.Text)))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Você selecionou pesquisa por Prontuário mas não informou o número pelo qual pesquisar. Favor informar!");
                return;
            }

            #endregion

            PesquisaCarregaPaci();
            //UpdatePanel2.Update();
            ExecutaJavaScript();
        }

        protected void imgPesqCEP_OnClick(object sender, EventArgs e)
        {
            if (txtCEP.Text.Replace("-", "") != "")
            {
                int numCep = int.Parse(txtCEP.Text.Replace("-", ""));

                TB235_CEP tb235 = TB235_CEP.RetornaTodosRegistros().Where(c => c.CO_CEP == numCep).FirstOrDefault();

                if (tb235 != null)
                {
                    txtLograEndResp.Text = tb235.NO_ENDER_CEP;
                    tb235.TB905_BAIRROReference.Load();
                    ddlUF.SelectedValue = tb235.TB905_BAIRRO.CO_UF;
                    carregaCidade();
                    ddlCidade.SelectedValue = tb235.TB905_BAIRRO.CO_CIDADE.ToString();
                    carregaBairro();
                    ddlBairro.SelectedValue = tb235.TB905_BAIRRO.CO_BAIRRO.ToString();
                }
                else
                {
                    txtLograEndResp.Text = "";
                    ddlBairro.SelectedValue = "";
                    ddlCidade.SelectedValue = "";
                    ddlUF.SelectedValue = "";
                }
            }
            ExecutaJavaScript();
        }

        protected void chkselect2_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            CheckBox chk;

            int coColPlantonista;
            int coEmpColPlantonista;

            // Valida se a grid de atividades possui algum registro
            if (grdMedicosPlanto.Rows.Count != 0)
            {
                // Passa por todos os registros da grid de atividades
                foreach (GridViewRow linha in grdMedicosPlanto.Rows)
                {
                    chk = (CheckBox)linha.Cells[0].FindControl("chkselect2");

                    // Desmarca todos os registros menos o que foi clicado
                    if (chk.ClientID != atual.ClientID)
                    {
                        chk.Checked = false;
                    }
                    else
                    {
                        if (chk.Checked)
                        {
                            coColPlantonista = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidcoCol")).Value);
                            hidCoColEncAnalise.Value = coColPlantonista.ToString();
                            coEmpColPlantonista = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidcoEmpColPla")).Value);
                            string coEspec = ((HiddenField)linha.Cells[0].FindControl("hidCoEspec")).Value;
                            GridMedicosPlantonistasSelecionada(coEspec, coColPlantonista, coEmpColPlantonista);
                        }
                        else
                            GridMedicosPlantonistasDesmarcada();
                    }
                }
            }
        }

        protected void ddlGrupoProc_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSubGrupos();
        }

        protected void btnMaisLinhaChequePgto_OnClick(object sender, EventArgs e)
        {
            CriaNovaLinhaGridSolicitacoes();
        }

        protected void btnMaisSolicitacoes_OnClick(object sender, EventArgs e)
        {
            CriaNovaLinhaGridSolicitacoes();
        }

        protected void imgPesqProcedimentos_OnClick(object sender, EventArgs e)
        {
            //Percorre cada item da grid
            foreach (GridViewRow li in grdSolicitacoes.Rows)
            {
                DropDownList ddlOper = (DropDownList)li.Cells[0].FindControl("ddlOperadora");
                DropDownList ddlProc = (DropDownList)li.Cells[7].FindControl("ddlProcedimento");

                //Recarrega os procedimentos de acordo com parâmetros
                CarregarProcedimentosMedicos(ddlProc, ddlOper, ddlGrupoProc, ddlSubGrupo);
            }
        }

        protected void imgExc_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            int aux = 0;
            if (grdSolicitacoes.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdSolicitacoes.Rows)
                {
                    img = (ImageButton)linha.Cells[10].FindControl("imgExc");

                    if (img.ClientID == atual.ClientID)
                        aux = linha.RowIndex;
                }
            }
            ExcluiItemGrid(aux);
            CarregarValoresTotaisFooter();
        }

        protected void imgInfos_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;
            if (grdSolicitacoes.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdSolicitacoes.Rows)
                {
                    img = (ImageButton)linha.Cells[8].FindControl("imgInfos");

                    if (img.ClientID == atual.ClientID)
                    {
                        string vlUnit = ((HiddenField)linha.Cells[7].FindControl("hidValUnitProc")).Value;
                        string procSelec = ((DropDownList)linha.Cells[7].FindControl("ddlProcedimento")).SelectedValue;

                        if(!string.IsNullOrEmpty(procSelec))
                        {
                            int proc = int.Parse(procSelec);
                            var res = (from tbs356 in TBS356_PROC_MEDIC_PROCE.RetornaTodosRegistros()
                                       where tbs356.ID_PROC_MEDI_PROCE == proc
                                       select new
                                       {
                                           tbs356.DE_OBSE_PROC_MEDI,
                                           tbs356.NM_PROC_MEDI,
                                           tbs356.ID_PROC_MEDI_PROCE,
                                           tbs356.CO_PROC_MEDI,
                                       }).FirstOrDefault();

                            if(res != null)
                            {
                                txtCodProc.Text = res.CO_PROC_MEDI;
                                txtNomeProc.Text = res.NM_PROC_MEDI;
                                txtObservacaoProc.Text = res.DE_OBSE_PROC_MEDI;
                                txtVlUnitProc.Text = vlUnit;
                            }
                        }
                        
                        ScriptManager.RegisterStartupScript(
                            this.Page,
                            this.GetType(),
                            "Acao",
                            "AbreModalInfoProcedimento();",
                            true
                        );
                    }
                }
            }
        }

        protected void grdSolicitacoes_OnRowDeleting(object sender, EventArgs e)
        {

        }

        protected void txtQtde_OnTextChanged(object sender, EventArgs e)
        {
            TextBox atual = (TextBox)sender;
            TextBox txt;
            if (grdSolicitacoes.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdSolicitacoes.Rows)
                {
                    txt = (TextBox)linha.Cells[6].FindControl("txtQtde");
                    if (txt.ClientID == atual.ClientID)
                    {
                        if (!string.IsNullOrEmpty(txt.Text))
                        {
                            //textbox que vai receber valor calculado
                            TextBox txtValor = (TextBox)linha.Cells[8].FindControl("txtVlUnitario");
                            //valor unitário do procedimento
                            string vlProcUnit = ((HiddenField)linha.Cells[7].FindControl("hidValUnitProc")).Value;
                            //Quantidade de sessões
                            int qt = int.Parse(txt.Text);
                            CalculaValorTotalProcedimento(qt, vlProcUnit, txtValor);
                            CarregarValoresTotaisFooter();
                        }
                    }
                }
            }
        }

        protected void txtVlDesconto_OnTextChanged(object sender, EventArgs e)
        {
            TextBox atual = (TextBox)sender;
            TextBox txt;
            if (grdSolicitacoes.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdSolicitacoes.Rows)
                {
                    txt = (TextBox)linha.Cells[9].FindControl("txtVlDesconto");
                    if (txt.ClientID == atual.ClientID)
                        CarregarValoresTotaisFooter();
                }
            }
        }

        #endregion
    }
}