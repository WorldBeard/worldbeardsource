//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using EarthCoreEngine;
//using NUnit.Framework;
//using WorldBeardEngine.Services.Collision.CollisionTypes;

//namespace WorldBeardEngine.Services.Collision.CollisionTypes.CollisionTests
//{
//    [TestFixture]
//    public class CollisionDetectorTest
//    {
        
//        private Sprite _circ_Radius_5_Sprite;
//        private Sprite _circ_Radius_3_Sprite;

//        private Sprite _circ_Corner_Radius_1_Sprite;

//        private Sprite _rect_10_By_10_Sprite;
//        private Sprite _rect_10_By_5_Sprite;
//        private Sprite _rect_5_By_10_Sprite;
//        private Sprite _rect_5_By_5_Sprite;

//        private Sprite _rotated_rect_1_Sprite;
//        private Sprite _rotated_rect_2_Sprite;

//        private CollisionComponent _circ_Radius_5;
//        private CollisionComponent _circ_Radius_3;

//        private CollisionComponent _circ_Corner_Radius_1;

//        private CollisionComponent _rect_10_By_10;
//        private CollisionComponent _rect_10_By_5;
//        private CollisionComponent _rect_5_By_10;
//        private CollisionComponent _rect_5_By_5;

//        private CollisionComponent _rotated_rect_1;
//        private CollisionComponent _rotated_rect_2;

//        [TestFixtureSetUp]
//        public void Init()
//        {
//            Texture texture = new Texture(0, 10, 10);

//            _circ_Radius_5_Sprite = new Sprite(texture, new Vector(0, 0, 0), 1, 1);
//            _circ_Radius_3_Sprite = new Sprite(texture, new Vector(0, 0, 0), 1000, 1000);

//            _circ_Corner_Radius_1_Sprite = new Sprite(texture, new Vector(0, 0, 0), 15, 15);

//            _rect_10_By_10_Sprite = new Sprite(texture, new Vector(0, 0, 0), 10, 10);
//            _rect_10_By_5_Sprite = new Sprite(texture, new Vector(0, 0, 0), 10, 5);
//            _rect_5_By_10_Sprite = new Sprite(texture, new Vector(0, 0, 0), 5, 10);
//            _rect_5_By_5_Sprite = new Sprite(texture, new Vector(0, 0, 0), 5, 5);

//            _rotated_rect_1_Sprite = new Sprite(texture, new Vector(0, 0, 0), 10, 10);
//            _rotated_rect_2_Sprite = new Sprite(texture, new Vector(0, 0, 0), 10, 10);

//            _circ_Radius_5 = new CollisionCircle("", _circ_Radius_5_Sprite, 5);
//            _circ_Radius_3 = new CollisionCircle("", _circ_Radius_5_Sprite, 3);

//            _circ_Corner_Radius_1 = new CollisionCircle("", _circ_Corner_Radius_1_Sprite, 1);

//            _rect_10_By_10 = new CollisionRectangle("", _rect_10_By_10_Sprite);
//            _rect_10_By_5 = new CollisionRectangle("", _rect_10_By_5_Sprite);
//            _rect_5_By_10 = new CollisionRectangle("", _rect_5_By_10_Sprite);
//            _rect_5_By_5 = new CollisionRectangle("", _rect_5_By_5_Sprite);

//            _rotated_rect_1 = new CollisionRectangle("", _rotated_rect_1_Sprite);
//            _rotated_rect_2 = new CollisionRectangle("", _rotated_rect_2_Sprite);
//        }

//        [Test]
//        public void TruthTest()
//        {
//            Assert.That(true);
//        }

//        [Test]
//        public void CircIdentityTest()
//        {
//            Assert.That(CollisionDetector.AreColliding(_circ_Radius_5, _circ_Radius_5));
//        }

//        [Test]
//        public void CircCircSameCenterTest()
//        {
//            Assert.That(CollisionDetector.AreColliding(_circ_Radius_5, _circ_Radius_3));
//            Assert.That(CollisionDetector.AreColliding(_circ_Radius_3, _circ_Radius_5));
//        }

//        [Test]
//        public void RectIdentityTest()
//        {
//            Assert.That(CollisionDetector.AreColliding(_rect_10_By_10, _rect_10_By_10));
//            Assert.That(CollisionDetector.AreColliding(_rect_10_By_5, _rect_10_By_5));
//            Assert.That(CollisionDetector.AreColliding(_rect_5_By_10, _rect_5_By_10));
//        }

//        [Test]
//        public void RectRectSameCenterTest()
//        {
//            Assert.That(CollisionDetector.AreColliding(_rect_10_By_10, _rect_10_By_5));
//            Assert.That(CollisionDetector.AreColliding(_rect_10_By_5, _rect_10_By_10));
//            Assert.That(CollisionDetector.AreColliding(_rect_10_By_10, _rect_5_By_10));
//            Assert.That(CollisionDetector.AreColliding(_rect_5_By_10, _rect_10_By_10));
//            Assert.That(CollisionDetector.AreColliding(_rect_10_By_5, _rect_5_By_10));
//            Assert.That(CollisionDetector.AreColliding(_rect_5_By_10, _rect_10_By_5));
//        }

//        [Test]
//        public void RectRectRotatedTest()
//        {
//            // Both rectangles start as 10x10 centered at (0,0)
//            Assert.That(CollisionDetector.AreColliding(_rotated_rect_1, _rotated_rect_2));
//            _rotated_rect_2_Sprite.SetPosition(10.000001, 10.000001); // Should now be just missing at top right of #1
//            Assert.False(CollisionDetector.AreColliding(_rotated_rect_1, _rotated_rect_2));
//            _rotated_rect_2_Sprite.SetPosition(9.999999, 9.999999); // Should now be just overlapping at top right of #1
//            Assert.That(CollisionDetector.AreColliding(_rotated_rect_1, _rotated_rect_2));
//            _rotated_rect_2_Sprite.Rotation = 0.0001 * Math.PI; // Rotate slightly; should not be colliding now
//            Assert.False(CollisionDetector.AreColliding(_rotated_rect_1, _rotated_rect_2));
//        }

//        [Test]
//        public void RectCircSameCenterTest()
//        {
//            Assert.That(CollisionDetector.AreColliding(_rect_10_By_10, _circ_Radius_5));
//            Assert.That(CollisionDetector.AreColliding(_circ_Radius_5, _rect_10_By_10));
//            Assert.That(CollisionDetector.AreColliding(_rect_10_By_5, _circ_Radius_5));
//            Assert.That(CollisionDetector.AreColliding(_circ_Radius_5, _rect_10_By_5));
//            Assert.That(CollisionDetector.AreColliding(_rect_5_By_10, _circ_Radius_5));
//            Assert.That(CollisionDetector.AreColliding(_circ_Radius_5, _rect_5_By_10));
//        }

//        [Test]
//        public void RectCircCornerTest()
//        {
//            // Top right corner of _rect_10_By_10 is (5, 5)
//            _circ_Corner_Radius_1_Sprite.SetPosition(5.99, 5);
//            Assert.That(CollisionDetector.AreColliding(_rect_10_By_10, _circ_Corner_Radius_1));
//            _circ_Corner_Radius_1_Sprite.SetPosition(6.01, 5);
//            Assert.False(CollisionDetector.AreColliding(_rect_10_By_10, _circ_Corner_Radius_1));
//            _circ_Corner_Radius_1_Sprite.SetPosition(5, 5.99);
//            Assert.That(CollisionDetector.AreColliding(_rect_10_By_10, _circ_Corner_Radius_1));
//            _circ_Corner_Radius_1_Sprite.SetPosition(5, 6.01);
//            Assert.False(CollisionDetector.AreColliding(_rect_10_By_10, _circ_Corner_Radius_1));
//            _circ_Corner_Radius_1_Sprite.SetPosition(5.2, 5.2);
//            Assert.That(CollisionDetector.AreColliding(_rect_10_By_10, _circ_Corner_Radius_1));
//            _circ_Corner_Radius_1_Sprite.SetPosition(5.707, 5.707); // Barely overlapping corner
//            Assert.That(CollisionDetector.AreColliding(_rect_10_By_10, _circ_Corner_Radius_1));
//            _circ_Corner_Radius_1_Sprite.SetPosition(5.7073, 5.7073); // Barely missing corner
//            Assert.False(CollisionDetector.AreColliding(_rect_10_By_10, _circ_Corner_Radius_1));
//        }

//    }
//}
