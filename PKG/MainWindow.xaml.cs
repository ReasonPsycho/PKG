using System;
using System.Windows;

namespace PKG
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CypherButton(object sender, RoutedEventArgs e)
        {
            if ((bool)FileSwitch.IsChecked)
            {
                // DESImplemantation.EncodeFile(jawnyPathTextBox,szyfrogramTextBox,)
            }
        }

        private void DecypherButton(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void GenerateKey(object sender, RoutedEventArgs e)
        {
            //keyTextBox.Text = Key;
        }
    }
}