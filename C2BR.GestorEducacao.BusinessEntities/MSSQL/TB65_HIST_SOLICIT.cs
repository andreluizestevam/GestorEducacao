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
    public partial class TB65_HIST_SOLICIT
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
        /// Exclue o registro da tabela TB65_HIST_SOLICIT do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB65_HIST_SOLICIT entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB65_HIST_SOLICIT na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB65_HIST_SOLICIT.</returns>
        public static TB65_HIST_SOLICIT Delete(TB65_HIST_SOLICIT entity)
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
        public static int SaveOrUpdate(TB65_HIST_SOLICIT entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB65_HIST_SOLICIT na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB65_HIST_SOLICIT.</returns>
        public static TB65_HIST_SOLICIT SaveOrUpdate(TB65_HIST_SOLICIT entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB65_HIST_SOLICIT de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB65_HIST_SOLICIT.</returns>
        public static TB65_HIST_SOLICIT GetByEntityKey(EntityKey entityKey)
        {
            return (TB65_HIST_SOLICIT)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB65_HIST_SOLICIT, ordenados pela data de envio da solicitação "DT_ENVIO_SOLI".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB65_HIST_SOLICIT de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB65_HIST_SOLICIT> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB65_HIST_SOLICIT.OrderBy( h => h.DT_ENVIO_SOLI ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB65_HIST_SOLICIT pelas chaves primárias "CO_EMP", "CO_ALU", "CO_CUR", "CO_SOLI_ATEN" e "CO_TIPO_SOLI".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="CO_ALU">Id do aluno</param>
        /// <param name="CO_CUR">Id da série</param>
        /// <param name="CO_SOLI_ATEN">Id da solicitação</param>
        /// <param name="CO_TIPO_SOLI">Id do tipo da solicitação</param>
        /// <returns>Entidade TB65_HIST_SOLICIT</returns>
        public static TB65_HIST_SOLICIT RetornaPelaChavePrimaria(int CO_EMP, int CO_ALU, int CO_CUR, int CO_SOLI_ATEN, int CO_TIPO_SOLI)
        {
            return (from tb65 in RetornaTodosRegistros().Include(typeof(TB64_SOLIC_ATEND).Name)
                    where tb65.CO_EMP == CO_EMP && tb65.CO_ALU == CO_ALU && tb65.CO_CUR == CO_CUR 
                    && tb65.CO_SOLI_ATEN == CO_SOLI_ATEN && tb65.CO_TIPO_SOLI == CO_TIPO_SOLI
                    select tb65).FirstOrDefault();
        }

        #endregion
    }
}
