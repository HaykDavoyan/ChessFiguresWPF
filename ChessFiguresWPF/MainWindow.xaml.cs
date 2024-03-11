using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ChessFiguresWPF
{
    public partial class MainWindow : Window
    {
        private const int SquareSize = 50;

        public MainWindow()
        {
            InitializeComponent();
            EnterButton.Click += EnterButton_Click;
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedColor = (Color.SelectedItem as ComboBoxItem)?.Content.ToString();
            string selectedFigure = (Figure.SelectedItem as ComboBoxItem)?.Content.ToString();
            string coordinates = CoordinatesTextBox.Text.ToUpper();

            if (!string.IsNullOrEmpty(selectedColor) && !string.IsNullOrEmpty(selectedFigure) && !string.IsNullOrEmpty(coordinates))
            {
                if (coordinates.Length == 2)
                {
                    char columnOne = coordinates[0];
                    char rowOne = coordinates[1];

                    if (columnOne >= 'A' && columnOne <= 'H' && rowOne >= '1' && rowOne <= '8')
                    {
                        int column = columnOne - 'A';
                        int row = '8' - rowOne;

                        bool positionEmpty = IsPositionEmpty(column, row);

                        if (positionEmpty)
                        {
                            double figureWidth = 50;
                            double figureHeight = 50;

                            double figureLeft = column * SquareSize;
                            double figureTop = row * SquareSize;

                            string imagePath = $"/jpg/{selectedFigure}{selectedColor.Substring(0, 1)}.png";

                            Image figureImage = new Image();
                            figureImage.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                            figureImage.Width = figureWidth;
                            figureImage.Height = figureHeight;

                            Canvas.SetLeft(figureImage, figureLeft);
                            Canvas.SetTop(figureImage, figureTop);

                            ChessBoardCanvas.Children.Add(figureImage);
                        }
                        else
                        {
                            MessageBox.Show("Position is already occupied.", "Invalid Position", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid coordinates. Please enter coordinates between A1 and H8.", "Invalid Coordinates", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid coordinates format. Please enter coordinates in the format [A-H][1-8].", "Invalid Format", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select color, figure, and enter coordinates.", "Incomplete Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private bool IsPositionEmpty(int column, int row)
        {
            foreach (var child in ChessBoardCanvas.Children)
            {
                if (child is Image image)
                {
                    double left = Canvas.GetLeft(image);
                    double top = Canvas.GetTop(image);

                    int col = (int)(left / SquareSize);
                    int r = (int)(top / SquareSize);

                    if (col == column && r == row)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
