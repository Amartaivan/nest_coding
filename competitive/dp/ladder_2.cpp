#include <iostream>
#include <vector>

using namespace std;

#define long_t unsigned long long

long_t calculate(int n, vector<long_t>& dp){

    if (dp[n] != -1)
        return dp[n];

    if (n < 2)
        dp[n] = 1;
    else
        dp[n] = calculate(n - 1, dp) + calculate(n - 2, dp);

    return dp[n];
}

int main(){

    int n, k;
    cin >> n >> k;

    vector<long_t> ladder(n + 1, -1);
    ladder[k] = 0;

    cout << calculate(n, ladder) << endl;
    return 0;
}