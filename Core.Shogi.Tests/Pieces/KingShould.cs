﻿using System.Collections.Generic;
using Core.Shogi.Pieces;
using NUnit.Framework;

namespace Core.Shogi.Tests.Pieces
{
    [TestFixture]
    public class KingShould
    {
        [Test]
        public void Have_K_AsShortName()
        {
            var king = new King(Player.Black, "5i");

            Assert.AreEqual('K', king.ShortName);
        }

        [TestCase(Player.Black, "7i", "7h", TestName = "BeAbleToMoveForwardAsBlackPlayer")]
        [TestCase(Player.White, "7a", "7b", TestName = "BeAbleToMoveForwardAsWhitePlayer")]
        [TestCase(Player.Black, "7i", "8h", TestName = "BeAbleToMoveForwardLeftAsBlackPlayer")]
        [TestCase(Player.White, "7a", "6b", TestName = "BeAbleToMoveForwardLeftAsWhitePlayer")]
        [TestCase(Player.Black, "5e", "6f", TestName = "BeAbleToMoveBackLeftAsBlackPlayer")]
        [TestCase(Player.White, "5e", "4d", TestName = "BeAbleToMoveBackLeftAsWhitePlayer")]
        [TestCase(Player.Black, "6h", "6i", TestName = "BeAbleToMoveBackAsBlackPlayer")]
        [TestCase(Player.White, "6b", "6a", TestName = "BeAbleToMoveBackAsWhitePlayer")]
        [TestCase(Player.Black, "5e", "4f", TestName = "BeAbleToBackRightAsBlackPlayer")]
        [TestCase(Player.White, "5e", "6d", TestName = "BeAbleToBackRightAsWhitePlayer")]
        [TestCase(Player.Black, "7i", "6h", TestName = "BeAbleToMoveForwardRightAsBlackPlayer")]
        [TestCase(Player.White, "7a", "8b", TestName = "BeAbleToMoveForwardRightAsWhitePlayer")]
        [TestCase(Player.Black, "5e", "6e", TestName = "BeAbleToMoveLeftAsBlackPlayer")]
        [TestCase(Player.White, "5e", "4e", TestName = "BeAbleToMoveLeftAsWhitePlayer")]
        [TestCase(Player.Black, "5e", "4e", TestName = "BeAbleToMoveRightAsBlackPlayer")]
        [TestCase(Player.White, "5e", "6e", TestName = "BeAbleToMoveRightAsWhitePlayer")]
        public void AllowValidMoves(Player player, string positionFrom, string positionTo)
        {
            var king = new King(player, positionFrom);

            var isMoveLegal = king.IsMoveLegal(positionTo);

            Assert.IsTrue(isMoveLegal);
        }

        [TestCase(Player.Black, "5e", new string[] {"5e5d", "5e5f", "5e4e", "5e6e", "5e6d", "5e4d", "5e6f", "5e4f"},
             TestName = "AsBlackPlayer")]
        [TestCase(Player.White, "5e", new string[] {"5e5d", "5e5f", "5e4e", "5e6e", "5e6f", "5e4f", "5e6d", "5e4d"},
             TestName = "AsWhitePlayer")]
        public void KnowAllItsPossibleMoves(Player player, string position,
            IEnumerable<string> expectedPossibleMovements)
        {
            var king = new King(player, position);

            var possibleMovements = king.PossibleMovements;

            Assert.AreEqual(expectedPossibleMovements, possibleMovements);
        }
    }
}