#include <iostream>
#include <vector>

using namespace std;

#define matrix_t vector< vector<int> >

int calculate(matrix_t& dp, vector<int> numbers, int i, int j){
    if (dp[i][j] != -1)
        return dp[i][j];
    if (j == 0)
        return 1;
    if (j == numbers[i]){
        dp[i][j] = 1;
        return dp[i][j];
    }
        
    if (i == 0){
        if (j > numbers[i] || j < numbers[i])
            dp[i][j] = 0;
        else
            dp[i][j] = 1;
        return dp[i][j];
    }
    else
        if (j >= numbers[i])
            dp[i][j] = calculate(dp, numbers, i - 1, j - numbers[i]) + 1;
        else
            dp[i][j] = calculate(dp, numbers, i - 1, j);
    return dp[i][j];
}

int main(){

    int n, k;
    cin >> n >> k;

    vector<int> numbers(n);
    for (int i = 0; i < n; i++)
        cin >> numbers[i];

    matrix_t dp(n, vector<int>(k + 1, -1));
    cout << calculate(dp, numbers, n - 1, k) << endl;

    for (auto a : dp){
        for (int b : a)
            cout << b << '\t';
        cout << endl;
    }

    return 0;
}