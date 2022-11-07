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
    public partial class TBS350_ITEM_CENTR_REGUL
    {
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
        /// Exclue o registro da tabela TBS350_ITEM_CENTR_REGUL do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS350_ITEM_CENTR_REGUL entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS350_ITEM_CENTR_REGUL na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS350_ITEM_CENTR_REGUL.</returns>
        public static TBS350_ITEM_CENTR_REGUL Delete(TBS350_ITEM_CENTR_REGUL entity)
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
        public static int SaveOrUpdate(TBS350_ITEM_CENTR_REGUL entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS336_ISDA_ITENS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS345_VACINA.</returns>
        public static TBS350_ITEM_CENTR_REGUL SaveOrUpdate(TBS350_ITEM_CENTR_REGUL entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS345_VACINA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS345_VACINA.</returns>
        public static TBS350_ITEM_CENTR_REGUL GetByEntityKey(EntityKey entityKey)
        {
            return (TBS350_ITEM_CENTR_REGUL)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS345_VACINA, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBS334_DIAGN_ATEND_MEDIC de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS350_ITEM_CENTR_REGUL> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS350_ITEM_CENTR_REGUL.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS350_ITEM_CENTR_REGUL pela chave primária "ID_VACINA".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TBS350_ITEM_CENTR_REGUL</returns>
        public static TBS350_ITEM_CENTR_REGUL RetornaPelaChavePrimaria(int ID_ITEM_CENTR_REGUL)
        {
            return (from tbs350 in RetornaTodosRegistros()
                    where tbs350.ID_ITEM_CENTR_REGUL == ID_ITEM_CENTR_REGUL
                    select tbs350).FirstOrDefault();
        }

        #endregion
    }
}
