using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDirection : MonoBehaviour
{
    private void Update()
    {
        transform.rotation = Quaternion.Euler(0, GetComponentInParent<Transform>().transform.rotation.y, 0);
    }
}
