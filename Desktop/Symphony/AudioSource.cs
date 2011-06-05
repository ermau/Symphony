//
// MIT License
// Copyright ©2011 Eric Maupin
// All rights reserved.

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.ComponentModel;
using Symphony.Encoding;

namespace Symphony
{
	public class AudioSource
		: INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public const string FormatName = "Format";
		public AudioFormat Format
		{
			get { return this.format; }
			set
			{
				if (this.format == value)
					return;

				this.format = value;
				OnPropertyChanged (FormatName);
			}
		}

		public const string LocationName = "Location";
		public Location Location
		{
			get { return this.location; }
			set
			{
				if (this.location == value)
					return;

				this.location = value;
				OnPropertyChanged (LocationName);
			}
		}

		public const string TagName = "Tag";
		public object Tag
		{
			get { return this.tag; }
			set
			{
				if (this.tag == value)
					return;

				this.tag = value;
				OnPropertyChanged (TagName);
			}
		}

		private Location location;
		private AudioFormat format;
		private object tag;

		protected void OnPropertyChanged (string name)
		{
			var changed = PropertyChanged;
			if (changed != null)
				changed (this, new PropertyChangedEventArgs (name));
		}
	}
}