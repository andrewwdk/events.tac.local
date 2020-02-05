using events.tac.local.Areas.Importer.Models;
using Sitecore;
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
        public int AddCount { get; private set; }
        public int UpdateCount { get; private set; }

        public EventsService()
        {
            AddCount = 0;
            UpdateCount = 0;
        }

        public void AddItems(Item parent, IEnumerable<Event> events, TemplateID templateID)
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
                            UpdateCount++;
                        }
                    }
                    else
                    {
                        AddItem(parent, currentEvent, templateID);
                        AddCount++;
                    }
                }
            }
        }

        private bool EventExists(ChildList children, string name, out Item foundItem)
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

        private void AddItem(Item parent, Event currentEvent, TemplateID templateID)
        {
            var name = ItemUtil.ProposeValidItemName(currentEvent.ContentHeading);
            Item item = parent.Add(name, templateID);
            item.Editing.BeginEdit();
            item[FieldIDs.Workflow] = "{8272D338-C40A-40DD-95A7-E88E8B4BDABB}";
            item[FieldIDs.WorkflowState] = "{2C88B62D-A1AB-4A41-96E8-7E842BC8C22F}";
            item["ContentHeading"] = currentEvent.ContentHeading;
            item["ContentIntro"] = currentEvent.ContentIntro;
            item["DifficultyLevel"] = currentEvent.Difficulty.ToString();
            item["Duration"] = currentEvent.Duration.ToString();
            item["Highlights"] = currentEvent.Highlights;
            item["StartDate"] = Sitecore.DateUtil.ToIsoDate(currentEvent.StartDate);
            item.Editing.EndEdit();
        }

        private void UpdateItem(Item item, Event currentEvent)
        {
            item.Editing.BeginEdit();
            item[FieldIDs.Workflow] = "{8272D338-C40A-40DD-95A7-E88E8B4BDABB}";
            item[FieldIDs.WorkflowState] = "{ 2C88B62D-A1AB-4A41-96E8-7E842BC8C22F}";
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