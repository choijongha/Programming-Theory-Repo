using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class HorseMovement : ObjectMovement
{
    // Start is called before the first frame update
    void Start()
    {
        //POLYMORPHISM
        IsStart(20);
        InvokeInstantiate(0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        AnimalMovement();
    }
}
