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
    public partial class TBG076_FAMIL_BENEF
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
        /// Exclue o registro da tabela TBG076_FAMIL_BENEF do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBG076_FAMIL_BENEF entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBG076_FAMIL_BENEF na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBG076_FAMIL_BENEF.</returns>
        public static TBG076_FAMIL_BENEF Delete(TBG076_FAMIL_BENEF entity)
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
        public static int SaveOrUpdate(TBG076_FAMIL_BENEF entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBG076_FAMIL_BENEF na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBG076_FAMIL_BENEF.</returns>
        public static TBG076_FAMIL_BENEF SaveOrUpdate(TBG076_FAMIL_BENEF entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBG076_FAMIL_BENEF de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBG076_FAMIL_BENEF.</returns>
        public static TBG076_FAMIL_BENEF GetByEntityKey(EntityKey entityKey)
        {
            return (TBG076_FAMIL_BENEF)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBG076_FAMIL_BENEF.
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBG076_FAMIL_BENEF.</returns>
        public static ObjectQuery<TBG076_FAMIL_BENEF> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBG076_FAMIL_BENEF.AsObjectQuery();
        }
        /// <summary>
        /// Retorna um registro da entidade TBG076_FAMIL_BENEF pela chave primária "ID_ATEST".
        /// </summary>
        /// <param name="ID_ATEST">Id da chave primária</param>
        /// <returns>Entidade TBG076_FAMIL_BENEF</returns>
        public static TBG076_FAMIL_BENEF RetornaPelaChavePrimaria(decimal ID_FAMIL_BENEF)
        {
            return (from tbg076 in RetornaTodosRegistros()
                    where tbg076.ID_FAMIL_BENEF == ID_FAMIL_BENEF
                    select tbg076).FirstOrDefault();
        }      
        #endregion

        #endregion
    }
}