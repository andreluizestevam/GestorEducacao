//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONTROLE OPERACIONAL PEDAGÓGICO
// SUBMÓDULO: CONTROLE OPERACIONAL DE REGIÕES DE ENSINO
// OBJETIVO: CADASTRAMENTO DE EQUIPES DE REGIÕES DE ENSINO ESCOLAR
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
namespace C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3800_CtrlEncerramentoLetivo.F3810_CtrlRegioesEnsino.F3812_CadastramentoRegiaoEquipeEnsino
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

        void CurrentPadraoBuscas_OnDefineColunasGridView()
        {
            CurrentPadraoBuscas.GridBusca.DataKeyNames = new string[] { "CO_EQUIP_NUCLEO" };

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_COL",
                HeaderText = "Nome do Funcionário"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "DE_NUCLEO",
                HeaderText = "Descrição do Núcleo"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "NO_SIGLA_NUCLEO",
                HeaderText = "Sigla Núcleo"
            });

            CurrentPadraoBuscas.GridBusca.Columns.Add(new BoundField
            {
                DataField = "CO_STATUS",
                HeaderText = "Status"
            });
        }

        void CurrentPadraoBuscas_OnAcaoBuscaDefineGridView()
        {
            var resultado = (from tbEquipNucInst in TB_EQUIPE_NUCLEO_INST.RetornaTodosRegistros()
                            where (txtDE_NUCLEO.Text != "" ? tbEquipNucInst.TB_NUCLEO_INST.DE_NUCLEO.Contains(txtDE_NUCLEO.Text) : txtDE_NUCLEO.Text == "")
                            || (txtDE_NUCLEO.Text != "" ? tbEquipNucInst.TB_NUCLEO_INST.NO_SIGLA_NUCLEO.Contains(txtDE_NUCLEO.Text) : txtDE_NUCLEO.Text == "")
                            && tbEquipNucInst.CO_EMP_COL == LoginAuxili.CO_EMP 
                            join tb03 in TB03_COLABOR.RetornaTodosRegistros() on tbEquipNucInst.CO_COL equals tb03.CO_COL
                            select new
                            {
                                 tbEquipNucInst.CO_EQUIP_NUCLEO, tbEquipNucInst.CO_FUN, tb03.NO_COL,
                                 DE_NUCLEO = tbEquipNucInst.TB_NUCLEO_INST.DE_NUCLEO, NO_SIGLA_NUCLEO = tbEquipNucInst.TB_NUCLEO_INST.NO_SIGLA_NUCLEO,
                                 CO_STATUS = (tbEquipNucInst.CO_STATUS == "A" ? "Ativo" : "Inativo")
                            }).OrderBy(r => r.NO_COL);
            
            CurrentPadraoBuscas.GridBusca.DataSource = (resultado.Count() > 0) ? resultado : null;
        }

        void CurrentPadraoBuscas_OnDefineQueryStringIds()
        {
            List<KeyValuePair<string, string>> queryStringKeys = new List<KeyValuePair<string, string>>();
            queryStringKeys.Add(new KeyValuePair<string, string>(QueryStrings.Id, "CO_EQUIP_NUCLEO"));

            CurrentPadraoBuscas.DefineQueryStringIdsFromDataKeyNames(queryStringKeys);
        }
        #endregion
    }
}
