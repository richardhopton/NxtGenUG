using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace Presenter
{
    /// <summary>
    /// OutlineText custom control class derives layout, event, data binding, and rendering from derived FrameworkElement class.
    /// </summary>
    public class OutlineTextBlock : FrameworkElement
    {
        public static readonly DependencyProperty FillProperty = DependencyProperty.Register(
            "Fill",
            typeof (Brush),
            typeof (OutlineTextBlock),
            new FrameworkPropertyMetadata(
                Brushes.Black,
                FrameworkPropertyMetadataOptions.AffectsRender,
                OnOutlineTextInvalidated,
                null
                )
            );

        public static readonly DependencyProperty FontFamilyProperty = DependencyProperty.Register(
            "FontFamily",
            typeof (FontFamily),
            typeof (OutlineTextBlock),
            new FrameworkPropertyMetadata(
                SystemFonts.MessageFontFamily,
                FrameworkPropertyMetadataOptions.AffectsRender |
                FrameworkPropertyMetadataOptions.AffectsMeasure,
                OnOutlineTextInvalidated,
                null
                )
            );

        public static readonly DependencyProperty FontSizeProperty = DependencyProperty.Register(
            "FontSize",
            typeof (double),
            typeof (OutlineTextBlock),
            new FrameworkPropertyMetadata(
                SystemFonts.MessageFontSize,
                FrameworkPropertyMetadataOptions.AffectsRender |
                FrameworkPropertyMetadataOptions.AffectsMeasure,
                OnOutlineTextInvalidated,
                null
                )
            );

        public static readonly DependencyProperty FontStyleProperty = DependencyProperty.Register(
            "FontStyle",
            typeof (FontStyle),
            typeof (OutlineTextBlock),
            new FrameworkPropertyMetadata(
                SystemFonts.MessageFontStyle,
                FrameworkPropertyMetadataOptions.AffectsRender |
                FrameworkPropertyMetadataOptions.AffectsMeasure,
                OnOutlineTextInvalidated,
                null
                )
            );

        public static readonly DependencyProperty FontWeightProperty = DependencyProperty.Register(
            "FontWeight",
            typeof (FontWeight),
            typeof (OutlineTextBlock),
            new FrameworkPropertyMetadata(
                SystemFonts.MessageFontWeight,
                FrameworkPropertyMetadataOptions.AffectsRender |
                FrameworkPropertyMetadataOptions.AffectsMeasure,
                OnOutlineTextInvalidated,
                null
                )
            );

        public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register(
            "Stroke",
            typeof (Brush),
            typeof (OutlineTextBlock),
            new FrameworkPropertyMetadata(
                Brushes.Black,
                FrameworkPropertyMetadataOptions.AffectsRender,
                OnOutlineTextInvalidated,
                null
                )
            );

        public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register(
            "StrokeThickness",
            typeof (ushort),
            typeof (OutlineTextBlock),
            new FrameworkPropertyMetadata(
                (ushort) 0,
                FrameworkPropertyMetadataOptions.AffectsRender |
                FrameworkPropertyMetadataOptions.AffectsMeasure,
                OnOutlineTextInvalidated,
                null
                )
            );

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text",
            typeof (string),
            typeof (OutlineTextBlock),
            new FrameworkPropertyMetadata(
                String.Empty,
                FrameworkPropertyMetadataOptions.AffectsRender,
                OnOutlineTextInvalidated,
                null
                )
            );

        private FormattedText _formattedText;
        private Geometry _textGeometry;

        public FontWeight FontWeight
        {
            get { return (FontWeight) GetValue(FontWeightProperty); }
            set { SetValue(FontWeightProperty, value); }
        }

        public Brush Fill
        {
            get { return (Brush) GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        public FontFamily FontFamily
        {
            get { return (FontFamily) GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        public double FontSize
        {
            get { return (double) GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public FontStyle FontStyle
        {
            get { return (FontStyle) GetValue(FontStyleProperty); }
            set { SetValue(FontStyleProperty, value); }
        }

        public Brush Stroke
        {
            get { return (Brush) GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        public ushort StrokeThickness
        {
            get { return (ushort) GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (_textGeometry != null)
            {
                return new Size(_formattedText.Width, _formattedText.Height);
            }
            return base.MeasureOverride(availableSize);
        }

        private static void OnOutlineTextInvalidated(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((OutlineTextBlock) d).CreateText();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            drawingContext.DrawGeometry(Fill, new Pen(Stroke, StrokeThickness), _textGeometry);
        }

        public void CreateText()
        {
            _formattedText = new FormattedText(
                Text,
                CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight,
                new Typeface(
                    FontFamily,
                    FontStyle,
                    FontWeight,
                    FontStretches.Normal),
                FontSize,
                Brushes.Black
                );

            _textGeometry = _formattedText.BuildGeometry(new Point(0, 0));
        }
    }
}