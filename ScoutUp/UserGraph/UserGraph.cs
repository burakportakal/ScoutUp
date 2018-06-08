using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ScoutUp.Classes;
using ScoutUp.DAL;
using ScoutUp.Models;

namespace ScoutUp
{
    public class UserGraph
    {
        private readonly ScoutUpDB dbContext = new ScoutUpDB();
        private Graph userGraph;

        public UserGraph()
        {
            userGraph = new Graph();
            MakeUserGraph();
        }
        public void MakeUserGraph()
        {

            IQueryable<User> Users = dbContext.GetAllUsers();
            
            foreach (var user in Users)
            {
                userGraph.addVertex(user.Id);
            }

            foreach (var user in Users)
            {
                foreach (var connection in user.UserFollow)
                {
                    try
                    {
                        var destUser = Users.FirstOrDefault(e => e.Id == connection.UserBeingFollowedUserId);
                        var weight = WeightCalc(user,destUser);
                        userGraph.addEdge(connection.UserId, connection.UserBeingFollowedUserId, (int)weight);
                    }
                    catch (Exception e)
                    {

                    }
                }
            }

            //var vertex =userGraph.find(1);
            //userGraph.shortestPath(vertex);
            //float[] distance = userGraph.Distance;
            //int[] prev = userGraph.Prev;
        }

        public double WeightCalc(User sourceUser,User destUser)
        {
            int counter = 0;
           string[] listSource= sourceUser.UserFollow.Select(e => e.UserBeingFollowedUserId).ToArray();
           string[] listDest = destUser.UserFollow.Select(e => e.UserBeingFollowedUserId).ToArray();

            var list = listSource.Except(listDest);
            counter = listSource.Length-list.Count();
            return counter;
        }
    }
}