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
    public partial class VW079_HIST_ALUNO
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
        /// Exclue o registro da tabela VW079_HIST_ALUNO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(VW079_HIST_ALUNO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela VW079_HIST_ALUNO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade VW079_HIST_ALUNO.</returns>
        public static VW079_HIST_ALUNO Delete(VW079_HIST_ALUNO entity)
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
        public static int SaveOrUpdate(VW079_HIST_ALUNO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela VW079_HIST_ALUNO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade VW079_HIST_ALUNO.</returns>
        public static VW079_HIST_ALUNO SaveOrUpdate(VW079_HIST_ALUNO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade VW079_HIST_ALUNO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade VW079_HIST_ALUNO.</returns>
        public static VW079_HIST_ALUNO GetByEntityKey(EntityKey entityKey)
        {
            return (VW079_HIST_ALUNO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade VW079_HIST_ALUNO, ordenados pelo Id do aluno "CO_ALU".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade VW079_HIST_ALUNO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<VW079_HIST_ALUNO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.VW079_HIST_ALUNO.OrderBy( h => h.CO_ALU ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade VW079_HIST_ALUNO pelas chaves primárias "CO_EMP", "CO_ALU", "CO_MODU_CUR", "CO_CUR", "CO_ANO_REF" e "CO_MAT".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="CO_ALU">Id do aluno</param>
        /// <param name="CO_MODU_CUR">Id da modalidade</param>
        /// <param name="CO_CUR">Id da série</param>
        /// <param name="CO_ANO_REF">Ano de referência</param>
        /// <param name="CO_MAT">Id da matéria</param>
        /// <returns>Entidade VW079_HIST_ALUNO</returns>
        public static VW079_HIST_ALUNO RetornaPelaChavePrimaria(int CO_EMP, int CO_ALU, int CO_MODU_CUR, int CO_CUR, string CO_ANO_REF, int CO_MAT)
        {
            return (from tb079 in RetornaTodosRegistros()
                    where tb079.CO_ALU == CO_ALU && tb079.CO_EMP == CO_EMP && tb079.CO_MODU_CUR == CO_MODU_CUR
                        && tb079.CO_CUR == CO_CUR && tb079.CO_ANO_REF == CO_ANO_REF && tb079.CO_MAT == CO_MAT
                    select tb079).FirstOrDefault();
        }

        #endregion
    }
}