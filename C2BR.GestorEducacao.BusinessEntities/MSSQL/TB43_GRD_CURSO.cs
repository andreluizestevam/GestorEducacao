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
    public partial class TB43_GRD_CURSO
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
        /// Exclue o registro da tabela TB43_GRD_CURSO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB43_GRD_CURSO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB43_GRD_CURSO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB43_GRD_CURSO.</returns>
        public static TB43_GRD_CURSO Delete(TB43_GRD_CURSO entity)
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
        public static int SaveOrUpdate(TB43_GRD_CURSO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB43_GRD_CURSO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB43_GRD_CURSO.</returns>
        public static TB43_GRD_CURSO SaveOrUpdate(TB43_GRD_CURSO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB43_GRD_CURSO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB43_GRD_CURSO.</returns>
        public static TB43_GRD_CURSO GetByEntityKey(EntityKey entityKey)
        {
            return (TB43_GRD_CURSO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB43_GRD_CURSO, ordenados pelo Id da série "CO_CUR".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB43_GRD_CURSO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB43_GRD_CURSO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB43_GRD_CURSO.OrderBy( g => g.CO_CUR ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB43_GRD_CURSO pelas chaves primárias "CO_EMP", "CO_ANO_GRADE", "CO_CUR" e "CO_MAT".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="CO_ANO_GRADE">Ano da grade</param>
        /// <param name="CO_CUR">Id da série</param>
        /// <param name="CO_MAT">Id da matéria</param>
        /// <returns>Entidade TB43_GRD_CURSO</returns>
        public static TB43_GRD_CURSO RetornaPelaChavePrimaria(int CO_EMP, string CO_ANO_GRADE, int CO_CUR, int CO_MAT)
        {
            return (from tb43 in RetornaTodosRegistros()
                    where tb43.CO_EMP == CO_EMP && tb43.CO_ANO_GRADE == CO_ANO_GRADE && tb43.CO_CUR == CO_CUR && tb43.CO_MAT == CO_MAT
                    select tb43).FirstOrDefault();
        }

        /// <summary>
        /// Retorna todos os registros da entidade TB43_GRD_CURSO de acordo com a unidade "CO_EMP".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <returns>ObjectQuery com todos os registros da entidade TB43_GRD_CURSO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB43_GRD_CURSO> RetornaPelaEmpresa(int CO_EMP)
        {
            return GestorEntities.CurrentContext.TB43_GRD_CURSO.Where( g => g.CO_EMP == CO_EMP ).OrderBy( g => g.CO_CUR ).AsObjectQuery();
        }

        #endregion
    }
}