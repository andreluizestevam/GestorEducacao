using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Text;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;

namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class TBS461_ATEND_MODEL_PRESC_MEDIC
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
        /// Exclue o registro da tabela TBS461_ATEND_MODEL_PRESC_MEDIC do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS461_ATEND_MODEL_PRESC_MEDIC entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS461_ATEND_MODEL_PRESC_MEDIC na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS335_ISDA_TIPO.</returns>
        public static TBS461_ATEND_MODEL_PRESC_MEDIC Delete(TBS461_ATEND_MODEL_PRESC_MEDIC entity)
        {
            Delete(entity, true);

           return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Faz a verificação para saber se inserção ou alteração e executa a ação necessária; você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        ///// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int SaveOrUpdate(TBS461_ATEND_MODEL_PRESC_MEDIC entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS461_ATEND_MODEL_PRESC_MEDIC na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS335_ISDA_TIPO.</returns>
        public static TBS461_ATEND_MODEL_PRESC_MEDIC SaveOrUpdate(TBS461_ATEND_MODEL_PRESC_MEDIC entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS461_ATEND_MODEL_PRESC_MEDIC de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS461_ATEND_MODEL_PRESC_MEDIC.</returns>
        public static TBS461_ATEND_MODEL_PRESC_MEDIC GetByEntityKey(EntityKey entityKey)
        {
            return (TBS461_ATEND_MODEL_PRESC_MEDIC)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS461_ATEND_MODEL_PRESC_MEDIC, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBS334_DIAGN_ATEND_MEDIC de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS461_ATEND_MODEL_PRESC_MEDIC> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS461_ATEND_MODEL_PRESC_MEDIC.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS461_ATEND_MODEL_PRESC_MEDIC pela chave primária "ID_PAGTO_CHEQUE".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TBS461_ATEND_MODEL_PRESC_MEDIC</returns>
        public static TBS461_ATEND_MODEL_PRESC_MEDIC RetornaPelaChavePrimaria(int ID_MODEL_MEDIC)
        {
            return (from tbs461 in RetornaTodosRegistros()
                    where tbs461.ID_MODEL_MEDIC == ID_MODEL_MEDIC
                    select tbs461).FirstOrDefault();
        }

        #endregion
    }
}
