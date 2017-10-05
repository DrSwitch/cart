using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart_v0._0.entity
{
    class Header
    {
        string nameHeader;

        public Header(string name) {
            this.nameHeader = name;
        }

        public void SetNameHeader(string name) {
            this.nameHeader = name;
        }

        public string GetNameHeader() {
            return nameHeader;
        }

    }
}
