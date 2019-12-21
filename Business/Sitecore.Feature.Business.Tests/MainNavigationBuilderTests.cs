using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Feature.Business.Models;
using Sitecore.Feature.Business.Builders;
using Sitecore.FakeDb;
using Sitecore.Data.Items;
using System.Linq;
using Xunit;

namespace Sitecore.Feature.Business.Tests
{
    public class MainNavigationBuilderTests
    {
        public class MainNavigationBuilderSimpleTests
        {
            [Fact]
            public void MainNavigationBuilderCanBeInstanced()
            {
                var builder = new MainNavigationBuilder();
                Assert.NotNull(builder);
            }
        }
    }

    public class MainNavigationBuilderDataTests
    {
        [Fact]
        public void CorrectTopLevelInformation()
        {
            using (Db db = new Db
            {
              new DbItem("Home")
              {
                  { "ContentHeading", "Home" },
                  { "ExcludeFromNavigation", ""}
              }
            })
            {
                Item home = db.GetItem("/sitecore/content/home");
                var result = new MainNavigationBuilder().Build(home);
                Assert.Equal(home.Fields["ContentHeading"].Value, result.Title);
                Assert.Equal(Sitecore.Links.LinkManager.GetItemUrl(home), result.URL);
            }
        }

        [Fact]
        public void CorrectFirstLevelInformation()
        {
            using (Db db = new Db
            {
              new DbItem("Home")
              { 
                  { "ContentHeading", "Home" },
                  { "ExcludeFromNavigation", ""},
                  new DbItem("Events1")
                  {
                      { "ContentHeading", "Events1" },
                      { "ExcludeFromNavigation", ""}
                  },
                  new DbItem("Events2")
                  {
                      { "ContentHeading", "Events2" },
                      { "ExcludeFromNavigation", "1"}
                  }
              }
            })
            {
                Item home = db.GetItem("/sitecore/content/home");
                Item events1 = db.GetItem("/sitecore/content/home/events1");
                var result = new MainNavigationBuilder().Build(home);
                Assert.Collection(result.Children,
                    m => { Assert.Equal(events1.Fields["ContentHeading"].Value, m.Title); Assert.Equal(Sitecore.Links.LinkManager.GetItemUrl(events1), m.URL); });
            }
        }

        [Fact]
        public void CorrectSecondLevelInformation()
        {
            using (Db db = new Db
            {
              new DbItem("Home")
              {
                  { "ContentHeading", "Home" },
                  { "ExcludeFromNavigation", ""},
                  new DbItem("Events1")
                  {
                      { "ContentHeading", "Events1" },
                      { "ExcludeFromNavigation", ""},
                      new DbItem("EventList1")
                      {
                          { "ContentHeading", "EventList1" },
                          { "ExcludeFromNavigation", ""}
                      },
                      new DbItem("EventList2")
                      {
                          { "ContentHeading", "EventList2" },
                          { "ExcludeFromNavigation", ""}
                      }
                  }
              }
            })
            {
                Item home = db.GetItem("/sitecore/content/home");
                Item events1 = db.GetItem("/sitecore/content/home/events1");
                Item eventList1 = db.GetItem("/sitecore/content/home/events1/eventlist1");
                Item eventList2 = db.GetItem("/sitecore/content/home/events1/eventlist2");
                var result = new MainNavigationBuilder().Build(home);
                Assert.Collection(result.Children.First(m => m.Title == events1.Fields["ContentHeading"].Value).Children,
                    m => { Assert.Equal(eventList1.Fields["ContentHeading"].Value, m.Title); Assert.Equal(Sitecore.Links.LinkManager.GetItemUrl(eventList1), m.URL); },
                    m => { Assert.Equal(eventList2.Fields["ContentHeading"].Value, m.Title); Assert.Equal(Sitecore.Links.LinkManager.GetItemUrl(eventList2), m.URL); });
            }
        }
    }
}
