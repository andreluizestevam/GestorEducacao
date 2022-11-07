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
    public partial class TBS333_ATEST_MEDIC_PACIE
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
        /// Exclue o registro da tabela TBS333_ATEST_MEDIC_PACIE do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS333_ATEST_MEDIC_PACIE entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS194_PRE_ATEND na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBE222_PAGTO_CHEQUE.</returns>
        public static TBS333_ATEST_MEDIC_PACIE Delete(TBS333_ATEST_MEDIC_PACIE entity)
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
        public static int SaveOrUpdate(TBS333_ATEST_MEDIC_PACIE entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS194_PRE_ATEND na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS333_ATEST_MEDIC_PACIE.</returns>
        public static TBS333_ATEST_MEDIC_PACIE SaveOrUpdate(TBS333_ATEST_MEDIC_PACIE entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS194_PRE_ATEND de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS333_ATEST_MEDIC_PACIE.</returns>
        public static TBS333_ATEST_MEDIC_PACIE GetByEntityKey(EntityKey entityKey)
        {
            return (TBS333_ATEST_MEDIC_PACIE)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS333_ATEST_MEDIC_PACIE, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBS194_PRE_ATEND de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS333_ATEST_MEDIC_PACIE> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS333_ATEST_MEDIC_PACIE.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS194_PRE_ATEND pela chave primária "ID_ATEST_MEDIC_PACIE".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TBS333_ATEST_MEDIC_PACIE</returns>
        public static TBS333_ATEST_MEDIC_PACIE RetornaPelaChavePrimaria(int ID_ATEST_MEDIC_PACIE)
        {
            return (from tbs333 in RetornaTodosRegistros()
                    where tbs333.ID_ATEST_MEDIC_PACIE == ID_ATEST_MEDIC_PACIE
                    select tbs333).FirstOrDefault();
        }

        #endregion
    }
}
