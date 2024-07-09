using System;
using UnityEngine;

namespace Facebook.FacebookSDK{
    [Serializable]
    public class PictureData{
        [SerializeField]
        public int height;
        [SerializeField]
        public bool is_silhouette;
        [SerializeField]
        public string url;
        [SerializeField]
        public int width;
    }
}