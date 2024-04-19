using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public static Combat instance;
    public bool inCombat;

    public delegate void PublishEvent();
    public static event PublishEvent StartCombat;    
    public static event PublishEvent EndCombat;

    public void PublishStartCombat()
    {
        if (StartCombat != null)
            StartCombat();
    }

    public void PublishEndCombat()
    {
        if (EndCombat != null)
            EndCombat();
    }
    void Start()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
}
