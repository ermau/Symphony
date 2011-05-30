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
	{
		LPCM8bit = 1,
		LPCM16bit = 2,
		LPCMSingle = 3,
		LPCMDouble = 4,
		ALaw = 5,
		MuLaw = 6
	}

	public class AudioFormat
	{
		public AudioFormat (RawAudioEncoding encoding, int channels, int frequency)
		{
			if (channels <= 0)
				throw new ArgumentOutOfRangeException ("channels");
			if (frequency <= 0)
				throw new ArgumentOutOfRangeException ("frequency");

			Encoding = encoding;
			Channels = channels;
			Frequency = frequency;
		}

		public RawAudioEncoding Encoding
		{
			get;
			private set;
		}

		public int Channels
		{
			get;
			private set;
		}

		public int Frequency
		{
			get;
			private set;
		}
	}
}