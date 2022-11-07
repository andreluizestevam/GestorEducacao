//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Web;
using System.Linq;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;
using System.Data.Objects.DataClasses;
using Resources;
using System.Collections.Generic;


//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.UI.Library.Auxiliares
{
    public class LogMovCaixa
    {

        #region Métodos

        /// <summary>
        /// Método que faz a inserção da atividade do usuário na tabela de LOG.
        /// </summary>
        /// <param name="tabelaUtilizada">Tabela que foi utilizada</param>
        /// <param name="acaoUsuario">Ação do usuário</param>
        public void AtualizaLOG(TB192_CAIXA_LOGMOV tb192)
        {
            // Informação padrão do log
            tb192.ORG_CODIGO_ORGAO = LoginAuxili.ORG_CODIGO_ORGAO;
            tb192.DT_LOG = DateTime.Now;
            tb192.CO_EMP_LOG = LoginAuxili.CO_EMP;
            tb192.NR_IP_ACESS_ATIVI_LOG = LoginAuxili.IP_USU;
            tb192.NR_ACESS_LOG = LoginAuxili.QTD_ACESSO_USU + 1;
            tb192.CO_EMP_LOG = LoginAuxili.CO_UNID_FUNC;
            tb192.CO_COL = LoginAuxili.CO_COL;

            TB192_CAIXA_LOGMOV.SaveOrUpdate(tb192, true);
        }
        #endregion
    }


    public class LogFormaPagamento {
    


    }

}