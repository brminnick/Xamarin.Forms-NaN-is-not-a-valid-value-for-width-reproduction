# Xamarin.Forms-NaN-is-not-a-valid-value-for-width-reproduction
A Xamarin.Forms solution to reproduce System.ArgumentException: NaN is not a valid value for width

Reproduction Code Snippet to induce `System.ArgumentException: NaN is not a valid value for width`

##Reproduction Steps
 1. Download and open `NaNReproduction.sln` in Xamarin Studio
 2. In the Solution Explorer, Right-click on `NaNReproduction.iOS` and select `Set As Startup Project`
 3. Build, Deploy, and Run the app on an iOS 10.2 Simulator

##Error
>System.ArgumentException: NaN is not a valid value for width

>Xamarin.Forms.Size.Size(double width, double height) Xamarin.Forms.VisualElement.GetSizeRequest(double widthConstraint, double heightConstraint) Xamarin.Forms.VisualElement.Measure(double widthConstraint, double heightConstraint, MeasureFlags flags) Xamarin.Forms.StackLayout.CompressHorizontalLayout(StackLayout.LayoutInformation layout, double widthConstraint, double heightConstraint) Xamarin.Forms.StackLayout.CompressNaiveLayout(StackLayout.LayoutInformation layout, StackOrientation orientation, double widthConstraint, double heightConstraint) Xamarin.Forms.StackLayout.CalculateLayout(StackLayout.LayoutInformation layout, double x, double y, double widthConstraint, double heightConstraint, bool processExpanders) Xamarin.Forms.StackLayout.LayoutChildren(double x, double y, double width, double height) Xamarin.Forms.Layout.UpdateChildrenLayout() Xamarin.Forms.Layout.OnSizeAllocated(double width, double height) Xamarin.Forms.VisualElement.SizeAllocated(double width, double height) Xamarin.Forms.VisualElement.SetSize(double width, double height) Xamarin.Forms.VisualElement.set_Bounds(Rectangle value) Xamarin.Forms.VisualElement.Layout(Rectangle bounds) Xamarin.Forms.Layout.LayoutChildIntoBoundingRegion(VisualElement child, Rectangle region) Xamarin.Forms.Grid.LayoutChildren(double x, double y, double width, double height) Xamarin.Forms.Layout.UpdateChildrenLayout() Xamarin.Forms.Layout.OnSizeAllocated(double width, double height) Xamarin.Forms.VisualElement.SizeAllocated(double width, double height) Xamarin.Forms.VisualElement.SetSize(double width, double height) Xamarin.Forms.VisualElement.set_Bounds(Rectangle value) Xamarin.Forms.VisualElement.Layout(Rectangle bounds) Xamarin.Forms.Layout.LayoutChildIntoBoundingRegion(VisualElement child, Rectangle region) Xamarin.Forms.Platform.iOS.ViewCellRenderer.ViewTableCell.LayoutSubviews() Xamarin.Forms.Platform.iOS.CellTableViewCell.GetNativeCell(UITableView tableView, Cell cell, bool recycleCells, string templateId) Xamarin.Forms.Platform.iOS.ListViewRenderer.ListViewDataSource.GetCell(UITableView tableView, NSIndexPath indexPath) UIKit.UIApplication.UIApplicationMain(int, string[], intptr, intptr)(wrapper managed-to-native) UIKit.UIApplication.Main(string[] args, IntPtr principal, IntPtr delegate)UIApplication.cs:79 UIKit.UIApplication.Main(string[] args, string principalClassName, string delegateClassName)UIApplication.cs:63 MondayPundayApp.iOS.Application.Main(string[] args)Main.cs:17

##Environment

=== Xamarin Studio Enterprise ===

Version 6.1.5 (build 0)
Installation UUID: 3ac98a61-67a7-411f-b124-19833ec9a519
Runtime:
 - Mono 4.6.2 (mono-4.6.0-branch/ac9e222) (64-bit)
 - GTK+ 2.24.23 (Raleigh theme)
 - Package version: 406020016

=== NuGet ===

Version: 3.4.3.0

=== Xamarin.Profiler ===

Version: 1.0.1
Location: /Applications/Xamarin Profiler.app/Contents/MacOS/Xamarin Profiler

=== Apple Developer Tools ===

Xcode 8.2.1 (11766.1)
Build 8C1002

=== Xamarin.iOS ===

Version: 10.3.1.8 (Xamarin Enterprise)
Hash: 7beaef4
Branch: cycle8-xi
Build date: 2016-12-20 02:58:14-0500

=== Xamarin.Android ===

Version: 7.0.2.42 (Xamarin Enterprise)
Android SDK: /Users/brandonm/Library/Developer/Xamarin/android-sdk-macosx
	Supported Android versions:
		4.1 (API level 16)
		6.0 (API level 23)
		7.0 (API level 24)

SDK Tools Version: 25.2.3
SDK Platform Tools Version: 25.0.1
SDK Build Tools Version: 24.0.3

Java SDK: /usr
java version "1.8.0_102"
Java(TM) SE Runtime Environment (build 1.8.0_102-b14)
Java HotSpot(TM) 64-Bit Server VM (build 25.102-b14, mixed mode)

Android Designer EPL code available here:
https://github.com/xamarin/AndroidDesigner.EPL

=== Xamarin Android Player ===

Version: 0.6.5
Location: /Applications/Xamarin Android Player.app

=== Xamarin.Mac ===

Version: 2.10.0.120 (Xamarin Enterprise)

=== Xamarin Inspector ===

Version: 1.0.0.0
Hash: 1f3067d
Branch: master
Build date: 11/15/2016 1:13:59 PM

=== Build Information ===

Release ID: 601050000
Git revision: 7494718e127af9eaec45a3bd6282d3da927488bd
Build date: 2017-01-17 10:31:01-05
Xamarin addins: c92d0626d347aaa02839689eaac2961d24c9f446
Build lane: monodevelop-lion-cycle8

=== Operating System ===

Mac OS X 10.12.3
Darwin https://brandonm-mac.guest.corp.microsoft.com:80/?WT.mc_id=mobile-0000-bramin 16.4.0 Darwin Kernel Version 16.4.0
    Thu Dec 22 22:53:21 PST 2016
    root:xnu-3789.41.3~3/RELEASE_X86_64 x86_64

=== Enabled user installed addins ===

Xamarin Inspector 1.0.0.0

