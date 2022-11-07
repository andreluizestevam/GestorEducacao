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
    public partial class TB185_GERAD_ENERG
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
        /// Exclue o registro da tabela TB185_GERAD_ENERG do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB185_GERAD_ENERG entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB185_GERAD_ENERG na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB185_GERAD_ENERG.</returns>
        public static TB185_GERAD_ENERG Delete(TB185_GERAD_ENERG entity)
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
        public static int SaveOrUpdate(TB185_GERAD_ENERG entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB185_GERAD_ENERG na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB185_GERAD_ENERG.</returns>
        public static TB185_GERAD_ENERG SaveOrUpdate(TB185_GERAD_ENERG entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB185_GERAD_ENERG de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB185_GERAD_ENERG.</returns>
        public static TB185_GERAD_ENERG GetByEntityKey(EntityKey entityKey )
        {
            return (TB185_GERAD_ENERG)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB185_GERAD_ENERG, ordenados pela descrição "DE_GERAD_ENERG".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB185_GERAD_ENERG de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB185_GERAD_ENERG> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB185_GERAD_ENERG.OrderBy( g => g.DE_GERAD_ENERG ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB185_GERAD_ENERG pela chave primária "CO_SIGLA_GERAD_ENERG".
        /// </summary>
        /// <param name="CO_SIGLA_GERAD_ENERG">Id da chave primária</param>
        /// <returns>Entidade TB185_GERAD_ENERG</returns>
        public static TB185_GERAD_ENERG RetornaPelaChavePrimaria(string CO_SIGLA_GERAD_ENERG)
        {
            return (from tb185 in RetornaTodosRegistros()
                    where tb185.CO_SIGLA_GERAD_ENERG == CO_SIGLA_GERAD_ENERG
                    select tb185).FirstOrDefault();
        }

        /// <summary>
        /// Retorna o primeiro registro da entidade TB185_GERAD_ENERG pelo código do gerador de energia "CO_GERAD_ENERG".
        /// </summary>
        /// <param name="CO_GERAD_ENERG">Código do gerador de energia</param>
        /// <returns>Entidade TB185_GERAD_ENERG</returns>
        public static TB185_GERAD_ENERG RetornaPeloCodigo(int CO_GERAD_ENERG)
        {
            return (from tb185 in RetornaTodosRegistros()
                    where tb185.CO_GERAD_ENERG == CO_GERAD_ENERG
                    select tb185).FirstOrDefault();
        }

        #endregion
    }
}