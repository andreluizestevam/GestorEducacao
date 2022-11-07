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
    public partial class TBS389_ASSOC_ITENS_PLANE_AGEND
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
        /// Exclue o registro da tabela TBS389_ASSOC_ITENS_PLANE_AGEND do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TBS389_ASSOC_ITENS_PLANE_AGEND entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TBS389_ASSOC_ITENS_PLANE_AGEND na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS389_ASSOC_ITENS_PLANE_AGEND.</returns>
        public static TBS389_ASSOC_ITENS_PLANE_AGEND Delete(TBS389_ASSOC_ITENS_PLANE_AGEND entity)
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
        public static int SaveOrUpdate(TBS389_ASSOC_ITENS_PLANE_AGEND entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TBS389_ASSOC_ITENS_PLANE_AGEND na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS389_ASSOC_ITENS_PLANE_AGEND.</returns>
        public static TBS389_ASSOC_ITENS_PLANE_AGEND SaveOrUpdate(TBS389_ASSOC_ITENS_PLANE_AGEND entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TBS389_ASSOC_ITENS_PLANE_AGEND de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TBS389_ASSOC_ITENS_PLANE_AGEND.</returns>
        public static TBS389_ASSOC_ITENS_PLANE_AGEND GetByEntityKey(EntityKey entityKey)
        {
            return (TBS389_ASSOC_ITENS_PLANE_AGEND)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TBS389_ASSOC_ITENS_PLANE_AGEND, ordenados pelo nome fantasia "NO_FANTAS_EMP".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TBS389_ASSOC_ITENS_PLANE_AGEND de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TBS389_ASSOC_ITENS_PLANE_AGEND> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS389_ASSOC_ITENS_PLANE_AGEND.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TBS389_ASSOC_ITENS_PLANE_AGEND pela chave primária "ID_ASSOC_ITENS_PLANE_AGEND".
        /// </summary>
        /// <param name="ID_AGEND_HORAR">Id da chave primária</param>
        /// <returns>Entidade TBS389_ASSOC_ITENS_PLANE_AGEND</returns>
        public static TBS389_ASSOC_ITENS_PLANE_AGEND RetornaPelaChavePrimaria(int ID_ASSOC_ITENS_PLANE_AGEND)
        {
            return (from tbs389 in RetornaTodosRegistros()
                    where tbs389.ID_ASSOC_ITENS_PLANE_AGEND == ID_ASSOC_ITENS_PLANE_AGEND
                    select tbs389).FirstOrDefault();
        }

        /// <summary>
        /// Retorna lista de associações da agenda e item de planejamento recebidos como parâmetro
        /// </summary>
        /// <param name="ID_AGEND_HORAR"></param>
        /// <param name="ID_ITENS_PLANE_AVALI"></param>
        /// <returns></returns>
        public static List<TBS389_ASSOC_ITENS_PLANE_AGEND> RetornaPelaAgendaEProcedimento(int ID_AGEND_HORAR, int ID_PROC_MEDI_PROCE)
        {
            return (from tbs389 in RetornaTodosRegistros()
                    where tbs389.TBS174_AGEND_HORAR.ID_AGEND_HORAR == ID_AGEND_HORAR
                    && tbs389.TBS386_ITENS_PLANE_AVALI.TBS356_PROC_MEDIC_PROCE.ID_PROC_MEDI_PROCE == ID_PROC_MEDI_PROCE
                    select tbs389).ToList();
        }

        #endregion
    }
}
