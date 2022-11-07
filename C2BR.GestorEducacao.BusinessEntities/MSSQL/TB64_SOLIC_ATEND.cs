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
    public partial class TB64_SOLIC_ATEND
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
        /// Exclue o registro da tabela TB64_SOLIC_ATEND do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB64_SOLIC_ATEND entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB64_SOLIC_ATEND na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB64_SOLIC_ATEND.</returns>
        public static TB64_SOLIC_ATEND Delete(TB64_SOLIC_ATEND entity)
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
        public static int SaveOrUpdate(TB64_SOLIC_ATEND entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB64_SOLIC_ATEND na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB64_SOLIC_ATEND.</returns>
        public static TB64_SOLIC_ATEND SaveOrUpdate(TB64_SOLIC_ATEND entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB64_SOLIC_ATEND de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB64_SOLIC_ATEND.</returns>
        public static TB64_SOLIC_ATEND GetByEntityKey(EntityKey entityKey)
        {
            return (TB64_SOLIC_ATEND)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB64_SOLIC_ATEND, ordenados pelo Id do aluno "CO_ALU".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB64_SOLIC_ATEND de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB64_SOLIC_ATEND> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB64_SOLIC_ATEND.OrderBy( s => s.CO_ALU ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB64_SOLIC_ATEND pelas chaves primárias "CO_ALU", "CO_EMP", "CO_CUR" e "CO_SOLI_ATEN".
        /// </summary>
        /// <param name="CO_ALU">Id do aluno</param>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="CO_CUR">Id da série</param>
        /// <param name="CO_SOLI_ATEN">Id da solicitação</param>
        /// <returns>Entidade TB64_SOLIC_ATEND</returns>
        public static TB64_SOLIC_ATEND RetornaPelaChavePrimaria(int CO_ALU, int CO_EMP, int CO_CUR, int CO_SOLI_ATEN)
        {
            return (from tb64 in RetornaTodosRegistros().Include(typeof(TB65_HIST_SOLICIT).Name).Include(typeof(TB25_EMPRESA).Name)
                    where tb64.CO_ALU == CO_ALU && tb64.CO_EMP == CO_EMP && tb64.CO_CUR == CO_CUR && tb64.CO_SOLI_ATEN == CO_SOLI_ATEN
                    select tb64).FirstOrDefault();
        }

        /// <summary>
        /// Retorna um registro da entidade TB64_SOLIC_ATEND pelo Id da solicitação "CO_SOLI_ATEN".
        /// </summary>
        /// <param name="CO_SOLI_ATEN">Id da solicitação</param>
        /// <returns>Entidade TB64_SOLIC_ATEND</returns>
        public static TB64_SOLIC_ATEND RetornaPeloID(int CO_SOLI_ATEN)
        {
            return (from tb64 in RetornaTodosRegistros().Include(typeof(TB65_HIST_SOLICIT).Name)
                    where tb64.CO_SOLI_ATEN == CO_SOLI_ATEN
                    select tb64).FirstOrDefault();
        }

        #endregion
    }
}