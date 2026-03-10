namespace AmmSimulator.Models;

public record SwapResult(
    decimal AmountOut,
    decimal NewReserveA,
    decimal NewReserveB,
    decimal EffectivePrice,
    decimal SlippagePercentage); 