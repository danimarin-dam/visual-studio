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
        ImageBrush imatgeRobot = new ImageBrush();
        ImageBrush imatgeTresor = new ImageBrush();
        Brush snakeColor = Brushes.Green;
        enum DIRECCIO
        {
            NORD = 8,
            SUD = 2,
            OEST = 4,
            EST = 6
        };
        Point puntInici = new Point(200, 200);
        Point posicioActual = new Point();
        int direccio = 2;
        int direccioPrevia = 2;
        int tamanyRobot = 100;
        int llargada = 1;
        int punts = 0;
        Random rnd = new Random();
        public MainWindow()
        {
            InitializeComponent();
            imatgeRobot.ImageSource = new BitmapImage(new Uri("robot.png", UriKind.Relative));
            imatgeTresor.ImageSource = new BitmapImage(new Uri("tresor.png", UriKind.Relative));
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            TimeSpan velocitat = new TimeSpan(800000);
            timer.Interval = velocitat;
            timer.Start();
            pintaRobot(puntInici);
            posicioActual = puntInici;
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

            if (paintCanvas.Children.Count - 1 > llargada)
            {
                paintCanvas.Children.RemoveAt(paintCanvas.Children.Count - (llargada + 1));
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
            if (posicioActual.X < 10 && direccio == (int)DIRECCIO.OEST)
                posicioActual.X = 500;
            else if (posicioActual.X > 480 && direccio == (int)DIRECCIO.EST)
                posicioActual.X = -10;
            else if (posicioActual.Y < 10 && direccio == (int)DIRECCIO.NORD)
                posicioActual.Y = 500;
            else if (posicioActual.Y > 480 && direccio == (int)DIRECCIO.SUD)
                posicioActual.Y = -10;

            Moviment();
            // Mou el robot en la direccio del moviment.
            Random r = new Random();
            if (r.Next(0, 2) == 1)
            {
                switch (direccio)
                {
                    case (int)DIRECCIO.SUD:
                        posicioActual.Y += 50;
                        pintaRobot(posicioActual);
                        break;
                    case (int)DIRECCIO.NORD:
                        posicioActual.Y -= 50;
                        pintaRobot(posicioActual);
                        break;
                    case (int)DIRECCIO.OEST:
                        posicioActual.X -= 50;
                        pintaRobot(posicioActual);
                        break;
                    case (int)DIRECCIO.EST:
                        posicioActual.X += 50;
                        pintaRobot(posicioActual);
                        break;
                }
            }
            if ((Math.Abs(tresor.X - posicioActual.X) < tamanyRobot) &&
                (Math.Abs(tresor.Y - posicioActual.Y) < tamanyRobot))
            {
                GameOver();
            }
        }
        private void Moviment() //Determina de manera random quan el robot es mou.
        {
            int moneda;
            Random rnd = new Random();
            moneda = rnd.Next(0, 2);
            if(moneda == 1)
            {
                if (direccioPrevia == (int)DIRECCIO.NORD)
                {
                    moneda = rnd.Next(0, 2);
                    if (moneda == 1)
                        direccio = (int)DIRECCIO.EST;
                }

                else if (direccioPrevia == (int)DIRECCIO.EST)
                {
                    moneda = rnd.Next(0, 2);
                    if (moneda == 1)
                        direccio = (int)DIRECCIO.SUD;
                }
                else if (direccioPrevia == (int)DIRECCIO.SUD)
                {
                    moneda = rnd.Next(0, 2);
                    if (moneda == 1)
                        direccio = (int)DIRECCIO.OEST;
                }
                else if (direccioPrevia == (int)DIRECCIO.OEST)
                {
                    moneda = rnd.Next(0, 2);
                    if (moneda == 1)
                        direccio = (int)DIRECCIO.NORD;
                }
            }
            direccioPrevia = direccio;
        }
        private void GameOver()
        {
            MessageBox.Show("Has guanyat!");
            this.Close();
        }
    }
}
