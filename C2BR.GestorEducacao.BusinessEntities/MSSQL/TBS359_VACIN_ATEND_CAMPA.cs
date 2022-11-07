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
    public partial class TBS359_VACIN_ATEND_CAMPA
    {
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
        /// Exclue o registro da tabela TBS359_VACIN_ATEND_CAMPA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS359_VACIN_ATEND_CAMPA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS359_VACIN_ATEND_CAMPA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS359_VACIN_ATEND_CAMPA.</returns>
        public static TBS359_VACIN_ATEND_CAMPA Delete(TBS359_VACIN_ATEND_CAMPA entity)
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
        public static int SaveOrUpdate(TBS359_VACIN_ATEND_CAMPA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS359_VACIN_ATEND_CAMPA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS359_VACIN_ATEND_CAMPA.</returns>
        public static TBS359_VACIN_ATEND_CAMPA SaveOrUpdate(TBS359_VACIN_ATEND_CAMPA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS359_VACIN_ATEND_CAMPA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS359_VACIN_ATEND_CAMPA.</returns>
        public static TBS359_VACIN_ATEND_CAMPA GetByEntityKey(EntityKey entityKey)
        {
            return (TBS359_VACIN_ATEND_CAMPA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS359_VACIN_ATEND_CAMPA, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBS359_VACIN_ATEND_CAMPA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS359_VACIN_ATEND_CAMPA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS359_VACIN_ATEND_CAMPA.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS359_VACIN_ATEND_CAMPA pela chave primária "ID_VACIN_ATEND_CAMPA".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TBS359_VACIN_ATEND_CAMPA</returns>
        public static TBS359_VACIN_ATEND_CAMPA RetornaPelaChavePrimaria(int ID_VACIN_ATEND_CAMPA)
        {
            return (from tbs359 in RetornaTodosRegistros()
                    where tbs359.ID_VACIN_ATEND_CAMPA == ID_VACIN_ATEND_CAMPA
                    select tbs359).FirstOrDefault();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS359_VACIN_ATEND_CAMPA de acordo com o CO_ALU, ID_CAMPAN e ID_VACINA
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TBS359_VACIN_ATEND_CAMPA</returns>
        public static TBS359_VACIN_ATEND_CAMPA RetornaPaciVacinCampan(int ID_VACINA, int ID_CAMPAN, int CO_ALU)
        {
            return (from tbs359 in RetornaTodosRegistros()
                    where tbs359.TBS345_VACINA.ID_VACINA == ID_VACINA
                    && tbs359.TBS339_CAMPSAUDE.ID_CAMPAN == ID_CAMPAN
                    && tbs359.TBS341_CAMP_ATEND.CO_ALU == CO_ALU
                    select tbs359).FirstOrDefault();
        }

        #endregion
    }
}
