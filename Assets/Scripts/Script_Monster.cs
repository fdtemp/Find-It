using UnityEngine;
using Game;

public class Script_Monster :MonoBehaviour {
    private abstract class State {
        public const int NONE = -1;
        public const int MOVING = 0;
        public const int ATTACKING = 1;

        public Script_Monster Script;

        abstract public int TrySwitch();
        virtual public void Regist() { }
        virtual public void Update() { }
        virtual public void Unregist() { }

        public class Moving : State {
            public float StartTime;
            public float LastUpdateTime = 0;
            public float LastJumpTime;
            public Vector3 LastPosition;
            public bool LastTimeMoved;
            private float LastImageChangeTime;
            private int MovingImageId;
            private int WaitingImageId;

            public override int TrySwitch() {
                if (Time.time - StartTime > Script.Monster.MoveStayTime) {
                    Vector3
                        monsterPos = Script.Monster.Entity.transform.localPosition,
                        playerPos = GameManager.Player.Entity.transform.localPosition;
                    if (Mathf.Abs(monsterPos.y - playerPos.y) < 0.25f
                        && (Mathf.Abs(playerPos.x - monsterPos.x) < Script.Monster.AttackBulletRange)
                        && ((monsterPos.x < playerPos.x && Script.Monster.TurnRight)
                            || (playerPos.x < monsterPos.x && !Script.Monster.TurnRight)))
                        return ATTACKING;
                }
                return NONE;
            }
            public override void Regist() {
                //Debug.Log("Monster is Moving.");
                StartTime = Time.time;
                LastPosition = new Vector3(0, 0, 0);
                LastJumpTime = Time.time - 0.5f;
                LastTimeMoved = false;
                WaitingImageId = 0;
                MovingImageId = 0;
                LastImageChangeTime = 0;
            }
            public override void Update() {
                Living monster = Script.Monster;
                float
                    Horizontal = 0,
                    Vertical = 0;
                if (Time.time - LastUpdateTime > monster.MoveInterval) {
                    LastUpdateTime = Time.time;

                    ScenePosition
                        monsterPos = monster.GetScenePosition(),
                        playerPos = GameManager.Player.GetScenePosition();
                    if (LastTimeMoved
                        && (LastPosition - Script.transform.localPosition).sqrMagnitude < 0.01f
                        && Script.IsGrounded()
                        && Time.time - LastJumpTime > 0.5f) {
                        Vertical++;
                        LastJumpTime = Time.time;
                    }
                    float
                        xd = monsterPos.x - playerPos.x,
                        yd = monsterPos.y - playerPos.y,
                        Dis = Mathf.Sqrt(xd * xd + yd * yd);
                    if (Dis < monster.SearchRange) {
                        if (playerPos.x - monsterPos.x > 0)
                            Horizontal++;
                        else
                            Horizontal--;
                        if (monster.IgnoreBricks) {
                            if (playerPos.y - monsterPos.y > 0)
                                Vertical++;
                            else
                                Vertical--;
                        }
                    } else {
                        switch (GameManager.Random.Next(3)) {
                        case 0: Horizontal++; break;
                        case 1: break;
                        case 2: Horizontal--; break;
                        }
                        if (monster.IgnoreBricks) {
                            switch (GameManager.Random.Next(3)) {
                            case 0: Vertical++; break;
                            case 1: break;
                            case 2: Vertical--; break;
                            }
                        }
                    }
                    if (Horizontal == 0 && Vertical == 0) {
                        LastTimeMoved = false;
                    } else {
                        LastTimeMoved = true;
                        LastPosition = Script.transform.localPosition;
                        Vector2 v = monster.rb2D.velocity;
                        if ((Horizontal == -1 && monsterPos.x > 0)
                            || (Horizontal == 1 && monsterPos.x < GameManager.CurrentScene.BrickXAmount - 1)) {
                            v.x = monster.MoveSpeed * Horizontal;
                        } else {
                            v.x = 0;
                        }
                        if (monster.IgnoreBricks) {
                            if ((Vertical == -1 && monsterPos.y > 0)
                            || (Vertical == 1 && monsterPos.y < GameManager.CurrentScene.BrickYAmount - 1)) {
                                v.y = monster.MoveSpeed * Vertical;
                            } else {
                                v.y = 0;
                            }
                        } else if (Script.IsGrounded()) {
                            v.y += monster.JumpSpeed * Vertical;
                        }
                        monster.rb2D.velocity = v;
                    }
                    if (Horizontal != 0) {
                        int _ImageCount = monster.MovingImages.Count;
                        if (Time.time - LastImageChangeTime > monster.MovingImageInterval) {
                            LastImageChangeTime = Time.time;
                            if (++MovingImageId == _ImageCount) MovingImageId = 0;
                            monster.SpriteRend.sprite = monster.MovingImages[MovingImageId];
                        }
                    }
                }
                int ImageCount = monster.WaitingImages.Count;
                if (Time.time - LastImageChangeTime > monster.WaitingImageInterval) {
                    LastImageChangeTime = Time.time;
                    if (++WaitingImageId == ImageCount) WaitingImageId = 0;
                    monster.SpriteRend.sprite = monster.WaitingImages[WaitingImageId];
                }
                if ((monster.TurnRight && monster.rb2D.velocity.x < 0)
                    || (!monster.TurnRight && monster.rb2D.velocity.x > 0))
                    monster.FlipImage();
            }
        }
        public class Attacking : State {
            public float StartTime;
            public float LastAttackTime;
            private float LastImageChangeTime;
            private int ImageId;
            public override int TrySwitch() {
                Vector3
                    monsterPos = Script.Monster.Entity.transform.localPosition,
                    playerPos = GameManager.Player.Entity.transform.localPosition;
                if (Mathf.Abs(monsterPos.y - playerPos.y) < 0.25f
                    && (Mathf.Abs(monsterPos.x - playerPos.x) < Script.Monster.AttackBulletRange)
                    && ((monsterPos.x < playerPos.x && Script.Monster.TurnRight)
                        || (playerPos.x < monsterPos.x && !Script.Monster.TurnRight)))
                    StartTime = Time.time;
                if (Time.time - StartTime > Script.Monster.AttackStayTime)
                    return MOVING;
                return NONE;
            }
            public override void Regist() {
                if (Script.Monster.IgnoreBricks)
                    Script.Monster.rb2D.velocity = Vector2.zero;
                //Debug.Log("Monster is attacking.");
                StartTime = Time.time;
                LastAttackTime = Time.time;
                ImageId = 0;
                LastImageChangeTime = 0;
            }
            public override void Update() {
                Living monster = Script.Monster;
                if (Time.time - LastAttackTime > monster.AttackInterval) {
                    LastAttackTime = Time.time;
                    Vector3 mp = Script.gameObject.transform.localPosition;
                    Point sp = new Point(
                        mp.x + (monster.TurnRight ? monster.AttackBulletOffset.x : -monster.AttackBulletOffset.x),
                        mp.y + monster.AttackBulletOffset.y);
                    if (monster.AttackBulletIsImage) {
                        GameManager.CurrentScene.CreateBullet(
                            Scene.MonsterBullet,
                            monster.AttackBulletDamage,
                            sp,
                            new Vector2(monster.TurnRight ? 1 : -1, 0),
                            monster.AttackBulletSpeed,
                            monster.AttackBulletRange,
                            monster.AttackBulletImagePath,
                            monster.AttackBulletImageWidth,
                            monster.AttackBulletImageHeight,
                            monster.AttackBulletImagePixelPerUnit);
                    } else {
                        GameManager.CurrentScene.CreateBullet(
                            Scene.MonsterBullet,
                            monster.AttackBulletDamage,
                            sp,
                            new Vector2(monster.TurnRight ? 1 : -1, 0),
                            monster.AttackBulletSpeed,
                            monster.AttackBulletRange,
                            monster.AttackBulletTexts[GameManager.Random.Next(monster.AttackBulletTexts.Count)],
                            monster.AttackBulletTextSize);
                    }
                }
                int ImageCount = monster.AttackingImages.Count;
                if (Time.time - LastImageChangeTime > monster.AttackingImageInterval) {
                    if (++ImageId == ImageCount) ImageId = 0;
                    monster.SpriteRend.sprite = monster.AttackingImages[ImageId];
                }
            }
        }
    }

    private State[] States = new State[2] {
        new State.Moving(),
        new State.Attacking(),
    };
    private State CurrentState;
    public Living Monster;
    public void Start() {
        if (Monster.IgnoreBricks) {
            gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
            Monster.rb2D.gravityScale = 0;
        }
        foreach (var state in States)
            state.Script = this;
        CurrentState = States[State.MOVING];
        CurrentState.Regist();
    }
    public void Update() {
        int next = CurrentState.TrySwitch();
        if (next != State.NONE) {
            CurrentState.Unregist();
            CurrentState = States[next];
            CurrentState.Regist();
        }
        CurrentState.Update();
    }
    bool IsGrounded() {
        Vector3 center = transform.localPosition;
        Vector2 pos = new Vector2(
            center.x,
            center.y - ((float)Monster.ImageHeight / Monster.PixelPerUnit) / 2 - 0.1f);
        return Physics2D.Raycast(pos, -Vector2.up, 0.1f);
    }
}