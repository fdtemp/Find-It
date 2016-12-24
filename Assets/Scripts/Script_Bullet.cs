using UnityEngine;
using Game;

public class Script_Bullet : MonoBehaviour {
    public const int FLYING = 0;
    public const int EXPLODING = 1;
    public const int FINISHED = 2;

    private float FlyingScale = 2;
    private float ExplodingScale = 6;
    private float ExplodingTime = 0.5f;

    public bool BulletIsImage;
    public int Kind;
    public float Damage;
    public float Speed;
    public float Range;
    public Vector2 Forward;
    public int State;

    public SpriteRenderer srend;
    public TextMesh tmesh;

    public float StartTime;
    public float CurrentTime;
    public float ScaleRate;
    public Rigidbody2D rb2D;

    void Start() {
        CurrentTime = StartTime = Time.time;
        ScaleRate = FlyingScale / (Range / Speed);
        State = FLYING;
        rb2D = GetComponent<Rigidbody2D>();
        
        if (BulletIsImage)
            srend = GetComponent<SpriteRenderer>();
        else
            tmesh = GetComponent<TextMesh>();
    }
    void Update() {
        CurrentTime += Time.deltaTime;
        float scale = transform.localScale.x + ScaleRate * Time.deltaTime;
        gameObject.transform.localScale = new Vector3(scale, scale, 1);
        if (State == FLYING) {
            GetComponent<BoxCollider2D>().size = new Vector2(0.3f / scale, 0.3f / scale);
            if (CurrentTime - StartTime > Range / Speed)
                Explode(null);
        } else if (State == EXPLODING) {
            if (CurrentTime - StartTime > ExplodingTime) {
                State = FINISHED;
                gameObject.SetActive(false);
            } else {
                float rate = (CurrentTime - StartTime) / ExplodingTime;
                
                if (BulletIsImage) {
                    Color col = srend.color;
                    col.a = 1 - 0.5f * rate;
                    srend.color = col;
                } else {
                    Color col = tmesh.color;
                    col.a = 1 - 0.5f * rate;
                    tmesh.color = col;
                }
            }
        };
    }
    void OnTriggerEnter2D(Collider2D coll) {
        if (State != FLYING) return;
        switch (coll.gameObject.tag) {
        case "Brick":
            Explode(null);
            break;
        case "Monster":
            if (Kind == Scene.PlayerBullet) {
                Explode(coll.gameObject);
                GameManager.ShowMonsterInfo(coll.gameObject.GetComponent<Script_Monster>().Monster);
            }
            break;
        case "Bullet":
            if (Kind != coll.gameObject.GetComponent<Script_Bullet>().Kind)
                Explode(null);
            break;
        case "Player":
            if (Kind == Scene.MonsterBullet)
                Explode(coll.gameObject);
            break;
        }
    }
    private void Explode(GameObject Target) {
        State = EXPLODING;
        rb2D.velocity = Vector2.zero;
        GameObject.Destroy(gameObject.GetComponent<BoxCollider2D>());
        ScaleRate = (ExplodingScale - transform.localScale.x) / ExplodingTime;
        StartTime = CurrentTime;
        if (Target != null) {
            if (Target.tag == "Player") {
                Target.GetComponent<Script_Player>().Player.ChangeHP(-Damage);
            } else if (Target.tag == "Monster") {
                Target.GetComponent<Script_Monster>().Monster.ChangeHP(-Damage);
            }
        }
    }
}