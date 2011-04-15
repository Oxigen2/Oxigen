using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using Aspose.Flash.Bitmaps;
using Aspose.Flash.Swf;
using Aspose.Flash.Text;
using ImageExtraction;

namespace Oxigen.ApplicationServices.Flash
{
    public class SWAFile
    {
        private FlashContainer _flashContainer;

        public SWAFile(string filePath)
        {
            var lic = new Aspose.Flash.License();
            lic.SetLicense("Aspose.Flash.lic");
            _flashContainer = new FlashContainer(filePath);
        }

        public SWAFile(Stream stream)
        {
            var lic = new Aspose.Flash.License();
            lic.SetLicense("Aspose.Flash.lic");
            _flashContainer = new FlashContainer(stream);
            stream.Close();
        }

        public void UpdateEmbeddedXML(string embeddedName, string XML) {
            //get all class to symbol mappings
            int xmlAssetId = GetAssetId(embeddedName);

            //find all binary data objects which can contain our xml

            ArrayList assets = _flashContainer.GetObjectsOfType(BaseObjectType.DefineBinaryData);

            //look for object with xml asset id
            foreach (DefineBinaryData data in assets) {
                if (data.Id == xmlAssetId) {
                    string xmlData = Encoding.UTF8.GetString(data.Data);
                    data.Data = Encoding.UTF8.GetBytes(XML);

                }

            }
        }

        private int GetAssetId(string embeddedName) {
            ArrayList symbolClasses =
                _flashContainer.GetObjectsOfType(BaseObjectType.SymbolClass);

            int xmlAssetId = -1;
            //find the id of the object that contains actual xml data

            foreach (SymbolClass classes in symbolClasses) {
                foreach (int classId in classes.ClassList.Keys) {
                    if (string.Compare((string)classes.ClassList[classId],
                                       embeddedName, StringComparison.Ordinal) ==
                        0) {
                        xmlAssetId = classId;
                        break;
                    }
                }
            }
            return xmlAssetId;
        }

        public void UpdateBitmap(string name, Image newImage) {

            var assetId = GetAssetId(name);

            foreach (var obj in _flashContainer.Objects) {
                if (obj is FlashBitmap) {
                    var oldBitmap = (FlashBitmap)obj;
                    if (oldBitmap.Id == assetId) {
                        Bitmap bitmap = new Bitmap(newImage, newImage.Size);

                        //create a FlashBitmap image object from the bitmap and add it to the flash container

                        FlashBitmap defineImg = new FlashBitmap(oldBitmap.Id, FlashBitmapType.FlashBitmapJpeg, false, bitmap);

                        _flashContainer.Objects[_flashContainer.Objects.IndexOf(obj)] = defineImg;
                        break;

                    }

                }


            }


        }

        public void Save(string fileName) {
            _flashContainer.Write(fileName);
        }


        public void UpdateText(string instanceName, string html) {
            var assetId = GetAssetId(instanceName);

            foreach (var obj in _flashContainer.Objects) {
                if (obj is DefineSprite) {
                    var sprite = (DefineSprite)obj;
                    if (sprite.Id == assetId) {
                        var defineTextObjectId = ((PlaceObject2)sprite.ControlTags[0]).ObjectId;

                        ArrayList editTextClasses = _flashContainer.GetObjectsOfType(BaseObjectType.DefineEditText);
                        foreach (DefineEditText defineEditText in editTextClasses) {
                            if (defineEditText.Id == defineTextObjectId) {
                                defineEditText.InitialText = html;
                                break;
                            }
                        }
                        break;

                    }


                }


            }


        }


        public Image GetLastFrameImage()
        {
            return _flashContainer.Render(_flashContainer.NumberOfFrames-1);
        }

        public Image GetLastFrameImageAsThumbnail()
        {
            return ImageUtilities.Crop(GetLastFrameImage(), 100, 75, AnchorPosition.Center);
        }
    }
}
