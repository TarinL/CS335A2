using System.Text;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using A2Template.Models;

namespace A2Template.Helper
{
    public class CalendarOutputFormatter : TextOutputFormatter
    {
        public CalendarOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/calendar; charset=utf-8"));
            SupportedEncodings.Add(Encoding.UTF8);
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context,
            Encoding selectedEncoding)
        {
            var utc = DateTime.UtcNow;
            var nzTime = TimeZoneInfo.FindSystemTimeZoneById("Pacific/Auckland");
            var currentTime = TimeZoneInfo.ConvertTimeFromUtc(utc, nzTime);
            string time = currentTime.ToString("yyyyMMdd'T'HHmmss'Z'");
            
            Event e = (Event)context.Object;
            
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("BEGIN:VCALENDAR");
            sb.AppendLine("VERSION:2.0");
            sb.AppendLine("PRODID:tlov250");
            sb.AppendLine("BEGIN:VEVENT");
            sb.AppendLine($"UID:{e.Id}");
            sb.AppendLine($"DTSTAMP:{time}");
            sb.AppendLine($"DTSTART:{e.Start}");
            sb.AppendLine($"DTEND:{e.End}");
            sb.AppendLine($"SUMMARY:{e.Summary}");
            sb.AppendLine($"DESCRIPTION:{e.Description}");
            sb.AppendLine($"LOCATION:{e.Location}");
            sb.AppendLine("END:VEVENT");
            sb.AppendLine("END:VCALENDAR");
            
            string outString = sb.ToString();
            byte[] bytes = selectedEncoding.GetBytes(outString);
            var response = context.HttpContext.Response.Body;
            return response.WriteAsync(bytes, 0, bytes.Length);
        }
    }
}

