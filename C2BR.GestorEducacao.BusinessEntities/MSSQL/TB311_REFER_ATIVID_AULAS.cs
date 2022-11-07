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
    public partial class TB311_REFER_ATIVID_AULAS
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
        /// Exclue o registro da tabela TB311_REFER_ATIVID_AULAS do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB311_REFER_ATIVID_AULAS entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB311_REFER_ATIVID_AULAS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB311_REFER_ATIVID_AULAS.</returns>
        public static TB311_REFER_ATIVID_AULAS Delete(TB311_REFER_ATIVID_AULAS entity)
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
        public static int SaveOrUpdate(TB311_REFER_ATIVID_AULAS entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB311_REFER_ATIVID_AULAS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB311_REFER_ATIVID_AULAS.</returns>
        public static TB311_REFER_ATIVID_AULAS SaveOrUpdate(TB311_REFER_ATIVID_AULAS entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB311_REFER_ATIVID_AULAS de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB311_REFER_ATIVID_AULAS.</returns>
        public static TB311_REFER_ATIVID_AULAS GetByEntityKey(EntityKey entityKey)
        {
            return (TB311_REFER_ATIVID_AULAS)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB311_REFER_ATIVID_AULAS, ordenados pelo nome "ID_REFER_ATIVID_AULAS".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB311_REFER_ATIVID_AULAS de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB311_REFER_ATIVID_AULAS> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB311_REFER_ATIVID_AULAS.OrderBy(c => c.ID_REFER_ATIVID_AULAS).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna primeiro registro da entidade TB311_REFER_ATIVID_AULAS pelo cliente "ID_REFER_ATIVID_AULAS".
        /// </summary>
        /// <param name="ID_REFER_ATIVID_AULAS">Id da chave primária</param>
        /// <returns>Entidade TB311_REFER_ATIVID_AULAS</returns>
        public static TB311_REFER_ATIVID_AULAS RetornaPelaChavePrimaria(int ID_REFER_ATIVID_AULAS)
        {
            return (from tb311 in RetornaTodosRegistros()
                    where tb311.ID_REFER_ATIVID_AULAS == ID_REFER_ATIVID_AULAS
                    select tb311).FirstOrDefault();
        }

        #endregion
    }
}
