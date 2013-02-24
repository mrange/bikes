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

namespace SASBikes.PushPins
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

}

