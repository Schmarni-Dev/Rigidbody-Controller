using TMPro;
using UnityEngine;

public class fpsDisplay : MonoBehaviour
{
    private TMP_Text text;
    // [SerializeField] private float _hudRefreshRate = 1f;
    private float _timer;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {


        
        // if (Time.unscaledTime > _timer)
        // {
            int fps = (int)(1f / Time.unscaledDeltaTime);
            text.text = "FPS: " + fps;
            // _timer = Time.unscaledTime + _hudRefreshRate;
        // }

    }
}
