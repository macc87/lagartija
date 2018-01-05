using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.API.Domain.BussinessObjects.FantasyBOs
{
    public class ContestBO
    {
        public long ContestId { get; set; }

        public ContestTypeBO ContestTypeId { get; set; }

        public string Name { get; set; }

        public int SignedUp { get; set; }

        public int MaxCapacity { get; set; }

        public double EntryFee { get; set; }

        public double SalaryCap { get; set; }

        public IEnumerable<GameBO> Games { get; set;}
        public IEnumerable<LineupBO> LineUps { get; set; }
    }
}
