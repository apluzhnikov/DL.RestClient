using DL.RestLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

namespace DL.RestClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<IShop> _shops = new List<IShop>();
        IShop _shop = null;
        public MainWindow()
        {
            InitializeShops();
            DataContext = this;
            InitializeComponent();
        }

        private void InitializeShops()
        {
            //shops            
            var engineSubDir = new DirectoryInfo("Shops");
            var files = engineSubDir.GetFiles("*.dll", SearchOption.TopDirectoryOnly);
            foreach (FileInfo addInFile in files)
            {
                Assembly assembly = Assembly.LoadFrom(addInFile.FullName);
                var type = assembly.GetTypes().FirstOrDefault(p => typeof(IShop).IsAssignableFrom(p));
                if (type != null)
                {
                    _shops.Add((IShop)Activator.CreateInstance(type));
                }
            }
        }

        public List<IShop> Shops { get { return _shops; } }

        private void shopsCbx_Selected(object sender, RoutedEventArgs e)
        {
            shopPanel.Children.Clear();
            try
            {
                var contentPresenter = (ContentPresenter)e.Source;
                _shop = (IShop)contentPresenter.DataContext;
            }
            catch
            {
                _shop = null;
            }
            if (_shop != null)
            {
                shopPanel.Children.Add(_shop.GetPanel());
                runQueryBtn.IsEnabled = true;
            }else
                runQueryBtn.IsEnabled = false;
        }
        

        private void runQueryBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_shop != null)
                _shop.SendRequest();
        }
    }
}
