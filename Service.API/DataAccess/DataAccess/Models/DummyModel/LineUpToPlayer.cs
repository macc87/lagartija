using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccess.Models.DummyModel
{
    public class LineUpToPlayer
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int LineUpId { get; set; }

        public virtual Player Player { get; set; }
        public virtual LineUp LineUp { get; set; }
    }

}