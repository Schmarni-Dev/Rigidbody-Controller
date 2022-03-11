using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Schmarni.input;
using Schmarni.config;


namespace Schmarni.player
{
    [RequireComponent(typeof(Rigidbody))]
    public class movement : MonoBehaviour
    {
        
        [SerializeField] protected movementSettings _config;
        protected Rigidbody rb;
        [SerializeField] protected playerState state = new playerState();

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
            state = new playerState();
            rb = GetComponent<Rigidbody>();
        }

        protected virtual void setGrounded()
        {
            state.OnGround = true;
            state.jumps = 1;
        }

        public static void Jump(Rigidbody _rb,playerState _state,movementSettings _config)
        {
            // print("cum");
            if ((_state.OnGround || (_config.allowDoubleJump && _state.jumps > 0)) && !_state.jumping)
            {
                // print("CUM");
                if(_config.allowDoubleJump && _state.jumps > 0 && !_state.OnGround) _state.jumps--;
                _rb.velocity = _rb.velocity.getFlat();
                _rb.AddForce(_rb.transform.up * (_config.jumpForce*100f),ForceMode.Force);
                _state.OnGround = false;
                _state._oldOnGround = false;
                _state.jumping = true;
            }
        }

        public static void move(Rigidbody _rb, inputData _inputData, playerState _state, Transform direction,movementSettings _config)
        {
            Vector3 fVel = _rb.velocity.getFlat();
            Vector3 input = helper.getFlatVector(_inputData.move);
            Vector3 MI = input;
            if (_config.Airstrafe && _config.AirstrafeOnGround ? true : !_state.OnGround)
            {
                AirMovement((direction.forward.getFlat() * input.x) + (direction.right.getFlat() * input.z), _rb, _config);
            }

            if (fVel.magnitude > _config.MaxSpeed)
            {
                MI = helper.getInputVactorNegative(Quaternion.LookRotation(fVel, _rb.transform.up).eulerAngles.normalized, input);
            }

            // print((Vector3.Scale(direction.forward, MI) * _config.moveSpeed) * Time.deltaTime);
            if (_state.OnGround)
            {
                // print("cum");
                Vector3 dir = (direction.forward.getFlat() * MI.x) + (direction.right.getFlat() * MI.z);
                if(_state.OnGround) dir = Vector3.ProjectOnPlane(dir, _state.contactNormal);
                _rb.AddForce((dir * (_config.moveSpeed * 100)) * Time.deltaTime);
            }

            fVel = _rb.velocity.getFlat();
            if (fVel.magnitude.abs() > _config.counterMovementThreshold && _state.OnGround)
            {
                // print("CUM");
                Quaternion q = Quaternion.LookRotation(fVel * -1, _rb.transform.up);
                _rb.AddForce(helper.eulerToForward(q.eulerAngles).getFlat() * _config.CounterMovement);
            }


        }
        public static void Look(inputData _inputData,Transform lookTransform, configData _config)
        {
            Vector2 input = _inputData.look * Time.deltaTime;
            Vector3 rot = lookTransform.rotation.eulerAngles;
            Vector3 newRot = helper.lockRotaition(new Vector3(rot.x - input.y, rot.y + input.x, rot.z));
            lookTransform.rotation = Quaternion.Euler(newRot);
        }

        private void OnCollisionStay(Collision other)
        {
            state.OnGround = false;
            foreach (var contact in other.contacts)
            {

                if (Vector3.Angle(contact.normal, rb.transform.up) < _config.maxSlopeAngle)
                {
                    if (!state._oldOnGround)
                    {
                        state._oldOnGround = true;
                        break;
                    }
                    setGrounded();
                    state.contactNormal = contact.normal;
                }
            }
        }

    }

    [Serializable]
    public class playerState
    {
        public bool OnGround = false;
        public int jumps = 0;
        [HideInInspector] public Vector3 contactNormal;
        [HideInInspector] public bool jumping = false;
        [HideInInspector] public bool _oldOnGround = false;
    }
}
