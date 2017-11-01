using Fantasy.API.Domain.Mapping.FantasyService;

namespace Assurant.ASP.Api.Domain.Mapping
{
    internal static class ModelFactories
    {
        private static FantasyMapping _modelFactoryFantasy;

        internal static FantasyMapping ModelFactoryFantasy
        {
            get
            {
                return _modelFactoryFantasy ?? (_modelFactoryFantasy = new FantasyMapping());
            }
        }
    }
}