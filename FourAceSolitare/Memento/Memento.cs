using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourAceSolitaire.Memento
{
    public class Memento : Imemento
    {
        DateTime DateTime;
        public object State { get; set; }


        public Memento(object state)
        {
            State = state;
            DateTime = DateTime.Now;
        }


        public async Task<DateTime> GetDate()
        {
            return await Task.FromResult(DateTime);

        }
        public async Task<object> GetState()
        {
            return await Task.FromResult(State);
        }


    }
}
