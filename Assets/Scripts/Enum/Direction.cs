using System;
using UnityEngine;

public enum Direction {
    UP,
    DOWN,
    LEFT,
    RIGHT,
    UPLEFT,
    UPRIGHT,
    DOWNLEFT,
    DOWNRIGHT
}


static class DirectionExtMethods {
    public static Vector2 GetVector2(this Direction dir) {
        if (dir == Direction.UP) return Vector2.up;
        if (dir == Direction.DOWN) return Vector2.down;
        if (dir == Direction.LEFT) return Vector2.left;
        if (dir == Direction.RIGHT) return Vector2.right;
        if (dir == Direction.UPLEFT) return (Vector2.up + Vector2.left).normalized;
        if (dir == Direction.UPRIGHT) return (Vector2.up + Vector2.right).normalized;
        if (dir == Direction.DOWNLEFT) return (Vector2.down + Vector2.left).normalized;
        if (dir == Direction.DOWNRIGHT) return (Vector2.down + Vector2.right).normalized;
        throw new Exception("Invalid direction");
    }
}