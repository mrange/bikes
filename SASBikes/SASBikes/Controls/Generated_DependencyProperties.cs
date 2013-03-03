// ----------------------------------------------------------------------------------------------
// Copyright (c) Mårten Rånge.
// ----------------------------------------------------------------------------------------------
// This source code is subject to terms and conditions of the Microsoft Public License. A 
// copy of the license can be found in the License.html file at the root of this distribution. 
// If you cannot locate the  Microsoft Public License, please send an email to 
// dlr@microsoft.com. By using this source code in any fashion, you are agreeing to be bound 
//  by the terms of the Microsoft Public License.
// ----------------------------------------------------------------------------------------------
// You must not remove this notice, or any other, from this software.
// ----------------------------------------------------------------------------------------------

using Bing.Maps;
using SASBikes.Common.DataModel;
using Windows.UI.Xaml.Media;

// ############################################################################
// #                                                                          #
// #        ---==>  T H I S  F I L E  I S   G E N E R A T E D  <==---         #
// #                                                                          #
// # This means that any edits to the .cs file will be lost when its          #
// # regenerated. Changes should instead be applied to the corresponding      #
// # template file (.tt)                                                      #
// ############################################################################





// ReSharper disable CompareOfFloatsByEqualityOperator
// ReSharper disable InconsistentNaming
// ReSharper disable InvocationIsSkipped
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantAssignment
// ReSharper disable RedundantUsingDirective

namespace SASBikes.Controls
{
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;

    using System.Windows;

    using Windows.UI.Xaml;

    // ------------------------------------------------------------------------
    // StationPushPin
    // ------------------------------------------------------------------------
    partial class StationPushPin
    {
        #region Uninteresting generated code
        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register (
            "IsOpen",
            typeof (bool),
            typeof (StationPushPin),
            new PropertyMetadata (
                default (bool),
                Changed_IsOpen
            ));

        static void Changed_IsOpen (DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var instance = dependencyObject as StationPushPin;
            if (instance != null)
            {
                var oldValue = (bool)eventArgs.OldValue;
                var newValue = (bool)eventArgs.NewValue;

                instance.Changed_IsOpen (oldValue, newValue);
            }
        }

        public static readonly DependencyProperty StationNameProperty = DependencyProperty.Register (
            "StationName",
            typeof (string),
            typeof (StationPushPin),
            new PropertyMetadata (
                default (string),
                Changed_StationName
            ));

        static void Changed_StationName (DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var instance = dependencyObject as StationPushPin;
            if (instance != null)
            {
                var oldValue = (string)eventArgs.OldValue;
                var newValue = (string)eventArgs.NewValue;

                instance.Changed_StationName (oldValue, newValue);
            }
        }

        public static readonly DependencyProperty LocationProperty = DependencyProperty.Register (
            "Location",
            typeof (Location),
            typeof (StationPushPin),
            new PropertyMetadata (
                default (Location),
                Changed_Location
            ));

        static void Changed_Location (DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var instance = dependencyObject as StationPushPin;
            if (instance != null)
            {
                var oldValue = (Location)eventArgs.OldValue;
                var newValue = (Location)eventArgs.NewValue;

                instance.Changed_Location (oldValue, newValue);
            }
        }

        public static readonly DependencyProperty StationBackgroundProperty = DependencyProperty.Register (
            "StationBackground",
            typeof (Brush),
            typeof (StationPushPin),
            new PropertyMetadata (
                default (Brush),
                Changed_StationBackground
            ));

        static void Changed_StationBackground (DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var instance = dependencyObject as StationPushPin;
            if (instance != null)
            {
                var oldValue = (Brush)eventArgs.OldValue;
                var newValue = (Brush)eventArgs.NewValue;

                instance.Changed_StationBackground (oldValue, newValue);
            }
        }

        #endregion

        // --------------------------------------------------------------------
        // Constructor
        // --------------------------------------------------------------------
        public StationPushPin ()
        {
            Constructed__StationPushPin ();
        }
        // --------------------------------------------------------------------
        partial void Constructed__StationPushPin ();
        // --------------------------------------------------------------------


        // --------------------------------------------------------------------
        // Properties
        // --------------------------------------------------------------------

           
        // --------------------------------------------------------------------
        public bool IsOpen
        {
            get
            {
                return (bool)GetValue (IsOpenProperty);
            }
            set
            {
                if (IsOpen != value)
                {
                    SetValue (IsOpenProperty, value);
                }
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_IsOpen (bool oldValue, bool newValue);
        // --------------------------------------------------------------------


           
        // --------------------------------------------------------------------
        public string StationName
        {
            get
            {
                return (string)GetValue (StationNameProperty);
            }
            set
            {
                if (StationName != value)
                {
                    SetValue (StationNameProperty, value);
                }
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_StationName (string oldValue, string newValue);
        // --------------------------------------------------------------------


           
        // --------------------------------------------------------------------
        public Location Location
        {
            get
            {
                return (Location)GetValue (LocationProperty);
            }
            set
            {
                if (Location != value)
                {
                    SetValue (LocationProperty, value);
                }
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_Location (Location oldValue, Location newValue);
        // --------------------------------------------------------------------


           
        // --------------------------------------------------------------------
        public Brush StationBackground
        {
            get
            {
                return (Brush)GetValue (StationBackgroundProperty);
            }
            private set
            {
                if (StationBackground != value)
                {
                    SetValue (StationBackgroundProperty, value);
                }
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_StationBackground (Brush oldValue, Brush newValue);
        // --------------------------------------------------------------------


    }
    // ------------------------------------------------------------------------

    // ------------------------------------------------------------------------
    // BikeMap
    // ------------------------------------------------------------------------
    partial class BikeMap
    {
        #region Uninteresting generated code
        public static readonly DependencyProperty My_LoProperty = DependencyProperty.Register (
            "My_Lo",
            typeof (double),
            typeof (BikeMap),
            new PropertyMetadata (
                11.973429,
                Changed_My_Lo
            ));

        static void Changed_My_Lo (DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var instance = dependencyObject as BikeMap;
            if (instance != null)
            {
                var oldValue = (double)eventArgs.OldValue;
                var newValue = (double)eventArgs.NewValue;

                instance.Changed_My_Lo (oldValue, newValue);
            }
        }

        public static readonly DependencyProperty My_LaProperty = DependencyProperty.Register (
            "My_La",
            typeof (double),
            typeof (BikeMap),
            new PropertyMetadata (
                57.700324,
                Changed_My_La
            ));

        static void Changed_My_La (DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var instance = dependencyObject as BikeMap;
            if (instance != null)
            {
                var oldValue = (double)eventArgs.OldValue;
                var newValue = (double)eventArgs.NewValue;

                instance.Changed_My_La (oldValue, newValue);
            }
        }

        public static readonly DependencyProperty View_LoProperty = DependencyProperty.Register (
            "View_Lo",
            typeof (double),
            typeof (BikeMap),
            new PropertyMetadata (
                11.973429,
                Changed_View_Lo
            ));

        static void Changed_View_Lo (DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var instance = dependencyObject as BikeMap;
            if (instance != null)
            {
                var oldValue = (double)eventArgs.OldValue;
                var newValue = (double)eventArgs.NewValue;

                instance.Changed_View_Lo (oldValue, newValue);
            }
        }

        public static readonly DependencyProperty View_LaProperty = DependencyProperty.Register (
            "View_La",
            typeof (double),
            typeof (BikeMap),
            new PropertyMetadata (
                57.700324,
                Changed_View_La
            ));

        static void Changed_View_La (DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var instance = dependencyObject as BikeMap;
            if (instance != null)
            {
                var oldValue = (double)eventArgs.OldValue;
                var newValue = (double)eventArgs.NewValue;

                instance.Changed_View_La (oldValue, newValue);
            }
        }

        public static readonly DependencyProperty View_ZoomLevelProperty = DependencyProperty.Register (
            "View_ZoomLevel",
            typeof (double),
            typeof (BikeMap),
            new PropertyMetadata (
                18.0,
                Changed_View_ZoomLevel
            ));

        static void Changed_View_ZoomLevel (DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var instance = dependencyObject as BikeMap;
            if (instance != null)
            {
                var oldValue = (double)eventArgs.OldValue;
                var newValue = (double)eventArgs.NewValue;

                instance.Changed_View_ZoomLevel (oldValue, newValue);
            }
        }

        public static readonly DependencyProperty StationsProperty = DependencyProperty.Register (
            "Stations",
            typeof (StationList),
            typeof (BikeMap),
            new PropertyMetadata (
                default (StationList),
                Changed_Stations
            ));

        static void Changed_Stations (DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var instance = dependencyObject as BikeMap;
            if (instance != null)
            {
                var oldValue = (StationList)eventArgs.OldValue;
                var newValue = (StationList)eventArgs.NewValue;

                instance.Changed_Stations (oldValue, newValue);
            }
        }

        #endregion

        // --------------------------------------------------------------------
        // Constructor
        // --------------------------------------------------------------------
        public BikeMap ()
        {
            Constructed__BikeMap ();
        }
        // --------------------------------------------------------------------
        partial void Constructed__BikeMap ();
        // --------------------------------------------------------------------


        // --------------------------------------------------------------------
        // Properties
        // --------------------------------------------------------------------

           
        // --------------------------------------------------------------------
        public double My_Lo
        {
            get
            {
                return (double)GetValue (My_LoProperty);
            }
            set
            {
                if (My_Lo != value)
                {
                    SetValue (My_LoProperty, value);
                }
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_My_Lo (double oldValue, double newValue);
        // --------------------------------------------------------------------


           
        // --------------------------------------------------------------------
        public double My_La
        {
            get
            {
                return (double)GetValue (My_LaProperty);
            }
            set
            {
                if (My_La != value)
                {
                    SetValue (My_LaProperty, value);
                }
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_My_La (double oldValue, double newValue);
        // --------------------------------------------------------------------


           
        // --------------------------------------------------------------------
        public double View_Lo
        {
            get
            {
                return (double)GetValue (View_LoProperty);
            }
            set
            {
                if (View_Lo != value)
                {
                    SetValue (View_LoProperty, value);
                }
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_View_Lo (double oldValue, double newValue);
        // --------------------------------------------------------------------


           
        // --------------------------------------------------------------------
        public double View_La
        {
            get
            {
                return (double)GetValue (View_LaProperty);
            }
            set
            {
                if (View_La != value)
                {
                    SetValue (View_LaProperty, value);
                }
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_View_La (double oldValue, double newValue);
        // --------------------------------------------------------------------


           
        // --------------------------------------------------------------------
        public double View_ZoomLevel
        {
            get
            {
                return (double)GetValue (View_ZoomLevelProperty);
            }
            set
            {
                if (View_ZoomLevel != value)
                {
                    SetValue (View_ZoomLevelProperty, value);
                }
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_View_ZoomLevel (double oldValue, double newValue);
        // --------------------------------------------------------------------


           
        // --------------------------------------------------------------------
        public StationList Stations
        {
            get
            {
                return (StationList)GetValue (StationsProperty);
            }
            set
            {
                if (Stations != value)
                {
                    SetValue (StationsProperty, value);
                }
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_Stations (StationList oldValue, StationList newValue);
        // --------------------------------------------------------------------


    }
    // ------------------------------------------------------------------------

    // ------------------------------------------------------------------------
    // ErrorIndicator
    // ------------------------------------------------------------------------
    partial class ErrorIndicator
    {
        #region Uninteresting generated code
        public static readonly DependencyProperty ErrorsProperty = DependencyProperty.Register (
            "Errors",
            typeof (ErrorList),
            typeof (ErrorIndicator),
            new PropertyMetadata (
                default (ErrorList),
                Changed_Errors
            ));

        static void Changed_Errors (DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var instance = dependencyObject as ErrorIndicator;
            if (instance != null)
            {
                var oldValue = (ErrorList)eventArgs.OldValue;
                var newValue = (ErrorList)eventArgs.NewValue;

                instance.Changed_Errors (oldValue, newValue);
            }
        }

        public static readonly DependencyProperty ErrorCountProperty = DependencyProperty.Register (
            "ErrorCount",
            typeof (int),
            typeof (ErrorIndicator),
            new PropertyMetadata (
                default (int),
                Changed_ErrorCount
            ));

        static void Changed_ErrorCount (DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var instance = dependencyObject as ErrorIndicator;
            if (instance != null)
            {
                var oldValue = (int)eventArgs.OldValue;
                var newValue = (int)eventArgs.NewValue;

                instance.Changed_ErrorCount (oldValue, newValue);
            }
        }

        public static readonly DependencyProperty ErrorTextProperty = DependencyProperty.Register (
            "ErrorText",
            typeof (string),
            typeof (ErrorIndicator),
            new PropertyMetadata (
                default (string),
                Changed_ErrorText
            ));

        static void Changed_ErrorText (DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var instance = dependencyObject as ErrorIndicator;
            if (instance != null)
            {
                var oldValue = (string)eventArgs.OldValue;
                var newValue = (string)eventArgs.NewValue;

                instance.Changed_ErrorText (oldValue, newValue);
            }
        }

        public static readonly DependencyProperty ErrorTextVisibilityProperty = DependencyProperty.Register (
            "ErrorTextVisibility",
            typeof (Visibility),
            typeof (ErrorIndicator),
            new PropertyMetadata (
                default (Visibility),
                Changed_ErrorTextVisibility
            ));

        static void Changed_ErrorTextVisibility (DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var instance = dependencyObject as ErrorIndicator;
            if (instance != null)
            {
                var oldValue = (Visibility)eventArgs.OldValue;
                var newValue = (Visibility)eventArgs.NewValue;

                instance.Changed_ErrorTextVisibility (oldValue, newValue);
            }
        }

        #endregion

        // --------------------------------------------------------------------
        // Constructor
        // --------------------------------------------------------------------
        public ErrorIndicator ()
        {
            Constructed__ErrorIndicator ();
        }
        // --------------------------------------------------------------------
        partial void Constructed__ErrorIndicator ();
        // --------------------------------------------------------------------


        // --------------------------------------------------------------------
        // Properties
        // --------------------------------------------------------------------

           
        // --------------------------------------------------------------------
        public ErrorList Errors
        {
            get
            {
                return (ErrorList)GetValue (ErrorsProperty);
            }
            set
            {
                if (Errors != value)
                {
                    SetValue (ErrorsProperty, value);
                }
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_Errors (ErrorList oldValue, ErrorList newValue);
        // --------------------------------------------------------------------


           
        // --------------------------------------------------------------------
        public int ErrorCount
        {
            get
            {
                return (int)GetValue (ErrorCountProperty);
            }
            set
            {
                if (ErrorCount != value)
                {
                    SetValue (ErrorCountProperty, value);
                }
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_ErrorCount (int oldValue, int newValue);
        // --------------------------------------------------------------------


           
        // --------------------------------------------------------------------
        public string ErrorText
        {
            get
            {
                return (string)GetValue (ErrorTextProperty);
            }
            private set
            {
                if (ErrorText != value)
                {
                    SetValue (ErrorTextProperty, value);
                }
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_ErrorText (string oldValue, string newValue);
        // --------------------------------------------------------------------


           
        // --------------------------------------------------------------------
        public Visibility ErrorTextVisibility
        {
            get
            {
                return (Visibility)GetValue (ErrorTextVisibilityProperty);
            }
            private set
            {
                if (ErrorTextVisibility != value)
                {
                    SetValue (ErrorTextVisibilityProperty, value);
                }
            }
        }
        // --------------------------------------------------------------------
        partial void Changed_ErrorTextVisibility (Visibility oldValue, Visibility newValue);
        // --------------------------------------------------------------------


    }
    // ------------------------------------------------------------------------

}

