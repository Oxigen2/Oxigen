using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OxigenIIAdvertising.LoggerInfo;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Security.Cryptography;
using NoAssetsAnimator.Properties;

namespace OxigenIIAdvertising.NoAssetsAnimator
{
  public class NoAssetsAnimatorPlayer : UserControl, IDisposable
  {
    private string _message;
    private RNGCryptoServiceProvider _random = null;
    private int _posX = -1;
    private int _posY = -1;
    private int _frontImageWidth = -1;
    private int _frontImageHeight = -1;
    private int _displayMessageX = -1;
    private int _displayMessageY = -1;
    private int _displayMessageWidth = -1;
    private int _displayMessageHeight = -1;
    private int _maximumFrontImagePosX = -1;
    private int _maximumFrontImagePosY = -1;
    private bool _bDisposed;
    private Label _displayMessage;
    private Image _frontImage;
    private byte[] _randomNumbers = null;

    /// <summary>
    /// The message to accompany the image
    /// </summary>
    public string Message
    {
      get { return _message; }
      set
      {
        _message = value;
        _displayMessage.Text = value;
      }
    }

    /// <summary>
    /// Instantiates an NoAssetsAnimatorPlayer
    /// </summary>
    /// <param name="debugFilePath">the path of the output debug file</param>
    public NoAssetsAnimatorPlayer()
    {
      _random = new RNGCryptoServiceProvider();
      _randomNumbers = new byte[8];

      InitializeComponent();

      _bDisposed = false;
    }

    public void Play()
    {
      GetImageWithSize();

      _maximumFrontImagePosX = this.Bounds.Width - _frontImage.Width;
      _maximumFrontImagePosY = this.Bounds.Height - _frontImage.Height;

      GetRandomImagePosition();
      GetDisplayMessageSizePosition();

      _displayMessage.Width = _displayMessageWidth;
      _displayMessage.Height = _displayMessageHeight;
      _displayMessage.Location = new Point(_displayMessageX, _displayMessageY);

      this.Invalidate();
    }

    private void GetImageWithSize()
    {
      Image tempImage = Resources.OxigenMessage;

      _frontImage = new Bitmap(tempImage);

      tempImage.Dispose();

      _frontImageWidth = _frontImage.Width;
      _frontImageHeight = _frontImage.Height;
    }

    void NoAssetsAnimatorPlayer_Paint(object sender, PaintEventArgs e)
    {
      // has image been defined
      if (_frontImage == null)
        return;

      Graphics g = e.Graphics;

      g.DrawImage(_frontImage, _posX, _posY);
    }

    private void GetDisplayMessageSizePosition()
    {
      _displayMessageWidth = (80 * _frontImageWidth / 100);
      _displayMessageHeight = (35 * _frontImageHeight / 100);

      _displayMessageX = _posX + (10 * _frontImageWidth / 100);
      _displayMessageY = _posY + (55 * _frontImageHeight / 100);
    }

    private void GetRandomImagePosition()
    {
      _posX = GetRandomInt(_maximumFrontImagePosX);
      _posY = GetRandomInt(_maximumFrontImagePosY);
    }

    /// <summary>
    /// Disposes of the resources used by OxigenIIAdvertising.NoAssetsAnimator.NoAssetsAnimatorPlayer
    /// </summary>
    public new void Dispose()
    {
      Dispose(true);

      // Use SupressFinalize in case a subclass
      // of this type implements a finalizer.
      GC.SuppressFinalize(this);
    }

    protected override void Dispose(bool bDisposing)
    {
      if (!_bDisposed)
      {
        if (bDisposing)
        {
          if (_frontImage != null)
            _frontImage.Dispose();
        }

        _frontImage = null;

        base.Dispose(bDisposing);

        _bDisposed = true;
      }
    }

    private int GetRandomInt(int maxValue)
    {
      _random.GetBytes(_randomNumbers);

      int result = BitConverter.ToInt32(_randomNumbers, 0);

      return new Random(result).Next(0, maxValue);
    }

    ~NoAssetsAnimatorPlayer()
    {
      Dispose(false);
    }

    private void InitializeComponent()
    {
      this._displayMessage = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // _displayMessage
      // 
      this._displayMessage.BackColor = System.Drawing.Color.White;
      this._displayMessage.CausesValidation = false;
      this._displayMessage.ForeColor = System.Drawing.Color.Black;
      this._displayMessage.Location = new System.Drawing.Point(144, 279);
      this._displayMessage.Name = "_displayMessage";
      this._displayMessage.TabIndex = 1;
      this._displayMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

      this._displayMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      
      // 
      // NoAssetsAnimatorPlayer
      // 
      this.BackColor = System.Drawing.Color.Black;
      this.BackgroundImage = global::NoAssetsAnimator.Properties.Resources.BubbleBackground;
      this.Controls.Add(this._displayMessage);
      this.Name = "NoAssetsAnimatorPlayer";
      this.Size = new System.Drawing.Size(964, 826);
      this.Paint += new System.Windows.Forms.PaintEventHandler(this.NoAssetsAnimatorPlayer_Paint);
      this.ResumeLayout(false);

    }
  }
}