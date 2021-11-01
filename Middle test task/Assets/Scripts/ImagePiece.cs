using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class ImagePiece : MonoBehaviour
{
    const int MAX_SCALED_RECT_X = 690;
    const int MAX_SCALED_RECT_Y = 690;
    const int SCALING_SPEED = 30;

    private float StartSizeX;
    private float StartSizeY;

    private Transform MaskTransform;
    private Transform PanelTranform;
    private void SetTransforms()
    {
        MaskTransform = CurrentImagePiece.transform.parent;
        PanelTranform = MaskTransform.transform.parent;
    }

    private BuyPanel BuyPanel;
    public int Price {get; set;}


    private enum ImageNumber {First = 0, Second = 1, Third = 2, Forth = 3};
    [SerializeField] private ImageNumber ImageNumb;
    public int ImageId {get => (int)ImageNumb;}

    public Sprite ImageSprite { get ; set;}
    private Image LockedSprite;
    private Image CurrentImagePiece {get => gameObject.GetComponent<Image>();}

    //States
    private bool isFullSize = false;
    private bool isLocked = false;
    private bool MayUnlock = false;
    private bool isUnlocked = false;
    private bool isScaling;
    
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
        if (MayUnlock && BuyPanel.IsTransactionSuccessed())
        {
            isUnlocked = true;
            MayUnlock = false;
            BuyPanel.SetState(false);
            CurrentImagePiece.enabled = true;
        }

        if (isLocked)
        {
            LockedSprite.enabled = true;
        }else
        {
            LockedSprite.enabled = false;
        }

        if (MayUnlock)
        {
            isLocked = false;
            BuyPanel.SetState(true);
            SearchNextImageToUnlock();
        }

        if (isUnlocked)
        {
            SearchNextImageToBuy();
        }
    }
    private void AddButtonComponent()
    {
        Button pieceButton = CurrentImagePiece.gameObject.AddComponent<Button>();
        pieceButton.onClick.AddListener(SetFullSizeImage);
    }

    private void SetFullSizeImage()
    {
        if (isScaling != true){

            isFullSize = !isFullSize;
            
            if (isFullSize == false){
                StartCoroutine(DecreasingVisualization());
                SoundController.Play(SoundController.statScalingSound);
            }
            if(isFullSize == true)
            {
                CurrentImagePiece.transform.SetParent(PanelTranform);
                StartCoroutine(ScalingVisualization());
                SoundController.Play(SoundController.statScalingSound);
            }
        }
    }

    private void SearchNextImageToBuy()
    {
        ImagePiece[] ArrayOfPieces = FindObjectsOfType<ImagePiece>();
        int i = 0;
        while(true)
        {
            if (i >= ArrayOfPieces.Length)
            {
                break;
            }

            if (ArrayOfPieces[i].ImageId == (ImageId + 1) && (ImageId + 1) < ArrayOfPieces.Length && i < ArrayOfPieces.Length)
            {
                ArrayOfPieces[i].MayUnlock = true;
                break;
            }
            i++;
        }
    }

    private void SearchNextImageToUnlock()
    {
        ImagePiece[] ArrayOfPieces = FindObjectsOfType<ImagePiece>();
        int i = 0;
        while(true)
        {
            if (i >= ArrayOfPieces.Length)
            {
                break;
            }
            if (ArrayOfPieces[i].ImageId == (ImageId + 1) && (ImageId + 1) < ArrayOfPieces.Length)
            {
                ArrayOfPieces[i].isLocked = true;
                break;
            }
            i++;
        }
    }
    
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
}
