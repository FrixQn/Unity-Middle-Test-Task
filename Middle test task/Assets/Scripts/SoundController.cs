using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundController : MonoBehaviour
{
    private static AudioSource AudioSource = new AudioSource();
    [SerializeField] private AudioClip Mistake;
    public static AudioClip statMistake;
    [SerializeField] private AudioClip ClickOnTheButton;
    public static AudioClip statClickOnTheButton;
    [SerializeField] private AudioClip ScalingImageSound;
    public static AudioClip statScalingSound;
    [SerializeField] private AudioClip PurchasingSound;
    public static AudioClip statPurchasingSound;

    private void Start()
    {
        AudioSource = gameObject.GetComponent<AudioSource>();
        statMistake = Mistake;
        statClickOnTheButton = ClickOnTheButton;
        statScalingSound = ScalingImageSound;
        statPurchasingSound = PurchasingSound;
    }

    public static void Play(AudioClip sound)
    {
        AudioSource.PlayOneShot(sound);
    }
}
