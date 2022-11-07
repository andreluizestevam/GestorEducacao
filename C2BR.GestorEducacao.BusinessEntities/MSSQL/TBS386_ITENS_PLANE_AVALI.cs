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
    public partial class TBS386_ITENS_PLANE_AVALI
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
        /// Exclue o registro da tabela TBS386_ITENS_PLANE_AVALI do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS386_ITENS_PLANE_AVALI entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS386_ITENS_PLANE_AVALI na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS386_ITENS_PLANE_AVALI.</returns>
        public static TBS386_ITENS_PLANE_AVALI Delete(TBS386_ITENS_PLANE_AVALI entity)
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
        public static int SaveOrUpdate(TBS386_ITENS_PLANE_AVALI entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS386_ITENS_PLANE_AVALI na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS386_ITENS_PLANE_AVALI.</returns>
        public static TBS386_ITENS_PLANE_AVALI SaveOrUpdate(TBS386_ITENS_PLANE_AVALI entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS386_ITENS_PLANE_AVALI de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS386_ITENS_PLANE_AVALI.</returns>
        public static TBS386_ITENS_PLANE_AVALI GetByEntityKey(EntityKey entityKey)
        {
            return (TBS386_ITENS_PLANE_AVALI)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS386_ITENS_PLANE_AVALI, ordenados pelo Id do Banco "IDEBANCO" e pelo Id da agência "CO_AGENCIA".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB30_AGENCIA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS386_ITENS_PLANE_AVALI> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS386_ITENS_PLANE_AVALI.AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TBS386_ITENS_PLANE_AVALI pela chave primária "ID_ITENS_PLANE_AVALI".
        /// </summary>
        /// <param name="IDEBANCO">Id do banco</param>
        /// <param name="CO_AGENCIA">Código da agência</param>
        /// <returns>Entidade TBS386_ITENS_PLANE_AVALI</returns>
        public static TBS386_ITENS_PLANE_AVALI RetornaPelaChavePrimaria(int ID_ITENS_PLANE_AVALI)
        {
            return (from tbs386 in RetornaTodosRegistros()
                    where tbs386.ID_ITENS_PLANE_AVALI == ID_ITENS_PLANE_AVALI
                    select tbs386).FirstOrDefault();
        }

        #endregion
    }
}
