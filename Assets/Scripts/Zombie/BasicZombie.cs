using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Zombie
{
    public class Basic_Zombie : Zombie
    {
        protected Basic_Zombie() : base(100, 0)
        {
            Speed = 5;
            Atk = 20;
        }
    }
}