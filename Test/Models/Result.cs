using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Test.Models
{
    public class Result
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        public int Count { get; set; }
        public string ReverceSentence { get; set; }
    }
}