using System.ComponentModel.DataAnnotations;

namespace APICep.DTOs
{
    public class BrasilCepResponse
    {
        [Required]
        public string Cep { get; set;} = string.Empty;

        [Required]
        public string? State { get; set;}

        [Required]
        public string? City { get; set; }
        public string? Neighborhood { get; set;}
        public string? Street { get; set;}
        public string? TimezoneName { get; set;}
        public Location? Location {  get; set;}

    }
    public class Location
    {
        public string? Type { get; set;}
        public Coordinates? Coordinates { get; set;}
    }
    public class Coordinates
    {
        public string? Longitude { get; set;}
        public string? Latitude { get; set;}
    }
}
