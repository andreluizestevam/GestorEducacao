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
    public partial class TB901_DESEMP_ESCOL
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
        /// Exclue o registro da tabela TB901_DESEMP_ESCOL do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB901_DESEMP_ESCOL entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB901_DESEMP_ESCOL na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB901_DESEMP_ESCOL.</returns>
        public static TB901_DESEMP_ESCOL Delete(TB901_DESEMP_ESCOL entity)
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
        public static int SaveOrUpdate(TB901_DESEMP_ESCOL entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB901_DESEMP_ESCOL na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB901_DESEMP_ESCOL.</returns>
        public static TB901_DESEMP_ESCOL SaveOrUpdate(TB901_DESEMP_ESCOL entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB901_DESEMP_ESCOL de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB901_DESEMP_ESCOL.</returns>
        public static TB901_DESEMP_ESCOL GetByEntityKey(EntityKey entityKey)
        {
            return (TB901_DESEMP_ESCOL)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB901_DESEMP_ESCOL, ordenados pelo Id "ID_DESEMP_ESCOL".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB901_DESEMP_ESCOL de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB901_DESEMP_ESCOL> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB901_DESEMP_ESCOL.OrderBy( d => d.ID_DESEMP_ESCOL ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB901_DESEMP_ESCOL pela chave primária "ID_DESEMP_ESCOL".
        /// </summary>
        /// <param name="ID_DESEMP_ESCOL">Id da chave primária</param>
        /// <returns>Entidade TB901_DESEMP_ESCOL</returns>
        public static TB901_DESEMP_ESCOL RetornaPelaChavePrimaria(int ID_DESEMP_ESCOL)
        {
            return (from tb901 in RetornaTodosRegistros()
                    where tb901.ID_DESEMP_ESCOL == ID_DESEMP_ESCOL
                    select tb901).FirstOrDefault();
        }

        /// <summary>
        /// Retorna o primeiro registro da entidade TB901_DESEMP_ESCOL pelos campos "CO_EMP", "CO_MODU_CUR", "CO_CUR", "CO_TUR", "CO_MAT_GRADE" e "CO_ANO_REF".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="CO_MODU_CUR">Id da modalidade</param>
        /// <param name="CO_CUR">Id da série</param>
        /// <param name="CO_TUR">Id da turma</param>
        /// <param name="CO_MAT_GRADE">Id da matéria</param>
        /// <param name="CO_ANO_REF">Ano de referência</param>
        /// <returns>Entidade TB901_DESEMP_ESCOL</returns>
        public static TB901_DESEMP_ESCOL RetornaPeloOcorrDesempEscol(int CO_EMP, int CO_MODU_CUR, int CO_CUR, int CO_TUR, int CO_MAT_GRADE, string CO_ANO_REF)
        {
            return (from tb901 in RetornaTodosRegistros()
                    where tb901.TB06_TURMAS.CO_EMP == CO_EMP && tb901.TB06_TURMAS.CO_MODU_CUR == CO_MODU_CUR && tb901.TB06_TURMAS.CO_CUR == CO_CUR
                    && tb901.TB06_TURMAS.CO_TUR == CO_TUR && tb901.CO_MAT_GRADE == CO_MAT_GRADE && tb901.CO_ANO_REF == CO_ANO_REF
                    select tb901).FirstOrDefault();
        }

        #endregion
    }
}