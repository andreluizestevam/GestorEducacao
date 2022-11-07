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
    public partial class TB91_MOV_PRODUTO
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
        /// Exclue o registro da tabela TB91_MOV_PRODUTO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB91_MOV_PRODUTO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB91_MOV_PRODUTO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB91_MOV_PRODUTO.</returns>
        public static TB91_MOV_PRODUTO Delete(TB91_MOV_PRODUTO entity)
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
        public static int SaveOrUpdate(TB91_MOV_PRODUTO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB91_MOV_PRODUTO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB91_MOV_PRODUTO.</returns>
        public static TB91_MOV_PRODUTO SaveOrUpdate(TB91_MOV_PRODUTO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB91_MOV_PRODUTO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB91_MOV_PRODUTO.</returns>
        public static TB91_MOV_PRODUTO GetByEntityKey(EntityKey entityKey)
        {
            return (TB91_MOV_PRODUTO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB91_MOV_PRODUTO, ordenados pelo Id "CO_MOV".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB91_MOV_PRODUTO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB91_MOV_PRODUTO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB91_MOV_PRODUTO.Include(typeof(TB93_TIPO_MOVIMENTO).Name).OrderBy( m => m.CO_MOV ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB91_MOV_PRODUTO pela chave primária "CO_MOV".
        /// </summary>
        /// <param name="CO_MOV">Id da chave primária</param>
        /// <returns>Entidade TB91_MOV_PRODUTO</returns>
        public static TB91_MOV_PRODUTO RetornaPelaChavePrimaria(int CO_MOV)
        {
            return (from tb91 in RetornaTodosRegistros()
                    where tb91.CO_MOV == CO_MOV
                    select tb91).FirstOrDefault();
        }

        #endregion
    }
}