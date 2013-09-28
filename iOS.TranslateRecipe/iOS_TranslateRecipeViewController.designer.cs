// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace iOS.TranslateRecipe
{
	[Register ("iOS_TranslateRecipeViewController")]
	partial class iOS_TranslateRecipeViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton fromLanguage { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView imageView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField textField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton toLanguage { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton translate { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (imageView != null) {
				imageView.Dispose ();
				imageView = null;
			}

			if (fromLanguage != null) {
				fromLanguage.Dispose ();
				fromLanguage = null;
			}

			if (textField != null) {
				textField.Dispose ();
				textField = null;
			}

			if (toLanguage != null) {
				toLanguage.Dispose ();
				toLanguage = null;
			}

			if (translate != null) {
				translate.Dispose ();
				translate = null;
			}
		}
	}
}
