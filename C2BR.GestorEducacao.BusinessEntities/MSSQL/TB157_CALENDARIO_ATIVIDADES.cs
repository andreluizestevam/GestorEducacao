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
    public partial class TB157_CALENDARIO_ATIVIDADES
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
        /// Exclue o registro da tabela TB157_CALENDARIO_ATIVIDADES do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB157_CALENDARIO_ATIVIDADES entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB157_CALENDARIO_ATIVIDADES na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB157_CALENDARIO_ATIVIDADES.</returns>
        public static TB157_CALENDARIO_ATIVIDADES Delete(TB157_CALENDARIO_ATIVIDADES entity)
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
        public static int SaveOrUpdate(TB157_CALENDARIO_ATIVIDADES entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB157_CALENDARIO_ATIVIDADES na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB157_CALENDARIO_ATIVIDADES.</returns>
        public static TB157_CALENDARIO_ATIVIDADES SaveOrUpdate(TB157_CALENDARIO_ATIVIDADES entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB157_CALENDARIO_ATIVIDADES de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB157_CALENDARIO_ATIVIDADES.</returns>
        public static TB157_CALENDARIO_ATIVIDADES GetByEntityKey(EntityKey entityKey)
        {
            return (TB157_CALENDARIO_ATIVIDADES)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB157_CALENDARIO_ATIVIDADES, ordenados pelo Id "CAL_ID_ATIVI_CALEND".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB157_CALENDARIO_ATIVIDADES de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB157_CALENDARIO_ATIVIDADES> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB157_CALENDARIO_ATIVIDADES.OrderBy( c => c.CAL_ID_ATIVI_CALEND ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB157_CALENDARIO_ATIVIDADES pela chave primária "CAL_ID_ATIVI_CALEND".
        /// </summary>
        /// <param name="CAL_ID_ATIVI_CALEND">Id da chave primária</param>
        /// <returns>Entidade TB157_CALENDARIO_ATIVIDADES</returns>
        public static TB157_CALENDARIO_ATIVIDADES RetornaPelaChavePrimaria(int CAL_ID_ATIVI_CALEND)
        {
            return (from tb157 in RetornaTodosRegistros()
                    where tb157.CAL_ID_ATIVI_CALEND == CAL_ID_ATIVI_CALEND
                    select tb157).FirstOrDefault();
        }

        /// <summary>
        /// Retorna todos os registros da entidade TB157_CALENDARIO_ATIVIDADES de acordo com a unidade "CO_EMP".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <returns>ObjectQuery com todos os registros da entidade TB157_CALENDARIO_ATIVIDADES de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB157_CALENDARIO_ATIVIDADES> RetornaPelaEmpresa(int CO_EMP)
        {
            return GestorEntities.CurrentContext.TB157_CALENDARIO_ATIVIDADES.Where( c => (c.TB25_EMPRESA == null || c.TB25_EMPRESA.CO_EMP == CO_EMP) ).OrderBy( c => c.CAL_ID_ATIVI_CALEND ).AsObjectQuery();
        }

        #endregion
    }
}