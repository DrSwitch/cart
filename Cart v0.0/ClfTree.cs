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
                // цикл по данным в заголовке нужно дистинкт этих данных сделать
                IEnumerable<string> data = this.data[index].DataInColumn();
                foreach (string dat in data) {
                    minPara.SetZnach(dat);

                }
            }

            MessageBox.Show(str);
        }

        // вычисляем мощность множества, где М это множество (Q,L,R)
        private int Cardinalist(string M) {


            return 0;
        }

        // записываем данные для левой ветки (<)
        private void Left(int idHeader, string znach) {

            leftdata = this.data;
            leftresuts = this.resuts;
            int N=0;
            IEnumerable<string> data = this.data[idHeader].DataInColumn();
            // цикл по данным в заголовке
            foreach (string dat in data) {
                // Первая строка следует за второй в порядке сортировки. OR Первая и вторая строка равны.
                if (String.Compare(dat,znach) >= 0) {
                    // цикл по заголовкам (Header)  кроме последнего, последний это заголовок результатов
                    // тут мы удаляем строку
                    for (int index = 0; index < headers.Count - 1; index++) {
                        // удаляем в столбце определённый элемент 
                        leftdata[index].RemoveAtData(N);
                    }
                    // удаляем результат
                    leftresuts.RemoveAt(N);
                }
                N++;
            }
        }

        // записываем данные для правой ветки (>=)
        private void Right(int idHeader, string znach) {
            rightdata = this.data;
            rightresuts = this.resuts;
            int N = 0;
            IEnumerable<string> data = this.data[idHeader].DataInColumn();
            // цикл по данным в заголовке
            foreach (string dat in data)
            {
                // если первая строка предшествует второй в порядке сортировки.
                if (String.Compare(dat, znach) < 0)
                {
                    // цикл по заголовкам (Header)  кроме последнего, последний это заголовок результатов
                    // тут мы удаляем строку
                    for (int index = 0; index < headers.Count - 1; index++)
                    {
                        // удаляем в столбце определённый элемент 
                        rightdata[index].RemoveAtData(N);
                    }
                    // удаляем результат
                    rightresuts.RemoveAt(N);
                }
                N++;
            }
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
