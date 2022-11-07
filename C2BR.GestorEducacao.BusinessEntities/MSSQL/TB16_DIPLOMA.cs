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
    public partial class TB16_DIPLOMA
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
        /// Exclue o registro da tabela TB16_DIPLOMA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB16_DIPLOMA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB16_DIPLOMA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB16_DIPLOMA.</returns>
        public static TB16_DIPLOMA Delete(TB16_DIPLOMA entity)
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
        public static int SaveOrUpdate(TB16_DIPLOMA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB16_DIPLOMA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB16_DIPLOMA.</returns>
        public static TB16_DIPLOMA SaveOrUpdate(TB16_DIPLOMA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB16_DIPLOMA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB16_DIPLOMA.</returns>
        public static TB16_DIPLOMA GetByEntityKey(EntityKey entityKey)
        {
            return (TB16_DIPLOMA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB16_DIPLOMA, ordenados pelo Id do aluno "TB07_ALUNO.CO_ALU".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB16_DIPLOMA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB16_DIPLOMA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB16_DIPLOMA.OrderBy( d => d.TB07_ALUNO.CO_ALU ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB16_DIPLOMA pela chave primária "CO_DIPLOMA".
        /// </summary>
        /// <param name="CO_DIPLOMA">Id da chave primária</param>
        /// <returns>Entidade TB16_DIPLOMA</returns>
        public static TB16_DIPLOMA RetornaPelaChavePrimaria(int CO_DIPLOMA)
        {
            return (from tb16 in RetornaTodosRegistros()
                    where tb16.CO_DIPLOMA == CO_DIPLOMA
                    select tb16).FirstOrDefault();
        }
        #endregion
    }
}