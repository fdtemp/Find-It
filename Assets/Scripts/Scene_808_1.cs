using Game;
using System.Collections.Generic;

public partial class Data {
    public static void Create_Scene_808_1() {
        Scene scene = new Scene();
        scene.Name = "808_1";
        scene.BGImagePath = "808\\bg.png";
        scene.BGImageWidth = 1280;
        scene.BGImageHeight = 960;
        scene.BrickYAmount = 12;
        scene.BrickXAmount = 16;
        scene.BrickDefine = new Dictionary<char, string>();
        scene.BrickDefine.Add('0', "808metal");
        scene.BrickDefine.Add('1', "808chain");
        scene.BrickDefine.Add('2', "808bush");
        scene.BrickDefine.Add('3', "808star");
        scene.BrickDefine.Add('4', "808cloud1");
        scene.BrickDefine.Add('5', "808cloud2");
        scene.BrickDefine.Add('6', "808nail");
        scene.BrickMap =
            "+         3   11 " +
            "+  3          11 " +
            "+           3 1  " +
            "+     3       1  " +
            "+   5         1  " +
            "+           4    " +
            "+                " +
            "+                " +
            "+     22     2   " +
            "+0000000000000000" +
            "+000001 000100100" +
            "+0000000  0000100";
        scene.Stages = new Dictionary<string, Stage>();
        {
            Stage stage = new Stage();
            stage.Events = new Dictionary<string, Event>();
            {
                Game.Events.CreateMonster cm = new Game.Events.CreateMonster();
                cm.Active = true;
                cm.ActiveTime = 0;
                cm.Condition = new Game.EventConditions.Always();
                cm.DelayTime = 1;
                cm.MonsterKind = "Dragon";
                cm.MonsterName = "m1";
                cm.Position = new ScenePosition(15, 5);
                stage.Events.Add("cm1", cm);
            }
            {
                Game.Events.SceneSwitch ss = new Game.Events.SceneSwitch();
                ss.Active = true;
                ss.ActiveTime = 0;
                {
                    Game.EventConditions.PlayerInSceneRegion pisr = new Game.EventConditions.PlayerInSceneRegion();
                    pisr.Start = new ScenePosition(15, 3);
                    pisr.End = new ScenePosition(15, 11);
                    ss.Condition = pisr;
                }
                ss.DelayTime = 0;
                ss.SceneName = "808_2";
                ss.StartPosition = new ScenePosition(1, 3);
                stage.Events.Add("ss", ss);
            }
            {
                Game.Events.BrickSwitch bs = new Game.Events.BrickSwitch();
                bs.Active = false;
                bs.ActiveTime = 0.5f;
                bs.BrickKind = "null";
                bs.Condition = new Game.EventConditions.Always();
                bs.DelayTime = 0;
                bs.Position = new ScenePosition(2, 10);
                bs.SwitchTime = 0.5f;
                stage.Events.Add("Star1_0", bs);
            }
            {
                Game.Events.BrickSwitch bs = new Game.Events.BrickSwitch();
                bs.Active = false;
                bs.ActiveTime = 0.5f;
                bs.BrickKind = "null";
                bs.Condition = new Game.EventConditions.Always();
                bs.DelayTime = 0.1f;
                bs.Position = new ScenePosition(5, 8);
                bs.SwitchTime = 0.5f;
                stage.Events.Add("Star2_0", bs);
            }
            {
                Game.Events.BrickSwitch bs = new Game.Events.BrickSwitch();
                bs.Active = false;
                bs.ActiveTime = 0.5f;
                bs.BrickKind = "null";
                bs.Condition = new Game.EventConditions.Always();
                bs.DelayTime = 0.5f;
                bs.Position = new ScenePosition(11, 9);
                bs.SwitchTime = 0.5f;
                stage.Events.Add("Star3_0", bs);
            }
            {
                Game.Events.BrickSwitch bs = new Game.Events.BrickSwitch();
                bs.Active = false;
                bs.ActiveTime = 0.5f;
                bs.BrickKind = "null";
                bs.Condition = new Game.EventConditions.Always();
                bs.DelayTime = 0.9f;
                bs.Position = new ScenePosition(9, 11);
                bs.SwitchTime = 0.5f;
                stage.Events.Add("Star4_0", bs);
            }
            {
                Game.Events.BrickSwitch bs = new Game.Events.BrickSwitch();
                bs.Active = false;
                bs.ActiveTime = 0.5f;
                bs.BrickKind = "808star";
                bs.Condition = new Game.EventConditions.Always();
                bs.DelayTime = 1;
                bs.Position = new ScenePosition(2, 10);
                bs.SwitchTime = 0.5f;
                stage.Events.Add("Star1_1", bs);
            }
            {
                Game.Events.BrickSwitch bs = new Game.Events.BrickSwitch();
                bs.Active = false;
                bs.ActiveTime = 0.5f;
                bs.BrickKind = "808star";
                bs.Condition = new Game.EventConditions.Always();
                bs.DelayTime = 1.1f;
                bs.Position = new ScenePosition(5, 8);
                bs.SwitchTime = 0.5f;
                stage.Events.Add("Star2_1", bs);
            }
            {
                Game.Events.BrickSwitch bs = new Game.Events.BrickSwitch();
                bs.Active = false;
                bs.ActiveTime = 0.5f;
                bs.BrickKind = "808star";
                bs.Condition = new Game.EventConditions.Always();
                bs.DelayTime = 1.5f;
                bs.Position = new ScenePosition(11, 9);
                bs.SwitchTime = 0.5f;
                stage.Events.Add("Star3_1", bs);
            }
            {
                Game.Events.BrickSwitch bs = new Game.Events.BrickSwitch();
                bs.Active = false;
                bs.ActiveTime = 0.5f;
                bs.BrickKind = "808star";
                bs.Condition = new Game.EventConditions.Always();
                bs.DelayTime = 1.9f;
                bs.Position = new ScenePosition(9, 11);
                bs.SwitchTime = 0.5f;
                stage.Events.Add("Star4_1", bs);
            }
            {
                Game.Events.WakeUpEvents wue = new Game.Events.WakeUpEvents();
                wue.Active = true;
                wue.ActiveTime = 0;
                wue.Condition = new Game.EventConditions.Always();
                wue.DelayTime = 2.5f;
                wue.EventNames = new List<string>();
                wue.EventNames.Add("Star1_0");
                wue.EventNames.Add("Star2_0");
                wue.EventNames.Add("Star3_0");
                wue.EventNames.Add("Star4_0");
                wue.EventNames.Add("Star1_1");
                wue.EventNames.Add("Star2_1");
                wue.EventNames.Add("Star3_1");
                wue.EventNames.Add("Star4_1");
                wue.EventNames.Add("StarRefresh");
                wue.EventNames.Add("ss");
                stage.Events.Add("StarRefresh", wue);
            }
            scene.Stages.Add("Main", stage);
        }
        scene.Unload().Save("Scene_808_1.xml");
    }
}