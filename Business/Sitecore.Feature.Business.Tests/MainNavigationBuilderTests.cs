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
    public class MainNavigationBuilderSimpleTests
    {
        [Fact]
        public void MainNavigationBuilder_ShouldNotReturnNull_IfInstanced()
        {
            var builder = new MainNavigationBuilder();

            Assert.NotNull(builder);
        }

        [Fact]
        public void MainNavigationBuilder_BuildMethodShouldNotThrowException_IfHomeIsNull()
        {
            var builder = new MainNavigationBuilder();
            Item home = null;

            var ex = Record.Exception(() => builder.Build(home));

            Assert.Null(ex);
        }
    }

    public class MainNavigationBuilderDataTests
    {
        [Fact]
        public void MainNavigationBuilder_BuildMethodReturnsCorrectHomeTitle()
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
            }
        }

        [Fact]
        public void MainNavigationBuilder_BuildMethodReturnsCorrectHomeUrl()
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

                Assert.Equal(Sitecore.Links.LinkManager.GetItemUrl(home), result.URL);
            }
        }

        [Fact]
        public void MainNavigationBuilder_BuildMethodReturnsCorrectFirstLevelItemTitle()
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
                    m =>  Assert.Equal(events1.Fields["ContentHeading"].Value, m.Title));
            }
        }

        [Fact]
        public void MainNavigationBuilder_BuildMethodReturnsCorrectFirstLevelItemUrl()
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
                    m => Assert.Equal(Links.LinkManager.GetItemUrl(events1), m.URL));
            }
        }

        [Fact]
        public void MainNavigationBuilder_BuildMethodReturnsCorrectSecondLevelItemTitle()
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

        [Fact]
        public void MainNavigationBuilder_BuildMethodReturnsCorrectSecondLevelItemUrl()
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
                    m =>  Assert.Equal(Sitecore.Links.LinkManager.GetItemUrl(eventList1), m.URL),
                    m => Assert.Equal(Sitecore.Links.LinkManager.GetItemUrl(eventList2), m.URL));
            }
        }
    }
}
