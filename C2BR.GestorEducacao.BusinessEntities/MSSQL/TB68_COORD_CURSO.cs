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
    public partial class TB68_COORD_CURSO
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
        /// Exclue o registro da tabela TB68_COORD_CURSO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB68_COORD_CURSO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB68_COORD_CURSO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB68_COORD_CURSO.</returns>
        public static TB68_COORD_CURSO Delete(TB68_COORD_CURSO entity)
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
        public static int SaveOrUpdate(TB68_COORD_CURSO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB68_COORD_CURSO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB68_COORD_CURSO.</returns>
        public static TB68_COORD_CURSO SaveOrUpdate(TB68_COORD_CURSO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB68_COORD_CURSO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB68_COORD_CURSO.</returns>
        public static TB68_COORD_CURSO GetByEntityKey(EntityKey entityKey)
        {
            return (TB68_COORD_CURSO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB68_COORD_CURSO, ordenados pelo Id do departamento "CO_DPTO_CUR".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB68_COORD_CURSO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB68_COORD_CURSO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB68_COORD_CURSO.OrderBy( c => c.CO_DPTO_CUR ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB68_COORD_CURSO pelas chaves primárias "CO_EMP", "CO_DPTO_CUR" e "CO_COOR_CUR".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="CO_DPTO_CUR">Id do departamento</param>
        /// <param name="CO_COOR_CUR">Id da coordenação</param>
        /// <returns>Entidade TB68_COORD_CURSO</returns>
        public static TB68_COORD_CURSO RetornaPelaChavePrimaria(int CO_EMP, int CO_DPTO_CUR, int CO_COOR_CUR)
        {
            return (from tb68 in RetornaTodosRegistros()
                    where tb68.CO_COOR_CUR == CO_COOR_CUR && tb68.CO_DPTO_CUR == CO_DPTO_CUR && tb68.CO_EMP == CO_EMP
                    select tb68).FirstOrDefault();
        }

        #endregion
    }
}