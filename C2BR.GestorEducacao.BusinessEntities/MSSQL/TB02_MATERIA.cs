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
    public partial class TB02_MATERIA
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
        /// Exclue o registro da tabela TB02_MATERIA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB02_MATERIA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB02_MATERIA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB02_MATERIA.</returns>
        public static TB02_MATERIA Delete(TB02_MATERIA entity)
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
        public static int SaveOrUpdate(TB02_MATERIA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB02_MATERIA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB02_MATERIA.</returns>
        public static TB02_MATERIA SaveOrUpdate(TB02_MATERIA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB02_MATERIA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB02_MATERIA.</returns>
        public static TB02_MATERIA GetByEntityKey(EntityKey entityKey)
        {
            return (TB02_MATERIA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB02_MATERIA, ordenado pelo Id "CO_MAT".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB02_MATERIA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB02_MATERIA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB02_MATERIA.OrderBy( m => m.CO_MAT ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna todos os registro da entidade TB02_MATERIA de acordo com a unidade, modalidade e série
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="CO_MODU_CUR">Id da modalidade</param>
        /// <param name="CO_CUR">Id da série</param>
        /// <returns>ObjectQuery com todos os registros da entidade TB02_MATERIA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB02_MATERIA> RetornaPelaModalidadeSerie(int CO_EMP, int CO_MODU_CUR, int CO_CUR)
        {
            return (from tb02 in TB02_MATERIA.RetornaTodosRegistros() 
                    where tb02.CO_EMP == CO_EMP && tb02.CO_MODU_CUR == CO_MODU_CUR && tb02.CO_CUR == CO_CUR
                    select tb02).AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TB02_MATERIA pelas chaves primárias "CO_EMP", "CO_MODU_CUR", "CO_MAT" e "CO_CUR".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="CO_MODU_CUR">Id da modalidade</param>
        /// <param name="CO_MAT">Id da matéria</param>
        /// <param name="CO_CUR">Id da série</param>
        /// <returns>Entidade TB02_MATERIA</returns>
        public static TB02_MATERIA RetornaPelaChavePrimaria(int CO_EMP, int CO_MODU_CUR, int CO_MAT, int CO_CUR)
        {
            return (from tb02 in RetornaTodosRegistros()
                    where tb02.CO_EMP == CO_EMP && tb02.CO_MODU_CUR == CO_MODU_CUR && tb02.CO_CUR == CO_CUR && tb02.CO_MAT == CO_MAT
                    select tb02).FirstOrDefault();
        }

        #endregion
    }
}