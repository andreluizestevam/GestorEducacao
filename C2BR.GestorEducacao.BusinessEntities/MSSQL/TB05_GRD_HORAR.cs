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
    public partial class TB05_GRD_HORAR
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
        /// Exclue o registro da tabela TB05_GRD_HORAR do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB05_GRD_HORAR entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB05_GRD_HORAR na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB05_GRD_HORAR.</returns>
        public static TB05_GRD_HORAR Delete(TB05_GRD_HORAR entity)
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
        public static int SaveOrUpdate(TB05_GRD_HORAR entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB05_GRD_HORAR na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB05_GRD_HORAR.</returns>
        public static TB05_GRD_HORAR SaveOrUpdate(TB05_GRD_HORAR entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB05_GRD_HORAR de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB05_GRD_HORAR.</returns>
        public static TB05_GRD_HORAR GetByEntityKey(EntityKey entityKey)
        {
            return (TB05_GRD_HORAR)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB05_GRD_HORAR, ordenados pelo ano da grade "CO_ANO_GRADE".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB05_GRD_HORAR de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB05_GRD_HORAR> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB05_GRD_HORAR.OrderBy( g => g.CO_ANO_GRADE ).AsObjectQuery();
        }

        #endregion
        
        /// <summary>
        /// Retorna um registro da entidade TB05_GRD_HORAR pelas chaves primárias "CO_EMP", "CO_MODU_CUR", "CO_CUR", "CO_TUR", "CO_MAT", "CO_DIA_SEMA_GRD", "TP_TURNO", "NR_TEMPO" e "CO_ANO_GRADE".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="CO_MODU_CUR">Id da modalidade</param>
        /// <param name="CO_CUR">Id da série</param>
        /// <param name="CO_TUR">Id da turma</param>
        /// <param name="CO_MAT">Id da matéria</param>
        /// <param name="CO_DIA_SEMA_GRD">Código do dia da semana</param>
        /// <param name="TP_TURNO">Turno</param>
        /// <param name="NR_TEMPO">Número do tempo de aula</param>
        /// <param name="CO_ANO_GRADE">Ano da grade</param>
        /// <returns>Entidade TB05_GRD_HORAR</returns>
        public static TB05_GRD_HORAR RetornaPelaChavePrimaria(int CO_EMP, int CO_MODU_CUR, int CO_CUR, int CO_TUR, int CO_MAT, int CO_DIA_SEMA_GRD, string TP_TURNO, int NR_TEMPO, int CO_ANO_GRADE)
        {
            return (from tb05 in RetornaTodosRegistros()
                    where tb05.CO_EMP == CO_EMP && tb05.CO_MODU_CUR == CO_MODU_CUR && tb05.CO_CUR == CO_CUR && tb05.CO_TUR == CO_TUR && tb05.CO_MAT == CO_MAT
                    && tb05.CO_DIA_SEMA_GRD == CO_DIA_SEMA_GRD && tb05.TP_TURNO == TP_TURNO && tb05.NR_TEMPO == NR_TEMPO && tb05.CO_ANO_GRADE == CO_ANO_GRADE
                    select tb05).OrderBy( g => g.CO_ANO_GRADE ).FirstOrDefault();
        }     
        #endregion
    }
}
