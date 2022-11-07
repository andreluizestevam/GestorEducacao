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
    public partial class TB108_RESPONSAVEL
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
        /// Exclue o registro da tabela TB108_RESPONSAVEL do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB108_RESPONSAVEL entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB108_RESPONSAVEL na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB108_RESPONSAVEL.</returns>
        public static TB108_RESPONSAVEL Delete(TB108_RESPONSAVEL entity)
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
        public static int SaveOrUpdate(TB108_RESPONSAVEL entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela ADMMODULO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB108_RESPONSAVEL.</returns>
        public static TB108_RESPONSAVEL SaveOrUpdate(TB108_RESPONSAVEL entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB108_RESPONSAVEL de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB108_RESPONSAVEL.</returns>
        public static TB108_RESPONSAVEL GetByEntityKey(EntityKey entityKey)
        {
            return (TB108_RESPONSAVEL)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB108_RESPONSAVEL, ordenados pelo nome "NO_RESP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB108_RESPONSAVEL de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB108_RESPONSAVEL> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB108_RESPONSAVEL.OrderBy( r => r.NO_RESP ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB108_RESPONSAVEL pela chave primária "CO_RESP"
        /// </summary>
        /// <param name="CO_RESP">Id da chave primária</param>
        /// <returns></returns>
        public static TB108_RESPONSAVEL RetornaPelaChavePrimaria(int CO_RESP)
        {
            return (from tb108 in RetornaTodosRegistros().Include(typeof(Image).Name)
                    where tb108.CO_RESP == CO_RESP
                    select tb108).FirstOrDefault();
        }

        #endregion
    }
}