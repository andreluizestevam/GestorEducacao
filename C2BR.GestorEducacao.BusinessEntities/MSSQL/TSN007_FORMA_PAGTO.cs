//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Text;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;

namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
   public partial class TSN007_FORMA_PAGTO
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
        /// Exclue o registro da tabela TSN007_FORMA_PAGTO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TSN007_FORMA_PAGTO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TSN007_FORMA_PAGTO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TSN007_FORMA_PAGTO.</returns>
        public static TSN007_FORMA_PAGTO Delete(TSN007_FORMA_PAGTO entity)
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
        public static int SaveOrUpdate(TSN007_FORMA_PAGTO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TSN007_FORMA_PAGTO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TSN007_FORMA_PAGTO.</returns>
        public static TSN007_FORMA_PAGTO SaveOrUpdate(TSN007_FORMA_PAGTO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TSN007_FORMA_PAGTO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TSN007_FORMA_PAGTO.</returns>
        public static TSN007_FORMA_PAGTO GetByEntityKey(EntityKey entityKey)
        {
            return (TSN007_FORMA_PAGTO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TSN007_FORMA_PAGTO, ordenados pelo Id "TSN007".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TSN007_FORMA_PAGTO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TSN007_FORMA_PAGTO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TSN007_FORMA_PAGTO.OrderBy(u => u.CO_FORMA_PAGTO).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TSN007_FORMA_PAGTO pela chave primária "TSN007".
        /// </summary>
        /// <param name="CODUF">Id da chave primária</param>
        /// <returns>Entidade TSN007_FORMA_PAGTO</returns>
        public static TSN007_FORMA_PAGTO RetornaPelaChavePrimaria(string DE_FORMA)
        {
            return (from TSN007 in RetornaTodosRegistros()
                    where TSN007.DE_FORMA == DE_FORMA
                    select TSN007).FirstOrDefault();
        }

        #endregion
    }
}
