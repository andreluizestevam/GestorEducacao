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
    public partial class TB092_RESER_MEDIC
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
        /// Exclue o registro da tabela TB092_RESER_MEDIC do Contexto e você pode escolher se deve persistir, ou não, as alterações na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <param name="saveChanges">Determina se irá persistir as alterações na base de dados.</param>
        /// <returns>O número de entidades que foi alterada no contexto atual, quando persistido na base de dados.</returns>
        public static int Delete(TB092_RESER_MEDIC entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        /// <summary>
        /// Exclue o registro da tabela TB092_RESER_MEDIC na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB092_RESER_MEDIC.</returns>
        public static TB092_RESER_MEDIC Delete(TB092_RESER_MEDIC entity)
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
        public static int SaveOrUpdate(TB092_RESER_MEDIC entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        /// <summary>
        /// Faz a verificação do estado atual da Entidade e salva/altera/deleta o registro da tabela TB250_OPERA na base de dados.
        /// </summary>
        /// <param name="entity">A entidade na qual será executada a ação.</param>
        /// <returns>A própria entidade TB250_OPERA.</returns>
        public static TB092_RESER_MEDIC SaveOrUpdate(TB092_RESER_MEDIC entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        /// <summary>
        /// Retorna o registro da entidade TB092_RESER_MEDIC de acordo com a chave.
        /// </summary>
        /// <param name="entityKey">A chave da entidade para filtro.</param>
        /// <returns>A própria entidade TB092_RESER_MEDIC.</returns>
        public static TB092_RESER_MEDIC GetByEntityKey(EntityKey entityKey)
        {
            return (TB092_RESER_MEDIC)GestorEntities.GetByEntityKey(entityKey);
        }

        public class Res094
        {
            public string stItem { get; set; }
        }

        public static string RetornaStatus(TB092_RESER_MEDIC entity)
        {
            string status = "";
            int qtItens = 0;
            int qtItensT = 0;
            int qtItensP = 0;

            var res = (from tb094 in GestorEntities.CurrentContext.TB094_ITEM_RESER_MEDIC
                       where tb094.TB092_RESER_MEDIC.ID_RESER_MEDIC == entity.ID_RESER_MEDIC
                       select new Res094
                        {
                            stItem = tb094.ST_ITEM_RESER
                        });

            qtItens = res.Count();

            foreach (Res094 i in res)
            {
                if (i.stItem == "T")
                {
                    qtItensT++;
                }

                if (i.stItem == "P")
                {
                    qtItensP++;
                }
            }

            if (qtItens == qtItensT)
            {
                status = "T";
            }

            if (qtItensT < qtItens || qtItensP == qtItens)
            {
                status = "P";
            }

            if (qtItensT == 0 && qtItensP == 0)
            {
                status = "A";
            }

            return status;
        }

        /// <summary>
        /// Retorna todos os registro da entidade TB092_RESER_MEDIC
        /// </summary>
        /// <returns>ObjectQuery com todos os registros da entidade TB092_RESER_MEDIC de acordo com a filtragem desenvolvida.</returns>
        public static ObjectQuery<TB092_RESER_MEDIC> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TB092_RESER_MEDIC.AsObjectQuery();
        }

        /// <summary>
        /// Retorna um registro da entidade TB092_RESER_MEDIC pelas chaves primárias "CO_PROD" e "CO_EMP".
        /// </summary>
        /// <param name="CO_PROD">Id do produto</param>
        /// <param name="CO_EMP">Id da unidade</param>
        /// <returns>Entidade TB092_RESER_MEDIC</returns>
        public static TB092_RESER_MEDIC RetornaPelaChavePrimaria(int ID_RESER_MEDIC)
        {
            return (from tb092 in RetornaTodosRegistros()
                    where tb092.ID_RESER_MEDIC == ID_RESER_MEDIC
                    select tb092).FirstOrDefault();
        }

        #endregion

    }
}
