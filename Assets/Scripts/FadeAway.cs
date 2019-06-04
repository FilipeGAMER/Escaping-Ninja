using UnityEngine;
using System.Collections;

public class FadeAway : MonoBehaviour {

    public float duration;

    private float ratio;
    private Color myColor;

    void Start() {
        myColor = GetComponent<TextMesh>().color;
    }



    // Update is called once per frame
    void Update() {
        if (Time.time > duration) {
            Destroy(gameObject);
        }

        ratio = Time.time / duration;
        myColor.a = Mathf.Lerp(1, 0, ratio);
        GetComponent<TextMesh>().color = myColor;
    }
}
