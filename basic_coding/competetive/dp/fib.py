def fib(n, dp):
    if n <= 1:
        return 1
    if dp[n] == -1:
        dp[n] = fib(n - 1, dp) + fib(n - 2, dp);
    return dp[n]

if __name__ == "__main__":
    n = input()
    dp = [-1 for i in range(n + 1)]

    print fib(n, dp)