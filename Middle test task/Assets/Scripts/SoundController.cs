using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundController : MonoBehaviour
{
    
#region AuioSource
    private static AudioSource AudioSource = new AudioSource();
#endregion

#region SerializebleFields
    [SerializeField] private AudioClip Mistake;
    [SerializeField] private AudioClip ClickOnTheButton;
    [SerializeField] private AudioClip ScalingImageSound;
    [SerializeField] private AudioClip PurchasingSound;
#endregion

#region AudioClip
    public static AudioClip statMistake;
    public static AudioClip statClickOnTheButton;   
    public static AudioClip statScalingSound;
    public static AudioClip statPurchasingSound;
#endregion
    
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
