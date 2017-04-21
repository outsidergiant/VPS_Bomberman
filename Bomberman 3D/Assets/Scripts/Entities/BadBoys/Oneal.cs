using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Entities.Characters.BadBoys
{
    public class Oneal : Enemy
    {
        public static int numberOfPoints = 200;

        public Oneal(GameObject gameObject) : base()
        {
            this.name = "Oneal";
            
            this.speed = SpeedTypes.Slowest;
            this.intelligence = Smart.High;
            //this.abilities.Add(PowerUp.Wallpass);
            this.gameObject = gameObject;
        }
    }
}
