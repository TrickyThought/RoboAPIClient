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

using RestSharp;

namespace RoboAPIClient
{
    public class MoveRequestParameters
    {
        public float speed;
        public float duration;

        public MoveRequestParameters(float speed, float duration)
        {
            this.speed = speed;
            this.duration = duration;
        }
    }
    
    /*public class MoveResponse
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "published_at")]
        public string PublishedAt { get; set; }
    }*/

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string ip = "192.168.1.106";
        private int port = 5000;
        private string uriFormat = "http://{0}:{1}/robo/api/v1.0/{2}";
        private string moveForwardURI;
        private string moveBackwardURI;
        private string moveLeftURI;
        private string moveRightURI;
        private string stopURI;

        public MainWindow()
        {
            InitializeComponent();

            moveForwardURI  = string.Format(uriFormat, ip, port, "moveforward");
            moveBackwardURI = string.Format(uriFormat, ip, port, "movebackward");
            moveLeftURI     = string.Format(uriFormat, ip, port, "moveleft");
            moveRightURI    = string.Format(uriFormat, ip, port, "moveright");
            stopURI         = string.Format(uriFormat, ip, port, "stop");
        }

        private void MoveForward_Click(object sender, RoutedEventArgs e)
        {
            textBlockResponse.Text = SendRequest(moveForwardURI);
        }

        private void MoveBackward_Click(object sender, RoutedEventArgs e)
        {
            textBlockResponse.Text = SendRequest(moveBackwardURI);
        }

        private void MoveLeft_Click(object sender, RoutedEventArgs e)
        {
            textBlockResponse.Text = SendRequest(moveLeftURI);
        }

        private void MoveRight_Click(object sender, RoutedEventArgs e)
        {
            textBlockResponse.Text = SendRequest(moveRightURI);
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            textBlockResponse.Text = SendRequest(stopURI);
        }

        public string SendRequest(string url)
        {
            var client = new RestClient(url);
            var request = new RestRequest();
            request.Method = Method.PUT;
            request.AddJsonBody(new MoveRequestParameters(99.9f, 0.9f));
            var response = client.Execute(request);

            return response.Content;
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W || e.Key == Key.Up)
            {
                MoveForward_Click(sender, e);
            }
            else if (e.Key == Key.S || e.Key == Key.Down)
            {
                MoveBackward_Click(sender, e);
            }
            else if (e.Key == Key.A || e.Key == Key.Left)
            {
                MoveLeft_Click(sender, e);
            }
            else if (e.Key == Key.D || e.Key == Key.Right)
            {
                MoveRight_Click(sender, e);
            }
            else if (e.Key == Key.Z)
            {
                Stop_Click(sender, e);
            }
        }
    }
}
