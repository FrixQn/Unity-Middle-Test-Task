using System.Collections;

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImagePiece : MonoBehaviour
{
#region Constants
    const int MAX_SCALED_RECT_X = 690;
    const int MAX_SCALED_RECT_Y = 690;
    const int SCALING_SPEED = 30;
#endregion

#region SerializeField
    [SerializeField] private ImageNumber ImageNumb;
#endregion

#region Float
    private float StartSizeX;
    private float StartSizeY;
#endregion

#region Transform
    private Transform MaskTransform;
    private Transform PanelTranform;
#endregion
    
#region BuyPanel
    private BuyPanel BuyPanel;
#endregion

#region Integer
    public int Price {get; set;}
    public int ImageId {get => (int)ImageNumb;}
#endregion

#region Enumerators
    private enum ImageNumber {First = 0, Second = 1, Third = 2, Forth = 3};
#endregion

#region Sprite
    public Sprite ImageSprite { get ; set;}
#endregion

#region Image
    private Image LockedSprite;
    private Image CurrentImagePiece {get => gameObject.GetComponent<Image>();}
#endregion

#region IEnumerators
    private IEnumerator SetImage()
    {
        while(true)
        {
            if (ImageSprite != null)
            {
                CurrentImagePiece.sprite = ImageSprite;
                break;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator WaitPrice()
    {
        while (true)
        {
            if (Price != 0)
            {
                BuyPanel.SetPrice(Price);
            }
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator ScalingVisualization()
    {
        CurrentImagePiece.transform.SetParent(PanelTranform);
        RectTransform ImageRect = gameObject.GetComponent<RectTransform>();
        isScaling = true;

        while(ImageRect.sizeDelta.x <= MAX_SCALED_RECT_X && ImageRect.sizeDelta.y <= MAX_SCALED_RECT_Y)
        {
            ImageRect.sizeDelta += Vector2.one * SCALING_SPEED;
        
            yield return new WaitForFixedUpdate();
        }
        isScaling = false;
    }

    private IEnumerator DecreasingVisualization()
    {
        RectTransform ImageRect = gameObject.GetComponent<RectTransform>();
        isScaling = true;

        while(ImageRect.sizeDelta.x > StartSizeX && ImageRect.sizeDelta.y > StartSizeY)
        {
            
            ImageRect.sizeDelta -= Vector2.one * SCALING_SPEED;

            yield return new WaitForFixedUpdate();
        }

        CurrentImagePiece.transform.SetParent(MaskTransform);
        isScaling = false;
    }
#endregion

#region Boolean
    private bool isFullSize;
    private bool isLocked;
    private bool MayUnlock;
    private bool isUnlocked;
    private bool isScaling;

#endregion
    private void OnEnable()
    {
        StartCoroutine(SetImage());
        SetTransforms();
        LockedSprite = MaskTransform.GetComponentInChildren<LockedSprite>().Image;
        BuyPanel = MaskTransform.GetComponentInChildren<BuyPanel>();
        StartCoroutine(WaitPrice());
        StartSizeX = gameObject.GetComponent<RectTransform>().sizeDelta.x;
        StartSizeY = gameObject.GetComponent<RectTransform>().sizeDelta.y;
    }

    
    private void SetTransforms()
    {
        MaskTransform = CurrentImagePiece.transform.parent;
        PanelTranform = MaskTransform.transform.parent;
    }


    void Start()
    {
        
        if ((int)ImageNumb == 0)
            isUnlocked = true;
        else
            isLocked = true;

        AddButtonComponent();
          
    }

    private void Update()
    {
        if (MayUnlock && BuyPanel.IsTransactionSuccessed)
        {
            isUnlocked = true;
            MayUnlock = false;
            BuyPanel.SetState(false);
            CurrentImagePiece.enabled = true;
        }

        if (isLocked)
            LockedSprite.enabled = true;
            else
            LockedSprite.enabled = false;

        if (MayUnlock)
        {
            isLocked = false;
            BuyPanel.SetState(true);
            LockNextImage();
        }

        if (isUnlocked)
        {
            MayUnlockNextImage();
        }
    }
    
    private void AddButtonComponent()
    {
        Button pieceButton = CurrentImagePiece.gameObject.AddComponent<Button>();
        pieceButton.onClick.AddListener(SetFullSizeImage);
    }

    private void SetFullSizeImage()
    {
        if (!isScaling){

            isFullSize = !isFullSize;
            
            if(isFullSize == true)
            {
                StartCoroutine(ScalingVisualization());
                SoundController.Play(SoundController.statScalingSound);
            }else
            {
                StartCoroutine(DecreasingVisualization());
                SoundController.Play(SoundController.statScalingSound);
            }
        }
    }

    private void MayUnlockNextImage()
    {
        ImagePiece[] ArrayOfPieces = FindObjectsOfType<ImagePiece>();
        
        if (ImageId + 1 < ArrayOfPieces.Length)
            ArrayOfPieces[ImageId + 1].MayUnlock = true;
    }

    private void LockNextImage()
    {
        ImagePiece[] ArrayOfPieces = FindObjectsOfType<ImagePiece>();
        if (ImageId + 1 < ArrayOfPieces.Length)
            ArrayOfPieces[(int)ImageNumb + 1].isLocked = true;
    }

}
