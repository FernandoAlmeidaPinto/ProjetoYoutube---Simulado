using AutoMapper;

namespace Simulado.Service.ConfigMapper
{
    public class ConfigMapper
    {
        private readonly MapperConfiguration _mapperConfig;
        public ConfigMapper()
        {
            this._mapperConfig = new MapperConfiguration(c => c.AddProfile(new SimuladoProfile()));
            this.AutoMapper = new Mapper(this._mapperConfig);
        }

        public IMapper AutoMapper { get; }
    }
}
