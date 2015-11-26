// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace BinaryTreeApp
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView DropableCanvas { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton GenerateNodesBtn { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView NodesContainerView { get; set; }

		[Action ("GenerateNodesBtn_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void GenerateNodesBtn_TouchUpInside (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (DropableCanvas != null) {
				DropableCanvas.Dispose ();
				DropableCanvas = null;
			}
			if (GenerateNodesBtn != null) {
				GenerateNodesBtn.Dispose ();
				GenerateNodesBtn = null;
			}
			if (NodesContainerView != null) {
				NodesContainerView.Dispose ();
				NodesContainerView = null;
			}
		}
	}
}
