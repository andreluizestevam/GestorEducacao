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
    public partial class TB06_TURMAS
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
        /// Exclue o registro da tabela TB06_TURMAS do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB06_TURMAS entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB06_TURMAS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB06_TURMAS.</returns>
        public static TB06_TURMAS Delete(TB06_TURMAS entity)
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
        public static int SaveOrUpdate(TB06_TURMAS entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB06_TURMAS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB06_TURMAS.</returns>
        public static TB06_TURMAS SaveOrUpdate(TB06_TURMAS entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB06_TURMAS de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB06_TURMAS.</returns>
        public static TB06_TURMAS GetByEntityKey(EntityKey entityKey)
        {
            return (TB06_TURMAS)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB06_TURMAS, ordenados pelo nome da turma "TB129_CADTURMAS.NO_TURMA".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB06_TURMAS de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB06_TURMAS> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB06_TURMAS.OrderBy( t => t.TB129_CADTURMAS.NO_TURMA ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB06_TURMAS pelo Id da turma "CO_TUR".
        /// </summary>
        /// <param name="CO_TUR">Id da turma</param>
        /// <returns>Entidade TB06_TURMAS</returns>
        public static TB06_TURMAS RetornaPeloCodigo(int CO_TUR)
        {
            return (from tb06 in RetornaTodosRegistros()
                    where tb06.CO_TUR == CO_TUR
                    select tb06).FirstOrDefault();
        }

        /// <summary>
        /// Retorna um registro da entidade TB06_TURMAS pelo Id da turma "CO_TUR" e pela Id da unidade "CO_EMP".
        /// </summary>
        /// <param name="CO_TUR">Id da turma</param>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <returns>Entidade TB06_TURMAS</returns>
        public static TB06_TURMAS RetornaPeloCodigo(int CO_TUR, int CO_EMP)
        {
            return (from tb06 in RetornaTodosRegistros()
                    where tb06.CO_EMP == CO_EMP && tb06.CO_TUR == CO_TUR
                    select tb06).FirstOrDefault();
        }

        /// <summary>
        /// Retorna um registro da entidade TB06_TURMAS pelas chaves primárias "CO_EMP", "CO_MODU_CUR", "CO_CUR" e "CO_TUR".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="CO_MODU_CUR">Id da modalidade</param>
        /// <param name="CO_CUR">Id da série</param>
        /// <param name="CO_TUR">Id da turma</param>
        /// <returns>Entidade TB06_TURMAS</returns>
        public static TB06_TURMAS RetornaPelaChavePrimaria(int CO_EMP, int CO_MODU_CUR, int CO_CUR, int CO_TUR)
        {
            return (from tb06 in RetornaTodosRegistros()
                    where tb06.CO_EMP == CO_EMP && tb06.CO_MODU_CUR == CO_MODU_CUR && tb06.CO_CUR == CO_CUR && tb06.CO_TUR == CO_TUR
                    select tb06).FirstOrDefault();
        }

        /// <summary>
        /// Retorna todos os registros da entidade TB06_TURMAS de acordo com a unidade "CO_EMP".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <returns>ObjectQuery com todos os registros da entidade TB06_TURMAS de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB06_TURMAS> RetornaPelaEmpresa(int CO_EMP) 
        {
            return (from tb06 in RetornaTodosRegistros()
                    where tb06.CO_EMP == CO_EMP
                    select tb06).AsObjectQuery();
        }

        #endregion
    }
}