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
    public partial class TB34_AUTOR
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
        /// Exclue o registro da tabela TB34_AUTOR do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB34_AUTOR entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB34_AUTOR na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB34_AUTOR.</returns>
        public static TB34_AUTOR Delete(TB34_AUTOR entity)
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
        public static int SaveOrUpdate(TB34_AUTOR entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB34_AUTOR na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB34_AUTOR.</returns>
        public static TB34_AUTOR SaveOrUpdate(TB34_AUTOR entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB34_AUTOR de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB34_AUTOR.</returns>
        public static TB34_AUTOR GetByEntityKey(EntityKey entityKey)
        {
            return (TB34_AUTOR)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB34_AUTOR, ordenados pelo nome "NO_AUTOR".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB34_AUTOR de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB34_AUTOR> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB34_AUTOR.OrderBy( a => a.NO_AUTOR ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB34_AUTOR pela chave primária "CO_AUTOR".
        /// </summary>
        /// <param name="CO_AUTOR">Id da chave primária</param>
        /// <returns>Entidade TB34_AUTOR</returns>
        public static TB34_AUTOR RetornaPelaChavePrimaria(int CO_AUTOR)
        {
            return (from tb34 in RetornaTodosRegistros()
                    where tb34.CO_AUTOR == CO_AUTOR
                    select tb34).FirstOrDefault();
        }

        #endregion
    }
}