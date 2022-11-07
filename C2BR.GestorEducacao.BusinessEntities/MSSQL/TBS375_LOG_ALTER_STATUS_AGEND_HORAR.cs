//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Text;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class TBS375_LOG_ALTER_STATUS_AGEND_HORAR
    {
        #region Métodos

        #region Métodos Básicos

        /// <summary>
        /// Salva as alterações do contexto na base de dados.
        /// </summary>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int SaveChanges()
        {
            return GestorEntities.CurrentContext.SaveChanges();
        }

        /// <summary>
        /// Exclue o registro da tabela TB905_BAIRRO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS375_LOG_ALTER_STATUS_AGEND_HORAR entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB905_BAIRRO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB905_BAIRRO.</returns>
        public static TBS375_LOG_ALTER_STATUS_AGEND_HORAR Delete(TBS375_LOG_ALTER_STATUS_AGEND_HORAR entity)
        {
            Delete(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Faz a verificação para saber se inserção ou alteração e executa a ação necessária; você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int SaveOrUpdate(TBS375_LOG_ALTER_STATUS_AGEND_HORAR entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB905_BAIRRO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB905_BAIRRO.</returns>
        public static TBS375_LOG_ALTER_STATUS_AGEND_HORAR SaveOrUpdate(TBS375_LOG_ALTER_STATUS_AGEND_HORAR entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB905_BAIRRO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB905_BAIRRO.</returns>
        public static TBS375_LOG_ALTER_STATUS_AGEND_HORAR GetByEntityKey(EntityKey entityKey)
        {
            return (TBS375_LOG_ALTER_STATUS_AGEND_HORAR)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB905_BAIRRO, ordenados pelo nome "NO_BAIRRO".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB905_BAIRRO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS375_LOG_ALTER_STATUS_AGEND_HORAR> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS375_LOG_ALTER_STATUS_AGEND_HORAR.OrderBy(b => b.DT_CADAS).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB905_BAIRRO pela chave primária "CO_BAIRRO".
        /// </summary>
        /// <param name="CO_BAIRRO">Id da chave primária</param>
        /// <returns>Entidade TB905_BAIRRO</returns>
        public static TBS375_LOG_ALTER_STATUS_AGEND_HORAR RetornaPelaChavePrimaria(int ID_LOG_ALTER_STATUS_AGEND_HORAR)
        {
            return (from tbs375 in RetornaTodosRegistros()
                    where tbs375.ID_LOG_ALTER_STATUS_AGEND_HORAR == ID_LOG_ALTER_STATUS_AGEND_HORAR
                    select tbs375).FirstOrDefault();
        }

        /// <summary>
        /// Retorna todos os registros da entidade TB905_BAIRRO de acordo com a cidade "CO_CIDADE".
        /// </summary>
        /// <param name="CO_CIDADE">Id da cidade</param>
        /// <returns>ObjectQuery com todos os registros da entidade TB905_BAIRRO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS375_LOG_ALTER_STATUS_AGEND_HORAR> RetornaPelaAgenda(int ID_AGEND_HORAR)
        {
            return (from tbs375 in RetornaTodosRegistros()
                    where tbs375.TBS174_AGEND_HORAR.ID_AGEND_HORAR == ID_AGEND_HORAR
                    select tbs375).OrderBy(b => b.DT_CADAS).AsObjectQuery();
        }

        #endregion
    }
}
