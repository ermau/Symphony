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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Symphony.Audio;
using AudioFormat = Symphony.Audio.AudioFormat;
using SLAudioCaptureDevice = System.Windows.Media.AudioCaptureDevice;
using SLAudioFormat = System.Windows.Media.AudioFormat;

namespace Symphony.Silverlight
{
	public class SilverlightAudioCaptureDevice
		: IAudioCaptureDevice, INotifyPropertyChanged
	{
		public SilverlightAudioCaptureDevice (SLAudioCaptureDevice captureDevice)
		{
			if (captureDevice == null)
				throw new ArgumentNullException ("captureDevice");

			this.captureDevice = captureDevice;
			var formatMap = new Dictionary<AudioFormat, SLAudioFormat>();
			foreach (SLAudioFormat format in captureDevice.SupportedFormats)
			{
				var syfmt = format.ToAudioFormat();
				if (formatMap.ContainsKey (syfmt))
					continue;

				formatMap.Add (syfmt, format);
			}

			this.formats = formatMap;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public event EventHandler IsAvailableChanged;

		public bool IsAvailable
		{
			get { return this.isAvailable; }

			internal set
			{
				if (this.isAvailable == value)
					return;

				this.isAvailable = value;

				var changed = IsAvailableChanged;
				if (changed != null)
					changed (this, EventArgs.Empty);

				OnPropertyChanged (new PropertyChangedEventArgs ("IsAvailable"));
			}
		}

		public string Name
		{
			get { return this.captureDevice.FriendlyName; }
		}

		public IEnumerable<AudioFormat> SupportedFormats
		{
			get;
			private set;
		}

		public void Open (AudioFormat format)
		{
			if (format == null)
				throw new ArgumentNullException ("format");

			SLAudioFormat mediaFormat;
			if (!this.formats.TryGetValue (format, out mediaFormat))
				throw new ArgumentException ("Unsupported format", "format");
		}

		public void Close()
		{
		}

		public void Dispose()
		{
		}

		private bool isAvailable = true;
		private readonly SLAudioCaptureDevice captureDevice;
		private readonly Dictionary<AudioFormat, SLAudioFormat> formats;

		private void OnPropertyChanged (PropertyChangedEventArgs e)
		{
			var changed = PropertyChanged;
			if (changed != null)
				changed (this, e);
		}
	}
}