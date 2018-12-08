#include <iostream>
#include <vector>

using namespace std;

#define long_t unsigned long long


long_t calculate(int n, vector<long_t>& dp){
    if (n < 3)
        return 1;

    if (dp[n - 1] == -1)
        dp[n - 1] = calculate(n - 1, dp);
    if (dp[n - 3] == -1)
        dp[n - 3] = calculate(n - 3, dp);
    
    dp[n] = dp[n - 1] + dp[n - 3];
    return dp[n];
}

/* Teacher's solution:
long_t calculate(int n, vector<long_t>& dp){

    if (dp[n] != -1)
        return dp[n];

    if (n < 3)
        dp[n] = 1;
    else
        dp[n] = calculate(n - 1, dp) + calculate(n - 3, dp);

    return dp[n];
}
*/

int main(){

    int n;
    cin >> n;
    vector<long_t> dp(n + 1, -1);

    cout << calculate(n, dp) << endl;
    return 0;
}