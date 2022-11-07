//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: ENCERRAMENTO ATIVIDADES DO PERÍODO LETIVO
// OBJETIVO: PROCESSO DE FINALIZAÇÃO DE MATRÍCULA (ANO LETIVO)
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
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2107_MatriculaAluno.ExclusaoMatricula
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaAnos();
                CarregaModalidades();
                CarregaSerieCurso();
                CarregaTurma();
                CarregaAluno();
            }     
        }

        #endregion

        #region Carregamento DropDown

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

            if (serie != 0)
            {
                ddlAlunos.DataSource = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                        where tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP && tb08.CO_CUR == serie && tb08.CO_ANO_MES_MAT == coAnoMesMat
                                        && tb08.TB44_MODULO.CO_MODU_CUR == modalidade && tb08.CO_TUR == turma
                                        select new { tb08.TB07_ALUNO.CO_ALU, tb08.TB07_ALUNO.NO_ALU }).Distinct().OrderBy(m => m.NO_ALU);

                ddlAlunos.DataTextField = "NO_ALU";
                ddlAlunos.DataValueField = "CO_ALU";
                ddlAlunos.DataBind();

                ddlAlunos.Items.Insert(0, new ListItem("Selecione", ""));
            }
            else
                ddlAlunos.Items.Clear();

            ddlAlunos.Items.Insert(0, new ListItem("Selecione", ""));

        }
        #endregion

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int modalidade = int.Parse(ddlModalidade.SelectedValue);
            int serie = int.Parse(ddlSerieCurso.SelectedValue);
            int turma = int.Parse(ddlTurma.SelectedValue);
            int aluno = int.Parse(ddlAlunos.SelectedValue);


//--------> Exclui os registros da tabela de grade de aluno
            List<TB48_GRADE_ALUNO> lstTb48 = (from lTb47 in TB48_GRADE_ALUNO.RetornaTodosRegistros()
                                               where lTb47.CO_EMP == LoginAuxili.CO_EMP && lTb47.CO_ALU == aluno &&
                                               lTb47.CO_ANO_MES_MAT == ddlAno.SelectedValue && lTb47.CO_CUR == serie &&
                                               lTb47.CO_MODU_CUR == modalidade && lTb47.CO_TUR == turma
                                               select lTb47).ToList();

            foreach (TB48_GRADE_ALUNO tb48 in lstTb48)
            {
                TB48_GRADE_ALUNO.Delete(tb48, true);
            }

//--------> Exclui os registros da tabela de histórico
            var listatb079 = (from lTb47 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                                            where lTb47.CO_EMP == LoginAuxili.CO_EMP && lTb47.CO_ALU == aluno &&
                                            lTb47.CO_ANO_REF == ddlAno.SelectedValue && lTb47.CO_CUR == serie &&
                                            lTb47.CO_MODU_CUR == modalidade && lTb47.CO_TUR == turma
                                            select lTb47).DefaultIfEmpty();
            if(listatb079 != null)
            {
                List<TB079_HIST_ALUNO> lstTb079 = listatb079.ToList();

                foreach (TB079_HIST_ALUNO tb079 in lstTb079)
                {
                    TB079_HIST_ALUNO.Delete(tb079, true);
                }
            }
//--------> Exclui os registros da tabela de títulos
            List<TB47_CTA_RECEB> lstTb47 = (from lTb47 in TB47_CTA_RECEB.RetornaTodosRegistros()
                                            where lTb47.CO_EMP == LoginAuxili.CO_EMP && lTb47.CO_ALU == aluno &&
                                            lTb47.CO_ANO_MES_MAT == ddlAno.SelectedValue && lTb47.CO_CUR == serie &&
                                            lTb47.CO_MODU_CUR == modalidade && lTb47.CO_TUR == turma
                                            select lTb47).ToList();

            foreach (TB47_CTA_RECEB tb47 in lstTb47)
            {
                TB47_CTA_RECEB.Delete(tb47, true);
            }

//--------> Pesquisa o registro da tabela de matricula
            TB08_MATRCUR tb08 = (from lTb47 in TB08_MATRCUR.RetornaTodosRegistros()
                                    where lTb47.CO_EMP == LoginAuxili.CO_EMP && lTb47.CO_ALU == aluno &&
                                    lTb47.CO_ANO_MES_MAT == ddlAno.SelectedValue && lTb47.CO_CUR == serie &&
                                    lTb47.TB44_MODULO.CO_MODU_CUR == modalidade && lTb47.CO_TUR == turma
                                    select lTb47).FirstOrDefault();

            if (tb08 != null)
            {
                //--------> Pesquisa o registro da tabela de Forma de Pagamento da Matrícula
                TBE220_MATRCUR_PAGTO tbe220 = (from litbe220 in TBE220_MATRCUR_PAGTO.RetornaTodosRegistros()
                                               where litbe220.CO_ALU_CAD == tb08.CO_ALU_CAD
                                               select litbe220).FirstOrDefault();

                if (tbe220 != null)
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
                }
            }

//--------> Exclui o registro da tabela de matricula
            if (tb08 != null)
                TB08_MATRCUR.Delete(tb08, true);
            

//--------> Exclui o registro da tabela de matricula master
            TB80_MASTERMATR tb80 = (from lTb47 in TB80_MASTERMATR.RetornaTodosRegistros()
                                 where lTb47.CO_EMP == LoginAuxili.CO_EMP && lTb47.CO_ALU == aluno &&
                                 lTb47.CO_ANO_MES_MAT == ddlAno.SelectedValue && lTb47.CO_CUR == serie &&
                                 lTb47.TB44_MODULO.CO_MODU_CUR == modalidade
                                 select lTb47).FirstOrDefault();
            if (tb80 != null)
                TB80_MASTERMATR.Delete(tb80, true);

//--------> Altera a modalidade, série e turma do aluno para NULL
            TB07_ALUNO tb07 = TB07_ALUNO.RetornaPeloCoAlu(aluno);

            tb07.CO_MODU_CUR = null;
            tb07.CO_CUR = null;
            tb07.CO_TUR = null;

            TB07_ALUNO.SaveOrUpdate(tb07, true);

            AuxiliPagina.RedirecionaParaPaginaSucesso("Exclusão Efetuada com Sucesso", Request.Url.AbsoluteUri);
        }

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
    }
}