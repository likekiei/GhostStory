using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace GhostStory.Models
{
    public class GhostStoryContext: DbContext
    {
        public GhostStoryContext() : base("name=GhostStoryConnection")
        { }

        public DbSet<Post> Post { get; set; }
        public DbSet<Member> Member { get; set; }

        public DbSet<Score> Score { get; set; }

        public DbSet<Themes> Themes { get; set; }

        public DbSet<Administrators> Administrators { get; set; }

       

    }
}