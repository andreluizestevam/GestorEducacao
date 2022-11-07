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
    public partial class TB952_ESTAT_MODAL
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
        /// Exclue o registro da tabela TB952_ESTAT_MODAL do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB952_ESTAT_MODAL entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB952_ESTAT_MODAL na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB952_ESTAT_MODAL.</returns>
        public static TB952_ESTAT_MODAL Delete(TB952_ESTAT_MODAL entity)
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
        public static int SaveOrUpdate(TB952_ESTAT_MODAL entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB952_ESTAT_MODAL na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB952_ESTAT_MODAL.</returns>
        public static TB952_ESTAT_MODAL SaveOrUpdate(TB952_ESTAT_MODAL entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB952_ESTAT_MODAL de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB952_ESTAT_MODAL.</returns>
        public static TB952_ESTAT_MODAL GetByEntityKey(EntityKey entityKey)
        {
            return (TB952_ESTAT_MODAL)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB952_ESTAT_MODAL, ordenados pelo Id "ID_ESTAT_MODAL".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB952_ESTAT_MODAL de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB952_ESTAT_MODAL> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB952_ESTAT_MODAL.OrderBy( e => e.ID_ESTAT_MODAL ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB952_ESTAT_MODAL pela chave primária "ID_ESTAT_MODAL".
        /// </summary>
        /// <param name="ID_ESTAT_MODAL">Id da chave primária</param>
        /// <returns>Entidade TB952_ESTAT_MODAL</returns>
        public static TB952_ESTAT_MODAL RetornaPelaChavePrimaria(int ID_ESTAT_MODAL)
        {
            return (from tb952 in RetornaTodosRegistros()
                    where tb952.ID_ESTAT_MODAL == ID_ESTAT_MODAL
                    select tb952).FirstOrDefault();
        }

        /// <summary>
        /// Retorna o primeiro registro da entidade TB952_ESTAT_MODAL pela unidade "CO_EMP", pela modalidade "CO_MODU_CUR" e ano de referência "CO_ANO_REF".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="CO_MODU_CUR">Id da modalidade</param>
        /// <param name="CO_ANO_REF">Ano de referência</param>
        /// <returns>Entidade TB952_ESTAT_MODAL</returns>
        public static TB952_ESTAT_MODAL RetornaPeloOcorrEstatModal(int CO_EMP, int CO_MODU_CUR, int CO_ANO_REF)
        {
            return (from tb952 in RetornaTodosRegistros()
                    where tb952.TB25_EMPRESA.CO_EMP == CO_EMP && tb952.TB44_MODULO.CO_MODU_CUR == CO_MODU_CUR && tb952.CO_ANO_REF == CO_ANO_REF
                    select tb952).FirstOrDefault();
        }

        #endregion
    }
}