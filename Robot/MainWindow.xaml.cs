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
        int tamanySnake = 100;
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
            TimeSpan velocitat = new TimeSpan(100000);
            timer.Interval = velocitat;
            timer.Start();
            pintaSnake(puntInici);
            posicioActual = puntInici;
            pintaPomes(0);
        }
        private void pintaSnake(Point posicio)
        {

            Ellipse newEllipse = new Ellipse();
            newEllipse.Fill = imatgeRobot;
            newEllipse.Width = tamanySnake;
            newEllipse.Height = tamanySnake;

            Canvas.SetTop(newEllipse, posicio.Y);
            Canvas.SetLeft(newEllipse, posicio.X);

            paintCanvas.Children.Add(newEllipse);

            if (paintCanvas.Children.Count - 1 > llargada)
            {
                paintCanvas.Children.RemoveAt(paintCanvas.Children.Count - (llargada + 1));
            }
        }
        private void pintaPomes(int index)
        {
            Point bonusPoint = new Point(0,0);
            tresor = bonusPoint;

            Ellipse newEllipse = new Ellipse();
            newEllipse.Fill = imatgeTresor;
            newEllipse.Width = tamanySnake;
            newEllipse.Height = tamanySnake;

            Canvas.SetTop(newEllipse, bonusPoint.Y);
            Canvas.SetLeft(newEllipse, bonusPoint.X);
            paintCanvas.Children.Insert(index, newEllipse);
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            // Determina que la snake torni pel costat contrari al que ha sortit.
            if (posicioActual.X < 10 && direccio == (int)DIRECCIO.OEST)
                posicioActual.X = 500;
            else if (posicioActual.X > 480 && direccio == (int)DIRECCIO.EST)
                posicioActual.X = -10;
            else if (posicioActual.Y < 10 && direccio == (int)DIRECCIO.NORD)
                posicioActual.Y = 500;
            else if (posicioActual.Y > 480 && direccio == (int)DIRECCIO.SUD)
                posicioActual.Y = -10;

            Moviment();
            // Mou el cap de la serp en la direccio del moviment.
            Random r = new Random();
            if (r.Next(0, 2) == 1)
            {
                switch (direccio)
                {
                    case (int)DIRECCIO.SUD:
                        posicioActual.Y += 50;
                        pintaSnake(posicioActual);
                        break;
                    case (int)DIRECCIO.NORD:
                        posicioActual.Y -= 50;
                        pintaSnake(posicioActual);
                        break;
                    case (int)DIRECCIO.OEST:
                        posicioActual.X -= 50;
                        pintaSnake(posicioActual);
                        break;
                    case (int)DIRECCIO.EST:
                        posicioActual.X += 50;
                        pintaSnake(posicioActual);
                        break;
                }
                moviments++;
            }
            if ((Math.Abs(tresor.X - posicioActual.X) < tamanySnake) &&
                (Math.Abs(tresor.Y - posicioActual.Y) < tamanySnake))
            {
                GameOver();
            }
        }
        private void Moviment()
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
            MessageBox.Show("Has guanyat! \n" + "Moviments totals: " + moviments);
            this.Close();
        }
    }
}
