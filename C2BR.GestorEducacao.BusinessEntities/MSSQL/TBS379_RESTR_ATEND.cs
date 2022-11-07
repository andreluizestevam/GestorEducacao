//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Text;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;

namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class TBS379_RESTR_ATEND
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
        /// Exclue o registro da tabela TBS379_RESTR_ATEND do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS379_RESTR_ATEND entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS367_RECEP_SOLIC na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS367_RECEP_SOLIC.</returns>
        public static TBS379_RESTR_ATEND Delete(TBS379_RESTR_ATEND entity)
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
        public static int SaveOrUpdate(TBS379_RESTR_ATEND entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS367_RECEP_SOLIC na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS367_RECEP_SOLIC.</returns>
        public static TBS379_RESTR_ATEND SaveOrUpdate(TBS379_RESTR_ATEND entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS367_RECEP_SOLIC de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS367_RECEP_SOLIC.</returns>
        public static TBS379_RESTR_ATEND GetByEntityKey(EntityKey entityKey)
        {
            return (TBS379_RESTR_ATEND)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS367_RECEP_SOLIC, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBS367_RECEP_SOLIC de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS379_RESTR_ATEND> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS379_RESTR_ATEND.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS367_RECEP_SOLIC pela chave primária "ID_RECEP_SOLIC".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TBS379_RESTR_ATEND</returns>
        public static TBS379_RESTR_ATEND RetornaPelaChavePrimaria(int ID_RESTR_ATEND)
        {
            return (from tbs367 in RetornaTodosRegistros()
                    where tbs367.ID_RESTR_ATEND == ID_RESTR_ATEND
                    select tbs367).FirstOrDefault();
        }

        #endregion
    }
}
