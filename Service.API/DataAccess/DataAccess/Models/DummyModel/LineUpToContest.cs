using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccess.Models.DummyModel
{
    public class LineUpToContest
    {
        public int Id { get; set; }
        public int ContestId { get; set; }
        public int LineUpId { get; set; }

        public virtual Contest Contest { get; set; }
        public virtual LineUp LineUp { get; set; }
    }

}