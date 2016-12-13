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
        static void Main(string[] args)
        {
            Dictionary<String, string> friendsMapping = new Dictionary<string, string>();
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
            }

            Console.WriteLine("Total number of members in Social network is" + friendsMapping.Count);
        }
    }
}
