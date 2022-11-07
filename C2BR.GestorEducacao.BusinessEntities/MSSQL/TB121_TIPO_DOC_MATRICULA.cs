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
    public partial class TB121_TIPO_DOC_MATRICULA
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
        /// Exclue o registro da tabela TB121_TIPO_DOC_MATRICULA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB121_TIPO_DOC_MATRICULA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB121_TIPO_DOC_MATRICULA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB121_TIPO_DOC_MATRICULA.</returns>
        public static TB121_TIPO_DOC_MATRICULA Delete(TB121_TIPO_DOC_MATRICULA entity)
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
        public static int SaveOrUpdate(TB121_TIPO_DOC_MATRICULA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB121_TIPO_DOC_MATRICULA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB121_TIPO_DOC_MATRICULA.</returns>
        public static TB121_TIPO_DOC_MATRICULA SaveOrUpdate(TB121_TIPO_DOC_MATRICULA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB121_TIPO_DOC_MATRICULA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB121_TIPO_DOC_MATRICULA.</returns>
        public static TB121_TIPO_DOC_MATRICULA GetByEntityKey(EntityKey entityKey)
        {
            return (TB121_TIPO_DOC_MATRICULA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB121_TIPO_DOC_MATRICULA, ordenados pela descrição "DE_TP_DOC_MAT".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB121_TIPO_DOC_MATRICULA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB121_TIPO_DOC_MATRICULA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB121_TIPO_DOC_MATRICULA.OrderBy( t => t.DE_TP_DOC_MAT ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB121_TIPO_DOC_MATRICULA pela chave primária "CO_TP_DOC_MAT".
        /// </summary>
        /// <param name="CO_TP_DOC_MAT">Id da chave primária</param>
        /// <returns>Entidade TB121_TIPO_DOC_MATRICULA</returns>
        public static TB121_TIPO_DOC_MATRICULA RetornaPelaChavePrimaria(int CO_TP_DOC_MAT)
        {
            return (from tb121 in RetornaTodosRegistros()
                    where tb121.CO_TP_DOC_MAT == CO_TP_DOC_MAT
                    select tb121).FirstOrDefault();
        }

        #endregion
    }
}