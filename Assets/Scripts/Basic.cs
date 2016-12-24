using System.Collections.Generic;
using System.Xml.Linq;

namespace Game {
    public interface XMLable {
        void Load(XDocument XML);
        XDocument Unload();
    }
    public interface XMLNodeable {
        void Load(XElement Node);
        XElement Unload(string Name);
    }
    public struct ScenePosition {
        public static ScenePosition E2D(XElement e) {
            return new ScenePosition(
                BasicTranslation.IntE2D(e.Element("x")),
                BasicTranslation.IntE2D(e.Element("y"))
            );
        }
        public static XElement D2E(string n, ScenePosition p) {
            return new XElement(
                n,
                BasicTranslation.IntD2E("x", p.x),
                BasicTranslation.IntD2E("y", p.y)
            );
        }

        public int x, y;
        public ScenePosition(int _x, int _y) { x = _x; y = _y; }
    }
    public struct Point {
        public static Point E2D(XElement e) {
            return new Point(
                BasicTranslation.FloatE2D(e.Element("x")),
                BasicTranslation.FloatE2D(e.Element("y"))
            );
        }
        public static XElement D2E(string n, Point p) {
            return new XElement(
                n,
                BasicTranslation.FloatD2E("x",p.x),
                BasicTranslation.FloatD2E("y",p.y)
            );
        }

        public float x, y;
        public Point(float _x, float _y) { x = _x; y = _y; }
    }
    public struct Rectangle {
        public static Rectangle E2D(XElement e) {
            return new Rectangle(
                BasicTranslation.FloatE2D(e.Element("x1")),
                BasicTranslation.FloatE2D(e.Element("y1")),
                BasicTranslation.FloatE2D(e.Element("x2")),
                BasicTranslation.FloatE2D(e.Element("y2"))
            );
        }
        public static XElement D2E(string n, Rectangle r) {
            return new XElement(
                n,
                BasicTranslation.FloatD2E("x1", r.x1),
                BasicTranslation.FloatD2E("y1", r.y1),
                BasicTranslation.FloatD2E("x2", r.x2),
                BasicTranslation.FloatD2E("y2", r.y2)
            );
        }

        public float x1, y1, x2, y2;
        public Rectangle(float _x1, float _y1, float _x2, float _y2) {
            x1 = _x1; y1 = _y1;
            x2 = _x2; y2 = _y2;
        }
        public bool IsZero() { return x1 == 0 && y1 == 0 && x2 == 0 && y2 == 0; }
    }
    public class Circle {
        public static Circle E2D(XElement e) {
            return new Circle(
                BasicTranslation.FloatE2D(e.Element("x")),
                BasicTranslation.FloatE2D(e.Element("y")),
                BasicTranslation.FloatE2D(e.Element("r"))
            );
        }
        public static XElement D2E(string n, Circle r) {
            return new XElement(
                n,
                BasicTranslation.FloatD2E("x", r.x),
                BasicTranslation.FloatD2E("y", r.y),
                BasicTranslation.FloatD2E("r", r.r)
            );
        }

        public float x, y, r;
        public Circle(float _x, float _y, float _r) {
            x = _x;
            y = _y;
            r = _r;
        }
    }
    public static class BasicTranslation {
        //string
        public static string StringE2D(XElement e) { return e.Value; }
        public static XElement StringD2E(string n, string d) { return new XElement(n, d); }
        public static string StringA2D(XAttribute a) { return a.Value; }
        public static XAttribute StringD2A(string n, string d) { return new XAttribute(n, d); }
        //int
        public static int IntE2D(XElement e) { return int.Parse(e.Value); }
        public static XElement IntD2E(string n, int d) { return new XElement(n, d.ToString()); }
        public static int IntA2D(XAttribute a) { return int.Parse(a.Value); }
        public static XAttribute IntD2A(string n, int d) { return new XAttribute(n, d.ToString()); }
        //float
        public static float FloatE2D(XElement e) { return float.Parse(e.Value); }
        public static XElement FloatD2E(string n, float d) { return new XElement(n, d.ToString()); }
        public static float FloatA2D(XAttribute a) { return float.Parse(a.Value); }
        public static XAttribute FloatD2A(string n, float d) { return new XAttribute(n, d.ToString()); }
        //char
        public static char CharE2D(XElement e) { return char.Parse(e.Value); }
        public static XElement CharD2E(string n, char d) { return new XElement(n, d.ToString()); }
        public static char CharA2D(XAttribute a) { return char.Parse(a.Value); }
        public static XAttribute CharD2A(string n, char d) { return new XAttribute(n, d.ToString()); }
        //bool
        public static bool BoolE2D(XElement e) { return bool.Parse(e.Value); }
        public static XElement BoolD2E(string n, bool d) { return new XElement(n, d.ToString()); }
        public static bool BoolA2D(XAttribute a) { return bool.Parse(a.Value); }
        public static XAttribute BoolD2A(string n, bool d) { return new XAttribute(n, d.ToString()); }
    }
    public static class Transform {
        public delegate T Element2Data<T>(XElement Element);
        public delegate XElement Data2Element<T>(string Name, T Data);
        public delegate T Attribute2Data<T>(XAttribute Element);
        public delegate XAttribute Data2Attribute<T>(string Name, T Data);

        public static List<T> TranslateList<T>(XElement Element, Element2Data<T> Translation) {
            List<T> lis = new List<T>();
            foreach (var item in Element.Elements("Item"))
                lis.Add(Translation(item));
            return lis;
        }
        public static XElement TranslateList<T>(string Name, Data2Element<T> Translation, List<T> List) {
            XElement Element = new XElement(Name);
            foreach (var item in List)
                Element.Add(Translation("Item", item));
            return Element;
        }
        public static Dictionary<TKey, TValue> TranslateDic<TKey, TValue>(
            XElement Element,
            Attribute2Data<TKey> KeyTranslation,
            Element2Data<TValue> ValueTranslation
            ) {
            Dictionary<TKey, TValue> Dic = new Dictionary<TKey, TValue>();
            foreach (var item in Element.Elements("Item"))
                Dic.Add(
                    KeyTranslation(item.Attribute("Key")),
                    ValueTranslation(item)
                );
            return Dic;
        }
        public static XElement TranslateDic<TKey, TValue>(
            string Name,
            Data2Attribute<TKey> KeyTranslation,
            Data2Element<TValue> ValueTranslation,
            Dictionary<TKey, TValue> Dic
            ) {
            XElement Element = new XElement(Name);
            foreach (var item in Dic) {
                XElement element = ValueTranslation("Item", item.Value);
                element.Add(KeyTranslation("Key", item.Key));
                Element.Add(element);
            }
            return Element;
        }
    }
}