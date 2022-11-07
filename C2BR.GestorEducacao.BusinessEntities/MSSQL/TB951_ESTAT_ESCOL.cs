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
    public partial class TB951_ESTAT_ESCOL
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
        /// Exclue o registro da tabela TB951_ESTAT_ESCOL do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB951_ESTAT_ESCOL entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB951_ESTAT_ESCOL na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB951_ESTAT_ESCOL.</returns>
        public static TB951_ESTAT_ESCOL Delete(TB951_ESTAT_ESCOL entity)
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
        public static int SaveOrUpdate(TB951_ESTAT_ESCOL entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB951_ESTAT_ESCOL na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB951_ESTAT_ESCOL.</returns>
        public static TB951_ESTAT_ESCOL SaveOrUpdate(TB951_ESTAT_ESCOL entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB951_ESTAT_ESCOL de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB951_ESTAT_ESCOL.</returns>
        public static TB951_ESTAT_ESCOL GetByEntityKey(EntityKey entityKey)
        {
            return (TB951_ESTAT_ESCOL)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB951_ESTAT_ESCOL, ordenados pelo Id "ID_ESTAT_ESCOL".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB951_ESTAT_ESCOL de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB951_ESTAT_ESCOL> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB951_ESTAT_ESCOL.OrderBy( e => e.ID_ESTAT_ESCOL ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB951_ESTAT_ESCOL pela chave primária "ID_ESTAT_ESCOL".
        /// </summary>
        /// <param name="ID_ESTAT_ESCOL">Id da chave primária</param>
        /// <returns>Entidade TB951_ESTAT_ESCOL</returns>
        public static TB951_ESTAT_ESCOL RetornaPelaChavePrimaria(int ID_ESTAT_ESCOL)
        {
            return (from tb951 in RetornaTodosRegistros()
                    where tb951.ID_ESTAT_ESCOL == ID_ESTAT_ESCOL
                    select tb951).FirstOrDefault();
        }

        /// <summary>
        /// Retorna o primeiro registro da entidade TB951_ESTAT_ESCOL pela unidade "CO_EMP" e ano de referência "CO_ANO_REF".
        /// </summary>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <param name="CO_ANO_REF">Ano de referência</param>
        /// <returns>Entidade TB951_ESTAT_ESCOL</returns>
        public static TB951_ESTAT_ESCOL RetornaPeloOcorrEstatEscol(int CO_EMP, int CO_ANO_REF)
        {
            return (from tb951 in RetornaTodosRegistros()
                    where tb951.TB25_EMPRESA.CO_EMP == CO_EMP && tb951.CO_ANO_REF == CO_ANO_REF
                    select tb951).FirstOrDefault();
        }

        #endregion
    }
}