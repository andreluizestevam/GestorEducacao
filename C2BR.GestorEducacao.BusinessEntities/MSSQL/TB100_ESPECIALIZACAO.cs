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
    public partial class TB100_ESPECIALIZACAO
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
        /// Exclue o registro da tabela TB100_ESPECIALIZACAO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB100_ESPECIALIZACAO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB100_ESPECIALIZACAO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB100_ESPECIALIZACAO.</returns>
        public static TB100_ESPECIALIZACAO Delete(TB100_ESPECIALIZACAO entity)
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
        public static int SaveOrUpdate(TB100_ESPECIALIZACAO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB100_ESPECIALIZACAO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB100_ESPECIALIZACAO.</returns>
        public static TB100_ESPECIALIZACAO SaveOrUpdate(TB100_ESPECIALIZACAO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB100_ESPECIALIZACAO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB100_ESPECIALIZACAO.</returns>
        public static TB100_ESPECIALIZACAO GetByEntityKey(EntityKey entityKey)
        {
            return (TB100_ESPECIALIZACAO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB100_ESPECIALIZACAO, ordenados pela descrição "DE_ESPEC".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB100_ESPECIALIZACAO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB100_ESPECIALIZACAO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB100_ESPECIALIZACAO.OrderBy( e => e.DE_ESPEC ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB100_ESPECIALIZACAO pela chaves primária "CO_ESPEC".
        /// </summary>
        /// <param name="CO_ESPEC">Id da chave primária</param>
        /// <returns>Entidade TB100_ESPECIALIZACAO</returns>
        public static TB100_ESPECIALIZACAO RetornaPeloCoEspec(int CO_ESPEC)
        {
            return (from tb100 in RetornaTodosRegistros()
                    where tb100.CO_ESPEC == CO_ESPEC
                    select tb100).FirstOrDefault();
        }

        /// <summary>
        /// Retorna todos os registros da entidade TB100_ESPECIALIZACAO de acordo com o tipo "TP_ESPEC".
        /// </summary>
        /// <param name="TP_ESPEC">Tipo de especialização</param>
        /// <returns>ObjectQuery com todos os registros da entidade TB100_ESPECIALIZACAO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB100_ESPECIALIZACAO> RetornaPeloTipo(string TP_ESPEC)
        {
            return (from tb100 in RetornaTodosRegistros()
                    where tb100.TP_ESPEC == TP_ESPEC
                    select tb100).AsObjectQuery();
        }

        #endregion
    }
}
