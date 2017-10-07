using UnityEngine;
using System.Collections;

public class Scroller : MonoBehaviour {
	public Vector2 speed = new Vector2(0f, 0.4f);
	void Update () {
		GetComponent<Renderer>().material.mainTextureOffset += speed * Time.deltaTime;
	}
}
