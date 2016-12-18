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
            string[] friends = System.IO.File.ReadAllLines(@"Source\TextFile1.txt");
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
            Console.WriteLine("---------------------------------");
            foreach(KeyValuePair<string,string> value in friendsMapping)
            {
                Console.WriteLine(value.Key + " -> " + value.Value);
            }
            Console.WriteLine("---------------------------------");
            // distanceBetween();*/
            tiesBetweenAandB();
        }

      /*  private static void tiesBetweenAandB()
        {
           // string A = "STACEY_STRIMPLE";
           // string B = "RICH_OMLI";
            string A = "A";
            string B = "B";
            Queue<string> nodesToVisit = new Queue<string>();
            List<string> visitedNodes = new List<string>();

            nodesToVisit.Enqueue(A);
            int numberOfLevels = 0;
            while(nodesToVisit.Count != 0)
            {
                numberOfLevels += 1;
                string node = nodesToVisit.Peek();
                while(visitedNodes.Contains(node))
                {
                    nodesToVisit.Dequeue();
                    node = nodesToVisit.Peek();
                }
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
                visitedNodes.Add(node);
      
            }
            foreach (string n in visitedNodes)
            {
                Console.WriteLine(n);
            }
            Console.WriteLine("no of ties is " + numberOfLevels);

        }*/

        private static void tiesBetweenAandB()
        {
            // string A = "STACEY_STRIMPLE";
            // string B = "RICH_OMLI";
            string A = "A";
            string B = "B";
            Queue<string> nodesToVisit = new Queue<string>();
            List<string> visitedNodes = new List<string>();
            List<Node> visitedNodesLevel = new List<Node>();
            nodesToVisit.Enqueue(A);
            string[] values = friendsMapping[A].Split(',');
            //initialise counters
            int presentLevelNode = 1;
            // int nextLevelNode = values.Length;
            int nextLevelNode = 0;
            int level = 1;
            int count = 0;

            while (nodesToVisit.Count != 0)
            {
                count++;
                string node = nodesToVisit.Peek();
                while (visitedNodes.Contains(node))
                {
                    nodesToVisit.Dequeue();
                    node = nodesToVisit.Peek();
                }
                values = friendsMapping[node].Split(',');

                int x = 0;
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
                        if (!visitedNodes.Contains(value))
                        {
                            nextLevelNode++;
                        }
                    }
                }
                if (nodesToVisit.Count > 0)
                {
                    nodesToVisit.Dequeue();
                }
                visitedNodes.Add(node);
                visitedNodesLevel.Add(new Node(node, level));
                if (count == presentLevelNode)
                {
                    level++;
                    count = 0;
                    presentLevelNode = nextLevelNode;
                    nextLevelNode = 0;
                }
            }
            foreach (string n in visitedNodes)
            {
                Console.WriteLine(n);
            }
            Console.WriteLine("------------");
            foreach (Node n in visitedNodesLevel)
            {
                Console.WriteLine(n.name + " -> " + n.level);
            }

            //   Console.WriteLine("no of ties is " + numberOfLevels);
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
