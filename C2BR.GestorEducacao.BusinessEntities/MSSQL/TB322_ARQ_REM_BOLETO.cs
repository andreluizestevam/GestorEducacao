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
    public partial class TB322_ARQ_REM_BOLETO
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
        public static int Delete(TB322_ARQ_REM_BOLETO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB322_ARQ_REM_BOLETO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB322_ARQ_REM_BOLETO.</returns>
        public static TB322_ARQ_REM_BOLETO Delete(TB322_ARQ_REM_BOLETO entity)
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
        public static int SaveOrUpdate(TB322_ARQ_REM_BOLETO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB320_CTRL_FLUXO_CAIXA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB322_ARQ_REM_BOLETO.</returns>
        public static TB322_ARQ_REM_BOLETO SaveOrUpdate(TB322_ARQ_REM_BOLETO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB322_ARQ_REM_BOLETO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB322_ARQ_REM_BOLETO.</returns>
        public static TB322_ARQ_REM_BOLETO GetByEntityKey(EntityKey entityKey)
        {
            return (TB322_ARQ_REM_BOLETO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB322_ARQ_REM_BOLETO, ordenados pela descrição "DE_AGRUP_ATIVEXTRA".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB322_ARQ_REM_BOLETO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB322_ARQ_REM_BOLETO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB322_ARQ_REM_BOLETO.AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB322_ARQ_REM_BOLETO onde o Id "ID_ARQ_REM_BOLETO" é o informado no parâmetro.
        /// </summary>
        /// <param name="ID_AGRUP_ATIVEXTRA">Id da chave primária</param>
        /// <returns>Entidade TB322_ARQ_REM_BOLETO</returns>
        public static TB322_ARQ_REM_BOLETO RetornaPelaChavePrimaria(int ID_ARQ_REM_BOLETO)
        {
            return (from tb320 in RetornaTodosRegistros()
                    where tb320.ID_ARQ_REM_BOLETO == ID_ARQ_REM_BOLETO
                    select tb320).FirstOrDefault();
        }

        public static TB322_ARQ_REM_BOLETO RetornaPeloNossoNumero(string nossoNumero, string carteira, string conta, string banco, int agencia)
        {
            return (from tb320 in RetornaTodosRegistros()
                    where tb320.NU_NOSSO_NUMERO == nossoNumero
                    && tb320.CO_CARTEIRA == carteira
                    && tb320.CO_AGENCIA == agencia
                    && tb320.IDEBANCO == banco
                    && tb320.CO_CONTA == conta
                    select tb320).FirstOrDefault();
        }

        #endregion
    }
}