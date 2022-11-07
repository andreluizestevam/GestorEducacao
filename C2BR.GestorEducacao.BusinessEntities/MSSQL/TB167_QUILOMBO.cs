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
    public partial class TB167_QUILOMBO
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
        /// Exclue o registro da tabela TB167_QUILOMBO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB167_QUILOMBO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB167_QUILOMBO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB167_QUILOMBO.</returns>
        public static TB167_QUILOMBO Delete(TB167_QUILOMBO entity)
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
        public static int SaveOrUpdate(TB167_QUILOMBO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB167_QUILOMBO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB167_QUILOMBO.</returns>
        public static TB167_QUILOMBO SaveOrUpdate(TB167_QUILOMBO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB167_QUILOMBO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB167_QUILOMBO.</returns>
        public static TB167_QUILOMBO GetByEntityKey(EntityKey entityKey)
        {
            return (TB167_QUILOMBO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB167_QUILOMBO, ordenados pelo nome "NO_QUI".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB167_QUILOMBO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB167_QUILOMBO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB167_QUILOMBO.OrderBy( q => q.NO_QUI ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB167_QUILOMBO pela chave primária "CO_QUI".
        /// </summary>
        /// <param name="CO_QUI">Id da chave primária</param>
        /// <returns>Entidade TB167_QUILOMBO</returns>
        public static TB167_QUILOMBO RetornaPelaChavePrimaria(int CO_QUI)
        {
            return (from tb167 in RetornaTodosRegistros()
                    where tb167.CO_QUI == CO_QUI
                    select tb167).FirstOrDefault();
        }

        #endregion
    }
}