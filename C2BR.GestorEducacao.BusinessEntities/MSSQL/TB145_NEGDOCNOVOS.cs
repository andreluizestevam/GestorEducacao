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
    public partial class TB145_NEGDOCNOVOS
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
        /// Exclue o registro da tabela TB145_NEGDOCNOVOS do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB145_NEGDOCNOVOS entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB145_NEGDOCNOVOS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB145_NEGDOCNOVOS.</returns>
        public static TB145_NEGDOCNOVOS Delete(TB145_NEGDOCNOVOS entity)
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
        public static int SaveOrUpdate(TB145_NEGDOCNOVOS entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB145_NEGDOCNOVOS na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB145_NEGDOCNOVOS.</returns>
        public static TB145_NEGDOCNOVOS SaveOrUpdate(TB145_NEGDOCNOVOS entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB145_NEGDOCNOVOS de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB145_NEGDOCNOVOS.</returns>
        public static TB145_NEGDOCNOVOS GetByEntityKey(EntityKey entityKey)
        {
            return (TB145_NEGDOCNOVOS)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB145_NEGDOCNOVOS, ordenados pelo código "co_negociacao".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB145_NEGDOCNOVOS de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB145_NEGDOCNOVOS> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB145_NEGDOCNOVOS.OrderBy( n => n.co_negociacao ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB145_NEGDOCNOVOS pelas chaves primárias "CO_EMP", "NU_DOC", "NU_PAR", "DT_CAD_DOC" e "CO_NEGOCIACAO".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="NU_DOC">Número do documento</param>
        /// <param name="NU_PAR">Número da parcela</param>
        /// <param name="DT_CAD_DOC">Data da cadastro do documento</param>
        /// <param name="CO_NEGOCIACAO">Código da negociação</param>
        /// <returns>Entidade TB145_NEGDOCNOVOS</returns>
        public static TB145_NEGDOCNOVOS RetornaPelaChavePrimaria(int CO_EMP, string NU_DOC, int NU_PAR, DateTime DT_CAD_DOC, int CO_NEGOCIACAO)
        {
            return (from tb145 in RetornaTodosRegistros()
                    where tb145.CO_UNID == CO_EMP && tb145.NU_DOC == NU_DOC && tb145.NU_PAR == NU_PAR && tb145.DT_CAD_DOC == DT_CAD_DOC 
                    && tb145.co_negociacao == CO_NEGOCIACAO
                    select tb145).FirstOrDefault();
        }

        #endregion
    }
}