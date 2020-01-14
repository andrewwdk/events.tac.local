using events.tac.local.Areas.Importer.Models;
using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.SecurityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace events.tac.local.Areas.Importer.Services
{
    public class EventsService
    {
        public static void AddItems(Item parent, IEnumerable<Event> events, TemplateID templateID)
        {
            var children = parent.GetChildren();
            Item foundItem;
            using (new SecurityDisabler())
            {
                foreach (var currentEvent in events)
                {
                    var name = ItemUtil.ProposeValidItemName(currentEvent.ContentHeading);
                    if (EventExists(children, name, out foundItem))
                    {
                        if(foundItem != null)
                        {
                            UpdateItem(foundItem, currentEvent);
                        }
                    }
                    else
                    {
                        AddItem(parent, currentEvent, templateID);
                    }
                }
            }
        }

        private static bool EventExists(ChildList children, string name, out Item foundItem)
        {
            foreach(var child in children)
            {
                if(((Item)child).DisplayName == name)
                {
                    foundItem = (Item)child;
                    return true;
                }
            }

            foundItem = null;
            return false;
        }

        private static void AddItem(Item parent, Event currentEvent, TemplateID templateID)
        {
            var name = ItemUtil.ProposeValidItemName(currentEvent.ContentHeading);
            Item item = parent.Add(name, templateID);
            item.Editing.BeginEdit();
            item["ContentHeading"] = currentEvent.ContentHeading;
            item["ContentIntro"] = currentEvent.ContentIntro;
            item["DifficultyLevel"] = currentEvent.Difficulty.ToString();
            item["Duration"] = currentEvent.Duration.ToString();
            item["Highlights"] = currentEvent.Highlights;
            item["StartDate"] = Sitecore.DateUtil.ToIsoDate(currentEvent.StartDate);
            item.Editing.EndEdit();
        }

        private static void UpdateItem(Item item, Event currentEvent)
        {
            item.Editing.BeginEdit();
            item["ContentHeading"] = currentEvent.ContentHeading;
            item["ContentIntro"] = currentEvent.ContentIntro;
            item["DifficultyLevel"] = currentEvent.Difficulty.ToString();
            item["Duration"] = currentEvent.Duration.ToString();
            item["Highlights"] = currentEvent.Highlights;
            item["StartDate"] = Sitecore.DateUtil.ToIsoDate(currentEvent.StartDate);
            item.Editing.EndEdit();
        }
    }
}