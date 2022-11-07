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
    public partial class TB182_TIPO_UNIDA
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
        /// Exclue o registro da tabela TB182_TIPO_UNIDA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB182_TIPO_UNIDA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB182_TIPO_UNIDA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB182_TIPO_UNIDA.</returns>
        public static TB182_TIPO_UNIDA Delete(TB182_TIPO_UNIDA entity)
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
        public static int SaveOrUpdate(TB182_TIPO_UNIDA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB182_TIPO_UNIDA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB182_TIPO_UNIDA.</returns>
        public static TB182_TIPO_UNIDA SaveOrUpdate(TB182_TIPO_UNIDA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB182_TIPO_UNIDA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB182_TIPO_UNIDA.</returns>
        public static TB182_TIPO_UNIDA GetByEntityKey(EntityKey entityKey)
        {
            return (TB182_TIPO_UNIDA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB182_TIPO_UNIDA, ordenados pela descrição "DE_TIPO_UNIDA".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB182_TIPO_UNIDA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB182_TIPO_UNIDA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB182_TIPO_UNIDA.OrderBy( t => t.DE_TIPO_UNIDA ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB182_TIPO_UNIDA pela chave primária "CO_SIGLA_TIPO_UNIDA".
        /// </summary>
        /// <param name="CO_SIGLA_TIPO_UNIDA">Id da chave primária</param>
        /// <returns>Entidade TB182_TIPO_UNIDA</returns>
        public static TB182_TIPO_UNIDA RetornaPelaChavePrimaria(string CO_SIGLA_TIPO_UNIDA)
        {
            return (from tb182 in RetornaTodosRegistros()
                    where tb182.CO_SIGLA_TIPO_UNIDA == CO_SIGLA_TIPO_UNIDA
                    select tb182).FirstOrDefault();
        }

        /// <summary>
        /// Retorna o primeiro registro da entidade TB182_TIPO_UNIDA pelo tipo de unidade "CO_TIPO_UNIDA".
        /// </summary>
        /// <param name="CO_TIPO_UNIDA">Id do tipo de unidade</param>
        /// <returns>Entidade TB182_TIPO_UNIDA</returns>
        public static TB182_TIPO_UNIDA RetornaPeloCoTipoUnida(int CO_TIPO_UNIDA)
        {
            return (from tb182 in RetornaTodosRegistros()
                    where tb182.CO_TIPO_UNIDA == CO_TIPO_UNIDA
                    select tb182).FirstOrDefault();
        }
        #endregion
    }
}