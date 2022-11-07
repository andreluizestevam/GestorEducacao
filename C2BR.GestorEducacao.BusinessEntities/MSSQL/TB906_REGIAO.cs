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
    public partial class TB906_REGIAO
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
        /// Exclue o registro da tabela TB906_REGIAO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB906_REGIAO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB906_REGIAO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB906_REGIAO.</returns>
        public static TB906_REGIAO Delete(TB906_REGIAO entity)
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
        public static int SaveOrUpdate(TB906_REGIAO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB906_REGIAO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB906_REGIAO.</returns>
        public static TB906_REGIAO SaveOrUpdate(TB906_REGIAO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB906_REGIAO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB906_REGIAO.</returns>
        public static TB906_REGIAO GetByEntityKey(EntityKey entityKey)
        {
            return (TB906_REGIAO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB906_REGIAO, ordenados pelo nome "NO_CIDADE".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB906_REGIAO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB906_REGIAO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB906_REGIAO.OrderBy(r => r.ID_REGIAO).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB906_REGIAO pela chave primária "CO_CIDADE".
        /// </summary>
        /// <param name="CO_CIDADE">Id da chave primária</param>
        /// <returns>Entidade TB906_REGIAO</returns>
        public static TB906_REGIAO RetornaPelaChavePrimaria(int ID_REGIAO)
        {
            return (from tb906 in RetornaTodosRegistros()
                    where tb906.ID_REGIAO == ID_REGIAO
                    select tb906).FirstOrDefault();
        }

        /// <summary>
        /// Retorna todos os registros da entidade TB906_REGIAO de acordo com a UF "CO_UF".
        /// </summary>
        /// <param name="CO_UF">Id da UF</param>
        /// <returns>ObjectQuery com todos os registros da entidade TB906_REGIAO de acordo com a filtragem desenvolvida.</returns>

        #endregion
    }
}
