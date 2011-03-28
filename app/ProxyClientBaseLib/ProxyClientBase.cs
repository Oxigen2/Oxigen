using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace ProxyClientBaseLib
{
  /// <summary>
  /// Applies WCF best practice to client proxy - service communication in a single class, 
  /// terminates gracefully when an Fault occurs.
  /// </summary>
  /// <typeparam name="TChannel">Contract for the proxy to get the methods from</typeparam>
  public class ProxyClientBase<TChannel> : ClientBase<TChannel>, IDisposable where TChannel : class
  {
    private bool _bDisposed = false;

    public ProxyClientBase() : base() { }

    public ProxyClientBase(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : base(binding, remoteAddress) { }

    ~ProxyClientBase()
    {
      this.Dispose(false);
    }

    /// <summary>
    /// Cleans up resources used by this ProxyClientBaseLib.ProxyClientBase object
    /// </summary>
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }
    
    private void Dispose(bool disposing)
    {
      if (!this._bDisposed)
      {
        try
        {
          if (this.State == CommunicationState.Opened)
            this.Close();
        }
        finally
        {
          if (this.State == CommunicationState.Faulted)
            this.Abort();
        }

        this._bDisposed = true;
      }
    }
  }
}
