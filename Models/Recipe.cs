using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Arabam.Com.Models
{
    public class Recipe
    {
        private string _extendedData;
        [Key]
        public int RecipeId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Title { get; set; }
        [Required] //
        [NotMapped]
        public string[] Categories { get; set; }

        //[Required]
        [NotMapped]
        public JObject[] Ingredients

        {
            get
            {
                return JsonConvert.DeserializeObject<JObject[]>(string.IsNullOrEmpty(_extendedData) ? "[{}]" : _extendedData);                
            }
            set
            {
                _extendedData = new JArray(value).ToString();
            }
        }

        public string Directions { get; set; }
    }
}
