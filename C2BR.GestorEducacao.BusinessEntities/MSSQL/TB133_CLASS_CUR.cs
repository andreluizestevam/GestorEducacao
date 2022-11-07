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
    public partial class TB133_CLASS_CUR
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
        /// Exclue o registro da tabela TB133_CLASS_CUR do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB133_CLASS_CUR entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB133_CLASS_CUR na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB133_CLASS_CUR.</returns>
        public static TB133_CLASS_CUR Delete(TB133_CLASS_CUR entity)
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
        public static int SaveOrUpdate(TB133_CLASS_CUR entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB133_CLASS_CUR na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB133_CLASS_CUR.</returns>
        public static TB133_CLASS_CUR SaveOrUpdate(TB133_CLASS_CUR entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB133_CLASS_CUR de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB133_CLASS_CUR.</returns>
        public static TB133_CLASS_CUR GetByEntityKey(EntityKey entityKey)
        {
            return (TB133_CLASS_CUR)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB133_CLASS_CUR, ordenados pelo nome "NO_CLASS_CUR".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB133_CLASS_CUR de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB133_CLASS_CUR> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB133_CLASS_CUR.OrderBy( c => c.NO_CLASS_CUR ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB133_CLASS_CUR pela chave primária "CO_CLASS_CUR".
        /// </summary>
        /// <param name="CO_CLASS_CUR">Id da chave primária</param>
        /// <returns>Entidade TB133_CLASS_CUR</returns>
        public static TB133_CLASS_CUR RetornaPelaChavePrimaria(int CO_CLASS_CUR)
        {
            return (from tb133 in RetornaTodosRegistros()
                    where tb133.CO_CLASS_CUR == CO_CLASS_CUR
                    select tb133).FirstOrDefault();
        }

        #endregion
    }
}