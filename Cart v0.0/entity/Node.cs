using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart_v0._0.entity
{
    // это класс для координат в в бинарном дереве
    // что бы определять в каком месте дерева находится эта ветка

    class Node
    {
        public Header header;
        public string result;
        public int level=0;
        public string way;

        public Node(int level, string way, Header header, string result) {
            this.level = level;
            this.way = way;
            this.header = header;
            this.result = result;
        }


    }
}
