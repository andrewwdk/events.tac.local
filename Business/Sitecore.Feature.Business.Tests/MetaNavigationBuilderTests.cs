using System;
using Sitecore.Feature.Business.Builders;
using Sitecore.FakeDb;
using Sitecore.Data;
using Sitecore.Data.Items;
using System.Linq;
using Xunit;

namespace Sitecore.Feature.Business.Tests
{
    public class MetaNavigationBuilderDataTests
    {
        [Fact]
        public void CorrectInformation()
        {
            var id1 = ID.NewID;
            var id2 = ID.NewID;
            var id3 = ID.NewID;

            using (Db db = new Db
            {
              new DbItem("Page1", id1)
              {
                  { "ContentHeading", "Page1" }
              },
              new DbItem("Page2", id2)
              {
                  { "ContentHeading", "Page2" }
              },
              new DbItem("Page3", id3)
              {
                  { "ContentHeading", "Page3" }
              },
              new DbItem("Home")
              {
                  { "ContentHeading", "Home" },
                  { "MetaPages", id1 + "|" + id2 }
              }
            })
            {
                Item home = db.GetItem("/sitecore/content/home");
                Item page1 = db.GetItem("/sitecore/content/page1");
                Item page2 = db.GetItem("/sitecore/content/page2");
                var result = new MetaNavigationBuilder().Build(home);
                Assert.Collection(result,
                     m => { Assert.Equal(page1.Fields["ContentHeading"].Value, m.Title); Assert.Equal(Sitecore.Links.LinkManager.GetItemUrl(page1), m.URL); },
                     m => { Assert.Equal(page2.Fields["ContentHeading"].Value, m.Title); Assert.Equal(Sitecore.Links.LinkManager.GetItemUrl(page2), m.URL); });
            }
        }
    }
}
