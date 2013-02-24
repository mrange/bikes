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

}

