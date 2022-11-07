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
    public partial class TBS365_PAGTO_CHEQUE
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
        /// Exclue o registro da tabela TBS365_PAGTO_CHEQUE do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS365_PAGTO_CHEQUE entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS365_PAGTO_CHEQUE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS335_ISDA_TIPO.</returns>
        public static TBS365_PAGTO_CHEQUE Delete(TBS365_PAGTO_CHEQUE entity)
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
        public static int SaveOrUpdate(TBS365_PAGTO_CHEQUE entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS365_PAGTO_CHEQUE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS335_ISDA_TIPO.</returns>
        public static TBS365_PAGTO_CHEQUE SaveOrUpdate(TBS365_PAGTO_CHEQUE entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS365_PAGTO_CHEQUE de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS365_PAGTO_CHEQUE.</returns>
        public static TBS365_PAGTO_CHEQUE GetByEntityKey(EntityKey entityKey)
        {
            return (TBS365_PAGTO_CHEQUE)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS365_PAGTO_CHEQUE, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBS334_DIAGN_ATEND_MEDIC de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS365_PAGTO_CHEQUE> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS365_PAGTO_CHEQUE.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS365_PAGTO_CHEQUE pela chave primária "ID_PAGTO_CHEQUE".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TBS365_PAGTO_CHEQUE</returns>
        public static TBS365_PAGTO_CHEQUE RetornaPelaChavePrimaria(int ID_PAGTO_CHEQUE)
        {
            return (from tbs365 in RetornaTodosRegistros()
                    where tbs365.ID_PAGTO_CHEQUE == ID_PAGTO_CHEQUE
                    select tbs365).FirstOrDefault();
        }

        #endregion
    }
}
