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
    public partial class TSN002_BANNER
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
        /// Exclue o registro da tabela TSN002_BANNER do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TSN002_BANNER entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TSN002_BANNER na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TSN002_BANNER.</returns>
        public static TSN002_BANNER Delete(TSN002_BANNER entity)
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
        public static int SaveOrUpdate(TSN002_BANNER entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TSN002_BANNER na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TSN002_BANNER.</returns>
        public static TSN002_BANNER SaveOrUpdate(TSN002_BANNER entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TSN002_BANNER de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TSN002_BANNER.</returns>
        public static TSN002_BANNER GetByEntityKey(EntityKey entityKey)
        {
            return (TSN002_BANNER)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TSN002_BANNER, ordenados pelo Id "CO_BANNER".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TSN002_BANNER de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TSN002_BANNER> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TSN002_BANNER.OrderBy(u => u.CO_BANNER).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TSN002_BANNER pela chave primária "DE_BANNER".
        /// </summary>
        /// <param name="NM_USUARIO">Id da chave primária</param>
        /// <returns>Entidade TSN002_BANNER</returns>
        public static TSN002_BANNER RetornaPelaChavePrimaria(string DE_BANNER)
        {
            return (from TSN002 in RetornaTodosRegistros()
                    where TSN002.DE_BANNER == DE_BANNER
                    select TSN002).FirstOrDefault();
        }

        #endregion
    }
}
