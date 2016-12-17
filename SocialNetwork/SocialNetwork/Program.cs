using SocialNetwork.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork
{
    class Program
    {
        static Dictionary<String, string> friendsMapping = new Dictionary<string, string>();
        static void Main(string[] args)
        { 
            Console.WriteLine("Hello");
            string[] friends = System.IO.File.ReadAllLines(@"Source\SocialNetwork.txt");
            foreach(string pair in friends)
            {
                string[] members = pair.Split(',');
                if(friendsMapping.ContainsKey(members[0]))
                {
                    string value = friendsMapping[members[0]];
                    value = value + ',' + members[1];
                    friendsMapping[members[0]] = value;
                }
                else
                {
                    friendsMapping[members[0]] = members[1];
                }
                if (friendsMapping.ContainsKey(members[1]))
                {
                    string value = friendsMapping[members[1]];
                    value = value + ',' + members[0];
                    friendsMapping[members[1]] = value;
                }
                else
                {
                    friendsMapping[members[1]] = members[0];
                }
            }

            Console.WriteLine("Total number of members in Social network is" + friendsMapping.Count);
          /*  Console.WriteLine("---------------------------------");
            foreach(KeyValuePair<string,string> value in friendsMapping)
            {
                Console.WriteLine(value.Key + " -> " + value.Value);
            }
            Console.WriteLine("---------------------------------");*/
            // distanceBetween();
            tiesBetweenAandB();
        }

        private static void tiesBetweenAandB()
        {
            string A = "STACEY_STRIMPLE";
            string B = "RICH_OMLI";
            Queue<string> nodesToVisit = new Queue<string>();
            List<Node> visitedNodes = new List<Node>();

            nodesToVisit.Enqueue(A);

            while(nodesToVisit.Count != 0)
            {
                string node = nodesToVisit.Peek();
                string[] values = friendsMapping[node].Split(',');
                //Check the values if there is B
                foreach (string value in values)
                {
                    if (value.Equals(B))
                    {
                        nodesToVisit.Clear();
                    }
                    else
                    {
                        nodesToVisit.Enqueue(value);
                        
                    }
                }
                if (nodesToVisit.Count > 0)
                {
                    nodesToVisit.Dequeue();
                }
                visitedNodes.Add(new Node(node,true));
      
            }
            foreach (Node n in visitedNodes)
            {
                Console.WriteLine(n.name);
            }

        }

        static void distanceBetween()
        {
            string A = "A";
            string B = "B";
            string ties ="";
            //Get values of key A 
            string[] values = friendsMapping[A].Split(',');
            //Check the values if there is B
            foreach(string value in values)
            {
                ties = A;
                if(value.Equals(B))
                {
                    Console.WriteLine(ties + "->" + B);
                    break;
                }
            }
            foreach(string value in values)
            {
                string[] j = friendsMapping[value].Split(',');
                foreach(string i in j)
                {
                    if(i.Equals(B))
                    {
                        ties = ties + i;
                        Console.WriteLine(ties + "->" + B);
                        break;
                    }
                }
                foreach(string i in j)
                {
                    string[] x = friendsMapping[i].Split(',');
                    foreach (string y in x)
                    {
                        if (y.Equals(B))
                        {
                            ties = ties + i + y;
                            Console.WriteLine(ties + "->" + B);
                            break;
                        }
                    }
                }

            }

        }
    }
}
