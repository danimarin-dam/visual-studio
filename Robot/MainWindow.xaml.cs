using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Robot
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Point tresor;
        int moviments = 0;
        ImageBrush imatgeRobot = new ImageBrush();
        ImageBrush imatgeTresor = new ImageBrush();
        ImageBrush imgFlecha = new ImageBrush();
        string flechaDireccio = "flechaDreta.png";

        enum DIRECCIO
        {
            NORD = 8,
            SUD = 2,
            OEST = 4,
            EST = 6
        };
        Point puntIniciRobot = new Point(250, 250);
        Point posicioActualRobot = new Point();
        Point puntIniciFlecha = new Point(290, 290);
        Point posicioActualFlecha = new Point();
        int direccio = 2;
        int direccioPrevia = 2;
        int tamanyRobot = 100;
        int llargada = 1;
        Random rnd = new Random();
        public MainWindow()
        {
            InitializeComponent();
            imatgeRobot.ImageSource = new BitmapImage(new Uri("robot.png", UriKind.Relative));
            imatgeTresor.ImageSource = new BitmapImage(new Uri("tresor.png", UriKind.Relative));
            imgFlecha.ImageSource = new BitmapImage(new Uri(flechaDireccio, UriKind.Relative));
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            TimeSpan velocitat = new TimeSpan(1000000);
            timer.Interval = velocitat;
            timer.Start();
            pintaRobot(puntIniciRobot);
            pintaFlecha(puntIniciFlecha);
            posicioActualRobot = puntIniciRobot;
            posicioActualFlecha = puntIniciFlecha;
            pintaTresor(0);
        }
        private void pintaRobot(Point posicio)
        {

            Ellipse robot = new Ellipse();
            robot.Fill = imatgeRobot;
            robot.Width = tamanyRobot;
            robot.Height = tamanyRobot;

            Canvas.SetTop(robot, posicio.Y);
            Canvas.SetLeft(robot, posicio.X);

            paintCanvas.Children.Add(robot);

            if (paintCanvas.Children.Count - 2 > llargada)
            {
                paintCanvas.Children.RemoveAt(paintCanvas.Children.Count - (llargada + 2));
            }
        }
        private void pintaFlecha(Point posicio)
        {

            Ellipse flecha = new Ellipse();
            flecha.Fill = imgFlecha;
            flecha.Width = 20;
            flecha.Height = 20;

            Canvas.SetTop(flecha, posicio.Y);
            Canvas.SetLeft(flecha, posicio.X);

            paintCanvas.Children.Add(flecha);

            if (paintCanvas.Children.Count - 2 > llargada)
            {
                paintCanvas.Children.RemoveAt(paintCanvas.Children.Count - (llargada + 2));
            }
        }
        private void pintaTresor(int index)
        {
            Point puntTresor = new Point(0,0);
            tresor = puntTresor;

            Ellipse tresorElipse = new Ellipse();
            tresorElipse.Fill = imatgeTresor;
            tresorElipse.Width = tamanyRobot;
            tresorElipse.Height = tamanyRobot;

            Canvas.SetTop(tresorElipse, puntTresor.Y);
            Canvas.SetLeft(tresorElipse, puntTresor.X);
            paintCanvas.Children.Insert(index, tresorElipse);
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            // Determina que el robot torni pel costat contrari al que ha sortit.
            if (posicioActualRobot.X < 10 && direccio == (int)DIRECCIO.OEST)
            {
                posicioActualRobot.X = 400;
                posicioActualFlecha.X = 440;
            }
            else if (posicioActualRobot.X > 380 && direccio == (int)DIRECCIO.EST)
            {
                posicioActualRobot.X = -10;
                posicioActualFlecha.X = 30;
            }
            else if (posicioActualRobot.Y < 10 && direccio == (int)DIRECCIO.NORD)
            {
                posicioActualRobot.Y = 400;
                posicioActualFlecha.Y = 440;
            }
            else if (posicioActualRobot.Y > 380 && direccio == (int)DIRECCIO.SUD)
            {
                posicioActualRobot.Y = -10;
                posicioActualFlecha.Y = 30;
            }

            Direccio();
            // Mou el robot en la direccio del moviment.
            Random r = new Random();
            if (r.Next(0, 2) == 1)
            {
                switch (direccio)
                {
                    case (int)DIRECCIO.SUD:
                        posicioActualRobot.Y += 50;
                        posicioActualFlecha.Y += 50;
                        pintaRobot(posicioActualRobot);
                        pintaFlecha(posicioActualFlecha);
                        break;
                    case (int)DIRECCIO.NORD:
                        posicioActualRobot.Y -= 50;
                        posicioActualFlecha.Y -= 50;
                        pintaRobot(posicioActualRobot);
                        pintaFlecha(posicioActualFlecha);
                        break;
                    case (int)DIRECCIO.OEST:
                        posicioActualRobot.X -= 50;
                        posicioActualFlecha.X -= 50;
                        pintaRobot(posicioActualRobot);
                        pintaFlecha(posicioActualFlecha);
                        break;
                    case (int)DIRECCIO.EST:
                        posicioActualRobot.X += 50;
                        posicioActualFlecha.X += 50;
                        pintaRobot(posicioActualRobot);
                        pintaFlecha(posicioActualFlecha);
                        break;
                }
                moviments++;
            }
            if ((Math.Abs(tresor.X - posicioActualRobot.X) < tamanyRobot) &&
                (Math.Abs(tresor.Y - posicioActualRobot.Y) < tamanyRobot))
            {
                GameOver();
            }
        }
        private void Direccio() //Determina de manera random la direccio.
        {
            int moneda;
            Random rnd = new Random();
            moneda = rnd.Next(0, 2);
            while(moneda==0)
            {
                if (direccioPrevia == (int)DIRECCIO.NORD)
                {
                    direccio = (int)DIRECCIO.EST;
                    flechaDireccio = "flechaDreta.png";
                    imgFlecha.ImageSource = new BitmapImage(new Uri(flechaDireccio, UriKind.Relative));

                }

                else if (direccioPrevia == (int)DIRECCIO.EST)
                {
                    direccio = (int)DIRECCIO.SUD;
                    flechaDireccio = "flechaAbaix.png";
                    imgFlecha.ImageSource = new BitmapImage(new Uri(flechaDireccio, UriKind.Relative));
                }
                else if (direccioPrevia == (int)DIRECCIO.SUD)
                {
                    direccio = (int)DIRECCIO.OEST;
                    flechaDireccio = "flechaEsquerra.png";
                    imgFlecha.ImageSource = new BitmapImage(new Uri(flechaDireccio, UriKind.Relative));
                }
                else if (direccioPrevia == (int)DIRECCIO.OEST)
                {
                    direccio = (int)DIRECCIO.NORD;
                    flechaDireccio = "flechaAdalt.png";
                    imgFlecha.ImageSource = new BitmapImage(new Uri(flechaDireccio, UriKind.Relative));
                }
                moneda = rnd.Next(0, 2);
            }
            direccioPrevia = direccio;
        }
        private void GameOver()
        {
            MessageBox.Show("Has guanyat! \n" + "Moviments totals: " + moviments);
            this.Close();
        }
    }
}
