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
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Resources;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Data.Objects;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5500_CtrlFluxoCaixa.F5501_GeraFluxoCaixa
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
            if (IsPostBack)
                return;

            CarregaUnidades();
            divGrid.Visible = false;
        }

        #endregion

        #region Carregamento DropDown

        /// <summary>
        /// Método que carrega o dropdown das Unidades
        /// </summary>
        private void CarregaUnidades()
        {
            ddlUnidade.DataSource = (from emp in TB25_EMPRESA.RetornaTodosRegistros()
                                     where emp.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                     select new { emp.NO_FANTAS_EMP, emp.CO_EMP });

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.Items.Insert(0, new ListItem("Selecione", ""));

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();

            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }
        #endregion

        protected void btnGerar_Click(object sender, EventArgs e)
        {
            DateTime dataCalendario;

            if (!DateTime.TryParse(txtDataPeriodoIni.Text, out dataCalendario))
            {
                AuxiliPagina.EnvioMensagemErro(this, "Informe uma data de início válida.");
                return;
            }

            if (!DateTime.TryParse(txtDataPeriodoFim.Text, out dataCalendario))
            {
                AuxiliPagina.EnvioMensagemErro(this, "Informe uma data final válida.");
                return;
            }

            if (rblTituloCAP.Items.OfType<ListItem>().Count(x => x.Selected) == 0 &&
                rblTituloCAR.Items.OfType<ListItem>().Count(x => x.Selected) == 0)
            {
                AuxiliPagina.EnvioMensagemErro(this, "Selecione o status dos títulos para gerar o fluxo de caixa.");
                return;
            }

            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            DateTime dtInicio = DateTime.ParseExact(txtDataPeriodoIni.Text, "dd/MM/yyyy", null);
            DateTime dtFim = DateTime.ParseExact(txtDataPeriodoFim.Text, "dd/MM/yyyy", null);
            string[] stsCAP = rblTituloCAP.Items.OfType<ListItem>()
                .Where(x => x.Selected).Select(x => x.Value).ToArray();
            string[] stsCAR = rblTituloCAR.Items.OfType<ListItem>()
                .Where(x => x.Selected).Select(x => x.Value).ToArray();

            var emp = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);
            if (!emp.DT_SALDO_INICIAL.HasValue || !emp.VL_SALDO_INICIAL.HasValue)
            {
                AuxiliPagina.EnvioMensagemErro(this, "É necessário cadastrar a data e o saldo inicial da unidade.");
                return;
            }

            if (dtInicio < emp.DT_SALDO_INICIAL.Value)
            {
                AuxiliPagina.EnvioMensagemErro(this, "O período inicial deve ser superior ao cadastrado no saldo inicial da unidade.");
                return;
            }

            // Deleta os registros existente no periodo com o movimento aberto
            var lstDelete = from fc in TB319_FLUXO_CAIXA.RetornaTodosRegistros()
                            where fc.DT_MOVIM_FLUXO_CAIXA >= dtInicio
                            && fc.DT_MOVIM_FLUXO_CAIXA <= dtFim
                            && fc.TB320_CTRL_FLUXO_CAIXA.CO_SITU_FLUXO_CAIXA == "A"
                            select fc;

            foreach (var fc in lstDelete)
            {
                GestorEntities.Delete(fc, false);
            }

            DateTime dtAtual = DateTime.Today;

            var lstCAP = from cap in TB38_CTA_PAGAR.RetornaTodosRegistros()
                         where stsCAP.Contains(cap.IC_SIT_DOC)
                         && ((cap.DT_VEN_DOC >= dtInicio && cap.DT_VEN_DOC <= dtAtual && cap.IC_SIT_DOC == "Q")
                         || (cap.DT_VEN_DOC < dtFim && cap.DT_VEN_DOC > dtAtual))
                         select new RegAux
                         {
                             DataSitu = DateTime.Now,
                             DtMovimento = (cap.DT_REC_DOC.HasValue) ? cap.DT_REC_DOC.Value : cap.DT_VEN_DOC,
                             TipoMovimento = "D",
                             OrigemPgto = cap.FL_ORIGEM_PGTO,
                             VlMovimento = (cap.IC_SIT_DOC == "A") ? cap.VR_PAR_DOC : cap.VR_PAG,
                             NuDoc = cap.NU_DOC,
                             Descricao = cap.TB41_FORNEC.CO_CPFCGC_FORN + " - " + cap.NU_DOC
                                + (cap.TB39_HISTORICO == null ? "" : " - " + cap.TB39_HISTORICO.DE_HISTORICO)
                         };

            var lstCAR = from car in TB47_CTA_RECEB.RetornaTodosRegistros()
                         where stsCAR.Contains(car.IC_SIT_DOC)
                         && ((car.DT_VEN_DOC >= dtInicio && car.DT_VEN_DOC <= dtAtual && car.IC_SIT_DOC == "Q")
                         || (car.DT_VEN_DOC < dtFim && car.DT_VEN_DOC > dtAtual))
                         select new RegAux
                         {
                             DataSitu = DateTime.Now,
                             DtMovimento = (car.DT_REC_DOC.HasValue) ? car.DT_REC_DOC.Value : car.DT_VEN_DOC,
                             TipoMovimento = "R",
                             OrigemPgto = car.FL_ORIGEM_PGTO,
                             VlMovimento = (car.IC_SIT_DOC == "A") ? car.VR_PAR_DOC : car.VR_PAG,
                             NuDoc = car.NU_DOC,
                             Descricao = (car.TP_CLIENTE_DOC == "A" ? car.TB108_RESPONSAVEL.NU_CPF_RESP
                                 : car.TB103_CLIENTE.CO_CPFCGC_CLI) + " - " + car.NU_DOC
                                 + (car.TB39_HISTORICO == null ? "" : " - " + car.TB39_HISTORICO.DE_HISTORICO)
                         };

            var lstTmp = lstCAP.ToList();
            lstTmp.AddRange(lstCAR);

            var lstTit = lstTmp.OrderBy(x => x.DtMovimento);

            var tb25 = TB25_EMPRESA.RetornaPelaChavePrimaria(coEmp);
            List<TB320_CTRL_FLUXO_CAIXA> lstCtrl = new List<TB320_CTRL_FLUXO_CAIXA>();

            foreach (var t in lstTit)
            {
                TB320_CTRL_FLUXO_CAIXA ctrl = lstCtrl.FirstOrDefault(x => x.CO_ANO_CTRL_FLUXO_CAIXA == t.DtMovimento.Year
                    && x.CO_MES_CTRL_FLUXO_CAIXA == t.DtMovimento.Month);

                if (ctrl == null)
                {
                    ctrl = TB320_CTRL_FLUXO_CAIXA.RetornaTodosRegistros()
                            .FirstOrDefault(x => x.CO_ANO_CTRL_FLUXO_CAIXA == t.DtMovimento.Year
                            && x.CO_MES_CTRL_FLUXO_CAIXA == t.DtMovimento.Month);

                    if (ctrl == null)
                    {
                        ctrl = new TB320_CTRL_FLUXO_CAIXA()
                        {
                            CO_ANO_CTRL_FLUXO_CAIXA = t.DtMovimento.Year,
                            CO_MES_CTRL_FLUXO_CAIXA = t.DtMovimento.Month,
                            CO_SITU_FLUXO_CAIXA = "A",
                            VL_FECHA_FLUXO_CAIXA = 0,
                            TB25_EMPRESA = tb25
                        };
                    }
                }

                TB319_FLUXO_CAIXA fc = new TB319_FLUXO_CAIXA()
                {
                    DE_MOVIM_FLUXO_CAIXA = t.Descricao ?? "",
                    DT_MOVIM_FLUXO_CAIXA = t.DtMovimento,
                    DT_SITU_MOVIM_FLUXO_CAIXA = t.DataSitu,
                    NU_DOC = t.NuDoc,
                    TB320_CTRL_FLUXO_CAIXA = ctrl,
                    TP_MOVIM_FLUXO_CAIXA = t.TipoMovimento,
                    FL_ORIGEM_PGTO = t.OrigemPgto,
                    VL_MOVIM_FLUXO_CAIXA = t.VlMovimento.HasValue ? t.VlMovimento.Value : 0
                };

                ctrl.TB319_FLUXO_CAIXA.Add(fc);
                if (!lstCtrl.Contains(ctrl))
                    lstCtrl.Add(ctrl);
            }

            foreach (var ctrl in lstCtrl)
            {
                GestorEntities.SaveOrUpdate(ctrl, false);
            }

            GestorEntities.SaveChanges(RefreshMode.ClientWins, null);

            divGrid.Visible = liObser.Visible = liResumo.Visible = true;

            if (lstTit.Count() > 0)
            {
                grdResumo.DataBind();
            }

            List<RegGrid> lstGrd = new List<RegGrid>();
            lstGrd.Add(new RegGrid()
            {
                TipoTitulo = "Contas a Pagar",
                QtdTitulos = lstTit.Count(x => x.TipoMovimento == "D").ToString("n0"),
                VlTotal = lstTit.Where(x => x.TipoMovimento == "D").Sum(x => x.VlMovimento).HasValue
                            ? lstTit.Where(x => x.TipoMovimento == "D").Sum(x => x.VlMovimento).Value.ToString("c2")
                            : 0.ToString("c2")
            });

            lstGrd.Add(new RegGrid()
            {
                TipoTitulo = "Contas a Receber",
                QtdTitulos = lstTit.Count(x => x.TipoMovimento == "R").ToString("n0"),
                VlTotal = lstTit.Where(x => x.TipoMovimento == "R").Sum(x => x.VlMovimento).HasValue
                            ? lstTit.Where(x => x.TipoMovimento == "R").Sum(x => x.VlMovimento).Value.ToString("c2")
                            : 0.ToString("c2")
            });

            grdResumo.DataSource = lstGrd;
            grdResumo.DataBind();
        }

        public class RegAux
        {
            public string NuDoc { get; set; }
            public DateTime DtMovimento { get; set; }
            public DateTime DataSitu { get; set; }
            public string TipoMovimento { get; set; }
            public string Descricao { get; set; }
            public string OrigemPgto { get; set; }
            public decimal? VlMovimento { get; set; }
        }

        public class RegGrid
        {
            public string TipoTitulo { get; set; }
            public string QtdTitulos { get; set; }
            public string VlTotal { get; set; }
        }
    }
}
