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
    public partial class TB953_ESTAT_SERIE
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
        /// Exclue o registro da tabela TB953_ESTAT_SERIE do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB953_ESTAT_SERIE entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB953_ESTAT_SERIE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB953_ESTAT_SERIE.</returns>
        public static TB953_ESTAT_SERIE Delete(TB953_ESTAT_SERIE entity)
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
        public static int SaveOrUpdate(TB953_ESTAT_SERIE entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB953_ESTAT_SERIE na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB953_ESTAT_SERIE.</returns>
        public static TB953_ESTAT_SERIE SaveOrUpdate(TB953_ESTAT_SERIE entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB953_ESTAT_SERIE de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB953_ESTAT_SERIE.</returns>
        public static TB953_ESTAT_SERIE GetByEntityKey(EntityKey entityKey)
        {
            return (TB953_ESTAT_SERIE)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB953_ESTAT_SERIE, ordenados pelo Id "ID_ESTAT_SERIE".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB953_ESTAT_SERIE de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB953_ESTAT_SERIE> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB953_ESTAT_SERIE.OrderBy( e => e.ID_ESTAT_SERIE ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB953_ESTAT_SERIE pela chave primária "ID_ESTAT_SERIE".
        /// </summary>
        /// <param name="ID_ESTAT_SERIE">Id da chave primária</param>
        /// <returns>Entidade TB953_ESTAT_SERIE</returns>
        public static TB953_ESTAT_SERIE RetornaPelaChavePrimaria(int ID_ESTAT_SERIE)
        {
            return (from tb953 in RetornaTodosRegistros()
                    where tb953.ID_ESTAT_SERIE == ID_ESTAT_SERIE
                    select tb953).FirstOrDefault();
        }

        /// <summary>
        /// Retorna o primeiro registro da entidade TB953_ESTAT_SERIE pela unidade "CO_EMP", pela modalidade "CO_MODU_CUR", pela série "CO_CUR" e ano de referência "CO_ANO_REF".
        /// </summary>
        /// <param name="CO_EMP">Id da Unidade</param>
        /// <param name="CO_MODU_CUR">Id da modalidade</param>
        /// <param name="CO_CUR">Id da série</param>
        /// <param name="CO_ANO_REF">Ano de referência</param>
        /// <returns>Entidade TB953_ESTAT_SERIE</returns>
        public static TB953_ESTAT_SERIE RetornaPeloOcorrEstatSerie(int CO_EMP, int CO_MODU_CUR, int CO_CUR, int CO_ANO_REF)
        {
            return (from tb953 in RetornaTodosRegistros()
                    where tb953.TB01_CURSO.CO_EMP == CO_EMP && tb953.TB01_CURSO.CO_MODU_CUR == CO_MODU_CUR
                    && tb953.TB01_CURSO.CO_CUR == CO_CUR && tb953.CO_ANO_REF == CO_ANO_REF
                    select tb953).FirstOrDefault();
        }

        #endregion
    }
}