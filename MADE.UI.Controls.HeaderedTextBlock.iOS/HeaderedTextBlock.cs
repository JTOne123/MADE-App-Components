﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HeaderedTextBlock.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a UI element representing read-only text with a header component.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.UI.Controls
{
    using System;
    using System.ComponentModel;

    using Foundation;
    using MADE.UI.Controls.Design.Styling;
    using MADE.UI.Design.Styling;
    using MADE.UI.Layout;
    using UIKit;

    /// <summary>
    /// Defines a UI element representing read-only text with a header component.
    /// </summary>
    [DesignTimeVisible(true)]
    public partial class HeaderedTextBlock : Control, IHeaderedTextBlock
    {
        private string header;

        private string text;

        private bool hideIfNullOrWhiteSpace;

        private Orientation orientation;

        private UILabelStyle headerStyle;
        private UILabelStyle textStyle;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderedTextBlock"/> class.
        /// </summary>
        /// <param name="handle">
        /// The handle.
        /// </param>
        public HeaderedTextBlock(IntPtr handle)
            : base(handle)
        {
            this.headerStyle = DefaultHeaderedTextBlockStyle.DefaultHeaderStyle;
            this.textStyle = DefaultHeaderedTextBlockStyle.DefaultTextStyle;
        }

        /// <summary>
        /// Gets or sets the string associated with the header.
        /// </summary>
        [Export("Header"), Browsable(true)]
        public string Header
        {
            get => this.header;
            set
            {
                this.Set(() => this.Header, ref this.header, value);
                this.UpdateHeader();
            }
        }

        /// <summary>
        /// Gets or sets the string associated with the text.
        /// </summary>
        [Export("Text"), Browsable(true)]
        public string Text
        {
            get => this.text;
            set
            {
                this.Set(() => this.Text, ref this.text, value);
                this.UpdateText();
            }
        }

        /// <summary>
        /// Gets or sets the orientation the header and text should layout as.
        /// </summary>
        [Export("Orientation"), Browsable(true)]
        public Orientation Orientation
        {
            get => this.orientation;
            set
            {
                this.Set(() => this.Orientation, ref this.orientation, value);
                this.UpdateOrientation();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to hide the control if the <see cref="IHeaderedTextBlock.Text"/> is null or whitespace.
        /// </summary>
        [Export("HideIfNullOrWhiteSpace"), Browsable(true)]
        public bool HideIfNullOrWhiteSpace
        {
            get => this.hideIfNullOrWhiteSpace;
            set
            {
                this.Set(() => this.HideIfNullOrWhiteSpace, ref this.hideIfNullOrWhiteSpace, value);
                this.UpdateVisibility();
            }
        }

        public UILabelStyle HeaderStyle
        {
            get
            {
                return headerStyle;
            }

            set
            {
                this.Set(() => this.HeaderStyle, ref this.headerStyle, value);
                this.UpdateHeader();
            }
        }

        public UILabelStyle TextStyle
        {
            get
            {
                return textStyle;
            }

            set
            {
                this.Set(() => this.TextStyle, ref this.textStyle, value);
                this.UpdateText();
            }
        }

        /// <summary>
        /// Gets the name of the nib to load.
        /// </summary>
        public override string NibName => "HeaderedTextBlock";

        public override UIView Root => this.RootView;

        /// <summary>
        /// Loads the relevant control template so that it's parts can be referenced.
        /// </summary>
        public override void OnApplyTemplate()
        {
            this.UpdateHeader();
            this.UpdateText();

            this.UpdateVisibility();
            this.UpdateOrientation();
        }

        /// <summary>
        /// Updates the layout for the control based on the current <see cref="IHeaderedTextBlock.Orientation"/> value.
        /// </summary>
        public void UpdateOrientation()
        {
            this.ContainerView?.SetOrientation(this.Orientation);
        }

        /// <summary>
        /// Updates the visibility of the control based on the values of the <see cref="IHeaderedTextBlock.Header"/> and <see cref="IHeaderedTextBlock.Text"/> properties.
        /// </summary>
        public void UpdateVisibility()
        {
            if (!this.HideIfNullOrWhiteSpace || !string.IsNullOrWhiteSpace(this.Text))
            {
                this.HeaderUiLabel?.SetVisible(!string.IsNullOrWhiteSpace(this.Header));
                this.TextUiLabel?.SetVisible(!string.IsNullOrWhiteSpace(this.Text));
            }
            else
            {
                this.HeaderUiLabel?.SetVisible(false);
                this.TextUiLabel?.SetVisible(false);
            }
        }

        private void UpdateText()
        {
            if (this.TextUiLabel != null)
            {
                this.TextStyle?.Apply(this.TextUiLabel);

                this.TextUiLabel.Text = this.Text;
            }
        }

        private void UpdateHeader()
        {
            if (this.HeaderUiLabel != null)
            {
                this.HeaderStyle?.Apply(this.HeaderUiLabel);

                this.HeaderUiLabel.Text = this.Header;
            }
        }
    }
}