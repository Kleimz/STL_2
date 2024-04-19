using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetTypes
{
    Enemy,
    Object
}
public interface Target
{
    public void Hit(float damage);
    public TargetTypes SetEnum(TargetTypes target);
}
