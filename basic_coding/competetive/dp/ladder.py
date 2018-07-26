def calculate(n, dp):
    if n < 3:
        return 1
    if n == 3:
        return 2

    if dp[n - 1] == -1:
        dp[n - 1] = calculate(n - 1, dp)
        dp[n] = dp[n - 1]
    if n - 3 >= 0:
        if dp[n - 3] == -1:
            dp[n - 3] = calculate(n - 3, dp)
        dp[n] += dp[n - 3]

    return dp[n]

if __name__ == "__main__":
    n = input()
    dp = [-1 for i in range(n + 1)]

    if n > 0:
        print calculate(n, dp)
    else:
        print 0