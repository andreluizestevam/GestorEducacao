//---> Inicialização  da Funcionalidade - Chamadas das Bibliotecas Utilizadas
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Text;
using C2BR.GestorEducacao.BusinessEntities.Auxiliares;
using System.Web;

//===> Início das Regras de Negócios
//---> Localização do Arquivo da Funcionalidade no Ambiente da Solução
namespace C2BR.GestorEducacao.BusinessEntities.MSSQL
{
    /// <summary>
    /// Provides a basic data manipulation methods beetween current ObjectContext and Database.
    /// </summary>
    public partial class GestorEntities
    {
        #region Métodos

        /// <summary>
        /// Try to get an entity by specified key, if no one is found return null.
        /// </summary>
        /// <param name="entityKey">The EntityKey to filter.</param>
        /// <returns></returns>
        public static EntityObject GetByEntityKey(EntityKey entityKey)
        {
            object resultObject = null;

            CurrentContext.TryGetObjectByKey(entityKey, out resultObject);

            return (EntityObject)resultObject;
        }

        public static int SaveChanges(RefreshMode refreshMode, object entity)
        {
            try
            {
                return CurrentContext.SaveChanges();
            }
            catch (OptimisticConcurrencyException)
            {
                CurrentContext.Refresh(refreshMode, entity);
                CurrentContext.AcceptAllChanges();
                return CurrentContext.SaveChanges();
            }
        }

        public static void AttachToHandled(EntityObject entity)
        {
            try
            {
                GestorEntities.CurrentContext.AttachTo(entity.GetType().Name, entity);
            }
            catch (Exception)
            {
                // Entidade ja esta no contexto
            }
        }

        /// <summary>
        /// Delete an Entity from database.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        /// <param name="saveChanges">Determine if persist changes to database.</param>
        /// <param name="refreshMode">Specify what method will be used to Optimistic Concurrency cases.</param>
        /// <returns>The number of objects in an Added, Modified, or Deleted state when SaveChanges was called.</returns>
        public static int Delete(object entity, bool saveChanges, RefreshMode refreshMode)
        {
            if (entity != null)
            {
                if (((EntityObject)entity).EntityKey != null)
                {
                    CurrentContext.DeleteObject(entity);

                    if (saveChanges)
                        return SaveChanges(refreshMode, entity);
                }
            }

            return 0;
        }

        /// <summary>
        /// Delete an Entity from database.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        /// <param name="saveChanges">Determine if persist changes to database.</param>
        /// <returns>The number of objects in an Added, Modified, or Deleted state when SaveChanges was called.</returns>
        public static int Delete(object entity, bool saveChanges)
        {
            return Delete(entity, saveChanges, RefreshMode.ClientWins);
        }

        /// <summary>
        /// Delete an Entity from database and persist the deletion to database.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        /// <returns>The number of objects in an Added, Modified, or Deleted state when SaveChanges was called.</returns>
        public static int Delete(object entity)
        {
            return Delete(entity, true);
        }

        /// <summary>
        /// Add an object to Current Context and you may choose to persists or not the changes to database and the refresh mode in case of Optimistic Concurrency.
        /// </summary>
        /// <param name="entity">The entity to be added to the current context.</param>
        /// <param name="saveChanges">Determine if persist changes to database.</param>
        /// <param name="refreshMode">Specify what method will be used to Optimistic Concurrency cases.</param>
        /// <returns>The number of objects in an Added, Modified, or Deleted state when SaveChanges was called.</returns>
        public static int AddObject(object entity, bool saveChanges, RefreshMode refreshMode)
        {
            CurrentContext.AddObject(entity.GetType().Name, entity);

            if (saveChanges)
                return SaveChanges(refreshMode, entity);

            return 0;
        }

        /// <summary>
        /// Add an object to Current Context and you may choose to persists or not the changes to database, it already handles Optimistic Concurrency to Client Wins.
        /// </summary>
        /// <param name="entity">The entity to be added to the current context.</param>
        /// <param name="saveChanges">Determine if persist changes to database.</param>
        /// <param name="refreshMode">Specify what method will be used to Optimistic Concurrency cases.</param>
        /// <returns>The number of objects in an Added, Modified, or Deleted state when SaveChanges was called.</returns>
        public static int AddObject(object entity, bool saveChanges)
        {
            return AddObject(entity, saveChanges, RefreshMode.ClientWins);
        }

        /// <summary>
        /// Add an object to Current Context and persists changes to database, it already handles Optimistic Concurrency to Client Wins.
        /// </summary>
        /// <param name="entity">The entity to be added to the database.</param>
        /// <returns>The number of objects in an Added, Modified, or Deleted state when SaveChanges was called.</returns>
        public static int AddObject(object entity)
        {
            return AddObject(entity, true);
        }

        /// <summary>
        /// Update an entity.
        /// </summary>
        /// <param name="entity">The entity to be updated to the current context</param>
        /// <param name="saveChanges">Determine if persist changes to database.</param>
        /// <param name="refreshMode">Specify what method will be used to Optimistic Concurrency cases.</param>
        /// <returns>The number of objects in an Added, Modified, or Deleted state when SaveChanges was called.</returns>
        public static int Update(EntityObject entity, bool saveChanges, RefreshMode refreshMode)
        {
            var varOriginal = GetByEntityKey(entity.EntityKey);

            if (varOriginal != null)
                CurrentContext.ApplyPropertyChanges(varOriginal.EntityKey.EntitySetName, entity);

            if (saveChanges)
                return SaveChanges(refreshMode, entity);

            return 0;
        }

        /// <summary>
        /// Update an entity.
        /// </summary>
        /// <param name="entity">The entity to be updated to the current context</param>
        /// <param name="saveChanges">Determine if persist changes to database.</param>
        /// <returns>The number of objects in an Added, Modified, or Deleted state when SaveChanges was called.</returns>
        public static int Update(EntityObject entity, bool saveChanges)
        {
            return Update(entity, saveChanges, RefreshMode.ClientWins);
        }

        /// <summary>
        /// Update an Entity and persist the changes to database.
        /// </summary>
        /// <param name="entity">The entity to be updated to the current context.</param>
        /// <returns>The number of objects in an Added, Modified, or Deleted state when SaveChanges was called.</returns>
        public static int Update(EntityObject entity)
        {
            return Update(entity, true);
        }

        /// <summary>
        /// Check the Entity State and determine if it need to be added or updated.
        /// </summary>
        /// <param name="entity">Entity to be Saved/Updated.</param>
        /// <param name="saveChanges">Determine if persist changes to database.</param>
        /// <returns>The number of objects in an Added, Modified, or Deleted state when SaveChanges was called.</returns>
        public static int SaveOrUpdate(EntityObject entity, bool saveChanges)
        {
            int affectedEntities = 0;

            switch (entity.EntityState)
            {
                case EntityState.Added:
                case EntityState.Detached:
                    affectedEntities = AddObject(entity, saveChanges);
                    break;
                case EntityState.Modified:
                    affectedEntities = Update(entity, saveChanges);
                    break;
                case EntityState.Deleted:
                    affectedEntities = Delete(entity, saveChanges);
                    break;
                case EntityState.Unchanged:
                    // Handle type
                    break;
                default:
                    break;
            }

            return affectedEntities;
        }

        /// <summary>
        /// Check the Entity State, determine if it need to be added or updated and persist the changes.
        /// </summary>
        /// <param name="entity">Entity to be Saved/Updated.</param>
        /// <returns>The number of objects in an Added, Modified, or Deleted state when SaveChanges was called.</returns>
        public static int SaveOrUpdate(EntityObject entity)
        {
            return SaveOrUpdate(entity, true);
        }

        #endregion

        #region Construtor Estatico

        static GestorEntities()
        {
            if (HttpContext.Current == null)
                throw new InvalidOperationException("Esse contexto só pode ser utilizado por aplicações Web");

            _lockObj = new object();
        }
        #endregion

        #region Propriedades

        private static object _lockObj;

        public static GestorEntities CurrentContext
        {
            get
            {
                string ctxKey = "ctx_" + HttpContext.Current.GetHashCode().ToString("x");

                lock (_lockObj)
                {
                    if (!HttpContext.Current.Items.Contains(ctxKey))
                    {
                        HttpContext.Current.Items.Add(ctxKey, new GestorEntities());
                    }
                }

                return HttpContext.Current.Items[ctxKey] as GestorEntities;
            }
        }

        #endregion
    }
}