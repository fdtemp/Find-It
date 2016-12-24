using Game;
using System.Collections.Generic;

public partial class Data {
    public static void Create_Scene_809_1() {
        Scene scene = new Scene();
        scene.Name = "809_1";
        scene.BGImagePath = "809\\bg.png";
        scene.BGImageWidth = 1280;
        scene.BGImageHeight = 960;
        scene.BrickYAmount = 12;
        scene.BrickXAmount = 16;
        scene.BrickDefine = new Dictionary<char, string>();
        scene.BrickDefine.Add('0', "809wall");
        scene.BrickDefine.Add('1', "809earth1");
        scene.BrickDefine.Add('2', "809earth2");
        scene.BrickDefine.Add('3', "809earth3");
        scene.BrickDefine.Add('4', "809transleft0");
        scene.BrickDefine.Add('5', "809transright0");
        scene.BrickMap =
            "+                " +
            "+                " +
            "+                " +
            "+                " +
            "+                " +
            "+                " +
            "+                " +
            "+                " +
            "+              45" +
            "+      1111111111" +
            "+1111112222222222" +
            "+3333333333333333";
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
                cm.MonsterKind = "Devil";
                cm.MonsterName = "m1";
                cm.Position = new ScenePosition(15, 5);
                stage.Events.Add("cm1", cm);
            }
            {
                Game.Events.CreateMonster cm = new Game.Events.CreateMonster();
                cm.Active = true;
                cm.ActiveTime = 0;
                cm.Condition = new Game.EventConditions.Always();
                cm.DelayTime = 1;
                cm.MonsterKind = "Devil";
                cm.MonsterName = "m2";
                cm.Position = new ScenePosition(12, 5);
                stage.Events.Add("cm2", cm);
            }
            {
                Game.Events.CreateMenu cm = new Game.Events.CreateMenu();
                cm.Active = true;
                cm.ActiveTime = 0;
                cm.Condition = new Game.EventConditions.AllMonsterIsKilled();
                cm.DelayTime = 1;
                cm.MenuName = "809Succ";
                stage.Events.Add("gift", cm);
            }
            {
                Game.Events.WakeUpEvents wue = new Game.Events.WakeUpEvents();
                wue.Active = true;
                wue.ActiveTime = 0;
                {
                    Game.EventConditions.And a = new Game.EventConditions.And();
                    {
                        Game.EventConditions.PlayerInSceneRegion pisr = new Game.EventConditions.PlayerInSceneRegion();
                        pisr.Start = new ScenePosition(14, 3);
                        pisr.End = new ScenePosition(15, 3);
                        a.A = pisr;
                    }
                    a.B = new Game.EventConditions.AllMonsterIsKilled();
                    wue.Condition = a;
                }
                wue.DelayTime = 0;
                wue.EventNames = new List<string>();

                wue.EventNames.Add("trans1");
                wue.EventNames.Add("trans2");
                wue.EventNames.Add("ss");
                stage.Events.Add("wue", wue);
            }
            {
                Game.Events.BrickSwitch bs = new Game.Events.BrickSwitch();
                bs.Active = false;
                bs.ActiveTime = 0.5f;
                bs.BrickKind = "809transleft1";
                bs.Condition = new Game.EventConditions.Always();
                bs.DelayTime = 0.1f;
                bs.Position = new ScenePosition(14, 3);
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
                bs.Position = new ScenePosition(15, 3);
                bs.SwitchTime = 0.5f;
                stage.Events.Add("trans2", bs);
            }
            {
                Game.Events.SceneSwitch ss = new Game.Events.SceneSwitch();
                ss.Active = false;
                ss.ActiveTime = 0;
                ss.Condition = new Game.EventConditions.Always();
                ss.DelayTime = 1;
                ss.SceneName = "810_1";
                ss.StartPosition = new ScenePosition(0, 3);
                stage.Events.Add("ss", ss);
            }
            scene.Stages.Add("Main", stage);
        }
        scene.Unload().Save("Scene_809_1.xml");
    }
}
