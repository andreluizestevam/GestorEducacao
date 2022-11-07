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
    public partial class TB_NUCLEO_INST
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
        /// Exclue o registro da tabela TB_NUCLEO_INST do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB_NUCLEO_INST entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB_NUCLEO_INST na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB_NUCLEO_INST.</returns>
        public static TB_NUCLEO_INST Delete(TB_NUCLEO_INST entity)
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
        public static int SaveOrUpdate(TB_NUCLEO_INST entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB_NUCLEO_INST na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB_NUCLEO_INST.</returns>
        public static TB_NUCLEO_INST SaveOrUpdate(TB_NUCLEO_INST entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB_NUCLEO_INST de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB_NUCLEO_INST.</returns>
        public static TB_NUCLEO_INST GetByEntityKey(EntityKey entityKey)
        {
            return (TB_NUCLEO_INST)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB_NUCLEO_INST, ordenados pela descrição "DE_NUCLEO".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB_NUCLEO_INST de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB_NUCLEO_INST> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB_NUCLEO_INST.OrderBy( t => t.DE_NUCLEO ).AsObjectQuery();
        }

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB_NUCLEO_INST onde o Id "CO_NUCLEO" é o informado no parâmetro.
        /// </summary>
        /// <param name="CO_NUCLEO">Id da chave primária</param>
        /// <returns>Entidade TB_NUCLEO_INST</returns>
        public static TB_NUCLEO_INST RetornaPeloCoNucleo(int CO_NUCLEO)
        {
            return (from tbNucInst in RetornaTodosRegistros()
                    where tbNucInst.CO_NUCLEO == CO_NUCLEO
                    select tbNucInst).FirstOrDefault();
        }

        #endregion
    }
}