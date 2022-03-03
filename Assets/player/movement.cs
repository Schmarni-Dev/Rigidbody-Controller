using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Schmarni.input;


namespace Schmarni.player
{
    [RequireComponent(typeof(Rigidbody))]
    public class movement : MonoBehaviour
    {
        [SerializeField]
        private movementSettings config;
        public static movementSettings _config;
        protected Rigidbody rb;
        [SerializeField]
        protected playerState state = new playerState();

        protected static void AirMovement(Vector3 _inputVec, Rigidbody _rb, movementSettings _settings)
        {
            // project the velocity onto the movevector
            Vector3 projVel = Vector3.Project(_rb.velocity, _inputVec);

            // check if the movevector is moving towards or away from the projected velocity
            bool isAway = Vector3.Dot(_inputVec, projVel) <= 0f;

            // only apply force if moving away from velocity or velocity is below MaxAirSpeed
            if (projVel.magnitude < _settings.MaxAirstarfeSpeed || isAway)
            {
                // calculate the ideal movement force
                Vector3 vc = _inputVec.normalized * _settings.AirstarfeForce;

                // cap it if it would accelerate beyond MaxAirSpeed directly.
                if (!isAway)
                {
                    vc = Vector3.ClampMagnitude(vc, _settings.MaxAirstarfeSpeed - projVel.magnitude);
                }
                else
                {
                    vc = Vector3.ClampMagnitude(vc, _settings.MaxAirstarfeSpeed + projVel.magnitude);
                }

                // Apply the force
                _rb.AddForce(vc, ForceMode.VelocityChange);
            }
        }

        protected virtual void Awake()
        {
            _config = config;
            rb = GetComponent<Rigidbody>();
        }

        protected virtual void setGrounded()
        {
            state.OnGround = true;
        }

        protected static void move(Rigidbody _rb, inputData _inputData, playerState _state,Transform direction)
        {
            Vector3 fVel = _rb.velocity.getFlat();
            Vector3 input = helper.getFlatVector(_inputData.move);
            Vector3 MI = input;
            if (_config.Airstrafe && _config.AirstrafeOnGround ? true : !_state.OnGround)
            {
                AirMovement(input,_rb,_config);
            }

            if (fVel.magnitude > _config.MaxSpeed )
            {
                MI = helper.getInputVactorNegative(Quaternion.LookRotation(fVel, _rb.transform.up).eulerAngles.normalized,input);
            }

            // print((Vector3.Scale(direction.forward, MI) * _config.moveSpeed) * Time.deltaTime);
            if (_state.OnGround)
            {
                _rb.AddForce((direction.forward * MI.x * (_config.moveSpeed * 100)) * Time.deltaTime);
                _rb.AddForce((direction.right * MI.z * (_config.moveSpeed * 100)) * Time.deltaTime);
            }

            fVel = _rb.velocity.getFlat();
            if (fVel.magnitude.abs() > _config.counterMovementThreshold)
            {
                Quaternion q = Quaternion.LookRotation(fVel * -1, _rb.transform.up);
                _rb.AddForce(helper.eulerToForward(q.eulerAngles).getFlat() * _config.CounterMovement);
            }
            

        }

        private void OnCollisionStay(Collision other)
        {
            state.OnGround = false;
            foreach (var contact in other.contacts)
            {
                
                if (Vector3.Angle(contact.normal,rb.transform.up) < _config.maxSlopeAngle)
                {
                    setGrounded();
                }
            }
        }
        
    }

    [Serializable]
    public class playerState
    {
        public bool OnGround = false;
    }
}
