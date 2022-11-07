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
    public partial class TBS392_ANEXO_ATEND
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
        /// Exclue o registro da tabela TBS392_ANEXO_ATEND do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS392_ANEXO_ATEND entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS392_ANEXO_ATEND na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS392_ANEXO_ATEND.</returns>
        public static TBS392_ANEXO_ATEND Delete(TBS392_ANEXO_ATEND entity)
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
        public static int SaveOrUpdate(TBS392_ANEXO_ATEND entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS392_ANEXO_ATEND na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS392_ANEXO_ATEND.</returns>
        public static TBS392_ANEXO_ATEND SaveOrUpdate(TBS392_ANEXO_ATEND entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS392_ANEXO_ATEND de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS392_ANEXO_ATEND.</returns>
        public static TBS392_ANEXO_ATEND GetByEntityKey(EntityKey entityKey)
        {
            return (TBS392_ANEXO_ATEND)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS392_ANEXO_ATEND, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBS392_ANEXO_ATEND de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS392_ANEXO_ATEND> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS392_ANEXO_ATEND.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS392_ANEXO_ATEND pela chave primária "ID_ANEXO_ATEND".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TBS392_ANEXO_ATEND</returns>
        public static TBS392_ANEXO_ATEND RetornaPelaChavePrimaria(int ID_ANEXO_ATEND)
        {
            return (from tbs392 in RetornaTodosRegistros()
                    where tbs392.ID_ANEXO_ATEND == ID_ANEXO_ATEND
                    select tbs392).FirstOrDefault();
        }

        #endregion
    }
}
