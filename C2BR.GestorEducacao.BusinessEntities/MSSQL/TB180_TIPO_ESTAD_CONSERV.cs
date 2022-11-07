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
    public partial class TB180_TIPO_ESTAD_CONSERV
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
        /// Exclue o registro da tabela TB180_TIPO_ESTAD_CONSERV do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB180_TIPO_ESTAD_CONSERV entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB180_TIPO_ESTAD_CONSERV na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB180_TIPO_ESTAD_CONSERV.</returns>
        public static TB180_TIPO_ESTAD_CONSERV Delete(TB180_TIPO_ESTAD_CONSERV entity)
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
        public static int SaveOrUpdate(TB180_TIPO_ESTAD_CONSERV entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB180_TIPO_ESTAD_CONSERV na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB180_TIPO_ESTAD_CONSERV.</returns>
        public static TB180_TIPO_ESTAD_CONSERV SaveOrUpdate(TB180_TIPO_ESTAD_CONSERV entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB180_TIPO_ESTAD_CONSERV de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB180_TIPO_ESTAD_CONSERV.</returns>
        public static TB180_TIPO_ESTAD_CONSERV GetByEntityKey(EntityKey entityKey)
        {
            return (TB180_TIPO_ESTAD_CONSERV)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB180_TIPO_ESTAD_CONSERV, ordenados pela descrição "DE_TIPO_ESTAD_CONSERV".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB180_TIPO_ESTAD_CONSERV de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB180_TIPO_ESTAD_CONSERV> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB180_TIPO_ESTAD_CONSERV.OrderBy( t => t.DE_TIPO_ESTAD_CONSERV ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB180_TIPO_ESTAD_CONSERV pela chave primária "CO_SIGLA_TIPO_ESTAD_CONSERV".
        /// </summary>
        /// <param name="CO_SIGLA_TIPO_ESTAD_CONSERV">Id da chave primária</param>
        /// <returns>Entidade TB180_TIPO_ESTAD_CONSERV</returns>
        public static TB180_TIPO_ESTAD_CONSERV RetornaPelaChavePrimaria(string CO_SIGLA_TIPO_ESTAD_CONSERV)
        {
            return (from tb180 in RetornaTodosRegistros()
                    where tb180.CO_SIGLA_TIPO_ESTAD_CONSERV == CO_SIGLA_TIPO_ESTAD_CONSERV
                    select tb180).FirstOrDefault();
        }

        /// <summary>
        /// Retorna o primeiro registro da entidade TB180_TIPO_ESTAD_CONSERV pelo tipo de estado de conservação "CO_TIPO_ESTAD_CONSERV".
        /// </summary>
        /// <param name="CO_TIPO_ESTAD_CONSERV">Id do tipo de estado de conservação</param>
        /// <returns>Entidade TB180_TIPO_ESTAD_CONSERV</returns>
        public static TB180_TIPO_ESTAD_CONSERV RetornaPeloCoTipoEstadConserv(int CO_TIPO_ESTAD_CONSERV)
        {
            return (from tb180 in RetornaTodosRegistros()
                    where tb180.CO_TIPO_ESTAD_CONSERV == CO_TIPO_ESTAD_CONSERV
                    select tb180).FirstOrDefault();
        }
        #endregion
    }
}