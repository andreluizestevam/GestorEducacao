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
    public partial class TB159_AGENDA_PLANT_COLABOR
    {
        #region Mensagens de inconsistências

        /// <summary>
        /// Menságem apresentada quando o tempo de descanso é insuficiente
        /// </summary>
        public string DESC_INSUF 
        {
            get
            {
                return "Quantidade de Hora de Descanso menor que o Mínimo Aceitavel";
            }
        }

        #endregion

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
        /// Exclue o registro da tabela TB159_AGENDA_PLANT_COLABOR do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB159_AGENDA_PLANT_COLABOR entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB159_AGENDA_PLANT_COLABOR na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB159_AGENDA_PLANT_COLABOR.</returns>
        public static TB159_AGENDA_PLANT_COLABOR Delete(TB159_AGENDA_PLANT_COLABOR entity)
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
        public static int SaveOrUpdate(TB159_AGENDA_PLANT_COLABOR entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB159_AGENDA_PLANT_COLABOR na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB159_AGENDA_PLANT_COLABOR.</returns>
        public static TB159_AGENDA_PLANT_COLABOR SaveOrUpdate(TB159_AGENDA_PLANT_COLABOR entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB159_AGENDA_PLANT_COLABOR de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB159_AGENDA_PLANT_COLABOR.</returns>
        public static TB159_AGENDA_PLANT_COLABOR GetByEntityKey(EntityKey entityKey)
        {
            return (TB159_AGENDA_PLANT_COLABOR)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB159_AGENDA_PLANT_COLABOR, ordenados pelo nome "NO_DEPTO".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB159_AGENDA_PLANT_COLABOR de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB159_AGENDA_PLANT_COLABOR> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB159_AGENDA_PLANT_COLABOR.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TB906_REGIAO pela chave primária "CO_CIDADE".
        /// </summary>
        /// <param name="CO_CIDADE">Id da chave primária</param>
        /// <returns>Entidade TB906_REGIAO</returns>
        public static TB159_AGENDA_PLANT_COLABOR RetornaPelaChavePrimaria(int CO_AGEND_PLANT_COLAB)
        {
            return (from tb159 in RetornaTodosRegistros()
                    where tb159.CO_AGEND_PLANT_COLAB == CO_AGEND_PLANT_COLAB
                    select tb159).FirstOrDefault();
        }


        #endregion

    }
}
