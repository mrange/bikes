// ReSharper disable InconsistentNaming

using System.Xml.Linq;
using SASBikes.Common;
using SASBikes.Data;

using System;
using System.Collections.Generic;
using SASBikes.DataModel;
using SASBikes.Source.Extensions;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Split Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234234

namespace SASBikes
{
    public sealed partial class SplitPage
    {
        const string SampleData = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<carto>
  <markers>
    <marker name=""LILLA BOMMEN"" number=""1"" address=""001_LILLA BOMMEN"" fullAddress=""001_LILLA BOMMEN  "" lat=""57.711547"" lng=""11.966574"" open=""0"" bonus=""0""/>
    <marker name=""LILLATORGET"" number=""2"" address=""002_LILLA TORGET"" fullAddress=""002_LILLA TORGET  "" lat=""57.705529"" lng=""11.962491"" open=""0"" bonus=""0""/>
    <marker name=""POSTGATAN"" number=""3"" address=""003_POSTGATAN"" fullAddress=""003_POSTGATAN  "" lat=""57.707476"" lng=""11.963926"" open=""0"" bonus=""0""/>
    <marker name=""STORA BADHUSGATAN"" number=""4"" address=""004_STORA BADHUSGATAN"" fullAddress=""004_STORA BADHUSGATAN  "" lat=""57.704819"" lng=""11.957907"" open=""0"" bonus=""0""/>
    <marker name=""VASAGATAN/HEDEN"" number=""5"" address=""005_VASAGATAN/HEDEN"" fullAddress=""005_VASAGATAN/HEDEN  "" lat=""57.701419"" lng=""11.9766"" open=""0"" bonus=""0""/>
    <marker name=""SLUSSPLATSEN"" number=""6"" address=""006_SLUSSPLATSEN"" fullAddress=""006_SLUSSPLATSEN  "" lat=""57.707316"" lng=""11.973448"" open=""0"" bonus=""0""/>
    <marker name=""ESPERANTOPLATSEN"" number=""8"" address=""008_ESPERANTOPLATSEN"" fullAddress=""008_ESPERANTOPLATSEN  "" lat=""57.702793"" lng=""11.955461"" open=""0"" bonus=""0""/>
    <marker name=""KANALTORGET"" number=""9"" address=""009_KANALTORGET"" fullAddress=""009_KANALTORGET  "" lat=""57.710396"" lng=""11.966421"" open=""0"" bonus=""0""/>
    <marker name=""VALAND"" number=""10"" address=""010_VALAND"" fullAddress=""010_VALAND  "" lat=""57.700324"" lng=""11.973429"" open=""0"" bonus=""0""/>
    <marker name=""STORAN"" number=""11"" address=""011_STORAN"" fullAddress=""011_STORAN  "" lat=""57.702482"" lng=""11.971552"" open=""0"" bonus=""0""/>
    <marker name=""GRÖNSAKSTORGET"" number=""12"" address=""012_GRÖNSAKSTORGET"" fullAddress=""012_GRÖNSAKSTORGET  "" lat=""57.702459"" lng=""11.965179"" open=""0"" bonus=""0""/>
    <marker name=""GUSTAF ADOLFS TORG"" number=""13"" address=""013_GUSTAF ADOLFS TORG"" fullAddress=""013_GUSTAF ADOLFS TORG  "" lat=""57.707233"" lng=""11.967502"" open=""0"" bonus=""0""/>
    <marker name=""KUNGSTORGET"" number=""17"" address=""017_KUNGSTORGET"" fullAddress=""017_KUNGSTORGET  "" lat=""57.702542"" lng=""11.968875"" open=""0"" bonus=""0""/>
    <marker name=""BASTIONSPLATSEN"" number=""21"" address=""021_BASTIONSPLATSEN"" fullAddress=""021_BASTIONSPLATSEN  "" lat=""57.704263"" lng=""11.972077"" open=""0"" bonus=""0""/>
    <marker name=""KRISTINELUNDSGATAN"" number=""14"" address=""014_KRISTINELUNDSGATAN"" fullAddress=""014_KRISTINELUNDSGATAN  "" lat=""57.699939"" lng=""11.976176"" open=""0"" bonus=""0""/>
    <marker name=""HEDEN/STENSTUREGATAN"" number=""15"" address=""015_HEDEN/STENSTUREGATAN"" fullAddress=""015_HEDEN/STENSTUREGATAN  "" lat=""57.702955"" lng=""11.98021"" open=""0"" bonus=""0""/>
    <marker name=""BOHUSGATAN"" number=""16"" address=""016_BOHUSGATAN"" fullAddress=""016_BOHUSGATAN  "" lat=""57.70452"" lng=""11.984298"" open=""0"" bonus=""0""/>
    <marker name=""ROSENLUNDSPLATSEN"" number=""18"" address=""018_ROSENLUNDSPLATSEN"" fullAddress=""018_ROSENLUNDSPLATSEN  "" lat=""57.700685"" lng=""11.960571"" open=""0"" bonus=""0""/>
    <marker name=""OLOF PALMES PLATS"" number=""19"" address=""019_OLOF PALMES PLATS"" fullAddress=""019_OLOF PALMES PLATS  "" lat=""57.700662"" lng=""11.95364"" open=""0"" bonus=""0""/>
    <marker name=""NILS-ERICSSON TERMINALEN"" number=""20"" address=""020_NILS-ERICSSON TERMINALEN"" fullAddress=""020_NILS-ERICSSON TERMINALEN  "" lat=""57.709752"" lng=""11.970787"" open=""0"" bonus=""0""/>
    <marker name=""DÄMMEPLATSEN"" number=""22"" address=""022_Dämmeplatsen"" fullAddress=""022_Dämmeplatsen  "" lat=""57.707576"" lng=""11.989893"" open=""0"" bonus=""0""/>
    <marker name=""SCANDINAVIUM"" number=""25"" address=""025_Scandinavium"" fullAddress=""025_Scandinavium  "" lat=""57.700094"" lng=""11.987908"" open=""0"" bonus=""0""/>
    <marker name=""LISEBERG STATION"" number=""27"" address=""027_Liseberg station"" fullAddress=""027_Liseberg station  "" lat=""57.697923"" lng=""11.995107"" open=""0"" bonus=""0""/>
    <marker name=""GÅRDATORGET"" number=""24"" address=""024_Gårdatorget"" fullAddress=""024_Gårdatorget  "" lat=""57.704675"" lng=""11.992441"" open=""0"" bonus=""0""/>
    <marker name=""KORSVÄGEN /SÖDRA VÄGEN"" number=""28"" address=""028_Korsvägen 1"" fullAddress=""028_Korsvägen 1  "" lat=""57.696916"" lng=""11.985703"" open=""0"" bonus=""0""/>
    <marker name=""KORSVÄGEN"" number=""29"" address=""029_Korsvägen 2"" fullAddress=""029_Korsvägen 2  "" lat=""57.696555"" lng=""11.987833"" open=""0"" bonus=""0""/>
    <marker name=""SKÅNEGATAN"" number=""23"" address=""023_Skånegatan"" fullAddress=""023_Skånegatan  "" lat=""57.706096"" lng=""11.984646"" open=""0"" bonus=""0""/>
    <marker name=""BERGAKUNGEN"" number=""30"" address=""030_Bergakungen"" fullAddress=""030_Bergakungen  "" lat=""57.702135"" lng=""11.985035"" open=""0"" bonus=""0""/>
    <marker name=""HEDEN SYD"" number=""26"" address=""026_Heden syd"" fullAddress=""026_Heden syd  "" lat=""57.699941"" lng=""11.979867"" open=""0"" bonus=""0""/>
    <marker name=""VASAGATAN/SCHILLERSKA"" number=""38"" address=""038_Vasagatan/Schillerska"" fullAddress=""038_Vasagatan/Schillerska  "" lat=""57.698265"" lng=""11.967115"" open=""0"" bonus=""0""/>
    <marker name=""PACKHUSPLATSEN"" number=""43"" address=""043_PACKHUSPLATSEN"" fullAddress=""043_PACKHUSPLATSEN  "" lat=""57.706767"" lng=""11.959101"" open=""0"" bonus=""0""/>
    <marker name=""ALSTRÖMER/FRIGGAGATAN"" number=""45"" address="""" fullAddress=""  "" lat=""57.71119"" lng=""11.98798"" open=""0"" bonus=""0""/>
    <marker name=""SKANSTORGET"" number=""35"" address=""035_Skanstorget"" fullAddress=""035_Skanstorget  "" lat=""57.695909"" lng=""11.958548"" open=""0"" bonus=""0""/>
    <marker name=""KASTELLGATAN"" number=""34"" address=""034_Kastellgatan"" fullAddress=""034_Kastellgatan  "" lat=""57.693029"" lng=""11.955692"" open=""0"" bonus=""0""/>
    <marker name=""BRUNNSGATAN"" number=""32"" address=""032_Brunnsgatan"" fullAddress=""032_Brunnsgatan  "" lat=""57.693517"" lng=""11.958559"" open=""0"" bonus=""0""/>
    <marker name=""MOLINSGATAN"" number=""37"" address=""037_Molinsgatan"" fullAddress=""037_Molinsgatan  "" lat=""57.69691"" lng=""11.97234"" open=""0"" bonus=""0""/>
    <marker name=""ÅVÄGEN"" number=""31"" address=""031_Åvägen"" fullAddress=""031_Åvägen  "" lat=""57.701249"" lng=""11.992191"" open=""0"" bonus=""0""/>
    <marker name=""FRIGÅNGSGATAN"" number=""39"" address=""039_Frigångsgatan"" fullAddress=""039_Frigångsgatan  "" lat=""57.697821"" lng=""11.952905"" open=""0"" bonus=""0""/>
    <marker name=""SLOTTSKOGEN/PLIKTA"" number=""41"" address=""041_Slottskogen/Plikta"" fullAddress=""041_Slottskogen/Plikta  "" lat=""57.689983"" lng=""11.947047"" open=""0"" bonus=""0""/>
    <marker name=""KAPELLPLATSEN"" number=""46"" address="""" fullAddress=""  "" lat=""57.69358"" lng=""11.97199"" open=""0"" bonus=""0""/>
    <marker name=""DROTTNINGTORGET"" number=""42"" address=""042_Drottningtorget"" fullAddress=""042_Drottningtorget  "" lat=""57.708074"" lng=""11.972737"" open=""0"" bonus=""0""/>
    <marker name=""BERZELIIGATAN"" number=""47"" address="""" fullAddress=""  "" lat=""57.69794"" lng=""11.98096"" open=""0"" bonus=""0""/>
    <marker name=""CARLANDERSPLATSEN"" number=""48"" address="""" fullAddress=""  "" lat=""57.69312"" lng=""11.98661"" open=""0"" bonus=""0""/>
    <marker name=""KAPONJÄRSGATAN"" number=""49"" address="""" fullAddress=""  "" lat=""57.69875"" lng=""11.95649"" open=""0"" bonus=""0""/>
    <marker name=""KUNGSGATAN/TELEKASERN"" number=""50"" address="""" fullAddress=""  "" lat=""57.70337"" lng=""11.96012"" open=""0"" bonus=""0""/>
    <marker name=""FÖRENINGSGATAN"" number=""33"" address=""033_Föreningsgatan"" fullAddress=""033_Föreningsgatan  "" lat=""57.694554"" lng=""11.961987"" open=""0"" bonus=""0""/>
    <marker name=""ENGELBREKTSGATAN"" number=""44"" address="""" fullAddress=""  "" lat=""57.69841"" lng=""11.97694"" open=""0"" bonus=""0""/>
    <marker name=""HAGAPARKEN"" number=""36"" address=""036_Hagaparken"" fullAddress=""036_Hagaparken  "" lat=""57.697984"" lng=""11.962405"" open=""0"" bonus=""0""/>
    <marker name=""LINNÉPLATSEN"" number=""40"" address=""040_Linnéplatsen"" fullAddress=""040_Linnéplatsen  "" lat=""57.690278"" lng=""11.95165"" open=""0"" bonus=""0""/>
    <marker name=""HAGABION"" number=""51"" address="""" fullAddress=""  "" lat=""57.696837"" lng=""11.951009"" open=""0"" bonus=""0""/>
    <marker name=""POSTHUSET/ÅKAREPLATSEN"" number=""7"" address="""" fullAddress=""  "" lat=""57.707647"" lng=""11.976117"" open=""0"" bonus=""0""/>
    <marker name=""GIBRALTARG/EKLANDAG"" number=""52"" address=""GIBRALTARGATAN/EKLANDAGATAN"" fullAddress=""GIBRALTARGATAN/EKLANDAGATAN  "" lat=""57.68542"" lng=""11.98322"" open=""1"" bonus=""0""/>
    <marker name=""MOLINSGATAN/LÄRAREGATAN"" number=""53"" address="""" fullAddress=""  "" lat=""0.0"" lng=""0.0"" open=""1"" bonus=""0""/>
    <marker name=""EKLANDA/UTLANDAGATAN"" number=""54"" address="""" fullAddress=""  "" lat=""0.0"" lng=""0.0"" open=""1"" bonus=""0""/>
  </markers>
  <arrondissements>
    <arrondissement number=""0"" minLat=""0.0"" minLng=""0.0"" maxLat=""57.711547"" maxLng=""11.995107""/>
  </arrondissements>
</carto>
";

        public SplitPage()
        {
            InitializeComponent();

            var context = new DataModelContext();

            var state = new State(context);

            var doc = XDocument.Parse(SampleData).Document;
            var markers = doc
                .Elements("carto")
                .Elements("markers")
                .Elements("marker")
                ;

            foreach (var marker in markers)
            {
                var station = new Station(context);

                station.Station_Name        = marker.GetAttributeValue("name"       , "NoName"  );
                station.Station_Number      = marker.GetAttributeValue("number"     , "0"       ).Parse(0);
                station.Station_Address     = marker.GetAttributeValue("address"    , "NoAddress");
                station.Station_FullAddress = marker.GetAttributeValue("fullAddress", "NoAddress");
                station.Station_La          = marker.GetAttributeValue("lat"        , "57.69").Parse(57.69M);
                station.Station_Lo          = marker.GetAttributeValue("lng"        , "11.95").Parse(11.95M);
                station.Station_IsOpen      = marker.GetAttributeValue("open"       , "0") == "1"; 
                station.Station_IsBonus     = marker.GetAttributeValue("bonus"      , "0") == "1";

                state.State_Stations.Add(station);
            }


        }

        #region Page state management

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            // TODO: Create an appropriate data model for your problem domain to replace the sample data
            var group = SampleDataSource.GetGroup((String)navigationParameter);
            DefaultViewModel["Group"] = group;
            DefaultViewModel["Items"] = group.Items;

            if (pageState == null)
            {
                itemListView.SelectedItem = null;
                // When this is a new page, select the first item automatically unless logical page
                // navigation is being used (see the logical page navigation #region below.)
                if (!UsingLogicalPageNavigation() && itemsViewSource.View != null)
                {
                    itemsViewSource.View.MoveCurrentToFirst();
                }
            }
            else
            {
                // Restore the previously saved state associated with this page
                if (pageState.ContainsKey("SelectedItem") && itemsViewSource.View != null)
                {
                    var selectedItem = SampleDataSource.GetItem((String)pageState["SelectedItem"]);
                    itemsViewSource.View.MoveCurrentTo(selectedItem);
                }
            }
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
            if (itemsViewSource.View != null)
            {
                var selectedItem = (SampleDataItem)itemsViewSource.View.CurrentItem;
                if (selectedItem != null) pageState["SelectedItem"] = selectedItem.UniqueId;
            }
        }

        #endregion

        #region Logical page navigation

        // Visual state management typically reflects the four application view states directly
        // (full screen landscape and portrait plus snapped and filled views.)  The split page is
        // designed so that the snapped and portrait view states each have two distinct sub-states:
        // either the item list or the details are displayed, but not both at the same time.
        //
        // This is all implemented with a single physical page that can represent two logical
        // pages.  The code below achieves this goal without making the user aware of the
        // distinction.

        /// <summary>
        /// Invoked to determine whether the page should act as one logical page or two.
        /// </summary>
        /// <param name="viewState">The view state for which the question is being posed, or null
        /// for the current view state.  This parameter is optional with null as the default
        /// value.</param>
        /// <returns>True when the view state in question is portrait or snapped, false
        /// otherwise.</returns>
        private bool UsingLogicalPageNavigation(ApplicationViewState? viewState = null)
        {
            if (viewState == null) viewState = ApplicationView.Value;
            return viewState == ApplicationViewState.FullScreenPortrait ||
                viewState == ApplicationViewState.Snapped;
        }

        /// <summary>
        /// Invoked when an item within the list is selected.
        /// </summary>
        /// <param name="sender">The GridView (or ListView when the application is Snapped)
        /// displaying the selected item.</param>
        /// <param name="e">Event data that describes how the selection was changed.</param>
        void ItemListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Invalidate the view state when logical page navigation is in effect, as a change
            // in selection may cause a corresponding change in the current logical page.  When
            // an item is selected this has the effect of changing from displaying the item list
            // to showing the selected item's details.  When the selection is cleared this has the
            // opposite effect.
            if (UsingLogicalPageNavigation()) InvalidateVisualState();
        }

        /// <summary>
        /// Invoked when the page's back button is pressed.
        /// </summary>
        /// <param name="sender">The back button instance.</param>
        /// <param name="e">Event data that describes how the back button was clicked.</param>
        protected override void GoBack(object sender, RoutedEventArgs e)
        {
            if (UsingLogicalPageNavigation() && itemListView.SelectedItem != null)
            {
                // When logical page navigation is in effect and there's a selected item that
                // item's details are currently displayed.  Clearing the selection will return
                // to the item list.  From the user's point of view this is a logical backward
                // navigation.
                itemListView.SelectedItem = null;
            }
            else
            {
                // When logical page navigation is not in effect, or when there is no selected
                // item, use the default back button behavior.
                base.GoBack(sender, e);
            }
        }

        /// <summary>
        /// Invoked to determine the name of the visual state that corresponds to an application
        /// view state.
        /// </summary>
        /// <param name="viewState">The view state for which the question is being posed.</param>
        /// <returns>The name of the desired visual state.  This is the same as the name of the
        /// view state except when there is a selected item in portrait and snapped views where
        /// this additional logical page is represented by adding a suffix of _Detail.</returns>
        protected override string DetermineVisualState(ApplicationViewState viewState)
        {
            // Update the back button's enabled state when the view state changes
            var logicalPageBack = UsingLogicalPageNavigation(viewState) && itemListView.SelectedItem != null;
            var physicalPageBack = Frame != null && Frame.CanGoBack;
            DefaultViewModel["CanGoBack"] = logicalPageBack || physicalPageBack;

            // Determine visual states for landscape layouts based not on the view state, but
            // on the width of the window.  This page has one layout that is appropriate for
            // 1366 virtual pixels or wider, and another for narrower displays or when a snapped
            // application reduces the horizontal space available to less than 1366.
            if (viewState == ApplicationViewState.Filled ||
                viewState == ApplicationViewState.FullScreenLandscape)
            {
                var windowWidth = Window.Current.Bounds.Width;
                if (windowWidth >= 1366) return "FullScreenLandscapeOrWide";
                return "FilledOrNarrow";
            }

            // When in portrait or snapped start with the default visual state name, then add a
            // suffix when viewing details instead of the list
            var defaultStateName = base.DetermineVisualState(viewState);
            return logicalPageBack ? defaultStateName + "_Detail" : defaultStateName;
        }

        #endregion
    }
}
