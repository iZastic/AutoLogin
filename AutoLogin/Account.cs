using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLogin
{
    public class Account
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Client { get; set; }
        public bool Multiple { get; set; }
        public int NumberAccounts { get; set; }
        public int SelectedAccount { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
