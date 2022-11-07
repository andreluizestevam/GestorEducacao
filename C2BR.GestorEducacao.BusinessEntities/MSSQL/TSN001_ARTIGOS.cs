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
    public partial class TSN001_ARTIGOS
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
        /// Exclue o registro da tabela TSN001_ARTIGOS do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TSN001_ARTIGOS entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TSN001_ARTIGOS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TSN001_ARTIGOS.</returns>
        public static TSN001_ARTIGOS Delete(TSN001_ARTIGOS entity)
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
        public static int SaveOrUpdate(TSN001_ARTIGOS entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TSN001_ARTIGOS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TSN001_ARTIGOS.</returns>
        public static TSN001_ARTIGOS SaveOrUpdate(TSN001_ARTIGOS entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TSN001_ARTIGOS de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TSN001_ARTIGOS.</returns>
        public static TSN001_ARTIGOS GetByEntityKey(EntityKey entityKey)
        {
            return (TSN001_ARTIGOS)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TSN001_ARTIGOS, ordenados pelo Id "CO_ARTIGOS".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB74_UF de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TSN001_ARTIGOS> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TSN001_ARTIGOS.OrderBy(u => u.CO_ARTIGOS).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TSN001_ARTIGOS pela chave primária "CO_ARTIGOS".
        /// </summary>
        /// <param name="CO_ARTIGOS">Id da chave primária</param>
        /// <returns>Entidade TSN001_ARTIGOS</returns>
        public static TSN001_ARTIGOS RetornaPelaChavePrimaria(string DE_ARTIGO)
        {
            
            return (from TSN001 in RetornaTodosRegistros()
                    where TSN001.DE_ARTIGO == DE_ARTIGO
                    select TSN001).FirstOrDefault();
        }

        #endregion
    }
}
