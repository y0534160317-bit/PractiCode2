using System.Collections.Generic; // אם לא קיים כבר

namespace p2
{
    public class HtmlElement
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Attributes { get; set; }
        public List<string> Classes { get; set; }
        public string InnerHtml { get; set; }
        public HtmlElement Parent { get; set; }
        public List<HtmlElement> Children { get; set; }

        public HtmlElement()
        {
            Attributes = new List<string>();
            Classes = new List<string>();
            Children = new List<HtmlElement>();
        }

        public IEnumerable<HtmlElement> Descendants()
        {
            var q = new Queue<HtmlElement>();
            q.Enqueue(this);

            while (q.Count > 0)
            {
                var cur = q.Dequeue();
                yield return cur;

                if (cur.Children != null)
                {
                    foreach (var child in cur.Children)
                        q.Enqueue(child);
                }
            }
        }

        public IEnumerable<HtmlElement> Ancestors()
        {
            var node = this.Parent;
            while (node != null)
            {
                yield return node;
                node = node.Parent;
            }
        }
    }
}