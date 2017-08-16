using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;

namespace Cart_v0._0
{
    class Excel
    {
        string path = "";
        _Application excel = new _Excel.Application();
        Workbook wb;
        Worksheet ws;

        public Excel(string path, int sheet) {
            this.path = path;
            wb = excel.Workbooks.Open(path);
            ws = wb.Worksheets[sheet];
        }
        
        //public Range LengthRows() {
        //    var lastCell = ws.Cells.SpecialCells(Microsoft.Office.Interop.Excel.XlCellType.xlCellTypeLastCell);
        //    return lastCell.Row;
        //}

        //public int LengthColumns() {
        //    var lastCell = ws.Cells.SpecialCells(Microsoft.Office.Interop.Excel.XlCellType.xlCellTypeLastCell);
        //    return (int)lastCell.Column;
        //}

        public string ReadCell(int i, int j)
        {
            i++;
            j++;

            if (ws.Cells[i, j].value2 != null)
            {
                return ws.Cells[i, j].value2;
            }
            else
            {
                return "";
            }
        }

    }
}
