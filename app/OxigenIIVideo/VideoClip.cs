using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OxigenIIChannel;

namespace OxigenIIAdvertising.OxigenIIAsset
{
  public class VideoClip : Asset
  {
    public VideoClip(Channel channel, int assetID, AssetLevel assetLevel, AssetType assetType, DateTime startTime, DateTime endtime)
      : base(channel, assetID, assetLevel, assetType, startTime, endtime)
    {
      // more logic here
    }


    public override void Play()
    {
      throw new NotImplementedException();
    }
  }
}
