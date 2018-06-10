using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ScoutUp.Classes;
using ScoutUp.DAL;
using ScoutUp.Models;

namespace ScoutUp.Search
{
    public class UserBfs
    {
        private readonly ScoutUpDB dbContext = new ScoutUpDB();
        public HashSet<string> Calculate(string userid)
        {
            IQueryable<User> Users = dbContext.GetAllUsers();
            var vertices = Users.Select(e => e.Id).ToArray();
           int counter = 0;
            foreach (var user in Users)
            {
                foreach (var connection in user.UserFollow)
                {
                    counter++;
                }
            }
            var edges = new Tuple<string,string>[counter];
            counter = 0;
            foreach (var user in Users)
            {
                foreach (var connection in user.UserFollow)
                {
                    edges[counter] = Tuple.Create(user.Id, connection.UserBeingFollowedUserId);
                    counter++;
                }
            }
            var graph = new Graph<string>(vertices, edges);
            var algorithms = new Algorithms();

            return algorithms.BFS(graph, userid);
        }
    }
}