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
        // заголовок отцовского узла
        public Header header;
        // результат отцовского узла
        public string result;
        // результат-решение, т.е. чем заканчивается эта ветка (может быть только у конечной ветки)
        public string decision = "";

        //путь до узла
        public string way;

        public Node(string way, Header header, string result) {
            this.way = way;
            this.header = header;
            this.result = result;
        }
    }
}
