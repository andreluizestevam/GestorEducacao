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
    public partial class TB186_SISTE_AQUEC
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
        /// Exclue o registro da tabela TB186_SISTE_AQUEC do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB186_SISTE_AQUEC entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB186_SISTE_AQUEC na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB186_SISTE_AQUEC.</returns>
        public static TB186_SISTE_AQUEC Delete(TB186_SISTE_AQUEC entity)
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
        public static int SaveOrUpdate(TB186_SISTE_AQUEC entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB186_SISTE_AQUEC na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB186_SISTE_AQUEC.</returns>
        public static TB186_SISTE_AQUEC SaveOrUpdate(TB186_SISTE_AQUEC entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB186_SISTE_AQUEC de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB186_SISTE_AQUEC.</returns>
        public static TB186_SISTE_AQUEC GetByEntityKey(EntityKey entityKey)
        {
            return (TB186_SISTE_AQUEC)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB186_SISTE_AQUEC, ordenados pela descrição "DE_SISTE_AQUEC".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB186_SISTE_AQUEC de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB186_SISTE_AQUEC> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB186_SISTE_AQUEC.OrderBy( s => s.DE_SISTE_AQUEC ).AsObjectQuery();
        }
        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB186_SISTE_AQUEC pela chave primária "CO_SIGLA_SISTE_AQUEC".
        /// </summary>
        /// <param name="CO_SIGLA_SISTE_AQUEC">Id da chave primária</param>
        /// <returns>Entidade TB186_SISTE_AQUEC</returns>
        public static TB186_SISTE_AQUEC RetornaPelaChavePrimaria(string CO_SIGLA_SISTE_AQUEC)
        {
            return (from tb186 in RetornaTodosRegistros()
                    where tb186.CO_SIGLA_SISTE_AQUEC == CO_SIGLA_SISTE_AQUEC
                    select tb186).FirstOrDefault();
        }

        /// <summary>
        /// Retorna o primeiro registro da entidade TB186_SISTE_AQUEC pelo código do sistema de aquecimento "CO_SISTE_AQUEC".
        /// </summary>
        /// <param name="CO_SISTE_AQUEC">Código do sistema de aquecimento</param>
        /// <returns>Entidade TB186_SISTE_AQUEC</returns>
        public static TB186_SISTE_AQUEC RetornaPelaChavePrimaria(int CO_SISTE_AQUEC)
        {
            return (from tb186 in RetornaTodosRegistros()
                    where tb186.CO_SISTE_AQUEC == CO_SISTE_AQUEC
                    select tb186).FirstOrDefault();
        }
       
        #endregion
    }
}