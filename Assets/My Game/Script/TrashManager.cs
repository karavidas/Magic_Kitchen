using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashManager : MonoBehaviour {

    public void DeletePotion(GameObject potion) {
        Destroy (potion);
    }
}
