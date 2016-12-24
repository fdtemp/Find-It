using System;
using UnityEngine;
using Game;

public class Script_Player : MonoBehaviour {
    private abstract class State {
        public const int NONE = -1;
        public const int WAITING = 0;
        public const int MOVING = 1;
        public const int ATTACKING = 2;

        public Script_Player Script;

        abstract public int TrySwitch();
        virtual public void Regist() { }
        virtual public void Update() { }
        virtual public void Unregist() { }

        public class Waiting : State {
            private float LastImageChangeTime = 0;
            private int ImageId = 0;
            public override int TrySwitch() {
                if (Input.GetMouseButton(0))
                    return ATTACKING;
                if (Input.GetKey(KeyCode.A)
                    || Input.GetKey(KeyCode.D)
                    || Input.GetKey(KeyCode.Space))
                    return MOVING;
                return NONE;
            }
            public override void Regist() {
                //Debug.Log("Player is waiting.");
                ImageId = 0;
                Script.Player.SpriteRend.sprite = Script.Player.WaitingImages[0];
            }
            public override void Update() {
                int ImageCount = Script.Player.WaitingImages.Count;
                if (ImageCount > 1
                    && Time.time - LastImageChangeTime > Script.Player.WaitingImageInterval) {
                    if (++ImageId == ImageCount) ImageId = 0;
                    Script.Player.SpriteRend.sprite = Script.Player.WaitingImages[ImageId];
                }
            }
        }
        public class Moving : State {
            private float StartTime;
            private float LastImageChangeTime;
            private float LastJumpTime;
            private int ImageId;
            public override int TrySwitch() {
                if (Time.time - StartTime > 1 / Script.Player.JumpSpeed) {
                    if (Input.GetMouseButton(0))
                        return ATTACKING;
                    return WAITING;
                }
                if (Input.GetKey(KeyCode.A)
                    || Input.GetKey(KeyCode.D)
                    || Input.GetKey(KeyCode.Space))
                    StartTime = Time.time;
                return NONE;
            }
            public override void Regist() {
                //Debug.Log("Player is moving.");
                StartTime = Time.time;
                LastJumpTime = Time.time - 0.5f;
                ImageId = 0;
                LastImageChangeTime = 0;
                Script.Player.SpriteRend.sprite = Script.Player.MovingImages[0];
            }
            public override void Update() {
                Living p = Script.Player;
                float
                    Horizontal = 0,
                    Vertical = 0;
                if (Input.GetKey(KeyCode.A)) Horizontal--;
                if (Input.GetKey(KeyCode.D)) Horizontal++;
                if (Input.GetKey(KeyCode.Space)
                    && Script.IsGrounded()
                    && Time.time - LastJumpTime > 0.5f) {
                    Vertical++;
                    LastJumpTime = Time.time;
                }
                Vector2 v = p.rb2D.velocity;
                if (Horizontal != 0)
                    v.x = p.MoveSpeed * Horizontal;
                v.y += p.JumpSpeed * Vertical;
                p.rb2D.velocity = v;
                if ((p.TurnRight && p.rb2D.velocity.x < 0)
                    || (!p.TurnRight && p.rb2D.velocity.x > 0))
                    p.FlipImage();
                int ImageCount = p.MovingImages.Count;
                if (ImageCount > 1
                    && Time.time - LastImageChangeTime > p.MovingImageInterval) {
                    LastImageChangeTime = Time.time;
                    if (++ImageId == ImageCount) ImageId = 0;
                    p.SpriteRend.sprite = p.MovingImages[ImageId];
                }
            }
        }
        public class Attacking : State {
            private float StartTime;
            private float LastAttackTime;
            private float LastImageChangeTime;
            private int ImageId;
            public override int TrySwitch() {
                if (Time.time - StartTime > Script.Player.AttackInterval + 0.1f) {
                    if (Input.GetKey(KeyCode.A)
                        || Input.GetKey(KeyCode.D)
                        || Input.GetKey(KeyCode.Space))
                        return MOVING;
                    return WAITING;
                }
                if (Input.GetMouseButton(0))
                    StartTime = Time.time;
                return NONE;
            }
            public override void Regist() {
                //Debug.Log("Player is attacking.");
                StartTime = Time.time;
                LastAttackTime = StartTime;
                ImageId = 0;
                LastImageChangeTime = 0;
                Script.Player.SpriteRend.sprite = Script.Player.AttackingImages[0];
            }
            public override void Update() {
                Living p = Script.Player;
                if (Time.time - LastAttackTime > p.AttackInterval) {
                    Vector3 pp = Script.gameObject.transform.localPosition;
                    Point sp = new Point(
                        pp.x + (p.TurnRight ? p.AttackBulletOffset.x : -p.AttackBulletOffset.x),
                        pp.y + p.AttackBulletOffset.y);
                    if (p.AttackBulletIsImage) {
                        GameManager.CurrentScene.CreateBullet(
                            Scene.PlayerBullet,
                            p.AttackBulletDamage,
                            sp,
                            new Vector2(p.TurnRight ? 1 : -1, 0),
                            p.AttackBulletSpeed,
                            p.AttackBulletRange,
                            p.AttackBulletImagePath,
                            p.AttackBulletImageWidth,
                            p.AttackBulletImageHeight,
                            p.AttackBulletImagePixelPerUnit);
                    } else {
                        GameManager.CurrentScene.CreateBullet(
                            Scene.PlayerBullet,
                            p.AttackBulletDamage,
                            sp,
                            new Vector2(p.TurnRight ? 1 : -1, 0),
                            p.AttackBulletSpeed,
                            p.AttackBulletRange,
                            p.AttackBulletTexts[GameManager.Random.Next(p.AttackBulletTexts.Count)],
                            p.AttackBulletTextSize);
                    }
                    LastAttackTime = Time.time;
                }
                int ImageCount = Script.Player.AttackingImages.Count;
                if (ImageCount > 1
                    && Time.time - LastImageChangeTime > Script.Player.AttackingImageInterval) {
                    if (++ImageId == ImageCount) ImageId = 0;
                    Script.Player.SpriteRend.sprite = Script.Player.AttackingImages[ImageId];
                }
            }
        }
    }
    private State[] States = new State[3] {
        new State.Waiting(),
        new State.Moving(),
        new State.Attacking(),
    };
    private State CurrentState;
    public Living Player;
    public void Start() {
        foreach (var state in States)
            state.Script = this;
        CurrentState = States[State.WAITING];
        CurrentState.Regist();
    }
    public void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) GameManager.ShowMenu("Paused");
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
            center.y - ((float)Player.ImageHeight / Player.PixelPerUnit) / 2 - 0.1f);
        return Physics2D.Raycast(pos, -Vector2.up, 0.1f);
    }
    void OnMouseDown() {
        Debug.Log("click");
    }
}