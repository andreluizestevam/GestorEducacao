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
    public partial class TB211_SOLIC_DIPLOMA
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
        /// Exclue o registro da tabela TB211_SOLIC_DIPLOMA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB211_SOLIC_DIPLOMA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB211_SOLIC_DIPLOMA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB211_SOLIC_DIPLOMA.</returns>
        public static TB211_SOLIC_DIPLOMA Delete(TB211_SOLIC_DIPLOMA entity)
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
        public static int SaveOrUpdate(TB211_SOLIC_DIPLOMA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB211_SOLIC_DIPLOMA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB211_SOLIC_DIPLOMA.</returns>
        public static TB211_SOLIC_DIPLOMA SaveOrUpdate(TB211_SOLIC_DIPLOMA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB211_SOLIC_DIPLOMA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB211_SOLIC_DIPLOMA.</returns>
        public static TB211_SOLIC_DIPLOMA GetByEntityKey(EntityKey entityKey)
        {
            return (TB211_SOLIC_DIPLOMA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB211_SOLIC_DIPLOMA, ordenados pelo Id do Aluno "TB07_ALUNO.CO_ALU".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB211_SOLIC_DIPLOMA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB211_SOLIC_DIPLOMA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB211_SOLIC_DIPLOMA.OrderBy( s => s.TB07_ALUNO.CO_ALU ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB211_SOLIC_DIPLOMA pela chave primária "ID_SOLIC_DIPLOMA".
        /// </summary>
        /// <param name="ID_SOLIC_DIPLOMA">Id da chave primária</param>
        /// <returns>Entidade TB211_SOLIC_DIPLOMA</returns>
        public static TB211_SOLIC_DIPLOMA RetornaPelaChavePrimaria(int ID_SOLIC_DIPLOMA)
        {
            return (from tb211 in RetornaTodosRegistros()
                    where (tb211.ID_SOLIC_DIPLOMA == ID_SOLIC_DIPLOMA)
                    select tb211).FirstOrDefault();
        }

        #endregion
    }
}