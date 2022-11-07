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
    public partial class TB139_SITU_TAREF_AGEND
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
        /// Exclue o registro da tabela TB139_SITU_TAREF_AGEND do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB139_SITU_TAREF_AGEND entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB139_SITU_TAREF_AGEND na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB139_SITU_TAREF_AGEND.</returns>
        public static TB139_SITU_TAREF_AGEND Delete(TB139_SITU_TAREF_AGEND entity)
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
        public static int SaveOrUpdate(TB139_SITU_TAREF_AGEND entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB139_SITU_TAREF_AGEND na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB139_SITU_TAREF_AGEND.</returns>
        public static TB139_SITU_TAREF_AGEND SaveOrUpdate(TB139_SITU_TAREF_AGEND entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB139_SITU_TAREF_AGEND de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB139_SITU_TAREF_AGEND.</returns>
        public static TB139_SITU_TAREF_AGEND GetByEntityKey(EntityKey entityKey)
        {
            return (TB139_SITU_TAREF_AGEND)GestorEntities.GetByEntityKey(entityKey);
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB139_SITU_TAREF_AGEND, ordenados pelo código "CO_SITU_TAREF_AGEND".
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB139_SITU_TAREF_AGEND de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB139_SITU_TAREF_AGEND> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB139_SITU_TAREF_AGEND.OrderBy( s => s.CO_SITU_TAREF_AGEND ).AsObjectQuery();
        }        

        #endregion

        /// <summary>
        /// Retorna um registro da entidade TB139_SITU_TAREF_AGEND pela chave primária "CO_SITU_TAREF_AGEND".
        /// </summary>
        /// <param name="CO_SITU_TAREF_AGEND">Id da chave primária</param>
        /// <returns>Entidade TB139_SITU_TAREF_AGEND</returns>
        public static TB139_SITU_TAREF_AGEND RetornaPelaChavePrimaria(string CO_SITU_TAREF_AGEND)
        {
            return (from tb139 in RetornaTodosRegistros()
                    where tb139.CO_SITU_TAREF_AGEND == CO_SITU_TAREF_AGEND
                    select tb139).FirstOrDefault();
        }

        #endregion

        #region Propriedades

        /// <summary>
        /// Tarefa Cadastrada
        /// </summary>
        public static TB139_SITU_TAREF_AGEND TC
        {
            get
            {
                return (from s in TB139_SITU_TAREF_AGEND.RetornaTodosRegistros()
                        where s.CO_SITU_TAREF_AGEND.Equals("TC")
                        select s).First();
            }
        }

        /// <summary>
        /// Tarefa Aceita
        /// </summary>
        public static TB139_SITU_TAREF_AGEND TA
        {
            get
            {
                return (from s in TB139_SITU_TAREF_AGEND.RetornaTodosRegistros()
                        where s.CO_SITU_TAREF_AGEND.Equals("TA")
                        select s).First();
            }
        }

        /// <summary>
        /// Em Andamento
        /// </summary>
        public static TB139_SITU_TAREF_AGEND EA
        {
            get
            {
                return (from s in TB139_SITU_TAREF_AGEND.RetornaTodosRegistros()
                        where s.CO_SITU_TAREF_AGEND.Equals("EA")
                        select s).First();
            }
        }

        /// <summary>
        /// Tarefa Repassada
        /// </summary>
        public static TB139_SITU_TAREF_AGEND TR
        {
            get
            {
                return (from s in TB139_SITU_TAREF_AGEND.RetornaTodosRegistros()
                        where s.CO_SITU_TAREF_AGEND.Equals("TR")
                        select s).First();
            }
        }

        /// <summary>
        /// Cancelada pela Origem
        /// </summary>
        public static TB139_SITU_TAREF_AGEND CO
        {
            get
            {
                return (from s in TB139_SITU_TAREF_AGEND.RetornaTodosRegistros()
                        where s.CO_SITU_TAREF_AGEND.Equals("CO")
                        select s).First();
            }
        }

        /// <summary>
        /// Cancelada pelo Responsável
        /// </summary>
        public static TB139_SITU_TAREF_AGEND CR
        {
            get
            {
                return (from s in TB139_SITU_TAREF_AGEND.RetornaTodosRegistros()
                        where s.CO_SITU_TAREF_AGEND.Equals("CR")
                        select s).First();
            }
        }

        /// <summary>
        /// Tarefa Finalizada
        /// </summary>
        public static TB139_SITU_TAREF_AGEND TF
        {
            get
            {
                return (from s in TB139_SITU_TAREF_AGEND.RetornaTodosRegistros()
                        where s.CO_SITU_TAREF_AGEND.Equals("TF")
                        select s).First();
            }
        }

        /// <summary>
        /// Tarefa Reaberta
        /// </summary>
        public static TB139_SITU_TAREF_AGEND TB
        {
            get
            {
                return (from s in TB139_SITU_TAREF_AGEND.RetornaTodosRegistros()
                        where s.CO_SITU_TAREF_AGEND.Equals("TB")
                        select s).First();
            }
        }

        #endregion
    }
}