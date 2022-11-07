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
    public partial class TBS398_ATEND_EXAMES
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
        /// Exclue o registro da tabela TBS398_ATEND_EXAMES do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS398_ATEND_EXAMES entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS398_ATEND_EXAMES na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS398_ATEND_EXAMES.</returns>
        public static TBS398_ATEND_EXAMES Delete(TBS398_ATEND_EXAMES entity)
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
        public static int SaveOrUpdate(TBS398_ATEND_EXAMES entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS398_ATEND_EXAMES na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS398_ATEND_EXAMES.</returns>
        public static TBS398_ATEND_EXAMES SaveOrUpdate(TBS398_ATEND_EXAMES entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS398_ATEND_EXAMES de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS398_ATEND_EXAMES.</returns>
        public static TBS398_ATEND_EXAMES GetByEntityKey(EntityKey entityKey)
        {
            return (TBS398_ATEND_EXAMES)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS398_ATEND_EXAMES, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBS394_RESUM_FINAN de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS398_ATEND_EXAMES> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS398_ATEND_EXAMES.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS398_ATEND_EXAMES pela chave primária "ID_ATEND_EXAMES".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TBS398_ATEND_EXAMES</returns>
        /// 

        public static TBS398_ATEND_EXAMES RetornaPeloIdItem(int ID_ITEM)
        {
            return (from tbs398 in RetornaTodosRegistros()
                    where tbs398.ID_ATEND_EXAMES == ID_ITEM
                    select tbs398).FirstOrDefault();
        }

        public static TBS398_ATEND_EXAMES RetornaPelaChavePrimaria(int ID_ATEND_EXAMES)
        {
            return (from tbs398 in RetornaTodosRegistros()
                    where tbs398.ID_ATEND_EXAMES == ID_ATEND_EXAMES
                    select tbs398).FirstOrDefault();
        }

        #endregion
    }
}
