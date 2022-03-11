using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Schmarni.player;

public class DebugDisplay : MonoBehaviour
{
    private Rigidbody rb;
    private TMPro.TMP_Text txt;
    public movementWrapper move;
    // Start is called before the first frame update
    void Awake()
    {
        txt = GetComponent<TMPro.TMP_Text>();
        rb = move.Debug();
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = string.Format("Velocity: {0}\n",rb.velocity.magnitude);
    }
}
