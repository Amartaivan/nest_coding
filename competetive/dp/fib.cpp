#include <iostream>
#include <vector>

using namespace std;

#define fib_t unsigned long long

fib_t fib(int n, vector<fib_t>& dp){
    if (n <= 1)
        return 1;
    if (dp[n] == -1)
        dp[n] = (fib(n - 1, dp) + fib(n - 2, dp)) % 10000007;
    return dp[n];
}

int main(){

    fib_t n;
    cin >> n;
    vector<fib_t> dp(n + 1, -1);

    cout << fib(n, dp) << endl;

    return 0;
}