using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using System.Data;
using Resources;
using System.Data.Objects;
using System.IO;
using C2BR.GestorEducacao.BusinessEntities.Models;

namespace C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8270_Prontuario._8271_Relatorios
{
    public partial class RelatorioProcedimentoPaciente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                CarregaOperadoras();
                pnlReport.Visible = false;
            }
        }

        private void CarregaOperadoras()
        {
            AuxiliCarregamentos.CarregaOperadorasPlanSaude(ddlOperadora, true, true, false);
        }

        private void CarregarPlanosSaude(DropDownList ddlOperadora)
        {
            AuxiliCarregamentos.CarregaPlanosSaude(ddlPlano, ddlOperadora, true);
        }

        protected void ddlOperadora_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarPlanosSaude(ddlOperadora);
        }

        private IList<Paciente> CarregarPacientes()
        {
            var query = (from tb06 in TB07_ALUNO.RetornaTodosRegistros()
                         select new Paciente
                         {
                             ID = tb06.CO_ALU,
                             Nome = tb06.NO_ALU,
                             Sexo = tb06.CO_SEXO_ALU,
                             Idade = DateTime.Now.Year - tb06.DT_NASC_ALU.Value.Year,
                             DataNascimento = tb06.DT_NASC_ALU
                         }).ToList();

            query.ForEach(paciente =>
            {
                paciente.Procedimentos = (from tbs389 in TBS389_ASSOC_ITENS_PLANE_AGEND.RetornaTodosRegistros()
                                          where
                                           ((tbs389.TBS386_ITENS_PLANE_AVALI.CO_SITUA == "A" &&
                                           tbs389.TBS386_ITENS_PLANE_AVALI.TBS356_PROC_MEDIC_PROCE.CO_SITU_PROC_MEDI == "A") &&
                                           tbs389.TBS174_AGEND_HORAR.CO_ALU == paciente.ID)
                                          select new Procedimento
                                              {
                                                  Data = tbs389.TBS174_AGEND_HORAR.DT_AGEND_HORAR,
                                                  Nome = tbs389.TBS386_ITENS_PLANE_AVALI.TBS356_PROC_MEDIC_PROCE.NM_PROC_MEDI,
                                                  Operadora = tbs389.TBS174_AGEND_HORAR.TB251_PLANO_OPERA.TB250_OPERA.NOM_OPER,
                                                  Plano = tbs389.TBS174_AGEND_HORAR.TB251_PLANO_OPERA.NOM_PLAN,
                                                  Valor = tbs389.TBS386_ITENS_PLANE_AVALI.TBS356_PROC_MEDIC_PROCE.TBS353_VALOR_PROC_MEDIC_PROCE.Sum(x => x.VL_BASE)
                                              }).OrderBy(x => x.Data).ToList();

                paciente.ValorTotal = paciente.Procedimentos.Where(x => x.Valor != null).Sum(x => x.Valor);
            });

            query.ForEach(paciente =>
            {
                paciente.Procedimentos.ToList().ForEach(procedimento =>
                {
                    var procedimentos = paciente.Procedimentos.Where(p => p.Data == procedimento.Data && p.Nome == procedimento.Nome);
                    if (procedimentos.Count() > 1)
                        paciente.Procedimentos.Remove(procedimentos.FirstOrDefault(x => string.IsNullOrEmpty(x.Plano)));
                });

                paciente.QPA = (from tbs174 in TBS174_AGEND_HORAR
                                    .RetornaTodosRegistros()
                                    .Where(x => x.CO_ALU == paciente.ID)
                                select tbs174).Count();

                paciente.QFA = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                    .Where(x => x.FL_JUSTI_CANCE == "N" &&
                                           x.CO_SITUA_AGEND_HORAR == "C" &&
                                           x.CO_ALU == paciente.ID)
                                select tbs174.CO_ALU).Count();

                paciente.QFJ = (from tbs174 in TBS174_AGEND_HORAR.RetornaTodosRegistros()
                                    .Where(x => x.FL_JUSTI_CANCE == "S" &&
                                           x.CO_SITUA_AGEND_HORAR == "C" &&
                                           x.CO_ALU == paciente.ID)
                                select tbs174).Count();

                paciente.QPF = (paciente.QPA - paciente.QFJ);

                paciente.QPR = (paciente.QPA - paciente.QFA - paciente.QFJ);
            });

            return query;
        }

        protected void ddlOperadora_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarPlanosSaude(ddlOperadora);
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            pnlFiltro.Visible = false;
            pnlReport.Visible = true;

            var pacientes = CarregarPacientes();

            if (ddlPlano.SelectedItem != null && ddlPlano.SelectedItem.Text != "Todos")
                pacientes = pacientes.Where(p => p.Procedimentos.Any(pro => pro.Plano == ddlPlano.SelectedItem.Text)).ToList();
            else if (ddlOperadora.SelectedItem != null && ddlOperadora.SelectedItem.Text != "Todos")
                pacientes = pacientes.Where(p => p.Procedimentos.Any(pro => pro.Operadora == ddlOperadora.SelectedItem.Text)).ToList();

            if (!string.IsNullOrEmpty(txtDataIni.Text) && !string.IsNullOrEmpty(txtDataFim.Text))
                pacientes = pacientes.Where(p => p.Procedimentos.Any(pro => pro.Data >= DateTime.Parse(txtDataIni.Text) &&
                    pro.Data <= DateTime.Parse(txtDataIni.Text))).ToList();

            rptPacientes.DataSource = pacientes;
            rptPacientes.DataBind();
        }
    }
}