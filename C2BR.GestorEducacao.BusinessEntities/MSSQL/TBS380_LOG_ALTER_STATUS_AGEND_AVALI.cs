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
    public partial class TBS380_LOG_ALTER_STATUS_AGEND_AVALI
    {
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
        /// Exclue o registro da tabela TBS380_LOG_ALTER_STATUS_AGEND_AVALI do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS380_LOG_ALTER_STATUS_AGEND_AVALI entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS380_LOG_ALTER_STATUS_AGEND_AVALI na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS380_LOG_ALTER_STATUS_AGEND_AVALI.</returns>
        public static TBS380_LOG_ALTER_STATUS_AGEND_AVALI Delete(TBS380_LOG_ALTER_STATUS_AGEND_AVALI entity)
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
        public static int SaveOrUpdate(TBS380_LOG_ALTER_STATUS_AGEND_AVALI entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS380_LOG_ALTER_STATUS_AGEND_AVALI na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS380_LOG_ALTER_STATUS_AGEND_AVALI.</returns>
        public static TBS380_LOG_ALTER_STATUS_AGEND_AVALI SaveOrUpdate(TBS380_LOG_ALTER_STATUS_AGEND_AVALI entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS380_LOG_ALTER_STATUS_AGEND_AVALI de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS380_LOG_ALTER_STATUS_AGEND_AVALI.</returns>
        public static TBS380_LOG_ALTER_STATUS_AGEND_AVALI GetByEntityKey(EntityKey entityKey)
        {
            return (TBS380_LOG_ALTER_STATUS_AGEND_AVALI)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS380_LOG_ALTER_STATUS_AGEND_AVALI, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBS380_LOG_ALTER_STATUS_AGEND_AVALI de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS380_LOG_ALTER_STATUS_AGEND_AVALI> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS380_LOG_ALTER_STATUS_AGEND_AVALI.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS380_LOG_ALTER_STATUS_AGEND_AVALI pela chave primária "ID_LOG_ALTER_STATUS_AGEND_AVALI".
        /// </summary>
        /// <param name="ID_AGEND_HORAR">Id da chave primária</param>
        /// <returns>Entidade TBS380_LOG_ALTER_STATUS_AGEND_AVALI</returns>
        public static TBS380_LOG_ALTER_STATUS_AGEND_AVALI RetornaPelaChavePrimaria(int ID_LOG_ALTER_STATUS_AGEND_AVALI)
        {
            return (from tbs380 in RetornaTodosRegistros()
                    where tbs380.ID_LOG_ALTER_STATUS_AGEND_AVALI == ID_LOG_ALTER_STATUS_AGEND_AVALI
                    select tbs380).FirstOrDefault();
        }

        #endregion
    }
}
