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
    public partial class TBS175_LOG_ALTER_AGENDA
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
        /// Exclue o registro da tabela TB906_REGIAO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS175_LOG_ALTER_AGENDA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB906_REGIAO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB906_REGIAO.</returns>
        public static TBS175_LOG_ALTER_AGENDA Delete(TBS175_LOG_ALTER_AGENDA entity)
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
        public static int SaveOrUpdate(TBS175_LOG_ALTER_AGENDA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB906_REGIAO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB906_REGIAO.</returns>
        public static TBS175_LOG_ALTER_AGENDA SaveOrUpdate(TBS175_LOG_ALTER_AGENDA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB906_REGIAO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB906_REGIAO.</returns>
        public static TBS175_LOG_ALTER_AGENDA GetByEntityKey(EntityKey entityKey)
        {
            return (TBS175_LOG_ALTER_AGENDA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB906_REGIAO, ordenados pelo nome "NO_CIDADE".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB906_REGIAO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS175_LOG_ALTER_AGENDA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS175_LOG_ALTER_AGENDA.OrderBy(r => r.DT_LOG).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB906_REGIAO pela chave primária "CO_CIDADE".
        /// </summary>
        /// <param name="CO_CIDADE">Id da chave primária</param>
        /// <returns>Entidade TB906_REGIAO</returns>
        public static TBS175_LOG_ALTER_AGENDA RetornaPelaChavePrimaria(int ID_LOG_ALTER_AGENDA_PLANTAO)
        {
            return (from tbs175 in RetornaTodosRegistros()
                    where tbs175.ID_LOG_ALTER_AGENDA_PLANTAO == ID_LOG_ALTER_AGENDA_PLANTAO
                    select tbs175).FirstOrDefault();
        }

        /// <summary>
        /// Retorna todos os registros da entidade TB906_REGIAO de acordo com a UF "CO_UF".
        /// </summary>
        /// <param name="CO_UF">Id da UF</param>
        /// <returns>ObjectQuery com todos os registros da entidade TB906_REGIAO de acordo com a filtragem desenvolvida.</returns>

        #endregion
    }
}
