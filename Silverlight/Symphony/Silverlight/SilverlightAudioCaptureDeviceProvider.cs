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
using System.Windows.Media;
using System.Windows.Threading;
using Symphony.Audio;
using SLAudioCaptureDevice = System.Windows.Media.AudioCaptureDevice;

namespace Symphony.Silverlight
{
	public class SilverlightAudioCaptureDeviceProvider
		: IAudioDeviceProvider, INotifyPropertyChanged
	{
		public SilverlightAudioCaptureDeviceProvider()
		{
			if (!CaptureDeviceConfiguration.AllowedDeviceAccess)
				CaptureDeviceConfiguration.RequestDeviceAccess();

			UpdateDevices();
			this.pollTimer.Interval = TimeSpan.FromMilliseconds (250);
			this.pollTimer.Tick += PollTimerOnTick;
			this.pollTimer.Start();
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public event EventHandler DefaultDeviceChanged;

		public IAudioDevice DefaultDevice
		{
			get;
			private set;
		}

		public IEnumerable<IAudioDevice> Devices
		{
			get
			{
				lock (this.devices)
					return this.devices.Values.Where (d => d.IsAvailable).Cast<IAudioDevice>().ToList();
			}
		}

		public void Dispose()
		{
			this.pollTimer.Stop();
		}

		private readonly DispatcherTimer pollTimer = new DispatcherTimer();
		private readonly Dictionary<AudioCaptureDevice, SilverlightAudioCaptureDevice> devices = new Dictionary<AudioCaptureDevice, SilverlightAudioCaptureDevice>();
		
		private class AudioCaptureDeviceEqualityComparer
			: IEqualityComparer<SLAudioCaptureDevice>
		{
			public static readonly AudioCaptureDeviceEqualityComparer Instance
				= new AudioCaptureDeviceEqualityComparer();

			public bool Equals (SLAudioCaptureDevice x, SLAudioCaptureDevice y)
			{
				if (x == y)
					return true;
				if (x == null || y == null)
					return false;

				return (x.FriendlyName == y.FriendlyName);
			}

			public int GetHashCode (SLAudioCaptureDevice obj)
			{
				if (obj == null)
					throw new ArgumentNullException ("obj");

				return obj.FriendlyName.GetHashCode();
			}
		}

		private void PollTimerOnTick(object sender, EventArgs eventArgs)
		{
			UpdateDevices();
		}

		private void UpdateDevices()
		{
			AudioCaptureDevice currentDefault = CaptureDeviceConfiguration.GetDefaultAudioCaptureDevice();
			SilverlightAudioCaptureDevice currentDefaultEx = null;

			lock (this.devices)
			{
				var newDevices = new HashSet<AudioCaptureDevice> (CaptureDeviceConfiguration.GetAvailableAudioCaptureDevices(),
																	AudioCaptureDeviceEqualityComparer.Instance);
				foreach (var kvp in this.devices)
				{
					if (currentDefault != null && kvp.Key.FriendlyName == currentDefault.FriendlyName)
						currentDefaultEx = kvp.Value;

					if (newDevices.Remove (kvp.Key))
					{
						kvp.Value.IsAvailable = true;
						continue;
					}

					this.devices[kvp.Key].IsAvailable = false;
				}

				foreach (AudioCaptureDevice device in newDevices)
				{
					var d = new SilverlightAudioCaptureDevice (device);
					if (currentDefault != null && device.FriendlyName == currentDefault.FriendlyName)
						currentDefaultEx = d;

					this.devices.Add (device, d);
				}
			}

			if (DefaultDevice != currentDefaultEx)
			{
				DefaultDevice = currentDefaultEx;
				OnDefaultDeviceChanged (EventArgs.Empty);
				OnPropertyChanged (new PropertyChangedEventArgs ("DefaultDevice"));
			}
		}

		private void OnDefaultDeviceChanged (EventArgs empty)
		{
			var changed = DefaultDeviceChanged;
			if (changed != null)
				changed (this, empty);
		}

		private void OnPropertyChanged (PropertyChangedEventArgs e)
		{
			var changed = PropertyChanged;
			if (changed != null)
				changed (this, e);
		}
	}
}