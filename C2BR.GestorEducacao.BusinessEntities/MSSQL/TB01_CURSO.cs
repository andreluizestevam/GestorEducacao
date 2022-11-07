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
    public partial class TB01_CURSO
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
        /// Exclue o registro da tabela TB01_CURSO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB01_CURSO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB01_CURSO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB01_CURSO.</returns>
        public static TB01_CURSO Delete(TB01_CURSO entity)
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
        public static int SaveOrUpdate(TB01_CURSO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB01_CURSO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB01_CURSO.</returns>
        public static TB01_CURSO SaveOrUpdate(TB01_CURSO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB01_CURSO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB01_CURSO.</returns>
        public static TB01_CURSO GetByEntityKey(EntityKey entityKey)
        {
            return (TB01_CURSO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB01_CURSO, ordenados pelo nome "NO_CUR"
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB01_CURSO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB01_CURSO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB01_CURSO.OrderBy( c => c.NO_CUR ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna todos os registros da entidade TB01_CURSO de acordo com a unidade "CO_EMP" e ordenado pelo nome da série.
        /// </summary>
        /// <param name="CO_EMP">Ida da Unidade</param>
        /// <returns>ObjectQuery com todos os registros da entidade TB01_CURSO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB01_CURSO> RetornaPelaEmpresa(int CO_EMP)
        {
            return GestorEntities.CurrentContext.TB01_CURSO.Where( c => c.CO_EMP == CO_EMP ).OrderBy( c => c.NO_CUR ).AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TB01_CURSO pelas chaves primárias "CO_EMP", "CO_MODU_CUR" e "CO_CUR".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="CO_MODU_CUR">Id da modalidade</param>
        /// <param name="CO_CUR">Id da série</param>
        /// <returns>Entidade TB01_CURSO</returns>
        public static TB01_CURSO RetornaPelaChavePrimaria(int CO_EMP, int CO_MODU_CUR, int CO_CUR)
        {
            return (from tb01 in RetornaTodosRegistros()
                    where tb01.CO_EMP == CO_EMP && tb01.CO_MODU_CUR == CO_MODU_CUR && tb01.CO_CUR == CO_CUR
                    select tb01).FirstOrDefault();
        }

        /// <summary>
        /// Retorna um registro da entidade TB01_CURSO onde o Id "CO_CUR" é o informado no parâmetro.
        /// </summary>
        /// <param name="CO_CUR">Id da série</param>
        /// <returns>Entidade TB01_CURSO</returns>
        public static TB01_CURSO RetornaPeloCoCur(int CO_CUR)
        {
            return (from tb01 in RetornaTodosRegistros()
                    where tb01.CO_CUR == CO_CUR
                    select tb01).FirstOrDefault();
        }

        #endregion
    }
}