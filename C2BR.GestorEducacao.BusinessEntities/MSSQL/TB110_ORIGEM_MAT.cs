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
    public partial class TB110_ORIGEM_MAT
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
        /// Exclue o registro da tabela TB110_ORIGEM_MAT do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB110_ORIGEM_MAT entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB110_ORIGEM_MAT na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB110_ORIGEM_MAT.</returns>
        public static TB110_ORIGEM_MAT Delete(TB110_ORIGEM_MAT entity)
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
        public static int SaveOrUpdate(TB110_ORIGEM_MAT entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB110_ORIGEM_MAT na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB110_ORIGEM_MAT.</returns>
        public static TB110_ORIGEM_MAT SaveOrUpdate(TB110_ORIGEM_MAT entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB110_ORIGEM_MAT de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB110_ORIGEM_MAT.</returns>
        public static TB110_ORIGEM_MAT GetByEntityKey(EntityKey entityKey)
        {
            return (TB110_ORIGEM_MAT)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB110_ORIGEM_MAT, ordenados pela descrição "DE_ORIGEM".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB110_ORIGEM_MAT de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB110_ORIGEM_MAT> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB110_ORIGEM_MAT.OrderBy( o => o.DE_ORIGEM ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB110_ORIGEM_MAT pela chave primária "CO_ORIGEM"
        /// </summary>
        /// <param name="CO_ORIGEM">Id da chave primária</param>
        /// <returns>Entidade TB110_ORIGEM_MAT</returns>
        public static TB110_ORIGEM_MAT RetornaPelaChavePrimaria(int CO_ORIGEM)
        {
            return (from tb110 in RetornaTodosRegistros()
                    where tb110.CO_ORIGEM == CO_ORIGEM
                    select tb110).FirstOrDefault();
        }

        #endregion
    }
}