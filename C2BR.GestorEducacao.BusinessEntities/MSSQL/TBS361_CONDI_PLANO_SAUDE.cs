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
    public partial class TBS361_CONDI_PLANO_SAUDE
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
        /// Exclue o registro da tabela TBS361_CONDI_PLANO_SAUDE do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS361_CONDI_PLANO_SAUDE entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS361_CONDI_PLANO_SAUDE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS361_CONDI_PLANO_SAUDE.</returns>
        public static TBS361_CONDI_PLANO_SAUDE Delete(TBS361_CONDI_PLANO_SAUDE entity)
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
        public static int SaveOrUpdate(TBS361_CONDI_PLANO_SAUDE entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS361_CONDI_PLANO_SAUDE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS361_CONDI_PLANO_SAUDE.</returns>
        public static TBS361_CONDI_PLANO_SAUDE SaveOrUpdate(TBS361_CONDI_PLANO_SAUDE entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS361_CONDI_PLANO_SAUDE de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS361_CONDI_PLANO_SAUDE.</returns>
        public static TBS361_CONDI_PLANO_SAUDE GetByEntityKey(EntityKey entityKey)
        {
            return (TBS361_CONDI_PLANO_SAUDE)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS361_CONDI_PLANO_SAUDE, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBS334_DIAGN_ATEND_MEDIC de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS361_CONDI_PLANO_SAUDE> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS361_CONDI_PLANO_SAUDE.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS361_CONDI_PLANO_SAUDE pela chave primária "ID_CONDI_PLANO_SAUDE".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TBS361_CONDI_PLANO_SAUDE</returns>
        public static TBS361_CONDI_PLANO_SAUDE RetornaPelaChavePrimaria(int ID_CONDI_PLANO_SAUDE)
        {
            return (from tbs361 in RetornaTodosRegistros()
                    where tbs361.ID_CONDI_PLANO_SAUDE == ID_CONDI_PLANO_SAUDE
                    select tbs361).FirstOrDefault();
        }

        #endregion
    }
}
