using CMS.Core;
using CMS.DataEngine;
using CMS.EmailEngine;
using CMS.MacroEngine;
using CMS.SiteProvider;
using Events;
using Locations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace MVC.Controllers
{
    public class EventRegistrationController : Controller
    {

        private readonly IEventSessionInfoProvider _sessionInfoProvider;
        private readonly IActiveAttendeeInfoProvider _attendeeInfoProvider;
        private readonly ILocationInfoProvider _locationInfoProvider;

        public EventRegistrationController(IEventSessionInfoProvider sessionInfoProvider, IActiveAttendeeInfoProvider attendeeInfoProvider, ILocationInfoProvider locationInfoProvider)
        {
            _sessionInfoProvider = sessionInfoProvider;
            _attendeeInfoProvider = attendeeInfoProvider;
            _locationInfoProvider = locationInfoProvider;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Index(Guid eventGuid, string fname, string midInitial, string lname, string phone, string email, string prnr, string comment)
        {
            var eventLog = Service.Resolve<IEventLogService>();

            try
            {               
                string phonePattern = @"\(?\d{3}\)?[-\.]? *\d{3}[-\.]? *[-\.]?\d{4}";
                Regex phoneRegex = new Regex(phonePattern, RegexOptions.IgnoreCase);

                string emailPattern = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$";
                RegexOptions emailRegexOptions = RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture;
                Regex emailRegex = new Regex(emailPattern, emailRegexOptions);

                //Form validation
                if (String.IsNullOrEmpty(fname))
                {
                    return Json(new { Status = "fail", Message = "First name is required. Please enter your first name." });
                }

                if (String.IsNullOrEmpty(lname))
                {
                    return Json(new { Status = "fail", Message = "Last name is required. Please enter your last name." });
                }

                if (String.IsNullOrEmpty(phone))
                {
                    return Json(new { Status = "fail", Message = "Phone number is required. Please enter your phone number." });
                }

                if (!phoneRegex.IsMatch(phone))
                {
                    return Json(new { Status = "fail", Message = "Please enter a valid phone number in the format XXX-XXX-XXXX" });
                }

                if (String.IsNullOrEmpty(email))
                {
                    return Json(new { Status = "fail", Message = "Email is required. Please enter your email address." });
                }

                if (!emailRegex.IsMatch(email))
                {
                    return Json(new { Status = "fail", Message = "Please enter a valid email address." });
                }

                var session = _sessionInfoProvider.Get().WhereEquals("EventSessionGuid", eventGuid).FirstOrDefault();
                
                if (session == null)
                {
                    eventLog.LogError("EventRegistrationController.cs", "ERROR", $"While processing a user event submission, the event could not be found and returned null for an event with the GUID '{eventGuid}'.");
                    return Json(new { Status = "fail", Message = "An error occurred while processing your request. Please refresh the page and try again. If you continue experiencing issues, please contact us." });
                }

                var attendeeCount = _attendeeInfoProvider.Get().Columns("ParentEventID").WhereEquals("ParentEventID", session.EventSessionID).Count;

                if(session.EventSize - attendeeCount <= 0)
                {
                    return Json(new { Status = "event_full", Message = "Unfortunately, this event ran out of open spots before we could process your registration. There are no longer any available spots open for this event. We apologize for the inconvenience." });
                }

                var attendee = new ActiveAttendeeInfo
                {
                    FirstName = HttpUtility.HtmlEncode(fname),
                    MiddleInitial = HttpUtility.HtmlEncode(midInitial),
                    LastName = HttpUtility.HtmlEncode(lname),
                    Phone = HttpUtility.HtmlEncode(phone),
                    Email = HttpUtility.HtmlEncode(email),
                    PRNR = HttpUtility.HtmlEncode(prnr),
                    Comment = HttpUtility.HtmlEncode(comment),
                    DateAdded = DateTime.Now,
                    ParentEventID = session.EventSessionID,
                };

                _attendeeInfoProvider.Set(attendee);

                SendNotificationEmail(session, attendee);
                SendAutoresponder(session, attendee);

                return Json(new { Status = "success", Message = $"Thank you for your registration! You will receive an email confirming your registration." });
            }
            catch (Exception e)
            {
                eventLog.LogException("EventRegistrationController.cs", "EXCEPTION", e, additionalMessage: "An exception occurred while processing a event registration submission. See exception for more details.");
                return Json(new { Status = "fail", Message = "An error occurred while processing your request. Please refresh the page and try again. If you continue experiencing issues, please contact us." });
            }
        }

        private void SendNotificationEmail(EventSessionInfo session, ActiveAttendeeInfo attendee)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"<strong>A new user has registered for the event '{session.EventName}'</strong><br><br>");
            sb.Append($"<p><strong>First Name: </strong>{attendee.FirstName}</p>");
            sb.Append($"<p><strong>Middle Initial: </strong>{attendee.MiddleInitial}</p>");
            sb.Append($"<p><strong>Last Name: </strong>{attendee.LastName}</p>");
            sb.Append($"<p><strong>Phone Number: </strong>{attendee.Phone}</p>");
            sb.Append($"<p><strong>Email: </strong>{attendee.Email}</p>");
            sb.Append($"<p><strong>PRNR: </strong>{attendee.PRNR}</p>");
            sb.Append($"<p><strong>Comment: </strong>{attendee.Comment}</p>");
            sb.Append($"<p><strong>Registration Date: </strong>{attendee.DateAdded.ToString("MM/dd/yyyy hh:mm tt")}</p>");

            var em = new EmailMessage
            {
                Recipients = session.ContactEmail,
                Body = sb.ToString(),
                From = SettingsKeyInfoProvider.GetValue("CMSNoreplyEmailAddress", SiteContext.CurrentSiteName),
                Subject = $"New User Registration - {session.EventName}"
            };

            EmailSender.SendEmail(em);
        }

        private void SendAutoresponder(EventSessionInfo session, ActiveAttendeeInfo attendee)
        {
            var emailTemplate = EmailTemplateInfo.Provider.Get("EventRegistrationAutoresponder", SiteContext.CurrentSiteID);

            if(emailTemplate != null)
            {
                var sb = new StringBuilder();
                sb.Append("<strong>Thank you for your registration!</strong>");
                sb.Append("<p>This email is to notify you that we have received your registration. See below for the event's information and please reach out to us if any information is incorrect.</p>");
                
                var locationLink = "";
                var locationName = "";
                var location = _locationInfoProvider.Get().WhereEquals("LocationGuid", session.LocationGuid).TopN(1).FirstOrDefault();
                
                if(location != null)
                {
                    var streetAddress = String.IsNullOrEmpty(location.StreetAddress2) ? location.StreetAddress : $"{location.StreetAddress} {location.StreetAddress2}";
                    locationLink = $"https://www.google.com/maps/search/?api=1&query={streetAddress} {location.City}, {location.State} {location.Zip}";
                    locationName = location.LocationName;
                }

                var emailMacros = MacroResolver.GetInstance();
                dynamic model = new
                {
                    EmailContent = sb.ToString(),
                    EventName = session.EventName,
                    FirstName = attendee.FirstName,
                    LastName = attendee.LastName,
                    Phone = attendee.Phone,
                    PRNR = attendee.PRNR,
                    ShowLocation = location == null ? "display: none;" : "",
                    LocationName = locationName,
                    LocationLink = locationLink,
                    EventStartTime = session.EventStart.ToString("M/dd/yyyy @ h:mm tt"),
                    ShowPRNR = String.IsNullOrEmpty(attendee.PRNR) ? "display: none;" : "",
                    TotalCost = session.Cost,
                };

                foreach(var property in model.GetType().GetProperties())
                {
                    emailMacros.SetNamedSourceData(property.Name, property.GetValue(model, null));
                }

                var message = new EmailMessage
                {
                    EmailFormat = EmailFormatEnum.Html,
                    From = SettingsKeyInfoProvider.GetValue("CMSNoreplyEmailAddress", SiteContext.CurrentSiteName),
                    Recipients = attendee.Email,
                    Subject = emailTemplate.TemplateSubject
                };

                EmailSender.SendEmail(SiteContext.CurrentSiteName, message, emailTemplate.TemplateName, emailMacros, false);
            }
        }
    }
}
