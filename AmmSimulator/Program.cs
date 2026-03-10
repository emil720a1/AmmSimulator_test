

using AmmSimulator.Core;
using AmmSimulator.Models;

var calculator = new AmmCalculator();

RunScenario(calculator, "Scenario 1: Small Swap (1%)", 1000m);

RunScenario(calculator, "Scenario 2: Medium Swap (10%)", 10000m);

RunScenario(calculator, "Scenario 3: Large Swap (40%)", 40000m);

static void RunScenario(AmmCalculator calc, string scenarioName, decimal amountIn)
{
    var pool = new LiquidityPool(reserveA: 100000m, reserveB: 100000m, fee: 0.003m);

    var request = new SwapInput(AmountIn: amountIn, Direction: SwapDirection.AToB);

    var result = calc.ExecuteSwap(pool, request);
    
    
    Console.WriteLine($"--- {scenarioName} ---");
    Console.WriteLine($"Amount In:        {amountIn} Token A");
    Console.WriteLine($"Amount Out:       {result.AmountOut:F2} Token B");
    Console.WriteLine($"Effective Price:  {result.EffectivePrice:F4} A per B");
    Console.WriteLine($"Slippage:         {result.SlippagePercentage:F2} %");
    Console.WriteLine($"New Reserve A:    {result.NewReserveA:F2}");
    Console.WriteLine($"New Reserve B:    {result.NewReserveB:F2}");
    Console.WriteLine();
}
