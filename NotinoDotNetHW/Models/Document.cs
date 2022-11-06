using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace NotinoDotNetHW.Models
{
    /// <summary>
    /// Document model
    /// </summary>
    public class Document
    {
        [Key]
        public Guid Id { get; set; }

        public List<string> Tags { get; set; } = new List<string>();

        public Dictionary<string,string>? Data { get; set; } = new Dictionary<string, string>();

    }
}
