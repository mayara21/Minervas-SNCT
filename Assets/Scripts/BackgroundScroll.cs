using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour {

    [SerializeField] private Transform cam;
    [SerializeField] private float xAmount = 1f;
    [SerializeField] private float yAmount = .2f;

    private Vector2 offset;
    private Vector2 newPos;
    private float x0, y0, xDiff, yDiff;

	// Use this for initialization
	void Start () {
        x0 = cam.position.x;
        y0 = cam.position.y;
        offset = new Vector2(x0 - transform.position.x, y0 - transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {
        xDiff = x0 + (cam.position.x * xAmount);
        yDiff = y0 + (cam.position.y * yAmount);

        newPos = new Vector2(cam.position.x - xDiff, cam.position.y - yDiff);
        transform.position = newPos - offset;
	}
}
