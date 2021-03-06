using System.Collections.Generic;
using Core.Shogi.Pieces;
using Xunit;

namespace Core.Shogi.Tests.Pieces
{
    
    public class BishopShould
    {
        [Fact]
        public void DescribeSimpleMovements()
        {
            var bishop = new Bishop(PlayerType.Black, "8h");
            string moveDescription = bishop.Move("6f");

            Assert.Equal("B8h-6f", moveDescription);
        }

        [Theory]
        [InlineData(PlayerType.Black, "1g", "1h")] //"NotMoveBackwardAsBlackPlayer")]
        [InlineData(PlayerType.White, "1c", "1b")] //"NotMoveBackwardAsWhitePlayer")]
        [InlineData(PlayerType.Black, "5g", "6g")] //"NotMoveLeftAsBlackPlayer")]
        [InlineData(PlayerType.White, "5c", "4c")] //"NotMoveLeftAsWhitePlayer")]
        [InlineData(PlayerType.Black, "5g", "4g")] //"NotMoveRightAsBlackPlayer")]
        [InlineData(PlayerType.White, "5c", "6c")] //"NotMoveRightAsWhitePlayer")]
        [InlineData(PlayerType.Black, "8g", "8f")] //"NotMoveForwardAsBlackPlayer")]
        [InlineData(PlayerType.White, "8c", "8d")] //"NotMoveForwardAsWhitePlayer")]
        public void NotAllowIllegalMoves(PlayerType player, string positionFrom, string positionTo)
        {
            var bishop = new Bishop(player, positionFrom);

            var isMoveLegal = bishop.IsMoveLegal(positionTo);

            Assert.False(isMoveLegal);
        }

        [Theory]
        [InlineData(PlayerType.Black, "5e", "9a")] //"BeAbleToMoveFromCentreToTopLeftCornerAsBlackPlayer")]
        [InlineData(PlayerType.White, "5e", "1i")] //"BeAbleToMoveFromCentreToTopLeftCornerAsWhitePlayer")]
        [InlineData(PlayerType.Black, "5e", "1a")] //"BeAbleToMoveFromCentreToTopRightCornerAsBlackPlayer")]
        [InlineData(PlayerType.White, "5e", "9i")] //"BeAbleToMoveFromCentreToTopRightCornerAsWhitePlayer")]
        [InlineData(PlayerType.Black, "5e", "9i")] //"BeAbleToMoveFromCentreToBottomLeftCornerAsBlackPlayer")]
        [InlineData(PlayerType.White, "5e", "1a")] //"BeAbleToMoveFromCentreToBottomLeftCornerAsWhitePlayer")]
        [InlineData(PlayerType.Black, "5e", "1i")] //"BeAbleToMoveFromCentreToBottomRightCornerAsBlackPlayer")]
        [InlineData(PlayerType.White, "5e", "9a")] //"BeAbleToMoveFromCentreToBottomRightCornerAsWhitePlayer")]
        public void AllowValidMoves(PlayerType player, string positionFrom, string positionTo)
        {
            var bishop = new Bishop(player, positionFrom);

            var isMoveLegal = bishop.IsMoveLegal(positionTo);

            Assert.True(isMoveLegal);
        }

        [Theory]
        [InlineData(PlayerType.Black, "5e",
             new string[]
             {
                 "5e4d", "5e6f", "5e4f", "5e6d", "5e3c", "5e7g", "5e3g", "5e7c", "5e2b", "5e8h", "5e2h", "5e8b", "5e1a",
                 "5e9i", "5e1i", "5e9a"
             })] //"AsBlackPlayerFromCentreOfBoard")]
        [InlineData(PlayerType.White, "5e",
             new string[]
             {
                 "5e4d", "5e6f", "5e4f", "5e6d", "5e3c", "5e7g", "5e3g", "5e7c", "5e2b", "5e8h", "5e2h", "5e8b", "5e1a",
                 "5e9i", "5e1i", "5e9a"
             })] //"AsWhitePlayerFromCentreOfBoard")]
        [InlineData(PlayerType.Black, "8c", new string[] { "8c7b", "8c9d", "8c7d", "8c9b", "8c6a", "8c6e", "8c5f", "8c4g", "8c3h", "8c2i" })] //"AsBlackPlayerFromNonSimmetricLocation")]
        public void KnowAllItsPossibleMoves(PlayerType player, string position,
            IEnumerable<string> expectedPossibleMovements)
        {
            var bishop = new Bishop(player, position);

            var possibleMovements = bishop.PossibleMovements;

            Assert.Equal(expectedPossibleMovements, possibleMovements);
        }
    }
}