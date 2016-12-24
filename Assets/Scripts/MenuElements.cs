using UnityEngine;
using System.Xml.Linq;
using System;

namespace Game.MenuElements {
    public static class _Summary {
        public struct Item {
            public string Kind;
            public Transform.Element2Data<MenuElement> E2D;
        }
        public static Item[] List = new Item[] {
            new Item {
                Kind = "Image",
                E2D = Image.E2D,
            },
            new Item {
                Kind = "BoxButton",
                E2D = BoxButton.E2D,
            },
            new Item {
                Kind = "ClickArea",
                E2D = ClickArea.E2D,
            },
        };
    }
    public class Image : MenuElement {
        public static new Image E2D(XElement e) { return new Image(e); }

        public string ImagePath;
        public int ImageWidth;
        public int ImageHeight;
        public Rectangle Region;
        public float Depth;

        private GameObject Entity;

        public Image() { }
        public Image(XElement e) {
            Load(e);
        }
        public override void Start() {
            Entity = new GameObject("Image");
            SpriteRenderer srend = Entity.AddComponent<SpriteRenderer>();
            byte[] data = GameManager.GetImage(ImagePath);
            Texture2D texture = new Texture2D(ImageWidth, ImageHeight);
            texture.LoadImage(data);
            srend.sprite = Sprite.Create(
                texture,
                new Rect(0, 0, ImageWidth, ImageHeight),
                new Vector2(0, 0),
                1000);
            float
                xpos = Region.x1 * GameManager.CurrentScene.BrickXAmount,
                ypos = Region.y1 * GameManager.CurrentScene.BrickYAmount,
                xscale = ((Region.x2 - Region.x1) * GameManager.CurrentScene.BrickXAmount) / (ImageWidth / 1000f),
                yscale = ((Region.y2 - Region.y1) * GameManager.CurrentScene.BrickYAmount) / (ImageHeight / 1000f);
            Entity.transform.localPosition = new Vector3(xpos, ypos, Depth);
            Entity.transform.localScale = new Vector3(xscale, yscale, 1);
        }
        public override void End() {
            GameObject.Destroy(Entity);
        }

        public override void Load(XElement e) {
            ImagePath = BasicTranslation.StringE2D(e.Element("ImagePath"));
            ImageWidth = BasicTranslation.IntE2D(e.Element("ImageWidth"));
            ImageHeight = BasicTranslation.IntE2D(e.Element("ImageHeight"));
            Region = Rectangle.E2D(e.Element("Region"));
            Depth = BasicTranslation.FloatE2D(e.Element("Depth"));
        }
        public override XElement Unload(string n) {
            XElement e = new XElement(n, new XAttribute("Kind", "Image"));
            e.Add(BasicTranslation.StringD2E("ImagePath", ImagePath));
            e.Add(BasicTranslation.IntD2E("ImageWidth", ImageWidth));
            e.Add(BasicTranslation.IntD2E("ImageHeight", ImageHeight));
            e.Add(Rectangle.D2E("Region", Region));
            e.Add(BasicTranslation.FloatD2E("Depth", Depth));
            return e;
        }
    }
    public class BoxButton : MenuElement {
        public static new BoxButton E2D(XElement e) { return new BoxButton(e); }

        public string OffImagePath;
        public string OnImagePath;
        public int ImageWidth;
        public int ImageHeight;
        public Rectangle Region;
        public Rectangle CollideRegion;
        public float Depth;
        public string Event;

        private Sprite OffSprite;
        private Sprite OnSprite;
        private GameObject Entity;
        private BoxCollider2D bc2D;
        private SpriteRenderer srend;
        private Script_UI script;
        private float xpos, ypos, xscale, yscale;
        private bool pressed;

        public BoxButton() { }
        public BoxButton(XElement e) {
            Load(e);
            //Off
            byte[] data = GameManager.GetImage(OffImagePath);
            Texture2D texture = new Texture2D(ImageWidth, ImageHeight);
            texture.LoadImage(data);
            OffSprite = Sprite.Create(
                texture,
                new Rect(0, 0, ImageWidth, ImageHeight),
                new Vector2(0.5f, 0.5f),
                1000);
            //On
            data = GameManager.GetImage(OnImagePath);
            texture = new Texture2D(ImageWidth, ImageHeight);
            texture.LoadImage(data);
            OnSprite = Sprite.Create(
                texture,
                new Rect(0, 0, ImageWidth, ImageHeight),
                new Vector2(0.5f, 0.5f),
                1000);
        }
        public override void Start() {
            Entity = new GameObject("Button");
            srend = Entity.AddComponent<SpriteRenderer>();
            bc2D = Entity.AddComponent<BoxCollider2D>();
            script = Entity.AddComponent<Script_UI>();
            xpos = (Region.x1 + Region.x2) * GameManager.CurrentScene.BrickXAmount / 2;
            ypos = (Region.y1 + Region.y2) * GameManager.CurrentScene.BrickYAmount / 2;
            xscale = ((Region.x2 - Region.x1) * GameManager.CurrentScene.BrickXAmount) / (ImageWidth / 1000f);
            yscale = ((Region.y2 - Region.y1) * GameManager.CurrentScene.BrickYAmount) / (ImageHeight / 1000f);
            Entity.transform.localPosition = new Vector3(xpos, ypos, Depth);
            Entity.transform.localScale = new Vector3(xscale, yscale, 1);
            bc2D.isTrigger = true;
            bc2D.offset = new Vector2(
                CollideRegion.x1 * GameManager.CurrentScene.BrickXAmount / xscale,
                CollideRegion.y1 * GameManager.CurrentScene.BrickYAmount / yscale);
            bc2D.size = new Vector2(
                CollideRegion.x2 * GameManager.CurrentScene.BrickXAmount / xscale,
                CollideRegion.y2 * GameManager.CurrentScene.BrickYAmount / yscale);
            pressed = false;
            script.mouseDown = mouseDown;
            script.mouseExit = mouseExit;
            script.mouseUp = mouseUp;
            srend.sprite = OffSprite;
        }
        private void mouseDown() {
            pressed = true;
            srend.sprite = OnSprite;
        }
        private void mouseExit() {
            pressed = false;
            srend.sprite = OffSprite;
        }
        private void mouseUp() {
            if (pressed)
                GameManager.MenuActions.ApplyEvent(Event);
        }
        public override void End() {
            GameObject.Destroy(Entity);
        }

        public override void Load(XElement e) {
            OffImagePath = BasicTranslation.StringE2D(e.Element("OffImagePath"));
            OnImagePath = BasicTranslation.StringE2D(e.Element("OnImagePath"));
            ImageWidth = BasicTranslation.IntE2D(e.Element("ImageWidth"));
            ImageHeight = BasicTranslation.IntE2D(e.Element("ImageHeight"));
            Region = Rectangle.E2D(e.Element("Region"));
            Depth = BasicTranslation.FloatE2D(e.Element("Depth"));
            CollideRegion = Rectangle.E2D(e.Element("CollideRegion"));
            Event = BasicTranslation.StringE2D(e.Element("Event"));
        }
        public override XElement Unload(string n) {
            XElement e = new XElement(n, new XAttribute("Kind", "BoxButton"));
            e.Add(BasicTranslation.StringD2E("OffImagePath", OffImagePath));
            e.Add(BasicTranslation.StringD2E("OnImagePath", OnImagePath));
            e.Add(BasicTranslation.IntD2E("ImageWidth", ImageWidth));
            e.Add(BasicTranslation.IntD2E("ImageHeight", ImageHeight));
            e.Add(Rectangle.D2E("Region", Region));
            e.Add(BasicTranslation.FloatD2E("Depth", Depth));
            e.Add(Rectangle.D2E("CollideRegion", CollideRegion));
            e.Add(BasicTranslation.StringD2E("Event", Event));
            return e;
        }
    }
    public class ClickArea : MenuElement {
        public static new ClickArea E2D(XElement e) { return new ClickArea(e); }

        public Rectangle CollideRegion;
        public float Depth;
        public string Event;

        private GameObject Entity;
        private BoxCollider2D bc2D;
        private Script_UI script;
        private float xpos, ypos;
        private bool pressed;

        public ClickArea() { }
        public ClickArea(XElement e) { Load(e); }
        public override void Start() {
            Entity = new GameObject("ClickArea");
            bc2D = Entity.AddComponent<BoxCollider2D>();
            script = Entity.AddComponent<Script_UI>();
            xpos = (CollideRegion.x1 + CollideRegion.x2) * GameManager.CurrentScene.BrickXAmount / 2;
            ypos = (CollideRegion.y1 + CollideRegion.y2) * GameManager.CurrentScene.BrickYAmount / 2;
            Entity.transform.localPosition = new Vector3(xpos, ypos, Depth);
            bc2D.isTrigger = true;
            bc2D.offset = Vector2.zero;
            bc2D.size = new Vector2(
                (CollideRegion.x2 - CollideRegion.x1) * GameManager.CurrentScene.BrickXAmount,
                (CollideRegion.y2 - CollideRegion.y1) * GameManager.CurrentScene.BrickYAmount);
            pressed = false;
            script.mouseDown = mouseDown;
            script.mouseExit = mouseExit;
            script.mouseUp = mouseUp;
        }
        private void mouseDown() {
            pressed = true;
        }
        private void mouseExit() {
            pressed = false;
        }
        private void mouseUp() {
            if (pressed)
                GameManager.MenuActions.ApplyEvent(Event);
        }
        public override void End() {
            GameObject.Destroy(Entity);
        }

        public override void Load(XElement e) {
            Depth = BasicTranslation.FloatE2D(e.Element("Depth"));
            CollideRegion = Rectangle.E2D(e.Element("CollideRegion"));
            Event = BasicTranslation.StringE2D(e.Element("Event"));
        }
        public override XElement Unload(string n) {
            XElement e = new XElement(n, new XAttribute("Kind", "ClickArea"));
            e.Add(BasicTranslation.FloatD2E("Depth", Depth));
            e.Add(Rectangle.D2E("CollideRegion", CollideRegion));
            e.Add(BasicTranslation.StringD2E("Event", Event));
            return e;
        }
    }
}