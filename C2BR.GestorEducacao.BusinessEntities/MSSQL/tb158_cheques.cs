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
    public partial class tb158_cheques
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
        /// Exclue o registro da tabela tb158_cheques do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(tb158_cheques entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela tb158_cheques na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade tb158_cheques.</returns>
        public static tb158_cheques Delete(tb158_cheques entity)
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
        public static int SaveOrUpdate(tb158_cheques entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela tb158_cheques na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade tb158_cheques.</returns>
        public static tb158_cheques SaveOrUpdate(tb158_cheques entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade tb158_cheques de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade tb158_cheques.</returns>
        public static tb158_cheques GetByEntityKey(EntityKey entityKey)
        {
            return (tb158_cheques)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade tb158_cheques, ordenados pelo código "co_cheque".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade tb158_cheques de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<tb158_cheques> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.tb158_cheques.OrderBy( c => c.co_cheque ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade tb158_cheques pelas chaves primárias "CO_CHEQUE" e ORG_CODIGO_ORGAO".
        /// </summary>
        /// <param name="CO_CHEQUE">Código do cheque</param>
        /// <param name="ORG_CODIGO_ORGAO">Id da instituição</param>
        /// <returns>Entidade tb158_cheques</returns>
        public static tb158_cheques RetornaPelaChavePrimaria(int CO_CHEQUE, int ORG_CODIGO_ORGAO)
        {
            return (from tb158 in RetornaTodosRegistros()
                    where tb158.co_cheque == CO_CHEQUE && tb158.ORG_CODIGO_ORGAO == ORG_CODIGO_ORGAO
                    select tb158).FirstOrDefault();
        }

        #endregion
    }
}