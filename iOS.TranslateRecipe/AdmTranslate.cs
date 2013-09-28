using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Threading.Tasks;

namespace iOS.TranslateRecipe
{
	public class AdmTranslate
	{
		public AdmTranslate ()
		{
		}

		/// <summary>
		/// Translates the text.
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="access_token">Access_token.</param>
		/// <param name="textToTranslate">Text to translate.</param>
		/// <param name="fromLanguage">From language.</param>
		/// <param name="toLanguage">To language.</param>
		public async Task<string> TranslateText(string access_token, string textToTranslate, string fromLanguage, string toLanguage)
		{
			string uri = string.Format("http://api.microsofttranslator.com/v2/Http.svc/Translate?text={0}&from={1}&to={2}",
			                           WebUtility.UrlEncode(textToTranslate), fromLanguage, toLanguage);

			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
			httpWebRequest.Headers.Add("Authorization", string.Format("Bearer {0}", access_token));

			try
			{
				using (WebResponse response = await httpWebRequest.GetResponseAsync())
				{
					using (StreamReader reader = new StreamReader(response.GetResponseStream()))
					{
						string streamResponse = reader.ReadToEnd ();
						XmlDocument translation = new XmlDocument();
						translation.LoadXml(streamResponse);

						return translation.InnerText;
					}
				}
			}
			catch (Exception ex) {
				Console.WriteLine("Error Translating Text: {0}", ex.Message);
				return null;
			}
		}
	}
}