using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.API.Domain.BussinessObjects.FantasyBOs
{
    public class GoalBO
    {
        public long GoalId { get; set; }

        public string Name { get; set; }

        public int CompletionCount { get; set; }

        public string GoalLogo { get; set; }

        public SportBO Sport { get; set; }
    }
}
