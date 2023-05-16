using System.IO;
using System.Windows;

namespace PKG
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class DES_Window
    {
        private readonly DES_ViewModel _viewModel;
        private long[] _keysValue;
        private ISymmetricCypher _symmetricCypher;
        private bool isDES = true;


        public DES_Window()
        {
            InitializeComponent();
            _viewModel = new DES_ViewModel();
            DataContext = _viewModel;
            _viewModel.IsThreeBoxesVisible = false;
            GenerateKeyAndCypher();
        }

        private void CheckKeyValue()
        {
            if (isDES)
                if (_keysValue[0] != long.Parse(keyTextBox.Text))
                {
                    _keysValue[0] = long.Parse(keyTextBox.Text);
                    _symmetricCypher = new DES(new Key(_keysValue[0]));
                }
                else
                {
                    var hasChanged = false;
                    if (_keysValue != null && _keysValue[0] != long.Parse(keyTextBox.Text))
                    {
                        hasChanged = true;
                        _keysValue[0] = long.Parse(keyTextBox.Text);
                    }

                    if (_keysValue != null && _keysValue[1] != long.Parse(keyTextBox.Text))
                    {
                        hasChanged = true;
                        _keysValue[1] = long.Parse(keyTextBox2.Text);
                    }

                    if (_keysValue != null && _keysValue[2] != long.Parse(keyTextBox.Text))
                    {
                        hasChanged = true;
                        _keysValue[2] = long.Parse(keyTextBox3.Text);
                    }

                    if (hasChanged)
                    {
                        Key[] keys = { new Key(_keysValue[0]), new Key(_keysValue[1]), new Key(_keysValue[2]) };
                        _symmetricCypher = new TripleDES(keys);
                    }
                }
        }

        private void GenerateKeyAndCypher()
        {
            _keysValue = new long[3];
            if (isDES)
            {
                _keysValue[0] = Key.GenrateRandomKeyInput();
                keyTextBox.Text = _keysValue[0].ToString();
                _symmetricCypher = new DES(new Key(_keysValue[0]));
            }
            else
            {
                _keysValue[0] = Key.GenrateRandomKeyInput();
                keyTextBox.Text = _keysValue[0].ToString();
                _keysValue[1] = Key.GenrateRandomKeyInput();
                keyTextBox2.Text = _keysValue[1].ToString();
                _keysValue[2] = Key.GenrateRandomKeyInput();
                keyTextBox3.Text = _keysValue[2].ToString();
                Key[] keys = { new Key(_keysValue[0]), new Key(_keysValue[1]), new Key(_keysValue[2]) };
                _symmetricCypher = new TripleDES(keys);
            }
        }

        private void CypherButton(object sender, RoutedEventArgs e)
        {
            CheckKeyValue();
            if ((bool)FileSwitch.IsChecked)
            {
                jawnyTextBox.Text = File.ReadAllText(jawnyPathTextBox.Text);
                szyfrogramTextBox.Text =
                    _symmetricCypher.EncodeFile(jawnyPathTextBox.Text, szyfrogramPathTextBox.Text);
            }
            else
            {
                var from = jawnyTextBox.Text;
                szyfrogramTextBox.Text = _symmetricCypher.EncodeString(from);
            }
        }

        private void DecypherButton(object sender, RoutedEventArgs e)
        {
            CheckKeyValue();
            if ((bool)FileSwitch.IsChecked)
            {
                szyfrogramTextBox.Text = File.ReadAllText(szyfrogramPathTextBox.Text);
                jawnyTextBox.Text =
                    _symmetricCypher.DecodeFile(szyfrogramPathTextBox.Text, jawnyPathTextBox.Text);
            }
            else
            {
                var from = szyfrogramTextBox.Text;
                jawnyTextBox.Text = _symmetricCypher.DecodeString(from);
            }
        }

        private void GenerateKeyButton(object sender, RoutedEventArgs e)
        {
            GenerateKeyAndCypher();
        }

        private void TripleDesButton_Click(object sender, RoutedEventArgs e)
        {
            isDES = false;
            _viewModel.IsThreeBoxesVisible = true;
        }

        private void DesButton_OnClick(object sender, RoutedEventArgs e)
        {
            isDES = true;
            _viewModel.IsThreeBoxesVisible = false;
        }
    }
}