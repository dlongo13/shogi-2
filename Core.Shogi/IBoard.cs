﻿using Core.Shogi.BitVersion;

namespace Core.Shogi
{
    public interface IBoard
    {
        void Reset();
        FullBitboardState BitboardState { get; }
    }
}