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
    public partial class TB237_TIPO_PESSOA
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
        /// Exclue o registro da tabela TB237_TIPO_PESSOA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB237_TIPO_PESSOA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB237_TIPO_PESSOA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB237_TIPO_PESSOA.</returns>
        public static TB237_TIPO_PESSOA Delete(TB237_TIPO_PESSOA entity)
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
        public static int SaveOrUpdate(TB237_TIPO_PESSOA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB237_TIPO_PESSOA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB237_TIPO_PESSOA.</returns>
        public static TB237_TIPO_PESSOA SaveOrUpdate(TB237_TIPO_PESSOA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB237_TIPO_PESSOA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB237_TIPO_PESSOA.</returns>
        public static TB237_TIPO_PESSOA GetByEntityKey(EntityKey entityKey)
        {
            return (TB237_TIPO_PESSOA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB237_TIPO_PESSOA, ordenados pelo nome "NM_TIPO_PESSOA".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB237_TIPO_PESSOA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB237_TIPO_PESSOA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB237_TIPO_PESSOA.OrderBy( t => t.NM_TIPO_PESSOA ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB237_TIPO_PESSOA pela chave primária "CO_CEP".
        /// </summary>
        /// <param name="ID_TIPO_PESSOA">Id da chave primária</param>
        /// <returns>Entidade TB237_TIPO_PESSOA</returns>
        public static TB237_TIPO_PESSOA RetornaPelaChavePrimaria(int ID_TIPO_PESSOA)
        {
            return (from tb237 in RetornaTodosRegistros()
                    where tb237.ID_TIPO_PESSOA == ID_TIPO_PESSOA
                    select tb237).FirstOrDefault();
        }

        #endregion
    }
}