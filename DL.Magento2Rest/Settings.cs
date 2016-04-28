using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Magento2Rest
{
    public class Settings
    {
        
        public string AccessToken { get; internal set; }
        public string AccessTokenSecret { get; internal set; }
        public string ConsumerKey { get; internal set; }
        public string ConsumerSecret { get; internal set; }
        public object Object { get; internal set; }
        public string Operation { get; internal set; }
        public string Uri { get; internal set; }
        public IRestResponse RestResponse { get; internal set; }

        public event EventHandler ResponceReceived;

        protected virtual void OnResponceReceived()
        {
            // Raise the event by using the () operator.
            if (ResponceReceived != null)
                ResponceReceived(this, new EventArgs());
        }

        public void OperationIsFinished() => OnResponceReceived();
    }
}
