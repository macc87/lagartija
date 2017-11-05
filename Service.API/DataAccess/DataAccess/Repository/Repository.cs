using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Fantasy.API.DataAccess.UnitOfWork;

namespace Fantasy.API.DataAccess.Repository
{

    /// <summary>
    /// Repository Implementation
    /// </summary>
    public sealed class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<TEntity> _dbSet;
        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Context.Set<TEntity>();

        }
        #region IRepository<T> Members

        public async Task<IEnumerable<TEntity>> PagedResultTaskAsync<TResult>(Expression<Func<TEntity, bool>> predicate, int pageNum, int pageSize, Expression<Func<TEntity, TResult>> orderByProperty, bool isAscending)
        {
            return await PagedResultTaskAsync<TResult, Object>(predicate, pageNum, pageSize, orderByProperty, isAscending, null, false);

        }
        public async Task<IEnumerable<TEntity>> PagedResultTaskAsync<TResult, TResult1>(Expression<Func<TEntity, bool>> predicate, int pageNum, int pageSize, Expression<Func<TEntity, TResult>> orderByFirstProperty, bool isFirstAscending, Expression<Func<TEntity, TResult1>> orderBySecondProperty, bool isSecondAscending)
        {
            //var exp = await ConvertExpression( predicate );
            var result = PagedResultInternal(predicate, pageNum, pageSize, orderByFirstProperty, isFirstAscending, orderBySecondProperty, isSecondAscending);
            return await result;

        }

        private async Task<IEnumerable<TEntity>> PagedResultInternal<TResult, TResult1>(Expression<Func<TEntity, bool>> predicate, int pageNum, int pageSize, Expression<Func<TEntity, TResult>> orderByFirstProperty, bool isFirstAscending, Expression<Func<TEntity, TResult1>> orderBySecondProperty, bool isSecondAscending)
        {
            if (pageSize <= 0)
                pageSize = 10;

            var dbset = _unitOfWork.Context.Set<TEntity>();
            var query = dbset.Where(predicate).AsQueryable();

            var rowsCount = query.Count();

            //If page number should be > 0 else set to first page
            if (rowsCount <= pageSize || pageNum <= 0) pageNum = 1;

            //Calculate nunber of rows to skip on pagesize
            var excludedRows = (pageNum - 1) * pageSize;
            if (orderByFirstProperty != null)
            {
                if (isFirstAscending)
                {
                    if (orderBySecondProperty != null)
                    {
                        query = isSecondAscending
                            ? query.OrderBy(orderByFirstProperty).ThenBy(orderBySecondProperty)
                            : query.OrderBy(orderByFirstProperty).ThenByDescending(orderBySecondProperty);

                    }
                    else
                    {
                        query = query.OrderBy(orderByFirstProperty);
                    }

                }
                else
                {
                    if (orderBySecondProperty != null)
                    {
                        query = isSecondAscending
                            ? query.OrderByDescending(orderByFirstProperty).ThenBy(orderBySecondProperty)
                            : query.OrderByDescending(orderByFirstProperty).ThenByDescending(orderBySecondProperty);

                    }
                    else
                    {
                        query = query.OrderByDescending(orderByFirstProperty);
                    }
                }
            }

            var result = query.Skip(excludedRows).Take(pageSize);

            return await Task.FromResult(result);
        }

        /// <summary>
        /// Search for entity records that satisfy the search criteria
        /// </summary> 
        /// <param name="predicate"></param>
        /// <param name="predicatesToInclude">All the navegation properties to include in the result</param>
        /// <returns>
        /// Entity list that satisfies the search criteria
        /// </returns>
        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] predicatesToInclude)
        {

            var predicateRewrited = await ConvertExpression(predicate);
            var result = _dbSet.Where(predicateRewrited);
            if (predicatesToInclude.Any())
            {
                var resultInclude = await IncludeAsync(result, predicatesToInclude);
                return await Task.FromResult(resultInclude.ToList());
            }
            return await Task.FromResult(result.ToList());
        }

        /// <summary>
        /// Get single record from the underlying database
        /// </summary> 
        /// <param name="predicate">
        /// expression used to retrieve single record from db
        /// </param>
        /// <param name="predicatesToInclude"></param>
        /// <returns></returns>
        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] predicatesToInclude)
        {

            var predicateRewrited = await ConvertExpression(predicate);
            var result = _dbSet.Where(predicateRewrited);
            if (predicatesToInclude.Any())
            {
                var resultInclude = await IncludeAsync(result, predicatesToInclude);
                return await Task.FromResult(resultInclude.FirstOrDefault());
            }
            return await Task.FromResult(result.FirstOrDefault());
        }


        /// <summary>
        /// Retrieve all the entity records from the underlying database
        /// </summary> 
        /// <returns>
        /// An IEnumerable   object
        /// </returns>
        public async Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] predicatesToInclude)
        {


            var result = _dbSet;
            if (predicatesToInclude.Any())
            {
                var resultInclude = await IncludeAsync(result, predicatesToInclude);
                return await Task.FromResult(resultInclude.ToList());
            }
            return await Task.FromResult(result.ToList());
        }
        /// <summary>
        /// Add a new entity to the context
        /// </summary> 
        /// <param name="entity">entity</param>
        public async Task AddAsync(TEntity entity)
        {

            _dbSet.Add(entity);
            await Task.FromResult<object>(null);

        }
        /// <summary>
        /// Delete single entity from the context
        /// </summary> 
        /// <param name="entity">Entity</param>
        public async Task DeleteAsync(TEntity entity)
        {

            _dbSet.Remove(entity);
            await Task.FromResult<object>(null);
        }
        /// <summary>
        /// Returns db2 system time
        /// </summary>
        /// <returns></returns>
        public async Task<DateTime> GetSystemTimeAsync()
        {

            DateTime result;
            result = _unitOfWork.Context.ExecuteQuery<DateTime>("SELECT current time FROM sysibm.sysdummy1").Single<DateTime>();

            return await Task.FromResult(result);
        }
        /// <summary>
        /// Returns db2 system time
        /// </summary>
        /// <returns></returns>
        public async Task<DateTime> GetSystemTimeStampAsync()
        {

            DateTime result;
            result = _unitOfWork.Context.ExecuteQuery<DateTime>("SELECT current timestamp FROM sysibm.sysdummy1").Single<DateTime>();
            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<T>> ExecuteQuery<T>(string cmd)
        {
            IEnumerable<T> result;
            result = _unitOfWork.Context.ExecuteQuery<T>(cmd).ToList();
            return await Task.FromResult(result);
        }
        public async Task<T> ExecuteQuerySingleAsync<T>(string cmd)
        {

            T result;
            result = _unitOfWork.Context.ExecuteQuery<T>(cmd).ToList().FirstOrDefault();
            return await Task.FromResult(result);

        }
        public async Task ExecuteAsync(string cmd)
        {
            _unitOfWork.Context.ExecuteCommand(cmd);
            await Task.FromResult<object>(null);
        }

        public async Task ExecuteAsync(string cmd,params object[] parameters)
        {
            var array = parameters.ToArray();
            _unitOfWork.Context.Database.ExecuteSqlCommand(cmd, array);
            await Task.FromResult<object>(null);
        }
        #endregion
        #region IDisposable Members


        /// <summary>
        /// Dispose the DbContext 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        ~Repository()
        {
            if (!_disposed)
            {
                _unitOfWork.Context.Dispose();
            }
        }

        #endregion

        #region Private Members

        private bool _disposed;

        /// <summary>
        /// Dispose the DbContext 
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _unitOfWork.Context.Dispose();
                }
            }
            _disposed = true;
            GC.SuppressFinalize(this);
        }

        private async Task<IQueryable<TEntity>> IncludeAsync(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] predicatesToInclude)
        {

            foreach (var predicateToInclude in predicatesToInclude)
            {
                query = query.Include(predicateToInclude);
            }
            return await Task.FromResult(query);
        }


        /// <summary>
        /// Use only before each Call to Db2 error details in <see cref="http://stackoverflow.com/questions/27930252/ibm-error-sql0417n-with-ef6"/> 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        private async Task<Expression<Func<T, bool>>> ConvertExpression<T>(Expression<Func<T, bool>> expression)
        {
            ExpressionVisitor v = new VisitorPattern();
            Expression<Func<T, bool>> rewrittenPredicate = (Expression<Func<T, bool>>)v.Visit(expression);
            return await Task.FromResult(rewrittenPredicate);
        }



        #endregion


    }
    internal class VisitorPattern : ExpressionVisitor
    {
        protected override Expression VisitBinary(BinaryExpression node)
        {
            var nodeType = node.NodeType;

            if (CheckForMatch(node.Left))
            {
                var expression = node.Right;
                var value = Rewrite(node.Right);
                ConstantExpression filter;
                if (expression.NodeType != ExpressionType.MemberAccess && expression.NodeType != ExpressionType.Constant)
                {
                    if (!(expression is MethodCallExpression))
                    {
                        var generic = expression.Type.GetGenericArguments()[0];
                        var valueConverted = Convert.ChangeType(value, generic);
                        filter = Expression.Constant(valueConverted, expression.Type);
                    }
                    else
                    {
                        var x = expression as MethodCallExpression;
                        object result = Expression.Lambda(x).Compile().DynamicInvoke();
                        filter = Expression.Constant(result, expression.Type);
                    }
                }
                else
                {
                    filter = Expression.Constant(value);
                }
                return Expression.MakeBinary(nodeType, node.Left, filter);
            }
            return Expression.MakeBinary(node.NodeType, Visit(node.Left), Visit(node.Right));
        }

        private bool CheckForMatch(Expression e)
        {
            if (e is MemberExpression || e is MethodCallExpression)
            {

                return true;
            }
            return false;
        }

        private object Rewrite(Expression e)
        {
            var exp = DecodeMemberAccessExpression(e);

            if (exp is MemberExpression)
            {
                MemberExpression me = exp as MemberExpression;
                var value = GetValue(me);
                return value;
            }
            else if (exp is ConstantExpression)
            {
                ConstantExpression me = exp as ConstantExpression;
                if (me !=null)
                {
                    return me.Value;
                }
               
            }
            return null;
        }

        private object GetValue(MemberExpression member)
        {
            var objectMember = Expression.Convert(member, typeof(object));

            var getterLambda = Expression.Lambda<Func<object>>(objectMember);

            Func<object> getter = getterLambda.Compile();

            return getter();
        }
        private static Expression DecodeMemberAccessExpression(Expression expression)
        {

            if (expression.NodeType != ExpressionType.MemberAccess)
            {
                if ((expression.NodeType == ExpressionType.Convert))
                {
                    var unaryExpression = (UnaryExpression)expression;
                    return DecodeMemberAccessExpression(unaryExpression.Operand);
                }

            }
            return expression;
        }
    }

}
