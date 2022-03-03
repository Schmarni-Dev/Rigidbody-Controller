using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Schmarni.input;
using Schmarni;

namespace Schmarni.player
{
    public class movementWrapper : movement
    {
        protected override void Awake()
        {
            base.Awake();
        }

        protected override void setGrounded()
        {
            // print("grounding");
            base.setGrounded();
        }

        private void FixedUpdate()
        {
            move(rb,inputManager.Singleton.getData(),state,this.transform);
        }
    }
}
