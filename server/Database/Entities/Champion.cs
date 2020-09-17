using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace server.Database.Entities
{
    public class Champion
    {
        public Guid Id { get; set; }
        [Required]
        public int ApiId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool Visible { get; set; }

        public List<ChampionListChampion> ChampionListChampions { get; set; }

        public Champion() {
            Visible = true;
        }
        public Champion(int pApiId, string pName)
        {
            ApiId = pApiId;
            Name = pName;
            Visible = true;
        }
    }
}
