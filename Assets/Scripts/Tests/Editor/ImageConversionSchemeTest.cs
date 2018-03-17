using NUnit.Framework;
using ProjectFTP.Level;
using UnityEngine;

namespace ProjectFTP.Tests.Editor
{
    class ImageConversionSchemeTest
    {
        ImageConversionScheme conversionScheme;
        ColorToPrefab config;

        [SetUp]
        public void SetUp()
        {
            conversionScheme = new ImageConversionScheme();

            config = new ColorToPrefab();
            config.color = new Color32();
            config.prefab = new GameObject();

            conversionScheme.colorsToPrefab.Add(config);
        }

        [Test]
        public void Can_Convert_Color_To_Prefab()
        {
            Assert.AreEqual(config.prefab, conversionScheme.GetPrefab(config.color));
        }
    }
}
