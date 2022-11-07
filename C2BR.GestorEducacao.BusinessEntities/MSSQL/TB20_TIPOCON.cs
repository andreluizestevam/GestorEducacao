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
    public partial class TB20_TIPOCON
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
        /// Exclue o registro da tabela TB20_TIPOCON do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB20_TIPOCON entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB20_TIPOCON na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB20_TIPOCON.</returns>
        public static TB20_TIPOCON Delete(TB20_TIPOCON entity)
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
        public static int SaveOrUpdate(TB20_TIPOCON entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB20_TIPOCON na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB20_TIPOCON.</returns>
        public static TB20_TIPOCON SaveOrUpdate(TB20_TIPOCON entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB20_TIPOCON de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB20_TIPOCON.</returns>
        public static TB20_TIPOCON GetByEntityKey(EntityKey entityKey)
        {
            return (TB20_TIPOCON)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB20_TIPOCON, ordenados pelo nome "NO_TPCON".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB20_TIPOCON de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB20_TIPOCON> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB20_TIPOCON.OrderBy( t => t.NO_TPCON ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB20_TIPOCON pela chave primária "CO_TPCON".
        /// </summary>
        /// <param name="CO_TPCON">Id da chave primária</param>
        /// <returns>Entidade TB20_TIPOCON</returns>
        public static TB20_TIPOCON RetornaPelaChavePrimaria(int CO_TPCON)
        {
            return (from tb20 in RetornaTodosRegistros()
                    where tb20.CO_TPCON == CO_TPCON
                    select tb20).FirstOrDefault();
        }

        #endregion
    }
}