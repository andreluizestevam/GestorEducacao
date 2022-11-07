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
// 
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
using System.Globalization;
using WRAuxiliares = C2BR.GestorEducacao.DLLRelatorioWeb.Auxiliares;
using C2BR.GestorEducacao.DLLRelatorioWeb.Interfaces;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports._3000_CtrlOperacionalPedagogico._3010_CtrlPedagogicoSeries;
using C2BR.GestorEducacao.Reports._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2107_MatriculaAluno;
using System.Data.Objects;

namespace C2BR.GestorEducacao.UI.GSAUD._7000_ControleOperRH._7100_CtrlPontoFuncional._7140_RegistroPlantao
{
    public partial class Cadastro : System.Web.UI.Page
    {
        public PadraoCadastros CurrentPadraoCadastros { get { return (PadraoCadastros)Page.Master; } }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            
            ///Criação das instâncias utilizadas
            CurrentPadraoCadastros.OnAcaoBarraCadastro += new PadraoCadastros.OnAcaoBarraCadastroHandler(CurrentPadraoCadastros_OnAcaoBarraCadastro);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDtIniResCons.Text = DateTime.Now.AddMonths(-1).ToString("dd/MM/yyyy");
                txtDtFimResCons.Text = DateTime.Now.AddMonths(1).ToString("dd/MM/yyyy");
                txtdtPl.Text = DateTime.Now.ToString("dd/MM/yyyy");

                CarregaEspecialidade(ddlEspMedResCons);
                CarregaEspecialidade(ddlEspecPlant);
                CarregaUnidades(ddlUnidPlant);
                CarregaTipoPlantao();
                CarregaDepartamento(ddlLocalPlant, ddlUnidPlant);
                CarregaUnidades(ddlUnidResCons);
                CarregaGridProfi();
            }
        }

        public bool ValidaTempoDescanso(int coCol, int coEmp, DateTime data)
        {
            try
            {
                bool r = false;

                //Faz uma lista de todos os plantões agendados para o colaborador em questão
                var res = (from tb159 in TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros()
                           where tb159.TB03_COLABOR.CO_COL == coCol
                           select new { tb159.DT_TERMIN_PREV, tb159.DT_INICIO_PREV, tb159.TB03_COLABOR.CO_COL, tb159.NU_TEMPO_DESCA_COLAB }).OrderByDescending(w => w.DT_INICIO_PREV).ToList();

                //Passa por todos os plantões do colaborador em questão verificando o tempo de descanso
                foreach (var l in res)
                {
                    if (r == false)
                    {
                        //Faz um Cálculo adicionando o tempo de descanso do colaborador à hora de término do plantão em questão, e verificando se a data e hora do plantão
                        //que está sendo associado ao colaborador, está dentro do tempo que deveria ser de descanso.
                        DateTime dt = l.DT_TERMIN_PREV.AddHours(l.NU_TEMPO_DESCA_COLAB);
                            if (data >= l.DT_TERMIN_PREV && data <= dt)
                            {
                                r = true;
                            }
                    }
                }

                return r;
            }
            catch
            {
                return false;
            }
        }

        void CurrentPadraoCadastros_OnAcaoBarraCadastro()
        {
            int coCol = 0;
            int coEmp = 0;

            bool colSelec = false;
            foreach (GridViewRow lis in grdProfi.Rows)
            {
                if (((CheckBox)lis.Cells[0].FindControl("ckSelect")).Checked == true)
                {
                    string hdc = (((HiddenField)lis.Cells[0].FindControl("hidCol")).Value);
                    string hde = (((HiddenField)lis.Cells[0].FindControl("hidEmp")).Value);
                    
                    //Atribui os valores do colaborador e da empresa selecionados.
                    coCol = (!string.IsNullOrEmpty(hdc) ? int.Parse(hdc) : 0);
                    coEmp = (!string.IsNullOrEmpty(hde) ? int.Parse(hde) : 0);
                    colSelec = true;
                }
            }

            //Verifica se foi selecionado um colaborador.
            if (colSelec == false)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "O Colaborador deve ser selecionado!");
                return;
            }

            if (ddlTipoPlant.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "O tipo de Plantão deve ser selecionado!");
                return;
            }

            if (ddlLocalPlant.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "O local do Plantão deve ser informado!");
                return;
            }

            TB159_AGENDA_PLANT_COLABOR tb159 = new TB159_AGENDA_PLANT_COLABOR();

            TB03_COLABOR colP = TB03_COLABOR.RetornaPelaChavePrimaria(coEmp, coCol);
            TB03_COLABOR colC = TB03_COLABOR.RetornaPelaChavePrimaria(LoginAuxili.CO_EMP, LoginAuxili.CO_COL);
            TB14_DEPTO depTo = TB14_DEPTO.RetornaPelaChavePrimaria(int.Parse(ddlLocalPlant.SelectedValue));
            TB153_TIPO_PLANT tpPlant = TB153_TIPO_PLANT.RetornaPelaChavePrimaria(int.Parse(ddlTipoPlant.SelectedValue));

            if (ddlLocalPlant.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this, "Nenhum Local de Plantão Selecionado.");
                return;
            }

            if (ddlTipoPlant.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this, "Nenhum Tipo de Plantão Selecionado.");
                return;
            }

            if (txtdtPl.Text == "")
            {
                AuxiliPagina.EnvioMensagemErro(this, "Data do Plantão não Informada.");
                return;
            }

            if (ddlEspecPlant.SelectedValue == "")
            {
                AuxiliPagina.EnvioMensagemErro(this, "Nenhuma Espcialidade do Plantão Selecionada.");
                return;
            }

            DateTime? dtIniP = (txtdtPl.Text != "" ? DateTime.Parse(txtdtPl.Text) : (DateTime?)null);

            dtIniP = dtIniP.Value.AddHours(int.Parse(tpPlant.HR_INI_TIPO_PLANT.Substring(0, 2)));
            dtIniP = dtIniP.Value.AddMinutes(int.Parse(tpPlant.HR_INI_TIPO_PLANT.Substring(3, 2)));

            DateTime dtFimP = dtIniP.Value.AddHours(tpPlant.QT_HORAS);

            DateTime dtIP = dtIniP.Value;
            DateTime dtFP = dtFimP;

            string flInconAgend = "N";
            string deInconAgend = "";

            if (ValidaTempoDescanso(colP.CO_COL, colP.CO_EMP, dtIP) == true)
            {
                flInconAgend = "S";
                deInconAgend = tb159.DESC_INSUF;
            }

            if (!colP.QT_HORAS_PLANT.HasValue)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "No cadastro do colaborador selecionado, não há informações da Quantidade de Horas dos Plantões permitida, favor preencher-la.");
                return;
            }
            if (!colP.QT_HORAS_DESCA.HasValue)
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "No cadastro do colaborador selecionado, não há informações da Quantidade de Horas de descanso entre plantões, favor preencher-la.");
                return;
            }

            tb159.TB03_COLABOR = colP;
            tb159.TB14_DEPTO = depTo;
            tb159.ID_TIPO_PLANT = tpPlant.ID_TIPO_PLANT;
            tb159.DT_INICIO_PREV = dtIP;
            tb159.DT_TERMIN_PREV = dtFP;
            tb159.CO_ESPEC_PLANT = int.Parse(ddlEspecPlant.SelectedValue);
            tb159.CO_ESPEC_COL = colP.CO_ESPEC;
            tb159.NU_CARGA_HORAR_PLANT = tpPlant.QT_HORAS;
            tb159.NU_CARGA_HORAR_COLAB = colP.QT_HORAS_PLANT.Value;
            tb159.NU_TEMPO_DESCA_COLAB = colP.QT_HORAS_DESCA.Value;
            tb159.TB03_COLABOR1 = colC;
            tb159.FL_INCON_AGEND = flInconAgend;
            tb159.DE_INCON_AGEND = deInconAgend;
            tb159.DT_CADAS_AGEND = DateTime.Now;
            tb159.CO_SITUA_AGEND = "P";
            tb159.DT_SITUA_AGEND = DateTime.Now;
            tb159.CO_EMP_AGEND_PLANT = int.Parse(ddlUnidPlant.SelectedValue);
            tb159.VL_HORA_PLANT_COLAB = colP.VL_HORA_PLANT.HasValue ? colP.VL_HORA_PLANT.Value : (decimal?)null;

            TB159_AGENDA_PLANT_COLABOR.SaveOrUpdate(tb159, true);

            //AuxiliPagina.EnvioAvisoGeralSistema(this, "Agendamento de Plantão Realizado com Sucesso!");
            AuxiliPagina.RedirecionaParaPaginaSucesso("Agendamento de Plantão Realizado com Sucesso!", HttpContext.Current.Request.Url.AbsoluteUri.ToLower());
        }

        #region Classes de saída

        public class HorarioSaida
        {
            public int idPlant { get; set; }
            public string dt { get; set; }
            public int cHo { get; set; }
            public string ch
            {
                get
                {
                    return this.cHo.ToString().PadLeft(2,'0');
                }
            }
            public string hrIni { get; set; }
            public string hrFim
            {
                get
                {
                    int h = int.Parse(this.hrIni.Substring(0, 2));
                    int m = this.hrIni.Length < 5 ? int.Parse(this.hrIni.Substring(2, 2)) : int.Parse(this.hrIni.Substring(3, 2));
                    int th = 24;

                    h = h + cHo;

                    if (h >= th)
                    {
                        h = h - th;
                    }

                    return h.ToString().PadLeft(2, '0') + ":" + m.ToString().PadLeft(2, '0');
                }
            }
            public string hr
            {
                get
                {
                    return this.hrIni + " - " + this.hrFim;
                }
            }
        }

        public class GrdProfiSaida
        {
            public int? CO_ESPEC { get; set; }
            public int? CO_DEPT { get; set; }
            public string NU_CPF { get; set; }
            public int CO_COL { get; set; }
            public int CO_EMP { get; set; }
            public string MATR_COL { get; set; }
            public string NOME
            {
                get
                {
                    string maCol = this.MATR_COL.PadLeft(6, '0').Insert(2, ".").Insert(6, "-");
                    string noCol = (this.NO_COL_RECEB.Length > 27 ? this.NO_COL_RECEB.Substring(0, 27) + "..." : this.NO_COL_RECEB);
                    return maCol + " - " + noCol;
                }
            }
            public string NO_COL_RECEB { get; set; }
            public string NO_EMP { get; set; }
            public string DE_ESP { get; set; }
            public decimal? CHPLANT { get; set; }
            public decimal? TMPDESCA { get; set; }
        }

        public class SaidaTipoPlant
        {
            public int idTipo { get; set; }
            public string sigla { get; set; }
            public int qtHoras { get; set; }
            public string hrIni { get; set; }
            public string saida
            {
                get
                {
                    return "Tipo: " + this.sigla + " - CH: " + this.qtHoras.ToString().PadLeft(2, '0') + "h - Início: " + this.hrIni;
                }
            }
        }

        public class SaidaAgenda
        {
            public string coSitua { get; set; }

            public DateTime dtIniPrev { get; set; }
            public DateTime dtFimPrev { get; set; }
            public DateTime? dtIniReal { get; set; }
            public DateTime? dtFimReal { get; set; }

            public string flIncon { get; set; }
            public bool Icon
            {
                get
                {
                    return this.flIncon == "S" ? true : false;
                }
            }

            public string hrIni
            {
                get
                {
                    string h = "";

                    switch (this.coSitua)
                    {
                        case "R":
                            h = (this.dtIniReal.HasValue ? this.dtIniReal.Value.ToString("HH:mm") : this.dtIniPrev.ToString("HH:mm"));
                            break;
                        default:
                            h = this.dtIniPrev.ToString("HH:mm");
                            break;
                    }

                    return h;
                }
            }

            public string hrFim
            {
                get
                {
                    string h = "";

                    switch (this.coSitua)
                    {
                        case "R":
                            h = (this.dtFimReal.HasValue ? this.dtFimReal.Value.ToString("HH:mm") : this.dtFimPrev.ToString("HH:mm"));
                            break;
                        default:
                            h = this.dtFimPrev.ToString("HH:mm");
                            break;
                    }

                    return h;
                }
            }


            public string data
            {
                get
                {
                    string d = "";

                    switch (this.coSitua)
                    {
                        case "R":
                            d = (this.dtIniReal.HasValue ? this.dtIniReal.Value.ToString("dd/MM/yy") : this.dtIniPrev.ToString("dd/MM/yy"));
                            break;
                        default:
                            d = this.dtIniPrev.ToString("dd/MM/yy");
                            break;
                    }

                    return d;
                }
            }

            public string coSiglaEmp { get; set; }

            public string coSiglaDepte { get; set; }

            public string coSiglaEspec { get; set; }

            public string deSitua
            {
                get
                {
                    string s = "";

                    switch (this.coSitua)
                    {
                        case "A":
                            s = "ABE";
                            break;
                        case "C":
                            s = "CAN";
                            break;
                        case "R":
                            s = "REA";
                            break;
                        case "P":
                            s = "PLA";
                            break;
                        case "S":
                            s = "SUS";
                            break;
                    }

                    return s;
                }
            }
        }

        #endregion

        #region Carregamentos

        /// <summary>
        /// Carrega as especialidades
        /// </summary>
        /// <param name="ddl"></param>
        private void CarregaEspecialidade(DropDownList ddl)
        {
            if (ddl == ddlEspMedResCons)
            {
                //Trazer apenas as especialidades que possuem algum colaborador associado
                var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                           join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tb03.CO_ESPEC equals tb63.CO_ESPECIALIDADE
                           where tb03.FL_PERM_PLANT == "S"
                           select new { tb63.CO_ESPECIALIDADE, tb63.NO_ESPECIALIDADE }).OrderBy(w => w.NO_ESPECIALIDADE).ToList().Distinct();

                ddl.DataTextField = "NO_ESPECIALIDADE";
                ddl.DataValueField = "CO_ESPECIALIDADE";
                ddl.DataSource = res;
                ddl.DataBind();

                if(res.Count() > 0)
                    ddl.Items.Insert(0, new ListItem("Todos", "0"));
                else
                    ddl.Items.Insert(0, new ListItem("Sem Especialidades com Plantonistas", ""));
            }
            else
                AuxiliCarregamentos.CarregaEspeciacialidades(ddl, LoginAuxili.CO_EMP, null, false);
        }

        /// <summary>
        /// Carrega os tipos de plantões
        /// </summary>
        private void CarregaTipoPlantao()
        {
            int coEmp = ddlUnidPlant.SelectedValue != "" ? int.Parse(ddlUnidPlant.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaTiposPlantoes(ddlTipoPlant, coEmp, false);
        }

        /// <summary>
        /// Carrega as Unidades
        /// </summary>
        /// <param name="ddl"></param>
        private void CarregaUnidades(DropDownList ddl)
        {
            if (ddl == ddlUnidResCons)
            {
                //Trazer apenas as especialidades que possuem algum colaborador associado
                var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                           join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb03.TB25_EMPRESA.CO_EMP equals tb25.CO_EMP
                           where tb03.FL_PERM_PLANT == "S"
                           select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).OrderBy(w => w.NO_FANTAS_EMP).ToList().Distinct();

                ddl.DataTextField = "NO_FANTAS_EMP";
                ddl.DataValueField = "CO_EMP";
                ddl.DataSource = res;
                ddl.DataBind();

                if (res.Count() > 0)
                    ddl.Items.Insert(0, new ListItem("Todos", "0"));
                else
                    ddl.Items.Insert(0, new ListItem("Sem Unidades com Plantonistas", ""));
            }
            else
                AuxiliCarregamentos.CarregaUnidade(ddl, LoginAuxili.ORG_CODIGO_ORGAO, false);
        }

        /// <summary>
        /// Carrega os departamentos de uma determinada empresa
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="ddlUnid"></param>
        private void CarregaDepartamento(DropDownList ddl, DropDownList ddlUnid)
        {
            int coEmp = ddlUnidPlant.SelectedValue != "" ? int.Parse(ddlUnidPlant.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaDepartamentos(ddlLocalPlant, coEmp, false);
        }

        /// <summary>
        /// Carrega a grid de horarios do profissional de saúde recebido como parâmetro
        /// </summary>
        /// <param name="coCol"></param>
        private void CarregaGridHorario(int coCol, bool ComPeriodo)
        {
            if (ComPeriodo == true)
            {
                DateTime? dtIni = txtDtIniResCons.Text != "" ? DateTime.Parse(txtDtIniResCons.Text) : (DateTime?)null;
                DateTime? dtFim = txtDtFimResCons.Text != "" ? DateTime.Parse(txtDtFimResCons.Text) : (DateTime?)null;
                string situ = ddlSituacao.SelectedValue;

                var res = (from tb159 in TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros()
                           join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb159.CO_EMP_AGEND_PLANT equals tb25.CO_EMP
                           join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tb159.CO_ESPEC_PLANT equals tb63.CO_ESPECIALIDADE
                           where tb159.TB03_COLABOR.CO_COL == coCol
                           && ((EntityFunctions.TruncateTime(tb159.DT_INICIO_PREV) >= (EntityFunctions.TruncateTime(dtIni))) && (EntityFunctions.TruncateTime(tb159.DT_INICIO_PREV) <= EntityFunctions.TruncateTime(dtFim)))
                           && (situ != "0" ? tb159.CO_SITUA_AGEND == situ : 0 == 0)
                           select new SaidaAgenda
                           {
                               coSitua = tb159.CO_SITUA_AGEND,
                               dtIniPrev = tb159.DT_INICIO_PREV,
                               dtFimPrev = tb159.DT_TERMIN_PREV,
                               coSiglaEmp = tb25.sigla,
                               coSiglaDepte = tb159.TB14_DEPTO.CO_SIGLA_DEPTO,
                               coSiglaEspec = tb63.NO_SIGLA_ESPECIALIDADE,
                               dtIniReal = tb159.DT_INICIO_REAL.Value,
                               dtFimReal = tb159.DT_TERMIN_REAL.Value,
                               flIncon = tb159.FL_INCON_AGEND
                           }).OrderByDescending(w => w.dtIniPrev).ToList();

                grdHorario.DataSource = res;
                grdHorario.DataBind();
            }
            //else
            //{
            //    var res = (from tb159 in TB159_AGENDA_PLANT_COLABOR.RetornaTodosRegistros()
            //               join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb159.CO_EMP_AGEND_PLANT equals tb25.CO_EMP
            //               join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tb159.CO_ESPEC_PLANT equals tb63.CO_ESPECIALIDADE
            //               where tb159.TB03_COLABOR.CO_COL == coCol
            //               select new SaidaAgenda
            //               {
            //                   coSitua = tb159.CO_SITUA_AGEND,
            //                   dtIniPrev = tb159.DT_INICIO_PREV,
            //                   dtFimPrev = tb159.DT_TERMIN_PREV,
            //                   coSiglaEmp = tb25.sigla,
            //                   coSiglaDepte = tb159.TB14_DEPTO.CO_SIGLA_DEPTO,
            //                   coSiglaEspec = tb63.NO_SIGLA_ESPECIALIDADE,
            //                   dtIniReal = tb159.DT_INICIO_REAL.Value,
            //                   dtFimReal = tb159.DT_TERMIN_REAL.Value,
            //                   flIncon = tb159.FL_INCON_AGEND
            //               }).OrderByDescending(w => w.dtIniPrev).ToList();

            //    grdHorario.DataSource = res;
            //    grdHorario.DataBind();
            //}
        }

        /// <summary>
        /// Carrega os profissionais na grid de acordo com a especialidade e unidade selecionadas na funcionalidade.
        /// </summary>
        private void CarregaGridProfi()
        {
            int coEsp = (ddlEspMedResCons.SelectedValue != "" ? int.Parse(ddlEspMedResCons.SelectedValue) : 0);
            int coEmp = (ddlUnidResCons.SelectedValue != "" ? int.Parse(ddlUnidResCons.SelectedValue) : 0);

            var res = (from tb03 in TB03_COLABOR.RetornaTodosRegistros()
                       join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tb03.CO_EMP equals tb25.CO_EMP
                       join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tb03.CO_ESPEC equals tb63.CO_ESPECIALIDADE
                       where tb03.FL_PERM_PLANT == "S"
                       && (coEsp != 0 ? tb03.CO_ESPEC == coEsp : coEsp == 0)
                       && (coEmp != 0 ? tb03.CO_EMP == coEmp : coEmp == 0)
                       select new GrdProfiSaida
                       {
                           NU_CPF = tb03.NU_CPF_COL,
                           CO_COL = tb03.CO_COL,
                           CO_EMP = tb03.CO_EMP,
                           NO_COL_RECEB = tb03.NO_COL,
                           MATR_COL = tb03.CO_MAT_COL,
                           NO_EMP = tb25.sigla,
                           DE_ESP = tb63.NO_SIGLA_ESPECIALIDADE,
                           CHPLANT = tb03.QT_HORAS_PLANT,
                           TMPDESCA = tb03.QT_HORAS_DESCA,
                           CO_ESPEC = tb03.CO_ESPEC,
                           CO_DEPT = tb03.CO_DEPTO,
                       }).OrderBy(w => w.NO_COL_RECEB).ToList();

            grdProfi.DataSource = res;
            grdProfi.DataBind();
        }


        /// <summary>
        /// Limpa a Grid de horários da agenda de plantões
        /// </summary>
        private void LimpaGridAgenda()
        {
            grdHorario.DataSource = null;
            grdHorario.DataBind();
        }

        /// <summary>
        /// Método responsável por carregar a grid de horários do plantonista após o usuário altera a data de início ou término na pesquisa
        /// </summary>
        private void LoadGrdHrPosAlterData()
        {
            string coCol = "";
            foreach (GridViewRow li in grdProfi.Rows)
            {
                if ((((CheckBox)li.Cells[0].FindControl("ckSelect")).Checked))
                    coCol = (((HiddenField)li.Cells[0].FindControl("hidCol")).Value);
            }

            if (coCol == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecionar um colaborador para pesquisar");
                return;
            }

            if (txtDtIniResCons.Text == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar a Data de Início para pesquisar");
                return;
            }

            if (txtDtFimResCons.Text == "")
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor informar a Data de Início para pesquisar");
                return;
            }


            //Recarrega a grid com as informações de plantão do colaborador selecionado dentro da nova data.
            int coColI = coCol != "" ? int.Parse(coCol) : 0;
            CarregaGridHorario(coColI, true);
        }

        #endregion

        #region Métodos de campo

        protected void btnPesqPlant_Click(object sender, ImageClickEventArgs e)
        {
            //string dtIni = txtDtIniResCons.Text != null ? txtDtIniResCons.Text : DateTime.Now.ToString("dd/MM/yyyy");
            //string dtFim = txtDtFimResCons.Text;
            //int coCol = int.Parse(hidCoCol.Value);

            //if (dtFim == "")
            //{
            //    CarregaGridHorario(coCol, dtIni);
            //}
            //else
            //{
            //    CarregaGridHorario(coCol, dtIni, dtFim);
            //}
        }

        protected void ddlUnidRealPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CarregaDepartamento(ddlLocalPlant, ddlUnidPlant);
        }

        protected void ckSelect_CheckedChange(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            CheckBox chk;
            int coCol = 0;
            int coEmp = 0;

            // Valida se a grid de atividades possui algum registro
            if (grdProfi.Rows.Count != 0)
            {
                // Passa por todos os registros da grid de atividades
                foreach (GridViewRow linha in grdProfi.Rows)
                {
                    chk = (CheckBox)linha.Cells[0].FindControl("ckSelect");

                    // Desmarca todos os registros menos o que foi clicado
                    if (chk.ClientID != atual.ClientID)
                    {
                        chk.Checked = false;
                    }
                    else
                    {
                        if (chk.Checked)
                        {
                            coCol = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidCol")).Value);
                            coEmp = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidEmp")).Value);
                            hidCoCol.Value = coCol.ToString();
                            hidCoEmpCol.Value = coEmp.ToString();

                            string coEs = (((HiddenField)linha.Cells[0].FindControl("hidcoEspec")).Value);
                            string coDe = (((HiddenField)linha.Cells[0].FindControl("hidcoDept")).Value);

                            //Seleciona apenas se os campos estiverem habilitados, ou seja, estiverem disponível para edição
                            if(ddlEspecPlant.Enabled == true)
                            {
                                ddlEspecPlant.SelectedValue = coEs != "" ? coEs : "";
                                HttpContext.Current.Session.Add("coesp", coEs);
                            }

                            //Atribui o valor do departamento ao campo dropdown e à um hidden para que esse valor possa ser recuperado posteriormente
                            if (ddlEspecPlant.Enabled == true)
                            {
                                ddlLocalPlant.SelectedValue = hidCoDepCol.Value = coDe != "" ? coDe : "";
                                HttpContext.Current.Session.Add("code", coDe);
                            }
                        }
                        else
                        {
                            hidCoCol.Value = "0";
                            hidCoEmpCol.Value = "0";
                        }
                    }
                }
            }

            CarregaGridHorario(coCol, true);
            updAgend.Update();
        }

        protected void ddlEspMedResCons_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGridProfi();
            LimpaGridAgenda();
        }

        protected void ddlUnidResCons_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGridProfi();
            LimpaGridAgenda();
        }

        protected void imgCpfResp_OnClick(object sender, EventArgs e)
        {
            LoadGrdHrPosAlterData();
        }

        protected void ddlTipoPlant_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlTipoPlant.SelectedValue != "")
            {
                TB153_TIPO_PLANT tb153 = TB153_TIPO_PLANT.RetornaPelaChavePrimaria(int.Parse(ddlTipoPlant.SelectedValue));
                string coesp = (string)(Session["coesp"]);
                string codep = (string)(Session["code"]);

                //Caso o plantão selecionado tenha especificado a especialidade e o departamento, eles são selecionados de acordo e desabilitados
                if (tb153.CO_ESPEC.HasValue)
                {
                    ListItem li1 = ddlEspecPlant.Items.FindByValue(tb153.CO_ESPEC.Value.ToString());
                    if (li1 != null)
                    {
                        ddlEspecPlant.SelectedValue = tb153.CO_ESPEC.Value.ToString();
                        ddlEspecPlant.Enabled = false;
                    }
                    else
                    {
                        ddlEspecPlant.Enabled = true;
                        ddlEspecPlant.SelectedValue = (!string.IsNullOrEmpty(coesp) ? coesp : "");
                    }
                }
                else
                {
                    ddlEspecPlant.Enabled = true;
                    ddlEspecPlant.SelectedValue = (!string.IsNullOrEmpty(coesp) ? coesp : "");
                }

                //Caso o plantão selecionado tenha especificado a especialidade e o departamento, eles são selecionados de acordo e desabilitados
                tb153.TB14_DEPTOReference.Load();
                if (tb153.TB14_DEPTO != null)
                {
                    ListItem li2 = ddlLocalPlant.Items.FindByValue(tb153.TB14_DEPTO.CO_DEPTO.ToString());
                    if (li2 != null)
                    {
                        ddlLocalPlant.SelectedValue = tb153.TB14_DEPTO.CO_DEPTO.ToString();
                        ddlLocalPlant.Enabled = false;
                    }
                    else
                    {
                        ddlLocalPlant.Enabled = true;
                        ddlLocalPlant.SelectedValue = (!string.IsNullOrEmpty(codep) ? codep : "");
                    }
                }
                else
                {
                    ddlLocalPlant.Enabled = true;
                    ddlLocalPlant.SelectedValue = (!string.IsNullOrEmpty(codep) ? codep : "");
                }
            }
        }
        
        protected void ddlUnidPlant_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaTipoPlantao();
        }

        #endregion
    }
} 