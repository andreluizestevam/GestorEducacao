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
    public partial class TB189_AGEND_MONIT_PROFE
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
        /// Exclue o registro da tabela TB153_TIPO_PLANT do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB189_AGEND_MONIT_PROFE entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB153_TIPO_PLANT na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB153_TIPO_PLANT.</returns>
        public static TB189_AGEND_MONIT_PROFE Delete(TB189_AGEND_MONIT_PROFE entity)
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
        public static int SaveOrUpdate(TB189_AGEND_MONIT_PROFE entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB153_TIPO_PLANT na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB153_TIPO_PLANT.</returns>
        public static TB189_AGEND_MONIT_PROFE SaveOrUpdate(TB189_AGEND_MONIT_PROFE entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB153_TIPO_PLANT de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB153_TIPO_PLANT.</returns>
        public static TB189_AGEND_MONIT_PROFE GetByEntityKey(EntityKey entityKey)
        {
            return (TB189_AGEND_MONIT_PROFE)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB153_TIPO_PLANT, ordenados pelo nome "NO_DEPTO".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB153_TIPO_PLANT de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB189_AGEND_MONIT_PROFE> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB189_AGEND_MONIT_PROFE.OrderBy(d => d.ID_AGEND_MONIT_PROFE).AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TB03_COLABOR onde o Id do funcionário "CO_COL" é o informado no parâmetro.
        /// </summary>
        /// <param name="CO_COL">Id da chave primária</param>
        /// <returns>Entidade TB03_COLABOR</returns>
        public static TB189_AGEND_MONIT_PROFE RetornaPelaChavePrimaria(int ID_AGEND_MONIT_PROFE)
        {
            return (from tb188 in RetornaTodosRegistros()
                    where tb188.ID_AGEND_MONIT_PROFE == ID_AGEND_MONIT_PROFE
                    select tb188).OrderBy(c => c.ID_AGEND_MONIT_PROFE).FirstOrDefault();
        }

        #endregion

    }
}
