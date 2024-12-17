using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Zombie
{
    public class Conehead_Zombie : Zombie
    {
        protected Conehead_Zombie() : base(100)
        {
            Speed = 5;
            Atk = 20;
            Armor = 20;
        }
    }
}