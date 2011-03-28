using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;

namespace OxigenIIPresentation.CommandHandlers
{
  public class SearchProcessor : CommandProcessor
  {
    internal override string Execute(NameValueCollection commandParameters)
    {
        return "1000,,Animated Penguin March,,Images/Temp-Savers/penguin.jpg,,true" +
        "||1001,,3D Wild Dolphins,,Images/Temp-Savers/penguin2.jpg,,false" +
        "||1002,,German Shephard Puppies,,Images/Temp-Savers/pup2.jpg,,false" +
        "||1003,,Cavalier King Charles,,Images/Temp-Savers/pup.jpg,,false" +
        "||1004,,Hounds,,Images/Temp-Savers/hounds.jpg,,false" +
        "||1005,,Dalmatian Puppies,,Images/Temp-Savers/puppies2.jpg,,true" +
        "||1006,,Curled Up Kittens,,Images/Temp-Savers/kitten.jpg,,false" +
        "||1007,,Pug,,Images/Temp-Savers/pug.jpg,,true" +
        "||1008,,Kittens Bouncing through Meadow,,Images/Temp-Savers/cats.jpg,,false" +
        "||1009,,Cavalier King Charles Pups,,Images/Temp-Savers/pup.jpg,,false" +
        "||1010,,Hounds,,Images/Temp-Savers/hounds.jpg,,false" +
        "||1011,,Labrador Puppies,,Images/Temp-Savers/puppies.jpg,,true" +
        "||1012,,Pug,,Images/Temp-Savers/pug.jpg,,true" +
        "||1013,,Kittens Bouncing through Meadow,,Images/Temp-Savers/cats.jpg,,false" +
        "||1014,,Cavalier King Charles Pups,,Images/Temp-Savers/pup.jpg,,false" +
        "||1015,,Hounds,,Images/Temp-Savers/hounds.jpg,,false" +
        "||1016,,Labrador Puppies,,Images/Temp-Savers/puppies.jpg,,true";
    }
  }
}
