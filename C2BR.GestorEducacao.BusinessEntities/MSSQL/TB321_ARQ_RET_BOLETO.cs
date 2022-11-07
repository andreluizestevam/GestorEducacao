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
    public partial class TB321_ARQ_RET_BOLETO
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
        /// Exclue o registro da tabela TB320_CTRL_FLUXO_CAIXA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB321_ARQ_RET_BOLETO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB321_ARQ_RET_BOLETO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB321_ARQ_RET_BOLETO.</returns>
        public static TB321_ARQ_RET_BOLETO Delete(TB321_ARQ_RET_BOLETO entity)
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
        public static int SaveOrUpdate(TB321_ARQ_RET_BOLETO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB320_CTRL_FLUXO_CAIXA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB321_ARQ_RET_BOLETO.</returns>
        public static TB321_ARQ_RET_BOLETO SaveOrUpdate(TB321_ARQ_RET_BOLETO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB321_ARQ_RET_BOLETO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB321_ARQ_RET_BOLETO.</returns>
        public static TB321_ARQ_RET_BOLETO GetByEntityKey(EntityKey entityKey)
        {
            return (TB321_ARQ_RET_BOLETO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB321_ARQ_RET_BOLETO, ordenados pela descrição "DE_AGRUP_ATIVEXTRA".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB321_ARQ_RET_BOLETO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB321_ARQ_RET_BOLETO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB321_ARQ_RET_BOLETO.AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB321_ARQ_RET_BOLETO onde o Id "ID_ARQ_RET_BOLETO" é o informado no parâmetro.
        /// </summary>
        /// <param name="ID_AGRUP_ATIVEXTRA">Id da chave primária</param>
        /// <returns>Entidade TB321_ARQ_RET_BOLETO</returns>
        public static TB321_ARQ_RET_BOLETO RetornaPelaChavePrimaria(int ID_ARQ_RET_BOLETO)
        {
            return (from tb320 in RetornaTodosRegistros()
                    where tb320.ID_ARQ_RET_BOLETO == ID_ARQ_RET_BOLETO
                    select tb320).FirstOrDefault();
        }

        public static TB321_ARQ_RET_BOLETO RetornaPeloNossoNumero(string nossoNumero)
        {
            return (from tb320 in RetornaTodosRegistros()
                    where tb320.NU_NOSSO_NUMERO == nossoNumero
                    select tb320).FirstOrDefault();
        }

        #endregion
    }
}