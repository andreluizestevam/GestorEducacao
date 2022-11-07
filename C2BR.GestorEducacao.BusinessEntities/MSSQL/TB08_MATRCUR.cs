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
    public partial class TB08_MATRCUR
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
        /// Exclue o registro da tabela TB08_MATRCUR do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB08_MATRCUR entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB08_MATRCUR na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB08_MATRCUR.</returns>
        public static TB08_MATRCUR Delete(TB08_MATRCUR entity)
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
        public static int SaveOrUpdate(TB08_MATRCUR entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB08_MATRCUR na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB08_MATRCUR.</returns>
        public static TB08_MATRCUR SaveOrUpdate(TB08_MATRCUR entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB08_MATRCUR de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB08_MATRCUR.</returns>
        public static TB08_MATRCUR GetByEntityKey(EntityKey entityKey)
        {
            return (TB08_MATRCUR)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB08_MATRCUR, ordenados pela Id da unidade "CO_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB08_MATRCUR de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB08_MATRCUR> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB08_MATRCUR.OrderBy( m => m.CO_EMP ).AsObjectQuery().Include(typeof(TB07_ALUNO).Name);
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB08_MATRCUR pelas chaves primárias "CO_ALU", "CO_CUR", "CO_ANO_MES_MAT" e "NU_SEM_LET".
        /// </summary>
        /// <param name="CO_ALU">Id do aluno</param>
        /// <param name="CO_CUR">Id da série</param>
        /// <param name="CO_ANO_MES_MAT">Ano de matrícula</param>
        /// <param name="NU_SEM_LET">Semetre de matrícula</param>
        /// <returns>Entidade TB08_MATRCUR</returns>
        public static TB08_MATRCUR RetornaPelaChavePrimaria(int CO_ALU, int CO_CUR, string CO_ANO_MES_MAT, string NU_SEM_LET)
        {
            return (from tb08 in RetornaTodosRegistros()
                    where tb08.CO_ALU == CO_ALU && tb08.CO_CUR == CO_CUR && tb08.CO_ANO_MES_MAT == CO_ANO_MES_MAT && tb08.NU_SEM_LET == NU_SEM_LET
                    select tb08).FirstOrDefault();
        }

        /// <summary>
        /// Retorna o primeiro registro da entidade TB08_MATRCUR pelo Id do aluno "CO_ALU" e pelo Id da série "CO_CUR".
        /// </summary>
        /// <param name="CO_ALU">Id do aluno</param>
        /// <param name="CO_CUR">Id da série</param>
        /// <returns>Entidade TB08_MATRCUR</returns>
        public static TB08_MATRCUR RetornaPeloCurso(int CO_ALU, int CO_CUR)
        {
            return (from tb08 in RetornaTodosRegistros()
                    where tb08.CO_ALU == CO_ALU && tb08.CO_CUR == CO_CUR
                    select tb08).OrderByDescending( m => m.CO_ANO_MES_MAT ).FirstOrDefault();
        }

        /// <summary>
        /// Retorna o primeiro registro da entidade TB08_MATRCUR pelo Id do aluno "CO_ALU" e pelo ano de matrícula "CO_ANO_MES_MAT".
        /// </summary>
        /// <param name="CO_ALU">Id do aluno</param>
        /// <param name="CO_ANO_MES_MAT">Ano de matrícula</param>
        /// <returns>Entidade TB08_MATRCUR</returns>
        public static TB08_MATRCUR RetornaPeloAluno(int CO_ALU)
        {
            return (from tb08 in RetornaTodosRegistros()
                    where tb08.TB07_ALUNO.CO_ALU == CO_ALU && tb08.CO_SIT_MAT != "T"
                    select tb08).OrderByDescending( m => m.CO_ANO_MES_MAT ).FirstOrDefault();
        }
        /// <summary>
        /// Retorna o primeiro registro da entidade TB08_MATRCUR pelo Id do aluno "CO_ALU" e pelo ano de matrícula "CO_ANO_MES_MAT".
        /// </summary>
        /// <param name="CO_ALU">Id do aluno</param>
        /// <param name="CO_ANO_MES_MAT">Ano de matrícula</param>
        /// <returns>Entidade TB08_MATRCUR</returns>
        public static TB08_MATRCUR RetornaPeloAluno(int CO_ALU, string CO_ANO_MES_MAT)
        {
            return (from tb08 in RetornaTodosRegistros()
                    where tb08.TB07_ALUNO.CO_ALU == CO_ALU && tb08.CO_ANO_MES_MAT == CO_ANO_MES_MAT && tb08.CO_SIT_MAT != "T"
                    select tb08).OrderByDescending(m => m.CO_ANO_MES_MAT).FirstOrDefault();
        }

        #endregion
    }
}