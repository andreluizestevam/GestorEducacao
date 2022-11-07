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
    public partial class TBS385_CID_ANALI_PREVI
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
        /// Exclue o registro da tabela TBS385_CID_ANALI_PREVI do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS385_CID_ANALI_PREVI entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS385_CID_ANALI_PREVI na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS385_CID_ANALI_PREVI.</returns>
        public static TBS385_CID_ANALI_PREVI Delete(TBS385_CID_ANALI_PREVI entity)
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
        public static int SaveOrUpdate(TBS385_CID_ANALI_PREVI entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB30_AGENCIA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS385_CID_ANALI_PREVI.</returns>
        public static TBS385_CID_ANALI_PREVI SaveOrUpdate(TBS385_CID_ANALI_PREVI entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS385_CID_ANALI_PREVI de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS385_CID_ANALI_PREVI.</returns>
        public static TBS385_CID_ANALI_PREVI GetByEntityKey(EntityKey entityKey)
        {
            return (TBS385_CID_ANALI_PREVI)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS385_CID_ANALI_PREVI, ordenados pelo Id do Banco "IDEBANCO" e pelo Id da agência "CO_AGENCIA".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBS385_CID_ANALI_PREVI de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS385_CID_ANALI_PREVI> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS385_CID_ANALI_PREVI.AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TBS385_CID_ANALI_PREVI pela chave primária "ID_CIDS_ANALI_PREVI".
        /// </summary>
        /// <param name="IDEBANCO">Id do banco</param>
        /// <param name="CO_AGENCIA">Código da agência</param>
        /// <returns>Entidade TBS385_CID_ANALI_PREVI</returns>
        public static TBS385_CID_ANALI_PREVI RetornaPelaChavePrimaria(int ID_CIDS_ANALI_PREVI)
        {
            return (from tbs385 in RetornaTodosRegistros()
                    where tbs385.ID_CIDS_ANALI_PREVI == ID_CIDS_ANALI_PREVI
                    select tbs385).FirstOrDefault();
        }

        /// <summary>
        /// Retorna uma lista da entidade TBS385_CID_ANALI_PREVI pela chave estrangeira "ID_AVALI_RECEP".
        /// </summary>
        /// <param name="IDEBANCO">Id do banco</param>
        /// <param name="CO_AGENCIA">Código da agência</param>
        /// <returns>Entidade TBS385_CID_ANALI_PREVI</returns>
        public static List<TBS385_CID_ANALI_PREVI> RetornaPeloIDAvaliacao(int ID_AVALI_RECEP)
        {
            return (from tbs385 in RetornaTodosRegistros()
                    where tbs385.TBS381_AVALI_RECEP.ID_AVALI_RECEP == ID_AVALI_RECEP
                    select tbs385).ToList();
        }

        #endregion
    }
}
