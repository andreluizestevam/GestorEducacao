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
    public partial class TB32_CLASSIF_ACER
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
        /// Exclue o registro da tabela TB32_CLASSIF_ACER do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB32_CLASSIF_ACER entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB32_CLASSIF_ACER na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB32_CLASSIF_ACER.</returns>
        public static TB32_CLASSIF_ACER Delete(TB32_CLASSIF_ACER entity)
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
        public static int SaveOrUpdate(TB32_CLASSIF_ACER entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB32_CLASSIF_ACER na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB32_CLASSIF_ACER.</returns>
        public static TB32_CLASSIF_ACER SaveOrUpdate(TB32_CLASSIF_ACER entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB32_CLASSIF_ACER de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB32_CLASSIF_ACER.</returns>
        public static TB32_CLASSIF_ACER GetByEntityKey(EntityKey entityKey)
        {
            return (TB32_CLASSIF_ACER)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB32_CLASSIF_ACER, ordenados pelo nome "NO_CLAS_ACER".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB32_CLASSIF_ACER de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB32_CLASSIF_ACER> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB32_CLASSIF_ACER.OrderBy( c => c.NO_CLAS_ACER ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB32_CLASSIF_ACER pela chave primária "CO_CLAS_ACER".
        /// </summary>
        /// <param name="CO_CLAS_ACER">Id da chave primária</param>
        /// <returns>Entidade TB32_CLASSIF_ACER</returns>
        public static TB32_CLASSIF_ACER RetornaPelaChavePrimaria(int CO_CLAS_ACER)
        {
            return (from tb32 in RetornaTodosRegistros()
                    where tb32.CO_CLAS_ACER.Equals(CO_CLAS_ACER)
                    select tb32).FirstOrDefault();
        }

        /// <summary>
        /// Retorna todos os registros da entidade TB32_CLASSIF_ACER de acordo com a área de conhecimento "CO_AREACON".
        /// </summary>
        /// <param name="CO_AREACON">Id da área de conhecimento</param>
        /// <returns>ObjectQuery com todos os registros da entidade TB32_CLASSIF_ACER de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB32_CLASSIF_ACER> RetornaPelaAreaConhecimento(int CO_AREACON)
        {
            return GestorEntities.CurrentContext.TB32_CLASSIF_ACER.Where( c => c.TB31_AREA_CONHEC.CO_AREACON == CO_AREACON).OrderBy( c => c.NO_CLAS_ACER ).AsObjectQuery();
        }

        #endregion
    }
}
