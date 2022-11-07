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
    public partial class TB197_REMES_SOLIC
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
        /// Exclue o registro da tabela TB197_REMES_SOLIC do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB197_REMES_SOLIC entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB197_REMES_SOLIC na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB197_REMES_SOLIC.</returns>
        public static TB197_REMES_SOLIC Delete(TB197_REMES_SOLIC entity)
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
        public static int SaveOrUpdate(TB197_REMES_SOLIC entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB197_REMES_SOLIC na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB197_REMES_SOLIC.</returns>
        public static TB197_REMES_SOLIC SaveOrUpdate(TB197_REMES_SOLIC entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB197_REMES_SOLIC de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB197_REMES_SOLIC.</returns>
        public static TB197_REMES_SOLIC GetByEntityKey(EntityKey entityKey)
        {
            return (TB197_REMES_SOLIC)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB197_REMES_SOLIC, ordenados pelo Id do aluno "CO_ALU".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB197_REMES_SOLIC de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB197_REMES_SOLIC> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB197_REMES_SOLIC.OrderBy( r => r.CO_ALU ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB197_REMES_SOLIC pelas chaves primárias "CO_ALU", "CO_EMP", "CO_CUR", "CO_SOLI_ATEN", "CO_TIPO_SOLI" e "ID_REMES_SOLIC".
        /// </summary>
        /// <param name="CO_ALU">Id do aluno</param>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="CO_CUR">Id da séria</param>
        /// <param name="CO_SOLI_ATEN">Id da solicitação</param>
        /// <param name="CO_TIPO_SOLI">Id do tipo de solicitação</param>
        /// <param name="ID_REMES_SOLIC">Id auto-incremento</param>
        /// <returns>Entidade TB197_REMES_SOLIC</returns>
        public static TB197_REMES_SOLIC RetornaPelaChavePrimaria(int CO_ALU, int CO_EMP, int CO_CUR, int CO_SOLI_ATEN, int CO_TIPO_SOLI, int ID_REMES_SOLIC)
        {
            return (from tb197 in RetornaTodosRegistros()
                    where tb197.CO_ALU == CO_ALU && tb197.CO_EMP == CO_EMP && tb197.CO_CUR == CO_CUR && tb197.CO_SOLI_ATEN == CO_SOLI_ATEN 
                    && tb197.CO_TIPO_SOLI == CO_TIPO_SOLI && tb197.ID_REMES_SOLIC == ID_REMES_SOLIC
                    select tb197).FirstOrDefault();
        }

        /// <summary>
        /// Retorna todos os registros da entidade TB197_REMES_SOLIC de acordo com os campos "CO_ALU", "CO_EMP", "CO_CUR", "CO_SOLI_ATEN" e "CO_TIPO_SOLI".
        /// </summary>
        /// <param name="CO_ALU">Id do aluno</param>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="CO_CUR">Id da série</param>
        /// <param name="CO_SOLI_ATEN">Id da solicitação</param>
        /// <param name="CO_TIPO_SOLI">Id do tipo de solicitação</param>
        /// <returns>ObjectQuery com todos os registros da entidade TB197_REMES_SOLIC de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB197_REMES_SOLIC> RetornaPeloItemSolicitacao(int CO_ALU, int CO_EMP, int CO_CUR, int CO_SOLI_ATEN, int CO_TIPO_SOLI)
        {
            return (from tb197 in RetornaTodosRegistros()
                    where tb197.CO_ALU == CO_ALU && tb197.CO_EMP == CO_EMP && tb197.CO_CUR == CO_CUR && tb197.CO_SOLI_ATEN == CO_SOLI_ATEN 
                    && tb197.CO_TIPO_SOLI == CO_TIPO_SOLI
                    select tb197).AsObjectQuery();
        }

        #endregion
    }
}