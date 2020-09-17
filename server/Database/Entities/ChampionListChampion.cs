using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Database.Entities
{
    public class ChampionListChampion
    {
        public Guid ChampionId { get; set; }
        public Champion Champion { get; set; }

        public Guid ChampionListId { get; set; }
        public ChampionList ChampionList { get; set; }
    }
}
