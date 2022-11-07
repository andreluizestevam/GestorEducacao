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
    public partial class ArquivoCompartilhado
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
        /// Exclue o registro da tabela ArquivoCompartilhado do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(ArquivoCompartilhado entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela ArquivoCompartilhado na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade ArquivoCompartilhado.</returns>
        public static ArquivoCompartilhado Delete(ArquivoCompartilhado entity)
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
        public static int SaveOrUpdate(ArquivoCompartilhado entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        //// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela ArquivoCompartilhado na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade ArquivoCompartilhado.</returns>
        public static ArquivoCompartilhado SaveOrUpdate(ArquivoCompartilhado entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade ArquivoCompartilhado de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade ArquivoCompartilhado.</returns>
        public static ArquivoCompartilhado GetByEntityKey(EntityKey entityKey)
        {
            return (ArquivoCompartilhado)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade ArquivoCompartilhado, ordenados pela descrição "Descricao".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade ArquivoCompartilhado de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<ArquivoCompartilhado> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.ArquivoCompartilhado.OrderBy( a => a.Descricao ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade ArquivoCompartilhado onde o Id "ArquivoCompartilhadoId" é o informado no parâmetro.
        /// </summary>
        /// <param name="ArquivoCompartilhadoId">Id da chave primária</param>
        /// <returns>Entidade ArquivoCompartilhado</returns>
        public static ArquivoCompartilhado RetornaPeloID(int ArquivoCompartilhadoId)
        {
            return (from arqComp in RetornaTodosRegistros()
                    where arqComp.ArquivoCompartilhadoId == ArquivoCompartilhadoId
                    select arqComp).FirstOrDefault();
        }

        #endregion
    }
}