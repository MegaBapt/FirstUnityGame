using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "inventaire/Item")]
public class Item : ScriptableObject
{
    public int id;
    public string name;
    public string description;
    public Sprite image;
    public int hpGiven;
    public int speedGiven;
    public float speedDuration;
    public float TimeSlow = 1;
    public float TimeSlowDuration;
    public int JumpBoostGiven;
    public float JumpBoostDuration;
}
