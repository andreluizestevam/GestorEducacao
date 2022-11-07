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
    public partial class TB086_TIPO_DOC
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
        /// Exclue o registro da tabela TB086_TIPO_DOC do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB086_TIPO_DOC entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB086_TIPO_DOC na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB086_TIPO_DOC.</returns>
        public static TB086_TIPO_DOC Delete(TB086_TIPO_DOC entity)
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
        public static int SaveOrUpdate(TB086_TIPO_DOC entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB086_TIPO_DOC na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB086_TIPO_DOC.</returns>
        public static TB086_TIPO_DOC SaveOrUpdate(TB086_TIPO_DOC entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB086_TIPO_DOC de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB086_TIPO_DOC.</returns>
        public static TB086_TIPO_DOC GetByEntityKey(EntityKey entityKey)
        {
            return (TB086_TIPO_DOC)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB086_TIPO_DOC, ordenados pela descrição "DES_TIPO_DOC".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB086_TIPO_DOC de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB086_TIPO_DOC> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB086_TIPO_DOC.OrderBy( t => t.DES_TIPO_DOC ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB086_TIPO_DOC pela chaves primária "CO_TIPO_DOC".
        /// </summary>
        /// <param name="CO_TIPO_DOC">Id da chave primária</param>
        /// <returns>Entidade TB086_TIPO_DOC</returns>
        public static TB086_TIPO_DOC RetornaPeloCoTipoDoc(int CO_TIPO_DOC)
        {
            return (from tb086 in RetornaTodosRegistros()
                    where tb086.CO_TIPO_DOC == CO_TIPO_DOC
                    select tb086).FirstOrDefault();
        }

        #endregion
    }
}