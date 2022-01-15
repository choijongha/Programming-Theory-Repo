using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class CowMovement : ObjectMovement
{
    // Start is called before the first frame update
    void Start()
    {
        //POLYMORPHISM
        IsStart(13);
        InvokeInstantiate(1f);
    }

    // Update is called once per frame
    void Update()
    {
        AnimalMovement();

    }
}
