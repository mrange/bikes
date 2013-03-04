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

// ReSharper disable InconsistentNaming

using System.Windows.Media;

namespace SASBikes.WP8.Controls
{
    public partial class StationPushPin
    {
        static readonly Brush s_isOpenBackground    = new SolidColorBrush (Colors.Purple);
        static readonly Brush s_isClosedBackground  = new SolidColorBrush (Colors.Yellow);

        partial void Constructed__StationPushPin()
        {
            InitializeComponent();
            StationBackground = s_isClosedBackground;
        }

        partial void Changed_IsOpen(bool oldValue, bool newValue)
        {
            StationBackground = newValue ? s_isOpenBackground : s_isClosedBackground;
        }


    }
}
