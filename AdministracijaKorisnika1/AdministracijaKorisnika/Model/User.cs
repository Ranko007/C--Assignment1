using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministracijaKorisnika.Model
{
    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string UserPass { get; set; }
        public bool IsAdmin { get; set; }
    }
}
