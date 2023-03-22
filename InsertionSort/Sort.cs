using Protocol;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsertionSort
{
    [Export(typeof(ISort))]
    public class InsertionSort: ISort
    {
        [ImportingConstructor]
        public InsertionSort() 
        {
        }

        public string Name => "InsertionSort";

        public string Description => "Алгоритм сортировки вставками";


        public IEnumerator<List<int>> Sort(List<int> array)
        {
            var list = array.ToList();
            for (var i = 1; i < list.Count; i++)
            {
                var key = list[i];
                var j = i;
                while ((j > 1) && (list[j - 1] > key))
                {
                    (list[j], list[j-1]) = (list[j-1], list[j]);
                    j--;
                    yield return list;
                }

                list[j] = key;
            }

            yield return list;
        }


    }
}
