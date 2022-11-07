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
    public partial class TB154_PLANT_COLABOR
    {
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
        /// Exclue o registro da tabela TB154_PLANT_COLABOR do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB154_PLANT_COLABOR entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB154_PLANT_COLABOR na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB154_PLANT_COLABOR.</returns>
        public static TB154_PLANT_COLABOR Delete(TB154_PLANT_COLABOR entity)
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
        public static int SaveOrUpdate(TB154_PLANT_COLABOR entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB154_PLANT_COLABOR na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB154_PLANT_COLABOR.</returns>
        public static TB154_PLANT_COLABOR SaveOrUpdate(TB154_PLANT_COLABOR entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB154_PLANT_COLABOR de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB154_PLANT_COLABOR.</returns>
        public static TB154_PLANT_COLABOR GetByEntityKey(EntityKey entityKey)
        {
            return (TB154_PLANT_COLABOR)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB154_PLANT_COLABOR, ordenados pelo nome "NO_DEPTO".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB154_PLANT_COLABOR de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB154_PLANT_COLABOR> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB154_PLANT_COLABOR.AsObjectQuery();
        }

        public static ObjectQuery<TB154_PLANT_COLABOR> RetornaPeloCoColPlantao(int CO_COL, int ID_TIPO_PLANT)
        {
            return (from TB154 in RetornaTodosRegistros()
                    where TB154.CO_COL == CO_COL
                    && TB154.ID_TIPO_PLANT == ID_TIPO_PLANT
                    select TB154).OrderBy(c => c.CO_COL).AsObjectQuery();
        }

        #endregion

    }
}
