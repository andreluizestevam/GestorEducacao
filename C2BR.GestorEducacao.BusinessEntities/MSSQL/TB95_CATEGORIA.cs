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
    public partial class TB95_CATEGORIA
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
        /// Exclue o registro da tabela TB95_CATEGORIA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB95_CATEGORIA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB95_CATEGORIA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB95_CATEGORIA.</returns>
        public static TB95_CATEGORIA Delete(TB95_CATEGORIA entity)
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
        public static int SaveOrUpdate(TB95_CATEGORIA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB95_CATEGORIA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB95_CATEGORIA.</returns>
        public static TB95_CATEGORIA SaveOrUpdate(TB95_CATEGORIA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB95_CATEGORIA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB95_CATEGORIA.</returns>
        public static TB95_CATEGORIA GetByEntityKey(EntityKey entityKey)
        {
            return (TB95_CATEGORIA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB95_CATEGORIA, ordenados pela descrição "DES_CATEG".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB95_CATEGORIA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB95_CATEGORIA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB95_CATEGORIA.OrderBy( c => c.DES_CATEG ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB95_CATEGORIA pela chave primária "CO_CATEG".
        /// </summary>
        /// <param name="CO_CATEG">Id da chave primária</param>
        /// <returns>Entidade TB95_CATEGORIA</returns>
        public static TB95_CATEGORIA RetornaPelaChavePrimaria(int CO_CATEG)
        {
            return (from tb95 in RetornaTodosRegistros()
                    where tb95.CO_CATEG == CO_CATEG
                    select tb95).FirstOrDefault();
        }

        #endregion
    }
}