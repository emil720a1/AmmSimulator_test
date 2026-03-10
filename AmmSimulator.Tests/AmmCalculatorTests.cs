using AmmSimulator.Core;
using AmmSimulator.Models;
using Xunit;

namespace AmmSimulator.Tests;

public class AmmCalculatorTests
{
    [Fact]
    public void ExecuteSwap_AToB_CalculatesCorrectAmountOut()
    {
        // Arrange
        var calculator = new AmmCalculator();
        var pool = new LiquidityPool(1000m, 1000m, fee: 0.003m);
        var request = new SwapInput(10m, SwapDirection.AToB);
        
        var result = calculator.ExecuteSwap(pool, request);

        Assert.Equal(9.8716m, result.AmountOut, precision: 4);

        Assert.Equal(1010m, result.NewReserveA);
        Assert.Equal(1000m - result.AmountOut, result.NewReserveB);
    }
    
    [Fact]
    public void ExecuteSwap_BToA_CalculatesCorrectAmountOut()
    {
        // Arrange
        var calculator = new AmmCalculator();
        var pool = new LiquidityPool(1000m, 1000m, fee: 0.003m);
        var request = new SwapInput(10m, SwapDirection.BToA);
        
        var result = calculator.ExecuteSwap(pool, request);

        Assert.Equal(9.8716m, result.AmountOut, precision: 4);
        Assert.Equal(1010m, result.NewReserveB);
        Assert.Equal(1000m - result.AmountOut, result.NewReserveA);
    }

    [Fact]
    public void ExecuteSwap_MaintainsOrIncreasesKInvariant()
    {
        // Arrange
        var calculator = new AmmCalculator();
        var pool = new LiquidityPool(100000m, 100000m, fee: 0.003m);
        var request = new SwapInput(5000m, SwapDirection.AToB);

        decimal initialK = pool.ReserveA * pool.ReserveB;
        
        var result = calculator.ExecuteSwap(pool, request);
        decimal newK = result.NewReserveA * result.NewReserveB;
        
        
        Assert.True(newK >= initialK, "The K invariant should not decrease after a swap with fees.");
    }
}