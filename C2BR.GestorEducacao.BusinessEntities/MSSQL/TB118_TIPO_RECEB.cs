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
    public partial class TB118_TIPO_RECEB
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
        /// Exclue o registro da tabela TB118_TIPO_RECEB do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB118_TIPO_RECEB entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB118_TIPO_RECEB na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB118_TIPO_RECEB.</returns>
        public static TB118_TIPO_RECEB Delete(TB118_TIPO_RECEB entity)
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
        public static int SaveOrUpdate(TB118_TIPO_RECEB entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB118_TIPO_RECEB na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB118_TIPO_RECEB.</returns>
        public static TB118_TIPO_RECEB SaveOrUpdate(TB118_TIPO_RECEB entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB118_TIPO_RECEB de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB118_TIPO_RECEB.</returns>
        public static TB118_TIPO_RECEB GetByEntityKey(EntityKey entityKey)
        {
            return (TB118_TIPO_RECEB)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB118_TIPO_RECEB, ordenados pela descrição "DE_RECEBIMENTO".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB118_TIPO_RECEB de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB118_TIPO_RECEB> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB118_TIPO_RECEB.OrderBy( t => t.DE_RECEBIMENTO ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB118_TIPO_RECEB pela chave primária "CO_TIPO_REC".
        /// </summary>
        /// <param name="CO_TIPO_REC">Id da chave primária</param>
        /// <returns>Entidade TB118_TIPO_RECEB</returns>
        public static TB118_TIPO_RECEB RetornaPelaChavePrimaria(int CO_TIPO_REC)
        {
            return (from tb118 in RetornaTodosRegistros()
                    where tb118.CO_TIPO_REC == CO_TIPO_REC
                    select tb118).FirstOrDefault();
        }

        #endregion
    }
}