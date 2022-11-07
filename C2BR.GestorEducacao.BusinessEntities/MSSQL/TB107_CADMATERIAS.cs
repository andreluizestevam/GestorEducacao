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
    public partial class TB107_CADMATERIAS
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
        /// Exclue o registro da tabela TB107_CADMATERIAS do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB107_CADMATERIAS entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB107_CADMATERIAS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB107_CADMATERIAS.</returns>
        public static TB107_CADMATERIAS Delete(TB107_CADMATERIAS entity)
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
        public static int SaveOrUpdate(TB107_CADMATERIAS entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB107_CADMATERIAS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB107_CADMATERIAS.</returns>
        public static TB107_CADMATERIAS SaveOrUpdate(TB107_CADMATERIAS entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB107_CADMATERIAS de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB107_CADMATERIAS.</returns>
        public static TB107_CADMATERIAS GetByEntityKey(EntityKey entityKey)
        {
            return (TB107_CADMATERIAS)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB107_CADMATERIAS, ordenados pelo nome "NO_MATERIA".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB107_CADMATERIAS de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB107_CADMATERIAS> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB107_CADMATERIAS.OrderBy( c => c.NO_MATERIA ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna todos os registros da entidade TB107_CADMATERIAS de acordo com a unidade "CO_EMP".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <returns>ObjectQuery com todos os registros da entidade TB107_CADMATERIAS de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB107_CADMATERIAS> RetornaPelaEmpresa(int CO_EMP)
        {
            return GestorEntities.CurrentContext.TB107_CADMATERIAS.Where( c => c.CO_EMP == CO_EMP ).OrderBy( c => c.CO_EMP ).AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TB107_CADMATERIAS pelas chaves primárias "CO_EMP" e "ID_MATERIA".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="ID_MATERIA">Id da matéria</param>
        /// <returns>Entidade TB107_CADMATERIAS</returns>
        public static TB107_CADMATERIAS RetornaPelaChavePrimaria(int CO_EMP, int ID_MATERIA)
        {
            return (from tb107 in RetornaTodosRegistros()
                    where tb107.CO_EMP == CO_EMP && tb107.ID_MATERIA == ID_MATERIA
                    select tb107).FirstOrDefault();
        }

        #endregion
    }
}