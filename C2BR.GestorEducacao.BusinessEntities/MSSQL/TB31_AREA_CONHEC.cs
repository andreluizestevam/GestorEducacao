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
    public partial class TB31_AREA_CONHEC
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
        /// Exclue o registro da tabela TB31_AREA_CONHEC do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB31_AREA_CONHEC entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB31_AREA_CONHEC na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB31_AREA_CONHEC.</returns>
        public static TB31_AREA_CONHEC Delete(TB31_AREA_CONHEC entity)
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
        public static int SaveOrUpdate(TB31_AREA_CONHEC entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB31_AREA_CONHEC na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB31_AREA_CONHEC.</returns>
        public static TB31_AREA_CONHEC SaveOrUpdate(TB31_AREA_CONHEC entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB31_AREA_CONHEC de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB31_AREA_CONHEC.</returns>
        public static TB31_AREA_CONHEC GetByEntityKey(EntityKey entityKey)
        {
            return (TB31_AREA_CONHEC)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB31_AREA_CONHEC, ordenados pelo nome "NO_AREACON".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB31_AREA_CONHEC de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB31_AREA_CONHEC> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB31_AREA_CONHEC.OrderBy( a => a.NO_AREACON ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB31_AREA_CONHEC pela chave primária "CO_AREACON".
        /// </summary>
        /// <param name="CO_AREACON">Id da chave primária</param>
        /// <returns>Entidade TB31_AREA_CONHEC</returns>
        public static TB31_AREA_CONHEC RetornaPelaChavePrimaria(int CO_AREACON)
        {
            return (from tb31 in RetornaTodosRegistros()
                    where tb31.CO_AREACON == CO_AREACON
                    select tb31).FirstOrDefault();
        }

        #endregion
    }
}
