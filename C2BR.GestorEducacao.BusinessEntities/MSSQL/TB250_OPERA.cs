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
    public partial class TB250_OPERA
    {
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
        /// Exclue o registro da tabela TB250_OPERA do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB250_OPERA entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB250_OPERA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB250_OPERA.</returns>
        public static TB250_OPERA Delete(TB250_OPERA entity)
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
        public static int SaveOrUpdate(TB250_OPERA entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB250_OPERA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB250_OPERA.</returns>
        public static TB250_OPERA SaveOrUpdate(TB250_OPERA entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB250_OPERA de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB250_OPERA.</returns>
        public static TB250_OPERA GetByEntityKey(EntityKey entityKey)
        {
            return (TB250_OPERA)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB250_OPERA, ordenados pelo nome fantasia "NOM_OPER".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB250_OPERA de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB250_OPERA> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB250_OPERA.OrderBy(o => o.NOM_OPER).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB250_OPERA pela chave primária "ID_OPER".
        /// </summary>
        /// <param name="ID_OPER">Id da chave primária</param>
        /// <returns>Entidade TB250_OPERA</returns>
        public static TB250_OPERA RetornaPelaChavePrimaria(int ID_OPER)
        {
            return (from tb250 in RetornaTodosRegistros()
                    where tb250.ID_OPER == ID_OPER
                    select tb250).FirstOrDefault();
        }
    }
}
