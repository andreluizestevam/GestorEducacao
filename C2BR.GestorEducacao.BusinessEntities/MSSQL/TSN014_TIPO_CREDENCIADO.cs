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
    public partial class TSN014_TIPO_CREDENCIADO
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
        /// Exclue o registro da tabela TSN014_TIPO_CREDENCIADO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TSN014_TIPO_CREDENCIADO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TSN014_TIPO_CREDENCIADO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TSN014_TIPO_CREDENCIADO.</returns>
        public static TSN014_TIPO_CREDENCIADO Delete(TSN014_TIPO_CREDENCIADO entity)
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
        public static int SaveOrUpdate(TSN014_TIPO_CREDENCIADO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TSN014_TIPO_CREDENCIADO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TSN014_TIPO_CREDENCIADO.</returns>
        public static TSN014_TIPO_CREDENCIADO SaveOrUpdate(TSN014_TIPO_CREDENCIADO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TSN014_TIPO_CREDENCIADO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TSN014_TIPO_CREDENCIADO.</returns>
        public static TSN014_TIPO_CREDENCIADO GetByEntityKey(EntityKey entityKey)
        {
            return (TSN014_TIPO_CREDENCIADO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TSN014_TIPO_CREDENCIADO, ordenados pelo Id "CO_TIPO_CREDENCIADO".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB74_UF de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TSN014_TIPO_CREDENCIADO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TSN014_TIPO_CREDENCIADO.OrderBy(u => u.CO_TIPO_CREDENCIADO).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TSN014_TIPO_CREDENCIADO pela chave primária "DE_TIPO_CREDENCIADO".
        /// </summary>
        /// <param name="DE_TIPO_CREDENCIADO">Id da chave primária</param>
        /// <returns>Entidade TSN014_TIPO_CREDENCIADO</returns>
        public static TSN014_TIPO_CREDENCIADO RetornaPelaChavePrimaria(string DE_TIPO_CREDENCIADO)
        {
            return (from TSN014 in RetornaTodosRegistros()
                    where TSN014.DE_TIPO_CREDENCIADO == DE_TIPO_CREDENCIADO
                    select TSN014).FirstOrDefault();
        }

        #endregion
    }
}
