using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBehaviour : MonoBehaviour
{
    public Sprite chestOpenSprite;
    public bool isChestOpen;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isChestOpen)
            GetComponent<SpriteRenderer>().sprite = chestOpenSprite;
    }
}
