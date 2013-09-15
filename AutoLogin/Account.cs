using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        public string[] AccountNames { get; set; }
        public int SelectedAccount { get; set; }
        public bool Windowed { get; set; }
        public string Resolution { get; set; }
        public bool LowDetail { get; set; }
        public bool SetRealm { get; set; }
        public string Realm { get; set; }
        public bool SetCharacter { get; set; }
        public int CharacterSlot { get; set; }
        public bool EnterWorld { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
