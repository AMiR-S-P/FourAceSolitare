using FourAceSolitaire.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourAceSolitaire.Memento
{
    public class CareTaker
    {
        Stack<Imemento> mementos = new Stack<Imemento>();

        BaseViewModel ViewModel;

        public CareTaker(BaseViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public async Task Backup()
        {
            //mementos.Push(await ViewModel.Save());
        }

        public async Task Undo()
        {
            if (mementos.Any())
            {
                //await ViewModel.Restore(mementos.Pop());
            }
        }
    }
}
