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
	public interface IAudioCodec
		: ICodec
	{
		/// <summary>
		/// Creates a decoder from the given format parser.
		/// </summary>
		/// <param name="options">The audio codec options.</param>
		/// <param name="mediaStream">The format parser to get packets from.</param>
		/// <returns>The audio decoder.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="mediaStream"/> or <paramref name="options"/> are <c>null</c>.</exception>
		/// <exception cref="ArgumentException">No mapping could be found for <paramref name="mediaStream"/> to this codec.</exception>
		IAudioDecoder CreateDecoder (AudioCodecOptions options, MediaStream mediaStream);
	}
}