using System;
using System.Transactions;
using Artworks.Infrastructure.Application.Domain;
using Artworks.Infrastructure.Application.CommonModel.Internal;

namespace Artworks.Infrastructure.Application.Persistence
{
    /// <summary>
    /// 数据库上下文。
    /// </summary>
    public class DatabaseContext : RepositoryContext
    {

        #region Private Fields

        private readonly Guid id = Guid.NewGuid();
        private readonly object lockObj = new object();

        #endregion

        #region Ctor

        public DatabaseContext()
        {

        }

        #endregion

        #region IUnitOfWork Members

        protected override void DoCommit()
        {

            using (TransactionScope scope = new TransactionScope())
            {
                lock (lockObj)
                {
                    try
                    {

                        foreach (var newObj in this.NewCollection)
                        {
                            var type = newObj.Value;
                            type.Value.PersistCreationOf<AggregateRoot>(type.Key);
                        }

                        foreach (var modifiedObj in this.ModifiedCollection)
                        {
                            var type = modifiedObj.Value;
                            type.Value.PersistUpdateOf<AggregateRoot>(type.Key);
                        }

                        foreach (var delObj in this.DeletedCollection)
                        {

                            /*
                            Type objType = delObj.GetType();

                            PropertyInfo propertyInfo = objType.GetProperty("ID", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                            if (propertyInfo == null)
                                throw new InvalidOperationException("Cannot delete an object which doesn't contain an ID property.");
                            Guid id = (Guid)propertyInfo.GetValue(delObj, null);
                            */

                            var type = delObj.Value;
                            type.Value.PersistDeletionOf<AggregateRoot>(type.Key);
                        }

                        scope.Complete();

                        this.ClearRegistrations();
                        this.Committed = true;

                    }
                    catch (System.Exception ex)
                    {
                        ExceptionHelper.HandleRepositoryException(ex);
                        this.Rollback();
                    }

                }
            }
        }

        public override bool DistributedTransactionSupported
        {
            get { return false; }
        }

        public override void Rollback()
        {
            this.Committed = false;
        }

        #endregion


    }
}
