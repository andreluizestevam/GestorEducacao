//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: CONTROLE DE TURMAS POR SÉRIES/CURSOS
// OBJETIVO: CADASTRAMENTO GERAL DE TURMAS POR MODALIDADE
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
using System.Data;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2107_MatriculaAluno.MatriculaLancFormaPgto
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
            //CurrentPadraoCadastros.BarraCadastro.OnDelete += new Library.Componentes.BarraCadastro.OnDeleteHandler(BarraCadastro_OnDelete);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                carregaGridChequesPgto();

                CarregaBanco(ddlBcoPgto1);
                CarregaBanco(ddlBcoPgto2);
                CarregaBanco(ddlBcoPgto3);

                CarregaAnos();
                CarregaModalidades();
                CarregaSerieCurso();
                CarregaTurma();
                CarregaAluno();

                txtNrContraPgto.Enabled = false;
                txtQtPgto.Enabled = false;

                chkCartaoCreditoPgto.Checked = false;
            }
        }

        //====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
           // É preciso para validar os campos da Aba de Forma de Pagamento
            #region Validação da Aba FORMA DE PAGAMENTO

            //Valida se foi escolhido um Aluno
            if (ddlAlunos.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor Preencher os campos do cabeçalho e selecionar um Aluno matriculado.");
                return;
            }

            //Valida o Campo Dinheiro ----------------------------------------------------------
            decimal? vldiPgto = null;
            if (chkDinhePgto.Checked && txtValDinPgto.Text == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "O campo Dinheiro está marcado, favor informar o valor.");
                return;
            }
            else if (chkDinhePgto.Checked)
                vldiPgto = decimal.Parse(txtValDinPgto.Text);



            //Valida o Campo Outros ---------------------------------------------------
            decimal? vloutPgto = null;
            if (chkOutrPgto.Checked && txtValOutPgto.Text == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "O campo Outros está marcado, favor informar o valor.");
                return;

            }
            else if (chkOutrPgto.Checked)
                vloutPgto = decimal.Parse(txtValOutPgto.Text);


            //Valida a Marcação da opção Cartão de Crédito ---------------------------------------
            if ((chkCartaoCreditoPgto.Checked) && (ddlBandePgto1.SelectedValue == "N") && (ddlBandePgto2.SelectedValue == "N") && (ddlBandePgto3.SelectedValue == "N"))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Você Selecionou a Forma de Pagamento Cartão de Crédito, é preciso preencher os campos condizentes");
                return;
            }

            //Valida a Marcação da opção Cartão de Débito ---------------------------------------
            if ((chkDebitPgto.Checked) && (ddlBcoPgto1.SelectedValue == "N") && (ddlBcoPgto2.SelectedValue == "N") && (ddlBcoPgto3.SelectedValue == "N"))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Você Selecionou a Forma de Pagamento Cartão de Débito, é preciso preencher os campos condizentes");
                return;
            }


            //----------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region Valida as Informações do Pagamento em Cartão de Crédito

            //Valida as Informações do Pagamento em Cartão de Crédito
            if ((ddlBandePgto1.SelectedValue != "N") && ((txtNumPgto1.Text == "") || (txtTitulPgto1.Text == "") || (txtVencPgto1.Text == "") || (txtValCarPgto1.Text == "")))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Você Iniciou o preenchimento da Primeira linha em Informações do Cartão de Crédito, favor Informar todos os campos");
                return;
            }

            //Valida as Informações do Pagamento em Cartão de Crédito
            if ((ddlBandePgto2.SelectedValue != "N") && ((txtNumPgto2.Text == "") || (txtTitulPgto2.Text == "") || (txtVencPgto2.Text == "") || (txtValCarPgto2.Text == "")))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Você Iniciou o preenchimento da Segunda linha em Informações do Cartão de Crédito, favor Informar todos os campos");
                return;
            }

            //Valida as Informações do Pagamento em Cartão de Crédito
            if ((ddlBandePgto3.SelectedValue != "N") && ((txtNumPgto3.Text == "") || (txtTitulPgto3.Text == "") || (txtVencPgto3.Text == "") || (txtValCarPgto3.Text == "")))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Você Iniciou o preenchimento da Terceira linha em Informações do Cartão de Crédito, favor Informar todos os campos");
                return;
            }

            #endregion


            //----------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region Valida as Informações do Pagamento em Cartão de Débito

            //Valida as Informações do Pagamento em Cartão de Débito
            if ((ddlBcoPgto1.SelectedValue != "N") && ((txtAgenPgto1.Text == "") || (txtNContPgto1.Text == "") || (txtNuDebtPgto1.Text == "") || (txtNuTitulDebitPgto1.Text == "") || (txtValDebitPgto1.Text == "")))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Você Iniciou o preenchimento da Primeira linha em Informações do Cartão de Débito, favor Informar todos os campos");
                return;
            }

            //Valida as Informações do Pagamento em Cartão de Débito
            if ((ddlBcoPgto2.SelectedValue != "N") && ((txtAgenPgto2.Text == "") || (txtNContPgto2.Text == "") || (txtNuDebtPgto2.Text == "") || (txtNuTitulDebitPgto2.Text == "") || (txtValDebitPgto2.Text == "")))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Você Iniciou o preenchimento da Segunda linha em Informações do Cartão de Débito, favor Informar todos os campos");
                return;
            }

            //Valida as Informações do Pagamento em Cartão de Débito
            if ((ddlBcoPgto3.SelectedValue != "N") && ((txtAgenPgto3.Text == "") || (txtNContPgto3.Text == "") || (txtNuDebtPgto3.Text == "") || (txtNuTitulDebitPgto3.Text == "") || (txtValDebitPgto3.Text == "")))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Você Iniciou o preenchimento da Terceira linha em Informações do Cartão de Débito, favor Informar todos os campos");
                return;
            }

            #endregion


            //----------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region Valida as Informações do Pagamento em Cheque
            //Percorre a grid verificando o que está marcado, e vendo se a linha do registro correspondente está preenchida ou não               
            foreach (GridViewRow linhaPgto in grdChequesPgto.Rows)
            {
                CheckBox chkGrdPgto = ((CheckBox)linhaPgto.Cells[0].FindControl("chkselectGridPgtoCheque"));

                if (chkGrdPgto.Checked)
                {
                    //Resgata os valores dos controles da GridView de Cheques
                    string vlBcoChe = ((DropDownList)linhaPgto.Cells[1].FindControl("ddlBcoChequePgto")).SelectedValue;
                    string vlAgeChe = ((TextBox)linhaPgto.Cells[2].FindControl("txtAgenChequePgto")).Text;
                    string vlConChe = ((TextBox)linhaPgto.Cells[3].FindControl("txtNrContaChequeConta")).Text;
                    string vlNuChe = ((TextBox)linhaPgto.Cells[4].FindControl("txtNrChequePgto")).Text;
                    string vlNuCpf = ((TextBox)linhaPgto.Cells[5].FindControl("txtNuCpfChePgto")).Text;
                    string vlNoTit = ((TextBox)linhaPgto.Cells[6].FindControl("txtTitulChequePgto")).Text;
                    string vlPgtChe = ((TextBox)linhaPgto.Cells[7].FindControl("txtVlChequePgto")).Text;
                    string dtVenChe = ((TextBox)linhaPgto.Cells[8].FindControl("txtDtVencChequePgto")).Text;

                    string vlPgtCheAux = ((TextBox)linhaPgto.Cells[7].FindControl("txtVlChequePgto")).Text;
                    string dtVenCheAux = ((TextBox)linhaPgto.Cells[8].FindControl("txtDtVencChequePgto")).Text;

                    if ((vlBcoChe == "N") || (vlAgeChe == "") || (vlConChe == "") || (vlNuChe == "") || (vlNuCpf == "") || (vlNoTit == "") || (vlPgtCheAux == "") || (dtVenCheAux == ""))
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Você selecionou um registro de cheque, favor Preecher os campos restantes");
                        return;
                    }
                    else
                        chkChequePgto.Checked = true;
                }
            }

            #endregion

            #endregion

            #region Atualiza os dados da Forma de Pagamento

                //Salva as informações sobre a forma de pagamento na tabela de TBE220 na TBE220_MATRCUR_PAGTO
            TBE220_MATRCUR_PAGTO tbe220 = RetornaEntidade();

            tbe220.CO_EMP_M = int.Parse(hdCoEmpMatr.Value);
            tbe220.CO_ALU_M = int.Parse(hdCoAluMatr.Value);
            tbe220.CO_CUR_M = int.Parse(hdCoCurMatr.Value);
            tbe220.CO_ANO_M = hdCoAnoMatr.Value;
            tbe220.NU_SEM_M = 1.ToString();
            tbe220.CO_ALU_CAD = hdCoMatrMat.Value;

            tbe220.QT_PARCE = (txtQtPgto.Text != "" ? int.Parse(txtQtPgto.Text) : (int?)null);
            tbe220.FL_DINHE = (chkDinhePgto.Checked ? "S" : "N");
            tbe220.VL_DINHE = (txtValDinPgto.Text != "" ? decimal.Parse(txtValDinPgto.Text) : (decimal?)null);
            tbe220.FL_OUTRO = (chkOutrPgto.Checked ? "S" : "N");
            tbe220.VL_OUTRO = (txtValOutPgto.Text != "" ? decimal.Parse(txtValOutPgto.Text) : (decimal?)null);
            tbe220.FL_CARTA_CRED = (chkCartaoCreditoPgto.Checked ? "S" : "N");
            tbe220.FL_CARTA_DEBI = (chkDebitPgto.Checked ? "S" : "N");
            tbe220.FL_CHEQUE = (chkChequePgto.Checked ? "S" : "N");
            tbe220.QT_PARCE = (txtQtPgto.Text != "" ? int.Parse(txtQtPgto.Text) : (int?)null);
            tbe220.CO_COL = LoginAuxili.CO_COL;
            tbe220.CO_EMP = LoginAuxili.CO_EMP;
            tbe220.DT_CAD = DateTime.Now;
                //tbe220.VL_DIFER_RECEB = ;

            //CurrentPadraoCadastros.CurrentEntity = tbe220;
            TBE220_MATRCUR_PAGTO.SaveOrUpdate(tbe220);

            decimal valCredTot = 0; 

            //Resgata um objeto da forma de pagamento que acabou de ser cadastrada, para ser usado como referência para outras inserções.
            int tbe220ob1 = (from t220 in TBE220_MATRCUR_PAGTO.RetornaTodosRegistros()
                             where t220.CO_ALU_CAD == hdCoMatrMat.Value
                             select new { t220.ID_MATRCUR_PAGTO }).FirstOrDefault().ID_MATRCUR_PAGTO;

            //int tbe220Li = TBE220_MATRCUR_PAGTO.RetornaTodosRegistros().Where(w => w.CO_ALU_CAD == hdCoAluMatr.Value).FirstOrDefault().ID_MATRCUR_PAGTO;

                //Salva as informações sobre o pagamento em cartão, quando este for selecionado na TBE221_PAGTO_CARTAO
                //Salva as informações da parte de cartão de Crédito
                if (ddlBandePgto1.SelectedValue != "N")
                {
                    //Chama o Método que verifica se já existe os registro em questão, para evitar salvar em duplicidade.
                    TBE221_PAGTO_CARTAO tbe221 = carregaListaCartaoCredito(tbe220ob1, (hdIDCC1.Value != "" ? int.Parse(hdIDCC1.Value) : (int?)null));

                    //Se o objeto retornado da consulta acima, for diferente de nulo, ele apenas altera o objeto já existente para os novos dados inseridos na página.
                    if (tbe221 != null)
                    {
                        tbe221.CO_BANDE = ddlBandePgto1.SelectedValue;
                        tbe221.CO_NUMER = txtNumPgto1.Text;
                        tbe221.NO_TITUL = txtTitulPgto1.Text;
                        tbe221.DT_VENCI = txtVencPgto1.Text;
                        tbe221.VL_PAGTO = (txtValCarPgto1.Text != "" ? decimal.Parse(txtValCarPgto1.Text) : (decimal?)null);

                        TBE221_PAGTO_CARTAO.SaveOrUpdate(tbe221, true);
                    }
                    //Caso seja nulo, ele cria um novo registro para os dados informados.
                    else
                    {
                        TBE221_PAGTO_CARTAO tbe221ob1 = new TBE221_PAGTO_CARTAO();
                        tbe221ob1.TBE220_MATRCUR_PAGTO = TBE220_MATRCUR_PAGTO.RetornaPelaChavePrimaria(tbe220ob1);
                        tbe221ob1.CO_BANDE = ddlBandePgto1.SelectedValue;
                        tbe221ob1.CO_NUMER = txtNumPgto1.Text;
                        tbe221ob1.NO_TITUL = txtTitulPgto1.Text;
                        tbe221ob1.DT_VENCI = txtVencPgto1.Text;
                        tbe221ob1.VL_PAGTO = (txtValCarPgto1.Text != "" ? decimal.Parse(txtValCarPgto1.Text) : (decimal?)null);
                        tbe221ob1.FL_TIPO_TRANSAC = "C";

                        TBE221_PAGTO_CARTAO.SaveOrUpdate(tbe221ob1, true);
                    }
                }

                //Faz o mesmo procedimento do código acima, na segunda linha do cartão de Crédito.
                if (ddlBandePgto2.SelectedValue != "N")
                {
                    //Chama o Método que verifica se já existe os registro em questão, para evitar salvar em duplicidade.
                    TBE221_PAGTO_CARTAO tbe221 = carregaListaCartaoCredito(tbe220ob1, (hdIDCC2.Value != "" ? int.Parse(hdIDCC2.Value) : (int?)null));

                     //Se o objeto retornado da consulta acima, for diferente de nulo, ele apenas altera o objeto já existente para os novos dados inseridos na página.
                    if (tbe221 != null)
                    {
                        tbe221.CO_BANDE = ddlBandePgto2.SelectedValue;
                        tbe221.CO_NUMER = txtNumPgto2.Text;
                        tbe221.NO_TITUL = txtTitulPgto2.Text;
                        tbe221.DT_VENCI = txtVencPgto2.Text;
                        tbe221.VL_PAGTO = (txtValCarPgto2.Text != "" ? decimal.Parse(txtValCarPgto2.Text) : (decimal?)null);

                        TBE221_PAGTO_CARTAO.SaveOrUpdate(tbe221, true);
                    }
                    //Caso seja nulo, ele cria um novo registro para os dados informados.
                    else
                    {
                        TBE221_PAGTO_CARTAO tbe221ob2 = new TBE221_PAGTO_CARTAO();

                        tbe221ob2.TBE220_MATRCUR_PAGTO = TBE220_MATRCUR_PAGTO.RetornaPelaChavePrimaria(tbe220ob1);
                        tbe221ob2.CO_BANDE = ddlBandePgto2.SelectedValue;
                        tbe221ob2.CO_NUMER = txtNumPgto2.Text;
                        tbe221ob2.NO_TITUL = txtTitulPgto2.Text;
                        tbe221ob2.DT_VENCI = txtVencPgto2.Text;
                        tbe221ob2.VL_PAGTO = (txtValCarPgto2.Text != "" ? decimal.Parse(txtValCarPgto2.Text) : (decimal?)null);
                        tbe221ob2.FL_TIPO_TRANSAC = "C";

                        TBE221_PAGTO_CARTAO.SaveOrUpdate(tbe221ob2, true);
                    }
                }

                //Faz o mesmo procedimento do código acima, na segunda linha do cartão de Crédito.
                if (ddlBandePgto3.SelectedValue != "N")
                {

                    //Chama o Método que verifica se já existe os registro em questão, para evitar salvar em duplicidade.
                    TBE221_PAGTO_CARTAO tbe221 = carregaListaCartaoCredito(tbe220ob1, (hdIDCC3.Value != "" ? int.Parse(hdIDCC3.Value) : (int?)null));

                     //Se o objeto retornado da consulta acima, for diferente de nulo, ele apenas altera o objeto já existente para os novos dados inseridos na página.
                    if (tbe221 != null)
                    {
                        tbe221.CO_BANDE = ddlBandePgto3.SelectedValue;
                        tbe221.CO_NUMER = txtNumPgto3.Text;
                        tbe221.NO_TITUL = txtTitulPgto3.Text;
                        tbe221.DT_VENCI = txtVencPgto3.Text;
                        tbe221.VL_PAGTO = (txtValCarPgto3.Text != "" ? decimal.Parse(txtValCarPgto3.Text) : (decimal?)null);

                        TBE221_PAGTO_CARTAO.SaveOrUpdate(tbe221, true);
                    }
                    //Caso seja nulo, ele cria um novo registro para os dados informados.
                    else
                    {
                        TBE221_PAGTO_CARTAO tbe221ob3 = new TBE221_PAGTO_CARTAO();

                        tbe221ob3.TBE220_MATRCUR_PAGTO = TBE220_MATRCUR_PAGTO.RetornaPelaChavePrimaria(tbe220ob1);
                        tbe221ob3.CO_BANDE = ddlBandePgto3.SelectedValue;
                        tbe221ob3.CO_NUMER = txtNumPgto3.Text;
                        tbe221ob3.NO_TITUL = txtTitulPgto3.Text;
                        tbe221ob3.DT_VENCI = txtVencPgto3.Text;
                        tbe221ob3.VL_PAGTO = (txtValCarPgto3.Text != "" ? decimal.Parse(txtValCarPgto3.Text) : (decimal?)null);
                        tbe221ob3.FL_TIPO_TRANSAC = "C";

                        TBE221_PAGTO_CARTAO.SaveOrUpdate(tbe221ob3, true);
                    }
                }

                //Salva as informações da parte de cartão de Débito
                if (ddlBcoPgto1.SelectedValue != "N")
                {
                    //Chama o Método que verifica se já existe os registro em questão, para evitar salvar em duplicidade.
                    TBE221_PAGTO_CARTAO tbe221 = carregaListaCartaoCredito(tbe220ob1, (hdIDCDebt1.Value != "" ? int.Parse(hdIDCDebt1.Value) : (int?)null));

                     //Se o objeto retornado da consulta acima, for diferente de nulo, ele apenas altera o objeto já existente para os novos dados inseridos na página.
                    if (tbe221 != null)
                    {
                        tbe221.CO_BCO = int.Parse(ddlBcoPgto1.SelectedValue);
                        tbe221.NR_AGENCI = txtAgenPgto1.Text;
                        tbe221.NR_CONTA = txtNContPgto1.Text;
                        tbe221.CO_NUMER = txtNuDebtPgto1.Text;
                        tbe221.NO_TITUL = txtNuTitulDebitPgto1.Text;
                        tbe221.VL_PAGTO = (txtValDebitPgto1.Text != "" ? decimal.Parse(txtValDebitPgto1.Text) : (decimal?)null);

                        TBE221_PAGTO_CARTAO.SaveOrUpdate(tbe221, true);
                    }
                    else
                    {
                        TBE221_PAGTO_CARTAO tbe221obC1 = new TBE221_PAGTO_CARTAO();

                        tbe221obC1.TBE220_MATRCUR_PAGTO = TBE220_MATRCUR_PAGTO.RetornaPelaChavePrimaria(tbe220ob1);
                        tbe221obC1.CO_BCO = int.Parse(ddlBcoPgto1.SelectedValue);
                        tbe221obC1.NR_AGENCI = txtAgenPgto1.Text;
                        tbe221obC1.NR_CONTA = txtNContPgto1.Text;
                        tbe221obC1.CO_NUMER = txtNuDebtPgto1.Text;
                        tbe221obC1.NO_TITUL = txtNuTitulDebitPgto1.Text;
                        tbe221obC1.VL_PAGTO = (txtValDebitPgto1.Text != "" ? decimal.Parse(txtValDebitPgto1.Text) : (decimal?)null);
                        tbe221obC1.FL_TIPO_TRANSAC = "D";

                        TBE221_PAGTO_CARTAO.SaveOrUpdate(tbe221obC1, true);
                    }
                }

                if (ddlBcoPgto2.SelectedValue != "N")
                {

                   //Chama o Método que verifica se já existe os registro em questão, para evitar salvar em duplicidade.
                    TBE221_PAGTO_CARTAO tbe221 = carregaListaCartaoCredito(tbe220ob1, (hdIDCDebt2.Value != "" ? int.Parse(hdIDCDebt2.Value) : (int?)null));

                     //Se o objeto retornado da consulta acima, for diferente de nulo, ele apenas altera o objeto já existente para os novos dados inseridos na página.
                    if (tbe221 != null)
                    {
                        tbe221.CO_BCO = int.Parse(ddlBcoPgto2.SelectedValue);
                        tbe221.NR_AGENCI = txtAgenPgto2.Text;
                        tbe221.NR_CONTA = txtNContPgto2.Text;
                        tbe221.CO_NUMER = txtNuDebtPgto2.Text;
                        tbe221.NO_TITUL = txtNuTitulDebitPgto2.Text;
                        tbe221.VL_PAGTO = (txtValDebitPgto2.Text != "" ? decimal.Parse(txtValDebitPgto2.Text) : (decimal?)null);

                        TBE221_PAGTO_CARTAO.SaveOrUpdate(tbe221, true);
                    }
                    else
                    {
                        TBE221_PAGTO_CARTAO tbe221obC2 = new TBE221_PAGTO_CARTAO();

                        tbe221obC2.TBE220_MATRCUR_PAGTO = TBE220_MATRCUR_PAGTO.RetornaPelaChavePrimaria(tbe220ob1);
                        tbe221obC2.CO_BCO = int.Parse(ddlBcoPgto2.SelectedValue);
                        tbe221obC2.NR_AGENCI = txtAgenPgto2.Text;
                        tbe221obC2.NR_CONTA = txtNContPgto2.Text;
                        tbe221obC2.CO_NUMER = txtNuDebtPgto2.Text;
                        tbe221obC2.NO_TITUL = txtNuTitulDebitPgto2.Text;
                        tbe221obC2.VL_PAGTO = (txtValDebitPgto2.Text != "" ? decimal.Parse(txtValDebitPgto2.Text) : (decimal?)null);
                        tbe221obC2.FL_TIPO_TRANSAC = "D";

                        TBE221_PAGTO_CARTAO.SaveOrUpdate(tbe221obC2, true);
                    }
                }

                if (ddlBcoPgto3.SelectedValue != "N")
                {
                   //Chama o Método que verifica se já existe os registro em questão, para evitar salvar em duplicidade.
                    TBE221_PAGTO_CARTAO tbe221 = carregaListaCartaoCredito(tbe220ob1, (hdIDCDebt3.Value != "" ? int.Parse(hdIDCDebt3.Value) : (int?)null));

                     //Se o objeto retornado da consulta acima, for diferente de nulo, ele apenas altera o objeto já existente para os novos dados inseridos na página.
                    if (tbe221 != null)
                    {
                        tbe221.CO_BCO = int.Parse(ddlBcoPgto3.SelectedValue);
                        tbe221.NR_AGENCI = txtAgenPgto3.Text;
                        tbe221.NR_CONTA = txtNContPgto3.Text;
                        tbe221.CO_NUMER = txtNuDebtPgto3.Text;
                        tbe221.NO_TITUL = txtNuTitulDebitPgto3.Text;
                        tbe221.VL_PAGTO = (txtValDebitPgto3.Text != "" ? decimal.Parse(txtValDebitPgto3.Text) : (decimal?)null);

                        TBE221_PAGTO_CARTAO.SaveOrUpdate(tbe221, true);
                    }
                    else
                    {
                        TBE221_PAGTO_CARTAO tbe221obC3 = new TBE221_PAGTO_CARTAO();

                        tbe221obC3.TBE220_MATRCUR_PAGTO = TBE220_MATRCUR_PAGTO.RetornaPelaChavePrimaria(tbe220ob1);
                        tbe221obC3.CO_BCO = int.Parse(ddlBcoPgto3.SelectedValue);
                        tbe221obC3.NR_AGENCI = txtAgenPgto3.Text;
                        tbe221obC3.NR_CONTA = txtNContPgto3.Text;
                        tbe221obC3.CO_NUMER = txtNuDebtPgto3.Text;
                        tbe221obC3.NO_TITUL = txtNuTitulDebitPgto3.Text;
                        tbe221obC3.VL_PAGTO = (txtValDebitPgto3.Text != "" ? decimal.Parse(txtValDebitPgto3.Text) : (decimal?)null);
                        tbe221obC3.FL_TIPO_TRANSAC = "D";

                        TBE221_PAGTO_CARTAO.SaveOrUpdate(tbe221obC3, true);
                    }
                }

                //Faz o Cálculo da diferença para Alimentar a coluna VL_DIFER_RECEB
                #region Cálculo do Valor diferença entre o valor pago e o valor do contrato

                decimal valDinPg = (txtValDinPgto.Text != "" ? decimal.Parse(txtValDinPgto.Text) : decimal.Parse("0,00"));
                decimal valOutPg = (txtValOutPgto.Text != "" ? decimal.Parse(txtValOutPgto.Text) : decimal.Parse("0,00"));
                decimal valCredPg1 = (txtValCarPgto1.Text != "" ? decimal.Parse(txtValCarPgto1.Text) : decimal.Parse("0,00"));
                decimal valCredPg2 = (txtValCarPgto2.Text != "" ? decimal.Parse(txtValCarPgto2.Text) : decimal.Parse("0,00"));
                decimal valCredPg3 = (txtValCarPgto3.Text != "" ? decimal.Parse(txtValCarPgto3.Text) : decimal.Parse("0,00"));
                decimal valDebPg1 = (txtValDebitPgto1.Text != "" ? decimal.Parse(txtValDebitPgto1.Text) : decimal.Parse("0,00"));
                decimal valDebPg2 = (txtValDebitPgto2.Text != "" ? decimal.Parse(txtValDebitPgto2.Text) : decimal.Parse("0,00"));
                decimal valDebPg3 = (txtValDebitPgto3.Text != "" ? decimal.Parse(txtValDebitPgto3.Text) : decimal.Parse("0,00"));

                decimal valTotalPgto = valDinPg + valOutPg + valCredPg1 + valCredPg2 + valCredPg3 + valDebPg1 + valDebPg2 + valDebPg3;


                //Salva as informações sobre o pagamento em cheque, quando este for selecionado na TBE222_PAGTO_CHEQUE
                foreach (GridViewRow linhaPgto in grdChequesPgto.Rows)
                {
                    CheckBox chkGrdPgto = ((CheckBox)linhaPgto.Cells[0].FindControl("chkselectGridPgtoCheque"));

                    if (chkGrdPgto.Checked)
                    {
                        //Resgata os valores dos controles da GridView de Cheques

                        string vlCodChe = ((HiddenField)linhaPgto.Cells[1].FindControl("hidCodPgtoChe")).Value;
                        int? vlCodCheValid = (vlCodChe != "" ? int.Parse(vlCodChe) : (int?)null);

                        int vlBcoChe = int.Parse(((DropDownList)linhaPgto.Cells[1].FindControl("ddlBcoChequePgto")).SelectedValue);
                        string vlAgeChe = ((TextBox)linhaPgto.Cells[2].FindControl("txtAgenChequePgto")).Text;
                        string vlConChe = ((TextBox)linhaPgto.Cells[3].FindControl("txtNrContaChequeConta")).Text;
                        string vlNuChe = ((TextBox)linhaPgto.Cells[4].FindControl("txtNrChequePgto")).Text;
                        string vlNuCpf = ((TextBox)linhaPgto.Cells[5].FindControl("txtNuCpfChePgto")).Text;
                        string vlNoTit = ((TextBox)linhaPgto.Cells[6].FindControl("txtTitulChequePgto")).Text;
                        decimal vlPgtChe = decimal.Parse(((TextBox)linhaPgto.Cells[7].FindControl("txtVlChequePgto")).Text);
                        DateTime dtVenChe = DateTime.Parse(((TextBox)linhaPgto.Cells[8].FindControl("txtDtVencChequePgto")).Text);

                        //Chama o método que lista os pagamentos em cartões nos parâmetros informados
                        TBE222_PAGTO_CHEQUE tbe222 = carregaListaPgtoCheque(tbe220ob1, vlCodCheValid);

                        if (tbe222 != null)
                        {
                            tbe222.CO_BCO = vlBcoChe;
                            tbe222.NR_AGENCI = vlAgeChe;
                            tbe222.NR_CONTA = vlConChe;
                            tbe222.NR_CHEQUE = vlNuChe;
                            tbe222.NR_CPF = vlNuCpf.Replace(".", "").Replace("-", "");
                            tbe222.NO_TITUL = vlNoTit;
                            tbe222.VL_PAGTO = vlPgtChe;
                            tbe222.DT_VENC = dtVenChe;

                            TBE222_PAGTO_CHEQUE.SaveOrUpdate(tbe222, true);
                        }
                        else
                        {
                            TBE222_PAGTO_CHEQUE tbe222ob = new TBE222_PAGTO_CHEQUE();

                            tbe222ob.TBE220_MATRCUR_PAGTO = TBE220_MATRCUR_PAGTO.RetornaPelaChavePrimaria(tbe220ob1);
                            tbe222ob.CO_BCO = vlBcoChe;
                            tbe222ob.NR_AGENCI = vlAgeChe;
                            tbe222ob.NR_CONTA = vlConChe;
                            tbe222ob.NR_CHEQUE = vlNuChe;
                            tbe222ob.NR_CPF = vlNuCpf.Replace(".", "").Replace("-", "");
                            tbe222ob.NO_TITUL = vlNoTit;
                            tbe222ob.VL_PAGTO = vlPgtChe;
                            tbe222ob.DT_VENC = dtVenChe;

                            TBE222_PAGTO_CHEQUE.SaveOrUpdate(tbe222ob, true);
                        }

                        //Soma o Valor pago em cheque no Valor Total
                        valTotalPgto += vlPgtChe;
                }
                #endregion
            }

                TBE220_MATRCUR_PAGTO tbe220obSalv = TBE220_MATRCUR_PAGTO.RetornaTodosRegistros().Where(w => w.CO_ALU_CAD == hdCoMatrMat.Value).FirstOrDefault();
                //Altera a coluna VL_DIFER_RECEB com a diferença calculada
                tbe220obSalv.VL_DIFER_RECEB = valTotalPgto - (hdValTotContrato.Value != null ? decimal.Parse(hdValTotContrato.Value) : decimal.Parse("0,00"));
                TBE220_MATRCUR_PAGTO.SaveOrUpdate(tbe220obSalv, true);

              AuxiliPagina.RedirecionaParaPaginaSucesso("Operação Realizada com Sucesso.", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());

            #endregion
        }

        //Exclui a forma de pagamento
        private void excluirFormaPgto()
        {
            bool excluido = false;
            try
            {
                TBE220_MATRCUR_PAGTO tbe220 = RetornaEntidade();

                if (tbe220 == null)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "O Aluno selecionado não possui forma de pagamento associada à sua matrícula.");
                    return;
                }
                else
                {
                    //--------> Pesquisa o registro da tabela de Forma de Pagamento da Matrícula em Cartão
                    List<TBE221_PAGTO_CARTAO> lsttbe221 = (from tbe221 in TBE221_PAGTO_CARTAO.RetornaTodosRegistros()
                                                           where tbe221.TBE220_MATRCUR_PAGTO.ID_MATRCUR_PAGTO == tbe220.ID_MATRCUR_PAGTO
                                                           select tbe221).ToList();

                    //--------> Pesquisa o registro da tabela de Forma de Pagamento da Matrícula em Cheque
                    List<TBE222_PAGTO_CHEQUE> lsttbe222 = (from tbe222 in TBE222_PAGTO_CHEQUE.RetornaTodosRegistros()
                                                           where tbe222.TBE220_MATRCUR_PAGTO.ID_MATRCUR_PAGTO == tbe220.ID_MATRCUR_PAGTO
                                                           select tbe222).ToList();

                    //--------> Exclui o registro da tabela de Forma de Pagamento da Matrícula em Cheque
                    if (lsttbe222 != null)
                    {
                        foreach (TBE222_PAGTO_CHEQUE tbe222ob in lsttbe222)
                        {
                            TBE222_PAGTO_CHEQUE.Delete(tbe222ob, true);
                        }
                    }

                    //--------> Exclui o registro da tabela de Forma de Pagamento da Matrícula em Cartão
                    if (lsttbe221 != null)
                    {
                        foreach (TBE221_PAGTO_CARTAO tbe221ob in lsttbe221)
                        {
                            TBE221_PAGTO_CARTAO.Delete(tbe221ob, true);
                        }
                    }

                    //--------> Exclui o registro da tabela de matricula
                    if (GestorEntities.Delete(tbe220) <= 0)
                        AuxiliPagina.EnvioMensagemErro(this, "Erro ao excluir a Foma de Pagamento.");
                }
                excluido = true;
            }
            catch
            {
                excluido = false;
            }
            if (excluido)
                AuxiliPagina.RedirecionaParaPaginaSucesso("Forma de pagamento e suas dependências excluídas com Sucesso!", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
            else
                AuxiliPagina.RedirecionaParaPaginaErro("Não foi possível excluir o aluno solicitado.", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
        }

        //void BarraCadastro_OnDelete(System.Data.Objects.DataClasses.EntityObject entity)
        //{
        //    excluirFormaPgto();
            //TBE220_MATRCUR_PAGTO tbe220 = RetornaEntidade();

            //if (GestorEntities.Delete(tbe220) <= 0)
            //    AuxiliPagina.EnvioMensagemErro(this, "Erro ao excluir registro.");
            //else
            //    AuxiliPagina.RedirecionaParaPaginaSucesso("Registro excluído com Sucesso", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());

        //É executado quando se clica em abrir um novo registro
        protected void btnNovo_Click(object sender, EventArgs e)
        {
            hdAuxQueryString.Value = "1";
            ddlAno.Enabled =
            ddlSerieCurso.Enabled =
            ddlModalidade.Enabled =
            ddlTurma.Enabled =
            txtNireAlunoPgto.Enabled =
            txtNisAlunoPgto.Enabled =
            ddlAlunos.Enabled = true;

            chkDebitPgto.Checked = chkChequePgto.Checked = chkCartaoCreditoPgto.Checked = txtValDinPgto.Enabled =
            txtValOutPgto.Enabled = chkDinhePgto.Checked = chkOutrPgto.Checked = false;

            CarregaAnos();
            CarregaModalidades();
            CarregaSerieCurso();
            CarregaTurma();
            CarregaAluno();

            ddlAno.SelectedValue = ddlModalidade.SelectedValue = ddlSerieCurso.SelectedValue = ddlTurma.SelectedValue =
                ddlAlunos.SelectedValue = txtNireAlunoPgto.Text = txtNisAlunoPgto.Text = txtNrContraPgto.Text =
                txtValContrPgto.Text = txtQtPgto.Text = txtValDinPgto.Text = txtValOutPgto.Text = hdCoAluMatr.Value =
                hdCoAnoMatr.Value = hdCoCurMatr.Value = hdCoEmpMatr.Value = hdCoMatrMat.Value = "";

            txtNrContraPgto.Enabled = false;
            txtQtPgto.Enabled = false;

            chkCartaoCreditoPgto.Checked = false;
            LimpaDadosCartoes();
            carregaGridChequesPgto();
            //Response.Redirect("~/GEDUC/2000_CtrlOperSecretariaEscolar/2100_CtrlServSecretariaEscolar/2107_MatriculaAluno/MatriculaLancFormaPgto/Cadastro.aspx?moduloId=1258");
        }

        //É executado quando se clica em salvar
        protected void btnGravar_Click(object sender, EventArgs e)
        {
            CurrentPadraoCadastros_OnAcaoBarraCadastro();
        }

        //É executado quando o usuário clica no botão de excluir o registro
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (hdCoMatrMat.Value == null)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar um Aluno matriculado para exclusão da forma de pagamento.");
                return;
            }
            else
            {
                ////--------> Pesquisa o registro da tabela de Forma de Pagamento da Matrícula
                //TBE220_MATRCUR_PAGTO tbe220 = (from litbe220 in TBE220_MATRCUR_PAGTO.RetornaTodosRegistros()
                //                               where litbe220.CO_ALU_CAD == hdCoMatrMat.Value
                //                               select litbe220).FirstOrDefault();

                TBE220_MATRCUR_PAGTO tbe220 = RetornaEntidade();

                if (tbe220 == null)
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "O Aluno selecionado não possui forma de pagamento associada à sua matrícula.");
                    return;
                }
                else
                {
                    //--------> Pesquisa o registro da tabela de Forma de Pagamento da Matrícula em Cartão
                    List<TBE221_PAGTO_CARTAO> lsttbe221 = (from tbe221 in TBE221_PAGTO_CARTAO.RetornaTodosRegistros()
                                                           where tbe221.TBE220_MATRCUR_PAGTO.ID_MATRCUR_PAGTO == tbe220.ID_MATRCUR_PAGTO
                                                           select tbe221).ToList();

                    //--------> Pesquisa o registro da tabela de Forma de Pagamento da Matrícula em Cheque
                    List<TBE222_PAGTO_CHEQUE> lsttbe222 = (from tbe222 in TBE222_PAGTO_CHEQUE.RetornaTodosRegistros()
                                                           where tbe222.TBE220_MATRCUR_PAGTO.ID_MATRCUR_PAGTO == tbe220.ID_MATRCUR_PAGTO
                                                           select tbe222).ToList();

                    //--------> Exclui o registro da tabela de Forma de Pagamento da Matrícula em Cheque
                    if (lsttbe222 != null)
                    {
                        foreach (TBE222_PAGTO_CHEQUE tbe222ob in lsttbe222)
                        {
                            TBE222_PAGTO_CHEQUE.Delete(tbe222ob, true);
                        }
                    }

                    //--------> Exclui o registro da tabela de Forma de Pagamento da Matrícula em Cartão
                    if (lsttbe221 != null)
                    {
                        foreach (TBE221_PAGTO_CARTAO tbe221ob in lsttbe221)
                        {
                            TBE221_PAGTO_CARTAO.Delete(tbe221ob, true);
                        }
                    }

                    //--------> Exclui o registro da tabela de matricula
                    if (tbe220 != null)
                        TBE220_MATRCUR_PAGTO.Delete(tbe220, true);

                    AuxiliPagina.RedirecionaParaPaginaSucesso("Forma de pagamento e suas dependências excluídas com Sucesso!", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());
                }
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Carrega uma lista com os parâmetros informados, para verificar se o registro em questão já existe.
        /// </summary>
        protected TBE221_PAGTO_CARTAO carregaListaCartaoCredito(int ID_MATRCUR_PAGTO, int? ID_PAGTO_CARTAO)
        {
            TBE221_PAGTO_CARTAO tbe221 = (from tbeli221 in TBE221_PAGTO_CARTAO.RetornaTodosRegistros()
                                          where tbeli221.TBE220_MATRCUR_PAGTO.ID_MATRCUR_PAGTO == ID_MATRCUR_PAGTO
                                          && tbeli221.ID_PAGTO_CARTAO == ID_PAGTO_CARTAO
                                          select tbeli221).FirstOrDefault();
            return tbe221;
        }

        /// <summary>
        /// Carrega uma lista dos cheques relacionados à forma de pagamento e id de cheque informado no parâmetro deste método
        /// </summary>
        /// <param name="ID_MATRCUR_PAGTO"></param>
        /// <param name="ID_PAGTO_CARTAO"></param>
        /// <returns></returns>
        protected TBE222_PAGTO_CHEQUE carregaListaPgtoCheque(int ID_MATRCUR_PAGTO, int? ID_PAGTO_CHEQUE)
        {
            TBE222_PAGTO_CHEQUE tbe222 = (from tbeli222 in TBE222_PAGTO_CHEQUE.RetornaTodosRegistros()
                                          where tbeli222.TBE220_MATRCUR_PAGTO.ID_MATRCUR_PAGTO == ID_MATRCUR_PAGTO
                                          && tbeli222.ID_PAGTO_CHEQUE == ID_PAGTO_CHEQUE
                                          select tbeli222).FirstOrDefault();
            return tbe222;
        }

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
        {
            TBE220_MATRCUR_PAGTO tbe220 = RetornaEntidade();

            if (tbe220 != null)
            {
                //Recupera um objeto do aluno centro desta forma de pagamento, para acessar os demais atributos
                var tb07 = TB07_ALUNO.RetornaTodosRegistros().Where(w => w.CO_ALU == tbe220.CO_ALU_M).FirstOrDefault();

                txtNireAlunoPgto.Text = tb07.NU_NIRE.ToString();
                txtNisAlunoPgto.Text = tb07.NU_NIS.ToString();
                chkDinhePgto.Checked = txtValDinPgto.Enabled = (tbe220.FL_DINHE == "S" ? true : false);
                chkOutrPgto.Checked = txtValOutPgto.Enabled = (tbe220.FL_OUTRO == "S" ? true : false);
                txtValDinPgto.Text = (tbe220.VL_DINHE != null ? tbe220.VL_DINHE.ToString() : "");
                txtValOutPgto.Text = (tbe220.VL_OUTRO != null ? tbe220.VL_OUTRO.ToString() : "");

                //Desabilita os campos dos filtros do aluno, quando se abrir a funcionalidade com um registro já feito.
                ddlAno.Enabled =
                ddlSerieCurso.Enabled =
                ddlModalidade.Enabled =
                ddlTurma.Enabled =
                txtNireAlunoPgto.Enabled =
                txtNisAlunoPgto.Enabled = 
                ddlAlunos.Enabled = false;

                //Busca na tabela de Matrículas, a matrícula relacionada ao registro de forma de pagamento em questão.
                var result = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                           where tb08.CO_ALU_CAD == tbe220.CO_ALU_CAD
                           select new
                           {
                               tb08.CO_ALU,
                               tb08.CO_ANO_MES_MAT,
                               tb08.CO_CUR,
                               tb08.CO_EMP,
                               tb08.CO_ALU_CAD,
                               tb08.QT_PAR_MOD_MAT,
                               tb08.NR_CONTR_MATRI,
                               tb08.TB44_MODULO.CO_MODU_CUR,
                               tb08.CO_TUR,
                               tb08.VL_TOT_MODU_MAT,
                               tb08.VL_DES_MOD_MAT,
                               tb08.VL_DES_BOL_MOD_MAT,

                           }).FirstOrDefault();

                //Atribui os valores obtidos na matrícula aos campos na tela.
                txtNrContraPgto.Text = result.NR_CONTR_MATRI;

                //Calcula o valor do Contrato que aparecerá no campo correspondente
                decimal vltotmod = (result.VL_TOT_MODU_MAT.HasValue ? result.VL_TOT_MODU_MAT.Value : decimal.Parse("0,00"));
                decimal vldesModMat = (result.VL_DES_MOD_MAT.HasValue ? result.VL_DES_MOD_MAT.Value : decimal.Parse("0,00"));
                decimal vldesBolMod = (result.VL_DES_BOL_MOD_MAT.HasValue ? result.VL_DES_BOL_MOD_MAT.Value : decimal.Parse("0,00"));
                decimal vltottot = vltotmod - vldesModMat - vldesBolMod;
                hdValTotContrato.Value = vltottot.ToString();
                txtValContrPgto.Text = vltottot.ToString();

                ddlAno.SelectedValue = result.CO_ANO_MES_MAT;
                CarregaModalidades();
                ddlModalidade.SelectedValue = result.CO_MODU_CUR.ToString();
                CarregaSerieCurso();
                ddlSerieCurso.SelectedValue = result.CO_CUR.ToString();
                CarregaTurma();
                ddlTurma.SelectedValue = result.CO_TUR.ToString();
                CarregaAluno();
                ddlAlunos.SelectedValue = result.CO_ALU.ToString();
                txtQtPgto.Text = result.QT_PAR_MOD_MAT.ToString();

                //Método que Carrega os Cartões com o Parâmetro o objeto da tabela de forma de pagamento
                carregaCartoes(tbe220);
                //Método que Carrega os Cheques com o Parâmetro o objeto da tabela de forma de pagamento
                CarregaCheques(tbe220);

                hdCoAluMatr.Value = result.CO_ALU.ToString();
                hdCoAnoMatr.Value = result.CO_ANO_MES_MAT;
                hdCoCurMatr.Value = result.CO_CUR.ToString();
                hdCoEmpMatr.Value = result.CO_EMP.ToString();
                hdCoMatrMat.Value = result.CO_ALU_CAD;
            }
            else
            {
                ddlAno.Enabled =
                ddlSerieCurso.Enabled =
                ddlModalidade.Enabled =
                ddlTurma.Enabled =
                txtNireAlunoPgto.Enabled =
                txtNisAlunoPgto.Enabled = 
                ddlAlunos.Enabled = true;
            }
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TBE220_MATRCUR_PAGTO</returns>
        private TBE220_MATRCUR_PAGTO RetornaEntidade()
        {
            if (hdAuxQueryString.Value == "1")
                  return new TBE220_MATRCUR_PAGTO();
            else
            {
                TBE220_MATRCUR_PAGTO tbe220 = TBE220_MATRCUR_PAGTO.RetornaPelaChavePrimaria(QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Id));
                return (tbe220 == null) ? new TBE220_MATRCUR_PAGTO() : tbe220;
            }

        }
        
        protected void LimpaDadosCartoes()
        {
                            //Server para desabilitar todos os campos e limpar todos os dados da tela, simulando um clicar no botão "Novo".
            hdIDCC1.Value = txtNumPgto1.Text = txtTitulPgto1.Text = txtVencPgto1.Text = txtTitulPgto3.Text = txtAgenPgto1.Text = 
            txtValCarPgto1.Text = hdIDCC2.Value = txtNumPgto2.Text = txtTitulPgto2.Text = txtVencPgto3.Text = txtNContPgto1.Text = 
            txtVencPgto2.Text = txtValCarPgto2.Text = hdIDCC3.Value = txtNumPgto3.Text = txtValCarPgto3.Text =
            txtNuDebtPgto1.Text = txtNuTitulDebitPgto1.Text = txtValDebitPgto1.Text = hdIDCDebt2.Value = txtAgenPgto2.Text =
            txtNContPgto2.Text = txtNuDebtPgto2.Text = txtNuTitulDebitPgto2.Text = txtValDebitPgto2.Text = hdIDCDebt3.Value =
            txtAgenPgto3.Text = txtNContPgto3.Text = txtNuDebtPgto3.Text = txtNuTitulDebitPgto3.Text = txtValDebitPgto3.Text = "";

                ddlBandePgto1.SelectedValue = ddlBandePgto2.SelectedValue = ddlBandePgto3.SelectedValue = "N";

                CarregaBancoChe(ddlBcoPgto1, "N");
                CarregaBancoChe(ddlBcoPgto2, "N");
                CarregaBancoChe(ddlBcoPgto3, "N");

                //Habilita os Campos que possuem registros
                ddlBandePgto1.Enabled =
                ddlBandePgto2.Enabled =
                ddlBandePgto3.Enabled =
                txtNumPgto1.Enabled =
                txtTitulPgto1.Enabled =
                txtVencPgto1.Enabled =
                txtValCarPgto1.Enabled = false;

                //Habilita os Campos que possuem registros
                ddlBandePgto1.Enabled =
                ddlBandePgto2.Enabled =
                ddlBandePgto3.Enabled =
                txtNumPgto2.Enabled =
                txtTitulPgto2.Enabled =
                txtVencPgto2.Enabled =
                txtValCarPgto2.Enabled = false;

                //Habilita os Campos que possuem registros
                ddlBandePgto1.Enabled =
                ddlBandePgto2.Enabled =
                ddlBandePgto3.Enabled =
                txtNumPgto3.Enabled =
                txtTitulPgto3.Enabled =
                txtVencPgto3.Enabled =
                txtValCarPgto3.Enabled = false;

                //Habilita os Campos que possuem registros
                ddlBcoPgto1.Enabled =
                ddlBcoPgto2.Enabled =
                ddlBcoPgto3.Enabled =
                txtAgenPgto1.Enabled =
                txtNContPgto1.Enabled =
                txtNuDebtPgto1.Enabled =
                txtNuTitulDebitPgto1.Enabled =
                txtValDebitPgto1.Enabled = false;
                
                //Habilita os Campos que possuem registros
                ddlBcoPgto1.Enabled =
                ddlBcoPgto2.Enabled =
                ddlBcoPgto3.Enabled =
                txtAgenPgto2.Enabled =
                txtNContPgto2.Enabled =
                txtNuDebtPgto2.Enabled =
                txtNuTitulDebitPgto2.Enabled =
                txtValDebitPgto2.Enabled = false;

                //Habilita os Campos que possuem registros
                ddlBcoPgto1.Enabled =
                ddlBcoPgto2.Enabled =
                ddlBcoPgto3.Enabled =
                txtAgenPgto3.Enabled =
                txtNContPgto3.Enabled =
                txtNuDebtPgto3.Enabled =
                txtNuTitulDebitPgto3.Enabled =
                txtValDebitPgto3.Enabled = false;
        }

        //Carrega os registros dos cartões relacionados ao Aluno escolhido.
        protected void carregaCartoes(TBE220_MATRCUR_PAGTO tbe220)
        {

                var listtbe221 = (from tbe221 in TBE221_PAGTO_CARTAO.RetornaTodosRegistros()
                                  where tbe221.TBE220_MATRCUR_PAGTO.ID_MATRCUR_PAGTO == tbe220.ID_MATRCUR_PAGTO
                                  select new
                                  {
                                      tbe221.ID_PAGTO_CARTAO,
                                      tbe221.VL_PAGTO,
                                      tbe221.FL_TIPO_TRANSAC,
                                      tbe221.NO_TITUL,
                                      tbe221.CO_BANDE,
                                      tbe221.CO_NUMER,
                                      tbe221.DT_VENCI,
                                      tbe221.CO_BCO,
                                      tbe221.NR_AGENCI,
                                      tbe221.NR_CONTA,
                                  }).ToList();

                //Declara as Variáveis que auxiliarão no preenchimento dos campos
                int auxCD = 0;
                int auxCC = 0;
                int auxCH = 0;

                CarregaBanco(ddlBcoPgto1);
                CarregaBanco(ddlBcoPgto2);
                CarregaBanco(ddlBcoPgto3);

                //Alimenta as Tags do Pagamento em Cartão de Débito e Crédito
                foreach (var at in listtbe221)
                {
                    if (at.FL_TIPO_TRANSAC == "C")
                    {
                        auxCC++;
                        switch (auxCC)
                        {
                            //Carrega os registros da primeira linha
                            case 1:
                                hdIDCC1.Value = at.ID_PAGTO_CARTAO.ToString();
                                ddlBandePgto1.SelectedValue = at.CO_BANDE;
                                txtNumPgto1.Text = at.CO_NUMER;
                                txtTitulPgto1.Text = at.NO_TITUL;
                                txtVencPgto1.Text = at.DT_VENCI;
                                txtValCarPgto1.Text = at.VL_PAGTO.ToString();

                                //Habilita os Campos que possuem registros
                                chkCartaoCreditoPgto.Checked = true;
                                ddlBandePgto1.Enabled =
                                ddlBandePgto2.Enabled =
                                ddlBandePgto3.Enabled =
                                txtNumPgto1.Enabled =
                                txtTitulPgto1.Enabled =
                                txtVencPgto1.Enabled =
                                txtValCarPgto1.Enabled = true;
                                break;

                            //Carrega os registros da Segunda linha
                            case 2:
                                hdIDCC2.Value = at.ID_PAGTO_CARTAO.ToString();
                                ddlBandePgto2.SelectedValue = at.CO_BANDE;
                                txtNumPgto2.Text = at.CO_NUMER;
                                txtTitulPgto2.Text = at.NO_TITUL;
                                txtVencPgto2.Text = at.DT_VENCI;
                                txtValCarPgto2.Text = at.VL_PAGTO.ToString();

                                //Habilita os Campos que possuem registros
                                chkCartaoCreditoPgto.Checked = true;
                                ddlBandePgto1.Enabled =
                                ddlBandePgto2.Enabled =
                                ddlBandePgto3.Enabled =
                                txtNumPgto2.Enabled =
                                txtTitulPgto2.Enabled =
                                txtVencPgto2.Enabled =
                                txtValCarPgto2.Enabled = true;
                                break;

                            //Carrega os registros da terceira linha
                            case 3:
                                hdIDCC3.Value = at.ID_PAGTO_CARTAO.ToString();
                                ddlBandePgto3.SelectedValue = at.CO_BANDE;
                                txtNumPgto3.Text = at.CO_NUMER;
                                txtTitulPgto3.Text = at.NO_TITUL;
                                txtVencPgto3.Text = at.DT_VENCI;
                                txtValCarPgto3.Text = at.VL_PAGTO.ToString();

                                //Habilita os Campos que possuem registros
                                chkCartaoCreditoPgto.Checked = true;
                                ddlBandePgto1.Enabled =
                                ddlBandePgto2.Enabled =
                                ddlBandePgto3.Enabled =
                                txtNumPgto3.Enabled =
                                txtTitulPgto3.Enabled =
                                txtVencPgto3.Enabled =
                                txtValCarPgto3.Enabled = true;
                                break;
                        }
                    }
                    else if (at.FL_TIPO_TRANSAC == "D")
                    {
                        auxCD++;
                        switch (auxCD)
                        {
                            case 1:
                                CarregaBancoChe(ddlBcoPgto1, (at.CO_BCO.Value.ToString().PadLeft(3, '0')));
                                hdIDCDebt1.Value = at.ID_PAGTO_CARTAO.ToString();
                                //ddlBcoPgto1.SelectedValue = at.CO_BCO.Value.ToString();
                                txtAgenPgto1.Text = at.NR_AGENCI;
                                txtNContPgto1.Text = at.NR_CONTA;
                                txtNuDebtPgto1.Text = at.CO_NUMER;
                                txtNuTitulDebitPgto1.Text = at.NO_TITUL;
                                txtValDebitPgto1.Text = at.VL_PAGTO.ToString();

                                //Habilita os Campos que possuem registros
                                chkDebitPgto.Checked = true;
                                ddlBcoPgto1.Enabled =
                                ddlBcoPgto2.Enabled =
                                ddlBcoPgto3.Enabled =
                                txtAgenPgto1.Enabled =
                                txtNContPgto1.Enabled =
                                txtNuDebtPgto1.Enabled =
                                txtNuTitulDebitPgto1.Enabled =
                                txtValDebitPgto1.Enabled = true;
                                break;

                            case 2:
                                CarregaBancoChe(ddlBcoPgto2, (at.CO_BCO.Value.ToString().PadLeft(3, '0')));
                                hdIDCDebt2.Value = at.ID_PAGTO_CARTAO.ToString();
                                //ddlBcoPgto2.SelectedValue = at.CO_BCO.Value.ToString();
                                txtAgenPgto2.Text = at.NR_AGENCI;
                                txtNContPgto2.Text = at.NR_CONTA;
                                txtNuDebtPgto2.Text = at.CO_NUMER;
                                txtNuTitulDebitPgto2.Text = at.NO_TITUL;
                                txtValDebitPgto2.Text = at.VL_PAGTO.ToString();

                                //Habilita os Campos que possuem registros
                                chkDebitPgto.Checked = true;
                                ddlBcoPgto1.Enabled =
                                ddlBcoPgto2.Enabled =
                                ddlBcoPgto3.Enabled =
                                txtAgenPgto2.Enabled =
                                txtNContPgto2.Enabled =
                                txtNuDebtPgto2.Enabled =
                                txtNuTitulDebitPgto2.Enabled =
                                txtValDebitPgto2.Enabled = true;
                                break;

                            case 3:
                                CarregaBancoChe(ddlBcoPgto3, (at.CO_BCO.Value.ToString().PadLeft(3, '0')));
                                hdIDCDebt3.Value = at.ID_PAGTO_CARTAO.ToString();
                                //ddlBcoPgto3.SelectedValue = at.CO_BCO.Value.ToString();
                                txtAgenPgto3.Text = at.NR_AGENCI;
                                txtNContPgto3.Text = at.NR_CONTA;
                                txtNuDebtPgto3.Text = at.CO_NUMER;
                                txtNuTitulDebitPgto3.Text = at.NO_TITUL;
                                txtValDebitPgto3.Text = at.VL_PAGTO.ToString();

                                //Habilita os Campos que possuem registros
                                chkDebitPgto.Checked = true;
                                ddlBcoPgto1.Enabled =
                                ddlBcoPgto2.Enabled =
                                ddlBcoPgto3.Enabled =
                                txtAgenPgto3.Enabled =
                                txtNContPgto3.Enabled =
                                txtNuDebtPgto3.Enabled =
                                txtNuTitulDebitPgto3.Enabled =
                                txtValDebitPgto3.Enabled = true;
                                break;
                        }
                    }
                }
        }

        //Calcula a Bandeira do Cartão de Crédito da Forma de Pagamento
        public static string calculaBandeiraCartao(string coBand)
        {
            string nomeBandeira = "***";
            switch (coBand)
            {
                case "Vis":
                    nomeBandeira = "Visa";
                    break;

                case "MasCar":
                    nomeBandeira = "Master Card";
                    break;

                case "HipCar":
                    nomeBandeira = "HiperCard";
                    break;

                case "Elo":
                    nomeBandeira = "Elo";
                    break;

                case "AmeExp":
                    nomeBandeira = "American Express";
                    break;

                case "BNDES":
                    nomeBandeira = "BNDES";
                    break;

                case "SorCr":
                    nomeBandeira = "SoroCred";
                    break;

                case "DinClub":
                    nomeBandeira = "Diners Club";
                    break;
            }

            return nomeBandeira;
        }

        //Carrega a Grid de cheques
        protected void CarregaCheques(TBE220_MATRCUR_PAGTO tbe220)
        {
            var res = (from tbe222 in TBE222_PAGTO_CHEQUE.RetornaTodosRegistros()
                       where tbe222.TBE220_MATRCUR_PAGTO.ID_MATRCUR_PAGTO == tbe220.ID_MATRCUR_PAGTO
                       select new ChequesPagamento
                       {
                           CodChePgto = tbe222.ID_PAGTO_CHEQUE,
                           AgenChe = tbe222.NR_AGENCI,
                           nuConChe = tbe222.NR_CONTA,
                           nuCheChe = tbe222.NR_CHEQUE,
                           nuCpfChe = tbe222.NR_CPF,
                           noTituChe = tbe222.NO_TITUL,
                           vlCheCru = tbe222.VL_PAGTO,
                           dtVencCru = tbe222.DT_VENC,
                           ddlBcoCheReceb = tbe222.CO_BCO,
                       }).ToList();

            grdChequesPgto.DataSource = res;
            grdChequesPgto.DataBind();


            //Percorre todos os registros da Grid de Cheques, e atribui os valores da Hidden de cada registro ao SelectedValue do DropDownList correspondente.
            CarregaBancosComContexto();

            //Traz todas as linhas do cheque provenientes da forma de pagamento em questão, marcadas.
            foreach (GridViewRow lin in grdChequesPgto.Rows)
            {
                CheckBox chk = ((CheckBox)lin.Cells[0].FindControl("chkselectGridPgtoCheque"));   
                chk.Checked = true;

                HabilitCamposGridCheques(lin, true);
                if (grdChequesPgto.Rows.Count > 0)
                    grdChequesPgto.Enabled = chkChequePgto.Checked = true;
            }
        }

        #region Classe de Saída da Grid de Cheques

        public class ChequesPagamento
        {
            public int CodChePgto { get; set; }
            public string IDChePgto
            {
                get
                {
                    return this.CodChePgto.ToString();
                }
            }
            public string AgenChe { get; set; }
            public string nuConChe { get; set; }
            public string nuCheChe { get; set; }
            public string nuCpfChe { get; set; }
            public string noTituChe { get; set; }
            public decimal? vlCheCru { get; set; }
            public string vlCheChe
            {
                get
                {
                    return (this.vlCheCru.HasValue ? this.vlCheCru.Value.ToString() : "");
                }
            }
            public DateTime? dtVencCru { get; set; }
            public string dtVencChe
            {
                get
                {
                    return (this.dtVencCru.HasValue ? dtVencCru.Value.ToString("dd/MM/yyyy") : "");
                }
            }
            public int? ddlBcoCheReceb { get; set; }
            public string BCO
            {
                get
                {
                    return (this.ddlBcoCheReceb.HasValue ? this.ddlBcoCheReceb.Value.ToString() : "");
                }
            }
            
        }

        #endregion

        /// <summary>
        /// Método que carrega o dropdown de Bancos
        /// </summary>
        protected void CarregaBanco(DropDownList ddlBco)
        {
            ddlBco.DataSource = TB29_BANCO.RetornaTodosRegistros();

            ddlBco.DataTextField = "IDEBANCO";
            ddlBco.DataValueField = "IDEBANCO";
            ddlBco.DataBind();

            ddlBco.Items.Insert(0, new ListItem("Nenhum", "N"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Bancos com contexto do que virá selecionado
        /// </summary>
        protected void CarregaBancoChe(DropDownList ddlBco, string opBanc)
        {
            ddlBco.DataSource = TB29_BANCO.RetornaTodosRegistros();

            ddlBco.DataTextField = "IDEBANCO";
            ddlBco.DataValueField = "IDEBANCO";
            ddlBco.DataBind();

            ddlBco.Items.Insert(0, new ListItem("Nenhum", "N"));

            if (opBanc != "")
                ddlBco.SelectedValue = opBanc;
        }

        /// <summary>
        /// Percorre todos os registros da Grid de Cheques, e atribui os valores da Hidden de cada registro ao SelectedValue do DropDownList correspondente.
        /// </summary>
        protected void CarregaBancosComContexto()
        {
            foreach (GridViewRow grvi in grdChequesPgto.Rows)
            {
                DropDownList ddlBcoCh = ((DropDownList)grvi.Cells[1].FindControl("ddlBcoChequePgto"));
                string ddlbco = ((HiddenField)grvi.Cells[1].FindControl("hdddlBcoChePgto")).Value.PadLeft(3, '0');
                CarregaBancoChe(ddlBcoCh, ddlbco);
            }
        }

        protected void CarregaBancoRefeito1()
        {
            var res = (from tb29 in TB29_BANCO.RetornaTodosRegistros()
                       select new { tb29.IDEBANCO }).ToList();

            ddlBcoPgto1.DataTextField = "IDEBANCO";
            ddlBcoPgto1.DataValueField = "IDEBANCO";
            ddlBcoPgto1.DataSource = res;
            ddlBcoPgto1.DataBind();

            ddlBcoPgto1.Items.Insert(0, new ListItem("Nenhum", "N"));
        }

        protected void CarregaBancoRefeito2()
        {
            var res = (from tb29 in TB29_BANCO.RetornaTodosRegistros()
                       select new { tb29.IDEBANCO }).ToList();

            ddlBcoPgto2.DataTextField = "IDEBANCO";
            ddlBcoPgto2.DataValueField = "IDEBANCO";
            ddlBcoPgto2.DataSource = res;
            ddlBcoPgto2.DataBind();

            ddlBcoPgto2.Items.Insert(0, new ListItem("Nenhum", "N"));
        }

        protected void CarregaBancoRefeito3()
        {
            var res = (from tb29 in TB29_BANCO.RetornaTodosRegistros()
                       select new { tb29.IDEBANCO }).ToList();

            ddlBcoPgto3.DataTextField = "IDEBANCO";
            ddlBcoPgto3.DataValueField = "IDEBANCO";
            ddlBcoPgto3.DataSource = res;
            ddlBcoPgto3.DataBind();

            ddlBcoPgto3.Items.Insert(0, new ListItem("Nenhum", "N"));
        }

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            ddlAno.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                 where tb08.CO_EMP == LoginAuxili.CO_EMP
                                 select new { tb08.CO_ANO_MES_MAT }).Distinct().OrderByDescending(g => g.CO_ANO_MES_MAT);

            ddlAno.DataTextField = "CO_ANO_MES_MAT";
            ddlAno.DataValueField = "CO_ANO_MES_MAT";
            ddlAno.DataBind();

            ddlAno.Items.Insert(0, new ListItem("Selecione", ""));
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
            string anoGrade = ddlAno.SelectedValue;

            if (modalidade != 0)
            {
                ddlSerieCurso.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                            where tb43.TB44_MODULO.CO_MODU_CUR == modalidade
                                            join tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on tb43.CO_CUR equals tb01.CO_CUR
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
                                       select new { tb06.TB129_CADTURMAS.NO_TURMA, tb06.CO_TUR });

                ddlTurma.DataTextField = "NO_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataBind();
            }
            else
                ddlTurma.Items.Clear();

            ddlTurma.Items.Insert(0, new ListItem("Selecione", ""));

        }

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        private void CarregaAluno()
        {
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            string coAnoMesMat = ddlAno.SelectedValue != "" ? ddlAno.SelectedValue : "0";

            ddlAlunos.Items.Clear();
            ddlAlunos.Items.Insert(0, new ListItem("Selecione", ""));

            if (serie != 0)
            {
                 var res = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                        where tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP && tb08.CO_CUR == serie && tb08.CO_ANO_MES_MAT == coAnoMesMat
                                        && tb08.TB44_MODULO.CO_MODU_CUR == modalidade && tb08.CO_TUR == turma
                                        select new { tb08.TB07_ALUNO.CO_ALU, tb08.TB07_ALUNO.NO_ALU }).Distinct().OrderBy(m => m.NO_ALU);

                ddlAlunos.DataTextField = "NO_ALU";
                ddlAlunos.DataValueField = "CO_ALU";
                ddlAlunos.DataSource = res;
                ddlAlunos.DataBind();

                ddlAlunos.Items.Insert(0, new ListItem("Selecione", ""));

            }
        }

        /// <summary>
        /// Método encarregado de liberar ou não os campos relacionados ao registro selecionado, passado como parâmetro
        /// </summary>
        /// <param name="linha"></param>
        /// <param name="libera"></param>
        private void HabilitCamposGridCheques(GridViewRow linha, bool libera)
        {
            DropDownList vlBcoChe = ((DropDownList)linha.Cells[1].FindControl("ddlBcoChequePgto"));
            TextBox vlAgeChe = ((TextBox)linha.Cells[2].FindControl("txtAgenChequePgto"));
            TextBox vlConChe = ((TextBox)linha.Cells[3].FindControl("txtNrContaChequeConta"));
            TextBox vlNuChe = ((TextBox)linha.Cells[4].FindControl("txtNrChequePgto"));
            TextBox vlNuCpf = ((TextBox)linha.Cells[5].FindControl("txtNuCpfChePgto"));
            TextBox vlNoTit = ((TextBox)linha.Cells[6].FindControl("txtTitulChequePgto"));
            TextBox vlPgtChe = ((TextBox)linha.Cells[7].FindControl("txtVlChequePgto"));
            TextBox dtVenChe = ((TextBox)linha.Cells[8].FindControl("txtDtVencChequePgto"));

            vlBcoChe.Enabled = vlAgeChe.Enabled = vlConChe.Enabled = vlNuChe.Enabled = vlNuCpf.Enabled = vlNoTit.Enabled
              = vlPgtChe.Enabled = dtVenChe.Enabled = true;
        }

        #endregion

        #region Funções de Campo

        #region Funções de Campo na Aba Forma de Pagamento

        #region Funções na Grid de Cheques
        //é Executado quando se clica no checkbox de Cheque, se for habilitado, a grid é liberada, caso não , a grid é inativada
        protected void chkChequePgto_OnCheckedChanged(object sender, EventArgs e)
        {
            if (chkChequePgto.Checked)
                grdChequesPgto.Enabled = true;
            else
                grdChequesPgto.Enabled = false;
        }

        protected void chkselectGridPgtoCheque_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            CheckBox chk;

            // Passa por todos os registros da grid de atividades
            foreach (GridViewRow linha in grdChequesPgto.Rows)
            {
                chk = (CheckBox)linha.Cells[0].FindControl("chkselectGridPgtoCheque");

                // Desmarca todos os registros menos o que foi clicado
                if (chk.ClientID == atual.ClientID)
                {

                    //Verifica se o checkbox foi marcado, caso tenha sido, ele habilita todos os campos da grid relacionados à este registro
                    if (atual.Checked)
                    {
                        HabilitCamposGridCheques(linha, true);
                    }
                    else
                    {
                        HabilitCamposGridCheques(linha, false);
                    }
                }
            }
        }

        #endregion
        //É executado quando se clica no checkbox de Dinheiro, então é habilitado o campo para que se informe o valor, caso seja desclicado, é desabilitado
        protected void chkDinhePgto_OnCheckedChanged(object sender, EventArgs e)
        {
            if (chkDinhePgto.Checked)
                txtValDinPgto.Enabled = true;
            else
                txtValDinPgto.Enabled = false;
        }
        //É executado quando se clica no checkbox de Outros, então é habilitado o campo para que se informe o valor, caso seja desclicado, é desabilitado
        protected void chkOutrPgto_OnCheckedChanged(object sender, EventArgs e)
        {
            if (chkOutrPgto.Checked)
                txtValOutPgto.Enabled = true;
            else
                txtValOutPgto.Enabled = false;
        }

        #region Cartão de Débito na Aba Forma de Pagamento

        //Habilitado os DropDownList do Banco do Cartão, quando for clicado no checkbox
        protected void chkDebitPgto_OnCheckedChanged(object sender, EventArgs e)
        {
            if (chkDebitPgto.Checked)
            {
                ddlBcoPgto1.Enabled = true;

                //Verifica, quando for clicado o checkbox do pagamento em Débito, se o DropDownList tem valor diferente de Nenhum, ele habilita todos os campos daquele registro
                if (ddlBcoPgto1.SelectedValue != "N")
                {
                    txtAgenPgto1.Enabled =
                    txtNContPgto1.Enabled =
                    txtNuDebtPgto1.Enabled =
                    txtNuTitulDebitPgto1.Enabled =
                    txtValDebitPgto1.Enabled = true;
                }

                ddlBcoPgto2.Enabled = true;

                //Verifica, quando for clicado o checkbox do pagamento em Débito, se o DropDownList tem valor diferente de Nenhum, ele habilita todos os campos daquele registro
                if (ddlBcoPgto2.SelectedValue != "N")
                {
                    txtAgenPgto2.Enabled =
                    txtNContPgto2.Enabled =
                    txtNuDebtPgto2.Enabled =
                    txtNuTitulDebitPgto2.Enabled =
                    txtValDebitPgto2.Enabled = true;
                }

                ddlBcoPgto3.Enabled = true;

                //Verifica, quando for clicado o checkbox do pagamento em Débito, se o DropDownList tem valor diferente de Nenhum, ele habilita todos os campos daquele registro
                if (ddlBcoPgto3.SelectedValue != "N")
                {
                    txtAgenPgto3.Enabled =
                    txtNContPgto3.Enabled =
                    txtNuDebtPgto3.Enabled =
                    txtNuTitulDebitPgto3.Enabled =
                    txtValDebitPgto3.Enabled = true;
                }
            }
            else
            {
                ddlBcoPgto1.Enabled =
                ddlBcoPgto2.Enabled =
                ddlBcoPgto3.Enabled = false;

                //Desabilita a primeira linha
                txtAgenPgto1.Enabled =
                txtNContPgto1.Enabled =
                txtNuDebtPgto1.Enabled =
                txtNuTitulDebitPgto1.Enabled =
                txtValDebitPgto1.Enabled = false;

                //Desabilita a segunda linha
                txtAgenPgto2.Enabled =
                txtNContPgto2.Enabled =
                txtNuDebtPgto2.Enabled =
                txtNuTitulDebitPgto2.Enabled =
                txtValDebitPgto2.Enabled = false;

                //Desabilita a Terceira linha
                txtAgenPgto3.Enabled =
                txtNContPgto3.Enabled =
                txtNuDebtPgto3.Enabled =
                txtNuTitulDebitPgto3.Enabled =
                txtValDebitPgto3.Enabled = false;
            }
        }

        //É executado quando o usuário escolhe um banco no DropDownList de bancos, o que faz habilitar as outras informações do registro, garantindo consistência
        protected void ddlBcoPgto1_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBcoPgto1.SelectedValue != "N")
            {
                txtAgenPgto1.Enabled =
                txtNContPgto1.Enabled =
                txtNuDebtPgto1.Enabled =
                txtNuTitulDebitPgto1.Enabled =
                txtValDebitPgto1.Enabled = true;
            }
            else
            {
                txtAgenPgto1.Enabled =
                txtNContPgto1.Enabled =
                txtNuDebtPgto1.Enabled =
                txtNuTitulDebitPgto1.Enabled =
                txtValDebitPgto1.Enabled = false;
            }
        }

        //É executado quando o usuário escolhe um banco no DropDownList de bancos, o que faz habilitar as outras informações do registro, garantindo consistência
        protected void ddlBcoPgto2_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBcoPgto2.SelectedValue != "N")
            {
                txtAgenPgto2.Enabled =
                txtNContPgto2.Enabled =
                txtNuDebtPgto2.Enabled =
                txtNuTitulDebitPgto2.Enabled =
                txtValDebitPgto2.Enabled = true;
            }
            else
            {
                txtAgenPgto2.Enabled =
                txtNContPgto2.Enabled =
                txtNuDebtPgto2.Enabled =
                txtNuTitulDebitPgto2.Enabled =
                txtValDebitPgto2.Enabled = false;
            }
        }

        //É executado quando o usuário escolhe um banco no DropDownList de bancos, o que faz habilitar as outras informações do registro, garantindo consistência
        protected void ddlBcoPgto3_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBcoPgto3.SelectedValue != "N")
            {
                txtAgenPgto3.Enabled =
                txtNContPgto3.Enabled =
                txtNuDebtPgto3.Enabled =
                txtNuTitulDebitPgto3.Enabled =
                txtValDebitPgto3.Enabled = true;
            }
            else
            {
                txtAgenPgto3.Enabled =
                txtNContPgto3.Enabled =
                txtNuDebtPgto3.Enabled =
                txtNuTitulDebitPgto3.Enabled =
                txtValDebitPgto3.Enabled = false;
            }
        }

        //Registra a nova linha à ser inserida na Grid de Cheques
        #region Nova Linha Grid
        protected void CriaNovaLinhaGridChequesPgto()
        {

            DataTable dtV = new DataTable();
            DataColumn dcATM;


            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "IDChePgto";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "BCO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "BcoChe";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "AgenChe";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "nuConChe";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "nuCheChe";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "nuCpfChe";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "noTituChe";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "vlCheChe";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "dtVencChe";
            dtV.Columns.Add(dcATM);

            DataRow linha;
            foreach (GridViewRow li in grdChequesPgto.Rows)
            {
                linha = dtV.NewRow();
                linha["IDChePgto"] = ((HiddenField)li.Cells[1].FindControl("hidCodPgtoChe")).Value;
                linha["BCO"] = ((HiddenField)li.Cells[1].FindControl("hdddlBcoChePgto")).Value;
                linha["BcoChe"] = ((DropDownList)li.Cells[1].FindControl("ddlBcoChequePgto")).SelectedValue;
                linha["AgenChe"] = ((TextBox)li.Cells[2].FindControl("txtAgenChequePgto")).Text;
                linha["nuConChe"] = ((TextBox)li.Cells[3].FindControl("txtNrContaChequeConta")).Text;
                linha["nuCheChe"] = ((TextBox)li.Cells[4].FindControl("txtNrChequePgto")).Text;
                linha["nuCpfChe"] = ((TextBox)li.Cells[5].FindControl("txtNuCpfChePgto")).Text;
                linha["noTituChe"] = ((TextBox)li.Cells[6].FindControl("txtTitulChequePgto")).Text;
                linha["vlCheChe"] = ((TextBox)li.Cells[7].FindControl("txtVlChequePgto")).Text;
                linha["dtVencChe"] = ((TextBox)li.Cells[8].FindControl("txtDtVencChequePgto")).Text;
                dtV.Rows.Add(linha);
            }

            linha = dtV.NewRow();
            linha["IDChePgto"] = "";
            linha["BCO"] = "";
            linha["BcoChe"] = "";
            linha["AgenChe"] = "";
            linha["nuConChe"] = "";
            linha["nuCheChe"] = "";
            linha["nuCpfChe"] = "";
            linha["noTituChe"] = "";
            linha["vlCheChe"] = "";
            linha["dtVencChe"] = "";
            dtV.Rows.Add(linha);

            Session["GridCheques"] = dtV;

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

            dtV = (DataTable)Session["GridCheques"];

            grdChequesPgto.DataSource = dtV;
            grdChequesPgto.DataBind();

            //foreach (GridViewRow lipgtoaux in grdChequesPgto.Rows)
            //{
            //    DropDownList ddlbcoauxChe = ((DropDownList)lipgtoaux.Cells[1].FindControl("ddlBcoChequePgto"));
            //    CarregaBanco(ddlbcoauxChe);
            //}

            CarregaBancosComContexto();
        }

        /// <summary>
        /// Carrega a Grid de Cheques da aba de Formas de Pagamento
        /// </summary>
        protected void carregaGridChequesPgto()
        {
            DataTable dtV = new DataTable();
            DataColumn dcATM;

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "IDChePgto";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "BCO";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "BcoChe";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "AgenChe";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "nuConChe";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "nuCheChe";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "nuCpfChe";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "noTituChe";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "vlCheChe";
            dtV.Columns.Add(dcATM);

            dcATM = new DataColumn();
            dcATM.DataType = System.Type.GetType("System.String");
            dcATM.ColumnName = "dtVencChe";
            dtV.Columns.Add(dcATM);

            int i = 1;
            DataRow linha;
            while (i <= 6)
            {
                linha = dtV.NewRow();
                linha["IDChePgto"] = "";
                linha["BCO"] = "";
                linha["BcoChe"] = "";
                linha["AgenChe"] = "";
                linha["nuConChe"] = "";
                linha["nuCheChe"] = "";
                linha["nuCpfChe"] = "";
                linha["noTituChe"] = "";
                linha["vlCheChe"] = "";
                linha["dtVencChe"] = "";
                dtV.Rows.Add(linha);
                i++;
            }

            HttpContext.Current.Session.Add("GridCheques", dtV);


            grdChequesPgto.DataSource = dtV;
            grdChequesPgto.DataBind();

            foreach (GridViewRow lipgtoaux in grdChequesPgto.Rows)
            {
                DropDownList ddlbcoauxChe = ((DropDownList)lipgtoaux.Cells[1].FindControl("ddlBcoChequePgto"));
                CarregaBanco(ddlbcoauxChe);
            }
        }

        protected void btnMaisLinhaChequePgto_OnClick(object sender, EventArgs e)
        {
            CriaNovaLinhaGridChequesPgto();
        }

        #endregion

        #endregion

        #region Cartão de Crédito na Aba Forma de Pagamento

        //Habilitado os DropDownList do Banco do Cartão, quando for clicado no checkbox
        protected void chkCartaoCreditoPgto_OnCheckedChanged(object sender, EventArgs e)
        {
            if (chkCartaoCreditoPgto.Checked)
            {
                ddlBandePgto1.Enabled = true;

                //Verifica, quando for clicado o checkbox do pagamento em Débito, se o DropDownList tem valor diferente de Nenhum, ele habilita todos os campos daquele registro
                if (ddlBandePgto1.SelectedValue != "N")
                {
                    txtNumPgto1.Enabled =
                    txtTitulPgto1.Enabled =
                    txtVencPgto1.Enabled =
                    txtValCarPgto1.Enabled = true;
                }

                ddlBandePgto2.Enabled = true;

                //Verifica, quando for clicado o checkbox do pagamento em Débito, se o DropDownList tem valor diferente de Nenhum, ele habilita todos os campos daquele registro
                if (ddlBandePgto2.SelectedValue != "N")
                {
                    txtNumPgto2.Enabled =
                    txtTitulPgto2.Enabled =
                    txtVencPgto2.Enabled =
                    txtValCarPgto2.Enabled = true;
                }

                ddlBandePgto3.Enabled = true;

                //Verifica, quando for clicado o checkbox do pagamento em Débito, se o DropDownList tem valor diferente de Nenhum, ele habilita todos os campos daquele registro
                if (ddlBandePgto3.SelectedValue != "N")
                {
                    txtNumPgto1.Enabled =
                    txtTitulPgto1.Enabled =
                    txtVencPgto1.Enabled =
                    txtValCarPgto1.Enabled = true;
                }
            }
            else
            {
                ddlBandePgto1.Enabled =
                ddlBandePgto2.Enabled =
                ddlBandePgto3.Enabled = false;

                //Desabilita a primeira linha
                txtNumPgto1.Enabled =
                txtTitulPgto1.Enabled =
                txtVencPgto1.Enabled =
                txtValCarPgto1.Enabled = false;

                //Desabilita a segunda linha
                txtNumPgto2.Enabled =
                txtTitulPgto2.Enabled =
                txtVencPgto2.Enabled =
                txtValCarPgto2.Enabled = false;

                //Desabilita a terceira linha
                txtNumPgto3.Enabled =
                txtTitulPgto3.Enabled =
                txtVencPgto3.Enabled =
                txtValCarPgto3.Enabled = false;
            }
        }

        //É executado quando o usuário escolhe um banco no DropDownList da Bandeira, o que faz habilitar as outras informações do registro, garantindo consistência
        protected void ddlBandePgto1_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBandePgto1.SelectedValue != "N")
            {
                txtNumPgto1.Enabled =
                txtTitulPgto1.Enabled =
                txtVencPgto1.Enabled =
                txtValCarPgto1.Enabled = true;
            }
            else
            {
                txtNumPgto1.Enabled =
                txtTitulPgto1.Enabled =
                txtVencPgto1.Enabled =
                txtValCarPgto1.Enabled = false;
            }
        }

        //É executado quando o usuário escolhe um banco no DropDownList da Bandeira, o que faz habilitar as outras informações do registro, garantindo consistência
        protected void ddlBandePgto2_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBandePgto2.SelectedValue != "N")
            {
                txtNumPgto2.Enabled =
                txtTitulPgto2.Enabled =
                txtVencPgto2.Enabled =
                txtValCarPgto2.Enabled = true;
            }
            else
            {
                txtNumPgto2.Enabled =
                txtTitulPgto2.Enabled =
                txtVencPgto2.Enabled =
                txtValCarPgto2.Enabled = false;
            }
        }

        //É executado quando o usuário escolhe um banco no DropDownList da Bandeira, o que faz habilitar as outras informações do registro, garantindo consistência
        protected void ddlBandePgto3_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBandePgto3.SelectedValue != "N")
            {
                txtNumPgto3.Enabled =
                txtTitulPgto3.Enabled =
                txtVencPgto3.Enabled =
                txtValCarPgto3.Enabled = true;
            }
            else
            {
                txtNumPgto3.Enabled =
                txtTitulPgto3.Enabled =
                txtVencPgto3.Enabled =
                txtValCarPgto3.Enabled = false;
            }
        }

        #endregion

        #endregion

        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
            CarregaAluno();
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
            CarregaAluno();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
            CarregaAluno();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAluno();
        }

        protected void ddlAlunos_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int coAlu = int.Parse(ddlAlunos.SelectedValue);
            int coCur = int.Parse(ddlSerieCurso.SelectedValue);
            string ano = ddlAno.SelectedValue;

            //TB08_MATRCUR tb08 = TB08_MATRCUR.RetornaPeloAluCurAno(coAlu, ano, coCur);

            var res = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                       where tb08.CO_ALU == coAlu
                       && tb08.CO_ANO_MES_MAT == ano
                       && tb08.CO_CUR == coCur
                       select new
                       {
                           tb08.CO_ALU,
                           tb08.CO_ANO_MES_MAT,
                           tb08.CO_CUR,
                           tb08.CO_EMP,
                           tb08.CO_ALU_CAD,
                           tb08.QT_PAR_MOD_MAT,
                           tb08.VL_TOT_MODU_MAT,
                           tb08.NR_CONTR_MATRI,
                           tb08.TB44_MODULO.CO_MODU_CUR,
                           tb08.CO_TUR,
                           tb08.VL_DES_MOD_MAT,
                           tb08.VL_DES_BOL_MOD_MAT,
                       }).FirstOrDefault();

            if(res != null)
            {
                hdCoAluMatr.Value = res.CO_ALU.ToString();
                hdCoAnoMatr.Value = res.CO_ANO_MES_MAT;
                hdCoCurMatr.Value = res.CO_CUR.ToString();
                hdCoEmpMatr.Value = res.CO_EMP.ToString();
                hdCoMatrMat.Value = res.CO_ALU_CAD;

                txtQtPgto.Text = res.QT_PAR_MOD_MAT.ToString();
                txtNrContraPgto.Text = res.NR_CONTR_MATRI;
                txtValContrPgto.Text = res.VL_TOT_MODU_MAT.ToString();

                var resInfos = (from tbe220 in TBE220_MATRCUR_PAGTO.RetornaTodosRegistros()
                                where tbe220.CO_ALU_CAD == res.CO_ALU_CAD
                                select tbe220 ).FirstOrDefault();
                if (resInfos != null)
                {
                    //Recupera um objeto do aluno centro desta forma de pagamento, para acessar os demais atributos
                    var tb07 = TB07_ALUNO.RetornaTodosRegistros().Where(w => w.CO_ALU == resInfos.CO_ALU_M).FirstOrDefault();

                    txtNireAlunoPgto.Text = tb07.NU_NIRE.ToString();
                    txtNisAlunoPgto.Text = tb07.NU_NIS.ToString();
                    chkDinhePgto.Checked = txtValDinPgto.Enabled = (resInfos.FL_DINHE == "S" ? true : false);
                    txtValDinPgto.Text = (resInfos.VL_DINHE != null ? resInfos.VL_DINHE.ToString() : "");
                    chkOutrPgto.Checked = txtValOutPgto.Enabled = (resInfos.FL_OUTRO == "S" ? true : false);
                    txtValOutPgto.Text = (resInfos.VL_OUTRO != null ? resInfos.VL_OUTRO.ToString() : "");

                    //Calcula o valor do Contrato que aparecerá no campo correspondente
                    decimal vltotmod = (res.VL_TOT_MODU_MAT.HasValue ? res.VL_TOT_MODU_MAT.Value : decimal.Parse("0,00"));
                    decimal vldesModMat = (res.VL_DES_MOD_MAT.HasValue ? res.VL_DES_MOD_MAT.Value : decimal.Parse("0,00"));
                    decimal vldesBolMod = (res.VL_DES_BOL_MOD_MAT.HasValue ? res.VL_DES_BOL_MOD_MAT.Value : decimal.Parse("0,00"));
                    decimal vltottot = vltotmod - vldesModMat - vldesBolMod;
                    hdValTotContrato.Value = vltottot.ToString();
                    txtValContrPgto.Text = vltottot.ToString();

                    //Atribui os valores obtidos na matrícula aos campos na tela.
                    txtNrContraPgto.Text = res.NR_CONTR_MATRI;
                    txtValContrPgto.Text = res.VL_TOT_MODU_MAT.ToString();
                    ddlAno.SelectedValue = res.CO_ANO_MES_MAT;
                    CarregaModalidades();
                    ddlModalidade.SelectedValue = res.CO_MODU_CUR.ToString();
                    CarregaSerieCurso();
                    ddlSerieCurso.SelectedValue = res.CO_CUR.ToString();
                    CarregaTurma();
                    ddlTurma.SelectedValue = res.CO_TUR.ToString();
                    CarregaAluno();
                    ddlAlunos.SelectedValue = res.CO_ALU.ToString();
                    txtQtPgto.Text = res.QT_PAR_MOD_MAT.ToString();
                    carregaCartoes(resInfos);
                    CarregaCheques(resInfos);
                }
            }
        }
        #endregion
    }
}