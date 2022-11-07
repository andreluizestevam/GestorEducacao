using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.App_Masters;


namespace C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3500_CtrlDesempenhoEscolar._3504ManuHomolMedias
{
    public partial class Cadastro : System.Web.UI.Page
    {

        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaAnos();
                CarregaModalidades();
                CarregaSerieCurso();
                CarregaTurma();
                CarregaDisciplina();
                CarregaMedidas();
                divGrid.Visible = false;
                liChk.Visible = false;
            }

        }

        //====> Carrega o tipo de medida da Referência (Mensal/Bimestre/Trimestre/Semestre/Anual)
        private void CarregaMedidas()
        {
            var tipo = "B";

            var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP);

            tb25.TB83_PARAMETROReference.Load();
            if (tb25.TB83_PARAMETRO != null)
                tipo = tb25.TB83_PARAMETRO.TP_PERIOD_AVAL;

            AuxiliCarregamentos.CarregaMedidasTemporais(ddlReferencia, false, tipo, false, true);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int materia = ddlDisciplina.SelectedValue != "" ? int.Parse(ddlDisciplina.SelectedValue) : 0;
            string anoRef = ddlAno.SelectedValue;
            string referencia = ddlReferencia.SelectedValue;
            if (modalidade == 0 || serie == 0 || turma == 0 || materia == 0)
            {
                grdBusca.DataBind();
                return;
            }

            //--------> Varre toda a grid de Busca
            foreach (GridViewRow linha in grdBusca.Rows)
            {
                TB079_HIST_ALUNO tb079;


                int coAluno = int.Parse((((HiddenField)linha.Cells[8].FindControl("hidCoAtividade")).Value));

                tb079 = TB079_HIST_ALUNO.RetornaPelaChavePrimaria(coAluno, modalidade, serie, anoRef, materia);

                if (((CheckBox)linha.Cells[8].FindControl("ckSelect")).Checked)
                {
                    switch (referencia)
                    {
                        case "B1":
                            tb079.FL_HOMOL_NOTA_BIM1 = "S";
                            break;
                        case "B2":
                            tb079.FL_HOMOL_NOTA_BIM2 = "S";
                            break;
                        case "B3":
                            tb079.FL_HOMOL_NOTA_BIM3 = "S";
                            break;
                        case "B4":
                            tb079.FL_HOMOL_NOTA_BIM4 = "S";
                            break;
                        default:
                            break;
                    }
                }
                else
                {

                    switch (referencia)
                    {
                        case "B1":
                            tb079.FL_HOMOL_NOTA_BIM1 = "N";
                            break;
                        case "B2":
                            tb079.FL_HOMOL_NOTA_BIM2 = "N";
                            break;
                        case "B3":
                            tb079.FL_HOMOL_NOTA_BIM3 = "N";
                            break;
                        case "B4":
                            tb079.FL_HOMOL_NOTA_BIM4 = "N";
                            break;
                        default:
                            break;
                    }

                }
                //tb119.FL_HOMOL_DIARIO = "N";


                TB079_HIST_ALUNO.SaveOrUpdate(tb079, true);


            }

            AuxiliPagina.RedirecionaParaPaginaSucesso("Registros Salvos com sucesso.", Request.Url.AbsoluteUri);
        }

        /// <summary>
        /// Método que carrega a grid
        /// </summary>
        private void CarregaGrid()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            int materia = ddlDisciplina.SelectedValue != "" ? int.Parse(ddlDisciplina.SelectedValue) : 0;
            string referencia = ddlReferencia.SelectedValue;
            string anoMesMat = ddlAno.SelectedValue;

            if (modalidade == 0 || serie == 0 || turma == 0 || materia == 0)
            {
                grdBusca.DataBind();
                return;
            }

            divGrid.Visible = ligrid.Visible = true;

            var resultado = (from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                             join tb08 in TB08_MATRCUR.RetornaTodosRegistros() on tb079.CO_ALU equals tb08.CO_ALU
                             where tb08.TB25_EMPRESA.CO_EMP == LoginAuxili.CO_EMP && tb08.CO_ANO_MES_MAT == anoMesMat
                             && tb08.CO_CUR == serie && tb08.CO_TUR == turma && tb079.CO_MAT == materia
                             select new Saida
                             {
                                 CO_ALU = tb079.CO_ALU,
                                 NO_ALU = tb08.TB07_ALUNO.NO_ALU,
                                 CO_ANO_REF = tb079.CO_ANO_REF,
                                 CO_MAT = tb079.CO_MAT,
                                 CO_CUR = tb079.CO_CUR,
                                 CO_ALU_CAD_R = tb08.CO_ALU_CAD.Insert(2, ".").Insert(6, "."),
                                 NU_NIRE = tb08.TB07_ALUNO.NU_NIRE,
                                 VL_PROVA_FINAL = tb079.VL_PROVA_FINAL,
                                 CO_SIT_MAT = tb08.CO_SIT_MAT,
                                 FL_HOMOL_NOTA_BIM1 = tb079.FL_HOMOL_NOTA_BIM1,
                                 FL_HOMOL_NOTA_BIM2 = tb079.FL_HOMOL_NOTA_BIM2,
                                 FL_HOMOL_NOTA_BIM3 = tb079.FL_HOMOL_NOTA_BIM3,
                                 FL_HOMOL_NOTA_BIM4 = tb079.FL_HOMOL_NOTA_BIM4,
                             }).OrderBy(w => w.NO_ALU).ToList();


            if (resultado.Count() > 0)
            {
                liChk.Visible = true;
                // Habilita o botão de salvar 

                foreach (var item in resultado)
                {
                    switch (referencia)
                    { 
                        case "B1":
                            item.FL_HOMOL_ATIV = item.FL_HOMOL_NOTA_BIM1 == "S" ? true : false;
                            break;
                        case "B2":
                            item.FL_HOMOL_ATIV = item.FL_HOMOL_NOTA_BIM2 == "S" ? true : false;
                            break;
                        case "B3":
                            item.FL_HOMOL_ATIV = item.FL_HOMOL_NOTA_BIM3 == "S" ? true : false;
                            break;
                        case "B4":
                            item.FL_HOMOL_ATIV = item.FL_HOMOL_NOTA_BIM3 == "S" ? true : false;
                            break;
                        default:
                            break;
                    }



                    #region Media do Bimestre

                    var nota = (from tb079 in TB079_HIST_ALUNO.RetornaTodosRegistros()
                                join tb43 in TB43_GRD_CURSO.RetornaTodosRegistros() on tb079.CO_CUR equals tb43.CO_CUR
                                join alu in TB07_ALUNO.RetornaTodosRegistros() on tb079.CO_ALU equals alu.CO_ALU
                                where tb079.CO_MODU_CUR == modalidade && tb079.CO_CUR == item.CO_CUR && tb079.CO_TUR == turma
                                && (tb079.CO_ANO_REF == anoMesMat)
                                && tb079.CO_ANO_REF == tb43.CO_ANO_GRADE && tb079.CO_MAT == tb43.CO_MAT
                                && (item.CO_CUR != 0 ? tb079.CO_ALU == item.CO_ALU : 0 == 0)
                                && tb43.CO_EMP == tb079.CO_EMP
                                && tb079.CO_EMP == LoginAuxili.CO_EMP
                                && tb079.CO_MAT == item.CO_MAT
                                select new
                                {
                                    media = (referencia == "B1" ? tb079.VL_NOTA_BIM1 : referencia == "B2" ? tb079.VL_NOTA_BIM2 : referencia == "B3" ? tb079.VL_NOTA_BIM3 : tb079.VL_NOTA_BIM4),
                                    idagrup = tb43.ID_MATER_AGRUP,
                                }).FirstOrDefault();
                    //Atribui a nota verificando se é nulo e se é disciplina filha
                    if (nota != null)
                    {
                        item.MB = (nota.idagrup == null ? nota.media.HasValue ? nota.media.Value.ToString("N1") : "" : "  ");

                    }
                    else
                    {

                        item.MB = "";
                    }


                    #endregion

                    #region AVs

                    int idMateria = TB02_MATERIA.RetornaTodosRegistros().Where(w => w.CO_MAT == materia).FirstOrDefault().ID_MATERIA;

                    int ano = int.Parse(anoMesMat);
                    var result = (from tb49 in TB49_NOTA_ATIV_ALUNO.RetornaTodosRegistros()
                                  join tb273 in TB273_TIPO_ATIVIDADE.RetornaTodosRegistros() on tb49.TB273_TIPO_ATIVIDADE.ID_TIPO_ATIV equals tb273.ID_TIPO_ATIV
                                  where tb49.TB07_ALUNO.CO_ALU == item.CO_ALU
                                  && tb49.TB01_CURSO.CO_CUR == item.CO_CUR
                                  && tb49.CO_ANO == ano
                                  && tb49.CO_BIMESTRE == referencia
                                  && tb49.TB107_CADMATERIAS.ID_MATERIA == idMateria
                                  select new
                                  {
                                      tb273.CO_SIGLA_ATIV,
                                      tb49.VL_NOTA,
                                      tb49.CO_REFER_NOTA,

                                  }).ToList();
                    foreach (var l in result)
                    {
                        if (l.CO_REFER_NOTA == "AV1")
                        {
                            item.AV1 = "";
                            item.AV1 += "  " + l.VL_NOTA.ToString("N2");
                        }
                        if (l.CO_REFER_NOTA == "AV2")
                        {
                            item.AV2 = "";
                            item.AV2 += "  " + l.VL_NOTA.ToString("N2");
                        }
                        if (l.CO_REFER_NOTA == "AV3")
                        {
                            item.AV3 = "";
                            item.AV3 += " " + l.VL_NOTA.ToString("N2");
                        }
                        if (l.CO_REFER_NOTA == "AV4")
                        {
                            item.AV4 = "";
                            item.AV4 += "  " + l.VL_NOTA.ToString("N2");
                        }
                        if (l.CO_REFER_NOTA == "AV5")
                        {
                            item.AV5 = "";
                            item.AV5 += " " + l.VL_NOTA.ToString("N2");
                        }
                    }
                    #endregion
                }
            }

            grdBusca.DataKeyNames = new string[] { "CO_ALU" };
            grdBusca.DataSource = resultado.Count() > 0 ? resultado : null;
            grdBusca.DataBind();

            if (grdBusca.DataSource != null)
            {

                foreach (GridViewRow linha in grdBusca.Rows)
                {

                    string coMB = (((HiddenField)linha.Cells[8].FindControl("HiddenMB")).Value);
                    if (coMB == "")
                    {

                        CheckBox chks = ((CheckBox)linha.Cells[8].FindControl("ckSelect"));
                        chks.Enabled = false;
                    }
                    else
                    {

                        CheckBox chkN = ((CheckBox)linha.Cells[8].FindControl("ckSelect"));
                        chkN.Enabled = true;
                    }



                }
            }

        }

        /// <summary>
        /// Método que carrega o dropdown de Anos
        /// </summary>
        private void CarregaAnos()
        {
            ddlAno.DataSource = (from tb43 in TB43_GRD_CURSO.RetornaPelaEmpresa(LoginAuxili.CO_EMP)
                                 select new { tb43.CO_ANO_GRADE }).Distinct().OrderByDescending(g => g.CO_ANO_GRADE);

            ddlAno.DataTextField = "CO_ANO_GRADE";
            ddlAno.DataValueField = "CO_ANO_GRADE";
            ddlAno.DataBind();
        }

        /// <summary>
        /// Método que carrega o dropdown de Modalidades
        /// </summary>
        private void CarregaModalidades()
        {
            AuxiliCarregamentos.carregaModalidades(ddlModalidade, LoginAuxili.ORG_CODIGO_ORGAO, false);
        }

        /// <summary>
        /// Método que carrega o dropdown de Séries
        /// </summary>
        private void CarregaSerieCurso()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            string anoGrade = ddlAno.SelectedValue;
            AuxiliCarregamentos.carregaSeriesGradeCurso(ddlSerieCurso, modalidade, anoGrade, LoginAuxili.CO_EMP, false);
        }

        /// <summary>
        /// Método que carrega o dropdown de Turmas
        /// </summary>
        private void CarregaTurma()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaTurmas(ddlTurma, LoginAuxili.CO_EMP, modalidade, serie, false);
        }

        /// <summary>
        /// Método que carrega o dropdown de Disciplinas
        /// </summary>
        private void CarregaDisciplina()
        {
            int modalidade = ddlModalidade.SelectedValue != "" ? int.Parse(ddlModalidade.SelectedValue) : 0;
            int serie = ddlSerieCurso.SelectedValue != "" ? int.Parse(ddlSerieCurso.SelectedValue) : 0;
            int turma = ddlTurma.SelectedValue != "" ? int.Parse(ddlTurma.SelectedValue) : 0;
            string anoGrade = ddlAno.SelectedValue;
            AuxiliCarregamentos.CarregaMateriasGradeCurso(ddlDisciplina, LoginAuxili.CO_EMP, modalidade, serie, anoGrade, false);
        }

        /// <summary>
        /// Homologa  todos que não estão homologados menos os que não tem média 
        /// </summary>
        private void ChkHomologaTodos()
        {


            if (Chk.SelectedValue == "M")
            {
                foreach (GridViewRow linha in grdBusca.Rows)
                {

                    string coMB = (((HiddenField)linha.Cells[8].FindControl("HiddenMB")).Value);
                    if (coMB == "")
                    {
                        CheckBox chk = ((CheckBox)linha.Cells[8].FindControl("ckSelect"));
                        chk.Checked = false;
                    }
                    else
                    {

                        CheckBox chkN = ((CheckBox)linha.Cells[8].FindControl("ckSelect"));
                        chkN.Checked = true;
                    }

                }
            }
            else
            {
                foreach (GridViewRow linha in grdBusca.Rows)
                {
                    CheckBox chkN = ((CheckBox)linha.Cells[8].FindControl("ckSelect"));
                    chkN.Checked = false;
                }
            }


        }


        /// <summary>
        /// Vejo se ah média caso não (não homologa) 
        /// </summary>

        private void ChkAMedia()
        {



            foreach (GridViewRow linha in grdBusca.Rows)
            {
                if (((CheckBox)linha.Cells[8].FindControl("ckSelect")).Checked)
                {
                    string coMB = (((HiddenField)linha.Cells[8].FindControl("HiddenMB")).Value);
                    if (coMB == "")
                    {

                        CheckBox chks = ((CheckBox)linha.Cells[8].FindControl("ckSelect"));
                        chks.Checked = false;
                        AuxiliPagina.EnvioMensagemErro(this.Page, "Não a média  cadastrada !! Por isso não e possível realizar está operação ");
                    }
                    else
                    {

                        CheckBox chkN = ((CheckBox)linha.Cells[8].FindControl("ckSelect"));
                        chkN.Checked = true;
                    }
                }


            }


        }

        protected void ddlModalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaSerieCurso();
        }

        protected void ddlSerieCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTurma();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaDisciplina();
        }

        protected void ChkHomologaTodos_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChkHomologaTodos();
        }

        protected void ckSelect_SelectedIndexChanged(object sender, EventArgs e)
        {

            ChkAMedia();

        }

        protected void lbkPesq_OnClick(object sender, EventArgs e)
        {
            CarregaGrid();
        }


        public class Saida
        {

            public int CO_ATIV_PROF_TUR { get; set; }

            //Dados do Aluno
            public int CO_ALU { get; set; }
            public string NO_ALU { get; set; }
            public int NU_NIRE { get; set; }

            public string CO_ALU_CAD
            {
                get
                {
                    return this.NU_NIRE.ToString().PadLeft(7, '0') + " - " + this.DESC_CO_SIT_MAT;
                }
            }
            public string DESC_CO_SIT_MAT
            {
                get
                {
                    return this.CO_SIT_MAT == "A" ? "Matriculado" : this.CO_SIT_MAT == "X" ? "Transferido" : this.CO_SIT_MAT == "F" ? "Finalizado" : this.CO_SIT_MAT == "T" ? "Trancado" : this.CO_SIT_MAT == "C" ? "Cancelado" : "Pendente";
                }
            }
            public string CO_SIT_MAT { get; set; }
            //Dados de Matrícula
            public string CO_ALU_CAD_R { get; set; }
            public string CO_ANO_REF { get; set; }
            public int CO_CUR { get; set; }
            public int CO_MAT { get; set; }


            //Dados de Notas

            //Mostra asterisco ao lado da nota caso não esteja homologada


            public decimal? VL_PROVA_FINAL { get; set; }



            //Dados de Notas
            public string AV1 { get; set; }
            public string AV2 { get; set; }
            public string AV3 { get; set; }
            public string AV4 { get; set; }
            public string AV5 { get; set; }
            public string MB { get; set; }
            public bool FL_HOMOL_ATIV { get; set; }
            public string FL_HOMOL_ATIV_VALOR { get; set; }
            public string FL_HOMOL_NOTA_BIM1 { get; set; }
            public string FL_HOMOL_NOTA_BIM2 { get; set; }
            public string FL_HOMOL_NOTA_BIM3 { get; set; }
            public string FL_HOMOL_NOTA_BIM4 { get; set; }



        }

    }
}