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
    public partial class TB301_MURAL
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
        /// Exclue o registro da tabela TB301_MURAL do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB301_MURAL entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB301_MURAL na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB301_MURAL.</returns>
        public static TB301_MURAL Delete(TB301_MURAL entity)
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
        public static int SaveOrUpdate(TB301_MURAL entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB301_MURAL na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB301_MURAL.</returns>
        public static TB301_MURAL SaveOrUpdate(TB301_MURAL entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB301_MURAL de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB301_MURAL.</returns>
        public static TB301_MURAL GetByEntityKey(EntityKey entityKey)
        {
            return (TB301_MURAL)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB301_MURAL, ordenados pelo Id "ID_MURAL".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB301_MURAL de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB301_MURAL> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB301_MURAL.OrderBy(p => p.ID_MURAL).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB301_MURAL pela chave primária "ID_MURAL".
        /// </summary>
        /// <param name="ID_MURAL">Id da chave primária</param>
        /// <returns>Entidade TB301_MURAL</returns>
        public static TB301_MURAL RetornaPelaChavePrimaria(int ID_MURAL)
        {
            return (from tb300 in RetornaTodosRegistros()
                    where tb300.ID_MURAL == ID_MURAL
                    select tb300).FirstOrDefault();
        }

        #endregion
    }
}