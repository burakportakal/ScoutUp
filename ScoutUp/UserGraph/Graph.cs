using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutUp
{
    class Graph
    {
        Vertex vertexHead;
        public float[] Distance;
        public int[] Prev;

        public void addVertex(string vertexId)
        {
            if (!search(vertexId))//vertex yoksa
            {
                if (vertexHead == null)
                    vertexHead = new Vertex(vertexId);
                else
                {
                    Vertex iterator = vertexHead;
                    while (iterator.NextVertex != null)
                        iterator = iterator.NextVertex;
                    iterator.NextVertex = new Vertex(vertexId);
                }
            }
        }
        public bool search(string vertexId)//Id'si verilen bir vertex'in graph'ta olup olmadigi bilgisini dondurur
        {
            Vertex iterator = vertexHead;
            while (iterator != null)
            {
                if (iterator.Vertexid == vertexId)
                    return true;
                iterator = iterator.NextVertex;
            }
            return false;
        }
        public void addEdge(string sourceId, string destId,float weight)//destId=>destinationId
        {
            Vertex sourceVertex = find(sourceId);
            Vertex destid2 = find(destId);
            if (sourceVertex != null && search(destId))
            {
                if (sourceVertex.EdgeLink == null)
                    sourceVertex.EdgeLink = new Edge(destid2,weight);
                else
                {
                    Edge iterator = sourceVertex.EdgeLink;
                    while (iterator.NextEdge != null)
                        iterator = iterator.NextEdge;
                    iterator.NextEdge = new Edge(destid2,weight);
                }
            }
            else
            {
                Console.WriteLine("Hata!!! Source veya destination yok.");
            }

        }
        
        public Vertex find(string vertexId)
        {
            Vertex iterator = vertexHead;
            while (iterator != null)
            {
                if (iterator.Vertexid == vertexId)
                    return iterator;
                iterator = iterator.NextVertex;
            }
            return null;
        }
        public float weightBetweenVertexes(Vertex source,Vertex dest)
        {
            Edge iterator = source.EdgeLink;
            while(iterator!=null)
            {
                if (dest.Vertexid == iterator.VertexLink.Vertexid)
                    return iterator.Weight;
                iterator = iterator.NextEdge;
            }
            return 100000;
        }
        public Vertex shortestEdgeFromVertex(Vertex source)
        {
            Edge iterator = source.EdgeLink;
            float sWeight = int.MaxValue;
            Vertex rVertex = null;
            while(iterator !=null)
            {
                if (sWeight > iterator.Weight)
                {
                    sWeight = iterator.Weight;
                    rVertex = iterator.VertexLink;
                }
                iterator = iterator.NextEdge;
            }
            return rVertex;
        }
        public void shortestPath(Vertex source)
        {
            
            List<Vertex> vertexList = new List<Vertex>();//  Q
            float[] distance = new float[10000];
            int[] prev = new int[10000];
            Vertex iterator = vertexHead;
            while(iterator != null)
            {
                //float weight = weightBetweenVertexes(source, iterator);
                //if (source.Vertexid == iterator.Vertexid)
                //{
                //    distance[iterator.Vertexid] = 0;
                //    iterator.Value = 0;
                //}
                //else
                //{
                //    distance[iterator.Vertexid] = weight == 0 ? 100000 : weight;
                //    iterator.Value = weight == 0 ? 100000 : weight;
                //}
                vertexList.Add(iterator);
                iterator = iterator.NextVertex;
            }
            while(vertexList.Count != 0)
            {
             
                Vertex u = (vertexList.OrderBy(x => x.EdgeLink == null ? 100000 : x.Value).ToList())[0];
                vertexList.Remove(u);
                Edge iteratorE = u.EdgeLink;
                while(iteratorE !=null)
                {
                    //float temp = distance[u.Vertexid] + weightBetweenVertexes(u, iteratorE.VertexLink);
                    //if(temp < distance[iteratorE.VertexLink.Vertexid])
                    //{
                    //    var item =vertexList.Find(x => x.Vertexid == iteratorE.VertexLink.Vertexid);
                    //    if(item !=null)
                    //        item.Value = temp;
                    //    distance[iteratorE.VertexLink.Vertexid] = temp;
                    //    prev[iteratorE.VertexLink.Vertexid] = u.Vertexid;   
                        
                    //}
                    iteratorE = iteratorE.NextEdge;
                }
            }

            Distance = distance;
            Prev = prev;
        }
       
       
        public void listele(int id)
        {
            Vertex sourceVertex=null;
            Vertex iteratorV = vertexHead;
            while(iteratorV!=null)
            {
                if(iteratorV.Vertexid.CompareTo(id)==0)
                {
                    sourceVertex = iteratorV;
                }
                iteratorV = iteratorV.NextVertex;
            }
            Edge iterator = sourceVertex.EdgeLink;
            while (iterator != null)
            {
                Console.Write(iterator.VertexLink.Vertexid+" ");
                iterator = iterator.NextEdge;
            }
        }
        public bool heap(int[] dizi)
        {
            for (int i = 1; i <= dizi.Length/2; i++)
            {
                if (2 * i + 1 >= dizi.Length)
                {
                    if (dizi[i] >= dizi[2 * i])
                        continue;
                }
                if (dizi[i] >= dizi[2 * i] && dizi[i] >= dizi[2 * i + 1])
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }
            return true;

        }
       
        public void display()//Vertex ve edgeleri goruntuler
        {
            Console.WriteLine("Graph:");
            Vertex vertexIterator = vertexHead;
            while (vertexIterator != null)
            {
                Console.Write(vertexIterator.Vertexid + ":");
                Edge edgeIterator = vertexIterator.EdgeLink;
                while (edgeIterator != null)
                {
                    Console.Write(edgeIterator.VertexLink.Vertexid + "-");
                    edgeIterator = edgeIterator.NextEdge;
                }

                Console.WriteLine();
                vertexIterator = vertexIterator.NextVertex;
            }
            Console.WriteLine();


        }
    }
}
