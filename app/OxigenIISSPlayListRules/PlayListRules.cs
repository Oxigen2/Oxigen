using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Text;
using System.Xml;

namespace OxigenIIAdvertising.PlaylistRules
{
  public static class PlayListRules
  {
    public static string GetPlayList()
    {      
      //return "<playlist></playlist>";

      string playlist = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
                          <playlist>
                            <asset id=""1000"">
                              <playertype value=""image""/>
                              <assetlocation value=""c:\documents and settings\all users\OxigenIIAssets\image_1.jpg""/>
                              <displaylength value=""10""/>
                              <clickdestination value=""http://www.google.co.uk""/>
                              <scheduleinfo>
                                <bracket>
                                  <day value=""Wednesday""/>
                                  <and/>
                                  <time value=""09:00"" comp=""gteq""/>
                                  <and/>
                                  <time value=""12:00"" comp=""lteq""/>
                                </bracket>
                                <or/>
                                <bracket>
                                  <day value=""Thursday""/>
                                  <and/>
                                  <bracket>
                                    <bracket>
                                      <time value=""09:30"" comp=""gteq""/>
                                      <and/>
                                      <time value=""12:30"" comp=""lteq""/>
                                    </bracket>
                                    <or/>
                                    <bracket>
                                      <time value=""20:30"" comp=""gteq""/>
                                      <and/>
                                      <time value=""22:30"" comp=""lteq""/>
                                    </bracket>
                                  </bracket>
                                </bracket>
                              </scheduleinfo>
                              <channel id=""1000""/>
                              <assetlevel value=""premium""/>
                              <assettype value=""content""/>
                            </asset>
                            <asset id=""1001"">
                              <playertype value=""avi""/>
                              <assetlocation value=""c:\documents and settings\all users\OxigenIIAssets\video_1.avi""/>
                              <displaylength value=""10""/>
                              <clickdestination value=""http://www.ask.co.uk""/>
                              <scheduleinfo>
                                <bracket>
                                  <day value=""Monday""/>
                                  <and/>
                                  <time value=""09:00"" comp=""gteq""/>
                                  <and/>
                                  <time value=""12:00"" comp=""lteq""/>
                                </bracket>
                                <or/>
                                <bracket>
                                  <day value=""Tuesday""/>
                                  <and/>
                                  <bracket>
                                    <bracket>
                                      <time value=""09:30"" comp=""gteq""/>
                                      <and/>
                                      <time value=""12:30"" comp=""lteq""/>
                                    </bracket>
                                    <or/>
                                    <bracket>
                                      <time value=""20:30"" comp=""gteq""/>
                                      <and/>
                                      <time value=""22:30"" comp=""lteq""/>
                                    </bracket>
                                  </bracket>
                                </bracket>
                              </scheduleinfo>
                              <channel id=""1000""/>
                              <assetlevel value=""premium""/>
                              <assettype value=""content""/>
                            </asset>
                          </playlist>";

      
    }
  }
}
