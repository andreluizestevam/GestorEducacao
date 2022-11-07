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
    public partial class TB106_ATIVEXTRA_ALUNO
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
        /// Exclue o registro da tabela TB106_ATIVEXTRA_ALUNO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB106_ATIVEXTRA_ALUNO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB106_ATIVEXTRA_ALUNO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB106_ATIVEXTRA_ALUNO.</returns>
        public static TB106_ATIVEXTRA_ALUNO Delete(TB106_ATIVEXTRA_ALUNO entity)
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
        public static int SaveOrUpdate(TB106_ATIVEXTRA_ALUNO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB106_ATIVEXTRA_ALUNO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB106_ATIVEXTRA_ALUNO.</returns>
        public static TB106_ATIVEXTRA_ALUNO SaveOrUpdate(TB106_ATIVEXTRA_ALUNO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB106_ATIVEXTRA_ALUNO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB106_ATIVEXTRA_ALUNO.</returns>
        public static TB106_ATIVEXTRA_ALUNO GetByEntityKey(EntityKey entityKey)
        {
            return (TB106_ATIVEXTRA_ALUNO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB106_ATIVEXTRA_ALUNO, ordenados pela id do aluno "CO_ALU".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB106_ATIVEXTRA_ALUNO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB106_ATIVEXTRA_ALUNO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB106_ATIVEXTRA_ALUNO.OrderBy( a => a.CO_ALU ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna todos os registros da entidade TB106_ATIVEXTRA_ALUNO de acordo com a unidade "CO_EMP".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <returns>ObjectQuery com todos os registros da entidade TB106_ATIVEXTRA_ALUNO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB106_ATIVEXTRA_ALUNO> RetornaPelaEmpresa(int CO_EMP)
        {
            return GestorEntities.CurrentContext.TB106_ATIVEXTRA_ALUNO.Where( a => a.CO_EMP == CO_EMP ).OrderBy( a => a.CO_ALU ).AsObjectQuery();
        }

        /// <summary>
        /// Retorna primeiro registro da entidade ADMMODULO pelas chaves primárias "CO_EMP", "CO_ALU" e "CO_ATIV_EXTRA".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="CO_ALU">Id do aluno</param>
        /// <param name="CO_ATIV_EXTRA">Id da atividade extra</param>
        /// <returns>Entidade TB106_ATIVEXTRA_ALUNO</returns>
        public static TB106_ATIVEXTRA_ALUNO RetornaPelaChavePrimaria(int CO_EMP, int CO_ALU, int CO_ATIV_EXTRA)
        {
            return (from tb106 in RetornaTodosRegistros()
                    where tb106.CO_EMP == CO_EMP && tb106.CO_ALU == CO_ALU && tb106.CO_ATIV_EXTRA == CO_ATIV_EXTRA
                    select tb106).FirstOrDefault();
        }

        /// <summary>
        /// Retorna um registro da entidade ADMMODULO pelas chaves primárias "CO_EMP", "CO_ALU", "CO_ATIV_EXTRA" e "CO_INSC_ATIV".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="CO_ALU">Id do aluno</param>
        /// <param name="CO_ATIV_EXTRA">Id da atividade extra</param>
        /// <param name="CO_INSC_ATIV">Id auto-incremento</param>
        /// <returns>Entidade TB106_ATIVEXTRA_ALUNO</returns>
        public static TB106_ATIVEXTRA_ALUNO RetornaPelaChavePrimaria(int CO_EMP, int CO_ALU, int CO_ATIV_EXTRA, int CO_INSC_ATIV)
        {
            return (from tb106 in RetornaTodosRegistros()
                    where tb106.CO_EMP == CO_EMP && tb106.CO_ALU == CO_ALU && tb106.CO_ATIV_EXTRA == CO_ATIV_EXTRA && tb106.CO_INSC_ATIV == CO_INSC_ATIV
                    select tb106).FirstOrDefault();
        }

        #endregion
    }
}