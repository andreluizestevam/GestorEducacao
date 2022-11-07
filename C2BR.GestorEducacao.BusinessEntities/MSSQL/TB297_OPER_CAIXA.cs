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
    public partial class TB297_OPER_CAIXA
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
        /// Exclue o registro da tabela TB297_OPER_CAIXA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB297_OPER_CAIXA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB297_OPER_CAIXA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB297_OPER_CAIXA.</returns>
        public static TB297_OPER_CAIXA Delete(TB297_OPER_CAIXA entity)
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
        public static int SaveOrUpdate(TB297_OPER_CAIXA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB297_OPER_CAIXA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB297_OPER_CAIXA.</returns>
        public static TB297_OPER_CAIXA SaveOrUpdate(TB297_OPER_CAIXA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB297_OPER_CAIXA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB297_OPER_CAIXA.</returns>
        public static TB297_OPER_CAIXA GetByEntityKey(EntityKey entityKey)
        {
            return (TB297_OPER_CAIXA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB297_OPER_CAIXA, ordenados pelo código "CO_OPER_CAIXA".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB297_OPER_CAIXA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB297_OPER_CAIXA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB297_OPER_CAIXA.OrderBy( o => o.CO_OPER_CAIXA ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB297_OPER_CAIXA pela chave primária "CO_OPER_CAIXA".
        /// </summary>
        /// <param name="CO_OPER_CAIXA">Id da chave primária</param>
        /// <returns>Entidade TB297_OPER_CAIXA</returns>
        public static TB297_OPER_CAIXA RetornaPelaChavePrimaria(int CO_OPER_CAIXA)
        {
            return (from tb297 in RetornaTodosRegistros()
                    where tb297.CO_OPER_CAIXA == CO_OPER_CAIXA
                    select tb297).FirstOrDefault();
        }

        #endregion
    }
}