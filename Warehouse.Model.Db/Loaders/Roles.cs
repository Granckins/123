using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Warehouse.Model.Db.Loaders
{
    public class Roles
    {
        private static Roles _instance;

        private void InitSources()
        {
            //List = new Mongo<Role, int>(ConfigurationManager.AppSettings["mongo"] ?? @"mongodb://localhost:27017/sfp")
            //    .GetAll().ToDictionary(p => p.Id, p => p.Name);
            List =new Dictionary<int,string>();

        }

        public Dictionary<int, string> List { get; set; }

        private Roles()
        {
            InitSources();
        }

        public void Update()
        {
            InitSources();
        }

        public static Roles GetInstance()
        {
            return _instance ?? (_instance = new Roles());
        }
    }
}
