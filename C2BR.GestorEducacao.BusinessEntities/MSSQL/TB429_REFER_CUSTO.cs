using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;

namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class TB429_REFER_CUSTO
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
        /// Exclue o registro da tabela TB425_EMAIL_USUAR_GPARC do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB429_REFER_CUSTO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB425_EMAIL_USUAR_GPARC na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB425_EMAIL_USUAR_GPARC.</returns>
        public static TB429_REFER_CUSTO Delete(TB429_REFER_CUSTO entity)
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
        public static int SaveOrUpdate(TB429_REFER_CUSTO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB425_EMAIL_USUAR_GPARC na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB425_EMAIL_USUAR_GPARC.</returns>
        public static TB429_REFER_CUSTO SaveOrUpdate(TB429_REFER_CUSTO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB425_EMAIL_USUAR_GPARC de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB425_EMAIL_USUAR_GPARC.</returns>
        public static TB429_REFER_CUSTO GetByEntityKey(EntityKey entityKey)
        {
            return (TB429_REFER_CUSTO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB425_EMAIL_USUAR_GPARC.
        /// </summary>
        public static ObjectQuery<TB429_REFER_CUSTO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB429_REFER_CUSTO.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TB425_EMAIL_USUAR_GPARC pela chave primária "ID_EXAME".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TB425_EMAIL_USUAR_GPARC</returns>
        public static TB429_REFER_CUSTO RetornaPelaChavePrimaria(int _ID_ITEM_REFER_CUSTO)
        {
            return (from tb429 in RetornaTodosRegistros()
                    where tb429.ID_ITEM_REFER_CUSTO == _ID_ITEM_REFER_CUSTO
                    select tb429).FirstOrDefault();
        }

        #endregion
    }
}
