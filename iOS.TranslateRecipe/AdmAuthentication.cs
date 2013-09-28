using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace iOS.TranslateRecipe
{
	public class AdmAuthentication
	{
		public AdmAuthentication ()
		{
		}

		public static readonly string DatamarketAccessUri = "https://datamarket.accesscontrol.windows.net/v2/OAuth2-13";
		private string request;
		private AdmAccessToken token;
		private Timer accessTokenRenewer;

		//Access token expires every 10 minutes. Renew it every 9 minutes only.
		private const int RefreshTokenDuration = 9;

		public AdmAuthentication(string clientId, string clientSecret)
		{
			this.request = string.Format("grant_type=client_credentials&client_id={0}&client_secret={1}&scope=http://api.microsofttranslator.com"
			                             , WebUtility.UrlEncode(clientId)
			                             , WebUtility.UrlEncode(clientSecret));
		}

		public async Task<AdmAccessToken> GetAccessToken()
		{
			// Get token
			this.token = await HttpPost(DatamarketAccessUri, this.request);

			// Renew the token every specfied minutes
			accessTokenRenewer = new Timer(new TimerCallback(OnTokenExpiredCallback)
			                               , this
			                               , TimeSpan.FromMinutes(RefreshTokenDuration)
			                               , TimeSpan.FromMilliseconds(-1));

			return this.token;
		}

		async void RenewAccessToken()
		{
			// Get new token
			AdmAccessToken newAccessToken = await HttpPost(DatamarketAccessUri, this.request);

			//swap the new token with old one
			//Note: the swap is thread unsafe
			this.token = newAccessToken;
		}

		/// <summary>
		/// Raises the token expired callback event.
		/// </summary>
		/// <param name="stateInfo">State info.</param>
		private void OnTokenExpiredCallback(object stateInfo)
		{
			try
			{
				RenewAccessToken();
			}
			catch (Exception ex)
			{
				Console.WriteLine(string.Format("Failed renewing access token. Details: {0}", ex.Message));
			}
			finally
			{
				try
				{
					accessTokenRenewer.Change(TimeSpan.FromMinutes(RefreshTokenDuration), TimeSpan.FromMilliseconds(-1));
				}
				catch (Exception ex)
				{
					Console.WriteLine(string.Format("Failed to reschedule the timer to renew access token. Details: {0}", ex.Message));
				}
			}
		}

		async Task<AdmAccessToken> HttpPost(string DatamarketAccessUri, string requestDetails)
		{
			//Prepare OAuth request 
			WebRequest webRequest = WebRequest.Create(DatamarketAccessUri);
			webRequest.ContentType = "application/x-www-form-urlencoded";
			webRequest.Method = "POST";
			byte[] bytes = Encoding.ASCII.GetBytes(requestDetails);
			webRequest.ContentLength = bytes.Length;
			using (Stream outputStream = await webRequest.GetRequestStreamAsync())
			{
				outputStream.Write(bytes, 0, bytes.Length);
			}

			using (WebResponse webResponse = await webRequest.GetResponseAsync())
			{
				using (StreamReader reader = new StreamReader(webResponse.GetResponseStream())) {
					string streamResponse = reader.ReadToEnd ();
					Console.WriteLine ("Stream response: {0}", streamResponse);
					AdmAccessToken token = (AdmAccessToken)Newtonsoft.Json.JsonConvert.DeserializeObject<AdmAccessToken> (streamResponse);

					return token;
				}
			}
		}
	}
}

