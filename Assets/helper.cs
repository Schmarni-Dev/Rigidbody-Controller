using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace Schmarni
{
    public static class helper
    {
        public static Vector3 clampV3(Vector3 _input,float _min,float _max)
        {
            return new Vector3(Mathf.Clamp(_input.x,_min,_max),Mathf.Clamp(_input.y,_min,_max),Mathf.Clamp(_input.z,_min,_max));
        }
        public static Vector3 eulerToRight(Vector3 _input)
        {
            // thanks to Pennywise881 for the function. https://forum.unity.com/threads/what-is-the-math-behind-calculating-transform-forward-of-an-object.521318/
            float xPos = Mathf.Cos(_input.x * Mathf.Deg2Rad) * Mathf.Cos(_input.y * Mathf.Deg2Rad);
            float yPos = Mathf.Sin(-_input.y * Mathf.Deg2Rad);
            float zPos =  Mathf.Sin(-_input.y * Mathf.Deg2Rad) * Mathf.Cos(_input.x * Mathf.Deg2Rad);

            return new Vector3(xPos,yPos,zPos);
        }
        public static Vector3 lockRotaition(Vector3 _input)
        {
            if (_input.x > 90f && _input.x < 180f)
            {
                _input.x = 90;
            }
            if (_input.x > 180f && _input.x < 270f)
            {
                _input.x = 270;
            }
            return _input;
        }
        public static Vector3 eulerToForward(Vector3 _input)
        {
            // thanks to Pennywise881 for the function. https://forum.unity.com/threads/what-is-the-math-behind-calculating-transform-forward-of-an-object.521318/
            float xPos = Mathf.Sin(_input.y * Mathf.Deg2Rad) * Mathf.Cos(_input.x * Mathf.Deg2Rad);
            float yPos = Mathf.Sin(-_input.x * Mathf.Deg2Rad);
            float zPos = Mathf.Cos(_input.x * Mathf.Deg2Rad) * Mathf.Cos(_input.y * Mathf.Deg2Rad);

            return new Vector3(xPos,yPos,zPos);
        }
        public static float abs(this float _input)
        {
            return Mathf.Abs(_input);
        }
        public static bool isInverse(float _base, float _input)
        {
            if (_base < 0 && _input > 0)
            {
                return true;
            }
            else if (_base > 0 && _input < 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public static Vector3 getInputVactorNegative(Vector3 _base, Vector3 _input)
        {
            return new Vector3(isInverse(_base.x, _input.x) ? _input.x : 0f, isInverse(_base.y, _input.y) ? _input.y : 0f, isInverse(_base.z, _input.z) ? _input.z : 0f);
        }
        public static Vector3 scaleWithFloat(this Vector3 vec3, float scale)
        {
            return new Vector3(vec3.x * scale, vec3.y * scale, vec3.z * scale);
        }
        public static Vector3 getFlatVector(Vector3 _input)
        {
            return new Vector3(_input.x, 0, _input.z);
        }
        public static Vector3 getFlatVector(Vector2 _input)
        {
            return new Vector3(_input.y, 0, _input.x);
        }
        public static Vector3 getFlat(this Vector3 _input)
        {
            return new Vector3(_input.x, 0, _input.z);
        }
        public static Vector3 getFlat(this Vector2 _input)
        {
            return new Vector3(_input.x, 0, _input.y);
        }
        public static Vector3 setFloatToAll(float _input)
        {
            return new Vector3(_input, _input, _input);
        }
        public static void InputEnable(InputActionAsset _input)
        {
            foreach (var item in _input.actionMaps)
            {
                foreach (var i in item.actions)
                {
                    i.Enable();
                }
            }
        }
        public class singelton<T> where T : class
        {
            private static T _singleton;
            public static T Singleton
            {
                get => _singleton;
                private set
                {
                    if (_singleton == null)
                        _singleton = value;
                    else if (_singleton != value)
                    {
                        Debug.Log($"{nameof(T)} instance already exists, destroying duplicate!");
                    }
                }
            }

        }
    }
    public interface IcomponentBehaviour
    {
        public void start();
        public void update();
    }
}