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
    public partial class TB89_UNIDADES
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
        /// Exclue o registro da tabela TB89_UNIDADES do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB89_UNIDADES entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB89_UNIDADES na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB89_UNIDADES.</returns>
        public static TB89_UNIDADES Delete(TB89_UNIDADES entity)
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
        public static int SaveOrUpdate(TB89_UNIDADES entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB89_UNIDADES na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB89_UNIDADES.</returns>
        public static TB89_UNIDADES SaveOrUpdate(TB89_UNIDADES entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB89_UNIDADES de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB89_UNIDADES.</returns>
        public static TB89_UNIDADES GetByEntityKey(EntityKey entityKey )
        {
            return (TB89_UNIDADES)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB89_UNIDADES, ordenados pelo nome "NO_UNID_ITEM".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB89_UNIDADES de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB89_UNIDADES> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB89_UNIDADES.OrderBy( u => u.NO_UNID_ITEM ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB89_UNIDADES pela chave primária "CO_UNID_ITEM".
        /// </summary>
        /// <param name="CO_UNID_ITEM">Id da chave primária</param>
        /// <returns>Entidade TB89_UNIDADES</returns>
        public static TB89_UNIDADES RetornaPelaChavePrimaria(int CO_UNID_ITEM)
        {
            return (from tb89 in RetornaTodosRegistros()
                    where tb89.CO_UNID_ITEM == CO_UNID_ITEM
                    select tb89).FirstOrDefault();
        }

        #endregion
    }
}