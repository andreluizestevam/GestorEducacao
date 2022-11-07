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
    public partial class TB907_AREA
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
        /// Exclue o registro da tabela TB907_AREA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB907_AREA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB907_AREA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB907_AREA.</returns>
        public static TB907_AREA Delete(TB907_AREA entity)
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
        public static int SaveOrUpdate(TB907_AREA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB907_AREA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB907_AREA.</returns>
        public static TB907_AREA SaveOrUpdate(TB907_AREA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB907_AREA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB907_AREA.</returns>
        public static TB907_AREA GetByEntityKey(EntityKey entityKey)
        {
            return (TB907_AREA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB907_AREA, ordenados pelo nome "NO_CIDADE".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB907_AREA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB907_AREA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB907_AREA.OrderBy(a => a.NM_AREA).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB907_AREA pela chave primária "CO_CIDADE".
        /// </summary>
        /// <param name="CO_CIDADE">Id da chave primária</param>
        /// <returns>Entidade TB907_AREA</returns>
        public static TB907_AREA RetornaPelaChavePrimaria(int ID_AREA)
        {
            return (from tb907 in RetornaTodosRegistros()
                    where tb907.ID_AREA == ID_AREA
                    select tb907).FirstOrDefault();
        }
        public static ObjectQuery<TB907_AREA> RetornaPelaRegiao(int ID_REGIAO)
        {
            return (from tb907 in RetornaTodosRegistros().Include(typeof(TB906_REGIAO).Name)
                    where tb907.TB906_REGIAO.ID_REGIAO == ID_REGIAO
                    select tb907).OrderBy(r => r.NM_AREA).AsObjectQuery();
        }


        /// <summary>
        /// Retorna todos os registros da entidade TB907_AREA de acordo com a UF "CO_UF".
        /// </summary>
        /// <param name="CO_UF">Id da UF</param>
        /// <returns>ObjectQuery com todos os registros da entidade TB907_AREA de acordo com a filtragem desenvolvida.</returns>

        #endregion
    }
}
