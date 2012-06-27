using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using EarthCoreEngine;

namespace WorldBeardEngine.Assets
{
    public class AssetTextureManager : TextureManager
    {

        public AssetTextureManager(string textureDirectory)
        {
            String[] fileNames = Directory.GetFiles(textureDirectory);

            int substringStart;
            int substringEnd;

            /**
             * Automatically add each file in the assets folder to the PlatformTextureManager. Automatically
             * parse the pseudonum as the filename (without the file extension). For example, the file "image01.png"
             * will be automatically added to the PlatformTextureManager with the pseudonym "image01".
             */
            foreach (String fileName in fileNames)
            {
                // The \\ is the char literal for \
                substringStart = fileName.LastIndexOf('\\') + 1;
                substringEnd = fileName.LastIndexOf('.');
                String filePseudonym = fileName.Substring(substringStart, substringEnd - substringStart);
                this.LoadTexture(filePseudonym, fileName);
            }

        }

        public Sprite GetSprite(String textureName, int xPos, int yPos, double width, double height)
        {
            Vector startingPosition = new Vector(xPos, yPos);
            Sprite sprite = new Sprite(this.Get(textureName), startingPosition, width, height);
            return sprite;
        }

        public AnimatedSprite GetAnimatedSprite(String textureName, int xPos, int yPos, double width, double height)
        {
            Vector startingPosition = new Vector(xPos, yPos);
            AnimatedSprite animatedSprite = new AnimatedSprite(this.Get(textureName), startingPosition, width, height);
            return animatedSprite;
        }

    }
}
