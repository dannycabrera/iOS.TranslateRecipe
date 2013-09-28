using System;
using System.Linq;
using System.Collections.Generic;

namespace iOS.TranslateRecipe.Languages
{
	public class Code
	{
		public Code(string admCode, string appleCode, string countryName)
		{
			this.AdmCode = admCode;
			this.AppleCode = appleCode;
			this.CountryName = countryName;
		}

		/// <summary>
		/// Azure data market translator language code.
		/// </summary>
		/// <value>The adm code.</value>
		/// <remarks>http://msdn.microsoft.com/en-us/library/hh456380.aspx</remarks>
		public string AdmCode { get; set; }

		/// <summary>
		/// Apple speech voice code.
		/// </summary>
		/// <value>The apple code.</value>
		/// <remarks>Available from: AVSpeechSynthesisVoice.GetSpeechVoices()</remarks>
		public string AppleCode { get; set; }

		/// <summary>
		/// Gets or sets the name of the country.
		/// </summary>
		/// <value>The name of the country.</value>
		public string CountryName { get; set; }
	}

	public static class Util
	{
		/// <summary>
		/// Get code instances using country name
		/// </summary>
		/// <returns>Instance of the code.</returns>
		/// <param name="countryName">Country name.</param>
		public static Code GetCode(string countryName)
		{
			var code = (
				from c in GetLanguages () 
				where c.CountryName == countryName
				select c).First();

			return code;
		}

		/// <summary>
		/// Gets the languages.
		/// </summary>
		/// <returns>List of languages.</returns>
		/// <remarks>
		public static List<Code> GetLanguages()
		{
			var codes = new List<Code> ();
			codes.Add (new Code("ar", "ar-SA", "Arabic (Saudi Arabia)"));
			codes.Add (new Code("cs", "cs-CZ", "Czech (Czech Republic)"));
			codes.Add (new Code("da", "da-DK", "Danish (Denmark)"));
			codes.Add (new Code("de", "de-DE", "German (Germany)"));
			codes.Add (new Code("el", "el-GR", "Greek, Modern (Greece)"));
			codes.Add (new Code("en", "en-AU", "English (Australia)"));
			codes.Add (new Code("en", "en-GB", "English (United Kingdom)"));
			codes.Add (new Code("en", "en-IE", "English (Ireland)"));
			codes.Add (new Code("en", "en-US", "English (United States)"));
			codes.Add (new Code("en", "en-ZA", "English (South Africa)"));
			codes.Add (new Code("es", "es-ES", "Spanish; Castilian (Spain)"));
			codes.Add (new Code("es", "es-MX", "Spanish; Castilian (Mexico)"));
			codes.Add (new Code("fi", "fi-FI", "Finnish (Finland)"));
			codes.Add (new Code("fr", "fr-CA", "French (Canada)"));
			codes.Add (new Code("fr", "fr-FR", "French (France)"));
			codes.Add (new Code("hi", "hi-IN", "Hindi (India)"));
			codes.Add (new Code("hu", "hu-HU", "Hungarian (Hungary)"));
			codes.Add (new Code("id", "id-ID", "Indonesian (Indonesia)"));
			codes.Add (new Code("it", "it-IT", "Italian (Italy)"));
			codes.Add (new Code("ja", "ja-JP", "Japanese (Japan)"));
			codes.Add (new Code("ko", "ko-KR", "Korean (Korea, Republic of)"));
			codes.Add (new Code("nl", "nl-BE", "Dutch (Belgium)"));
			codes.Add (new Code("nl", "nl-NL", "Dutch (Netherlands)"));
			codes.Add (new Code("no", "no-NO", "Norwegian (Norway)"));
			codes.Add (new Code("pl", "pl-PL", "Polish (Poland)"));
			codes.Add (new Code("pt", "pt-BR", "Portuguese (Brazil)"));
			codes.Add (new Code("pt", "pt-PT", "Portuguese (Portugal)"));
			codes.Add (new Code("ro", "ro-RO", "Romanian, Moldavian, Moldovan (Romania)"));
			codes.Add (new Code("ru", "ru-RU", "Russian (Russian Federation)"));
			codes.Add (new Code("sk", "sk-SK", "Slovak (Slovakia)"));
			codes.Add (new Code("sv", "sv-SE", "Swedish (Sweden)"));
			codes.Add (new Code("th", "th-TH", "Thai (Thailand)"));
			codes.Add (new Code("tr", "tr-TR", "Turkish (Turkey)"));
			codes.Add (new Code("zh-CHS", "zh-CN", "Chinese (China)"));
			codes.Add (new Code("zh-CHT", "zh-HK", "Chinese (Hong Kong)"));
			codes.Add (new Code("zh-CHT", "zh-TW", "Chinese (Taiwan, Province of China)"));

			return codes;
		}
	}
}