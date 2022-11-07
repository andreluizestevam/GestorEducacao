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
    public partial class TB164_INST_ESP
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
        /// Exclue o registro da tabela TB164_INST_ESP do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB164_INST_ESP entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB164_INST_ESP na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB164_INST_ESP.</returns>
        public static TB164_INST_ESP Delete(TB164_INST_ESP entity)
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
        public static int SaveOrUpdate(TB164_INST_ESP entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB164_INST_ESP na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB164_INST_ESP.</returns>
        public static TB164_INST_ESP SaveOrUpdate(TB164_INST_ESP entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB164_INST_ESP de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB164_INST_ESP.</returns>
        public static TB164_INST_ESP GetByEntityKey(EntityKey entityKey)
        {
            return (TB164_INST_ESP)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB164_INST_ESP, ordenados pelo nome "NO_INST_ESP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB164_INST_ESP de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB164_INST_ESP> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB164_INST_ESP.OrderBy( i => i.NO_INST_ESP ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB164_INST_ESP pela chave primária "CO_INST_ESP".
        /// </summary>
        /// <param name="CO_INST_ESP">Id da chave primária</param>
        /// <returns>Entidade TB164_INST_ESP</returns>
        public static TB164_INST_ESP RetornaPelaChavePrimaria(int CO_INST_ESP)
        {
            return (from tb164 in RetornaTodosRegistros()
                    where tb164.CO_INST_ESP == CO_INST_ESP
                    select tb164).FirstOrDefault();
        }

        #endregion
    }
}