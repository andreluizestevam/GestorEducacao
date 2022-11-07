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
    public partial class TBS397_ATEND_PROC_PLANO
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
        /// Exclue o registro da tabela TBS397_ATEND_PROC_PLANO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS397_ATEND_PROC_PLANO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS397_ATEND_PROC_PLANO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS397_ATEND_PROC_PLANO.</returns>
        public static TBS397_ATEND_PROC_PLANO Delete(TBS397_ATEND_PROC_PLANO entity)
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
        public static int SaveOrUpdate(TBS397_ATEND_PROC_PLANO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS397_ATEND_PROC_PLANO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS397_ATEND_PROC_PLANO.</returns>
        public static TBS397_ATEND_PROC_PLANO SaveOrUpdate(TBS397_ATEND_PROC_PLANO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS397_ATEND_PROC_PLANO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS397_ATEND_PROC_PLANO.</returns>
        public static TBS397_ATEND_PROC_PLANO GetByEntityKey(EntityKey entityKey)
        {
            return (TBS397_ATEND_PROC_PLANO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS397_ATEND_PROC_PLANO, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBS394_RESUM_FINAN de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS397_ATEND_PROC_PLANO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS397_ATEND_PROC_PLANO.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS397_ATEND_PROC_PLANO pela chave primária "ID_ATEND_PROC_PLANO".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TBS397_ATEND_PROC_PLANO</returns>
        public static TBS397_ATEND_PROC_PLANO RetornaPelaChavePrimaria(int ID_ATEND_PROC_PLANO)
        {
            return (from tbs397 in RetornaTodosRegistros()
                    where tbs397.ID_ATEND_PROC_PLANO == ID_ATEND_PROC_PLANO
                    select tbs397).FirstOrDefault();
        }

        #endregion
    }
}
