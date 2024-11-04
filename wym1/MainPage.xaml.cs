using System.Net;
using System.Text.Json;
using System.Xml.Linq;
namespace wym1
{
    public partial class MainPage : ContentPage
    {

        public class Currency
        {
            public string? table { get; set; }
            public string? currency { get; set; }
            public string? code { get; set; }
            public IList<Rate> rates { get; set; }
        }

        public class Rate
        {
            public string? no { get; set; }
            public string? effectiveDate { get; set; }
            public double? bid { get; set; }
            public double? ask { get; set; }
        }

        public MainPage()
        {
            InitializeComponent();
            DateTime dzis = DateTime.Now;
            dpData1.MaximumDate = dzis;
        }

        private void Bcurrency1(object sender, EventArgs e)
        {


            string dat = dpData1.Date.ToString("yyyy-MM-dd");
            int index = wybierzwalute1.SelectedIndex;
            string walut = "";
            if (index != -1)
            {
                walut = (string)wybierzwalute1.ItemsSource[index];
            }
            string url1 = "https://api.nbp.pl/api/exchangerates/rates/c/" + walut + "/" + dat + "/?format=json";
            string json;
            using (var webClient = new WebClient())
            {
                json = webClient.DownloadString(url1);
            }
            Currency c = JsonSerializer.Deserialize<Currency>(json);


            int index1 = wybierzwalute2.SelectedIndex;
            string walut1 = "";
            if (index1 != -1)
            {
                walut1 = (string)wybierzwalute2.ItemsSource[index1];
            }
            string url2 = "https://api.nbp.pl/api/exchangerates/rates/c/" + walut1 + "/" + dat + "/?format=json";
            string json1;
            using (var webClient = new WebClient())
            {
                json1 = webClient.DownloadString(url2);
            }
            Currency c1 = JsonSerializer.Deserialize<Currency>(json1);

            double ile = Convert.ToDouble(Ilosc.Text);
            string wym = $"{c.rates[0].bid}";
            double wym1 = Convert.ToDouble(wym);
            double ilepln = wym1 * ile;
            string skup = $"{c1.rates[0].bid}";
            double skup1 = Convert.ToDouble(skup);
            double wynik = ilepln / skup1;
            textCurrency1.Text = wynik.ToString();
        }
    }
}

