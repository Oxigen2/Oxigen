using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;
using DexterLib;

namespace ImageExtraction
{
  /// <summary>Used to extract Bitmap frames from video files.</summary>
  public sealed class VideoFrameReader : IDisposable, IEnumerable<Bitmap>
  {
    /// <summary>The media detector used to extract frames from the video.</summary>
    private MediaDetClass _mediaDetector;
    /// <summary>The type of the video.</summary>
    private _AMMediaType _mediaType;
    /// <summary>The size of a frame in the video.</summary>
    private Size _frameSize;
    /// <summary>The number of frames in the video.</summary>
    private int _numberOfFrames;
    /// <summary>The length of the video stream.</summary>
    private double _streamLength;
    /// <summary>The size of a BITMAPINFOHEADER.</summary>
    private int _headerSize;
    /// <summary>An unmanaged memory buffer used during the frame extraction process.</summary>
    private SafeLocalAllocHandle _frameBuffer;
    /// <summary>The size of the unmanaged frame buffer.</summary>
    private int _bufferSize;

    /// <summary>Initialize the VideoFrameReader.</summary>
    /// <param name="path">The path to the video to load.</param>
    public VideoFrameReader(string path)
    {
      if (path == null) throw new ArgumentNullException(path);

      // Load the video, determine the size of a frame in the video,
      // determine the length of the video, and calculate the number of frames in it.
      _mediaDetector = LoadVideo(path, out _mediaType);
      _frameSize = GetFrameSize(_mediaType);
      _streamLength = _mediaDetector.StreamLength;
      _numberOfFrames = (int)(_streamLength * _mediaDetector.FrameRate);

      // Create an unmanaged buffer to use in the frame extraction process.
      // We need a buffer large enough to store three bytes per pixel (RGB)
      // plus the size of a BITMAPINFOHEADER structure.
      _headerSize = Marshal.SizeOf(typeof(BITMAPINFOHEADER));
      _bufferSize = (_frameSize.Width * _frameSize.Height * 3) + _headerSize;
      _frameBuffer = SafeLocalAllocHandle.LocalAlloc(_bufferSize);
    }

    /// <summary>Dispose of the instance.</summary>
    public void Dispose()
    {
      if (_mediaDetector != null)
      {
        // Free the frame buffer
        _frameBuffer.Dispose();
        _frameBuffer = null;

        // Free the media type
        if (_mediaType.cbFormat != 0)
        {
          Marshal.FreeCoTaskMem(new IntPtr(_mediaType.cbFormat));
        }
        _mediaType = new _AMMediaType();

        // Release the media detector
        Marshal.ReleaseComObject(_mediaDetector);
        _mediaDetector = null;
      }
    }

    /// <summary>Throws an exception if the object has been disposed.</summary>
    private void ThrowIfDisposed()
    {
      if (_mediaDetector == null) throw new ObjectDisposedException(GetType().Name);
    }

    /// <summary>Get the number of frames in the video.</summary>
    public int NumberOfFrames
    {
      get
      {
        ThrowIfDisposed();
        return _numberOfFrames;
      }
    }

    /// <summary>Extract a frame from the video.</summary>
    /// <param name="frameNumber">The number of the frame to extract.</param>
    /// <returns>A new Bitmap containing the data from the video frame.</returns>
    public unsafe Bitmap GetFrame(int frameNumber)
    {
      ThrowIfDisposed();
      if (frameNumber < 0 || frameNumber >= _numberOfFrames) throw new ArgumentOutOfRangeException("frameNumber");

      // Determine how far into the video stream this frame resides
      double percent = frameNumber / (_numberOfFrames - 1.0);

      // Extract the bits from the frame into the frame buffer
      byte* frameBufferPtr = (byte*)_frameBuffer.DangerousGetHandle();
      _mediaDetector.GetBitmapBits(_streamLength * percent,
          ref _bufferSize, ref *frameBufferPtr, _frameSize.Width, _frameSize.Height);

      // Create and return a new Bitmap based on the data in the frame buffer
      Bitmap bmp = new Bitmap(_frameSize.Width, _frameSize.Height, _frameSize.Width * 3,
          PixelFormat.Format24bppRgb, new IntPtr(frameBufferPtr + _headerSize));
      bmp.RotateFlip(RotateFlipType.Rotate180FlipX);
      return bmp;
    }

    /// <summary>Enumerates the frames in the video.</summary>
    /// <returns>An enumerator for the frames in the video.</returns>
    /// <remarks>
    /// It is the client's responsibility to dispose of the returned Bitmaps.
    /// New Bitmap instances are returned during each enumeration.
    /// </remarks>
    public IEnumerator<Bitmap> GetEnumerator()
    {
      for (int i = 0; i < _numberOfFrames; i++) yield return GetFrame(i);
    }
    IEnumerator IEnumerable.GetEnumerator() { return ((IEnumerable<Bitmap>)this).GetEnumerator(); }

    /// <summary>Loads a video from a file into a MediaDet.</summary>
    /// <param name="filename">The path to the file to be loaded.</param>
    /// <param name="mediaType">The media type of the video loaded.</param>
    /// <returns>The MediaDet configured with the loaded video.</returns>
    private static MediaDetClass LoadVideo(string filename, out _AMMediaType mediaType)
    {
      // Initialize the MediaDet with the video file
      MediaDetClass mediaDet = new MediaDetClass();
      mediaDet.Filename = filename;
      mediaType = new _AMMediaType();

      // Loop through each of the streams in the video searching for the actual video stream
      int numberOfStreams = mediaDet.OutputStreams;
      for (int i = 0; i < numberOfStreams; i++)
      {
        // Return when we find the video stream, leaving the MediaDet set to
        // use that stream.
        mediaDet.CurrentStream = i;
        if (mediaDet.StreamType == MEDIATYPE_Video)
        {
          mediaType = mediaDet.StreamMediaType;
          return mediaDet;
        }
      }

      // No video stream found.  Clean up and error out.
      Marshal.ReleaseComObject(mediaDet);
      throw new ArgumentOutOfRangeException("filename", "No video stream found.");
    }

    /// <summary>Gets the size of a frame based on a video's media type.</summary>
    /// <param name="mediaType">The media type of the video.</param>
    /// <returns>The size of a frame in the video.</returns>
    private static Size GetFrameSize(_AMMediaType mediaType)
    {
      VIDEOINFOHEADER videoInfo = (VIDEOINFOHEADER)Marshal.PtrToStructure(
          mediaType.pbFormat, typeof(VIDEOINFOHEADER));
      return new Size(videoInfo.bmiHeader.biWidth, videoInfo.bmiHeader.biHeight);
    }

    /// <summary>A safe handle for memory allocated with LocalAlloc.</summary>
    [SuppressUnmanagedCodeSecurity]
    private sealed class SafeLocalAllocHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
      [SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
      private SafeLocalAllocHandle() : base(true) { }

      /// <summary>Allocates unmanaged memory.</summary>
      /// <param name="size">Number of bytes to allocate.</param>
      /// <returns>A handle to the newly allocated memory object.</returns>
      public static SafeLocalAllocHandle LocalAlloc(int size)
      {
        SafeLocalAllocHandle handle = LocalAlloc(0, new IntPtr(size));
        if (handle.IsInvalid) throw new OutOfMemoryException();
        return handle;
      }

      /// <summary>Allocates unmanaged memory.</summary>
      /// <param name="uFlags">Memory allocation attributes.</param>
      /// <param name="sizetdwBytes">Number of bytes to allocate.</param>
      /// <returns>A handle to the newly allocated memory object.</returns>
      [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
      private static extern SafeLocalAllocHandle LocalAlloc(int uFlags, IntPtr sizetdwBytes);

      /// <summary>Releases the unmanaged memory.</summary>
      protected override bool ReleaseHandle()
      {
        Marshal.FreeHGlobal(handle);
        handle = IntPtr.Zero;
        return true;
      }
    }

    /// <summary>Media major type for video.</summary>
    private static Guid MEDIATYPE_Video = new Guid("73646976-0000-0010-8000-00AA00389B71");

    /// <summary>This structure defines the coordinates of the upper-left and lower-right corners of a rectangle.</summary>
    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
      /// <summary>Specifies the x-coordinate of the upper-left corner of the rectangle.</summary>
      public int left;
      /// <summary>Specifies the y-coordinate of the upper-left corner of the rectangle.</summary>
      public int top;
      /// <summary>Specifies the x-coordinate of the lower-right corner of the rectangle.</summary>
      public int right;
      /// <summary>Specifies the y-coordinate of the lower-right corner of the rectangle.</summary>
      public int bottom;
    }

    /// <summary>This structure contains information about the dimensions and color format of a device-independent bitmap (DIB).</summary>
    [StructLayout(LayoutKind.Sequential)]
    private struct BITMAPINFOHEADER
    {
      /// <summary>Specifies the size of the structure, in bytes.</summary>
      public uint biSize;
      /// <summary>Specifies the width of the bitmap, in pixels.</summary>
      public int biWidth;
      /// <summary>Specifies the height of the bitmap, in pixels.</summary>
      public int biHeight;
      /// <summary>Specifies the number of planes for the target device.</summary>
      public short biPlanes;
      /// <summary>Specifies the number of bits-per-pixel.</summary>
      public short biBitCount;
      /// <summary>Specifies the type of compression for a compressed bottom-up bitmap.</summary>
      public uint biCompression;
      /// <summary>Specifies the size, in bytes, of the image.</summary>
      public uint biSizeImage;
      /// <summary>Specifies the horizontal resolution, in pixels-per-meter, of the target device for the bitmap.</summary>
      public int biXPelsPerMeter;
      /// <summary>Specifies the vertical resolution, in pixels-per-meter, of the target device for the bitmap.</summary>
      public int biYPelsPerMeter;
      /// <summary>Specifies the number of color indexes in the color table that are actually used by the bitmap.</summary>
      public uint biClrUsed;
      /// <summary>Specifies the number of color indexes required for displaying the bitmap.</summary>
      public uint biClrImportant;
    }

    /// <summary>This structure describes the bitmap and color information for a video image.</summary>
    [StructLayout(LayoutKind.Sequential)]
    private struct VIDEOINFOHEADER
    {
      /// <summary>RECT structure that specifies the source video window.</summary>
      public RECT rcSource;
      /// <summary>RECT structure that specifies the destination video window.</summary>
      public RECT rcTarget;
      /// <summary>DWORD value that specifies the video stream's approximate data rate, in bits per second.</summary>
      public uint dwBitRate;
      /// <summary>DWORD value that specifies the video stream's data error rate, in bit errors per second.</summary>
      public uint dwBitErrorRate;
      /// <summary>REFERENCE_TIME value that specifies the video frame's average display time, in 100-nanosecond units.</summary>
      public ulong AvgTimePerFrame;
      /// <summary>Win32 BITMAPINFOHEADER structure that contains color and dimension information for the video image bitmap.</summary>
      public BITMAPINFOHEADER bmiHeader;
    }
  }
}
