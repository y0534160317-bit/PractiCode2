using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace p2
{
    public class HtmlParser
    {




        public async Task<string> Load(string url)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);
            var html = await response.Content.ReadAsStringAsync();
            return html;
        }

        public HtmlElement ParseHtml(string html)
        {
            return BuildTree(html);
        }

        private HtmlElement BuildTree(string html)
        {
            var regex = new Regex(@"<[^>]+>");
            var matches = regex.Matches(html);

            var root = new HtmlElement { Name = "root" };
            var currentElement = root;

            int lastIndex = 0;
            for (int i = 0; i < matches.Count; i++)
            {
                var match = matches[i];
                var tag = match.Value.Trim();

                // capture text between previous tag end and this tag start
                if (match.Index > lastIndex)
                {
                    var textBetween = html.Substring(lastIndex, match.Index - lastIndex).Trim();
                    if (!string.IsNullOrWhiteSpace(textBetween))
                    {
                        // append or set InnerHtml of current element
                        currentElement.InnerHtml = string.IsNullOrEmpty(currentElement.InnerHtml)
                            ? textBetween
                            : currentElement.InnerHtml + " " + textBetween;
                    }
                }

                lastIndex = match.Index + match.Length;

                if (string.IsNullOrWhiteSpace(tag))
                    continue;

                var tagNameToken = tag.Trim('<', '>').Split(' ')[0];
                var normalizedTagName = tagNameToken.TrimEnd('/');

                // if closing html tag encountered, stop parsing
                if (tagNameToken == "/html")
                {
                    break;
                }

                if (tagNameToken.StartsWith("/"))
                {
                    // closing tag: go up one level if possible
                    if (currentElement.Parent != null)
                    {
                        currentElement = currentElement.Parent;
                    }
                }
                else
                {
                    // opening or self-closing tag
                    var newElement = new HtmlElement
                    {
                        Name = normalizedTagName,
                        Parent = currentElement
                    };

                    // parse attributes
                    var attributes = ParseAttributes(tag);
                    newElement.Attributes.AddRange(attributes.Item1);
                    newElement.Classes.AddRange(attributes.Item2);
                    newElement.Id = attributes.Item3;

                    // detect self-closing: tag ends with "/>" OR tag is listed as self-closing in HtmlHelper
                    bool isSelfClosing = tag.EndsWith("/>") || HtmlHelper.Instance.SelfClosingTags.Contains(normalizedTagName, StringComparer.OrdinalIgnoreCase);

                    currentElement.Children.Add(newElement);

                    if (!isSelfClosing)
                    {
                        // descend into this element
                        currentElement = newElement;
                    }
                }
            }

            // capture any trailing text after last tag
            if (lastIndex < html.Length)
            {
                var tail = html.Substring(lastIndex).Trim();
                if (!string.IsNullOrWhiteSpace(tail))
                {
                    currentElement.InnerHtml = string.IsNullOrEmpty(currentElement.InnerHtml)
                        ? tail
                        : currentElement.InnerHtml + " " + tail;
                }
            }

            return root;
        }

        public static Selector Parse(string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return null;
            var parts = query.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var root = new Selector();
            var current = root;

            foreach (var part in parts)
            {
                // נתח את החלק ללא שימוש ב־regex כתחילת דרך פשוטה
                string working = part;
                // קח tag אם מתחיל באות ולא ב# או .
                if (!working.StartsWith("#") && !working.StartsWith("."))
                {
                    var idx = working.IndexOfAny(new[] { '#', '.' });
                    if (idx >= 0)
                    {
                        current.TagName = working.Substring(0, idx);
                        working = working.Substring(idx);
                    }
                    else
                    {
                        current.TagName = working;
                        working = string.Empty;
                    }
                }

                while (!string.IsNullOrEmpty(working))
                {
                    if (working.StartsWith(\"#\"))
                    {
                        var next = working.IndexOfAny(new[] { '.' });
                        if (next == -1) { current.Id = working.Substring(1); working = string.Empty; }
                        else { current.Id = working.Substring(1, next - 1); working = working.Substring(next); }
                    }
                    else if (working.StartsWith(\".\"))
                    {
                        var next = working.IndexOfAny(new[] { '.', '#' });
                        if (next == -1) { current.Classes.Add(working.Substring(1)); working = string.Empty; }
                        else { current.Classes.Add(working.Substring(1, next - 1)); working = working.Substring(next); }
                    }
                    else break;
                }

                // הכנס סלקטור חדש כבן (לרמה הבאה) אם יש עוד חלקים
                if (parts.Last() != part)
                {
                    var nextSel = new Selector { Parent = current };
                    current.Child = nextSel;
                    current = nextSel;
                }
            }

            return root;
        }
    }
}

















