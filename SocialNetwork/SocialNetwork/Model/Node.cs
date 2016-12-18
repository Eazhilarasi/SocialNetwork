using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Model
{
    public class Node
    {
        public string name { get; set; }
        public int level { get; set; }

        public Node(string name, int level)
        {
            this.name = name;
            this.level = level;
        }
    }
}
