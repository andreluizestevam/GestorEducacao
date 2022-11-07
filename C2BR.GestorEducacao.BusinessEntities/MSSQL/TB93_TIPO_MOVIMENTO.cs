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
    public partial class TB93_TIPO_MOVIMENTO
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
        /// Exclue o registro da tabela TB93_TIPO_MOVIMENTO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB93_TIPO_MOVIMENTO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB93_TIPO_MOVIMENTO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB93_TIPO_MOVIMENTO.</returns>
        public static TB93_TIPO_MOVIMENTO Delete(TB93_TIPO_MOVIMENTO entity)
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
        public static int SaveOrUpdate(TB93_TIPO_MOVIMENTO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB93_TIPO_MOVIMENTO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB93_TIPO_MOVIMENTO.</returns>
        public static TB93_TIPO_MOVIMENTO SaveOrUpdate(TB93_TIPO_MOVIMENTO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB93_TIPO_MOVIMENTO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB93_TIPO_MOVIMENTO.</returns>
        public static TB93_TIPO_MOVIMENTO GetByEntityKey(EntityKey entityKey)
        {
            return (TB93_TIPO_MOVIMENTO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB93_TIPO_MOVIMENTO, ordenados pela descrição "DE_TIPO_MOV".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB93_TIPO_MOVIMENTO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB93_TIPO_MOVIMENTO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB93_TIPO_MOVIMENTO.OrderBy( t => t.DE_TIPO_MOV ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB93_TIPO_MOVIMENTO pela chave primária "CO_TIPO_MOV".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TB93_TIPO_MOVIMENTO</returns>
        public static TB93_TIPO_MOVIMENTO RetornaPelaChavePrimaria(int CO_TIPO_MOV)
        {
            return (from tb93 in RetornaTodosRegistros()
                    where tb93.CO_TIPO_MOV == CO_TIPO_MOV
                    select tb93).FirstOrDefault();
        }

        /// <summary>
        /// Retorna um registro da entidade TB93_TIPO_MOVIMENTO pela sigla "CO_SIGLA"
        /// </summary>
        /// <param name="CO_SIGLA">Sigla do tipo de movimento</param>
        /// <returns>Entidade TB93_TIPO_MOVIMENTO</returns>
        public static TB93_TIPO_MOVIMENTO RetornaPelaSigla(string CO_SIGLA)
        {
            return (from tb93 in RetornaTodosRegistros()
                    where tb93.CO_SIGLA == CO_SIGLA
                    select tb93).FirstOrDefault();
        }
        #endregion
    }
}