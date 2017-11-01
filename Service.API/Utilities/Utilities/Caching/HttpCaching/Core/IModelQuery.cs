namespace Fantasy.API.Utilities.Caching.HttpCaching.Core
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface IModelQuery<in TModel, out TResult>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        TResult Execute(TModel model);
    }
}