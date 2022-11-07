//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Text;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class TBS382_INFOS_GERAIS
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
        /// Exclue o registro da tabela TBS382_INFOS_GERAIS do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS382_INFOS_GERAIS entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS382_INFOS_GERAIS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS382_INFOS_GERAIS.</returns>
        public static TBS382_INFOS_GERAIS Delete(TBS382_INFOS_GERAIS entity)
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
        public static int SaveOrUpdate(TBS382_INFOS_GERAIS entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS380_LOG_ALTER_STATUS_AGEND_AVALI na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS382_INFOS_GERAIS.</returns>
        public static TBS382_INFOS_GERAIS SaveOrUpdate(TBS382_INFOS_GERAIS entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS382_INFOS_GERAIS de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS382_INFOS_GERAIS.</returns>
        public static TBS382_INFOS_GERAIS GetByEntityKey(EntityKey entityKey)
        {
            return (TBS382_INFOS_GERAIS)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS382_INFOS_GERAIS, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBS382_INFOS_GERAIS de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS382_INFOS_GERAIS> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS382_INFOS_GERAIS.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS382_INFOS_GERAIS pela chave primária "ID_INFOS_GERAIS".
        /// </summary>
        /// <param name="ID_AGEND_HORAR">Id da chave primária</param>
        /// <returns>Entidade TBS382_INFOS_GERAIS</returns>
        public static TBS382_INFOS_GERAIS RetornaPelaChavePrimaria(int ID_INFOS_GERAIS)
        {
            return (from tbs382 in RetornaTodosRegistros()
                    where tbs382.ID_INFOS_GERAIS == ID_INFOS_GERAIS
                    select tbs382).FirstOrDefault();
        }

        #endregion
    }
}
