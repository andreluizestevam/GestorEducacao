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
    public partial class TB318_AGRUP_ATIVEXTRA
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
        /// Exclue o registro da tabela TB318_AGRUP_ATIVEXTRA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB318_AGRUP_ATIVEXTRA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB318_AGRUP_ATIVEXTRA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB318_AGRUP_ATIVEXTRA.</returns>
        public static TB318_AGRUP_ATIVEXTRA Delete(TB318_AGRUP_ATIVEXTRA entity)
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
        public static int SaveOrUpdate(TB318_AGRUP_ATIVEXTRA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB318_AGRUP_ATIVEXTRA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB318_AGRUP_ATIVEXTRA.</returns>
        public static TB318_AGRUP_ATIVEXTRA SaveOrUpdate(TB318_AGRUP_ATIVEXTRA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB318_AGRUP_ATIVEXTRA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB318_AGRUP_ATIVEXTRA.</returns>
        public static TB318_AGRUP_ATIVEXTRA GetByEntityKey(EntityKey entityKey)
        {
            return (TB318_AGRUP_ATIVEXTRA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB318_AGRUP_ATIVEXTRA, ordenados pela descrição "DE_AGRUP_ATIVEXTRA".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB318_AGRUP_ATIVEXTRA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB318_AGRUP_ATIVEXTRA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB318_AGRUP_ATIVEXTRA.OrderBy(t => t.DE_AGRUP_ATIVEXTRA).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB318_AGRUP_ATIVEXTRA onde o Id "ID_AGRUP_ATIVEXTRA" é o informado no parâmetro.
        /// </summary>
        /// <param name="ID_AGRUP_ATIVEXTRA">Id da chave primária</param>
        /// <returns>Entidade TB318_AGRUP_ATIVEXTRA</returns>
        public static TB318_AGRUP_ATIVEXTRA RetornaPelaChavePrimaria(int ID_AGRUP_ATIVEXTRA)
        {
            return (from tb318 in RetornaTodosRegistros()
                    where tb318.ID_AGRUP_ATIVEXTRA == ID_AGRUP_ATIVEXTRA
                    select tb318).FirstOrDefault();
        }

        #endregion
    }
}