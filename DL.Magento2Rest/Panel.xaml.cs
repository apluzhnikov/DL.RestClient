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

namespace DL.Magento2Rest
{
    /// <summary>
    /// Interaction logic for Panel.xaml
    /// </summary>
    public partial class Panel : UserControl
    {
        Settings _settings = new Settings();
        public Panel()
        {
            InitializeComponent();
            _settings.ResponceReceived += _settings_ResponceReceived;
        }

        private void _settings_ResponceReceived(object sender, EventArgs e)
        {
            ResponceTblc.Text = _settings.RestResponse.StatusCode.ToString();
            ResponceTbx.Text = _settings.RestResponse.Content;
        }

        public Settings Settings { get { return _settings; } }

        

        private void secretTbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            _settings.ConsumerKey = consumerKeyTbx.Text;
            _settings.ConsumerSecret = consumerSecretTbx.Text;
            _settings.AccessToken = accessTockenTbx.Text;
            _settings.AccessTokenSecret = accessTockenSecretTbx.Text;
            _settings.Operation = typeTbx.Text;
            _settings.Uri = urlTbx.Text;
            _settings.Object = RequestTbx.Text;
        }
    }
}
