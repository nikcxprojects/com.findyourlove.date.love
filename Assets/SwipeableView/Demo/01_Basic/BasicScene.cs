using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = System.Random;

namespace SwipeableView
{
    public class BasicScene : MonoBehaviour
    {
        [SerializeField]
        private UISwipeableViewBasic swipeableView = default;

        [SerializeField] private Profile _profile;
        [SerializeField] private UnityEvent _onCardOver;

        public string malePath;
        public string femalePath;
        
        void Start()
        {
            UpdateData();
        }

        public void UpdateData()
        {
            var data = new List<BasicCardData>();

            Debug.Log(_profile.GetTargetGender());
            
            switch (_profile.GetTargetGender())
            {
                case Profile.TargetGender.Male:
                    data = GenerateData(malePath);
                    break;
                case Profile.TargetGender.Female:
                    data = GenerateData(femalePath);
                    break;
                case Profile.TargetGender.Everyone:
                    data = GenerateData(malePath);
                    var a = GenerateData(femalePath);
                    data.AddRange(a);
                    break;
            }
            
            var rng = new Random();
            var d = data.OrderBy(a => rng.Next()).ToList();
            
            swipeableView.UpdateData(d);
        }

        public void CardsIsOver()
        {
            if(swipeableView.cardsIsOver())  _onCardOver.Invoke();
        }

        private List<BasicCardData> GenerateData(string path)
        {
            var data = new List<BasicCardData>();
            var jFemale= JObject.Parse(Resources.Load($"{path}/data").ToString());
            var array = (JArray) jFemale["array"];
            foreach (var jToken in array)
            {
                var obj = (JObject) jToken;
                var cardData = new BasicCardData();
                var id = (int) obj["photo"];
                var sprite = Resources.Load<Sprite>($"{path}/{id}");
                var description = (string) obj["Description"];
                var title = (string) obj["Title"];
                var tags = new List<string>();
                var _tags = (JArray) obj["Tags"];
                foreach (string tag in _tags)
                    tags.Add(tag);

                cardData.pathToPhoto = path + "/" + id;
                cardData.description = description;
                cardData.tags = tags;
                cardData.title = title;
                cardData.sprite = sprite;
                cardData.swipeableViewBasic = swipeableView;
                data.Add(cardData);
            }

            return data;
        }
    }
}