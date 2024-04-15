using CMS;
using CMS.Base.Web.UI;
using CMS.DataEngine;
using CMS.EmailEngine;
using CMS.Helpers;
using CMS.MacroEngine;
using CMS.SiteProvider;
using CMS.UIControls;
using CMSApp.Old_App_Code.App.Extenders.Events;
using Events;
using Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using MessageTypeEnum = CMS.Base.Web.UI.MessageTypeEnum;

[assembly: RegisterCustomClass("EventSessionExtender", typeof(EventSessionExtender))]

namespace CMSApp.Old_App_Code.App.Extenders.Events
{
    public class EventSessionExtender : ControlExtender<UniGrid>
    {
        public override void OnInit()
        {
            Control.OnExternalDataBound += Control_OnExternalDataBound;
            Control.OnAction += Control_OnAction;
        }

        private void Control_OnAction(string actionName, object actionArgument)
        {
            switch (actionName)
            {
                case "movetowaitinglist":
                    var activeAttendeeID = ValidationHelper.GetInteger(actionArgument, 0);
                    var currentActiveAttendee = ActiveAttendeeInfo.Provider.Get(activeAttendeeID);

                    if (currentActiveAttendee != null)
                    {
                        var newWaitingListAttendee = new InactiveAttendeeInfo
                        {
                            ParentEventID = currentActiveAttendee.ParentEventID,
                            FirstName = currentActiveAttendee.FirstName,
                            MiddleInitial = currentActiveAttendee.MiddleInitial,
                            LastName = currentActiveAttendee.LastName,
                            Phone = currentActiveAttendee.Phone,
                            Email = currentActiveAttendee.Email,
                            PRNR = currentActiveAttendee.PRNR,
                            Comment = currentActiveAttendee.Comment,
                            DateAdded = currentActiveAttendee.DateAdded
                        };

                        InactiveAttendeeInfo.Provider.Set(newWaitingListAttendee);
                        currentActiveAttendee.Delete();

                        Control.ShowMessage(MessageTypeEnum.Confirmation, "Attendee has been moved to the waiting list.", "", "", false);
                        SendMovedToWaitingListEmail(EventSessionInfo.Provider.Get(currentActiveAttendee.ParentEventID), newWaitingListAttendee);
                    }
                    else
                    {
                        Control.ShowMessage(MessageTypeEnum.Error, "Attendee could not be found. This attendee may have been deleted or moved by another user.", "", "", false);
                    }
                    return;
                case "movetoactivelist":
                    var waitingListAttendeeID = ValidationHelper.GetInteger(actionArgument, 0);
                    var currentWaitingListAttendee = InactiveAttendeeInfo.Provider.Get(waitingListAttendeeID);

                    if (currentWaitingListAttendee != null)
                    {
                        var session = EventSessionInfo.Provider.Get(currentWaitingListAttendee.ParentEventID);

                        if (session != null)
                        {
                            var activeAttendeeCount = ActiveAttendeeInfo.Provider.Get().WhereEquals("ParentEventID", session.EventSessionID).Count;

                            if (session.EventSize - activeAttendeeCount <= 0)
                            {
                                Control.ShowMessage(MessageTypeEnum.Error, "Could not move the selected attendee to the Active list as the event is full.", "", "", true);
                            }
                            else
                            {
                                var newActiveAttendee = new ActiveAttendeeInfo
                                {
                                    ParentEventID = currentWaitingListAttendee.ParentEventID,
                                    FirstName = currentWaitingListAttendee.FirstName,
                                    MiddleInitial = currentWaitingListAttendee.MiddleInitial,
                                    LastName = currentWaitingListAttendee.LastName,
                                    Phone = currentWaitingListAttendee.Phone,
                                    Email = currentWaitingListAttendee.Email,
                                    PRNR = currentWaitingListAttendee.PRNR,
                                    Comment = currentWaitingListAttendee.Comment,
                                    DateAdded = currentWaitingListAttendee.DateAdded
                                };

                                ActiveAttendeeInfo.Provider.Set(newActiveAttendee);
                                currentWaitingListAttendee.Delete();

                                SendMovedToActiveListEmail(session, newActiveAttendee);

                                Control.ShowMessage(MessageTypeEnum.Confirmation, "Attendee has been moved to the active list.", "", "", false);
                            }
                        }

                        else
                        {
                            Control.ShowMessage(MessageTypeEnum.Error, "Parent event could not be found. It may have been deleted by another user.", "", "", false);
                        }
                    }
                    else
                    {
                        Control.ShowMessage(MessageTypeEnum.Error, "Attendee could not be found. This attendee may have been deleted or moved by another user.", "", "", false);
                    }
                    return;
            }
        }

        private object Control_OnExternalDataBound(object sender, string sourceName, object parameter)
        {
            switch (sourceName)
            {
                case "locationguid":
                    var location = LocationInfo.Provider.Get().WhereEquals("LocationGuid", parameter).FirstOrDefault();

                    if (location != null)
                    {
                        return location.LocationName;
                    }

                    return "<strong style=\"color: red\">ERROR - LOCATION NOT FOUND<strong>";
                case "category":
                    var category = CategoryInfo.Provider.Get(ValidationHelper.GetString(parameter, ""));
                    if (category != null)
                    {
                        return category.CategoryName;
                    }

                    return "<strong style=\"color: red\">ERROR - Could not find category</strong>";
                case "remainingspots":
                    var remaingSpotsSession = EventSessionInfo.Provider.Get(ValidationHelper.GetInteger(parameter, 0));

                    if (remaingSpotsSession != null)
                    {
                        var activeAttendees = ActiveAttendeeInfo.Provider.Get().WhereEquals("ParentEventID", remaingSpotsSession.EventSessionID);

                        return remaingSpotsSession.EventSize - activeAttendees.Count;
                    }

                    return "<strong style=\"color: red\">ERROR - Could not find session</strong>";
                case "waitinglistsize":
                    var waitingListSession = EventSessionInfo.Provider.Get(ValidationHelper.GetInteger(parameter, 0));

                    if (waitingListSession != null)
                    {
                        return InactiveAttendeeInfo.Provider.Get().WhereEquals("ParentEventID", waitingListSession.EventSessionID).Count;
                    }

                    return "<strong style=\"color: red\">ERROR - Could not find session</strong>";
                default:
                    return parameter;
            }
        }

        private void SendMovedToActiveListEmail(EventSessionInfo session, ActiveAttendeeInfo activeAttendee)
        {
            var emailTemplate = EmailTemplateInfo.Provider.Get("AttendeeMovedToActiveList", SiteContext.CurrentSiteID);

            if (emailTemplate != null)
            {
                var sb = new StringBuilder();
                sb.Append($"<strong>You have been moved to the Active Attendee List for the event '{session.EventName}'</strong>");
                sb.Append("<p>This email is to notify you that the instructor has reviewed your information and moved you to the Active Attendee list.</p>");
                sb.Append("<p>This means you are officially registered for the event and in the attendance list. <strong>We look forward to seeing you!</strong></p>");
                sb.Append("<p>See below for a reminder on the details of the event and your registration.</p>");

                var emailMacros = MacroResolver.GetInstance();
                dynamic model = new
                {
                    EmailContent = sb.ToString(),
                    EventName = session.EventName,
                    FirstName = activeAttendee.FirstName,
                    LastName = activeAttendee.LastName,
                    Phone = activeAttendee.Phone,
                    PRNR = activeAttendee.PRNR,
                    ShowPRNR = String.IsNullOrEmpty(activeAttendee.PRNR) ? "display: none;" : "",
                    TotalCost = session.Cost,
                    EventStartTime = session.EventStart
                };

                foreach (var property in model.GetType().GetProperties())
                {
                    emailMacros.SetNamedSourceData(property.Name, property.GetValue(model, null));
                }

                var message = new EmailMessage
                {
                    EmailFormat = EmailFormatEnum.Html,
                    From = SettingsKeyInfoProvider.GetValue("CMSNoreplyEmailAddress", SiteContext.CurrentSiteName),
                    Recipients = activeAttendee.Email,
                    Subject = emailTemplate.TemplateSubject
                };

                EmailSender.SendEmail(SiteContext.CurrentSiteName, message, emailTemplate.TemplateName, emailMacros, true);
            }
        }

        private void SendMovedToWaitingListEmail(EventSessionInfo session, InactiveAttendeeInfo inactiveAttendee)
        {
            var emailTemplate = EmailTemplateInfo.Provider.Get("AttendeeMovedToWaitingList", SiteContext.CurrentSiteID);

            if (emailTemplate != null)
            {
                var sb = new StringBuilder();
                sb.Append($"<strong>You have been moved to the Waiting List for the event '{session.EventName}'</strong>");
                sb.Append("<p>This email is to notify you that the instructor has moved you to the Waiting List for this event.</p>");
                sb.Append("<p>This means you are not officially registered for this event and are not on the attendance list for this event. If you feel this was done in error, please <a style=\"color: blue;\" href=\"mailto:\">contact the instructor(s) by clicking here.</a></p>");
                sb.Append("<p>See below for a reminder on the details of the event and your registration.</p>");

                var emailMacros = MacroResolver.GetInstance();
                dynamic model = new
                {
                    EmailContent = sb.ToString(),
                    EventName = session.EventName,
                    FirstName = inactiveAttendee.FirstName,
                    LastName = inactiveAttendee.LastName,
                    Phone = inactiveAttendee.Phone,
                    PRNR = inactiveAttendee.PRNR,
                    ShowPRNR = String.IsNullOrEmpty(inactiveAttendee.PRNR) ? "display: none;" : "",
                    TotalCost = session.Cost,
                    EventStartTime = session.EventStart
                };

                foreach (var property in model.GetType().GetProperties())
                {
                    emailMacros.SetNamedSourceData(property.Name, property.GetValue(model, null));
                }
                var message = new EmailMessage
                {
                    EmailFormat = EmailFormatEnum.Html,
                    From = SettingsKeyInfoProvider.GetValue("CMSNoreplyEmailAddress", SiteContext.CurrentSiteName),
                    Recipients = inactiveAttendee.Email,
                    Subject = emailTemplate.TemplateSubject
                };

                EmailSender.SendEmail(SiteContext.CurrentSiteName, message, emailTemplate.TemplateName, emailMacros, true);
            }
        }
    }
}