using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private Text textFPS;
    private float updateInterval = 0.5f;
    private float lastInterval;
    private int frames = 0;
    private float fps;

    void Start()
    {
        lastInterval = Time.realtimeSinceStartup;
        frames = 0;
    }

    void OnGUI()
    {
        textFPS.text = $"FPS: {(int)fps}";
    }
    void Update()
    {
        ++frames;
        float timeNow = Time.realtimeSinceStartup;
        if (timeNow > lastInterval + updateInterval)
        {
            fps = frames / (timeNow - lastInterval);
            frames = 0;
            lastInterval = timeNow;
        }
    }
}
