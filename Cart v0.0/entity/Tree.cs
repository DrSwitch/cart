using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart_v0._0.entity
{
    abstract class Tree
    {
        public static List<Node> Nodes { get; set; }
        public static List<entity.Header> headers = new List<entity.Header>();
        public static List<entity.Data> data = new List<entity.Data>();
        public static List<string> resuts = new List<string>();

        public static int GetHeight() {
            int height=0;
            for (int i = 0; i < Nodes.Count-1; i++) {
                if (Nodes[i+1].way.Length > Nodes[i].way.Length) {
                    height = Nodes[i + 1].way.Length;
                }
            }
            return height;
        }

    }
}
