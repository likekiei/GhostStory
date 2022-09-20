using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace GhostStory.Models
{
    public class DBInitializer:DropCreateDatabaseAlways<GhostStoryContext>
    {
        protected override void Seed(GhostStoryContext context)
        {
            base.Seed(context);
            //////////////////////////////

            //List<Post> post = new List<Post>
            //{
            //    new Post
            //    {
            //       PostID="A00000001",
            //       Account="Goodbook001",
            //       themesID="A",
            //       Publishtime=DateTime.Today,
            //       Title= "這是文章標體",
            //       Content="這是文章內容",
            //       location="高雄市前金區河東路150號"
            //    }
            //};
            //post.ForEach(s => context.Post.Add(s));
            //context.SaveChanges();

            List<Administrators> administrators = new List<Administrators>
            {
                new Administrators
                {
                  Account="001",
                  Passwd="00000000"
                }
            };
            administrators.ForEach(s => context.Administrators.Add(s));
            context.SaveChanges();


            List<Member> member = new List<Member>
            {
                new Member
                {
                  MemberID="A000000001",
                  Level="E",
                  Account="001",
                  Password="00000000",
                  Name="小明",
                  Gender="男",
                  Phone="0909000000",
                  Address="高雄市前金區河東路150號",
                  Email="000@gmail.com"
                }
            };
            member.ForEach(s => context.Member.Add(s));
            context.SaveChanges();

            List<Themes> themes = new List<Themes>
            {
                new Themes
                {
                  themesID="A",
                  Category="都會傳說",
                  
                },
                new Themes
                {
                  themesID="B",
                  Category="鄉野奇談",

                },
                new Themes
                {
                  themesID="C",
                  Category="真實故事區",

                },
                new Themes
                {
                  themesID="D",
                  Category="虛構詭故事",

                },
                new Themes
                {
                  themesID="E",
                  Category="靈異照片區",

                },
                 new Themes
                {
                  themesID="F",
                  Category="恐怖電影推坑區",

                },
            };
            themes.ForEach(s => context.Themes.Add(s));
            context.SaveChanges();


        }

    }
}