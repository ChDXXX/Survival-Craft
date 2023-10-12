using UnityEngine;

public class LightManager : MonoBehaviour
{
    public float cycleDuration = 240f; // Total cycle duration in seconds (4 minutes: morning to night)
    public LightSetting preset; // Reference to your DayNightPreset scriptable object
    private Light directionalLight;
    private float TimeOfDay = 0f;

    private void Start()
    {
        directionalLight = GetComponent<Light>();
        // Set the initial rotation to represent morning
        directionalLight.transform.localRotation = Quaternion.Euler(new Vector3(50f, -30f, 0));
    }

    private void Update()
    {
        if (preset == null)
            return;

        if (Application.isPlaying)
        {
            TimeOfDay += Time.deltaTime;
            TimeOfDay %= cycleDuration; // Modulus to ensure always between 0 and cycleDuration
            UpdateLighting(TimeOfDay / cycleDuration);
        }
        else
        {
            UpdateLighting(TimeOfDay / cycleDuration);
        }
    }

    private void UpdateLighting(float timePercent)
    {
        // Set ambient and fog
        RenderSettings.ambientLight = preset.ambientColor.Evaluate(timePercent);
        RenderSettings.fogColor = preset.fogColor.Evaluate(timePercent);

        // If the directional light is set then rotate and set its color
        if (directionalLight != null)
        {
            directionalLight.color = preset.directionalColor.Evaluate(timePercent);

            directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) +50f, -30f, 0));
        }
    }

    // Try to find a directional light to use if we haven't set one
    private void OnValidate()
    {
        if (directionalLight != null)
            return;

        // Search for lighting tab sun
        if (RenderSettings.sun != null)
        {
            directionalLight = RenderSettings.sun;
        }
        // Search scene for light that fits criteria (directional)
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    directionalLight = light;
                    return;
                }
            }
        }
    }
}
