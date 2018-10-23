using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;

namespace WeatherAppDemo
{
    class APIManager
    {
        public async static Task<RootObject> GetWeather(double lat, double lon)
        {
            var http = new HttpClient();
            var url = String.Format("http://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&mode=json&units=metric&appid=99966711973c1912633344cde47d99d4",lat,lon);
            var response = await http.GetAsync(url); // Nhan data user tu weathermap.org
            var result = await response.Content.ReadAsStringAsync();

            var serializer = new DataContractJsonSerializer(typeof(RootObject));
            // Khoi tao Stream local de doc json
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            // Doc object da phan tich duoc tu json vao stream local de phan tich
            var data = (RootObject)serializer.ReadObject(ms);

            return data;

        }
    }
    [DataContract]
    public class Coord
    {
        public double lon { get; set; }
        public int lat { get; set; }
    }

    [DataContract]
    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public string icon { get; set; }
    }

    [DataContract]
    public class Main
    {
        [DataMember]
        public int temp { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public int temp_min { get; set; }
        public int temp_max { get; set; }
    }

    [DataContract]
    public class Wind
    {
        public double speed { get; set; }
        public int deg { get; set; }
    }

    [DataContract]
    public class Clouds
    {
        public int all { get; set; }
    }

    [DataContract]
    public class Sys
    {
        public int type { get; set; }
        public int id { get; set; }
        public double message { get; set; }
        public string country { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }

    [DataContract]
    public class RootObject
    {
        public Coord coord { get; set; }
        [DataMember]
        public List<Weather> weather { get; set; }
        public string @base { get; set; }
        [DataMember]
        public Main main { get; set; }
        public int visibility { get; set; }
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }
        public int dt { get; set; }
        public Sys sys { get; set; }
        public int id { get; set; }
        [DataMember]
        public string name { get; set; }
        public int cod { get; set; }
    }

}
