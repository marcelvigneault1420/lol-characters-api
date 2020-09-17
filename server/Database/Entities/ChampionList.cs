using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Database.Entities
{
    public class ChampionList
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<ChampionListChampion> ChampionListChampions { get; set; }
    }
}
