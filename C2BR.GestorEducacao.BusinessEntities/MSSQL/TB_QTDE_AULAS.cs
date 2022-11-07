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
    public partial class TB_QTDE_AULAS
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
        /// Exclue o registro da tabela TB_QTDE_AULAS do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB_QTDE_AULAS entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB_QTDE_AULAS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB_QTDE_AULAS.</returns>
        public static TB_QTDE_AULAS Delete(TB_QTDE_AULAS entity)
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
        public static int SaveOrUpdate(TB_QTDE_AULAS entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB_QTDE_AULAS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB_QTDE_AULAS.</returns>
        public static TB_QTDE_AULAS SaveOrUpdate(TB_QTDE_AULAS entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB_QTDE_AULAS de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB_QTDE_AULAS.</returns>
        public static TB_QTDE_AULAS GetByEntityKey(EntityKey entityKey)
        {
            return (TB_QTDE_AULAS)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB_QTDE_AULAS, ordenados pelo ano de referência "CO_ANO_REF".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB_QTDE_AULAS de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB_QTDE_AULAS> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB_QTDE_AULAS.OrderBy( q => q.CO_ANO_REF ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB_QTDE_AULAS pelas chaves primárias informadadas no parâmetro.
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="CO_MODU_CUR">Id da modalidade</param>
        /// <param name="CO_CUR">Id da série</param>
        /// <param name="CO_ANO_REF">Ano de Referência</param>
        /// <param name="CO_MAT">Id da matéria</param>
        /// <returns>Entidade TB_QTDE_AULAS</returns>
        public static TB_QTDE_AULAS RetornaPelaChavePrimaria(int CO_EMP, int CO_MODU_CUR, int CO_CUR, int CO_ANO_REF, int CO_MAT)
        {
            return (from tbQtdAulas in RetornaTodosRegistros()
                    where tbQtdAulas.CO_EMP == CO_EMP && tbQtdAulas.CO_MODU_CUR == CO_MODU_CUR && tbQtdAulas.CO_CUR == CO_CUR
                    && tbQtdAulas.CO_ANO_REF == CO_ANO_REF && tbQtdAulas.CO_MAT == CO_MAT
                    select tbQtdAulas).FirstOrDefault();
        }

        #endregion
    }
}