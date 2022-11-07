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
    public partial class TB184_FONTE_ENERG
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
        /// Exclue o registro da tabela TB184_FONTE_ENERG do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB184_FONTE_ENERG entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB184_FONTE_ENERG na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB184_FONTE_ENERG.</returns>
        public static TB184_FONTE_ENERG Delete(TB184_FONTE_ENERG entity)
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
        public static int SaveOrUpdate(TB184_FONTE_ENERG entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB184_FONTE_ENERG na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB184_FONTE_ENERG.</returns>
        public static TB184_FONTE_ENERG SaveOrUpdate(TB184_FONTE_ENERG entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB184_FONTE_ENERG de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB184_FONTE_ENERG.</returns>
        public static TB184_FONTE_ENERG GetByEntityKey(EntityKey entityKey )
        {
            return (TB184_FONTE_ENERG)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB184_FONTE_ENERG, ordenados pela descrição "DE_FONTE_ENERG".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB184_FONTE_ENERG de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB184_FONTE_ENERG> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB184_FONTE_ENERG.OrderBy( f => f.DE_FONTE_ENERG ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB184_FONTE_ENERG pela chave primária "CO_SIGLA_FONTE_ENERG".
        /// </summary>
        /// <param name="CO_SIGLA_FONTE_ENERG">Id da chave primária</param>
        /// <returns>Entidade TB184_FONTE_ENERG</returns>
        public static TB184_FONTE_ENERG RetornaPelaChavePrimaria(string CO_SIGLA_FONTE_ENERG)
        {
            return (from tb184 in RetornaTodosRegistros()
                    where tb184.CO_SIGLA_FONTE_ENERG == CO_SIGLA_FONTE_ENERG
                    select tb184).FirstOrDefault();
        }

        /// <summary>
        /// Retorna o primeiro registro da entidade TB184_FONTE_ENERG pelo código da fonte de energia "CO_FONTE_ENERG".
        /// </summary>
        /// <param name="CO_FONTE_ENERG">Código da fonte de energia</param>
        /// <returns>Entidade TB184_FONTE_ENERG</returns>
        public static TB184_FONTE_ENERG RetornaPelaChavePrimaria(int CO_FONTE_ENERG)
        {
            return (from tb184 in RetornaTodosRegistros()
                    where tb184.CO_FONTE_ENERG == CO_FONTE_ENERG
                    select tb184).FirstOrDefault();
        }

        #endregion
    }
}