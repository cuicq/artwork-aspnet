using System.Collections.Generic;
using System.Linq;
using Artworks.Infrastructure.Application.CommonModel;
using Artworks.Infrastructure.Application.Persistence;
using Artworks.Infrastructure.Application.Query;
using Artworks.Infrastructure.Application.Service.CommonModel;
using Artworks.Infrastructure.Application.Validation;
using Artworks.Infrastructure.Application.Validation.CommonModel;
using Artworks.Infrastructure.Application.Domain;
using Artworks.Infrastructure.Application.CommonModel.Internal;
using Artworks.Infrastructure.Application.Query.CommonModel;

namespace Artworks.Infrastructure.Application.Service
{
    /// <summary>
    /// 应用层服务的抽象类型。（基础CRUD的支持）
    /// </summary>
    public abstract class ApplicationService<T> :
        DisposableObject,
        IApplicationService<T>
        where T : class ,IAggregateRoot
    {

        #region Properties

        /// <summary>
        /// 仓储实例。
        /// </summary>
        protected IRepository<T> Repository { get; private set; }
        /// <summary>
        /// 验证服务实例。
        /// </summary>
        protected IValidationService Validator { get; private set; }
        /// <summary>
        /// 应用程序服务上下文实例。
        /// </summary>
        public IAggregateRootService<T> AggregateRootService { get; private set; }

        #endregion

        #region Ctor

        public ApplicationService(IRepository<T> Repository)
            : this(Repository, null)
        {

        }

        public ApplicationService(IRepository<T> repository, IValidationService validator)
        {
            this.Repository = repository;
            this.Validator = validator;
            this.AggregateRootService = new AggregateRootService<T>(this);
        }

        #endregion

        #region Customer  Methods

        public virtual ResponseResult Create(T model, QueryObject queryObject)
        {
            if (model == null)
                return new ResponseResult { Status = 0, Message = Resource.SERVICE_VALIDATE_OBJECT_TEMPT };

            try
            {
                if (this.Validator != null)
                {
                    ValidationStateDictionary validationState = new ValidationStateDictionary();
                    validationState.Add(typeof(T), this.Validator.Validate(model));

                    if (!validationState.IsValid)
                    {
                        string message = string.Empty;
                        foreach (var item in validationState.SingleOrDefault().Value.Errors)
                        {
                            message = item.Message;
                            break;
                        }
                        return new ResponseResult { Status = 0, Message = message };
                    }
                }

                if (queryObject != null)
                {
                    //验证是否存在相同记录
                    if (this.Repository.FindBy(queryObject) != null)
                    {
                        return new ResponseResult { Status = 0, Message = Resource.SERVICE_OPERATE_FAIL_REPEAT_RECORD };
                    }
                }

                this.Repository.Save(model);
                this.Repository.Context.Commit();

            }
            catch (System.Exception ex)
            {
                this.Repository.Context.Rollback();
                ExceptionHelper.HandleServiceException(ex);
            }

            if (this.Repository.Context.Committed)
            {
                return new ResponseResult { Status = 1, Message = Resource.SERVICE_OPERATE_SUCCEED };
            }

            return new ResponseResult { Status = 0, Message = Resource.SERVICE_OPERATE_FAIL };
        }

        public virtual ResponseResult Update(T model, QueryObject queryObject)
        {
            if (model == null)
                return new ResponseResult { Status = 0, Message = Resource.SERVICE_VALIDATE_OBJECT_TEMPT };

            try
            {
                if (this.Validator != null)
                {

                    ValidationStateDictionary validationState = new ValidationStateDictionary();
                    validationState.Add(typeof(T), this.Validator.Validate(model));

                    if (!validationState.IsValid)
                    {
                        string message = string.Empty;
                        foreach (var item in validationState.SingleOrDefault().Value.Errors)
                        {
                            message = item.Message;
                            break;
                        }
                        return new ResponseResult { Status = 0, Message = message };
                    }
                }

                if (queryObject != null)
                {
                    //验证是否存在相同记录
                    if (this.Repository.FindBy(queryObject) != null)
                    {
                        return new ResponseResult { Status = 0, Message = Resource.SERVICE_OPERATE_FAIL_REPEAT_RECORD };
                    }
                }

                this.Repository.Change(model);
                this.Repository.Context.Commit();

            }
            catch (System.Exception ex)
            {
                this.Repository.Context.Rollback();
                ExceptionHelper.HandleServiceException(ex);
            }

            if (this.Repository.Context.Committed)
            {
                return new ResponseResult { Status = 1, Message = Resource.SERVICE_OPERATE_SUCCEED };
            }

            return new ResponseResult { Status = 0, Message = Resource.SERVICE_OPERATE_FAIL };
        }

        #endregion

        #region IApplicationServiceContext

        public virtual ResponseResult Create(T model)
        {
            return this.Create(model, null);
        }

        public virtual ResponseResult Update(T model)
        {
            return this.Update(model, null);
        }

        public virtual ResponseResult Delete(RequestContext request)
        {
            List<object> data = new List<object>();
            if (request != null && request.Data != null)
            {
                if (request.Data is string[])
                {
                    var list = request.Data as string[];
                    foreach (var item in list)
                    {
                        data.Add(item);
                    }
                }
                else if (request.Data is int[])
                {
                    var list = request.Data as int[];
                    foreach (var item in list)
                    {
                        data.Add(item);
                    }
                }
                else if (request.Data is string || request.Data is int)
                {
                    data.Add(request.Data);
                }
            }
            else
            {
                object id = request.Context.Request["id"];
                if (id != null)
                {
                    data.Add(id);
                }
            }

            if (data.Count > 0)
            {
                try
                {
                    foreach (var id in data)
                    {
                        var model = this.Get(id);
                        if (model != null)
                            this.Repository.Remove(model);
                    }

                    this.Repository.Context.Commit();
                }
                catch (System.Exception ex)
                {
                    this.Repository.Context.Rollback();
                    ExceptionHelper.HandleServiceException(ex);
                }
            }

            if (this.Repository.Context.Committed)
            {
                return new ResponseResult { Status = 1, Message = Resource.SERVICE_OPERATE_SUCCEED };
            }

            return new ResponseResult { Status = 0, Message = Resource.SERVICE_OPERATE_FAIL };
        }

        #endregion

        public virtual T Get(object id)
        {
            return this.Repository.FindBy(id);
        }

        public virtual IEnumerable<T> GetList()
        {
            return this.Repository.FindAll();
        }

        public virtual PagedResult<T> GetList(QueryObject query, int pageIndex, int pageSize)
        {
            return this.Repository.FindAll(query, pageIndex, pageSize);
        }


        #region DisposableObject

        protected override void Dispose(bool disposing)
        {

        }

        #endregion


    }

}
