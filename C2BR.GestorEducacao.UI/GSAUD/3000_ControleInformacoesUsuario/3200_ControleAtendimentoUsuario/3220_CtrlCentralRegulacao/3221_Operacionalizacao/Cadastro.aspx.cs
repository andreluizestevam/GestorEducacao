//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CADASTRO E INFORMAÇÕES DE ALUNOS
// OBJETIVO: Registro de Atendimento do Usuário
// DATA DE CRIAÇÃO: 08/03/2014
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//25/08/2014| Maxwell Almeida            | Criação da funcionalidade de Operacionalização da Central de Regulação

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Data;
using System.Data.Objects;
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
using C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3400_CtrlAtendimentoUsuario;

namespace C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3220_CtrlCentralRegulacao._3221_Operacionalizacao
{
    public partial class Cadastro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtIniPeri.Text = DateTime.Now.AddDays(-2).ToString();

                carregaUnidades();
                CarregaPacientes();
                CarregaProfissionais();
                CarregaBolsas();
                CarregaBairros();
                CarregaCidades();
                AuxiliCarregamentos.CarregaUFs(ddlUfRgAluMod, false, null, false);
                ddlUfRgAluMod.Items.Insert(0, new ListItem("", ""));
                AuxiliCarregamentos.CarregaUFs(ddlUfAluMod, false, null, false);
                ddlUfAluMod.Items.Insert(0, new ListItem("", ""));

                //------------> Tamanho da imagem de paciente
                upImagemAluno.ImagemLargura = 70;
                upImagemAluno.ImagemAltura = 85;

                CarregaGridAtendimentosPendentes(ddlOrdeAtendPend.SelectedValue);
            }
        }

        #region Carregamentos

        /// <summary>
        /// Carrega na grid todos os atendimentos que possuem algum encaminhamento pendente
        /// </summary>
        private void CarregaGridAtendimentosPendentes(string ord = "")
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int coPac = ddlPaciente.SelectedValue != "" ? int.Parse(ddlPaciente.SelectedValue) : 0;
            int coPro = ddlProfissional.SelectedValue != "" ? int.Parse(ddlProfissional.SelectedValue) : 0;
            DateTime dtIni = (!string.IsNullOrEmpty(txtIniPeri.Text) ? DateTime.Parse(txtIniPeri.Text) : DateTime.Now);
            DateTime? dtFim = (!string.IsNullOrEmpty(txtFimPeri.Text) ? DateTime.Parse(txtFimPeri.Text) : (DateTime?)null);
            string tpSitu = ddlTipo.SelectedValue;
            var res = (from tbs347 in TBS347_CENTR_REGUL.RetornaTodosRegistros()
                       join tbs219 in TBS219_ATEND_MEDIC.RetornaTodosRegistros() on tbs347.TBS219_ATEND_MEDIC.ID_ATEND_MEDIC equals tbs219.ID_ATEND_MEDIC
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs219.CO_ALU equals tb07.CO_ALU
                       join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs219.CO_EMP equals tb25.CO_EMP
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs219.CO_COL equals tb03.CO_COL
                       join tbs350 in TBS350_ITEM_CENTR_REGUL.RetornaTodosRegistros() on tbs347.ID_CENTR_REGUL equals tbs350.TBS347_CENTR_REGUL.ID_CENTR_REGUL
                       where 
                       (tpSitu != "0" ? tbs350.FL_APROV_ENCAM == tpSitu : 0 == 0)
                       && (coPac != 0 ? tbs219.CO_ALU == coPac : 0 == 0)
                       && (coPro != 0 ? tbs219.CO_COL == coPro : 0 == 0)
                       && (EntityFunctions.TruncateTime(tbs347.DT_SOLIC_ENCAM) >= EntityFunctions.TruncateTime(dtIni))
                       && (dtFim.HasValue ? EntityFunctions.TruncateTime(tbs347.DT_SOLIC_ENCAM) <= EntityFunctions.TruncateTime(dtFim.Value) : 0 == 0)
                       select new AtendimentosPendentes
                       {
                           NO_PAC = tb07.NO_ALU,
                           NIS_PAC = tb07.NU_NIS,
                           CO_ALU = tb07.CO_ALU,
                           CO_SEXO = tb07.CO_SEXO_ALU,
                           dt_nascimento = tb07.DT_NASC_ALU,
                           UNID = tb25.sigla,
                           dataAtendMed = tbs219.DT_ATEND_CADAS,
                           CO_ATEND = tbs219.CO_ATEND_MEDIC,
                           ID_ATEND_MEDIC = tbs347.TBS219_ATEND_MEDIC.ID_ATEND_MEDIC,
                           ID_CENTR_REGUL = tbs347.ID_CENTR_REGUL,
                           FL_USO = tbs347.FL_USO,

                           //Medico
                           NO_COL_RECEB = tb03.NO_COL,
                           CO_MATRIC_COL = tb03.CO_MAT_COL,
                           SIGLA_ENT = tb03.CO_SIGLA_ENTID_PROFI,
                           NUMER_ENT = tb03.NU_ENTID_PROFI,
                           UF_ENT = tb03.CO_UF_ENTID_PROFI,
                           DT_ENT = tb03.DT_EMISS_ENTID_PROFI,

                       }).DistinctBy(w => w.ID_ATEND_MEDIC).OrderByDescending(w => w.dataAtendMed).ToList();

            //Ordena de acordo com parâmetro
            switch (ord)
            {
                case "P":
                    res = res.OrderBy(w => w.NO_PAC).OrderBy(w => w.CO_TIPO_RISCO).ToList();
                    break;

                case "U":
                    res = res.OrderBy(w => w.UNID).OrderBy(w => w.CO_TIPO_RISCO).ToList();
                    break;

                default:
                    res = res.OrderBy(w => w.CO_TIPO_RISCO).ToList();
                    break;
            }

            grdAtendimPendentes.DataSource = res;
            grdAtendimPendentes.DataBind();
        }

        /// <summary>
        /// Método responsável por carregar a grid de detalhe de pendências
        /// </summary>
        private void CarregaGridItensPendentes(int ID_CENTR_REGUL, string co_sigla, string ord = "")
        {
            var res = (from tbs350 in TBS350_ITEM_CENTR_REGUL.RetornaTodosRegistros()
                       join tbs347 in TBS347_CENTR_REGUL.RetornaTodosRegistros() on tbs350.TBS347_CENTR_REGUL.ID_CENTR_REGUL equals tbs347.ID_CENTR_REGUL
                       where tbs350.TBS347_CENTR_REGUL.ID_CENTR_REGUL == ID_CENTR_REGUL
                       && (co_sigla != "0" ? tbs350.CO_SIGLA_ITEM_ENCAM == co_sigla : 0 == 0)
                       select new DetalhePendencia
                       {
                           DT_ALTER = tbs350.DT_ALTER_ENCAM ?? tbs350.DT_SOLIC_ENCAM,
                           CO_SIGLA = tbs350.CO_SIGLA_ITEM_ENCAM,
                           ID_ATEND_MEDIC = tbs347.TBS219_ATEND_MEDIC.ID_ATEND_MEDIC,
                           ID_ITEM = tbs350.ID_ITEM_ENCAM,
                           ID_CENTR_REGUL = tbs350.TBS347_CENTR_REGUL.ID_CENTR_REGUL,
                           NO_ITEM = tbs350.NO_ITEM,
                           CO_PROTOCOLO = tbs350.NU_REGIS_ITEM,
                           ID_ITEM_CENTR_REGUL = tbs350.ID_ITEM_CENTR_REGUL,
                           NU_APROV = tbs350.NU_APROV,
                           NU_GUIA = tbs350.NU_GUIA,
                           DE_OBS = tbs350.DE_OBSER,
                       }).OrderByDescending(W => W.DT_ALTER).ThenByDescending(w => w.DT_ALTER).ToList();

            //Ordena de acordo com parâmetro
            switch (ord)
            {
                case "R":
                    res = res.OrderBy(w => w.CO_PROTOCOLO).ToList();
                    break;

                case "U":
                    res = res.OrderBy(w => w.CO_CAD_ITEM).ToList();
                    break;

                default:
                    res = res.OrderBy(w => w.NO_ITEM).ToList();
                    break;
            }

            grdDetalhePendencia.DataSource = res;
            grdDetalhePendencia.DataBind();

            liItensPendentes.Visible = true;
        }

        /// <summary>
        /// Carrega todas as ocorrências na grid de acordo com o id recebido
        /// </summary>
        /// <param name="ID_ITEM_CENTR_REGUL"></param>
        private void CarregaHistoricoOcorrencias(int ID_ITEM_CENTR_REGUL)
        {
            var res = (from tbs349 in TBS349_CENTR_REGUL_OCORR.RetornaTodosRegistros()
                       join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbs349.CO_COL_REGIS_OCORR equals tb03.CO_COL
                       where tbs349.TBS350_ITEM_CENTR_REGUL.ID_ITEM_CENTR_REGUL == ID_ITEM_CENTR_REGUL
                       select new HistoOcorrencias
                       {
                           DT_OCORR_RECEB = tbs349.DT_REGIS_OCORR,
                           NO_COL = tb03.NO_COL,
                           DE_OCORR = tbs349.DE_REGIS_OCORR_ITEM,
                       }).OrderByDescending(w => w.DT_OCORR_RECEB).ToList();

            grdHistOcorrencias.DataSource = res;
            grdHistOcorrencias.DataBind();
        }

        public class AtendimentosPendentes
        {
            //Dados do(a) Paciente
            public string NO_PAC { get; set; }
            public decimal? NIS_PAC { get; set; }
            public int CO_ALU { get; set; }
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
            public string NO_PAC_V
            {
                get
                {
                    //Coleta apenas o primeiro nome
                    string nomeV = this.NO_PAC.Length > 18 ? this.NO_PAC.Substring(0, 18) + "..." : this.NO_PAC;
                    return (this.NIS_PAC.HasValue ? this.NIS_PAC.ToString().PadLeft(7, '0') + " " + nomeV : nomeV);
                }
            }
            public int ID_CENTR_REGUL { get; set; }
            public string FL_USO { get; set; }

            //Dados Gerais
            public string UNID { get; set; }
            public int ID_ATEND_MEDIC { get; set; }
            public int CO_TIPO_RISCO
            {
                get
                {
                    var res = (from tb219 in TBS219_ATEND_MEDIC.RetornaTodosRegistros()
                               where tb219.ID_ATEND_MEDIC == this.ID_ATEND_MEDIC
                               select tb219).FirstOrDefault();

                    res.TBS194_PRE_ATENDReference.Load();
                    return res.TBS194_PRE_ATEND.CO_TIPO_RISCO;
                }
            }
            public string CLASS_RISCO
            {
                get
                {
                    string s = "";
                    switch (this.CO_TIPO_RISCO)
                    {
                        case 1:
                            s = "EMERGÊNCIA";
                            break;

                        case 2:
                            s = "MUITO URGENTE";
                            break;

                        case 3:
                            s = "URGENTE";
                            break;

                        case 4:
                            s = "POUCO URGENTE";
                            break;

                        case 5:
                            s = "NÃO URGENTE";
                            break;
                    }

                    return s;
                }
            }
            public string NO_OPERA { get; set; }

            //Trata data e hora do Atendimento
            public DateTime? dataAtendMed { get; set; }
            public string dataEMValid
            {
                get
                {
                    return this.dataAtendMed.Value.ToString("dd/MM/yy");
                }
            }
            public string horaEMValid
            {
                get
                {
                    return this.dataAtendMed.Value.ToString("HH:mm");
                }
            }
            public string DT_ATEND
            {
                get
                {
                    return this.dataEMValid + " " + this.horaEMValid;
                }
            }

            //Trata as cores de acordo com a classificação de risco
            public bool DIV_1
            {
                get
                {
                    return (this.CO_TIPO_RISCO == 1 ? true : false);
                }
            }
            public bool DIV_2
            {
                get
                {
                    return (this.CO_TIPO_RISCO == 2 ? true : false);
                }
            }
            public bool DIV_3
            {
                get
                {
                    return (this.CO_TIPO_RISCO == 3 ? true : false);
                }
            }
            public bool DIV_4
            {
                get
                {
                    return (this.CO_TIPO_RISCO == 4 ? true : false);
                }
            }
            public bool DIV_5
            {
                get
                {
                    return (this.CO_TIPO_RISCO == 5 ? true : false);
                }
            }

            public string CO_ATEND { get; set; }
            public string CO_ATEND_V
            {
                get
                {
                    return this.CO_ATEND.Insert(2, ".").Insert(6, ".").Insert(9, ".");
                }
            }

            //Dados do Médico(a)
            public string SIGLA_ENT { get; set; }
            public string NUMER_ENT { get; set; }
            public string UF_ENT { get; set; }
            public DateTime? DT_ENT { get; set; }
            public string ENT_CONCAT
            {
                get
                {
                    return (!string.IsNullOrEmpty(this.SIGLA_ENT) ? this.SIGLA_ENT + " " + this.NUMER_ENT + " - " + this.UF_ENT : "");
                }
            }
            public string CO_MATRIC_COL { get; set; }
            public string NO_COL_RECEB { get; set; }
            public string NO_COL
            {
                get
                {
                    //Trata o tamanho e apresentação do nome do médico
                    string nomeCol = (this.NO_COL_RECEB.Length > 13 ? this.NO_COL_RECEB.Substring(0, 13) + "..." : this.NO_COL_RECEB);
                    
                    //Concatena a matrícula e o nome do colaborador responsável pelo diagnóstico
                    return (!string.IsNullOrEmpty(this.ENT_CONCAT) ? this.ENT_CONCAT + " - " + nomeCol : (!string.IsNullOrEmpty(this.CO_MATRIC_COL) ? "MAT " + this.CO_MATRIC_COL.Insert(5, "-").Insert(2, ".") + " - " + nomeCol : nomeCol));
                }
            }

            //Tratacomo mostrar os status

            //Exames
            public bool SW_NAO_PRETO_EX
            {
                get
                {
                    //Verifica se existe qualquer exame do atendimento selecionado em aberto. Caso não exista nenhum retorna TRUE para mostrar NÃO em "preto", caso exista algum, retorna FALSE para ocultar o "NÃO".
                    return (!TBS350_ITEM_CENTR_REGUL.RetornaTodosRegistros().Where(w => w.TBS347_CENTR_REGUL.ID_CENTR_REGUL == this.ID_CENTR_REGUL && w.CO_SIGLA_ITEM_ENCAM == "EX").Any());
                }
            }
            public bool SW_SIM_PRETO_EX
            {
                get
                {
                    //Faz uma lista de todos os itens de exame para o atendimento em questão na central de regulação
                    var res = (from tbs350 in TBS350_ITEM_CENTR_REGUL.RetornaTodosRegistros()
                               where tbs350.TBS347_CENTR_REGUL.ID_CENTR_REGUL == this.ID_CENTR_REGUL && tbs350.CO_SIGLA_ITEM_ENCAM == "EX"
                               select new { tbs350.ID_ITEM_ENCAM, tbs350.FL_APROV_ENCAM }).Distinct().ToList();

                    int a = 0;
                    foreach (var li in res)
                    {

                        //Conta, dos lançamentos mais recentes de cada exame, quantos existem de cada tipo.
                        switch (li.FL_APROV_ENCAM)
                        {
                            case "A":
                                a++;
                                break;
                        }
                    }

                    //Testa, se está para mostrar NÃO em preto, a conta feita neste bloco de código não será válida
                    if (SW_NAO_PRETO_EX)
                        return false;
                    //Caso todos os itens que existem estejam em análise, mostra o SIM preto.
                    else
                        return (a == res.Count ? true : false);
                }
            }
            public bool SW_SIM_AZUL_EX
            {
                get
                {
                    //Faz uma lista de todos os itens de exame para o atendimento em questão na central de regulação
                    var res = (from tbs350 in TBS350_ITEM_CENTR_REGUL.RetornaTodosRegistros()
                               where tbs350.TBS347_CENTR_REGUL.ID_CENTR_REGUL == this.ID_CENTR_REGUL && tbs350.CO_SIGLA_ITEM_ENCAM == "EX"
                               select new { tbs350.ID_ITEM_ENCAM, tbs350.FL_APROV_ENCAM }).Distinct().ToList();

                    int a = 0;
                    foreach (var li in res)
                    {

                        //Conta, dos lançamentos mais recentes de cada exame, quantos existem de cada tipo.
                        switch (li.FL_APROV_ENCAM)
                        {
                            case "A":
                                a++;
                                break;
                        }
                    }

                    //Testa, se está para mostrar NÃO em preto, a conta feita neste bloco de código não será válida
                    if (SW_NAO_PRETO_EX)
                        return false;
                    //Caso todos os itens que existem tenham sido Analisados(seja autorizado ou não), mostra o SIM Azul.
                    else
                        return (a == 0 ? true : false);
                }
            }
            public bool SW_SIM_VERMELHO_EX
            {
                get
                {
                    //Faz uma lista de todos os itens de exame para o atendimento em questão na central de regulação
                    var res = (from tbs350 in TBS350_ITEM_CENTR_REGUL.RetornaTodosRegistros()
                               where tbs350.TBS347_CENTR_REGUL.ID_CENTR_REGUL == this.ID_CENTR_REGUL && tbs350.CO_SIGLA_ITEM_ENCAM == "EX"
                               select new { tbs350.ID_ITEM_ENCAM, tbs350.FL_APROV_ENCAM }).Distinct().ToList();

                    int a = 0;
                    foreach (var li in res)
                    {

                        //Conta, dos lançamentos mais recentes de cada exame, quantos existem de cada tipo.
                        switch (li.FL_APROV_ENCAM)
                        {
                            case "A":
                                a++;
                                break;
                        }
                    }

                    //Caso alguns itens(não todos) ainda estejam pendentes de aprovação, mostra SIM vermelho
                    return (a > 0 && a != res.Count ? true : false);
                }
            }

            //Serviços Ambulatoriais
            public bool SW_NAO_PRETO_SA
            {
                get
                {
                    //Verifica se existe qualquer exame do atendimento selecionado em aberto. Caso não exista nenhum retorna TRUE para mostrar NÃO em "preto", caso exista algum, retorna FALSE para ocultar o "NÃO".
                    return (!TBS350_ITEM_CENTR_REGUL.RetornaTodosRegistros().Where(w => w.TBS347_CENTR_REGUL.ID_CENTR_REGUL == this.ID_CENTR_REGUL && w.CO_SIGLA_ITEM_ENCAM == "SA").Any());
                }
            }
            public bool SW_SIM_PRETO_SA
            {
                get
                {
                    //Faz uma lista de todos os itens de exame para o atendimento em questão na central de regulação
                    var res = (from tbs350 in TBS350_ITEM_CENTR_REGUL.RetornaTodosRegistros()
                               where tbs350.TBS347_CENTR_REGUL.ID_CENTR_REGUL == this.ID_CENTR_REGUL && tbs350.CO_SIGLA_ITEM_ENCAM == "SA"
                               select new { tbs350.ID_ITEM_ENCAM, tbs350.FL_APROV_ENCAM }).Distinct().ToList();

                    int a = 0;
                    foreach (var li in res)
                    {

                        //Conta, dos lançamentos mais recentes de cada exame, quantos existem de cada tipo.
                        switch (li.FL_APROV_ENCAM)
                        {
                            case "A":
                                a++;
                                break;
                        }
                    }

                    //Testa, se está para mostrar NÃO em preto, a conta feita neste bloco de código não será válida
                    if (SW_NAO_PRETO_SA)
                        return false;
                    //Caso todos os itens que existem estejam em análise, mostra o SIM preto.
                    else
                        return (a == res.Count ? true : false);
                }
            }
            public bool SW_SIM_AZUL_SA
            {
                get
                {
                    //Faz uma lista de todos os itens de exame para o atendimento em questão na central de regulação
                    var res = (from tbs350 in TBS350_ITEM_CENTR_REGUL.RetornaTodosRegistros()
                               where tbs350.TBS347_CENTR_REGUL.ID_CENTR_REGUL == this.ID_CENTR_REGUL && tbs350.CO_SIGLA_ITEM_ENCAM == "SA"
                               select new { tbs350.ID_ITEM_ENCAM, tbs350.FL_APROV_ENCAM }).Distinct().ToList();

                    int a = 0;
                    foreach (var li in res)
                    {

                        //Conta, dos lançamentos mais recentes de cada exame, quantos existem de cada tipo.
                        switch (li.FL_APROV_ENCAM)
                        {
                            case "A":
                                a++;
                                break;
                        }
                    }

                    //Testa, se está para mostrar NÃO em preto, a conta feita neste bloco de código não será válida
                    if (SW_NAO_PRETO_SA)
                        return false;
                    //Caso todos os itens que existem tenham sido Analisados(seja autorizado ou não), mostra o SIM Azul.
                    else
                        return (a == 0 ? true : false);
                }
            }
            public bool SW_SIM_VERMELHO_SA
            {
                get
                {
                    //Faz uma lista de todos os itens de exame para o atendimento em questão na central de regulação
                    var res = (from tbs350 in TBS350_ITEM_CENTR_REGUL.RetornaTodosRegistros()
                               where tbs350.TBS347_CENTR_REGUL.ID_CENTR_REGUL == this.ID_CENTR_REGUL && tbs350.CO_SIGLA_ITEM_ENCAM == "SA"
                               select new { tbs350.ID_ITEM_ENCAM, tbs350.FL_APROV_ENCAM }).Distinct().ToList();

                    int a = 0;
                    foreach (var li in res)
                    {

                        //Conta, dos lançamentos mais recentes de cada exame, quantos existem de cada tipo.
                        switch (li.FL_APROV_ENCAM)
                        {
                            case "A":
                                a++;
                                break;
                        }
                    }

                    //Caso alguns itens(não todos) ainda estejam pendentes de aprovação, mostra SIM vermelho
                    return (a > 0 && a != res.Count ? true : false);
                }
            }
        }

        public class DetalhePendencia
        {
            public int ID_ITEM_CENTR_REGUL { get; set; }
            public string CO_SIGLA { get; set; }
            public int ID_ATEND_MEDIC { get; set; }
            public int ID_ITEM { get; set; }
            public string CO_PROTOCOLO { get; set; }
            public string CO_CAD_ITEM
            {
                get
                {
                    switch (this.CO_SIGLA)
                    {
                        case "SA":
                            var ressa = (from tbs332 in TBS332_ATEND_SERV_AMBUL.RetornaTodosRegistros()
                                         where tbs332.ID_ATEND_SERV_AMBUL == this.ID_ITEM
                                         select tbs332).FirstOrDefault();

                            ressa.TBS356_PROC_MEDIC_PROCEReference.Load();
                            return ressa.TBS356_PROC_MEDIC_PROCE.CO_PROC_MEDI;

                        case "EX":
                            var resex = (from tbs218 in TBS218_EXAME_MEDICO.RetornaTodosRegistros()
                                         where tbs218.ID_EXAME == this.ID_ITEM
                                         select tbs218).FirstOrDefault();

                            resex.TBS356_PROC_MEDIC_PROCEReference.Load();
                            return TBS356_PROC_MEDIC_PROCE.RetornaPelaChavePrimaria(resex.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE).NM_PROC_MEDI;

                        default:
                            return " - ";
                    }
                }
            }
            public string NO_ITEM { get; set; }
            public string CO_STATUS
            {
                get
                {
                    //Retorna o status mais recente lançado para o item em questão
                    var res = (from tbs350 in TBS350_ITEM_CENTR_REGUL.RetornaTodosRegistros()
                               where tbs350.ID_ITEM_ENCAM == this.ID_ITEM
                               select new { tbs350.FL_APROV_ENCAM }).FirstOrDefault();

                    return res.FL_APROV_ENCAM;
                }
            }
            public string NO_TIPO_ITEM
            {
                get
                {
                    string s = "";
                    switch (this.CO_SIGLA)
                    {
                        case "EX":
                            s = "EXA";
                            break;
                        case "SA":
                            s = "SRA";
                            break;
                        case "RM":
                            s = "RME";
                            break;
                        default:
                            s = " - ";
                            break;
                    }
                    return s;
                }
            }
            public int ID_CENTR_REGUL { get; set; }

            public string DE_OBS { get; set; }
            //{
            //    get
            //    {
            //        return TBS348_LOG_CENTR_REGUL.RetornaTodosRegistros().Where(w => w.TBS350_ITEM_CENTR_REGUL.ID_ITEM_CENTR_REGUL == this.ID_ITEM_CENTR_REGUL).OrderByDescending(w => w.DT_ALTER_ENCAM).FirstOrDefault().DE_OBSER;
            //    }
            //}
            public string NU_APROV { get; set; }
            public string NU_GUIA { get; set; }

            public string DT_ALTER_V
            {
                get
                {
                    return this.DT_ALTER.ToString("dd/MM/yy") + " " + this.DT_ALTER.ToString("HH:mm") + " / ";
                }
            }
            public DateTime DT_ALTER { get; set; }

            //Trata o status que será apresentado
            public bool SW_NAO_VERMELHO
            {
                get
                {
                    return (this.CO_STATUS == "N" ? true : false);
                }
            }
            public bool SW_SIM_VERDE
            {
                get
                {
                    return (this.CO_STATUS == "S" ? true : false);
                }
            }
            public bool SW_ANALISE_AZUL
            {
                get
                {
                    return (this.CO_STATUS == "A" ? true : false);
                }
            }
            public bool SW_PENDENTE_ABOBORA
            {
                get
                {
                    return (this.CO_STATUS == "P" ? true : false);
                }
            }
            public bool SW_CANCELADO_PRETO
            {
                get
                {
                    return false;
                }
            }
        }

        public class HistoOcorrencias
        {
            public DateTime DT_OCORR_RECEB { get; set; }
            public string DT_REGIS
            {
                get
                {
                    return this.DT_OCORR_RECEB.ToString("dd/MM/yy") + " - " + this.DT_OCORR_RECEB.ToString("HH:mm");
                }
            }
            public string NO_COL { get; set; }
            public string DE_OCORR { get; set; }    
        }

        /// <summary>
        /// Carrega os tipos de itens existentes de acordo com o atendimento selecionado
        /// </summary>
        private void CarregaTiposItensExistentes(int ID_CENTR_REGUL)
        {
            ddlTipoItem.Items.Clear();

            bool temExame = TBS350_ITEM_CENTR_REGUL.RetornaTodosRegistros().Where(w => w.TBS347_CENTR_REGUL.ID_CENTR_REGUL == ID_CENTR_REGUL && w.CO_SIGLA_ITEM_ENCAM == "EX").Any();
            bool temSerAmbul = TBS350_ITEM_CENTR_REGUL.RetornaTodosRegistros().Where(w => w.TBS347_CENTR_REGUL.ID_CENTR_REGUL == ID_CENTR_REGUL && w.CO_SIGLA_ITEM_ENCAM == "SA").Any();

            if (temSerAmbul)
                ddlTipoItem.Items.Insert(0, new ListItem("Serviços Ambulatoriais", "SA"));

            if (temExame)
                ddlTipoItem.Items.Insert(0, new ListItem("Exame", "EX"));

            ddlTipoItem.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Carrega todos os tipos de itens possíveis para filtro dos itens pendentes
        /// </summary>
        private void CarregaTiposItensGeral()
        {
            ddlTipoItem.Items.Clear();
            ddlTipoItem.Items.Insert(0, new ListItem("Encaminhamentos Internação", "EI"));
            ddlTipoItem.Items.Insert(0, new ListItem("Encaminhamentos Médicos", "EM"));
            ddlTipoItem.Items.Insert(0, new ListItem("Reserva de Medicamentos", "RM"));
            ddlTipoItem.Items.Insert(0, new ListItem("Serviços Ambulatoriais", "SA"));
            ddlTipoItem.Items.Insert(0, new ListItem("Exame", "EX"));
            ddlTipoItem.Items.Insert(0, new ListItem("Todos", "0"));
        }

        /// <summary>
        /// Devido ao método de reload na grid de Atendimentos Pendentes, ela perde a seleção do checkbox, este método coleta 
        /// </summary>
        private void selecionaGridAtendPendentes()
        {
            CheckBox chk;
            string idCentrRegu;
            // Valida se a grid de atividades possui algum registro
            if (grdAtendimPendentes.Rows.Count != 0)
            {
                // Passa por todos os registros da grid de atividades
                foreach (GridViewRow linha in grdAtendimPendentes.Rows)
                {
                    idCentrRegu = ((HiddenField)linha.Cells[0].FindControl("hidCoCentrRegul")).Value;
                    int coate = (int)HttpContext.Current.Session["VL_Atend_OP"];

                    if (int.Parse(idCentrRegu) == coate)
                    {
                        chk = (CheckBox)linha.Cells[0].FindControl("chkSelecGridDetalhada");
                        chk.Checked = true;
                    }
                }
            }
        }

        /// <summary>
        /// Carrega as Unidades
        /// </summary>
        private void carregaUnidades()
        {
            AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
        }

        /// <summary>
        /// Carrega os Pacientes
        /// </summary>
        private void CarregaPacientes()
        {
            int unid = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaPacientes(ddlPaciente, unid, true, true);
        }

        /// <summary>
        /// Carrega todos os profissionais de saude
        /// </summary>
        private void CarregaProfissionais()
        {
            int unid = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            AuxiliCarregamentos.CarregaMedicos(ddlProfissional, unid, true, true);
        }

        /// <summary>
        /// Método que carrega o dropdown de Cidades
        /// </summary>
        private void CarregaCidades()
        {
            ddlCidadeAluMod.DataSource = TB904_CIDADE.RetornaPeloUF(ddlUfAluMod.SelectedValue);

            ddlCidadeAluMod.DataTextField = "NO_CIDADE";
            ddlCidadeAluMod.DataValueField = "CO_CIDADE";
            ddlCidadeAluMod.DataBind();

            ddlCidadeAluMod.Enabled = ddlCidadeAluMod.Items.Count > 0;
            ddlCidadeAluMod.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// Método que carrega o dropdown de Bairros
        /// </summary>
        private void CarregaBairros()
        {
            int coCidade = ddlCidadeAluMod.SelectedValue != "" ? int.Parse(ddlCidadeAluMod.SelectedValue) : 0;

            if (coCidade == 0)
            {
                ddlBairroAluMod.Enabled = false;
                ddlBairroAluMod.Items.Clear();
                return;
            }
            else
            {
                ddlBairroAluMod.DataSource = TB905_BAIRRO.RetornaPelaCidade(coCidade);

                ddlBairroAluMod.DataTextField = "NO_BAIRRO";
                ddlBairroAluMod.DataValueField = "CO_BAIRRO";
                ddlBairroAluMod.DataBind();

                ddlBairroAluMod.Enabled = ddlBairroAluMod.Items.Count > 0;
                ddlBairroAluMod.Items.Insert(0, new ListItem("", ""));
            }
        }

        /// <summary>
        /// Método que carrega o dropdown de Bolsa
        /// </summary>
        private void CarregaBolsas()
        {
            ddlBolsaAluMod.DataSource = TB148_TIPO_BOLSA.RetornaTodosRegistros().Where(c => c.TP_GRUPO_BOLSA == ddlTipoBolsaMod.SelectedValue && c.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                && c.CO_SITUA_TIPO_BOLSA == "A");

            ddlBolsaAluMod.DataTextField = "NO_TIPO_BOLSA";
            ddlBolsaAluMod.DataValueField = "CO_TIPO_BOLSA";
            ddlBolsaAluMod.DataBind();

            ddlBolsaAluMod.Items.Insert(0, new ListItem("Nenhuma", ""));

            txtValorDesctoMod.Text = txtPeriodoDeAluMod.Text = txtPeriodoAteAluMod.Text = "";
            chkDesctoPercBolsaMod.Checked = chkDesctoPercBolsaMod.Enabled =
            txtValorDesctoMod.Enabled = txtPeriodoDeAluMod.Enabled = txtPeriodoAteAluMod.Enabled = false;
        }

        /// <summary>
        /// Carrega as informações do usuário recebido como parâmetro na div de Informações Cadastrais
        /// </summary>
        /// <param name="CO_ALU"></param>
        private void CarregaInfosCadastrais(int CO_ALU)
        {
            var res = (from tb07 in TB07_ALUNO.RetornaTodosRegistros()
                       where tb07.CO_ALU == CO_ALU
                       select tb07).FirstOrDefault();

            //------------> Tamanho da imagem de paciente
            upImagemAluno.ImagemLargura = 70;
            upImagemAluno.ImagemAltura = 85;

            res.ImageReference.Load();
            res.TB905_BAIRROReference.Load();
            res.TB148_TIPO_BOLSAReference.Load();

            if (res.Image != null)
                upImagemAluno.CarregaImagem(res.Image.ImageId);
            else
                upImagemAluno.CarregaImagem(0);

            //Dados gerais do paciente
            txtNisAluMod.Text = res.NU_NIS.ToString().PadLeft(7, '0');
            txtNomeAluMod.Text = res.NO_ALU;
            ddlSexoAluMod.SelectedValue = res.CO_SEXO_ALU;
            txtRgAluMod.Text = res.CO_RG_ALU;
            txtOrgaoEmissorAluMod.Text = res.CO_ORG_RG_ALU;
            ddlUfRgAluMod.SelectedValue = res.CO_ESTA_RG_ALU;
            txtCpfAluMod.Text = res.NU_CPF_ALU;
            txtDataNascimentoAluMod.Text = (res.DT_NASC_ALU.HasValue ? res.DT_NASC_ALU.Value.ToString("dd/MM/yyyy") : "");
            ddlEtniaAluMod.SelectedValue = res.TP_RACA;
            txtEmailAluMod.Text = res.NO_WEB_ALU;

            //Endereço Residencial
            txtCepAluMod.Text = res.CO_CEP_ALU;
            txtLogradouroAluMod.Text = res.DE_ENDE_ALU;
            txtNumeroAluMod.Text = res.NU_ENDE_ALU.ToString();
            txtComplementoAluMod.Text = res.DE_COMP_ALU;
            ddlUfAluMod.SelectedValue = res.CO_ESTA_ALU;
            CarregaCidades();
            ddlCidadeAluMod.SelectedValue = res.TB905_BAIRRO != null ? res.TB905_BAIRRO.CO_CIDADE.ToString() : "";
            CarregaBairros();
            ddlBairroAluMod.SelectedValue = res.TB905_BAIRRO != null ? res.TB905_BAIRRO.CO_BAIRRO.ToString() : "";

            //Plano de Saúde/Convênio
            #region Bolsa
            if (res.TB148_TIPO_BOLSA != null)
            {
                var tb148 = (from iTb148 in TB148_TIPO_BOLSA.RetornaTodosRegistros()
                             where iTb148.CO_TIPO_BOLSA == res.TB148_TIPO_BOLSA.CO_TIPO_BOLSA
                             select new { iTb148.VL_TIPO_BOLSA, iTb148.FL_TIPO_VALOR_BOLSA, iTb148.TP_GRUPO_BOLSA }).FirstOrDefault();

                if (tb148 != null)
                {
                    ddlTipoBolsaMod.SelectedValue = tb148.TP_GRUPO_BOLSA;
                    CarregaBolsas();
                    ddlBolsaAluMod.SelectedValue = res.TB148_TIPO_BOLSA.CO_TIPO_BOLSA.ToString();

                    chkDesctoPercBolsaMod.Checked = res.NU_PEC_DESBOL != null;
                    txtValorDesctoMod.Text = chkDesctoPercBolsaMod.Checked ? (res.NU_PEC_DESBOL != null ? String.Format("{0:N}", res.NU_PEC_DESBOL) : "") : (res.NU_VAL_DESBOL != null ? String.Format("{0:N}", res.NU_VAL_DESBOL) : "");

                    chkDesctoPercBolsaMod.Enabled = chkDesctoPercBolsaMod.Enabled = txtValorDesctoMod.Enabled = true;

                    txtPeriodoDeAluMod.Text = res.DT_VENC_BOLSA != null ? res.DT_VENC_BOLSA.Value.ToString("dd/MM/yyyy") : "";
                    txtPeriodoAteAluMod.Text = res.DT_VENC_BOLSAF != null ? res.DT_VENC_BOLSAF.Value.ToString("dd/MM/yyyy") : "";
                }
            }

            #endregion

            updModalInfosCadas.Update();
        }

        /// <summary>
        /// Executa método javascript que mostra a Modal com o histórico de ocorrências
        /// </summary>
        private void abreModalHistOcorrencias()
        {
            ScriptManager.RegisterStartupScript(
                updModalHistoOcorr,
                this.GetType(),
                "Acao",
                "AbreModalHistOcorr();",
                true
            );
        }

        /// <summary>
        /// Executa método javascript que mostra a Modal com o histórico de ocorrências
        /// </summary>
        private void abreModalInfosCadastrais()
        {
            ScriptManager.RegisterStartupScript(
                updModalInfosCadas,
                this.GetType(),
                "Acao",
                "AbreModalInfosCadas();",
                true
            );
        }

        #endregion

        #region Impressões Guias

        /// <summary>
        /// Realiza a impressão da guia de exame médico
        /// </summary>
        private void ImpressaoGuiaExame(int ID_EXAME)
        {
            string parametros = "";
            string infos;

            int coEmp = LoginAuxili.CO_EMP;
            int lRetorno;
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptGuiaExame fpcb = new RptGuiaExame();
            lRetorno = fpcb.InitReport(parametros, infos, coEmp, 0, ID_EXAME);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            //AuxiliPagina.AbreNovaJanela(this, Session["URLRelatorio"].ToString());

            string strURL = String.Format("{0}", Session["URLRelatorio"].ToString());
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "newpageE", "customOpen('" + strURL + "\');", true);

            //----------------> Limpa a var de sessão com o url do relatório.
            Session.Remove("URLRelatorio");

            //----------------> Limpa a ref da url utilizada para carregar o relatório.
            PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
            isreadonly.SetValue(this.Request.QueryString, false, null);
            isreadonly.SetValue(this.Request.QueryString, true, null);
        }

        /// <summary>
        /// Realiza a impressão da guia de Serviços Ambulatoriais
        /// </summary>
        /// <param name="ID_ATEND_SERV_AMBU"></param>
        private void ImpressaoGuiaServAmbu(int ID_ATEND_SERV_AMBU)
        {
            string parametros = "";
            string infos;

            int coEmp = LoginAuxili.CO_EMP;
            int lRetorno;
            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);

            RptGuiaServAmbul fpcb = new RptGuiaServAmbul();
            lRetorno = fpcb.InitReport(parametros, infos, coEmp, 0, ID_ATEND_SERV_AMBU);
            Session["Report"] = fpcb;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            //AuxiliPagina.AbreNovaJanela(this, Session["URLRelatorio"].ToString());

            string strURL = String.Format("{0}", Session["URLRelatorio"].ToString());
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "newpageS", "customOpen('" + strURL + "\');", true);

            //----------------> Limpa a var de sessão com o url do relatório.
            Session.Remove("URLRelatorio");

            //----------------> Limpa a ref da url utilizada para carregar o relatório.
            PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
            isreadonly.SetValue(this.Request.QueryString, false, null);
            isreadonly.SetValue(this.Request.QueryString, true, null);
        }

        #endregion

        #region Funções de Campo

        protected void ddlOrdeAtendPend_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGridAtendimentosPendentes(ddlOrdeAtendPend.SelectedValue);
            updAtendPenden.Update();
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            CarregaGridAtendimentosPendentes(ddlOrdeAtendPend.SelectedValue);
            int idCentrRegul = (!string.IsNullOrEmpty(hidIdCentrRegul.Value) ? int.Parse(hidIdCentrRegul.Value) : 0);
            CarregaGridItensPendentes(idCentrRegul, ddlTipoItem.SelectedValue);

            //Verifica se a grid já possuia um registro selecionado antes, e chama um método responsável por selecioná-lo de novo no PRÉ-ATENDIMENTO
            if ((string)HttpContext.Current.Session["FL_Select_Grid_OP"] == "S")
            {
                selecionaGridAtendPendentes();
            }

            updAtendPenden.Update();
            //updItensPendentes.Update();
        }

        protected void grdAtendimPendentes_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.DataItem != null)
            //{
            //    grdAtendimPendentes.Columns[0].HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            //}
        }

        protected void grdDetalhePendencia_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                //Verifica se o item foi aprovado ou não, e faz as devidas alterações nos ícones de impressão
                string status = (((HiddenField)e.Row.Cells[0].FindControl("hidCoStatus")).Value);
                //Instancia os objetos dos imagebuttons 
                ImageButton img = (ImageButton)e.Row.Cells[7].FindControl("imgImpGuia");
                ImageButton imgDesab = (ImageButton)e.Row.Cells[7].FindControl("imgImpGuiaDesabilitada");
                if (status == "S")
                {
                    imgDesab.Visible = false;
                    img.Visible = true;
                }
                else
                {
                    imgDesab.ToolTip = "Impressão da Guia não disponível( " + AuxiliFormatoExibicao.RetornaSituacaoCentralRegulacao(status) + " ).";
                    imgDesab.Visible = true;
                    img.Visible = false;
                }
            }
        }

        protected void chkSelecGridDetalhada_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            CheckBox chk;

            // Valida se a grid de atividades possui algum registro
            if (grdAtendimPendentes.Rows.Count != 0)
            {
                // Passa por todos os registros da grid de atividades
                foreach (GridViewRow linha in grdAtendimPendentes.Rows)
                {
                    chk = (CheckBox)linha.Cells[0].FindControl("chkSelecGridDetalhada");
                    HiddenField hidSele = ((HiddenField)linha.Cells[0].FindControl("hidLocalSelecionado"));
                    int idCentrRegul = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidCoCentrRegul")).Value);

                    // Desmarca todos os registros menos o que foi clicado
                    if (chk.ClientID != atual.ClientID)
                    {
                        bool sele = false;
                        if (chk.Checked)
                            sele = true;

                        chk.Checked = false;

                        //Só libera o item se for um item que estava selecionado anteriormente
                        if (sele)
                        {
                            TBS347_CENTR_REGUL tb347 = TBS347_CENTR_REGUL.RetornaPelaChavePrimaria(idCentrRegul);
                            tb347.FL_USO = "N";
                            TBS347_CENTR_REGUL.SaveOrUpdate(tb347, true);
                        }
                    }
                    else
                    {
                        if (chk.Checked)
                        {
                            int idCentrRegu = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidCoCentrRegul")).Value);
                            int coAlu = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidCoAlu")).Value);
                            //string idCentRegu = ((HiddenField)linha.Cells[0].FindControl("hidCoCentrRegul")).Value;

                            CarregaGridItensPendentes(idCentrRegu, ddlTipoItem.SelectedValue);

                            //Bloqueia o registro
                            hidItemSelec.Value = idCentrRegul.ToString();

                            //Salva no banco que o registro em questão está em uso
                            TBS347_CENTR_REGUL tb347 = TBS347_CENTR_REGUL.RetornaPelaChavePrimaria(idCentrRegul);
                            tb347.FL_USO = "S";
                            TBS347_CENTR_REGUL.SaveOrUpdate(tb347, true);

                            //Atribui valores à variáveis que serão usadas posteriormente
                            hidIdCentrRegul.Value = idCentrRegu.ToString();

                            //Guarda a FLAG para saber se o Checkbox está sendo clicado para marcar ou desmarcar, neste caso, ele grava como para marcar.
                            HttpContext.Current.Session.Add("FL_Select_Grid_OP", "S");

                            //Guarda o Valor do Pré-Atendimento, para fins de posteriormente comparar este valor 
                            HttpContext.Current.Session.Add("VL_Atend_OP", idCentrRegu);

                            CarregaTiposItensExistentes(idCentrRegu);
                            //updItensPendentes.Update();
                        }
                        else
                        {
                            hidIdCentrRegul.Value = "";
                            CarregaTiposItensGeral();

                            HttpContext.Current.Session.Remove("VL_Atend_OP");
                            HttpContext.Current.Session.Add("FL_Select_Grid_OP", "N");

                            //Libera o registro
                            hidItemSelec.Value = "";

                            //Salva no banco que o registro não está em uso
                            TBS347_CENTR_REGUL tb347 = TBS347_CENTR_REGUL.RetornaPelaChavePrimaria(idCentrRegul);
                            tb347.FL_USO = "N";
                            TBS347_CENTR_REGUL.SaveOrUpdate(tb347, true);

                            CarregaGridItensPendentes(0, "");

                            //updItensPendentes.Update();
                        }
                    }
                }
            }
        }

        protected void ddlTipoItem_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGridItensPendentes((!string.IsNullOrEmpty(hidIdCentrRegul.Value) ? int.Parse(hidIdCentrRegul.Value) : 0), ddlTipoItem.SelectedValue, ddlOrdDetalhePendencia.SelectedValue);
            //updItensPendentes.Update();
        }

        protected void ddlOrdDetalhePendencia_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaGridItensPendentes((!string.IsNullOrEmpty(hidIdCentrRegul.Value) ? int.Parse(hidIdCentrRegul.Value) : 0), ddlTipoItem.SelectedValue, ddlOrdDetalhePendencia.SelectedValue);
        }

        protected void ddlUnidade_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaPacientes();
            CarregaProfissionais();
        }

        protected void imgPesqGrid_OnClick(object sender, EventArgs e)
        {
            CarregaGridAtendimentosPendentes(ddlOrdeAtendPend.SelectedValue);
            updAtendPenden.Update();
        }

        protected void imgImpGuia_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;

            if (grdAtendimPendentes.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdDetalhePendencia.Rows)
                {
                    img = (ImageButton)linha.Cells[7].FindControl("imgImpGuia");

                    //Atribui à session o id da central de regulação clicada para ser usada no popup que será aberto
                    if (img.ClientID == atual.ClientID)
                    {
                        string coTipo = linha.Cells[3].Text.Substring(0, 2);
                        int ID_ITEM = int.Parse(((HiddenField)linha.Cells[5].FindControl("hidIdItem")).Value);
                        int ID_ITEM_CENTR_REGUL = int.Parse(((HiddenField)linha.Cells[5].FindControl("hidIdItemCentrRegul")).Value);

                        #region Atualiza Nº da Guia

                        #region Verifica o sequencial

                        var res = (from tbs350pesq in TBS350_ITEM_CENTR_REGUL.RetornaTodosRegistros()
                                   where tbs350pesq.NU_GUIA != null
                                   select new { tbs350pesq.NU_GUIA }).OrderByDescending(w => w.NU_GUIA).FirstOrDefault();

                        string seq;
                        int seq2;
                        int seqConcat;
                        string seqcon;
                        if (res == null)
                        {
                            seq2 = 1;
                        }
                        else
                        {
                            seq = res.NU_GUIA.Substring(2, 7);
                            seq2 = int.Parse(seq);
                        }

                        seqConcat = seq2 + 1;
                        seqcon = seqConcat.ToString().PadLeft(7, '0');

                        string sequencialPronto = "GU" + seqcon;

                        #endregion

                        TBS350_ITEM_CENTR_REGUL tbs350 = TBS350_ITEM_CENTR_REGUL.RetornaPelaChavePrimaria(ID_ITEM_CENTR_REGUL);
                        if (string.IsNullOrEmpty(tbs350.NU_GUIA))
                            tbs350.NU_GUIA = sequencialPronto;
                        TBS350_ITEM_CENTR_REGUL.SaveOrUpdate(tbs350, true);

                        #endregion

                        //Chama a impressão da guia de acordo com o tipo do item
                        switch (coTipo)
                        {
                            case "EX":
                                ImpressaoGuiaExame(ID_ITEM);
                                break;
                            case "SA":
                                ImpressaoGuiaServAmbu(ID_ITEM);
                                break;
                        }
                    }
                }
            }
        }
        
        protected void imgHistOcorrencias_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;

            if (grdDetalhePendencia.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdDetalhePendencia.Rows)
                {
                    img = (ImageButton)linha.Cells[5].FindControl("imgHistOcorrencias");
                    int idItemCentrRegu = int.Parse(((HiddenField)linha.Cells[5].FindControl("hidIdItemCentrRegul")).Value);

                    //Atribui à session o id da central de regulação clicada para ser usada no popup que será aberto
                    if (img.ClientID == atual.ClientID)
                    {
                        CarregaHistoricoOcorrencias(idItemCentrRegu);

                        updModalHistoOcorr.Update();
                        abreModalHistOcorrencias();

                        break;
                    }
                }
            }
        }

        protected void imgInfosCadastrais_OnClick(object sender, EventArgs e)
        {
            ImageButton atual = (ImageButton)sender;
            ImageButton img;

            if (grdAtendimPendentes.Rows.Count != 0)
            {
                foreach (GridViewRow linha in grdAtendimPendentes.Rows)
                {
                    img = (ImageButton)linha.Cells[2].FindControl("imgInfosCadastrais");

                    //Atribui à session o id da central de regulação clicada para ser usada no popup que será aberto
                    if (img.ClientID == atual.ClientID)
                    {
                        int co_alu = int.Parse(((HiddenField)linha.Cells[0].FindControl("hidCoAlu")).Value);

                        CarregaInfosCadastrais(co_alu);
                        abreModalInfosCadastrais();

                        break;
                    }
                }
            }
        }

        #endregion
    }
}