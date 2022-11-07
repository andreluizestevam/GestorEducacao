//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLADORIA ADMINISTRATIVA ESCOLAR
// SUBMÓDULO: PLANEJAMENTO ESCOLAR (CENÁRIO ALUNOS)
// OBJETIVO: REGISTRO DO PERFIL DE OCUPAÇÃO DE ALUNOS
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA    |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ---------+----------------------------+-------------------------------------
//

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
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
namespace C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1120_PlanejEscolarAlunos.F1124_RegisPerfilOcupAluno
{
    public partial class Busca : System.Web.UI.Page 
    {
        public PadraoBuscas CurrentPadraoBuscas { get { return ((PadraoBuscas)this.Master); } }

        #region Eventos

        protected override void OnPreInit(EventArgs e) 
        {
            base.OnPreInit(e);

            CurrentPadraoBuscas.OnAcaoBuscaDefineGridView += new PadraoBuscas.OnAcaoBuscaDefineGridViewHandler(CurrentPadraoBuscas_OnAcaoBuscaDefineGridView);
            CurrentPadraoBuscas.OnDefineColunasGridView += new PadraoBuscas.OnDefineColunasGridViewHandler(CurrentPadraoBuscas_OnDefineColunasGridView);
            CurrentPadraoBuscas.OnDefineQueryStringIds += new PadraoBuscas.OnDefineQueryStringIdsHandler(CurrentPadraoBuscas_OnDefineQueryStringIds);
        }

        protected void Page_Load(object sender, EventArgs e) 
        {
            if (!IsPostBack) 
            {
                CarregaUnidades();
                CarregaAno();   
            }
        }        

        private void CurrentPadraoBuscas_OnDefineColunasGridView() 
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_UNIDADE_PERFIL_VAGAS" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField 
            {
                DataField = "NO_FANTAS_EMP",
                HeaderText = "Unidade"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField 
            {
                DataField = "CO_ANO_PERFIL_VAGAS",
                HeaderText = "Ano"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField 
            {
                DataField = "QT_VAGAS_PLANEJ_PERFIL",
                HeaderText = "QVP"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField 
            {
                DataField = "QT_VAGAS_RESERV_PERFIL",
                HeaderText = "QRV"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField 
            {
                DataField = "QT_VAGAS_MATRIC_PERFIL",
                HeaderText = "QMN"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField 
            {
                DataField = "QT_VAGAS_RENOVA_PERFIL",
                HeaderText = "QMR"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField 
            {
                DataField = "QT_VAGAS_ATIVOS_PERFIL",
                HeaderText = "QAA"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField 
            {
                DataField = "QT_VAGAS_TRANSF_PERFIL",
                HeaderText = "QAT"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField 
            {
                DataField = "QT_VAGAS_CANCEL_PERFIL",
                HeaderText = "QAC"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField 
            {
                DataField = "QT_VAGAS_EVADID_PERFIL",
                HeaderText = "QEV"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField 
            {
                DataField = "QT_VAGAS_EXPULS_PERFIL",
                HeaderText = "QEX"
            });      
        }

        private void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView() 
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            int coAnoPerfil = ddlAno.SelectedValue != "" ? int.Parse(ddlAno.SelectedValue) : 0;

            var resultado = (from tb246 in TB246_UNIDADE_PERFIL_VAGAS.RetornaTodosRegistros()
                             join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb246.TB25_EMPRESA.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                            where (coEmp != 0 ? tb246.TB25_EMPRESA.CO_EMP == coEmp : coEmp == 0)
                            && (coAnoPerfil != 0 ? tb246.CO_ANO_PERFIL_VAGAS == coAnoPerfil : coAnoPerfil == 0)
                            && tb134.ADMUSUARIO.ideAdmUsuario == LoginAuxili.IDEADMUSUARIO
                            select new 
                            {
                                tb246.ID_UNIDADE_PERFIL_VAGAS, tb246.CO_ANO_PERFIL_VAGAS, tb246.TB25_EMPRESA.NO_FANTAS_EMP,
                                tb246.TB25_EMPRESA.CO_EMP, tb246.QT_VAGAS_PLANEJ_PERFIL, tb246.QT_VAGAS_RESERV_PERFIL,
                                tb246.QT_VAGAS_MATRIC_PERFIL, tb246.QT_VAGAS_RENOVA_PERFIL, tb246.QT_VAGAS_ATIVOS_PERFIL,
                                tb246.QT_VAGAS_TRANSF_PERFIL, tb246.QT_VAGAS_CANCEL_PERFIL, tb246.QT_VAGAS_EVADID_PERFIL, tb246.QT_VAGAS_EXPULS_PERFIL                
                            }).OrderBy( u => u.NO_FANTAS_EMP ).ThenByDescending( u => u.CO_ANO_PERFIL_VAGAS );

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        private void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_UNIDADE_PERFIL_VAGAS"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown

//====> Método que carrega o DropDown de Unidades Escolares
        private void CarregaUnidades()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                     where tb134.ADMUSUARIO.ideAdmUsuario == Library.Auxiliares.LoginAuxili.IDEADMUSUARIO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).Distinct().OrderBy(e => e.NO_FANTAS_EMP);

            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.Items.Insert(0, new ListItem("Todos", ""));
        }

//====> Método que carrega o DropDown de Anos
        private void CarregaAno()
        {
            ddlAno.DataSource = (from tb246 in TB246_UNIDADE_PERFIL_VAGAS.RetornaTodosRegistros()
                                 where tb246.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == LoginAuxili.ORG_CODIGO_ORGAO
                                 select new { tb246.CO_ANO_PERFIL_VAGAS }).OrderByDescending( u => u.CO_ANO_PERFIL_VAGAS ).Distinct();

            ddlAno.DataValueField = "CO_ANO_PERFIL_VAGAS";
            ddlAno.DataTextField = "CO_ANO_PERFIL_VAGAS";
            ddlAno.DataBind();

            ddlAno.Items.Insert(0, new ListItem("Todos", ""));
        }
        #endregion
    }
}
