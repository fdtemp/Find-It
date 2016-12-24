using Game;
using System.Collections.Generic;

public partial class Data {
    public static void Create_Scene_Main() {
        Scene scene = new Scene();
        scene.Name = "Main";
        scene.BGImagePath = "809\\bg.png";
        scene.BGImageWidth = 1280;
        scene.BGImageHeight = 960;
        scene.BrickYAmount = 12;
        scene.BrickXAmount = 16;
        scene.BrickDefine = new Dictionary<char, string>();
        scene.BrickDefine.Add('1', "808metal");
        scene.BrickDefine.Add('2', "809wall");
        scene.BrickDefine.Add('3', "810stone");
        scene.BrickDefine.Add('4', "811earth");
        scene.BrickMap =
            "+1113213213211232" +
            "+1312313231213213" +
            "+1313211232321131" +
            "+   111222333    " +
            "+   1 12 23 3    " +
            "+   111222333    " +
            "+                " +
            "+                " +
            "+4441 12 23 34444" +
            "+4441112223334444" +
            "+4444444444444444" +
            "+4444444444444444";
        scene.Stages = new Dictionary<string, Stage>();
        {
            Stage stage = new Stage();
            stage.Events = new Dictionary<string, Event>();
            {
                Game.Events.CreateMenu cm = new Game.Events.CreateMenu();
                cm.Active = true;
                cm.ActiveTime = 0;
                cm.Condition = new Game.EventConditions.Always();
                cm.DelayTime = 0;
                cm.MenuName = "Story";
                stage.Events.Add("showinfo", cm);
            }
            {
                Game.Events.ShowInfo si = new Game.Events.ShowInfo();
                si.Active = true;
                si.ActiveTime = 0;
                si.Condition = new Game.EventConditions.Always();
                si.DelayTime = 1;
                stage.Events.Add("story", si);
            }
            {
                Game.Events.CreatePlayer cp = new Game.Events.CreatePlayer();
                cp.Active = true;
                cp.ActiveTime = 0;
                cp.Condition = new Game.EventConditions.Always();
                cp.DelayTime = 0;
                cp.PlayerKind = "NPC";
                cp.Position = new ScenePosition(0, 4);
                stage.Events.Add("cp", cp);
            }
            {
                Game.Events.DestroyPlayer dp = new Game.Events.DestroyPlayer();
                dp.Active = false;
                dp.ActiveTime = 0;
                dp.Condition = new Game.EventConditions.Always();
                dp.DelayTime = 0;
                stage.Events.Add("DestoryPlayer", dp);
            }
            {
                Game.Events.SceneSwitch ss = new Game.Events.SceneSwitch();
                ss.Active = false;
                ss.ActiveTime = 0;
                ss.Condition = new Game.EventConditions.Always();
                ss.DelayTime = 1;
                ss.SceneName = "808_1";
                ss.StartPosition = new ScenePosition(0, 3);
                stage.Events.Add("SwitchScene", ss);
            }
            {
                Game.Events.CreateNPC cn = new Game.Events.CreateNPC();
                cn.Active = true;
                cn.ActiveTime = 0;
                cn.Condition = new Game.EventConditions.Always();
                cn.DelayTime = 0;
                cn.NPCKind = "PM";
                cn.Position = new ScenePosition(4, 7);
                stage.Events.Add("c1", cn);
            }
            {
                Game.Events.CreatePlayer cp = new Game.Events.CreatePlayer();
                cp.Active = false;
                cp.ActiveTime = 0;
                cp.Condition = new Game.EventConditions.Always();
                cp.DelayTime = 0.5f;
                cp.PlayerKind = "PM";
                cp.Position = new ScenePosition(4, 3);
                stage.Events.Add("cp1", cp);
            }
            {
                Game.Events.WakeUpEvents wue = new Game.Events.WakeUpEvents();
                wue.Active = true;
                wue.ActiveTime = 0;
                {
                    Game.EventConditions.PlayerInSceneRegion pisr = new Game.EventConditions.PlayerInSceneRegion();
                    pisr.Start = new ScenePosition(4, 3);
                    pisr.End = new ScenePosition(4, 3);
                    wue.Condition = pisr;
                }
                wue.DelayTime = 0;
                wue.EventNames = new List<string>();
                wue.EventNames.Add("DestoryPlayer");
                wue.EventNames.Add("cp1");
                wue.EventNames.Add("SwitchScene");
                stage.Events.Add("wue1", wue);
            }
            {
                Game.Events.CreateNPC cn = new Game.Events.CreateNPC();
                cn.Active = true;
                cn.ActiveTime = 0;
                cn.Condition = new Game.EventConditions.Always();
                cn.DelayTime = 0;
                cn.NPCKind = "Designer";
                cn.Position = new ScenePosition(7, 7);
                stage.Events.Add("c2", cn);
            }
            {
                Game.Events.CreatePlayer cp = new Game.Events.CreatePlayer();
                cp.Active = false;
                cp.ActiveTime = 0;
                cp.Condition = new Game.EventConditions.Always();
                cp.DelayTime = 0.5f;
                cp.PlayerKind = "Designer";
                cp.Position = new ScenePosition(7, 3);
                stage.Events.Add("cp2", cp);
            }
            {
                Game.Events.WakeUpEvents wue = new Game.Events.WakeUpEvents();
                wue.Active = true;
                wue.ActiveTime = 0;
                {
                    Game.EventConditions.PlayerInSceneRegion pisr = new Game.EventConditions.PlayerInSceneRegion();
                    pisr.Start = new ScenePosition(7, 3);
                    pisr.End = new ScenePosition(7, 3);
                    wue.Condition = pisr;
                }
                wue.DelayTime = 0;
                wue.EventNames = new List<string>();
                wue.EventNames.Add("DestoryPlayer");
                wue.EventNames.Add("cp2");
                wue.EventNames.Add("SwitchScene");
                stage.Events.Add("wue2", wue);
            }
            {
                Game.Events.CreateNPC cn = new Game.Events.CreateNPC();
                cn.Active = true;
                cn.ActiveTime = 0;
                cn.Condition = new Game.EventConditions.Always();
                cn.DelayTime = 0;
                cn.NPCKind = "Coder";
                cn.Position = new ScenePosition(10, 7);
                stage.Events.Add("c3", cn);
            }
            {
                Game.Events.CreatePlayer cp = new Game.Events.CreatePlayer();
                cp.Active = false;
                cp.ActiveTime = 0;
                cp.Condition = new Game.EventConditions.Always();
                cp.DelayTime = 0.5f;
                cp.PlayerKind = "Coder";
                cp.Position = new ScenePosition(10, 3);
                stage.Events.Add("cp3", cp);
            }
            {
                Game.Events.WakeUpEvents wue = new Game.Events.WakeUpEvents();
                wue.Active = true;
                wue.ActiveTime = 0;
                {
                    Game.EventConditions.PlayerInSceneRegion pisr = new Game.EventConditions.PlayerInSceneRegion();
                    pisr.Start = new ScenePosition(10, 3);
                    pisr.End = new ScenePosition(10, 3);
                    wue.Condition = pisr;
                }
                wue.DelayTime = 0;
                wue.EventNames = new List<string>();
                wue.EventNames.Add("DestoryPlayer");
                wue.EventNames.Add("cp3");
                wue.EventNames.Add("SwitchScene");
                stage.Events.Add("wue3", wue);
            }
            scene.Stages.Add("Main", stage);
        }
        scene.Unload().Save("Scene_Main.xml");
    }
}