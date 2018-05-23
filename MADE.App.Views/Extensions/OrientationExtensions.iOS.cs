﻿#if __IOS__
namespace MADE.App.Views.Extensions
{
    using MADE.App.Views.Layout;

    using UIKit;

    public static partial class OrientationExtensions
    {
        /// <summary>
        /// Sets the current orientation of the given <see cref="UIStackView"/> layout control by the given orientation value.
        /// </summary>
        /// <param name="layout">
        /// The <see cref="UIStackView"/> to update the orientation.
        /// </param>
        /// <param name="orientation">
        /// The orientation to update with.
        /// </param>
        public static void SetOrientation(this UIStackView layout, Orientation orientation)
        {
            if (layout == null)
            {
                return;
            }

            layout.Axis = orientation == Orientation.Vertical ? UILayoutConstraintAxis.Vertical : UILayoutConstraintAxis.Horizontal;
        }
    }
}
#endif