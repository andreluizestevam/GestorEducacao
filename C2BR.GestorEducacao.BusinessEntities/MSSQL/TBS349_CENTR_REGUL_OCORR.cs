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
    public partial class TBS349_CENTR_REGUL_OCORR
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
        /// Exclue o registro da tabela TBS349_CENTR_REGUL_OCORR do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS349_CENTR_REGUL_OCORR entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS349_CENTR_REGUL_OCORR na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS349_CENTR_REGUL_OCORR.</returns>
        public static TBS349_CENTR_REGUL_OCORR Delete(TBS349_CENTR_REGUL_OCORR entity)
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
        public static int SaveOrUpdate(TBS349_CENTR_REGUL_OCORR entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS349_CENTR_REGUL_OCORR na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS196_TIPO_EXAME.</returns>
        public static TBS349_CENTR_REGUL_OCORR SaveOrUpdate(TBS349_CENTR_REGUL_OCORR entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS349_CENTR_REGUL_OCORR de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS349_CENTR_REGUL_OCORR.</returns>
        public static TBS349_CENTR_REGUL_OCORR GetByEntityKey(EntityKey entityKey)
        {
            return (TBS349_CENTR_REGUL_OCORR)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS349_CENTR_REGUL_OCORR, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBS196_TIPO_EXAME de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS349_CENTR_REGUL_OCORR> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS349_CENTR_REGUL_OCORR.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS349_CENTR_REGUL_OCORR pela chave primária "CO_TIPO_MOV".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TBS349_CENTR_REGUL_OCORR</returns>
        public static TBS349_CENTR_REGUL_OCORR RetornaPelaChavePrimaria(int ID_CENTR_REGUL_OCORR)
        {
            return (from tbs349 in RetornaTodosRegistros()
                    where tbs349.ID_CENTR_REGUL_OCORR == ID_CENTR_REGUL_OCORR
                    select tbs349).FirstOrDefault();
        }

        #endregion
    }
}
