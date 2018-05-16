using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

internal sealed class SnapshotZone : MonoBehaviour
{
    private static AudioMixerSnapshot[] activeSnapshots;

    private static SnapshotZone activeZone;

    [SerializeField]
    private AudioMixer mixer;

    [SerializeField]
    private AudioMixerSnapshot[] snapshots;

    [SerializeField]
    private float[] weights;

    [SerializeField]
    private float timeToReach = 1f;

    [SerializeField]
    private int priority;

    private bool HasStartedTransition
    {
        get { return activeSnapshots == this.snapshots; }
    }

    private int registerCounter;

    private void OnTriggerStay(Collider other)
    {
        this.RegisterActiveZone();
    }

    private int resetCounter;

    private void Update()
    {
        if (activeZone && !activeZone.HasStartedTransition)
        {
            activeZone.DoTransition();
        }

        activeZone = null;
    }

    private void RegisterActiveZone()
    {
        if (!activeZone || activeZone.priority < this.priority)
        {
            activeZone = this;
        }
    }

    private void DoTransition()
    {
        Debug.Log("DoTransition");
        this.mixer.TransitionToSnapshots(this.snapshots, this.weights, this.timeToReach);
        activeSnapshots = this.snapshots;
    }
}
