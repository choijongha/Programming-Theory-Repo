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
        InitialStart();
    }
    protected override void InitialStart()
    {
        base.InitialStart();
    }
}
