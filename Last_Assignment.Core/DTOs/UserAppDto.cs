using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Last_Assignment.Core.DTOs
{
    // UserApp e karşılık gelecek

    // Entity leri dış dünyaya açmak istemiyoruz,Client lar entitiyler üzerinde bilgi sahibi olmasın diye sarmallıyoruz,ihtiyaç olan property ler görünsün
    public class UserAppDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string City { get; set; } // bunu biz eklemiştik
    }
}
