using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using JumpVerse.Views;

namespace JumpVerse.ViewModels
{
    public class MovimientoViewModel : INotifyPropertyChanged
    {
        public ICommand AdelanteCommand { get; set; }
        public ICommand GirarCommand { get; set; }
        public ICommand SaltarCommand { get; set; }
        JumpVerseInicio menuview = new JumpVerseInicio();
        ///Valores Iniciales
        private string iniciotop = "300"; //Valor inicial para Y (segun el plano cartesiano)
        private string inicioleft = "70"; //Valor inicial para x (segun el plano cartesiano)
        private string? top;
        public string? CanvasTop //propiedad a modificar para mover arriba y abajo
        {
            get { return top; }
            set
            {
                top = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CanvasTop"));
            }
        }
        private string left;
        public string CanvasLeft //propiedad a modificar para mover a la izquierda y derecha
        {
            get { return left; }
            set
            {
                left = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CanvasLeft"));
            }
        }
        //Constructor
        public MovimientoViewModel() //ctor
        {
            CanvasTop = iniciotop;
            CanvasLeft = inicioleft;
            AdelanteCommand = new RelayCommand(Avanzar);
            GirarCommand = new RelayCommand<string>(Girar);
            SaltarCommand = new RelayCommand(Saltar);
        }

        private string sourceimage = "/Source/arriba.png"; //Imagen inicial del astronauta

        public string SourceImage //imagen
        {
            get { return sourceimage; }
            set
            {
                sourceimage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SourceImage"));
            }
        }

        int orientacion = 0;
        private void Girar(string rotacion) //punto donde esta viendo el astronauta
        {
            if (byte.Parse(rotacion) == 1)
            {
                orientacion++;
                orientacion = (orientacion >= 4) ? 0 : orientacion;
                if (orientacion == 0)
                    SourceImage = "/Source/arriba.png";
                else if (orientacion == 1)
                    SourceImage = "/Source/derecha.png";
                else if (orientacion == 2)
                    SourceImage = "/Source/abajo.png";
                else
                    SourceImage = "/Source/izquierda.png";
            }
            else
            {
                orientacion--;
                orientacion = (orientacion <= -4) ? 0 : orientacion;
                if (orientacion == 0)
                    SourceImage = "/Source/arriba.png";
                else if (orientacion == -1)
                    SourceImage = "/Source/izquierda.png";
                else if (orientacion == -2)
                    SourceImage = "/Source/abajo.png";
                else
                    SourceImage = "/Source/derecha.png";
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void Avanzar() //Metodo para mover de lugar al astronauta
        {
            string top = CanvasTop;
            string left = CanvasLeft;
            if (orientacion == 0)//Norte
            {
                if (int.Parse(top) <= 300 && int.Parse(top) > 60)
                {
                    var valorreducido = (int.Parse(top) - 80).ToString();
                    CanvasTop = $"{valorreducido}";
                }
                else
                    Reinicio();
            }
            if (orientacion == 1 || orientacion == -3)//Este
            {
                if (int.Parse(CanvasLeft) <= 550 && int.Parse(CanvasLeft) >= 70)
                {
                    var valorreducido = (int.Parse(CanvasLeft) + 80).ToString();
                    CanvasLeft = $"{valorreducido}";
                }
                else
                    Reinicio();
            }
            if (orientacion == 2 || orientacion == -2)//Sur
            {
                if (int.Parse(top) <= 300 && int.Parse(top) >= 60)
                {
                    var valorreducido = (int.Parse(top) + 80).ToString();
                    CanvasTop = $"{valorreducido}";
                }
                else
                    Reinicio();
            }
            if (orientacion == 3 || orientacion == -1)//Oeste
            {
                if (int.Parse(left) <= 630 && int.Parse(left) >= 80)
                {
                    var valorreducido = (int.Parse(CanvasLeft) - 80).ToString();
                    CanvasLeft = $"{valorreducido}";
                }
                else
                    Reinicio();
            }
            Validar();
        }

        private void Validar() //Metodo para checar si se equivocaron
        {
            if(int.Parse(CanvasLeft)==550 && int.Parse(CanvasTop)==60)
                Reinicio();
            if ((int.Parse(CanvasTop) <= 300 && int.Parse(CanvasTop) >= 140) && (int.Parse(CanvasLeft) <= 550) && int.Parse(CanvasLeft) >= 150)
                Reinicio();
            if(int.Parse(CanvasTop)==300 &&int.Parse(CanvasLeft)==630)
                menuview.ShowDialog();
        }

        private void Saltar() //Literalmente eso, imposible confundirse
        {
            if (orientacion == 1 || orientacion == -3)//Este
            {
                if (int.Parse(CanvasTop) == 60 && int.Parse(CanvasLeft) == 470)
                {
                    var valorreducido = (int.Parse(left) + 160).ToString();
                    CanvasLeft = $"{valorreducido}";
                }
            }
        }
        private void Reinicio() //Reiniciar los valores a como estan originalmente
        {
            CanvasTop = iniciotop;
            CanvasLeft = inicioleft;
            SourceImage = "/Source/arriba.png";
            orientacion = 0;
        }
    }
}