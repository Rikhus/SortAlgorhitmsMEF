using Protocol;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellSort
{
    [Export(typeof(ISort))]
    public class ShellSort: ISort
    {
        [ImportingConstructor]
        public ShellSort() 
        {
        }

        public string Name => "ShellSort";

        public string Description => "Алгоритм Шелла";


        public IEnumerator<List<int>> Sort(List<int> array)
        {
            var list = array.ToList();
            //расстояние между элементами, которые сравниваются
            var d = list.Count / 2;
            while (d >= 1)
            {
                for (var i = d; i < list.Count; i++)
                {
                    var j = i;
                    while ((j >= d) && (list[j - d] > list[j]))
                    {
                        (list[j], list[j - d]) = (list[j - d], list[j]);
                        j = j - d;
                        yield return list;
                    }
                }

                d = d / 2;
            }

            yield return list;
        }

        //метод для обмена элементов
        static void Swap(ref int a, ref int b)
        {
            var t = a;
            a = b;
            b = t;
        }  



    }
}
