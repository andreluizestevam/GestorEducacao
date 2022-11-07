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
    public partial class TB317_AGRUP_BOLSA
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
        /// Exclue o registro da tabela TB317_AGRUP_BOLSA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB317_AGRUP_BOLSA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB317_AGRUP_BOLSA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB317_AGRUP_BOLSA.</returns>
        public static TB317_AGRUP_BOLSA Delete(TB317_AGRUP_BOLSA entity)
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
        public static int SaveOrUpdate(TB317_AGRUP_BOLSA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB317_AGRUP_BOLSA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB317_AGRUP_BOLSA.</returns>
        public static TB317_AGRUP_BOLSA SaveOrUpdate(TB317_AGRUP_BOLSA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB317_AGRUP_BOLSA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB317_AGRUP_BOLSA.</returns>
        public static TB317_AGRUP_BOLSA GetByEntityKey(EntityKey entityKey)
        {
            return (TB317_AGRUP_BOLSA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB317_AGRUP_BOLSA, ordenados pela descrição "NO_AGRUP_BOLSA".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB_NUCLEO_INST de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB317_AGRUP_BOLSA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB317_AGRUP_BOLSA.OrderBy(t => t.NO_AGRUP_BOLSA).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB317_AGRUP_BOLSA onde o Id "ID_AGRUP_BOLSA" é o informado no parâmetro.
        /// </summary>
        /// <param name="ID_AGRUP_BOLSA">Id da chave primária</param>
        /// <returns>Entidade TB317_AGRUP_BOLSA</returns>
        public static TB317_AGRUP_BOLSA RetornaPelaChavePrimaria(int ID_AGRUP_BOLSA)
        {
            return (from tb317 in RetornaTodosRegistros()
                    where tb317.ID_AGRUP_BOLSA == ID_AGRUP_BOLSA
                    select tb317).FirstOrDefault();
        }

        #endregion
    }
}