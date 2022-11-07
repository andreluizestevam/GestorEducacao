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
    public partial class TB77_DPTO_CURSO
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
        /// Exclue o registro da tabela TB77_DPTO_CURSO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB77_DPTO_CURSO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB77_DPTO_CURSO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB77_DPTO_CURSO.</returns>
        public static TB77_DPTO_CURSO Delete(TB77_DPTO_CURSO entity)
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
        public static int SaveOrUpdate(TB77_DPTO_CURSO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB77_DPTO_CURSO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB77_DPTO_CURSO.</returns>
        public static TB77_DPTO_CURSO SaveOrUpdate(TB77_DPTO_CURSO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB77_DPTO_CURSO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB77_DPTO_CURSO.</returns>
        public static TB77_DPTO_CURSO GetByEntityKey(EntityKey entityKey)
        {
            return (TB77_DPTO_CURSO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB77_DPTO_CURSO, ordenados pelo nome "NO_DPTO_CUR".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB77_DPTO_CURSO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB77_DPTO_CURSO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB77_DPTO_CURSO.OrderBy( d => d.NO_DPTO_CUR ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB77_DPTO_CURSO pela chave primária "CO_DPTO_CUR".
        /// </summary>
        /// <param name="CO_DPTO_CUR">Id da chave primária</param>
        /// <returns>Entidade TB77_DPTO_CURSO</returns>
        public static TB77_DPTO_CURSO RetornaPelaChavePrimaria(int CO_DPTO_CUR)
        {
            return (from tb77 in RetornaTodosRegistros()
                    where tb77.CO_DPTO_CUR == CO_DPTO_CUR
                    select tb77).FirstOrDefault();
        }

        /// <summary>
        /// Retorna todos os registros da entidade TB77_DPTO_CURSO de acordo com a unidade "CO_EMP".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <returns>ObjectQuery com todos os registros da entidade TB77_DPTO_CURSO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB77_DPTO_CURSO> RetornaPelaEmpresa(int CO_EMP)
        {
            return GestorEntities.CurrentContext.TB77_DPTO_CURSO.Where(d => d.CO_EMP == CO_EMP).OrderBy( d => d.NO_DPTO_CUR ).AsObjectQuery();
        }

        #endregion
    }
}