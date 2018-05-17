using System.Collections;
using UnityEngine;
using VRTK;

[RequireComponent(typeof(VRTK_ControllerEvents))]
internal sealed class TimeControl : MonoBehaviour
{
    [SerializeField]
    private AutoIntensity sunControl;

    [SerializeField]
    private float
        multiplier = 2f,
        smoothTime = 1f;

    private float
        normalSkySpeed,
        fastSkySpeed,
        speedVelocity;
    
    private VRTK_ControllerEvents events;

    private void Awake()
    {
        this.events = this.GetComponent<VRTK_ControllerEvents>();

        if (!(this.events && this.sunControl))
        {
            Debug.LogWarning("Missing dependencies");
            this.enabled = false;
        }
    }

    private void Start()
    {
        this.normalSkySpeed = this.sunControl.SkySpeed;
        this.fastSkySpeed = this.normalSkySpeed * this.multiplier;
    }

    private void Update()
    {
        this.sunControl.SkySpeed = Mathf.SmoothDamp(
            this.sunControl.SkySpeed,
            this.events.triggerPressed
                ? this.fastSkySpeed
                : this.normalSkySpeed,
            ref this.speedVelocity,
            this.smoothTime);
    }
}
