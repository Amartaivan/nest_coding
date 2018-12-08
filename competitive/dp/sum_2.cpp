#include <iostream>
#include <vector>

using namespace std;

int calculate(vector< vector<int> >& dp, vector<int> numbers, int i, int j){
    if (dp[i][j] != -1)
        return dp[i][j];
    if (j == 0)
        return 1;

    if (i == 0)
        if (j == numbers[i])
            dp[i][j] = 1;
        else if (j % numbers[i] == 0)
            dp[i][j] = calculate(dp, numbers, i, j - numbers[i]) + 1;
        else
            dp[i][j] = 0;
    else {
        if (j >= i)
            dp[i][j] = calculate(dp, numbers, i - 1, j) + calculate(dp, numbers, i, j - numbers[i]);
        else
            dp[i][j] = calculate(dp, numbers, i - 1, j);
    }
    return dp[i][j];
}

int main(){
    int n, k, result;
    cin >> n >> k;
    vector<int> numbers(n);

    for (int i = 0; i < n; i++)
        cin >> numbers[i];

    vector< vector<int> > dp(n, vector<int>(k + 1, -1));
    cout << calculate(dp, numbers, n - 1, k) << endl;

    return 0;
}