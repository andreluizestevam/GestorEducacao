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
    public partial class TB955_ESTAT_SERMAT
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
        /// Exclue o registro da tabela TB955_ESTAT_SERMAT do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB955_ESTAT_SERMAT entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB955_ESTAT_SERMAT na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB955_ESTAT_SERMAT.</returns>
        public static TB955_ESTAT_SERMAT Delete(TB955_ESTAT_SERMAT entity)
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
        public static int SaveOrUpdate(TB955_ESTAT_SERMAT entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB955_ESTAT_SERMAT na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB955_ESTAT_SERMAT.</returns>
        public static TB955_ESTAT_SERMAT SaveOrUpdate(TB955_ESTAT_SERMAT entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB955_ESTAT_SERMAT de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB955_ESTAT_SERMAT.</returns>
        public static TB955_ESTAT_SERMAT GetByEntityKey(EntityKey entityKey)
        {
            return (TB955_ESTAT_SERMAT)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB955_ESTAT_SERMAT, ordenados pelo Id "ID_ESTAT_SERMAT".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB955_ESTAT_SERMAT de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB955_ESTAT_SERMAT> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB955_ESTAT_SERMAT.OrderBy( e => e.ID_ESTAT_SERMAT ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB955_ESTAT_SERMAT pela chave primária "ID_ESTAT_SERMAT".
        /// </summary>
        /// <param name="ID_ESTAT_SERMAT">Id da chave primária</param>
        /// <returns>Entidade TB955_ESTAT_SERMAT</returns>
        public static TB955_ESTAT_SERMAT RetornaPelaChavePrimaria(int ID_ESTAT_SERMAT)
        {
            return (from tb955 in RetornaTodosRegistros()
                    where tb955.ID_ESTAT_SERMAT == ID_ESTAT_SERMAT
                    select tb955).FirstOrDefault();
        }

        /// <summary>
        /// Retorna o primeiro registro da entidade TB955_ESTAT_SERMAT pelos campos "CO_EMP", "CO_MODU_CUR", "CO_CUR", "ID_MATERIA" e "CO_ANO_REF".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="CO_MODU_CUR">Id da modalidade</param>
        /// <param name="CO_CUR">Id da série</param>
        /// <param name="ID_MATERIA">Id da matéria</param>
        /// <param name="CO_ANO_REF">Ano de referência</param>
        /// <returns>Entidade TB955_ESTAT_SERMAT</returns>
        public static TB955_ESTAT_SERMAT RetornaPeloOcorrEstatSerMat(int CO_EMP, int CO_MODU_CUR, int CO_CUR, int ID_MATERIA, int CO_ANO_REF)
        {
            return (from tb955 in RetornaTodosRegistros()
                    where tb955.TB01_CURSO.CO_EMP == CO_EMP && tb955.TB01_CURSO.CO_MODU_CUR == CO_MODU_CUR && tb955.TB01_CURSO.CO_CUR == CO_CUR
                    && tb955.ID_MATERIA == ID_MATERIA && tb955.CO_ANO_REF == CO_ANO_REF
                    select tb955).FirstOrDefault();
        }

        #endregion
    }
}