using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playbook.Bussiness.Model
{
    public class DBInformation
    {
        public string Name { get; set; }
        public string ClickHouse { get; set; }
        public string Mongo { get; set; }
        public string Shard { get; set; }
    }
}
