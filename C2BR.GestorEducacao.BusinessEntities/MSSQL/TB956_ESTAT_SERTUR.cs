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
    public partial class TB956_ESTAT_SERTUR
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
        /// Exclue o registro da tabela TB956_ESTAT_SERTUR do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB956_ESTAT_SERTUR entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB956_ESTAT_SERTUR na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB956_ESTAT_SERTUR.</returns>
        public static TB956_ESTAT_SERTUR Delete(TB956_ESTAT_SERTUR entity)
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
        public static int SaveOrUpdate(TB956_ESTAT_SERTUR entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB956_ESTAT_SERTUR na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB956_ESTAT_SERTUR.</returns>
        public static TB956_ESTAT_SERTUR SaveOrUpdate(TB956_ESTAT_SERTUR entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB956_ESTAT_SERTUR de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB956_ESTAT_SERTUR.</returns>
        public static TB956_ESTAT_SERTUR GetByEntityKey(EntityKey entityKey)
        {
            return (TB956_ESTAT_SERTUR)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB956_ESTAT_SERTUR, ordenados pelo Id "ID_ESTAT_SERTUR".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB956_ESTAT_SERTUR de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB956_ESTAT_SERTUR> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB956_ESTAT_SERTUR.OrderBy( e => e.ID_ESTAT_SERTUR ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB956_ESTAT_SERTUR pela chave primária "ID_ESTAT_SERTUR".
        /// </summary>
        /// <param name="ID_ESTAT_SERTUR">Id da chava primária</param>
        /// <returns>Entidade TB956_ESTAT_SERTUR</returns>
        public static TB956_ESTAT_SERTUR RetornaPelaChavePrimaria(int ID_ESTAT_SERTUR)
        {
            return (from tb956 in RetornaTodosRegistros()
                    where tb956.ID_ESTAT_SERTUR == ID_ESTAT_SERTUR
                    select tb956).FirstOrDefault();
        }

        /// <summary>
        /// Retorna o primeiro registro da entidade TB956_ESTAT_SERTUR pelos campos "CO_EMP", "CO_MODU_CUR", "CO_CUR", "CO_TUR", "ID_MATERIA" e "CO_ANO_REF".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="CO_MODU_CUR">Id da modalidade</param>
        /// <param name="CO_CUR">Id da série</param>
        /// /// <param name="CO_TUR">Id da turma</param>
        /// <param name="ID_MATERIA">Id da matéria</param>
        /// <param name="CO_ANO_REF">Ano de referência</param>
        /// <returns>Entidade TB956_ESTAT_SERTUR</returns>
        public static TB956_ESTAT_SERTUR RetornaPeloOcorrEstatSerTur(int CO_EMP, int CO_MODU_CUR, int CO_CUR, int CO_TUR, int ID_MATERIA, int CO_ANO_REF)
        {
            return (from tb956 in RetornaTodosRegistros()
                    where tb956.TB06_TURMAS.CO_EMP == CO_EMP && tb956.TB06_TURMAS.CO_MODU_CUR == CO_MODU_CUR && tb956.TB06_TURMAS.CO_CUR == CO_CUR
                    && tb956.TB06_TURMAS.CO_TUR == CO_TUR && tb956.ID_MATERIA == ID_MATERIA && tb956.CO_ANO_REF == CO_ANO_REF
                    select tb956).FirstOrDefault();
        }

        #endregion
    }
}