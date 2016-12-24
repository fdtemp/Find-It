using Game;
using System.Collections.Generic;

public partial class Data {
    public static void Create_Scene_811_1() {
        Scene scene = new Scene();
        scene.Name = "811_1";
        scene.BGImagePath = "811\\bg.png";
        scene.BGImageWidth = 1280;
        scene.BGImageHeight = 960;
        scene.BrickYAmount = 12;
        scene.BrickXAmount = 16;
        scene.BrickDefine = new Dictionary<char, string>();
        scene.BrickDefine.Add('0', "811earth");
        scene.BrickDefine.Add('1', "811cloud1");
        scene.BrickDefine.Add('2', "811cloud2");
        scene.BrickDefine.Add('3', "811cloud3");
        scene.BrickDefine.Add('4', "811cloud4");
        scene.BrickDefine.Add('5', "811moon");
        scene.BrickDefine.Add('6', "810transleftbot");
        scene.BrickDefine.Add('7', "810transrightbot");
        scene.BrickDefine.Add('8', "810translefttop");
        scene.BrickDefine.Add('9', "810transrighttop");
        scene.BrickMap =
            "+      1         " +
            "+  5 3      4    " +
            "+  1   4   1     " +
            "+     2       1  " +
            "+   2      3     " +
            "+             3  " +
            "+      4         " +
            "+ 89             " +
            "+ 67             " +
            "+                " +
            "+0000000000000000" +
            "+0000000000000000";
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
                cm.Position = new ScenePosition(13, 5);
                stage.Events.Add("cm2", cm);
            }
            {
                Game.Events.CreateMonster cm = new Game.Events.CreateMonster();
                cm.Active = true;
                cm.ActiveTime = 0;
                cm.Condition = new Game.EventConditions.Always();
                cm.DelayTime = 1;
                cm.MonsterKind = "Ghost";
                cm.MonsterName = "m3";
                cm.Position = new ScenePosition(11, 5);
                stage.Events.Add("cm3", cm);
            }
            {
                Game.Events.CreateMonster cm = new Game.Events.CreateMonster();
                cm.Active = true;
                cm.ActiveTime = 0;
                cm.Condition = new Game.EventConditions.Always();
                cm.DelayTime = 1;
                cm.MonsterKind = "Ghost";
                cm.MonsterName = "m4";
                cm.Position = new ScenePosition(9, 5);
                stage.Events.Add("cm4", cm);
            }
            {
                Game.Events.CreateMonster cm = new Game.Events.CreateMonster();
                cm.Active = true;
                cm.ActiveTime = 0;
                cm.Condition = new Game.EventConditions.Always();
                cm.DelayTime = 1;
                cm.MonsterKind = "Ghost";
                cm.MonsterName = "m5";
                cm.Position = new ScenePosition(7, 5);
                stage.Events.Add("cm5", cm);
            }
            {
                Game.Events.CreateMenu cm = new Game.Events.CreateMenu();
                cm.Active = true;
                cm.ActiveTime = 0;
                cm.Condition = new Game.EventConditions.AllMonsterIsKilled();
                cm.DelayTime = 1;
                cm.MenuName = "811Succ";
                stage.Events.Add("gift", cm);
            }
            {
                Game.Events.BrickSwitch bs = new Game.Events.BrickSwitch();
                bs.Active = true;
                bs.ActiveTime = 0.5f;
                bs.BrickKind = "null";
                bs.Condition = new Game.EventConditions.Always();
                bs.DelayTime = 0.1f;
                bs.Position = new ScenePosition(1, 3);
                bs.SwitchTime = 0.5f;
                stage.Events.Add("trans1", bs);
            }
            {
                Game.Events.BrickSwitch bs = new Game.Events.BrickSwitch();
                bs.Active = true;
                bs.ActiveTime = 0.5f;
                bs.BrickKind = "null";
                bs.Condition = new Game.EventConditions.Always();
                bs.DelayTime = 0.1f;
                bs.Position = new ScenePosition(2, 3);
                bs.SwitchTime = 0.5f;
                stage.Events.Add("trans2", bs);
            }
            {
                Game.Events.BrickSwitch bs = new Game.Events.BrickSwitch();
                bs.Active = true;
                bs.ActiveTime = 0.5f;
                bs.BrickKind = "null";
                bs.Condition = new Game.EventConditions.Always();
                bs.DelayTime = 0.1f;
                bs.Position = new ScenePosition(1, 4);
                bs.SwitchTime = 0.5f;
                stage.Events.Add("trans3", bs);
            }
            {
                Game.Events.BrickSwitch bs = new Game.Events.BrickSwitch();
                bs.Active = true;
                bs.ActiveTime = 0.5f;
                bs.BrickKind = "null";
                bs.Condition = new Game.EventConditions.Always();
                bs.DelayTime = 0.1f;
                bs.Position = new ScenePosition(2, 4);
                bs.SwitchTime = 0.5f;
                stage.Events.Add("trans4", bs);
            }
            scene.Stages.Add("Main", stage);
        }
        scene.Unload().Save("Scene_811_1.xml");
    }
}
