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
    public partial class TB299_PAISES
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
        /// Exclue o registro da tabela TB299_PAISES do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB299_PAISES entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB299_PAISES na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB299_PAISES.</returns>
        public static TB299_PAISES Delete(TB299_PAISES entity)
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
        public static int SaveOrUpdate(TB299_PAISES entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB299_PAISES na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB299_PAISES.</returns>
        public static TB299_PAISES SaveOrUpdate(TB299_PAISES entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB299_PAISES de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB299_PAISES.</returns>
        public static TB299_PAISES GetByEntityKey(EntityKey entityKey)
        {
            return (TB299_PAISES)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB299_PAISES, ordenados pelo nome "NO_PAISES".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB299_PAISES de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB299_PAISES> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB299_PAISES.OrderBy(p => p.NO_PAISES).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB299_PAISES pela chave primária "CO_ISO_PAISES".
        /// </summary>
        /// <param name="CO_ISO_PAISES">Id da chave primária</param>
        /// <returns>Entidade TB299_PAISES</returns>
        public static TB299_PAISES RetornaPelaChavePrimaria(string CO_ISO_PAISES)
        {
            return (from tb299 in RetornaTodosRegistros()
                    where tb299.CO_ISO_PAISES == CO_ISO_PAISES
                    select tb299).FirstOrDefault();
        }

        #endregion
    }
}