using UnityEngine;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        /*FileStream fileStream = new FileStream("t.jpg", FileMode.Open, FileAccess.Read);
        fileStream.Seek(0, SeekOrigin.Begin);
        byte[] bytes = new byte[fileStream.Length];
        fileStream.Read(bytes, 0, (int)fileStream.Length);
        fileStream.Close();

        int width = 672;
        int height = 974;
        Texture2D texture = new Texture2D(width, height);
        texture.LoadImage(bytes);
        GameObject obj = new GameObject("aaa");
        SpriteRenderer rend = obj.AddComponent<SpriteRenderer>();
        rend.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));*/
    }
	// Update is called once per frame
	void Update () {
	}
    void OnCollisionEnter2D(Collision2D c) {
        Debug.Log(c.gameObject);
    }
}
