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


// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using SASBikes.Common.DataModel;
using SASBikes.Pages;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SASBikes.Controls
{
    public sealed partial class ErrorIndicator
    {
        partial void Constructed__ErrorIndicator()
        {
            InitializeComponent();
        }

        partial void Changed_Errors(ErrorList oldValue, ErrorList newValue)
        {
            if (oldValue != null)
            {
                oldValue.CollectionChanged -= Errors_CollectionChanged;    
            }

            if (newValue != null)
            {
                newValue.CollectionChanged += Errors_CollectionChanged;
            }

            UpdateErrorCount(newValue);
        }

        void Errors_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateErrorCount(Errors);
        }

        void UpdateErrorCount(ObservableCollection<Error> newValue)
        {
            if (newValue != null)
            {
                ErrorCount = newValue.Count;
            }
            else
            {
                ErrorCount = 0;
            }
        }

        partial void Changed_ErrorCount(int oldValue, int newValue)
        {
            if (newValue < 1)
            {
                ErrorTextVisibility = Visibility.Collapsed;
                return;
            }

            ErrorTextVisibility = Visibility.Visible;

            newValue = Math.Min(10, newValue);

            var charValue = (char)(newValue + 0x2460 - 1);

            ErrorText = charValue.ToString();
        }

        void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var rootFrame = Window.Current.Content as Frame;
            if (rootFrame != null)
            {
                rootFrame.Navigate(typeof(ErrorsPage));    
            }
        }
    }
}
