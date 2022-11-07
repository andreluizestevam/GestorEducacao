//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: ENCERRAMENTO ATIVIDADES DO PERÍODO LETIVO
// OBJETIVO: REGISTRO DE PROVA FINAL / MÉDIA FINAL 
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
// 16/05/2013| André Nobre Vinagre        | Adicionado tratamento de prova final para o
//           |                            | Colégio específico
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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3900_CtrlEncerramentoLetivo.F3902_RegistroProvaMediaFinal
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentCadastroMasterPage { get { return (PadraoCadastros)Page.Master; } }
        Dictionary<string, string> tiposAprovacao = AuxiliBaseApoio.chave(statusAprovacaoAluno.ResourceManager);
        Dictionary<string, string> statusMatricula = AuxiliBaseApoio.chave(statusMatriculaAluno.ResourceManager);
        Dictionary<string, string> tipoMedia = AuxiliBaseApoio.chave(tipoMediaBimestral.ResourceManager);
        Dictionary<string, string> tipoConceito = AuxiliBaseApoio.chave(tipoConceitoAluno.ResourceManager);

        #region Eventos

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //--------> Criação das instâncias utilizadas
            CurrentCadastroMasterPage.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentCadastroMasterPage_OnAcaoBarraCadastro);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            CarregaAnos();
            CarregaModalidades();
            CarregaSerieCurso();
            CarregaTurma();
            divGrid.Visible = false;
        }

        /// <summary>
        /// Processo de Inclusão, Alteração e Exclusão de Registros na Entidade do BD, após a ação de salvar
        /// </summary>
        void CurrentCadastroMasterPage_OnAcaoBarraCadastro()
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
                                 select new { CO_ANO_GRADE = tb43.CO_ANO_GRADE.Trim() }).Distinct().OrderByDescending(g => g.CO_ANO_GRADE);

            ddlAno.DataTextField = "CO_ANO_GRADE";
            ddlAno.DataValueField = "CO_ANO_GRADE";
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
                ddlSerieCurso.DataSource = (from tb01 in TB01_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                            where tb01.CO_MODU_CUR == modalidade
                                            join tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP) on tb01.CO_CUR equals tb43.CO_CUR
                                            where tb43.TB44_MODULO.CO_MODU_CUR == tb01.CO_MODU_CUR
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
                                       select new { tb06.TB129_CADTURMAS.CO_SIGLA_TURMA, tb06.CO_TUR }).OrderBy(t => t.CO_SIGLA_TURMA);

                ddlTurma.DataTextField = "CO_SIGLA_TURMA";
                ddlTurma.DataValueField = "CO_TUR";
                ddlTurma.DataBind();
            }
            else
                ddlTurma.Items.Clear();

            ddlTurma.Items.Insert(0, new ListItem("Selecione", ""));
        }

        #endregion

        #region Evento de componentes

        protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
            CarregaTurma();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
        }

        protected void imgAdd_Click(object sender, EventArgs e)
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string ano = ddlAno.SelectedValue != "" ? ddlAno.SelectedValue : DateTime.Now.Year.ToString();

            if (modalidade == 0 || serie == 0 || turma == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Modalidade, série e turma devem ser selecionados.");
                return;
            }

            ///Carrega os parametros para verificação de aprovação nas matérias
            TB01_CURSO curso = TB01_CURSO.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, modalidade, serie);
            if (curso == null)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Não possível localizar a série/curso.");
                return;
            }
            string statusMatri = statusMatricula[statusMatriculaAluno.A];
            ///Busca todas as matrículas ativas e aprovadas em frequência
            var matriculas = (from tb08 in TB08_MATRCUR.RetornaTodosRegistros()
                              where tb08.TB44_MODULO.CO_MODU_CUR == modalidade
                             && tb08.CO_CUR == serie
                             && tb08.CO_TUR == turma
                             && tb08.CO_ANO_MES_MAT == ano
                             && tb08.CO_SIT_MAT == statusMatri
                             && (chkPrioriAnalFreq.Checked ? tb08.CO_STA_APROV_FREQ == statusMatri : 0 == 0)
                              select new lstMediaFinal
                              {
                                  nomeAluno = tb08.TB07_ALUNO.NO_ALU,
                                  numeroNire = tb08.TB07_ALUNO.NU_NIRE,
                                  aprovacaoFreq = tb08.CO_STA_APROV_FREQ,
                                  faltas = 0,
                                  nota = 0,
                                  obs = "",
                                  status = "",

                                  codigoCadastroAluno = tb08.CO_ALU_CAD,
                                  codigoAluno = tb08.CO_ALU,
                                  codigoModulo = tb08.TB44_MODULO.CO_MODU_CUR,
                                  codigoSerie = tb08.CO_CUR,
                                  codigoTurma = (tb08.CO_TUR ?? 0),
                                  referenciaAno = tb08.CO_ANO_MES_MAT
                              }).DistinctBy(d => d.codigoAluno).OrderBy(o => o.nomeAluno).DefaultIfEmpty().ToList();
            if (matriculas == null || matriculas.Count() <= 0 || matriculas.ElementAt(0) == null)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Nenhuma matrícula aprovada em frequência encontrada para a turma selecionada.");
                return;
            }

            List<lstMediaFinal> listagemGride = new List<lstMediaFinal>();
            ///Realizando o verredura nos alunos da turma
            foreach (var aluno in matriculas)
            {
                string observacao = string.Empty;
                string status = string.Empty;
                aluno.faltas = 0;
                int totalMateriasAprovado = 0;
                int totalMateriasReprovado = 0;
                var materias = (from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                                where tb079.CO_ALU == aluno.codigoAluno
                                && tb079.CO_ANO_REF == aluno.referenciaAno
                                && tb079.TB44_MODULO.CO_MODU_CUR == aluno.codigoModulo
                                && tb079.CO_CUR == aluno.codigoSerie
                                && tb079.CO_TUR == aluno.codigoTurma
                                && tb079.CO_MAT != null
                                join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb079.CO_MAT equals tb02.CO_MAT into resultado
                                from tb02 in resultado.DefaultIfEmpty()
                                join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA into resultado1
                                from tb107 in resultado1.DefaultIfEmpty()
                                join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on tb02.CO_MAT equals tb43.CO_MAT
                                where tb02 != null
                                && tb107 != null
                                && tb43 != null
                                && tb43.CO_ANO_GRADE == tb079.CO_ANO_REF
                                && tb43.CO_CUR == tb079.CO_CUR
                                && tb43.CO_EMP == tb079.CO_EMP
                                && tb43.TB44_MODULO.CO_MODU_CUR == tb079.CO_MODU_CUR
                                && tb43.CO_SITU_MATE_GRC == "A"
                                && tb43.ID_MATER_AGRUP == null
                                && tb107.CO_CLASS_BOLETIM != 4
                                orderby tb107.NO_MATERIA
                                select new { tb079, tb107 }).DistinctBy(d => d.tb079.CO_MAT).ToList();

                ///Valor de conceito quando houver, na ausência a maior nota
                aluno.nota = materias.Max(m => m.tb079.VL_MEDIA_FINAL);
                aluno.faltas = materias.Sum(s => (s.tb079.QT_FALTA_SEM1 + s.tb079.QT_FALTA_SEM2));

                if (materias == null && materias.Count() <= 0)
                {
                    observacao = "Sem materias";
                    status = "Não processada";

                }
                else
                {
                    //if (aluno.aprovacaoFreq == tiposAprovacao[statusAprovacaoAluno.R])
                    if ((chkPrioriAnalFreq.Checked) && (aluno.aprovacaoFreq == tiposAprovacao[statusAprovacaoAluno.R]))
                    {
                        observacao = statusAprovacaoAluno.F;
                        status = statusAprovacaoAluno.R;
                    }
                    else
                    {
                        foreach (var materia in materias)
                        {
                            //Instancia um objeto da tb079 para persistir o status da disciplina no histórico do aluno
                            TB079_HIST_ALUNO tb079obPers = (from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                                                            where tb079.CO_ALU == aluno.codigoAluno
                                                            && tb079.CO_ANO_REF == aluno.referenciaAno
                                                            && tb079.TB44_MODULO.CO_MODU_CUR == aluno.codigoModulo
                                                            && tb079.CO_CUR == aluno.codigoSerie
                                                            && tb079.CO_TUR == aluno.codigoTurma
                                                            && tb079.CO_MAT == materia.tb079.CO_MAT
                                                            select tb079).FirstOrDefault();

                            decimal? notaRecuFinal = tb079obPers.VL_PROVA_FINAL; //Coleta a nota de recuperação final para uso posterior

                            if (materia.tb079.FL_TIPO_LANC_MEDIA == tipoMedia[tipoMediaBimestral.N])
                            {
                                decimal? media = 0;
                                //Verifica se qual a maior nota entre a média final e a nota de recuperação final
                                #region Verifica maior nota entre Recuperação e Média Final
                                //Se tem nota de recuperação final
                                if (materia.tb079.VL_PROVA_FINAL.HasValue)
                                {
                                    //se tem nota de recuperação final verifica quem é maior
                                    media = (materia.tb079.VL_MEDIA_FINAL.HasValue ? (materia.tb079.VL_PROVA_FINAL.Value > materia.tb079.VL_MEDIA_FINAL.Value ? materia.tb079.VL_PROVA_FINAL.Value : materia.tb079.VL_MEDIA_FINAL.Value) : materia.tb079.VL_PROVA_FINAL.Value);
                                }
                                else
                                    media = materia.tb079.VL_MEDIA_FINAL.HasValue ? materia.tb079.VL_MEDIA_FINAL : null;

                                #endregion

                                if (media != null && curso.MED_FINAL_CUR != null && media >= curso.MED_FINAL_CUR)
                                {
                                    totalMateriasAprovado++;
                                    //if (status != statusAprovacaoAluno.R)
                                    //{
                                    observacao = "";
                                    if (status != statusAprovacaoAluno.R) // Se játiver sido dado como reprovado em outra disciplina, não
                                        status = statusAprovacaoAluno.A;//permite alterar o status para aprovado na seguinte, evitando problemas.

                                    tb079obPers.CO_STA_APROV_MATERIA = tiposAprovacao[statusAprovacaoAluno.A];
                                    TB079_HIST_ALUNO.SaveOrUpdate(tb079obPers, true);
                                    //}
                                }
                                else
                                {
                                    totalMateriasReprovado++;
                                    observacao = statusAprovacaoAluno.N;
                                    status = statusAprovacaoAluno.R;
                                    tb079obPers.CO_STA_APROV_MATERIA = tiposAprovacao[statusAprovacaoAluno.R];
                                    TB079_HIST_ALUNO.SaveOrUpdate(tb079obPers, true);
                                }
                            }
                            else
                            {
                                int quantidade = 0;
                                TB83_PARAMETRO tb83 = new TB83_PARAMETRO();
                                if (tb83.TP_PERIOD_AVAL.Equals("T"))
                                {
                                    if (materia.tb079.VL_CONC_TRI1 == tipoConceito[tipoConceitoAluno.B]
                                        || materia.tb079.VL_CONC_TRI1 == tipoConceito[tipoConceitoAluno.O]
                                        || materia.tb079.VL_CONC_TRI1 == tipoConceito[tipoConceitoAluno.E])
                                        quantidade++;
                                    if (materia.tb079.VL_CONC_TRI2 == tipoConceito[tipoConceitoAluno.B]
                                        || materia.tb079.VL_CONC_TRI2 == tipoConceito[tipoConceitoAluno.O]
                                        || materia.tb079.VL_CONC_TRI2 == tipoConceito[tipoConceitoAluno.E])
                                        quantidade++;
                                    if (materia.tb079.VL_CONC_TRI3 == tipoConceito[tipoConceitoAluno.B]
                                        || materia.tb079.VL_CONC_TRI3 == tipoConceito[tipoConceitoAluno.O]
                                        || materia.tb079.VL_CONC_TRI3 == tipoConceito[tipoConceitoAluno.E])
                                        quantidade++;

                                    if (quantidade == 3)
                                    {
                                        totalMateriasAprovado++;
                                        if (status != statusAprovacaoAluno.R)
                                        {
                                            observacao = "";
                                            status = statusAprovacaoAluno.A;
                                            tb079obPers.CO_STA_APROV_MATERIA = tiposAprovacao[statusAprovacaoAluno.A];
                                            TB079_HIST_ALUNO.SaveOrUpdate(tb079obPers, true);
                                        }
                                    }
                                    else
                                    {
                                        totalMateriasReprovado++;
                                        observacao = statusAprovacaoAluno.N;
                                        status = statusAprovacaoAluno.R;
                                        tb079obPers.CO_STA_APROV_MATERIA = tiposAprovacao[statusAprovacaoAluno.R];
                                        TB079_HIST_ALUNO.SaveOrUpdate(tb079obPers, true);
                                    }
                                }
                                else
                                {
                                    if (materia.tb079.VL_CONC_BIM1 == tipoConceito[tipoConceitoAluno.B]
                                        || materia.tb079.VL_CONC_BIM1 == tipoConceito[tipoConceitoAluno.O]
                                        || materia.tb079.VL_CONC_BIM1 == tipoConceito[tipoConceitoAluno.E])
                                        quantidade++;
                                    if (materia.tb079.VL_CONC_BIM2 == tipoConceito[tipoConceitoAluno.B]
                                        || materia.tb079.VL_CONC_BIM2 == tipoConceito[tipoConceitoAluno.O]
                                        || materia.tb079.VL_CONC_BIM2 == tipoConceito[tipoConceitoAluno.E])
                                        quantidade++;
                                    if (materia.tb079.VL_CONC_BIM3 == tipoConceito[tipoConceitoAluno.B]
                                        || materia.tb079.VL_CONC_BIM3 == tipoConceito[tipoConceitoAluno.O]
                                        || materia.tb079.VL_CONC_BIM3 == tipoConceito[tipoConceitoAluno.E])
                                        quantidade++;
                                    if (materia.tb079.VL_CONC_BIM4 == tipoConceito[tipoConceitoAluno.B]
                                        || materia.tb079.VL_CONC_BIM4 == tipoConceito[tipoConceitoAluno.O]
                                        || materia.tb079.VL_CONC_BIM4 == tipoConceito[tipoConceitoAluno.E])
                                        quantidade++;

                                    if (quantidade == 4)
                                    {
                                        totalMateriasAprovado++;
                                        if (status != statusAprovacaoAluno.R)
                                        {
                                            observacao = "";
                                            status = statusAprovacaoAluno.A;
                                            tb079obPers.CO_STA_APROV_MATERIA = tiposAprovacao[statusAprovacaoAluno.A];
                                            TB079_HIST_ALUNO.SaveOrUpdate(tb079obPers, true);
                                        }
                                    }
                                    else
                                    {
                                        totalMateriasReprovado++;
                                        observacao = statusAprovacaoAluno.N;
                                        status = statusAprovacaoAluno.R;
                                        tb079obPers.CO_STA_APROV_MATERIA = tiposAprovacao[statusAprovacaoAluno.R];
                                        TB079_HIST_ALUNO.SaveOrUpdate(tb079obPers, true);
                                    }
                                }
                            }

                            #region Verifica os status recuperação, depedência e conselho
                            ///Status de recuperação
                            if (totalMateriasReprovado > 0 && materia.tb079.FL_TIPO_LANC_MEDIA == tipoMedia[tipoMediaBimestral.N])
                            {
                                if (curso.FL_RECU)
                                {
                                    /*Verifica se a quantidade de reprovações é menor que a configurada no cadastro do curso e se ainda não existe
                                     nota de recuperação no histórico do aluno para esta disciplina.*/
                                    if (totalMateriasReprovado > 0 && totalMateriasReprovado <= curso.QT_MATE_RECU && (!notaRecuFinal.HasValue))
                                    {
                                        observacao = statusAprovacaoAluno.E;
                                        status = statusAprovacaoAluno.R;
                                    }
                                }
                                ///Status para dependência
                                if (curso.FL_DEPE)
                                {
                                    if (curso.FL_RECU)
                                    {
                                        if (totalMateriasReprovado > 0 && totalMateriasReprovado > curso.QT_MATE_RECU && totalMateriasReprovado <= curso.QT_MATE_DEPE)
                                        {
                                            observacao = statusAprovacaoAluno.D;
                                            status = statusAprovacaoAluno.A;
                                        }
                                    }
                                    else
                                    {
                                        if (totalMateriasReprovado > 0 && totalMateriasReprovado <= curso.QT_MATE_DEPE)
                                        {
                                            observacao = statusAprovacaoAluno.D;
                                            status = statusAprovacaoAluno.A;
                                        }
                                    }
                                }
                                ///Marcar status como conselho
                                if (curso.FL_CONS)
                                {
                                    if (curso.FL_RECU)
                                    {
                                        if (totalMateriasReprovado > 0 && totalMateriasReprovado > curso.QT_MATE_RECU && totalMateriasReprovado <= curso.QT_MATE_CONS)
                                        {
                                            observacao = statusAprovacaoAluno.C;
                                            status = statusAprovacaoAluno.R;
                                        }
                                        if (curso.FL_DEPE)
                                        {
                                            if (totalMateriasReprovado > 0 && totalMateriasReprovado > curso.QT_MATE_DEPE && totalMateriasReprovado <= curso.QT_MATE_CONS)
                                            {
                                                observacao = statusAprovacaoAluno.C;
                                                status = statusAprovacaoAluno.R;
                                            }
                                        }
                                    }
                                    else if (curso.FL_DEPE)
                                    {
                                        if (totalMateriasReprovado > 0 && totalMateriasReprovado > curso.QT_MATE_DEPE && totalMateriasReprovado <= curso.QT_MATE_CONS)
                                        {
                                            observacao = statusAprovacaoAluno.C;
                                            status = statusAprovacaoAluno.R;
                                        }
                                    }
                                    else
                                    {
                                        if (totalMateriasReprovado > 0 && totalMateriasReprovado <= curso.QT_MATE_CONS)
                                        {
                                            observacao = statusAprovacaoAluno.C;
                                            status = statusAprovacaoAluno.R;
                                        }
                                    }

                                }


                            #endregion
                                //observacao += "Matérias: Reprovados " + totalMateriasReprovado + ", Aprovados " + totalMateriasAprovado;
                                if (status == statusAprovacaoAluno.R)
                                {
                                    observacao += "Matérias: Reprovados " + totalMateriasReprovado + ", Aprovados " + totalMateriasAprovado;
                                }

                            }
                            else
                            {
                                status = statusAprovacaoAluno.A;
                                observacao = statusAprovacaoAluno.A + " Matérias aprovadas:" + totalMateriasAprovado;
                            }
                        }

                    }

                }

                lstMediaFinal valores = new lstMediaFinal();
                valores.status = status;
                valores.obs = observacao;
                valores.nota = aluno.nota;
                valores.nire = aluno.nire;
                valores.nomeAluno = aluno.nomeAluno;
                valores.codigoAluno = aluno.codigoAluno;
                valores.faltas = aluno.faltas;
                valores.codigoCadastroAluno = aluno.codigoCadastroAluno;
                listagemGride.Add(valores);
            }
            divGrid.Visible = true;
            grdAlunos.DataSource = listagemGride;
            grdAlunos.DataBind();

            AuxiliPagina.EnvioAvisoGeralSistema(this.Page, "Processamento e Análise concluídos com êxito. Favor salvar as informações pressionando o botão Salvar");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            TB08_MATRCUR tb08;

            foreach (GridViewRow linha in grdAlunos.Rows)
            {
                string codigo = grdAlunos.DataKeys[linha.RowIndex].Value.ToString();
                if (codigo != "")
                {
                    if ((linha.Cells[3].Text != null && linha.Cells[3].Text != "") && (linha.Cells[3].Text == statusAprovacaoAluno.A || linha.Cells[3].Text == statusAprovacaoAluno.R))
                    {
                        tb08 = (from lTb08 in TB08_MATRCUR.RetornaTodosRegistros()
                                where lTb08.CO_ALU_CAD == codigo && lTb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP
                                select lTb08).FirstOrDefault();
                        if (tb08 != null)
                        {
                            if (linha.Cells[3].Text == statusAprovacaoAluno.R)
                                tb08.CO_STA_APROV = tiposAprovacao[statusAprovacaoAluno.R];
                            else
                                tb08.CO_STA_APROV = tiposAprovacao[statusAprovacaoAluno.A];

                            TB08_MATRCUR.SaveOrUpdate(tb08, true);
                        }
                        else
                        {
                            AuxiliPagina.RedirecionaParaPaginaErro("Não foi possível salvar as alterações do aluno.", Request.Url.AbsoluteUri);
                            return;
                        }
                    }
                }
                else
                {
                    AuxiliPagina.RedirecionaParaPaginaErro("Não foi possível localizar os dados adicionais do aluno.", Request.Url.AbsoluteUri);
                    return;
                }
            }

            AuxiliPagina.RedirecionaParaPaginaSucesso("Resultado Salvo com Sucesso", Request.Url.AbsoluteUri);

        }

        protected void imgDet_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;

            if (grdAlunos.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdAlunos.Rows)
                {
                    img = (ImageButton)linha.Cells[2].FindControl("imgDet");

                    //Carrega a grid de detalhes das disciplinas do aluno
                    if (img.ClientID == atual.ClientID)
                    {
                        #region Coleta Informações para o Select

                        int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
                        int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
                        int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
                        string ano = ddlAno.SelectedValue != "" ? ddlAno.SelectedValue : DateTime.Now.Year.ToString();
                        int coAlu = int.Parse(((HiddenField)linha.Cells[2].FindControl("hidCoAlu")).Value);

                        #endregion

                        lblNomeAluno.Text = linha.Cells[1].Text.ToUpper();

                        var res = (from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                                   join tb02 in TB02_MATERIA.RetornaTodosRegistros() on tb079.CO_MAT equals tb02.CO_MAT
                                   join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on tb02.CO_MAT equals tb43.CO_MAT
                                   join tb107 in TB107_CADMATERIAS.RetornaTodosRegistros() on tb02.ID_MATERIA equals tb107.ID_MATERIA
                                   where tb079.CO_ALU == coAlu
                                   && tb079.CO_ANO_REF == ano
                                   && tb079.TB44_MODULO.CO_MODU_CUR == modalidade
                                   && tb079.CO_CUR == serie
                                   && tb079.CO_TUR == turma
                                   && tb43 != null
                                   && tb43.CO_ANO_GRADE == tb079.CO_ANO_REF
                                   && tb43.CO_CUR == tb079.CO_CUR
                                   && tb43.CO_EMP == tb079.CO_EMP
                                   && tb43.TB44_MODULO.CO_MODU_CUR == tb079.CO_MODU_CUR
                                   && tb43.CO_SITU_MATE_GRC == "A"
                                   && tb43.ID_MATER_AGRUP == null
                                   && tb107.CO_CLASS_BOLETIM != 4
                                   select new lstDetalhesAluno
                                   {
                                       NO_MATERIA = tb107.NO_MATERIA,
                                       VL_MEDIA_FINAL = tb079.VL_MEDIA_FINAL,
                                       VL_NOTA_RECU = tb079.VL_PROVA_FINAL,
                                       DE_RESULTADO = tb079.CO_STA_APROV_MATERIA,
                                   }).ToList();

                        grdDetalhes.DataSource = res;
                        grdDetalhes.DataBind();

                        AbreModal();

                        break; //Depois de encontrado o mesmo objeto que startou o postback, não faz sentido continuar o loop
                    }
                }
            }
        }

        protected void grdDetalhes_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            //--------> Criação do estilo e links das linhas da GRID
            if (e.Row.DataItem != null)
            {
                //Muda a cor da linha caso a disciplina no histórico do aluno não esteja com status de aprovada
                string status = (((HiddenField)e.Row.Cells[0].FindControl("hidCoAprov")).Value);
                if (status != "A")
                    e.Row.BackColor = System.Drawing.Color.LightSalmon;
            }
        }

        #endregion

        #region Classes
        private class lstMediaFinal
        {
            public string nire { get; set; }
            public int numeroNire
            {
                set
                {
                    this.nire = value.ToString("0000000000");
                }
            }
            public string nomeAluno { get; set; }
            public string obs { get; set; }
            public string status { get; set; }
            public decimal? nota { get; set; }
            public int? faltas { get; set; }
            public string aprovacaoFreq { get; set; }
            public string aprovacaoMat { get; set; }
            public string codigoCadastroAluno { get; set; }
            public int codigoAluno { get; set; }
            public int codigoModulo { get; set; }
            public int codigoSerie { get; set; }
            public int codigoTurma { get; set; }
            public string referenciaAno { get; set; }
        }

        public class lstDetalhesAluno
        {
            public string NO_MATERIA { get; set; }

            public decimal? VL_MEDIA_FINAL { get; set; }
            public string VL_MEDIA_FINAL_V
            {
                get
                {
                    return (this.VL_MEDIA_FINAL.HasValue ? this.VL_MEDIA_FINAL.Value.ToString("N2") : " - ");
                }
            }

            public decimal? VL_NOTA_RECU { get; set; }
            public string VL_NOTA_RECU_V
            {
                get
                {
                    return (this.VL_NOTA_RECU.HasValue ? this.VL_NOTA_RECU.Value.ToString("N2") : " - ");
                }
            }

            public string DE_RESULTADO { get; set; }
            public string DE_RESULTADO_V
            {
                get
                {
                    string s = "";
                    switch (this.DE_RESULTADO)
                    {
                        case "A":
                            s = "Aprovado(a)";
                            break;
                        case "R":
                            s = "Reprovado(a)";
                            break;
                        default:
                            s = " - ";
                            break;
                    }
                    return s;
                }
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método responsável por abrir a modal com as informações de aprovações ou reprovações do aluno clicado
        /// </summary>
        private void AbreModal()
        {
            ScriptManager.RegisterStartupScript(
                this.Page,
                this.GetType(),
                "Acao",
                "mostraModal();",
                true
            );
        }

        #endregion
    }
}
