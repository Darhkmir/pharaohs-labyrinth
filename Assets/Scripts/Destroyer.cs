using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

    [SerializeField] private LayerMask ignoreDestruction;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer != ignoreDestruction) {
            Destroy(other.gameObject);
        }
    }
    
}
