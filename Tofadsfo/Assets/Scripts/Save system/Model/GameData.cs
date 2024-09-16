using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public int level = 1;
    public int savedMoney = 0;
    public GameData(int level, int savedMoney)
    {
        this.level = level;
        this.savedMoney = savedMoney;
    }
}
