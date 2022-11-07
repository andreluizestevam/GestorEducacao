//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: GESTÃO DE DADOS DE ALUNOS
// OBJETIVO: RELAÇÃO DE ALUNOS - ANIVERSARIANTES
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//29/11/2014| Maxwell Almeida            | Criação da página para emissão da ficha de pré-atendimento
//          |                            | 

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.IO;
using System.Data.Objects;
using System.ServiceModel;
using System.Configuration;
using Resources;
using WRAuxiliares = C2BR.GestorEducacao.DLLRelatorioWeb.Auxiliares;
using C2BR.GestorEducacao.UI.Library.Componentes;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.DLLRelatorioWeb.Interfaces;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.Reports.GSAUD._3000_ControleInformacoesUsuario._3400_CtrlAtendimentoUsuario;

namespace C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3299_Relatorios
{
    public partial class FichaDirecionamento : System.Web.UI.Page
    {
        public PadraoRelatorios PadraoRelatoriosCorrente { get { return (PadraoRelatorios)Page.Master; } }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //--------> Criação das instâncias utilizadas
            PadraoRelatoriosCorrente.OnAcaoGeraRelatorio += new PadraoRelatorios.OnAcaoGeraRelatorioHandler(PadraoRelatoriosCorrente_OnAcaoGeraRelatorio);
        }

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IniPeri.Text = DateTime.Now.ToString();
                FimPeri.Text = DateTime.Now.ToString();
                CarregaGrid("");
                AuxiliCarregamentos.CarregaUnidade(ddlUnidade, LoginAuxili.ORG_CODIGO_ORGAO, true);
            }
        }

        #endregion

        #region Carregamentos

        //====> Método que faz a chamada de outro método de acordo com o ddlTipoPesq
        void PadraoRelatoriosCorrente_OnAcaoGeraRelatorio()
        {
            string parametros;

            parametros = "";//"( Unidade: " + noUnidade.ToUpper() + " - Departamento: " + noDeptoLocal.ToUpper() + " - Médico: " + noMedico.ToUpper() + "" + " - Período: " + Periodo.ToUpper() + " - Status: " + noStatus + " - Situação: " + noSitu + " )";
            if (string.IsNullOrEmpty(hididEncam.Value))
            {
                AuxiliPagina.EnvioMensagemErro(this.Page, "Favor selecione ao menos um Direcionaento para emitir a Ficha");
                return;
            }

            //--------> Variáveis de parâmetro do Relatório
            string infos, NomeFuncionalidadeCadastrada;
            int lRetorno, idEncam;

            idEncam = int.Parse(hididEncam.Value);

            infos = AuxiliRelatorioTemporario.GeraIdentFuncionarioRelatorio(Request.UserHostAddress);
            NomeFuncionalidadeCadastrada = "";
            RptFichaDirecionamento rpt = new RptFichaDirecionamento();
            lRetorno = rpt.InitReport(parametros, infos, LoginAuxili.CO_EMP, idEncam, NomeFuncionalidadeCadastrada);
            Session["Report"] = rpt;
            Session["URLRelatorio"] = "/GeducReportViewer.aspx";

            AuxiliPagina.TrataRetornoRelatorio(lRetorno, this.AppRelativeVirtualPath);
        }

        /// <summary>
        /// Carrega a grid de Direcionamentos
        /// </summary>
        protected void CarregaGrid(string NO_ALU)
        {
            DateTime dtFim = DateTime.Parse(FimPeri.Text);
            DateTime dtIni = DateTime.Parse(IniPeri.Text);
            int coEmp = (!string.IsNullOrEmpty(ddlUnidade.SelectedValue) ? int.Parse(ddlUnidade.SelectedValue) : 0);
            var res = (from tbs195 in TBS195_ENCAM_MEDIC.RetornaTodosRegistros()
                       join tb108 in TB108_RESPONSAVEL.RetornaTodosRegistros() on tbs195.CO_RESP equals tb108.CO_RESP
                       join tb07 in TB07_ALUNO.RetornaTodosRegistros() on tbs195.CO_ALU equals tb07.CO_ALU
                       join tb25 in TB25_EMPRESA.RetornaTodosRegistros() on tbs195.CO_EMP_COL equals tb25.CO_EMP
                       join tb63 in TB63_ESPECIALIDADE.RetornaTodosRegistros() on tbs195.CO_ESPEC equals tb63.CO_ESPECIALIDADE
                       where tb07.NO_ALU.Contains(NO_ALU)
                       && ((EntityFunctions.TruncateTime(tbs195.DT_ENCAM_MEDIC) >= EntityFunctions.TruncateTime(dtIni))
                       && (EntityFunctions.TruncateTime(tbs195.DT_ENCAM_MEDIC) <= EntityFunctions.TruncateTime(dtFim)))
                       && (coEmp != 0 ? tbs195.CO_EMP_ENCAM_MEDIC == coEmp : 0 == 0)
                       select new Saida
                       {
                           Data = tbs195.DT_ENCAM_MEDIC,
                           UNID = tb25.NO_FANTAS_EMP,
                           ID_ENCAM_MEDIC = tbs195.ID_ENCAM_MEDIC,
                           NO_ESPEC = tb63.NO_ESPECIALIDADE,
                           NO_RESP = tb108.NO_RESP,
                           CO_ENCAM = tbs195.CO_ENCAM_MEDIC,
                           NO_ALU = tb07.NO_ALU,
                       }).DistinctBy(w => w.ID_ENCAM_MEDIC).OrderBy(w => w.Data).ToList();

            grdAtendimentos.DataSource = res;
            grdAtendimentos.DataBind();
        }

        public class Saida
        {
            public DateTime? Data { get; set; }
            public string Data_V
            {
                get
                {
                    return (this.Data.HasValue ? this.Data.Value.ToString("dd/MM/yy") + " " + this.Data.Value.ToString("HH:mm") : " - ");
                }
            }
            public string UNID { get; set; }
            public string NO_ESPEC { get; set; }
            public string NO_RESP { get; set; }
            public string NO_ALU { get; set; }
            public int ID_ENCAM_MEDIC { get; set; }
            //public string SENHA { get; set; }
            public string CO_ENCAM { get; set; }
            public string CO_ENCAM_V
            {
                get
                {
                    if (string.IsNullOrEmpty(this.CO_ENCAM))
                        return "-";
                    else
                        return AuxiliFormatoExibicao.FormataCodigosServicosSaude(this.CO_ENCAM);
                }
            }
        }

        #endregion

        #region Funções de Campo

        protected void chkSelectCamp_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox atual = (CheckBox)sender;
            CheckBox chk;

            // Valida se a grid de atividades possui algum registro
            if (grdAtendimentos.Rows.Count != 0)
            {
                // Passa por todos os registros da grid de atividades
                foreach (GridViewRow linha in grdAtendimentos.Rows)
                {
                    chk = (CheckBox)linha.Cells[0].FindControl("chkSelectCamp");

                    // Desmarca todos os registros menos o que foi clicado
                    if (chk.ClientID != atual.ClientID)
                    {
                        chk.Checked = false;
                    }
                    else
                    {
                        if (chk.Checked)
                            hididEncam.Value = (((HiddenField)linha.Cells[0].FindControl("hidIdEncam")).Value);
                        else
                            hididEncam.Value = "";
                    }
                }
            }
        }

        #endregion

        #region Funções de Campo

        protected void imgPesqGridAgenda_OnClick(object sender, EventArgs e)
        {
            CarregaGrid(txtNomePacie.Text);
        }

        #endregion
    }
}