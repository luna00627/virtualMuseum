using UnityEngine;
using System;
using System.Collections.Generic;

namespace Google.GoogleSDK
{
    /// <summary>
    /// Represents a utility class for calling the Google Platform APIs.
    /// </summary>
    public partial class GoogleAPI
    {
        /// <summary>
        /// Refreshes the current access token.
        /// </summary>
        /// <param name="action">
        /// The callback action to be invoked after this operation.
        /// </param>
        public static void RefreshAccessToken(Action<Result<AccessTokenRefreshResult>> action)
        {
            var identifier = AddAction(FlattenAction.JsonFlatten<AccessTokenRefreshResult>(action));
            NativeInterface.RefreshAccessToken(identifier);
        }

        /// <summary>
        /// Revokes the current access token.
        ///
        /// After the access token is revoked, you cannot use it again to access
        /// the Google Platform. The user must authorize your app again to issue
        /// a new access token before accessing the Google Platform.
        /// </summary>
        /// <param name="action">
        /// The callback action to be invoked after this operation.
        /// </param>
        public static void RevokeAccessToken(Action<Result<Unit>> action)
        {
            var identifier = AddAction(FlattenAction.UnitFlatten(action));
            NativeInterface.RevokeAccessToken(identifier);
        }

        /// <summary>
        /// Verifies the current access token.
        /// </summary>
        /// <param name="action">
        /// The callback action to be invoked after this operation.
        /// </param>
        public static void VerifyAccessToken(Action<Result<AccessTokenVerifyResult>> action)
        {
            var identifier = AddAction(FlattenAction.JsonFlatten<AccessTokenVerifyResult>(action));
            NativeInterface.VerifyAccessToken(identifier);
        }

        /// <summary>
        /// Gets the user’s profile.
        ///
        /// The "profile" scope is required to perform this operation.
        /// </summary>
        /// <param name="action">
        /// The callback action to be invoked after this operation.
        /// </param>
        public static void GetProfile(Action<Result<UserProfile>> action)
        {
            var identifier = AddAction(FlattenAction.JsonFlatten<UserProfile>(action));
            NativeInterface.GetProfile(identifier);
        }
    }

    partial class GoogleAPI
    {
        internal static void Login(string[] scopes, LoginOption option, Action<Result<LoginResult>> action)
        {
            var identifier = AddAction(FlattenAction.JsonFlatten<LoginResult>(action));
            if (scopes == null || scopes.Length == 0)
            {
                scopes = new string[] { "profile" };
            }

            string access_type = null;
            string state=null;
            bool include_granted_scopes=true;
            string login_hint=null;
            string prompt=null;
            if (option != null)
            {
                access_type = option.access_type;
                state = option.state;
                include_granted_scopes = option.include_granted_scopes;
                login_hint = option.login_hint;
                prompt = option.prompt;
            }
            NativeInterface.Login(string.Join(" ", scopes), access_type,state,include_granted_scopes,login_hint,prompt, identifier);
        }

        internal static void Logout(Action<Result<Unit>> action)
        {
            var identifier = AddAction(FlattenAction.UnitFlatten(action));
            NativeInterface.Logout(identifier);
        }
    }

    partial class GoogleAPI
    {
        internal static Dictionary<String, FlattenAction> actions = new Dictionary<string, FlattenAction>();
        static string AddAction(FlattenAction action)
        {
            var identifier = Guid.NewGuid().ToString();
            actions.Add(identifier, action);
            return identifier;
        }

        static FlattenAction PopActionFromPayload(CallbackPayload payload)
        {
            var identifier = payload.Identifier;
            if (identifier == null)
            {
                return null;
            }
            FlattenAction action = null;
            if (actions.TryGetValue(identifier, out action))
            {
                actions.Remove(identifier);
                return action;
            }
            return null;
        }

        internal static void _OnApiOk(string result)
        {
            var payload = CallbackPayload.FromJson(result);
            var action = PopActionFromPayload(payload);
            if (action != null)
            {
                action.CallOk(payload.Value);
            }
        }

        internal static void _OnApiError(string result)
        {
            var payload = CallbackPayload.FromJson(result);
            var action = PopActionFromPayload(payload);
            if (action != null)
            {
                action.CallError(payload.Value);
            }
        }
    }
}
