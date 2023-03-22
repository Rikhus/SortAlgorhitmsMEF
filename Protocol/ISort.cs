using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol
{
    public interface ISort: IExport
    {
        public IEnumerator<List<int>> Sort(List<int> list);
    }
}
