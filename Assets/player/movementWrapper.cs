using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Schmarni.input;
using Schmarni;

namespace Schmarni.player
{
    public class movementWrapper : movement
    {
        public Vector3 VEc;
        public Vec3Debuger debuger;
        public Transform cam;
        protected override void Awake()
        {
            base.Awake();
        }

        protected override void setGrounded()
        {
            // print("grounding");
            base.setGrounded();
        }

        private void Update()
        {
            Look(inputManager.Singleton.getData(),cam,inputManager.Singleton.configManager.getData());
        }

        private void FixedUpdate()
        {
            move(rb,inputManager.Singleton.getData(),state,cam,_config);
            if (inputManager.Singleton.getData().jump) Jump(rb,state,_config); else state.jumping = false;
        }

        public Rigidbody Debug()
        {
            

            return(rb);
        }
    }
}
