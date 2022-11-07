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
    public partial class TSN016_VALORES_SERVICO
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
        /// Exclue o registro da tabela TSN016_VALORES_SERVICO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TSN016_VALORES_SERVICO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TSN001_ARTIGOS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TSN001_ARTIGOS.</returns>
        public static TSN016_VALORES_SERVICO Delete(TSN016_VALORES_SERVICO entity)
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
        public static int SaveOrUpdate(TSN016_VALORES_SERVICO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TSN001_ARTIGOS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TSN001_ARTIGOS.</returns>
        public static TSN016_VALORES_SERVICO SaveOrUpdate(TSN016_VALORES_SERVICO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TSN001_ARTIGOS de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TSN001_ARTIGOS.</returns>
        public static TSN016_VALORES_SERVICO GetByEntityKey(EntityKey entityKey)
        {
            return (TSN016_VALORES_SERVICO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TSN001_ARTIGOS, ordenados pelo Id "CO_ARTIGOS".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB74_UF de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TSN016_VALORES_SERVICO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TSN016_VALORES_SERVICO.OrderBy(u => u.CO_VALORES_SERVICOS).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TSN016_VALORES_SERVICO pela chave primária "CO_ARTIGOS".
        /// </summary>
        /// <param name="CO_ARTIGOS">Id da chave primária</param>
        /// <returns>Entidade TSN001_ARTIGOS</returns>
        public static TSN016_VALORES_SERVICO RetornaPelaChavePrimaria(int CO_VALORES_SERVICOS)
        {

            return (from TSN016 in RetornaTodosRegistros()
                    where TSN016.CO_VALORES_SERVICOS == CO_VALORES_SERVICOS
                    select TSN016).FirstOrDefault();
        }

        #endregion
    }
}
