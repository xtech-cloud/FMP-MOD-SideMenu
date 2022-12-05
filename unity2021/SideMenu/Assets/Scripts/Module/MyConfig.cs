
using System.Xml.Serialization;

namespace XTC.FMP.MOD.SideMenu.LIB.Unity
{
    /// <summary>
    /// 配置类
    /// </summary>
    public class MyConfig : MyConfigBase
    {
        public class Padding
        {
            [XmlAttribute("top")]
            public int top { get; set; } = 0;
            [XmlAttribute("bottom")]
            public int bottom { get; set; } = 0;
            [XmlAttribute("left")]
            public int left { get; set; } = 0;
            [XmlAttribute("right")]
            public int right { get; set; } = 0;
        }

        public class Item : UiElement
        {
            [XmlAttribute("name")]
            public string name { get; set; } = "";

            [XmlArray("OnSubjects"), XmlArrayItem("Subject")]
            public Subject[] onSubjects { get; set; } = new Subject[0];
            [XmlArray("OffSubjects"), XmlArrayItem("Subject")]
            public Subject[] offSubjects { get; set; } = new Subject[0];
        }

        public class Viewport
        {
            [XmlElement("Padding")]
            public Padding padding { get; set; } = new Padding();
        }

        public class Menu : UiElement
        {
            [XmlElement("Viewport")]
            public Viewport viewport { get; set; } = new Viewport();
        }

        public class Page
        {
            [XmlAttribute("image")]
            public string image { get; set; } = "";
            [XmlElement("Viewport")]
            public Viewport viewport { get; set; } = new Viewport();
        }


        public class Style
        {
            [XmlAttribute("name")]
            public string name { get; set; } = "";

            [XmlAttribute("position")]
            public string position { get; set; } = "left";

            [XmlElement("Menu")]
            public Menu menu { get; set; } = new Menu();
            [XmlElement("Cursor")]
            public UiElement cursor { get; set; } = new UiElement();
            [XmlArray("ItemS"), XmlArrayItem("Item")]
            public Item[] itemS { get; set; } = new Item[0];
            [XmlElement("Page")]
            public Page page { get; set; } = new Page();
        }


        [XmlArray("Styles"), XmlArrayItem("Style")]
        public Style[] styles { get; set; } = new Style[0];
    }
}

