using AmmSimulator.Models;

namespace AmmSimulator.Core;

public class AmmCalculator
{
    public SwapResult ExecuteSwap(LiquidityPool pool, SwapInput request)
    {
        // Determine the active reserves based on the trade direction
        decimal reserveIn;
        decimal reserveOut;

        if (request.Direction == SwapDirection.AToB)
        {
            reserveIn = pool.ReserveA;
            reserveOut = pool.ReserveB;
        }
        else
        {
            reserveIn = pool.ReserveB;
            reserveOut = pool.ReserveA;
        }
        
        // Calculate the input amount after applying the liquidity provider fee
        decimal amountInWithFee = request.AmountIn * (1 - pool.Fee);

        // Apply the constant product formula (x * y = k) to calculate the output amount
        decimal amountOut = (reserveOut * amountInWithFee) / (reserveIn + amountInWithFee);
        
        // Calculate the slippage percentage
        decimal spotPrice = reserveIn / reserveOut;
        decimal effectivePrice = request.AmountIn / amountOut;
        decimal slippagePercentage = (effectivePrice - spotPrice) / spotPrice * 100m;

        // Update the pool's global state.
        if (request.Direction == SwapDirection.AToB)
        {
            pool.ReserveA += request.AmountIn;
            pool.ReserveB -= amountOut;
        }
        else
        {
            pool.ReserveB += request.AmountIn;
            pool.ReserveA -= amountOut;
        }

        return new SwapResult(
            AmountOut: amountOut,
            NewReserveA: pool.ReserveA,
            NewReserveB: pool.ReserveB,
            EffectivePrice: effectivePrice,
            SlippagePercentage: slippagePercentage
            );
    }
}