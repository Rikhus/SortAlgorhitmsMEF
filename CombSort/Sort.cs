using Protocol;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombSort
{
    [Export(typeof(ISort))]
    public class CombSort: ISort
    {
        [ImportingConstructor]
        public CombSort() 
        {
        }

        public string Name => "CombSort";

        public string Description => "Алгоритм расчески";


        public IEnumerator<List<int>> Sort(List<int> array)
        {
            var list = array.ToList();
            var arrayLength = list.Count;
            var currentStep = arrayLength - 1;

            while (currentStep > 1)
            {
                for (int i = 0; i + currentStep < list.Count; i++)
                {
                    if (list[i] > list[i + currentStep])
                    {
                        var temp = list[i];
                        list[i] = list[i + currentStep];
                        list[i + currentStep] = temp;
                        yield return list;
                    }
                }

                currentStep = GetNextStep(currentStep);
            }

            //сортировка пузырьком
            for (var i = 1; i < arrayLength; i++)
            {
                var swapFlag = false;
                for (var j = 0; j < arrayLength - i; j++)
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

                if (!swapFlag)
                {
                    break;
                }
            }

            yield return list;
        }

        //метод для генерации следующего шага
        static int GetNextStep(int s)
        {
            s = s * 1000 / 1247;
            return s > 1 ? s : 1;
        }


    }
}
