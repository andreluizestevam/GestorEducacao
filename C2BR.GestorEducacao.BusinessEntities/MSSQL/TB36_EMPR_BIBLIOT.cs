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
    public partial class TB36_EMPR_BIBLIOT
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
        /// Exclue o registro da tabela TB36_EMPR_BIBLIOT do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB36_EMPR_BIBLIOT entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB36_EMPR_BIBLIOT na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB36_EMPR_BIBLIOT.</returns>
        public static TB36_EMPR_BIBLIOT Delete(TB36_EMPR_BIBLIOT entity)
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
        public static int SaveOrUpdate(TB36_EMPR_BIBLIOT entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB36_EMPR_BIBLIOT na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB36_EMPR_BIBLIOT.</returns>
        public static TB36_EMPR_BIBLIOT SaveOrUpdate(TB36_EMPR_BIBLIOT entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB36_EMPR_BIBLIOT de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB36_EMPR_BIBLIOT.</returns>
        public static TB36_EMPR_BIBLIOT GetByEntityKey(EntityKey entityKey)
        {
            return (TB36_EMPR_BIBLIOT)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB36_EMPR_BIBLIOT, ordenados pela data de empréstimo "DT_EMPR_BIBLIOT".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB36_EMPR_BIBLIOT de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB36_EMPR_BIBLIOT> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB36_EMPR_BIBLIOT.OrderBy( e => e.DT_EMPR_BIBLIOT ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB36_EMPR_BIBLIOT pela chave primária "CO_NUM_EMP".
        /// </summary>
        /// <param name="CO_NUM_EMP">Id da chave primária</param>
        /// <returns>Entidade TB36_EMPR_BIBLIOT</returns>
        public static TB36_EMPR_BIBLIOT RetornaPelaChavePrimaria(int CO_NUM_EMP)
        {
            return (from tb36 in RetornaTodosRegistros()
                    where tb36.CO_NUM_EMP == CO_NUM_EMP
                    select tb36).FirstOrDefault();
        }

        #endregion
    }
}