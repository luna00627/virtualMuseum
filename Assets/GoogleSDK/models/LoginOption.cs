namespace Google.GoogleSDK
{
    /// <summary>
    /// Represents options for logging in to the Google Platform.
    /// </summary>
    public class LoginOption
    {
        /// <summary>
        /// 指出應用程式是否在使用者瀏覽器中重新整理時，可以重新整理存取權杖。有效的參數值為 online，也就是預設值和 offline。
        /// 如果您的應用程式並未在使用者瀏覽器中重新整理權杖，請將值設為 offline。這是重新整理本文件稍後說明存取權杖的方法。
        /// </summary>
        /// <value>
        /// - "offline": 這個值會指示 Google 授權伺服器在應用程式首次交換權杖的驗證權杖時傳回更新權杖「和」存取權杖。
        /// - "online": 
        /// </value>
        public string access_type { get; set; }

        /// <summary>
        /// 指定應用程式用來維持授權要求和授權伺服器回應狀態的任何字串值。使用者同意或拒絕應用程式存取要求後，伺服器會傳回 redirect_uri 網址查詢元件 (?) 中做為 name=value 組合傳送的確切值。
        /// </summary>
        /// <value>
        /// 您可以將這個參數用於多種用途，例如將使用者導向應用程式中的正確資源、傳送 Nonce，以及減少跨網站偽造要求。由於您可以猜測 redirect_uri，因此使用 state 值可能會增加傳入連線視為驗證要求的結果。如果您產生隨機字串或對 Cookie 的雜湊進行編碼，或者是擷取用戶端狀態的其他值，可以驗證回應，進一步確保要求和回應來自同一個瀏覽器，以提供防範跨網站要求偽造等攻擊的功能。如需如何建立及確認 state 權杖的範例，請參閱 OpenID Connect 說明文件。
        /// </value>
        public string state { get; set; }

        /// <summary>
        /// 允許應用程式使用漸進式授權功能要求結構定義中的其他範圍。
        /// </summary>
        /// <value>
        /// - true: 新的存取權杖也會涵蓋使用者先前已授予應用程式存取權的所有範圍。
        /// </value>
        public bool include_granted_scopes { get; set; }


        /// <summary>
        /// 如果您的應用程式知道要嘗試驗證的使用者，可以使用這個參數向 Google 驗證伺服器提供提示。伺服器會提示使用者在登入表單中填寫電子郵件欄位，或是選取適當的多重登入工作階段，藉此簡化登入流程。
        /// </summary>
        /// <value>
        /// 將參數值設為電子郵件地址或 sub ID，等同於使用者的 Google ID。
        /// </value>
        public string login_hint { get; set; }

        /// <summary>
        /// 以空格分隔且區分大小寫的提示清單，以向使用者顯示。如未指定這個參數，使用者會在專案首次要求存取權時收到提示。詳情請參閱「提示重新同意聲明」。
        /// </summary>
        /// <value>
        /// - "none": 不要顯示任何驗證或同意畫面。不得指定其他值。
        /// - "consent": 提示使用者提供同意聲明。
        /// - "select_account": 提示使用者選取帳戶。
        /// </value>
        public string prompt { get; set; }
    }
}
