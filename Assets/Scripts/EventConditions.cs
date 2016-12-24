using System;
using System.Xml.Linq;

namespace Game.EventConditions {
    public static class _Summary {
        public struct Item {
            public string Kind;
            public Transform.Element2Data<EventCondition> E2D;
        }
        public static Item[] List = new Item[] {
            new Item {
                Kind = "Always",
                E2D = Always.E2D,
            },
            new Item {
                Kind = "And",
                E2D = And.E2D,
            },
            new Item {
                Kind = "Or",
                E2D = Or.E2D,
            },
            new Item {
                Kind = "PlayerInSceneRegion",
                E2D = PlayerInSceneRegion.E2D,
            },
            new Item {
                Kind = "AllMonsterIsKilled",
                E2D = AllMonsterIsKilled.E2D,
            },
            new Item {
                Kind = "OnlyJudgeOnce",
                E2D = OnlyJudgeOnce.E2D,
            },
        };
    }
    public class Always : EventCondition {
        public static new Always E2D(XElement e) { return new Always(); }

        public override bool IsOK(Stage s, Event e) { return true; }
        public override void Load(XElement e) { }
        public override XElement Unload(string n) { return new XElement(n, new XAttribute("Kind", "Always")); }
    }
    public class PlayerInSceneRegion : EventCondition {
        public static new PlayerInSceneRegion E2D(XElement e) { return new PlayerInSceneRegion(e); }

        public ScenePosition Start;
        public ScenePosition End;

        public PlayerInSceneRegion() { }
        public PlayerInSceneRegion(XElement e) { Load(e); }

        public override bool IsOK(Stage s, Event e) {
            if (GameManager.Player == null) return false;
            ScenePosition pp = GameManager.Player.GetScenePosition();
            if (Start.x <= pp.x && pp.x <= End.x && Start.y <= pp.y && pp.y <= End.y)
                return true;
            else
                return false;
        }
        public override void Load(XElement e) {
            Start = ScenePosition.E2D(e.Element("Start"));
            End = ScenePosition.E2D(e.Element("End"));
        }
        public override XElement Unload(string n) {
            XElement e = new XElement(n, new XAttribute("Kind", "PlayerInSceneRegion"));
            e.Add(ScenePosition.D2E("Start", Start));
            e.Add(ScenePosition.D2E("End", End));
            return e;
        }
    }
    public class AllMonsterIsKilled : EventCondition {
        public static new AllMonsterIsKilled E2D(XElement e) { return new AllMonsterIsKilled(); }

        public override bool IsOK(Stage s, Event e) {
            if (s.mScene.Monsters.Count == 0) return true;
            foreach (var m in s.mScene.Monsters)
                if (m.Entity.activeSelf)
                    return false;
            return true;
        }
        public override void Load(XElement e) { }
        public override XElement Unload(string n) { return new XElement(n, new XAttribute("Kind", "AllMonsterIsKilled")); }
    }
    public class And :EventCondition {
        public static new And E2D(XElement e) { return new And(e); }

        public EventCondition A, B;

        public And() { }
        public And(XElement e) { Load(e); }

        public override bool IsOK(Stage s, Event e) { return A.IsOK(s, e) && B.IsOK(s, e); }
        public override void Load(XElement e) {
            A = EventCondition.E2D(e.Element("A"));
            B = EventCondition.E2D(e.Element("B"));
        }
        public override XElement Unload(string n) {
            XElement e = new XElement(n, new XAttribute("Kind", "And"));
            e.Add(EventCondition.D2E("A", A));
            e.Add(EventCondition.D2E("B", B));
            return e;
        }
    }
    public class Or : EventCondition {
        public static new Or E2D(XElement e) { return new Or(e); }

        public EventCondition A, B;

        public Or() { }
        public Or(XElement e) { Load(e); }

        public override bool IsOK(Stage s, Event e) { return A.IsOK(s, e) || B.IsOK(s, e); }
        public override void Load(XElement e) {
            A = EventCondition.E2D(e.Element("A"));
            B = EventCondition.E2D(e.Element("B"));
        }
        public override XElement Unload(string n) {
            XElement e = new XElement(n, new XAttribute("Kind", "Or"));
            e.Add(EventCondition.D2E("A", A));
            e.Add(EventCondition.D2E("B", B));
            return e;
        }
    }
    public class OnlyJudgeOnce : EventCondition {
        public static new OnlyJudgeOnce E2D(XElement e) { return new OnlyJudgeOnce(e); }

        public EventCondition Condition;

        public OnlyJudgeOnce() { }
        public OnlyJudgeOnce(XElement e) { Load(e); }

        public override bool IsOK(Stage s, Event e) {
            e.State = Event.FINISHED;
            return Condition.IsOK(s, e);
        }
        public override void Load(XElement e) {
            Condition = EventCondition.E2D(e.Element("Condition"));
        }
        public override XElement Unload(string n) {
            XElement e = new XElement(n, new XAttribute("Kind", "OnlyJudgeOnce"));
            e.Add(EventCondition.D2E("Condition", Condition));
            return e;
        }
    }
}