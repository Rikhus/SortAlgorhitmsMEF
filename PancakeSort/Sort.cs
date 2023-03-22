using Protocol;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PancakeSort
{
    [Export(typeof(ISort))]
    public class PancakeSort: ISort
    {
        [ImportingConstructor]
        public PancakeSort() 
        {
        }

        public string Name => "PancakeSort";

        public string Description => "Алгоритм блинной сортировки";


        public IEnumerator<List<int>> Sort(List<int> array)
        {
            var list = array.ToList();
            for (var subArrayLength = list.Count - 1; subArrayLength >= 0; subArrayLength--)
            {
                //получаем позицию максимального элемента подмассива
                var indexOfMax = IndexOfMax(list, subArrayLength);
                if (indexOfMax != subArrayLength)
                {
                    //переворот массива до индекса максимального элемента
                    //максимальный элемент подмассива расположится вначале
                    Flip(list, indexOfMax);
                    //переворот всего подмассива
                    Flip(list, subArrayLength);
                }
                yield return list;

            }

            yield return list;
        }

        //метод для получения индекса максимального элемента подмассива
        static int IndexOfMax(List<int> array, int n)
        {
            int result = 0;
            for (var i = 1; i <= n; ++i)
            {
                if (array[i] > array[result])
                {
                    result = i;
                }
            }

            return result;
        }

        //метод для переворота массива
        static void Flip(List<int> array, int end)
        {
            for (var start = 0; start < end; start++, end--)
            {
                var temp = array[start];
                array[start] = array[end];
                array[end] = temp;
            }
        }


    }
}
