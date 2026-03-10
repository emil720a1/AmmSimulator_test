namespace AmmSimulator.Core;

public class LiquidityPool
{
    public decimal ReserveA { get; set; }
    public decimal ReserveB { get; set; }
    
    public decimal Fee { get; init; }

    public LiquidityPool(decimal reserveA, decimal reserveB, decimal fee = 0.003m)
    {
        ReserveA = reserveA;
        ReserveB = reserveB;
        Fee = fee;
    }
}