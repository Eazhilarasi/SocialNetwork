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
        static Dictionary<String, string> friendsMapping;
        static void Main(string[] args)
        {             
            Console.WriteLine("Welcome to Social Network :)");
            //Creates a Hash table with each member in the Social network and all friends related to that member.
            CreateFriendsMapping();
            //Calculates the minimum number of levels required from the 
            //source person to reach the destination person on Social network 
            string A = "STACEY_STRIMPLE";
            string B = "RICH_OMLI";
            CalculateTiesBetweenAandB(A,B);
        }

        private static void CreateFriendsMapping()
        {
            friendsMapping = new Dictionary<string, string>();
            //read the data text file line by line into an array
            string[] friends = System.IO.File.ReadAllLines(@"Source\SocialNetwork.txt");
            //store the member as key and friends as value separated by comma.
            foreach (string pair in friends)
            {
                string[] members = pair.Split(',');
                if (friendsMapping.ContainsKey(members[0]))
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
            Console.WriteLine("Total number of members in Social network is " + friendsMapping.Count);
        }

        private static void CalculateTiesBetweenAandB(string source,string destination)
        {
            string A = source;
            string B = destination;
          
            //A queue to maintain the friends to visit to check if B is in their friends list
            Queue<string> friendsToVisit = new Queue<string>();
            //List that holds the freinds visited and their level from the source A.
            List<Node> visitedFriendsLevel = new List<Node>();
            List<string> visitedFriends = new List<string>();

            Console.WriteLine("Crunching data please wait...");

            friendsToVisit.Enqueue(A);
            string[] values = friendsMapping[A].Split(',');
            //initialise counters to calulate the levels traversing to find B
            int presentLevelNode = 1;
            int nextLevelNode = 0;
            int level = 1;
            int count = 0;

            while (friendsToVisit.Count != 0)
            {
                count++;
                string node = friendsToVisit.Peek();
                //A check to not visit a member who is already visited
                while (visitedFriends.Contains(node))
                {
                    friendsToVisit.Dequeue();
                    node = friendsToVisit.Peek();
                }
                if (friendsMapping != null && friendsMapping.ContainsKey(node))
                {
                    values = friendsMapping[node].Split(',');

                    //Check the values if there is B if friend of any
                    foreach (string value in values)
                    {
                        if (value.Equals(B))
                        {
                            friendsToVisit.Clear();
                        }
                        else
                        {
                            friendsToVisit.Enqueue(value);
                            if (!visitedFriends.Contains(value))
                            {
                                // incrementing the counter to keep track of the number of memnbers in the next level.
                                nextLevelNode++;
                            }
                        }
                    }
                    if (friendsToVisit.Count > 0)
                    {
                        //remove the person from queue as they are visited
                        friendsToVisit.Dequeue();
                    }
                    visitedFriends.Add(node);
                    visitedFriendsLevel.Add(new Node(node, level));
                    //Check to see if all the members of the particular levels are visited to increment the level for next members.
                    if (count == presentLevelNode)
                    {
                        level++;
                        count = 0;
                        presentLevelNode = nextLevelNode;
                        nextLevelNode = 0;
                    }
                }
            }
            
            Console.WriteLine("------------------------------");
            foreach (Node member in visitedFriendsLevel)
            {
                Console.WriteLine(member.name + " -> " + member.level);
            }
            Console.WriteLine("------------------------------");
            int numberOfLevel = (visitedFriendsLevel[visitedFriendsLevel.Count-1].level)-1;
            Console.WriteLine("Number of minimum ties required to reach from " + A + " to " + B + " is " + numberOfLevel);
        }
    }
}
