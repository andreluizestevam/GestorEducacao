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
    public partial class TB247_UNIDADE_PERFIL_DESEMPENHO
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
        /// Exclue o registro da tabela TB247_UNIDADE_PERFIL_DESEMPENHO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB247_UNIDADE_PERFIL_DESEMPENHO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB247_UNIDADE_PERFIL_DESEMPENHO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB247_UNIDADE_PERFIL_DESEMPENHO.</returns>
        public static TB247_UNIDADE_PERFIL_DESEMPENHO Delete(TB247_UNIDADE_PERFIL_DESEMPENHO entity)
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
        public static int SaveOrUpdate(TB247_UNIDADE_PERFIL_DESEMPENHO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB247_UNIDADE_PERFIL_DESEMPENHO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB247_UNIDADE_PERFIL_DESEMPENHO.</returns>
        public static TB247_UNIDADE_PERFIL_DESEMPENHO SaveOrUpdate(TB247_UNIDADE_PERFIL_DESEMPENHO entity)
        {
            SaveOrUpdate(entity, true);
            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB247_UNIDADE_PERFIL_DESEMPENHO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB247_UNIDADE_PERFIL_DESEMPENHO.</returns>
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
        /// Retorna um registro da entidade TB247_UNIDADE_PERFIL_DESEMPENHO pela chave primária "ID_UNIDADE_PERFIL_DESEM".
        /// </summary>
        /// <param name="ID_UNIDADE_PERFIL_DESEM">Id da chave primária</param>
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