using System;
using System.Collections.Generic;
using System.Text;

namespace FluentBuilder
{
    internal class Program
    {
        //In below example I have implemented Builder pattern with Fluent API so we can create complex HTML representation.

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

            public HtmlElement AddElement(string name, string text = "", HtmlElement addToThisElement = null)
            {
                var element = new HtmlElement(name, text);

                if (addToThisElement != null)
                {
                    addToThisElement.Elements.Add(element);
                }
                else
                {
                    rootElement.Elements.Add(element);
                }

                return element;
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
            var htmlBuilder = new HtmlBuilder("body");
            var ulElement = htmlBuilder.AddElement("ul");
            htmlBuilder.AddElement("li", "Hello", ulElement);
            htmlBuilder.AddElement("li", "World!", ulElement);
            Console.WriteLine(htmlBuilder.ToString());
            Console.ReadKey();
        }
    }
}