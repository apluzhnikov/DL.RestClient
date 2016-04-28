using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DL.RestLibrary
{
    public interface IShop
    {
        string Name { get; }
        UserControl GetPanel();
        void SendRequest();

    }
}
