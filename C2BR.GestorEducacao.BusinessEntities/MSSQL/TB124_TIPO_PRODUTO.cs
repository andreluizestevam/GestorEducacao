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
    public partial class TB124_TIPO_PRODUTO
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
        /// Exclue o registro da tabela TB124_TIPO_PRODUTO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB124_TIPO_PRODUTO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB124_TIPO_PRODUTO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB124_TIPO_PRODUTO.</returns>
        public static TB124_TIPO_PRODUTO Delete(TB124_TIPO_PRODUTO entity)
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
        public static int SaveOrUpdate(TB124_TIPO_PRODUTO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB124_TIPO_PRODUTO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB124_TIPO_PRODUTO.</returns>
        public static TB124_TIPO_PRODUTO SaveOrUpdate(TB124_TIPO_PRODUTO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB124_TIPO_PRODUTO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB124_TIPO_PRODUTO.</returns>
        public static TB124_TIPO_PRODUTO GetByEntityKey(EntityKey entityKey)
        {
            return (TB124_TIPO_PRODUTO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB124_TIPO_PRODUTO, ordenados pela descrição "DE_TIP_PROD".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB124_TIPO_PRODUTO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB124_TIPO_PRODUTO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB124_TIPO_PRODUTO.OrderBy( t => t.DE_TIP_PROD ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB124_TIPO_PRODUTO pela chave primária "CO_TIP_PROD".
        /// </summary>
        /// <param name="CO_TIP_PROD">Id da chave primária</param>
        /// <returns>Entidade TB124_TIPO_PRODUTO</returns>
        public static TB124_TIPO_PRODUTO RetornaPelaChavePrimaria(int CO_TIP_PROD)
        {
            return (from tb124 in RetornaTodosRegistros()
                    where tb124.CO_TIP_PROD == CO_TIP_PROD
                    select tb124).FirstOrDefault();
        }

        #endregion
    }
}
