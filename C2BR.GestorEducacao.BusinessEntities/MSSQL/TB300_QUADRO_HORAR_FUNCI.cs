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
    public partial class TB300_QUADRO_HORAR_FUNCI
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
        /// Exclue o registro da tabela TB300_QUADRO_HORAR_FUNCI do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB300_QUADRO_HORAR_FUNCI entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB300_QUADRO_HORAR_FUNCI na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB300_QUADRO_HORAR_FUNCI.</returns>
        public static TB300_QUADRO_HORAR_FUNCI Delete(TB300_QUADRO_HORAR_FUNCI entity)
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
        public static int SaveOrUpdate(TB300_QUADRO_HORAR_FUNCI entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB300_QUADRO_HORAR_FUNCI na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB300_QUADRO_HORAR_FUNCI.</returns>
        public static TB300_QUADRO_HORAR_FUNCI SaveOrUpdate(TB300_QUADRO_HORAR_FUNCI entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB300_QUADRO_HORAR_FUNCI de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB300_QUADRO_HORAR_FUNCI.</returns>
        public static TB300_QUADRO_HORAR_FUNCI GetByEntityKey(EntityKey entityKey)
        {
            return (TB300_QUADRO_HORAR_FUNCI)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB300_QUADRO_HORAR_FUNCI, ordenados pelo Id "ID_QUADRO_HORAR_FUNCI".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB300_QUADRO_HORAR_FUNCI de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB300_QUADRO_HORAR_FUNCI> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB300_QUADRO_HORAR_FUNCI.OrderBy(p => p.ID_QUADRO_HORAR_FUNCI).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB300_QUADRO_HORAR_FUNCI pela chave primária "ID_QUADRO_HORAR_FUNCI".
        /// </summary>
        /// <param name="ID_QUADRO_HORAR_FUNCI">Id da chave primária</param>
        /// <returns>Entidade TB300_QUADRO_HORAR_FUNCI</returns>
        public static TB300_QUADRO_HORAR_FUNCI RetornaPelaChavePrimaria(int ID_QUADRO_HORAR_FUNCI)
        {
            return (from tb300 in RetornaTodosRegistros()
                    where tb300.ID_QUADRO_HORAR_FUNCI == ID_QUADRO_HORAR_FUNCI
                    select tb300).FirstOrDefault();
        }

        /// <summary>
        /// Retorna todos os registros da entidade TB300_QUADRO_HORAR_FUNCI de acordo com a instituição "ORG_CODIGO_ORGAO".
        /// </summary>
        /// <param name="ORG_CODIGO_ORGAO">Id da instituição</param>
        /// <returns>ObjectQuery com todos os registros da entidade TB300_QUADRO_HORAR_FUNCI de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB300_QUADRO_HORAR_FUNCI> RetornaPelaInstituicao(int ORG_CODIGO_ORGAO)
        {
            return (from tb300 in RetornaTodosRegistros()
                    where tb300.TB000_INSTITUICAO.ORG_CODIGO_ORGAO == ORG_CODIGO_ORGAO
                    && tb300.TB25_EMPRESA.CO_EMP == null
                    select tb300).AsObjectQuery();
        }

        /// <summary>
        /// Retorna todos os registros da entidade TB300_QUADRO_HORAR_FUNCI de acordo com a unidade "CO_EMP".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <returns>ObjectQuery com todos os registros da entidade TB300_QUADRO_HORAR_FUNCI de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB300_QUADRO_HORAR_FUNCI> RetornaPelaUnidade(int CO_EMP)
        {
            return (from tb300 in RetornaTodosRegistros()
                    where tb300.TB25_EMPRESA.CO_EMP == CO_EMP
                    select tb300).AsObjectQuery();
        }

        #endregion
    }
}