﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Feature.Search.Models;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Mvc.Presentation;
using Sitecore.ContentSearch.Linq;
using Sitecore.Links;

namespace Sitecore.Feature.Search
{
    public class EventsProvider
    {
        private const int PageSize = 4;
        public EventsList GetEventsList(int pageNo)
        { 
            var indexname = $"events_{RenderingContext.Current.ContextItem.Database.Name.ToLower()}_index";
            var index = ContentSearchManager.GetIndex(indexname);
            using (var context = index.CreateSearchContext())
            {
                var results = context.GetQueryable<EventDetails>()
                    .Where(i => i.Paths.Contains(RenderingContext.Current.ContextItem.ID))
                    .Where(i => i.Language == RenderingContext.Current.ContextItem.Language.Name)
                    .Page(pageNo, PageSize)
                    .GetResults();


                EventDetails[] eventDetailsList  = results.Hits.Select(h => h.Document).ToArray();
               
                for(int i = 0; i < eventDetailsList.Length; i++)
                {
                    var item = eventDetailsList[i].GetItem();
                    var url = LinkManager.GetItemUrl(item);
                    eventDetailsList[i].ItemUrl = url;
                }

                return new EventsList()
                { 
                    Events = eventDetailsList,
                    TotalResultCount = results.TotalSearchResults,
                    PageSize = PageSize
                };
            }
        }

        public EventsList GetEventsListBySearch(int pageNo, string search, int[] durations, int[] difficulties)
        {
            var indexname = $"events_{RenderingContext.Current.ContextItem.Database.Name.ToLower()}_index";
            var index = ContentSearchManager.GetIndex(indexname);
            var home = Sitecore.Context.Database.GetItem(Sitecore.Context.Site.StartPath);
            int pageSize, contentLength = 0;
            int.TryParse(RenderingContext.CurrentOrNull.Rendering.Parameters["PageSize"], out pageSize);
            int.TryParse(RenderingContext.CurrentOrNull.Rendering.Parameters["ContentBodyMaxLength"], out contentLength);
            pageSize = pageSize <= 0 ? PageSize : pageSize;
            using (var context = index.CreateSearchContext())
            {
                var foundEvents = context.GetQueryable<EventDetails>()
                    .Where(i => i.Paths.Contains(home.ID))
                    .Where(i => i.Language == RenderingContext.Current.ContextItem.Language.Name);

                if (search != null)
                { 
                    foundEvents = foundEvents.Where(i => i.Name.Contains(search));
                }

                if(contentLength > 0)
                {
                    foundEvents = foundEvents.Where(i => i.ComputedContentLength <= contentLength);
                }

                if(durations != null && durations.Length > 0)
                {
                    foundEvents = foundEvents.Where(i => durations.Contains(i.Duration));
                }

                if (difficulties != null && difficulties.Length > 0)
                {
                    foundEvents = foundEvents.Where(i => difficulties.Contains(i.DifficultyLevel));
                }

                var pastEvents = foundEvents.Where(i => !(i.StartDate >= DateTime.Now)).OrderByDescending(i => i.StartDate);
                var futureEvents = foundEvents.Where(i => i.StartDate >= DateTime.Now).OrderBy(i => i.StartDate);

                var results = futureEvents.Union(pastEvents).Page(pageNo, pageSize).GetResults();

                var eventDetailsList = results.Hits.Select(h => h.Document).ToArray();
                
                for (int i = 0; i < eventDetailsList.Length; i++)
                {
                    var item = eventDetailsList[i].GetItem();
                    var url = LinkManager.GetItemUrl(item);
                    eventDetailsList[i].ItemUrl = url;
                }

                return new EventsList()
                {
                    Events = eventDetailsList,
                    TotalResultCount = results.TotalSearchResults,
                    PageSize = pageSize
                };
            }
        }
    }
}