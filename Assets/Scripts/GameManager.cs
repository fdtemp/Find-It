using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using Game;

public static class GameManager {

    public static Dictionary<string, Game.Transform.Element2Data<EventCondition>>
        ConditionE2DDic = new Dictionary<string, Game.Transform.Element2Data<EventCondition>>();
    public static Dictionary<string, Game.Transform.Element2Data<Game.Event>>
        EventE2DDic = new Dictionary<string, Game.Transform.Element2Data<Game.Event>>();
    public static Dictionary<string, Game.Transform.Element2Data<MenuElement>>
        MenuElementE2DDic = new Dictionary<string, Game.Transform.Element2Data<MenuElement>>();

    public static GameInfo GameInfo;
    public static Dictionary<string, Scene> SceneDic;
    public static Dictionary<string, Brick> BrickDic;
    public static Dictionary<string, Living> LivingDic;
    public static Dictionary<string, Menu> MenuDic;

    public static Dictionary<string, byte[]> ImageDic;
    public static string _GameInfoPath;
    public static System.Random Random;
    public static Scene CurrentScene;
    public static Menu CurrentMenu;
    public static Living Player, _Monster;
    public static Vector2 _Player;
    public static Camera Camera;

    public static GameObject
        VS, PContent, MContent, PIcon, MIcon, PText, MText;

    public static void Init(string GameInfoPath) {
        _GameInfoPath = GameInfoPath;
        ImageDic = new Dictionary<string, byte[]>();
        foreach (var item in Game.EventConditions._Summary.List)
            ConditionE2DDic[item.Kind] = item.E2D;
        foreach (var item in Game.Events._Summary.List)
            EventE2DDic[item.Kind] = item.E2D;
        foreach (var item in Game.MenuElements._Summary.List)
            MenuElementE2DDic[item.Kind] = item.E2D;
        //GameInfo
        GameInfo = new GameInfo(XDocument.Load(GameInfoPath));
        //Bricks
        BrickDic = Game.Transform.TranslateDic(
            XDocument.Load(GameInfo.BrickPath).Root,
            BasicTranslation.StringA2D,
            Brick.E2D);
        //Livings
        LivingDic = Game.Transform.TranslateDic(
            XDocument.Load(GameInfo.LivingPath).Root,
            BasicTranslation.StringA2D,
            Living.E2D);
        //Menus
        MenuDic = Game.Transform.TranslateDic(
            XDocument.Load(GameInfo.MenuPath).Root,
            BasicTranslation.StringA2D,
            Menu.E2D);
        //Scenes
        SceneDic = new Dictionary<string, Scene>();
        foreach (var path in GameInfo.ScenePaths) {
            Scene scene = new Scene(XDocument.Load(path));
            SceneDic[scene.Name] = scene;
        }
        VS = GameObject.Find("VS");
        PContent = GameObject.Find("PContent");
        MContent = GameObject.Find("MContent");
        PIcon = GameObject.Find("PIcon");
        MIcon = GameObject.Find("MIcon");
        PText = GameObject.Find("PText");
        MText = GameObject.Find("MText");
        VS.SetActive(false);
        PContent.SetActive(false);
        MContent.SetActive(false);
        PIcon.SetActive(false);
        MIcon.SetActive(false);
        PText.GetComponent<UnityEngine.UI.Text>().text = "";
        MText.GetComponent<UnityEngine.UI.Text>().text = "";
        _Monster = null;
    }   
    public static void Start() {
        Random = new System.Random();
        Player = null;
        Camera = GameObject.Find("Camera").GetComponent<Camera>();
        CurrentScene = SceneDic["Main"];
        CurrentScene.Start();
        Pause();
        CurrentMenu = MenuDic["Main"];
        CurrentMenu.Start();
    }
    public static void RefreshInfo() {
        byte[] data = GameManager.GetImage(Player.ShowImagePath);
        Texture2D texture = new Texture2D(160, 160);
        texture.LoadImage(data);
        PIcon.GetComponent<UnityEngine.UI.Image>().sprite = Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0, 0));
        PText.GetComponent<UnityEngine.UI.Text>().text = 
            "Name : " + Player.Kind + System.Environment.NewLine +
            "HP: " + Mathf.FloorToInt(Player.HP) + " / " + Mathf.FloorToInt(Player.HPLimit);
        if (_Monster != null) {
            if (_Monster.HP < 0) return;
            data = GameManager.GetImage(_Monster.ShowImagePath);
            texture = new Texture2D(160, 160);
            texture.LoadImage(data);
            MIcon.GetComponent<UnityEngine.UI.Image>().sprite = Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0, 0));
            MText.GetComponent<UnityEngine.UI.Text>().text = 
            "Name : " + _Monster.Kind + System.Environment.NewLine +
            "HP: " + Mathf.FloorToInt(_Monster.HP) + " / " + Mathf.FloorToInt(_Monster.HPLimit);
        }
    }
    public static void Update() {
        if (CurrentMenu != null)
            CurrentMenu.Update();
        CurrentScene.Update();
    }
    public static void Pause() {
        CurrentScene.Pause();
        if (Player != null) {
            Player.Entity.GetComponent<Script_Player>().enabled = false;
            _Player = Player.rb2D.velocity;
            Player.rb2D.Sleep();
        }
    }
    public static void Resume() {
        CurrentScene.Resume();
        if (Player != null) {
            Player.Entity.GetComponent<Script_Player>().enabled = true;
            Player.rb2D.WakeUp();
            Player.rb2D.velocity = _Player;
        }
    }
    public static void End() {
        VS.SetActive(false);
        PContent.SetActive(false);
        MContent.SetActive(false);
        PIcon.SetActive(false);
        MIcon.SetActive(false);
        PText.GetComponent<UnityEngine.UI.Text>().text = "";
        MText.GetComponent<UnityEngine.UI.Text>().text = "";
        if (CurrentMenu != null)
            CurrentMenu.End();
        CurrentScene.End();
    }
    public static void Restart() {
        //Destroy
        ConditionE2DDic.Clear();
        EventE2DDic.Clear();
        foreach (var b in BrickDic)
            b.Value.Destroy();
        foreach (var l in LivingDic)
            l.Value.Destroy();
        foreach (var s in SceneDic)
            s.Value.Destroy();
        if (Player != null)
            Player.Destroy();
        CurrentMenu.End();
        //Restart
        Init(_GameInfoPath);
        Start();
    }
    public static void Exit() {
        Application.Quit();
    }
    public static byte[] GetImage(string ImagePath) {
        if (ImageDic.ContainsKey(ImagePath)) {
            return ImageDic[ImagePath];
        } else {
            System.IO.FileStream fileStream = new System.IO.FileStream(
                ImagePath,
                System.IO.FileMode.Open,
                System.IO.FileAccess.Read);
            fileStream.Seek(0, System.IO.SeekOrigin.Begin);
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);
            fileStream.Close();
            return ImageDic[ImagePath] = bytes;
        }
    }
    public static void SwitchScene(string SceneName, ScenePosition PlayerPosition) {
        CurrentScene.End();
        CurrentScene = SceneDic[SceneName];
        CurrentScene.Start();
        Player.SetScenePosition(PlayerPosition, Living.PLAYER);
    }
    public static void ShowMonsterInfo(Living monster) {
        _Monster = monster;
    }
    public static void ShowMenu(string name) {
        Pause();
        if (CurrentMenu != null)
            CurrentMenu.End();
        CurrentMenu = MenuDic[name];
        CurrentMenu.Start();
    }

    public static class MenuActions {
        public static void ApplyEvent(string Event) {
            switch (Event) {
            case "MenuExit": MenuExit();break;
            case "GameRestart": GameRestart();break;
            case "GameExit": GameExit(); break;
            }
        }
        public static void MenuExit() {
            CurrentMenu.End();
            CurrentMenu = null;
            Resume();
        }
        public static void GameRestart() {
            Restart();
        }
        public static void GameExit() {
            Exit();
        }
    }
}
