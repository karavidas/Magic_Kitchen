using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class PotionManager : MonoBehaviour {

    private ClientManager[] clients;
    private TrashManager[] trashes;
    private Vector3 startPosition;
    private bool dragging = false;
    private float distance;

    void Start()
    {
        startPosition = transform.position;

        trashes = FindObjectsOfType<TrashManager>();
    }

    void Update()
    {

        clients = FindObjectsOfType<ClientManager>();

        if (dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = rayPoint;
        }
    }

    void OnMouseDown()
    {
        if (GameManager.instance.gameState == GameManager.gameStates.Playing)
        {
            dragging = true;
            if (this.gameObject.GetComponent<SkeletonAnimation>())
            {
                this.gameObject.GetComponent<SkeletonAnimation>().GetComponent<MeshRenderer>().sortingLayerName = "Draged";
            }
            else
            {
                this.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Draged";
            }
            distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        }
    }

    void OnMouseUp()
    {
        dragging = false;
        if (this.gameObject.GetComponent<SkeletonAnimation>())
        {
            this.gameObject.GetComponent<SkeletonAnimation>().GetComponent<MeshRenderer>().sortingLayerName = "Ingredients";
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Ingredients";
        }

        for (int i = 0; i < clients.Length; i++)
        {
            if (transform.position.x < clients[i].transform.position.x + 1.5 && transform.position.x > clients[i].transform.position.x - 1.5 && transform.position.y > clients[i].transform.position.y - 2 && transform.position.y < clients[i].transform.position.y + 2)
            {
                clients[i].AddingPotion(this.gameObject);
                break;
            }
        }

        for (int i = 0; i < trashes.Length; i++)
        {
            if (transform.position.x < trashes[i].transform.position.x + 1.5 && transform.position.x > trashes[i].transform.position.x - 1.5 && transform.position.y > trashes[i].transform.position.y - 2 && transform.position.y < trashes[i].transform.position.y + 2)
            {
                trashes[i].DeletePotion(this.gameObject);
                break;
            }
        }

        transform.position = startPosition;
    }

}
