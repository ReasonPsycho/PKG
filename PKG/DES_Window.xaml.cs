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
        private ElGamal _elGamal;
        private long[] _keysValue;
        private ISymmetricCypher _symmetricCypher;
        private bool isDES = true;
        private bool isElG;
        private int openKey;
        private int primery;
        private int privateKey;


        public DES_Window()
        {
            InitializeComponent();
            _viewModel = new DES_ViewModel();
            DataContext = _viewModel;
            _viewModel.IsThreeBoxesVisible = false;
            _symmetricCypher = new DES();
            GenerateKeyAndCypher();
        }

        private void CheckKeyValue()
        {
            if (!isElG)
            {
                if (isDES)
                {
                    if (_keysValue[0] != long.Parse(keyTextBox.Text))
                    {
                        _keysValue[0] = long.Parse(keyTextBox.Text);
                        _symmetricCypher = new DES(new Key(_keysValue[0]));
                    }
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
            else
            {
                var hasChanged = false;
                if (primery != null && primery != int.Parse(keyTextBox.Text))
                {
                    hasChanged = true;
                    primery = int.Parse(keyTextBox.Text);
                }

                if (openKey != null && openKey != int.Parse(keyTextBox2.Text))
                {
                    hasChanged = true;
                    openKey = int.Parse(keyTextBox2.Text);
                }

                if (privateKey != null && privateKey != int.Parse(keyTextBox3.Text))
                {
                    hasChanged = true;
                    privateKey = int.Parse(keyTextBox2.Text);
                }

                if (hasChanged) _elGamal = new ElGamal(int.Parse(keyTextBox.Text), int.Parse(keyTextBox2.Text));
            }
        }

        private void GenerateKeyAndCypher()
        {
            if (!isElG)
            {
                _keysValue = new long[3];
                if (isDES)
                {
                    _keysValue[0] = _symmetricCypher.GenrateRandomKeyInput();
                    keyTextBox.Text = _keysValue[0].ToString();
                    _symmetricCypher = new DES(new Key(_keysValue[0]));
                }
                else
                {
                    _keysValue[0] = _symmetricCypher.GenrateRandomKeyInput();
                    keyTextBox.Text = _keysValue[0].ToString();
                    _keysValue[1] = _symmetricCypher.GenrateRandomKeyInput();
                    keyTextBox2.Text = _keysValue[1].ToString();
                    _keysValue[2] = _symmetricCypher.GenrateRandomKeyInput();
                    keyTextBox3.Text = _keysValue[2].ToString();
                    Key[] keys = { new Key(_keysValue[0]), new Key(_keysValue[1]), new Key(_keysValue[2]) };
                    _symmetricCypher = new TripleDES(keys);
                }
            }
            else
            {
                primery = ElGamalPrimeGenerator.Generate();
                keyTextBox.Text = primery.ToString();
                _elGamal = new ElGamal(primery, 0);
                privateKey = _elGamal.x;
                keyTextBox2.Text = _elGamal.x.ToString();
                openKey = _elGamal.GetPublicKey();
                keyTextBox3.Text = openKey.ToString();
            }
        }

        private void CypherButton(object sender, RoutedEventArgs e)
        {
            CheckKeyValue();
            if (!isElG)
            {
                if ((bool)FileSwitch.IsChecked)
                {
                    if (CheckFileExists(jawnyPathTextBox.Text))
                    {
                        jawnyTextBox.Text = File.ReadAllText(jawnyPathTextBox.Text);
                        szyfrogramTextBox.Text =
                            _symmetricCypher.EncodeFile(jawnyPathTextBox.Text, szyfrogramPathTextBox.Text);
                    }
                }
                else
                {
                    var from = jawnyTextBox.Text;
                    szyfrogramTextBox.Text = _symmetricCypher.EncodeString(from);
                }
            }
            else
            {
                if ((bool)FileSwitch.IsChecked)
                {
                    if (CheckFileExists(jawnyPathTextBox.Text))
                    {
                        jawnyTextBox.Text = File.ReadAllText(jawnyPathTextBox.Text);
                        szyfrogramTextBox.Text =
                            ElGamalImplemantation.EncodeFile(_elGamal, jawnyPathTextBox.Text,
                                szyfrogramPathTextBox.Text);
                    }
                }
                else
                {
                    var from = jawnyTextBox.Text;
                    szyfrogramTextBox.Text = ElGamalImplemantation.EncodeString(_elGamal, from);
                }
            }
        }

        private void DecypherButton(object sender, RoutedEventArgs e)
        {
            CheckKeyValue();
            if (!isElG)
            {
                if ((bool)FileSwitch.IsChecked)
                {
                    if (CheckFileExists(szyfrogramTextBox.Text))
                    {
                        szyfrogramTextBox.Text = File.ReadAllText(szyfrogramPathTextBox.Text);
                        jawnyTextBox.Text =
                            _symmetricCypher.DecodeFile(szyfrogramPathTextBox.Text, jawnyPathTextBox.Text);
                    }
                }
                else
                {
                    var from = szyfrogramTextBox.Text;
                    jawnyTextBox.Text = _symmetricCypher.DecodeString(from);
                }
            }
            else
            {
                if ((bool)FileSwitch.IsChecked)
                {
                    if (CheckFileExists(szyfrogramPathTextBox.Text))
                    {
                        szyfrogramTextBox.Text = File.ReadAllText(szyfrogramPathTextBox.Text);
                        jawnyTextBox.Text =
                            ElGamalImplemantation.DecodeFile(_elGamal, szyfrogramPathTextBox.Text,
                                jawnyPathTextBox.Text);
                    }
                }
                else
                {
                    var from = szyfrogramTextBox.Text;
                    jawnyTextBox.Text = ElGamalImplemantation.DecodeString(_elGamal, from);
                }
            }
        }

        private void GenerateKeyButton(object sender, RoutedEventArgs e)
        {
            GenerateKeyAndCypher();
        }

        private void TripleDesButton_Click(object sender, RoutedEventArgs e)
        {
            isDES = false;
            isElG = false;
            _viewModel.IsThreeBoxesVisible = true;
        }

        private void DesButton_OnClick(object sender, RoutedEventArgs e)
        {
            isDES = true;
            isElG = false;
            _viewModel.IsThreeBoxesVisible = false;
        }

        public static bool CheckFileExists(string filePath)
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show("File doesn't exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void ElGamal_Click(object sender, RoutedEventArgs e)
        {
            isDES = false;
            isElG = true;
            _viewModel.IsThreeBoxesVisible = true;
        }
    }
}