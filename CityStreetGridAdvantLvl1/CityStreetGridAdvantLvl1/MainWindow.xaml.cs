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
using System.IO;


namespace CityStreetGridAdvantLvl1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> movementList = new List<string>();

        public enum Directon
        {
            North, 
            East, 
            West, 
            South
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFile(movementList);
        }

        private void CalculateDirections(List<string> movementList)
        {
            int distance = 0;
            int x = 0;
            int y = 0;

            Directon oldDir = Directon.North;
            Directon newDir;

            Calculations calc = new Calculations();

            foreach(string move in movementList)
            {
                newDir = DetermineDirection(oldDir, Convert.ToChar(move.Substring(0,1))); // Returns direction facing (N,E,W,S)

                x = 0;
                y = 0;

                switch(newDir)
                {
                    case Directon.North:
                            y += Convert.ToInt32(move.Substring(1));
                            calc.AddLocationY(y);
                        break;

                    case Directon.East :
                            x -= Convert.ToInt32(move.Substring(1));
                            calc.AddLocationX(x);
                        break;

                    case Directon.West :
                            x += Convert.ToInt32(move.Substring(1));
                            calc.AddLocationX(x);
                        break;

                    case Directon.South :
                            y -= Convert.ToInt32(move.Substring(1));
                            calc.AddLocationY(y);
                        break;
                    default :
                        break;
                }

                
                oldDir = newDir;
            }

            
            distance = Math.Abs(x) + Math.Abs(y);
            tbX.Text = x.ToString();
            tbY.Text = y.ToString();
            tbDistance.Text = distance.ToString();
        }

        private Directon DetermineDirection(Directon oldDirection, char direction)
        {
            Directon directionFacing = Directon.North;
            
            if (oldDirection == Directon.North)
            {
                if (direction == 'R')
                    directionFacing = Directon.West;
                else if (direction == 'L')
                    directionFacing = Directon.East;
            }

            if (oldDirection == Directon.East)
            {
                if (direction == 'R')
                    directionFacing = Directon.North;
                else if (direction == 'L')
                    directionFacing = Directon.South;
            }

            if (oldDirection == Directon.West)
            {
                if (direction == 'R')
                    directionFacing = Directon.South;
                else if (direction == 'L')
                    directionFacing = Directon.North;
            }

            if (oldDirection == Directon.South)
            {
                if (direction == 'R')
                    directionFacing = Directon.East;
                else if (direction == 'L')
                    directionFacing = Directon.West;
            }

            return directionFacing;            
        }

        private void OpenFile(List<string> movements)
        {
            StreamReader sr = null;

            try
            {
                sr = new StreamReader(tbFileName.Text.ToString());
                
                string directions = sr.ReadToEnd();

                movements.AddRange(directions.Split(',').Select(p => p.Trim()).ToList());

                lbDirections.ItemsSource = movements;

                tbCount.Text = movements.Count().ToString();
            }

            catch(FileNotFoundException ex)
            {
                MessageBox.Show("File not found.  " + ex.Message);
            }

            catch(UnauthorizedAccessException ex)
            {
                MessageBox.Show("You are not authorized to access this file.  " + ex.Message);
            }

            catch(Exception ex)
            {
                MessageBox.Show("Error occured trying to open or read file.  " + ex.Message);
            }

            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {   
            CalculateDirections(movementList);
        }
    }
}
