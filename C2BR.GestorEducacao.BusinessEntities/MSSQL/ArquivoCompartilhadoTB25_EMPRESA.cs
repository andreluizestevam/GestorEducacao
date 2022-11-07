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
    public partial class ArquivoCompartilhadoTB25_EMPRESA
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
        /// Exclue o registro da tabela ArquivoCompartilhadoTB25_EMPRESA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(ArquivoCompartilhadoTB25_EMPRESA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela ArquivoCompartilhadoTB25_EMPRESA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade ArquivoCompartilhadoTB25_EMPRESA.</returns>
        public static ArquivoCompartilhadoTB25_EMPRESA Delete(ArquivoCompartilhadoTB25_EMPRESA entity)
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
        public static int SaveOrUpdate(ArquivoCompartilhadoTB25_EMPRESA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        //// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela ArquivoCompartilhadoTB25_EMPRESA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade ArquivoCompartilhadoTB25_EMPRESA.</returns>
        public static ArquivoCompartilhadoTB25_EMPRESA SaveOrUpdate(ArquivoCompartilhadoTB25_EMPRESA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade ArquivoCompartilhadoTB25_EMPRESA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade ArquivoCompartilhadoTB25_EMPRESA.</returns>
        public static ArquivoCompartilhadoTB25_EMPRESA GetByEntityKey(EntityKey entityKey)
        {
            return (ArquivoCompartilhadoTB25_EMPRESA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade ArquivoCompartilhadoTB25_EMPRESA, ordenados pela descrição do arquivo "Descricao".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade ArquivoCompartilhadoTB25_EMPRESA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<ArquivoCompartilhadoTB25_EMPRESA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.ArquivoCompartilhadoTB25_EMPRESA.OrderBy(a => a.ArquivoCompartilhado.Descricao).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade ArquivoCompartilhadoTB25_EMPRESA onde o Id "ArquivoCompartilhadoTB25_EMPRESAId" é o informado no parâmetro.
        /// </summary>
        /// <param name="ArquivoCompartilhadoTB25_EMPRESAId">Id da chave primária</param>
        /// <returns>Entidade ArquivoCompartilhadoTB25_EMPRESA</returns>
        public static ArquivoCompartilhadoTB25_EMPRESA RetornaPeloID(int ArquivoCompartilhadoTB25_EMPRESAId)
        {
            return (from arqCompTb25 in RetornaTodosRegistros()
                    where arqCompTb25.ArquivoCompartilhadoTB25_EMPRESAId == ArquivoCompartilhadoTB25_EMPRESAId
                    select arqCompTb25).FirstOrDefault();
        }

        #endregion
    }
}