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
    public partial class TB168_ABST_AGUA
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
        /// Exclue o registro da tabela TB168_ABST_AGUA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB168_ABST_AGUA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB168_ABST_AGUA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB168_ABST_AGUA.</returns>
        public static TB168_ABST_AGUA Delete(TB168_ABST_AGUA entity)
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
        public static int SaveOrUpdate(TB168_ABST_AGUA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB168_ABST_AGUA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB168_ABST_AGUA.</returns>
        public static TB168_ABST_AGUA SaveOrUpdate(TB168_ABST_AGUA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB168_ABST_AGUA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB168_ABST_AGUA.</returns>
        public static TB168_ABST_AGUA GetByEntityKey(EntityKey entityKey)
        {
            return (TB168_ABST_AGUA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB168_ABST_AGUA, ordenados pelo nome "NO_ABAST".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB168_ABST_AGUA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB168_ABST_AGUA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB168_ABST_AGUA.OrderBy( a => a.NO_ABAST ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB168_ABST_AGUA pela chave primária "CO_ABAST".
        /// </summary>
        /// <param name="CO_ABAST">Id da chave primária</param>
        /// <returns>Entidade TB168_ABST_AGUA</returns>
        public static TB168_ABST_AGUA RetornaPelaChavePrimaria(int CO_ABAST)
        {
            return (from tb168 in RetornaTodosRegistros()
                    where tb168.CO_ABAST == CO_ABAST
                    select tb168).FirstOrDefault();
        }

        #endregion
    }
}