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
    public partial class TB174_DEPTO_TIPO
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
        /// Exclue o registro da tabela TB_EQUIPE_NUCLEO_INST do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB174_DEPTO_TIPO entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB174_DEPTO_TIPO na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB174_DEPTO_TIPO.</returns>
        public static TB174_DEPTO_TIPO Delete(TB174_DEPTO_TIPO entity)
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
        public static int SaveOrUpdate(TB174_DEPTO_TIPO entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB_EQUIPE_NUCLEO_INST na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TBS433_EXAME_FISIC_ITEM.</returns>
        public static TB174_DEPTO_TIPO SaveOrUpdate(TB174_DEPTO_TIPO entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB174_DEPTO_TIPO de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB174_DEPTO_TIPO.</returns>
        public static TB174_DEPTO_TIPO GetByEntityKey(EntityKey entityKey)
        {
            return (TB174_DEPTO_TIPO)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB174_DEPTO_TIPO, ordenados pelo Id "ID_DEPTO_TIPO".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB_EQUIPE_NUCLEO_INST de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB174_DEPTO_TIPO> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB174_DEPTO_TIPO.OrderBy(t => t.ID_DEPTO_TIPO).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB_EQUIPE_NUCLEO_INST onde o Id "TB174_DEPTO_TIPO" é o informado no parâmetro.
        /// </summary>
        /// <param name="CO_EQUIP_NUCLEO">Id da chave primária</param>
        /// <returns>Entidade TB174_DEPTO_TIPO</returns>
        public static TB174_DEPTO_TIPO RetornaPelaChavePrimaria(int _ID_DEPTO_TIPO)
        {
            return (from TB174 in RetornaTodosRegistros()
                    where TB174.ID_DEPTO_TIPO == _ID_DEPTO_TIPO
                    select TB174).FirstOrDefault();
        }

        #endregion
    }
}