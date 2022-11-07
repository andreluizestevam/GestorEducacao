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
    public partial class TBS360_VACIN_CAMPSAUDE
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
        /// Exclue o registro da tabela TBS356_PROC_MEDIC_PROCE do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS360_VACIN_CAMPSAUDE entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS360_VACIN_CAMPSAUDE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS360_VACIN_CAMPSAUDE.</returns>
        public static TBS360_VACIN_CAMPSAUDE Delete(TBS360_VACIN_CAMPSAUDE entity)
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
        public static int SaveOrUpdate(TBS360_VACIN_CAMPSAUDE entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS356_PROC_MEDIC_PROCE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS360_VACIN_CAMPSAUDE.</returns>
        public static TBS360_VACIN_CAMPSAUDE SaveOrUpdate(TBS360_VACIN_CAMPSAUDE entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS360_VACIN_CAMPSAUDE de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS360_VACIN_CAMPSAUDE.</returns>
        public static TBS360_VACIN_CAMPSAUDE GetByEntityKey(EntityKey entityKey)
        {
            return (TBS360_VACIN_CAMPSAUDE)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS360_VACIN_CAMPSAUDE, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBS360_VACIN_CAMPSAUDE de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS360_VACIN_CAMPSAUDE> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS360_VACIN_CAMPSAUDE.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS360_VACIN_CAMPSAUDE pela chave primária "ID_VACIN_CAMPSAUDE".
        /// </summary>
        /// <param name="CO_TIPO_MOV">Id da chave primária</param>
        /// <returns>Entidade TBS360_VACIN_CAMPSAUDE</returns>
        public static TBS360_VACIN_CAMPSAUDE RetornaPelaChavePrimaria(int ID_VACIN_CAMPSAUDE)
        {
            return (from tbs360 in RetornaTodosRegistros()
                    where tbs360.ID_VACIN_CAMPSAUDE == ID_VACIN_CAMPSAUDE
                    select tbs360).FirstOrDefault();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS360_VACIN_CAMPSAUDE pelos campos "ID_CAMPAN" e ID_VACINA.
        /// </summary>
        /// <param name="ID_CAMPAN"></param>
        /// <param name="ID_VACINA"></param>
        /// <returns></returns>
        public static TBS360_VACIN_CAMPSAUDE RetornaPelaVacinaCampsaude(int ID_CAMPAN, int ID_VACINA)
        {
            return (from tbs360 in RetornaTodosRegistros()
                    where tbs360.TBS339_CAMPSAUDE.ID_CAMPAN == ID_CAMPAN
                    && tbs360.TBS345_VACINA.ID_VACINA == ID_VACINA
                    select tbs360).FirstOrDefault();
        }

        #endregion
    }
}
