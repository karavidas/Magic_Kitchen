using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

	public GameObject ingredient;

    private KitleManager[] kitles;
    private bool dragging = false;
    private float distance;
    private Vector3 startPosition;

    void Start() {
        startPosition = transform.position;

        kitles = FindObjectsOfType<KitleManager>();
    }

    void Update() {
        if (dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = rayPoint;
        }
    }

    void OnMouseDown() {

        dragging = true;
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
	}

    void OnMouseUp() {
        dragging = false;

        for (int i = 0; i < kitles.Length; i++)
        {
            if (transform.position.x < kitles[i].transform.position.x + 1.5 && transform.position.x > kitles[i].transform.position.x - 1.5 && transform.position.y > kitles[i].transform.position.y - 2 && transform.position.y < kitles[i].transform.position.y + 2)
            {
                kitles[i].AddingElement(ingredient);
                break;
            }
        }

        transform.position = startPosition;
    }
}
