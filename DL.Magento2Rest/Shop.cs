using DL.RestClient.RestClients.Magento2.OAuth;
using DL.RestLibrary;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DL.Magento2Rest
{
    public class Shop : IShop
    {
        public string Name { get { return "Magento 2.0"; } }

        Panel _panel;

        public void SendRequest()
        {
            try
            {
                Method method = (Method)Enum.Parse(typeof(Method), _panel.Settings.Operation);
                var restClient = new RestSharp.RestClient(_panel.Settings.Uri);
                var request = new RestRequest(method);
                OAuthBase oAuth = new OAuthBase();
                string nonce = oAuth.GenerateNonce();

                string timeStamp = oAuth.GenerateTimeStamp();
                string parameters;
                string normalizedUrl;
                string signature = oAuth.GenerateSignature(new Uri(_panel.Settings.Uri), _panel.Settings.ConsumerKey, _panel.Settings.ConsumerSecret, _panel.Settings.AccessToken, _panel.Settings.AccessTokenSecret, _panel.Settings.Operation, timeStamp, nonce, OAuthBase.SignatureTypes.HMACSHA1, out normalizedUrl, out parameters);
                /* string signature = oAuth.GenerateSignature(
                    new Uri("http://magento2.holbidev.co.uk/rest/V1/products/28732400HB")
                    , "wqn46nijaiinkddqvm7nh9yuu1gpd34e", "5becg1jr05edhlr0838a3tyacaw7ud7i", "v4jduksl8cdgr21v99vgv1shxnai96oc", "6le50ake47pjv2cen4y63yjroo8447fs",
                    "", timeStamp, nonce, OAuthBase.SignatureTypes.HMACSHA1, out normalizedUrl, out parameters);*/

                signature = oAuth.UrlEncode(signature);
                var requestParamsString = string.Format("OAuth realm={0},oauth_consumer_key={1},oauth_token={2},oauth_nonce={3},oauth_signature_method={4},oauth_timestamp={5},oauth_version={6},oauth_signature={7}",
                                                        _panel.Settings.Uri, _panel.Settings.ConsumerKey, _panel.Settings.AccessToken, nonce, "HMAC-SHA1", timeStamp, "1.0", signature);
                request.AddHeader("Authorization", requestParamsString);
                request.AddHeader("Accept", "application/json");
                /*if (_panel.Settings.Object != null)
                    request.AddJsonBody(_panel.Settings.Object);*/

                _panel.Settings.RestResponse = restClient.Execute(request);
                _panel.Settings.OperationIsFinished();

            }
            catch (Exception ex)
            {
                //return null;
            }
        }

        public UserControl GetPanel()
        {
            if (_panel == null)
                _panel = new Panel();
                        
            return _panel;

        }
    }
}
