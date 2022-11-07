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
    public partial class TB87_GRUPO_ITENS
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
        /// Exclue o registro da tabela TB87_GRUPO_ITENS do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB87_GRUPO_ITENS entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB87_GRUPO_ITENS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB87_GRUPO_ITENS.</returns>
        public static TB87_GRUPO_ITENS Delete(TB87_GRUPO_ITENS entity)
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
        public static int SaveOrUpdate(TB87_GRUPO_ITENS entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB87_GRUPO_ITENS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB87_GRUPO_ITENS.</returns>
        public static TB87_GRUPO_ITENS SaveOrUpdate(TB87_GRUPO_ITENS entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB87_GRUPO_ITENS de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB87_GRUPO_ITENS.</returns>
        public static TB87_GRUPO_ITENS GetByEntityKey(EntityKey entityKey)
        {
            return (TB87_GRUPO_ITENS)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB87_GRUPO_ITENS, ordenados pelo nome "NO_GRUPO_ITEM".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB87_GRUPO_ITENS de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB87_GRUPO_ITENS> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB87_GRUPO_ITENS.OrderBy( g => g.NO_GRUPO_ITEM ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB87_GRUPO_ITENS pela chave primária "CO_GRUPO_ITEM".
        /// </summary>
        /// <param name="CO_GRUPO_ITEM">Id da chave primária</param>
        /// <returns>Entidade TB87_GRUPO_ITENS</returns>
        public static TB87_GRUPO_ITENS RetornaPelaChavePrimaria(int CO_GRUPO_ITEM)
        {
            return (from tb87 in RetornaTodosRegistros()
                    where tb87.CO_GRUPO_ITEM == CO_GRUPO_ITEM
                    select tb87).FirstOrDefault();
        }

        #endregion
    }
}
