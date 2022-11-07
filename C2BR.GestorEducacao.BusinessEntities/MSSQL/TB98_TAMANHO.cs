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
    public partial class TB98_TAMANHO
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
        /// Exclue o registro da tabela TB98_TAMANHO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB98_TAMANHO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB98_TAMANHO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB98_TAMANHO.</returns>
        public static TB98_TAMANHO Delete(TB98_TAMANHO entity)
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
        public static int SaveOrUpdate(TB98_TAMANHO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB98_TAMANHO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB98_TAMANHO.</returns>
        public static TB98_TAMANHO SaveOrUpdate(TB98_TAMANHO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB98_TAMANHO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB98_TAMANHO.</returns>
        public static TB98_TAMANHO GetByEntityKey(EntityKey entityKey )
        {
            return (TB98_TAMANHO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB98_TAMANHO, ordenados pela descrição "DES_TAMANHO".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB98_TAMANHO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB98_TAMANHO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB98_TAMANHO.OrderBy( t => t.DES_TAMANHO ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB98_TAMANHO pela chave primária "CO_TAMANHO".
        /// </summary>
        /// <param name="CO_TAMANHO">Id da chave primária</param>
        /// <returns>Entidade TB98_TAMANHO</returns>
        public static TB98_TAMANHO RetornaPelaChavePrimaria(int CO_TAMANHO)
        {
            return (from tb98 in RetornaTodosRegistros()
                    where tb98.CO_TAMANHO == CO_TAMANHO
                    select tb98).FirstOrDefault();
        }

        #endregion
    }
}