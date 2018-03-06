using Artworks.Infrastructure.Application.Domain;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Artworks.Infrastructure.Application.Persistence
{
    /// <summary>
    /// Represents the base class for repository contexts.
    /// </summary>
    public abstract class RepositoryContext : DisposableObject, IRepositoryContext
    {

        #region Private Fields

        private readonly Guid id = Guid.NewGuid();
        private readonly ThreadLocal<Dictionary<Guid, KeyValuePair<AggregateRoot, IUnitOfWorkRepository>>> localNewCollection = new ThreadLocal<Dictionary<Guid, KeyValuePair<AggregateRoot, IUnitOfWorkRepository>>>(() => new Dictionary<Guid, KeyValuePair<AggregateRoot, IUnitOfWorkRepository>>());
        private readonly ThreadLocal<Dictionary<Guid, KeyValuePair<AggregateRoot, IUnitOfWorkRepository>>> localModifiedCollection = new ThreadLocal<Dictionary<Guid, KeyValuePair<AggregateRoot, IUnitOfWorkRepository>>>(() => new Dictionary<Guid, KeyValuePair<AggregateRoot, IUnitOfWorkRepository>>());
        private readonly ThreadLocal<Dictionary<Guid, KeyValuePair<AggregateRoot, IUnitOfWorkRepository>>> localDeletedCollection = new ThreadLocal<Dictionary<Guid, KeyValuePair<AggregateRoot, IUnitOfWorkRepository>>>(() => new Dictionary<Guid, KeyValuePair<AggregateRoot, IUnitOfWorkRepository>>());
        private readonly ThreadLocal<bool> localCommitted = new ThreadLocal<bool>(() => true);
        private readonly object sync = new object();

        #endregion

        #region Ctor

        public RepositoryContext() { }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Clears all the registration in the repository context.
        /// </summary>
        /// <remarks>Note that this can only be called after the repository context has successfully committed.</remarks>
        protected void ClearRegistrations()
        {
            this.localNewCollection.Value.Clear();
            this.localModifiedCollection.Value.Clear();
            this.localDeletedCollection.Value.Clear();
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing">A <see cref="System.Boolean"/> value which indicates whether
        /// the object should be disposed explicitly.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.localCommitted.Dispose();
                this.localDeletedCollection.Dispose();
                this.localModifiedCollection.Dispose();
                this.localNewCollection.Dispose();
            }
        }

        /// <summary>
        /// Do commit 
        /// </summary>
        protected abstract void DoCommit();

        #endregion

        #region Protected Properties

        /// <summary>
        /// Gets an enumerator which iterates over the collection that contains all the objects need to be added to the repository.
        /// </summary>
        protected IEnumerable<KeyValuePair<Guid, KeyValuePair<AggregateRoot, IUnitOfWorkRepository>>> NewCollection
        {
            get { return localNewCollection.Value; }
        }

        /// <summary>
        /// Gets an enumerator which iterates over the collection that contains all the objects need to be modified in the repository.
        /// </summary>
        protected IEnumerable<KeyValuePair<Guid, KeyValuePair<AggregateRoot, IUnitOfWorkRepository>>> ModifiedCollection
        {
            get { return localModifiedCollection.Value; }
        }

        /// <summary>
        /// Gets an enumerator which iterates over the collection that contains all the objects need to be deleted from the repository.
        /// </summary>
        protected IEnumerable<KeyValuePair<Guid, KeyValuePair<AggregateRoot, IUnitOfWorkRepository>>> DeletedCollection
        {
            get { return localDeletedCollection.Value; }
        }

        #endregion

        #region IRepositoryContext Members

        /// <summary>
        /// Gets the ID of the repository context.
        /// </summary>
        public Guid ID
        {
            get { return id; }
        }

        /// <summary>
        /// Registers a new object to the repository context.
        /// </summary>
        /// <typeparam name="TAggregateRoot">The type of the aggregate root.</typeparam>
        /// <param name="obj">The object to be registered.</param>
        /// <param name="unitOfWorkRepository">the object to be unitofwork repository</param>
        public void RegisterNew<TAggregateRoot>(TAggregateRoot obj, IUnitOfWorkRepository unitOfWorkRepository) where TAggregateRoot : class, IAggregateRoot
        {
            if (obj.UniqueID.Equals(Guid.Empty))
                throw new ArgumentException("The ID of the object is empty.", "obj");
            if (localModifiedCollection.Value.ContainsKey(obj.UniqueID))
                throw new InvalidOperationException("The object cannot be registered as a new object since it was marked as modified.");
            if (localNewCollection.Value.ContainsKey(obj.UniqueID))
                throw new InvalidOperationException("The object has already been registered as a new object.");

            var item = new KeyValuePair<AggregateRoot, IUnitOfWorkRepository>(obj as AggregateRoot, unitOfWorkRepository);
            localNewCollection.Value.Add(obj.UniqueID, item);
            localCommitted.Value = false;
        }

        /// <summary>
        /// Registers a modified object to the repository context.
        /// </summary>
        /// <typeparam name="TAggregateRoot">The type of the aggregate root.</typeparam>
        /// <param name="obj">The object to be registered.</param>
        /// <param name="unitOfWorkRepository">the object to be unitofwork repository</param>
        public void RegisterModified<TAggregateRoot>(TAggregateRoot obj, IUnitOfWorkRepository unitOfWorkRepository) where TAggregateRoot : class, IAggregateRoot
        {
            if (obj.UniqueID.Equals(Guid.Empty))
                throw new ArgumentException("The ID of the object is empty.", "obj");
            if (localDeletedCollection.Value.ContainsKey(obj.UniqueID))
                throw new InvalidOperationException("The object cannot be registered as a modified object since it was marked as deleted.");
            if (!localModifiedCollection.Value.ContainsKey(obj.UniqueID) && !localNewCollection.Value.ContainsKey(obj.UniqueID))
            {
                var item = new KeyValuePair<AggregateRoot, IUnitOfWorkRepository>(obj as AggregateRoot, unitOfWorkRepository);
                localModifiedCollection.Value.Add(obj.UniqueID, item);
            }
            localCommitted.Value = false;
        }

        /// <summary>
        /// Registers a deleted object to the repository context.
        /// </summary>
        /// <typeparam name="TAggregateRoot">The type of the aggregate root.</typeparam>
        /// <param name="obj">The object to be registered.</param>
        /// <param name="unitOfWorkRepository">the object to be unitofwork repository</param>
        public void RegisterDeleted<TAggregateRoot>(TAggregateRoot obj, IUnitOfWorkRepository unitOfWorkRepository) where TAggregateRoot : class, IAggregateRoot
        {
            if (obj.UniqueID.Equals(Guid.Empty))
                throw new ArgumentException("The ID of the object is empty.", "obj");
            if (localNewCollection.Value.ContainsKey(obj.UniqueID))
            {
                if (localNewCollection.Value.Remove(obj.UniqueID))
                    return;
            }
            bool removedFromModified = localModifiedCollection.Value.Remove(obj.UniqueID);
            bool addedToDeleted = false;
            if (!localDeletedCollection.Value.ContainsKey(obj.UniqueID))
            {
                var item = new KeyValuePair<AggregateRoot, IUnitOfWorkRepository>(obj as AggregateRoot, unitOfWorkRepository);
                localDeletedCollection.Value.Add(obj.UniqueID, item);
                addedToDeleted = true;
            }
            localCommitted.Value = !(removedFromModified || addedToDeleted);
        }

        #endregion

        #region IUnitOfWork Members

        /// <summary>
        /// 获得一个<see cref="System.Boolean"/>值，该值表示当前的Unit Of Work是否支持Microsoft分布式事务处理机制。
        /// </summary>
        public abstract bool DistributedTransactionSupported { get; }
        /// <summary>
        /// Gets a <see cref="System.Boolean"/> value which indicates whether the UnitOfWork
        /// was committed.
        /// </summary>
        public bool Committed
        {
            get { return localCommitted.Value; }
            protected set { localCommitted.Value = value; }
        }
        /// <summary>
        /// Commits the UnitOfWork.
        /// </summary>
        public virtual void Commit()
        {
            this.DoCommit();
        }
        /// <summary>
        /// Rolls-back the UnitOfWork.
        /// </summary>
        public abstract void Rollback();

        #endregion
    }
}
