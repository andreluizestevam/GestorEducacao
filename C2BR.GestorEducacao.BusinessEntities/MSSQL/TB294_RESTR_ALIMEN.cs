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
    public partial class TB294_RESTR_ALIMEN
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
        /// Exclue o registro da tabela TB294_RESTR_ALIMEN do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB294_RESTR_ALIMEN entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB294_RESTR_ALIMEN na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB294_RESTR_ALIMEN.</returns>
        public static TB294_RESTR_ALIMEN Delete(TB294_RESTR_ALIMEN entity)
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
        public static int SaveOrUpdate(TB294_RESTR_ALIMEN entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB294_RESTR_ALIMEN na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB294_RESTR_ALIMEN.</returns>
        public static TB294_RESTR_ALIMEN SaveOrUpdate(TB294_RESTR_ALIMEN entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB294_RESTR_ALIMEN de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB294_RESTR_ALIMEN.</returns>
        public static TB294_RESTR_ALIMEN GetByEntityKey(EntityKey entityKey)
        {
            return (TB294_RESTR_ALIMEN)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB294_RESTR_ALIMEN, ordenados pelo Id "ID_RESTR_ALIMEN".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB294_RESTR_ALIMEN de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB294_RESTR_ALIMEN> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB294_RESTR_ALIMEN.OrderBy( r => r.ID_RESTR_ALIMEN ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB294_RESTR_ALIMEN pela chave primária "ID_RESTR_ALIMEN".
        /// </summary>
        /// <param name="ID_RESTR_ALIMEN">Id da chave primária</param>
        /// <returns>Entidade TB294_RESTR_ALIMEN</returns>
        public static TB294_RESTR_ALIMEN RetornaPelaChavePrimaria(int ID_RESTR_ALIMEN)
        {
            return (from tb294 in RetornaTodosRegistros()
                    where tb294.ID_RESTR_ALIMEN == ID_RESTR_ALIMEN
                    select tb294).FirstOrDefault();
        }

        #endregion
    }
}