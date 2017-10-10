using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart_v0._0.entity
{
    // это класс для координат в в бинарном дереве
    class LevelXY
    {
        public int level=0;
        public string way;

        public LevelXY(int level, string way) {
            this.level = level;
            this.way = way;
        }
    }
}
