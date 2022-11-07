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
    public partial class TB131_TEMPO_AULA
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
        /// Exclue o registro da tabela TB131_TEMPO_AULA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB131_TEMPO_AULA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB131_TEMPO_AULA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB131_TEMPO_AULA.</returns>
        public static TB131_TEMPO_AULA Delete(TB131_TEMPO_AULA entity)
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
        public static int SaveOrUpdate(TB131_TEMPO_AULA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB131_TEMPO_AULA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB131_TEMPO_AULA.</returns>
        public static TB131_TEMPO_AULA SaveOrUpdate(TB131_TEMPO_AULA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB131_TEMPO_AULA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB131_TEMPO_AULA.</returns>
        public static TB131_TEMPO_AULA GetByEntityKey(EntityKey entityKey)
        {
            return (TB131_TEMPO_AULA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB131_TEMPO_AULA, ordenados pelo Id da unidade "CO_EMP" e pelo Id da série "CO_CUR".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB131_TEMPO_AULA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB131_TEMPO_AULA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB131_TEMPO_AULA.OrderBy( t => t.CO_EMP ).ThenBy( t => t.CO_CUR ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna todos os registros da entidade TB131_TEMPO_AULA de acordo com a unidade "CO_EMP".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <returns>ObjectQuery com todos os registros da entidade TB131_TEMPO_AULA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB131_TEMPO_AULA> RetornaPelaEmpresa(int CO_EMP)
        {
            return GestorEntities.CurrentContext.TB131_TEMPO_AULA.Where( t => t.CO_EMP == CO_EMP ).OrderBy( t => t.CO_CUR ).AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TB131_TEMPO_AULA pelas chaves primárias "CO_EMP", "CO_MODU_CUR", "CO_CUR", "NR_TEMPO" e "TP_TURNO".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="CO_MODU_CUR">Id da modalidade</param>
        /// <param name="CO_CUR">Id da série</param>
        /// <param name="NR_TEMPO">Número do tempo de aula</param>
        /// <param name="TP_TURNO">Turno</param>
        /// <returns>Entidade TB131_TEMPO_AULA</returns>
        public static TB131_TEMPO_AULA RetornaPelaChavePrimaria(int CO_EMP, int CO_MODU_CUR, int CO_CUR, int NR_TEMPO, string TP_TURNO)
        {
            return (from tb131 in RetornaTodosRegistros()
                    where tb131.CO_EMP == CO_EMP && tb131.CO_MODU_CUR == CO_MODU_CUR && tb131.CO_CUR == CO_CUR 
                    && tb131.NR_TEMPO == NR_TEMPO && tb131.TP_TURNO == TP_TURNO
                    select tb131).FirstOrDefault();
        }
        #endregion
    }
}