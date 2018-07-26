#include <iostream>
#include <vector>
#include <string>

using namespace std;

#define matrix_t vector< vector<int> >

int calculate(matrix_t& dp, vector<int> numbers, int i, int j, vector<int>& result){
    if (dp[i][j] != -1){
        result.push_back(numbers[i]);
        return dp[i][j];
    }
    if (j == 0)
        return 1;
    if (j == numbers[i]){
        dp[i][j] = 1;
        result.push_back(numbers[i]);
        return dp[i][j];
    }
        
    if (i == 0){
        if (j > numbers[i] || j < numbers[i])
            dp[i][j] = 0;
        else {
            dp[i][j] = 1;
            result.push_back(numbers[i]);
        }
        return dp[i][j];
    }
    else
        if (j >= numbers[i]){
            dp[i][j] = calculate(dp, numbers, i - 1, j - numbers[i], result) + 1;
            result.push_back(numbers[i]);
        }
        else
            dp[i][j] = calculate(dp, numbers, i - 1, j, result);
    return dp[i][j];
}

int main(){

    int n, k;
    cin >> n >> k;

    vector<int> numbers(n);
    vector<int> result;
    for (int i = 0; i < n; i++)
        cin >> numbers[i];

    matrix_t dp(n, vector<int>(k + 1, -1));
    calculate(dp, numbers, n - 1, k, result);

    for (int i = 0; i < result.size() - 1; i++)
        cout << result[i] << '+';
    cout << result[result.size() - 1] << '=' << k << endl;
    return 0;
}
