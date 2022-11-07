//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL SECRETARIA ESCOLAR
// SUBMÓDULO: CONTROLE DE TURMAS POR SÉRIES/CURSOS
// OBJETIVO: CADASTRAMENTO DE SALAS DE AULA
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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3020_CtrlPedagogicoTurmas.F3021_CadastramentoSalaAula
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

        void Page_Load()
        {
            if (IsPostBack) return;
                CarregaUnidade();
        }

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "ID_SALA_AULA" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField 
            { 
                DataField = "sigla", 
                HeaderText = "Unidade" 
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField 
            { 
                DataField = "CO_TIPO_SALA_AULA", 
                HeaderText = "Tipo" 
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_SALA_AULA",
                HeaderText = "Descrição"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField 
            { 
                DataField = "CO_IDENTI_SALA_AULA", 
                HeaderText = "Código" 
            });
            
            BoundField bf1 = new BoundField();
            bf1.DataField = "VL_AREA_SALA_AULA";
            bf1.HeaderText = "Area (M²)";
            bf1.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            bf1.DataFormatString = "{0:N2}";
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf1);            

            BoundField bf2 = new BoundField();
            bf2.DataField = "QT_ALUNOS_MAXIM_SALA_AULA";
            bf2.HeaderText = "Cap. Vagas";
            bf2.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf2);

            BoundField bf3 = new BoundField();
            bf3.DataField = "QT_VAGAS_DISP_SALA_AULA";
            bf3.HeaderText = "Disp. Vagas";
            bf3.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf3);

            BoundField bf4 = new BoundField();
            bf4.DataField = "QT_CADEIR_DISPON_SALA_AULA";
            bf4.HeaderText = "Disp. Cadeiras";
            bf4.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf4);
            /*
            BoundField bf5 = new BoundField();
            bf5.DataField = "QT_VENTIL_SALA_AULA";
            bf5.HeaderText = "Ventil.";
            bf5.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf5);

            BoundField bf6 = new BoundField();
            bf6.DataField = "QT_ARCOND_SALA_AULA";
            bf6.HeaderText = "Ar. Cond.";
            bf6.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            CurrentPadraoBuscas.GridBusca.Columns.Add(bf6);      */  
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            int coEmp = ddlUnidade.SelectedValue != "" ? int.Parse(ddlUnidade.SelectedValue) : 0;
            String strCoTipoSalaAula = ddlTipoSala.SelectedValue;

            var resultado = (from tb248 in TB248_UNIDADE_SALAS_AULA.RetornaTodosRegistros()
                             where (coEmp != 0 ? tb248.TB25_EMPRESA.CO_EMP == coEmp : coEmp == 0)
                             && (strCoTipoSalaAula != "T" ? tb248.CO_TIPO_SALA_AULA == strCoTipoSalaAula : strCoTipoSalaAula == "T")
                             && (txtDescSala.Text != "" ? tb248.DE_SALA_AULA.Contains(txtDescSala.Text) : txtDescSala.Text == "")
                             select new
                             {
                                 tb248.ID_SALA_AULA, tb248.TB25_EMPRESA.sigla, tb248.CO_IDENTI_SALA_AULA, tb248.QT_ALUNOS_MAXIM_SALA_AULA, tb248.DE_SALA_AULA,
                                 CO_TIPO_SALA_AULA = tb248.CO_TIPO_SALA_AULA == "A" ? "Aula" : tb248.CO_TIPO_SALA_AULA == "L" ? "Laboratório" : tb248.CO_TIPO_SALA_AULA == "E" ? "Estudo" : tb248.CO_TIPO_SALA_AULA == "M" ? "Monitoria" : "Outro",                                 
                                 VL_AREA_SALA_AULA = (tb248.VL_LARGUR_SALA_AULA != 0 && tb248.VL_COMPRI_SALA_AULA != 0) ? (tb248.VL_LARGUR_SALA_AULA * tb248.VL_COMPRI_SALA_AULA) : 0,
                                 QT_VAGAS_DISP_SALA_AULA = tb248.QT_ALUNOS_MAXIM_SALA_AULA - tb248.QT_ALUNOS_MATRIC_SALA_AULA, tb248.QT_VENTIL_SALA_AULA,
                                 QT_CADEIR_DISPON_SALA_AULA = tb248.QT_CADEIR_MAXIM_SALA_AULA - tb248.QT_CADEIR_DISPON_SALA_AULA, tb248.QT_ARCOND_SALA_AULA
                             }).Distinct().OrderBy( u => u.DE_SALA_AULA ).ThenBy( c => c.sigla );

            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "ID_SALA_AULA"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion

        #region Carregamento DropDown

//====> Método que carrega o DropDown de Unidades Escolares
        private void CarregaUnidade()
        {
            ddlUnidade.DataSource = (from tb25 in TB25_EMPRESA.RetornaTodosRegistros()
                                     join tb134 in TB134_USR_EMP.RetornaTodosRegistros() on tb25.CO_EMP equals tb134.TB25_EMPRESA.CO_EMP
                                     where tb134.ADMUSUARIO.ideAdmUsuario == LoginAuxili.IDEADMUSUARIO
                                     select new { tb25.NO_FANTAS_EMP, tb25.CO_EMP }).OrderBy( e => e.NO_FANTAS_EMP ).Distinct();

            ddlUnidade.DataTextField = "NO_FANTAS_EMP";
            ddlUnidade.DataValueField = "CO_EMP";
            ddlUnidade.DataBind();

            ddlUnidade.Items.Insert(0, new ListItem("Todas", ""));
            ddlUnidade.SelectedValue = LoginAuxili.CO_EMP.ToString();
        }
        #endregion
    }
}
