using System;
using Foundation;
using System.ComponentModel;
using UIKit;
using CoreGraphics;

namespace BinaryTreeApp
{
	[Register("BinarySearchTreeView"), DesignTimeVisible(true)]
	public class BinarySearchTreeView : UIView
	{
		const float NodeHeight = 50;
		const float NodeWidth = 50;
		const float HeightDistanceBetweenLevels = 50;

		NodeView root, current;

		public BinarySearchTreeView ()
		{
			initialize ();
		}

		public BinarySearchTreeView (IntPtr p) : base(p)
		{
			initialize ();
		}

		void initialize()
		{
			this.BackgroundColor = UIColor.Blue;
		}

		public void InsertNode(int data)
		{
			var newNode = new NodeView (data);

			AddSubview (newNode);

			if (root == null) {
				root = newNode;
				return;
			}

			current = root;

			while (true)
			{
				if (newNode.Data > current.Data)
				{
					if (current.RightChild == null)
					{
						current.RightChild = newNode;
						break;
					}
					current = current.RightChild;
				}
				else
				{
					if (current.LeftChild == null)
					{
						current.LeftChild = newNode;
						break;
					}  
					current = current.LeftChild;
				}
			}

			//Set the current frame
			var treeHeight = HeightOfTheNode (root) + 1;
	
			var frameHeightToSet = treeHeight * NodeHeight + (treeHeight - 1) * HeightDistanceBetweenLevels;
			var frameWidthToSet = Math.Pow (2, treeHeight) * NodeWidth;

			this.Frame = new CoreGraphics.CGRect (0, 0, frameWidthToSet, frameHeightToSet);

			var parent = (this.Superview as UIScrollView);

			if (parent != null)
				parent.ContentSize = new CGSize (Frame.Width, Frame.Height);

			SetNeedsLayout ();
			SetNeedsDisplay ();
		}

		public int HeightOfTheNode(NodeView node)
		{
			if (node == null)
				return -1;
			else
				return Math.Max (HeightOfTheNode (node.LeftChild), HeightOfTheNode (node.RightChild)) + 1;
		}

		public override void Draw (CoreGraphics.CGRect rect)
		{
			base.Draw (rect);
			Console.WriteLine ("Draw");

			if (root != null) {
				//start from root and draw lines to the children
				using (var g = UIGraphics.GetCurrentContext ()) {
					UIColor.Red.SetStroke ();
					//g.SetStrokeColor (UIColor.Red.CGColor);
					g.SetLineWidth (2f);

					drawLinesToChildren (g, root);
				}
			}
		}

		void drawLinesToChildren(CGContext context, NodeView node)
		{
			if (node.LeftChild != null) {
				context.MoveTo (node.Frame.X + NodeWidth / 2, node.Frame.Y + NodeHeight);
				context.AddLineToPoint (node.LeftChild.Frame.X + NodeWidth / 2, node.LeftChild.Frame.Y);
				context.StrokePath ();
				drawLinesToChildren (context, node.LeftChild);
			}

			if (node.RightChild != null) {
				context.MoveTo (node.Frame.X + NodeWidth / 2, node.Frame.Y + NodeHeight);
				context.AddLineToPoint (node.RightChild.Frame.X + NodeWidth / 2, node.RightChild.Frame.Y);
				context.StrokePath ();
				drawLinesToChildren (context, node.RightChild);
			}
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			Console.WriteLine ("Layout");

			//start with root node and set the frame for its children
			var treeHeight = HeightOfTheNode (root);

			if (treeHeight != -1)
				SetFrame (root, new CGRect (((Math.Pow (2, treeHeight) / 2)) * 2 * NodeWidth, 0, NodeWidth, NodeHeight));

			SetNeedsDisplay ();
		}


		void SetFrame(NodeView node, CGRect frame)
		{
			node.Frame = frame;

			if (node.LeftChild != null) {
				var nodeHeight = HeightOfTheNode (node.LeftChild) + 1;

				SetFrame (node.LeftChild, new CGRect (frame.X - (nodeHeight * NodeWidth), frame.Y + NodeHeight + HeightDistanceBetweenLevels , NodeWidth, NodeHeight));
			}
					
			if (node.RightChild != null) {
				var nodeHeight = HeightOfTheNode (node.RightChild) + 1;

				SetFrame (node.RightChild, new CGRect (frame.X + (NodeWidth * nodeHeight), frame.Y + NodeHeight + HeightDistanceBetweenLevels , NodeWidth, NodeHeight));
			}
		}
	}
}

