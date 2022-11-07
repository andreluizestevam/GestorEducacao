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
    public partial class VW47_CTA_RECEB
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
        /// Exclue o registro da tabela VW47_CTA_RECEB do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(VW47_CTA_RECEB entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela VW47_CTA_RECEB na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade VW47_CTA_RECEB.</returns>
        public static VW47_CTA_RECEB Delete(VW47_CTA_RECEB entity)
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
        public static int SaveOrUpdate(VW47_CTA_RECEB entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela VW47_CTA_RECEB na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade VW47_CTA_RECEB.</returns>
        public static VW47_CTA_RECEB SaveOrUpdate(VW47_CTA_RECEB entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade VW47_CTA_RECEB de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade VW47_CTA_RECEB.</returns>
        public static VW47_CTA_RECEB GetByEntityKey(EntityKey entityKey)
        {
            return (VW47_CTA_RECEB)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade VW47_CTA_RECEB, ordenados pelo Id da unidade "CO_EMP" e pela data de cadastro "DT_CAD_DOC".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade VW47_CTA_RECEB de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<VW47_CTA_RECEB> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.VW47_CTA_RECEB.OrderBy( c => c.CO_EMP ).ThenBy( c => c.DT_CAD_DOC ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade VW47_CTA_RECEB pelas chaves primárias "NU_DOC", "NU_PAR" e "CO_EMP".
        /// </summary>
        /// <param name="NU_DOC">Número do documento</param>
        /// <param name="NU_PAR">Número da parcela</param>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <returns>Entidade VW47_CTA_RECEB</returns>
        public static VW47_CTA_RECEB RetornaPelaChavePrimaria(string NU_DOC, int NU_PAR, int CO_EMP)
        {
            return (from tb47 in VW47_CTA_RECEB.RetornaTodosRegistros()
                    where tb47.NU_DOC == NU_DOC && tb47.NU_PAR == NU_PAR && tb47.CO_EMP == CO_EMP
                    select tb47).OrderByDescending( c => c.DT_CAD_DOC ).FirstOrDefault();
        }

        #endregion
    }
}