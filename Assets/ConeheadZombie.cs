using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Zombie
{
    public class Conehead_Zombie : Zombie
    {
        protected Conehead_Zombie() : base(100, 20)
        {
            Speed = 5;
            Atk = 20;
        }
    }
}