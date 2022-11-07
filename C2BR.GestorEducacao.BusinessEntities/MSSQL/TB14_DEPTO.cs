//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Text;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;
using System.Linq.Expressions;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class TB14_DEPTO
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
        /// Exclue o registro da tabela TB14_DEPTO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB14_DEPTO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB14_DEPTO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB14_DEPTO.</returns>
        public static TB14_DEPTO Delete(TB14_DEPTO entity)
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
        public static int SaveOrUpdate(TB14_DEPTO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB14_DEPTO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB14_DEPTO.</returns>
        public static TB14_DEPTO SaveOrUpdate(TB14_DEPTO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB14_DEPTO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB14_DEPTO.</returns>
        public static TB14_DEPTO GetByEntityKey(EntityKey entityKey)
        {
            return (TB14_DEPTO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB14_DEPTO, ordenados pelo nome "NO_DEPTO".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB14_DEPTO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB14_DEPTO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB14_DEPTO.OrderBy( d => d.NO_DEPTO ).AsObjectQuery();
        }

        public static IQueryable<TB14_DEPTO> RetornarRegistros(Expression<Func<TB14_DEPTO, bool>> predicate) {

            return GestorEntities.CurrentContext.TB14_DEPTO.Where(predicate);
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB14_DEPTO pela chave primária "CO_DEPTO".
        /// </summary>
        /// <param name="CO_DEPTO">Id da chave primária</param>
        /// <returns>Entidade TB14_DEPTO</returns>
        public static TB14_DEPTO RetornaPelaChavePrimaria(int CO_DEPTO)
        {
            return (from tb14 in RetornaTodosRegistros()
                    where tb14.CO_DEPTO == CO_DEPTO
                    select tb14).FirstOrDefault();
        }

        #endregion
    }
}