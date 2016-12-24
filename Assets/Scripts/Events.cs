using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

namespace Game.Events {
    public static class _Summary {
        public struct Item {
            public string Kind;
            public Transform.Element2Data<Event> E2D;
        }
        public static Item[] List = new Item[] {
            new Item {
                Kind = "SceneSwitch",
                E2D = SceneSwitch.E2D,
            },
            new Item {
                Kind = "StageSwitch",
                E2D = StageSwitch.E2D,
            },
            new Item {
                Kind = "BrickSwitch",
                E2D = BrickSwitch.E2D,
            },
            new Item {
                Kind = "CreateMonster",
                E2D = CreateMonster.E2D,
            },
            new Item {
                Kind = "CreatePlayer",
                E2D = CreatePlayer.E2D,
            },
            new Item {
                Kind = "DestroyPlayer",
                E2D = DestroyPlayer.E2D,
            },
            new Item {
                Kind = "CreateNPC",
                E2D = CreateNPC.E2D,
            },
            new Item {
                Kind = "WakeUpEvents",
                E2D = WakeUpEvents.E2D,
            },
            new Item {
                Kind = "CreateMenu",
                E2D = CreateMenu.E2D,
            },
            new Item {
                Kind = "ShowInfo",
                E2D = ShowInfo.E2D,
            }
        };
    }
    public class SceneSwitch : Event {
        public static new SceneSwitch E2D(XElement e) { return new SceneSwitch(e); }

        public string SceneName;
        public ScenePosition StartPosition;

        public SceneSwitch() { }
        public SceneSwitch(XElement e) { Load(e); }
        public override void Start(Stage stage) { GameManager.SwitchScene(SceneName, StartPosition); }

        public override void Load(XElement e) {
            BaseLoad(e);
            SceneName = BasicTranslation.StringE2D(e.Element("SceneName"));
            StartPosition = ScenePosition.E2D(e.Element("StartPosition"));
        }
        public override XElement Unload(string n) {
            XElement e = BaseUnload(new XElement(n, new XAttribute("Kind", "SceneSwitch")));
            e.Add(BasicTranslation.StringD2E("SceneName", SceneName));
            e.Add(ScenePosition.D2E("StartPosition", StartPosition));
            return e;
        }
    }
    public class StageSwitch : Event {
        public static new StageSwitch E2D(XElement e) { return new StageSwitch(e); }

        public string StageName;

        public StageSwitch() { }
        public StageSwitch(XElement e) { Load(e); }
        public override void Start(Stage stage) { stage.mScene.SwitchStage(StageName); }

        public override void Load(XElement e) {
            BaseLoad(e);
            StageName = BasicTranslation.StringE2D(e.Element("StageName"));
        }
        public override XElement Unload(string n) {
            XElement e = BaseUnload(new XElement(n, new XAttribute("Kind", "StageSwitch")));
            e.Add(BasicTranslation.StringD2E("SceneName", StageName));
            return e;
        }
    }
    public class BrickSwitch : Event {
        public static new BrickSwitch E2D(XElement e) { return new BrickSwitch(e); }

        public ScenePosition Position;
        public string BrickKind;
        public float SwitchTime;
        private Stage s;
        private Brick brick;
        private bool Finished;

        public BrickSwitch() { }
        public BrickSwitch(XElement e) { Load(e); }
        public override void Start(Stage stage) {
            Finished = false;
            s = stage;
            if (BrickKind != "null") {
                brick = GameManager.BrickDic[BrickKind].Clone();
                SpriteRenderer srend = brick.Entity.GetComponent<SpriteRenderer>();
                brick.Entity.SetActive(true);
                brick.SetPosition(Position);
                Color col = srend.color;
                col.a = 0;
                srend.color = col;
            } else {
                brick = null;
            }
        }
        public override void Update() {
            float rate = (s.GetTime() - StartTime) / SwitchTime;
            if (rate > 1) {
                if (!Finished)
                    End();
                return;
            }
            if (s.mScene.Bricks[Position.y, Position.x] != null) {
                SpriteRenderer srend = s.mScene.Bricks[Position.y, Position.x].Entity.GetComponent<SpriteRenderer>();
                Color col = srend.color;
                col.a = 1 - rate;
                srend.color = col;
            }
            if (brick != null) {
                SpriteRenderer srend = brick.Entity.GetComponent<SpriteRenderer>();
                Color col = srend.color;
                col.a = rate;
                srend.color = col;
            }
        }
        public override void End() {
            if (!Finished) {
                Finished = true;
                if (s.mScene.Bricks[Position.y, Position.x] != null) {
                    s.mScene.Bricks[Position.y, Position.x].Destroy();
                    s.mScene.Bricks[Position.y, Position.x] = null;
                }
                if (brick != null)
                    s.mScene.Bricks[Position.y, Position.x] = brick;
            }
        }
        public override void Destroy() {
            if (brick != null) brick.Destroy();
        }

        public override void Load(XElement e) {
            BaseLoad(e);
            Position = ScenePosition.E2D(e.Element("Position"));
            BrickKind = BasicTranslation.StringE2D(e.Element("BrickKind"));
            SwitchTime = BasicTranslation.FloatE2D(e.Element("SwitchTime"));
        }
        public override XElement Unload(string n) {
            XElement e = BaseUnload(new XElement(n, new XAttribute("Kind", "BrickSwitch")));
            e.Add(ScenePosition.D2E("Position", Position));
            e.Add(BasicTranslation.StringD2E("BrickKind", BrickKind));
            e.Add(BasicTranslation.FloatD2E("SwitchTime", SwitchTime));
            return e;
        }
    }
    public class CreateMonster : Event {
        public static new CreateMonster E2D(XElement e) { return new CreateMonster(e); }

        public ScenePosition Position;
        public string MonsterName;
        public string MonsterKind;

        public CreateMonster() { }
        public CreateMonster(XElement e) { Load(e); }
        public override void Start(Stage stage) {
            Living monster = GameManager.LivingDic[MonsterKind].Clone();
            Script_Monster script = monster.Entity.AddComponent<Script_Monster>();
            script.Monster = monster;
            monster.SetTag("Monster");
            monster.SetName(MonsterName);
            monster.SetScenePosition(Position, Living.MONSTER);
            monster.Entity.SetActive(true);
            stage.mScene.Monsters.Add(monster);
        }

        public override void Load(XElement e) {
            BaseLoad(e);
            Position = ScenePosition.E2D(e.Element("Position"));
            MonsterName = BasicTranslation.StringE2D(e.Element("MonsterName"));
            MonsterKind = BasicTranslation.StringE2D(e.Element("MonsterKind"));
        }
        public override XElement Unload(string n) {
            XElement e = BaseUnload(new XElement(n, new XAttribute("Kind", "CreateMonster")));
            e.Add(ScenePosition.D2E("Position", Position));
            e.Add(BasicTranslation.StringD2E("MonsterName", MonsterName));
            e.Add(BasicTranslation.StringD2E("MonsterKind", MonsterKind));
            return e;
        }
    }
    public class CreatePlayer : Event {
        public static new CreatePlayer E2D(XElement e) { return new CreatePlayer(e); }

        public ScenePosition Position;
        public string PlayerKind;

        public CreatePlayer() { }
        public CreatePlayer(XElement e) { Load(e); }
        public override void Start(Stage stage) {
            Living player = GameManager.LivingDic[PlayerKind].Clone();
            Script_Player script = player.Entity.AddComponent<Script_Player>();
            script.Player = player;
            player.SetTag("Player");
            player.SetScenePosition(Position, Living.PLAYER);
            player.Entity.SetActive(true);
            GameManager.Player = player;
        }

        public override void Load(XElement e) {
            BaseLoad(e);
            Position = ScenePosition.E2D(e.Element("Position"));
            PlayerKind = BasicTranslation.StringE2D(e.Element("PlayerKind"));
        }
        public override XElement Unload(string n) {
            XElement e = BaseUnload(new XElement(n, new XAttribute("Kind", "CreatePlayer")));
            e.Add(ScenePosition.D2E("Position", Position));
            e.Add(BasicTranslation.StringD2E("PlayerKind", PlayerKind));
            return e;
        }
    }
    public class DestroyPlayer : Event {
        public static new DestroyPlayer E2D(XElement e) { return new DestroyPlayer(e); }

        public DestroyPlayer() { }
        public DestroyPlayer(XElement e) { Load(e); }
        public override void Start(Stage stage) {
            GameManager.Player.Destroy();
            GameManager.Player = null;
        }

        public override void Load(XElement e) { BaseLoad(e); }
        public override XElement Unload(string n) {
            return BaseUnload(new XElement(n, new XAttribute("Kind", "DestroyPlayer")));
        }
    }

    public class CreateNPC : Event {
        public static new CreateNPC E2D(XElement e) { return new CreateNPC(e); }

        public ScenePosition Position;
        public string NPCKind;

        public CreateNPC() { }
        public CreateNPC(XElement e) { Load(e); }
        public override void Start(Stage stage) {
            Living npc = GameManager.LivingDic[NPCKind].Clone();
            npc.SetTag("NPC");
            npc.SetScenePosition(Position, Living.NPC);
            npc.Entity.SetActive(true);
            stage.mScene.NPCs.Add(npc);
        }

        public override void Load(XElement e) {
            BaseLoad(e);
            Position = ScenePosition.E2D(e.Element("Position"));
            NPCKind = BasicTranslation.StringE2D(e.Element("NPCKind"));
        }
        public override XElement Unload(string n) {
            XElement e = BaseUnload(new XElement(n, new XAttribute("Kind", "CreateNPC")));
            e.Add(ScenePosition.D2E("Position", Position));
            e.Add(BasicTranslation.StringD2E("NPCKind", NPCKind));
            return e;
        }
    }
    public class WakeUpEvents : Event {
        public static new WakeUpEvents E2D(XElement e) { return new WakeUpEvents(e); }

        public List<string> EventNames;

        public WakeUpEvents() { }
        public WakeUpEvents(XElement e) { Load(e); }
        public override void Start(Stage stage) {
            foreach (var name in EventNames) {
                Event e = stage.Events[name];
                e.Active = true;
                e.StartTime = stage.GetTime() + e.DelayTime;
                e.State = WAITING;
                stage.CurrentEvents.Add(e);
            }
        }

        public override void Load(XElement e) {
            BaseLoad(e);
            EventNames = Transform.TranslateList(e.Element("EventNames"), BasicTranslation.StringE2D);
        }
        public override XElement Unload(string n) {
            XElement e = BaseUnload(new XElement(n, new XAttribute("Kind", "WakeUpEvents")));
            e.Add(Transform.TranslateList("EventNames", BasicTranslation.StringD2E, EventNames));
            return e;
        }
    }
    public class CreateMenu : Event {
        public static new CreateMenu E2D(XElement e) { return new CreateMenu(e); }

        public string MenuName;

        public CreateMenu() { }
        public CreateMenu(XElement e) { Load(e); }
        public override void Start(Stage stage) {
            GameManager.ShowMenu(MenuName);
        }

        public override void Load(XElement e) {
            BaseLoad(e);
            MenuName = BasicTranslation.StringE2D(e.Element("MenuName"));
        }
        public override XElement Unload(string n) {
            XElement e = BaseUnload(new XElement(n, new XAttribute("Kind", "CreateMenu")));
            e.Add(BasicTranslation.StringD2E("MenuName", MenuName));
            return e;
        }
    }
    public class ShowInfo : Event {
        public static new ShowInfo E2D(XElement e) { return new ShowInfo(e); }

        public ShowInfo() { }
        public ShowInfo(XElement e) { Load(e); }
        public override void Start(Stage stage) {
            GameManager.VS.SetActive(true);
            GameManager.PContent.SetActive(true);
            GameManager.MContent.SetActive(true);
            GameManager.PIcon.SetActive(true);
            GameManager.MIcon.SetActive(true);
        }

        public override void Load(XElement e) { BaseLoad(e); }
        public override XElement Unload(string n) {
            XElement e = BaseUnload(new XElement(n, new XAttribute("Kind", "ShowInfo")));
            return e;
        }
    }
}