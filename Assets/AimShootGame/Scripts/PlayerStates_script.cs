using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates_script : MonoBehaviour
{
    public int Cash;
    public float AimTime;
    public float MinAimTime;
    public int Bullets;
    public int MaxBullets;
    public float Luck;
    public float MaxLuck;
    public void ModifyAimSpeed(float a)
    {
        AimTime += a;
        if (AimTime < MinAimTime)
        {
            AimTime = MinAimTime;
        }
    }

    public void ModifyBullets(int b)
    {
        Bullets += b;
        if (Bullets > MaxBullets)
        {
            Bullets = MaxBullets;
        }
        else if(Bullets < 0)
        {
            Bullets = 0;
        }
    }

    public void ModifyLuck(float l)
    {
        Luck += l;
        if (Luck > MaxLuck)
        {
            Luck = MaxLuck;
        }
    }

    public void ModifyCash(int c)
    {
        Cash += c;
    }

}
