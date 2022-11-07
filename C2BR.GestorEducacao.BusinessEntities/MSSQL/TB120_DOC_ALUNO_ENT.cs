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
    public partial class TB120_DOC_ALUNO_ENT
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
        /// Exclue o registro da tabela TB120_DOC_ALUNO_ENT do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB120_DOC_ALUNO_ENT entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB120_DOC_ALUNO_ENT na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB120_DOC_ALUNO_ENT.</returns>
        public static TB120_DOC_ALUNO_ENT Delete(TB120_DOC_ALUNO_ENT entity)
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
        public static int SaveOrUpdate(TB120_DOC_ALUNO_ENT entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB120_DOC_ALUNO_ENT na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB120_DOC_ALUNO_ENT.</returns>
        public static TB120_DOC_ALUNO_ENT SaveOrUpdate(TB120_DOC_ALUNO_ENT entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB120_DOC_ALUNO_ENT de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB120_DOC_ALUNO_ENT.</returns>
        public static TB120_DOC_ALUNO_ENT GetByEntityKey(EntityKey entityKey)
        {
            return (TB120_DOC_ALUNO_ENT)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB120_DOC_ALUNO_ENT, ordenados pelo Id do aluno "CO_ALU".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB120_DOC_ALUNO_ENT de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB120_DOC_ALUNO_ENT> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB120_DOC_ALUNO_ENT.OrderBy( d => d.CO_ALU ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna todos os registros da entidade TB120_DOC_ALUNO_ENT de acordo com a unidade "CO_EMP".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <returns>ObjectQuery com todos os registros da entidade TB120_DOC_ALUNO_ENT de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB120_DOC_ALUNO_ENT> RetornaPelaEmpresa(int CO_EMP)
        {
            return GestorEntities.CurrentContext.TB120_DOC_ALUNO_ENT.Where( d => d.CO_EMP == CO_EMP ).OrderBy( d => d.CO_ALU ).AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade ADMMODULO pelas chaves primárias "CO_EMP", "CO_ALU" e "CO_TP_DOC_MAT".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="CO_ALU">Id do aluno</param>
        /// <param name="CO_TP_DOC_MAT">Id do tipo de documento de matrícula</param>
        /// <returns>Entidade TB120_DOC_ALUNO_ENT</returns>
        public static TB120_DOC_ALUNO_ENT RetornaPelaChavePrimaria(int CO_EMP, int CO_ALU, int CO_TP_DOC_MAT)
        {
            return (from tb120 in RetornaTodosRegistros()
                    where tb120.CO_EMP == CO_EMP && tb120.CO_ALU == CO_ALU && tb120.CO_TP_DOC_MAT == CO_TP_DOC_MAT
                    select tb120).FirstOrDefault();
        }

        #endregion
    }
}