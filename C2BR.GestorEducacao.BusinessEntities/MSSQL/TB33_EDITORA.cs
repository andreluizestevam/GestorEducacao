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
    public partial class TB33_EDITORA
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
        /// Exclue o registro da tabela TB33_EDITORA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB33_EDITORA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB33_EDITORA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB33_EDITORA.</returns>
        public static TB33_EDITORA Delete(TB33_EDITORA entity)
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
        public static int SaveOrUpdate(TB33_EDITORA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB33_EDITORA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB33_EDITORA.</returns>
        public static TB33_EDITORA SaveOrUpdate(TB33_EDITORA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB33_EDITORA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB33_EDITORA.</returns>
        public static TB33_EDITORA GetByEntityKey(EntityKey entityKey)
        {
            return (TB33_EDITORA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB33_EDITORA, ordenados pelo nome "NO_EDITORA".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB33_EDITORA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB33_EDITORA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB33_EDITORA.OrderBy( e => e.NO_EDITORA ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB33_EDITORA pela chave primária "CO_EDITORA".
        /// </summary>
        /// <param name="CO_EDITORA">Id da chave primária</param>
        /// <returns>Entidade TB33_EDITORA</returns>
        public static TB33_EDITORA RetornaPelaChavePrimaria(int CO_EDITORA)
        {
            return (from tb33 in RetornaTodosRegistros()
                    where tb33.CO_EDITORA == CO_EDITORA
                    select tb33).FirstOrDefault();
        }

        #endregion
    }
}