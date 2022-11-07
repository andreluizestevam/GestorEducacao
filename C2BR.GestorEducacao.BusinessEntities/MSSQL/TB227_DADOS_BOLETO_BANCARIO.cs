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
    public partial class TB227_DADOS_BOLETO_BANCARIO
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
        /// Exclue o registro da tabela TB227_DADOS_BOLETO_BANCARIO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB227_DADOS_BOLETO_BANCARIO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB227_DADOS_BOLETO_BANCARIO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB227_DADOS_BOLETO_BANCARIO.</returns>
        public static TB227_DADOS_BOLETO_BANCARIO Delete(TB227_DADOS_BOLETO_BANCARIO entity)
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
        public static int SaveOrUpdate(TB227_DADOS_BOLETO_BANCARIO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB227_DADOS_BOLETO_BANCARIO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB227_DADOS_BOLETO_BANCARIO.</returns>
        public static TB227_DADOS_BOLETO_BANCARIO SaveOrUpdate(TB227_DADOS_BOLETO_BANCARIO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB227_DADOS_BOLETO_BANCARIO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB227_DADOS_BOLETO_BANCARIO.</returns>
        public static TB227_DADOS_BOLETO_BANCARIO GetByEntityKey(EntityKey entityKey)
        {
            return (TB227_DADOS_BOLETO_BANCARIO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB227_DADOS_BOLETO_BANCARIO, ordenados pelo Id "ID_BOLETO".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB227_DADOS_BOLETO_BANCARIO de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB227_DADOS_BOLETO_BANCARIO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB227_DADOS_BOLETO_BANCARIO.OrderBy( d => d.ID_BOLETO ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB227_DADOS_BOLETO_BANCARIO pela chave primária "ID_BOLETO".
        /// </summary>
        /// <param name="ID_BOLETO">Id da chave primária</param>
        /// <returns>Entidade TB227_DADOS_BOLETO_BANCARIO</returns>
        public static TB227_DADOS_BOLETO_BANCARIO RetornaPelaChavePrimaria(int ID_BOLETO)
        {
            return (from tb227 in RetornaTodosRegistros()
                    where tb227.ID_BOLETO == ID_BOLETO
                    select tb227).FirstOrDefault();
        }

        #endregion
    }
}