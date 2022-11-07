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
    public partial class TB908_SUBAREA
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
        /// Exclue o registro da tabela TB908_SUBAREA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB908_SUBAREA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB908_SUBAREA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB908_SUBAREA.</returns>
        public static TB908_SUBAREA Delete(TB908_SUBAREA entity)
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
        public static int SaveOrUpdate(TB908_SUBAREA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB908_SUBAREA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB908_SUBAREA.</returns>
        public static TB908_SUBAREA SaveOrUpdate(TB908_SUBAREA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB908_SUBAREA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB908_SUBAREA.</returns>
        public static TB908_SUBAREA GetByEntityKey(EntityKey entityKey)
        {
            return (TB908_SUBAREA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB908_SUBAREA, ordenados pelo nome "NO_CIDADE".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB908_SUBAREA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB908_SUBAREA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB908_SUBAREA.OrderBy(sb => sb.NM_SUBAREA).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB908_SUBAREA pela chave primária "CO_CIDADE".
        /// </summary>
        /// <param name="CO_CIDADE">Id da chave primária</param>
        /// <returns>Entidade TB908_SUBAREA</returns>
        public static TB908_SUBAREA RetornaPelaChavePrimaria(int ID_SUBAREA)
        {
            return (from tb908 in RetornaTodosRegistros()
                    where tb908.ID_SUBAREA == ID_SUBAREA
                    select tb908).FirstOrDefault();
        }

        public static ObjectQuery<TB908_SUBAREA> RetornaPelaArea(int ID_AREA)
        {
            return (from tb908 in RetornaTodosRegistros().Include(typeof(TB907_AREA).Name)
                    where tb908.TB907_AREA.ID_AREA == ID_AREA
                    select tb908).OrderBy(r => r.NM_SUBAREA).AsObjectQuery();
        }
        /// <summary>
        /// Retorna todos os registros da entidade TB908_SUBAREA de acordo com a UF "CO_UF".
        /// </summary>
        /// <param name="CO_UF">Id da UF</param>
        /// <returns>ObjectQuery com todos os registros da entidade TB908_SUBAREA de acordo com a filtragem desenvolvida.</returns>

        #endregion
    }
}
