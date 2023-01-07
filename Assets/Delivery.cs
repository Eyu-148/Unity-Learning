using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    [SerializeField] Color32 hasPackageColor = new Color32(1, 1, 1, 1);
    [SerializeField] Color32 noPackageColor = new Color32(1, 1, 1, 1);
    [SerializeField] float destroyDelay;
    bool hasPackage; // initially false

    SpriteRenderer spriteRenderer;
    void Start() {
        // get the reference of component stuff
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        // make sure we pick up only one package at one time
        if (other.tag == "Package" && !hasPackage) {
            Debug.Log("Package picked up!");
            hasPackage = true;
            spriteRenderer.color = hasPackageColor;
            Destroy(other.gameObject, destroyDelay);
        }
        // delivered
        if (other.tag == "Customer" && hasPackage) {
            Debug.Log("Package delivered!");
            hasPackage = false;
            spriteRenderer.color = noPackageColor;
        }
    }
}
