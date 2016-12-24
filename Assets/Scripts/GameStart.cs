using UnityEngine;
using System.Collections.Generic;
using System.Xml.Linq;
using Game;

public class GameStart : MonoBehaviour {
    public void Start() {
        //GameInfo
        /*GameInfo info = new GameInfo();
        info.BrickPath = "Bricks.xml";
        info.LivingPath = "Livings.xml";
        info.MenuPath = "Menus.xml";
        info.Name = "Test";
        info.ScenePaths = new List<string>();
        info.ScenePaths.Add("Scene_Main.xml");
        info.ScenePaths.Add("Scene_808_1.xml");
        info.ScenePaths.Add("Scene_808_2.xml");
        info.ScenePaths.Add("Scene_809_1.xml");
        info.ScenePaths.Add("Scene_810_1.xml");
        info.ScenePaths.Add("Scene_811_1.xml");
        info.Unload().Save("GameInfo.xml");
        //Scene
        Data.Create_Scene_Main();
        Data.Create_Scene_808_1();
        Data.Create_Scene_808_2();
        Data.Create_Scene_809_1();
        Data.Create_Scene_810_1();
        Data.Create_Scene_811_1();
        //Bricks
        Dictionary<string, Brick> brickdic = new Dictionary<string, Brick>();
        {
            Brick brick;
            //808
            brick = new Brick();
            brick.CollideRegion = new Rectangle(0, 0, 0, 0);
            brick.ImagePath = "808\\bush.png";
            brick.Length = 80;
            brickdic.Add("808bush", brick);
            brick = new Brick();
            brick.CollideRegion = new Rectangle(0, 0, 0, 0);
            brick.ImagePath = "808\\chain.png";
            brick.Length = 80;
            brickdic.Add("808chain", brick);
            brick = new Brick();
            brick.CollideRegion = new Rectangle(0, 0, 0, 0);
            brick.ImagePath = "808\\cloud1.png";
            brick.Length = 80;
            brickdic.Add("808cloud1", brick);
            brick = new Brick();
            brick.CollideRegion = new Rectangle(0, 0, 0, 0);
            brick.ImagePath = "808\\cloud2.png";
            brick.Length = 80;
            brickdic.Add("808cloud2", brick);
            brick = new Brick();
            brick.CollideRegion = new Rectangle(0.5f, 0.5f, 1, 1);
            brick.ImagePath = "808\\metal.png";
            brick.Length = 80;
            brickdic.Add("808metal", brick);
            brick = new Brick();
            brick.CollideRegion = new Rectangle(0.5f, 0.5f, 1, 1);
            brick.ImagePath = "808\\nail.png";
            brick.Length = 80;
            brickdic.Add("808nail", brick);
            brick = new Brick();
            brick.CollideRegion = new Rectangle(0, 0, 0, 0);
            brick.ImagePath = "808\\star.png";
            brick.Length = 80;
            brickdic.Add("808star", brick);
            //809
            brick = new Brick();
            brick.CollideRegion = new Rectangle(0.5f, 0.5f, 1, 1);
            brick.ImagePath = "809\\wall.png";
            brick.Length = 80;
            brickdic.Add("809wall", brick);
            brick = new Brick();
            brick.CollideRegion = new Rectangle(0, 0, 0, 0);
            brick.ImagePath = "809\\transleft0.png";
            brick.Length = 80;
            brickdic.Add("809transleft0", brick);
            brick = new Brick();
            brick.CollideRegion = new Rectangle(0, 0, 0, 0);
            brick.ImagePath = "809\\transright0.png";
            brick.Length = 80;
            brickdic.Add("809transright0", brick);
            brick = new Brick();
            brick.CollideRegion = new Rectangle(0, 0, 0, 0);
            brick.ImagePath = "809\\transleft1.png";
            brick.Length = 80;
            brickdic.Add("809transleft1", brick);
            brick = new Brick();
            brick.CollideRegion = new Rectangle(0, 0, 0, 0);
            brick.ImagePath = "809\\transright1.png";
            brick.Length = 80;
            brickdic.Add("809transright1", brick);
            brick = new Brick();
            brick.CollideRegion = new Rectangle(0.5f, 0.5f, 1, 1);
            brick.ImagePath = "809\\earth1.png";
            brick.Length = 80;
            brickdic.Add("809earth1", brick);
            brick = new Brick();
            brick.CollideRegion = new Rectangle(0.5f, 0.5f, 1, 1);
            brick.ImagePath = "809\\earth2.png";
            brick.Length = 80;
            brickdic.Add("809earth2", brick);
            brick = new Brick();
            brick.CollideRegion = new Rectangle(0.5f, 0.5f, 1, 1);
            brick.ImagePath = "809\\earth3.png";
            brick.Length = 80;
            brickdic.Add("809earth3", brick);
            //810
            brick = new Brick();
            brick.CollideRegion = new Rectangle(0.5f, 0.5f, 1, 1);
            brick.ImagePath = "810\\stone.png";
            brick.Length = 80;
            brickdic.Add("810stone", brick);
            brick = new Brick();
            brick.CollideRegion = new Rectangle(0, 0, 0, 0);
            brick.ImagePath = "810\\doorleftbot0.png";
            brick.Length = 80;
            brickdic.Add("810doorleftbot0", brick);
            brick = new Brick();
            brick.CollideRegion = new Rectangle(0, 0, 0, 0);
            brick.ImagePath = "810\\doorrightbot0.png";
            brick.Length = 80;
            brickdic.Add("810doorrightbot0", brick);
            brick = new Brick();
            brick.CollideRegion = new Rectangle(0, 0, 0, 0);
            brick.ImagePath = "810\\doorlefttop0.png";
            brick.Length = 80;
            brickdic.Add("810doorlefttop0", brick);
            brick = new Brick();
            brick.CollideRegion = new Rectangle(0, 0, 0, 0);
            brick.ImagePath = "810\\doorrighttop0.png";
            brick.Length = 80;
            brickdic.Add("810doorrighttop0", brick);
            brick = new Brick();
            brick.CollideRegion = new Rectangle(0, 0, 0, 0);
            brick.ImagePath = "810\\doorleftbot1.png";
            brick.Length = 80;
            brickdic.Add("810doorleftbot1", brick);
            brick = new Brick();
            brick.CollideRegion = new Rectangle(0, 0, 0, 0);
            brick.ImagePath = "810\\doorrightbot1.png";
            brick.Length = 80;
            brickdic.Add("810doorrightbot1", brick);
            brick = new Brick();
            brick.CollideRegion = new Rectangle(0, 0, 0, 0);
            brick.ImagePath = "810\\doorlefttop1.png";
            brick.Length = 80;
            brickdic.Add("810doorlefttop1", brick);
            brick = new Brick();
            brick.CollideRegion = new Rectangle(0, 0, 0, 0);
            brick.ImagePath = "810\\doorrighttop1.png";
            brick.Length = 80;
            brickdic.Add("810doorrighttop1", brick);
            brick = new Brick();
            brick.CollideRegion = new Rectangle(0, 0, 0, 0);
            brick.ImagePath = "810\\transleftbot.png";
            brick.Length = 80;
            brickdic.Add("810transleftbot", brick);
            brick = new Brick();
            brick.CollideRegion = new Rectangle(0, 0, 0, 0);
            brick.ImagePath = "810\\transrightbot.png";
            brick.Length = 80;
            brickdic.Add("810transrightbot", brick);
            brick = new Brick();
            brick.CollideRegion = new Rectangle(0, 0, 0, 0);
            brick.ImagePath = "810\\translefttop.png";
            brick.Length = 80;
            brickdic.Add("810translefttop", brick);
            brick = new Brick();
            brick.CollideRegion = new Rectangle(0, 0, 0, 0);
            brick.ImagePath = "810\\transrighttop.png";
            brick.Length = 80;
            brickdic.Add("810transrighttop", brick);
            //811
            brick = new Brick();
            brick.CollideRegion = new Rectangle(0, 0, 0, 0);
            brick.ImagePath = "811\\cloud1.png";
            brick.Length = 80;
            brickdic.Add("811cloud1", brick);
            brick = new Brick();
            brick.CollideRegion = new Rectangle(0, 0, 0, 0);
            brick.ImagePath = "811\\cloud2.png";
            brick.Length = 80;
            brickdic.Add("811cloud2", brick);
            brick = new Brick();
            brick.CollideRegion = new Rectangle(0, 0, 0, 0);
            brick.ImagePath = "811\\cloud3.png";
            brick.Length = 80;
            brickdic.Add("811cloud3", brick);
            brick = new Brick();
            brick.CollideRegion = new Rectangle(0, 0, 0, 0);
            brick.ImagePath = "811\\cloud4.png";
            brick.Length = 80;
            brickdic.Add("811cloud4", brick);
            brick = new Brick();
            brick.CollideRegion = new Rectangle(0.5f, 0.5f, 1, 1);
            brick.ImagePath = "811\\earth.png";
            brick.Length = 80;
            brickdic.Add("811earth", brick);
            brick = new Brick();
            brick.CollideRegion = new Rectangle(0, 0, 0, 0);
            brick.ImagePath = "811\\moon.png";
            brick.Length = 80;
            brickdic.Add("811moon", brick);
            brick = new Brick();
            brick.CollideRegion = new Rectangle(0, 0, 0, 0);
            brick.ImagePath = "811\\moun1.png";
            brick.Length = 80;
            brickdic.Add("811moun1", brick);
            brick = new Brick();
            brick.CollideRegion = new Rectangle(0, 0, 0, 0);
            brick.ImagePath = "811\\moun2.png";
            brick.Length = 80;
            brickdic.Add("811moun2", brick);
        }
        XElement xe = Game.Transform.TranslateDic("Root", BasicTranslation.StringD2A, Brick.D2E, brickdic);
        XDocument xd = new XDocument();
        xd.Add(xe);
        xd.Save("Bricks.xml");
        //Livings
        Dictionary<string, Living> livingdic = new Dictionary<string, Living>();
        {
            Living living = new Living();
            living.Kind = "Dragon";
            living.IgnoreBricks = false;
            living.SearchRange = 4;
            living.AttackStayTime = 2.5f;
            living.MoveStayTime = 2f;
            living.MoveInterval = 0.5f;
            living.PixelPerUnit = 90;
            living.AttackBulletRange = 6;
            living.AttackBulletSpeed = 1.2f;
            living.AttackBulletDamage = 20;
            living.AttackBulletOffset = new Point(0.1f, 0.2f);
            living.AttackBulletIsImage = true;
            living.AttackBulletImagePath = "living\\dragon_bullet.png";
            living.AttackBulletImageWidth = 80;
            living.AttackBulletImageHeight = 80;
            living.AttackBulletImagePixelPerUnit = 80 * 3;
            living.AttackBulletTexts = new List<string>();
            living.AttackBulletTextSize = 0.25f;
            living.AttackInterval = 2f;
            living.CollideRegion = new Circle(0, 0, (80f / 90) / 2);
            living.HPLimit = 200;
            living.ImageHeight = 80;
            living.ImageWidth = 80;
            living.JumpSpeed = 6f;
            living.GravityScale = 0.8f;
            living.MoveSpeed = 2.5f;
            living.ShowImagePath = "ui\\dragon.png";
            living.WaitingImagePaths = new List<string>();
            living.MovingImagePaths = new List<string>();
            living.AttackingImagePaths = new List<string>();
            living.WaitingImagePaths.Add("living\\dragon_wait.png");
            living.MovingImagePaths.Add("living\\dragon_move.png");
            living.AttackingImagePaths.Add("living\\dragon_attack.png");
            living.WaitingImageInterval = 0.5f;
            living.AttackingImageInterval = 1;
            living.MovingImageInterval = 0.33f;
            livingdic.Add("Dragon", living);
        }
        {
            Living living = new Living();
            living.Kind = "Ghost";
            living.IgnoreBricks = true;
            living.SearchRange = 6;
            living.AttackStayTime = 1.5f;
            living.MoveStayTime = 1f;
            living.MoveInterval = 0.4f;
            living.PixelPerUnit = 100;
            living.AttackBulletRange = 4f;
            living.AttackBulletSpeed = 2.2f;
            living.AttackBulletDamage = 5;
            living.AttackBulletOffset = new Point(0.3f, 0.1f);
            living.AttackBulletIsImage = true;
            living.AttackBulletImagePath = "living\\ghost_bullet.png";
            living.AttackBulletImageWidth = 80;
            living.AttackBulletImageHeight = 80;
            living.AttackBulletImagePixelPerUnit = 90 * 3;
            living.AttackBulletTexts = new List<string>();
            living.AttackBulletTextSize = 0.25f;
            living.AttackInterval = 0.6f;
            living.CollideRegion = new Circle(0, 0, 0.45f);
            living.HPLimit = 100;
            living.ImageHeight = 80;
            living.ImageWidth = 80;
            living.JumpSpeed = 5f;
            living.GravityScale = 0.8f;
            living.MoveSpeed = 1.5f;
            living.ShowImagePath = "ui\\ghost.png";
            living.WaitingImagePaths = new List<string>();
            living.MovingImagePaths = new List<string>();
            living.AttackingImagePaths = new List<string>();
            living.WaitingImagePaths.Add("living\\ghost_wait.png");
            living.MovingImagePaths.Add("living\\ghost_move.png");
            living.AttackingImagePaths.Add("living\\ghost_attack.png");
            living.WaitingImageInterval = 1;
            living.AttackingImageInterval = 1;
            living.MovingImageInterval = 1;
            livingdic.Add("Ghost", living);
        }
        {
            Living living = new Living();
            living.Kind = "Devil";
            living.IgnoreBricks = false;
            living.SearchRange = 6;
            living.AttackStayTime = 4f;
            living.MoveStayTime = 2.6f;
            living.MoveInterval = 2.5f;
            living.PixelPerUnit = 100;
            living.AttackBulletRange = 1;
            living.AttackBulletSpeed = 5f;
            living.AttackBulletDamage = 2;
            living.AttackBulletOffset = new Point(0, 0);
            living.AttackBulletIsImage = false;
            living.AttackBulletImagePath = "";
            living.AttackBulletImageWidth = 80;
            living.AttackBulletImageHeight = 80;
            living.AttackBulletImagePixelPerUnit = 90 * 3;
            living.AttackBulletTexts = new List<string>();
            living.AttackBulletTexts.Add("");
            living.AttackBulletTextSize = 0.25f;
            living.AttackInterval = 0.1f;
            living.CollideRegion = new Circle(0, 0, 0.45f);
            living.HPLimit = 150;
            living.ImageHeight = 80;
            living.ImageWidth = 80;
            living.JumpSpeed = 6f;
            living.GravityScale = 0.8f;
            living.MoveSpeed = 6f;
            living.ShowImagePath = "ui\\devil.png";
            living.WaitingImagePaths = new List<string>();
            living.MovingImagePaths = new List<string>();
            living.AttackingImagePaths = new List<string>();
            living.WaitingImagePaths.Add("living\\devil_wait.png");
            living.MovingImagePaths.Add("living\\devil_move.png");
            living.AttackingImagePaths.Add("living\\devil_move.png");
            living.WaitingImageInterval = 1;
            living.AttackingImageInterval = 1;
            living.MovingImageInterval = 1;
            livingdic.Add("Devil", living);
        }
        {
            Living living = new Living();
            living.Kind = "NPC";
            living.IgnoreBricks = false;
            living.SearchRange = 6;
            living.AttackStayTime = 1.5f;
            living.MoveStayTime = 1f;
            living.MoveInterval = 0.4f;
            living.PixelPerUnit = 100;
            living.AttackBulletRange = 0f;
            living.AttackBulletSpeed = 1f;
            living.AttackBulletDamage = 0;
            living.AttackBulletOffset = new Point(0.3f, 0.1f);
            living.AttackBulletIsImage = false;
            living.AttackBulletImagePath = "";
            living.AttackBulletImageWidth = 80;
            living.AttackBulletImageHeight = 80;
            living.AttackBulletImagePixelPerUnit = 90 * 3;
            living.AttackBulletTexts = new List<string>();
            living.AttackBulletTexts.Add("a");
            living.AttackBulletTextSize = 0;
            living.AttackInterval = 0.1f;
            living.CollideRegion = new Circle(0, 0, 0.45f);
            living.HPLimit = 1;
            living.ImageHeight = 80;
            living.ImageWidth = 80;
            living.JumpSpeed = 5f;
            living.GravityScale = 0.8f;
            living.MoveSpeed = 2f;
            living.ShowImagePath = "ui\\npc.png";
            living.WaitingImagePaths = new List<string>();
            living.MovingImagePaths = new List<string>();
            living.AttackingImagePaths = new List<string>();
            living.WaitingImagePaths.Add("living\\npc.png");
            living.MovingImagePaths.Add("living\\npc.png");
            living.AttackingImagePaths.Add("living\\npc.png");
            living.WaitingImageInterval = 1;
            living.AttackingImageInterval = 1;
            living.MovingImageInterval = 1;
            livingdic.Add("NPC", living);
        }
        {
            Living living = new Living();
            living.Kind = "PM";
            living.IgnoreBricks = false;
            living.SearchRange = 6;
            living.AttackStayTime = 2f;
            living.MoveStayTime = 2.6f;
            living.MoveInterval = 2.5f;
            living.PixelPerUnit = 90;
            living.AttackBulletRange = 6f;
            living.AttackBulletSpeed = 6f;
            living.AttackBulletDamage = 28;
            living.AttackBulletOffset = new Point(0.25f, -0.1f);
            living.AttackBulletIsImage = true;
            living.AttackBulletImagePath = "living\\pm_bullet.png";
            living.AttackBulletImageWidth = 80;
            living.AttackBulletImageHeight = 80;
            living.AttackBulletImagePixelPerUnit = 80 * 3;
            living.AttackBulletTexts = new List<string>();
            living.AttackBulletTexts.Add("");
            living.AttackBulletTextSize = 0.25f;
            living.AttackInterval = 0.8f;
            living.CollideRegion = new Circle(0, 0, 0.45f);
            living.HPLimit = 200;
            living.ImageHeight = 80;
            living.ImageWidth = 80;
            living.JumpSpeed = 5f;
            living.GravityScale = 0.8f;
            living.MoveSpeed = 2.5f;
            living.ShowImagePath = "ui\\pm.png";
            living.WaitingImagePaths = new List<string>();
            living.MovingImagePaths = new List<string>();
            living.AttackingImagePaths = new List<string>();
            living.WaitingImagePaths.Add("living\\pm_wait.png");
            living.MovingImagePaths.Add("living\\pm_move.png");
            living.AttackingImagePaths.Add("living\\pm_attack.png");
            living.WaitingImageInterval = 1;
            living.AttackingImageInterval = 1;
            living.MovingImageInterval = 1;
            livingdic.Add("PM", living);
        }
        {
            Living living = new Living();
            living.Kind = "Designer";
            living.IgnoreBricks = false;
            living.SearchRange = 6;
            living.AttackStayTime = 2f;
            living.MoveStayTime = 2.6f;
            living.MoveInterval = 2.5f;
            living.PixelPerUnit = 90;
            living.AttackBulletRange = 7f;
            living.AttackBulletSpeed = 7f;
            living.AttackBulletDamage = 40;
            living.AttackBulletOffset = new Point(0.25f, -0.1f);
            living.AttackBulletIsImage = true;
            living.AttackBulletImagePath = "living\\designer_bullet.png";
            living.AttackBulletImageWidth = 80;
            living.AttackBulletImageHeight = 80;
            living.AttackBulletImagePixelPerUnit = 80 * 3;
            living.AttackBulletTexts = new List<string>();
            living.AttackBulletTexts.Add("");
            living.AttackBulletTextSize = 0.25f;
            living.AttackInterval = 0.8f;
            living.CollideRegion = new Circle(0, 0, 0.45f);
            living.HPLimit = 100;
            living.ImageHeight = 80;
            living.ImageWidth = 80;
            living.JumpSpeed = 5f;
            living.GravityScale = 0.8f;
            living.MoveSpeed = 2.5f;
            living.ShowImagePath = "ui\\designer.png";
            living.WaitingImagePaths = new List<string>();
            living.MovingImagePaths = new List<string>();
            living.AttackingImagePaths = new List<string>();
            living.WaitingImagePaths.Add("living\\designer_wait.png");
            living.MovingImagePaths.Add("living\\designer_move.png");
            living.AttackingImagePaths.Add("living\\designer_attack.png");
            living.WaitingImageInterval = 1;
            living.AttackingImageInterval = 1;
            living.MovingImageInterval = 1;
            livingdic.Add("Designer", living);
        }
        {
            Living living = new Living();
            living.Kind = "Coder";
            living.IgnoreBricks = false;
            living.SearchRange = 6;
            living.AttackStayTime = 2f;
            living.MoveStayTime = 2.6f;
            living.MoveInterval = 2.5f;
            living.PixelPerUnit = 90;
            living.AttackBulletRange = 3.5f;
            living.AttackBulletSpeed = 4f;
            living.AttackBulletDamage = 21;
            living.AttackBulletOffset = new Point(0.25f, -0.1f);
            living.AttackBulletIsImage = true;
            living.AttackBulletImagePath = "living\\coder_bullet.png";
            living.AttackBulletImageWidth = 80;
            living.AttackBulletImageHeight = 80;
            living.AttackBulletImagePixelPerUnit = 80 * 3;
            living.AttackBulletTexts = new List<string>();
            living.AttackBulletTexts.Add("");
            living.AttackBulletTextSize = 0.25f;
            living.AttackInterval = 0.5f;
            living.CollideRegion = new Circle(0, 0, 0.45f);
            living.HPLimit = 150;
            living.ImageHeight = 80;
            living.ImageWidth = 80;
            living.JumpSpeed = 5f;
            living.GravityScale = 0.8f;
            living.MoveSpeed = 2.5f;
            living.ShowImagePath = "ui\\coder.png";
            living.WaitingImagePaths = new List<string>();
            living.MovingImagePaths = new List<string>();
            living.AttackingImagePaths = new List<string>();
            living.WaitingImagePaths.Add("living\\coder_wait.png");
            living.MovingImagePaths.Add("living\\coder_move.png");
            living.AttackingImagePaths.Add("living\\coder_attack.png");
            living.WaitingImageInterval = 1;
            living.AttackingImageInterval = 1;
            living.MovingImageInterval = 1;
            livingdic.Add("Coder", living);
        }
        xd = new XDocument();
        xd.Add(Game.Transform.TranslateDic("Root", BasicTranslation.StringD2A, Living.D2E, livingdic));
        xd.Save("Livings.xml");
        Dictionary<string, Menu> menuDic = new Dictionary<string, Menu>();
        Menu menu;
        {
            menu = new Menu();
            menu.Elements = new List<MenuElement>();
            {
                Game.MenuElements.Image i = new Game.MenuElements.Image();
                i.Depth = -5;
                i.ImageHeight = 960;
                i.ImageWidth = 1280;
                i.ImagePath = "ui\\story.png";
                i.Region = new Rectangle(0, 0, 1, 1);
                menu.Elements.Add(i);
            }
            {
                Game.MenuElements.BoxButton bb = new Game.MenuElements.BoxButton();
                bb.Depth = -6;
                bb.ImageHeight = 60;
                bb.ImageWidth = 240;
                bb.OffImagePath = "ui\\enter0.png";
                bb.OnImagePath = "ui\\enter1.png";
                bb.Region = new Rectangle(520 / 1280f, 60 / 960f, 760 / 1280f, 120 / 960f);
                bb.CollideRegion = new Rectangle(0, 0, 240 / 1280f, 60 / 960f);
                bb.Event = "MenuExit";
                menu.Elements.Add(bb);
            }
            menuDic.Add("Story", menu);
        }
        {
            menu = new Menu();
            menu.Elements = new List<MenuElement>();
            {
                Game.MenuElements.Image i = new Game.MenuElements.Image();
                i.Depth = -5;
                i.ImageHeight = 960;
                i.ImageWidth = 1280;
                i.ImagePath = "ui\\startbg.png";
                i.Region = new Rectangle(0, 0, 1, 1);
                menu.Elements.Add(i);
            }
            {
                Game.MenuElements.ClickArea ca = new Game.MenuElements.ClickArea();
                ca.CollideRegion = new Rectangle(400 / 1280f, 240 / 960f, 880 / 1280f, 370 / 960f);
                ca.Depth = -6;
                ca.Event = "MenuExit";
                menu.Elements.Add(ca);
            }
            menuDic.Add("Main", menu);
        }
        {
            menu = new Menu();
            menu.Elements = new List<MenuElement>();
            {
                Game.MenuElements.Image i = new Game.MenuElements.Image();
                i.Depth = -5;
                i.ImageHeight = 520;
                i.ImageWidth = 800;
                i.ImagePath = "ui\\pausebg.png";
                i.Region = new Rectangle(240/1280f, 240/960f, 1040/1280f, 720/960f);
                menu.Elements.Add(i);
            }
            {
                Game.MenuElements.BoxButton bb = new Game.MenuElements.BoxButton();
                bb.Depth = -6;
                bb.ImageHeight = 160;
                bb.ImageWidth = 160;
                bb.OffImagePath = "ui\\resume0.png";
                bb.OnImagePath = "ui\\resume1.png";
                bb.Region = new Rectangle(400 / 1280f, 400 / 960f, 560 / 1280f, 560 / 960f);
                bb.CollideRegion = new Rectangle(0, 0, 160 / 1280f, 160 / 960f);
                bb.Event = "MenuExit";
                menu.Elements.Add(bb);
            }
            {
                Game.MenuElements.BoxButton bb = new Game.MenuElements.BoxButton();
                bb.Depth = -6;
                bb.ImageHeight = 160;
                bb.ImageWidth = 160;
                bb.OffImagePath = "ui\\exit0.png";
                bb.OnImagePath = "ui\\exit1.png";
                bb.Region = new Rectangle(720 / 1280f, 400 / 960f, 880 / 1280f, 560 / 960f);
                bb.CollideRegion = new Rectangle(0, 0, 160 / 1280f, 160 / 960f);
                bb.Event = "GameExit";
                menu.Elements.Add(bb);
            }
            menuDic.Add("Paused", menu);
        }
        {
            menu = new Menu();
            menu.Elements = new List<MenuElement>();
            {
                Game.MenuElements.Image i = new Game.MenuElements.Image();
                i.Depth = -5;
                i.ImageHeight = 960;
                i.ImageWidth = 1280;
                i.ImagePath = "ui\\failbg.png";
                i.Region = new Rectangle(0, 0, 1, 1);
                menu.Elements.Add(i);
            }
            {
                Game.MenuElements.BoxButton bb = new Game.MenuElements.BoxButton();
                bb.Depth = -6;
                bb.ImageHeight = 60;
                bb.ImageWidth = 240;
                bb.OffImagePath = "ui\\retry0.png";
                bb.OnImagePath = "ui\\retry1.png";
                bb.Region = new Rectangle(520 / 1280f, 300 / 960f, 760 / 1280f, 360 / 960f);
                bb.CollideRegion = new Rectangle(0, 0, 240 / 1280f, 60 / 960f);
                bb.Event = "GameRestart";
                menu.Elements.Add(bb);
            }
            {
                Game.MenuElements.BoxButton bb = new Game.MenuElements.BoxButton();
                bb.Depth = -6;
                bb.ImageHeight = 80;
                bb.ImageWidth = 80;
                bb.OffImagePath = "ui\\close0.png";
                bb.OnImagePath = "ui\\close1.png";
                bb.Region = new Rectangle(960 / 1280f, 640 / 960f, 1040 / 1280f, 720 / 960f);
                bb.CollideRegion = new Rectangle(0, 0, 80 / 1280f, 80 / 960f);
                bb.Event = "MenuExit";
                menu.Elements.Add(bb);
            }
            menuDic.Add("Failed", menu);
        }
        {
            menu = new Menu();
            menu.Elements = new List<MenuElement>();
            {
                Game.MenuElements.Image i = new Game.MenuElements.Image();
                i.Depth = -5;
                i.ImageHeight = 520;
                i.ImageWidth = 800;
                i.ImagePath = "ui\\808succ.png";
                i.Region = new Rectangle(240 / 1280f, 240 / 960f, 1040 / 1280f, 720 / 960f);
                menu.Elements.Add(i);
            }
            {
                Game.MenuElements.BoxButton bb = new Game.MenuElements.BoxButton();
                bb.Depth = -6;
                bb.ImageHeight = 60;
                bb.ImageWidth = 240;
                bb.OffImagePath = "ui\\next0.png";
                bb.OnImagePath = "ui\\next1.png";
                bb.Region = new Rectangle(520 / 1280f, 300 / 960f, 760 / 1280f, 360 / 960f);
                bb.CollideRegion = new Rectangle(0, 0, 240 / 1280f, 60 / 960f);
                bb.Event = "MenuExit";
                menu.Elements.Add(bb);
            }
            {
                Game.MenuElements.BoxButton bb = new Game.MenuElements.BoxButton();
                bb.Depth = -6;
                bb.ImageHeight = 80;
                bb.ImageWidth = 80;
                bb.OffImagePath = "ui\\close0.png";
                bb.OnImagePath = "ui\\close1.png";
                bb.Region = new Rectangle(960 / 1280f, 640 / 960f, 1040 / 1280f, 720 / 960f);
                bb.CollideRegion = new Rectangle(0, 0, 80 / 1280f, 80 / 960f);
                bb.Event = "MenuExit";
                menu.Elements.Add(bb);
            }
            menuDic.Add("808Succ", menu);
        }
        {
            menu = new Menu();
            menu.Elements = new List<MenuElement>();
            {
                Game.MenuElements.Image i = new Game.MenuElements.Image();
                i.Depth = -5;
                i.ImageHeight = 520;
                i.ImageWidth = 800;
                i.ImagePath = "ui\\809succ.png";
                i.Region = new Rectangle(240 / 1280f, 240 / 960f, 1040 / 1280f, 720 / 960f);
                menu.Elements.Add(i);
            }
            {
                Game.MenuElements.BoxButton bb = new Game.MenuElements.BoxButton();
                bb.Depth = -6;
                bb.ImageHeight = 60;
                bb.ImageWidth = 240;
                bb.OffImagePath = "ui\\next0.png";
                bb.OnImagePath = "ui\\next1.png";
                bb.Region = new Rectangle(520 / 1280f, 300 / 960f, 760 / 1280f, 360 / 960f);
                bb.CollideRegion = new Rectangle(0, 0, 240 / 1280f, 60 / 960f);
                bb.Event = "MenuExit";
                menu.Elements.Add(bb);
            }
            {
                Game.MenuElements.BoxButton bb = new Game.MenuElements.BoxButton();
                bb.Depth = -6;
                bb.ImageHeight = 80;
                bb.ImageWidth = 80;
                bb.OffImagePath = "ui\\close0.png";
                bb.OnImagePath = "ui\\close1.png";
                bb.Region = new Rectangle(960 / 1280f, 640 / 960f, 1040 / 1280f, 720 / 960f);
                bb.CollideRegion = new Rectangle(0, 0, 80 / 1280f, 80 / 960f);
                bb.Event = "MenuExit";
                menu.Elements.Add(bb);
            }
            menuDic.Add("809Succ", menu);
        }
        {
            menu = new Menu();
            menu.Elements = new List<MenuElement>();
            {
                Game.MenuElements.Image i = new Game.MenuElements.Image();
                i.Depth = -5;
                i.ImageHeight = 520;
                i.ImageWidth = 800;
                i.ImagePath = "ui\\810succ.png";
                i.Region = new Rectangle(240 / 1280f, 240 / 960f, 1040 / 1280f, 720 / 960f);
                menu.Elements.Add(i);
            }
            {
                Game.MenuElements.BoxButton bb = new Game.MenuElements.BoxButton();
                bb.Depth = -6;
                bb.ImageHeight = 60;
                bb.ImageWidth = 240;
                bb.OffImagePath = "ui\\next0.png";
                bb.OnImagePath = "ui\\next1.png";
                bb.Region = new Rectangle(520 / 1280f, 300 / 960f, 760 / 1280f, 360 / 960f);
                bb.CollideRegion = new Rectangle(0, 0, 240 / 1280f, 60 / 960f);
                bb.Event = "MenuExit";
                menu.Elements.Add(bb);
            }
            {
                Game.MenuElements.BoxButton bb = new Game.MenuElements.BoxButton();
                bb.Depth = -6;
                bb.ImageHeight = 80;
                bb.ImageWidth = 80;
                bb.OffImagePath = "ui\\close0.png";
                bb.OnImagePath = "ui\\close1.png";
                bb.Region = new Rectangle(960 / 1280f, 640 / 960f, 1040 / 1280f, 720 / 960f);
                bb.CollideRegion = new Rectangle(0, 0, 80 / 1280f, 80 / 960f);
                bb.Event = "MenuExit";
                menu.Elements.Add(bb);
            }
            menuDic.Add("810Succ", menu);
        }
        {
            menu = new Menu();
            menu.Elements = new List<MenuElement>();
            {
                Game.MenuElements.Image i = new Game.MenuElements.Image();
                i.Depth = -5;
                i.ImageHeight = 520;
                i.ImageWidth = 800;
                i.ImagePath = "ui\\811succ.png";
                i.Region = new Rectangle(240 / 1280f, 240 / 960f, 1040 / 1280f, 720 / 960f);
                menu.Elements.Add(i);
            }
            {
                Game.MenuElements.BoxButton bb = new Game.MenuElements.BoxButton();
                bb.Depth = -6;
                bb.ImageHeight = 60;
                bb.ImageWidth = 240;
                bb.OffImagePath = "ui\\retry0.png";
                bb.OnImagePath = "ui\\retry1.png";
                bb.Region = new Rectangle(520 / 1280f, 300 / 960f, 760 / 1280f, 360 / 960f);
                bb.CollideRegion = new Rectangle(0, 0, 240 / 1280f, 60 / 960f);
                bb.Event = "GameRestart";
                menu.Elements.Add(bb);
            }
            {
                Game.MenuElements.BoxButton bb = new Game.MenuElements.BoxButton();
                bb.Depth = -6;
                bb.ImageHeight = 80;
                bb.ImageWidth = 80;
                bb.OffImagePath = "ui\\close0.png";
                bb.OnImagePath = "ui\\close1.png";
                bb.Region = new Rectangle(960 / 1280f, 640 / 960f, 1040 / 1280f, 720 / 960f);
                bb.CollideRegion = new Rectangle(0, 0, 80 / 1280f, 80 / 960f);
                bb.Event = "MenuExit";
                menu.Elements.Add(bb);
            }
            menuDic.Add("811Succ", menu);
        }
        xd = new XDocument();
        xd.Add(Game.Transform.TranslateDic("Root", BasicTranslation.StringD2A, Menu.D2E, menuDic));
        xd.Save("Menus.xml");*/
        
        GameManager.Init("GameInfo.xml");
        GameManager.Start();
    }
    public void Update() {
        GameManager.Update();
        Resources.UnloadUnusedAssets();
    }
}