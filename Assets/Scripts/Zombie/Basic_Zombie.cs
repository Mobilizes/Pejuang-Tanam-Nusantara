using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Zombie
{
    public class Basic_Zombie : Zombie
    {
        protected Basic_Zombie()
        {
            MaxHp = 100;

            Speed = 1;
            Atk = 20;
        }
    }
}