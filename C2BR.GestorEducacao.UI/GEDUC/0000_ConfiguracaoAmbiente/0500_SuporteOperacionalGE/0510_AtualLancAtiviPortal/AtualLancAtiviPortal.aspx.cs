//=============================================================================
// EMPRESA: C2BR Soluções em Tecnologia
// SISTEMA: PE - Portal Educação
// PROGRAMADOR: Equipe Desenvolvimento
// MÓDULO: CONFIGURAÇÃO AMBIENTE E ATUALIZAÇÃO BD
// SUBMÓDULO: *****
// OBJETIVO: *****
// DATA DE CRIAÇÃO: 
//-----------------------------------------------------------------------------
//                           HISTÓRICO ATIVIDADES DE MANUTENÇÃO
//-----------------------------------------------------------------------------
//  DATA     |  NOME DO PROGRAMADOR       | DESCRIÇÃO RESUMIDA
// ----------+----------------------------+-------------------------------------
//           |                            | 

//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.UI.App_Masters;
using C2BR.GestorEducacao.UI.Library.Auxiliares;
using System.Data;
using System.IO;
using System.Text;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using Resources;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.Configuration;
using System.Collections.Generic;


namespace C2BR.GestorEducacao.UI.GEDUC._0000_ConfiguracaoAmbiente._0500_SuporteOperacionalGE._0510_AtualLancAtiviPortal
{
    public partial class AtualLancAtiviPortal : System.Web.UI.Page
    {
        public PadraoGenericas CurrentPadraoBuscas { get { return (App_Masters.PadraoGenericas)Master; } }


        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            //CurrentPadraoBuscas.DefineMensagem("", "Selecione um ou mais itens no quadro abaixo, escolha uma das ações de execução <br /> (botões abaixo do quadro) e clique para executar.");
            CurrentPadraoBuscas.DefineMensagem("", "Clique no botão abaixo para importação dos dados <br /> de lançamento de atividades oriundos do portal.");
        }

        #endregion

        #region Métodos

        private void HabilitaCampos(bool habilita)
        {
            lnkAtualBP.Enabled = habilita;

            if (habilita)
            {
                liLnkAtualBD.Style.Add("display", "block");
            }
        }

        private string RetornaTipoAtividade(string siglaTipoAtiv)
        {
            string descTipoAtiv = "";

            if (siglaTipoAtiv == "ANO")
            {
                descTipoAtiv = "Aula Normal";
            }
            else if (siglaTipoAtiv == "AEX")
            {
                descTipoAtiv = "Aula Extra";
            }
            else if (siglaTipoAtiv == "ARE")
            {
                descTipoAtiv = "Aula Reforço";
            }
            else if (siglaTipoAtiv == "ARC")
            {
                descTipoAtiv = "Aula de Recuperação";
            }
            else if (siglaTipoAtiv == "TES")
            {
                descTipoAtiv = "Teste";
            }
            else if (siglaTipoAtiv == "PRO")
            {
                descTipoAtiv = "Prova";
            }
            else if (siglaTipoAtiv == "TRA")
            {
                descTipoAtiv = "Trabalho";
            }
            else if (siglaTipoAtiv == "AGR")
            {
                descTipoAtiv = "Atividade em Grupo";
            }
            else if (siglaTipoAtiv == "ATE")
            {
                descTipoAtiv = "Atividade Externa";
            }
            else if (siglaTipoAtiv == "ATI")
            {
                descTipoAtiv = "Atividade Interna";
            }
            else if (siglaTipoAtiv == "OUT")
            {
                descTipoAtiv = "Outros";
            }

            return descTipoAtiv;
        }

        #endregion


        protected void lnkAtualBP_Click(object sender, EventArgs e)
        {
            HabilitaCampos(false);

            var portal = new BasePortal();
            var ctx = GestorEntities.CurrentContext;
            DateTime dataPadrao = new DateTime(2010, 01, 01, 1, 1, 1);
            DateTime dataPadraoAtual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 1, 1, 1);
            string cnpjInst = TB000_INSTITUICAO.RetornaPelaChavePrimaria(LoginAuxili.ORG_CODIGO_ORGAO).ORG_NUMERO_CNPJ.ToString("00000000000000");
            var GW554 = (from ativ in portal.GW554_ATIV_PROF_TURMA
                         where ativ.FL_BAIXA_NOTA == "N"
                         select ativ);


            #region Notas e Médias do Aluno

            foreach (var linha in GW554)
            {
                int CO_EMP = linha.CO_EMP;
                int CO_CUR = linha.CO_CUR;
                int CO_TUR = linha.CO_TUR;
                int CO_COL = linha.CO_COL;
                int CO_MAT = linha.CO_MAT;
                // Campo auto-increment, por isso não precisa ir para o Gestor
                //int CO_ATIV_PROF_TUR = int.Parse(reader["CO_ATIV_PROF_TUR"].ToString());
                int CO_COL_ATIV = linha.CO_COL_ATIV;
                DateTime DT_ATIV_REAL = linha.DT_ATIV_REAL;
                DateTime DT_ATIV_REAL_TERM = linha.DT_ATIV_REAL_TERM;
                string CO_TIPO_ATIV = linha.CO_TIPO_ATIV;
                string DE_TEMA_AULA = linha.DE_TEMA_AULA;
                string DE_FORMA_AVALI_ATIV = linha.DE_FORMA_AVALI_ATIV;
                bool FLA_AULA_PLAN = linha.FLA_AULA_PLAN;
                string FL_AVALIA_ATIV = linha.FL_AVALIA_ATIV;
                string FL_LANCA_NOTA = linha.FL_LANCA_NOTA;
                DateTime created = linha.created;
                int CO_PLA_AULA = linha.CO_PLA_AULA;
                string CO_ANO_MES_MAT = linha.CO_ANO_MES_MAT;
                int CO_MODU_CUR = linha.CO_MODU_CUR;
                string NU_SEM_LET = linha.NU_SEM_LET;
                string HR_INI_ATIV = linha.HR_INI_ATIV;
                string HR_TER_ATIV = linha.HR_TER_ATIV;
                string DE_RES_ATIV = linha.DE_RES_ATIV;
                string CO_IP_CADAST = linha.CO_IP_CADAST;
                int ID_TIPO_ATIV = linha.ID_TIPO_ATIV;
                string CO_REFER_ATIV = linha.CO_REFER_ATIV;


                #region Lançamento das atividades do professor

                TB119_ATIV_PROF_TURMA tb119 = new TB119_ATIV_PROF_TURMA();

                tb119.CO_EMP = CO_EMP;
                tb119.CO_CUR = CO_CUR;
                tb119.CO_TUR = CO_TUR;
                tb119.CO_COL = CO_COL;
                tb119.CO_MAT = CO_MAT;
                tb119.CO_COL_ATIV = CO_COL_ATIV;
                tb119.DT_ATIV_REAL = DT_ATIV_REAL;
                tb119.DT_ATIV_REAL_TERM = DT_ATIV_REAL_TERM;
                tb119.CO_TIPO_ATIV = CO_TIPO_ATIV;
                tb119.DE_TEMA_AULA = DE_TEMA_AULA;
                tb119.DE_FORMA_AVALI_ATIV = DE_FORMA_AVALI_ATIV;
                tb119.FLA_AULA_PLAN = FLA_AULA_PLAN;
                tb119.FL_AVALIA_ATIV = FL_AVALIA_ATIV;
                tb119.FL_LANCA_NOTA = FL_LANCA_NOTA;
                tb119.CO_PLA_AULA = CO_PLA_AULA;
                tb119.CO_ANO_MES_MAT = CO_ANO_MES_MAT;
                tb119.CO_MODU_CUR = CO_MODU_CUR;
                tb119.NU_SEM_LET = NU_SEM_LET;
                tb119.HR_INI_ATIV = HR_INI_ATIV;
                tb119.HR_TER_ATIV = HR_TER_ATIV;
                tb119.DE_RES_ATIV = DE_RES_ATIV;
                tb119.CO_IP_CADAST = CO_IP_CADAST;

                TB273_TIPO_ATIVIDADE tb273 = TB273_TIPO_ATIVIDADE.RetornaPelaChavePrimaria(ID_TIPO_ATIV);

                tb119.TB273_TIPO_ATIVIDADE = tb273;
                tb119.CO_REFER_ATIV = CO_REFER_ATIV;

                linha.ID_COLAB_BAIXA_NOTA = LoginAuxili.CO_COL;
                linha.NR_IP_COLAB_BAIXA_NOTA = LoginAuxili.IP_USU;
                linha.FL_BAIXA_NOTA = "S";
                linha.DT_BAIXA_NOTA = dataPadraoAtual;
                #endregion
            }
            #endregion


            string Error = string.Empty;
            try
            {
                portal.SaveChanges();
            }
            catch (Exception)
            {
                Error = "Erro ao salvar";
            }


            int idModulo = 0;
            if (!int.TryParse(HttpContext.Current.Session[SessoesHttp.IdModuloCorrente].ToString(), out idModulo))
            {
                HabilitaCampos(true);
                divTelaExportacaoCarregando.Style.Add("display", "none");
                AuxiliPagina.RedirecionaParaPaginaErro("Erro ao salvar na tabela de log de atividades.", Request.Url.AbsoluteUri);
            }

            TB236_LOG_ATIVIDADES tb236 = new TB236_LOG_ATIVIDADES();

            tb236.ORG_CODIGO_ORGAO = LoginAuxili.ORG_CODIGO_ORGAO;
            tb236.DT_ATIVI_LOG = DateTime.Now;
            tb236.CO_EMP_ATIVI_LOG = LoginAuxili.CO_EMP;
            tb236.IDEADMMODULO = idModulo;
            tb236.CO_ACAO_ATIVI_LOG = "A";
            tb236.CO_TABEL_ATIVI_LOG = "TB119_ATIV_PROF_TURMA";
            tb236.NR_IP_ACESS_ATIVI_LOG = LoginAuxili.IP_USU;
            tb236.NR_ACESS_ATIVI_LOG = LoginAuxili.QTD_ACESSO_USU + 1;
            tb236.CO_EMP = LoginAuxili.CO_UNID_FUNC;
            tb236.CO_COL = LoginAuxili.CO_COL;

            TB236_LOG_ATIVIDADES.SaveOrUpdate(tb236);

            GestorEntities.CurrentContext.SaveChanges();

            GC.Collect();
            GC.WaitForPendingFinalizers();

            HabilitaCampos(true);
            divTelaExportacaoCarregando.Style.Add("display", "none");
            if (Error == string.Empty)
            {
                AuxiliPagina.RedirecionaParaPaginaSucesso("Base de dados atualizada com sucesso. Tabela modificada: TB119_ATIV_PROF_TURMA", Request.Url.AbsoluteUri);
            }
            else
                AuxiliPagina.RedirecionaParaPaginaErro("Nenhum dado foi atualizado. Pois não existem registros para baixa na tabela de notas do aluno no portal.", Request.Url.AbsoluteUri);

        }
    }
}