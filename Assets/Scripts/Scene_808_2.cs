using Game;
using System.Collections.Generic;

public partial class Data {
    public static void Create_Scene_808_2() {
        Scene scene = new Scene();
        scene.Name = "808_2";
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
        scene.BrickDefine.Add('7', "809transleft0");
        scene.BrickDefine.Add('8', "809transright0");
        scene.BrickMap =
            "+      3 1       " +
            "+  3     1       " +
            "+      451  3    " +
            "+        1       " +
            "+   5    1      3" +
            "+        1  4    " +
            "+        1     78" +
            "+        1     00" +
            "+ 2200   10000000" +
            "+0000000000000000" +
            "+0006  1000000000" +
            "+00000 1  0000000";
        scene.Stages = new Dictionary<string, Stage>();
        {
            Stage stage = new Stage();
            stage.Events = new Dictionary<string, Event>();
            {
                Game.Events.CreateMonster cm = new Game.Events.CreateMonster();
                cm.Active = true;
                cm.ActiveTime = 0;
                cm.Condition = new Game.EventConditions.Always();
                cm.DelayTime = 0;
                cm.MonsterKind = "Dragon";
                cm.MonsterName = "m1";
                cm.Position = new ScenePosition(15, 5);
                stage.Events.Add("cm1", cm);
            }
            {
                Game.Events.CreateMonster cm = new Game.Events.CreateMonster();
                cm.Active = true;
                cm.ActiveTime = 0;
                cm.Condition = new Game.EventConditions.Always();
                cm.DelayTime = 0;
                cm.MonsterKind = "Dragon";
                cm.MonsterName = "m2";
                cm.Position = new ScenePosition(12, 5);
                stage.Events.Add("cm2", cm);
            }
            {
                Game.Events.CreateMonster cm = new Game.Events.CreateMonster();
                cm.Active = true;
                cm.ActiveTime = 0;
                cm.Condition = new Game.EventConditions.Always();
                cm.DelayTime = 0;
                cm.MonsterKind = "Dragon";
                cm.MonsterName = "m3";
                cm.Position = new ScenePosition(9, 5);
                stage.Events.Add("cm3", cm);
            }
            {
                Game.Events.SceneSwitch ss = new Game.Events.SceneSwitch();
                ss.Active = true;
                ss.ActiveTime = 0;
                {
                    Game.EventConditions.PlayerInSceneRegion pisr = new Game.EventConditions.PlayerInSceneRegion();
                    pisr.Start = new ScenePosition(0, 3);
                    pisr.End = new ScenePosition(0, 11);
                    ss.Condition = pisr;
                }
                ss.DelayTime = 0;
                ss.SceneName = "808_1";
                ss.StartPosition = new ScenePosition(14, 3);
                stage.Events.Add("ss1", ss);
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
                bs.Position = new ScenePosition(6, 11);
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
                bs.Position = new ScenePosition(15, 7);
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
                bs.Position = new ScenePosition(6, 11);
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
                bs.Position = new ScenePosition(15, 7);
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
                wue.EventNames.Add("ss1");
                stage.Events.Add("StarRefresh", wue);
            }
            {
                Game.Events.WakeUpEvents wue = new Game.Events.WakeUpEvents();
                wue.Active = true;
                wue.ActiveTime = 0;
                {
                    Game.EventConditions.And a = new Game.EventConditions.And();
                    {
                        Game.EventConditions.PlayerInSceneRegion pisr = new Game.EventConditions.PlayerInSceneRegion();
                        pisr.Start = new ScenePosition(14, 5);
                        pisr.End = new ScenePosition(15, 5);
                        a.A = pisr;
                    }
                    a.B = new Game.EventConditions.AllMonsterIsKilled();
                    wue.Condition = a;
                }
                wue.DelayTime = 0;
                wue.EventNames = new List<string>();
                
                wue.EventNames.Add("trans1");
                wue.EventNames.Add("trans2");
                wue.EventNames.Add("ss2");
                stage.Events.Add("wue", wue);
            }
            {
                Game.Events.CreateMenu cm = new Game.Events.CreateMenu();
                cm.Active = true;
                cm.ActiveTime = 0;
                cm.Condition = new Game.EventConditions.AllMonsterIsKilled();
                cm.DelayTime = 1;
                cm.MenuName = "808Succ";
                stage.Events.Add("gift", cm);
            }
            {
                Game.Events.BrickSwitch bs = new Game.Events.BrickSwitch();
                bs.Active = false;
                bs.ActiveTime = 0.5f;
                bs.BrickKind = "809transleft1";
                bs.Condition = new Game.EventConditions.Always();
                bs.DelayTime = 0.1f;
                bs.Position = new ScenePosition(14, 5);
                bs.SwitchTime = 0.5f;
                stage.Events.Add("trans1", bs);
            }
            {
                Game.Events.BrickSwitch bs = new Game.Events.BrickSwitch();
                bs.Active = false;
                bs.ActiveTime = 0.5f;
                bs.BrickKind = "809transright1";
                bs.Condition = new Game.EventConditions.Always();
                bs.DelayTime = 0.1f;
                bs.Position = new ScenePosition(15, 5);
                bs.SwitchTime = 0.5f;
                stage.Events.Add("trans2", bs);
            }
            {
                Game.Events.SceneSwitch ss = new Game.Events.SceneSwitch();
                ss.Active = false;
                ss.ActiveTime = 0;
                ss.Condition = new Game.EventConditions.Always();
                ss.DelayTime = 1;
                ss.SceneName = "809_1";
                ss.StartPosition = new ScenePosition(0, 2);
                stage.Events.Add("ss2", ss);
            }
            scene.Stages.Add("Main", stage);
        }
        scene.Unload().Save("Scene_808_2.xml");
    }
}