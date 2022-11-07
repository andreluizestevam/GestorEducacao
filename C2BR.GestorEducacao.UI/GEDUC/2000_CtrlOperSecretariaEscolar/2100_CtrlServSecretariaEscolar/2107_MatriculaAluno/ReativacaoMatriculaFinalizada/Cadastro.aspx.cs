//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: MATRÍCULA NOVA PARA ALUNO
// OBJETIVO: REATIVAÇÃO DE MATRÍCULA FINALIZADA
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2107_MatriculaAluno.ReativacaoMatriculaFinalizada
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

            if (QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoInsercao) ||
                QueryStringAuxili.OperacaoCorrenteQueryString.Equals(QueryStrings.OperacaoExclusao))
                AuxiliPagina.RedirecionaParaPaginaMensagem("Favor, selecione um aluno.", Request.Url.AbsolutePath.ToLower().Replace("cadastro", "busca") + "&moduloNome=" + Request.QueryString["moduloNome"], C2BR.GestorEducacao.UI.RedirecionaMensagem.TipoMessagemRedirecionamento.Error);
        }

//====> Chamada do método de preenchimento do formulário da funcionalidade
        void CurrentPadraoCadastros_OnCarregaFormulario() { CarregaFormulario(); }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int modalidade = Convert.ToInt32(hdCodMod.Value);
            int serie = Convert.ToInt32(hdSerie.Value);
            int coAlu = Convert.ToInt32(hdAluno.Value);
            string anoSelecionado = txtAno.Text;

            TB08_MATRCUR tb08;
            
//--------> Primeiro: Se aluno estiver como rematriculado excluirá registros das tabelas TB08_MATRCUR/TB079_HIST_ALUNO/TB48_GRADE_ALUNO/TB80_MASTERMATR/TB132_FREQ_ALU       
            if (hdRematriculado.Value == "S")
            {
                string anoRematricula = (int.Parse(anoSelecionado) + 1).ToString();
                      
                var lstTb079 = (from lTb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                               where lTb079.CO_ANO_REF == anoRematricula && lTb079.CO_ALU == coAlu && lTb079.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                               select new { lTb079.CO_MAT });

                if (lstTb079 != null)
                {
                    TB079_HIST_ALUNO tb079;

                    foreach (var iTb079 in lstTb079)
                    {
                        tb079 = (from lTb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                                where lTb079.CO_ANO_REF == anoRematricula && lTb079.CO_ALU == coAlu && lTb079.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                && lTb079.CO_MAT == iTb079.CO_MAT
                                select lTb079).FirstOrDefault();

                        TB079_HIST_ALUNO.Delete(tb079, false);                       
                    }                   
                }

                var lstTb48 = (from lTb48 in TB48_GRADE_ALUNO.RetornaTodosRegistros()
                                where lTb48.CO_ANO_MES_MAT == anoRematricula && lTb48.CO_ALU == coAlu
                                select new { lTb48.CO_MAT });

                if (lstTb48 != null)
                {
                    TB48_GRADE_ALUNO tb48;

                    foreach (var iTb48 in lstTb48)
                    {
                        tb48 = (from lTb48 in TB48_GRADE_ALUNO.RetornaTodosRegistros()
                                where lTb48.CO_ANO_MES_MAT == anoRematricula && lTb48.CO_ALU == coAlu && lTb48.CO_MAT == iTb48.CO_MAT
                                select lTb48).FirstOrDefault();

                        TB48_GRADE_ALUNO.Delete(tb48, false);
                    }                   
                }

                TB80_MASTERMATR tb80 = TB80_MASTERMATR.RetornaPelaChavePrimaria(modalidade, coAlu, anoRematricula, null);

                tb80.CO_SITU_MTR = "C";
                tb80.DE_OBS_MATR = txtMotRea.Text;

                TB80_MASTERMATR.SaveOrUpdate(tb80, false);                

                tb08 = (from lTb08 in TB08_MATRCUR.RetornaTodosRegistros()
                        where lTb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP && lTb08.CO_ALU == coAlu && lTb08.CO_ANO_MES_MAT == anoRematricula
                        select lTb08).FirstOrDefault();

                TB08_MATRCUR.Delete(tb08, false);                
               
                decimal decimalAnoRemat = Convert.ToDecimal(anoRematricula);

                var lstTb132 = (from lTb132 in TB132_FREQ_ALU.RetornaTodosRegistros()
                            where lTb132.TB07_ALUNO.CO_ALU == coAlu && lTb132.CO_ANO_REFER_FREQ_ALUNO == decimalAnoRemat
                            select lTb132);

                if (lstTb132 != null)
                {
                    TB132_FREQ_ALU tb132;

                    foreach (var iTb132 in lstTb132)
                    {
                        tb132 = (from lTb132 in TB132_FREQ_ALU.RetornaTodosRegistros()
                                 where lTb132.CO_ANO_REFER_FREQ_ALUNO == decimalAnoRemat && lTb132.TB07_ALUNO.CO_ALU == coAlu && lTb132.CO_MAT == iTb132.CO_MAT
                                 select lTb132).FirstOrDefault();

                        TB132_FREQ_ALU.Delete(tb132, false);
                    }
                }
            }

//-------> Segundo: Carregar registro de matrícula da TB08_MATRCUR e Atualizar o campo FLA_REMATRICULADO para "N" e CO_SIT_MAT = "A"
            tb08 = TB08_MATRCUR.RetornaPelaChavePrimaria(coAlu, serie, anoSelecionado, "1");

            tb08.FLA_REMATRICULADO = null;
            tb08.CO_SIT_MAT = "A";

            TB08_MATRCUR.SaveOrUpdate(tb08, false);
            
            GestorEntities.CurrentContext.SaveChanges();

            AuxiliPagina.RedirecionaParaPaginaSucesso("Matrícula Reaberta com Sucesso", AuxiliPagina.RetornaURLRedirecionamentoPaginaBusca());        
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método de preenchimento do formulário da funcionalidade
        /// </summary>
        private void CarregaFormulario()
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
                                  tb06.TB129_CADTURMAS.NO_TURMA, tb44.CO_MODU_CUR, tb44.DE_MODU_CUR, tb08.FLA_REMATRICULADO
                              }).FirstOrDefault();

            if (alunoSelecionado != null)
            {
                txtAluno.Text = alunoSelecionado.NO_ALU;
                txtAno.Text = anoMatricula.ToString();
                txtModalidade.Text = alunoSelecionado.DE_MODU_CUR;
                txtSérie.Text = alunoSelecionado.NO_CUR;
                txtTurma.Text = alunoSelecionado.NO_TURMA;
                hdRematriculado.Value = alunoSelecionado.FLA_REMATRICULADO;
                hdAluno.Value = alunoSelecionado.CO_ALU.ToString();
                hdCodMod.Value = alunoSelecionado.CO_MODU_CUR.ToString();
                hdSerie.Value = alunoSelecionado.CO_CUR.ToString();
                hdTurma.Value = alunoSelecionado.CO_TUR.ToString();
            }            
        }
        #endregion                  

        protected void grdAlunos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label status = (Label)e.Row.FindControl("status");

                if (status.Text == "Aluno Aprovado" || status.Text == "Aluno Reprovado")
                    e.Row.ControlStyle.BackColor = System.Drawing.Color.FromArgb(251, 233, 183);
            }
        }                

        protected void rbReativa_SelectedIndexChanged(object sender, EventArgs e)
        {
            string anoMatricula = (QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.Ano) + 1).ToString();
            int coAlu = QueryStringAuxili.RetornaQueryStringComoIntPelaChave(QueryStrings.CoAlu);
            
            if (rbReativa.SelectedValue == "S")
            {
                if (hdRematriculado.Value == "S")
                {
                    string strNomeSerie = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                           join tb01 in TB01_CURSO.RetornaTodosRegistros() on tb08.CO_CUR equals tb01.CO_CUR
                                           where tb08.CO_ANO_MES_MAT == anoMatricula && tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP && tb08.CO_ALU == coAlu
                                           select new { tb01.NO_CUR }).FirstOrDefault().NO_CUR;

                    lblMsg.Text = "ATENÇÃO! Aluno possui rematrícula para o ano " + anoMatricula.ToString() + " na série "
                    + strNomeSerie + ". Ao clicar em Reativar Matrícula todos os dados para o ano "
                    + anoMatricula.ToString() + " serão excluídos.";

                    lblDeseja.Text = "Deseja Continuar a Operação?";

                    rbConfirma.Visible = divSuperior.Visible = true;
                }
                else
                {
                   //Habilitar o botao de salvar
                }
            }
            else
            {
                lblMsg.Text = "";
                rbConfirma.Visible = divSuperior.Visible = false;
                //  Desabilitar o botao de salvar
            }
        }

        protected void rbConfirma_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbConfirma.SelectedValue == "S")
            {
                // Habilitar o botao de salvar
            }
            else
            {
                //  Desabilitar o botao de salvar
                divSuperior.Visible = false;
                rbReativa.SelectedValue = "N";
            }
        }
    }
}
