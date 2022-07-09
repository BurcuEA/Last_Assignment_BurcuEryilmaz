using AutoMapper;

namespace Last_Assignment.Service
{
    // Lazy Loading 
    //Sadece ihtiyaç duyulduğunda memory'e data yüklesin ..

    // AutoMapper a hangi nesnenin hangi nesneye ihtiyaç duyduğunu bilgilendirmemiz gerek ....

    public static class ObjectMapper
    {
        //private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() => BÖYLEYDİ İSTERSEN BÖYLE BIRAK  karar ver !!!
        private static readonly Lazy<IMapper> lazy = new (() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // AutoMapper a DTO daki ayrları bildiriyoruz ... Projeye göre birden fazla da olabilir ..
                cfg.AddProfile<DtoMapper>();
            });
            return config.CreateMapper();
        });

        public static IMapper Mapper => lazy.Value;  // get yerine => lambda kullanıldı ... set e gerek yok ...
    }
}
