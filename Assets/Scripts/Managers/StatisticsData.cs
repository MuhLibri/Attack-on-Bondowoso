using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsData
{
    public int shotsFired;
    public int shotsHit;
    public float distance;
    public float playtime;

    public StatisticsData(int shotsFired, int shotsHit, float distance, float playtime)
    {
        this.shotsFired = shotsFired;
        this.shotsHit = shotsHit;
        this.distance = distance;
        this.playtime = playtime;
    }
}
