//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Text;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;
using System.Linq.Expressions;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    public partial class TBS451_INTER_REGIST 
    {            
        #region Métodos

        #region Métodos Básicos

        /// <summary>
        public static int SaveChanges()
        {
            return GestorEntities.CurrentContext.SaveChanges();
        }

        public static int Delete(TBS451_INTER_REGIST entity, bool saveChanges)
        {
            return GestorEntities.Delete(entity, saveChanges);
        }

        public static int DeletePorID(int ID_ITEM)
        {
            var tbs438 = RetornaPelaChavePrimaria(ID_ITEM);
            return GestorEntities.Delete(tbs438);
        }

        public static TBS451_INTER_REGIST Delete(TBS451_INTER_REGIST entity)
        {
            Delete(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static int SaveOrUpdate(TBS451_INTER_REGIST entity, bool saveChanges)
        {
            return GestorEntities.SaveOrUpdate(entity, saveChanges);
        }

        public static TBS451_INTER_REGIST SaveOrUpdate(TBS451_INTER_REGIST entity)
        {
            SaveOrUpdate(entity, true);

            return GetByEntityKey(entity.EntityKey);
        }

        public static TBS451_INTER_REGIST GetByEntityKey(EntityKey entityKey)
        {
            return (TBS451_INTER_REGIST)GestorEntities.GetByEntityKey(entityKey);
        }

        public static ObjectQuery<TBS451_INTER_REGIST> RetornaTodosRegistros()
        {
            return GestorEntities.CurrentContext.TBS451_INTER_REGIST.OrderBy(g => g.ID_INTER_REGIS).AsObjectQuery();
        }

        public static TBS451_INTER_REGIST RetornarUmRegistro(Expression<Func<TBS451_INTER_REGIST, bool>> predicate)
        {
            return GestorEntities.CurrentContext.TBS451_INTER_REGIST.FirstOrDefault(predicate);
        }

        #endregion
        
        /// <summary>
        public static TBS451_INTER_REGIST RetornaPelaChavePrimaria(int ID_INTER_REGIS)
        {
            return GestorEntities.CurrentContext.TBS451_INTER_REGIST.FirstOrDefault(p => p.ID_INTER_REGIS == ID_INTER_REGIS);
        }     
        #endregion
    }
}
