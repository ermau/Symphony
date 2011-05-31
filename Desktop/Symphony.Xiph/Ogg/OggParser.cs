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
using System.IO;
using Symphony.Encoding;

namespace Symphony.Xiph.Ogg
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

		public bool TryReadPacket (out Packet packet)
		{
			packet = null;

			bool havePacket = false;
			
			List<byte[]> pagesOfData = new List<byte[]>();

			bool streamIndexFound = false;
			int streamIndex = 0;

			while (!havePacket)
			{
				OggPage page; int i = 0;
				if (this.offset == 0 || this.offset == this.lastPage.Segments.Length)
				{
					if (!streamIndexFound && this.pageBuffer.Count != 0)
						page = this.pageBuffer.Dequeue();
					else
					{
						page = ReadPage();
						if (page == null)
							break;
					}

					if (!streamIndexFound)
					{
						streamIndexFound = true;
						streamIndex = page.StreamIndex;
					}
					else if (page.StreamIndex != streamIndex)
					{
						this.pageBuffer.Enqueue (page);
						continue;
					}
				}
				else
				{
					page = this.lastPage;
					i = this.offset;
				}

				this.lastPage = page;

				int packetLength = 0;
				for (int s = 0; s < page.Segments.Length; ++s)
				{
					byte len = (byte)page.Segments[s].Length;
					packetLength += len;

					if (len < 255)
						break;
				}

				byte[] packetData = new byte[packetLength];

				for (; i < page.Segments.Length; ++i)
				{
					int segmentLength = page.Segments[i].Length;
					Buffer.BlockCopy (page.Segments[i], 0, packetData, i * 255, segmentLength);
					if (segmentLength < 255)
					{
						havePacket = true;
						break;
					}
				}

				pagesOfData.Add (packetData);

				this.offset = (i == page.Segments.Length) ? 0 : i;
			}

			if (havePacket)
				packet = new OggPacket (streamIndex, 0, this.lastPage.GranuleNumber, AssemblePacket (pagesOfData));

			return havePacket;
		}

		public bool TryReadContext (out IFormatContext context)
		{
			throw new NotImplementedException();
		}

		private int offset;
		private OggPage lastPage;
		private readonly Stream stream;

		private Queue<OggPage> pageBuffer = new Queue<OggPage>();

		private byte[] AssemblePacket (List<byte[]> pages)
		{
			if (pages.Count == 1)
				return pages[0];

			int size = 0;
			for (int i = 0; i < pages.Count; ++i)
				size += pages[i].Length;

			int offset = 0;
			byte[] data = new byte[size];
			for (int i = 0; i < pages.Count; ++i)
			{
				byte[] page = pages[i];
				int len = page.Length;

				Buffer.BlockCopy (page, 0, data, offset, len);
				offset += len;
			}

			return data;
		}

		private OggPage ReadPage()
		{
			byte[] pageHeaderData;
			if (stream.ReadBytes (out pageHeaderData, 27) < 27)
				return null;

			string magic = System.Text.Encoding.UTF8.GetString (pageHeaderData, 0, 4);
			if (magic != "OggS")
				return null;

			byte version = pageHeaderData[4];

			OggPageType type = (OggPageType)pageHeaderData[5];

			byte[] granule = new byte[8];
			Array.Copy (pageHeaderData, 6, granule, 0, 8);
			//Buffer.BlockCopy (pageHeaderData, 6, granule, 0, 8);

			int streamSerialNumber = BitConverter.ToInt32 (pageHeaderData, 14);
			int pageSequenceNumber = BitConverter.ToInt32 (pageHeaderData, 18);

			int checksum = BitConverter.ToInt32 (pageHeaderData, 22);

			byte segments = pageHeaderData[26];

			byte[] segmentLengths = new byte[segments];
			if (stream.ReadBytes (out segmentLengths, segments) < segments)
				return null;

			byte[][] data = new byte[segmentLengths.Length][];
			for (int i = 0; i < segmentLengths.Length; ++i)
			{
				byte len = segmentLengths[i];
				byte[] segment;
				if (stream.ReadBytes (out segment, len) != len)
					return null;

				data[i] = segment;
			}

			OggPage page = new OggPage (streamSerialNumber, pageSequenceNumber);
			page.Type = type;
			page.Version = version;
			page.Segments = data;
			page.Granule = granule;

			return page;
		}
	}

	[Flags]
	public enum OggPageType
		: byte
	{
		Continuation = 0x01,
		BeginningOfStream = 0x02,
		EndOfStream = 0x04
	}
}