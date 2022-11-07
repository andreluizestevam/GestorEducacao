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
    public partial class TB210_HIST_DIPLOMA
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
        /// Exclue o registro da tabela TB210_HIST_DIPLOMA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB210_HIST_DIPLOMA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB210_HIST_DIPLOMA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB210_HIST_DIPLOMA.</returns>
        public static TB210_HIST_DIPLOMA Delete(TB210_HIST_DIPLOMA entity)
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
        public static int SaveOrUpdate(TB210_HIST_DIPLOMA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB210_HIST_DIPLOMA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB210_HIST_DIPLOMA.</returns>
        public static TB210_HIST_DIPLOMA SaveOrUpdate(TB210_HIST_DIPLOMA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB210_HIST_DIPLOMA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB210_HIST_DIPLOMA.</returns>
        public static TB210_HIST_DIPLOMA GetByEntityKey(EntityKey entityKey)
        {
            return (TB210_HIST_DIPLOMA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB210_HIST_DIPLOMA, ordenados pelo Id "CO_HIST_DIPLOMA".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB210_HIST_DIPLOMA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB210_HIST_DIPLOMA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB210_HIST_DIPLOMA.OrderBy( h => h.CO_HIST_DIPLOMA ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB210_HIST_DIPLOMA pela chave primária "CO_HIST_DIPLOMA".
        /// </summary>
        /// <param name="CO_HIST_DIPLOMA">Id da chave primária</param>
        /// <returns>Entidade TB210_HIST_DIPLOMA</returns>
        public static TB210_HIST_DIPLOMA RetornaPelaChavePrimaria(int CO_HIST_DIPLOMA)
        {
            return (from tb210 in RetornaTodosRegistros()
                    where tb210.CO_HIST_DIPLOMA == CO_HIST_DIPLOMA
                    select tb210).FirstOrDefault();
        }

        #endregion
    }
}