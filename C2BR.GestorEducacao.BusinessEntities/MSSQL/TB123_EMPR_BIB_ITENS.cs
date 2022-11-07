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
    public partial class TB123_EMPR_BIB_ITENS
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
        /// Exclue o registro da tabela TB123_EMPR_BIB_ITENS do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB123_EMPR_BIB_ITENS entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB123_EMPR_BIB_ITENS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB123_EMPR_BIB_ITENS.</returns>
        public static TB123_EMPR_BIB_ITENS Delete(TB123_EMPR_BIB_ITENS entity)
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
        public static int SaveOrUpdate(TB123_EMPR_BIB_ITENS entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB123_EMPR_BIB_ITENS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB123_EMPR_BIB_ITENS.</returns>
        public static TB123_EMPR_BIB_ITENS SaveOrUpdate(TB123_EMPR_BIB_ITENS entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB123_EMPR_BIB_ITENS de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB123_EMPR_BIB_ITENS.</returns>
        public static TB123_EMPR_BIB_ITENS GetByEntityKey(EntityKey entityKey)
        {
            return (TB123_EMPR_BIB_ITENS)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB123_EMPR_BIB_ITENS, ordenados pelo Id "CO_EMPR_BIB_ITENS".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB123_EMPR_BIB_ITENS de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB123_EMPR_BIB_ITENS> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB123_EMPR_BIB_ITENS.OrderBy( e => e.CO_EMPR_BIB_ITENS ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB123_EMPR_BIB_ITENS pela chave primária "CO_EMPR_BIB_ITENS".
        /// </summary>
        /// <param name="CO_EMPR_BIB_ITENS">Id da chave primária</param>
        /// <returns>Entidade TB123_EMPR_BIB_ITENS</returns>
        public static TB123_EMPR_BIB_ITENS RetornaPelaChavePrimaria(int CO_EMPR_BIB_ITENS)
        {
            return (from tb123 in RetornaTodosRegistros()
                    where tb123.CO_EMPR_BIB_ITENS == CO_EMPR_BIB_ITENS
                    select tb123).FirstOrDefault();
        }

        #endregion
    }
}