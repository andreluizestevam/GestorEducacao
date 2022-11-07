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
    public partial class TB156_FormaPagamento
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
        /// Exclue o registro da tabela TB156_FormaPagamento do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB156_FormaPagamento entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB156_FormaPagamento na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB156_FormaPagamento.</returns>
        public static TB156_FormaPagamento Delete(TB156_FormaPagamento entity)
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
        public static int SaveOrUpdate(TB156_FormaPagamento entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB156_FormaPagamento na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB156_FormaPagamento.</returns>
        public static TB156_FormaPagamento SaveOrUpdate(TB156_FormaPagamento entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB156_FormaPagamento de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB156_FormaPagamento.</returns>
        public static TB156_FormaPagamento GetByEntityKey(EntityKey entityKey)
        {
            return (TB156_FormaPagamento)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB156_FormaPagamento, ordenados pelo código "CO_FORMAPAGAMENTO".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB156_FormaPagamento de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB156_FormaPagamento> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB156_FormaPagamento.OrderBy( f => f.CO_FORMAPAGAMENTO ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB156_FormaPagamento pela chave primária "CO_FORMAPAGAMENTO".
        /// </summary>
        /// <param name="CO_FORMAPAGAMENTO">Id da chave primária</param>
        /// <returns>Entidade TB156_FormaPagamento</returns>
        public static TB156_FormaPagamento RetornaPelaChavePrimaria(int CO_FORMAPAGAMENTO)
        {
            return (from tb156 in RetornaTodosRegistros()
                    where tb156.CO_FORMAPAGAMENTO == CO_FORMAPAGAMENTO
                    select tb156).FirstOrDefault();
        }

        #endregion
    }
}