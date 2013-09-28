using System;
using System.Drawing;
using System.Collections.Generic;
using System.Threading.Tasks;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.AVFoundation;
using BigTed;

namespace iOS.TranslateRecipe
{
	public partial class iOS_TranslateRecipeViewController : UIViewController
	{
		AdmAuthentication admAuthentication;
		AdmAccessToken token;
		UIActionSheet languageSheet;
		Languages.Code from;
		Languages.Code to;
		bool fromTapped;
		const string CLIENT_ID = "YOUR CLIENT ID HERE";
		const string CLIENT_SECRET = "YOUR CLIENT SECRET HERE";

		public iOS_TranslateRecipeViewController () : base ("iOS_TranslateRecipeViewController", null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
		
			// Setup button handles
			translate.TouchUpInside += TranslateHandleTouchUpInside;
			fromLanguage.TouchUpInside += FromLanguageHandleTouchUpInside; 
			toLanguage.TouchUpInside += ToLanguageHandleTouchUpInside; 

			// Set default languages
			from = Languages.Util.GetCode ("English (United States)");
			to = Languages.Util.GetCode ("Spanish; Castilian (Spain)");

			// Set UI
			imageView.Image = UIImage.FromFile ("microsoftTranslatorIcon.png"); 
			fromLanguage.SetTitle ("From: " + from.CountryName, UIControlState.Normal);
			toLanguage.SetTitle ("To: " + to.CountryName, UIControlState.Normal);
		}

		void FromLanguageHandleTouchUpInside (object sender, EventArgs e)
		{
			fromTapped = true;
			DisplayActionSheet ();
		}

		void ToLanguageHandleTouchUpInside (object sender, EventArgs e)
		{
			fromTapped = false;
			DisplayActionSheet ();
		}

		void DisplayActionSheet()
		{
			// Only load action sheet 1 time
			if (languageSheet == null) {
				languageSheet = new UIActionSheet ("Select language");
				languageSheet.Clicked += delegate(object sender, UIButtonEventArgs e) {

					// Get selected language
					var selectedSheetLanguage = ((UIActionSheet)sender).ButtonTitle (e.ButtonIndex);

					// Get the corresponding code instance
					var selected = Languages.Util.GetCode (selectedSheetLanguage);

					// Set UI labels & set language code instance
					if (fromTapped) {
						fromLanguage.SetTitle ("From: " + selected.CountryName, UIControlState.Normal);
						from = selected;
					} else {
						toLanguage.SetTitle ("To: " + selected.CountryName, UIControlState.Normal);
						to = selected;
					}
				};

				// Load sheet buttons
				foreach (var code in Languages.Util.GetLanguages()) {
					languageSheet.AddButton (code.CountryName);	
				}
			}

			// Display sheet
			languageSheet.ShowInView (this.View);
		}

		async void TranslateHandleTouchUpInside (object sender, EventArgs e)
		{
			// Make sure some text was entered.
			if (textField.Text.Length == 0) {
				var alert = new UIAlertView (null, "Please enter some text to translate.", null, "OK");
				alert.Show ();
				return;
			}

			await ProcessTranslation ();
		}

		async Task ProcessTranslation()
		{
			try {
				BTProgressHUD.Show("Translating...");	

				// setup clientId & clientSecret
				if (admAuthentication == null)
					admAuthentication = new AdmAuthentication (CLIENT_ID, CLIENT_SECRET);

				// Gets access token for microsoft translator
				if (token == null)
					token = await admAuthentication.GetAccessToken ();

				// Get translated text
				string translatedText = await new AdmTranslate ().TranslateText (token.access_token, textField.Text, from.AdmCode, to.AdmCode);

				// Setup speech and speak
				var speechSynthesizer = new AVSpeechSynthesizer ();
				var speechUtterance = new AVSpeechUtterance (translatedText) {
					Rate = AVSpeechUtterance.MaximumSpeechRate/10,
					Voice = AVSpeechSynthesisVoice.FromLanguage (to.AppleCode),
					Volume = 0.5f,
					PitchMultiplier = 1.0f
				};
				speechSynthesizer.SpeakUtterance (speechUtterance);

			} catch (Exception ex) {
				Console.WriteLine ("Error Processing Translation: {0}", ex.Message);
			}
			finally {
				BTProgressHUD.Dismiss ();
			}
		}
	}
}

