using Fantasy.API.Service.Mapping.RequestMapping;
using Fantasy.API.Service.Mapping.ResponseMapping;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fantasy.API.Service.Mapping
{
    /// <summary>
    /// 
    /// </summary>
    public class DtoFactories
    {
        private static DtoFactoryRequest _dtoFactoryRequest;
        private static DtoFactoryResponse _dtoFactoryResponse;
        /// <summary>
        /// 
        /// </summary>
        public static DtoFactoryRequest DtoFactoryRequest
        {
            get
            {
                return _dtoFactoryRequest ?? (_dtoFactoryRequest = new DtoFactoryRequest());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static DtoFactoryResponse DtoFactoryResponse
        {
            get
            {
                return _dtoFactoryResponse ?? (_dtoFactoryResponse = new DtoFactoryResponse());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="objects"></param>
        /// <param name="isResponse"></param>
        /// <returns></returns>
        public static async Task<List<TOut>> CreateList<TIn, TOut>(IEnumerable<TIn> objects, bool isResponse = true)
            where TOut : class
            where TIn : class
        {
            if (objects == null) return null;

            var itemOutList = new List<TOut>();

            foreach (var item in objects)
            {
                dynamic itemConverted = item;
                dynamic itemObject = isResponse ? await DtoFactoryResponse.Create(itemConverted) : await DtoFactoryRequest.Create(itemConverted);

                itemOutList.Add(itemObject as TOut);
            }

            //await objects.ForEachAsync(async item =>
            //{
            //    dynamic itemConverted = item;
            //    dynamic itemObject = isResponse ? await DtoFactoryResponse.Create(itemConverted) : await DtoFactoryRequest.Create(itemConverted);

            //    itemOutList.Add(itemObject as TOut);
            //});

            return itemOutList;
        }
    }
}
