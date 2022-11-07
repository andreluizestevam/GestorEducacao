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
    public partial class TB90_PRODUTO
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
        /// Exclue o registro da tabela TB90_PRODUTO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB90_PRODUTO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB90_PRODUTO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB90_PRODUTO.</returns>
        public static TB90_PRODUTO Delete(TB90_PRODUTO entity)
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
        public static int SaveOrUpdate(TB90_PRODUTO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB90_PRODUTO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB90_PRODUTO.</returns>
        public static TB90_PRODUTO SaveOrUpdate(TB90_PRODUTO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB90_PRODUTO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB90_PRODUTO.</returns>
        public static TB90_PRODUTO GetByEntityKey(EntityKey entityKey)
        {
            return (TB90_PRODUTO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB90_PRODUTO, ordenados pela descrição "DES_PROD".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB90_PRODUTO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB90_PRODUTO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB90_PRODUTO.OrderBy( p => p.DES_PROD ).AsObjectQuery();
        }

        public static IQueryable<TB90_PRODUTO> RetornarRegistros(Expression<Func<TB90_PRODUTO, bool>> predicate)
        {
            return GestorEntities.CurrentContext.TB90_PRODUTO.Where(predicate);
        }

     
        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB90_PRODUTO pelas chaves primárias "CO_PROD" e "CO_EMP".
        /// </summary>
        /// <param name="CO_PROD">Id do produto</param>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <returns>Entidade TB90_PRODUTO</returns>
        public static TB90_PRODUTO RetornaPelaChavePrimaria(int CO_PROD, int CO_EMP)
        {
            return (from tb90 in RetornaTodosRegistros()
                    where tb90.CO_PROD == CO_PROD && tb90.TB25_EMPRESA.CO_EMP == CO_EMP
                    select tb90).FirstOrDefault();
        }

        /// <summary>
        /// Retorna um registro da entidade TB90_PRODUTO pelas chaves primárias "CO_PROD".
        /// </summary>
        /// <param name="CO_PROD">Id do produto</param>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <returns>Entidade TB90_PRODUTO</returns>
        public static TB90_PRODUTO RetornaPeloCoProd(int CO_PROD)
        {
            return (from tb90 in RetornaTodosRegistros()
                    where tb90.CO_PROD == CO_PROD
                    select tb90).FirstOrDefault();
        }

        #endregion
    }
}