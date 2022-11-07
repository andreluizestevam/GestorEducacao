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
    public partial class TSN017_TIPO_SERVICO
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
        /// Exclue o registro da tabela TSN017 do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TSN017_TIPO_SERVICO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB74_UF na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB74_UF.</returns>
        public static TSN017_TIPO_SERVICO Delete(TSN017_TIPO_SERVICO entity)
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
        public static int SaveOrUpdate(TSN017_TIPO_SERVICO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TSN017 na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TSN017.</returns>
        public static TSN017_TIPO_SERVICO SaveOrUpdate(TSN017_TIPO_SERVICO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TSN017 de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TSN017.</returns>
        public static TSN017_TIPO_SERVICO GetByEntityKey(EntityKey entityKey)
        {
            return (TSN017_TIPO_SERVICO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TSN017, ordenados pelo Id "DE_SERVICO".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TSN017 de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TSN017_TIPO_SERVICO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TSN017_TIPO_SERVICO.OrderBy(u => u.CO_TIPO_SERVICO).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TSN017 pela chave primária "DE_SERVICO".
        /// </summary>
        /// <param name="CODUF">Id da chave primária</param>
        /// <returns>Entidade TSN017</returns>
        public static TSN017_TIPO_SERVICO RetornaPelaChavePrimaria(string DE_SIGLA)
        {
            return (from TSN017 in RetornaTodosRegistros()
                    where TSN017.DE_SIGLA == DE_SIGLA
                    select TSN017).FirstOrDefault();
        }

        #endregion
    }
}
