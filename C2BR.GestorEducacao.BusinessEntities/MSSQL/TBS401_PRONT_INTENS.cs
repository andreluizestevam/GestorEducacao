using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Text;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;

namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
     public partial class TBS401_PRONT_INTENS
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
        /// Exclue o registro da tabela TBS401_PRONT_INTENS do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS401_PRONT_INTENS entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS372_AGEND_AVALI na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS401_PRONT_INTENS</returns>
        public static TBS401_PRONT_INTENS Delete(TBS401_PRONT_INTENS entity)
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
        public static int SaveOrUpdate(TBS401_PRONT_INTENS entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB30_AGENCIA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade ID_PRONT_ITENS.</returns>
        public static TBS401_PRONT_INTENS SaveOrUpdate(TBS401_PRONT_INTENS entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS372_AGEND_AVALI de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS401_PRONT_INTENS</returns>
        public static TBS401_PRONT_INTENS GetByEntityKey(EntityKey entityKey)
        {
            return (TBS401_PRONT_INTENS)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS401_PRONT_INTENS, ordenados pelo Id do Banco "IDEBANCO" e pelo Id da agência "CO_AGENCIA".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBS401_PRONT_INTENS de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS401_PRONT_INTENS> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS401_PRONT_INTENS.AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TBS372_AGEND_AVALI pelas chaves primárias "IDEBANCO" e "CO_AGENCIA".
        /// </summary>
        /// <param name="IDEBANCO">Id do banco</param>
        /// <param name="CO_AGENCIA">Código da agência</param>
        /// <returns>Entidade ID_PRONT_ITENS</returns>
        public static TBS401_PRONT_INTENS RetornaPelaChavePrimaria(int ID_PRONT_ITENS)
        {
            return (from tbs401 in RetornaTodosRegistros()
                    where tbs401.ID_PRONT_ITENS == ID_PRONT_ITENS
                    select tbs401).FirstOrDefault();
        }
        #endregion
    }
}
