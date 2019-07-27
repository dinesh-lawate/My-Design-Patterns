using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleBuilder
{
    internal class Program
    {
        //Builder pattern helps us to construct complex object in step by step manner.
        //In dot net we have StringBuilder class which allow us to construct multi-line string.
        //Similarly in below example I have created HTML builder to construct HTML representation.

        public interface IHtmlElement
        {
            List<IHtmlElement> Elements { get; set; }
            string Name { get; set; }
            string Text { get; set; }

            string ToStringImpl(int indent);

            string ToString();
        }

        public class HtmlElement : IHtmlElement
        {
            public string Name { get; set; }
            public string Text { get; set; }
            public List<IHtmlElement> Elements { get; set; } = new List<IHtmlElement>();
            private const int indentSize = 2;

            public HtmlElement(string name, string text = "")
            {
                Name = name;
                Text = text;
            }

            public string ToStringImpl(int indent)
            {
                var sb = new StringBuilder();
                var i = new string(' ', indentSize * indent);
                sb.AppendLine($"{i}<{Name}>");

                if (!string.IsNullOrWhiteSpace(Text))
                {
                    sb.Append(new string(' ', indentSize * indent + 1));
                    sb.AppendLine($"{Text}");
                }

                foreach (var element in Elements)
                {
                    sb.Append(element.ToStringImpl(indent + 1));
                }

                sb.AppendLine($"{i}</{Name}>");
                return sb.ToString();
            }

            public override string ToString()
            {
                return ToStringImpl(0);
            }
        }

        public class HtmlBuilder
        {
            private readonly string rootName;
            private IHtmlElement rootElement;

            public HtmlBuilder(string rootName)
            {
                this.rootName = rootName;
                rootElement = new HtmlElement(rootName);
            }

            public void AddElement(string name, string text = "")
            {
                var element = new HtmlElement(name, text);

                rootElement.Elements.Add(element);
            }

            public void Clear()
            {
                rootElement = new HtmlElement(rootName);
            }

            public override string ToString()
            {
                return rootElement.ToString();
            }
        }

        private static void Main(string[] args)
        {
            var htmlBuilder = new HtmlBuilder("ul");
            htmlBuilder.AddElement("li", "Hello");
            htmlBuilder.AddElement("li", "World!");
            Console.WriteLine(htmlBuilder.ToString());
            Console.ReadKey();
        }
    }
}