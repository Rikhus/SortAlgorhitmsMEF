using Protocol;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BubbleSort
{
    [Export(typeof(ISort))]
    public class BubbleSort: ISort
    {
        [ImportingConstructor]
        public BubbleSort() 
        {
        }

        public string Name => "BubbleSort";

        public string Description => "Алгоритм сортировки пузырьком";


        public IEnumerator<List<int>> Sort(List<int> array)
        {
            var list = array.ToList();
            var len = list.Count;
            for (var i = 1; i < len; i++)
            {
                for (var j = 0; j < len - i; j++)
                {
                    if (list[j] > list[j + 1])
                    {
                        (list[j + 1], list[j]) = (list[j], list[j + 1]);
                        yield return list;
                    }
                }
            }
            yield return list;
        }

    }
}
