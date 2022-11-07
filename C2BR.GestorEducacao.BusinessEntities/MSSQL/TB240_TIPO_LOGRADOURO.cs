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
    public partial class TB240_TIPO_LOGRADOURO
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
        /// Exclue o registro da tabela TB240_TIPO_LOGRADOURO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB240_TIPO_LOGRADOURO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB240_TIPO_LOGRADOURO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB240_TIPO_LOGRADOURO.</returns>
        public static TB240_TIPO_LOGRADOURO Delete(TB240_TIPO_LOGRADOURO entity)
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
        public static int SaveOrUpdate(TB240_TIPO_LOGRADOURO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB240_TIPO_LOGRADOURO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB240_TIPO_LOGRADOURO.</returns>
        public static TB240_TIPO_LOGRADOURO SaveOrUpdate(TB240_TIPO_LOGRADOURO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB240_TIPO_LOGRADOURO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB240_TIPO_LOGRADOURO.</returns>
        public static TB240_TIPO_LOGRADOURO GetByEntityKey(EntityKey entityKey)
        {
            return (TB240_TIPO_LOGRADOURO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB240_TIPO_LOGRADOURO, ordenados pela descrição "DE_TIPO_LOGRA".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB240_TIPO_LOGRADOURO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB240_TIPO_LOGRADOURO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB240_TIPO_LOGRADOURO.OrderBy( t => t.DE_TIPO_LOGRA ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB240_TIPO_LOGRADOURO pela chave primária "ID_TIPO_LOGRA".
        /// </summary>
        /// <param name="ID_TIPO_LOGRA">Id da chave primária</param>
        /// <returns>Entidade TB240_TIPO_LOGRADOURO</returns>
        public static TB240_TIPO_LOGRADOURO RetornaPelaChavePrimaria(int ID_TIPO_LOGRA)
        {
            return (from tb240 in RetornaTodosRegistros()
                    where tb240.ID_TIPO_LOGRA == ID_TIPO_LOGRA
                    select tb240).FirstOrDefault();
        }

        #endregion
    }
}