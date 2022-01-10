using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseMovement : ObjectMovement
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeInstantiate();
        speed = 20;
    }

    // Update is called once per frame
    void Update()
    {
        AnimalMovement();
    }
    protected override void AnimalMovement()
    {
        base.AnimalMovement();
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
