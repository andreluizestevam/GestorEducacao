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
    public partial class TB19_INFOR_CURSO
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
        /// Exclue o registro da tabela TB19_INFOR_CURSO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB19_INFOR_CURSO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB19_INFOR_CURSO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB19_INFOR_CURSO.</returns>
        public static TB19_INFOR_CURSO Delete(TB19_INFOR_CURSO entity)
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
        public static int SaveOrUpdate(TB19_INFOR_CURSO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB19_INFOR_CURSO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB19_INFOR_CURSO.</returns>
        public static TB19_INFOR_CURSO SaveOrUpdate(TB19_INFOR_CURSO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB19_INFOR_CURSO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB19_INFOR_CURSO.</returns>
        public static TB19_INFOR_CURSO GetByEntityKey(EntityKey entityKey)
        {
            return (TB19_INFOR_CURSO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB19_INFOR_CURSO, ordenados pelo Id da unidade "CO_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB19_INFOR_CURSO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB19_INFOR_CURSO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB19_INFOR_CURSO.OrderBy( i => i.CO_EMP ).ThenBy( i => i.CO_CUR ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB19_INFOR_CURSO pelas chaves primárias "CO_EMP" e "CO_CUR".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="CO_CUR">Id da série</param>
        /// <returns>Entidade TB19_INFOR_CURSO</returns>
        public static TB19_INFOR_CURSO RetornaPelaChavePrimaria(int CO_EMP, int CO_CUR)
        {
            return (from tb19 in RetornaTodosRegistros()
                    where tb19.TB01_CURSO.CO_CUR == CO_CUR && tb19.TB01_CURSO.CO_EMP == CO_EMP
                    select tb19).FirstOrDefault();
        }

        #endregion
    }
}