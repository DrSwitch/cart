using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart_v0._0.entity
{
    class Results
    {
        string nameResult;

        public Results(string name) {
            this.nameResult = name;
        }

        public string GetNameResult() {
            return this.nameResult;
        }
    }
}
