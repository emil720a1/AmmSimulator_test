# DEX AMM Simulator

A simple C# console app simulating token swaps using the constant-product formula (`x * y = k`), based on the Uniswap V2 model.

## Formulas Used
The logic relies on maintaining the pool's invariant. We apply a standard 0.3% fee to the input amount before calculating the output.

- `amountInWithFee = amountIn * 0.997`
- `amountOut = (reserveOut * amountInWithFee) / (reserveIn + amountInWithFee)`
- `spotPrice = reserveIn / reserveOut`
- `effectivePrice = amountIn / amountOut`
- `slippage % = ((effectivePrice - spotPrice) / spotPrice) * 100`

## Scenarios Results
Initial pool state: 100,000 Token A and 100,000 Token B. Swap direction: A -> B. 
Data generated directly by the simulator:

| Swap Size | Amount In (Token A) | Amount Out (Token B) | Effective Price | Slippage |
| --- | --- | --- | --- | --- |
| Small (1%) | 1000 | 987.16 | 1.0130 | 1.30% |
| Medium (10%) | 10000 | 9066.11 | 1.1030 | 10.30% |
| Large (40%) | 40000 | 28510.15 | 1.4030 | 40.30% |

*(Note: New pool reserves are tracked and printed in the console output).*

## Conclusions on Slippage Behavior
- **It scales exponentially, not linearly.** Swapping 1% of the pool gives a tiny ~1.3% slippage, but trying to drain 40% of the pool spikes the slippage to over 40%. 
- **Built-in pool protection.** The AMM math acts as a defense mechanism. It makes it practically impossible to completely empty the pool. As Token B becomes scarce, its price skyrockets.
- **Impact on trading/arbitrage.** Liquidity depth is everything. If you try to push a huge order through a "shallow" pool, the slippage will instantly eat all your expected profit.
