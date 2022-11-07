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
    public partial class TB273_TIPO_ATIVIDADE
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
        /// Exclue o registro da tabela TB273_TIPO_ATIVIDADE do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB273_TIPO_ATIVIDADE entity, bool saveChanges) 
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB273_TIPO_ATIVIDADE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB273_TIPO_ATIVIDADE.</returns>
        public static TB273_TIPO_ATIVIDADE Delete(TB273_TIPO_ATIVIDADE entity) 
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
        public static int SaveOrUpdate(TB273_TIPO_ATIVIDADE entity, bool saveChanges) 
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB273_TIPO_ATIVIDADE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB273_TIPO_ATIVIDADE.</returns>
        public static TB273_TIPO_ATIVIDADE SaveOrUpdate(TB273_TIPO_ATIVIDADE entity) 
        {
            SaveOrUpdate(entity, true);
            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB273_TIPO_ATIVIDADE de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB273_TIPO_ATIVIDADE.</returns>
        public static TB273_TIPO_ATIVIDADE GetByEntityKey(EntityKey entityKey)
        {
            return (TB273_TIPO_ATIVIDADE)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB273_TIPO_ATIVIDADE, ordenados pelo Id "ID_TIPO_ATIV".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB273_TIPO_ATIVIDADE de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB273_TIPO_ATIVIDADE> RetornaTodosRegistros() 
        {
            return GestorEntities.CurrentContext.TB273_TIPO_ATIVIDADE.OrderBy( t => t.ID_TIPO_ATIV ).AsObjectQuery();
        }
        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB273_TIPO_ATIVIDADE pela chave primária "ID_TIPO_ATIV".
        /// </summary>
        /// <param name="ID_TIPO_ATIV">Id da chave primária</param>
        /// <returns>Entidade TB273_TIPO_ATIVIDADE</returns>
        public static TB273_TIPO_ATIVIDADE RetornaPelaChavePrimaria(int ID_TIPO_ATIV) 
        {
            return (from tb273 in RetornaTodosRegistros()
                    where tb273.ID_TIPO_ATIV == ID_TIPO_ATIV
                    select tb273).FirstOrDefault();
        }

        /// <summary>
        /// Retorna um registro da entidade TB273_TIPO_ATIVIDADE pela chave primária "CO_SIGLA_ATIV".
        /// </summary>
        /// <param name="ID_TIPO_ATIV">Id da chave primária</param>
        /// <returns>Entidade TB273_TIPO_ATIVIDADE</returns>
        public static TB273_TIPO_ATIVIDADE RetornaPelaSigla(string CO_SIGLA_ATIV)
        {
            return (from tb273 in RetornaTodosRegistros()
                    where tb273.CO_SIGLA_ATIV == CO_SIGLA_ATIV
                    select tb273).FirstOrDefault();
        }

        #endregion
    }
}
