//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA FINANCEIRA
// SUBMÓDULO: CONTROLE DE RECEITAS (CONTAS A RECEBER)
// OBJETIVO: CADASTRAMENTO GERAL DE TÍTULOS DE RECEITAS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//05/08/2014| Maxwell Almeida            | Criação da Funcionalidade com a possibilidade de alteração do responsável financeiro de boletos já gerados.

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
//using System;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Collections.Generic;
//using System.Linq;
//using C2BR.GestorEducacao.UI.Library.Auxiliares;
//using C2BR.GestorEducacao.UI.App_Masters;
//using C2BR.GestorEducacao.BusinessEntities.MSSQL;
//using Resources;

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Web;

using System.Data.SqlClient.SqlGen;
using System.Data.SqlClient;
using System.Data;


//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC._5000_CtrlFinanceira._5200_CtrlReceitas._5215_AlterarRespFinancBolet
{
    public partial class Cadastro : System.Web.UI.Page
    {
        //public PadraoCadastros CurrentPadraoCadastros { get { return ((PadraoCadastros)this.Master); } }
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }
        private Dictionary<string, string> tipoAgrupador = AuxiliBaseApoio.chave(tipoAgrupadorFinanceiro.ResourceManager);
        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //--------> Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CarregaUnidade();
                CarregaAlunos();
                CarregaAgrupador();
                CarregaModalidade();
                CarregaCurso();
                CarregaTurma();
                CarregaAlunos();
                CarregaAno();
            }
        }

        //====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            RegistroLog log = new RegistroLog();

            bool editado = false;

            ///Varre toda a grid de Pesquisa
            foreach (GridViewRow linha in grdResumo.Rows)
            {
                ///Verifica se linha está checada
                if (((CheckBox)linha.Cells[0].FindControl("chkSelect")).Checked)
                {
                    string novoResp = ((DropDownList)linha.Cells[7].FindControl("ddlRespNovo")).SelectedValue;
                    string NuDoc = linha.Cells[1].Text;
                    int NuPar = int.Parse(linha.Cells[2].Text);

                    if (string.IsNullOrWhiteSpace(novoResp))
                        continue;

                    int novoRespin = (!string.IsNullOrEmpty(novoResp) ? int.Parse(novoResp) : 0);

                  var ctx = GestorEntities.CurrentContext;

                    //Resgata um objeto do novo responsável selecionado
                    var ri = (from tb8 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                              where tb8.CO_RESP == novoRespin
                              select tb8).FirstOrDefault();

                    //Resgata o título o qual o usuário selecionou para alteração.
                    var tit = (from t4 in ctx.TB47_CTA_RECEB
                               where t4.CO_EMP == LoginAuxili.CO_EMP
                               && t4.NU_DOC == NuDoc
                               && t4.NU_PAR == NuPar
                               select t4).FirstOrDefault();

                    //Atribui ao campo o novo objeto com o valor do responsável.
                    tit.TB108_RESPONSAVEL = TB108_RESPONSAVEL.RetornaTodosRegistros().Where(w => w.CO_RESP == novoRespin).FirstOrDefault();
                    tit.TB108_RESPONSAVEL = ri;

                    TB47_CTA_RECEB.SaveChanges();
                    //Registra o Log de alteração.
                    log.RegistroLOG(tit, RegistroLog.ACAO_EDICAO);
                    editado = true;
                }
            }            

            if (!editado)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Selecione ao menos um título para alterar a data.");
                return;
            }
            else
                AuxiliPagina.RedirecionaParaPaginaSucesso("Responsável Alterado com sucesso!", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
        }
               // ====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        //void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        //{
        //    bool editado = false;
        //    ///Varre toda a grid de Pesquisa
        //    foreach (GridViewRow linha in grdResumo.Rows)
        //    {
        //        ///Verifica se linha está checada
        //        if (((CheckBox)linha.Cells[0].FindControl("chkSelect")).Checked)
        //        {
        //            string novoResp = ((DropDownList)linha.Cells[7].FindControl("ddlRespNovo")).SelectedValue;
        //            string NuDoc = linha.Cells[1].Text;
        //            int NuPar = int.Parse(linha.Cells[2].Text);

        //            if (string.IsNullOrWhiteSpace(novoResp))
        //                continue;

        //            int novoRespin = (!string.IsNullOrEmpty(novoResp) ? int.Parse(novoResp) : 0);

        //            var ctx = GestorEntities.CurrentContext;

        //            ///Faz a adicão do item
        //            //var tit = TB47_CTA_RECEB.RetornaPrimeiroRegistroPeloCoEmpNuDocNuPar(LoginAuxili.CO_EMP, NuDoc, NuPar);
        //            //var tit = ctx.TB47_CTA_RECEB.
        //            //tit.TB108_RESPONSAVEL = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(novoRespin);

        //            var ri = (from tb8 in TB108_RESPONSAVEL.RetornaTodosRegistros()
        //                      where tb8.CO_RESP == novoRespin
        //                      select tb8).FirstOrDefault();

        //            var tit = (from t4 in ctx.TB47_CTA_RECEB
        //                       where t4.CO_EMP == LoginAuxili.CO_EMP
        //                       && t4.NU_DOC == NuDoc
        //                       && t4.NU_PAR == NuPar
        //                       select t4).FirstOrDefault();

        //            tit.TB108_RESPONSAVEL = TB108_RESPONSAVEL.RetornaTodosRegistros().Where(w => w.CO_RESP == novoRespin).FirstOrDefault();
        //            tit.TB108_RESPONSAVEL = ri;

        //            TB47_CTA_RECEB.SaveChanges();

        //            //if (GestorEntities.SaveOrUpdate(tit) < 0)
        //            //{
        //            //    AuxiliPagina.EnvioMensagemErro(this, "Erro ao salvar um dos títulos.");
        //            //    return;
        //            //}

        //            //IDbConnection connection;
        //            //string s = connection.ConnectionString;
        //            //SqlConnection con = new SqlConnection();

        //            //SqlCommand sql = new SqlCommand("update TB47_CTA_RECEB set CO_RESP = " +
        //            //              novoRespin +
        //            //              "where CO_EMP = " +
        //            //              LoginAuxili.CO_EMP +
        //            //              "AND NU_DOC = " +
        //            //              NuDoc +
        //            //              "AND NU_PAR = " +
        //            //              NuPar);
                    
        //            //con.ConnectionString = s;
        //            //SqlCommand cmd = new SqlCommand(sql, con);
        //            //cmd.Connection.Open();
        //            //sql.ExecuteNonQuery();

        //            editado = true;
        //        }
        //    }

        //    if (!editado)
        //    {
        //        AuxiliPagina.EnvioMensagemErro(this, "Selecione ao menos um título para alterar a data.");
        //        return;
        //    }

        //    AuxiliPagina.RedirecionaParaPaginaSucesso("Datas Alteradas com Sucesso!", "/GEDUC/5000_CtrlFinanceira/5200_CtrlReceitas/5206_AlterarDataTituReceita/Cadastro.aspx?moduloId=981&moduloNome=Alteração+de+Datas+de+Vencimento+de+Títulos+de+Receitas");
        //}
        #endregion

        #region Carregamento

        /// <summary>
        /// Carrega os Anos que tenham títulos 
        /// </summary>
        private void CarregaAno()
        {
            var res = (from tb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                       where tb47.IC_SIT_DOC == "A"
                       &&  (!string.IsNullOrEmpty(tb47.CO_ANO_MES_MAT))
                       select new { tb47.CO_ANO_MES_MAT }).ToList().OrderByDescending(w => w.CO_ANO_MES_MAT).Distinct();

            ddlAno.DataTextField = "CO_ANO_MES_MAT";
            ddlAno.DataValueField = "CO_ANO_MES_MAT";
            ddlAno.DataSource = res;
            ddlAno.DataBind();
        }

        /// <summary>
        /// Método que carrega o DropDown Nome da Fonte com os Alunos
        /// </summary>
        private void CarregaModalidade()
        {
            AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, false);
        }

        /// <summary>
        /// Carrega os cursos relacionados à modalidade e empresa selecionada
        /// </summary>
        private void CarregaCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int emp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            AuxiliCarregamentos.carregaSeriesCursos(ddlSerieCurso, modalidade, emp, false);
        }

        /// <summary>
        /// Carrega as turmas relacionadas à modalidade/série/Unidade selecionadas anteriormente.
        /// </summary>
        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int emp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int curso = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaTurmas(ddlTurma, emp, modalidade, curso, false);
        }

        /// <summary>
        /// Carrega os Alunos de acordo com as informações selecionadas em 
        /// </summary>
        private void CarregaAlunos()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int emp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int curso = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string ano = ddlAno.SelectedValue;
            AuxiliCarregamentos.CarregaAlunoMatriculado(ddlAluno, emp, modalidade, curso, turma, ano, false);
        }

        /// <summary>
        /// Método que carrega o DropDown de Unidades de Contrato
        /// </summary>
        private void CarregaAgrupador()
        {
            ddlAgrupador.Items.Clear();
            ddlAgrupador.Items.AddRange(AuxiliBaseApoio.AgrupadoresDDL(tipoAgrupador[tipoAgrupadorFinanceiro.R], todos: true));

        }

        /// <summary>
        /// Carrega os DropDownList's de responsável na Grid de Títulos recebendo o DropDown no qual será carregado e o código de qual responsável já virá selecionado.
        /// </summary>
        /// <param name="ddlresp"></param>
        /// <param name="co_resp"></param>
        private void CarregaResponsavel(DropDownList ddlresp, string co_resp)
        {
            var res = (from tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros()
                       select new { tb108.CO_RESP, tb108.NO_RESP }).ToList();

            ddlresp.DataValueField = "CO_RESP";
            ddlresp.DataTextField = "NO_RESP";
            ddlresp.DataSource = res;
            ddlresp.DataBind();

            if (co_resp != "")
                ddlresp.SelectedValue = co_resp;
        }

        /// <summary>
        /// Método que carrega o DropDown de Unidades de Contrato
        /// </summary>
        private void CarregaUnidade()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, false);
        }

        #endregion

        #region Funções de Campo

        protected void ddlUnidade_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaModalidade();
            CarregaCurso();
            CarregaTurma();
            CarregaAlunos();
        }

        protected void ddlModalidade_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaCurso();
        }

        protected void ddlSerieCurso_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
        }

        protected void ddlTurma_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaAlunos();
        }

        /// <summary>
        /// Preenche a grid dos Títulos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPesquisar_Click(object sender, EventArgs e)
        {            
            if (ddlAluno.SelectedValue == "" || ddlAluno.SelectedValue == "0")
            {
                AuxiliPagina.EnvioMensagemErro(this, "Selecione o aluno.");
                return;
            }
            int agrupador = int.Parse(ddlAgrupador.SelectedValue);
            int und = int.Parse(ddlUnidade.SelectedValue);
            int coAlu = int.Parse(ddlAluno.SelectedValue);

            DateTime dtInicio = DateTime.ParseExact(txtDataPeriodoIni.Text, "dd/MM/yyyy", null);
            DateTime dtFim = DateTime.ParseExact(txtDataPeriodoFim.Text, "dd/MM/yyyy", null);

            var lst = from tb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                      where tb47.DT_VEN_DOC >= dtInicio && tb47.DT_VEN_DOC <= dtFim
                      && (tb47.IC_SIT_DOC == "A" || tb47.IC_SIT_DOC == "R")
                      && tb47.CO_ALU == coAlu
                      && (agrupador != -1 ? tb47.CO_AGRUP_RECDESP == agrupador : 0 == 0)
                      && (und != 0 ? tb47.CO_EMP == und : 0 == 0)
                      select new Dummy
                      {
                          Checked = false,
                          NU_DOC = tb47.NU_DOC,
                          NU_PAR = tb47.NU_PAR,
                          VR_PAR_DOC = tb47.VR_PAR_DOC,
                          dt_venc_doc_receb = tb47.DT_VEN_DOC,
                          DE_HISTORICO = tb47.TB39_HISTORICO.DE_HISTORICO,
                          CO_RESP = tb47.TB108_RESPONSAVEL.CO_RESP,
                      };

            this.grdResumo.DataSource = lst.ToList();
            this.grdResumo.DataBind();

            //Carrega os DropDownLists de responsável de acordo com o Código do Responsável na tabela de Títulos
            foreach (GridViewRow li in grdResumo.Rows)
            {
                CarregaResponsavel(((DropDownList)li.Cells[7].FindControl("ddlRespNovo")), (((HiddenField)li.Cells[6].FindControl("hdcoResp")).Value));
            }

            divGrid.Visible = liResumo.Visible = true;
        }

        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            HabilitaCamposGride();
        }

        /// <summary>
        /// Método que habilita campos da gride
        /// </summary>
        protected void HabilitaCamposGride()
        {
            foreach (GridViewRow linha in grdResumo.Rows)
            {
                //------------> Faz a verificação dos itens marcados na Grid de Títulos
                if (((CheckBox)linha.Cells[0].FindControl("chkSelect")).Checked)
                {
                    ((DropDownList)linha.Cells[7].FindControl("ddlRespNovo")).Enabled = true;

                    if (((DropDownList)linha.Cells[7].FindControl("ddlRespNovo")).SelectedValue == "")
                        ((DropDownList)linha.Cells[7].FindControl("ddlRespNovo")).SelectedValue = ((HiddenField)linha.Cells[0].FindControl("hdcoResp")).Value;
                }
                else
                {
                    ((DropDownList)linha.Cells[7].FindControl("ddlRespNovo")).Enabled = false;
                    ((DropDownList)linha.Cells[7].FindControl("ddlRespNovo")).SelectedValue = ((HiddenField)linha.Cells[0].FindControl("hdcoResp")).Value;
                }
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            bool editado = false;

            ///Varre toda a grid de Pesquisa
            foreach (GridViewRow linha in grdResumo.Rows)
            {
                ///Verifica se linha está checada
                if (((CheckBox)linha.Cells[0].FindControl("chkSelect")).Checked)
                {
                    string novoResp = ((DropDownList)linha.Cells[7].FindControl("ddlRespNovo")).SelectedValue;
                    string NuDoc = linha.Cells[1].Text;
                    int NuPar = int.Parse(linha.Cells[2].Text);

                    if (string.IsNullOrWhiteSpace(novoResp))
                        continue;

                    int novoRespin = (!string.IsNullOrEmpty(novoResp) ? int.Parse(novoResp) : 0);

                    ///Faz a adicão do item
                    var tit = TB47_CTA_RECEB.RetornaPrimeiroRegistroPeloCoEmpNuDocNuPar(LoginAuxili.CO_EMP, NuDoc, NuPar);
                    tit.TB108_RESPONSAVEL = TB108_RESPONSAVEL.RetornaPelaChavePrimaria(novoRespin);

                    if (GestorEntities.SaveOrUpdate(tit) < 0)
                    {
                        AuxiliPagina.EnvioMensagemErro(this, "Erro ao salvar um dos títulos.");
                        return;
                    }

                    editado = true;
                }
            }

            if (!editado)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Selecione ao menos um título para alterar a data.");
                return;
            }

            AuxiliPagina.RedirecionaParaPaginaSucesso("Responsável Alterado com Sucesso!", "/GEDUC/5000_CtrlFinanceira/5200_CtrlReceitas/5206_AlterarDataTituReceita/Cadastro.aspx?moduloId=981&moduloNome=Alteração+de+Datas+de+Vencimento+de+Títulos+de+Receitas");
        }

        #endregion
    }

    public class Dummy
    {
        public string NU_DOC { get; set; }
        public int NU_PAR { get; set; }
        public decimal VR_PAR_DOC { get; set; }
        public DateTime dt_venc_doc_receb { get; set; }
        public string DT_VEN_DOC
        {
            get
            {
                return this.dt_venc_doc_receb.ToString("dd/MM/yy");
            }
        }
        public string DE_HISTORICO { get; set; }
        public int CO_RESP { get; set; }
        public string RESPATU
        {
            get
            {
                string noResp = this.CO_RESP != 0 ? TB108_RESPONSAVEL.RetornaPelaChavePrimaria(this.CO_RESP).NO_RESP : "";
                return noResp;
            }
        }
        public bool Checked { get; set; }
    }
}