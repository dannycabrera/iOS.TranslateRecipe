using System;

namespace iOS.TranslateRecipe
{
	/// <summary>
	/// Adm access token.
	/// </summary>
	/// <remarks>http://msdn.microsoft.com/en-us/library/hh454950.aspx</remarks>
	public class AdmAccessToken
	{
		// The access token that you can use to authenticate you access to the Microsoft Translator API.
		public string access_token { get; set; }

		// The format of the access token.
		public string token_type { get; set; }

		// The number of seconds for which the access token is valid.
		public string expires_in { get; set; }

		// The domain for which this token is valid.
		public string scope { get; set; }
	}
}

