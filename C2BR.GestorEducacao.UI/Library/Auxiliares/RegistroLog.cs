//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
using System.Linq;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Data.Objects.DataClasses;
using Resources;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.Library.Auxiliares
{
    public class RegistroLog
    {
        #region Variáveis

        public const string NENHUMA_ACAO = "X";
        public const string ACAO_GRAVAR = "G";
        public const string ACAO_EDICAO = "E";
        public const string ACAO_DELETE = "D";
        public const string ACAO_PESQUISA = "P";
        public const string ACAO_RELATORIO = "R";
        #endregion

        #region Métodos

        /// <summary>
        /// Método que verifica se existe código do usuário e se a ação aconteceu para chamada de outro método que registra na tabela de LOG (TB236_LOG_ATIVIDADES)
        /// </summary>
        /// <param name="tabelaUtilizada">Tabela que foi utilizada</param>
        /// <param name="acaoUsuario">Ação do usuário</param>
        public void RegistroLOG(EntityObject tabelaUtilizada, string acaoUsuario) 
        {
            if (LoginAuxili.CO_COL > 0)
            {
                string URLReferencia = HttpContext.Current.Request.Url.ToString().Split('?')[0];

                if (acaoUsuario == NENHUMA_ACAO)
                    if (URLReferencia == HttpContext.Current.Session[SessoesHttp.UltimaURLAcessada] as String)
                        return;

                HttpContext.Current.Session[SessoesHttp.UltimaURLAcessada] = URLReferencia;
                AtualizaLOG(tabelaUtilizada, acaoUsuario);
            }            
        }

        /// <summary>
        /// Método que faz a inserção da atividade do usuário na tabela de LOG.
        /// </summary>
        /// <param name="tabelaUtilizada">Tabela que foi utilizada</param>
        /// <param name="acaoUsuario">Ação do usuário</param>
        private void AtualizaLOG(EntityObject tabelaUtilizada, string acaoUsuario) 
        {
            if (HttpContext.Current.Session[SessoesHttp.IdModuloCorrente] != null)
            {
                TB236_LOG_ATIVIDADES tb236 = new TB236_LOG_ATIVIDADES();

                tb236.ORG_CODIGO_ORGAO = LoginAuxili.ORG_CODIGO_ORGAO;
                tb236.DT_ATIVI_LOG = DateTime.Now;
                tb236.CO_EMP_ATIVI_LOG = LoginAuxili.CO_EMP;
                int idModulo = 0;
                int.TryParse(HttpContext.Current.Session[SessoesHttp.IdModuloCorrente].ToString(), out idModulo);
                tb236.IDEADMMODULO = idModulo;
                tb236.CO_ACAO_ATIVI_LOG = acaoUsuario;
                tb236.CO_TABEL_ATIVI_LOG = tabelaUtilizada == null ? null : tabelaUtilizada.GetType().Name;            
                tb236.NR_IP_ACESS_ATIVI_LOG = LoginAuxili.IP_USU;
                tb236.NR_ACESS_ATIVI_LOG = LoginAuxili.QTD_ACESSO_USU + 1;
                tb236.CO_EMP = LoginAuxili.CO_UNID_FUNC;
                tb236.CO_COL = LoginAuxili.CO_COL;

                TB236_LOG_ATIVIDADES.SaveOrUpdate(tb236, true);   
            }
        }
        #endregion
    }
}