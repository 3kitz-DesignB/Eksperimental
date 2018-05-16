using UnityEngine;
using UnityEngine.Audio;

internal sealed class MusicZone : MonoBehaviour
{
    [SerializeField]
    private AudioMixer mixer;

    [SerializeField]
    private AudioMixerSnapshot[] snapshots;

    [SerializeField]
    private float[] weights;

    [SerializeField]
    private float timeToReach = 1f;

    private void OnTriggerEnter(Collider other)
    {
        this.mixer.TransitionToSnapshots(this.snapshots, this.weights, this.timeToReach);
    }
}
