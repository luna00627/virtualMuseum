using UnityEngine;
using System;
using System.Collections.Generic;

namespace Facebook.FacebookSDK{
    
    public partial class FacebookAPI{

        public static void RefreshAccessToken(Action<Result<AccessToken>> action){
            var identifier = AddAction(FlattenAction.JsonFlatten<AccessToken> (action));
            NativeInterface.RefreshAccessToken(identifier);
        }

        public static void RevokeAccessToken(Action<Result<Unit>> action){
            var identifier = AddAction(FlattenAction.JsonFlatten<Unit>(action));
            NativeInterface.RevokeAccessToken(identifier);
        }

        public static void VerifyAccessToken(Action<Result<AccessTokenVerifyResult>> action){
            var identifier = AddAction(FlattenAction.JsonFlatten<AccessTokenVerifyResult>(action));
            NativeInterface.VerifyAccessToken(identifier);
        }

        public static void GetProfile(Action<Result<UserProfile>> action){
            var identifier = AddAction(FlattenAction.JsonFlatten<UserProfile>(action));
            NativeInterface.GetProfile(identifier);
        }
    }

    partial class FacebookAPI {
        internal static void Login(string[] scopes, LoginOption option, Action<Result<LoginResult>> action){
            var identifier = AddAction(FlattenAction.JsonFlatten<LoginResult>(action));
            if (scopes == null || scopes.Length == 0){
                scopes = new string[] {"public_profile"};
            }

            NativeInterface.Login(string.Join(" ", scopes), identifier);
        }

        internal static void Logout(Action<Result<Unit>> action){
            var identifier = AddAction(FlattenAction.UnitFlatten(action));
            NativeInterface.Logout(identifier);
        }
    }

    partial class FacebookAPI {
        internal static Dictionary<string, FlattenAction> actions = new Dictionary<string, FlattenAction>();
        static string AddAction(FlattenAction action){
            var identifier = Guid.NewGuid().ToString();
            actions.Add(identifier, action);
            return identifier;
        }

        static FlattenAction PopActionFromPayload(CallbackPayload payload){
            var identifier = payload.Identifier;
            if(identifier == null){
                return null;
            }
            FlattenAction action = null;
            if(actions.TryGetValue(identifier, out action)){
                actions.Remove(identifier);
                return action;
            }
            return null;
        }

        internal static void _OnApiOk(string result){
            var payload = CallbackPayload.FromJson(result);
            var action = PopActionFromPayload(payload);
            if(action != null){
                action.CallOk(payload.Value);
            }
        }

        internal static void _OnApiError(string result){
            var payload = CallbackPayload.FromJson(result);
            var action = PopActionFromPayload(payload);
            if(action != null){
                action.CallError(payload.Value);
            }
        }
    }
}