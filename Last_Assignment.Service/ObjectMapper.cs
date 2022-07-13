using AutoMapper;

namespace Last_Assignment.Service
{
    // Lazy Loading 
    //Sadece ihtiyaç duyulduğunda memory'e data yüklesin ..

    public static class ObjectMapper
    { 
        private static readonly Lazy<IMapper> lazy = new (() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // Projeye göre birden fazla da olabilir ..
                cfg.AddProfile<DtoMapper>();
            });
            return config.CreateMapper();
        });

        public static IMapper Mapper => lazy.Value;  
    }
}
