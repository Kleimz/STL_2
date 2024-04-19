using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum characterTags
{
    Elf,
    Dwarf,
    NPC
}
public class Line : MonoBehaviour
{
     public characterTags dialogueSpeaker;
     public string chosenLine;
}
