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
    public partial class TB22_TIPOPAG
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
        /// Exclue o registro da tabela TB22_TIPOPAG do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB22_TIPOPAG entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB22_TIPOPAG na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB22_TIPOPAG.</returns>
        public static TB22_TIPOPAG Delete(TB22_TIPOPAG entity)
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
        public static int SaveOrUpdate(TB22_TIPOPAG entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB22_TIPOPAG na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB22_TIPOPAG.</returns>
        public static TB22_TIPOPAG SaveOrUpdate(TB22_TIPOPAG entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB22_TIPOPAG de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB22_TIPOPAG.</returns>
        public static TB22_TIPOPAG GetByEntityKey(EntityKey entityKey)
        {
            return (TB22_TIPOPAG)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB22_TIPOPAG, ordenados pelo nome "NO_TIPO_PAGA".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB22_TIPOPAG de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB22_TIPOPAG> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB22_TIPOPAG.OrderBy( t => t.NO_TIPO_PAGA ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB22_TIPOPAG pela chave primária "CO_TIPO_PAGA".
        /// </summary>
        /// <param name="CO_TIPO_PAGA">Id da chave primária</param>
        /// <returns>Entidade TB22_TIPOPAG</returns>
        public static TB22_TIPOPAG RetornaPelaChavePrimaria(int CO_TIPO_PAGA)
        {
            return (from tb22 in RetornaTodosRegistros()
                    where tb22.CO_TIPO_PAGA == CO_TIPO_PAGA
                    select tb22).FirstOrDefault();
        }

        #endregion
    }
}