using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Managers.UserActions
{
    public class CardDragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler 
    {
        private RectTransform _rectTransform;

        [SerializeField]
        private CanvasGroup _canvasGroup;

        [SerializeField]
        private Canvas _canvas;

        public Actor Actor { get; set; }
        public Vector2 OriginPos { get; set; }
        public Vector2 PositionOnStart { get; set; }
        public ActorSlot CurrentSlot { get; set; }

        public void Start()
        {
            PositionOnStart = _rectTransform.anchoredPosition;
            gameObject.SetActive(false);       
        }

        public void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            //_canvasGroup = GetComponent<CanvasGroup>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _canvasGroup.alpha = .6f;
            _canvasGroup.blocksRaycasts = false;

            if(CurrentSlot != null)
            {
                OriginPos = CurrentSlot.GetComponent<RectTransform>().anchoredPosition;
            }
            else
            {
                OriginPos = _rectTransform.anchoredPosition;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;            
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.blocksRaycasts = true;
            SetCardToOriginalPosition();
        }

        public void SetCardToOriginalPosition()
        {
            _rectTransform.anchoredPosition = OriginPos;
        }

        public void TurnOffRayCast()
        {
            _canvasGroup.blocksRaycasts = false;
        }

        public void TurnOnRayCast()
        {
            _canvasGroup.blocksRaycasts = true;
        }
    }
}
