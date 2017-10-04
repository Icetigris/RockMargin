// OptionsPage.cs - Get/set values for each setting in RockMargin's options.

using System.ComponentModel;
using System.Drawing;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text.Editor;

namespace RockMargin
{
	public class OptionsPage : DialogPage
	{
		const string GeneralCategoryName = "General";
		const string ScrollCategoryName = "Scroll coloring";
		const string HighlightsCategoryName = "Text markers";
		const string ChangesCategoryName = "Change margin";

		public static IEditorOptionsFactoryService OptionsService;
		public static IVsSettingsManager SettingsManager;
//General
		[Category(GeneralCategoryName)]
		[DisplayName("Scroll Bar Width")]
        [Description("Width of the scroll bar in pixels.")]
        public uint Width
		{
			get { return GetOption(OptionsKeys.Width); }
			set { SetOption(OptionsKeys.Width, value); }
		}

		[Category(GeneralCategoryName)]
		[DisplayName("Enhanced text rendering")]
		[Description("Higher-contrast text rendering in the scroll bar.")]
		public bool EnhancedTextRendering
		{
			get { return GetOption(OptionsKeys.EnhancedTextRendering); }
			set { SetOption(OptionsKeys.EnhancedTextRendering, value); }
		}
//Scroll Coloring
		[Category(ScrollCategoryName)]
		[DisplayName("Scroll margin")]
		[Description("Border color of the scroll bar.")]
		public Color TrackMarginColor
		{
			get { return FromArgb(GetOption(OptionsKeys.MarginColor)); }
			set { SetOption(OptionsKeys.MarginColor, ToArgb(value)); }
		}

		[Category(ScrollCategoryName)]
		[DisplayName("Scroll thumb")]
		[Description("The little box that follows the chunk of the file that is visible. It will move with you as you scroll.")]
		public Color TrackThumbColor
		{
			get { return FromArgb(GetOption(OptionsKeys.ThumbColor)); }
			set { SetOption(OptionsKeys.ThumbColor, ToArgb(value)); }
		}

		[Category(ScrollCategoryName)]
		[DisplayName("Text markers")]
		[Description("Double-clicked highlighted words will appear on the scroll bar in this color.")]
		public Color TrackHighlightColor
		{
			get { return FromArgb(GetOption(OptionsKeys.HighlightColor)); }
			set { SetOption(OptionsKeys.HighlightColor, ToArgb(value)); }
		}

		[Category(ScrollCategoryName)]
		[DisplayName("Comments")]
		[Description("Commented code will appear on the scroll bar in this color.")]
		public Color TrackCommentsColor
		{
			get { return FromArgb(GetOption(OptionsKeys.CommentsColor)); }
			set { SetOption(OptionsKeys.CommentsColor, ToArgb(value)); }
		}

		[Category(ScrollCategoryName)]
		[DisplayName("Text")]
		[Description("Default color for blocks of text in the scroll bar.")]
		public Color TrackTextColor
		{
			get { return FromArgb(GetOption(OptionsKeys.TextColor)); }
			set { SetOption(OptionsKeys.TextColor, ToArgb(value)); }
		}

		[Category(ScrollCategoryName)]
		[DisplayName("Background")]
		[Description("Scroll bar background color.")]
		public Color TrackBackgroundColor
		{
			get { return FromArgb(GetOption(OptionsKeys.BackgroundColor)); }
			set { SetOption(OptionsKeys.BackgroundColor, ToArgb(value)); }
		}
//Text markers
		[Category(HighlightsCategoryName)]
		[DisplayName("Alt + Double-click/Sticky Bar Markers")]
		[Description("If enabled, double-clicking a word only highlights it locally.\n Alt + double-clicked words will appear marked in the sidebar until another word is selected this way.")]
		public bool AltHighlights
		{
			get { return GetOption(OptionsKeys.AltHighlights); }
			set { SetOption(OptionsKeys.AltHighlights, value); }
		}

		[Category(HighlightsCategoryName)]
		[DisplayName("Enabled")]
		[Description("Toggle selected word markers in the scroll bar.")]
		public bool HighlightsEnabled
		{
			get { return GetOption(OptionsKeys.HighlightsEnabled); }
			set { SetOption(OptionsKeys.HighlightsEnabled, value); }
		}

		[Category(HighlightsCategoryName)]
		[DisplayName("Marker background color")]
		[Description("When you double-click a word, this will be the color behind it and all its matches in a file.")]
		public Color HighlightBackgroundColor
		{
			get { return FromArgb(GetOption(OptionsKeys.TextMarkerBackgroundColor)); }
			set { SetOption(OptionsKeys.TextMarkerBackgroundColor, ToArgb(value)); }
		}

		[Category(HighlightsCategoryName)]
		[DisplayName("Marker foreground color")]
		[Description("When you double-click a word, this will be the color on top of it and all its matches in a file.")]
		public Color HighlightForegroundColor
		{
			get { return FromArgb(GetOption(OptionsKeys.TextMarkerForegroundColor)); }
			set { SetOption(OptionsKeys.TextMarkerForegroundColor, ToArgb(value)); }
		}
//Change margin
		[Category(ChangesCategoryName)]
		[DisplayName("Enabled")]
		[Description("Toggle markers to show changed lines of text in the scroll bar.")]
		public bool ChangeMarginEnabled
		{
			get { return GetOption(OptionsKeys.ChangeMarginEnabled); }
			set { SetOption(OptionsKeys.ChangeMarginEnabled, value); }
		}

		[Category(ChangesCategoryName)]
		[DisplayName("Saved change color")]
		[Description("Lines you have changed will be marked on the scroll bar in this color after you save those changes.")]
		public Color SavedChangeColor
		{
			get { return FromArgb(GetOption(OptionsKeys.SavedChangeColor)); }
			set { SetOption(OptionsKeys.SavedChangeColor, ToArgb(value)); }
		}

		[Category(ChangesCategoryName)]
		[DisplayName("Unsaved change color")]
		[Description("Lines you have changed will be marked on the scroll bar in this color before you save those changes.")]
		public Color UnsavedChangeColor
		{
			get { return FromArgb(GetOption(OptionsKeys.UnsavedChangeColor)); }
			set { SetOption(OptionsKeys.UnsavedChangeColor, ToArgb(value)); }
		}


		private Color FromArgb(uint argb)
		{
			return Color.FromArgb((int)argb);
		}

		private uint ToArgb(Color color)
		{
			return (uint)(color.A << 24 | color.R << 16 | color.G << 8 | color.B);
		}

		private void SetOption<T>(EditorOptionKey<T> key, T value)
		{
			OptionsService.GlobalOptions.SetOptionValue(key, value);
		}

		private T GetOption<T>(EditorOptionKey<T> key)
		{
			return OptionsService.GlobalOptions.GetOptionValue(key);
		}

		public override void LoadSettingsFromStorage()
		{
			var s = new SettingsStore(SettingsManager, OptionsService.GlobalOptions);
			s.Load();
		}

		public override void SaveSettingsToStorage()
		{
			var s = new SettingsStore(SettingsManager, OptionsService.GlobalOptions);
			s.Save();
		}
	}
}
