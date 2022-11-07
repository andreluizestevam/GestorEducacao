//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE DE TRANSFERÊNCIA ESCOLAR
// OBJETIVO: REGISTRO DE TRANSFERÊNCIA DE ALUNOS PARA ESCOLAS QUE NÃO PERTENCEM A REDE DE ENSINO
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+------------------------------------
// 06/08/2013| André Nobre Vinagre        | Desenvolvida solução para salvar registro na TB_TRANSF_EXTERNA
//           |                            |

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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3300_CtrlTransferenciaEscolar.F3303_RegistroTransfAlunoEscolaExterna
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
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaUnidade();
                CarregaModalidade();
                CarregaSerieCurso();
                CarregaTurma();
                CarregaAluno();
                CarregaUF();
                ddlCO_UF_DESTI.SelectedValue = LoginAuxili.CO_UF_INSTITUICAO;
                txtDT_EFETI_TRANSF.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                txtANO_REF.Text = DateTime.Now.Year.ToString();
            }
        }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            decimal decimalRetorno = 0;

            int coEmp = ddlCO_EMP.SelectedValue != "" ? int.Parse(ddlCO_EMP.SelectedValue) : 0;
            int coAlu = ddlCO_ALUNO.SelectedValue != "" ? int.Parse(ddlCO_ALUNO.SelectedValue) : 0;
            var modalidade = int.Parse(ddlCO_MODU_CUR.SelectedValue);
            var serie = int.Parse(ddlCO_CUR.SelectedValue);
            var turma = int.Parse(ddlCO_TUR.SelectedValue);
            var anoMesMat = txtANO_REF.Text; 
            //if (!decimal.TryParse(txtNU_INEP_DESTI.Text, out decimalRetorno))
            //{
            //    AuxiliPagina.EnvioMensagemErro(this, "INEP inválido");
            //    return;
            //}

            var tb08 = (from lTb08 in TB08_MATRCUR.RetornaTodosRegistros()
                        where lTb08.CO_ALU == coAlu && lTb08.CO_ANO_MES_MAT == anoMesMat
                           && lTb08.CO_CUR == serie && lTb08.CO_TUR == turma && lTb08.TB44_MODULO.CO_MODU_CUR == modalidade
                          select lTb08).FirstOrDefault();

            tb08.TB07_ALUNOReference.Load();

            TB07_ALUNO tb07 = TB07_ALUNO.RetornaPelaChavePrimaria(tb08.CO_ALU, tb08.CO_EMP);

            decimal nireAlu = (decimal)tb08.TB07_ALUNO.NU_NIRE;

            //Pega o NIRE do aluno e não o NIS, porque é um campo obrigatório e único na tabela
            var ocoTransf = (from te in TB_TRANSF_EXTERNA.RetornaTodosRegistros()
                             where te.CO_UNIDA_ATUAL == coEmp && te.CO_NIS_ALUNO == nireAlu
                             select te).FirstOrDefault();

            if (ocoTransf != null)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Aluno já possui registro de transferência externa.");
                return;
            }

            
            //Pega o NIRE do aluno e não o NIS, porque é um campo obrigatório e único na tabela
            TB_TRANSF_EXTERNA tbTransfExterna = new TB_TRANSF_EXTERNA();

            tbTransfExterna.CO_UNIDA_ATUAL = coEmp;
            tbTransfExterna.CO_NIS_ALUNO = nireAlu;
            tbTransfExterna.NU_INEP_DESTI = decimalRetorno;
            tbTransfExterna.NU_ANO_REF = Convert.ToDecimal(txtANO_REF.Text);
            tbTransfExterna.DT_REGIST_TRANSF = Convert.ToDateTime(txtDT_REGIST_TRANSF.Text);
            tbTransfExterna.DT_EFETI_TRANSF = Convert.ToDateTime(txtDT_EFETI_TRANSF.Text);
            tbTransfExterna.CO_CURSO_ATUAL = tb08.CO_CUR;
            tbTransfExterna.CO_TURMA_ATUAL = tb08.CO_TUR.Value;
            tbTransfExterna.CO_MATRI_ATUAL = tb08.CO_ALU_CAD;
            tbTransfExterna.NM_UNIDA_DESTI = txtNM_UNIDA_DESTI.Text != "" ? txtNM_UNIDA_DESTI.Text : ".";
            tbTransfExterna.DE_ENDER_DESTI = txtDE_ENDER_DESTI.Text != "" ? txtDE_ENDER_DESTI.Text : ".";
            tbTransfExterna.DE_COMPL_DESTI = txtDE_COMPL_DESTI.Text != "" ? txtDE_COMPL_DESTI.Text : ".";
            tbTransfExterna.NO_BAIRR_DESTI = txtNO_BAIRR_DESTI.Text != "" ? txtNO_BAIRR_DESTI.Text : ".";
            tbTransfExterna.NO_CIDAD_DESTI = txtNO_CIDAD_DESTI.Text != "" ? txtNO_CIDAD_DESTI.Text : ".";
            tbTransfExterna.CO_UF_DESTI = ddlCO_UF_DESTI.SelectedValue;
            tbTransfExterna.CO_CEP_DESTI = txtCO_CEP_DESTI.Text != "" ? txtCO_CEP_DESTI.Text : ".";
            tbTransfExterna.DE_MOTIVO_TRANSF = txtDE_MOTIVO_TRANSF.Text;

//--------> Altera a data e situação da matrícula
            tb08.DT_SIT_MAT = Convert.ToDateTime(txtDT_EFETI_TRANSF.Text);
            tb08.CO_SIT_MAT = "X";

            TB08_MATRCUR.SaveOrUpdate(tb08,true);

            tb07.CO_SITU_ALU = "I";

            TB07_ALUNO.SaveOrUpdate(tb07,true);

            TB_TRANSF_EXTERNA.SaveOrUpdate(tbTransfExterna, true);

            AuxiliPagina.RedirecionaParaPaginaSucesso("Registro Salvo com Sucesso", Request.Url.AbsoluteUri);
        }

        /// <summary>
        /// Método de retorno da entidade informada
        /// </summary>
        /// <returns>Entidade TB_TRANSF_EXTERNA</returns>
        private TB_TRANSF_EXTERNA RetornaEntidade(int coEmpAtual, decimal nireAluno)
        {
            var tbTransExter = TB_TRANSF_EXTERNA.RetornaPelaChavePrimaria(coEmpAtual, nireAluno);

            return (tbTransExter == null ? new TB_TRANSF_EXTERNA() : tbTransExter);
        }
        #endregion

        #region Carregamento

        /// <summary>
        /// Método que carrega o dropdown de Unidades Escolares
        /// </summary>
        private void CarregaUnidade()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlCO_EMP, LoginAuxili.ORG_CODIGO_ORGAO, false);
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidade()
        {
            AuxiliCarregamentos.carregaModalidades(ddlCO_MODU_CUR, LoginAuxili.ORG_CODIGO_ORGAO, false);
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {
            int coEmp = ddlCO_EMP.SelectedValue != "" ? int.Parse(ddlCO_EMP.SelectedValue) : 0;
            int modalidade = ddlCO_MODU_CUR.SelectedValue != "" ? int.Parse(ddlCO_MODU_CUR.SelectedValue) : 0;
            AuxiliCarregamentos.carregaSeriesCursos(ddlCO_CUR, modalidade, coEmp, false);
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        private void CarregaTurma()
        {
            int modalidade = ddlCO_MODU_CUR.SelectedValue != "" ? int.Parse(ddlCO_MODU_CUR.SelectedValue) : 0;
            int coEmp = ddlCO_EMP.SelectedValue != "" ? int.Parse(ddlCO_EMP.SelectedValue) : 0;
            int serie = ddlCO_CUR.SelectedValue != "" ? int.Parse(ddlCO_CUR.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaTurmas(ddlCO_TUR, coEmp, modalidade, serie, false);
        }

        /// <summary>
        /// Método que carrega o dropdown de Alunos
        /// </summary>
        private void CarregaAluno()
        {
            int coEmp = ddlCO_EMP.SelectedValue != "" ? int.Parse(ddlCO_EMP.SelectedValue) : 0;
            int modalidade = ddlCO_MODU_CUR.SelectedValue != "" ? int.Parse(ddlCO_MODU_CUR.SelectedValue) : 0;
            int serie = ddlCO_CUR.SelectedValue != "" ? int.Parse(ddlCO_CUR.SelectedValue) : 0;
            int turma = ddlCO_TUR.SelectedValue != "" ? int.Parse(ddlCO_TUR.SelectedValue) : 0;
            string ano = txtANO_REF.Text;

            if (turma != 0)
            {
                ddlCO_ALUNO.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                          where (tb08.CO_SIT_MAT.Equals("A") || tb08.CO_SIT_MAT.Equals("F"))
                                          && tb08.TB25_EMPRESA.CO_EMP == coEmp && tb08.TB44_MODULO.CO_MODU_CUR == modalidade 
                                          && tb08.CO_CUR == serie && tb08.CO_TUR == turma
                                          && tb08.CO_ANO_MES_MAT == ano
                                          select new { tb08.CO_ALU, tb08.TB07_ALUNO.NO_ALU }).Distinct().OrderBy( m => m.NO_ALU );

                ddlCO_ALUNO.DataTextField = "NO_ALU";
                ddlCO_ALUNO.DataValueField = "CO_ALU";
                ddlCO_ALUNO.DataBind();
            }
            else
                ddlCO_ALUNO.Items.Clear();

            ddlCO_ALUNO.Items.Insert(0, new ListItem("Selecione", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de UFs
        /// </summary>
        private void CarregaUF()
        {
            ddlCO_UF_DESTI.DataSource = TB74_UF.RetornaTodosRegistros();

            ddlCO_UF_DESTI.DataTextField = "CODUF";
            ddlCO_UF_DESTI.DataValueField = "CODUF";
            ddlCO_UF_DESTI.DataBind();
        }
        #endregion

        protected void ddlCO_EMP_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaModalidade();
            CarregaSerieCurso();
            CarregaTurma();
            CarregaAluno();
        }

        protected void ddlCO_MODU_CUR_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
        }

        protected void ddlCO_CUR_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
        }
        
        protected void ddlCO_TUR_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAluno();
        }
    }
}