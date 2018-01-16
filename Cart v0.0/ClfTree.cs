using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace Cart_v0._0
{
    class ClfTree
    {
        List<entity.Header> headers = new List<entity.Header>();
        List<entity.Data> data = new List<entity.Data>();
        List<string> resuts = new List<string>();

        // данные для левой ветки
        List<entity.Data> leftdata = new List<entity.Data>();
        List<string> leftresuts = new List<string>();

        // данные для правой ветки
        List<entity.Data> rightdata = new List<entity.Data>();
        List<string> rightresuts = new List<string>();

        public ClfTree(List<entity.Header> headers, List<entity.Data> data, List<string> resuts)
        {
            this.headers = headers;
            this.data = data;
            this.resuts = resuts;
            CreateTree();

        }

        // строим дерево 
        private void CreateTree() {

            // это пара данных по которым будет делиться дерево (содержит заголовок и значение)
            entityClf.Para minPara = new entityClf.Para();

            string str="";

            int index = 0;
            // цикл по заголовкам (Header)  кроме последнего, последний это заголовок результатов
            for (index = 0; index < headers.Count - 1; index++){
                entity.Header header = headers[index];
                minPara.SetIdHeader(index);
                str += header.GetNameHeader() + "={";
                // цикл по данным в заголовке нужно дистинкт этих данных сделать
                IEnumerable<string> data = this.data[index].DataInColumn();
                foreach (string dat in data) {
                    minPara.SetZnach(dat);
                    str += dat + " ";
                }
                str += "}\n";
            }
            MessageBox.Show(str);
        }

        // вычисляем мощность множества, где М это множество (Q,L,R)
        private int Cardinalist(string M) {


            return 0;
        }

        // записываем данные для левой ветки (>)
        private void Left(int idHeader, string znach) {
            string str = "";


            IEnumerable<string> data = this.data[idHeader].DataInColumn();
            // цикл по данным в заголовке
            foreach (string dat in data) {
                // если первая строка предшествует второй в порядке сортировки.
                if (String.Compare(dat,znach) < 0) {
                    // цикл по заголовкам (Header)  кроме последнего, последний это заголовок результатов
                    for (int index = 0; index < headers.Count - 1; index++) {

                    }
                }
            }


        }

        // записываем данные для правой ветки (<=)
        private void Right(entity.Header header, string znach) {

        }
        //тест ветки
        private List<string> SelectDistinctInColumn(List<string> dataInColumn)
        {

            List<string> hash = new List<string>();
            IEnumerable<string> distinctData = dataInColumn.Distinct();
            foreach (string data in distinctData)
            {
                hash.Add(data);
            }
            return hash;
        }
    }
}
