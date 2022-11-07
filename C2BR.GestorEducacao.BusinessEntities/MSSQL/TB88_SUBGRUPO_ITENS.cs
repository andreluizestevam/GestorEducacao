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
    public partial class TB88_SUBGRUPO_ITENS
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
        /// Exclue o registro da tabela TB88_SUBGRUPO_ITENS do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB88_SUBGRUPO_ITENS entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB88_SUBGRUPO_ITENS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB88_SUBGRUPO_ITENS.</returns>
        public static TB88_SUBGRUPO_ITENS Delete(TB88_SUBGRUPO_ITENS entity)
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
        public static int SaveOrUpdate(TB88_SUBGRUPO_ITENS entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB88_SUBGRUPO_ITENS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB88_SUBGRUPO_ITENS.</returns>
        public static TB88_SUBGRUPO_ITENS SaveOrUpdate(TB88_SUBGRUPO_ITENS entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB88_SUBGRUPO_ITENS de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB88_SUBGRUPO_ITENS.</returns>
        public static TB88_SUBGRUPO_ITENS GetByEntityKey(EntityKey entityKey)
        {
            return (TB88_SUBGRUPO_ITENS)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB88_SUBGRUPO_ITENS, ordenados pelo nome "NO_SUBGRP_ITEM".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB88_SUBGRUPO_ITENS de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB88_SUBGRUPO_ITENS> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB88_SUBGRUPO_ITENS.OrderBy( s => s.NO_SUBGRP_ITEM ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB88_SUBGRUPO_ITENS pela chave primária "CO_SUBGRP_ITEM".
        /// </summary>
        /// <param name="CO_SUBGRP_ITEM">Id da chave primária</param>
        /// <returns>Entidade TB88_SUBGRUPO_ITENS</returns>
        public static TB88_SUBGRUPO_ITENS RetornaPelaChavePrimaria(int CO_SUBGRP_ITEM)
        {
            return (from tb88 in RetornaTodosRegistros()
                    where tb88.CO_SUBGRP_ITEM == CO_SUBGRP_ITEM
                    select tb88).FirstOrDefault();
        }

        #endregion
    }
}
