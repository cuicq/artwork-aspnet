using Artworks.Infrastructure.Application.Query;
using Artworks.Infrastructure.Application.Query.CommonModel;
using System;
using System.Collections.Generic;
using Artworks.Infrastructure.Application.Domain;
using Artworks.Database.CommonModel;


namespace Artworks.Infrastructure.Application.Persistence
{
    /// <summary>
    /// Represents the base class for repositories.
    /// </summary>
    /// <typeparam name="T">The type of the aggregate root on which the repository operations
    /// should be performed.</typeparam>
    public abstract class Repository<T> : IRepository<T>, IUnitOfWorkRepository where T : class, IAggregateRoot
    {

        #region Private Fields

        private readonly IRepositoryContext context;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <c>Repository&lt;T&gt;</c> class.
        /// </summary>
        /// <param name="context">The repository context being used by the repository.</param>
        public Repository(IRepositoryContext context)
        {
            this.context = context;
        }

        #endregion

        #region IRepository<T> Members

        public IRepositoryContext Context
        {
            get { return this.context; }
        }

        public void Save(T model)
        {
            this.Context.RegisterNew<T>(model, this);
        }

        public void Remove(T model)
        {
            this.Context.RegisterDeleted<T>(model, this);
        }

        public void Change(T model)
        {
            this.Context.RegisterModified<T>(model, this);
        }

        public virtual T FindBy(object id)
        {
            throw new NotImplementedException();
        }

        public virtual T FindBy(QueryObject queryObject)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> FindAll()
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> FindAll(QueryObject queryObject)
        {
            throw new NotImplementedException();
        }

        public virtual PagedResult<T> FindAll(QueryObject queryObject, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Adds an aggregate root to the repository.
        /// </summary>
        /// <param name="model">The model to be added to the repository.</param>
        protected virtual void DoSave(T model)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes the aggregate root from current repository.
        /// </summary>
        /// <param name="model">The model to be removed.</param>
        protected virtual void DoRemove(T model)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Updates the aggregate root in the current repository.
        /// </summary>
        /// <param name="model">The model to be updated.</param>
        protected virtual void DoChange(T model)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IUnitOfWorkRepository Members

        public void PersistCreationOf<TModel>(TModel model)
        {
            this.DoSave(model as T);
        }

        public void PersistUpdateOf<TModel>(TModel model)
        {
            this.DoChange(model as T);
        }

        public void PersistDeletionOf<TModel>(TModel model)
        {
            this.DoRemove(model as T);
        }

        #endregion

    }
}
