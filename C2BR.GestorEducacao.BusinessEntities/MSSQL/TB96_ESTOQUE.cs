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
    public partial class TB96_ESTOQUE
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
        /// Exclue o registro da tabela TB96_ESTOQUE do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB96_ESTOQUE entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB96_ESTOQUE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB96_ESTOQUE.</returns>
        public static TB96_ESTOQUE Delete(TB96_ESTOQUE entity)
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
        public static int SaveOrUpdate(TB96_ESTOQUE entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB96_ESTOQUE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB96_ESTOQUE.</returns>
        public static TB96_ESTOQUE SaveOrUpdate(TB96_ESTOQUE entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB96_ESTOQUE de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB96_ESTOQUE.</returns>
        public static TB96_ESTOQUE GetByEntityKey(EntityKey entityKey)
        {
            return (TB96_ESTOQUE)GestorEntities.GetByEntityKey(entityKey);
        }

        public static TB96_ESTOQUE RetornarUmRegistro(Expression<Func<TB96_ESTOQUE, bool>> predicate)
        {
            return GestorEntities.CurrentContext.TB96_ESTOQUE.FirstOrDefault(predicate);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB96_ESTOQUE, ordenados pelo Id do produto "CO_PROD".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB96_ESTOQUE de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB96_ESTOQUE> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB96_ESTOQUE.OrderBy( e => e.TB90_PRODUTO.CO_PROD ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB96_ESTOQUE pelas chaves primárias "CO_EMP" e "CO_PROD".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="CO_PROD">Id do produto</param>
        /// <returns>Entidade TB96_ESTOQUE</returns>
        public static TB96_ESTOQUE RetornaPelaChavePrimaria(int CO_EMP, int CO_PROD)
        {
            return (from tb96 in RetornaTodosRegistros()
                    where tb96.TB25_EMPRESA.CO_EMP == CO_EMP && tb96.TB90_PRODUTO.CO_PROD == CO_PROD
                    select tb96).FirstOrDefault();
        }

        #endregion
    }
}