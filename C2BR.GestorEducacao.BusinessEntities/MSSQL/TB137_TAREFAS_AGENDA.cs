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
    public partial class TB137_TAREFAS_AGENDA
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
        /// Exclue o registro da tabela TB137_TAREFAS_AGENDA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB137_TAREFAS_AGENDA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB137_TAREFAS_AGENDA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB137_TAREFAS_AGENDA.</returns>
        public static TB137_TAREFAS_AGENDA Delete(TB137_TAREFAS_AGENDA entity)
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
        public static int SaveOrUpdate(TB137_TAREFAS_AGENDA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB137_TAREFAS_AGENDA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB137_TAREFAS_AGENDA.</returns>
        public static TB137_TAREFAS_AGENDA SaveOrUpdate(TB137_TAREFAS_AGENDA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB137_TAREFAS_AGENDA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB137_TAREFAS_AGENDA.</returns>
        public static TB137_TAREFAS_AGENDA GetByEntityKey(EntityKey entityKey)
        {
            return (TB137_TAREFAS_AGENDA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB137_TAREFAS_AGENDA, ordenados pelo Id "CO_IDENT_TAREF".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB137_TAREFAS_AGENDA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB137_TAREFAS_AGENDA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB137_TAREFAS_AGENDA.OrderBy( t => t.CO_IDENT_TAREF ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna todos os registros da entidade TB137_TAREFAS_AGENDA de acordo com a chave única "CO_CHAVE_UNICA_TAREF".
        /// </summary>
        /// <param name="CO_CHAVE_UNICA_TAREF">Id da chave única</param>
        /// <returns>ObjectQuery com todos os registros da entidade TB137_TAREFAS_AGENDA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB137_TAREFAS_AGENDA> RetornaPelaChaveUnica(double CO_CHAVE_UNICA_TAREF)
        {
            return (from tb137 in RetornaTodosRegistros().Include(typeof(TB03_COLABOR).Name)
                    where tb137.CO_CHAVE_UNICA_TAREF == CO_CHAVE_UNICA_TAREF
                    select tb137).AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TB137_TAREFAS_AGENDA pela chave única "CO_CHAVE_UNICA_TAREF" e pelo código identificador "CO_IDENT_TAREF".
        /// </summary>
        /// <param name="CO_CHAVE_UNICA_TAREF">Id da chave única</param>
        /// <param name="CO_IDENT_TAREF">Código identificador</param>
        /// <returns>Entidade TB137_TAREFAS_AGENDA</returns>
        public static TB137_TAREFAS_AGENDA RetornaPelaChaveUnicaEIdent(double CO_CHAVE_UNICA_TAREF, int CO_IDENT_TAREF)
        {
            return (from tb137 in RetornaTodosRegistros().Include(typeof(TB03_COLABOR).Name)
                    where tb137.CO_CHAVE_UNICA_TAREF.Equals(CO_CHAVE_UNICA_TAREF)
                    && tb137.CO_IDENT_TAREF.Equals(CO_IDENT_TAREF)
                    select tb137).FirstOrDefault();
        }
        #endregion
    }
}