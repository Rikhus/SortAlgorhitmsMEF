using Protocol;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BozoSort
{
    [Export(typeof(ISort))]
    public class BozoSort: ISort
    {
        [ImportingConstructor]
        public BozoSort() 
        {
        }

        public string Name => "BozoSort";

        public string Description => "Алгоритм случайной сортировки";


        public IEnumerator<List<int>> Sort(List<int> array)
        {
            var a = array.ToList();
            while (!IsSorted(a))
            {
                a = RandomPermutation(a);
                yield return a;
            }

            yield return a;
        }

        //метод для проверки упорядоченности массива
        static bool IsSorted(List<int> a)
        {
            for (int i = 0; i < a.Count - 1; i++)
            {
                if (a[i] > a[i + 1])
                    return false;
            }

            return true;
        }

        //перемешивание элементов массива
        static List<int> RandomPermutation(List<int> a)
        {
            Random random = new Random();
            
            var a1 = random.Next(0, a.Count - 1);
            var a2 = random.Next(0, a.Count - 1);

            (a[a1], a[a2]) = (a[a2], a[a1]);

            return a;
        }

    }
}
