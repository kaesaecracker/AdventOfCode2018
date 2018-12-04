namespace ClaimOverlaps
{
    public struct Claim
    {
        public int id;
        public int x, y;
        public int width, height;

        public int x2 => this.x + this.width -1;
        public int y2 => this.y + this.height -1;

        public override string ToString() => $"[#{id}: ({x}|{y}), {this.width}*{this.height}]";
    }
}