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
    public partial class TB63_ESPECIALIDADE
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
        /// Exclue o registro da tabela TB63_ESPECIALIDADE do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB63_ESPECIALIDADE entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB63_ESPECIALIDADE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB63_ESPECIALIDADE.</returns>
        public static TB63_ESPECIALIDADE Delete(TB63_ESPECIALIDADE entity)
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
        public static int SaveOrUpdate(TB63_ESPECIALIDADE entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB63_ESPECIALIDADE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB63_ESPECIALIDADE.</returns>
        public static TB63_ESPECIALIDADE SaveOrUpdate(TB63_ESPECIALIDADE entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB63_ESPECIALIDADE de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB63_ESPECIALIDADE.</returns>
        public static TB63_ESPECIALIDADE GetByEntityKey(EntityKey entityKey)
        {
            return (TB63_ESPECIALIDADE)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB63_ESPECIALIDADE, ordenados pelo Id da unidade "CO_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB63_ESPECIALIDADE de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB63_ESPECIALIDADE> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB63_ESPECIALIDADE.OrderBy( e => e.CO_EMP ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB63_ESPECIALIDADE pelas chaves primárias "CO_EMP", "CO_ESPEC", e "CO_ESPECIALIDADE".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="CO_ESPEC">Id da especialização</param>
        /// <param name="CO_ESPECIALIDADE">Id da especialidade</param>
        /// <returns>Entidade TB63_ESPECIALIDADE</returns>
        public static TB63_ESPECIALIDADE RetornaPelaChavePrimaria(int CO_EMP, int CO_ESPEC, int CO_ESPECIALIDADE)
        {
            return (from tb63 in RetornaTodosRegistros()
                    where tb63.CO_EMP == CO_EMP && tb63.CO_ESPEC == CO_ESPEC && tb63.CO_ESPECIALIDADE == CO_ESPECIALIDADE
                    select tb63).FirstOrDefault();
        }

        #endregion
    }
}