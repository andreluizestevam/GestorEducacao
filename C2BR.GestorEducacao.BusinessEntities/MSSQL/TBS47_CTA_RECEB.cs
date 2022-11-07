using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;

namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class TBS47_CTA_RECEB
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
        /// Exclue o registro da tabela TBS47_CTA_RECEB do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS47_CTA_RECEB entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS47_CTA_RECEB na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS47_CTA_RECEB.</returns>
        public static TBS47_CTA_RECEB Delete(TBS47_CTA_RECEB entity)
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
        public static int SaveOrUpdate(TBS47_CTA_RECEB entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS47_CTA_RECEB na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS47_CTA_RECEB.</returns>
        public static TBS47_CTA_RECEB SaveOrUpdate(TBS47_CTA_RECEB entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS47_CTA_RECEB de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS47_CTA_RECEB.</returns>
        public static TBS47_CTA_RECEB GetByEntityKey(EntityKey entityKey)
        {
            return (TBS47_CTA_RECEB)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS47_CTA_RECEB, ordenados pelo Id da unidade "CO_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBS47_CTA_RECEB de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS47_CTA_RECEB> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS47_CTA_RECEB.OrderBy(c => c.CO_EMP).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TBS47_CTA_RECEB pelas chaves primárias "CO_EMP", "NU_DOC", "NU_PAR" e "DT_CAD_DOC".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="NU_DOC">Número do documento</param>
        /// <param name="NU_PAR">Número da parcela</param>
        /// <param name="DT_CAD_DOC">Data de cadastro</param>
        /// <returns>Entidade TBS47_CTA_RECEB</returns>
        public static TBS47_CTA_RECEB RetornaPelaChavePrimaria(int CO_EMP, string NU_DOC, int NU_PAR, DateTime DT_CAD_DOC)
        {
            return (from tbs47 in RetornaTodosRegistros()
                    where tbs47.CO_EMP == CO_EMP && tbs47.NU_DOC == NU_DOC && tbs47.NU_PAR == NU_PAR &&
                    (
                    tbs47.DT_CAD_DOC.Day == DT_CAD_DOC.Day &&
                    tbs47.DT_CAD_DOC.Month == DT_CAD_DOC.Month &&
                    tbs47.DT_CAD_DOC.Year == DT_CAD_DOC.Year &&
                    tbs47.DT_CAD_DOC.Hour == DT_CAD_DOC.Hour &&
                    tbs47.DT_CAD_DOC.Minute == DT_CAD_DOC.Minute &&
                    tbs47.DT_CAD_DOC.Second == DT_CAD_DOC.Second
                    )
                    select tbs47).OrderByDescending(c => c.DT_CAD_DOC).FirstOrDefault();
        }

        /// <summary>
        /// Retorna o primeiro registro da entidade TBS47_CTA_RECEB pelas chaves primárias "CO_EMP", "NU_DOC" e "NU_PAR" de acordo com a data de cadastro.
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="NU_DOC">Número do documento</param>
        /// <param name="NU_PAR">Número da parcela</param>
        /// <returns>Entidade TBS47_CTA_RECEB</returns>
        public static TBS47_CTA_RECEB RetornaPrimeiroRegistroPeloCoEmpNuDocNuPar(int CO_EMP, string NU_DOC, int NU_PAR)
        {
            return (from tbs47 in RetornaTodosRegistros()
                    where tbs47.CO_EMP == CO_EMP && tbs47.NU_DOC == NU_DOC && tbs47.NU_PAR == NU_PAR
                    select tbs47).OrderByDescending(c => c.DT_CAD_DOC).FirstOrDefault();
        }

        /// <summary>
        /// Retorna um registro da tabela TBS47_CTA_RECEB de acordo com um nosso número registrado na tabela TB045_NOS_NUM
        /// </summary>
        /// <param name="tb045">Entidade TB045_NOS_NUM</param>
        /// <returns></returns>
        public static TBS47_CTA_RECEB RetornaPeloRegistroDeNossoNumero(TB045_NOS_NUM tb045)
        {
            return RetornaPelaChavePrimaria(tb045.CO_EMP, tb045.NU_DOC, tb045.NU_PAR, tb045.DT_CAD_DOC);
        }
        
        /// <summary>
        /// Retorna um registro da tabela TBS47_CTA_RECEB de acordo com um nosso número registrado na tabela TB045_NOS_NUM
        /// </summary>
        public static String RetornaUltimoNumContrato()
        {
            var res = RetornaTodosRegistros().OrderBy(c => c.DT_CAD_DOC).ToList().LastOrDefault();

            return res != null ? res.NU_CONTRATO : "000000";
        }

        #endregion
    }
}
