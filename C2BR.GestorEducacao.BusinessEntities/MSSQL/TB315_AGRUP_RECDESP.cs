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
    public partial class TB315_AGRUP_RECDESP
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
        /// Exclue o registro da tabela TB315_AGRUP_RECDESP do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB315_AGRUP_RECDESP entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB315_AGRUP_RECDESP na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB315_AGRUP_RECDESP.</returns>
        public static TB315_AGRUP_RECDESP Delete(TB315_AGRUP_RECDESP entity)
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
        public static int SaveOrUpdate(TB315_AGRUP_RECDESP entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB315_AGRUP_RECDESP na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB315_AGRUP_RECDESP.</returns>
        public static TB315_AGRUP_RECDESP SaveOrUpdate(TB315_AGRUP_RECDESP entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB315_AGRUP_RECDESP de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB315_AGRUP_RECDESP.</returns>
        public static TB315_AGRUP_RECDESP GetByEntityKey(EntityKey entityKey)
        {
            return (TB315_AGRUP_RECDESP)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB315_AGRUP_RECDESP, ordenados pelo nome "DE_SITU_AGRUP_RECDESP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB315_AGRUP_RECDESP de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB315_AGRUP_RECDESP> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB315_AGRUP_RECDESP.OrderBy(t => t.DE_SITU_AGRUP_RECDESP).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB315_AGRUP_RECDESP pela chave primária "CO_AGRUP".
        /// </summary>
        /// <param name="CO_AGRUP">Id da chave primária</param>
        /// <returns>Entidade TB315_AGRUP_RECDESP</returns>
        public static TB315_AGRUP_RECDESP RetornaPelaChavePrimaria(int CO_AGRUP)
        {
            return (from tb315 in RetornaTodosRegistros()
                    where tb315.ID_AGRUP_RECDESP == CO_AGRUP
                    select tb315).FirstOrDefault();
        }

        #endregion
    }
}
