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
        public HashSet<int> Calculate(int userid)
        {
            IQueryable<User> Users = dbContext.GetAllUsers();
            var vertices = Users.Select(e => e.UserID).ToArray();
           int counter = 0;
            foreach (var user in Users)
            {
                foreach (var connection in user.UserFollow)
                {
                    counter++;
                }
            }
            var edges = new Tuple<int,int>[counter];
            counter = 0;
            foreach (var user in Users)
            {
                foreach (var connection in user.UserFollow)
                {
                    edges[counter] = Tuple.Create(user.UserID, connection.UserBeingFollowedUserID);
                    counter++;
                }
            }
            var graph = new Graph<int>(vertices, edges);
            var algorithms = new Algorithms();

            return algorithms.BFS(graph, userid);
        }
    }
}