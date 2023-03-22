using Protocol;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakerSort
{
    [Export(typeof(ISort))]
    public class ShakerSort: ISort
    {
        [ImportingConstructor]
        public ShakerSort() 
        {
        }

        public string Name => "ShakerSort";

        public string Description => "Алгоритм шейкерной сортировки";


        public IEnumerator<List<int>> Sort(List<int> array)
        {
            var list = array.ToList();
            for (var i = 0; i < list.Count / 2; i++)
            {
                var swapFlag = false;
                //проход слева направо
                for (var j = i; j < list.Count - i - 1; j++)
                {
                    if (list[j] > list[j + 1])
                    {
                        var temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                        swapFlag = true;
                        yield return list;
                    }
                }

                //проход справа налево
                for (var j = list.Count - 2 - i; j > i; j--)
                {
                    if (list[j - 1] > list[j])
                    {
                        var temp = list[j];
                        list[j] = list[j - 1];
                        list[j - 1] = temp;
                        swapFlag = true;
                        yield return list;
                    }
                }

                //если обменов не было выходим
                if (!swapFlag)
                {
                    break;
                }
            }

            yield return list;
        }
    }
}
