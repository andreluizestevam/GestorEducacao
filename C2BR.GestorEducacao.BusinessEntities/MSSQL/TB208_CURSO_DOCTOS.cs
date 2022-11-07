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
    public partial class TB208_CURSO_DOCTOS
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
        /// Exclue o registro da tabela TB208_CURSO_DOCTOS do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB208_CURSO_DOCTOS entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB208_CURSO_DOCTOS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB208_CURSO_DOCTOS.</returns>
        public static TB208_CURSO_DOCTOS Delete(TB208_CURSO_DOCTOS entity)
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
        public static int SaveOrUpdate(TB208_CURSO_DOCTOS entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB208_CURSO_DOCTOS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB208_CURSO_DOCTOS.</returns>
        public static TB208_CURSO_DOCTOS SaveOrUpdate(TB208_CURSO_DOCTOS entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB208_CURSO_DOCTOS de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB208_CURSO_DOCTOS.</returns>
        public static TB208_CURSO_DOCTOS GetByEntityKey(EntityKey entityKey)
        {
            return (TB208_CURSO_DOCTOS)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB208_CURSO_DOCTOS, ordenados pelo Id da série "CO_CUR".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB208_CURSO_DOCTOS de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB208_CURSO_DOCTOS> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB208_CURSO_DOCTOS.OrderBy( c => c.CO_CUR ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna todos os registros da entidade TB208_CURSO_DOCTOS de acordo com a unidade "CO_EMP", modalidade "CO_MODU_CUR" e série "CO_CUR".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="CO_MODU_CUR">Id da modalidade</param>
        /// <param name="CO_CUR">Id da série</param>
        /// <returns>ObjectQuery com todos os registros da entidade TB208_CURSO_DOCTOS de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB208_CURSO_DOCTOS> RetornaPeloCoEmpCoModuCurCoCur(int CO_EMP, int CO_MODU_CUR, int CO_CUR)
        {
            return (from tb208 in RetornaTodosRegistros()
                    where tb208.CO_EMP == CO_EMP && tb208.CO_MODU_CUR == CO_MODU_CUR && tb208.CO_CUR == CO_CUR
                    select tb208).AsObjectQuery();
        }

        #endregion
    }
}