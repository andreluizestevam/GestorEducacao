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
    public partial class TB143_ATEST_MEDIC
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
        /// Exclue o registro da tabela TB143_ATEST_MEDIC do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB143_ATEST_MEDIC entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB143_ATEST_MEDIC na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB143_ATEST_MEDIC.</returns>
        public static TB143_ATEST_MEDIC Delete(TB143_ATEST_MEDIC entity)
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
        public static int SaveOrUpdate(TB143_ATEST_MEDIC entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB143_ATEST_MEDIC na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB143_ATEST_MEDIC.</returns>
        public static TB143_ATEST_MEDIC SaveOrUpdate(TB143_ATEST_MEDIC entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB143_ATEST_MEDIC de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB143_ATEST_MEDIC.</returns>
        public static TB143_ATEST_MEDIC GetByEntityKey(EntityKey entityKey)
        {
            return (TB143_ATEST_MEDIC)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB143_ATEST_MEDIC, ordenados pelo Id "IDE_ATEST_MEDIC".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB143_ATEST_MEDIC de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB143_ATEST_MEDIC> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB143_ATEST_MEDIC.OrderBy( a => a.IDE_ATEST_MEDIC ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB143_ATEST_MEDIC pelas chaves primárias "IDE_ATEST_MEDIC" e "ORG_CODIGO_ORGAO".
        /// </summary>
        /// <param name="IDE_ATEST_MEDIC">Id do atestado médico</param>
        /// <param name="ORG_CODIGO_ORGAO">Id da instituição</param>
        /// <returns>Entidade TB143_ATEST_MEDIC</returns>
        public static TB143_ATEST_MEDIC RetornaPelaChavePrimaria(int IDE_ATEST_MEDIC, int ORG_CODIGO_ORGAO)
        {
            return (from tb143 in RetornaTodosRegistros()
                    where tb143.IDE_ATEST_MEDIC == IDE_ATEST_MEDIC && tb143.ORG_CODIGO_ORGAO == ORG_CODIGO_ORGAO
                    select tb143).FirstOrDefault();
        }

        /// <summary>
        /// Retorna todos os registros da entidade TB143_ATEST_MEDIC de acordo com a instituição "ORG_CODIGO_ORGAO".
        /// </summary>
        /// <param name="ORG_CODIGO_ORGAO">Id da instituição</param>
        /// <returns>ObjectQuery com todos os registros da entidade TB143_ATEST_MEDIC de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB143_ATEST_MEDIC> RetornaPelaInstituicao(int ORG_CODIGO_ORGAO)
        {
            return GestorEntities.CurrentContext.TB143_ATEST_MEDIC.Where( a => a.ORG_CODIGO_ORGAO == ORG_CODIGO_ORGAO).OrderBy( a => a.IDE_ATEST_MEDIC ).AsObjectQuery();
        }

        #endregion
    }
}