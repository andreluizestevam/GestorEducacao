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
    public partial class TBG151_HISTO_SALAR_RESPO
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
        /// Exclue o registro da tabela TBG151_HISTO_SALAR_RESPO do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBG151_HISTO_SALAR_RESPO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBG151_HISTO_SALAR_RESPO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBG151_HISTO_SALAR_RESPO.</returns>
        public static TBG151_HISTO_SALAR_RESPO Delete(TBG151_HISTO_SALAR_RESPO entity)
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
        public static int SaveOrUpdate(TBG151_HISTO_SALAR_RESPO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBG151_HISTO_SALAR_RESPO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBG151_HISTO_SALAR_RESPO.</returns>
        public static TBG151_HISTO_SALAR_RESPO SaveOrUpdate(TBG151_HISTO_SALAR_RESPO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBG151_HISTO_SALAR_RESPO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBG151_HISTO_SALAR_RESPO.</returns>
        public static TBG151_HISTO_SALAR_RESPO GetByEntityKey(EntityKey entityKey)
        {
            return (TBG151_HISTO_SALAR_RESPO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBG151_HISTO_SALAR_RESPO.
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBG151_HISTO_SALAR_RESPO.</returns>
        public static ObjectQuery<TBG151_HISTO_SALAR_RESPO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBG151_HISTO_SALAR_RESPO.AsObjectQuery();
        }
        /// <summary>
        /// Retorna um registro da entidade TBG151_HISTO_SALAR_RESPO pela chave primária "ID_HISTO_SALAR_RESPO".
        /// </summary>
        /// <param name="ID_HISTO_SALAR_RESPO">Id da chave primária</param>
        /// <returns>Entidade TBG151_HISTO_SALAR_RESPO</returns>
        public static TBG151_HISTO_SALAR_RESPO RetornaPelaChavePrimaria(decimal ID_HISTO_SALAR_RESPO)
        {
            return (from tbg151 in RetornaTodosRegistros()
                    where tbg151.ID_HISTO_SALAR_RESPO == ID_HISTO_SALAR_RESPO
                    select tbg151).FirstOrDefault();
        }
        /// <summary>
        /// Retorna todos os registro da entidade TBG151_HISTO_SALAR_RESPO pelo "CO_RES".
        /// </summary>
        /// <param name="CO_RESP">Id da chave primária</param>
        /// <returns>Entidade TBG151_HISTO_SALAR_RESPO</returns>
        public static ObjectQuery<TBG151_HISTO_SALAR_RESPO> RetornaPeloResponsavel(int CO_RESP)
        {
            return (from tbg151 in RetornaTodosRegistros()
                    where tbg151.TB108_RESPONSAVEL.CO_RESP == CO_RESP
                    select tbg151).OrderByDescending(T => T.ANO_MES).AsObjectQuery();
        }
        #endregion

        #endregion
    }      
}