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
        public bool isVisited { get; set; }

        public Node(string name, bool isVisited)
        {
            this.name = name;
            this.isVisited = isVisited;
        }
    }
}
