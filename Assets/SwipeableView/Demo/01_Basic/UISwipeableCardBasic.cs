using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SwipeableView
{
    public class UISwipeableCardBasic : UISwipeableCard<BasicCardData>
    {
        [SerializeField]
        private Image bg = default;

        [SerializeField]
        private CanvasGroup imgLike = default;

        [SerializeField]
        private CanvasGroup imgNope = default;

        [SerializeField] private Text _title;
        [SerializeField] private Text _description;
        [SerializeField] private Transform _tagsContainer;
        [SerializeField] private GameObject _tagPrefab;
        
        [SerializeField] private UnityEvent onDislike;
        [SerializeField] private UnityEvent onLike;
        
        [SerializeField] private VerticalLayoutGroup _info;

        private UISwipeableViewBasic swipeableView;

        private BasicCardData _data;

        public override void UpdateContent(BasicCardData data)
        {
            _data = data;
            swipeableView = data.swipeableViewBasic;

            bg.sprite = data.sprite;
            _title.text = data.title;
            _description.text = data.description;
            
            foreach (Transform tag in _tagsContainer)
                Destroy(tag.gameObject);

            foreach (var tag in data.tags)
            {
                var t = Instantiate(_tagPrefab, _tagsContainer);
                t.transform.Find("Text").GetComponent<Text>().text = tag;
            }
            imgLike.alpha = 0;
            imgNope.alpha = 0;
            StartCoroutine(UpdateContent());
        }

        private IEnumerator UpdateContent()
        {
            yield return new WaitForSeconds(0.01f);
            _info.spacing += 0.1f;
        }
        
        public void OnClickLike()
        {
            if (swipeableView.IsAutoSwiping) return;
            swipeableView.AutoSwipe(SwipeDirection.Right);
        }

        public void OnClickNope()
        {
            if (swipeableView.IsAutoSwiping) return;
            swipeableView.AutoSwipe(SwipeDirection.Left);
        }

        private bool cardSelected;

        protected override void SwipingRight(float rate)
        {
            OnEndDrag(rate, History.SwipeType.Like);
            imgLike.alpha = rate;
            imgNope.alpha = 0;
        }

        private void OnEndDrag(float rate, History.SwipeType type)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (cardSelected)
                    return;
            
                if (Math.Abs(rate) > 1)
                {
                    History.Add(_data, type);
                    cardSelected = true;
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (rate == 0)
                    cardSelected = false;   
            }
        }

        protected override void SwipingLeft(float rate)
        {
            OnEndDrag(rate, History.SwipeType.Dislike);

            imgNope.alpha = rate;
            imgLike.alpha = 0;
        }
    }
}