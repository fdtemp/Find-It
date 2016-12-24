using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

namespace Game {
    public class GameInfo : XMLable {
        public string Name;
        public List<string> ScenePaths;
        public string BrickPath;
        public string LivingPath;
        public string MenuPath;

        public GameInfo() { }
        public GameInfo(XDocument XML) { Load(XML); }

        public void Load(XDocument XML) {
            XElement root = XML.Root;
            ScenePaths = Transform.TranslateList(
                root.Element("ScenePaths"),
                BasicTranslation.StringE2D);
            BrickPath = BasicTranslation.StringE2D(root.Element("BrickPath"));
            LivingPath = BasicTranslation.StringE2D(root.Element("LivingPath"));
            MenuPath = BasicTranslation.StringE2D(root.Element("MenuPath"));
        }
        public XDocument Unload() {
            XDocument XML = new XDocument(new XElement("Root"));
            XElement root = XML.Root;
            root.Add(Transform.TranslateList(
                "ScenePaths",
                BasicTranslation.StringD2E,
                ScenePaths));
            root.Add(BasicTranslation.StringD2E("BrickPath", BrickPath));
            root.Add(BasicTranslation.StringD2E("LivingPath", LivingPath));
            root.Add(BasicTranslation.StringD2E("MenuPath", MenuPath));
            return XML;
        }
    }
    public class Scene : XMLable {
        public const int PlayerBullet = 0;
        public const int MonsterBullet = 1;
        public const int FilmBullet = 2;

        public string Name;
        public int BrickXAmount;
        public int BrickYAmount;
        public Dictionary<char, string> BrickDefine;
        public string BrickMap;
        public Dictionary<string, Stage> Stages;
        public string BGImagePath;
        public int BGImageWidth;
        public int BGImageHeight;

        public GameObject[] Bounds;
        public GameObject BG;
        public Brick[,] Bricks;
        public Stage CurrentStage;
        public Stage _CurrentStage;
        public List<Living> Monsters;
        public List<Vector2> _Monsters;
        public List<GameObject> Bullets;
        public List<Vector2> _Bullets;
        public List<Living> NPCs;
        public float RecordTime;
        public float GetTime() { return RecordTime; }

        public Scene() { }
        public Scene(XDocument XML) {
            Load(XML);
            RecordTime = 0;
            CurrentStage = Stages["Main"];
            //brick
            Bricks = new Brick[BrickYAmount, BrickXAmount];
            int p = 0;
            for (int i = BrickYAmount - 1; i >= 0; i--) {
                while (BrickMap[p++] != '+') ;
                for (int j = 0; j < BrickXAmount; j++)
                    if (BrickMap[p] == ' ') {
                        Bricks[i, j] = null;
                        p++;
                    } else {
                        Bricks[i, j] = GameManager.BrickDic[BrickDefine[BrickMap[p++]]].Clone();
                        Bricks[i, j].SetPosition(new ScenePosition(j, i));
                        Bricks[i, j].Entity.SetActive(false);
                    }
            }
            //background
            byte[] data = GameManager.GetImage(BGImagePath);
            Texture2D texture = new Texture2D(BGImageWidth, BGImageHeight);
            texture.LoadImage(data);
            BG = new GameObject("BG");
            SpriteRenderer rend = BG.AddComponent<SpriteRenderer>();
            rend.sprite = Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0, 0),
                BGImageWidth);
            BG.transform.localPosition = new Vector3(0, 0, 10);
            BG.transform.localScale = new Vector3(
                BrickXAmount,
                BrickYAmount/((float)BGImageHeight/BGImageWidth)
                );
            BG.SetActive(false);
            //bound
            Bounds = new GameObject[4];
            for (int i = 0; i < 4; i++)
                Bounds[i] = new GameObject("Bound");
            BoxCollider2D bc2D;
            Bounds[0].transform.localPosition = new Vector3(BrickXAmount / 2f, -1);
            bc2D = Bounds[0].AddComponent<BoxCollider2D>();
            bc2D.size = new Vector2(BrickXAmount, 2);
            Bounds[1].transform.localPosition = new Vector3(-1, BrickYAmount / 2f);
            bc2D = Bounds[1].AddComponent<BoxCollider2D>();
            bc2D.size = new Vector2(2, BrickYAmount);
            Bounds[2].transform.localPosition = new Vector3(BrickXAmount + 1, BrickYAmount / 2f);
            bc2D = Bounds[2].AddComponent<BoxCollider2D>();
            bc2D.size = new Vector2(2, BrickYAmount);
            Bounds[3].transform.localPosition = new Vector3(BrickXAmount / 2f, BrickYAmount + 1);
            bc2D = Bounds[3].AddComponent<BoxCollider2D>();
            bc2D.size = new Vector2(BrickXAmount, 2);
            for (int i = 0; i < 4; i++)
                Bounds[i].SetActive(false);
            //others
            Monsters = new List<Living>();
            NPCs = new List<Living>();
            Bullets = new List<GameObject>();
        }
        public void Start() {
            GameManager.Camera.orthographicSize = (float)BrickYAmount / 2;
            GameManager.Camera.gameObject.transform.localPosition = new Vector3(
                (float)BrickXAmount / 2,
                (float)BrickYAmount / 2,
                -10);
            BG.SetActive(true);
            for (int i = 0; i < BrickYAmount; i++)
                for (int j = 0; j < BrickXAmount; j++)
                    if (Bricks[i, j] != null)
                        Bricks[i, j].Entity.SetActive(true);
            for (int i = 0; i < 4; i++)
                Bounds[i].SetActive(true);
            foreach (var liv in Monsters) liv.Entity.SetActive(true);
            foreach (var liv in NPCs) liv.Entity.SetActive(true);
            foreach (var go in Bullets) go.SetActive(true);
            CurrentStage.Start(this);
        }
        public void Update() {
            
            List<GameObject> lis = new List<GameObject>();
            for (int i = 0; i < Bullets.Count; i++) {
                GameObject bullet = Bullets[i];
                if (bullet.GetComponent<Script_Bullet>().State == Script_Bullet.FINISHED)
                    GameObject.Destroy(bullet);
                else
                    lis.Add(bullet);
            }
            foreach (var liv in Monsters)
                if (liv.HP < 0)
                    liv.Entity.SetActive(false);
            Bullets = lis;
            if (CurrentStage != null) {
                RecordTime += Time.deltaTime;
                CurrentStage.Update();
            }
        }
        public void Pause() {
            _CurrentStage = CurrentStage;
            CurrentStage = null;
            _Bullets = new List<Vector2>();
            foreach (var b in Bullets) {
                b.GetComponent<Script_Bullet>().enabled = false;
                _Bullets.Add(b.GetComponent<Rigidbody2D>().velocity);
                b.GetComponent<Rigidbody2D>().Sleep();
            }
            _Monsters = new List<Vector2>();
            foreach (var m in Monsters) {
                m.Entity.GetComponent<Script_Monster>().enabled = false;
                _Monsters.Add(m.rb2D.velocity);
                m.rb2D.Sleep();
            }
        }
        public void Resume() {
            CurrentStage = _CurrentStage;
            int p = 0;
            foreach (var b in Bullets) {
                b.GetComponent<Script_Bullet>().enabled = true;
                b.GetComponent<Rigidbody2D>().WakeUp();
                b.GetComponent<Rigidbody2D>().velocity = _Bullets[p++];
            }
            p = 0;
            foreach (var m in Monsters) {
                m.Entity.GetComponent<Script_Monster>().enabled = true;
                m.rb2D.WakeUp();
                m.rb2D.velocity = _Monsters[p++];
            }
        }
        public void End() {
            BG.SetActive(false);
            foreach (var liv in Monsters) liv.Entity.SetActive(false);
            foreach (var liv in NPCs) liv.Entity.SetActive(false);
            foreach (var go in Bullets) go.SetActive(false);
            for (int i = 0; i < BrickYAmount; i++)
                for (int j = 0; j < BrickXAmount; j++)
                    if (Bricks[i, j] != null)
                        Bricks[i, j].Entity.SetActive(false);
            for (int i = 0; i < 4; i++)
                Bounds[i].SetActive(false);
            CurrentStage.End();
        }
        public void Destroy() {
            foreach (var s in Stages)
                s.Value.Destroy();
            for (int i = 0; i < 4; i++)
                GameObject.Destroy(Bounds[i]);
            GameObject.Destroy(BG);
            foreach (var b in Bricks)
                if (b != null) b.Destroy();
            foreach (var m in Monsters)
                m.Destroy();
            foreach (var b in Bullets)
                GameObject.Destroy(b);
            foreach (var n in NPCs)
                n.Destroy();
        }

        public void SwitchStage(string StageName) {
            CurrentStage.End();
            CurrentStage = Stages[StageName];
            CurrentStage.Start(this);
        }
        public GameObject CreateBullet(int Kind, float Damage, Point Position, Vector2 Forward, float Speed, float Range,
        string ImagePath, int ImageWidth, int ImageHeight, int PixelPerUnit) {
            GameObject go = new GameObject("Bullet");
            go.tag = "Bullet";
            SpriteRenderer rend = go.AddComponent<SpriteRenderer>();
            BoxCollider2D bc2D = go.AddComponent<BoxCollider2D>();
            Script_Bullet script = go.AddComponent<Script_Bullet>();
            Rigidbody2D rb2D = go.AddComponent<Rigidbody2D>();
            go.transform.localPosition = new Vector3(Position.x, Position.y, -2);
            go.transform.localRotation = Quaternion.FromToRotation(
                new Vector3(1,0),
                new Vector3(Forward.x, Forward.y));
            byte[] data = GameManager.GetImage(ImagePath);
            Texture2D texture = new Texture2D(ImageWidth, ImageHeight);
            texture.LoadImage(data);
            rend.sprite = Sprite.Create(
                texture,
                new Rect(0, 0, ImageWidth, ImageWidth),
                new Vector2(0.5f, 0.5f),
                PixelPerUnit
            );
            bc2D.offset = Vector2.zero;
            bc2D.size = new Vector2(0.3f, 0.3f);
            bc2D.isTrigger = true;
            rb2D.gravityScale = 0;
            rb2D.velocity = Forward.normalized * Speed;
            script.BulletIsImage = true;
            script.Kind = Kind;
            script.Damage = Damage;
            script.Speed = Speed;
            script.Range = Range;
            script.Forward = Forward;
            Bullets.Add(go);
            return go;
        }
        public GameObject CreateBullet(int Kind, float Damage, Point Position, Vector2 Forward, float Speed, float Range,
            string Text, float Size) {
            GameObject go = new GameObject("Bullet");
            go.tag = "Bullet";
            TextMesh textmesh = go.AddComponent<TextMesh>();
            BoxCollider2D bc2D = go.AddComponent<BoxCollider2D>();
            Script_Bullet script = go.AddComponent<Script_Bullet>();
            Rigidbody2D rb2D = go.AddComponent<Rigidbody2D>();
            go.transform.localPosition = new Vector3(Position.x, Position.y, -2);
            textmesh.text = Text;
            textmesh.anchor = TextAnchor.MiddleCenter;
            textmesh.characterSize = Size;
            bc2D.offset = Vector2.zero;
            bc2D.size = new Vector2(0.3f, 0.3f);
            bc2D.isTrigger = true;
            rb2D.gravityScale = 0;
            rb2D.velocity = Forward.normalized * Speed;
            script.BulletIsImage = false;
            script.Kind = Kind;
            script.Damage = Damage;
            script.Speed = Speed;
            script.Range = Range;
            script.Forward = Forward;
            Bullets.Add(go);
            return go;
        }

        public void Load(XDocument XML) {
            XElement root = XML.Root;
            Name = BasicTranslation.StringE2D(root.Element("Name"));
            BrickXAmount = BasicTranslation.IntE2D(root.Element("BrickXAmount"));
            BrickYAmount = BasicTranslation.IntE2D(root.Element("BrickYAmount"));
            BrickDefine = Transform.TranslateDic(
                root.Element("BrickDefine"),
                BasicTranslation.CharA2D,
                BasicTranslation.StringE2D);
            BrickMap = BasicTranslation.StringE2D(root.Element("BrickMap"));
            Stages = Transform.TranslateDic(
                root.Element("Stages"),
                BasicTranslation.StringA2D,
                Stage.E2D);
            BGImagePath = BasicTranslation.StringE2D(root.Element("BGImagePath"));
            BGImageWidth = BasicTranslation.IntE2D(root.Element("BGImageWidth"));
            BGImageHeight = BasicTranslation.IntE2D(root.Element("BGImageHeight"));
        }
        public XDocument Unload() {
            XDocument XML = new XDocument(new XElement("Root"));
            XElement root = XML.Root;
            root.Add(BasicTranslation.StringD2E("Name", Name));
            root.Add(BasicTranslation.IntD2E("BrickXAmount", BrickXAmount));
            root.Add(BasicTranslation.IntD2E("BrickYAmount", BrickYAmount));
            root.Add(Transform.TranslateDic(
                "BrickDefine",
                BasicTranslation.CharD2A,
                BasicTranslation.StringD2E,
                BrickDefine));
            root.Add(BasicTranslation.StringD2E("BrickMap", BrickMap));
            root.Add(Transform.TranslateDic(
                "Stages",
                BasicTranslation.StringD2A,
                Stage.D2E,
                Stages));
            root.Add(BasicTranslation.StringD2E("BGImagePath", BGImagePath));
            root.Add(BasicTranslation.IntD2E("BGImageWidth", BGImageWidth));
            root.Add(BasicTranslation.IntD2E("BGImageHeight", BGImageHeight));
            return XML;
        }
    }
    public class Stage : XMLNodeable {
        public static Stage E2D(XElement e) { return new Stage(e); }
        public static XElement D2E(string n, Stage s) { return s.Unload(n); }
        
        public Dictionary<string, Event> Events;
        public List<Event> CurrentEvents;

        public Scene mScene;
        public float RecordTime;
        public float LastTime;
        public float GetTime() { return RecordTime; }
        public bool Stopped;

        public Stage() { }
        public Stage(XElement e) {
            Load(e);
            RecordTime = 0;
            CurrentEvents = new List<Event>();
            foreach (var eve in Events) {
                eve.Value.State = Event.WAITING;
                eve.Value.StartTime = eve.Value.DelayTime;
                if (eve.Value.Active)
                    CurrentEvents.Add(eve.Value);
            }
        }
        public void Start(Scene scene) {
            mScene = scene;
            LastTime = mScene.GetTime();
            Stopped = false;
        }
        public void Update() {
            if (GameManager.Player != null) {
                if (GameManager.Player.HP < 0) {
                    GameManager.Player.HP = 0.1f;
                    GameManager.ShowMenu("Failed");
                }
                GameManager.RefreshInfo();
            }
            float CurrentTime = mScene.GetTime();
            RecordTime += CurrentTime - LastTime;
            LastTime = CurrentTime;
            for (int i = 0; i < CurrentEvents.Count; i++) {
                if (Stopped) break;
                Event e = CurrentEvents[i];
                if (e.Active
                    && e.State != Event.FINISHED
                    && e.StartTime < RecordTime
                    && e.Condition.IsOK(this, e)) {
                    if (e.State == Event.WAITING) {
                        e.State = Event.RUNNING;
                        e.Start(this);
                    } else if (e.State == Event.RUNNING) {
                        if (e.StartTime + e.ActiveTime < RecordTime) {
                            e.State = Event.FINISHED;
                            e.End();
                        } else {
                            e.Update();
                        }
                    } else {
                        e.Start(this);
                        e.End();
                    }
                }
            }
        }
        public void End() {
            Stopped = true;
        }
        public void Destroy() {
            foreach (var item in Events)
                item.Value.Destroy();
        }

        public void Load(XElement e) {
            Events = Transform.TranslateDic(e.Element("Events"), BasicTranslation.StringA2D, Event.E2D);
        }
        public XElement Unload(string n) {
            XElement e = new XElement(n);
            e.Add(Transform.TranslateDic("Events", BasicTranslation.StringD2A, Event.D2E, Events));
            return e;
        }
    }
    public class Brick : XMLNodeable {
        public static Brick E2D(XElement e) { return new Brick(e); }
        public static XElement D2E(string n, Brick s) { return s.Unload(n); }

        public int Length;
        public string ImagePath;
        public Rectangle CollideRegion;

        public GameObject Entity;

        public Brick() { }
        public Brick(XElement e) {
            Load(e);
            byte[] data = GameManager.GetImage(ImagePath);
            Texture2D texture = new Texture2D(Length, Length);
            texture.LoadImage(data);
            Entity = new GameObject("Brick");
            Entity.tag = "Brick";
            SpriteRenderer rend = Entity.AddComponent<SpriteRenderer>();
            rend.sprite = Sprite.Create(
                texture,
                new Rect(0, 0, Length, Length),
                new Vector2(0, 0),
                Length);
            BoxCollider2D bc2d = Entity.AddComponent<BoxCollider2D>();
            bc2d.offset = new Vector2(CollideRegion.x1, CollideRegion.y1);
            bc2d.size = new Vector2(CollideRegion.x2, CollideRegion.y2);
            Entity.SetActive(false);
        }
        public void Destroy() {
            GameObject.Destroy(Entity);
        }
        public void SetPosition(ScenePosition Position) {
            Entity.transform.localPosition = new Vector3(Position.x, Position.y);
        }
        public Brick Clone() {
            Brick brick = (Brick)MemberwiseClone();
            brick.Entity = GameObject.Instantiate<GameObject>(Entity);
            return brick;
        }

        public void Load(XElement e) {
            Length = BasicTranslation.IntE2D(e.Element("Length"));
            ImagePath = BasicTranslation.StringE2D(e.Element("ImagePath"));
            CollideRegion = Rectangle.E2D(e.Element("CollideRegion"));
        }
        public XElement Unload(string n) {
            XElement e = new XElement(n);
            e.Add(BasicTranslation.IntD2E("Length", Length));
            e.Add(BasicTranslation.StringD2E("ImagePath", ImagePath));
            e.Add(Rectangle.D2E("CollideRegion", CollideRegion));
            return e;
        }
    }
    public class Living : XMLNodeable {
        public static Living E2D(XElement e) { return new Living(e); }
        public static XElement D2E(string n, Living s) { return s.Unload(n); }
        public const int NPC = 1;
        public const int PLAYER = 0;
        public const int MONSTER = -1;

        public string Name { get; private set; }
        public string Kind;
        public bool IgnoreBricks;
        public float SearchRange;
        public float AttackStayTime;
        public float MoveStayTime;
        public float MoveInterval;
        public int ImageWidth;
        public int ImageHeight;
        public int PixelPerUnit;
        public Circle CollideRegion;
        public float GravityScale;
        public float MoveSpeed;
        public float JumpSpeed;
        public string ShowImagePath;
        public List<string> WaitingImagePaths;
        public List<string> MovingImagePaths;
        public List<string> AttackingImagePaths;
        public float WaitingImageInterval;
        public float MovingImageInterval;
        public float AttackingImageInterval;
        public float HPLimit;
        public float AttackInterval;
        public float AttackBulletRange;
        public float AttackBulletDamage;
        public float AttackBulletSpeed;
        public Point AttackBulletOffset;
        public bool AttackBulletIsImage;
        public int AttackBulletImageWidth;
        public int AttackBulletImageHeight;
        public int AttackBulletImagePixelPerUnit;
        public string AttackBulletImagePath;
        public List<string> AttackBulletTexts;
        public float AttackBulletTextSize;

        public List<Sprite> WaitingImages;
        public List<Sprite> MovingImages;
        public List<Sprite> AttackingImages;
        public float HP;
        public bool TurnRight;
        public GameObject Entity;
        public SpriteRenderer SpriteRend;
        public Rigidbody2D rb2D;
        public CircleCollider2D cc2D;

        private List<Sprite> SpriteTranslate(List<string> Paths) {
            List<Sprite> lis = new List<Sprite>();
            foreach (var path in Paths) {
                byte[] data = GameManager.GetImage(path);
                Texture2D texture = new Texture2D(ImageWidth, ImageHeight);
                texture.LoadImage(data);
                lis.Add(Sprite.Create(
                    texture,
                    new Rect(0, 0, ImageWidth, ImageHeight),
                    new Vector2(0.5f, 0.5f),
                    PixelPerUnit
                ));
            }
            return lis;
        }

        public Living() { }
        public Living(XElement e) {
            Load(e);
            WaitingImages = SpriteTranslate(WaitingImagePaths);
            MovingImages = SpriteTranslate(MovingImagePaths);
            AttackingImages = SpriteTranslate(AttackingImagePaths);
            HP = HPLimit;
            TurnRight = true;
            Entity = new GameObject("Living");
            SpriteRend = Entity.AddComponent<SpriteRenderer>();
            SpriteRend.sprite = WaitingImages[0];
            cc2D = Entity.AddComponent<CircleCollider2D>();
            cc2D.offset = new Vector2(CollideRegion.x, CollideRegion.y);
            cc2D.radius = CollideRegion.r;
            rb2D = Entity.AddComponent<Rigidbody2D>();
            rb2D.gravityScale = GravityScale;
            rb2D.freezeRotation = true;
            Entity.SetActive(false);
        }
        public void Destroy() {
            foreach (var i in WaitingImages)
                GameObject.Destroy(i);
            foreach (var i in MovingImages)
                GameObject.Destroy(i);
            foreach (var i in AttackingImages)
                GameObject.Destroy(i);
            GameObject.Destroy(SpriteRend);
            GameObject.Destroy(rb2D);
            GameObject.Destroy(cc2D);
            GameObject.Destroy(Entity);
        }
        public void SetTag(string NewTag) { Entity.tag = NewTag; }
        public void SetName(string NewName) { Name = NewName; }
        public ScenePosition GetScenePosition() {
            Vector3 p = Entity.transform.localPosition;
            return new ScenePosition(
                Mathf.FloorToInt(p.x),
                Mathf.FloorToInt(p.y)
            );
        }
        public void SetScenePosition(ScenePosition sp, float Depth) {
            Entity.transform.localPosition = new Vector3(
                sp.x + 0.5f,
                sp.y + 0.5f,
                Depth
            );
        }
        public void FlipImage() {
            TurnRight = !TurnRight;
            Entity.transform.localScale = new Vector3(TurnRight ? 1 : -1, 1, 1);
        }
        public void ChangeHP(float Delta) {
            HP += Delta;
        }

        public Living Clone() {
            Living living = (Living)MemberwiseClone();
            living.Entity = GameObject.Instantiate<GameObject>(Entity);
            living.SpriteRend = living.Entity.GetComponent<SpriteRenderer>();
            living.cc2D = living.Entity.GetComponent<CircleCollider2D>();
            living.rb2D = living.Entity.GetComponent<Rigidbody2D>();
            return living;
        }

        public void Load(XElement e) {
            Kind = BasicTranslation.StringE2D(e.Element("Kind"));
            IgnoreBricks = BasicTranslation.BoolE2D(e.Element("IgnoreBricks"));
            SearchRange = BasicTranslation.FloatE2D(e.Element("SearchRange"));
            AttackStayTime = BasicTranslation.FloatE2D(e.Element("AttackStayTime"));
            MoveStayTime = BasicTranslation.FloatE2D(e.Element("MoveStayTime"));
            MoveInterval = BasicTranslation.FloatE2D(e.Element("MoveInterval"));
            ImageWidth = BasicTranslation.IntE2D(e.Element("ImageWidth"));
            ImageHeight = BasicTranslation.IntE2D(e.Element("ImageHeight"));
            PixelPerUnit = BasicTranslation.IntE2D(e.Element("PixelPerUnit"));
            CollideRegion = Circle.E2D(e.Element("CollideRegion"));
            GravityScale = BasicTranslation.FloatE2D(e.Element("GravityScale"));
            MoveSpeed = BasicTranslation.FloatE2D(e.Element("MoveSpeed"));
            JumpSpeed = BasicTranslation.FloatE2D(e.Element("JumpSpeed"));
            ShowImagePath = BasicTranslation.StringE2D(e.Element("ShowImagePath"));
            WaitingImagePaths = Transform.TranslateList(e.Element("WaitingImagePaths"), BasicTranslation.StringE2D);
            MovingImagePaths = Transform.TranslateList(e.Element("MovingImagePaths"), BasicTranslation.StringE2D);
            AttackingImagePaths = Transform.TranslateList(e.Element("AttackingImagePaths"), BasicTranslation.StringE2D);
            WaitingImageInterval = BasicTranslation.FloatE2D(e.Element("WaitingImageInterval"));
            MovingImageInterval = BasicTranslation.FloatE2D(e.Element("MovingImageInterval"));
            AttackingImageInterval = BasicTranslation.FloatE2D(e.Element("AttackingImageInterval"));
            HPLimit = BasicTranslation.FloatE2D(e.Element("HPLimit"));
            AttackInterval = BasicTranslation.FloatE2D(e.Element("AttackInterval"));
            AttackBulletRange = BasicTranslation.FloatE2D(e.Element("AttackBulletRange"));
            AttackBulletDamage = BasicTranslation.FloatE2D(e.Element("AttackBulletDamage"));
            AttackBulletSpeed = BasicTranslation.FloatE2D(e.Element("AttackBulletSpeed"));
            AttackBulletOffset = Point.E2D(e.Element("AttackBulletOffset"));
            AttackBulletIsImage = BasicTranslation.BoolE2D(e.Element("AttackBulletIsImage"));
            AttackBulletImageWidth = BasicTranslation.IntE2D(e.Element("AttackBulletImageWidth"));
            AttackBulletImageHeight = BasicTranslation.IntE2D(e.Element("AttackBulletImageHeight"));
            AttackBulletImagePixelPerUnit = BasicTranslation.IntE2D(e.Element("AttackBulletImagePixelPerUnit"));
            AttackBulletImagePath = BasicTranslation.StringE2D(e.Element("AttackBulletImagePath"));
            AttackBulletTexts = Transform.TranslateList(e.Element("AttackBulletTexts"), BasicTranslation.StringE2D);
            AttackBulletTextSize = BasicTranslation.FloatE2D(e.Element("AttackBulletTextSize"));
        }
        public XElement Unload(string n) {
            XElement e = new XElement(n);
            e.Add(BasicTranslation.StringD2E("Kind", Kind));
            e.Add(BasicTranslation.BoolD2E("IgnoreBricks", IgnoreBricks));
            e.Add(BasicTranslation.FloatD2E("SearchRange", SearchRange));
            e.Add(BasicTranslation.FloatD2E("AttackStayTime", AttackStayTime));
            e.Add(BasicTranslation.FloatD2E("MoveStayTime", MoveStayTime));
            e.Add(BasicTranslation.FloatD2E("MoveInterval", MoveInterval));
            e.Add(BasicTranslation.IntD2E("ImageWidth", ImageWidth));
            e.Add(BasicTranslation.IntD2E("ImageHeight", ImageHeight));
            e.Add(BasicTranslation.IntD2E("PixelPerUnit", PixelPerUnit));
            e.Add(Circle.D2E("CollideRegion", CollideRegion));
            e.Add(BasicTranslation.FloatD2E("GravityScale", GravityScale));
            e.Add(BasicTranslation.FloatD2E("MoveSpeed", MoveSpeed));
            e.Add(BasicTranslation.FloatD2E("JumpSpeed", JumpSpeed));
            e.Add(BasicTranslation.StringD2E("ShowImagePath", ShowImagePath));
            e.Add(Transform.TranslateList("WaitingImagePaths", BasicTranslation.StringD2E, WaitingImagePaths));
            e.Add(Transform.TranslateList("MovingImagePaths", BasicTranslation.StringD2E, MovingImagePaths));
            e.Add(Transform.TranslateList("AttackingImagePaths", BasicTranslation.StringD2E, AttackingImagePaths));
            e.Add(BasicTranslation.FloatD2E("WaitingImageInterval", WaitingImageInterval));
            e.Add(BasicTranslation.FloatD2E("MovingImageInterval", MovingImageInterval));
            e.Add(BasicTranslation.FloatD2E("AttackingImageInterval", AttackingImageInterval));
            e.Add(BasicTranslation.FloatD2E("HPLimit", HPLimit));
            e.Add(BasicTranslation.FloatD2E("AttackInterval", AttackInterval));
            e.Add(BasicTranslation.FloatD2E("AttackBulletRange", AttackBulletRange));
            e.Add(BasicTranslation.FloatD2E("AttackBulletDamage", AttackBulletDamage));
            e.Add(BasicTranslation.FloatD2E("AttackBulletSpeed", AttackBulletSpeed));
            e.Add(Point.D2E("AttackBulletOffset", AttackBulletOffset));
            e.Add(BasicTranslation.BoolD2E("AttackBulletIsImage", AttackBulletIsImage));
            e.Add(BasicTranslation.IntD2E("AttackBulletImageWidth", AttackBulletImageWidth));
            e.Add(BasicTranslation.IntD2E("AttackBulletImageHeight", AttackBulletImageHeight));
            e.Add(BasicTranslation.IntD2E("AttackBulletImagePixelPerUnit", AttackBulletImagePixelPerUnit));
            e.Add(BasicTranslation.StringD2E("AttackBulletImagePath", AttackBulletImagePath));
            e.Add(Transform.TranslateList("AttackBulletTexts", BasicTranslation.StringD2E, AttackBulletTexts));
            e.Add(BasicTranslation.FloatD2E("AttackBulletTextSize", AttackBulletTextSize));
            return e;
        }
    }
    //abstract ...
    public abstract class EventCondition : XMLNodeable {
        public static EventCondition E2D(XElement e) { return GameManager.ConditionE2DDic[e.Attribute("Kind").Value](e); }
        public static XElement D2E(string n, EventCondition s) { return s.Unload(n); }

        abstract public bool IsOK(Stage s, Event e);

        abstract public void Load(XElement e);
        abstract public XElement Unload(string n);
    }
    public abstract class Event : XMLNodeable {
        public static Event E2D(XElement e) { return GameManager.EventE2DDic[e.Attribute("Kind").Value](e); }
        public static XElement D2E(string n, Event s) { return s.Unload(n); }
        public const int WAITING = 0;
        public const int RUNNING = 1;
        public const int FINISHED = 2;

        public bool Active;
        public int State;
        public float DelayTime;
        public EventCondition Condition;
        public float ActiveTime;

        public float StartTime;

        abstract public void Start(Stage stage);
        virtual public void Update() { }
        virtual public void End() { }
        virtual public void Destroy() { }

        abstract public void Load(XElement e);
        abstract public XElement Unload(string n);

        public void BaseLoad(XElement e) {
            Active = BasicTranslation.BoolE2D(e.Element("Active"));
            DelayTime = BasicTranslation.FloatE2D(e.Element("DelayTime"));
            Condition = EventCondition.E2D(e.Element("Condition"));
            ActiveTime = BasicTranslation.FloatE2D(e.Element("ActiveTime"));
        }
        public XElement BaseUnload(XElement e) {
            e.Add(BasicTranslation.BoolD2E("Active", Active));
            e.Add(BasicTranslation.FloatD2E("DelayTime", DelayTime));
            e.Add(EventCondition.D2E("Condition", Condition));
            e.Add(BasicTranslation.FloatD2E("ActiveTime", ActiveTime));
            return e;
        }
    }
    public abstract class MenuElement : XMLNodeable {
        public static MenuElement E2D(XElement e) { return GameManager.MenuElementE2DDic[e.Attribute("Kind").Value](e); }
        public static XElement D2E(string n, MenuElement s) { return s.Unload(n); }

        abstract public void Start();
        virtual public void Update() { }
        abstract public void End();

        abstract public void Load(XElement e);
        abstract public XElement Unload(string n);
    }
    public class Menu : XMLNodeable {
        public static Menu E2D(XElement e) { return new Menu(e); }
        public static XElement D2E(string n, Menu s) { return s.Unload(n); }

        public List<MenuElement> Elements;
        public bool Stopped;

        public Menu() { }
        public Menu(XElement e) { Load(e); }

        public void Start() {
            Stopped = false;
            foreach (var e in Elements)
                e.Start();
        }
        public void Update() {
            foreach (var e in Elements) {
                if (Stopped) break;
                e.Update();
            }
        }
        public void End() {
            Stopped = true;
            foreach (var e in Elements)
                e.End();
        }

        public void Load(XElement e) {
            Elements = Transform.TranslateList(e.Element("Elements"), MenuElement.E2D);
        }
        public XElement Unload(string n) {
            XElement e = new XElement(n);
            e.Add(Transform.TranslateList("Elements", MenuElement.D2E, Elements));
            return e;
        }
    }
}