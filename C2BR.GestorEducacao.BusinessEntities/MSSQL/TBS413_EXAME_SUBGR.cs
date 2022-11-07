using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;

namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class TBS413_EXAME_SUBGR
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
        /// Exclue o registro da tabela TBS413_EXAME_SUBGR do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS413_EXAME_SUBGR entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS413_EXAME_SUBGR na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS413_EXAME_SUBGR.</returns>
        public static TBS413_EXAME_SUBGR Delete(TBS413_EXAME_SUBGR entity)
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
        public static int SaveOrUpdate(TBS413_EXAME_SUBGR entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS413_EXAME_SUBGR na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS413_EXAME_SUBGR.</returns>
        public static TBS413_EXAME_SUBGR SaveOrUpdate(TBS413_EXAME_SUBGR entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS413_EXAME_SUBGR de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS413_EXAME_SUBGR.</returns>
        public static TBS413_EXAME_SUBGR GetByEntityKey(EntityKey entityKey)
        {
            return (TBS413_EXAME_SUBGR)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS413_EXAME_SUBGR.
        /// </summary>
        public static ObjectQuery<TBS413_EXAME_SUBGR> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS413_EXAME_SUBGR.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS413_EXAME_SUBGR pela chave primária "ID_EXAME".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TBS413_EXAME_SUBGR</returns>
        public static TBS413_EXAME_SUBGR RetornaPelaChavePrimaria(int ID_SUBGRUPO)
        {
            return (from tbs413 in RetornaTodosRegistros()
                    where tbs413.ID_SUBGRUPO == ID_SUBGRUPO
                    select tbs413).FirstOrDefault();
        }

        #endregion
    }
}

