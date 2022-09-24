using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using JumpVerse.Views;

namespace JumpVerse.ViewModels
{
    public class MenuViewModels
    {
        public ICommand IniciarCommand { get; set; }
        public MenuViewModels()
        {
            IniciarCommand = new RelayCommand(Iniciar);
        }
        NivelUNO pag = new NivelUNO();
        private void Iniciar()
        {
            pag.ShowDialog();
        }
    }
}