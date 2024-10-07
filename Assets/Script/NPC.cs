using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    Animator anim;
    Collider coll;
    public GameObject Shop;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider>();
    }
    private void Update()
    {
        
    }
    public void OnClick()
    {
        Shop.SetActive(true);
        Debug.Log("NPC¸¦ Å¬¸¯");
    }
}
