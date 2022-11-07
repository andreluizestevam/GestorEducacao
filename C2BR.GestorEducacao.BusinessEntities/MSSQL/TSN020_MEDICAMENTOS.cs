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
    public partial class TSN020_MEDICAMENTOS
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
        /// Exclue o registro da tabela TSN020_MEDICAMENTOS do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TSN020_MEDICAMENTOS entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TSN020_MEDICAMENTOS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TSN001_ARTIGOS.</returns>
        public static TSN020_MEDICAMENTOS Delete(TSN020_MEDICAMENTOS entity)
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
        public static int SaveOrUpdate(TSN020_MEDICAMENTOS entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TSN020_MEDICAMENTOS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TSN001_ARTIGOS.</returns>
        public static TSN020_MEDICAMENTOS SaveOrUpdate(TSN020_MEDICAMENTOS entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TSN020_MEDICAMENTOS de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TSN020_MEDICAMENTOS.</returns>
        public static TSN020_MEDICAMENTOS GetByEntityKey(EntityKey entityKey)
        {
            return (TSN020_MEDICAMENTOS)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TSN020_MEDICAMENTOS, ordenados pelo Id "CO_MEDICAMENTOS".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TSN020_MEDICAMENTOS de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TSN020_MEDICAMENTOS> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TSN020_MEDICAMENTOS.OrderBy(u => u.CO_MEDICAMENTOS).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TSN020_MEDICAMENTOS pela chave primária "DE_PRINCIPIO_ATIVO".
        /// </summary>
        /// <param name="CO_ARTIGOS">Id da chave primária</param>
        /// <returns>Entidade TSN020_MEDICAMENTOS</returns>
        public static TSN020_MEDICAMENTOS RetornaPelaChavePrimaria(string DE_PRINCIPIO_ATIVO)
        {
            return (from TSN020 in RetornaTodosRegistros()
                    where TSN020.DE_PRINCIPIO_ATIVO == DE_PRINCIPIO_ATIVO
                    select TSN020).FirstOrDefault();
        }

        #endregion
    }
}
