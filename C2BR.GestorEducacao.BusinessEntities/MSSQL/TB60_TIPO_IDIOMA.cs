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
    public partial class TB60_TIPO_IDIOMA
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
        /// Exclue o registro da tabela TB60_TIPO_IDIOMA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB60_TIPO_IDIOMA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB60_TIPO_IDIOMA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB60_TIPO_IDIOMA.</returns>
        public static TB60_TIPO_IDIOMA Delete(TB60_TIPO_IDIOMA entity)
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
        public static int SaveOrUpdate(TB60_TIPO_IDIOMA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB60_TIPO_IDIOMA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB60_TIPO_IDIOMA.</returns>
        public static TB60_TIPO_IDIOMA SaveOrUpdate(TB60_TIPO_IDIOMA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB60_TIPO_IDIOMA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB60_TIPO_IDIOMA.</returns>
        public static TB60_TIPO_IDIOMA GetByEntityKey(EntityKey entityKey)
        {
            return (TB60_TIPO_IDIOMA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB60_TIPO_IDIOMA, ordenados pelo nome "NO_IDIOM".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB60_TIPO_IDIOMA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB60_TIPO_IDIOMA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB60_TIPO_IDIOMA.OrderBy( t => t.NO_IDIOM ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB60_TIPO_IDIOMA pela chave primária "CO_IDIOM".
        /// </summary>
        /// <param name="CO_IDIOM">Id da chave primária</param>
        /// <returns>Entidade TB60_TIPO_IDIOMA</returns>
        public static TB60_TIPO_IDIOMA RetornaPelaChavePrimaria(int CO_IDIOM)
        {
            return (from tb60 in RetornaTodosRegistros()
                    where tb60.CO_IDIOM == CO_IDIOM
                    select tb60).FirstOrDefault();
        }

        #endregion
    }
}