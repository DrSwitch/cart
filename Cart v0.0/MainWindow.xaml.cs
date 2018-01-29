using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Forms;
using System.Data.OleDb;

namespace Cart_v0._0
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private Microsoft.Office.Interop.Excel.Application ExcelApp;
        private Microsoft.Office.Interop.Excel.Workbook WorkBookExcel;
        private Microsoft.Office.Interop.Excel.Worksheet WorkSheetExcel;
        private Microsoft.Office.Interop.Excel.Range RangeExcel;

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            DgvColorClear();
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Файл Excel|*.XLSX;*.XLS";
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                string fileName = System.IO.Path.GetFileName(fd.FileName);

                ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                //Книга.
                WorkBookExcel = ExcelApp.Workbooks.Open(fd.FileName);
                //Таблица.
                WorkSheetExcel = (Microsoft.Office.Interop.Excel.Worksheet)WorkBookExcel.Sheets[1];

                var lastCell = WorkSheetExcel.Cells.SpecialCells(Microsoft.Office.Interop.Excel.XlCellType.xlCellTypeLastCell);

                dgv.RowCount = (int)lastCell.Row;
                dgv.ColumnCount = (int)lastCell.Column;

                for (int i = 0; i < (int)lastCell.Column; i++) {
                    for (int j = 0; j < (int)lastCell.Row; j++) {
                        if (j == 0) {
                            dgv.Columns[i].HeaderText = WorkSheetExcel.Cells[j + 1, i + 1].Text.ToString();
                        }
                        else {
                            dgv.Rows[j-1].Cells[i].Value = WorkSheetExcel.Cells[j + 1, i + 1].Text.ToString();
                        }
                    }
                }

                WorkBookExcel.Close(false, Type.Missing, Type.Missing); //закрыл не сохраняя
                ExcelApp.Quit(); // вышел из Excel
                GC.Collect(); // убрал за собой
            }
        }
      
        DataGridView dgv = new DataGridView();
        //это что бы датагридвью добавить из винформс
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Create the interop host control.
            System.Windows.Forms.Integration.WindowsFormsHost host =
                new System.Windows.Forms.Integration.WindowsFormsHost();

            host.Child = dgv;
            dgv.ColumnHeadersVisible = true;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.ReadOnly = true;
            
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.grid1.Children.Add(host);
        }

        List<entity.Header> headers = new List<entity.Header>();
        List<entity.Data> data = new List<entity.Data>();
        List<string> resuts = new List<string>();

        // выделить список заголовков
        private void SelectHeaders_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < dgv.RowCount; i++)
                for (int j = 0; j < dgv.ColumnCount; j++)
                    if (dgv[j, i].Style.BackColor == System.Drawing.Color.LightBlue)
                        dgv[j, i].Style.BackColor = System.Drawing.Color.White; 

            headers = new List<entity.Header>();
            headerstext.Text = "";
            Int32 selectedCellCount = dgv.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                for (int i = selectedCellCount-1; i >= 0 ; i--)
                {
                    dgv[dgv.SelectedCells[i].ColumnIndex, dgv.SelectedCells[i].RowIndex].Style.BackColor = System.Drawing.Color.LightBlue;
                    headers.Add(new entity.Header(dgv[dgv.SelectedCells[i].ColumnIndex, dgv.SelectedCells[i].RowIndex].Value.ToString()));
                    headerstext.Text += dgv[dgv.SelectedCells[i].ColumnIndex, dgv.SelectedCells[i].RowIndex].Value.ToString()+"\n";
                }
            }
        }
        // выделить список результатов
        private void SelectResults_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < dgv.RowCount; i++)
                for (int j = 0; j < dgv.ColumnCount; j++)
                    if (dgv[j, i].Style.BackColor == System.Drawing.Color.LightCyan)
                        dgv[j, i].Style.BackColor = System.Drawing.Color.White;

            resuts = new List<string>();
            resultstext.Text = "";
            Int32 selectedCellCount = dgv.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                for (int i = selectedCellCount - 1; i >= 0; i--)
                {
                    dgv[dgv.SelectedCells[i].ColumnIndex, dgv.SelectedCells[i].RowIndex].Style.BackColor = System.Drawing.Color.LightCyan;
                    resuts.Add(dgv[dgv.SelectedCells[i].ColumnIndex, dgv.SelectedCells[i].RowIndex].Value.ToString());
                    resultstext.Text += dgv[dgv.SelectedCells[i].ColumnIndex, dgv.SelectedCells[i].RowIndex].Value.ToString() + ";\n";
                }
            }
        }
        // выделить список данных
        private void SelectData_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < dgv.RowCount; i++)
                for (int j = 0; j < dgv.ColumnCount; j++)
                    if (dgv[j, i].Style.BackColor == System.Drawing.Color.LightGreen)
                        dgv[j, i].Style.BackColor = System.Drawing.Color.White;

            data = new List<entity.Data>();
            Int32 selectedCellCount = dgv.GetCellCount(DataGridViewElementStates.Selected);
            List<string> hash = new List<string>();

            int x1, y1, x2, y2;
            int z = dgv.SelectedCells.Count;
            x1 = dgv.SelectedCells[z - 1].RowIndex;
            y1 = dgv.SelectedCells[z - 1].ColumnIndex;
            x2 = dgv.SelectedCells[0].RowIndex;
            y2 = dgv.SelectedCells[0].ColumnIndex;
            datatext.Text = "";

            for (int j = y1; j <= y2; j++)
            {
                hash = new List<string>();
                for (int i = x1; i <= x2; i++)
                {
                    hash.Add(dgv.Rows[i].Cells[j].Value.ToString());
                    datatext.Text += dgv.Rows[i].Cells[j].Value.ToString();
                }
                datatext.Text += "\n";
                data.Add(new entity.Data(headers[j], hash));
            }
            //System.Windows.MessageBox.Show("Диапазон выделенных ячеек (строка/столбец): (" 
            //    + x1.ToString() + "," + y1.ToString() + ") : (" 
            //    + x2.ToString() + "," + y2.ToString() + ")" +"\n" 
            //    + datatext.Text);


            //if (selectedCellCount > 0)
            //{
            //    for (int i = selectedCellCount - 1; i >= 0; i--)
            //    {
            //        if (i < selectedCellCount - 1)
            //            if (dgv.SelectedCells[i].ColumnIndex != dgv.SelectedCells[i + 1].ColumnIndex)
            //            {
            //                data.Add(new entity.Data(headers[dgv.SelectedCells[i + 1].ColumnIndex], hash));
            //                hash = new List<string>();
            //                datatext.Text += "\n"+ headers[dgv.SelectedCells[i + 1].ColumnIndex].GetNameHeader() + ";\n";
            //            }
            //        dgv[dgv.SelectedCells[i].ColumnIndex, dgv.SelectedCells[i].RowIndex].Style.BackColor = System.Drawing.Color.LightGreen;
            //        hash.Add(dgv[dgv.SelectedCells[i].ColumnIndex, dgv.SelectedCells[i].RowIndex].Value.ToString());
            //        datatext.Text += dgv[dgv.SelectedCells[i].ColumnIndex, dgv.SelectedCells[i].RowIndex].Value.ToString() + ", ";
            //        if (i == 0)
            //        {
            //            data.Add(new entity.Data(headers[dgv.SelectedCells[i + 1].ColumnIndex], hash));
            //            hash = new List<string>();
            //            datatext.Text += "\n" + headers[dgv.SelectedCells[i + 1].ColumnIndex].GetNameHeader() + ";\n";

            //        }
            //    }
            //}

        }

        List<entity.Node> nodes = new List<entity.Node>();

        private void CreateTree_Click(object sender, RoutedEventArgs e)
        {
            entity.Tree.headers = headers;
            entity.Tree.data = data;
            entity.Tree.resuts = resuts;

            ViewTree vt = new ViewTree();
            vt.ShowDialog();

            //string str = "";
            //for (int i = 0; i < nodes.Count; i++)
            //{
            //    string pr = "";
            //    for (int j = 0; j < nodes[i].way.Length; j++)
            //    {
            //        pr += "  ";
            //    }
            //    str += pr + nodes[i].header.GetNameHeader() + " = " + nodes[i].result + " way = " + nodes[i].way
            //        + "\nРешение = " + nodes[i].decision
            //        + "\n";
            //}
            //System.Windows.MessageBox.Show(str);
        }

        private void DgvColorClear() {
            for (int i = 0; i < dgv.RowCount; i++)
                for (int j = 0; j < dgv.ColumnCount; j++)
                        dgv[j,i].Style.BackColor = System.Drawing.Color.White; //FFADD8E6
        }
    }
}