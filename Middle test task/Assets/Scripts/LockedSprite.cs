using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LockedSprite : MonoBehaviour
{
#region Image
    public Image Image{ get => GetComponent<Image>();}
#endregion
}
