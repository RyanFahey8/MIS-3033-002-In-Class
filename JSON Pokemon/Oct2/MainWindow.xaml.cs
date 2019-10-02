using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace Oct2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync(@"https://pokeapi.co/api/v2/pokemon?limit=964").Result;
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    var pokemon = JsonConvert.DeserializeObject<Results>(content);

                    foreach (var item in pokemon.results)
                    {
                        DisplayLB.Items.Add(item);
                    }

                }
            }
        }
        
        private void PokeButton_Click(object sender, RoutedEventArgs e)
        {
            var choice = ((Result)DisplayLB.SelectedItem);
            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync(choice.url).Result;
                //or var response = client.GetAsync($"https://pokeapi.co/api/v2/pokemon/{choice}").Result;
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    var pokemon = JsonConvert.DeserializeObject<Pokemon>(content);

                    InfoTB.Text = pokemon.ToString();
                }
            }
        }
    }
}
