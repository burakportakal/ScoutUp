using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutUp
{
    class Edge
    {
        Vertex vertexLink;
        Edge nextEdge;

        float weight;
        internal Vertex VertexLink
        {
            get
            {
                return vertexLink;
            }

            set
            {
                vertexLink = value;
            }
        }

        internal Edge NextEdge
        {
            get
            {
                return nextEdge;
            }

            set
            {
                nextEdge = value;
            }
        }

        public float Weight
        {
            get
            {
                return weight;
            }

            set
            {
                weight = value;
            }
        }

        public Edge(Vertex vertexlink,float weight)
        {
            VertexLink = vertexlink;
            Weight = weight;
        }
    }
}
