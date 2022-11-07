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
    public partial class TB226_CARTEIRA_BANCO
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
        /// Exclue o registro da tabela TB226_CARTEIRA_BANCO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB226_CARTEIRA_BANCO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB226_CARTEIRA_BANCO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB226_CARTEIRA_BANCO.</returns>
        public static TB226_CARTEIRA_BANCO Delete(TB226_CARTEIRA_BANCO entity)
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
        public static int SaveOrUpdate(TB226_CARTEIRA_BANCO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB226_CARTEIRA_BANCO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB226_CARTEIRA_BANCO.</returns>
        public static TB226_CARTEIRA_BANCO SaveOrUpdate(TB226_CARTEIRA_BANCO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB226_CARTEIRA_BANCO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB226_CARTEIRA_BANCO.</returns>
        public static TB226_CARTEIRA_BANCO GetByEntityKey(EntityKey entityKey)
        {
            return (TB226_CARTEIRA_BANCO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB226_CARTEIRA_BANCO, ordenados pela descrição "DE_CARTEIRA".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB226_CARTEIRA_BANCO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB226_CARTEIRA_BANCO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB226_CARTEIRA_BANCO.OrderBy( c => c.DE_CARTEIRA ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB226_CARTEIRA_BANCO pelas chaves primárias "IDEBANCO" e "CO_CARTEIRA".
        /// </summary>
        /// <param name="IDEBANCO">Id do banco</param>
        /// <param name="CO_CARTEIRA">Código da carteira</param>
        /// <returns>Entidade TB226_CARTEIRA_BANCO</returns>
        public static TB226_CARTEIRA_BANCO RetornaPelaChavePrimaria(string IDEBANCO, string CO_CARTEIRA)
        {
            return (from tb226 in RetornaTodosRegistros()
                    where tb226.TB29_BANCO.IDEBANCO == IDEBANCO && tb226.CO_CARTEIRA == CO_CARTEIRA
                    select tb226).FirstOrDefault();
        }

        #endregion
    }
}