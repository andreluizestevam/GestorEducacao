//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: ENCERRAMENTO ATIVIDADES DO PERÍODO LETIVO
// OBJETIVO: PROCESSA FREQUÊNCIA FINAL DE ALUNO POR TURMA
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
using C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3900_CtrlEncerramentoLetivo.F3901_ProcessaFreqFinalAlunoTurma;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Web.Services;
using System.Web;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.GEDUC.F3000_CtrlOperacionalPedagogico.F3900_CtrlEncerramentoLetivo.F3901_ProcessaFreqFinalAlunoTurma
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public int qtTotal;
        public int qtCalculado;

        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }
        Dictionary<string, string> statusMatri = AuxiliBaseApoio.chave(statusMatriculaAluno.ResourceManager);
        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

//--------> Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            
            CarregaAnos();
            CarregaModalidades();
            divGrid.Visible = false;

            this.qtCalculado = 2;
            this.qtTotal = 6;
        }

//====> Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
      
        }
        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            ddlAno.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                 select new { tb43.CO_ANO_GRADE }).Distinct().OrderByDescending( g => g.CO_ANO_GRADE );

            ddlAno.DataTextField = "CO_ANO_GRADE";
            ddlAno.DataValueField = "CO_ANO_GRADE";
            ddlAno.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            ddlModalidade.DataSource = TB44_MODULO.RetornaTodosRegistros().Where( m => m.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO);

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
                ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                            where tb01.CO_MODU_CUR == modalidade
                                            join tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on tb01.CO_CUR equals tb43.CO_CUR
                                            where tb43.TB44_MODULO.CO_MODU_CUR == tb01.CO_MODU_CUR && tb43.CO_ANO_GRADE == anoGrade
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
                                       select new { tb06.TB129_CADTURMAS.CO_SIGLA_TURMA, tb06.CO_TUR }).OrderBy( t => t.CO_SIGLA_TURMA );

                ddlTurma.DataTextField = "CO_SIGLA_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataBind();
            }
            else
                ddlTurma.Items.Clear();

            ddlTurma.Items.Insert(0, new ListItem("Selecione", ""));
        }
        #endregion       

        #region Eventos de componentes

        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
            grdBusca.DataBind();
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
            grdBusca.DataBind();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
            grdBusca.DataBind();
        }
      
        protected void grdBusca_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {               
                int coAlu = Convert.ToInt32(grdBusca.DataKeys[e.Row.RowIndex].Values[0]);

                List<RelAlunoFrequencia> lstRelAlu = (List<RelAlunoFrequencia>) ViewState["Geral"];
                if (lstRelAlu != null)
                {
                    var varRelAluFreq = (from lRelAlu in lstRelAlu
                                         where lRelAlu.StatusMateria == "Reprovado"
                                         select new { lRelAlu.CoAlu });

                    foreach (var item in varRelAluFreq)
                    {
                        if (item.CoAlu == coAlu)
                            e.Row.ControlStyle.BackColor = System.Drawing.Color.FromArgb(251, 233, 183);
                    }
                }
            }
        }

        [WebMethod(EnableSession = false)]
        private static string RetornaQuantidade()
        {
            //return HttpContext.Current.Session["qtCalcAFF"].ToString();
            return "teste";
        }

        /// <summary>
        /// Valida se aprovado ou reprovado por falta com base na tabela TB079_HIST_ALUNO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgAdd_Click(object sender, EventArgs e)
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            var ctx = GestorEntities.CurrentContext;
            List<RelAlunoFrequencia> lstRelAlunoFrequencia = new List<RelAlunoFrequencia>();
            RelAlunoFrequencia relAlunoFrequencia;
            ///Faz a verificação na tb01 ql o % de falta e ql o tipo de verificação
            var varSerie = (from tb01 in TB01_CURSO.RetornaTodosRegistros()
                            where tb01.CO_CUR == serie
                            select new { tb01.CO_CUR, tb01.CO_PARAM_FREQ_TIPO, tb01.PE_FALT_CUR }).FirstOrDefault();

            decimal dcmPercFalta, dcmAnoRefer;
            dcmPercFalta = varSerie.PE_FALT_CUR;
            dcmAnoRefer = decimal.Parse(ddlAno.SelectedValue);
            int intAnoRef = int.Parse(ddlAno.SelectedValue);
            string situMat = statusMatri[statusMatriculaAluno.A];
            ///Pega todas as matérias da turma
            var materias = (from tb43 in TB43_GRD_CURSO.RetornaTodosRegistros()
                            where tb43.CO_MAT != null
                            join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb43.CO_MAT equals tb02.CO_MAT
                            join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA into resultado1
                                from tb107 in resultado1.DefaultIfEmpty()
                            where tb43.CO_ANO_GRADE == ddlAno.SelectedValue
                            && tb43.CO_SITU_MATE_GRC == situMat
                            && tb43.CO_EMP == LoginAuxili.CO_EMP
                            && tb43.CO_CUR == serie
                            && tb43.TB44_MODULO.CO_MODU_CUR == modalidade
                            && tb43.ID_MATER_AGRUP == null
                            && tb107.CO_CLASS_BOLETIM != 4
                            select new { tb43.CO_MAT,
                                tb107.NO_MATERIA,
                                tb43.QTDE_AULA_SEM, 
                                tb43.QT_AULAS_BIM1, 
                                tb43.QT_AULAS_BIM2, 
                                tb43.QT_AULAS_BIM3,
                                tb43.QT_AULAS_BIM4
                            });

            if (materias != null && materias.Where(f => f.QTDE_AULA_SEM == null).Count() > 0)
            {
                string materiasSem = "";
                var materiasSemLista = materias.Where(f => f.QTDE_AULA_SEM == null);
                foreach (var linha in materiasSemLista)
                {
                    if (materiasSem != "")
                        materiasSem += ", ";
                    materiasSem += linha.NO_MATERIA;
                }
                AuxiliPagina.EnvioMensagemErro(this, "Existe a(s) sequinte(s) materia(s) sem quantidade de aulas especificada: " + materiasSem + ".");
                return;
            }

            ///Pega todos os alunos da turma
            var alunos = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                          join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tb08.CO_ALU equals tb07.CO_ALU into resultado
                          from tb07 in resultado.DefaultIfEmpty()
                          where tb08.CO_EMP == LoginAuxili.CO_EMP
                          && tb08.CO_CUR == serie
                          && tb08.CO_ANO_MES_MAT == ddlAno.SelectedValue
                          && tb08.CO_TUR == turma
                          && tb08.TB44_MODULO.CO_MODU_CUR == modalidade
                          && tb08.CO_SIT_MAT == situMat
                          select new { tb07.CO_ALU, tb07.NO_ALU, tb07.NU_NIRE });

            qtTotal= alunos.Count();

            ///Realiza um loop na lista de alunos para validar aluno por aluno
            foreach (var aluno in alunos)
            {
                relAlunoFrequencia = new RelAlunoFrequencia();
                relAlunoFrequencia.NoAlu = aluno.NO_ALU;
                relAlunoFrequencia.CoAlu = aluno.CO_ALU;
                relAlunoFrequencia.nire = aluno.NU_NIRE.ToString("0000000000");

                string statusMateria = "";
                string statusMatricula = "";
                ///Realiza um loop em todas as materias de para cada aluno validando a quantidade de faltas
                foreach (var materia in materias)
                {
                    var historico = (from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                                      where tb079.CO_EMP == LoginAuxili.CO_EMP
                                      && tb079.CO_ANO_REF == ddlAno.SelectedValue
                                      && tb079.CO_MODU_CUR == modalidade
                                      && tb079.CO_CUR == serie
                                      && tb079.CO_TUR == turma
                                      && tb079.CO_MAT == materia.CO_MAT
                                      && tb079.CO_ALU == aluno.CO_ALU
                                      select tb079).FirstOrDefault();
                    if (historico != null)
                    {
                        int totalFaltas = ((historico.QT_FALTA_BIM1 ?? 0) + (historico.QT_FALTA_BIM2 ?? 0) + (historico.QT_FALTA_BIM3 ?? 0) + (historico.QT_FALTA_BIM4 ?? 0));
                        int percFalta = ((totalFaltas * (materia.QTDE_AULA_SEM ?? 0)) / 100);
                        if (percFalta < dcmPercFalta) //Verifica se o percentual de faltas está abaixo do limite máximo para o curso
                        {
                            if (statusMateria != statusMatriculaAluno.R) //Se ainda não estiver com status "R", insere status "A"
                                statusMateria = statusMatriculaAluno.A;
                        }
                        else //Se o percentual de faltas ficou acima do limite permitido no cadastro do curso
                        {
                            statusMateria = statusMatriculaAluno.R;
                            //Personaliza o status para apresentar as disciplinas nas quais o aluno ficou com faltas acima do permitido
                            if (!statusMatricula.Contains("Pendências em:"))
                                statusMatricula = "Pendências em:";
                            statusMatricula += materia.NO_MATERIA + "; ";
                        }
                    }
                    
                }
                //todo: Verificar finalização de falta e notas

                relAlunoFrequencia.StatusMateria = statusMateria;
                relAlunoFrequencia.StatusMatricula = statusMatricula;
                lstRelAlunoFrequencia.Add(relAlunoFrequencia);
                this.qtCalculado = this.qtCalculado + 1;
                HttpContext.Current.Session["qtCalcAFF"] = this.qtCalculado;
                //updCount.Update();
            }

            grdBusca.DataKeyNames = new string[] { "CoAlu", "StatusMateria" };
            grdBusca.DataSource = lstRelAlunoFrequencia.OrderBy(r => r.NoAlu);
            grdBusca.DataBind();

            divGrid.Visible = true;

            AuxiliPagina.EnvioAvisoGeralSistema(this.Page, "Processamento e Análise realizados. Favor salvar as informações pressionando o botão Salvar");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // todo: Mudar modo de registro de frequência
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string anoMesMat = ddlAno.SelectedValue;

            if (modalidade == 0 || serie == 0 || turma == 0)
            {
                grdBusca.DataBind();
                return;
            }

            int codAluno = 0;

            for (int numero = 0; numero < grdBusca.Rows.Count; numero++)
            {
                string codAlunoString = grdBusca.DataKeys[numero]["CoAlu"].ToString();
                codAluno = codAlunoString != "" ? int.Parse(codAlunoString) : 0;
                if (codAluno > 0)
                {
                    TB08_MATRCUR tb08 = TB08_MATRCUR.RetornaPelaChavePrimaria(codAluno, serie, anoMesMat, "1");
                    if (tb08 != null)
                    {
                        tb08.CO_STA_APROV_FREQ = grdBusca.Rows[numero].Cells[2].Text == statusMatriculaAluno.A ? statusMatri[statusMatriculaAluno.A] : statusMatri[statusMatriculaAluno.A];

                        if (tb08.EntityState != System.Data.EntityState.Unchanged && GestorEntities.SaveOrUpdate(tb08) < 1)
                        {
                            AuxiliPagina.EnvioMensagemErro(this.Page, "Erro ao salvar os itens.");
                            return;
                        }
                    }
                    else
                    {
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Matrícula não encontrada.");
                        return;
                    }
                }
                else
                {
                    AuxiliPagina.EnvioMensagemErro(this.Page, "Aluno não encontrado.");
                    return;
                }
            }

            AuxiliPagina.RedirecionaParaPaginaSucesso("Registros Salvos com sucesso.", Request.Url.AbsoluteUri);
        }

        #endregion

    }
}