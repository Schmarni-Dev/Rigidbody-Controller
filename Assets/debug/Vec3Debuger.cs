using UnityEngine;
using Schmarni;

public class Vec3Debuger : MonoBehaviour
{
    public bool autoUpdate = true;
    public float scale = 1;
    public Vector3 Vector;
    public bool direction = false;
    public Transform[] arrows;

    public void DoUpdate()
    {
        Vector3 vec = Vector.scaleWithFloat(scale);
        arrows[0].localScale = helper.setFloatToAll(vec.x);
        arrows[1].localScale = helper.setFloatToAll(vec.y);
        arrows[2].localScale = helper.setFloatToAll(vec.z);
        if (direction)
        {
            arrows[3].gameObject.SetActive(true);
            arrows[3].localRotation =  Quaternion.LookRotation(Vector,transform.up);
            arrows[3].localScale = helper.setFloatToAll(Vector.magnitude);
        }
        else arrows[3].gameObject.SetActive(false);
    }

    private void Update()
    {
        if (autoUpdate) DoUpdate();
    }
    public void Init(bool _autoUpdate,bool _direction,float _scale)
    {
        autoUpdate = _autoUpdate;
        direction = _direction;
        scale = _scale;
    }
}
