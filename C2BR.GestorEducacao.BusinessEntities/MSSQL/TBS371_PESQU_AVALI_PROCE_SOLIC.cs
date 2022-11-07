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
    public partial class TBS371_PESQU_AVALI_PROCE_SOLIC
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
        /// Exclue o registro da tabela TBS371_PESQU_AVALI_PROCE_SOLIC do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS371_PESQU_AVALI_PROCE_SOLIC entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS371_PESQU_AVALI_PROCE_SOLIC na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS371_PESQU_AVALI_PROCE_SOLIC.</returns>
        public static TBS371_PESQU_AVALI_PROCE_SOLIC Delete(TBS371_PESQU_AVALI_PROCE_SOLIC entity)
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
        public static int SaveOrUpdate(TBS371_PESQU_AVALI_PROCE_SOLIC entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB30_AGENCIA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS371_PESQU_AVALI_PROCE_SOLIC.</returns>
        public static TBS371_PESQU_AVALI_PROCE_SOLIC SaveOrUpdate(TBS371_PESQU_AVALI_PROCE_SOLIC entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS371_PESQU_AVALI_PROCE_SOLIC de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS371_PESQU_AVALI_PROCE_SOLIC.</returns>
        public static TBS371_PESQU_AVALI_PROCE_SOLIC GetByEntityKey(EntityKey entityKey)
        {
            return (TBS371_PESQU_AVALI_PROCE_SOLIC)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB30_AGENCIA, ordenados pelo Id do Banco "IDEBANCO" e pelo Id da agência "CO_AGENCIA".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB30_AGENCIA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS371_PESQU_AVALI_PROCE_SOLIC> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS371_PESQU_AVALI_PROCE_SOLIC.AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TBS371_PESQU_AVALI_PROCE_SOLIC pelas chaves primárias "IDEBANCO" e "CO_AGENCIA".
        /// </summary>
        /// <param name="IDEBANCO">Id do banco</param>
        /// <param name="CO_AGENCIA">Código da agência</param>
        /// <returns>Entidade TBS371_PESQU_AVALI_PROCE_SOLIC</returns>
        public static TBS371_PESQU_AVALI_PROCE_SOLIC RetornaPelaChavePrimaria(int ID_PESQU_AVALI_PROCE_SOLIC)
        {
            return (from tbs371 in RetornaTodosRegistros()
                    where tbs371.ID_PESQU_AVALI_PROCE_SOLIC == ID_PESQU_AVALI_PROCE_SOLIC
                    select tbs371).FirstOrDefault();
        }

        /// <summary>
        /// Retorna uma lista da entidade TBS371_PESQU_AVALI_PROCE_SOLIC pela chave estrangeira "ID_AVALI_RECEP".
        /// </summary>
        /// <param name="IDEBANCO">Id do banco</param>
        /// <param name="CO_AGENCIA">Código da agência</param>
        /// <returns>Entidade TBS371_PESQU_AVALI_PROCE_SOLIC</returns>
        public static List<TBS371_PESQU_AVALI_PROCE_SOLIC> RetornaPeloIDAvaliacao(int ID_AVALI_RECEP)
        {
            return (from tbs371 in RetornaTodosRegistros()
                    where tbs371.TBS381_AVALI_RECEP.ID_AVALI_RECEP == ID_AVALI_RECEP
                    select tbs371).ToList();
        }

        #endregion
    }
}
