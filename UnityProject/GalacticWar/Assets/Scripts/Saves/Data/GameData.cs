using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameData
{
    public int deaths;
    public int missionsCompleted;
    //player's current health
    public int health;
    public int money;
    public int crystals;
    public int exoticmatter;
    public string shipframe;
    //references the index of the weapons list
    public List<int> equippedWeapons;
    //hull patches and shields also go here.
    public List<string> weapons;

    //numbers set here are starting values
    public GameData()
    {
        this.deaths = 0;
        this.health = 50;
        this.money = 0;
        this.crystals = 0;
        this.exoticmatter = 0;
        this.shipframe = "Basic";
        this.equippedWeapons = new List<int> { 0,-1,-1,-1, -1, -1, -1, -1, -1, -1, -1, -1 };
        this.weapons = new List<string> {"Basic"};
    }
}
