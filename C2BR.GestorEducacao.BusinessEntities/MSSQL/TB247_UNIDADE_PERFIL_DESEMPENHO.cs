//---> Inicializa��o  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Text;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;

//===> In�cio das Regras de Neg�cios
//---> Localiza��o do Arquivo da Funcionalidade no Ambiente da Solu��o
namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class TB247_UNIDADE_PERFIL_DESEMPENHO
    {
        #region M�todos

        #region M�todos B�sicos

        /// <summary>
        /// Salva as altera��es do contexto na base de dados.
        /// </summary>
        /// <returns>O n�mero de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int SaveChanges()
        {
            return GestorEntities.CurrentContext.SaveChanges();
        }

        /// <summary>
        /// Exclue o registro da tabela TB247_UNIDADE_PERFIL_DESEMPENHO do Contexto e voc� pode escolher se deve persistir, ou n�o, as altera��es na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual ser� executada a a��o.</param>
        /// <param name="saveChanges">Determina se ir� persistir as altera��es na base de dados.</param>
        /// <returns>O n�mero de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB247_UNIDADE_PERFIL_DESEMPENHO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB247_UNIDADE_PERFIL_DESEMPENHO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual ser� executada a a��o.</param>
        /// <returns>A pr�pria entidade TB247_UNIDADE_PERFIL_DESEMPENHO.</returns>
        public static TB247_UNIDADE_PERFIL_DESEMPENHO Delete(TB247_UNIDADE_PERFIL_DESEMPENHO entity)
        {
            Delete(entity, true);
            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Faz a verifica��o para saber se inser��o ou altera��o e executa a a��o necess�ria; voc� pode escolher se deve persistir, ou n�o, as altera��es na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual ser� executada a a��o.</param>
        /// <param name="saveChanges">Determina se ir� persistir as altera��es na base de dados.</param>
        /// <returns>O n�mero de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int SaveOrUpdate(TB247_UNIDADE_PERFIL_DESEMPENHO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verifica��o do estado atual da Entidade e salva/altera/deleta o registro da tabela TB247_UNIDADE_PERFIL_DESEMPENHO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual ser� executada a a��o.</param>
        /// <returns>A pr�pria entidade TB247_UNIDADE_PERFIL_DESEMPENHO.</returns>
        public static TB247_UNIDADE_PERFIL_DESEMPENHO SaveOrUpdate(TB247_UNIDADE_PERFIL_DESEMPENHO entity)
        {
            SaveOrUpdate(entity, true);
            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB247_UNIDADE_PERFIL_DESEMPENHO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A pr�pria entidade TB247_UNIDADE_PERFIL_DESEMPENHO.</returns>
        public static TB247_UNIDADE_PERFIL_DESEMPENHO GetByEntityKey(EntityKey entityKey)
        {
            return (TB247_UNIDADE_PERFIL_DESEMPENHO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB247_UNIDADE_PERFIL_DESEMPENHO, ordenados pelo Id da unidade "TB06_TURMAS.CO_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB247_UNIDADE_PERFIL_DESEMPENHO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB247_UNIDADE_PERFIL_DESEMPENHO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB247_UNIDADE_PERFIL_DESEMPENHO.OrderBy( u => u.TB06_TURMAS.CO_EMP ).AsObjectQuery();
        }
        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB247_UNIDADE_PERFIL_DESEMPENHO pela chave prim�ria "ID_UNIDADE_PERFIL_DESEM".
        /// </summary>
        /// <param name="ID_UNIDADE_PERFIL_DESEM">Id da chave prim�ria</param>
        /// <returns>Entidade TB247_UNIDADE_PERFIL_DESEMPENHO</returns>
        public static TB247_UNIDADE_PERFIL_DESEMPENHO RetornaPelaChavePrimaria(int ID_UNIDADE_PERFIL_DESEM)
        {
            return (from tb247 in RetornaTodosRegistros()
                    where tb247.ID_UNIDADE_PERFIL_DESEM == ID_UNIDADE_PERFIL_DESEM
                    select tb247).FirstOrDefault();
        }
        #endregion
    }
}