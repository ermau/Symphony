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
using System.IO;

namespace Symphony.Encoding.Formats.Ogg
{
	public class OggParser
		: IFormatParser
	{
		public OggParser (Stream stream)
		{
			if (stream == null)
				throw new ArgumentNullException ("stream");

			this.stream = stream;
		}

		public bool ReadPacket (out Packet packet)
		{
			packet = null;

			byte[] pageHeaderData;
			if (stream.ReadBytes (out pageHeaderData, 27) < 27)
				return false;

			string magic = System.Text.Encoding.UTF8.GetString (pageHeaderData, 0, 4);
			if (magic != "OggS")
				return false;

			byte version = pageHeaderData[4];

			OggPageType type = (OggPageType)pageHeaderData[5];

			byte[] granule = new byte[8];
			Array.Copy (pageHeaderData, 6, granule, 0, 8);
			//Buffer.BlockCopy (pageHeaderData, 6, granule, 0, 8);

			int streamSerialNumber = BitConverter.ToInt32 (pageHeaderData, 14);
			int pageSequenceNumber = BitConverter.ToInt32 (pageHeaderData, 18);

			int checksum = BitConverter.ToInt32 (pageHeaderData, 22);

			int segments = BitConverter.ToInt32 (pageHeaderData, 26);

			byte[] segmentLengths = new byte[segments];

			return true;
		}

		private readonly Stream stream;
	}

	[Flags]
	public enum OggPageType
		: byte
	{
		Continuation = 0x01,
		BeginningOfStream = 0x02,
		EndOfStream = 0x04
	}

	public class OggPage
		: Packet
	{
		public OggPage (int streamIndex)
			: base (streamIndex)
		{
		}

		public byte Version
		{
			get;
			set;
		}

		public OggPageType Type
		{
			get;
			set;
		}

		public byte[] SegmentLengths
		{
			get;
			set;
		}
	}
}