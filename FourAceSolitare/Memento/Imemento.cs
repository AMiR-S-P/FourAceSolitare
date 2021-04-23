using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourAceSolitaire.Memento
{
    public interface Imemento
    {
        object State { set; get; }
        public Task<object> GetState();
        public Task<DateTime> GetDate();

    }


}
