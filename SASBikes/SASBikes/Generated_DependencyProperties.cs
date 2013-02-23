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

using SASBikes.DataModel;

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

namespace SASBikes
{
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;

    using System.Windows;

    using Windows.UI.Xaml;

    // ------------------------------------------------------------------------
    // BindTo
    // ------------------------------------------------------------------------
    static partial class BindTo
    {
        #region Uninteresting generated code
        public static readonly DependencyProperty DataProperty = DependencyProperty.RegisterAttached (
            "Data",
            typeof (string),
            typeof (BindTo),
            new PropertyMetadata (
                default (string),
                Changed_Data
            ));

        static void Changed_Data (DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (dependencyObject != null)
            {
                var oldValue = (string)eventArgs.OldValue;
                var newValue = (string)eventArgs.NewValue;

                Changed_Data (dependencyObject, oldValue, newValue);
            }
        }

        public static readonly DependencyProperty FriendlyNameProperty = DependencyProperty.RegisterAttached (
            "FriendlyName",
            typeof (string),
            typeof (BindTo),
            new PropertyMetadata (
                default (string),
                Changed_FriendlyName
            ));

        static void Changed_FriendlyName (DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (dependencyObject != null)
            {
                var oldValue = (string)eventArgs.OldValue;
                var newValue = (string)eventArgs.NewValue;

                Changed_FriendlyName (dependencyObject, oldValue, newValue);
            }
        }

        #endregion


        // --------------------------------------------------------------------
        // Properties
        // --------------------------------------------------------------------

           
        // --------------------------------------------------------------------
        public static string GetData (DependencyObject dependencyObject)
        {
            if (dependencyObject == null)
            {
                return default (string);
            }

            return (string)dependencyObject.GetValue (DataProperty);
        }

        public static void SetData (DependencyObject dependencyObject, string value)
        {
            if (dependencyObject != null)
            {
                if (GetData (dependencyObject) != value)
                {
                    dependencyObject.SetValue (DataProperty, value);
                }
            }
        }

        public static void ClearData (DependencyObject dependencyObject)
        {
            if (dependencyObject != null)
            {
                dependencyObject.ClearValue (DataProperty);
            }
        }
        // --------------------------------------------------------------------
        static partial void Changed_Data (DependencyObject dependencyObject, string oldValue, string newValue);
        // --------------------------------------------------------------------


           
        // --------------------------------------------------------------------
        public static string GetFriendlyName (DependencyObject dependencyObject)
        {
            if (dependencyObject == null)
            {
                return default (string);
            }

            return (string)dependencyObject.GetValue (FriendlyNameProperty);
        }

        public static void SetFriendlyName (DependencyObject dependencyObject, string value)
        {
            if (dependencyObject != null)
            {
                if (GetFriendlyName (dependencyObject) != value)
                {
                    dependencyObject.SetValue (FriendlyNameProperty, value);
                }
            }
        }

        public static void ClearFriendlyName (DependencyObject dependencyObject)
        {
            if (dependencyObject != null)
            {
                dependencyObject.ClearValue (FriendlyNameProperty);
            }
        }
        // --------------------------------------------------------------------
        static partial void Changed_FriendlyName (DependencyObject dependencyObject, string oldValue, string newValue);
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
                default (double),
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
                default (double),
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
                default (double),
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
                default (double),
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
                default (double),
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

}

