using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yz_app
{
    public partial class Form1 : Form
    {
        public string link;
        public Boolean music;
        public Boolean playlist;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MakeApiRequest(textBox1.Text);
            /*
            music = checkBox2.Checked;
            playlist = checkBox1.Checked;
            link = textBox1.Text;
            if(music==true && playlist==false)
            {
                var restClient = new RestClient("http://127.0.0.1:5000/");
                var request = new RestRequest("music_recommendation",Method.Get);
                request.AddQueryParameter("link",link);
                var response = restClient.Execute<List<response_model>>(request);
                List<response_model> result = response.Data;
                MessageBox.Show(response.Data.ToString());
                dataGridView1.DataSource = result;
                //label4.Text = response.Data;

            }else if(music==false && playlist == true)
            {
                var restClient = new RestClient("http://127.0.0.1:5000/");
                var request = new RestRequest("recommend", Method.Post);
                request.AddQueryParameter("playlist_id", link);
                var response = restClient.Execute<List<response_model>>(request);
                List<response_model> result = response.Data;
                MessageBox.Show(response.Data.ToString());
                dataGridView1.DataSource = result;
                //label4.Text = response.Data;
            }
            else
            {
                MessageBox.Show("Hatalı bir seçim yaptınız, lütfen seçim yapıp tekrar deneyiniz.");
            }*/
        }
        private const string ApiUrl = "http://localhost:5000/recommend";
        public async void MakeApiRequest(string playlistId)
        {
            using (HttpClient client = new HttpClient())
            {
                // API isteği için JSON verisini oluşturma
                var requestData = new { playlist_id = playlistId };
                var json = JsonConvert.SerializeObject(requestData);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                // API isteği gönderme
                var response = await client.PostAsync(ApiUrl, content);

                // Yanıtı okuma
                var responseContent = await response.Content.ReadAsStringAsync();

                // Önerilen şarkıları işleme
                dynamic recommendations = JsonConvert.DeserializeObject(responseContent);
                foreach (var recommendation in recommendations.recommendations)
                {
                    string songName = recommendation[0].ToString();
                    string artistName = recommendation[1].ToString();

                    // Önerilen şarkıları kullanmak için burada işlem yapabilirsiniz
                    Console.WriteLine("Song: " + songName);
                    Console.WriteLine("Artist: " + artistName);
                    Console.WriteLine();
                }
            }
        }
        public class response_model
        {
            public string isim { get; set; }
            public string sanatci { get; set; }
        }
    }
}
