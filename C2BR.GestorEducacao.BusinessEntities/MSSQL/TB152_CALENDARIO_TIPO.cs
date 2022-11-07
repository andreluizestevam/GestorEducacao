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
    public partial class TB152_CALENDARIO_TIPO
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
        /// Exclue o registro da tabela TB152_CALENDARIO_TIPO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB152_CALENDARIO_TIPO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB152_CALENDARIO_TIPO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB152_CALENDARIO_TIPO.</returns>
        public static TB152_CALENDARIO_TIPO Delete(TB152_CALENDARIO_TIPO entity)
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
        public static int SaveOrUpdate(TB152_CALENDARIO_TIPO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB152_CALENDARIO_TIPO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB152_CALENDARIO_TIPO.</returns>
        public static TB152_CALENDARIO_TIPO SaveOrUpdate(TB152_CALENDARIO_TIPO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB152_CALENDARIO_TIPO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB152_CALENDARIO_TIPO.</returns>
        public static TB152_CALENDARIO_TIPO GetByEntityKey(EntityKey entityKey)
        {
            return (TB152_CALENDARIO_TIPO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB152_CALENDARIO_TIPO, ordenados pelo nome "CAT_NOME_TIPO_CALEN".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB152_CALENDARIO_TIPO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB152_CALENDARIO_TIPO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB152_CALENDARIO_TIPO.OrderBy( c => c.CAT_NOME_TIPO_CALEN ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB152_CALENDARIO_TIPO pela chave primária "CAT_ID_TIPO_CALEN".
        /// </summary>
        /// <param name="CAT_ID_TIPO_CALEN">Id da chave primária</param>
        /// <returns>Entidade TB152_CALENDARIO_TIPO</returns>
        public static TB152_CALENDARIO_TIPO RetornaPelaChavePrimaria(int CAT_ID_TIPO_CALEN)
        {
            return (from tb152 in RetornaTodosRegistros()
                    where tb152.CAT_ID_TIPO_CALEN == CAT_ID_TIPO_CALEN
                    select tb152).FirstOrDefault();
        }

        #endregion
    }
}