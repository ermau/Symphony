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

namespace Symphony.Encoding
{
	public enum RawAudioEncoding
		: byte
	{
		Unknown = 0,
		LinearPcm = 1,
		ALaw = 2,
		MuLaw = 3
	}

	/// <summary>
	/// Contains audio formatting settings.
	/// </summary>
	public class AudioFormat
		: IEquatable<AudioFormat>
	{
		/// <summary>
		/// Initializes a new instance of the <seealso cref="AudioFormat"/> class.
		/// </summary>
		/// <param name="encoding">The format of the raw audio encoding.</param>
		/// <param name="bitsPerSample">The bits in each sample of audio, multiples of 8 only.</param>
		/// <param name="channels">The number of channels in the format.</param>
		/// <param name="frequency">The number of samples per second.</param>
		public AudioFormat (RawAudioEncoding encoding, int bitsPerSample, int channels, int frequency)
		{
			if (channels <= 0)
				throw new ArgumentOutOfRangeException ("channels");
			if (frequency <= 0)
				throw new ArgumentOutOfRangeException ("frequency");
			if ((bitsPerSample % 8) != 0)
				throw new ArgumentOutOfRangeException ("bitsPerSample", "bitsPerSample is not a multiple of 8");

			BitsPerSample = bitsPerSample;
			Encoding = encoding;
			Channels = channels;
			Frequency = frequency;
		}

		/// <summary>
		/// Gets the bits in each sample.
		/// </summary>
		public int BitsPerSample
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the format of the raw audio encoding.
		/// </summary>
		public RawAudioEncoding Encoding
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the number of channels in this format.
		/// </summary>
		public int Channels
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the number of samples per second in this format.
		/// </summary>
		public int Frequency
		{
			get;
			private set;
		}

		public override bool Equals (object obj)
		{
			if (ReferenceEquals (null, obj))
				return false;
			if (ReferenceEquals (this, obj))
				return true;
			if (obj.GetType() != typeof (AudioFormat))
				return false;
			return Equals ((AudioFormat) obj);
		}

		public bool Equals (AudioFormat other)
		{
			if (ReferenceEquals (null, other))
				return false;
			if (ReferenceEquals (this, other))
				return true;
			return other.BitsPerSample == BitsPerSample && Equals (other.Encoding, Encoding) && other.Channels == Channels && other.Frequency == Frequency;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int result = this.BitsPerSample;
				result = (result * 397) ^ Encoding.GetHashCode();
				result = (result * 397) ^ Channels;
				result = (result * 397) ^ Frequency;
				return result;
			}
		}

		public static bool operator == (AudioFormat left, AudioFormat right)
		{
			return Equals (left, right);
		}

		public static bool operator != (AudioFormat left, AudioFormat right)
		{
			return !Equals (left, right);
		}
	}
}