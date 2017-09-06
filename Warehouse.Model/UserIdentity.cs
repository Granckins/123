using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Warehouse.Model
{
    public class UserIdentity
    {
        private List<string> roles = new List<string>();

        public string UserId { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        //public int Balance { get; set; }
        //public bool IsBlocked { get; set; }

    }
}
