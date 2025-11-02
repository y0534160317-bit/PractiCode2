using System;

public class HTMLSerializer
{
	public HTMLSerializer()
	{
		return BuildTree(html);
    }

}


==========================

https://github.com/t0548572430-sys/practicode2.git

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace task2
{
    internal class HtmlSerializer
    {
        /// <summary>
        /// מחזיר מחרוזת HTML מלאה של העץ הנתון
        /// </summary>
        public static string Serialize(HtmlElement element)
        {
            var sb = new StringBuilder();
            SerializeElement(element, sb, 0);
            return sb.ToString();
        }

        /// <summary>
        /// פונקציה רקורסיבית שממירה אלמנט HTML + כל ילדיו למחרוזת HTML
        /// </summary>
        private static void SerializeElement(HtmlElement element, StringBuilder sb, int indent)
        {
            string indentStr = new string(' ', indent * 2); // ריווח לפי רמת ההיררכיה
            sb.Append($"{indentStr}<{element.Name}");

            // אם יש id
            if (!string.IsNullOrEmpty(element.Id))
                sb.Append($" id=\"{element.Id}\"");

            // אם יש classים
            if (element.Classes.Count > 0)
                sb.Append($" class=\"{string.Join(" ", element.Classes)}\"");

            // אם יש attributes נוספים
            if (element.Attributes.Count > 0)
                sb.Append(" " + string.Join(" ", element.Attributes));

            sb.Append(">");

            // אם יש InnerHtml כתוב בתוך התגית
            if (!string.IsNullOrWhiteSpace(element.InnerHtml))
                sb.Append(element.InnerHtml);

            // רקורסיה על כל הילדים
            if (element.Children.Count > 0)
            {
                sb.AppendLine();
                foreach (var child in element.Children)
                {
                    SerializeElement(child, sb, indent + 1);
                }
                sb.Append($"{indentStr}");
            }

            sb.AppendLine($"</{element.Name}>");
        }
    }
}


