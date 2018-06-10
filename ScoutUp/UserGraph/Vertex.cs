using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutUp
{
    class Vertex
    {
        string vertexid;
        string vertexName;
        float value;
        Vertex nextVertex;
        Edge edgeLink;

        public string Vertexid
        {
            get
            {
                return vertexid;
            }

            set
            {
                vertexid = value;
            }
        }

        internal Vertex NextVertex
        {
            get
            {
                return nextVertex;
            }

            set
            {
                nextVertex = value;
            }
        }

        internal Edge EdgeLink
        {
            get
            {
                return edgeLink;
            }

            set
            {
                edgeLink = value;
            }
        }

        public string VertexName
        {
            get
            {
                return vertexName;
            }

            set
            {
                vertexName = value;
            }
        }

        public float Value
        {
            get
            {
                return value;
            }

            set
            {
                this.value = value;
            }
        }

        public override string ToString()
        {
            return "id : " + this.vertexid +" name : "+ VertexName+" ";
        }
        public Vertex(string vertexid)
        {
            Vertexid = vertexid;
        }
    }
}
