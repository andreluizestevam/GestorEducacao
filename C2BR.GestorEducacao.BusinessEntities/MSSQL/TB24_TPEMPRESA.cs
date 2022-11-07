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
    public partial class TB24_TPEMPRESA
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
        /// Exclue o registro da tabela TB24_TPEMPRESA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB24_TPEMPRESA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB24_TPEMPRESA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB24_TPEMPRESA.</returns>
        public static TB24_TPEMPRESA Delete(TB24_TPEMPRESA entity)
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
        public static int SaveOrUpdate(TB24_TPEMPRESA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB24_TPEMPRESA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB24_TPEMPRESA.</returns>
        public static TB24_TPEMPRESA SaveOrUpdate(TB24_TPEMPRESA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB24_TPEMPRESA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB24_TPEMPRESA.</returns>
        public static TB24_TPEMPRESA GetByEntityKey(EntityKey entityKey)
        {
            return (TB24_TPEMPRESA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB24_TPEMPRESA, ordenados pelo nome "NO_TIPOEMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB24_TPEMPRESA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB24_TPEMPRESA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB24_TPEMPRESA.OrderBy( t => t.NO_TIPOEMP ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB24_TPEMPRESA pela chave primária "CO_TIPOEMP".
        /// </summary>
        /// <param name="CO_TIPOEMP">Id da chave primária</param>
        /// <returns>Entidade TB24_TPEMPRESA</returns>
        public static TB24_TPEMPRESA RetornaPelaChavePrimaria(int CO_TIPOEMP)
        {
            return (from tb24 in RetornaTodosRegistros()
                    where tb24.CO_TIPOEMP == CO_TIPOEMP
                    select tb24).FirstOrDefault();
        }

        #endregion
    }
}
