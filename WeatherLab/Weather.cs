using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WeatherLab
{
    public class Weather
    {
        [Key]
        [Column("id")]
        public int WeatherId { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
        public string dateh { get; set; }
        public string adate { get; set; }
        public double hddv { get; set; }
        public string datec { get; set; }
        public double cvvv { get; set; }
        public double maxtemp { get; set; }
        public double mintemp { get; set; }
        public double meantemp { get; set; }
        public double precipitation { get; set; }

    }
}
