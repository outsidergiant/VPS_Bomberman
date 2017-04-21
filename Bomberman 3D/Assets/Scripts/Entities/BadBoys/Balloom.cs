using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Entities.Characters.BadBoys
{
    public class Balloom : Enemy
    {
        public Balloom(GameObject gameObject) : base()
        {
            this.name = "Balloom";
            this.numberOfPoints = 100;
            this.speed = SpeedTypes.Slow;
            this.intelligence = Smart.Low;
            //this.abilities.Add(PowerUp.Wallpass);
            this.gameObject = gameObject;
        }
    }
}
