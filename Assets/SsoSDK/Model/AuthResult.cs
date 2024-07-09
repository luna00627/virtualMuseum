namespace Sso.SsoSDK{
    using System;
    using UnityEngine;
    [Serializable]
    public class AuthResult{
        [SerializeField]
        public long userId;
        [SerializeField]
        public string userName;
        [SerializeField]
        public string userPhotoUrl;


        public UserProfile ToUserProfile(){
            if(this==null){
                return null;
            }
            UserProfile userProfile = new UserProfile();
            userProfile.userId = this.userId;
            userProfile.userName = this.userName;
            userProfile.userPhotoUrl = this.userPhotoUrl;
            return userProfile;
        }
    }
}