//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Text;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2BR.GestorEducacao.BusinessEntities.MSSQL;


namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class TSN012_CREDENCIADOS
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
        /// Exclue o registro da tabela TSN012_CREDENCIADOS do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TSN012_CREDENCIADOS entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TSN012_CREDENCIADOS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TSN012_CREDENCIADOS.</returns>
        public static TSN012_CREDENCIADOS Delete(TSN012_CREDENCIADOS entity)
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
        public static int SaveOrUpdate(TSN012_CREDENCIADOS entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TSN012_CREDENCIADOS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TSN012_CREDENCIADOS.</returns>
        public static TSN012_CREDENCIADOS SaveOrUpdate(TSN012_CREDENCIADOS entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TSN012_CREDENCIADOS de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TSN012_CREDENCIADOS.</returns>
        public static TSN012_CREDENCIADOS GetByEntityKey(EntityKey entityKey)
        {
            return (TSN012_CREDENCIADOS)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TSN012_CREDENCIADOS, ordenados pelo Id "CO_CREDENCIADO".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TSN012 de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TSN012_CREDENCIADOS> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TSN012_CREDENCIADOS.OrderBy(u => u.CO_CREDENCIADO).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TSN012_CREDENCIADOS pela chave primária "CO_CREDENCIADO".
        /// </summary>
        /// <param name="CO_CREDENCIADO">Id da chave primária</param>
        /// <returns>Entidade TSN012_CREDENCIADOS</returns>
        public static TSN012_CREDENCIADOS RetornaPelaChavePrimaria(string NM_PRESTADOR)
        {

            return (from TSN012 in RetornaTodosRegistros()
                    where TSN012.NM_PRESTADOR == NM_PRESTADOR
                    select TSN012).FirstOrDefault();
        }

        #endregion
    }
}
