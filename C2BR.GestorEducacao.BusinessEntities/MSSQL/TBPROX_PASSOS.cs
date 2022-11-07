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
    public partial class TBPROX_PASSOS
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
        /// Exclue o registro da tabela TBPROX_PASSOS do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBPROX_PASSOS entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBPROX_PASSOS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBPROX_PASSOS.</returns>
        public static TBPROX_PASSOS Delete(TBPROX_PASSOS entity)
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
        public static int SaveOrUpdate(TBPROX_PASSOS entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBPROX_PASSOS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBPROX_PASSOS.</returns>
        public static TBPROX_PASSOS SaveOrUpdate(TBPROX_PASSOS entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBPROX_PASSOS de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBPROX_PASSOS.</returns>
        public static TBPROX_PASSOS GetByEntityKey(EntityKey entityKey)
        {
            return (TBPROX_PASSOS)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBPROX_PASSOS, ordenados pelo número de ordem de menu "CO_ORDEM_MENU".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBPROX_PASSOS de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBPROX_PASSOS> RetornaTodosRegistros()
        {
            GestorEntities.CurrentContext.TBPROX_PASSOS.EnablePlanCaching = false;

            return GestorEntities.CurrentContext.TBPROX_PASSOS.OrderBy( p => p.CO_ORDEM_MENU ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TBPROX_PASSOS pela chave primária "CO_PROXIPASSOS".
        /// </summary>
        /// <param name="CO_PROXIPASSOS">Id da chave primária</param>
        /// <returns>Entidade TBPROX_PASSOS</returns>
        public static TBPROX_PASSOS RetornaPelaChavePrimaria(int CO_PROXIPASSOS)
        {
            return (from tbProxPassos in RetornaTodosRegistros()
                    where tbProxPassos.CO_PROXIPASSOS == CO_PROXIPASSOS
                    select tbProxPassos).FirstOrDefault();
        }

        /// <summary>
        /// Retorna todos os registros da entidade TBPROX_PASSOS de acordo com o módulo "ideAdmModulo".
        /// </summary>
        /// <param name="ideAdmModulo">Id do módulo</param>
        /// <returns>ObjectQuery com todos os registros da entidade TBPROX_PASSOS de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBPROX_PASSOS> RetornaPeloIDeAdmModulo(int ideAdmModulo)
        {
            return (from tbProxPassos in RetornaTodosRegistros()
                    where tbProxPassos.ADMMODULO.ideAdmModulo == ideAdmModulo
                    select tbProxPassos).AsObjectQuery();
        }

        #endregion     
    }
}
