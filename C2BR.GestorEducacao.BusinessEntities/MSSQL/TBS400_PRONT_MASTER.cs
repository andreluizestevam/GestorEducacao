using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;
using System.Data.Objects;
using System.Data;

namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class TBS400_PRONT_MASTER
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
        /// Exclue o registro da tabela TB03_COLABOR do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS400_PRONT_MASTER entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB03_COLABOR na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB03_COLABOR.</returns>
        public static TBS400_PRONT_MASTER Delete(TBS400_PRONT_MASTER entity)
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
        public static int SaveOrUpdate(TBS400_PRONT_MASTER entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

      
        public static TBS400_PRONT_MASTER SaveOrUpdate(TBS400_PRONT_MASTER entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB03_COLABOR de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB03_COLABOR.</returns>
        public static TBS400_PRONT_MASTER GetByEntityKey(EntityKey entityKey)
        {
            return (TBS400_PRONT_MASTER)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS400_PRONT_MASTER, ordenados pelo nome "NO_COL".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB03_COLABOR de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS400_PRONT_MASTER> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS400_PRONT_MASTER.OrderBy(c => c.TBS390_ATEND_AGEND.NU_REGIS).AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS400_PRONT_MASTER pela chave primária "ID_PRONT_MASTER".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TBS400_PRONT_MASTER</returns>
        public static TBS400_PRONT_MASTER RetornaPelaChavePrimaria(int idPront)
        {
            return (from tbs400 in RetornaTodosRegistros()
                    where tbs400.ID_PRONT_MASTER == idPront
                    select tbs400).FirstOrDefault();
        }

       
        #endregion
        #endregion
    }
}


