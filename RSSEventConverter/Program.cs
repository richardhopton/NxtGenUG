using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using System.Text;
using RSSEventConverter.Properties;
using System.Collections;

namespace RSSEventConverter
{
    static class Program
    {
        static void AppendNonFormattedLine(this StringBuilder sb, String line)
        {
            sb.AppendFormattedLine("{0}", line);
        }

        static void AppendFormattedLine(this StringBuilder sb, String format, params Object[] args)
        {
            String line = String.Format(format, args);
            String upperLine = line.ToUpperInvariant();
            //tidy up HTML tags present in rss feed
            while (upperLine.Contains("<BR/>"))
            {
                Int32 brLocation = upperLine.IndexOf("<BR/>");
                line = String.Format("{0}\n{1}", line.Remove(brLocation), line.Substring(brLocation + 5));
                upperLine = line.ToUpperInvariant();
            }
            while (upperLine.Contains("<B>"))
            {
                Int32 brLocation = upperLine.IndexOf("<B>");
                line = String.Format("{0}{1}", line.Remove(brLocation), line.Substring(brLocation + 3));
                upperLine = line.ToUpperInvariant();
            }
            while (upperLine.Contains("</B>"))
            {
                Int32 brLocation = upperLine.IndexOf("</B>");
                line = String.Format("{0}{1}", line.Remove(brLocation), line.Substring(brLocation + 4));
                upperLine = line.ToUpperInvariant();
            }
            while (line.Contains("\n\n\n"))
            {
                line = line.Replace("\n\n\n", "\n\n");
            }
            line = line.TrimEnd('\n').Replace("\n", @"\n").Replace(",", @"\,");
            while (line.Length > 75)
            {
                sb.AppendLine(line.Remove(75));
                sb.Append(' ');
                line = line.Substring(75);
            }
            sb.AppendLine(line);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            String[] skipLocations = Settings.Default.SkipLocations.ToLower().Split(';');
            String fileName = String.Format("NxtGenUG_Events_{0}.ics",DateTime.Now.ToString("yyyyMMdd_HHmmss"));
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            saveDialog.Filter = "ics files (*.ics)|*.ics|All files (*.*)|*.*";    
            saveDialog.FileName = fileName;
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = saveDialog.FileName;
                try
                {
                    XDocument feed = XDocument.Load(@"http://www.nxtgenug.net/RSS/RSSEvents.aspx");
                    var posts = from item in feed.Descendants("item")
                                select new
                                {
                                    Title = item.Element("title").Value,
                                    Description = item.Element("description").Value,
                                    Link = item.Element("link").Value,
                                    Date = DateTime.Parse(item.Element("pubDate").Value)
                                };

                    StringBuilder sb = new StringBuilder();

                    sb.AppendNonFormattedLine("BEGIN:VCALENDAR");
                    sb.AppendNonFormattedLine("PRODID:-//NxtGenUG//Calendar//EN");
                    sb.AppendNonFormattedLine("VERSION:2.0");
                    sb.AppendNonFormattedLine("CALSCALE:GREGORIAN");

                    sb.AppendNonFormattedLine("METHOD:PUBLISH");
                    
                    sb.AppendNonFormattedLine("X-WR-CALNAME:events@nxtgenug.net");
                    sb.AppendNonFormattedLine("X-WR-TIMEZONE:Europe/London");

                    sb.AppendNonFormattedLine("BEGIN:VTIMEZONE");
                    sb.AppendNonFormattedLine("TZID:UK Time");
                    sb.AppendNonFormattedLine("BEGIN:STANDARD");
                    sb.AppendNonFormattedLine("DTSTART:20081026T020000");
                    sb.AppendNonFormattedLine("RRULE:FREQ=YEARLY;BYDAY=-1SU;BYMONTH=10");
                    sb.AppendNonFormattedLine("TZOFFSETFROM:+0100");
                    sb.AppendNonFormattedLine("TZOFFSETTO:+0000");
                    sb.AppendNonFormattedLine("END:STANDARD");
                    sb.AppendNonFormattedLine("BEGIN:DAYLIGHT");
                    sb.AppendNonFormattedLine("DTSTART:20080330T010000");
                    sb.AppendNonFormattedLine("RRULE:FREQ=YEARLY;BYDAY=-1SU;BYMONTH=3");
                    sb.AppendNonFormattedLine("TZOFFSETFROM:+0000");
                    sb.AppendNonFormattedLine("TZOFFSETTO:+0100");
                    sb.AppendNonFormattedLine("END:DAYLIGHT");
                    sb.AppendNonFormattedLine("END:VTIMEZONE");

                    foreach (var post in posts)
                    {
                        String location = String.Empty;
                        if (post.Title.StartsWith("NxtGenUG ") && post.Title.Contains(":"))
                        {
                            location = post.Title.Remove(post.Title.IndexOf(':')-1).Substring(9);
                        }
                        if (!skipLocations.Contains(location.ToLower()))
                        {
                            sb.AppendNonFormattedLine("BEGIN:VEVENT");
                            sb.AppendFormattedLine("SUMMARY:{0}", post.Title);

                            sb.AppendFormattedLine("LOCATION:{0}", location);
                            DateTime date = post.Date;
                            if (post.Date.IsDaylightSavingTime())
                            {
                                date = post.Date.AddHours(-1);
                            }

                            sb.AppendFormattedLine("DTSTART;TZID=UK Time:{0}", date.ToString("yyyyMMddTHHmmss"));
                            sb.AppendFormattedLine("DTEND;TZID=UK Time:{0}", date.AddHours(2).ToString("yyyyMMddTHHmmss"));
                            sb.AppendFormattedLine("DTSTAMP:{0}", DateTime.Now.ToString("yyyyMMddTHHmmss"));
                            String eventId = post.Link.Substring(post.Link.LastIndexOf('=') + 1);
                            sb.AppendFormattedLine("UID:{0}_{1}@nxtgenug.net", location.ToLower(), eventId);

                            sb.AppendFormattedLine("DESCRIPTION:{0}\n\n{1}", post.Description, post.Link);
                            sb.AppendNonFormattedLine("BEGIN:VALARM");
                            sb.AppendNonFormattedLine("ACTION:DISPLAY");
                            sb.AppendNonFormattedLine("DESCRIPTION:Reminder");
                            sb.AppendNonFormattedLine("TRIGGER:-PT720M");
                            sb.AppendNonFormattedLine("END:VALARM");
                            sb.AppendNonFormattedLine("END:VEVENT");
                        }
                    }
                    sb.AppendNonFormattedLine("END:VCALENDAR");

                    using (StreamWriter writer = new StreamWriter(fileName))
                    {
                        writer.Write(sb.ToString());
                        writer.Close();
                    }
                    MessageBox.Show("Convertion Complete");
                }
                catch (Exception e)
                {
                    MessageBox.Show(String.Format("Convertion Failed : {0}", e.Message));
                }
            }
        }
    }
}
